<%--
  Created by IntelliJ IDEA.
  User: wanghe
  Date: 2016/7/28
  Time: 15:38
  To change this template use File | Settings | File Templates.
--%>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<%@ taglib prefix="form" uri="http://www.springframework.org/tags/form" %>
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<%@ taglib prefix="fn" uri="http://java.sun.com/jsp/jstl/functions" %>
<%@ taglib prefix="fmt" uri="http://java.sun.com/jsp/jstl/fmt" %>

<html>
    <jsp:include page="include/header_pc.jsp"></jsp:include>
    <body>
        <div class="container-fluid">
            <jsp:include page="include/topnav.jsp"></jsp:include>
            <div class="row">
                <% request.setAttribute("nav",1);%>
                <jsp:include page="include/leftnav.jsp"></jsp:include>
                <div class="col-lg-10 col-md-9">
                    <ol class="breadcrumb">
                        <li><a href="#"><i class="fa fa-home"></i> &nbsp; 当前页面</a></li>
                        <li class="active" >运单查询</li>
                    </ol>
                    <div>
                        <div class="col-md-offset-3 col-md-6">
                            <input type="text" class="form-control" value="${number}" style="height: 40px;" id="number" placeholder="请输入12位的运单编号"/>
                        </div>
                        <button type="button" id="query" style="height: 40px;position: relative; top:-40px;" class="btn btn-primary col-md-offset-9">查询</button>
                        <c:if test="${not empty number}">
                            <c:if test="${fn:length(findNumberList)<=0}">
                                <div class="alert alert-danger">抱歉，没有找到对应的运单信息！</div>
                            </c:if>
                            <c:if test="${fn:length(findNumberList)>0}">
                                <div class="table-responsive">
                                    <div class="well" align="center">
                                        <form class="form-horizontal">
                                            <div class="form-group">
                                                <label class="col-md-2 control-label">发货单位</label>
                                                <div class="col-md-3">
                                                    <div class="input-group">
                                                        <input type="text" class = "form-control" style = "width:240px; text-align: center;" disabled = "disabled" value="${waybillBase.senderOrg}"/>
                                                    </div>
                                                </div>
                                                <label class="col-md-2 control-label">收货单位</label>
                                                <div class="col-md-3">
                                                    <div class="input-group">
                                                        <input type="text" class = "form-control" style = "width:240px;text-align: center;" disabled = "disabled" value="${waybillBase.receiverOrg}"/>
                                                    </div>
                                                </div>
                                                <br/><br/>
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
                                                <br/><br/>
                                                <label class="col-md-2 control-label">开始时间</label>
                                                <div class="col-md-3">
                                                    <div class="input-group">
                                                        <input type="text" class="form-control" style="width:240px; text-align: center;" disabled="disabled" value="<fmt:formatDate value="${waybillBase.beginAt}" pattern="yyyy-MM-dd HH:mm:ss"/>"/>
                                                    </div>
                                                </div>
                                                <label class="col-md-2 control-label">结束时间</label>
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
                                                <label class="col-md-2 control-label">收货人</label>
                                                <div class="col-md-3">
                                                    <div class="input-group">
                                                        <input type="text" class = "form-control" style = "width:240px; text-align: center;" disabled = "disabled" value="${waybillBase.receiverPerson}"/>
                                                    </div>
                                                </div>
                                                <br/><br/>
                                                <label class="col-md-2 control-label">发货电话</label>
                                                <div class="col-md-3" >
                                                    <div class="input-group" >
                                                        <input type="text" class = "form-control" style = "width:240px; text-align: center;" disabled = "disabled" value="${waybillBase.senderTel}"/>
                                                    </div>
                                                </div>
                                                <label class="col-md-2 control-label">收货电话</label>
                                                <div class="col-md-3">
                                                    <div class="input-group">
                                                        <input type="text" class = "form-control" style = "width:240px; text-align: center;" disabled = "disabled" value="${waybillBase.receiverTel}"/>
                                                    </div>
                                                </div>

                                            </div>
                                        </form>
                                    </div>
                                    <table class="table table-striped">
                                        <a href="/pc/waybill/coldchainAll?number=${number}" target="_blank">
                                            <input  type="button" class="btn btn-sm btn-primary" id="coldchain" value="全程冷链信息"/>
                                        </a> &nbsp;&nbsp;
                                        <a href="/pc/waybill/coldchainRealdata?number=${number}" target="_blank">
                                            <input type="button" class="btn btn-sm btn-primary"  value="实时状态"/>
                                        </a>
                                        </br></br>
                                        <tr class="success">
                                            <td style = "width:360px;">日期</td>
                                            <td>物流信息</td>
                                        </tr>
                                        <c:forEach items="${findNumberList}" var="numberlist">
                                            <tr class="active" style="font-size: 13px;">
                                                <td style = "width:360px;"><fmt:formatDate value="${numberlist.operateAt}" pattern="yyyy-MM-dd HH:mm:ss E"/></td>
                                                <input type="hidden" id = "new ">
                                                <c:if test="${numberlist.arrived==2}">
                                                    <c:if test="${empty pictures}">
                                                        <td>${numberlist.content}</td>
                                                    </c:if>
                                                    <c:if test="${not empty pictures}">
                                                        <td>${numberlist.content}&nbsp;&nbsp;
                                                            <input type="button" class="btn btn-sm btn-primary" onclick="$('#myModal').modal('show');" data-toggle="modal" value="签收图片"/>
                                                                <%--SELECT * from waybill_node n,waybill_base b WHERE n.baseId=b.id and b.number=057410007052 057410023414;--%>
                                                        </td>
                                                    </c:if>
                                                </c:if>
                                                <c:if test="${numberlist.arrived==1}">
                                                    <td><a href="/pc/waybill/coldchain?number=${number}&storageId=${numberlist.storageId}&id=${numberlist.nodeid}">${numberlist.content}</a></td>
                                                </c:if>
                                                    <td style="display: none;">${numberlist.storageId}${numberlist.nodeid}</td>
                                            </tr>
                                        </c:forEach>

                                    </table>
                                    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                                        <div class="modal-dialog" role="document">
                                            <div class="modal-content">
                                                <div class="modal-header">
                                                    <h4 class="modal-title" id="myModalLabel">签收图片</h4>
                                                </div>
                                                <input type="hidden" id="imgsrc" value="${imgsrc}"/>
                                                <div class="modal-body imgs" id="picture" align="center">
                                                       <c:forEach items="${pictures}" var="pictures">
                                                             <img src="${imgsrc}${pictures.picName}" style="width: 320px; height: 240px; margin-top: 25px;">
                                                       </c:forEach>
                                                </div>
                                                <div class="modal-footer">
                                                    <button type="button" class="btn btn-default" id="close" data-dismiss="modal">关闭</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </c:if>
                        </c:if>
                    </div>
                </div>
            </div>
        </div>
        <jsp:include page="include/footer_pc.jsp"></jsp:include>
        <script src="/assets/js/numberquery.js" type="text/javascript"></script>
        <script type="text/javascript">
            var numberquery = new NumberQuery();
        </script>
    </body>
</html>
