package com.sby.c2lp.service.impl;

import com.sby.c2lp.dao.WechatUserInfoDao;
import com.sby.c2lp.model.WechatUserInfo;
import com.sby.c2lp.service.WechatUserInfoService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

/**
 * Created by zhaoyou on 9/5/16.
 */
@Service
public class WechatUserInfoServiceImpl implements WechatUserInfoService {

    @Autowired
    private WechatUserInfoDao wechatUserInfoDao;


    @Override
    public boolean saveWechatUserInfo(WechatUserInfo userInfo) {
        return wechatUserInfoDao.saveWechatUserInfo(userInfo) == 1 ? true : false;
    }

    @Override
    public WechatUserInfo isAlreadyBind(String openid) {
        return wechatUserInfoDao.findByOpenid(openid);
    }

    @Override
    public boolean bindUser(String openid, Integer userId) {
        return wechatUserInfoDao.saveWechatWithCustomer(openid, userId) == 1 ? true : false ;
    }

    @Override
    public boolean unbindUser(String openid) {
        return wechatUserInfoDao.deleteWechatWithCustomer(openid) == 1 ? true : false;
    }
}
