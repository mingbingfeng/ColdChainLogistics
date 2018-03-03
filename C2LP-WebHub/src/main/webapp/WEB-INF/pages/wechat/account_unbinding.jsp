<%--
  Created by IntelliJ IDEA.
  User: zhaoyou
  Date: 7/29/16
  Time: 10:56 AM
  To change this template use File | Settings | File Templates.
--%>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<html>
<jsp:include page="include/header.jsp"></jsp:include>
<body>
<div class="container" id="container">

    <div class="weui_msg">
        <div class="weui_icon_area"><i class="weui_icon_success weui_icon_msg"></i></div>
        <div class="weui_text_area">
            <h2 class="weui_msg_title">操作成功</h2>
            <p class="weui_msg_desc">账号解除绑定成功，如需要重新绑定,请关闭当前页面从微信菜单重新进入系统</p>
        </div>
    </div>
</div>
<jsp:include page="include/footer.jsp"></jsp:include>
</body>
<script>
    window.setTimeout(function(){
        if(WeixinJSBridge) {
            WeixinJSBridge.invoke('closeWindow',{},function(res) {
                //alert(res.err_msg);
            });
        }
    }, 4000);
</script>
</html>
