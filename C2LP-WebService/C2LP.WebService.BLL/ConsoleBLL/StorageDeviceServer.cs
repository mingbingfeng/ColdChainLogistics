using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C2LP.WebService.Model;
using MySql.Data.MySqlClient;

namespace C2LP.WebService.BLL.ConsoleBLL
{
    public class StorageDeviceServer : BaseServer
    {
        public static Model_Storage_Device GetDefaultDevices(Model_Storage_Device storageDevice)
        {
            string sql = "update storage_device set isDefault=0 where deviceId=?deviceId ";
            MySqlParameter[] para = new MySqlParameter[1];
            para[0] = new MySqlParameter("deviceId", storageDevice.deviceId);
            int stode = _SqlHelp.ExecuteNonQuery(sql, para);
            if (stode < 0)
            {
                throw new Exception("操作失败");
            }
            return storageDevice;
        }
    }
}
