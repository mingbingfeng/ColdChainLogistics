using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C2LP.ColdStorageDataHubClient.DBHelper.Model;

namespace C2LP.ColdStorageDataHubClient.DBHelper.BLL
{
    public class RefInfoServer:BaseServer
    {
        /// <summary>
        /// 获取所有的仓库信息
        /// </summary>
        /// <returns></returns>
        public static List<TbccRefInfo> GetAllRefInfo() {
            List<TbccRefInfo> list = new List<TbccRefInfo>();
            string sql = "select * from tbccRefInfo order by NetId, refid";
            try
            {
                list = _DBHelper.ReadEntityList<TbccRefInfo>(sql);
            }
            catch
            {
                sql = "select * from tbccRefInfo order by refid";
                try
                {
                    list = _DBHelper.ReadEntityList<TbccRefInfo>(sql);
                }
                catch 
                {
                    
                }
            }
            return list;
        }
    }
}
