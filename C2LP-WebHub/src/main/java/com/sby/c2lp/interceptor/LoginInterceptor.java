package com.sby.c2lp.interceptor;

import com.sby.c2lp.util.LoginToken;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.core.env.Environment;
import org.springframework.stereotype.Component;
import org.springframework.web.servlet.ModelAndView;
import org.springframework.web.servlet.handler.HandlerInterceptorAdapter;

import javax.servlet.http.Cookie;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

/**
 * Created by hua on 2015/5/21.
 */
@Component
public class LoginInterceptor extends HandlerInterceptorAdapter {

    private Logger logger = LoggerFactory.getLogger(LoginInterceptor.class);

    @Autowired
    private LoginToken loginToken;

    @Autowired
    private Environment env;

    @Override
    public boolean preHandle(HttpServletRequest request, HttpServletResponse response, Object handler) throws Exception {


        String urlString = request.getQueryString() != null ? "?" + request.getQueryString() : "";
        String token = getTokenCookie(request);
        String requestType = request.getHeader("X-Requested-With");

        String redirectUrl = request.getRequestURI().equals("/")
                ? "/pc/account/login"
                : "/pc/account/login?redirectUrl=" + java.net.URLEncoder.encode(request.getRequestURI() + urlString, "utf-8");

        logger.info((requestType == null ? "" : requestType) + " 请求地址 URL:" + request.getRequestURI() + urlString);


        // 当用户请求token过期或为空时,则重定向用户到登陆页面.
        // 如果是ajax请求的, 则发送一个401的请求到客户端,让客户端js重定向到登陆页面.
        if ("".equals(token)) {
            if(requestType == null || "".equals(requestType)) {
                response.sendRedirect(redirectUrl);
            } else {
                response.setStatus(401);
            }
            return false;
        }

        try {
            String[] userInfo = loginToken.getUserInfo(token);
            if (userInfo == null) {
                if(requestType == null || "".equals(requestType)) {
                    response.sendRedirect(redirectUrl);
                } else {
                    response.setStatus(401);
                }
                return false;
            } else {
                request.setAttribute("role", userInfo[0]);
                request.setAttribute("userid", userInfo[1]);
                request.setAttribute("customerId", userInfo[2]);
                request.setAttribute("username", userInfo[3]);
                return true;
            }
        } catch (Exception e) {
            logger.error("解析登陆token发生错误 " + e.getMessage());
            if(requestType == null || "".equals(requestType)) {
                response.sendRedirect(redirectUrl);
            } else {
                response.setStatus(401);
            }
            return false;
        }
    }

    private String getTokenCookie(HttpServletRequest request) {
        Cookie[] cookies = request.getCookies();
        String tokenName = env.getProperty("cookie.pc.token");
        if (cookies != null) {
            for (Cookie cookie : cookies) {
                if (tokenName.equals(cookie.getName())) {
                    return cookie.getValue();
                }
            }
        }
        return "";
    }



    @Override
    public void postHandle(HttpServletRequest request, HttpServletResponse response, Object handler, ModelAndView modelAndView) throws Exception {
        super.postHandle(request, response, handler, modelAndView);
    }
}

