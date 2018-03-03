using C2LP.WebService.Model.MyEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2LP.WebService.Model
{
    /// <summary>
    /// 实体类：客户用户信息
    /// </summary>
    [Serializable]
    public class Model_CustomerUser
    {
        /// <summary>
        /// 自动增长标示Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 引用企业客户 customer的标示Id
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// 企业登陆用户名称
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// MD5（password）后的密码值
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 用户的中文显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 用户新建的时间
        /// </summary>
        public DateTime CreateAt { get; set; }

        /// <summary>
        /// 0 启用 1 停用
        /// </summary>
        public Enum_Active Actived { get; set; }
    }
}
