package com.sby.c2lp.dao;

import com.sby.c2lp.model.WechatUserInfo;

/**
 * Created by zhaoyou on 9/5/16.
 */
public interface WechatUserInfoDao {

    public Integer saveWechatUserInfo(WechatUserInfo userInfo);

    public WechatUserInfo findByOpenid(String openid);

    public Integer saveWechatWithCustomer(String openid, Integer userId);

    public Integer deleteWechatWithCustomer(String openid);
}
