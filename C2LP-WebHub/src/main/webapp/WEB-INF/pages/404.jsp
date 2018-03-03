<%--
  Created by IntelliJ IDEA.
  User: zhaoyou
  Date: 11/17/14
  Time: 11:57
  To change this template use File | Settings | File Templates.
--%>
<%@ page contentType="text/html;charset=UTF-8" language="java" isErrorPage="true" %>
<!DOCTYPE html>
<html lang="en">
<link href="/assets/css/bootstrap.min.css" rel="stylesheet">
<link href="/assets/css/bootstrap-theme.min.css" rel="stylesheet">
<link href="/assets/css/font-awesome.min.css" rel="stylesheet">

<meta charset="utf-8">
<meta http-equiv="X-UA-Compatible" content="IE=edge">
<meta name="viewport" content="width=device-width, initial-scale=1">
<% request.setAttribute("title", "你查找的页面不存在喔 ！");%>
<div class="container">
    <div class="bs-callout bs-callout-danger" style="padding-top: 50px;">
        <h3><i class="fa fa-plug"></i>&nbsp;404:请求的资源不存在!</h3>
    </div>
    <code>
        <h5>错误:<%=exception%>
        </h5>
    </code>
</div>
<script src="/assets/js/lib/jquery.min.js"></script>

</html>