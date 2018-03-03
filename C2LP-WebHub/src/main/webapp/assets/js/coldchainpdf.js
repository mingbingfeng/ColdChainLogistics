/**
 * Created by wanghe on 2016/8/10.
 */
var ColdChainPdf = function() {
    this.init();
}
ColdChainPdf.prototype.init = function() {
    this.loadingShow();
    $("#downpdf").bind("click", this.getPdf);
    $('#nullpdf').click(this.nullpdf.bind(this));
    $('#nullmap').click(this.nullmap.bind(this));
    $("#loadingWork").click(this.loadingWork.bind(this));
//    $(window).scroll(function(){
//        if($(window).scrollTop() + $(window).height() == $(document).height()) {
//            if (document.getElementById('loadingWork')) {$('#loadingWork').click();}
//        }
//    });
}



ColdChainPdf.prototype.loadingShow = function () {
    if($(".workTr").length < $("#historyCount").text()){
        $(".pager").removeClass("hide");
    } else {
        $(".pager").addClass("hide");
    }
}

ColdChainPdf.prototype.loadingWork = function () {
    var pageNum =$(".workTr").length;
    var number=$("#number").val();
    var storageId=$("#storageId").val();
    var nodeid=$("#nodeid").val();
    var count=$("#historyCount").text();
    if(pageNum>=count){
        return false;
    }
    URLHelper.ajax({
        'url': URLHelper.coldChainPagingUrl(),
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
                notie.alert(1, '没有查找到数据!',2)
            }
            if(data!=""){
                var str = "";
                for(var i=0;i<data.length;i++){
                    str += "<tr class='workTr'>";
                    str += "<td>" + data[i][0] + "</td>";
                    for(var j=1;j<data[i].length-1;j++){
                        if(data[i][j]==null || data[i][j]==-300){
                            str += "<td>--</td>";
                        }else{
                            str += "<td>"+data[i][j].toFixed(1)+"</td>";
                        }
                    }
                    if(data[i][data[i].length-1]==0){
                        str += "<td>正常</td>";
                    }
                    if(data[i][data[i].length-1]==1){
                        str += "<td style='color: red;'>报警</td>";
                    }
                    str += "</tr>"
                }
                $("#context").append(str);
            }
        },
        'error': function(err) {
        }
    });
}

ColdChainPdf.prototype.getPdf = function() {
    var number = document.getElementById("number").value ;
    var nodeid = document.getElementById("nodeid").value ;
    var coldid = document.getElementById("coldid").value ;
    var storageName = document.getElementById("storageName").value ;
    window.open(URLHelper.downpdf(number,nodeid,coldid,storageName));
}

ColdChainPdf.prototype.nullpdf = function () {
    notie.alert(2,"探头数据为空不能PDF导出！",2);
}

ColdChainPdf.prototype.nullmap = function () {
    notie.alert(2,"探头数据为空没有路线轨迹记录！",2);
}

