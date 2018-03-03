using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2LP.WebService.Model.MyEnum
{
    /// <summary>
    /// 载体类型
    /// </summary>
    [Serializable]
    public enum Enum_StorageType
    {
        /// <summary>
        /// 冷库
        /// </summary>
        ColdStorage = 1,
        /// <summary>
        /// 车载
        /// </summary>
        CarStorage = 2
    }
}
