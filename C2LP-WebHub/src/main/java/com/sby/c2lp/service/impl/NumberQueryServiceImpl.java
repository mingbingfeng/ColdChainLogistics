package com.sby.c2lp.service.impl;
import com.sby.c2lp.dao.NumberQueryDao;
import com.sby.c2lp.model.Aiinfo;
import com.sby.c2lp.model.WaybillBase;
import com.sby.c2lp.model.WaybillNode;
import com.sby.c2lp.service.AiinfoHistoryService;
import com.sby.c2lp.service.NumberQueryService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import java.sql.Timestamp;
import java.util.*;

/**
 * Created by wanghe on 2016/8/1.
 */
@Service
public class NumberQueryServiceImpl implements NumberQueryService {
    @Autowired
    private NumberQueryDao numberQueryDao;
    @Autowired
    private AiinfoHistoryService aiinfoHistoryService;
    @Override
    public List<WaybillNode> findNumberList(String number,Integer customerId,Integer role) {
        return numberQueryDao.findNumberList(number,customerId,role);
    }

    @Override
    public WaybillBase sanfangnumber(String number,Integer customerId,Integer role) {
        return numberQueryDao.sanfangnumber(number,customerId,role);
    }

    @Override
    public WaybillNode findstartTime(String number, Long id) {
        return numberQueryDao.findstartTime(number,id);
    }

    @Override
    public WaybillNode findendTime(String number, Timestamp operateAt) {
        return numberQueryDao.findendTime(number, operateAt);
    }

    @Override
    public List<Map<String,Object>> allHistoryDataByNumber(String number, Integer customerId, Integer role){
        List<WaybillNode> numberList = findNumberList(number, customerId, role);
        List<Map<String, Object>> list = new ArrayList<Map<String, Object>>();
        Map<String, Object> map;
        for(int i=numberList.size()-1;i>0;i--){
            map = new HashMap<String, Object>();
            Integer storageId =numberList.get(i).getStorageId();
            List<Aiinfo> aiList = aiinfoHistoryService.aiinfoList(storageId);
            Timestamp startTime = numberList.get(i).getOperateAt();
            Timestamp endTime = numberList.get(i-1).getOperateAt();
            List<Object []> aiinfoHistoryList=aiinfoHistoryService.findAiinfoHistoryTablePdf(storageId, aiList, startTime, endTime);
            if(aiinfoHistoryList.size()>0){
                map.put("aiList",aiList);
                map.put("aiinfoHistoryList",aiinfoHistoryList);
                map.put("storageName", numberList.get(i).getStorageName());
                map.put("startTime",startTime);
                map.put("endTime",endTime);
                list.add(map);
            }
        }
        return list;
    }

    @Override
    public List<List<Object []>> findLongitudeWithLatitudeData(String number){
        List<WaybillNode> numberList = findNumberList(number, 1, 1);
        List<List<Object []>> list = new ArrayList<List<Object []>>();
        List<String> trucks = new ArrayList<String>();
        for(int i=numberList.size()-1;i>0;i--){
            trucks.add( numberList.get(i).getStorageName().replace("[默认]","") );
            Integer storageId =numberList.get(i).getStorageId();
            List<Aiinfo> aiList =aiinfoHistoryService.aiinfoLltudeList(storageId);//select * from aiinfo where (pointType=1 or pointType=2) and storageId=?
            Timestamp startTime = numberList.get(i).getOperateAt();
            Timestamp endTime = numberList.get(i-1).getOperateAt();
            List<Object []> aiinfoHistoryList=aiinfoHistoryService.findLongitudeWithLatitudeData(storageId, aiList, startTime, endTime);
            if(aiinfoHistoryList.size()>0){
                aiinfoHistoryList.add(new Object[]{numberList.get(i).getStorageName().replace("[默认]", "")});
                list.add(aiinfoHistoryList);
            }
        }
        return list;
    }
}
