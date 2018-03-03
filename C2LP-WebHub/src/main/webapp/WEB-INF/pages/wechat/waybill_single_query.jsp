<%--
  Created by IntelliJ IDEA.
  User: zhaoyou
  Date: 7/29/16
  Time: 10:57 AM
  To change this template use File | Settings | File Templates.
--%>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>
<html>
<jsp:include page="include/header.jsp"></jsp:include>
<% request.setAttribute("nav",1);%>
<body>
<div class="container" id="container">
    <div class="tabbar">
        <div class="weui_tab">
            <div class="weui_tab_bd">
                <div class="hd">
                    <h1 class="page_title">物流运单查询</h1>
                    <br/>
                    <p class="page_desc">
                        <div class="weui_cells weui_cells_form">
                            <div class="weui_cell">
                                <div class="weui_cell_hd">
                                    <label class="weui_label">运单编号</label>
                                </div>
                                <div class="weui_cell_bd weui_cell_primary">
                                        <input class="weui_input" id="number" type="text" value="${number}" placeholder="请输入12位运单编号"/>
                                </div>
                            </div>
                        </div>
                        <div class="weui_btn_area">
                            <input type="button" class="weui_btn weui_btn_primary btn-waybillquery" value="运单查询" />
                        </div>
                    </p>
                </div>
            </div>
        </div>
        <jsp:include page="include/tabbar.jsp"></jsp:include>
    </div>
</div>
<jsp:include page="include/footer.jsp"></jsp:include>
<script type="text/javascript" src="/assets/js/wechat/WaybillQuery.js"></script>
<script type="text/javascript">
    var waybillQuery = new WaybillQuery();
</script>
</body>
</html>


