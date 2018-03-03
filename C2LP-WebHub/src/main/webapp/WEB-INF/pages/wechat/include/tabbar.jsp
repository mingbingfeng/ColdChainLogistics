<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<div class="weui_tabbar">
    <a href="javascript:window.location.href='/wechat/waybill/query';" class="weui_tabbar_item ${nav == 1 ? 'weui_bar_item_on':''}">
        <div class="weui_tabbar_icon">
            <img src="/assets/images/nav/nav_query.png" alt="">
        </div>
        <p class="weui_tabbar_label">运单查询</p>
    </a>
    <a href="javascript:window.location.href='/wechat/waybill/history';" class="weui_tabbar_item ${nav == 2 ? 'weui_bar_item_on':''}">
        <div class="weui_tabbar_icon">
            <img src="/assets/images/nav/nav_history.png" alt="">
        </div>
        <p class="weui_tabbar_label">历史运单</p>
    </a>
    <a href="javascript:window.location.href='/wechat/my';" class="weui_tabbar_item ${nav == 3 ? 'weui_bar_item_on':''}">
        <div class="weui_tabbar_icon">
            <img src="/assets/images/nav/nav_my.png" alt="">
        </div>
        <p class="weui_tabbar_label">个人中心</p>
    </a>
</div>