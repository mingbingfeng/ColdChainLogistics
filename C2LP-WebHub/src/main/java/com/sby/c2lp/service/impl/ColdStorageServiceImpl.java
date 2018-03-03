package com.sby.c2lp.service.impl;

import com.sby.c2lp.dao.ColdStorageDao;
import com.sby.c2lp.model.ColdStorage;
import com.sby.c2lp.model.Customer;
import com.sby.c2lp.service.ColdStorageService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.util.List;

/**
 * Created by wanghe on 2016/8/9.
 */
@Service
public class ColdStorageServiceImpl implements ColdStorageService{

    @Autowired
    private ColdStorageDao coldStorageDao;

    @Override
    public ColdStorage coldObject(Integer id) {
        return coldStorageDao.coldObject(id);
    }

    @Override
    public List<ColdStorage> AllCars (){
        return coldStorageDao.AllCars();
    }

}
