using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using C2LP.PDA.Datas.Model;
using System.Data;

namespace C2LP.PDA.Datas.BLL
{
    public class CustomerServer : BaseServer
    {
        /// <summary>
        /// 更新客户信息
        /// </summary>
        /// <param name="valueStr">sql value数据字符</param>
        /// <returns></returns>
        public static int UpdateCustomers(string valueStr)
        {
            string sql = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(valueStr))
                {
                    sql += "insert into c2lp_customer (Id,fullName,contactPerson,contactTel,contactAddress,provinceId,cityId,role,countyId) values " + valueStr;
                    return _SqlHelp.ExecuteNonQuery(sql, System.Data.CommandType.Text);
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 根据拉取到的客户信息，获取需要更新的客户ID
        /// </summary>
        /// <param name="updateIdList"></param>
        /// <returns></returns>
        public static List<int> GetUpdateCustomerIdList(List<string> updateIdList)
        {
            List<int> idList = new List<int>();
            if (updateIdList.Count > 0)
            {
                try
                {
                    string sql = "select Id from c2lp_customer where Id in (" + string.Join(",", updateIdList.ToArray()) + ")";
                    DataSet ds = _SqlHelp.ExecuteDataSet(sql, System.Data.CommandType.Text);
                    if (ds != null && ds.Tables.Count > 0)
                    {
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            idList.Add(Convert.ToInt32(row["Id"]));
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return idList;
        }

        /// <summary>
        /// 更新客户信息
        /// </summary>
        /// <returns></returns>
        public static void UpdateCustomers(ref string insertSql, ref string updateSql)
        {
            try
            {
                if (!string.IsNullOrEmpty(insertSql))
                    insertSql = _SqlHelp.ExecuteNonQuery(insertSql, System.Data.CommandType.Text).ToString();
                else
                    insertSql = "0";
                //int updateCount = 0;
                //if (!string.IsNullOrEmpty(updateSql))
                //{
                //    foreach (string sql in updateSql.Split(';'))
                //    {
                //        if (!string.IsNullOrEmpty(sql))
                //            updateCount += _SqlHelp.ExecuteNonQuery(sql, System.Data.CommandType.Text);
                //    }
                //}
                if (!string.IsNullOrEmpty(updateSql))
                    updateSql = _SqlHelp.ExecuteNonQuery(updateSql, System.Data.CommandType.Text).ToString();
                else
                    updateSql = "0";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int ClearCustomers()
        {
            string sql = "delete from c2lp_customer;";
            try
            {
                return _SqlHelp.ExecuteNonQuery(sql, CommandType.Text);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 检查客户表是否存在CountyId字段，没有就添加
        /// </summary>
        public static void AddCustomersCountyId()
        {
            try
            {
                string sql = "select Count(*) from sqlite_master  where tbl_name='c2lp_customer' and sql like '%countyId%';";
                object obj = _SqlHelp.ExecuteScalar(sql, CommandType.Text);
                if (Convert.ToInt32(obj) == 0)
                {
                    sql = "alter table c2lp_customer add countyId int null";
                    _SqlHelp.ExecuteNonQuery(sql, CommandType.Text);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 获取所有客户
        /// </summary>
        /// <param name="role">1：发货单位；2：收货单位：其他：全部单位</param>
        /// <returns></returns>
        public static List<Customer> GetAllCustomer(string role)
        {
            List<Customer> list = new List<Customer>();
            try
            {
                string sql = "select Id,fullName,contactPerson,contactTel,contactAddress,provinceId,cityId,role,CountyId from c2lp_customer ";
                if (role != null && (role == "2" || role == "3"))
                    sql += "where role=" + role;

                DataSet ds = _SqlHelp.ExecuteDataSet(sql, System.Data.CommandType.Text);
                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Customer c = new Customer();
                        c.Id = Convert.ToInt32(row["Id"]);
                        c.ContactAddress = row["ContactAddress"].ToString();
                        c.ContactPerson = row["ContactPerson"].ToString();
                        c.ContactTel = row["ContactTel"].ToString();
                        c.FullName = row["FullName"].ToString();
                        c.ProvinceId = Convert.ToInt32(row["ProvinceId"]);
                        c.CityId = Convert.ToInt32(row["CityId"]);
                        c.Role = Convert.ToInt32(row["Role"]);
                        c.CountyId = Convert.ToInt32(row["CountyId"]);
                        list.Add(c);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }
    }
}
