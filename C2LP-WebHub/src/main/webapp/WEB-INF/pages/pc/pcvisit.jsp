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
    <% request.setAttribute("nav",3);%>
    <div class="container-fluid">
        <jsp:include page="include/topnav.jsp"></jsp:include>
        <div class="row">
            <jsp:include page="include/leftnav.jsp"></jsp:include>
            <div class="col-lg-10 col-md-9">
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-home"></i>当前页面</a></li>
                    <li class="active" >登录统计</li>
                </ol>
            <div class="well">
                <form class="form-horizontal" style="margin-bottom: 0px;">
                    <div class="form-group" >
                        <label for="customer" class="col-md-2 control-label">客户</label>
                        <div class="col-md-4">
                            <div class="input-group">
                                <div class="input-group-addon">
                                    <span class="fa fa-user"></span>
                                </div>
                                <select class="form-control" id="customer">
                                    <option value="0">全部</option>
                                    <c:forEach var="c" items="${allCustomer}">
                                        <c:choose>
                                            <c:when test="${c.id==customerId}">
                                                <option value="${c.id}" selected>${c.fullName}</option>
                                            </c:when>
                                            <c:otherwise>
                                                <option value="${c.id}" >${c.fullName}</option>
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
                    <div class="form-group" style="margin-bottom: 0px;">
                        <div class="col-md-offset-2 col-md-2">
                            <button type="button" class="btn btn-success" id="querybutton"><i class="fa fa-search"></i>&nbsp;搜索</button>
                            <c:if test="${fn:length(visitList)>0}">
                                &nbsp;&nbsp;
                                <button type="button" class="btn btn-success" id="pdf">PDF导出</button>
                            </c:if>
                        </div>
                    </div>
                </form>
            </div>
                <input type="hidden" id="role" value="${role}"/>

                  <c:if test="${not empty beginAt && not empty signinAt}">
                    <c:if test="${fn:length(visitList)<=0}">
                        <div class="alert alert-danger" style="text-align: center;">抱歉，没有找到对应的访问记录！</div>
                    </c:if>
                    <c:if test="${fn:length(visitList)>0}">
                <table class="table table-striped" style="text-align: center;">
                    <tr class="success">
                        <td>客户名称</td>
                        <td>电脑登录次数</td>
                        <td>微信登录次数</td>
                        <td>登录总次数</td>
                    </tr>
                        <c:forEach items="${visitList}" var="v">
                            <tr>
                                <td>${v.fullName}</td>
                                <td>${v.pc}</td>
                                <td>${v.wechat}</td>
                                <td>${v.wechat+v.pc}</td>
                            </tr>
                          </c:forEach>
                      </c:if>
                   </c:if>
                </table>
            </div>
        </div>
    </div>
    <jsp:include page="include/footer_pc.jsp"></jsp:include>
    <script src="/assets/js/visit.js" type="text/javascript"></script>
    <script type="text/javascript">
        var startendtime = new Visit();
    </script>
    </body>
</html>
