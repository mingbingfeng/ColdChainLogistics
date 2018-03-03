package com.sby.c2lp.dao;

import com.sby.c2lp.model.Aiinfo;

import java.sql.Timestamp;
import java.util.List;

/**
 * Created by wanghe on 2016/8/5.
 */
public interface AiinfoHistoryDao {
    public List<Object []> findAiinfoHistoryTable(String tableName,String pointName,List<Aiinfo> AiList,Timestamp startTime,Timestamp endTime,Integer pageAge,Integer pageNum);

    public List<Object []> findAiinfoHistoryTables(String tableName,String pointName,List<Aiinfo> AiList,Timestamp startTime,Timestamp endTime);

    public List<Object []> findLongitudeWithLatitudeData(String tableName,String pointName,List<Aiinfo> AiList,Timestamp startTime,Timestamp endTime);

    public List<Object []> findLastLongitudeWithLatitudeData(String tableName,String pointName,List<Aiinfo> AiList,String startTime);

    public List<Object []> findAiinfoHistoryTablePdf(String tableName,String pointName,List<Aiinfo> AiList,Timestamp startTime,Timestamp endTime);

    public List<Object[]> coldchainpaging(String tableName, String pointName, final List<Aiinfo> AiList, Timestamp startTime, Timestamp endTime,Integer pageAge,Integer pageNum);
//查询冷链数据个数
    public Integer historyCount(String tableName,String pointName,List<Aiinfo> AiList,Timestamp startTime,Timestamp endTime);
//温湿度信息数据
    public List<Aiinfo> aiinfoList(Integer storageId);
//经纬度信息数据
    public List<Aiinfo> aiinfoLltudeList(Integer storageId);

    //所有信息数据
    public List<Aiinfo> aiinfoListall(Integer storageId);
}
