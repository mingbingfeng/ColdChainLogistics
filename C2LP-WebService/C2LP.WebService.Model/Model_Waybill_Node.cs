using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C2LP.WebService.Model.MyEnum;

namespace C2LP.WebService.Model
{
    /// <summary>
    /// 实体类：运单号物流节点
    /// </summary>
    [Serializable]
    public class Model_Waybill_Node
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
        /// 运单物流节点的操作时间
        /// </summary>
        public DateTime operateAt { get; set; }

        /// <summary>
        /// 物流运输载体 冷库标示Id coldstorage
        /// </summary>
        public int StorageId { get; set; }

        /// <summary>
        /// 冷库名称或者车载名称
        /// </summary>
        public string StorageName { get; set; }

        /// <summary>
        /// 物流节点的信息， 把显示的信息在生成节点的时候保存起来
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 是否运单到达 1 运输中 2 已运抵 (最后一次车载扫描节点和拍照的节点）
        /// </summary>
        public Enum_Arrived Arrived { get; set; }
        
    }
}
