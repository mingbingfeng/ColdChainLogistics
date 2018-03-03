<%--
  Created by IntelliJ IDEA.
  User: wanghe
  Date: 2016/8/2
  Time: 14:10
  To change this template use File | Settings | File Templates.
--%>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<%@ taglib prefix="fn" uri="http://java.sun.com/jsp/jstl/functions" %>
<%@taglib uri="http://java.sun.com/jsp/jstl/fmt" prefix="fmt"%>
<html>
    <jsp:include page="include/header_pc.jsp"></jsp:include>
    <body>
    <% request.setAttribute("nav",2);%>
    <div class="container-fluid">
        <jsp:include page="include/topnav.jsp"></jsp:include>
        <div class="row">
            <jsp:include page="include/leftnav.jsp"></jsp:include>
            <div class="col-lg-10 col-md-9">
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-home"></i>当前页面</a></li>
                    <li class="active" >历史运单查询</li>
                </ol>
            <div class="well">
                <form class="form-horizontal" style="margin-bottom:0px;">
                    <div class="form-group">

                        <c:if test="${role==1}">
                            <label for="car" class="col-md-2 control-label">车载</label>
                            <div class="col-md-3 ">
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        <span class="fa fa-truck"></span>
                                    </div>
                                    <select class="form-control" id="car">
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
                            <label for="sender" class="col-md-2 control-label">委托商</label>
                            <div class="col-md-4 ">
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        <span class="glyphicon glyphicon-log-out"></span>
                                    </div>
                                    <select class="form-control" id="sender">
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
                            <br/><br/><br/>
                        </c:if>

                        <label for="beginAt" class="col-md-2 control-label">开始时间</label>
                        <div class="col-md-3">
                            <div class="input-group">
                                <div class="input-group-addon">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </div>
                                <input type="text" class="form-control" name="beginAt" id="beginAt" value="${beginAt}" placeholder="无"/>
                            </div>
                        </div>
                        <label for="signinAt" class="col-md-2 control-label">结束时间</label>
                        <div class="col-md-3">
                            <div class="input-group">
                                <div class="input-group-addon">
                                    <span class="glyphicon glyphicon-calendar"></span>
                                </div>
                                <input type="text" class="form-control" name="signinAt" id="signinAt" value="${signinAt}" placeholder="无"/>
                            </div>
                        </div>
                    </div>
                        <div class="form-group" style="margin-bottom:0px;">
                            <div class="col-md-offset-2 col-md-2">
                                <button type="button" class="btn btn-success" id="querybutton"><i class="fa fa-search"></i>&nbsp;搜索</button>
                            </div>
                        </div>
                </form>
            </div>
                <input type="hidden" id="imgsrc" value="${imgsrc}"/>
                <input type="hidden" id="role" value="${role}"/>

                  <c:if test="${not empty beginAt && not empty signinAt}">
                    <c:if test="${fn:length(startendTimeList)<=0}">
                        <div class="alert alert-danger" style="text-align: center;">抱歉，没有找到对应的历史运单信息！</div>
                    </c:if>
                    <c:if test="${fn:length(startendTimeList)>0}">
                共${fn:length(startendTimeList)}条记录
                <table class="table table-striped" style="text-align: center;">
                    <tr class="success">
                        <td>寄货时间</td>
                        <td>收货时间</td>
                        <c:if test="${role!=2}">
                           <td>寄货单位</td>
                            <td>寄件人</td>
                        </c:if>

                        <c:if test="${role!=3}">
                            <td>收货单位</td>
                            <td>收货人</td>
                        </c:if>

                        <td>运单状态</td>
                        <td>详细</td>
                    </tr>
                        <c:forEach items="${startendTimeList}" var="stl">
                            <tr>
                                <td style="display: none;" class="id">${stl.id}</td>
                                <td style="display: none;">${stl.number}</td>
                                <td><fmt:formatDate value="${stl.beginAt}" pattern="yyyy-MM-dd HH:mm:ss"/></td>
                                <c:if test="${stl.stage==1}">
                                    <td><fmt:formatDate value="${stl.signinAt}" pattern="yyyy-MM-dd HH:mm:ss"/></td>
                                </c:if>
                                <c:if test="${stl.stage==0}">
                                    <td>--</td>
                                </c:if>

                                <c:if test="${role!=2}">
                                    <td>${stl.senderOrg}</td>
                                    <td>${stl.senderPerson}</td>
                                </c:if>

                                <c:if test="${role!=3}">
                                    <td>${stl.receiverOrg}</td>
                                    <td>${stl.receiverPerson}</td>
                                </c:if>

                                <c:if test="${stl.stage==0}">
                                    <td>运输中</td>
                                </c:if>
                                <c:if test="${stl.stage==1}">
                                    <td><a class="clickpicture" data-toggle="modal" data-id="${stl.id}">已签收</a></td>
                                </c:if>
                                <td>
                                    <input type="button" class="btn btn-default" onclick="window.open('/pc/waybill/show?number=${stl.number}')" value="查看"/>
                                </td>
                            </tr>
                          </c:forEach>
                      </c:if>
                   </c:if>
                </table>
                <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                    <div class="modal-dialog" role="document">
                        <div class="modal-content">
                            <div class="modal-header">
                                <h4 class="modal-title" id="myModalLabel">签收图片</h4>
                            </div>
                            <div class="modal-body imgs" id="picture" align="center">

                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-default" id="close" data-dismiss="modal">关闭</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <jsp:include page="include/footer_pc.jsp"></jsp:include>
    <script src="/assets/js/startendtime.js" type="text/javascript"></script>
    <script type="text/javascript">
        var startendtime = new StartEndTime();
    </script>
    </body>
</html>
