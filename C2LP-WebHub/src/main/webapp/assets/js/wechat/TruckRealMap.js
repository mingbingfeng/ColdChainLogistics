/**
 * Created by wanghe on 2016/8/12.
 */
var TruckRealMap = function() {
    this.init();
}
TruckRealMap.prototype.init = function() {
    this.initMap();
}
//初始化地图
TruckRealMap.prototype.initMap = function() {
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
    this.styles = {
        color : "red",
        fontSize : "12px",
        height : "20px",
        lineHeight : "20px",
        fontFamily:"微软雅黑"
    };
    this.getData();
    var intervalId = setInterval(function () {
        truckRealMap.getData();
    },15000);
    this.intervalId = intervalId;
}

//获取数据
TruckRealMap.prototype.getData = function() {
    var number=$("#number").val();
    var startTime = $('#bottom').data("starttimee");
    var nodeId = $('#bottom').data("nodeid");
    $.ajax({
        'url':'/wechat/cold/coldchainRealmapdata',
        'method':'get',
        'dataType':'json',
        'data':{'number':number,'startTime':startTime, 'nodeId':nodeId},
        'success':function (data){
            $('.alert-none').hide();
            if(data.storageName=="arrived"){
                $('#map').hide();
                $('#arrived').show();
                window.clearInterval(truckRealMap.intervalId);
                return;
            } else if(data.aiinfolist.length>0){
                $('#bottom').data("starttimee",data.aiinfolist[data.aiinfolist.length-1][0].toString());
                $('#bottom').data("nodeid",data.nodeId);
                    truckRealMap.dealData(data);
            }else if(startTime=="first"){
                //第一次进页面未查出数据
                $('.alert-none').show();
            }
        },
        'error':function(msg){
            window.alert("加载车载实时数据失败..."+msg);
        }
    });
}

TruckRealMap.prototype.dealData = function(data) {
    truckRealMap.index = 0;
    truckRealMap.num = 0;
    truckRealMap.allsize = data.aiinfolist.length;
    truckRealMap.allresult = data.aiinfolist;
    truckRealMap.storageName = data.storageName;

    truckRealMap.firstPoint = null;
    truckRealMap.lastPoint = null;

    truckRealMap.getNextAddress(data);
};

TruckRealMap.prototype.getNextAddress = function(data) {
    var obj = data.aiinfolist[truckRealMap.index];
    var lat = Util.formatSBYCoords(obj[obj.length-2]);
    var lng = Util.formatSBYCoords(obj[obj.length-3]); //经度
    var s = '  ';

    obj[1] = obj[1] == -300? "--" : obj[1]+(data.aiList[0].pointType==2?"%":"℃");
    s +=  data.aiList[0].pointName+":"+obj[1];

    var len = obj.length-3;
    if(data.storageType==1){
        lng = Util.formatSBYCoords(data.lng);
        lat = Util.formatSBYCoords(data.lat);
        len = obj.length-1;
    }

    for(var i=2;i<len;i++){
        obj[i] = obj[i] == -300? "--" : obj[i]+(data.aiList[i-1].pointType==2?"%":"℃");
        s +=  " ,"+data.aiList[i-1].pointName+":"+obj[i];
    }

    if (lng != -300 && lat != -300 && lng > 0 && lat > 0) {
        Util.transferGPS2BD(lng, lat, function (xyresult) {

            if (xyresult.status === 0) {

                var point = new BMap.Point(xyresult.result[0].x, xyresult.result[0].y);

                if (!truckRealMap.firstPoint) {        // 第一次获取到有效的点为起始点
                    truckRealMap.firstPoint = point;
                    //truckRealMap.drawFirstLabel();
                }

                truckRealMap.lastPoint = point;

                truckRealMap.drawPath(point);

                truckRealMap.index++;

                if (truckRealMap.index === truckRealMap.allsize) {      // 遍历的点等于总共的长度是停止,结束.
                    if (truckRealMap.lastPoint) truckRealMap.drawLastLabel(s);
                    return; // 绘制完毕
                }
                window.setTimeout(truckRealMap.getNextAddress(data), 20);
            }
        });
    } else {

        console.log('不绘制经纬度');

        truckRealMap.index ++;

        if (truckRealMap.index === truckRealMap.allsize) {      // 遍历的点等于总共的长度是停止,结束.
            if (truckRealMap.lastPoint) truckRealMap.drawLastLabel(s);
            return; // 如果
        }
        window.setTimeout(truckRealMap.getNextAddress(data), 20);
    }
};

TruckRealMap.prototype.drawFirstLabel = function() {
    console.log('drawFirstLabel', truckRealMap.firstPoint);
    var startLabel = new BMap.Label("起始点", truckRealMap.opts);
    startLabel.setPosition(truckRealMap.firstPoint);
    startLabel.setStyle(truckRealMap.styles);
    truckRealMap.map.addOverlay(startLabel);
    truckRealMap.map.panTo(truckRealMap.firstPoint);

};

TruckRealMap.prototype.drawLastLabel = function(s) {
    console.log('drawLastLabel', truckRealMap.lastPoint);
    truckRealMap.map.centerAndZoom(truckRealMap.lastPoint, 13);

    if(truckRealMap.marker){
        truckRealMap.map.removeOverlay(truckRealMap.marker);
    }
    var marker = new BMap.Marker(truckRealMap.lastPoint);
    truckRealMap.map.addOverlay(marker);
    marker.setLabel(new BMap.Label(truckRealMap.storageName+s,{offset: new BMap.Size(20, -10)}));
    truckRealMap.marker = marker;
};

TruckRealMap.prototype.drawPath = function(point) {
    if (truckRealMap.polyline) {                       // 如果存在曲线了,则添加当前点到曲线.
        var points = truckRealMap.polyline.getPath();
        points.push(point);
        truckRealMap.polyline.setPath(points);
    } else {                                        //  当曲线不存在是,新建一体条曲线.
        var polyline = new BMap.Polyline([point], {strokeColor:"blue", strokeWeight:6, strokeOpacity:0.5});
        truckRealMap.polyline = polyline;
        truckRealMap.map.addOverlay(polyline);
    }
};




//获取单个数据
TruckRealMap.prototype.getDataFromArr = function() {

    while (truckRealMap.index < truckRealMap.allsize) {

        var obj = truckRealMap.allresult[truckRealMap.index];		//获取当前一条的记录

        //获取经纬度信息
        var lng = Util.formatSBYCoords(obj[1]);
        var lat = Util.formatSBYCoords(obj[2]);


        if (lng != -300 && lat != -300 && lng > 0 && lat > 0) {
            Util.transferGPS2BD(lng, lat, function (xyresult) {
                if (xyresult.status === 0) {

                    var point = new BMap.Point(xyresult.result[0].x, xyresult.result[0].y);
                    //self.map.centerAndZoom(point, 15);  // 编写自定义函数，创建标注
                    truckRealMap.updateMap(point, obj);
                }
            });
            break;
        }
        truckRealMap.index++;
    }

    if (truckRealMap.index == truckRealMap.allsize) {
        window.clearInterval(truckRealMap.mytime);
    }
}
//更新地图信息
TruckRealMap.prototype.updateMap = function(point, obj) {
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
    var map = truckRealMap.map;
    if(truckRealMap.index == 0 || truckRealMap.num == 0) {
        var polyline = new BMap.Polyline([point], {strokeColor:"blue", strokeWeight:6, strokeOpacity:0.5});
        truckRealMap.polyline = polyline;
        map.addOverlay(polyline);
        var startLabel = new BMap.Label("起始点", opts);
        startLabel.setPosition(point);
        startLabel.setStyle(styles);
        map.addOverlay(startLabel);
        map.panTo(point);
    } else {
        var polyline = truckRealMap.polyline;
        var points = polyline.getPath();
        points.push(point);
        polyline.setPath(points);
    }
    if(truckRealMap.index == truckRealMap.allsize) {
        var endLabel = new BMap.Label("终止点", opts);
        endLabel.setPosition(point);
        endLabel.setStyle(styles);
        map.addOverlay(endLabel);
    }
    truckRealMap.index++ ;
    truckRealMap.num++;
};

TruckRealMap.prototype.getBaiduAddress = function() {

    var execFunctionList = [];

    for (var i = 0; i < truckRealMap.allsize; i++) {

        var obj = truckRealMap.allresult[i];		//获取当前一条的记录

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
        truckRealMap.displayPath(results);
    });
};


TruckRealMap.prototype.displayPath = function(pointList) {

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
    truckRealMap.map.addOverlay(startLabel);
    truckRealMap.map.panTo(firstPoint);


    // polyline
    var polyline = new BMap.Polyline(pointList, {strokeColor:"blue", strokeWeight:6, strokeOpacity:0.5});
    truckRealMap.map.addOverlay(polyline);


    // last lable
    var endLabel = new BMap.Label("终止点", opts);
    endLabel.setPosition(lastPoint);
    endLabel.setStyle(styles);
    truckRealMap.map.addOverlay(endLabel);

    console.log('绘制结束');
};


TruckRealMap.prototype.getParameterByName = function(name, url) {
    if (!url) url = window.location.href;
    name = name.replace(/[\[\]]/g, "\\$&");
    var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
        results = regex.exec(url);
    if (!results) return null;
    if (!results[2]) return '';
    return decodeURIComponent(results[2].replace(/\+/g, " "));
};






