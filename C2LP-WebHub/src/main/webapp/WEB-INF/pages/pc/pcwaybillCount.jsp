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
    <% request.setAttribute("nav",4);%>
    <div class="container-fluid">
        <jsp:include page="include/topnav.jsp"></jsp:include>
        <div class="row">
            <jsp:include page="include/leftnav.jsp"></jsp:include>
            <div class="col-lg-10 col-md-9">
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-home"></i>当前页面</a></li>
                    <li class="active" >运单统计</li>
                </ol>
            <div class="well">
                <form class="form-horizontal" style="margin-bottom:0px;">
                    <div class="form-group">
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
                            <div class="col-md-offset-2 col-md-4">
                                <button type="button" class="btn btn-success" id="querybutton"><i class="fa fa-search"></i>&nbsp;搜索</button>
                                <c:if test="${not empty list}">
                                    &nbsp;&nbsp; &nbsp;&nbsp;<button type="button" class="btn btn-success" id="pdf"><i class="fa fa-search"></i>&nbsp;数据导出</button>
                                </c:if>
                            </div>
                        </div>
                </form>
            </div>
                <input type="hidden" id="imgsrc" value="${imgsrc}"/>
                <input type="hidden" id="role" value="${role}"/>

                  <c:if test="${not empty beginAt && not empty signinAt}">
                    <c:if test="${fn:length(list)<=0}">
                        <div class="alert alert-danger" style="text-align: center;">抱歉，没有找到对应的历史信息！</div>
                    </c:if>
                    <c:if test="${fn:length(list)>0}">
                共${fn:length(list)}条记录
                <table class="table table-striped" style="text-align: center;">
                    <tr class="success">
                        <td>委托商</td>
                        <td>运单票数</td>
                        <td>件数</td>
                    </tr>
                        <c:forEach items="${list}" var="stl">
                            <tr>
                                <td>${stl.senderOrg}</td>
                                <td>${stl.count}</td>
                                <td>${stl.sum}</td>
                            </tr>
                          </c:forEach>
                      </c:if>
                   </c:if>
                </table>
            </div>
        </div>
    </div>
    <jsp:include page="include/footer_pc.jsp"></jsp:include>
    <script src="/assets/js/waybillCount.js" type="text/javascript"></script>
    <script type="text/javascript">
        var waybillCount = new WaybillCount();
    </script>
    </body>
</html>
