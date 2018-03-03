package com.sby.c2lp.controller.pc;

import com.google.gson.Gson;
import com.sby.c2lp.model.*;
import com.sby.c2lp.service.*;
import org.apache.commons.codec.digest.DigestUtils;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.core.env.Environment;
import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.ui.ModelMap;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseBody;
import org.springframework.web.servlet.ModelAndView;

import javax.servlet.http.HttpServletRequest;
import java.sql.Timestamp;
import java.text.SimpleDateFormat;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * Created by wanghe on 2016/9/2.
 */
@Controller
@RequestMapping(value = "/pc/waybill")
public class WaybillController {
    private static Integer PAGEAGE = 10;
    @Autowired
    private StartEndTimeService startEndTimeService;
    @Autowired
    private Environment env;
    @Autowired
    private WaybillBaseService waybillBaseService;
    @Autowired
    private AiinfoHistoryService aiinfoHistoryService;
    @Autowired
    private NumberQueryService numberQueryService;
    @Autowired
    private ColdStorageService coldStorageService;
    @Autowired
    private UserService userService;

    private final String FAIL_PWD = "{\"code\": 2, \"msg\": \"原密码不正确！\"}";
    private final String RETURN_PWD = "{\"code\": 0, \"url\": \"pc/waybill/updatepwd\"}";

    @RequestMapping(value = "/query")
    public String query() {
        return "pc/pcsuccess";
    }

    @RequestMapping(value = "/search")
    public String historybill(Model model,HttpServletRequest request) {
        String role = (String) request.getAttribute("role");
        if("1".equals(role)){
            model.addAttribute("allCars", coldStorageService.AllCars());
            model.addAttribute("allSenders", userService.AllSender());
        }
        return "pc/pcwaybill_his";
    }

    @RequestMapping(value = "/count")
    public String waybillCount(Model model,HttpServletRequest request) {
        String role = (String) request.getAttribute("role");
        if("1".equals(role)){
            model.addAttribute("allSenders", userService.AllSender());
            return "pc/pcwaybillCount";
        }
        return null;
    }

    @RequestMapping(value = "/countCar")
    public String waybillCountCar(Model model,HttpServletRequest request) {
        String role = (String) request.getAttribute("role");
        if("1".equals(role)){
            model.addAttribute("allCars", coldStorageService.AllCars());
            return "pc/pcwaybillCountCar";
        }
        return null;
    }

    @RequestMapping(value = "/visit")
    public String visitRecord(@RequestParam(required = false) String beginAt,
                              @RequestParam(required = false) String signinAt,
                              @RequestParam(required = false) String customerId,
                              Model model,HttpServletRequest request) {
        String role = (String) request.getAttribute("role");
        if(!role.equals("1")){ return "pc/pcsuccess"; }

        model.addAttribute("visitList",startEndTimeService.visitRecordList(customerId,beginAt,signinAt));
        model.addAttribute("allCustomer",userService.AllCustomer());
        model.addAttribute("customerId", customerId);
        model.addAttribute("beginAt", beginAt);
        model.addAttribute("signinAt", signinAt);
        return "pc/pcvisit";
    }

    @RequestMapping(value = "/visit/pdf")
    public ModelAndView visitpdf(@RequestParam(required = false) String beginAt,
                              @RequestParam(required = false) String signinAt,
                              @RequestParam(required = false) String customerId,
                              HttpServletRequest request) {
        String role = (String) request.getAttribute("role");
        Map<String, Object> model = new HashMap<String, Object>();
        if(!role.equals("1")){
            return new ModelAndView("visitpdf", model);
        }
        List<VisitRecord> list = startEndTimeService.visitRecordList(customerId,beginAt,signinAt);
        if(list.size()==0){
            return new ModelAndView("visitpdf", model);
        }
        model.put("visitList",list);
        model.put("startTime",beginAt);
        model.put("endTime", signinAt);
        model.put("customerId", Integer.valueOf(customerId));
        return new ModelAndView("visitpdf", model);
    }

    @RequestMapping(value = "/search/startendtime", method = RequestMethod.GET)
    public String startendtime(@RequestParam String beginAt, @RequestParam String signinAt,
                               @RequestParam(required = false) String carId,
                               @RequestParam(required = false) String senderId,
                               ModelMap model, HttpServletRequest request) {
        Integer customerId = Integer.parseInt((String) request.getAttribute("customerId"));
        Integer role = Integer.parseInt((String) request.getAttribute("role"));
        List<WaybillBase> waybillBases = startEndTimeService.listTimeQuery(beginAt, signinAt,carId,senderId,customerId, role);
        model.addAttribute("startendTimeList", waybillBases);
        model.addAttribute("imgsrc", env.getProperty("internet.name"));
        model.addAttribute("beginAt", beginAt);
        model.addAttribute("signinAt", signinAt);
        if(role==1){
            model.addAttribute("allCars", coldStorageService.AllCars());
            model.addAttribute("allSenders", userService.AllSender());
            model.addAttribute("carId", carId);
            model.addAttribute("senderId",senderId);
        }
        return "pc/pcwaybill_his";
    }

    @RequestMapping(value = "/count/startendtime", method = RequestMethod.GET)
    public String waybillCount(@RequestParam String beginAt, @RequestParam String signinAt,
                               @RequestParam(required = false) String senderId,
                               ModelMap model, HttpServletRequest request) {
        Integer role = Integer.parseInt((String) request.getAttribute("role"));
        if(role==1) {
            List<Map<String, Object>> list = startEndTimeService.waybillCount(beginAt, signinAt, senderId);
            model.addAttribute("list", list);
            model.addAttribute("allSenders", userService.AllSender());
            model.addAttribute("beginAt", beginAt);
            model.addAttribute("signinAt", signinAt);
            model.addAttribute("senderId",senderId);
            return "pc/pcwaybillCount";
        }
        return null;
    }

    @RequestMapping(value = "/countCar/startendtime", method = RequestMethod.GET)
    public String waybillCountCar(@RequestParam String beginAt, @RequestParam String signinAt,
                               @RequestParam(required = false) String carId,
                               ModelMap model, HttpServletRequest request) {
        Integer role = Integer.parseInt((String) request.getAttribute("role"));
        if(role==1) {
            List<Map<String, Object>> list = startEndTimeService.waybillCountCar(beginAt, signinAt, carId);
            model.addAttribute("list", list);
            model.addAttribute("allCars", coldStorageService.AllCars());
            model.addAttribute("beginAt", beginAt);
            model.addAttribute("signinAt", signinAt);
            model.addAttribute("carId",carId);
           /* int sum = 0;
            for(Map<String,Object> map :list){
                sum += Integer.parseInt(map.get("count").toString());
            }
            model.addAttribute("sum", sum);*/
            return "pc/pcwaybillCountCar";
        }
        return null;
    }

    @RequestMapping(value = "/countPDF/export", method = RequestMethod.GET)
    public ModelAndView waybillCountPDF(@RequestParam String beginAt, @RequestParam String signinAt,
                               @RequestParam(required = false) String senderId,
                               HttpServletRequest request) {
        Integer role = Integer.parseInt((String) request.getAttribute("role"));
        if(role==1) {
            List<Map<String, Object>> list = startEndTimeService.waybillCount(beginAt, signinAt, senderId);
            Map<String, Object> model = new HashMap<String, Object>();
            model.put("list", list);
            model.put("startTime", beginAt);
            model.put("endTime", signinAt);
            model.put("sender",list.get(0).get("senderOrg"));
            if(senderId.equals("0")){
                model.put("sender","全部");
            }
            return new ModelAndView("countpdf", model);
        }
        return null;
    }

    @RequestMapping(value = "/countCarPDF/export", method = RequestMethod.GET)
    public ModelAndView waybillCountCarPDF(@RequestParam String beginAt, @RequestParam String signinAt,
                                        @RequestParam String carId,
                                        HttpServletRequest request) {
        Integer role = Integer.parseInt((String) request.getAttribute("role"));
        if(role==1) {
            List<Map<String, Object>> list = startEndTimeService.waybillCountCar(beginAt, signinAt, carId);
            Map<String, Object> model = new HashMap<String, Object>();
            model.put("list", list);
            model.put("startTime", beginAt);
            model.put("endTime", signinAt);
            model.put("car",list.get(0).get("storageName"));
            if(carId.equals("0")){
                model.put("car","全部");
            }
           /* int sum = 0;
            for(Map<String,Object> map :list){
                sum += Integer.parseInt(map.get("count").toString());
            }
            model.put("sum", sum);*/
            return new ModelAndView("countCarpdf", model);
        }
        return null;
    }

    @RequestMapping(value = "/coldchain")
    public String coldchainshow(@RequestParam String number,@RequestParam Integer storageId,@RequestParam Long id,ModelMap model,HttpServletRequest request){
        Integer customerId=Integer.parseInt((String) request.getAttribute("customerId"));
        Integer role=Integer.parseInt((String) request.getAttribute("role"));
        SimpleDateFormat sdf=new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
        WaybillBase waybillBase=waybillBaseService.WaybillObject(number, customerId, role);
        WaybillNode numberQuery=numberQueryService.findstartTime(number, id);
        Timestamp startTime=numberQuery.getOperateAt();
        ColdStorage coldStorage=coldStorageService.coldObject(storageId);
        String storageName=coldStorage.getStorageName();
        if(numberQueryService.findendTime(number, startTime)==null){

        }else{
            Integer coldid=coldStorage.getId();
            List<Aiinfo> aiList =aiinfoHistoryService.aiinfoList(coldid);
            WaybillNode numberQuery1=numberQueryService.findendTime(number, startTime);
            Timestamp endTime=numberQuery1.getOperateAt();
            List<Object []> aiinfoHistoryList=aiinfoHistoryService.findAiinfoHistoryTable(storageId, aiList, startTime, endTime,PAGEAGE,0);
            model.addAttribute("historyCount",aiinfoHistoryService.historyCount(storageId, aiList, startTime, endTime));
            model.addAttribute("aiinfoHistoryList", aiinfoHistoryList);
            model.addAttribute("endTime",sdf.format(endTime));
            model.addAttribute("coldid", coldid);
            model.addAttribute("storagetype",coldStorage.getStorageType());
            model.addAttribute("aiList", aiList);
        }
        model.addAttribute("storageName",storageName);
        model.addAttribute("number", number);
        model.addAttribute("storageId", storageId);
        model.addAttribute("id", id);
        model.addAttribute("startTime",sdf.format(startTime));
        model.addAttribute("waybillBase",waybillBase);
        return "pc/coldchainshow";
    }

    @RequestMapping(value = "/coldchainAll")
    public String coldchaindata(@RequestParam String number,ModelMap model,HttpServletRequest request){
        Integer customerId=Integer.parseInt((String) request.getAttribute("customerId"));
        Integer role=Integer.parseInt((String) request.getAttribute("role"));
        WaybillBase waybillBase=waybillBaseService.WaybillObject(number, customerId, role);
        List<Map<String,Object>> allHistoryData = numberQueryService.allHistoryDataByNumber(number, customerId, role);
        model.addAttribute("waybillBase",waybillBase);
        model.addAttribute("allHistoryData", allHistoryData);
        return "pc/coldchainshowAll";
    }

    @RequestMapping(value = "/coldchain/export", method = RequestMethod.GET)
    public ModelAndView doPDF(@RequestParam String number,@RequestParam Long nodeid,@RequestParam Integer coldid,@RequestParam String storageName,HttpServletRequest request) {
        Integer customerId=Integer.parseInt((String) request.getAttribute("customerId"));
        Integer role=Integer.parseInt((String) request.getAttribute("role"));
        WaybillBase waybillBase=waybillBaseService.WaybillObject(number, customerId, role);
        WaybillNode numberQuery=numberQueryService.findstartTime(number, nodeid);
        Timestamp startTime=numberQuery.getOperateAt();
        WaybillNode numberQuery1=numberQueryService.findendTime(number, startTime);
        Timestamp endTime=numberQuery1.getOperateAt();
        List<Aiinfo> aiList =aiinfoHistoryService.aiinfoList(coldid);
        List<Object []> aiinfoHistoryList=aiinfoHistoryService.findAiinfoHistoryTablePdf(coldid, aiList, startTime, endTime);
        Map<String, Object> model = new HashMap<String, Object>();
        model.put("number",number);
        model.put("coldid",coldid);
        model.put("senderOrg",waybillBase.getSenderOrg());
        model.put("receiverOrg",waybillBase.getReceiverOrg());
        model.put("startTime",startTime);
        model.put("endTime",endTime);
        model.put("storageName",storageName);
        model.put("aiList",aiList);
        model.put("aiinfoHistoryList",aiinfoHistoryList);
        return new ModelAndView("pdfView", model);
    }

    @RequestMapping(value = "/coldchainAll/export", method = RequestMethod.GET)
    public ModelAndView doAllPDF(@RequestParam String number,HttpServletRequest request){
        Integer customerId=Integer.parseInt((String) request.getAttribute("customerId"));
        Integer role=Integer.parseInt((String) request.getAttribute("role"));
        WaybillBase waybillBase=waybillBaseService.WaybillObject(number, customerId, role);
        List<Map<String,Object>> allHistoryData = numberQueryService.allHistoryDataByNumber(number, customerId, role);
        Map<String, Object> model = new HashMap<String, Object>();
        model.put("number",number);
        model.put("allpdfView",allHistoryData);
        model.put("senderOrg",waybillBase.getSenderOrg());
        model.put("receiverOrg",waybillBase.getReceiverOrg());
        return new ModelAndView("allpdfView", model);
    }

    @RequestMapping(value = "/coldchainpaging",method = RequestMethod.GET, produces = "application/json;charset=utf-8")
    public @ResponseBody String coldchainpaging(@RequestParam String number, @RequestParam Integer storageId, @RequestParam Long nodeid, @RequestParam Integer pageNum){
        WaybillNode numberQuery=numberQueryService.findstartTime(number, nodeid);
        Timestamp startTime=numberQuery.getOperateAt();
        WaybillNode numberQuery1=numberQueryService.findendTime(number, startTime);
        Timestamp endTime=numberQuery1.getOperateAt();
        ColdStorage coldStorage=coldStorageService.coldObject(storageId);
        Integer coldid=coldStorage.getId();
        List<Aiinfo> aiList =aiinfoHistoryService.aiinfoList(coldid);
        Gson gson = new Gson();
        List<Object []> aiinfoHistoryList=aiinfoHistoryService.coldchainpaging(storageId, aiList, startTime, endTime,PAGEAGE,pageNum);
        return gson.toJson(aiinfoHistoryList);
    }

    @RequestMapping(value = "/coldchain/map")
    public String map(@RequestParam String number,@RequestParam Integer storageId,@RequestParam Long id,ModelMap model,HttpServletRequest request){
        Integer customerId=Integer.parseInt((String) request.getAttribute("customerId"));
        Integer role=Integer.parseInt((String) request.getAttribute("role"));
        SimpleDateFormat sdf=new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
        WaybillBase waybillBase=waybillBaseService.WaybillObject(number, customerId, role);
        WaybillNode numberQuery=numberQueryService.findstartTime(number, id);
        Timestamp startTime=numberQuery.getOperateAt();
        WaybillNode numberQuery1=numberQueryService.findendTime(number, startTime);
        Timestamp endTime=numberQuery1.getOperateAt();
        ColdStorage coldStorage=coldStorageService.coldObject(storageId);
        Integer coldid=coldStorage.getId();
        String storageName=coldStorage.getStorageName();
        List<Aiinfo> aiList =aiinfoHistoryService.aiinfoLltudeList(coldid);
        model.addAttribute("coldid", coldid);
        model.addAttribute("id",id);
        model.addAttribute("number",number);
        model.addAttribute("storageId", storageId);
        model.addAttribute("aiList", aiList);
        model.addAttribute("startTime",sdf.format(startTime));
        model.addAttribute("endTime",sdf.format(endTime));
        model.addAttribute("storageName",storageName);
        model.addAttribute("waybillBase",waybillBase);
        return "pc/coldmapTrajectory";
    }

    @RequestMapping(value = "/coldchainRealdata")
    public String mapRealdata(@RequestParam String number, ModelMap model,HttpServletRequest request){
        Integer customerId=Integer.parseInt((String) request.getAttribute("customerId"));
        Integer role=Integer.parseInt((String) request.getAttribute("role"));
        model.addAttribute("waybillBase",waybillBaseService.WaybillObject(number, customerId, role));
        return "pc/coldmapRealdata";
    }

    @RequestMapping(value = "/coldchainAll/map")
    public String map(@RequestParam String number,ModelMap model,HttpServletRequest request){
        Integer customerId=Integer.parseInt((String) request.getAttribute("customerId"));
        Integer role=Integer.parseInt((String) request.getAttribute("role"));
        model.addAttribute("waybillBase",waybillBaseService.WaybillObject(number, customerId, role));
        return "pc/coldmapAll";
    }

    @RequestMapping(value = "/querypwd", method = RequestMethod.POST, produces = "application/json;charset=utf-8")
    public @ResponseBody String querypwd(@RequestParam String mypwd, HttpServletRequest request) {
        Integer customerId = Integer.parseInt((String) request.getAttribute("customerId"));
        UserInfo querypassword = userService.querypwd(customerId, DigestUtils.md5Hex(mypwd));
        if (querypassword == null) {
            return FAIL_PWD;
        }
        return RETURN_PWD;
    }

    @RequestMapping(value = "/updatepwd", method = RequestMethod.GET)
    public String updatepwd(@RequestParam String newpwd, HttpServletRequest request) {
        Integer customerId = Integer.parseInt((String) request.getAttribute("customerId"));
        userService.updatepwd(customerId, DigestUtils.md5Hex(newpwd));
        return "redirect:/pc/account/dologout";
    }

    //ajax判断是否存在此运单编号
    @RequestMapping(value = "/numberquery", method = RequestMethod.GET,produces = "application/json;charset=utf-8")
    public @ResponseBody
    String findWork(@RequestParam String number,HttpServletRequest request){
        Integer customerId=Integer.parseInt((String) request.getAttribute("customerId"));
        Integer role=Integer.parseInt((String) request.getAttribute("role"));
        WaybillBase waybillBase=waybillBaseService.WaybillObject(number,customerId,role);
        if(waybillBase==null){
            WaybillBase way = numberQueryService.sanfangnumber(number,customerId,role);
            if(way == null){
                return "{\"code\": 2, \"msg\": \"没找到相应的运单编号!\"}";
            }
            String basenumber=way.getNumber();
            if(basenumber != null){
                return "{\"code\": 0, \"url\": \"pc/waybill/show?number="+basenumber+"\"}";
            }
        }else{
            return "{\"code\": 0, \"url\": \"pc/waybill/show?number="+number+"\"}";
        }
        return null;
    }

    //根据运单编号查询
    @RequestMapping(value = "/show", method = RequestMethod.GET)
    public String searchnumberquery(@RequestParam String number,ModelMap model,HttpServletRequest request){
        Integer customerId=Integer.parseInt((String) request.getAttribute("customerId"));
        Integer role=Integer.parseInt((String) request.getAttribute("role"));
        model.addAttribute("number",number);
        WaybillBase waybillBase=waybillBaseService.WaybillObject(number,customerId,role);
        Long baseId = Long.valueOf(waybillBase.getId());
        model.addAttribute("waybillBase",waybillBase);
        model.addAttribute("pictures",waybillBaseService.WabillPicture(baseId));
        model.addAttribute("imgsrc",env.getProperty("internet.name"));
        model.addAttribute("findNumberList", numberQueryService.findNumberList(number, customerId, role));
        return "pc/pcsuccess";
    }

    //折线统计图页面跳转
    @RequestMapping(value = "/coldDataPicture")
    public String coldDataPicture(@RequestParam String number,@RequestParam Integer storageId,@RequestParam Long id,ModelMap model,HttpServletRequest request){
        Integer customerId=Integer.parseInt((String) request.getAttribute("customerId"));
        Integer role=Integer.parseInt((String) request.getAttribute("role"));
        SimpleDateFormat sdf=new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
        WaybillBase waybillBase=waybillBaseService.WaybillObject(number, customerId, role);
        WaybillNode numberQuery=numberQueryService.findstartTime(number, id);
        Timestamp startTime=numberQuery.getOperateAt();
        WaybillNode numberQuery1=numberQueryService.findendTime(number, startTime);
        Timestamp endTime=numberQuery1.getOperateAt();
        ColdStorage coldStorage=coldStorageService.coldObject(storageId);
        Integer coldid=coldStorage.getId();
        String storageName=coldStorage.getStorageName();
        List<Aiinfo> aiList =aiinfoHistoryService.aiinfoLltudeList(coldid);
        model.addAttribute("number", number);
        model.addAttribute("storageId", storageId);
        model.addAttribute("id",id);
        model.addAttribute("coldid", coldid);
        model.addAttribute("aiList", aiList);
        model.addAttribute("startTime",sdf.format(startTime));
        model.addAttribute("endTime",sdf.format(endTime));
        model.addAttribute("storageName",storageName);
        model.addAttribute("waybillBase",waybillBase);
        return "pc/zhexiandatapicture";
    }

    @RequestMapping(value = "/coldDataPictureAll")
    public String coldDataPicture(@RequestParam String number,ModelMap model,HttpServletRequest request){
        Integer customerId=Integer.parseInt((String) request.getAttribute("customerId"));
        Integer role=Integer.parseInt((String) request.getAttribute("role"));
        WaybillBase waybillBase=waybillBaseService.WaybillObject(number, customerId, role);
        model.addAttribute("waybillBase",waybillBase);
        return "pc/coldzhexianAll";
    }

    @RequestMapping(value = "/pccoldzhexiantuall", method = RequestMethod.GET, produces = "application/json;charset=utf-8")
    public @ResponseBody
    String pccoldzhexiantu(@RequestParam String number,HttpServletRequest request){
        Integer customerId=Integer.parseInt((String) request.getAttribute("customerId"));
        Integer role=Integer.parseInt((String) request.getAttribute("role"));
        List<Map<String,Object>> allHistoryData = numberQueryService.allHistoryDataByNumber(number, customerId, role);
        return new Gson().toJson(allHistoryData);
    }

    //折线统计图ajax方法
    @RequestMapping(value = "/pccoldzhexiantu", method = RequestMethod.GET, produces = "application/json;charset=utf-8")
    public @ResponseBody
    String pccoldzhexiantu(@RequestParam String number,@RequestParam Integer storageId,@RequestParam Long id){
        WaybillNode numberQuery=numberQueryService.findstartTime(number, id);
        Timestamp startTime=numberQuery.getOperateAt();
        ColdStorage coldStorage=coldStorageService.coldObject(storageId);
        if(numberQueryService.findendTime(number, startTime)==null){
            return null;
        }
            Integer coldid=coldStorage.getId();
            List<Aiinfo> aiList =aiinfoHistoryService.aiinfoList(coldid);
            WaybillNode numberQuery1=numberQueryService.findendTime(number, startTime);
            Timestamp endTime=numberQuery1.getOperateAt();
            List<Object []> aiinfoHistoryList=aiinfoHistoryService.findAiinfoHistoryTables(storageId, aiList, startTime, endTime);
            Map<String, List> map = new HashMap<String, List>();
            map.put("aiList", aiList);
            map.put("dataList", aiinfoHistoryList);
            return new Gson().toJson(map);

    }
}
