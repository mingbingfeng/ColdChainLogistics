package com.sby.c2lp.controller.pc;

import org.springframework.stereotype.Controller;
import org.springframework.ui.ModelMap;
import org.springframework.web.bind.annotation.RequestMapping;
import javax.servlet.http.HttpServletRequest;

/**
 * Created by wanghe on 2016/9/2.
 */
@Controller
@RequestMapping(value = "/pc/user")
public class UpdatePwdController {

    @RequestMapping(value = "/changepw")
    public String updatepwd() {
        return "pc/updatePwd";
    }
}
