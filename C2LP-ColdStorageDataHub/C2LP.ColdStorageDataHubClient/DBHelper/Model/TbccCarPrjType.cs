using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2LP.ColdStorageDataHubClient.DBHelper.Model
{
    /// <summary>
    /// 数据库实体 TbccCarPrjType 车载项目列表
    /// </summary>
    public class TbccCarPrjType
    {
        public int ID { get; set; }

        public string CarProjectId { get; set; }

        public string CarProjectName { get; set; }

        public string ProjectAuthCode { get; set; }

        public int SelectPortNo { get; set; }
    }
}
