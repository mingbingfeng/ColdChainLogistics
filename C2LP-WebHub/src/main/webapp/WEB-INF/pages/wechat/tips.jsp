<%--
  Created by IntelliJ IDEA.
  User: zhaoyou
  Date: 9/7/16
  Time: 2:44 PM
  To change this template use File | Settings | File Templates.
--%>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<html>
<jsp:include page="include/header.jsp"></jsp:include>
<body>
<div class="container" id="container">

    <div class="weui_msg">
        <div class="weui_icon_area"><i class="weui_icon_warn weui_icon_msg"></i></div>
        <div class="weui_text_area">
            <h2 class="weui_msg_title">${title}</h2>
            <p class="weui_msg_desc">${desc}</p>
        </div>
    </div>
</div>
<jsp:include page="include/footer.jsp"></jsp:include>
</body>
</html>
