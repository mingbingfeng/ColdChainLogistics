package com.sby.c2lp.controller.wechat;

import com.google.gson.Gson;
import com.sby.c2lp.model.Aiinfo;
import com.sby.c2lp.model.ColdStorage;
import com.sby.c2lp.model.WaybillNode;
import com.sby.c2lp.model.WaybillBase;
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
import java.io.PrintWriter;
import java.sql.Timestamp;
import java.text.SimpleDateFormat;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * Created by wanghe on 2016/8/24.
 */
@Controller
@RequestMapping(value = "/wechat/themap")
public class WechatColdMapController {
    @Autowired
    private WaybillBaseService waybillBaseService;
    @Autowired
    private AiinfoHistoryService aiinfoHistoryService;
    @Autowired
    private NumberQueryService numberQueryService;
    @Autowired
    private ColdStorageService coldStorageService;

    @RequestMapping(value = "/coldTrajectory")
    public String coldchainshow(@RequestParam String number,@RequestParam Integer storageId,@RequestParam Long id,ModelMap model,HttpServletRequest request){
        Integer customerId=Integer.parseInt((String) request.getAttribute("customerId"));
        Integer role=Integer.parseInt((String) request.getAttribute("role"));
        SimpleDateFormat sdf=new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
        WaybillBase waybillBase=waybillBaseService.WaybillObject(number,customerId,role);
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
        return "wechat/waybill_info_map";
    }

    @RequestMapping(value = "/coldDataPicture")
    public String coldDataPicture(@RequestParam String number,@RequestParam Integer storageId,@RequestParam Long id,ModelMap model,HttpServletRequest request){
        WaybillNode numberQuery=numberQueryService.findstartTime(number, id);
        Timestamp startTime=numberQuery.getOperateAt();
        WaybillNode numberQuery1=numberQueryService.findendTime(number, startTime);
        Timestamp endTime=numberQuery1.getOperateAt();
        model.addAttribute("number", number);
        model.addAttribute("storageId", storageId);
        model.addAttribute("id",id);
        return "wechat/waybill_picture_data";
    }

    @RequestMapping(value = "/coldzhexiantu", method = RequestMethod.GET, produces = "application/json;charset=utf-8")
    public @ResponseBody String coldzhexiantu(@RequestParam String number,@RequestParam Integer storageId,@RequestParam Long id){
        WaybillNode numberQuery=numberQueryService.findstartTime(number, id);
        Timestamp startTime=numberQuery.getOperateAt();
        ColdStorage coldStorage=coldStorageService.coldObject(storageId);
        if(numberQueryService.findendTime(number, startTime)==null){

        }else{
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
        return null;
    }


    @RequestMapping(value = "/coldDataMap")
    public String coldmapshow(@RequestParam Integer storageId,@RequestParam Timestamp startTime,@RequestParam Timestamp endTime,HttpServletRequest request, HttpServletResponse response,ModelMap model) throws IOException {
        ColdStorage coldStorage=coldStorageService.coldObject(storageId);
        Integer coldid=coldStorage.getId();
        List<Aiinfo> aiList =aiinfoHistoryService.aiinfoLltudeList(coldid);
        List<Object []> aiinfoHistoryList=aiinfoHistoryService.findLongitudeWithLatitudeData(storageId, aiList, startTime, endTime);
        PrintWriter out=response.getWriter();
        Gson gson = new Gson();
        out.print(aiinfoHistoryList == null ? "{}" : gson.toJson(aiinfoHistoryList));
        return null;
    }

}
