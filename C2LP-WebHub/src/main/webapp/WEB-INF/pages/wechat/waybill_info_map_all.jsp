<%--
  Created by IntelliJ IDEA.
  User: zhaoyou
  Date: 7/29/16
  Time: 11:04 AM
  To change this template use File | Settings | File Templates.
--%>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<html>
<% request.setAttribute("nav",1);%>
<% request.setAttribute("title", request.getAttribute("number") + " 地图轨迹回放");%>
<jsp:include page="include/header.jsp"></jsp:include>
<body>
<div class="container" id="container">
    <input type="hidden" id="number" value="${number}"/>
    <div class="tabbar">
        <div class="weui_tab">
            <div class="weui_tab_bd">
                <a href="/wechat/cold/coldchainAlldata?number=${number}" class="weui_btn weui_btn_primary">
                        <i class="fa fa-bar-chart" aria-hidden="true"></i>&nbsp;温 湿 度 数 据
                </a>
                <div id="map" class="view" style="width:100%; height:100%;"></div>
                <div class="clear"></div>
            </div>
        </div>
        <jsp:include page="include/tabbar.jsp"></jsp:include>
    </div>
</div>
<jsp:include page="include/footer.jsp"></jsp:include>
<script src="http://api.map.baidu.com/api?v=2.0&ak=vQYyBMOGiQkToVW5hzsITR7xem7Nr7oH" type="text/javascript"></script>
<script type="text/javascript" src="/assets/js/script/util.js"></script>
<script type="text/javascript" src="/assets/js/wechat/CarHisDataMapAll.js"></script>
<script type="text/javascript">
    var truckHisMap = new TruckHisMap();
</script>
</body>
</html>

