package com.sby.c2lp.dao.impl;

import com.sby.c2lp.dao.NumberQueryDao;
import com.sby.c2lp.model.WaybillBase;
import com.sby.c2lp.model.WaybillNode;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.dao.DataAccessException;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.jdbc.core.ResultSetExtractor;
import org.springframework.jdbc.core.RowMapper;
import org.springframework.stereotype.Repository;

import javax.sql.DataSource;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Timestamp;
import java.util.List;
import java.util.Map;

/**
 * Created by wanghe on 2016/8/1.
 */
@Repository
public class NumberQueryDaoImpl implements NumberQueryDao {
    private JdbcTemplate template;

    @Autowired
    public void setDataSource(DataSource dataSource) {
        this.template = new JdbcTemplate(dataSource);
    }

    @Override
    public List<WaybillNode> findNumberList(String number,Integer customerId,Integer role) {
        String sql="";
        if(role==1){
            sql="select * from waybill_node wn inner join waybill_base wb on wn.baseId=wb.id where wb.number=? order by wn.operateAt desc";
        }/*else {
            sql="select * from waybill_node wn inner join waybill_base wb on wn.baseId=wb.id where wb.number=? and (wb.senderId="+customerId+    " or receiverId="+customerId+     ") order by wn.operateAt desc";
        }*/
        if(role==2){
            sql="select * from waybill_node wn inner join waybill_base wb on wn.baseId=wb.id where wb.number=? and wb.senderId="+customerId+    " order by wn.operateAt desc";
        }
        if(role==3){
            sql="select * from waybill_node wn inner join waybill_base wb on wn.baseId=wb.id where wb.number=? and wb.receiverId="+customerId+" order by wn.operateAt desc";
        }
        return template.query(sql, new Object[]{number}, new NumberQueryList());

    }

    @Override
    public WaybillBase sanfangnumber(String number,Integer customerId,Integer role) {
        String sql="";
        Object[] obj = new Object[]{};
        if(role==1){
            sql = "select base.number basenumber from waybill_base base inner join huadong_tmsorder_waybillbase hua on base.number=hua.number where hua.relationId=?";
            obj = new Object[]{number};
        }/*else {
            sql = "select base.number basenumber from waybill_base base inner join huadong_tmsorder_waybillbase hua on base.number=hua.number where hua.relationId=? and (senderId=? or receiverId=?)";
            obj = new Object[]{number,customerId,customerId};
        }*/
        if(role==2){
            sql = "select base.number basenumber from waybill_base base inner join huadong_tmsorder_waybillbase hua on base.number=hua.number where hua.relationId=? and senderId=?";
            obj = new Object[]{number,customerId};
        }
        if(role==3){
            sql = "select base.number basenumber from waybill_base base inner join huadong_tmsorder_waybillbase hua on base.number=hua.number where hua.relationId=? and receiverId=?";
            obj = new Object[]{number,customerId};
        }
        return template.query(sql,obj,new ResultSetExtractor<WaybillBase>() {
            @Override
            public WaybillBase extractData(ResultSet rs) throws SQLException, DataAccessException {
                if (rs.next()) {
                    WaybillBase way = new WaybillBase();
                    way.setNumber(rs.getString("basenumber"));
                    return way;
                } else {
                    return null;
                }
            }
        });
    }

    @Override
    public WaybillNode findstartTime(String number,Long id) {
        String sql="select * from waybill_base wb inner join waybill_node wn on wn.baseId=wb.id where wb.number=? and wn.id=? order by wn.operateAt";
        return template.query(sql,new Object[]{number,id},new ResultSetExtractor<WaybillNode>() {
            @Override
            public WaybillNode extractData(ResultSet rs) throws SQLException, DataAccessException {
                if (rs.next()) {
                    WaybillNode numberQuery = new WaybillNode();
                    numberQuery.setNumber(rs.getString("number"));
                    numberQuery.setNodeid(rs.getLong("id"));
                    numberQuery.setOperateAt(rs.getTimestamp("operateAt"));
                    return numberQuery;
                } else {
                    return null;
                }
            }
        });
    }

    @Override
    public WaybillNode findendTime(String number, Timestamp operateAt) {
        String sql="select * from waybill_base wb inner join waybill_node wn on wn.baseId=wb.id where  wb.number=? and wn.operateAt>? order by wn.operateAt asc LIMIT 0,1";
        return template.query(sql,new Object[]{number,operateAt},new ResultSetExtractor<WaybillNode>() {
            @Override
            public WaybillNode extractData(ResultSet rs) throws SQLException, DataAccessException {
                if (rs.next()) {
                    WaybillNode numberQuery = new WaybillNode();
                    numberQuery.setNumber(rs.getString("number"));
                    numberQuery.setNodeid(rs.getLong("id"));
                    numberQuery.setOperateAt(rs.getTimestamp("operateAt"));
                    return numberQuery;
                } else {
                    return null;
                }
            }
        });
    }


    class NumberQueryList implements RowMapper<WaybillNode> {
        @Override
        public WaybillNode mapRow(ResultSet rs, int i) throws SQLException {
            WaybillNode numberquery = new WaybillNode();
            numberquery.setNumber(rs.getString("number"));
            numberquery.setStorageId(rs.getInt("storageId"));
            numberquery.setContent(rs.getString("content"));
            numberquery.setOperateAt(rs.getTimestamp("operateAt"));
            numberquery.setReceiverOrg(rs.getString("receiverOrg"));
            numberquery.setSenderOrg(rs.getString("senderOrg"));
            numberquery.setNodeid(rs.getLong("id"));
            numberquery.setArrived(rs.getInt("arrived"));
            numberquery.setStorageName(rs.getString("storageName"));
            return numberquery;
        }
    }

}