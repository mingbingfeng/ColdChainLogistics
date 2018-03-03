package com.sby.c2lp.controller.pc;
import com.google.gson.Gson;
import com.sby.c2lp.service.*;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Controller;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestMethod;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.ResponseBody;

@Controller
@RequestMapping(value = "/pc/search")
public class NumberQueryController {
        @Autowired
        private WaybillBaseService waybillBaseService;

       @RequestMapping(value = "/signpictures", method = RequestMethod.GET, produces = "application/json;charset=utf-8")
        public
        @ResponseBody
        String signpictures(@RequestParam Long id) {
                Gson gson = new Gson();
           return gson.toJson(waybillBaseService.WabillPicture(id));
            }
        }
