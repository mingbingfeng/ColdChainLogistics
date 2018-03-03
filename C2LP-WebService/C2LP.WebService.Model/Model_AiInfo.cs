using C2LP.WebService.Model.MyEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2LP.WebService.Model
{
    /// <summary>
    /// 实体类：Ai信息
    /// </summary>
    [Serializable]
    public class Model_AiInfo
    {
        /// <summary>
        /// 自动增长标示Id
        /// </summary>
        public int PointId { get; set; }

        /// <summary>
        /// 引用冷库、车载存储表 coldstorage 标示ID
        /// </summary>
        public int StorageId { get; set; }

        /// <summary>
        /// 探头名称
        /// </summary>
        public string PpointName { get; set; }

        /// <summary>
        /// 探头类型 1 温度 2湿度 3 经度 4 纬度 方便前端数据显示单位
        /// </summary>
        public Enum_PointType PointType { get; set; }

        /// <summary>
        /// 探头激活状态 0 启用 1 停用
        /// </summary>
        public Enum_Active Actived { get; set; }
    }
}
