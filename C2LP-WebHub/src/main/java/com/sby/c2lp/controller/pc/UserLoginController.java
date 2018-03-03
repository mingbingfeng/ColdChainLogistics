package com.sby.c2lp.controller.pc;

import com.sby.c2lp.model.UserInfo;
import com.sby.c2lp.service.UserService;
import com.sby.c2lp.util.LoginToken;
import org.apache.commons.codec.digest.DigestUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.core.env.Environment;
import org.springframework.stereotype.Controller;
import org.springframework.ui.ModelMap;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseBody;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.UnsupportedEncodingException;
import java.sql.Date;

/**
 * Created by zhaoyou on 7/27/16.
 */
@Controller
@RequestMapping(value = "/pc/account")
public class UserLoginController {
    private static Logger logger = LoggerFactory.getLogger(UserLoginController.class);
    @Autowired
    private UserService userService;

    @Autowired
    private LoginToken loginToken;

    @Autowired
    private Environment env;

    private final String FAIL_USER = "{\"code\": 2, \"msg\": \"用户名或密码错误！\"}";
    private final String RETURN_SUCCESS = "{\"code\": 0, \"url\": \"pc/waybill/query\"}";

    @RequestMapping(value = "/login")
    public String LoginUser(@RequestParam(required = false) String redirectUrl, ModelMap model) {
        if(redirectUrl != null) {
            try {
                String url = java.net.URLDecoder.decode(redirectUrl, "utf-8");
                model.addAttribute("redirectUrl", url);
            } catch (UnsupportedEncodingException e) {
                e.printStackTrace();
            }
        }
        return "pc/pclogin";
    }

    @RequestMapping(value = "/doLogin",method = RequestMethod.POST, produces = "application/json;charset=utf-8")
    public @ResponseBody String login(@RequestParam String account,@RequestParam String username,@RequestParam String password,HttpServletResponse response,HttpServletRequest request) {
        UserInfo userInfo=userService.loginuser(account, username, DigestUtils.md5Hex(password));
        if(userInfo!=null){
            String ip = request.getHeader("real_IP");
            if(ip==null||ip.equals("")){
                ip = (String)request.getRemoteAddr();
            }
            String agent = (String)request.getHeader("user-agent");
            userService.visitRecord(userInfo.getCustomerId(),ip,agent,1);
            sendCookie(response,userInfo.getCustomer().getRole(),userInfo.getId(),userInfo.getCustomerId(),userInfo.getUsername());
            return RETURN_SUCCESS;
        }
        return FAIL_USER;
    }

    //真正实现注销
    @RequestMapping(value = "/dologout")
    public String doLogout(HttpServletResponse response) {
        response.addCookie(loginToken.eraseCookie(env.getProperty("cookie.pc.token")));
        return "pc/pclogin";
    }

    public void sendCookie(HttpServletResponse response,Integer role,Integer userid,Integer customerId,String username) {
        String token = loginToken.buildToken(role, userid, customerId,username);
        logger.debug("loginToken" + token);
        response.addCookie(loginToken.buildCookie(env.getProperty("cookie.pc.token"), token));
    }

}
