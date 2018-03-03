using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using C2LP.PDA.Datas.Model;
using System.Data;

namespace C2LP.PDA.Datas.BLL
{
    public class ConsignorServer : BaseServer
    {
        /// <summary>
        /// 清空供应商信息后添加新的供应商信息
        /// </summary>
        /// <param name="valueStr"></param>
        /// <returns></returns>
        public static int UpdateConsignor(string valueStr)
        {
            string sql = "delete from c2lp_consignor;";
            int insertCount = 0;
            try
            {
                //_SqlHelp.ExecuteNonQuery(sql, System.Data.CommandType.Text);
                if (!string.IsNullOrEmpty(valueStr))
                {
                    insertCount++;
                    sql += "insert into c2lp_consignor (consignorid,consignorName,linkType,linkRegex) values " + valueStr;
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
        /// 获取所有的供应商
        /// </summary>
        /// <returns></returns>
        public static List<Consignor> GetAllConsignor()
        {
            List<Consignor> list = new List<Consignor>();
            string sql = "select * from c2lp_consignor order by linkType";
            try
            {
                DataSet ds = _SqlHelp.ExecuteDataSet(sql, System.Data.CommandType.Text);
                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Consignor c = new Consignor();
                        c.ConsignorId = Convert.ToInt32(row["ConsignorId"]);
                        c.ConsignorName = row["ConsignorName"].ToString();
                        c.LinkType = row["LinkType"] is DBNull ? 0 : Convert.ToInt32(row["LinkType"]);
                        c.LinkRegex = row["LinkRegex"] is DBNull ? string.Empty : row["LinkRegex"].ToString();
                        list.Add(c);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("请重新同步:" + ex.Message);
            }
            return list;
        }
    }
}
