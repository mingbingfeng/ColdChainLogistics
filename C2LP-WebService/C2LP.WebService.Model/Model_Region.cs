using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2LP.WebService.Model
{
    /// <summary>
    /// 实体类：行政区域
    /// </summary>
    [Serializable]
    public class Model_Region
    {
        /// <summary>
        /// 自动增长标示Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 行政代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 行政区域的名称 省份名称 市级名称 区县名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 上一级行政区域Id， 省份的上级Id默认 0
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 行政级别
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 顺序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 英文名称
        /// </summary>
        public string NameEnglish { get; set; }

        /// <summary>
        /// 英文缩写
        /// </summary>
        public string NameShortEnglish { get; set; }
    }
}
