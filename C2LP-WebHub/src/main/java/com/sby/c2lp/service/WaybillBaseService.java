package com.sby.c2lp.service;

import com.sby.c2lp.model.WaybillBase;

import java.util.List;

/**
 * Created by wanghe on 2016/8/4.
 */
public interface WaybillBaseService {
    public WaybillBase WaybillObject(String number,Integer customerId,Integer role);
    public List<WaybillBase> WabillPicture(Long id);
}
