/**
 * Created by wanghe on 2016/8/8.
 */
var QueryTimeHistory = function() {
    this.init();
}
QueryTimeHistory.prototype.init = function () {
    $("#beginAt").calendar();
    $("#signinAt").calendar();
    $("#querybutton").click(this.queryButton.bind(this));
    $(".clickpicture").click(this.clickPicture);
}

QueryTimeHistory.prototype.queryButton = function() {
    var beginAt=$("#beginAt").val();
    var signinAt=$("#signinAt").val();
    var carId = $("#car").val();
    var senderId = $("#sender").val();
    if(!beginAt || !signinAt) {
        $.toast("请输入时间范围！", "cancel");
        return;
    }else if(beginAt>signinAt){
        $.toast("结束日期不能小于开始日期！", "cancel");
        return;
    }else{
        window.location.href = URLHelper.queryTimeHistoryUrl(beginAt,signinAt)+"&carId="+carId+"&senderId="+senderId;
    }
}

QueryTimeHistory.prototype.clickPicture = function(e) {
    var id=$(e.currentTarget).attr('data-id');
    var imgsrc=$("#imgsrc").val();
    URLHelper.ajax({
        'url': URLHelper.weclickPicture(id),
        'method': 'GET',
        'dataType': 'json',
        'success': function(data) {
            if(!data){
                $.toast('没有拍签收图片!',"forbidden");
            }
            if(data){
                var imgs=[];
                for(var i=0;i<data.length;i++){
                    imgs.push(imgsrc+data[i].picName);
                }
                    var pb1 = $.photoBrowser({
                        currentScale:20,
                        items:imgs
                    });
                pb1.width=100;
                pb1.hei=100;
                pb1.open();
            }
        },
        'error': function(err) {
        }
    });
}




