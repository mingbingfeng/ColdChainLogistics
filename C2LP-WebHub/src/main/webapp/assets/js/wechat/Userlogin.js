/**
 * Created by zhaoyou on 7/29/16.
 */
var Userlogin = function() {
    this.init();
}

Userlogin.prototype.init = function() {
    this.account = $('.account');
    this.username = $('.username');
    this.password = $('.password');
    this.redirectUrl = $('.redirectUrl');
    $('.btn-submit').click(this.doLogin.bind(this));
    $('input[type=text],[type=password]').keydown(this.returnEntry.bind(this));
}

Userlogin.prototype.returnEntry = function(e) {
    if (e.keyCode === 13 != '' && this.account.val() !='' && this.username.val() !='' && this.password.val() != '')  {
        $('.btn-submit').click();
    }
}

Userlogin.prototype.doLogin = function() {
    var self = this;
    if (!/\S/.test(this.account.val()) || !/\S/.test(this.username.val()) || !/\S/.test(this.password.val())) {
        $.toast("请确保信息完整！", "cancel");
        return;
    }
    URLHelper.ajax({
        'url': URLHelper.loginWei(),
        'type': 'POST',
        'dataType': 'json',
        'data': {
            'account': this.account.val(),
            'username': this.username.val(),
            'password': this.password.val()
        },
        'success': function(data) {
            if (data.code == 0) {
                $.showLoading("数据加载中...")
                setTimeout(function () {
                    $.hideLoading();
                }, 2000);
                if (self.redirectUrl.val() && self.redirectUrl.val() != '/') {
                    var redirectUrl=location.href;
                    window.location.href = redirectUrl.substr(redirectUrl.indexOf("=")+1);
                } else {
                    window.location.href = URLHelper.useruser(data.url);
                }
            } else {
                $.toast("登陆失败，账号信息错误！", "forbidden");
            }
        },
        'error': function(err) {
            $('.btn-submit').button('reset');
            window.alert('无法登陆系统， 请联系管理人员! ' + err);
            console.log(err);
        }
    });
}
