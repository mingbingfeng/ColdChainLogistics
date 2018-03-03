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
            <input type="hidden"	id="number" value="${number}"/>
            <input type="hidden"	id="storageId" value="${storageId}"/>
            <input type="hidden"	id="id" value="${id}"/>
            <a href="/wechat/cold/coldchaindata?number=${number}&id=${id}&storageId=${storageId}" class="weui_btn weui_btn_primary">
                <i class="fa fa-bar-chart" aria-hidden="true"></i>&nbsp;温 湿 度 数 据
            </a>
            <div id="container" style="width: 100%;height: 100%;"></div>
        </div>
    </div>
    <jsp:include page="include/tabbar.jsp"></jsp:include>
</div>
</body>
<jsp:include page="include/footer.jsp"></jsp:include>
<script src="/assets/js/lib/highcharts.js"></script>
<script type="text/javascript" src="/assets/js/wechat/waybillpicturedata.js"></script>
<script type="text/javascript">
    var waybillpicturedata = new WaybillPictureData();
</script>

</html>
