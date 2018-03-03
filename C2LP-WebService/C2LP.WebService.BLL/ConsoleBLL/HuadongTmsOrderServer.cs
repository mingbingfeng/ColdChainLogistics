using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C2LP.WebService.Model;
using C2LP.WebService.Utility;
using C2LP.WebService.Model.MyEnum;
using MySql.Data.MySqlClient;

namespace C2LP.WebService.BLL.ConsoleBLL
{
    public class HuadongTmsOrderServer : BaseServer
    {
        /// <summary>
        /// 查询华东医药托运订单信息
        /// </summary>
        /// <returns></returns>
        public static List<Model_Huadong_Tms_Order> GethuadongTmsOrder(string pageIndexAndCount)
        {
            string sql = "select relationId,"+
                            "code,"+
                            "SRCEXPNO,"+
                            "ROADID,"+
                            "SHIPDETAILID,"+
                            "TOTALID,"+
                            "LEGCODE,"+
                            "SHIPMENTCODE,"+
                            "OPERATIONTYPE,"+
                            "DEMANDARRIVETIME,"+
                            "TOGTRANSNAME,"+
                            "RECEIVEMAN,"+
                            "RECEIVEPHONE,"+
                            "RECEIVEADDR from huadong_tms_order ";
            if (pageIndexAndCount!=null)
            {
                string page = pageIndexAndCount.Substring(0,pageIndexAndCount.LastIndexOf("."));
                string size = pageIndexAndCount.Substring(pageIndexAndCount.LastIndexOf(".")+1,pageIndexAndCount.Length-(pageIndexAndCount.LastIndexOf(".")+1));
                sql += " order by id desc limit " + ((Convert.ToInt32(page) - 1) * Convert.ToInt32(size)) + "," + size + ";";
            }
            else
            {
                sql += " ;";
            }
            List<Model_Huadong_Tms_Order> list = _SqlHelp.ExecuteObjects<Model_Huadong_Tms_Order>(sql);
            return list;
        }
        /// <summary>
        /// 查询华东信息总条数
        /// </summary>
        /// <returns></returns>
        public static int GethuadongTmsOrderCount()
        {
            string sql = "select count(*) from huadong_tms_order ;";
            return Convert.ToInt32(_SqlHelp.ExecuteScalar(sql));
            
        }

        /// <summary>
        /// 根据运输任务单号或是tms运单号查询华东信息
        /// </summary>
        /// <param name="SHIPDETAILID">运输任务单号</param>
        /// <param name="LEGCODE">tms运单号</param>
        /// <param name="pageIndexAndCount"></param>
        /// <returns></returns>
        public static List<Model_Huadong_Tms_Order > GetHuadongWaybillNumberQuerys(string SHIPDETAILID,string pageIndexAndCount)
        {
            string sql = string.Format("select relationId,code,SRCEXPNO,ROADID,SHIPDETAILID,TOTALID,LEGCODE,SHIPMENTCODE,OPERATIONTYPE,DEMANDARRIVETIME,TOGTRANSNAME,RECEIVEMAN,RECEIVEPHONE, RECEIVEADDR from huadong_tms_order ");
            if (SHIPDETAILID !=string.Empty )
            {
                sql += string.Format("where (SHIPDETAILID = '{0}' or LEGCODE = '{0}') ", SHIPDETAILID);
            }
            if (pageIndexAndCount != null)
            {
                string page = pageIndexAndCount.Substring(0, pageIndexAndCount.LastIndexOf("."));
                string size = pageIndexAndCount.Substring(pageIndexAndCount.LastIndexOf(".") + 1, pageIndexAndCount.Length - (pageIndexAndCount.LastIndexOf(".") + 1));
                sql += " order by id desc limit " + ((Convert.ToInt32(page) - 1) * Convert.ToInt32(size)) + "," + size + ";";
            }
            else
            {
                sql += " ;";
            }
            List<Model_Huadong_Tms_Order> list = _SqlHelp.ExecuteObjects<Model_Huadong_Tms_Order>(sql);
            return list;
        }

        public static int GethuadongWaybillNumberCounts(string SHIPDETAILID)
        {
            string sql =string.Format( "select count(*) from huadong_tms_order ");
            if (SHIPDETAILID!=string.Empty)
            {
                sql += string.Format(" where (SHIPDETAILID = '{0}' or LEGCODE = '{0}') ;",SHIPDETAILID);
            }
            else
            {
                sql += " ;";
            }
            return Convert.ToInt32(_SqlHelp.ExecuteScalar(sql));
        }
        /// <summary>
        /// 模糊查询第三方运单
        /// </summary>
        /// <param name="SHIPDETAILID">运输任务单号</param>
        /// <param name="pageIndexAndCount">tms运单号</param>
        /// <returns></returns>
        public static List<Model_Huadong_Tms_Order> GetHuadongWaybillVagueQuerys(string SHIPDETAILID, string pageIndexAndCount)
        {
            string sql = string.Format("select relationId,code,SRCEXPNO,ROADID,SHIPDETAILID,TOTALID,LEGCODE,SHIPMENTCODE,OPERATIONTYPE,DEMANDARRIVETIME,TOGTRANSNAME,RECEIVEMAN,RECEIVEPHONE, RECEIVEADDR from huadong_tms_order ");
            if (SHIPDETAILID != string.Empty)
            {
                sql += string.Format("where (SHIPDETAILID like '%{0}%' or LEGCODE like '%{0}%')", SHIPDETAILID);
            }
            if (pageIndexAndCount != null)
            {
                string page = pageIndexAndCount.Substring(0, pageIndexAndCount.LastIndexOf("."));
                string size = pageIndexAndCount.Substring(pageIndexAndCount.LastIndexOf(".") + 1, pageIndexAndCount.Length - (pageIndexAndCount.LastIndexOf(".") + 1));
                sql += " order by id desc limit " + ((Convert.ToInt32(page) - 1) * Convert.ToInt32(size)) + "," + size + ";";
            }
            else
            {
                sql += " ;";
            }
            List<Model_Huadong_Tms_Order> list = _SqlHelp.ExecuteObjects<Model_Huadong_Tms_Order>(sql);
            return list;
        }
        public static int GethuadongWaybillVagueCounts(string SHIPDETAILID)
        {
            string sql = string.Format("select count(*) from huadong_tms_order ");
            if (SHIPDETAILID != string.Empty)
            {
                sql += string.Format(" where (SHIPDETAILID like '%{0}%' or LEGCODE like '%{0}%') ;", SHIPDETAILID);
            }
            else
            {
                sql += " ;";
            }
            return Convert.ToInt32(_SqlHelp.ExecuteScalar(sql));
        }
    }
}
