<%--
  Created by IntelliJ IDEA.
  User: zhaoyou
  Date: 7/29/16
  Time: 11:03 AM
  To change this template use File | Settings | File Templates.
--%>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<%@ taglib prefix="c" uri="http://java.sun.com/jsp/jstl/core" %>
<%@ taglib prefix="fn" uri="http://java.sun.com/jsp/jstl/functions" %>
<%@ taglib prefix="fmt" uri="http://java.sun.com/jsp/jstl/fmt" %>
<html>
<jsp:include page="include/header.jsp"></jsp:include>
<body>
<div class="container" id="container">
    <div class="tabbar">
        <div class="weui_tab">
            <div class="weui_tab_bd">
                <div class="hd" style="padding-bottom: 5px;">
                    <h1 class="page_title">运单详情显示</h1>
                </div>
                <div class="weui_cells_title" align="center"><h2>运单信息</h2></div>
                <div class="weui_cells" style="font-size: 13px;">
                    <div class="weui_cell">
                        <div class="weui_cell_hd">
                            <label class="weui_label">运单编号</label>
                        </div>
                        <div class="weui_cell_bd weui_cell_primary">
                            <input class="weui_input" id="number" readonly type="number" value="${number}"/>
                        </div>
                    </div>
                    <div class="weui_cell">
                        <div class="weui_cell_hd">
                            <label class="weui_label">发货单位</label>
                        </div>
                        <div class="weui_cell_bd weui_cell_primary">
                            <input class="weui_input" id="senderOrg" readonly  value="${waybillBase.senderOrg}"/>
                        </div>
                    </div>
                    <div class="weui_cell">
                        <div class="weui_cell_hd">
                            <label class="weui_label">收货单位</label>
                        </div>
                        <div class="weui_cell_bd weui_cell_primary">
                            <input class="weui_input" id="receiverOrg" readonly value="${waybillBase.receiverOrg}"/>
                        </div>
                    </div>
                    <div class="weui_cell">
                        <div class="weui_cell_hd">
                            <label class="weui_label">发货地址</label>
                        </div>
                        <div class="weui_cell_bd weui_cell_primary">
                            <input class="weui_input" id="sendaddr" readonly value="${waybillBase.senderAddress}"/>
                        </div>
                    </div>
                    <div class="weui_cell">
                        <div class="weui_cell_hd">
                            <label class="weui_label">收货地址</label>
                        </div>
                        <div class="weui_cell_bd weui_cell_primary">
                            <input class="weui_input" id="receiveraddr" readonly value="${waybillBase.receiverAddress}"/>
                        </div>
                    </div>
                </div>
                <div class="weui_btn_area">
                    <a href="/wechat/cold/coldchainAlldata?number=${number}" >
                        <input type="button"   class="weui_btn  weui_btn_primary" value="冷链信息"  />
                    </a>
                </div>
                <div class="weui_btn_area">
                    <a href="/wechat/cold/coldchainRealmap?number=${number}" >
                        <input type="button"   class="weui_btn  weui_btn_primary " value="实时状态"/>
                    </a>
                </div>
                <input type="hidden" id="imgsrc" value="${imgsrc}"/>
                <c:if test="${fn:length(findNumberList)<=0}">
                    <div class="alert alert-danger">抱歉，没有找到对应的运单信息！</div>
                </c:if>
                <br/>
                <a href="javascript:;" class="weui_btn weui_btn_disabled weui_btn_default" style="cursor: none;margin-top:20px;">
                    <i class="weui_icon_waiting_circle"></i>&nbsp;物 流 节 点
                </a>
                <c:forEach items="${findNumberList}" var="numberlist">
                    <div class="weui_cells_title"><fmt:formatDate value="${numberlist.operateAt}" pattern="yyyy-MM-dd HH:mm:ss  E"/></div>
                    <div class="weui_cells weui_cells_form">
                            <div class="weui_cell" style="font-size: 13px;">
                                <div class="weui_cell_bd weui_cell_primary">
                                    <c:if test="${numberlist.arrived==2}">
                                        <c:if test="${empty pictures}">
                                            ${numberlist.content}
                                        </c:if>
                                        <c:if test="${not empty pictures}">
                                            ${numberlist.content}&nbsp;&nbsp;<input type="button" data-id="${waybillBase.id}" onclick="showpicture();" class="weui_btn weui_btn_mini weui_btn_primary" value="签收图片"/>
                                        </c:if>
                                    </c:if>
                                    <c:if test="${numberlist.arrived==1}">
                                            <a href="/wechat/cold/coldchaindata?number=${number}&storageId=${numberlist.storageId}&id=${numberlist.nodeid}">
                                                    ${numberlist.content}
                                            </a>
                                    </c:if>
                                    <p style="display: none;">${numberlist.storageId}${numberlist.nodeid}</p>
                                </div>
                            </div>
                    </div>
                </c:forEach>
            </div>
        </div>
        <c:forEach items="${pictures}" var="t">
            <input type="hidden" name="picName" class="picName" value="${t.picName}"/>
        </c:forEach>
        <jsp:include page="include/tabbar.jsp"></jsp:include>
    </div>
</div>
<jsp:include page="include/footer.jsp"></jsp:include>
<script type="text/javascript" src="/assets/js/lib/swiper.js" charset='utf-8'></script>
<script type="text/javascript" src="/assets/js/wechat/waybillpicture.js"></script>
<script type="text/javascript">
      function showpicture(){
          var imgsrc = $("#imgsrc").val();
          var imgs=[];
          $.each($('.picName'), function(index, obj){
              imgs.push(imgsrc + obj.value);
          });

          var pb1 = $.photoBrowser({
              currentScale:20,
              items:imgs
          });
          pb1.width=150;
          pb1.hei=100;
          pb1.open();
      }
</script>
</body>
</html>

