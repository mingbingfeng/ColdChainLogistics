<%--
  Created by IntelliJ IDEA.
  User: zhaoyou
  Date: 7/29/16
  Time: 10:59 AM
  To change this template use File | Settings | File Templates.
--%>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<html>
<jsp:include page="include/header.jsp"></jsp:include>
<% request.setAttribute("nav",3);%>
<body>
<div class="container" id="container">
    <div class="tabbar">
        <div class="weui_tab">
            <div class="weui_tab_bd">
                <div class="hd">
                    <h1 class="page_title">个人账号中心</h1>
                    <br/>
                    <p class="page_desc">
                        <div class="weui_cells">
                            <div class="weui_cell">
                                <div class="weui_cell_bd weui_cell_primary">
                                    <h5>${fullName}</h5>
                                </div>
                            </div>
                        </div>
                        <div class="weui_cells_title">企业账号</div>
                        <div class="weui_cells">
                            <div class="weui_cell">
                                <div class="weui_cell_bd weui_cell_primary">
                                    <h5>${account}</h5>
                                </div>
                            </div>
                        </div>
                        <div class="weui_cells_title">登录用户</div>
                        <div class="weui_cells">
                            <div class="weui_cell">
                                <div class="weui_cell_bd weui_cell_primary">
                                    <h5>${username}</h5>
                                </div>
                            </div>
                        </div>
                        <br/><br/><br/>
                       <input type="button" id="exitbutton" class="weui_btn weui_btn_warn" value=" 解 除 绑 定 "/>
                    </p>
                </div>
            </div>
        </div>
        <jsp:include page="include/tabbar.jsp"></jsp:include>
    </div>
</div>
<jsp:include page="include/footer.jsp"></jsp:include>
<script src="/assets/js/exitlogin.js" type="text/javascript"></script>
<script type="text/javascript">
    var exitlogin = new ExitLogin();
</script>
</body>
</html>

