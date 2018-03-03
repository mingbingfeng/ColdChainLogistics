/**
 * Created by wanghe on 2016/9/1.
 */
var UpdatePwd = function() {
    this.init();
}

UpdatePwd.prototype.init = function() {
    $('.btn-ture').click(this.doUpdatePwd.bind(this));
}

UpdatePwd.prototype.doUpdatePwd = function() {
    var mypwd=$('.mypwd').val();
    var newpwd=$('.newpwd').val();
    var pwdture=$('.pwdture').val();
    if(!mypwd||!newpwd||!pwdture){
        notie.alert(2,"请确保信息完整",2);
        return;
    }
    $('.btn-ture').button('loading');
    URLHelper.ajax({
        'url': URLHelper.querypwdURL(),
        'method': 'POST',
        'dataType': 'json',
        'data': {
            'mypwd':mypwd
        },
        'success': function(data) {
            $('.btn-ture').button('reset');
            if(data.code==2){
                notie.alert(3,"原密码输入有误！",2);
                return;
            }else if(newpwd!=pwdture){
                notie.alert(3,"两次输入的新密码不相同",2);
                return;
            }else{
                window.location.href = URLHelper.user(data.url+"?newpwd="+newpwd);
            }
        },
        'error': function(err) {
            window.alert('无法修改密码， 请联系管理人员! ' + err);
        }
    });
}
