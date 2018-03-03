using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2LP.WebService.Model
{
    /// <summary>
    /// 实体类：冷库，车载与PDA设备关联绑定关系 
    /// </summary>
    [Serializable]
    public class Model_Storage_Device
    {
        /// <summary>
        /// 自动增长标示Id
        /// </summary>
        public int id { set; get; }

        /// <summary>
        /// 关联的存储载体 coldstorage Id
        /// </summary>
        public int storageId { set; get; }

        /// <summary>
        /// PDA 设备 device_info 的标示ID
        /// </summary>
        public int deviceId { set; get; }

        /// <summary>
        /// 0 不是默认 1 默认绑定
        /// </summary>
        public int isDefault { set; get; }
    }
}
