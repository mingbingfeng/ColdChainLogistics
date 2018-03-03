using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2LP.WebService.Model.MyEnum
{
    /// <summary>
    /// 角色
    /// </summary>
    [Serializable]
    public enum Enum_Role
    {
        /// <summary>
        /// 惊尘物流公司(软件部署公司）
        /// </summary>
        Administrator = 1,
        /// <summary>
        /// 合作客户（发货单位）
        /// </summary>
        Sender = 2,
        /// <summary>
        /// 收货单位
        /// </summary>
        Receiver = 3
    }
}
