using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2LP.WebService.Model
{
    [Serializable]
    public class Model_Destination
    {
        /// <summary>
        /// 标识ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 目的地
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 设备 device_info的的标示Id
        /// </summary>
        public int DeviceId { get; set; }
    }
}
