<%--
  Created by IntelliJ IDEA.
  User: wanghe
  Date: 2017/1/4
  Time: 9:32
  To change this template use File | Settings | File Templates.
--%>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<%@ taglib prefix="fn"  uri="http://java.sun.com/jsp/jstl/functions" %>
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<%@ taglib prefix="fmt" uri="http://java.sun.com/jsp/jstl/fmt" %>
<html>
<head>
    <title></title>
</head>
<jsp:include page="include/header.jsp"></jsp:include>
<body>
<div class="tabbar">
    <div class="weui_tab">
        <div class="weui_tab_bd">
            <input type="hidden"	id="number" value="${waybillBase.number}"/>
            <a href="/wechat/cold/coldchainAlldata?number=${waybillBase.number}" class="weui_btn weui_btn_primary">
                <i class="fa fa-bar-chart" aria-hidden="true"></i>&nbsp;温 湿 度 数 据
            </a>
            <c:if test="${empty waybillBase}">
                <div class="weui_msg">
                    <div class="weui_icon_area"><i class="weui_icon_safe_warn weui_icon_msg"></i></div>
                    <div class="weui_text_area">
                        <h2 class="weui_msg_title">查询失败</h2>
                        <p class="weui_msg_desc">没有找到对应的冷链数据</p>
                    </div>
                </div>
            </c:if>
            <div id="bottom" style="width: 100%;height: 100%;"></div>
        </div>
    </div>
    <jsp:include page="include/tabbar.jsp"></jsp:include>
</div>
</body>
<jsp:include page="include/footer.jsp"></jsp:include>
<script src="/assets/js/lib/highcharts.js"></script>
<script type="text/javascript" src="/assets/js/wechat/waybillpicturedataAll.js"></script>
<script type="text/javascript">
    var waybillpictureAll = new WaybillPictureAll();
</script>

</html>
