using C2LP.WebService.Model;
using C2LP.WebService.Utility;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace C2LP.WebService.BLL.PDABLL
{
    public class PDA_StorageServer:BaseServer
    {

        /// <summary>
        /// 根据设备ID获取所有关联的冷库载体信息
        /// </summary>
        /// <param name="pid">设备Id</param>
        /// <returns></returns>
        public static List<Model_ColdStorage> GetStoragesByPId(int pid) {
            List<Model_ColdStorage> list = new List<Model_ColdStorage>();
            try
            {
                string sql = "select c.id,case when sd.isdefault=1 then CONCAT(storageName,'[默认]') else storageName end as storageName,storageType,driver,driverTel,remark,createAt from coldstorage c left join storage_device sd on c.id = sd.storageid where sd.deviceid=?id and c.actived=0 order by sd.isdefault desc";
                MySqlParameter[] para = new MySqlParameter[1];
                para[0] = new MySqlParameter("id", pid);
                list = _SqlHelp.ExecuteObjects<Model_ColdStorage>(sql, para);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }

        /// <summary>
        /// 获取所有冷藏载体信息
        /// </summary>
        /// <returns></returns>
        public static List<Model_ColdStorage> GetStorageScan() {
            List<Model_ColdStorage> list = new List<Model_ColdStorage>();
            try
            {
                string sql = "select Id ,StorageName,StorageType from coldstorage where actived=0";
                list = _SqlHelp.ExecuteObjects<Model_ColdStorage>(sql);
            }
            catch (Exception ex)
            {
                throw ex; 
            }
            return list;
        }
    }
}
