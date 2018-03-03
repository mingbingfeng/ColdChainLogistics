package com.sby.c2lp.service.impl;

import com.sby.c2lp.dao.StartEndTimeDao;
import com.sby.c2lp.model.VisitRecord;
import com.sby.c2lp.model.WaybillBase;
import com.sby.c2lp.service.StartEndTimeService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.sql.Timestamp;
import java.util.List;
import java.util.Map;

/**
 * Created by wanghe on 2016/8/8.
 */
@Service
public class StartEndTimeServiceImpl implements StartEndTimeService {
    @Autowired
    private StartEndTimeDao startEndTimeDao;
    @Override
    public List<WaybillBase> listTimeQuery(String beginAt, String signinAt,String carId,String senderId,Integer customerId,Integer role) {
        return startEndTimeDao.listTimeQuery(beginAt,signinAt,carId,senderId,customerId,role);
    }

    @Override
    public List<Map<String,Object>> waybillCount(String beginAt, String signinAt, String senderId){
        return startEndTimeDao.waybillCount(beginAt, signinAt, senderId);
    }
    @Override
    public List<VisitRecord> visitRecordList(String customerId, String beginAt, String signinAt){
       if(customerId==null){
           return null;
       }
        return startEndTimeDao.visitRecordList(customerId,beginAt,signinAt);
    }

    @Override
    public List<Map<String,Object>> waybillCountCar(String beginAt, String signinAt, String carId){
        return startEndTimeDao.waybillCountCar(beginAt, signinAt, carId);
    }
}
