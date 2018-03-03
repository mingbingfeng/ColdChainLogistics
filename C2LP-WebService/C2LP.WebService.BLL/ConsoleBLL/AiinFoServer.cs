using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C2LP.WebService.Utility;
using C2LP.WebService.Model;
using MySql.Data.MySqlClient;

namespace C2LP.WebService.BLL.ConsoleBLL
{
    public class AiinFoServer:BaseServer
    {
        /// <summary>
        /// 根据冷库/车载获取其所有的AI信息
        /// </summary>
        /// <param name="storageId"></param>
        /// <param name="pageIndexAndCount"></param>
        /// <returns></returns>
        public static List<Model_AiInfo> GetAiInfoByStorageIds(int storageId,string pageIndexAndCount)
        {
            //string sql = "";
            //string page = "";
            //string size = "";

            //if (pageIndexAndCount!=null)
            //    sql = "select *,pointName as PpointName from aiinfo where storageId=?storageId ";
            //else
            string sql = "select *,pointName as PpointName from aiinfo where storageId=?storageId ";

            if (pageIndexAndCount != null)
            {
                string page = pageIndexAndCount.Substring(0, pageIndexAndCount.LastIndexOf("."));
                string size = pageIndexAndCount.Substring(pageIndexAndCount.LastIndexOf(".") + 1, pageIndexAndCount.Length - (pageIndexAndCount.LastIndexOf(".") + 1));
                sql += " limit " + ((Convert.ToInt32(page) - 1) * Convert.ToInt32(size)) + "," + size + ";";
            }
            else
                sql += " and (pointType=1 or pointType=2);";
            
            MySqlParameter[] para = new MySqlParameter[1];
            para[0] = new MySqlParameter("storageId",storageId);

            List<Model_AiInfo> list = _SqlHelp.ExecuteObjects<Model_AiInfo>(sql, para);
            return list;
        }
        /// <summary>
        /// 编辑AI信息
        /// </summary>
        /// <param name="aiInfo"></param>
        /// <param name="IsDeleteAi"></param>
        /// <returns></returns>
        public static Model_AiInfo EditAiInfos(Model_AiInfo aiInfo, bool IsDeleteAi)
        {
            string sql = "";
            if (aiInfo.PointId==0 && IsDeleteAi==false)
            
                sql = "insert into aiinfo(storageId,pointName,pointType,actived) values(?storageId,?pointName,?pointType,?actived);";
            
            else if (aiInfo.PointId != 0 && IsDeleteAi == true)
            
                sql = "update aiinfo set storageId=?storageId,pointName=?pointName,pointType=?pointType,actived=?actived where pointId=?pointId ;";
            

            MySqlParameter[] para = new MySqlParameter[5];
            para[0] = new MySqlParameter("storageId",aiInfo.StorageId);
            para[1] = new MySqlParameter("pointName",aiInfo.PpointName);
            para[2] = new MySqlParameter("pointType",aiInfo.PointType);
            para[3] = new MySqlParameter("actived",aiInfo.Actived);
            para[4] = new MySqlParameter("pointId",aiInfo.PointId);
            int result = 0;
            if(aiInfo.PointId==0)
                result= _SqlHelp.ExecuteNonQuery(sql,para);
            else
                result = _SqlHelp.ExecuteNonQuery(sql, para);
            if (result != 1)
                throw new Exception("操作失败");

            return aiInfo;
        }
    }
}
