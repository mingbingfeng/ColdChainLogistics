<%--
  Created by IntelliJ IDEA.
  User: zhaoyou
  Date: 7/29/16
  Time: 1:26 PM
  To change this template use File | Settings | File Templates.
--%>
<html>
    <body>
        <%@ page contentType="text/html;charset=UTF-8" language="java" %>
        <% request.setAttribute("title", "模板");%>
        <jsp:include page="include/header_pc.jsp"></jsp:include>
        <div class="container-fluid">
            <jsp:include page="include/topnav.jsp"></jsp:include>
            <div class="row">
                <jsp:include page="include/leftnav.jsp"></jsp:include>
                <div class="col-lg-9 col-md-9">
                    <ol class="breadcrumb">
                        <li><a href="#"><i class="fa fa-home"></i> &nbsp; 系统</a></li>
                        <li class="active" ><a href="#">运单查询</a></li>
                    </ol>
                    <!--    每一个页面的内容就是写在这里    -->
                </div>
            </div>
        </div>
        <jsp:include page="include/footer_pc.jsp"></jsp:include>
    </body>
</html>