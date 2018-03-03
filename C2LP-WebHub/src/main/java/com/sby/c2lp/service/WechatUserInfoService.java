package com.sby.c2lp.service;

import com.sby.c2lp.model.WechatUserInfo;

/**
 * Created by zhaoyou on 9/5/16.
 */
public interface WechatUserInfoService {

    public boolean saveWechatUserInfo(WechatUserInfo userInfo);

    public WechatUserInfo isAlreadyBind(String openid);

    public boolean bindUser(String openid, Integer userId);

    public boolean unbindUser(String openid);
}
