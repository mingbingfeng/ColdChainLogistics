<%--
  Created by IntelliJ IDEA.
  User: zhaoyou
  Date: 7/29/16
  Time: 11:04 AM
  To change this template use File | Settings | File Templates.
--%>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<%@ taglib prefix="fn"  uri="http://java.sun.com/jsp/jstl/functions" %>
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<%@ taglib prefix="fmt" uri="http://java.sun.com/jsp/jstl/fmt" %>

<html>

<jsp:include page="include/header.jsp"></jsp:include>
<body>
<div class="container" id="container">
    <div class="tabbar">
        <div class="weui_tab">
            <div class="weui_tab_bd">
                <div class="hd" style="padding-bottom: 5px;">
                    <h1 class="page_title">${waybillBase.number}</h1>
                </div>
                <div class="weui_cells_title" style="text-align: center;"><h2>运单全程信息</h2></div>
                <div class="weui_cells">
                    <div class="weui_cell">
                        <div class="weui_cell_hd">
                            <label class="weui_label">开始时间</label>
                        </div>
                        <div class="weui_cell_bd weui_cell_primary">
                            <fmt:formatDate value="${waybillBase.beginAt}" pattern="yyyy-MM-dd HH:mm:ss"/>
                        </div>
                    </div>
                    <div class="weui_cell">
                        <div class="weui_cell_hd">
                            <label class="weui_label">结束时间</label>
                        </div>
                        <div class="weui_cell_bd weui_cell_primary">
                            <fmt:formatDate value="${waybillBase.signinAt}" pattern="yyyy-MM-dd HH:mm:ss"/>
                        </div>
                    </div>
                    <div class="weui_cell">
                        <div class="weui_cell_hd">
                            <label class="weui_label">发货地址</label>
                        </div>
                        <div class="weui_cell_bd weui_cell_primary">
                            ${waybillBase.senderAddress}
                        </div>
                    </div>
                    <div class="weui_cell">
                        <div class="weui_cell_hd">
                            <label class="weui_label">收货地址</label>
                        </div>
                        <div class="weui_cell_bd weui_cell_primary">
                            ${waybillBase.receiverAddress}
                        </div>
                    </div>
                </div>
                <div class="weui_btn_area">
                    <a href="/wechat/cold/coldchainAllmap?number=${waybillBase.number}" class="weui_btn weui_btn_primary">
                        <i class="fa fa-map-marker" aria-hidden="true"></i>&nbsp;路 线 轨 迹
                    </a>
                </div>
                <div class="weui_btn_area">
                    <a href="/wechat/cold/coldchainAllzhex?number=${waybillBase.number}" class="weui_btn weui_btn_primary">
                        <i class="fa fa-bar-chart-o" aria-hidden="true"></i>&nbsp;折 线 绘 图
                    </a>
                </div>
                </br>
                <div class="weui_cells_title" >
                    <h2 style="text-align: center;">数据</h2>
                </div>
                <c:if test="${not empty allHistoryData}" var="nodelist">
                    <c:forEach var="node" items="${allHistoryData}">
                        <c:if test="${not empty node.aiinfoHistoryList && not empty node.aiList}">
                            <div class="weui_cells_title" >
                                <strong>
                                        ${node.storageName} &nbsp;-- &nbsp;<code id="historyCount" style="color: green;">${fn:length(node.aiinfoHistoryList)}</code>&nbsp;条记录
                                </strong>
                            </div>
                            <div class="context">
                                <c:forEach var="data" items="${node.aiinfoHistoryList}">
                                    <div class="workTr">
                                        <div class="weui_cells_title"><i class="fa fa-calendar"></i>&nbsp; ${data[0]}</div>
                                        <div class="weui_cells">
                                            <c:forEach begin="0" step="1" end="${fn:length(node.aiList)-1}"  var="i" varStatus="irow">
                                                <div class="weui_cell" >
                                                    <div class="weui_cell_bd weui_cell_primary">
                                                        <p>${node.aiList[i].pointName}</p>
                                                    </div>
                                                    <div class="weui_cell_ft">
                                                        <c:if test="${data[i+1]==null || data[i+1]==-300}">
                                                            --
                                                        </c:if>
                                                        <c:if test="${data[i+1]!=null && data[i+1]!=-300}">
                                                            <fmt:formatNumber value="${data[i+1]}" pattern="#.0"/>
                                                        </c:if>
                                                    </div>
                                                </div>
                                            </c:forEach>
                                            <div class="weui_cell" >
                                                <div class="weui_cell_bd weui_cell_primary">
                                                    <p>报警状态</p>
                                                </div>
                                                <div class="weui_cell_ft">
                                                    <c:if test="${data[fn:length(node.aiList)+1]==0}">
                                                        正常
                                                    </c:if>
                                                    <c:if test="${data[fn:length(node.aiList)+1]==1}">
                                                        <span style="color: red;">报警</span>
                                                    </c:if>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </c:forEach>
                            </div>
                        </c:if>
                    </c:forEach>
                </c:if>
                <c:if test="${!nodelist}">
                    <div class="weui_msg">
                        <div class="weui_icon_area"><i class="weui_icon_safe_warn weui_icon_msg"></i></div>
                        <div class="weui_text_area">
                            <h2 class="weui_msg_title">查询失败</h2>
                            <p class="weui_msg_desc">没有找到对应的冷链数据</p>
                        </div>
                    </div>
                </c:if>
                        <div style="text-align: center;">
                            加载完成
                        </div>
            </div>
        </div>
        <jsp:include page="include/tabbar.jsp"></jsp:include>
    </div>
</div>
<jsp:include page="include/footer.jsp"></jsp:include>
<script type="text/javascript" src="/assets/js/wechat/waybillinfodatanull.js"></script>
</body>
</html>
