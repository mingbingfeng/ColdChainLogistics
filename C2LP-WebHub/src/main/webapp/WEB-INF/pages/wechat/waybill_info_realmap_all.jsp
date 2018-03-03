<%--
  Created by IntelliJ IDEA.
  User: zhaoyou
  Date: 7/29/16
  Time: 11:04 AM
  To change this template use File | Settings | File Templates.
--%>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<html>
<% request.setAttribute("nav",1);%>
<% request.setAttribute("title", request.getAttribute("number") + " 实时地图轨迹");%>
<jsp:include page="include/header.jsp"></jsp:include>
<body>
<div class="container" id="container">
    <input type="hidden" id="number" value="${number}"/>
    <div class="tabbar">
        <div class="weui_tab">
            <div class="weui_tab_bd">
                <a href="/wechat/cold/coldchainAlldata?number=${number}" class="weui_btn weui_btn_primary">
                        <i class="fa fa-bar-chart" aria-hidden="true"></i>&nbsp;实 时 地 图 轨 迹
                </a>
                <c:if test="${waybillBase.stage==1}">
                    <div id = "arrived"style="text-align: center;">当前运单已运达！</div>
                </c:if>

                <c:if test="${waybillBase.stage==0}">
                    <div id="bottom" data-starttimee ="first" data-nodeId ="first">
                        <div class="alert-none"  style="text-align: center;display: none;">
                            未查找到当前节点的最新数据，请稍后。</br></br>
                        </div>
                        <div id="map" class="view" style="width:100%; height: 510px;"></div>
                        </br>
                    </div>
                </c:if>
                <div class="clear"></div>
            </div>
        </div>
        <jsp:include page="include/tabbar.jsp"></jsp:include>
    </div>
</div>
<jsp:include page="include/footer.jsp"></jsp:include>
<script src="http://api.map.baidu.com/api?v=2.0&ak=vQYyBMOGiQkToVW5hzsITR7xem7Nr7oH" type="text/javascript"></script>
<script type="text/javascript" src="/assets/js/script/util.js"></script>
<script type="text/javascript" src="/assets/js/wechat/TruckRealMap.js"></script>
<script type="text/javascript">
    var truckRealMap = new TruckRealMap();
</script>
</body>
</html>

