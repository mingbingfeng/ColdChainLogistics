using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2LP.WebService.Model.MyEnum
{
    /// <summary>
    /// 绑定状态
    /// </summary>
    [Serializable]
    public enum Enum_IsDefault
    {
        /// <summary>
        /// 0 不默认绑定
        /// </summary>
        NotDefault = 0,

        /// <summary>
        /// 1 默认绑定
        /// </summary>
        Eefault =1
    }
}
