/**
 * Created by wanghe on 2016/9/9.
 */
var WaybillInfoDataNull = function() {
    this.init();
}

WaybillInfoDataNull.prototype.init = function () {
    this.loadingShow();
    $('#nullbutton').click(this.nullbutton.bind(this));
    $("#loadingWork").click(this.loadingWork.bind(this));

    $(window).scroll(function(){
        if($(window).scrollTop() + $(window).height() == $(document).height()) {
            if (document.getElementById('loadingWork')) {$('#loadingWork').click();}
        }
    });
}

WaybillInfoDataNull.prototype.loadingShow = function () {
    if($(".workTr").length < $("#historyCount").text()){
        $(".pager").removeClass("hide");
    } else {
        $(".pager").hide();
    }
}

WaybillInfoDataNull.prototype.loadingWork = function () {
    $.showLoading("数据加载中");
    var pageNum =$(".workTr").length;
    var number=$("#number").val();
    var storageId=$("#storageId").val();
    var nodeid=$("#nodeid").val();
    var count=$("#historyCount").text();
    var aiList=JSON.parse($("#aiList").val());

    if(pageNum <10){
        document.getElementById("loadingWork").visibility="visible";
    }
    if(pageNum >= count){
        $.hideLoading();
        return false;
    }
    URLHelper.ajax({
        'url': URLHelper.wechatcoldChainPagingUrl(),
        'method': 'GET',
        'dataType': 'json',
        'data': {
            'number':number,
            'storageId':storageId,
            'nodeid':nodeid,
            'pageNum':pageNum
        },
        'success': function(data) {
            if(data==""){
                $.hideLoading();
                notie.alert(1, '没有查找到数据!',2)
            }
            if(data!=""){
                $.hideLoading();
                var str = "";
                for(var i=0;i<data.length;i++){
                    str +="<div class='workTr'>";
                        str +="<div class='weui_cells_title'><i class='fa fa-calendar'></i>&nbsp;" + data[i][0] + "</div>";
                            str += "<div class='weui_cells'>";
                            for(var j=1;j<data[i].length-1;j++){
                                str += "<div class='weui_cell' >";
                                    str += "<div class='weui_cell_bd weui_cell_primary'>";
                                        str+= "<div>"+aiList[j-1].pointName+"</div>";
                                    str += "</div>";
                                    str += "<div class='weui_cell_ft'>";
                                    if(data[i][j]==null || data[i][j]==-300){
                                        str += "<p>--</p>";
                                    }else{
                                        str += "<p>"+data[i][j].toFixed(1)+"</p>";
                                    }
                                    str += "</div>";
                                str +="</div>";
                            }
                            str += "<div class='weui_cell' >";
                                str += "<div class='weui_cell_bd weui_cell_primary'>";
                                    str  += "<p>报警状态</p>";
                                str += "</div>";
                                str += "<div class='weui_cell_ft'>";
                                    if(data[i][data[i].length-1]==0){
                                       str += "正常";
                                    }
                                    if(data[i][data[i].length-1]==1) {
                                        str += "<span style='color: red;'>报警</span>";
                                    }
                            str += "</div>";
                            str += "</div>";
                        str +="</div>";
                    str +="</div>";
                }
                $(".context").append(str);
            }
        },
        'error': function(err) {
        }
    });
}

WaybillInfoDataNull.prototype.nullbutton = function () {
    $.toast("探头数据为空没有路线轨迹记录！", "cancel");
}