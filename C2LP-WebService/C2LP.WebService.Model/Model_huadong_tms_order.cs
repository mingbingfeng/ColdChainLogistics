using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2LP.WebService.Model
{
    /// <summary>
    /// 华东医药托运订单 
    /// </summary>
    [Serializable]
    public class Model_Huadong_Tms_Order
    {
        /// <summary>
        /// 自动增长标示Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 关联的编号， 运输任务单好或者TMS运单号二选一， 用来标记与waybillbase 关联使用
        /// </summary>
        public string RelationId { get; set; }
                /// <summary>
        /// 发货单位
        /// </summary>
        public string SenderOrg { get; set; }
        /// <summary>
        /// 寄件人
        /// </summary>
        public string SenderPerson { get; set; }
        /// <summary>
        /// 寄件人电话
        /// </summary>
        public string SenderTel { get; set; }
        /// <summary>
        /// 寄件人地址
        /// </summary>
        public string SenderAddress { get; set; }
        /// <summary>
        /// 收货单位
        /// </summary>
        public string ReceiverOrg { get; set; }
        /// <summary>
        /// 收货人
        /// </summary>
        public string ReceiverPerson { get; set; }
        /// <summary>
        /// 收货人电话
        /// </summary>
        public string ReceiverTel { get; set; }
        /// <summary>
        /// 收货人地址
        /// </summary>
        public string ReceiverAddress { get; set; }
        /// <summary>
        /// 代码： 仓库代码+ 路单ID 如： HD2_82403782
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 订单组号 暂留
        /// </summary>
        public string SRCEXPNO { get; set; }
        /// <summary>
        /// 相关单据号1 路单ID
        /// </summary>
        public string ROADID { get; set; }
        /// <summary>
        /// 相关单据2： 运输任务单号
        /// </summary>
        public string SHIPDETAILID { get; set; }
        /// <summary>
        /// 相关单据3： 来源总单号
        /// </summary>
        public string TOTALID { get; set; }
        /// <summary>
        /// TMS 运单号
        /// </summary>
        public string LEGCODE { get; set; }
        /// <summary>
        /// TMS 派车单号
        /// </summary>
        public string SHIPMENTCODE { get; set; }
        /// <summary>
        /// 货主代码
        /// </summary>
        public string CONSIGNORCODE { get; set; }
        /// <summary>
        /// 货主名称 
        /// </summary>
        public string CONSIGNORNAME { get; set; }
        /// <summary>
        /// 货主部门代码
        /// </summary>
        public string DEPTNO { get; set; }
        /// <summary>
        /// 货主部门名称
        /// </summary>
        public string DEPTNAME { get; set; }
        /// <summary>
        /// 客户代码
        /// </summary>
        public string CUSTOMERCODE { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        public string CUSTOMERNAME { get; set; }
        /// <summary>
        /// 仓库代码
        /// </summary>
        public string AREAHOUSE { get; set; }
        /// <summary>
        /// 业务员和业务员电话
        /// </summary>
        public string SALESMAN { get; set; }
        /// <summary>
        /// 运输方式代码 铁路、公路、航空、(邮寄、快递)默认公路
        /// </summary>
        public string TRANSMODEID { get; set; }
        /// <summary>
        /// ERP运输方式 Bms下来
        /// </summary>
        public string ERPTRANSMODENAME { get; set; }
        /// <summary>
        /// 订单类型：销售、销退、移库出、移库入、提维修、送维修、进货退出、赠品出库、借出、补送药检单、补送发票，补送货物、换货、急救、退厂维修
        /// </summary>
        public string OPERATIONTYPE { get; set; }
        /// <summary>
        /// 要求到达日期 yyyy-mm-dd hh：MM：ss
        /// </summary>
        public DateTime DEMANDARRIVETIME { get; set; }
        /// <summary>
        /// 运输业务类型 冷藏、冷冻、麻药等… 
        /// </summary>
        public string TRANSPORTTYPE { get; set; }
        /// <summary>
        /// 订单紧急性 普通单、加急单
        /// </summary>
        public string ORDERINSTANCY { get; set; }
        /// <summary>
        /// 运输类别 默认市内、市外
        /// </summary>
        public string TRANSPORTCATEGORY { get; set; }
        /// <summary>
        /// 线路号
        /// </summary>
        public string ROUTENO { get; set; }
        /// <summary>
        /// 运输时限要求
        /// </summary>
        public string TRANSDEADLINE { get; set; }
        /// <summary>
        /// 发货地址代码
        /// </summary>
        public string FROMGTRANSID { get; set; }
        /// <summary>
        /// 发货方名称
        /// </summary>
        public string FROMGTRANSNAME { get; set; }
        /// <summary>
        /// 收货方代码
        /// </summary>
        public string TOGTRANSID { get; set; }
        /// <summary>
        /// 收货方名称
        /// </summary>
        public string TOGTRANSNAME { get; set; }
        /// <summary>
        /// 送货地址
        /// </summary>
        public string RECEIVEADDR { get; set; }
        /// <summary>
        /// 收货人
        /// </summary>
        public string RECEIVEMAN { get; set; }
        /// <summary>
        /// 收货人电话
        /// </summary>
        public string RECEIVEPHONE { get; set; }
        /// <summary>
        /// 开单日期
        /// </summary>
        public DateTime CREDATE { get; set; }
        /// <summary>
        /// 出入库类型
        /// </summary>
        public string INOUTFLAG { get; set; }
        /// <summary>
        /// Wms集货线流水号
        /// </summary>
        public string WMSROUTEWAVENO { get; set; }
        /// <summary>
        /// 打印分类
        /// </summary>
        public string PRINTTYPE { get; set; }
        /// <summary>
        /// 总件数
        /// </summary>
        public string TOTALQUNTITY { get; set; }
        /// <summary>
        /// 整件数
        /// </summary>
        public Double WHOLEQUNTITY { get; set; }
        /// <summary>
        /// 散件数
        /// </summary>
        public Double PARTQUNTITY { get; set; }
        /// <summary>
        /// 计费件数
        /// </summary>
        public Double JFQUNTITY { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string DESCRIPTION { get; set; }
        /// <summary>
        /// 扩展字段0
        /// </summary>
        public string EXTCOL0 { get; set; }
        /// <summary>
        /// 扩展字段1
        /// </summary>
        public string EXTCOL1 { get; set; }
        /// <summary>
        /// 扩展字段2
        /// </summary>
        public string EXTCOL2 { get; set; }
        /// <summary>
        /// 扩展字段3
        /// </summary>
        public string EXTCOL3 { get; set; }
        /// <summary>
        /// 扩展字段4
        /// </summary>
        public string EXTCOL4 { get; set; }
        /// <summary>
        /// 扩展字段5
        /// </summary>
        public string EXTCOL5 { get; set; }
        /// <summary>
        /// 扩展字段6
        /// </summary>
        public string EXTCOL6 { get; set; }
        /// <summary>
        /// 扩展字段7
        /// </summary>
        public string EXTCOL7 { get; set; }
        /// <summary>
        /// 扩展字段8
        /// </summary>
        public string EXTCOL8 { get; set; }
        /// <summary>
        /// 扩展字段9
        /// </summary>
        public string EXTCOL9 { get; set; }
        /// <summary>
        /// 扩展字段10
        /// </summary>
        public string EXTCOL10 { get; set; }
        /// <summary>
        /// 扩展字段11
        /// </summary>
        public string EXTCOL11 { get; set; }
        /// <summary>
        /// 扩展字段12
        /// </summary>
        public string EXTCOL12 { get; set; }
        /// <summary>
        /// 扩展字段13
        /// </summary>
        public string EXTCOL13 { get; set; }
        /// <summary>
        /// 扩展字段14
        /// </summary>
        public string EXTCOL14 { get; set; }
        /// <summary>
        /// 扩展字段15
        /// </summary>
        public string EXTCOL15 { get; set; }
        /// <summary>
        /// 扩展字段16
        /// </summary>
        public string EXTCOL16 { get; set; }
        /// <summary>
        /// 扩展字段17
        /// </summary>
        public string EXTCOL17 { get; set; }
        /// <summary>
        /// 扩展字段18
        /// </summary>
        public string EXTCOL18 { get; set; }
        /// <summary>
        /// 扩展字段19
        /// </summary>
        public string EXTCOL19 { get; set; }

    }
}
