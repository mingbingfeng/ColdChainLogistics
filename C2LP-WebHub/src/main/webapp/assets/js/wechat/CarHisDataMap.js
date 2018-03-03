/**
 * Created by wanghe on 2016/8/12.
 */
var CarHisDataMap = function() {
    this.init();
}
CarHisDataMap.prototype.init = function() {
    this.initMap();
}

//初始化地图
CarHisDataMap.prototype.initMap = function() {
    var map = new BMap.Map("map");
    map.centerAndZoom(new BMap.Point(121.486267,  31.164612),12);
    map.enableScrollWheelZoom(true);
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
CarHisDataMap.prototype.getData = function() {
    var storageId=$("#storageId").val();
    var startTime=$("#startTime").val();
    var endTime=$("#endTime").val();
    $.ajax({
        'url':'/wechat/themap/coldDataMap',
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
                carHisDataMap.dealData(data);
            }
        },
        'error':function(msg){
            window.alert("加载车载历史数据失败..."+msg);
        }
    });
}

CarHisDataMap.prototype.dealData = function(data) {
    carHisDataMap.index = 0;
    carHisDataMap.num = 0;
    carHisDataMap.allsize = data.length;
    carHisDataMap.allresult = data;

    carHisDataMap.firstPoint = null;
    carHisDataMap.lastPoint = null;

    carHisDataMap.getNextAddress();
};

CarHisDataMap.prototype.getNextAddress = function() {
    var obj = carHisDataMap.allresult[carHisDataMap.index];

    var lng = Util.formatSBYCoords(obj[1]);
    var lat = Util.formatSBYCoords(obj[2]);


    if (lng != -300 && lat != -300 && lng > 0 && lat > 0) {
        Util.transferGPS2BD(lng, lat, function (xyresult) {
            if (xyresult.status === 0) {

                var point = new BMap.Point(xyresult.result[0].x, xyresult.result[0].y);

                if (!carHisDataMap.firstPoint) {        // 第一次获取到有效的点为起始点
                    carHisDataMap.firstPoint = point;
                    carHisDataMap.drawFirstLabel();
                }

                carHisDataMap.lastPoint = point;

                carHisDataMap.drawPath(point);


                carHisDataMap.index++;


                if (carHisDataMap.index === carHisDataMap.allsize) {      // 遍历的点等于总共的长度是停止,结束.
                    if (carHisDataMap.lastPoint) carHisDataMap.drawLastLabel();
                    return; // 绘制完毕
                }

                window.setTimeout(carHisDataMap.getNextAddress, 20);

            }
        });
    } else {

        console.log('不绘制经纬度');

        carHisDataMap.index ++;

        if (carHisDataMap.index === carHisDataMap.allsize) {      // 遍历的点等于总共的长度是停止,结束.
            if (carHisDataMap.lastPoint) { carHisDataMap.drawLastLabel();}
            return; // 如果
        }

        window.setTimeout(carHisDataMap.getNextAddress, 20);
    }
};

CarHisDataMap.prototype.drawFirstLabel = function() {
    console.log('drawFirstLabel', carHisDataMap.firstPoint);
    var startLabel = new BMap.Label("起始点", carHisDataMap.opts);
    startLabel.setPosition(carHisDataMap.firstPoint);
    startLabel.setStyle(carHisDataMap.styles);
    carHisDataMap.map.addOverlay(startLabel);
    carHisDataMap.map.panTo(carHisDataMap.firstPoint);

};

CarHisDataMap.prototype.drawLastLabel = function() {
    console.log('drawLastLabel', carHisDataMap.firstPoint);
    var endLabel = new BMap.Label("终止点", carHisDataMap.opts);
    endLabel.setPosition(carHisDataMap.lastPoint);
    endLabel.setStyle(carHisDataMap.styles);
    carHisDataMap.map.addOverlay(endLabel);
};

CarHisDataMap.prototype.drawPath = function(point) {
    if (carHisDataMap.polyline) {                       // 如果存在曲线了,则添加当前点到曲线.
        var points = carHisDataMap.polyline.getPath();
        points.push(point);
        carHisDataMap.polyline.setPath(points);
    } else {                                        //  当曲线不存在是,新建一体条曲线.
        var polyline = new BMap.Polyline([point], {strokeColor:"blue", strokeWeight:6, strokeOpacity:0.5});
        carHisDataMap.polyline = polyline;
        carHisDataMap.map.addOverlay(polyline);
    }
};



//获取单个数据
CarHisDataMap.prototype.getDataFromArr = function() {

    while (carHisDataMap.index < carHisDataMap.allsize) {

        var obj = carHisDataMap.allresult[carHisDataMap.index];		//获取当前一条的记录

        //获取经纬度信息
        var lng = Util.formatSBYCoords(obj[1]);
        var lat = Util.formatSBYCoords(obj[2]);


        if (lng != -300 && lat != -300 && lng > 0 && lat > 0) {
            Util.transferGPS2BD(lng, lat, function (xyresult) {
                if (xyresult.status === 0) {

                    var point = new BMap.Point(xyresult.result[0].x, xyresult.result[0].y);
                    //self.map.centerAndZoom(point, 15);  // 编写自定义函数，创建标注
                    carHisDataMap.updateMap(point, obj);
                }
            });
            break;
        }
        carHisDataMap.index++;
    }

    if (carHisDataMap.index == carHisDataMap.allsize) {
        window.clearInterval(carHisDataMap.mytime);
    }
}
//更新地图信息
CarHisDataMap.prototype.updateMap = function(point, obj) {
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
    var map = carHisDataMap.map;
    if(carHisDataMap.index == 0 || carHisDataMap.num == 0) {
        var polyline = new BMap.Polyline([point], {strokeColor:"blue", strokeWeight:6, strokeOpacity:0.5});
        carHisDataMap.polyline = polyline;
        map.addOverlay(polyline);
        var startLabel = new BMap.Label("起始点", opts);
        startLabel.setPosition(point);
        startLabel.setStyle(styles);
        map.addOverlay(startLabel);
        map.panTo(point);
    } else {
        var polyline = carHisDataMap.polyline;
        var points = polyline.getPath();
        points.push(point);
        polyline.setPath(points);
    }
    if(carHisDataMap.index == carHisDataMap.allsize) {
        var endLabel = new BMap.Label("终止点", opts);
        endLabel.setPosition(point);
        endLabel.setStyle(styles);
        map.addOverlay(endLabel);
    }
    carHisDataMap.index++ ;
    carHisDataMap.num++;
}


