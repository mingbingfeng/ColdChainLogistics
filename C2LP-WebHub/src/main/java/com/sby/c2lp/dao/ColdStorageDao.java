package com.sby.c2lp.dao;

import com.sby.c2lp.model.ColdStorage;
import com.sby.c2lp.model.Customer;
import com.sby.c2lp.model.VisitRecord;

import java.util.List;
import java.util.Map;

/**
 * Created by wanghe on 2016/8/9.
 */
public interface ColdStorageDao {
    public ColdStorage coldObject(Integer id);
    public List<ColdStorage> AllCars ();
}
