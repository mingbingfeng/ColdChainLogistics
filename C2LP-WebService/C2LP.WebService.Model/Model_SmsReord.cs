using C2LP.WebService.Model.MyEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2LP.WebService.Model
{

    /// <summary>
    /// 实体类：短信记录
    /// </summary>
    [Serializable]
    public class Model_SmsReord
    {
        /// <summary>
        /// 自增ID
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 关联的运单ID
        /// </summary>
        public int BaseId { get; set; }

        /// <summary>
        /// 关联的节点ID
        /// </summary>
        public int NodeId { get; set; }

        /// <summary>
        /// 关联的节点运输状态
        /// </summary>
        public Enum_Arrived Arrived { get; set; }

        /// <summary>
        /// 短信记录创建时间
        /// </summary>
        public DateTime CreateAt { get; set; }

        /// <summary>
        /// 短信接收者号码
        /// </summary>
        public string SmsReceiver { get; set; }

        /// <summary>
        /// 短信内容
        /// </summary>
        public string SmsContent { get; set; }

        /// <summary>
        /// 发送短信的时间
        /// </summary>
        public DateTime SendTime { get; set; }

        /// <summary>
        /// 发送短信结果
        /// </summary>
        public string SendResult { get; set; }


        #region 关联信息
        /// <summary>
        /// 运单编号
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// 发货单位
        /// </summary>
        public string SenderOrg { get; set; }

        /// <summary>
        /// 收货单位
        /// </summary>
        public string ReceiverOrg { get; set; }
        
        public string SenderTel { get; set; }

        public string ReceiverTel { get; set; }

        public string StorageName { get; set; }
        #endregion

        public override string ToString()
        {
            string msg = string.Format("{0} 运单号[{1}] 接收号码[{2}] 短信内容[{3}] 发送结果[{4}]", (Arrived == Enum_Arrived.InTransit ? "揽件短信通知" : "运抵短信通知"), Number, SmsReceiver, SmsContent, SendResult);
            return msg;
        }
    }
}
