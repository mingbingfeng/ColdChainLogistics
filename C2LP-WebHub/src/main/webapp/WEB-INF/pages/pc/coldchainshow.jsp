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
                <jsp:include page="include/leftnav.jsp"></jsp:include>
                <div class="col-lg-10 col-md-9">
                    <ol class="breadcrumb">
                        <li><a href="#"><i class="fa fa-home"></i> &nbsp; 当前页面：</a></li>
                        <li><a href="/pc/waybill/show?number=${number}">运单编号：${waybillBase.number}</a></li>
                        <li class="active" >冷链信息展示</li>
                        <span class="pull-right"><a href="/pc/waybill/show?number=${number}"><input type="button" class="btn btn-primary btn-xs" value="返回"/></a></span>
                    </ol>
                    <input type="hidden" id="number" value="${waybillBase.number}"/>
                    <input type="hidden" id="coldid" value="${coldid}"/>
                    <input type="hidden" id="startTime" value="${startTime}"/>
                    <input type="hidden" id="endTime" value="${endTime}"/>
                    <input type="hidden" id="senderOrg" value="${waybillBase.senderOrg}"/>
                    <input type="hidden" id="receiverOrg" value="${waybillBase.receiverOrg}"/>
                    <input type="hidden" id="storageName" value="${storageName}"/>
                    <input type="hidden" id="storageId" value="${storageId}"/>
                    <input type="hidden" id="nodeid" value="${id}"/>
                    <div>
                        <div class="well" align="center">
                            <form class="form-horizontal">
                                <div class="form-group">
                                    <label class="col-md-2 control-label">运单编号</label>
                                    <div class="col-md-3">
                                        <div class="input-group">
                                            <input type="text" class = "form-control" style = "width:240px; text-align: center;" disabled = "disabled" value="${waybillBase.number}"/>
                                        </div>
                                    </div>
                                    <label class="col-md-2 control-label">车载名称</label>
                                    <div class="col-md-3">
                                        <div class="input-group">
                                            <input type="text" class = "form-control" style = "width:240px;text-align: center;" disabled = "disabled" value="${storageName}"/>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">发货地址</label>
                                    <div class="col-md-3">
                                        <div class="input-group">
                                            <input type="text" class = "form-control" style = "width:240px; text-align: center;" disabled = "disabled" value="${waybillBase.senderAddress}"/>
                                        </div>
                                    </div>
                                    <label class="col-md-2 control-label">收货地址</label>
                                    <div class="col-md-3">
                                        <div class="input-group">
                                            <input type="text" class = "form-control" style = "width:240px; text-align: center;" disabled = "disabled" value="${waybillBase.receiverAddress}"/>
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-2 control-label">开始时间</label>
                                    <div class="col-md-3">
                                        <div class="input-group">
                                            <input type="text" class="form-control" style="width:240px; text-align: center;" disabled="disabled" value="${startTime}"/>
                                        </div>
                                    </div>
                                    <label class="col-md-2 control-label">结束时间</label>
                                    <div class="col-md-3">
                                        <div class="input-group">
                                            <input type="text" class="form-control" style="width:240px; text-align: center;" disabled="disabled" value="${endTime}"/>
                                        </div>
                                    </div>
                                </div>
                            </form>
                        </div>
                        <div>
                            <c:if test="${empty aiinfoHistoryList || empty aiList}">
                                <input class="btn btn-primary btn-sm" id="nullpdf" type="button" value="导出PDF"/>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <input class="btn btn-primary btn-sm" id="nullmap" type="button" value="路线轨迹"/>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <input class="btn btn-primary btn-sm" id="nullzhexian" type="button" value="折现绘图"/>
                            </c:if>
                            <c:if test="${not empty aiinfoHistoryList && not empty aiList}">
                                <input class="btn btn-primary btn-sm" id="downpdf" type="button" value="导出PDF"/>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <c:if test="${storagetype==2}">
                                    <a href="/pc/waybill/coldchain/map?number=${number}&storageId=${storageId}&id=${id}"><input class="btn btn-primary btn-sm" type="button" value="路线轨迹"/></a>
                                </c:if>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <a href="/pc/waybill/coldDataPicture?number=${number}&storageId=${storageId}&id=${id}"><input class="btn btn-primary btn-sm" type="button" value="折线绘图"/></a>
                            </c:if>
                        </div>
                        <br/>
                        <div class="data-container">
                            <c:if test="${empty aiinfoHistoryList || empty aiList}">
                                <div class="alert alert-danger" style="text-align: center;">抱歉，没有相关的探头数据！</div>
                            </c:if>
                            <c:if test="${not empty aiinfoHistoryList && not empty aiList}">
                                <div>
                                    <strong>
                                        总共记录&nbsp;<code id="historyCount">${historyCount}</code>&nbsp;条
                                    </strong>
                                </div>
                                <table id="table" class="table table-striped text-center">
                                    <thead class="row">
                                    <tr class="success">
                                        <td><div style="min-width: 170px;">记录时间</div></td>
                                            <c:forEach var="ai" items="${aiList}">
                                                <td>
                                                   <div style="min-width: 85px;">${ai.pointName}</div>
                                                </td>
                                            </c:forEach>
                                        <td><div style="min-width: 65px;">报警状态</div></td>
                                    </tr>
                                    </thead>
                                    <tbody id="context">
                                    <c:forEach var="data" items="${aiinfoHistoryList}">
                                        <tr class="workTr">
                                            <td>
                                                ${data[0]}
                                            </td>
                                            <c:forEach begin="1" end="${fn:length(aiList)}" var="rrow">
                                                    <c:if test="${data[rrow]==null || data[rrow]==-300}">
                                                        <td>--</td>
                                                    </c:if>
                                                    <c:if test="${data[rrow]!=null && data[rrow]!=-300}">
                                                        <td><fmt:formatNumber value="${data[rrow]}" pattern="#.0"/></td>
                                                    </c:if>
                                            </c:forEach>
                                            <td align="center"  nowrap="nowrap">
                                                <c:if test="${data[fn:length(aiList)+1]==0}">
                                                    正常
                                                </c:if>
                                                <c:if test="${data[fn:length(aiList)+1]==1}">
                                                    <span style="color: red;">报警</span>
                                                </c:if>
                                            </td>
                                        </tr>
                                    </c:forEach>
                                    </tbody>
                                </table>
                                <div>
                                    <ul class="pager hide">
                                        <li><a href="javascript:void(0)" id="loadingWork">
                                            加载更多&nbsp;&nbsp;
                                            <i class="fa fa-long-arrow-down"></i>
                                            <i class="fa fa-spinner fa-spin hide"></i>
                                        </a></li>
                                    </ul>
                                </div>
                            </c:if>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    <jsp:include page="include/footer_pc.jsp"></jsp:include>
    <script src="/assets/js/coldchainpdf.js" type="text/javascript"></script>
    <script type="text/javascript">
        var coldchainpdf = new ColdChainPdf();
    </script>
  </body>
</html>
