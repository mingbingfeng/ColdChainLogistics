package com.sby.c2lp.service;

import com.sby.c2lp.model.ColdStorage;
import com.sby.c2lp.model.Customer;

import java.util.List;

/**
 * Created by wanghe on 2016/8/9.
 */
public interface ColdStorageService {
    public ColdStorage coldObject(Integer id);
    public List<ColdStorage> AllCars ();
}
