<%@ taglib prefix="form" uri="http://www.springframework.org/tags/form" %>
<%--
  Created by IntelliJ IDEA.
  User: zhaoyou
  Date: 7/29/16
  Time: 10:58 AM
  To change this template use File | Settings | File Templates.
--%>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<%@ taglib prefix="fn" uri="http://java.sun.com/jsp/jstl/functions" %>
<%@taglib uri="http://java.sun.com/jsp/jstl/fmt" prefix="fmt"%>
<jsp:include page="include/header.jsp"></jsp:include>
<link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/v/dt/jqc-1.12.3/dt-1.10.12/datatables.min.css"/>
<% request.setAttribute("nav",2);%>
<div class="container" id="container">
    <div class="tabbar">
        <div class="weui_tab">
            <div class="weui_tab_bd">
                <div class="hd">
                    <h1 class="page_title">历史运单查询</h1>
                </div>

                <c:if test="${role==1}">
                    <div class="weui_cells_title">车载</div>
                    <div class="weui_cells">
                        <div class="weui_cell" style="padding-top: 0px;padding-bottom: 0px;">
                            <div class="weui_cell_bd weui_cell_primary">
                                <select class="weui_select" name="select2" id="car">
                                    <option value="0">全部</option>
                                    <c:forEach var="car" items="${allCars}">
                                        <c:choose>
                                            <c:when test="${car.id==carId}">
                                                <option value="${car.id}" selected>${car.storageName}</option>
                                            </c:when>
                                            <c:otherwise>
                                                <option value="${car.id}" >${car.storageName}</option>
                                            </c:otherwise>
                                        </c:choose>
                                    </c:forEach>
                                </select>
                            </div>
                        </div>
                    </div>

                    <div class="weui_cells_title">委托商</div>
                    <div class="weui_cells">
                        <div class="weui_cell" style="padding-top: 0px;padding-bottom: 0px;">
                            <div class="weui_cell_bd weui_cell_primary">
                                <select class="weui_select" name="select2" id="sender">
                                    <option value="0">全部</option>
                                    <c:forEach var="s" items="${allSenders}">
                                        <c:choose>
                                            <c:when test="${s.id==senderId}">
                                                <option value="${s.id}" selected>${s.fullName}</option>
                                            </c:when>
                                            <c:otherwise>
                                                <option value="${s.id}" >${s.fullName}</option>
                                            </c:otherwise>
                                        </c:choose>
                                    </c:forEach>
                                </select>
                            </div>
                        </div>
                    </div>
                </c:if>

                <div class="weui_cells_title">开始时间</div>
                <div class="weui_cells">
                    <div class="weui_cell">
                        <div class="weui_cell_bd weui_cell_primary">
                            <input class="weui_input" type="text" data-toggle='date' id="beginAt" value="${beginAt}" />
                        </div>
                    </div>
                </div>
                <div class="weui_cells_title">结束时间</div>
                <div class="weui_cells">
                    <div class="weui_cell">
                        <div class="weui_cell_bd weui_cell_primary">
                            <input class="weui_input" type="text" data-toggle='date'  id="signinAt" value="${signinAt}"/>
                        </div>
                    </div>
                </div>
                <div class="weui_btn_area">
                    <input type="button" id="querybutton" class="weui_btn weui_btn_primary btn-submit" value="历史运单查询" />
                </div>
                <div class="hd">
                    <input type="hidden" id="imgsrc" value="${imgsrc}"/>
                    <input type="hidden" id="role" value="${role}"/>
                <c:if test="${not empty beginAt && not empty signinAt}">
                    <c:if test="${fn:length(startendTimeList)<=0}">
                        <div class="alert alert-danger" style="text-align: center;">抱歉，没有找到对应的历史运单信息！</div>
                    </c:if>
                    <c:if test="${fn:length(startendTimeList)>0}">
                        <c:forEach items="${startendTimeList}" var="stl">
                            <table class="table" width="100%">
                                <thead>
                                <tr>
                                    <td></td>
                                    <td></td>
                                </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>运单编号</td>
                                        <td>${stl.number}</td>
                                    </tr>
                                    <tr>
                                        <td>寄货时间</td>
                                        <td><fmt:formatDate value="${stl.beginAt}" pattern="yyyy-MM-dd HH:mm:ss"/></td>
                                    </tr>
                                    <tr>
                                        <td>收货时间</td>
                                        <c:if test="${stl.stage==1}">
                                            <td><fmt:formatDate value="${stl.signinAt}" pattern="yyyy-MM-dd HH:mm:ss"/></td>
                                        </c:if>
                                        <c:if test="${stl.stage==0}">
                                            <td><i class="weui_icon_info"></i>&nbsp;未签收</td>
                                        </c:if>
                                    </tr>
                                    <c:if test="${role!=2}">
                                        <tr>
                                            <td>寄货单位</td>
                                            <td>${stl.senderOrg}</td>
                                        </tr>
                                    </c:if>
                                    <c:if test="${role!=2}">
                                        <tr>
                                            <td>寄件人</td>
                                            <td>${stl.senderPerson}</td>
                                        </tr>
                                    </c:if>
                                    <c:if test="${role!=3}">
                                        <tr>
                                            <td>收货单位</td>
                                            <td>${stl.receiverOrg}</td>
                                        </tr>
                                    </c:if>
                                    <c:if test="${role!=3}">
                                        <tr>
                                            <td>收货人</td>
                                            <td>${stl.receiverPerson}</td>
                                        </tr>
                                    </c:if>
                                    <tr>
                                        <td>运单状态</td>
                                        <c:if test="${stl.stage==0}">
                                            <td> <i class="weui_icon_waiting_circle"></i>&nbsp;运输中</td>
                                        </c:if>
                                        <c:if test="${stl.stage==1}">
                                            <td><a class="clickpicture" data-id="${stl.id}"><input type="button" class="weui_btn weui_btn_mini weui_btn_primary" value="已签收"/></a></td>
                                        </c:if>
                                    </tr>
                                    <tr>
                                        <td>详细</td>
                                        <td>
                                            <a href="/wechat/waybill/infotwo?number=${stl.number}"><input type="button" class="weui_btn weui_btn_mini weui_btn_primary" value="查看"/></a>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                            <br/>
                        </c:forEach>
                    </c:if>
                </c:if>
                </div>
            </div>
        </div>
        <jsp:include page="include/tabbar.jsp"></jsp:include>
    </div>
</div>
<jsp:include page="include/footer.jsp"></jsp:include>
</body>
<script type="text/javascript" src="/assets/js/lib/swiper.js" charset='utf-8'></script>
<script type="text/javascript" src="/assets/js/wechat/querytimehistory.js"></script>
<script type="text/javascript" src="https://cdn.datatables.net/1.10.12/js/jquery.dataTables.min.js"></script>
<script type="text/javascript">
    var queryTimeHistory = new QueryTimeHistory();
    $('.table').DataTable({
        "paging":   false,
        "ordering": false,
        'searching': false,
        'info': false
    });
</script>
</html>
