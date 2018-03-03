package com.sby.c2lp.controller.wechat;

import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.RequestMapping;

/**
 * Created by zhaoyou on 9/8/16.
 */
@Controller
@RequestMapping("/wechat/tips")
public class TipsController {

    @RequestMapping(value = "/not_in_wechat")
    public String notwechat(Model model) {
        model.addAttribute("title", "发生错误");
        model.addAttribute("desc", "请确保在微信里面访问冷链物流系统的页面");
        return "wechat/tips";
    }

    @RequestMapping(value = "/binding_error")
    public String accountBindingError(Model model) {
        model.addAttribute("title", "获取账号信息失败");
        model.addAttribute("desc", "无法绑定冷链账号.请关闭当前页面从微信菜单重新进入系统");
        return "wechat/tips";
    }

    @RequestMapping(value = "/auth_error")
    public String authError(Model model) {
        model.addAttribute("title", "登陆信息失效");
        model.addAttribute("desc", "请关闭当前页面从微信菜单重新进入系统");
        return "wechat/tips";
    }
}
