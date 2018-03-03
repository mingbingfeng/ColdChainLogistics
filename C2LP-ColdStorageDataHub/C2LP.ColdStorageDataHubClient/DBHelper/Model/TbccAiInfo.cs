using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2LP.ColdStorageDataHubClient.DBHelper.Model
{
    /// <summary>
    /// 数据库实体 TbccAiInfo 仓库AI点信息
    /// </summary>
    public class TbccRefAiInfo
    {
        public string ProjectID { get; set; }

        public int NetId { get; set; }

        public int RefId { get; set; }

        public float PortNo { get; set; }

        public string PortName { get; set; }

        public AiDataTypeEnum DataType { get; set; }
    }

    /// <summary>
    /// 5100探头结构 数据库实体 TbccSensorInfo 仓库探头信息
    /// </summary>
    public class TbccRefSensorInfo {
        public int SensorId { get; set; }

        public string ProjectId { get; set; }

        public int refId { get; set; }

        public string  TName { get; set; }

        public string RhName { get; set; }

        public int SensorMode { get; set; }
    }

    /// <summary>
    /// AI数据类型
    /// </summary>
    public enum AiDataTypeEnum {
        温度=1,
        湿度=2
    }

    ///// <summary>
    ///// 探头类型
    ///// </summary>
    //public enum SensorModel {
    //    温湿度一体=0,
    //    单温度=1,
    //    单湿度=2
    //}
}
