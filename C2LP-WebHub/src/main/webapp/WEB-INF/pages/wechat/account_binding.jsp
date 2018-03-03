<%--
  Created by IntelliJ IDEA.
  User: zhaoyou
  Date: 9/5/16
  Time: 4:24 PM
  To change this template use File | Settings | File Templates.
--%>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>

<% request.setAttribute("title", "账号绑定");%>
<jsp:include page="include/header.jsp"></jsp:include>

<div class="container" id="container">
    <div class="tabbar">
        <div class="weui_tab">
            <div class="weui_tab_bd">
                <div class="hd">
                    <h5 class="page_title">惊尘冷链物流</h5>
                    <br/>
                    <p class="page_desc">
                    <div class="weui_cells weui_cells_form">
                        <div class="weui_cell">
                            <div class="weui_cell_hd">
                                <label class="weui_label">企业账号</label>
                            </div>
                            <div class="weui_cell_bd weui_cell_primary">
                                <input class="weui_input account" type="text" placeholder="必填"/>
                            </div>
                        </div>
                        <div class="weui_cell">
                            <div class="weui_cell_hd">
                                <label class="weui_label">企业用户</label>
                            </div>
                            <div class="weui_cell_bd weui_cell_primary">
                                <input class="weui_input username" type="text" placeholder="必填"/>
                            </div>
                        </div>
                        <div class="weui_cell">
                            <div class="weui_cell_hd">
                                <label class="weui_label">用户密码</label>
                            </div>
                            <div class="weui_cell_bd weui_cell_primary">
                                <input class="weui_input password" type="password" placeholder="必填"/>
                            </div>
                        </div>
                    </div>
                    <br/><br/>
                    <div class="weui_btn_area">
                        <input type="hidden" class="redirectUrl" value="${param.redirectUrl}"/>
                        <input type="hidden" class="openid" value="${openid}"/>
                        <input type="button" class="weui_btn weui_btn_primary btn-submit" value="登录" />
                    </div>
                    </p>
                </div>
            </div>
        </div>
    </div>
</div>
<jsp:include page="include/footer.jsp"></jsp:include>
<script type="text/javascript" src="/assets/js/wechat/userbinding2.js"></script>
<script type="text/javascript">
    var UserBinding = new UserBinding();
</script>
</body>
</html>

