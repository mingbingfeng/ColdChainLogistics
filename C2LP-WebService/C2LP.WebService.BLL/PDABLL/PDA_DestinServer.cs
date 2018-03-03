using C2LP.WebService.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace C2LP.WebService.BLL.PDABLL
{
    public class PDA_DestinServer:BaseServer
    {
        /// <summary>
        /// 根据设备ID获取其关联的所有目的地
        /// </summary>
        /// <param name="pId">设备ID</param>
        /// <returns></returns>
        public static List<string> GetDistins(int pId) {
            List<string> list = new List<string>();
            try
            {
                string sql = "select address from device_destination where deviceId=?id";
                MySqlParameter[] para = new MySqlParameter[1];
                para[0] = new MySqlParameter("id", pId);
                List< Model_Destination> dList  = _SqlHelp.ExecuteObjects<Model_Destination>(sql, para);
                list = dList.Select(l => l.Address).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }
    }
}
