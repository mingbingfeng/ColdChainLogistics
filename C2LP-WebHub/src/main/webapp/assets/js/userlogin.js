/**
 * Created by zhaoyou on 7/27/16.
 */
/**
 * Created by hua on 2015/5/21.
 */

var UserLogin = function() {
    this.init();
}

UserLogin.prototype.init = function() {
    this.account = $('.account');
    this.username = $('.username');
    this.password = $('.password');
    this.redirectUrl = $('.redirectUrl');
    $('.btn-submit').click(this.doLogin.bind(this));
    $('input[type=text],[type=password]').keydown(this.returnEntry.bind(this));
}

UserLogin.prototype.returnEntry = function(e) {
    if (e.keyCode === 13 != '' && this.account.val() !='' && this.username.val() !='' && this.password.val() != '')  {
        $('.btn-submit').click();
    }
}

UserLogin.prototype.doLogin = function() {
    var self = this;
    if (!/\S/.test(this.account.val()) || !/\S/.test(this.username.val()) || !/\S/.test(this.password.val())) {
        notie.alert(2,"警告：请确保信息完整！",2);
        return;
    }
    $('.btn-submit').button('loading');

    URLHelper.ajax({
        'url': URLHelper.loginURL(),
        'method': 'POST',
            'dataType': 'json',
            'data': {
                'account': this.account.val(),
                'username': this.username.val(),
                'password': this.password.val()
        },
        'success': function(data) {
            $('.btn-submit').button('reset');
            if (data.code == 0) {
                $('.btn-submit').data().loadingText = '跳转中...';
                $('.btn-submit').button('loading');
                if (self.redirectUrl.val() && self.redirectUrl.val() != '/') {
                    $('.btn-submit').button('reset');
                    window.location.href = self.redirectUrl.val();
                } else {
                    window.location.href = URLHelper.user(data.url);
                }
            } else {
                notie.alert(3,"登陆失败，账号信息错误！",2);
            }
        },
        'error': function(err) {
            $('.btn-submit').button('reset');
            window.alert('无法登陆系统， 请联系管理人员! ' + err);
            console.log(err);
        }
    });
}