package com.sby.c2lp.dao;


import com.sby.c2lp.model.VisitRecord;
import com.sby.c2lp.model.WaybillBase;

import java.sql.Timestamp;
import java.util.List;
import java.util.Map;

/**
 * Created by wanghe on 2016/8/8.
 */
public interface StartEndTimeDao {
    public List<WaybillBase> listTimeQuery(String beginAt,String signinAt,String carId,String senderId,Integer customerId,Integer role);
    public List<VisitRecord> visitRecordList(String customerId, String beginAt, String signinAt);
    public List<Map<String,Object>> waybillCount(String beginAt, String signinAt, String senderId);
    public List<Map<String,Object>> waybillCountCar(String beginAt, String signinAt, String carId);
}
