package com.sby.c2lp.service.impl;

import com.sby.c2lp.dao.AiinfoHistoryDao;
import com.sby.c2lp.model.Aiinfo;
import com.sby.c2lp.service.AiinfoHistoryService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.sql.Timestamp;
import java.util.List;

/**
 * Created by wanghe on 2016/8/5.
 */
@Service
public class AiinfoHistoryServiceImpl implements AiinfoHistoryService {
    @Autowired
    private AiinfoHistoryDao aiinfoHistoryDao;

    @Override
    public List<Object []> findAiinfoHistoryTable(Integer storageId, List<Aiinfo> AiList, Timestamp startTime, Timestamp endTime,Integer pageAge,Integer pageNum) {
            return aiinfoHistoryDao.findAiinfoHistoryTable(buildAiinfoHistoryTable(storageId), buildAiinfoHistoryData(AiList), AiList, startTime, endTime, pageAge, pageNum);
    }

    @Override
    public List<Object []> findAiinfoHistoryTables(Integer storageId, List<Aiinfo> AiList, Timestamp startTime, Timestamp endTime) {
        return aiinfoHistoryDao.findAiinfoHistoryTables(buildAiinfoHistoryTable(storageId), buildAiinfoHistoryData(AiList), AiList, startTime, endTime);
    }


    @Override
    public List<Object[]> findAiinfoHistoryTablePdf(Integer storageId, List<Aiinfo> AiList, Timestamp startTime, Timestamp endTime) {
        return aiinfoHistoryDao.findAiinfoHistoryTablePdf(buildAiinfoHistoryTable(storageId),buildAiinfoHistoryData(AiList),AiList, startTime, endTime);
    }

    @Override
    public List<Object[]> coldchainpaging(Integer storageId,List<Aiinfo> AiList, Timestamp startTime, Timestamp endTime, Integer pageAge, Integer pageNum) {
        return aiinfoHistoryDao.coldchainpaging(buildAiinfoHistoryTable(storageId), buildAiinfoHistoryData(AiList),AiList, startTime, endTime, pageAge, pageNum);
    }

    @Override
    public List<Object[]> findLongitudeWithLatitudeData(Integer storageId, List<Aiinfo> AiList, Timestamp startTime, Timestamp endTime) {
        return aiinfoHistoryDao.findLongitudeWithLatitudeData(buildAiinfoHistoryTable(storageId), buildLongitudeLatitudeFieldData(AiList),
                AiList, startTime, endTime);
    }

    @Override
    public List<Object[]> findLastLongitudeWithLatitudeData(Integer storageId, List<Aiinfo> AiList, String startTime, Timestamp nodeTime) {
        if(startTime.equals("first")){
            startTime = "(SELECT dataTime from "+buildAiinfoHistoryTable(storageId)+" where dataTime > '"+nodeTime+"' GROUP BY dataTime  ORDER BY dataTime DESC LIMIT 1,1)";
        }else {
            startTime = "'"+startTime +"'";
        }
        return aiinfoHistoryDao.findLastLongitudeWithLatitudeData(buildAiinfoHistoryTable(storageId), buildLongitudeLatitudeFieldData(AiList),
                AiList, startTime);
    }

    @Override
    public Integer historyCount(Integer storageId, List<Aiinfo> AiList, Timestamp startTime, Timestamp endTime) {
        return aiinfoHistoryDao.historyCount(buildAiinfoHistoryTable(storageId),buildAiinfoHistoryData(AiList),AiList, startTime, endTime);
    }

    @Override
    public List<Aiinfo> aiinfoList(Integer storageId) {
        return aiinfoHistoryDao.aiinfoList(storageId);
    }

    @Override
    public List<Aiinfo> aiinfoListall(Integer storageId){
        return aiinfoHistoryDao.aiinfoListall(storageId);
    }

    @Override
    public List<Aiinfo> aiinfoLltudeList(Integer storageId) {
        return aiinfoHistoryDao.aiinfoLltudeList(storageId);
    }

    private String buildAiinfoHistoryTable(Integer storageId) {
        return "history_data_" + storageId;
    }

    private String buildAiinfoHistoryData(List<Aiinfo> aiinfoHistoryList) {

        StringBuffer sb = new StringBuffer(" t1.dataTime, ");
        for (Aiinfo aiinfoHistory: aiinfoHistoryList) {
            sb.append(" max(case t1.pointName when '" + aiinfoHistory.getPointName() +"' then t1.data else null end) as '" + aiinfoHistory.getPointName() + "',");
        }
        sb.append(" min(case t1.isAlarm when 0 then 0 else 1 end) as 'isAlarm'");
        return sb.toString();
    }

    private String buildLongitudeLatitudeFieldData(List<Aiinfo> aiinfoHistoryList) {
        StringBuffer sb = new StringBuffer(" t1.dataTime, ");
        for (Aiinfo aiinfoHistory: aiinfoHistoryList) {
            sb.append(" max(case t1.pointId when " + aiinfoHistory.getPointId() +" then t1.data else null end) as '" + aiinfoHistory.getPointId() + "',");
        }
        sb.append(" min(case t1.isAlarm when 0 then 0 else 1 end) as 'isAlarm'");
        return sb.toString();
    }
}
