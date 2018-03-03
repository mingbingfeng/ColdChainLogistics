<%--
  Created by IntelliJ IDEA.
  User: zhaoyou
  Date: 7/27/16
  Time: 10:49 PM
  To change this template use File | Settings | File Templates.
--%>
<%@ page language="java" pageEncoding="utf-8"%>
<html>
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="shortcut icon" type="image/x-icon" href="/assets/img/thermoberg.png" />
    <title>${title}</title>
</head>
<jsp:include page="../pc/include/yincss.jsp"></jsp:include>
    <body>
    <div class="container" style="position:relative; top: 150px; ">
        <div class="row">
            <div class="col-md-6 col-md-offset-3">
                <div class="login-panel panel panel-default">
                    <div class="panel-heading">
                        <h3 class="panel-title text-center"><i class="fa fa-home"></i>&nbsp;欢迎进入惊尘冷链物流追溯系统</h3>
                    </div>
                    <div class="panel-body">
                        <form role="form">
                            <fieldset>
                                <div class="form-group">
                                    <div class="input-group">
                                        <div class="input-group-addon"><i class="fa fa-user"></i></div>
                                        <input type="text" class="form-control  account" placeholder="企业账号">
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="input-group">
                                        <div class="input-group-addon"><i class="fa fa-user"></i></div>
                                        <input type="text" class="form-control  username" placeholder="企业用户">
                                    </div>
                                </div>
                                <div class="form-group">
                                    <div class="input-group">
                                        <div class="input-group-addon"><i class="fa fa-eye"></i></div>
                                        <input type="password" class="form-control password" placeholder="用户密码">
                                    </div>
                                </div>
                                <div class="form-group">
                                    <input type="hidden" class="redirectUrl" value="${redirectUrl}"/>
                                    <button class="btn btn-success btn-lg btn-block btn-submit" data-loading-text="登陆中..." type="button"><i class="fa fa-sign-in"></i>&nbsp;登陆系统</button>
                                </div>
                            </fieldset>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <jsp:include page="../pc/include/yinjs.jsp"></jsp:include>
    <script type="text/javascript" src="/assets/js/userlogin.js" ></script>
    <script type="text/javascript" src="/assets/js/util.js" ></script>
    <script type="text/javascript">
        var login = new UserLogin();
    </script>
    </body>
</html>

