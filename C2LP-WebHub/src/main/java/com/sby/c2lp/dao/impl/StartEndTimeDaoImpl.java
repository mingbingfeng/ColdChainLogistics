package com.sby.c2lp.dao.impl;

import com.sby.c2lp.dao.StartEndTimeDao;
import com.sby.c2lp.model.VisitRecord;
import com.sby.c2lp.model.WaybillBase;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.jdbc.core.RowMapper;
import org.springframework.stereotype.Repository;

import javax.sql.DataSource;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Timestamp;
import java.text.SimpleDateFormat;
import java.util.List;
import java.util.Map;

/**
 * Created by wanghe on 2016/8/8.
 */
@Repository
public class StartEndTimeDaoImpl implements StartEndTimeDao {
    private JdbcTemplate template;
    @Autowired
    public void setDataSource(DataSource dataSource) {
        this.template = new JdbcTemplate(dataSource);
    }

    @Override
    public List<Map<String,Object>> waybillCount(String beginAt, String signinAt,String senderId){
        String sender = "0".equals(senderId)? "true" : "senderId="+senderId;
        String sql="SELECT senderOrg,count(1) count,sum(billingCount) sum from waybill_base WHERE "+ sender+ " and ((beginAt between '"+beginAt+"' and '"+signinAt+"' or signinAt between '"+beginAt+"' and '"+signinAt+"') or (beginAt<'"+beginAt+"' and signinAt>'"+signinAt+"'))"+" GROUP BY senderId ORDER BY count";
        return template.queryForList(sql);
    }

    @Override
    public List<Map<String,Object>> waybillCountCar(String beginAt, String signinAt, String carId){
        String car = "0".equals(carId)? "" : "where cn.id="+carId;
        String sql="SELECT s.storageId,cn.storageName,count(1) count from (SELECT storageId from waybill_node n JOIN coldstorage c ON n.storageId=c.id WHERE  n.operateAt between '"+beginAt+"' and '"+signinAt+"' and c.storageType=2 GROUP BY baseId , storageId) s JOIN coldstorage cn on s.storageId=cn.id "+car+" GROUP BY s.storageId ORDER BY count";
        return template.queryForList(sql);
    }

    @Override
    public List<WaybillBase> listTimeQuery(String beginAt, String signinAt,String carId,String senderId,Integer customerId,Integer role) {
        String sql="";
        if(role==1){
            String car = carId.equals("0") ? "true" : "id in( select baseId from waybill_node WHERE storageId="+carId+ ")" ;
            String sender = senderId.equals("0") ? "true" : "senderId="+senderId;
            sql="select * from waybill_base where "+car+" and "+sender+" and ((beginAt between '"+beginAt+"' and '"+signinAt+"' or signinAt between '"+beginAt+"' and '"+signinAt+"') or (beginAt<'"+beginAt+"' and signinAt>'"+signinAt+"'))";
        }/*else {
            sql="select * from waybill_base where (senderId="+customerId+" or receiverId="+customerId+") and ((beginAt between '"+beginAt+"' and '"+signinAt+"' or signinAt between '"+beginAt+"' and '"+signinAt+"') or (beginAt<'"+beginAt+"' and signinAt>'"+signinAt+"'))";
        }*/
        if(role==2){
            sql="select * from waybill_base where senderId="+customerId+" and ((beginAt between '"+beginAt+"' and '"+signinAt+"' or signinAt between '"+beginAt+"' and '"+signinAt+"') or (beginAt<'"+beginAt+"' and signinAt>'"+signinAt+"'))";
        }
        if(role==3){
            sql="select * from waybill_base where receiverId="+customerId+" and ((beginAt between '"+beginAt+"' and '"+signinAt+"' or signinAt between '"+beginAt+"' and '"+signinAt+"') or (beginAt<'"+beginAt+"' and signinAt>'"+signinAt+"'))";
        }
        return template.query(sql,new RowMapper<WaybillBase>() {
            @Override
            public WaybillBase mapRow(ResultSet rs, int i) throws SQLException {
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
        });
    }

    @Override
    public List<VisitRecord> visitRecordList(String customerId,String beginAt,String signinAt){
            String sql = "SELECT t.fullname,SUM(t.count) sum,MAX(case t.visitType when 0 then t.count else 0 end) as 'wechat' ,MAX(case t.visitType when 1 then t.count else 0 end) as 'pc' from (\n" +
                    "SELECT customerId,fullname,COUNT(1) count,visitType FROM visitRecord WHERE (customerId = "+customerId+" or "+customerId+" =0) and visitTime BETWEEN '" +beginAt+"' and '"+signinAt+"' GROUP BY customerId,visitType ) t GROUP BY t.customerId";
        return this.template.query(sql, new RowMapper<VisitRecord>() {
            @Override
            public VisitRecord mapRow(ResultSet resultSet, int i) throws SQLException {
                VisitRecord v = new VisitRecord();
                v.setFullName(resultSet.getString("fullname"));
                v.setWechat(resultSet.getInt("wechat"));
                v.setPc(resultSet.getInt("pc"));
                return v;
            }
        });


    }

}
