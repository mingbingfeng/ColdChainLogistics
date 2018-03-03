package com.sby.c2lp.service;

import com.sby.c2lp.model.Aiinfo;

import java.sql.Timestamp;
import java.util.List;

/**
 * Created by wanghe on 2016/8/5.
 */
public interface AiinfoHistoryService {
    public List<Object []> findAiinfoHistoryTable(Integer storageId,List<Aiinfo> AiList,Timestamp startTime,Timestamp endTime,Integer pageAge,Integer pageNum);
//节点详细数据一次查询全部
    public List<Object []> findAiinfoHistoryTables(Integer storageId,List<Aiinfo> AiList,Timestamp startTime,Timestamp endTime);

    public List<Object []> findAiinfoHistoryTablePdf(Integer storageId,List<Aiinfo> AiList,Timestamp startTime,Timestamp endTime);

    public List<Object[]> coldchainpaging(Integer storageId,List<Aiinfo> AiList, Timestamp startTime, Timestamp endTime, Integer pageAge, Integer pageNum);

    // 获取指定节点的经纬度数据
    public List<Object []> findLongitudeWithLatitudeData(Integer storageId,List<Aiinfo> AiList,Timestamp startTime,Timestamp endTime);

    // 获取指定时间点之后的实时经纬度数据
    public List<Object[]> findLastLongitudeWithLatitudeData(Integer storageId, List<Aiinfo> AiList, String startTime, Timestamp nodeTime);

    //获取冷链数据个数
    public Integer historyCount(Integer storageId, List<Aiinfo> AiList, Timestamp startTime, Timestamp endTime);
    //温湿度信息数据
    public List<Aiinfo> aiinfoList(Integer storageId);
    //经纬度信息数据
    public List<Aiinfo> aiinfoLltudeList(Integer storageId);

    public List<Aiinfo> aiinfoListall(Integer storageId);
}
