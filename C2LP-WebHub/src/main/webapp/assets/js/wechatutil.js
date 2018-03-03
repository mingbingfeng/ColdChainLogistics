/**
 * Created by wanghe on 2016/8/22.
 */

var URLHelper = {};
URLHelper.prefixUrl = location.protocol + "//" + location.host + '/';

var WechatHelper = {};

//微信方向
URLHelper.loginWei = function() {
    return this.prefixUrl + 'wechat/account/loginwechat';
}

URLHelper.authErrorUrl = function() {
    return this.prefixUrl + 'wechat/tips/auth_error';
}


URLHelper.bindingUrl = function() {
    return this.prefixUrl + 'wechat/account/dobinding';
}

URLHelper.waybillqueryUrl = function () {
    return this.prefixUrl + 'wechat/waybill/info';
}

URLHelper.useruser = function(url) {
    return this.prefixUrl + url;
}

URLHelper.queryTimeHistoryUrl = function (beginAt,signinAt) {
    return this.prefixUrl + "wechat/waybill/queryTimeHistory?beginAt="+beginAt+"&signinAt="+signinAt;
}

URLHelper.weclickPicture = function(id) {
    return this.prefixUrl + 'wechat/waybill/signpictures?id='+id;
}

URLHelper.logoutUrl = function() {
    return this.prefixUrl + 'wechat/unbinding';
}

URLHelper.notWechatEnvUrl = function() {
    return this.prefixUrl + 'wechat/tips/not_in_wechat';
}

URLHelper.wechatcoldChainPagingUrl = function () {
    return this.prefixUrl + "wechat/cold/wechatcoldchainpaging";
}

URLHelper.waybillpicturedataUrl = function () {
    return this.prefixUrl + "wechat/themap/coldzhexiantu";
}

URLHelper.coldchainAllzhexdataUrl = function () {
    return this.prefixUrl + "wechat/cold/coldchainAllzhexdata";
}

// 判断当前运行环境是否是微信浏览器
WechatHelper.isWeChatBrower = function() {
   return /MicroMessenger/i.test(window.navigator.userAgent);
}


$(function() {
    //if (!WechatHelper.isWeChatBrower() && !/wechat\/account\/notwechat/.test(window.location.href)) {
    //    window.location.href = URLHelper.notWechatEnvUrl();
    //}
});




URLHelper.ajax = function(url, type, data, callback, errorFun) {
    var self = this;
    if (typeof url === 'string') {
        $.ajax({
            'url' : url,
            'type' : type,
            'dataType' : 'json',
            'data': data,
            'success': function(data) {
                $('.errStyle').remove();
                callback(data);
            },
            'complete': function(req) {
                if(req.status == 401) {
                    window.location.href = self.authErrorUrl();
                }
            },
            'error': errFun
        });
    } else if  (typeof  url === 'object') {
        if (!url['complete']) {
            url['complete'] = function(req) {if(req.status == 401) {  window.location.href = self.authErrorUrl()}}
        }
        url['error'] = errFun;
        var originFuc = url['success'];
        url['success'] = function(data) {
            $('.errStyle').remove();
            originFuc(data);
        }
        $.ajax(url);
    } else {
        alert('非法参数');
    }
}


function errFun(err) {
    var str = '';
    if (err.responseText != '') {
        str = '服务器无法提供服务';
        console.log(err);
    } else {
        str = "网络连接异常";
    }
    $.toast(str, "forbidden");
}