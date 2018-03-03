using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2LP.ColdStorageDataHubClient.DBHelper.Model
{
    /// <summary>
    /// 数据库实体 TbccRefInfo 仓库配置
    /// </summary>
    public class TbccRefInfo
    {
        public string ProjectId { get; set; }

        public int NetId { get; set; }

        public int RefId { get; set; }

        public string RefName { get; set; }
    }
}
