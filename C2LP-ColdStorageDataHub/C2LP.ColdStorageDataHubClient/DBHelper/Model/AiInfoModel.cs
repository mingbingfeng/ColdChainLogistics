using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2LP.ColdStorageDataHubClient.DBHelper.Model
{
    /// <summary>
    /// 企业探头信息实体
    /// </summary>
    public class AiInfoModel
    {
        /// <summary>
        /// 自动增列 唯一标识
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 探头编号
        /// </summary>
        public int aiNumber { get; set; }

        ///// <summary>
        ///// 企业编号
        ///// </summary>
        //public string oid { get; set; }

        ///// <summary>
        ///// 探头名称
        ///// </summary>
        //public string ainame { get; set; }

        ///// <summary>
        ///// 冷库名称
        ///// </summary>
        //public string refname { get; set; }

        ///// <summary>
        ///// 项目工程编号 （车载，保温箱存在）
        ///// </summary>
        //public string projectNO { get; set; }

        /// <summary>
        /// 项目类型 `1 仓库类型` `2 车载类型` ` 3保温箱类型`
        /// </summary>
        //public AiTypeEnum type { get; set; }

        /////// <summary>
        /////// 冷库编号
        /////// </summary>
        ////public string refId { get; set; }

        ///// <summary>
        ///// 数据类型
        ///// </summary>
        //public AiDataTypeEnum selftype { get; set; }

        /// <summary>
        /// 关联本系统的 项目编号
        /// </summary>
        public string LinkProjectNo { get; set; }

        /// <summary>
        /// 关联本系统的 网络编号
        /// </summary>
        public int LinkNetId { get; set; }

        /// <summary>
        /// 关联本系统的 冷库编号
        /// </summary>
        public int LinkRefId { get; set; }

        /// <summary>
        /// 关联本系统的 点编号
        /// </summary>
        public float LinkPortNo { get; set; }

    }

    ///// <summary>
    ///// 项目类型 `1 仓库类型` `2 车载类型` ` 3保温箱类型`
    ///// </summary>
    //public enum AiTypeEnum
    //{
    //    仓库 = 1,
    //    车载 = 2,
    //    小批零 = 3
    //}
}
