using C2LP.WebService.Model.MyEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2LP.WebService.Model
{
    /// <summary>
    /// 实体类：客户信息
    /// </summary>
    [Serializable]
    public class Model_Customer
    {
        /// <summary>
        /// 自动增长标示Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 客户的全称
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// 客户的联系人
        /// </summary>
        public string ContactPerson { get; set; }

        /// <summary>
        /// 客户的联系电话
        /// </summary>
        public string ContactTel { get; set; }

        /// <summary>
        /// 客户联系地址
        /// </summary>
        public string ContactAddress { get; set; }

        /// <summary>
        /// 所在省份 region Id
        /// </summary>
        public int ProvinceId { get; set; }

        /// <summary>
        /// 所在省份名称
        /// </summary>
        public string ProvinceName { get; set; }

        /// <summary>
        /// 所在市级 region Id
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// 	所在市级名称
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// 所在县级 region Id
        /// </summary>
        public int CountyId { get; set; }

        /// <summary>
        /// 所在县级名称
        /// </summary>
        public string CountyName { get; set; }

        /// <summary>
        /// 	客户企业登陆账号 系统唯一
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 客户的角色
        /// 1 惊尘物流公司(软件部署公司） 2 合作客户（发货单位） 3 收货单位
        /// </summary>
        public Enum_Role Role { get; set; }

        /// <summary>
        /// 客户的当前状态 0 启用 1 停用
        /// </summary>
        public Enum_Active Actived { get; set; }

        /// <summary>
        /// 客户新建时间
        /// </summary>
        public DateTime CreateAt { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark { get; set; }


    }
}
