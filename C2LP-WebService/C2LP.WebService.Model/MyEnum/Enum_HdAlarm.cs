using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2LP.WebService.Model.MyEnum
{
    /// <summary>
    /// 历史数据报警状态
    /// </summary>
    [Serializable]
    public  enum Enum_HdAlarm
    {
        /// <summary>
        /// 正常
        /// </summary>
        Normal=0,
        /// <summary>
        /// 报警
        /// </summary>
        Alarm =1
    }
}
