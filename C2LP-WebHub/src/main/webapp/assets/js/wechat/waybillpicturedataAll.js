/**
 * Created by wanghe on 2017/1/9.
 */
var WaybillPictureAll = function() {
    this.init();
}

WaybillPictureAll.prototype.init = function () {
       this.onloadpicture();
}

WaybillPictureAll.prototype.onloadpicture = function () {
    URLHelper.ajax({
            'url': URLHelper.coldchainAllzhexdataUrl(),
            'method': 'GET',
            'data': {'number': $("#number").val()},
            'dataType': 'json',
            'success': function (result) {
                for(var j=0;j<result.length;j++){
                    var data = result[j];
                    var aiList = data.aiList;
                    var dataList = data.aiinfoHistoryList;
                    var list = [];
                    $(aiList).each(function (i) {
                        var arr = [];
                        $(dataList).each(function (a) {
                            var str = new Date(Date.parse(dataList[a][0].replace(/-/g, '/'))).getTime() + (8 * 3600 * 1000);
                            if (dataList[a][i + 1] == -300 || dataList[a][i + 1] == -200) {
                            } else {
                                arr.push([str, dataList[a][i + 1]]);
                            }
                        });
                        arr.sort();
                        if (aiList[i].pointType == 1) {
                            list.push({'yAxis': 0, 'name': aiList[i].pointName, 'data': arr});
                        } else {
                            list.push({'yAxis': 1, 'name': aiList[i].pointName, 'data': arr});
                        }
                    });
                    if (list != '') {
                        var html = '<b>&nbsp;&nbsp;&nbsp;--'+data.storageName+'</b><div id="container'+(j+1)+'"  style="width:100%;margin-bottom: 15px;"></div>'
                        $('#bottom').append( $(html) );
                        $('#container'+(j+1)).highcharts({
                            chart: {type: 'line'},
                            credits: {enabled: false   //右下角不显示LOGO
                            },
                            title: {
                                text: '温湿度曲线'
                            },
                            xAxis: {
                                title: {
                                    text: '时间'
                                },
                                type: 'time',
                                labels: {
                                    formatter: function () {
                                        return Highcharts.dateFormat('%H:%M:%S', this.value);
                                    },
                                    step: 0,
                                    rotation: -45
                                }
                            },
                            yAxis: [
                                {
                                    title: {
                                        text: '温度 (°C)'
                                    },
                                    opposite: false
                                },
                                {
                                    title: {
                                        text: '湿度 (%)'
                                    },
                                    opposite: true
                                }
                            ],
                            tooltip: {
                                formatter: function () {
                                    if (this.series.yAxis.opposite) {
                                        return '<b>' + this.series.name + '</b><br/>' +
                                            Highcharts.dateFormat('%Y-%m-%d  %H:%M:%S', this.x) + " : " + this.y.toFixed(1) + "%";
                                    } else {
                                        return '<b>' + this.series.name + '</b><br/>' +
                                            Highcharts.dateFormat('%Y-%m-%d  %H:%M:%S', this.x) + " : " + this.y.toFixed(1) + "°C";
                                    }
                                }
                            },
                            legend: {
                                align: 'center',
                                verticalAlign: 'bottom',
                                borderWidth: 0
                            },
                            series: list
                        });
                    }
                }
            }
        })
}