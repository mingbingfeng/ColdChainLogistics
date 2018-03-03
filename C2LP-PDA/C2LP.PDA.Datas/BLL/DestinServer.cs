using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace C2LP.PDA.Datas.BLL
{
    public class DestinServer : BaseServer
    {

        /// <summary>
        /// 更新目的地信息
        /// </summary>
        /// <param name="destins">目的地信息数组</param>
        /// <returns></returns>
        public static int UpdateDestins(string[] destins)
        {
            int insertCount = 0;
            string sql = "delete from c2lp_Destins;";
            try
            {
                //_SqlHelp.ExecuteNonQuery(sql, System.Data.CommandType.Text);
                if (destins != null && destins.Count() > 0)
                {
                    sql += "insert into c2lp_Destins (address) values";
                    foreach (string address in destins)
                    {
                        insertCount++;
                        sql += "('" + address + "'),";
                    }
                    sql = sql.Substring(0, sql.Length - 1);
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
        /// 查询所有目的地信息
        /// </summary>
        /// <returns></returns>
        public static List<string> GetDestins()
        {
            List<string> list = new List<string>();
            try
            {
                string sql = "select address from c2lp_Destins";
                DataSet ds = _SqlHelp.ExecuteDataSet(sql, System.Data.CommandType.Text);
                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        string address = row["address"].ToString();
                        list.Add(address);
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
