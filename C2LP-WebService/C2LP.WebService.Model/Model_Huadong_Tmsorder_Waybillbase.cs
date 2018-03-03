using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C2LP.WebService.Model.MyEnum;

namespace C2LP.WebService.Model
{
    [Serializable]
    public class Model_Huadong_Tmsorder_Waybillbase
    {
        /// <summary>
        /// 自动增长标示Id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 托运订单编号
        /// </summary>
        public string relationId { get; set; }
        /// <summary>
        /// 运单编号
        /// </summary>
        public string number { get; set; }
        /// <summary>
        /// 最后上报的节点ID。-1：未上传；NodeID：当前已上报截止的节点ID；0：上报完成
        /// </summary>
        public int currentUploadNodeId { get; set; }
        /// <summary>
        /// 最后上报节点数据的节点ID。-1：未上传；NodeID：当前已上节点数据报截止的节点ID；0：上报完成
        /// </summary>
        public int currentUploadDataNodeId { get; set; }
        /// <summary>
        /// 最后上报节点中数据的时间进度标记
        /// </summary>
        public DateTime currentUploadDataTime { get; set; }
    }
}
