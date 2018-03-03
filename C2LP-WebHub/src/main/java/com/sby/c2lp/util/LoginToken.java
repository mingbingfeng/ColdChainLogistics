package com.sby.c2lp.util;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.core.env.Environment;
import org.springframework.stereotype.Component;

import javax.servlet.http.Cookie;
import java.util.Date;

@Component
public class LoginToken {

    private Logger logger = LoggerFactory.getLogger(LoginToken.class);

    @Autowired
    private CryToDemo crytoDemo;

    @Autowired
    private Environment env;

    public String buildToken(Integer role,Integer userid,Integer customerId,String username) {
        try {
            int interval =
                    env.getProperty("cookie.expiry") == null ? 2 : Integer.parseInt(env.getProperty("cookie.expiry")) ;
            Long now = new Date().getTime() + (1000 * 60 * 60 * interval);
            return crytoDemo.textToEncrypt(role + ":" + userid + ":"+ customerId +":"+ username +":" + now);
        } catch (Exception e) {
            e.printStackTrace();
            logger.error(e.getMessage());
            throw new RuntimeException("加密登陆token失败！");
        }
    }

    public String[] getUserInfo(String token) {
        try {
            String plainText = crytoDemo.decrypt(token);
            String [] items = plainText.split(":");
            Long now = new Date().getTime();

            // 判断用户token是否过期。
            if (now > Long.parseLong(items[4])) {
                return null;
            }
            return items;

        } catch (Exception e) {
            e.printStackTrace();
            logger.error("无法解析token, " + e.getMessage());
            return null;
        }
    }

    public String buildToken4Wechat(Integer role,Integer userid,Integer customerId,String username, String openid) {
        try {
            int interval =
                    env.getProperty("cookie.expiry") == null ? 2 : Integer.parseInt(env.getProperty("cookie.expiry")) ;
            Long now = new Date().getTime() + (1000 * 60 * 60 * interval);
            return crytoDemo.textToEncrypt(role + ":" + userid + ":"+ customerId +":"+ username + ":" + openid + ":" + now);
        } catch (Exception e) {
            e.printStackTrace();
            logger.error(e.getMessage());
            throw new RuntimeException("加密微信登陆token失败！");
        }
    }

    public String[] getUserInfo4Wechat(String token) {
        try {
            String plainText = crytoDemo.decrypt(token);
            String [] items = plainText.split(":");
            Long now = new Date().getTime();

            // 判断用户token是否过期。
            if (now > Long.parseLong(items[5])) {
                return null;
            }
            return items;

        } catch (Exception e) {
            e.printStackTrace();
            logger.error("无法解析微信登陆token, " + e.getMessage());
            return null;
        }
    }

    public Cookie buildCookie(String name, String token) {
        Cookie cookie = new Cookie(name, token);
        cookie.setMaxAge(-1);
        cookie.setPath("/");
        cookie.setHttpOnly(true);
        return cookie;
    }

    public Cookie eraseCookie(String name) {
        Cookie cookie = new Cookie(name, "");
        cookie.setMaxAge(0);
        cookie.setPath("/");
        cookie.setHttpOnly(true);
        return cookie;
    }
}
