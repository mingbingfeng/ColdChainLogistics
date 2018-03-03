package com.sby.c2lp.dao.impl;

import com.sby.c2lp.dao.WechatUserInfoDao;
import com.sby.c2lp.model.WechatUserInfo;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.dao.DataAccessException;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.jdbc.core.ResultSetExtractor;
import org.springframework.stereotype.Repository;

import javax.sql.DataSource;
import java.sql.ResultSet;
import java.sql.SQLException;

/**
 * Created by zhaoyou on 9/5/16.
 */
@Repository
public class WechatUserInfoDaoImpl implements WechatUserInfoDao {

    private JdbcTemplate template;

    private String SAVE = "insert into wechat_users (openid, nickname, sex, province, city, country, headimgurl, unionid, createAt) values" +
            " (?, ?, ?, ?, ?, ?, ?, ?, now())";

    private String DELETE_WECHAT_USER = "delete from wechat_users where openid = ?";

    private String FIND = "select u.*, c.userId from wechat_users u inner join  wechat_with_customer c " +
            " on c.openid = u.openid where u.openid = ?";

    private String SAVE_RELATION =
            "insert into wechat_with_customer (userId, openid) values (?, ?);";

    private String DELETE_RELATION = "delete from wechat_with_customer where openid = ?;";

    @Autowired
    public void setDataSource(DataSource dataSource) {
        this.template = new JdbcTemplate(dataSource);
    }

    @Override
    public Integer saveWechatUserInfo(WechatUserInfo userInfo) {
        this.template.update(DELETE_WECHAT_USER, new Object[]{userInfo.getOpenid()});
        return this.template.update(SAVE, new Object[]{userInfo.getOpenid(), "", userInfo.getSex(),
        userInfo.getProvince(), userInfo.getCity(), userInfo.getCountry(), userInfo.getHeadimgurl(), userInfo.getUnionid()});
    }

    @Override
    public WechatUserInfo findByOpenid(String openid) {
        return template.query(FIND, new Object[]{openid}, new ResultSetExtractor<WechatUserInfo>() {
            @Override
            public WechatUserInfo extractData(ResultSet rs) throws SQLException, DataAccessException {
                if (rs.next()) {
                    WechatUserInfo userInfo = new WechatUserInfo();
                    userInfo.setOpenid(rs.getString("openid"));
                    userInfo.setNickname(rs.getString("nickname"));
                    userInfo.setSex(rs.getString("sex"));
                    userInfo.setCity(rs.getString("city"));
                    userInfo.setProvince(rs.getString("province"));
                    userInfo.setCountry(rs.getString("country"));
                    userInfo.setHeadimgurl(rs.getString("headimgurl"));
                    userInfo.setUnionid(rs.getString("unionid"));
                    userInfo.setUserId(rs.getInt("userId"));
                    return userInfo;
                }
                return null;
            }
        });
    }

    @Override
    public Integer saveWechatWithCustomer(String openid, Integer userId) {
        this.template.update(DELETE_RELATION, new Object[]{openid});
        return this.template.update(SAVE_RELATION, new Object[]{userId, openid});
    }

    @Override
    public Integer deleteWechatWithCustomer(String openid) {
        return this.template.update(DELETE_RELATION, new Object[]{openid});
    }
}
