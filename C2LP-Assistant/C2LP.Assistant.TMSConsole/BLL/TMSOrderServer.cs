using C2LP.Assistant.TMSConsole.Logink;
using C2LP.Assistant.TMSConsole.Model.TMSOrder.MESSAGEDETAIL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace C2LP.Assistant.TMSConsole.BLL
{
    class TMSOrderServer
    {

        public static bool AddTMSOrders(XML orders)
        {
            Utility.AddLogText("开始进入TMS运单保存逻辑");
            string dtNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            List<string> sqlList = new List<string>();
            int result = 0;
            try
            {
                int customerId = GetCustomerIdBySenderCode("", 1);
                foreach (var order in orders.CONTENTLIST)
                {
                    string sqlOrder = string.Format("INSERT INTO `coldchain_logistics_db`.`huadong_tms_order` (`code`, `SRCEXPNO`, `ROADID`, `SHIPDETAILID`, `TOTALID`, `LEGCODE`, `SHIPMENTCODE`, `CONSIGNORCODE`, `CONSIGNORNAME`, `DEPTNO`, `DEPTNAME`, `CUSTOMERCODE`, `CUSTOMERNAME`, `AREAHOUSE`, `SALESMAN`, `TRANSMODEID`, `ERPTRANSMODENAME`, `OPERATIONTYPE`, `DEMANDARRIVETIME`, `TRANSPORTTYPE`, `ORDERINSTANCY`, `TRANSPORTCATEGORY`, `ROUTENO`, `TRANSDEADLINE`, `FROMGTRANSID`, `FROMGTRANSNAME`, `TOGTRANSID`, `TOGTRANSNAME`, `RECEIVEADDR`, `RECEIVEMAN`, `RECEIVEPHONE`, `CREDATE`, `INOUTFLAG`, `WMSROUTEWAVENO`, `PRINTTYPE`, `TOTALQUNTITY`, `WHOLEQUNTITY`, `PARTQUNTITY`, `JFQUNTITY`, `DESCRIPTION`, `EXTCOL0`, `EXTCOL1`, `EXTCOL2`, `EXTCOL3`, `EXTCOL4`, `EXTCOL5`, `EXTCOL6`, `EXTCOL7`, `EXTCOL8`, `EXTCOL9`, `EXTCOL10`, `EXTCOL11`, `EXTCOL12`, `EXTCOL13`, `EXTCOL14`, `EXTCOL15`, `EXTCOL16`, `EXTCOL17`, `EXTCOL18`, `EXTCOL19`,`SecretKey`,`CreateTime`,`customerId`) VALUES ('{0}', '{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}', '{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}', '{21}','{22}','{23}','{24}','{25}','{26}','{27}','{28}','{29}','{30}', '{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}','{39}','{40}', '{41}','{42}','{43}','{44}','{45}','{46}','{47}','{48}','{49}','{50}', '{51}','{52}','{53}','{54}','{55}','{56}','{57}','{58}','{59}','{60}','{61}','{62}');", order.HEADER.CODE, order.HEADER.SRCEXPNO, order.HEADER.ROADID, order.HEADER.SHIPDETAILID, order.HEADER.TOTALID, order.HEADER.LEGCODE, order.HEADER.SHIPMENTCODE, order.HEADER.CONSIGNORCODE, order.HEADER.CONSIGNORNAME, order.HEADER.DEPTNO, order.HEADER.DEPTNAME, order.HEADER.CUSTOMERCODE, order.HEADER.CUSTOMERNAME, order.HEADER.AREAHOUSE, order.HEADER.SALESMAN, order.HEADER.TRANSMODEID, order.HEADER.ERPTRANSMODENAME, order.HEADER.OPERATIONTYPE, order.HEADER.DEMANDARRIVETIME, order.HEADER.TRANSPORTTYPE, order.HEADER.ORDERINSTANCY, order.HEADER.TRANSPORTCATEGORY, order.HEADER.ROUTENO, order.HEADER.TRANSDEADLINE, order.HEADER.FROMGTRANSID, order.HEADER.FROMGTRANSNAME, order.HEADER.TOGTRANSID, order.HEADER.TOGTRANSNAME, order.HEADER.RECEIVEADDR, order.HEADER.RECEIVEMAN, order.HEADER.RECEIVEPHONE, order.HEADER.CREDATE, order.HEADER.INOUTFLAG, order.HEADER.WMSROUTEWAVENO, order.HEADER.PRINTTYPE, order.HEADER.TOTALQUNTITY, order.HEADER.WHOLEQUNTITY, order.HEADER.PARTQUNTITY, order.HEADER.JFQUNTITY, order.HEADER.DESCRIPTION, order.HEADER.EXTCOL0, order.HEADER.EXTCOL1, order.HEADER.EXTCOL2, order.HEADER.EXTCOL3, order.HEADER.EXTCOL4, order.HEADER.EXTCOL5, order.HEADER.EXTCOL6, order.HEADER.EXTCOL7, order.HEADER.EXTCOL8, order.HEADER.EXTCOL9, order.HEADER.EXTCOL10, order.HEADER.EXTCOL11, "供应链", order.HEADER.EXTCOL13, order.HEADER.EXTCOL14, order.HEADER.EXTCOL15, order.HEADER.EXTCOL16, order.HEADER.EXTCOL17, order.HEADER.EXTCOL18, order.HEADER.EXTCOL19, Utility._SecretKey, dtNow, customerId);
                    Utility.AddLogText(string.Format("添加第三方运单：{0}", sqlOrder));
                    sqlList.Add(sqlOrder);
                    foreach (var detail in order.BODY.DETAILLIST)
                    {
                        string sqlDetail = string.Format("INSERT INTO `coldchain_logistics_db`.`huadong_tms_orderDetail` (`TMSORDERDTLID`, `BMSORDERDTLID`, `SALESNO`, `BACODE`, `GOODSOWNID`, `GOODSCODE`, `GOODSNAME`, `GOODSTYPE`, `DRUGTYPE`, `LOTNO`, `PRODDATE`, `APPROVENO`, `TRADEPACK`, `PACKAGEUNIT`, `PACKAGEQTY`, `QUNTITY`, `FACTQUNTITY`, `WHOLEQUNTITY`, `SINGLEQUNTITY`, `WEIGHT`, `VOLUME`, `VALIDDATE`, `PINBACK`, `WORKREQUIRE`, `EXTCOL0`) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}','{24}');", detail.TMSORDERDTLID, detail.BMSORDERDTLID, detail.SALESNO, detail.BACODE, detail.GOODSOWNID, detail.GOODSCODE, detail.GOODSNAME, detail.GOODSTYPE, detail.DRUGTYPE, detail.LOTNO, detail.PRODDATE, detail.APPROVENO, detail.TRADEPACK, detail.PACKAGEUNIT, detail.PACKAGEQTY, detail.QUNTITY, detail.FACTQUNTITY, detail.WHOLEQUNTITY, detail.SINGLEQUNTITY, detail.WEIGHT, detail.VOLUME, detail.VALIDDATE, detail.PINBACK, detail.WORKREQUIRE, detail.EXTCOL0);
                        sqlList.Add(sqlDetail);
                    }
                }
                #region xxx
                //List<string> tmsNumbers = orders.CONTENTLIST.Select(l => l.HEADER.SHIPDETAILID).ToList();
                //tmsNumbers.AddRange(orders.CONTENTLIST.Select(l => l.HEADER.LEGCODE));
                //tmsNumbers = tmsNumbers.Distinct().ToList();
                ////查询关系表，查找是否存在已经扫描过的单，如果存在则更新订单表中的信息
                //string sql = string.Format("select relationId,number from huadong_tmsorder_waybillbase  where relationId in ('{0}');", string.Join("','", tmsNumbers));
                //DataSet ds = DbHelperMySQL.Query(sql);
                //Utility.AddLogText(string.Format("查询关系表:{0},执行结果:{1}",sql,ds.Tables.Count));
                //if (ds != null && ds.Tables.Count > 0)
                //{
                //    Utility.AddLogText("开始更新waybill_base表中的信息");
                //    foreach (DataRow row in ds.Tables[0].Rows)
                //    {
                //        string relationId = row["relationId"].ToString();
                //        string number = row["number"].ToString();
                //        Utility.AddLogText(string.Format("更新waybill_base表,{0},{1}", relationId, number));
                //        CONTENT order = orders.CONTENTLIST.Find(l => l.HEADER.LEGCODE == relationId || l.HEADER.SHIPDETAILID == relationId);
                //        string huadongbase=string.Format("update waybill_base set senderPerson = '{0}',senderTel='{1}',senderAddress='{2}',receiverOrg='{3}',receiverPerson='{4}',receiverTel='{5}',receiverAddress='{6}' where number = '{7}';", order.HEADER.EXTCOL12, "", order.HEADER.EXTCOL11, order.HEADER.CUSTOMERNAME, order.HEADER.RECEIVEMAN, order.HEADER.RECEIVEPHONE, order.HEADER.RECEIVEADDR, number);
                //        sqlList.Add(huadongbase);
                //        Utility.AddLogText(huadongbase);
                //        //string hudaongkey =string.Format("update huadong_tms_order set SecretKey = '{0}'  where relationId = '{1}' and customerId={2} ;", Utility._SecretKey, relationId, customerId);
                //        string hudaongkey = string.Format("update  huadong_tms_order set  `code`='{0}', `SRCEXPNO`='{1}', `ROADID`='{2}', `TOTALID`='{3}', `SHIPMENTCODE`= '{4}', `CONSIGNORCODE`= '{5}', " +
                //            "`CONSIGNORNAME`= '{6}', `DEPTNO`= '{7}', `DEPTNAME`= '{8}', `CUSTOMERCODE`= '{9}', `CUSTOMERNAME`= '{10}', `AREAHOUSE`= '{11}'," +
                //            " `SALESMAN`= '{12}', `TRANSMODEID`= '{13}',`ERPTRANSMODENAME`= '{14}', `OPERATIONTYPE`= '{15}', `DEMANDARRIVETIME`= '{16}', `TRANSPORTTYPE`= '{17}',"+
                //            " `ORDERINSTANCY`= '{18}', `TRANSPORTCATEGORY`= '{19}',`ROUTENO`= '{20}', `TRANSDEADLINE`= '{21}', `FROMGTRANSID`= '{22}', `FROMGTRANSNAME`= '{23}',"+
                //            " `TOGTRANSID`= '{24}', `TOGTRANSNAME`= '{25}', `RECEIVEADDR`= '{26}',`RECEIVEMAN`= '{27}', `RECEIVEPHONE`= '{28}', `CREDATE`= '{29}',"+
                //            " `INOUTFLAG`= '{30}',`WMSROUTEWAVENO`= '{31}', `PRINTTYPE`= '{32}', `TOTALQUNTITY`= '{33}', `WHOLEQUNTITY`= '{34}',`PARTQUNTITY`= '{35}', "+
                //            "`JFQUNTITY`= '{36}', `DESCRIPTION`= '{37}', `EXTCOL0`= '{38}', `EXTCOL1`= '{39}', `EXTCOL2`= '{40}', `EXTCOL3`= '{41}', "+
                //            "`EXTCOL4`= '{42}', `EXTCOL5`= '{43}', `EXTCOL6`= '{44}', `EXTCOL7`= '{45}', `EXTCOL8`= '{46}', `EXTCOL9`= '{47}',"+
                //            " `EXTCOL10`= '{48}', `EXTCOL11`= '{49}', `EXTCOL12`= '{50}', `EXTCOL13`= '{51}',`EXTCOL14`= '{52}', `EXTCOL15`= '{53}',"+
                //            " `EXTCOL16`= '{54}', `EXTCOL17`= '{55}', `EXTCOL18`= '{56}', `EXTCOL19`= '{57}',`SecretKey`= '{58}'  where relationId = '{59}' and customerId = {60}  ",
                //                        order.HEADER.CODE, order.HEADER.SRCEXPNO, order.HEADER.ROADID, order.HEADER.TOTALID, order.HEADER.SHIPMENTCODE, order.HEADER.CONSIGNORCODE,
                //                        order.HEADER.CONSIGNORNAME, order.HEADER.DEPTNO, order.HEADER.DEPTNAME, order.HEADER.CUSTOMERCODE, order.HEADER.CUSTOMERNAME, order.HEADER.AREAHOUSE,
                //                        order.HEADER.SALESMAN, order.HEADER.TRANSMODEID, order.HEADER.ERPTRANSMODENAME, order.HEADER.OPERATIONTYPE, order.HEADER.DEMANDARRIVETIME, order.HEADER.TRANSPORTTYPE,
                //                        order.HEADER.ORDERINSTANCY, order.HEADER.TRANSPORTCATEGORY, order.HEADER.ROUTENO, order.HEADER.TRANSDEADLINE, order.HEADER.FROMGTRANSID, order.HEADER.FROMGTRANSNAME,
                //                        order.HEADER.TOGTRANSID, order.HEADER.TOGTRANSNAME, order.HEADER.RECEIVEADDR, order.HEADER.RECEIVEMAN, order.HEADER.RECEIVEPHONE, order.HEADER.CREDATE,
                //                        order.HEADER.INOUTFLAG, order.HEADER.WMSROUTEWAVENO, order.HEADER.PRINTTYPE, order.HEADER.TOTALQUNTITY, order.HEADER.WHOLEQUNTITY, order.HEADER.PARTQUNTITY,
                //                        order.HEADER.JFQUNTITY, order.HEADER.DESCRIPTION, order.HEADER.EXTCOL0, order.HEADER.EXTCOL1, order.HEADER.EXTCOL2, order.HEADER.EXTCOL3,
                //                        order.HEADER.EXTCOL4, order.HEADER.EXTCOL5, order.HEADER.EXTCOL6, order.HEADER.EXTCOL7, order.HEADER.EXTCOL8, order.HEADER.EXTCOL9,
                //                        order.HEADER.EXTCOL10, order.HEADER.EXTCOL11, order.HEADER.EXTCOL12, order.HEADER.EXTCOL13, order.HEADER.EXTCOL14, order.HEADER.EXTCOL15,
                //                        order.HEADER.EXTCOL16, order.HEADER.EXTCOL17, order.HEADER.EXTCOL18, order.HEADER.EXTCOL19, Utility._SecretKey, relationId, customerId);
                //        sqlList.Add(hudaongkey);
                //        Utility.AddLogText(hudaongkey);
                //    }
                //}
                #endregion
                result = DbHelperMySQL.ExecuteSqlTran(sqlList);
                Utility.AddLogText("保存TMS运单结果：" + result);
                return result > 0;
            }
            catch (Exception ex)
            {
                Utility.AddLogText(string.Format("AddTMSOrders,保存TMS运单出错：{0}", ex.Message));
                throw new Exception("保存TMS运单出错：" + ex.Message);
            }
            finally
            {
                HandleScanLaterOrder();
            }
        }

        /// <summary>
        /// 处理延迟发单的第三方运单
        /// </summary>
        public static void HandleScanLaterOrder(bool showLog =false)
        {
            try
            {
                string sql = "select relationId,customerId from huadong_tms_order where DATE_SUB(CURDATE(), INTERVAL 30 DAY)<createtime and secretkey = '未知'";
                Utility.AddLogText("查询延迟发单的第三方运单:" + sql, showLog);
                DataSet ds = DbHelperMySQL.Query(sql);
                List<string> sqlList = new List<string>();
                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (DataRow item in ds.Tables[0].Rows)
                    {
                        string relationId = item["relationId"].ToString();
                        string customerId = item["customerId"].ToString();
                        sql = string.Format("update huadong_tms_order set relationId='{0}' where (shipdetailid='{0}' or legcode='{0}') and secretkey<>'未知';", relationId);
                        sqlList.Add(sql);
                        Utility.AddLogText(sql, showLog);
                        if (customerId == "669")
                        {
                            //大华东
                            sql = string.Format("update waybill_base b inner join (select * from huadong_tms_order where (shipdetailid='{0}' or legcode='{0}') and SecretKey<>'未知' limit 1) o set b.senderPerson = '供应链' ,b.senderAddress=o.EXTCOL11,b.receiverOrg=o.CUSTOMERNAME,b.receiverPerson=o.RECEIVEMAN,b.receiverTel=o.RECEIVEPHONE,b.receiverAddress=o.RECEIVEADDR where number like '%{0}%';", relationId);
                            sqlList.Add(sql);
                            Utility.AddLogText(sql, showLog);
                        }
                        else if (customerId == "2")
                        {
                            //华东医药
                            sql = string.Format("update waybill_base b inner join (select * from huadong_tms_order where (shipdetailid='{0}' or legcode='{0}') and SecretKey<>'未知' limit 1) o set b.senderPerson = '配送中心',b.senderTel=o.senderTel ,b.senderAddress=o.senderAddress,b.receiverOrg=o.receiverOrg,b.receiverTel=o.receiverTel,b.receiverPerson=o.receiverPerson,b.receiverAddress=o.receiverAddress where number like '%{0}%';", relationId);
                            sqlList.Add(sql);
                            Utility.AddLogText(sql, showLog);
                        }
                        sql = string.Format("update huadong_tms_order a inner join(select * from huadong_tms_order where (shipdetailid='{0}' or legcode='{0}') and secretkey<>'未知' limit 1)  b set a.secretkey = CONCAT( b.secretkey,'_early') where a.relationid = '{0}' and a.secretkey = '未知';", relationId);
                        sqlList.Add(sql);
                        Utility.AddLogText(sql);
                    }
                    int count = DbHelperMySQL.ExecuteSqlTran(sqlList);
                    Utility.AddLogText("成功处理" + count + "条.", showLog);
                    Utility.AddLogText("成功处理[" + ds.Tables[0].Rows.Count + "][" + count + "]条后派单的运单.", showLog);
                    Console.WriteLine("成功处理[" + ds.Tables[0].Rows.Count + "][" + count + "]条后派单的运单.", showLog);
                }
                else
                    Utility.AddLogText("没有延迟发单的第三方运单", showLog);
            }
            catch (Exception ex)
            {
                Utility.AddLogText("处理出错：" + ex.Message, showLog);
            }
        }

        /// <summary>
        /// 获取所有通过运管平台交换数据的上游发货单位的物流交换代码
        /// </summary>
        /// <returns></returns>
        public static List<string> GetAllLoginkSenderCode(string customerId = null)
        {
            List<string> list = new List<string>();
            try
            {
                string sql = "select interchangeCode from customer_transport where linkType=2";
                if (customerId != null)
                    sql += " and customerId = " + customerId;
                DataSet ds = DbHelperMySQL.Query(sql);
                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        list.Add(row["interchangeCode"].ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("查询第三方供应商出错[" + ex.Message + "]");
            }
            return list;
        }

        public static int GetCustomerIdBySenderCode(string senderCode, int linkType)
        {
            int customerId = 0;
            try
            {
                string sql = "select customerId from customer_transport where interchangeCode = " + senderCode + "";
                if (linkType != 2)
                    sql = "select customerId from customer_transport where linkType=" + linkType;
                object obj = DbHelperMySQL.GetSingle(sql);
                if (obj != null)
                    int.TryParse(obj.ToString(), out customerId);
            }
            catch (Exception ex)
            {
                throw new Exception("查询[SenderCode=" + senderCode + "]的上游客户ID出错:" + ex.Message);
            }
            return customerId;
        }

        /// <summary>
        /// 插入从运管平台拉取的订单
        /// </summary>
        /// <param name="orders"></param>
        /// <param name="senderCode"></param>
        public static void AddTMSOrders_Logink(List<M_TMSOrder> orders, string senderCode)
        {
            string dtNow = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            try
            {
                int customerId = GetCustomerIdBySenderCode(senderCode, 2);

                List<string> sqlList = new List<string>();
                foreach (M_TMSOrder item in orders)
                {
                    item.senderPerson = "配送中心";
                    sqlList.Add(string.Format("INSERT INTO `coldchain_logistics_db`.`huadong_tms_order` (`SHIPDETAILID`,`LEGCODE`, `CREDATE`, `senderOrg`, `senderPerson`, `senderTel`, `senderAddress`, `receiverOrg`, `receiverPerson`, `receiverTel`, `receiverAddress`, `SecretKey`, `CreateTime`, `customerId`,`JFQUNTITY`) VALUES ('{0}', '{0}','{12}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', '{7}', '{8}', '{9}', '{10}', '{11}','{13}');", item.orderNo, item.senderOrg, item.senderPerson, item.senderTel, item.senderAddress, item.receiverOrg, item.receiverPerson, item.receiverTel, item.receiverAddress, senderCode, dtNow, customerId, item.beginAt.ToString("yyyy-MM-dd HH:mm:ss"),item.billingCount));
                }
                IEnumerable<string> tmsNumbers = orders.Select(l => l.orderNo);
                //查询关系表，查找是否存在已经扫描过的单，如果存在则更新订单表中的信息
                string sql = string.Format("select relationId,number from huadong_tmsorder_waybillbase where relationId in ('{0}');", string.Join("','", tmsNumbers));
                DataSet ds = DbHelperMySQL.Query(sql);
                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        string relationId = row["relationId"].ToString();
                        string number = row["number"].ToString();
                        M_TMSOrder order = orders.Find(l => l.orderNo == relationId);
                        order.senderPerson = "配送中心";
                        sqlList.Add(string.Format("update waybill_base set senderPerson = '{0}',senderTel='{1}',senderAddress='{2}',receiverOrg='{3}',receiverPerson='{4}',receiverTel='{5}',receiverAddress='{6}' where number = '{7}';", order.senderPerson, order.senderTel, order.senderAddress, order.receiverOrg, order.receiverPerson, order.receiverTel, order.receiverAddress, number));
                    }
                }
                DbHelperMySQL.ExecuteSqlTran(sqlList);
            }
            catch (Exception ex)
            {
                throw new Exception("保存TMS运单出错：" + ex.Message);
            }
        }
    }
}
