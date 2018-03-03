/**
 * Created by wanghe on 2016/8/12.
 */
var TruckHisMap = function() {
    this.init();
}
TruckHisMap.prototype.init = function() {
    $("#downpdf").bind("click", this.getPdf);
    this.initMap();
}

TruckHisMap.prototype.getPdf = function() {
    window.open(  URLHelper.downallpdf(  $('#number').val()  )   );
}

//初始化地图
TruckHisMap.prototype.initMap = function() {
    var map = new BMap.Map("map");
    map.centerAndZoom(new BMap.Point(121.486267,  31.164612),12);
    map.enableScrollWheelZoom(true);
    map.addControl(new BMap.MapTypeControl());
    map.addControl(new BMap.NavigationControl());
    map.addControl(new BMap.ScaleControl());
    map.addControl(new BMap.OverviewMapControl());
    this.map = map;
    this.opts = {
        offset   : new BMap.Size(5, -5)    //设置文本偏移量
    };
    this.optso = {
        offset   : new BMap.Size(5, -25)    //设置文本偏移量
    };
    this.styles = {
        color : "red",
        fontSize : "12px",
        height : "20px",
        lineHeight : "20px",
        fontFamily:"微软雅黑"
    };
    this.getData();
}

//获取数据
TruckHisMap.prototype.getData = function() {
    var number=$("#number").val();
    $.ajax({
        'url':'/pc/themap/coldMapall',
        'method':'get',
        'dataType':'json',
        'data':{'number':number},
        'success':function (data){
            if(data==null || data.length==0){
                window.alert('当前运单的位置数据尚未上传...');
                $('.alert-danger').show();
            }else{
                for(j=0;j<data.length;j++){
                    if(data[j].length!=0){
                        truckHisMap.dealData(data[j],j);
                    }
                }
            }
        },
        'error':function(msg){
            window.alert("加载车载历史数据失败..."+msg);
        }
    });
}

TruckHisMap.prototype.dealData = function(data,i) {
    truckHisMap['index'+i]= 0;
    truckHisMap['num'+i] = 0;
    truckHisMap['allsize'+i] = data.length;
    truckHisMap['allresult'+i] = data;

    truckHisMap['firstPoint'+i] = null;
    truckHisMap['lastPoint'+i] = null;
    /**
     * 如果地址栏有参数ad则,使用async.js 方式获取地址显示曲线
     * 避免第一种方式带来的返回时间不一定顺序的问题.
     */
    var ad = truckHisMap.getParameterByName('ad');
    if (!ad) {
        //truckHisMap.mytime = window.setInterval(truckHisMap.getDataFromArr,50);
        truckHisMap.getNextAddress(i);
    } else {
        console.log('go to getbaiduAddress');
        truckHisMap.getBaiduAddress();
    }
};

TruckHisMap.prototype.getNextAddress = function(i) {
    var index = truckHisMap['index'+i];
    var obj = truckHisMap['allresult'+i][index];
    var lng = Util.formatSBYCoords(obj[1]);
    var lat = Util.formatSBYCoords(obj[2]);
    if (lng != -300 && lat != -300 && lng > 0 && lat > 0) {
        Util.transferGPS2BD(lng, lat, function (xyresult) {
            if (xyresult.status === 0) {

                var point = new BMap.Point(xyresult.result[0].x, xyresult.result[0].y);

                if (!truckHisMap['firstPoint' + i]) {
                    // 第一次获取到有效的点为起始点
                    truckHisMap['firstPoint' + i] = point;
                    truckHisMap.drawFirstLabel(i);

                }
                truckHisMap['lastPoint' + i] = point;

                truckHisMap.drawPath(point, i);

                truckHisMap['index' + i]++;

                if (truckHisMap['index' + i] >= truckHisMap['allsize' + i]-1) {     // 遍历的点等于总共的长度是停止,结束.
                    if (truckHisMap['lastPoint' + i]) truckHisMap.drawLastLabel(i);
                    return; // 绘制完毕
                }
                window.setTimeout(truckHisMap.getNextAddress(i), 20);
            }
        });
    } else {
        console.log('不绘制经纬度');
        truckHisMap['index' + i]++;

        if (truckHisMap['index' + i] >= truckHisMap['allsize' + i]-1) {      // 遍历的点等于总共的长度是停止,结束.
            if (truckHisMap['lastPoint' + i]) truckHisMap.drawLastLabel(i);
            return; // 如果
        }
        window.setTimeout(truckHisMap.getNextAddress(i), 20);
    }
}

TruckHisMap.prototype.drawFirstLabel = function(i) {
    console.log('drawFirstLabel', truckHisMap['firstPoint'+i]);
    var startLabel = new BMap.Label(truckHisMap['allresult'+i][truckHisMap['allsize'+i]-1][0]+"起始点", truckHisMap.opts);
    startLabel.setPosition(truckHisMap['firstPoint'+i]);
    startLabel.setStyle(truckHisMap.styles);
    truckHisMap.map.addOverlay(startLabel);
    truckHisMap.map.panTo(truckHisMap['firstPoint'+i]);

};

TruckHisMap.prototype.drawLastLabel = function(i) {
    console.log('drawLastLabel', truckHisMap['lastPoint'+i]);
    var endLabel = new BMap.Label(truckHisMap['allresult'+i][truckHisMap['allsize'+i]-1][0]+"终止点", truckHisMap.optso);
    endLabel.setPosition(truckHisMap['lastPoint'+i]);
    endLabel.setStyle(truckHisMap.styles);
    truckHisMap.map.addOverlay(endLabel);
};

TruckHisMap.prototype.drawPath = function(point,i) {
    if (truckHisMap['polyline'+i]) {// 如果存在曲线了,则添加当前点到曲线.
        var points = truckHisMap['polyline'+i].getPath();
        points.push(point);
        truckHisMap['polyline'+i].setPath(points);
    } else {                                        //  当曲线不存在是,新建一体条曲线.
        var polyline = new BMap.Polyline([point], {strokeColor:"blue", strokeWeight:6, strokeOpacity:0.5});
        truckHisMap['polyline'+i] = polyline;
        truckHisMap.map.addOverlay(polyline);
    }
};

//获取单个数据
TruckHisMap.prototype.getDataFromArr = function() {

    while (truckHisMap.index < truckHisMap.allsize) {

        var obj = truckHisMap.allresult[truckHisMap.index];		//获取当前一条的记录

        //获取经纬度信息
        var lng = Util.formatSBYCoords(obj[1]);
        var lat = Util.formatSBYCoords(obj[2]);


        if (lng != -300 && lat != -300 && lng > 0 && lat > 0) {
            Util.transferGPS2BD(lng, lat, function (xyresult) {
                if (xyresult.status === 0) {

                    var point = new BMap.Point(xyresult.result[0].x, xyresult.result[0].y);
                    //self.map.centerAndZoom(point, 15);  // 编写自定义函数，创建标注
                    truckHisMap.updateMap(point, obj);
                }
            });
            break;
        }
        truckHisMap.index++;
    }

    if (truckHisMap.index == truckHisMap.allsize) {
        window.clearInterval(truckHisMap.mytime);
    }
}
//更新地图信息
TruckHisMap.prototype.updateMap = function(point, obj) {
    var opts = {
        offset   : new BMap.Size(5, -5)    //设置文本偏移量
    };
    var styles = {
        color : "red",
        fontSize : "12px",
        height : "20px",
        lineHeight : "20px",
        fontFamily:"微软雅黑"
    };
    var map = truckHisMap.map;
    if(truckHisMap.index == 0 || truckHisMap.num == 0) {
        var polyline = new BMap.Polyline([point], {strokeColor:"blue", strokeWeight:6, strokeOpacity:0.5});
        truckHisMap.polyline = polyline;
        map.addOverlay(polyline);
        var startLabel = new BMap.Label("起始点", opts);
        startLabel.setPosition(point);
        startLabel.setStyle(styles);
        map.addOverlay(startLabel);
        map.panTo(point);
    } else {
        var polyline = truckHisMap.polyline;
        var points = polyline.getPath();
        points.push(point);
        polyline.setPath(points);
    }
    if(truckHisMap.index == truckHisMap.allsize) {
        var endLabel = new BMap.Label("终止点", opts);
        endLabel.setPosition(point);
        endLabel.setStyle(styles);
        map.addOverlay(endLabel);
    }
        truckHisMap.index++ ;
        truckHisMap.num++;
};

TruckHisMap.prototype.getBaiduAddress = function() {

    var execFunctionList = [];

    for (var i = 0; i < truckHisMap.allsize; i++) {

        var obj = truckHisMap.allresult[i];		//获取当前一条的记录

        //获取经纬度信息
        var lng = Util.formatSBYCoords(obj[1]);
        var lat = Util.formatSBYCoords(obj[2]);


        if (lng != -300 && lat != -300 && lng > 0 && lat > 0) {

            execFunctionList.push((function(lng, lat){
                return function(callback) {
                    Util.transferGPS2BD(lng, lat, function (xyresult) {
                        if (xyresult.status === 0) {

                            var point = new BMap.Point(xyresult.result[0].x, xyresult.result[0].y);
                            callback(null, point);
                        } else {
                            callback(null, null);
                        }
                    });
                };
            })(lng, lat));
        }
    }

    console.log('aysnc parallel before...');

    // 并行执行获取百度ip地址的的任务
    async.parallel(execFunctionList, function(error, results) {
        if (error) { console.log('获取百度地图失败', error); return;}
        console.log('百度地图ip地址', results);
        truckHisMap.displayPath(results);
    });
};

TruckHisMap.prototype.displayPath = function(pointList) {

    console.log('displayPath .....');
    var firstPoint = pointList[0];
    var lastPoint = pointList[pointList.size - 1];

    // options
    var opts = {
        offset   : new BMap.Size(5, -5)    //设置文本偏移量
    };

    var styles = {
        color : "red",
        fontSize : "12px",
        height : "20px",
        lineHeight : "20px",
        fontFamily:"微软雅黑"
    };

    // first lable
    var startLabel = new BMap.Label("起始点", opts);
    startLabel.setPosition(firstPoint);
    startLabel.setStyle(styles);
    truckHisMap.map.addOverlay(startLabel);
    truckHisMap.map.panTo(firstPoint);


    // polyline
    var polyline = new BMap.Polyline(pointList, {strokeColor:"blue", strokeWeight:6, strokeOpacity:0.5});
    truckHisMap.map.addOverlay(polyline);


    // last lable
    var endLabel = new BMap.Label("终止点", opts);
    endLabel.setPosition(lastPoint);
    endLabel.setStyle(styles);
    truckHisMap.map.addOverlay(endLabel);

    console.log('绘制结束');
};

TruckHisMap.prototype.getParameterByName = function(name, url) {
        if (!url) url = window.location.href;
        name = name.replace(/[\[\]]/g, "\\$&");
        var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
            results = regex.exec(url);
        if (!results) return null;
        if (!results[2]) return '';
        return decodeURIComponent(results[2].replace(/\+/g, " "));
}



