package com.sby.c2lp.controller.pc;

import com.google.gson.Gson;
import com.sby.c2lp.model.Aiinfo;
import com.sby.c2lp.model.ColdStorage;
import com.sby.c2lp.model.WaybillNode;
import com.sby.c2lp.service.AiinfoHistoryService;
import com.sby.c2lp.service.ColdStorageService;
import com.sby.c2lp.service.NumberQueryService;
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
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/**
 * Created by wanghe on 2016/8/12.
 */
@Controller
@RequestMapping(value = "/pc/themap")
public class ColdmapTrajectoryController {
    @Autowired
    private AiinfoHistoryService aiinfoHistoryService;
    @Autowired
    private ColdStorageService coldStorageService;
    @Autowired
    private NumberQueryService numberQueryService;

    @RequestMapping(value = "/coldMap", method = RequestMethod.GET, produces = "application/json;charset=utf-8")
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

    @RequestMapping(value = "/coldMapall", method = RequestMethod.GET, produces = "application/json;charset=utf-8")
    @ResponseBody
    public String coldmapall(@RequestParam String number,HttpServletRequest request, HttpServletResponse response,ModelMap model) throws IOException {
        List<List<Object[]>> aiinfolist = numberQueryService.findLongitudeWithLatitudeData(number);
        Gson gson = new Gson();
        return ( aiinfolist == null ? "{}" : gson.toJson(aiinfolist) );
    }

    @RequestMapping(value = "/coldRealDataMap", method = RequestMethod.GET, produces = "application/json;charset=utf-8")
    @ResponseBody
    public String coldRealDataMap(@RequestParam String number,@RequestParam String startTime,@RequestParam String nodeId,HttpServletRequest request, HttpServletResponse response,ModelMap model) throws IOException {
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
}
