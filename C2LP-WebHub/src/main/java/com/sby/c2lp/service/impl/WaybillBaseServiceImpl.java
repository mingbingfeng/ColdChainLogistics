package com.sby.c2lp.service.impl;

import com.sby.c2lp.dao.WaybillBaseDao;
import com.sby.c2lp.model.WaybillBase;
import com.sby.c2lp.service.WaybillBaseService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

/**
 * Created by wanghe on 2016/8/4.
 */
@Service
public class WaybillBaseServiceImpl implements WaybillBaseService {
    @Autowired
    private WaybillBaseDao waybillBaseDao;
    @Override
    public WaybillBase WaybillObject(String number,Integer customerId,Integer role) {
        return waybillBaseDao.WaybillObject(number,customerId,role);
    }

    @Override
    public List<WaybillBase> WabillPicture(Long id) {
        return waybillBaseDao.WabillPicture(id);
    }
}
