package com.sby.c2lp.dao.impl;

import com.sby.c2lp.dao.AccountAiinforDao;
import com.sby.c2lp.model.Customer;
import com.sby.c2lp.model.UserInfo;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.dao.DataAccessException;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.jdbc.core.ResultSetExtractor;
import org.springframework.stereotype.Repository;

import javax.sql.DataSource;
import java.sql.Date;
import java.sql.ResultSet;
import java.sql.SQLException;

/**
 * Created by wanghe on 2016/8/30.
 */
@Repository
public class AccountAiinforDaoImpl implements AccountAiinforDao {
    private JdbcTemplate template;
    @Autowired
    public void setDataSource(DataSource dataSource) {
        this.template = new JdbcTemplate(dataSource);
    }

    @Override
    public UserInfo huoAccount(Integer userId, Integer customerId) {
        String sql="select c.account, u.username, c.fullName from customer c inner join customer_users u on c.id = u.customerId where u.id = ? and u.customerId = ?";
        return template.query(sql,new Object[]{userId,customerId},new ResultSetExtractor<UserInfo>() {
            @Override
            public UserInfo extractData(ResultSet rs) throws SQLException, DataAccessException {
                if (rs.next()) {
                    UserInfo userInfo = new UserInfo();
                    userInfo.setUsername(rs.getString("username"));
                    Customer customer=new Customer();
                    customer.setAccount(rs.getString("account"));
                    customer.setFullName(rs.getString("fullName"));
                    userInfo.setCustomer(customer);
                    return userInfo;
                } else {
                    return null;
                }
            }
        });
    }
}
