<%--
  Created by IntelliJ IDEA.
  User: zhaoyou
  Date: 7/29/16
  Time: 10:56 AM
  To change this template use File | Settings | File Templates.
--%>
<%@ page contentType="text/html;charset=UTF-8" language="java" %>

<%-- 公共的css,以及头部文件在head.jsp页面定义, 每个页面,传递标题过去即可--%>
<% request.setAttribute("title", "账号绑定");%>
<jsp:include page="include/header.jsp"></jsp:include>

<div class="container" id="container">
    <div class="tabbar">
        <div class="weui_tab">
            <div class="weui_tab_bd">
                <div class="hd">
                    <h1 class="page_title">CCDCC</h1>
                    <p class="page_desc">车载实时数据</p>
                </div>

                <div class="bd">
                    <div class="weui_panel">
                        <div class="weui_panel_hd"><i class="weui_icon_success"></i>运行</div>
                        <div class="weui_panel_bd">
                            <div class="weui_media_box weui_media_text">
                                <h4 class="weui_media_title">沪C10001</h4>
                                <p class="weui_media_desc">T1：12.2℃  T2 10.8℃ T3 10.1℃ RH 58.3%</p>
                                <ul class="weui_media_info">
                                    <li class="weui_media_info_meta">长清路507号</li>
                                    <li class="weui_media_info_meta">2016-05-01 12:00</li>
                                    <li class="weui_media_info_meta weui_media_info_meta_extra"><a href="/car/carreal.html">地图</a></li>
                                </ul>
                            </div>
                            <div class="weui_media_box weui_media_text">
                                <h4 class="weui_media_title">沪C10001</h4>
                                <p class="weui_media_desc">T1：12.2℃  T2 10.8℃ T3 10.1℃ RH 58.3%</p>
                                <ul class="weui_media_info">
                                    <li class="weui_media_info_meta">长清路507号</li>
                                    <li class="weui_media_info_meta">2016-05-01 12:00</li>
                                    <li class="weui_media_info_meta weui_media_info_meta_extra"><a href="/car/carreal.html">地图</a></li>
                                </ul>
                            </div>
                            <div class="weui_media_box weui_media_text">
                                <h4 class="weui_media_title">沪C10001</h4>
                                <p class="weui_media_desc">T1：12.2℃  T2 10.8℃ T3 10.1℃ RH 58.3%</p>
                                <ul class="weui_media_info">
                                    <li class="weui_media_info_meta">长清路507号</li>
                                    <li class="weui_media_info_meta">2016-05-01 12:00</li>
                                    <li class="weui_media_info_meta weui_media_info_meta_extra">正常</li>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <div class="weui_panel">
                        <div class="weui_panel_hd"><i class="weui_icon_circle"></i>停止</div>
                        <div class="weui_panel_bd">
                            <div class="weui_media_box weui_media_small_appmsg">
                                <div class="weui_cells weui_cells_access">
                                    <a class="weui_cell" href="javascript:;">
                                        <div class="weui_cell_hd"><img src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAC4AAAAuCAMAAABgZ9sFAAAAVFBMVEXx8fHMzMzr6+vn5+fv7+/t7e3d3d2+vr7W1tbHx8eysrKdnZ3p6enk5OTR0dG7u7u3t7ejo6PY2Njh4eHf39/T09PExMSvr6+goKCqqqqnp6e4uLgcLY/OAAAAnklEQVRIx+3RSRLDIAxE0QYhAbGZPNu5/z0zrXHiqiz5W72FqhqtVuuXAl3iOV7iPV/iSsAqZa9BS7YOmMXnNNX4TWGxRMn3R6SxRNgy0bzXOW8EBO8SAClsPdB3psqlvG+Lw7ONXg/pTld52BjgSSkA3PV2OOemjIDcZQWgVvONw60q7sIpR38EnHPSMDQ4MjDjLPozhAkGrVbr/z0ANjAF4AcbXmYAAAAASUVORK5CYII=" alt="" style="width:20px;margin-right:5px;display:block"></div>
                                        <div class="weui_cell_bd weui_cell_primary">
                                            <p>沪C10001</p>
                                        </div>
                                        <span class="weui_cell_ft"></span>
                                    </a>
                                    <a class="weui_cell" href="javascript:;">
                                        <div class="weui_cell_hd"><img src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAC4AAAAuCAMAAABgZ9sFAAAAVFBMVEXx8fHMzMzr6+vn5+fv7+/t7e3d3d2+vr7W1tbHx8eysrKdnZ3p6enk5OTR0dG7u7u3t7ejo6PY2Njh4eHf39/T09PExMSvr6+goKCqqqqnp6e4uLgcLY/OAAAAnklEQVRIx+3RSRLDIAxE0QYhAbGZPNu5/z0zrXHiqiz5W72FqhqtVuuXAl3iOV7iPV/iSsAqZa9BS7YOmMXnNNX4TWGxRMn3R6SxRNgy0bzXOW8EBO8SAClsPdB3psqlvG+Lw7ONXg/pTld52BjgSSkA3PV2OOemjIDcZQWgVvONw60q7sIpR38EnHPSMDQ4MjDjLPozhAkGrVbr/z0ANjAF4AcbXmYAAAAASUVORK5CYII=" alt="" style="width:20px;margin-right:5px;display:block"></div>
                                        <div class="weui_cell_bd weui_cell_primary">
                                            <p>沪C10002</p>
                                        </div>
                                        <span class="weui_cell_ft"></span>
                                    </a>
                                    <a class="weui_cell" href="javascript:;">
                                        <div class="weui_cell_hd"><img src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAC4AAAAuCAMAAABgZ9sFAAAAVFBMVEXx8fHMzMzr6+vn5+fv7+/t7e3d3d2+vr7W1tbHx8eysrKdnZ3p6enk5OTR0dG7u7u3t7ejo6PY2Njh4eHf39/T09PExMSvr6+goKCqqqqnp6e4uLgcLY/OAAAAnklEQVRIx+3RSRLDIAxE0QYhAbGZPNu5/z0zrXHiqiz5W72FqhqtVuuXAl3iOV7iPV/iSsAqZa9BS7YOmMXnNNX4TWGxRMn3R6SxRNgy0bzXOW8EBO8SAClsPdB3psqlvG+Lw7ONXg/pTld52BjgSSkA3PV2OOemjIDcZQWgVvONw60q7sIpR38EnHPSMDQ4MjDjLPozhAkGrVbr/z0ANjAF4AcbXmYAAAAASUVORK5CYII=" alt="" style="width:20px;margin-right:5px;display:block"></div>
                                        <div class="weui_cell_bd weui_cell_primary">
                                            <p>沪C10003</p>
                                        </div>
                                        <span class="weui_cell_ft"></span>
                                    </a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%-- 页面底部的公共的tabbar菜单 --%>
        <jsp:include page="include/tabbar.jsp"></jsp:include>
    </div>
</div>
<jsp:include page="include/footer.jsp"></jsp:include>
<%-- 自定义的js文件在这个位置引用进来--%>
</body>
</html>

