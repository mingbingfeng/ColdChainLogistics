using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2LP.WebService.Model
{
    /// <summary>
    /// 实体类：运单签字拍照回传信息表 
    /// </summary>
    [Serializable]
    public class Model_Waybill_Postback_Pic
    {
        /// <summary>
        /// 自动增长标示Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 引用运单物流 waybill_base 标示Id
        /// </summary>
        public string BaseId { get; set; }
        /// <summary>
        /// 图片的名称
        /// </summary>
        public string PicName { get; set; }
    }
}
