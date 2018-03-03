/**
 * Created by wanghe on 2016/8/12.
 */
var CarHisMap = function() {
    this.init();
}
CarHisMap.prototype.init = function() {
    this.initMap();
}

//初始化baidu地图
CarHisMap.prototype.initMap = function() {
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
}

//获取数据
CarHisMap.prototype.getData = function() {
    var storageId=$("#storageId").val();
    var startTime=$("#startTime").val();
    var endTime=$("#endTime").val();
    $.ajax({
        'url':'/pc/themap/coldMap',
        'method':'get',
        'dataType':'json',
        'data':{
            'storageId':storageId,
            'startTime':startTime,
            'endTime':endTime
        },
        'success':function (data){
            if(data==null || data.length==0){
                window.alert('当前车载的数据尚未上传...');
            }else{
                carHisMap.dealData(data);
            }
        },
        'error':function(msg){
            window.alert("加载车载历史数据失败..."+msg);
        }
    });
}

CarHisMap.prototype.dealData = function(data) {
    carHisMap.index = 0;
    carHisMap.num = 0;
    carHisMap.allsize = data.length;
    carHisMap.allresult = data;

    carHisMap.firstPoint = null;
    carHisMap.lastPoint = null;

    /**
     * 如果地址栏有参数ad则,使用async.js 方式获取地址显示曲线
     * 避免第一种方式带来的返回时间不一定顺序的问题.
     */
    var ad = carHisMap.getParameterByName('ad');

    if (!ad) {
        //carHisMap.mytime = window.setInterval(carHisMap.getDataFromArr,50);
        carHisMap.getNextAddress();
    } else {
        console.log('go to getbaiduAddress');
        carHisMap.getBaiduAddress();
    }
};


CarHisMap.prototype.getNextAddress = function() {
    var obj = carHisMap.allresult[carHisMap.index];

    var lng = Util.formatSBYCoords(obj[1]);
    var lat = Util.formatSBYCoords(obj[2]);


    if (lng != -300 && lat != -300 && lng > 0 && lat > 0) {
        Util.transferGPS2BD(lng, lat, function (xyresult) {
            if (xyresult.status === 0) {

                var point = new BMap.Point(xyresult.result[0].x, xyresult.result[0].y);

                if (!carHisMap.firstPoint) {        // 第一次获取到有效的点为起始点
                    carHisMap.firstPoint = point;
                    carHisMap.drawFirstLabel();
                }

                carHisMap.lastPoint = point;

                carHisMap.drawPath(point);


                carHisMap.index++;


                if (carHisMap.index === carHisMap.allsize) {      // 遍历的点等于总共的长度是停止,结束.
                    if (carHisMap.lastPoint) carHisMap.drawLastLabel();
                    return; // 绘制完毕
                }

                window.setTimeout(carHisMap.getNextAddress, 20);

            }
        });
    } else {

        console.log('不绘制经纬度');

        carHisMap.index ++;

        if (carHisMap.index === carHisMap.allsize) {      // 遍历的点等于总共的长度是停止,结束.
            if (carHisMap.lastPoint) carHisMap.drawLastLabel();
            return; // 如果
        }

        window.setTimeout(carHisMap.getNextAddress, 20);
    }
};

CarHisMap.prototype.drawFirstLabel = function() {
    console.log('drawFirstLabel', carHisMap.firstPoint);
    var startLabel = new BMap.Label("起始点", carHisMap.opts);
    startLabel.setPosition(carHisMap.firstPoint);
    startLabel.setStyle(carHisMap.styles);
    carHisMap.map.addOverlay(startLabel);
    carHisMap.map.panTo(carHisMap.firstPoint);

};

CarHisMap.prototype.drawLastLabel = function() {
    console.log('drawLastLabel', carHisMap.lastPoint);
    var endLabel = new BMap.Label("终止点", carHisMap.opts);
    endLabel.setPosition(carHisMap.lastPoint);
    endLabel.setStyle(carHisMap.styles);
    carHisMap.map.addOverlay(endLabel);
};

CarHisMap.prototype.drawPath = function(point) {
    if (carHisMap.polyline) {                       // 如果存在曲线了,则添加当前点到曲线.
        var points = carHisMap.polyline.getPath();
        points.push(point);
        carHisMap.polyline.setPath(points);
    } else {                                        //  当曲线不存在是,新建一体条曲线.
        var polyline = new BMap.Polyline([point], {strokeColor:"blue", strokeWeight:6, strokeOpacity:0.5});
        carHisMap.polyline = polyline;
        carHisMap.map.addOverlay(polyline);
    }
};




//获取单个数据
CarHisMap.prototype.getDataFromArr = function() {

    while (carHisMap.index < carHisMap.allsize) {

        var obj = carHisMap.allresult[carHisMap.index];		//获取当前一条的记录

        //获取经纬度信息
        var lng = Util.formatSBYCoords(obj[1]);
        var lat = Util.formatSBYCoords(obj[2]);


        if (lng != -300 && lat != -300 && lng > 0 && lat > 0) {
            Util.transferGPS2BD(lng, lat, function (xyresult) {
                if (xyresult.status === 0) {

                    var point = new BMap.Point(xyresult.result[0].x, xyresult.result[0].y);
                    //self.map.centerAndZoom(point, 15);  // 编写自定义函数，创建标注
                    carHisMap.updateMap(point, obj);
                }
            });
            break;
        }
        carHisMap.index++;
    }

    if (carHisMap.index == carHisMap.allsize) {
        window.clearInterval(carHisMap.mytime);
    }
}
//更新地图信息
CarHisMap.prototype.updateMap = function(point, obj) {
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
    var map = carHisMap.map;
    if(carHisMap.index == 0 || carHisMap.num == 0) {
        var polyline = new BMap.Polyline([point], {strokeColor:"blue", strokeWeight:6, strokeOpacity:0.5});
        carHisMap.polyline = polyline;
        map.addOverlay(polyline);
        var startLabel = new BMap.Label("起始点", opts);
        startLabel.setPosition(point);
        startLabel.setStyle(styles);
        map.addOverlay(startLabel);
        map.panTo(point);
    } else {
        var polyline = carHisMap.polyline;
        var points = polyline.getPath();
        points.push(point);
        polyline.setPath(points);
    }
    if(carHisMap.index == carHisMap.allsize) {
        var endLabel = new BMap.Label("终止点", opts);
        endLabel.setPosition(point);
        endLabel.setStyle(styles);
        map.addOverlay(endLabel);
    }
        carHisMap.index++ ;
        carHisMap.num++;
};

CarHisMap.prototype.getBaiduAddress = function() {

    var execFunctionList = [];

    for (var i = 0; i < carHisMap.allsize; i++) {

        var obj = carHisMap.allresult[i];		//获取当前一条的记录

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
        carHisMap.displayPath(results);
    });
};


CarHisMap.prototype.displayPath = function(pointList) {

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
    carHisMap.map.addOverlay(startLabel);
    carHisMap.map.panTo(firstPoint);


    // polyline
    var polyline = new BMap.Polyline(pointList, {strokeColor:"blue", strokeWeight:6, strokeOpacity:0.5});
    carHisMap.map.addOverlay(polyline);


    // last lable
    var endLabel = new BMap.Label("终止点", opts);
    endLabel.setPosition(lastPoint);
    endLabel.setStyle(styles);
    carHisMap.map.addOverlay(endLabel);

    console.log('绘制结束');
};


CarHisMap.prototype.getParameterByName = function(name, url) {
        if (!url) url = window.location.href;
        name = name.replace(/[\[\]]/g, "\\$&");
        var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
            results = regex.exec(url);
        if (!results) return null;
        if (!results[2]) return '';
        return decodeURIComponent(results[2].replace(/\+/g, " "));
};



