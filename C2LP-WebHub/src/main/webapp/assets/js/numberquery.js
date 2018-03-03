/*
  Created by IntelliJ IDEA.
  User: wanghe
  Date: 2016/8/1
  Time: 17:35
*/
var NumberQuery = function() {
    this.init();
}

NumberQuery.prototype.init = function () {
    $('#query').click(this.doquery.bind(this));
    $('input[type=text]').keydown(this.returnEntry.bind(this));
}

NumberQuery.prototype.returnEntry = function(e) {
    this.number=$('#number');
    if (e.keyCode === 13 != '' && this.number.val() != '')  {
        $('#query').click();
    }
}

NumberQuery.prototype.doquery = function () {
    var number= $('#number').val();
    if(!number){
        notie.alert(1,"运单编号不能为空！",2);
        return;
    }
    URLHelper.ajax({
        'url': URLHelper.queryUrl(),
        'type': 'GET',
        'dataType': 'json',
        'data': {
            'number': number
        },
        'success': function(data) {
            if(data.code!=0){
                notie.alert(3,"抱歉，没找到相关联的运单信息！",2);
                return;
            }else{
                window.location.href = URLHelper.user(data.url);
            }
        },
        'error': function(err) {
            window.alert('查询不到运单编号，请联系内部人员创建运单! ' + err);
            console.log(err);
        }
    });
}
