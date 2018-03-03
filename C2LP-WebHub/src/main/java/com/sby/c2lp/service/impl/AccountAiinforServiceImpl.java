package com.sby.c2lp.service.impl;

import com.sby.c2lp.dao.AccountAiinforDao;
import com.sby.c2lp.model.UserInfo;
import com.sby.c2lp.service.AccountAiinforService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

/**
 * Created by wanghe on 2016/8/30.
 */
@Service
public class AccountAiinforServiceImpl implements AccountAiinforService {

    @Autowired
    private AccountAiinforDao accountAiinforDao;
    @Override
    public UserInfo huoAccount(Integer userId, Integer customerId) {
        return accountAiinforDao.huoAccount(userId, customerId);
    }
}
