/**
 * Created by wanghe on 2016/9/7.
 */
var ExitLogin = function() {
    this.init();
}
ExitLogin.prototype.init = function () {
    $("#exitbutton").bind("click", this.exitButton);
}

ExitLogin.prototype.exitButton = function() {
    $.actions({
        actions: [
                {
                    title: "选择操作",
                    text: "确定解除微信绑定",
                    className: "color-danger",
                    onClick: function() {
                        window.location.href = URLHelper.logoutUrl();
                    }
                }
            ]
    });
}