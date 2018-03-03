using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2LP.WebService.Model.MyEnum
{
    /// <summary>
    /// 是否运单到达
    /// </summary>
    [Serializable]
    public enum Enum_Arrived
    {
        /// <summary>
        /// 未知
        /// </summary>
        NON=0,
        /// <summary>
        /// 运输中
        /// </summary>
        InTransit=1,
        /// <summary>
        /// 已运抵
        /// </summary>
        HaveArrived=2
    }
}
