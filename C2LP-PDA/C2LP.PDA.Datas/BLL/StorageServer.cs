using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using C2LP.PDA.Datas.Model;
using System.Data;

namespace C2LP.PDA.Datas.BLL
{
    public class StorageServer : BaseServer
    {
        /// <summary>
        /// 更新冷藏载体信息
        /// </summary>
        /// <param name="valueStr">sql value数据字符</param>
        /// <returns></returns>
        public static int UpdateStorages(string valueStr)
        {
            string sql = "delete from c2lp_storage;";
            int insertCount = 0;
            try
            {
                //_SqlHelp.ExecuteNonQuery(sql, System.Data.CommandType.Text);
                if (!string.IsNullOrEmpty(valueStr))
                {
                    insertCount++;
                    sql += "insert into c2lp_storage (Id,storageName,storageType,driver,driverTel,remark,createAt) values " + valueStr;
                }
                int result = _SqlHelp.ExecuteNonQuery(sql, System.Data.CommandType.Text);
                if (insertCount > 0)
                    return result;
                else
                    return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取冷藏载体列表
        /// </summary>
        /// <returns></returns>
        public static List<ColdStorage> GetStorageList(string storageName)
        {
            List<ColdStorage> list = new List<ColdStorage>();
            try
            {
                string sql = "select Id,storageName,storageType,driver,driverTel,remark,createAt from c2lp_storage";
                if (!string.IsNullOrEmpty(storageName))
                    sql += " where storageName like '%" + storageName + "[%' or storageName='" + storageName + "'";
                DataSet ds = _SqlHelp.ExecuteDataSet(sql, System.Data.CommandType.Text);
                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        ColdStorage cs = new ColdStorage();
                        cs.Id = Convert.ToInt32(row["Id"]);
                        cs.storageName = row["StorageName"].ToString();
                        cs.storageType = Convert.ToInt32(row["StorageType"]);
                        cs.driver = row["Driver"] is DBNull ? "" : row["Driver"].ToString();
                        cs.driverTel = row["driverTel"] is DBNull ? "" : row["driverTel"].ToString();
                        cs.remark = row["remark"] is DBNull ? "" : row["remark"].ToString();
                        cs.createAt = row["createAt"].ToString();
                        list.Add(cs);
                    }
                }
            }
            catch
            {
            }
            return list;
        }
    }
}
