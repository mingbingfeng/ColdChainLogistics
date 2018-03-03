package com.sby.c2lp.controller.wechat;

import com.google.gson.Gson;
import com.sby.c2lp.model.UserInfo;
import com.sby.c2lp.model.WechatAccessToken;
import com.sby.c2lp.model.WechatUserInfo;
import com.sby.c2lp.service.UserService;
import com.sby.c2lp.service.WechatUserInfoService;
import com.sby.c2lp.util.LoginToken;
import okhttp3.OkHttpClient;
import okhttp3.Request;
import okhttp3.Response;
import org.apache.commons.codec.digest.DigestUtils;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.core.env.Environment;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseBody;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

/**
 * Created by zhaoyou on 7/27/16.
 */
@Controller(value = "wechatLoginController")
@RequestMapping(value = "/wechat/account")
public class UserLoginController {

    private static Logger logger = LoggerFactory.getLogger(UserLoginController.class);

    private Gson gson = new Gson();

    @Autowired
    private UserService userService;

    @Autowired
    private LoginToken loginToken;

    @Autowired
    private Environment env;

    @Autowired
    private WechatUserInfoService wechatUserInfoService;

    private final String FAIL_USER = "{\"code\": 2, \"msg\": \"用户名或密码错误！\"}";
    private final String RETURN_SUCCESS = "{\"code\": 0, \"url\": \"wechat/waybill/query\"}";

    @RequestMapping(value = "/login")
    public String index() {
        return "wechat/account_login";
    }

    @RequestMapping(value = "/loginwechat",method = RequestMethod.POST, produces = "application/json;charset=utf-8")
    public @ResponseBody String login(@RequestParam String account,
                                      @RequestParam String username,
                                      @RequestParam String password,
                                      HttpServletResponse response) {
        UserInfo userInfo = userService.loginuser(account, username, DigestUtils.md5Hex(password));
        if (userInfo!=null) {
            sendCookie(response,userInfo.getCustomer().getRole(),userInfo.getId(),userInfo.getCustomerId(),userInfo.getUsername(), "");
            return RETURN_SUCCESS;
        }
        return FAIL_USER;
    }

    @RequestMapping(value = "/callback", method = RequestMethod.GET)
    public String wechatCallback(@RequestParam String code,
                                               @RequestParam String state,
                                               Model model,HttpServletRequest request,
                                               HttpServletResponse response) {

        // code 微信服务器授权成功后回调,返回的值
        logger.info("callback code: {}", code);

        try {


            String result = getAccessToken(env.getProperty("wechat.appid"), env.getProperty("wechat.secret"), code);

            WechatAccessToken accessToken = gson.fromJson(result, WechatAccessToken.class);

            logger.info("accessToken: {}", accessToken.toString());

            if (accessToken == null || accessToken.getOpenid() == null || accessToken.getAccess_token() == null) {
                throw  new Exception("获取请求AccessToken失败!");
            }

            // 判断openid是否已经绑定,如果已经绑定成功了,直接跳转到用户主页,否则跳转到用户绑定页面
            WechatUserInfo userInfo = wechatUserInfoService.isAlreadyBind(accessToken.getOpenid());

            if (userInfo != null && userInfo.getUserId() != null) { // 用户微信账号信息存在,且已绑定成功.

                UserInfo info = userService.findById(userInfo.getUserId());
                sendCookie(response, info.getCustomer().getRole(), info.getId(),
                        info.getCustomerId(), info.getUsername(), accessToken.getOpenid());

                String ip = request.getHeader("real_IP");
                if(ip==null||ip.equals("")){
                    ip = (String)request.getRemoteAddr();
                }
                String agent = (String)request.getHeader("user-agent");
                userService.visitRecord(info.getCustomerId(),ip,agent,0);

                return "redirect:/wechat/waybill/query";

            } else {

                String userinfoDetail = getUserInfo(accessToken.getAccess_token(), accessToken.getOpenid());

                logger.info("userinfoDetail String: " + userinfoDetail);

                userInfo = gson.fromJson(userinfoDetail, WechatUserInfo.class);

                logger.info("wechatUserInfo: {}", userInfo.toString());

                if (userInfo == null) {     // 调用微信接口获取用户的基本信息失败
                    model.addAttribute("msg", userInfo);
                    return "redirect:/wechat/tips/binding_error";
                } else {
                    wechatUserInfoService.saveWechatUserInfo(userInfo);
                    model.addAttribute("openid", accessToken.getOpenid());
                    return "wechat/account_binding";
                }
            }
        } catch (Exception e) {
            e.printStackTrace();
            return "redirect:/wechat/tips/binding_error";
        }
    }



    @RequestMapping(value = "/dobinding",method = RequestMethod.POST, produces = "application/json;charset=utf-8")
    public @ResponseBody String wechatBinding(@RequestParam String account,
                                              @RequestParam String username,
                                              @RequestParam String password,
                                              @RequestParam String openid, HttpServletResponse response) {
        UserInfo userInfo = userService.loginuser(account, username, DigestUtils.md5Hex(password));
        if (userInfo != null) {
            wechatUserInfoService.bindUser(openid, userInfo.getId());
            sendCookie(response,
                        userInfo.getCustomer().getRole(),
                        userInfo.getId(),
                        userInfo.getCustomerId(),
                        userInfo.getUsername(),
                        openid);
            return RETURN_SUCCESS;
        }
        return FAIL_USER;
    }

//    @RequestMapping(value = "/unbinding", method = RequestMethod.GET)
//    public String wechatUnbinding(HttpServletRequest request, HttpServletResponse response) {
//        String openid = request.getAttribute("openid") != null ? (String) request.getAttribute("openid") : "";
//        logger.info("wechat user unbinding openid is {}", openid);
//        response.addCookie(loginToken.eraseCookie(env.getProperty("cookie.wechat.token")));
//        wechatUserInfoService.unbindUser(openid);
//        return "wechat/account_unbinding";
//    }



    // 获取需要绑定的微信账号的信息.
    private String getUserInfo(String accessToken, String openId) throws Exception {
        //  get Userinfo
        //  https://api.weixin.qq.com/sns/userinfo?access_token=ACCESS_TOKEN&openid=OPENID&lang=zh_CN
        String url = "https://api.weixin.qq.com/sns/userinfo?access_token=#ACCESS_TOKEN&openid=#OPENID&lang=zh_CN"
                .replace("#ACCESS_TOKEN", accessToken)
                .replace("#OPENID", openId);
        OkHttpClient client = new OkHttpClient();

        Request request = new Request.Builder()
                .url(url)
                .build();

        Response response = client.newCall(request).execute();
        return response.body().string();
    }


    // 获取网页访问accesstoken.
    private String getAccessToken(String appid, String secret, String code) throws Exception {

        String url = "https://api.weixin.qq.com/sns/oauth2/access_token?appid=#APPID&secret=#SECRET&code=#CODE&grant_type=authorization_code"
                .replace("#APPID", appid )
                .replace("#SECRET", secret)
                .replace("#CODE", code);

        OkHttpClient client = new OkHttpClient();

        Request request = new Request.Builder()
                .url(url)
                .build();

        Response response = client.newCall(request).execute();
        return response.body().string();
    }


    public void sendCookie(HttpServletResponse response,Integer role,Integer userid,Integer customerId,String username,
                           String openid) {
        String token = loginToken.buildToken4Wechat(role, userid, customerId, username, openid);
        response.addCookie(loginToken.buildCookie(env.getProperty("cookie.wechat.token"), token));
    }

}