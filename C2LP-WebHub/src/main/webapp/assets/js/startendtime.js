/**
 * Created by wanghe on 2016/8/8.
 */
var StartEndTime = function() {
    this.init();
}
StartEndTime.prototype.init = function () {
    $("select").select2({allowClear: true});
    $('#beginAt,#signinAt').datetimepicker({
        viewMode:'years',
        format:'YYYY-MM-DD HH:mm:ss',
        locale: 'zh-cn'
    });
    $("#querybutton").bind("click", this.queryButton);
    $(".clickpicture").bind("click", this.clickPicture);
}

StartEndTime.prototype.queryButton = function() {
    var beginAt=$("#beginAt").val();
    var signinAt=$("#signinAt").val();
    var carId = $("#car").val();
    var senderId = $("#sender").val();
    if(!beginAt || !signinAt) {
        notie.alert(2,"开始或结束时间不能为空！",2);
        return;
    }else if(beginAt>signinAt){
        notie.alert(3,"结束日期不能小于开始日期！",2);
        return;
    }else{
        NProgress.start();
        window.location.href = URLHelper.StartEndUrl(beginAt,signinAt)+"&carId="+carId+"&senderId="+senderId;
    }
}

StartEndTime.prototype.clickPicture = function() {
    var id=$(this).attr('data-id');
    var imgsrc=$("#imgsrc").val();
    URLHelper.ajax({
        'url': URLHelper.clickPicture(id),
        'method': 'GET',
        'dataType': 'json',
        'success': function(data) {
            if(data==""){
                notie.alert(2, '没有拍签收图片!',2)
            }
            if(data!=""){
                //每次进来先清空
                $('.imgs').html('');
                // 创建图片对象
                for(var i=0;i<data.length;i++){
                    var img = document.createElement("img");
                    img.width = "320";
                    img.height = "240";
                    // 设置图片链接
                    img.src =imgsrc+data[i].picName;
                    // 向DIV区添加图片对象
                    img.style.marginTop="20px";
                    document.getElementById("picture").appendChild(img);
                    $('#myModal').modal('show');
                }
            }
        },
        'error': function(err) {
        }
    });
}




