<%--
  Created by IntelliJ IDEA.
  User: zhaoyou
  Date: 7/29/16
  Time: 1:26 PM
  To change this template use File | Settings | File Templates.
--%>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<div class="col-lg-2 col-md-3">
    <div class="list-group" style="text-align: center;">
            <a href="/pc/waybill/query" class="list-group-item  ${nav==1 ? 'active' : ''}"><i class="fa fa-search"></i>&nbsp;运 单 查 询</a>
            <a href="/pc/waybill/search" class="list-group-item  ${nav==2 ? 'active' : ''}"><i class="fa fa-list"></i>&nbsp;历 史 运 单</a>
            <c:if test="${role==1}"><a href="/pc/waybill/countCar" class="list-group-item  ${nav==5 ? 'active' : ''}"><i class="fa fa-truck"></i>&nbsp;<span>车 载 统 计</span></a></c:if>
            <c:if test="${role==1}"><a href="/pc/waybill/count" class="list-group-item  ${nav==4 ? 'active' : ''}"><i class="fa fa-list"></i>&nbsp;运 单 统 计</a></c:if>
            <c:if test="${role==1}"><a href="/pc/waybill/visit" class="list-group-item  ${nav==3 ? 'active' : ''}"><i class="fa fa-reorder"></i>&nbsp;登 录 统 计</a></c:if>
    </div>
</div>