package com.sby.c2lp.dao.impl;

import com.sby.c2lp.dao.UserDao;
import com.sby.c2lp.model.Customer;
import com.sby.c2lp.model.UserInfo;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.dao.DataAccessException;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.jdbc.core.ResultSetExtractor;
import org.springframework.jdbc.core.RowMapper;
import org.springframework.stereotype.Repository;

import javax.sql.DataSource;
import java.sql.Date;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Timestamp;
import java.util.List;
import java.util.Map;

/**
 * Created by zhaoyou on 7/27/16.
 */
@Repository
public class UserDaoImpl implements UserDao {

    private JdbcTemplate template;

    private String FIND_BY_ID = "select u.id, u.username, u.customerId, c.role from customer c inner join " +
            " customer_users u on  c.id = u.customerId where u.id = ? and  c.actived=0 and u.actived=0";

    @Autowired
    public void setDataSource(DataSource dataSource) {
        this.template = new JdbcTemplate(dataSource);
    }

    @Override
    public void visitRecord(Integer customerId, String ip, String agent, Integer visitType){
        Timestamp visitTime = new Timestamp(System.currentTimeMillis());
        String sql = "INSERT into visitrecord (customerId,fullname,visitTime,ip,userAgent,visitType)VALUES (?,(SELECT fullName from customer WHERE id = ? ),?,?,?,?)";
         template.update(sql,new Object[]{customerId,customerId,visitTime,ip,agent,visitType});
    }

    @Override
    public UserInfo loginuser(final String account, final String username, final String password) {
        String sql="select u.id userid, u.customerId, u.username, u.password, c.account, c.role from customer c inner join customer_users u on c.id = u.customerId where c.account=? and u.username=? and u.password=? and c.actived=0 and u.actived=0";
        return template.query(sql,new Object[]{account,username,password},new ResultSetExtractor<UserInfo>() {
            @Override
            public UserInfo extractData(ResultSet rs) throws SQLException, DataAccessException {
                if (rs.next()) {
                    UserInfo userInfo = new UserInfo();
                    userInfo.setId(rs.getInt("userid"));
                    userInfo.setCustomerId(rs.getInt("customerId"));
                    userInfo.setUsername(rs.getString("username"));
                    userInfo.setPassword(rs.getString("password"));

                    Customer c = new Customer();
                    c.setAccount(rs.getString("account"));
                    c.setRole(rs.getInt("role"));
                    userInfo.setCustomer(c);
                    return userInfo;
                } else {
                    return null;
                }
            }
        });
    }

    @Override
    public UserInfo querypwd(Integer customerId, String password) {
        String sql="select * from customer_users where customerId=? and password=?";
        return template.query(sql,new Object[]{customerId,password},new ResultSetExtractor<UserInfo>() {
            @Override
            public UserInfo extractData(ResultSet rs) throws SQLException, DataAccessException {
                if (rs.next()) {
                    UserInfo userInfo = new UserInfo();
                    userInfo.setCustomerId(rs.getInt("customerId"));
                    userInfo.setPassword(rs.getString("password"));
                    return userInfo;
                } else {
                    return null;
                }
            }
        });
    }

    @Override
    public Integer updatepwd(Integer customerId, String password) {
        String sql="update customer_users set password='"+password+"' where customerId="+customerId+"";
        return template.update(sql);
    }

    @Override
    public UserInfo findById(Integer userId) {

        return template.query(FIND_BY_ID, new Object[]{userId},new ResultSetExtractor<UserInfo>() {
            @Override
            public UserInfo extractData(ResultSet rs) throws SQLException, DataAccessException {
                if (rs.next()) {
                    UserInfo userInfo = new UserInfo();
                    userInfo.setId(rs.getInt("id"));
                    userInfo.setCustomerId(rs.getInt("customerId"));
                    userInfo.setUsername(rs.getString("username"));
                    Customer customer=new Customer();
                    customer.setRole(rs.getInt("role"));
                    userInfo.setCustomer(customer);
                    return userInfo;
                }
                return null;
            }
        });
    }

    @Override
    public List<Customer> AllSender() {
        String sql = "SELECT id,fullname from customer WHERE role=2 ORDER BY fullname";
        return template.query(sql, new RowMapper<Customer>() {
            @Override
            public Customer mapRow(ResultSet resultSet, int i) throws SQLException {
                Customer c = new Customer();
                c.setId(resultSet.getInt("id"));
                c.setFullName(resultSet.getString("fullname"));
                return c;
            }
        });
    }

    @Override
    public List<Customer> AllReceivers() {
        String sql = "SELECT id,fullname from customer WHERE role=3 ORDER BY fullname";
        return template.query(sql, new RowMapper<Customer>() {
            @Override
            public Customer mapRow(ResultSet resultSet, int i) throws SQLException {
                Customer c = new Customer();
                c.setId(resultSet.getInt("id"));
                c.setFullName(resultSet.getString("fullname"));
                return c;
            }
        });
    }

    @Override
    public List<Customer> AllCustomer() {
        String sql = "SELECT id,fullname from customer ORDER BY fullname";
        return template.query(sql, new RowMapper<Customer>() {
            @Override
            public Customer mapRow(ResultSet resultSet, int i) throws SQLException {
                Customer c = new Customer();
                c.setId(resultSet.getInt("id"));
                c.setFullName(resultSet.getString("fullname"));
                return c;
            }
        });
    }
}