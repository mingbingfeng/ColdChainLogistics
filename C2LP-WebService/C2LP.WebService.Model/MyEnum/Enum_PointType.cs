using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2LP.WebService.Model.MyEnum
{
    /// <summary>
    /// 点类型
    /// </summary>
    [Serializable]
    public enum Enum_PointType
    {
        /// <summary>
        /// 温度
        /// </summary>
        Temp=1,
        /// <summary>
        /// 湿度
        /// </summary>
        Hump=2,
        /// <summary>
        /// 经度
        /// </summary>
        Longitude = 3,
        /// <summary>
        /// 纬度
        /// </summary>
        Latitude = 4
    }
}
