using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using C2LP.PDA.Datas.Model;
using System.Data;

namespace C2LP.PDA.Datas.BLL
{
    public class RegionServer : BaseServer
    {
        /// <summary>
        /// 清空所有区域信息
        /// </summary>
        /// <returns></returns>
        public static int ClearRegions()
        {
            string sql = "delete from c2lp_Region;";
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
        /// 根据拉取到的区域信息，获取需要更新的区域ID
        /// </summary>
        /// <param name="updateIdList"></param>
        /// <returns></returns>
        public static List<int> GetUpdateRegionIdList(List<string> updateIdList)
        {
            List<int> idList = new List<int>();
            if (updateIdList.Count > 0)
            {
                try
                {
                    string sql = "select Id from c2lp_region where Id in (" + string.Join(",", updateIdList.ToArray()) + ")";
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
        /// 更新区域信息
        /// </summary>
        /// <param name="valueStr">sql value数据字符</param>
        /// <returns></returns>
        public static int UpdateRegions(string valueStr)
        {
            //string sql = "delete from c2lp_Region;";
            string sql = string.Empty;
            try
            {
                //_SqlHelp.ExecuteNonQuery(sql, System.Data.CommandType.Text);
                if (!string.IsNullOrEmpty(valueStr))
                {
                    sql += "insert into c2lp_Region (Id,`name`,parentId) values " + valueStr;
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
        /// 更新区域信息
        /// </summary>
        /// <param name="valueStr">sql value数据字符</param>
        /// <returns></returns>
        public static int UpdateRegions(ref string insertSql, ref string updateSql)
        {
            //string sql = "delete from c2lp_Region;";
            //string sql = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(insertSql))
                    insertSql = _SqlHelp.ExecuteNonQuery(insertSql, System.Data.CommandType.Text).ToString();
                else
                    insertSql = "0";

                if (!string.IsNullOrEmpty(updateSql))
                    updateSql = _SqlHelp.ExecuteNonQuery(updateSql, System.Data.CommandType.Text).ToString();
                else
                    updateSql = "0";

                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        /// <summary>
        /// 获取所有区域
        /// </summary>
        /// <returns></returns>
        public static List<MyRegion> GetAllRegion()
        {
            List<MyRegion> list = new List<MyRegion>();
            try
            {
                //查询所有城市
                string sql = "select id,`name`,parentId from c2lp_region ";
                DataSet ds = _SqlHelp.ExecuteDataSet(sql, System.Data.CommandType.Text);
                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        MyRegion r = new MyRegion();
                        r.Id = Convert.ToInt32(row["id"]);
                        r.Name = row["Name"].ToString();
                        r.ParentId = Convert.ToInt32(row["parentId"]);
                        list.Add(r);
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
