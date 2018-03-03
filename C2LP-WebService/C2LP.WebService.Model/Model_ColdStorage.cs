using C2LP.WebService.Model.MyEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2LP.WebService.Model
{
    /// <summary>
    /// 实体类：冷库/车载信息
    /// </summary>
    [Serializable]
    public class Model_ColdStorage
    {
        /// <summary>
        /// 自动增长标示Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 冷库/车载车牌的名称
        /// </summary>
        public string StorageName { get; set; }

        /// <summary>
        /// 存储冷库类型 1 冷库 2 车载
        /// </summary>
        public Enum_StorageType StorageType { get; set; }

        /// <summary>
        /// 车载驾驶员司机
        /// </summary>
        public string Driver { get; set; }

        /// <summary>
        /// 车载驾驶员电话
        /// </summary>
        public string DriverTel { get; set; }

        /// <summary>
        /// 扩展信息
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 冷库/车载添加的时间
        /// </summary>
        public DateTime CreateAt { get; set; }

        /// <summary>
        /// 客户的当前状态 0 启用 1 停用
        /// </summary>
        public Enum_Active Actived { get; set; }
    }
}
