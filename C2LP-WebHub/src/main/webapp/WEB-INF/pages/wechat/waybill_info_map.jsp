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
<% request.setAttribute("title", request.getAttribute("storageName") + " 地图轨迹回放");%>
<jsp:include page="include/header.jsp"></jsp:include>
<body>
<div class="container" id="container">
    <input type="hidden" id="id" value="${id}"/>
    <input type="hidden" id="number" value="${number}"/>
    <input type="hidden" id="coldid" value="${coldid}"/>
    <input type="hidden" id="senderOrg" value="${waybillBase.senderOrg}"/>
    <input type="hidden" id="receiverOrg" value="${waybillBase.receiverOrg}"/>
    <input type="hidden" id="startTime" value="${startTime}"/>
    <input type="hidden" id="endTime" value="${endTime}"/>
    <input type="hidden" id="storageName" value="${storageName}"/>
    <input type="hidden" id="storageId" value="${storageId}"/>
    <div class="tabbar">
        <div class="weui_tab">
            <div class="weui_tab_bd">
                <a href="/wechat/cold/coldchaindata?number=${number}&id=${id}&storageId=${storageId}" class="weui_btn weui_btn_primary">
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
<script type="text/javascript" src="/assets/js/wechat/CarHisDataMap.js"></script>
<script type="text/javascript">
    var carHisDataMap = new CarHisDataMap();
</script>
</body>
</html>

