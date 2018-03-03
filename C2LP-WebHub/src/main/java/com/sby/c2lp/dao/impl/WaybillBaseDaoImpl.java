package com.sby.c2lp.dao.impl;

import com.sby.c2lp.dao.WaybillBaseDao;
import com.sby.c2lp.model.WaybillBase;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.dao.DataAccessException;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.jdbc.core.ResultSetExtractor;
import org.springframework.jdbc.core.RowMapper;
import org.springframework.stereotype.Service;

import javax.sql.DataSource;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.List;

/**
 * Created by wanghe on 2016/8/4.
 */
@Service
public class WaybillBaseDaoImpl implements WaybillBaseDao{
    private JdbcTemplate template;

    @Autowired
    public void setDataSource(DataSource dataSource) {
        this.template = new JdbcTemplate(dataSource);
    }

    @Override
    public WaybillBase WaybillObject(String number,Integer customerId,Integer role) {
        String sql="";
        Object[] obj = new Object[]{};
        if(role==1){
            sql = "select * from waybill_base where number=?";
            obj = new Object[]{number};
        }/*else{
            sql = "select * from waybill_base where number=? and (senderId=? or receiverId=?)";
            obj = new Object[]{number,customerId,customerId};
        }*/
        if(role==2){
            sql = "select * from waybill_base where number=? and senderId=?";
            obj = new Object[]{number,customerId};
        }
        if(role==3){
            sql = "select * from waybill_base where number=? and receiverId=?";
            obj = new Object[]{number,customerId};
        }
        return template.query(sql, obj, new ResultSetExtractor<WaybillBase>() {
            @Override
            public WaybillBase extractData(ResultSet rs) throws SQLException, DataAccessException {
                if(rs.next()){
                    WaybillBase waybillBase = new WaybillBase();
                    waybillBase.setId(rs.getInt("id"));
                    waybillBase.setNumber(rs.getString("number"));
                    waybillBase.setSenderId(rs.getInt("senderId"));
                    waybillBase.setSenderOrg(rs.getString("senderOrg"));
                    waybillBase.setSenderPerson(rs.getString("senderPerson"));
                    waybillBase.setSenderTel(rs.getString("senderTel"));
                    waybillBase.setSenderAddress(rs.getString("senderAddress"));
                    waybillBase.setReceiverId(rs.getInt("receiverId"));
                    waybillBase.setReceiverOrg(rs.getString("receiverOrg"));
                    waybillBase.setReceiverPerson(rs.getString("receiverPerson"));
                    waybillBase.setReceiverTel(rs.getString("receiverTel"));
                    waybillBase.setReceiverAddress(rs.getString("receiverAddress"));
                    waybillBase.setBillingCount(rs.getInt("billingCount"));
                    waybillBase.setStage(rs.getInt("stage"));
                    waybillBase.setBeginAt(rs.getTimestamp("beginAt"));
                    waybillBase.setSigninAt(rs.getTimestamp("signinAt"));
                    waybillBase.setPicPostbackAt(rs.getTimestamp("picPostbackAt"));
                    return waybillBase;
                }
                return null;
            }
        });
    }

    @Override
    public List<WaybillBase> WabillPicture(Long id) {
        String sql="select picName from waybill_postback_pic where baseId=?";
        return template.query(sql,new Object[]{id},new RowMapper<WaybillBase>() {
            @Override
            public WaybillBase mapRow(ResultSet rs, int i) throws SQLException {
                WaybillBase waybillBase = new WaybillBase();
                waybillBase.setPicName(rs.getString("picName"));
                return waybillBase;
            }
        });
    }
}
