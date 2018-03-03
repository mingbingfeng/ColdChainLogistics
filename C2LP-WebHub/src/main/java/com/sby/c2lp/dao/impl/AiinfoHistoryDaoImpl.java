package com.sby.c2lp.dao.impl;

import com.sby.c2lp.dao.AiinfoHistoryDao;
import com.sby.c2lp.model.Aiinfo;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.jdbc.core.RowMapper;
import org.springframework.stereotype.Repository;

import javax.sql.DataSource;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Timestamp;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.List;

/**
 * Created by wanghe on 2016/8/5.
 */
@Repository
public class AiinfoHistoryDaoImpl implements AiinfoHistoryDao {


    Logger logger = LoggerFactory.getLogger(AiinfoHistoryDaoImpl.class);

    private JdbcTemplate template;

    @Autowired
    public void setDataSource(DataSource dataSource) {
        this.template = new JdbcTemplate(dataSource);
    }

    @Override
    public List<Object[]> findAiinfoHistoryTable(String tableName, String pointName, final List<Aiinfo> AiList, Timestamp startTime, Timestamp endTime,Integer pageAge,Integer pageNum) {
        String sql = " select  " + pointName + "  from  " +
                "(select a.pointName, a.pointId, d.data, d.dataTime, d.isAlarm  from aiinfo a join " + tableName + " d on a.pointId  = d.pointId where (a.pointType=1 or a.pointType=2) and "+
                "d.dataTime between '"+ startTime + "' and  '"+ endTime + "' ) t1 GROUP BY t1.dataTime order by t1.dataTime limit "+pageNum+","+pageAge+";";
        final SimpleDateFormat dateFormat = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
        List<Object[]> list = template.query(sql, new RowMapper<Object[]>() {
            @Override
            public Object[] mapRow(ResultSet rs, int i) throws SQLException {
                Object[] obj = new Object[AiList.size() + 2];
                obj[0] = dateFormat.format(rs.getTimestamp("dataTime"));
                obj[AiList.size()+1] = rs.getInt("isAlarm");
                for (int j = 0; j < AiList.size(); j++) {
                    if(rs.getString(AiList.get(j).getPointName()) != null) {
                        obj[j + 1] = rs.getDouble(AiList.get(j).getPointName());
                    } else {
                        obj[j + 1] = null;
                    }
                }
                return obj;
            }
        });
        return list;
    }
    @Override
    public List<Object[]> findAiinfoHistoryTables(String tableName, String pointName, final List<Aiinfo> AiList, Timestamp startTime, Timestamp endTime) {
        String sql = " select  " + pointName + "  from  " +
                "(select a.pointName, a.pointId, d.data, d.dataTime, d.isAlarm  from aiinfo a join " + tableName + " d on a.pointId  = d.pointId where (a.pointType=1 or a.pointType=2) and "+
                "d.dataTime between '"+ startTime + "' and  '"+ endTime + "' ) t1 GROUP BY t1.dataTime order by t1.dataTime";
        final SimpleDateFormat dateFormat = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
        List<Object[]> list = template.query(sql, new RowMapper<Object[]>() {
            @Override
            public Object[] mapRow(ResultSet rs, int i) throws SQLException {
                Object[] obj = new Object[AiList.size() + 2];
                obj[0] = dateFormat.format(rs.getTimestamp("dataTime"));
                obj[AiList.size()+1] = rs.getInt("isAlarm");
                for (int j = 0; j < AiList.size(); j++) {
                    if(rs.getString(AiList.get(j).getPointName()) != null) {
                        obj[j + 1] = rs.getDouble(AiList.get(j).getPointName());
                    } else {
                        obj[j + 1] = null;
                    }
                }
                return obj;
            }
        });
        return list;
    }
    @Override
    public List<Object[]> findLongitudeWithLatitudeData(String tableName, String pointName, final List<Aiinfo> AiList, Timestamp startTime, Timestamp endTime) {
        String pointTypeSQL =  "(a.pointType=3 or a.pointType=4)";
        String sql = " select  " + pointName + "  from  " +
                "(select  a.pointId, d.data, d.dataTime, d.isAlarm  from aiinfo a join " + tableName + " d on a.pointId  = d.pointId where " + pointTypeSQL + " and "+
                "d.dataTime between '"+ startTime + "' and  '"+ endTime + "' ) t1 GROUP BY t1.dataTime order by t1.dataTime ;";
        final SimpleDateFormat dateFormat = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
        List<Object[]> list = template.query(sql, new RowMapper<Object[]>() {
            @Override
            public Object[] mapRow(ResultSet rs, int i) throws SQLException {
                Object[] obj = new Object[AiList.size() + 2];
                obj[0] = dateFormat.format(rs.getTimestamp("dataTime"));
                obj[AiList.size()+1] = rs.getInt("isAlarm");
                for (int j = 0; j < AiList.size(); j++) {
                    if(rs.getString(AiList.get(j).getPointId().toString()) != null) {
                        obj[j + 1] = rs.getDouble(AiList.get(j).getPointId().toString());
                    } else {
                        obj[j + 1] = null;
                    }
                }
                return obj;
            }
        });
        return list;
    }
    @Override
    public List<Object[]> findAiinfoHistoryTablePdf(String tableName, String pointName, final List<Aiinfo> AiList, Timestamp startTime, Timestamp endTime) {
        String sql = " select  " + pointName + "  from  " +
                "(select a.pointName, a.pointId, d.data, d.dataTime, d.isAlarm  from aiinfo a join " + tableName + " d on a.pointId  = d.pointId where (a.pointType=1 or a.pointType=2) and "+
                "d.dataTime between '"+ startTime + "' and  '"+ endTime + "' ) t1 GROUP BY t1.dataTime order by t1.dataTime;";
        final SimpleDateFormat dateFormat = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
        List<Object[]> list = template.query(sql, new RowMapper<Object[]>() {
            @Override
            public Object[] mapRow(ResultSet rs, int i) throws SQLException {
                Object[] obj = new Object[AiList.size() + 2];
                obj[0] = dateFormat.format(rs.getTimestamp("dataTime"));
                obj[AiList.size()+1] = rs.getInt("isAlarm");
                for (int j = 0; j < AiList.size(); j++) {
                    if(rs.getString(AiList.get(j).getPointName()) != null) {
                        obj[j + 1] = rs.getDouble(AiList.get(j).getPointName());
                    } else {
                        obj[j + 1] = null;
                    }
                }
                return obj;
            }
        });
        return list;
    }

    @Override
    public List<Object []> findLastLongitudeWithLatitudeData(String tableName,String pointName,final List<Aiinfo> AiList,String startTime){
        final SimpleDateFormat dateFormat = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
        String sql = " select  " + pointName + "  from  " +
                "(select  a.pointId, d.data, d.dataTime, d.isAlarm  from aiinfo a join " + tableName + " d on a.pointId  = d.pointId where d.dataTime > "
                + startTime + " ) t1 GROUP BY t1.dataTime order by t1.dataTime ";
        List<Object[]> list = template.query(sql, new RowMapper<Object[]>() {
            @Override
            public Object[] mapRow(ResultSet rs, int i) throws SQLException {
                Object[] obj = new Object[AiList.size() + 2];
                obj[0] = dateFormat.format(rs.getTimestamp("dataTime"));
                obj[AiList.size()+1] = rs.getInt("isAlarm");
                for (int j = 0; j < AiList.size(); j++) {
                    if(rs.getString(AiList.get(j).getPointId().toString()) != null) {
                        obj[j + 1] = rs.getDouble(AiList.get(j).getPointId().toString());
                    } else {
                        obj[j + 1] = null;
                    }
                }
                return obj;
            }
        });
        return list;
    }

    @Override
    public List<Object[]> coldchainpaging(String tableName, String pointName, final List<Aiinfo> AiList, Timestamp startTime, Timestamp endTime,Integer pageAge,Integer pageNum) {
        String sql = " select  " + pointName + "  from  " +
                "(select a.pointName, a.pointId, d.data, d.dataTime, d.isAlarm  from aiinfo a join " + tableName + " d on a.pointId  = d.pointId where (a.pointType=1 or a.pointType=2) and "+
                "d.dataTime between '"+ startTime + "' and  '"+ endTime + "' ) t1 GROUP BY t1.dataTime order by t1.dataTime limit "+pageNum+","+pageAge+";";
        final SimpleDateFormat dateFormat = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
        List<Object[]> list = template.query(sql, new RowMapper<Object[]>() {
            @Override
            public Object[] mapRow(ResultSet rs, int i) throws SQLException {
                Object[] obj = new Object[AiList.size() + 2];
                obj[0] = dateFormat.format(rs.getTimestamp("dataTime"));
                obj[AiList.size()+1] = rs.getInt("isAlarm");
                for (int j = 0; j < AiList.size(); j++) {
                    if(rs.getString(AiList.get(j).getPointName()) != null) {
                        obj[j + 1] = rs.getDouble(AiList.get(j).getPointName());
                    } else {
                        obj[j + 1] = null;
                    }
                }
                return obj;
            }
        });
        return list;
    }

    @Override
    public Integer historyCount(String tableName, String pointName, List<Aiinfo> AiList, Timestamp startTime, Timestamp endTime) {
        String sql = "select count(*) from(select "+pointName+" from  " +
                "(select a.pointName, a.pointId, d.data, d.dataTime, d.isAlarm  from aiinfo a join " + tableName + " d on a.pointId  = d.pointId where (a.pointType=1 or a.pointType=2) and "+
                "d.dataTime between '"+ startTime + "' and  '"+ endTime + "' ) t1 GROUP BY t1.dataTime order by t1.dataTime) jia;";
        return template.queryForObject(sql,java.lang.Integer.class);
    }

    @Override
    public List<Aiinfo> aiinfoList(Integer storageId) {
        String sql="select * from aiinfo where (pointType=1 or pointType=2) and storageId=?";
        return template.query(sql,new Object[]{storageId},new RowMapper<Aiinfo>() {
            @Override
            public Aiinfo mapRow(ResultSet rs, int i) throws SQLException {
                Aiinfo aiinfoHistory = new Aiinfo();
                aiinfoHistory.setPointId(rs.getInt("pointId"));
                aiinfoHistory.setStorageId(rs.getInt("storageId"));
                aiinfoHistory.setPointName(rs.getString("pointName"));
                aiinfoHistory.setPointType(rs.getInt("pointType"));
                aiinfoHistory.setActived(rs.getInt("actived"));
                return aiinfoHistory;
            }
        });
    }

    @Override
    public List<Aiinfo> aiinfoLltudeList(Integer storageId) {
        String sql="select * from aiinfo where (pointType=3 or pointType=4) and storageId=?";
        return template.query(sql,new Object[]{storageId},new RowMapper<Aiinfo>() {
            @Override
            public Aiinfo mapRow(ResultSet rs, int i) throws SQLException {
                Aiinfo aiinfoHistory = new Aiinfo();
                aiinfoHistory.setPointId(rs.getInt("pointId"));
                aiinfoHistory.setStorageId(rs.getInt("storageId"));
                aiinfoHistory.setPointName(rs.getString("pointName"));
                aiinfoHistory.setPointType(rs.getInt("pointType"));
                aiinfoHistory.setActived(rs.getInt("actived"));
                return aiinfoHistory;
            }
        });
    }

    @Override
    public List<Aiinfo> aiinfoListall(Integer storageId) {
        String sql="select * from aiinfo where storageId=? ";
        return template.query(sql,new Object[]{storageId},new RowMapper<Aiinfo>() {
            @Override
            public Aiinfo mapRow(ResultSet rs, int i) throws SQLException {
                Aiinfo aiinfoHistory = new Aiinfo();
                aiinfoHistory.setPointId(rs.getInt("pointId"));
                aiinfoHistory.setStorageId(rs.getInt("storageId"));
                aiinfoHistory.setPointName(rs.getString("pointName"));
                aiinfoHistory.setPointType(rs.getInt("pointType"));
                aiinfoHistory.setActived(rs.getInt("actived"));
                return aiinfoHistory;
            }
        });
    }
}