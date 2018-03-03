/**
 * Created by zhaoyou on 9/5/16.
 */
/**
 * Created by zhaoyou on 7/29/16.
 */
var UserBinding = function() {
    this.init();
}

UserBinding.prototype.init = function() {
    this.account = $('.account');
    this.username = $('.username');
    this.password = $('.password');
    this.openid = $('.openid');
    $('.btn-submit').click(this.doLogin.bind(this));
}


UserBinding.prototype.doLogin = function() {
    var self = this;
    if (!/\S/.test(this.account.val())
        || !/\S/.test(this.username.val())
        || !/\S/.test(this.password.val()) || this.openid.val() == '') {
        $.toast("请确保信息完整！", "cancel");
        return;
    }
    URLHelper.ajax({
        'url': URLHelper.bindingUrl(),
        'type': 'POST',
        'dataType': 'json',
        'data': {
            'account': this.account.val(),
            'username': this.username.val(),
            'password': this.password.val(),
            'openid': this.openid.val()
        },
        'success': function(data) {
            if (data.code == 0) {
                $.showLoading("数据加载中...")
                setTimeout(function () {
                    $.hideLoading();
                }, 1000);

                window.location.href = URLHelper.useruser(data.url);

            } else {
                $.toast("信息有误！", "forbidden");
            }
        },
        'error': function(err) {
            $('.btn-submit').button('reset');
            window.alert('无法登陆系统， 请联系管理人员! ' + err);
            console.log(err);
        }
    });
}
