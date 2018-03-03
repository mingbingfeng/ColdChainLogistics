/**
 * Created by wanghe on 2016/7/28.
 */

var URLHelper = {};
URLHelper.prefixUrl = location.protocol + "//" + location.host + '/';

//网站方向
URLHelper.loginURL = function() {
    return this.prefixUrl + 'pc/account/doLogin';
}

URLHelper.querypwdURL = function() {
    return this.prefixUrl + 'pc/waybill/querypwd';
}

URLHelper.user = function(url) {
    return this.prefixUrl + url;
}

URLHelper.queryUrl = function () {
    return this.prefixUrl + "pc/waybill/numberquery";
}

URLHelper.clickPicture = function(id) {
   return this.prefixUrl + 'pc/search/signpictures?id='+id;
}

URLHelper.StartEndUrl = function (beginAt,signinAt) {
    return this.prefixUrl + "pc/waybill/search/startendtime?beginAt="+beginAt+"&signinAt="+signinAt;
}

URLHelper.waybillCountUrl = function (beginAt,signinAt) {
    return this.prefixUrl + "pc/waybill/count/startendtime?beginAt="+beginAt+"&signinAt="+signinAt;
}

URLHelper.waybillCountCarUrl = function (beginAt,signinAt) {
    return this.prefixUrl + "pc/waybill/countCar/startendtime?beginAt="+beginAt+"&signinAt="+signinAt;
}

URLHelper.waybillCountPDFUrl = function (beginAt,signinAt) {
    return this.prefixUrl + "pc/waybill/countPDF/export?beginAt="+beginAt+"&signinAt="+signinAt;
}
URLHelper.waybillCountCarPDFUrl = function (beginAt,signinAt) {
    return this.prefixUrl + "pc/waybill/countCarPDF/export?beginAt="+beginAt+"&signinAt="+signinAt;
}

URLHelper.VisitUrl = function (beginAt,signinAt,customerId) {
    return this.prefixUrl + "pc/waybill/visit?beginAt="+beginAt+"&signinAt="+signinAt+"&customerId="+customerId;
}

URLHelper.VisitPDFUrl = function (beginAt,signinAt,customerId) {
    return this.prefixUrl + "pc/waybill/visit/pdf?beginAt="+beginAt+"&signinAt="+signinAt+"&customerId="+customerId;
}

URLHelper.downpdf = function (number,nodeid,coldid,storageName) {
    return this.prefixUrl + "pc/waybill/coldchain/export?number="+number+"&nodeid="+nodeid+"&coldid="+coldid+"&storageName="+storageName;
}

URLHelper.downallpdf = function (number) {
    return this.prefixUrl + "pc/waybill/coldchainAll/export?number="+number;
}

URLHelper.historicaldatapdf = function (number,senderOrg,receiverOrg) {
    return this.prefixUrl + "pc/historical/historicaldatapdf?number="+number+"&senderOrg="+senderOrg+"&receiverOrg="+receiverOrg;
}

URLHelper.coldChainPagingUrl = function () {
    return this.prefixUrl + "pc/waybill/coldchainpaging";
}

URLHelper.zhexiandatapictureUrl = function () {
    return this.prefixUrl + "pc/waybill/pccoldzhexiantu";
}


URLHelper.ajax = function(url, method, data, callback, errorFun) {
    var self = this;
    if (typeof url === 'string') {
        $.ajax({
            'url' : url,
            'method' : method,
            'dataType' : 'json',
            'data': data,
            'success': function(data) {
                $('.errStyle').remove();
                callback(data);
            },
            'complete': function(req) {
                if(req.status == 401) {
                    window.location.href = self.loginURL();
                }
            },
            'error': errFun
        });
    } else if  (typeof  url === 'object') {
        if (!url['complete']) {
            url['complete'] = function(req) {if(req.status == 401) {  window.location.href = self.toLoginUrl() ;}}
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
    if(err.responseText != '') {
        str = '服务器发生了错误';
        console.log(err);
    } else {
        str = "网络连接异常";
    }
    notie.alert(3, str, 2);
}
