<%--
  Created by IntelliJ IDEA.
  User: zhaoyou
  Date: 7/29/16
  Time: 10:56 AM
  To change this template use File | Settings | File Templates.
--%>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<html>
    <jsp:include page="include/header_pc.jsp"></jsp:include>
    <body>
    <div class="container-fluid">
        <jsp:include page="include/topnav.jsp"></jsp:include>
        <div class="row">
            <jsp:include page="include/leftnav.jsp"></jsp:include>
            <div class="col-lg-10 col-md-9">
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-home"></i> &nbsp; 当前页面</a></li>
                    <li class="active" >修改密码</li>
                </ol>
                <div class="container">
                    <h3 class="page-header"><i class="fa fa-user"></i>&nbsp;修改密码</h3>
                    <div class="row">
                        <div class="col-lg-8 col-lg-offset-2 well">
                            <form class="form-horizontal" role="form">
                                <div class="form-group">
                                    <label for="password" class="col-sm-2 control-label">旧密码</label>
                                    <div class="col-sm-10">
                                        <div class="input-group">
                                            <div class="input-group-addon"><i class="fa fa-keyboard-o"></i></div>
                                            <input type="password" class="form-control mypwd" id="password" placeholder="旧密码">
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label for="newpassword" class="col-sm-2 control-label">新密码</label>
                                    <div class="col-sm-10">
                                        <div class="input-group">
                                            <div class="input-group-addon"><i class="fa fa-key fa-fw"></i></div>
                                            <input type="password" class="form-control newpwd" id="newpassword" placeholder="新密码">
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <label for="newpassword2" class="col-sm-2 control-label">确认新密码</label>
                                    <div class="col-sm-10">
                                        <div class="input-group">
                                            <div class="input-group-addon"><i class="fa fa-key fa-fw"></i></div>
                                            <input type="password" class="form-control pwdture" id="newpassword2" placeholder="确认新密码">
                                        </div>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-sm-offset-2 col-sm-10">
                                        <input type="button" class="btn btn-primary btn-ture" data-loading-text="更新中..." value="更新"/>
                                        &nbsp;&nbsp;
                                        <input type="reset" class="btn btn-default btn-cancel" value="重置"/>
                                    </div>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <jsp:include page="include/footer_pc.jsp"></jsp:include>
    <script type="text/javascript" src="/assets/js/updatepwd.js" ></script>
    <script type="text/javascript">
        var updatepwd = new UpdatePwd();
    </script>
    </body>
</html>