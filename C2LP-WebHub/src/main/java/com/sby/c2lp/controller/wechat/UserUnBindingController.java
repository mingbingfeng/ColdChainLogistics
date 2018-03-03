package com.sby.c2lp.controller.wechat;

import com.google.gson.Gson;
import com.sby.c2lp.service.UserService;
import com.sby.c2lp.service.WechatUserInfoService;
import com.sby.c2lp.util.LoginToken;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.core.env.Environment;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

/**
 * Created by zhaoyou on 09/11/2016.
 */
@Controller
@RequestMapping(value = "/wechat")
public class UserUnBindingController {



    @Autowired
    private LoginToken loginToken;

    @Autowired
    private Environment env;

    @Autowired
    private WechatUserInfoService wechatUserInfoService;


    Logger logger = LoggerFactory.getLogger(UserUnBindingController.class);


    @RequestMapping(value = "/unbinding", method = RequestMethod.GET)
    public String wechatUnbinding(HttpServletRequest request, HttpServletResponse response) {
        String openid = request.getAttribute("openid") != null ? (String) request.getAttribute("openid") : "";
        logger.info("wechat user unbinding openid is {}", openid);
        response.addCookie(loginToken.eraseCookie(env.getProperty("cookie.wechat.token")));
        wechatUserInfoService.unbindUser(openid);
        return "wechat/account_unbinding";
    }

}
