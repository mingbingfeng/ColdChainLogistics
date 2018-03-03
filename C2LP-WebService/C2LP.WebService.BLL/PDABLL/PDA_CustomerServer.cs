using C2LP.WebService.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace C2LP.WebService.BLL.PDABLL
{
    public class PDA_CustomerServer : BaseServer
    {
        /// <summary>
        /// 根据设备ID获取其负责区域的客户信息
        /// </summary>
        /// <param name="pId">设备ID</param>
        /// <returns></returns>
        public static List<Model_Customer> GetCustomerByPId(int pId, int maxId)
        {
            List<Model_Customer> list = new List<Model_Customer>();
            try
            {
                string sql = "select regionId as id from region_device where deviceId=?id ";
                List<MySqlParameter> para = new List<MySqlParameter>();
                para.Add(new MySqlParameter("id", pId));
                List<Model_Region> rList = _SqlHelp.ExecuteObjects<Model_Region>(sql, para.ToArray());
                para.Clear();
                List<int> rIdList = rList.Select(l => l.Id).ToList();
                string clms = "id,fullName,contactPerson,contactTel,contactAddress,provinceId,cityId,countyId,role";
                sql = "select [clms] from customer where role<>1 and actived=0 and Id>" + maxId + "";
                if (rIdList != null && rIdList.Count > 0 && rIdList[0] != 0)
                {
                    sql += " and provinceId in(?idList) or cityId in (?idList) order by provinceId,cityId";
                    para.Add(new MySqlParameter("idList", string.Join(",", rIdList)));
                }
                sql += string.Format(" limit {0}", 50);
                list = _SqlHelp.ExecuteObjects<Model_Customer>(sql.Replace("[clms]", clms), para.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }

        /// <summary>
        /// 根据设备ID获取其负责区域的客户信息
        /// </summary>
        /// <param name="pId">设备ID</param>
        /// <returns></returns>
        public static List<Model_Customer> GetCustomerByPId(int pId)
        {
            List<Model_Customer> list = new List<Model_Customer>();
            try
            {
                string sql = "select regionId as id from region_device where deviceId=?id";
                List<MySqlParameter> para = new List<MySqlParameter>();
                para.Add(new MySqlParameter("id", pId));
                List<Model_Region> rList = _SqlHelp.ExecuteObjects<Model_Region>(sql, para.ToArray());
                para.Clear();
                List<int> rIdList = rList.Select(l => l.Id).ToList();
                string clms = "id,fullName,contactPerson,contactTel,contactAddress,provinceId,cityId,countyId,role";
                sql = "select [clms] from customer where role<>1 and actived=0";
                if (rIdList != null && rIdList.Count > 0 && rIdList[0] != 0)
                {
                    sql += " and provinceId in(?idList) or cityId in (?idList) order by provinceId,cityId";
                    para.Add(new MySqlParameter("idList", string.Join(",", rIdList)));
                }
                list = _SqlHelp.ExecuteObjects<Model_Customer>(sql.Replace("[clms]", clms), para.ToArray());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }

        /// <summary>
        /// 获取指定时间后更新的客户信息
        /// </summary>
        /// <param name="getTime">获取时间</param>
        /// <param name="maxId">当前获取到的客户信息索引</param>
        /// <returns></returns>
        public static List<PDA_Model_Customer> GetNewCustomers(DateTime getTime, int maxId)
        {
            List<PDA_Model_Customer> list = new List<PDA_Model_Customer>();
            try
            {
                string sql = string.Format("select id,fullName,contactPerson,contactTel,contactAddress,provinceId,cityId,role,countyId from customer where lastUpdateTime>'{0}' and id>{1} limit {2}", getTime.ToString("yyyy-MM-dd HH:mm:ss"), maxId, 50);
                list = _SqlHelp.ExecuteObjects<PDA_Model_Customer>(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }

        /// <summary>
        /// 获取所有第三方客户
        /// </summary>
        /// <returns></returns>
        public static List<Model_ThirdCustomer> GetThirdCustomers(int customerId=0) {
            List<Model_ThirdCustomer> list = new List<Model_ThirdCustomer>();
            try
            {
                string sql = "select customerid,c.fullname as customerName,ct.linkType,ct.linkRegex from customer_transport ct left join customer c on c.id = ct.customerid";
                if (customerId != 0)
                    sql += " where customerId= "+customerId;
                list = _SqlHelp.ExecuteObjects<Model_ThirdCustomer>(sql);
                if (customerId != 0 && list.Count == 0)
                    throw new Exception("查询ID为["+customerId+"]的客户信息失败");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }
        
    }
}
