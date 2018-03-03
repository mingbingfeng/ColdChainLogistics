using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2LP.WebService.Model.MyEnum
{
    /// <summary>
    /// 运单状态
    /// </summary>
    [Serializable]
    public  enum Enum_WaybillStage
    {
        /// <summary>
        /// 运输中
        /// </summary>
        Transporting=0,
        /// <summary>
        /// 已签收
        /// </summary>
        Received=1
    }
}
