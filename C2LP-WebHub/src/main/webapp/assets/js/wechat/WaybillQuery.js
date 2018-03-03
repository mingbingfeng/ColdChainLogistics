/**
 * Created by wanghe on 2016/8/22.
 */
var WaybillQuery = function() {
    this.init();
}

WaybillQuery.prototype.init = function () {
    $('.btn-waybillquery').click(this.doquery.bind(this));
}

WaybillQuery.prototype.doquery = function () {
    var number=$('#number').val();
    if(!number){
        $.toast("运单编号不能为空！", "cancel");
        return;
    }
    URLHelper.ajax({
        'url': URLHelper.waybillqueryUrl(),
        'type': 'GET',
        'dataType': 'json',
        'data': {
            'number': number
        },
        'success': function(data) {
            if (data.code != 0) {
                $.toast("没找到相关联的运单信息！", "forbidden");
                return;
            }else{
                window.location.href = URLHelper.useruser(data.url);
            }
        },
        'error': function(err) {
            window.alert('查询不到运单编号，请联系内部人员创建运单! ' + err);
            console.log(err);
        }
    });

}
