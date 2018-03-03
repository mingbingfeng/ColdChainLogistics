using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2LP.Assistant.TMSConsole.Model
{
    class RelationModel
    {
        /// <summary>
        /// 唯一键值
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// TMS运单号
        /// </summary>
        public string RelationId { get; set; }

        /// <summary>
        /// 内部运单号
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// 最后上报的节点ID。-1：未上传；NodeID：当前已上报截止的节点ID；0：上报完成
        /// </summary>
        public int? CurrentUploadNodeId { get; set; }

        /// <summary>
        /// 最后上报节点数据的节点ID。-1：未上传；NodeID：当前已上节点数据报截止的节点ID；0：上报完成
        /// </summary>
        public int? CurrentUploadDataNodeId { get; set; }

        /// <summary>
        /// 最后上报节点中数据的时间进度标记
        /// </summary>
        public string CurrentUploadDataTime { get; set; }

        /// <summary>
        /// 大华东供应链密钥 || 运管平台物流交换代码
        /// </summary>
        public int CustomerId { get; set; }
    }
}
