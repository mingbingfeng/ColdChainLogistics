package com.sby.c2lp.service.impl;

import com.sby.c2lp.dao.UserDao;
import com.sby.c2lp.model.Customer;
import com.sby.c2lp.model.UserInfo;
import com.sby.c2lp.service.UserService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;

import java.sql.Date;
import java.sql.Timestamp;
import java.util.List;

/**
 * Created by zhaoyou on 7/27/16.
 */
@Service
public class UserServiceImpl implements UserService{

    @Autowired
    private UserDao userDao;

    @Override
    public UserInfo loginuser(String account, String username, String password) {
        return userDao.loginuser(account, username, password);
    }

    @Override
    public UserInfo querypwd(Integer customerId, String password) {
        return userDao.querypwd(customerId,password);
    }

    @Override
    public Integer updatepwd(Integer customerId, String password) {
        return userDao.updatepwd(customerId,password);
    }

    @Override
    public UserInfo findById(Integer userId) {
        return userDao.findById(userId);
    }

    @Override
    public void visitRecord(Integer customerId, String ip, String agent, Integer visitType){
        userDao.visitRecord(customerId,ip,agent,visitType);
    }

    @Override
    public List<Customer> AllSender() {
        return userDao.AllSender() ;
    }

    @Override
    public List<Customer> AllReceivers(){return userDao.AllReceivers() ;};

    @Override
    public List<Customer> AllCustomer() {
        return userDao.AllCustomer();
    }
}
