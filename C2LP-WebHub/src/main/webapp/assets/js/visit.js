/**
 * Created by wanghe on 2016/8/8.
 */
var Visit = function() {
    this.init();
}
Visit.prototype.init = function () {
    $("select").select2({allowClear: true});
    $('#beginAt,#signinAt').datetimepicker({
        viewMode:'years',
        format:'YYYY-MM-DD HH:mm:ss',
        locale: 'zh-cn'
    });
    $("#querybutton").bind("click", this.queryButton);
    $("#pdf").bind("click", this.getpdf);
}

Visit.prototype.queryButton = function() {
    var beginAt=$("#beginAt").val();
    var signinAt=$("#signinAt").val();
    var customerId = $("#customer").val();
    if(!beginAt || !signinAt) {
        notie.alert(2,"开始或结束时间不能为空！",2);
        return;
    }else if(beginAt>signinAt){
        notie.alert(3,"结束日期不能小于开始日期！",2);
        return;
    }else{
        NProgress.start();
        window.location.href = URLHelper.VisitUrl(beginAt,signinAt,customerId);
    }
}

Visit.prototype.getpdf = function() {
    var beginAt=$("#beginAt").val();
    var signinAt=$("#signinAt").val();
    var customerId = $("#customer").val();
    if(!beginAt || !signinAt) {
        notie.alert(2,"开始或结束时间不能为空！",2);
        return;
    }else if(beginAt>signinAt){
        notie.alert(3,"结束日期不能小于开始日期！",2);
        return;
    }else{
        window.open(URLHelper.VisitPDFUrl(beginAt,signinAt,customerId));
    }
}





