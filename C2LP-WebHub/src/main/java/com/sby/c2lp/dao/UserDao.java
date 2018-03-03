package com.sby.c2lp.dao;

import com.sby.c2lp.model.Customer;
import com.sby.c2lp.model.UserInfo;

import java.sql.Date;
import java.sql.Timestamp;
import java.util.List;

/**
 * Created by zhaoyou on 7/27/16.
 */
public interface UserDao {
    //登录功能
    public UserInfo loginuser(String account,String username,String password);

    //查询密码
    public UserInfo querypwd(Integer customerId,String password);

    //修改密码
    public Integer updatepwd(Integer customerId, String password);

    //  根据用户id获取对应的用户信息
    public UserInfo findById(Integer userId);

    public void visitRecord(Integer customerId, String ip, String agent, Integer visitType);

    public List<Customer> AllSender();
    public List<Customer> AllReceivers();
    public List<Customer> AllCustomer();
}
