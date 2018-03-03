var Util = window.Util || {};

/**
 * 数据库的格式 12121.212   ddmmm.mm
 * 处理经纬度信息121.21212 dd.mm.mmm  转化为 dd.dddddd
 * 如果是 -300 非法值,则直接返回原值.
 */
Util.formatSBYCoords = function(lnglat) {
    if (lnglat === -300) {return lnglat;}
    var l = lnglat  ;
    var lq = parseInt(l/100) ;
    var lqq = (l-lq*100)/60 ;
    var lqqq = lq +lqq ;
    return lqqq ;
}

/**
 * 真实的GPS坐标转换成百度的坐标.
 * @param lng GPS 经度
 * @param lat GPS 纬度
 * @param callback 转换成功后执行的回调函数,如果成功时返回的对象里面 x  代表经度  y 代表纬度.
 */
Util.transferGPS2BD = function(lng, lat, callback) {
    var callbackName = 'cbk_' + Math.round(Math.random() * 10000);
    var arr = lng + ',' + lat;
    var PositionUrl = "http://api.map.baidu.com/geoconv/v1/?coords="+arr+"&from=1&to=5&ak=gxB9Y6SdvED78vddAYI3psLH&callback=BMap." + callbackName;
    var script = document.createElement('script');
    script.src = PositionUrl;
    document.getElementsByTagName("head")[0].appendChild(script);
    BMap[callbackName] = function(xyResult) {
        delete BMap[callbackName];    //调用完需要删除改函数
        //var point = new BMap.Point(xyResult.x, xyResult.y);

        //console.log('callback', xyResult);
        callback && callback(xyResult);
    }
}

/**
 * 根据GPS坐标转换成实际的地址。
 * @param lng GPS 经度
 * @param lat GPS 纬度
 * @param callback 回调函数
 */
Util.getAddress = function(lng, lat, callback) {
    var callbackName = 'cbk_' + Math.round(Math.random() * 10000);
    var arr = lat + ',' + lng;
    var PositionUrl = "http://api.map.baidu.com/geocoder/v2/?ak=gxB9Y6SdvED78vddAYI3psLH&callback=BMap." + callbackName + "&location=" + arr + "&coordtype=wgs84ll&output=json&pois=0";
    var script = document.createElement('script');
    script.src = PositionUrl;
    document.getElementsByTagName("head")[0].appendChild(script);
    BMap[callbackName] = function(xyResult) {
        delete BMap[callbackName];    //调用完需要删除改函数
        //var point = new BMap.Point(xyResult.x, xyResult.y);

        //console.log('callback', xyResult);
        callback && callback(xyResult);
    }
}
