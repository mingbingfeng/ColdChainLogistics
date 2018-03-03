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
    <style>
        .BMapLabel {
            max-width: none;
        }
    </style>
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
                        <li class="active" >路线轨迹</li>
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
                        </div>
                        <ul class="nav nav-tabs">
                            <li role="presentation" >
                                <a href="/pc/waybill/coldchainAll?number=${waybillBase.number}"><b>数据</b></a>
                            </li>
                            <li role="presentation" class="active">
                                <a href="/pc/waybill/coldchainAll/map?number=${waybillBase.number}"><b>轨迹</b></a>
                            </li>
                            <li role="presentation" >
                                <a href="/pc/waybill/coldDataPictureAll?number=${waybillBase.number}"><b>曲线</b></a>
                            </li>
                        </ul>
                       <%-- <a href="/pc/waybill/coldchainAll/map?number=${waybillBase.number}"><input class="btn btn-primary btn-sm" type="button" value="轨迹"/></a>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <a href="/pc/waybill/coldchainAll?number=${waybillBase.number}"><input class="btn btn-primary btn-sm" type="button" value="数据"/></a>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <a href="/pc/waybill/coldDataPictureAll?number=${waybillBase.number}"><input class="btn btn-primary btn-sm" type="button" value="曲线"/></a>--%>
                        <br/>
                        <div class="alert alert-danger"  style="text-align: center;display: none;">抱歉，没有相关的探头数据！</div>
                        <div id="bottom">
                            <div id="map" class="view" style="width:100%; height: 510px;"></div>
                            </br>
                        </div>
                    </c:if>
                </div>
                <div class="clear"></div>
            </div>
        </div>
    <jsp:include page="include/footer_pc.jsp"></jsp:include>
  </body>
    <script src="http://api.map.baidu.com/api?v=2.0&ak=vQYyBMOGiQkToVW5hzsITR7xem7Nr7oH" type="text/javascript"></script>
    <script type="text/javascript" src="/assets/js/script/util.js"></script>
    <script type="text/javascript" src="/assets/js/lib/async.min.js"></script>
    <script type="text/javascript" src="/assets/js/TruckHisMap.js"></script>
    <script type="text/javascript">
        var truckHisMap = new TruckHisMap();
    </script>
</html>
