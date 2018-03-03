using C2LP.WebService.Model.MyEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2LP.WebService.Model
{
    /// <summary>
    /// 实体类：运单号信息
    /// </summary>
    [Serializable]
    public class Model_Waybill_Base
    {
        /// <summary>
        /// 自动增长标示Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 运单编号。系统唯一 ，12位数字 【4位数字的行政编号】【8位的增长序列】eg: 上海地区发送的 [021010000001]
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// 寄货客户单位 customer 标示Id
        /// </summary>
        public int SenderId { get; set; }

        /// <summary>
        /// 	发货单位
        /// </summary>
        public string SenderOrg { get; set; }

        /// <summary>
        /// 寄件人
        /// </summary>
        public string SenderPerson { get; set; }

        /// <summary>
        /// 	寄件人电话
        /// </summary>
        public string SenderTel { get; set; }

        /// <summary>
        /// 寄件人地址
        /// </summary>
        public string SenderAddress { get; set; }

        /// <summary>
        /// 收货客户单位 customer 标示Id, 可以允许为空。 PDA现场可以选择收货单位， 可能系统后台之前并没有录入。
        /// </summary>
        public int ReceiverId { get; set; }

        /// <summary>
        /// 	收货单位
        /// </summary>
        public string ReceiverOrg { get; set; }

        /// <summary>
        /// 	收货人
        /// </summary>
        public string ReceiverPerson { get; set; }

        /// <summary>
        /// 	收货人电话
        /// </summary>
        public string ReceiverTel { get; set; }

        /// <summary>
        /// 收货人地址
        /// </summary>
        public string ReceiverAddress { get; set; }

        /// <summary>
        /// 计费数量、单元， PDA上输入，后台管理统计报表使用
        /// </summary>
        public int BillingCount { get; set; }

        /// <summary>
        /// 	运单状态 运输中 已签收
        /// </summary>
        public Enum_WaybillStage Stage { get; set; }

        /// <summary>
        /// 运单揽件开始时间 。（第一次扫描入系统时间）
        /// </summary>
        public DateTime BeginAt { get; set; }

        /// <summary>
        /// 运单抵达，签收时间。
        /// </summary>
        public DateTime SigninAt { get; set; }

        /// <summary>
        /// 签字图片
        /// </summary>
        public DateTime PicPostbackAt { get; set; }

        /// <summary>
        /// 公司名称 0 惊尘公司 1 第三方公司
        /// </summary>
        public Enum_Company Company { get; set; }
    }
}
