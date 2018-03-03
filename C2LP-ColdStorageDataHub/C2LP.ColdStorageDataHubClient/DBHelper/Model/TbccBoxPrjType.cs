using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2LP.ColdStorageDataHubClient.DBHelper.Model
{
    /// <summary>
    /// 数据库实体 TbccBoxPrjType 保温箱项目列表
    /// </summary>
    public class TbccBoxPrjType
    {
        public int ID { get; set; }

        public string BoxProjectId { get; set; }

        public string BoxProjectName { get; set; }

        public string projectAuthCode { get; set; }

        public string projectCode { get; set; }

        public int SelectPortNo { get; set; }
    }
}
