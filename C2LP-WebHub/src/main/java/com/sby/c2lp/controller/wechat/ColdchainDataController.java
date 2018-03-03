package com.sby.c2lp.controller.wechat;

import com.google.gson.Gson;
import com.sby.c2lp.model.Aiinfo;
import com.sby.c2lp.model.ColdStorage;
import com.sby.c2lp.model.WaybillBase;
import com.sby.c2lp.model.WaybillNode;
import com.sby.c2lp.service.AiinfoHistoryService;
import com.sby.c2lp.service.ColdStorageService;
import com.sby.c2lp.service.NumberQueryService;
import com.sby.c2lp.service.WaybillBaseService;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.ui.ModelMap;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseBody;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.io.IOException;
import java.sql.Timestamp;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * Created by wanghe on 2016/8/23.
 */
@Controller
@RequestMapping(value = "/wechat/cold")
public class ColdchainDataController {
    private static Integer PAGEAGE = 10;
    @Autowired
    private WaybillBaseService waybillBaseService;
    @Autowired
    private AiinfoHistoryService aiinfoHistoryService;
    @Autowired
    private NumberQueryService numberQueryService;
    @Autowired
    private ColdStorageService coldStorageService;

    @RequestMapping(value = "/coldchaindata")
    public String coldchaindata(@RequestParam String number,@RequestParam Integer storageId,@RequestParam Long id,ModelMap model,HttpServletRequest request){
        Integer customerId=Integer.parseInt((String) request.getAttribute("customerId"));
        Integer role=Integer.parseInt((String) request.getAttribute("role"));
        SimpleDateFormat sdf=new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
        Gson gson = new Gson();
        WaybillBase waybillBase=waybillBaseService.WaybillObject(number,customerId,role);
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
            model.addAttribute("aiListString", gson.toJson(aiList));
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
        return "wechat/waybill_info_data";
    }

    @RequestMapping(value = "/coldchainAlldata")
    public String coldchaindataAll(@RequestParam String number,ModelMap model,HttpServletRequest request){
        Integer customerId=Integer.parseInt((String) request.getAttribute("customerId"));
        Integer role=Integer.parseInt((String) request.getAttribute("role"));
        WaybillBase waybillBase=waybillBaseService.WaybillObject(number, customerId, role);
        List<Map<String,Object>> allHistoryData = numberQueryService.allHistoryDataByNumber(number, customerId, role);
        model.addAttribute("waybillBase",waybillBase);
        model.addAttribute("allHistoryData", allHistoryData);
        return  "wechat/waybill_info_data_all";
    }

    @RequestMapping(value = "/coldchainAllzhex")
    public String coldchainzhexAll(@RequestParam String number,ModelMap model,HttpServletRequest request){
        Integer customerId=Integer.parseInt((String) request.getAttribute("customerId"));
        Integer role=Integer.parseInt((String) request.getAttribute("role"));
        WaybillBase waybillBase=waybillBaseService.WaybillObject(number, customerId, role);
        model.addAttribute("waybillBase",waybillBase);
        return  "wechat/waybill_info_zhex_all";
    }

    @RequestMapping(value = "/coldchainAllzhexdata",method = RequestMethod.GET, produces = "application/json;charset=utf-8")
    @ResponseBody
    public String coldchainzhexAlldata(@RequestParam String number,ModelMap model,HttpServletRequest request){
        Integer customerId=Integer.parseInt((String) request.getAttribute("customerId"));
        Integer role=Integer.parseInt((String) request.getAttribute("role"));
        List<Map<String,Object>> allHistoryData = numberQueryService.allHistoryDataByNumber(number, customerId, role);
        return new Gson().toJson(allHistoryData);
    }

    @RequestMapping(value = "/coldchainAllmap")
    public String coldchainAllmap(@RequestParam String number,ModelMap model,HttpServletRequest request){
        Integer customerId=Integer.parseInt((String) request.getAttribute("customerId"));
        Integer role=Integer.parseInt((String) request.getAttribute("role"));
        WaybillBase waybillBase=waybillBaseService.WaybillObject(number, customerId, role);
        model.addAttribute("waybillBase",waybillBase);
        model.addAttribute("number",waybillBase.getNumber());
        return  "wechat/waybill_info_map_all";
    }

    @RequestMapping(value = "/coldchainAllmapdata",method = RequestMethod.GET, produces = "application/json;charset=utf-8")
    @ResponseBody
    public String coldchainAllmapdata(@RequestParam String number,ModelMap model,HttpServletRequest request){
        Integer customerId=Integer.parseInt((String) request.getAttribute("customerId"));
        Integer role=Integer.parseInt((String) request.getAttribute("role"));
        List<List<Object[]>> aiinfolist = numberQueryService.findLongitudeWithLatitudeData(number);
        return new Gson().toJson(aiinfolist);
    }

    @RequestMapping(value = "/coldchainRealmap")
    public String coldchainRealdata(@RequestParam String number,ModelMap model,HttpServletRequest request){
        Integer customerId=Integer.parseInt((String) request.getAttribute("customerId"));
        Integer role=Integer.parseInt((String) request.getAttribute("role"));
        WaybillBase waybillBase=waybillBaseService.WaybillObject(number, customerId, role);
        model.addAttribute("waybillBase",waybillBase);
        model.addAttribute("number",waybillBase.getNumber());
        return  "wechat/waybill_info_realmap_all";
    }

    @RequestMapping(value = "/coldchainRealmapdata", method = RequestMethod.GET, produces = "application/json;charset=utf-8")
    @ResponseBody
    public String coldRealDataMap(@RequestParam String number, @RequestParam String startTime,@RequestParam String nodeId, HttpServletRequest request, HttpServletResponse response, ModelMap model) throws IOException {
        Integer customerId=Integer.parseInt((String) request.getAttribute("customerId"));
        Integer role=Integer.parseInt((String) request.getAttribute("role"));
        List<WaybillNode> numberList = numberQueryService.findNumberList(number, customerId, role);
        Map<String, Object> map = new HashMap<String, Object>();
        Gson gson = new Gson();
        if(numberList.get(0).getArrived()==2) {
            map.put("storageName", "arrived");
        }else {
            Timestamp nodeTime = numberList.get(0).getOperateAt();
            Integer storageId = numberList.get(0).getStorageId();
            ColdStorage coldStorage=coldStorageService.coldObject(storageId);
            List<Aiinfo> aiList = aiinfoHistoryService.aiinfoListall(storageId);
            String id = numberList.get(0).getNodeid().toString();
            if( !startTime.equals("first") && !id.equals(nodeId) ){
                DateFormat sdf = new SimpleDateFormat("yyyy/MM/dd HH:mm:ss");
                startTime = sdf.format(nodeTime);
            }
            List<Object []> aiinfolist = aiinfoHistoryService.findLastLongitudeWithLatitudeData(storageId,aiList,startTime,nodeTime);
            map.put("storageName", coldStorage.getStorageName());
            map.put("storageType", coldStorage.getStorageType());
            map.put("lng", coldStorage.getLongitude());
            map.put("lat", coldStorage.getLatitude());
            map.put("aiinfolist", aiinfolist);
            map.put("aiList", aiList);
            map.put("nodeId",id);
        }
        return gson.toJson(map);
    }

    @RequestMapping(value = "/wechatcoldchainpaging",method = RequestMethod.GET, produces = "application/json;charset=utf-8")
    public @ResponseBody
    String coldchainpaging(@RequestParam String number,@RequestParam Integer storageId,@RequestParam Long nodeid,@RequestParam Integer pageNum){
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
}
