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
<% request.setAttribute("nav",1);%>
<body>
<div class="container" id="container">
    <input type="hidden" id="number" value="${waybillBase.number}"/>
    <input type="hidden" id="coldid" value="${coldid}"/>
    <input type="hidden" id="startTime" value="${startTime}"/>
    <input type="hidden" id="endTime" value="${endTime}"/>
    <input type="hidden" id="senderOrg" value="${waybillBase.senderOrg}"/>
    <input type="hidden" id="receiverOrg" value="${waybillBase.receiverOrg}"/>
    <input type="hidden" id="storageName" value="${storageName}"/>
    <input type="hidden" id="storageId" value="${storageId}"/>
    <input type="hidden" id="nodeid" value="${id}"/>
    <input type="hidden" id="aiList" value='${aiListString}'/>
    <div class="tabbar">
        <div class="weui_tab">
            <div class="weui_tab_bd">
                <div class="hd" style="padding-bottom: 5px;">
                    <h1 class="page_title">${storageName}</h1>
                </div>
                <div class="weui_cells_title" style="text-align: center;"><h2>物流节点信息</h2></div>
                <div class="weui_cells">
                    <div class="weui_cell">
                        <div class="weui_cell_hd">
                            <label class="weui_label">运单编号</label>
                        </div>
                        <div class="weui_cell_bd weui_cell_primary">
                            ${number}
                        </div>
                    </div>
                    <div class="weui_cell">
                        <div class="weui_cell_hd">
                            <label class="weui_label">开始时间</label>
                        </div>
                        <div class="weui_cell_bd weui_cell_primary">
                            ${startTime}
                        </div>
                    </div>
                    <div class="weui_cell">
                        <div class="weui_cell_hd">
                            <label class="weui_label">结束时间</label>
                        </div>
                        <div class="weui_cell_bd weui_cell_primary">
                            ${endTime}
                        </div>
                    </div>
                </div>
                <c:if test="${not empty aiinfoHistoryList && not empty aiList && storagetype==2}">
                        <div class="weui_btn_area">
                            <a href="/wechat/themap/coldTrajectory?number=${number}&storageId=${storageId}&id=${id}" class="weui_btn weui_btn_primary">
                                <i class="fa fa-map-marker" aria-hidden="true"></i>&nbsp;路 线 轨 迹
                            </a>
                        </div>
                </c:if>
                <c:if test="${not empty aiinfoHistoryList && not empty aiList}">
                    <div class="weui_btn_area">
                        <a href="/wechat/themap/coldDataPicture?number=${number}&storageId=${storageId}&id=${id}" class="weui_btn weui_btn_primary">
                            <i class="fa fa-bar-chart-o" aria-hidden="true"></i>&nbsp;折 线 绘 图
                        </a>
                    </div>
                </c:if>
                <div class="weui_cells_title">
                    <strong>
                        总共记录&nbsp;<code id="historyCount" style="color: green;">${historyCount}</code>&nbsp;条
                    </strong>
                </div>
                <c:choose>
                    <c:when test="${not empty aiinfoHistoryList && not empty aiList}">
                        <div class="context">
                            <c:forEach var="data" items="${aiinfoHistoryList}">
                                <div class="workTr">
                                <div class="weui_cells_title"><i class="fa fa-calendar"></i>&nbsp; ${data[0]}</div>
                                <div class="weui_cells">
                                    <c:forEach begin="0" step="1" end="${fn:length(aiList)-1}"  var="i" varStatus="irow">
                                        <div class="weui_cell" >
                                            <div class="weui_cell_bd weui_cell_primary">
                                                <p>${aiList[i].pointName}</p>
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
                                            <c:if test="${data[fn:length(aiList)+1]==0}">
                                                正常
                                            </c:if>
                                            <c:if test="${data[fn:length(aiList)+1]==1}">
                                                <span style="color: red;">报警</span>
                                            </c:if>
                                        </div>
                                    </div>
                                </div>
                                </div>
                            </c:forEach>
                        </div>
                    </c:when>
                    <c:otherwise>
                        <div class="weui_msg">
                            <div class="weui_icon_area"><i class="weui_icon_safe_warn weui_icon_msg"></i></div>
                            <div class="weui_text_area">
                                <h2 class="weui_msg_title">查询失败</h2>
                                <p class="weui_msg_desc">没有找到对应的冷链数据</p>
                            </div>
                        </div>
                    </c:otherwise>
                </c:choose>
                        <div style="text-align: center;">
                            <ul class="pager hide">
                                <li><button class="weui_btn weui_btn_mini weui_btn_primary" href="javascript:void(0)" id="loadingWork">
                                    加载更多&nbsp;&nbsp;
                                </button></li>
                            </ul>
                        </div>
            </div>
        </div>
        <jsp:include page="include/tabbar.jsp"></jsp:include>
    </div>
</div>
<jsp:include page="include/footer.jsp"></jsp:include>
<script type="text/javascript" src="/assets/js/wechat/waybillinfodatanull.js"></script>
<script type="text/javascript">
    var waybillinfodatanull = new WaybillInfoDataNull();
</script>
</body>
</html>
