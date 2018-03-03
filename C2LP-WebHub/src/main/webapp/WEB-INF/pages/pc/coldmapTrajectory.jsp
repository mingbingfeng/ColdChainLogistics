<%--
  Created by IntelliJ IDEA.
  User: wanghe
  Date: 2016/8/12
  Time: 11:04
  To change this template use File | Settings | File Templates.
--%>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>

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
            <jsp:include page="include/leftnav.jsp"></jsp:include>
            <div class="col-lg-10 col-md-9">
                <ol class="breadcrumb">
                    <li><a href="#"><i class="fa fa-home"></i> &nbsp;冷链信息</a></li>
                    <li class="active" >路线轨迹</li>
                </ol>
                <div id="center">
                    <input type="hidden" id="id" value="${id}"/>
                    <input type="hidden" id="number" value="${number}"/>
                    <input type="hidden" id="coldid" value="${coldid}"/>
                    <input type="hidden" id="senderOrg" value="${waybillBase.senderOrg}"/>
                    <input type="hidden" id="receiverOrg" value="${waybillBase.receiverOrg}"/>
                    <input type="hidden" id="startTime" value="${startTime}"/>
                    <input type="hidden" id="endTime" value="${endTime}"/>
                    <input type="hidden" id="storageName" value="${storageName}"/>
                    <input type="hidden" id="storageId" value="${storageId}"/>

                    <div class="well">
                        <form class="form-horizontal">
                            <div class="form-group">
                                <label class="col-md-2 control-label">出发地</label>
                                <div class="col-md-2">
                                    <div class="input-group">
                                        <input type="text" class = "form-control" style = "width:240px; text-align: center;" disabled = "disabled" value="${waybillBase.senderOrg}"/>
                                    </div>
                                </div>
                                <label class="col-md-3 control-label">目的地</label>
                                <div class="col-md-2">
                                    <div class="input-group">
                                        <input type="text" class = "form-control" style = "width:240px;text-align: center;" disabled = "disabled" value="${waybillBase.receiverOrg}"/>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-2 control-label">开始时间</label>
                                <div class="col-md-2">
                                    <div class="input-group">
                                        <input type="text" class="form-control" style="width:240px; text-align: center;" disabled="disabled" value="${startTime}"/>
                                    </div>
                                </div>
                                <label class="col-md-3 control-label">结束时间</label>
                                <div class="col-md-2">
                                    <div class="input-group">
                                        <input type="text" class="form-control" style="width:240px; text-align: center;" disabled="disabled" value="${endTime}"/>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-2 control-label">车载名称</label>
                                <div class="col-md-2">
                                    <div class="input-group">
                                        <input type="text" class = "form-control" style = "width:240px; text-align: center;" disabled = "disabled" value="${storageName}"/>
                                    </div>
                                </div>
                            </div>
                        </form>
                    </div>
                </div>

                <input class="btn btn-primary btn-sm" type="button"  onclick="window.open('/pc/waybill/coldchain/export?number=${number}&nodeid=${id}&coldid=${coldid}&storageName=${storageName}')" value="导出PDF"/>
                &nbsp;&nbsp;&nbsp;&nbsp;
                <a><input class="btn btn-primary btn-sm" type="button" id="but" onclick="window.location.href='/pc/waybill/coldchain?number=${number}&id=${id}&storageId=${storageId}';" value="返回冷链信息"/></a>
                <h1></h1>
                <div id="bottom">
                    <div id="map" class="view" style="width:100%; height: 510px;">
                    </div>
                </div>
            </div>
            <div class="clear"></div>
            </div>
    </div>
<jsp:include page="include/footer_pc.jsp"></jsp:include>
</body>
<script src="http://api.map.baidu.com/api?v=2.0&ak=vQYyBMOGiQkToVW5hzsITR7xem7Nr7oH" type="text/javascript"></script>
<script type="text/javascript" src="/assets/js/script/util.js"></script>
<script type="text/javascript" src="/assets/js/lib/async.min.js"></script>
<script type="text/javascript" src="/assets/js/CarHisMap.js"></script>
<script type="text/javascript">
    var carHisMap = new CarHisMap();
</script>
</html>