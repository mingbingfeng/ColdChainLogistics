package com.sby.c2lp.controller.wechat;

import com.google.gson.Gson;
import com.sby.c2lp.model.UserInfo;
import com.sby.c2lp.model.WaybillBase;
import com.sby.c2lp.model.WaybillNode;
import com.sby.c2lp.service.*;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.core.env.Environment;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.ui.ModelMap;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseBody;

import javax.servlet.http.HttpServletRequest;
import java.util.List;

/**
 * Created by zhaoyou on 7/29/16.
 */
@Controller(value = "wechatWaybillQueryController")
@RequestMapping(value = "/wechat")
public class WaybillQueryController {
    @Autowired
    private WaybillBaseService waybillBaseService;
    @Autowired
    private NumberQueryService numberQueryService;
    @Autowired
    private StartEndTimeService startEndTimeService;
    @Autowired
    private AccountAiinforService accountAiinforService;
    @Autowired
    private ColdStorageService coldStorageService;
    @Autowired
    private UserService userService;

    @Autowired
    private Environment env;

    @RequestMapping("/waybill/query")
    public String singleQuery() {
        return "wechat/waybill_single_query";
    }

    //ajax判断是否存在此运单编号
    @RequestMapping(value="/waybill/info", method = RequestMethod.GET,produces = "application/json;charset=utf-8")
    public @ResponseBody String info(@RequestParam String number,HttpServletRequest request) {
        Integer customerId=Integer.parseInt((String) request.getAttribute("customerId"));
        Integer role=Integer.parseInt((String) request.getAttribute("role"));
        WaybillBase waybillBase=waybillBaseService.WaybillObject(number,customerId,role);
        if(waybillBase == null){
            WaybillBase way = numberQueryService.sanfangnumber(number,customerId,role);
            if(way == null){
                return "{\"code\": 2, \"msg\": \"没找到相应的运单编号!\"}";
            }
            String basenumber=way.getNumber();
            if(basenumber != null){
                return "{\"code\": 0, \"url\": \"wechat/waybill/infotwo?number="+basenumber+"\"}";
            }
        }else{
            return "{\"code\": 0, \"url\": \"wechat/waybill/infotwo?number="+number+"\"}";
        }
        return null;
    }

    //根据运单编号查询
    @RequestMapping(value = "/waybill/infotwo", method = RequestMethod.GET)
    public String waybillInfotwo(@RequestParam String number,ModelMap model,HttpServletRequest request) {
        Integer customerId=Integer.parseInt((String) request.getAttribute("customerId"));
        Integer role=Integer.parseInt((String) request.getAttribute("role"));
        WaybillBase waybillBase=waybillBaseService.WaybillObject(number,customerId,role);
        List<WaybillNode> queryList=numberQueryService.findNumberList(number, customerId, role);
        Long baseId = Long.valueOf(waybillBase.getId());
        model.addAttribute("waybillBase",waybillBase);
        model.addAttribute("findNumberList",queryList);
        model.addAttribute("number",number);
        model.addAttribute("imgsrc",env.getProperty("internet.name"));
        model.addAttribute("pictures",waybillBaseService.WabillPicture(baseId));
        return "wechat/waybill_info";
    }

    @RequestMapping("/waybill/history")
    public String historyQuery(Model model, HttpServletRequest request) {
        String role = (String) request.getAttribute("role");
        if("1".equals(role)){
            model.addAttribute("allCars", coldStorageService.AllCars());
            model.addAttribute("allSenders", userService.AllSender());
        }
        return "wechat/waybill_history_query";
    }

    @RequestMapping(value = "/waybill/queryTimeHistory", method = RequestMethod.GET)
    public String queryTimeHistory(@RequestParam String beginAt,@RequestParam String signinAt,
                                   @RequestParam(required = false) String carId,
                                   @RequestParam(required = false) String senderId,
                                   ModelMap model,HttpServletRequest request){
        String starttime=beginAt+" 00:00:00";
        String endtime=signinAt+" 23:59:59";
        Integer customerId=Integer.parseInt((String) request.getAttribute("customerId"));
        Integer role=Integer.parseInt((String) request.getAttribute("role"));
        model.addAttribute("role",role);
        model.addAttribute("beginAt",beginAt);
        model.addAttribute("signinAt",signinAt);
        List<WaybillBase> waybillBases=startEndTimeService.listTimeQuery(starttime, endtime,carId, senderId,customerId, role);
        model.addAttribute("imgsrc",env.getProperty("internet.name"));
        model.addAttribute("startendTimeList",waybillBases);
        if(role==1){
            model.addAttribute("allCars", coldStorageService.AllCars());
            model.addAttribute("allSenders", userService.AllSender());
            model.addAttribute("carId", carId);
            model.addAttribute("senderId",senderId);
        }
        return "wechat/waybill_history_query";
    }

    @RequestMapping(value = "/waybill/signpictures", method = RequestMethod.GET, produces = "application/json;charset=utf-8")
    public @ResponseBody String signpictures(@RequestParam Long id){
        Gson gson = new Gson();
        return gson.toJson(waybillBaseService.WabillPicture(id));
    }

    @RequestMapping("/my")
    public String accountCenter(ModelMap model,HttpServletRequest request) {
        Integer customerId=Integer.parseInt((String) request.getAttribute("customerId"));
        Integer userid=Integer.parseInt((String) request.getAttribute("userid"));
        UserInfo userInfo=accountAiinforService.huoAccount(userid, customerId);
        model.addAttribute("fullName", userInfo.getCustomer().getFullName());
        model.addAttribute("account", userInfo.getCustomer().getAccount());
        model.addAttribute("username", userInfo.getUsername());
        return "wechat/account_center";
    }
}
