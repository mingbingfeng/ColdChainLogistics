using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C2LP.WebService.Model.MyEnum;

namespace C2LP.WebService.Model
{
    /// <summary>
    /// 实体类：冷库/车载信息/PDA设备信息
    /// </summary>
    [Serializable]
    public class Model_ColdstoragePDA
    {
        #region 冷库/车载
        /// <summary>
        /// 自动增长标示Id
        /// </summary>
        public int StorageId { get; set; }

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
        public DateTime StorageCreateAt { get; set; }

        /// <summary>
        /// 客户的当前状态 0 启用 1 停用
        /// </summary>
        public Enum_Active StorageActived { get; set; }
        #endregion

        #region PDA设备
        /// <summary>
        ///  自动增长标示Id
        /// </summary>
        public int PDAId { get; set; }

        /// <summary>
        /// PDA设备编号 (系统唯一)
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// PDA设备的名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// PDA设备添加的时间
        /// </summary>
        public DateTime DeviceCreateAt { get; set; }

        /// <summary>
        /// PDA设备当前状态 0 启用 1 停用
        /// </summary>
        public Enum_Active DeviceActived { get; set; } 
        #endregion

        #region 冷库/车载与PDA设备关系表
        /// <summary>
        ///  自动增长标示Id
        /// </summary>
        public int StorageDeviceId { get; set; }

        /// <summary>
        /// 绑定状态 0 不默认 1 默认
        /// </summary>
        public Enum_IsDefault isDefault { get; set; } 
        #endregion
    }
}
