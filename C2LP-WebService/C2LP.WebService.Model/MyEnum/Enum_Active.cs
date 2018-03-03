using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2LP.WebService.Model.MyEnum
{
    /// <summary>
    /// 激活状态
    /// </summary>
    [Serializable]
    public enum Enum_Active
    {
        /// <summary>
        /// 0 启用
        /// </summary>
        Enabled = 0,
        /// <summary>
        /// 1 停用
        /// </summary>
        Disable = 1
    }
}
