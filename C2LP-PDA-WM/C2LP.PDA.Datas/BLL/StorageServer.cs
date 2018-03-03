using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using C2LP.PDA.Datas.Model;
using System.Data;
using System.Collections;

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
            string sql = "delete from c2lp_storage;update sqlite_sequence set seq=0 where name='c2lp_storage';";
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
        /// 更新所有冷藏载体信息_Scan
        /// </summary>
        /// <param name="valueStr">sql value数据字符</param>
        /// <returns></returns>
        public static int UpdateStorageScan(string valueStr)
        {
            string sql = "delete from c2lp_storage_scan;";
            int insertCount = 0;
            try
            {
                //_SqlHelp.ExecuteNonQuery(sql, System.Data.CommandType.Text);
                if (!string.IsNullOrEmpty(valueStr))
                {
                    insertCount++;
                    sql += "insert into c2lp_storage_scan (storageId,storageName,storageType) values " + valueStr;
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
                    sql += " where storageName like '%" + storageName + "%' or storageName='" + storageName + "'";
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

        /// <summary>
        /// 获取所有冷藏载体信息_Scan
        /// </summary>
        /// <returns></returns>
        public static Hashtable GetAllStorageScan()
        {
            Hashtable list = new Hashtable();
            try
            {
                string sql = "select storageId,storageName,storageType from c2lp_storage_scan order by storageType";
                DataSet ds = _SqlHelp.ExecuteDataSet(sql, CommandType.Text);
                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        ColdStorage cs = new ColdStorage();
                        cs.Id = Convert.ToInt32(row["storageId"]);
                        cs.storageName = row["StorageName"].ToString();
                        cs.storageType = Convert.ToInt32(row["StorageType"]);
                        if (list.Contains(cs.Id))
                            list[cs.Id] = cs;
                        else
                            list.Add(cs.Id, cs);
                    }
                }
            }
            catch (Exception )
            {
            }
            return list;
        }
    }
}
