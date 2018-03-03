using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2LP.ColdStorageDataHubClient.DBHelper.Model
{
    /// <summary>
    /// 数据库实体 tbcchistdata_box_ProjectId_1 保温箱历史数据
    /// </summary>
    public class TbccBoxAiData
    {
        public int Id { get; set; }

        public string ProjectId { get; set; }

        public DateTime UpdateTime { get; set; }

        public decimal AI1 { get; set; }
        public decimal AI2 { get; set; }
        public decimal AI3 { get; set; }
        public decimal AI4 { get; set; }

        /// <summary>
        /// 上报标记：0表示未上报,1表示已上报
        /// </summary>
        public int fdap4zgswUploadState { get; set; }
    }
}
