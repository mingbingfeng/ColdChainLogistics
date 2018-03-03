using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2LP.WebService.Model
{
    /// <summary>
    /// 临时图片表
    /// </summary>
    [Serializable]
    public class Model_TemporaryPictures
    {
        public int id { get; set; }

        public string baseId { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime operateAt { get; set; }
        /// <summary>
        /// 图片路径
        /// </summary>
        public string PicName { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remarks { get; set; }
        /// <summary>
        /// 插入时间
        /// </summary>
        public DateTime inserttime { get; set; }
    }
}
