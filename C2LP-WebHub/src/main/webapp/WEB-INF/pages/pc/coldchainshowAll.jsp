<%--
  Created by IntelliJ IDEA.
  User: wanghe
  Date: 2016/8/4
  Time: 13:22
  To change this template use File | Settings | File Templates.
--%>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<%@ taglib prefix="fn"  uri="http://java.sun.com/jsp/jstl/functions" %>
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<%@ taglib prefix="fmt" uri="http://java.sun.com/jsp/jstl/fmt" %>
<html>
    <jsp:include page="include/header_pc.jsp"></jsp:include>
    <body>
        <div class="container-fluid">
            <jsp:include page="include/topnav.jsp"></jsp:include>
            <div class="row">
                <% request.setAttribute("nav",1);%>
                <jsp:include page="include/leftnav.jsp"></jsp:include>
                <div class="col-lg-10 col-md-9">
                    <ol class="breadcrumb">
                        <li><a href="#"><i class="fa fa-home"></i> &nbsp; 当前页面：</a></li>
                        <li><a href="/pc/waybill/show?number=${waybillBase.number}">运单编号：${waybillBase.number}</a></li>
                        <li><a href="/pc/waybill/coldchainAll?number=${waybillBase.number}">运单全程冷链信息</a></li>
                    </ol>
                    <c:if test="${empty waybillBase}">
                        <div class="alert alert-danger">抱歉，没有找到对应的运单信息！</div>
                    </c:if>
                    <c:if test="${ not empty waybillBase}">
                        <div>
                            <div class="well" align="center">
                                <form class="form-horizontal" style="margin: 0px;">
                                    <div class="form-group" >
                                        <label class="col-md-2 control-label">运单编号</label>
                                        <div class="col-md-3">
                                            <div class="input-group">
                                                <input type="text" id="number" class = "form-control" style = "width:240px; text-align: center;" disabled = "disabled" value="${waybillBase.number}"/>
                                            </div>
                                        </div>
                                        <br/><br/>
                                        <label class="col-md-2 control-label">发货地址</label>
                                        <div class="col-md-3">
                                            <div class="input-group">
                                                <input type="text" class = "form-control" style = "width:240px; text-align: center;" disabled = "disabled" value="${waybillBase.senderAddress}"/>
                                            </div>
                                        </div>
                                        <label class="col-md-3 control-label">收货地址</label>
                                        <div class="col-md-3">
                                            <div class="input-group">
                                                <input type="text" class = "form-control" style = "width:240px; text-align: center;" disabled = "disabled" value="${waybillBase.receiverAddress}"/>
                                            </div>
                                        </div>
                                        <br/><br/>
                                        <label class="col-md-2 control-label">开始时间</label>
                                        <div class="col-md-3">
                                            <div class="input-group">
                                                <input type="text" class="form-control" style="width:240px; text-align: center;" disabled="disabled" value="<fmt:formatDate value="${waybillBase.beginAt}" pattern="yyyy-MM-dd HH:mm:ss"/>"/>
                                            </div>
                                        </div>
                                        <label class="col-md-3 control-label">结束时间</label>
                                        <div class="col-md-3">
                                            <div class="input-group">
                                                <input type="text" class="form-control" style="width:240px; text-align: center;" disabled="disabled" value="${empty waybillBase.signinAt ?'-- --':''}<fmt:formatDate value="${waybillBase.signinAt}" pattern="yyyy-MM-dd HH:mm:ss"/>"/>
                                            </div>
                                        </div>
                                        <br/><br/>
                                        <label class="col-md-2 control-label">发货人</label>
                                        <div class="col-md-3" >
                                            <div class="input-group" >
                                                <input type="text" class = "form-control" style = "width:240px; text-align: center;" disabled = "disabled" value="${waybillBase.senderPerson}"/>
                                            </div>
                                        </div>
                                        <label class="col-md-3 control-label">收货人</label>
                                        <div class="col-md-3">
                                            <div class="input-group">
                                                <input type="text" class = "form-control" style = "width:240px; text-align: center;" disabled = "disabled" value="${waybillBase.receiverPerson}"/>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group" style="margin: 0px;">
                                        <div class="col-md-offset-2 col-md-3" >
                                            <div style = "width:240px;text-align:left;">
                                                <a href="/pc/waybill/coldchainAll/export?number=${waybillBase.number}" target="_blank">
                                                    <input class="btn btn-success" id="downpdf" type="button" value="导出数据"/>
                                                </a>
                                            </div>
                                        </div>
                                    </div>

                                </form>
                            </div>
                            <ul class="nav nav-tabs">
                                <li role="presentation" class="active">
                                    <a href="/pc/waybill/coldchainAll?number=${waybillBase.number}"><b>数据</b></a>
                                </li>
                                <li role="presentation" >
                                    <a href="/pc/waybill/coldchainAll/map?number=${waybillBase.number}"><b>轨迹</b></a>
                                </li>
                                <li role="presentation" >
                                    <a href="/pc/waybill/coldDataPictureAll?number=${waybillBase.number}"><b>曲线</b></a>
                                </li>
                            </ul>
                            <br/>
                            <div class="data-container">
                                <c:if test="${empty allHistoryData}">
                                    <div class="alert alert-danger" style="text-align: center;">抱歉，没有相关的探头数据！</div>
                                </c:if>
                                <c:forEach items="${allHistoryData}" var="d" varStatus="s">
                                        <div>
                                            <strong>${d.storageName} &nbsp;</strong>-- ${fn:length(d.aiinfoHistoryList)}条记录
                                        </div>
                                        <table id="table" class="table table-striped text-center">
                                            <thead class="row">
                                            <tr class="success">
                                                <td><div style="min-width: 170px;">记录时间</div></td>
                                                <c:forEach var="ai" items="${d.aiList}">
                                                    <td>
                                                        <div style="min-width: 85px;">${ai.pointName}</div>
                                                    </td>
                                                </c:forEach>
                                                <td><div style="min-width: 65px;">报警状态</div></td>
                                            </tr>
                                            </thead>
                                            <tbody id="context">
                                            <c:forEach var="data" items="${d.aiinfoHistoryList}">
                                                <tr class="workTr">
                                                    <td>
                                                            ${data[0]}
                                                    </td>
                                                    <c:forEach begin="1" end="${fn:length(d.aiList)}" var="rrow">
                                                        <c:if test="${data[rrow]==null || data[rrow]==-300}">
                                                            <td>--</td>
                                                        </c:if>
                                                        <c:if test="${data[rrow]!=null && data[rrow]!=-300}">
                                                            <td><fmt:formatNumber value="${data[rrow]}" pattern="#.0"/></td>
                                                        </c:if>
                                                    </c:forEach>
                                                    <td align="center"  nowrap="nowrap">
                                                        <c:if test="${data[fn:length(d.aiList)+1]==0}">
                                                            正常
                                                        </c:if>
                                                        <c:if test="${data[fn:length(d.aiList)+1]==1}">
                                                            <span style="color: red;">报警</span>
                                                        </c:if>
                                                    </td>
                                                </tr>
                                            </c:forEach>
                                            </tbody>
                                        </table>
                                </c:forEach>
                            </div>
                        </div>
                    </c:if>
                </div>
            </div>
        </div>
    <jsp:include page="include/footer_pc.jsp"></jsp:include>
    <script src="/assets/js/coldchainAll.js" type="text/javascript"></script>
    <script type="text/javascript">
      //  var coldchaindata = new ColdChainData();
    </script>
  </body>
</html>
