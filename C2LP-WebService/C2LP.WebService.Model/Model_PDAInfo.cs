using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C2LP.WebService.Model.MyEnum;

namespace C2LP.WebService.Model
{
    /// <summary>
    /// 实体类：PDA信息
    /// </summary>
    [Serializable]
    public class Model_PDAInfo
    {
        /// <summary>
        /// 自动增长标示Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// PDA设备编号 (系统唯一)
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// PDA设备的名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// PDA设备添加的时间
        /// </summary>
        public DateTime CreateAt { get; set; }

        /// <summary>
        /// 激活状态 0 启用 1 停用
        /// </summary>
        public Enum_Active Actived { get; set; }
    }
}
