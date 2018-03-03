using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2LP.WebService.Model.MyEnum
{
    /// <summary>
    /// 公司名
    /// </summary>
    [Serializable]
    public enum Enum_Company
    {
        /// <summary>
        /// 惊尘物流公司(软件部署公司）
        /// </summary>
        Administrator = 0,
        /// <summary>
        /// 第三方公司
        /// </summary>
        ThirdParty = 1
    }
}
