using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C2LP.WebService.Model;
using C2LP.WebService.Utility;
using MySql.Data.MySqlClient;
using C2LP.WebService.Model.MyEnum;

namespace C2LP.WebService.BLL.PDABLL
{
    public class PDA_HuadongTmsOrderServer : BaseServer
    {

        /// <summary>
        /// 创建第三方运单
        /// </summary>
        /// <param name="orderInfo"></param>
        /// <returns>0:处理失败; 1:处理成功; 2:运单已存在</returns>
        public static int UploadThirdPartyOrder(Model_ThirdPartOrder orderInfo, int customerId = 0)
        {
            int result = 0;
            try
            {
                LogServer.AddLogText(string.Format("进入创建第三方运单逻辑{0},操作时间{1}", orderInfo.RelationId, orderInfo.OperateAt), orderInfo.RelationId);
                if (orderInfo == null)
                    result = 1;
                string sql = string.Empty;
                Model_ThirdCustomer customerInfo = new Model_ThirdCustomer();
                //检查是否已经创建过第三方运单的关联运单
                Model_Waybill_Base IsExist = CheckThirdPartyNumberExist(orderInfo.RelationId);
                LogServer.AddLogText(string.Format("CheckThirdPartyNumberExist:{0}", IsExist != null ? 1 : 0), orderInfo.RelationId);
                if (IsExist == null)
                {
                    #region 获取自动生成的运单号
                    //sql = "select concat('99', 1000000000 + Count(*) + 1) from waybill_base where company = 1";
                    //object obj = _SqlHelp.ExecuteScalar(sql);
                    string number = string.Empty;
                    string nHead = "981000000000";
                    if (customerId != 2)
                        nHead = "991000000000";
                    int jLen = orderInfo.RelationId.Length;
                    //if (orderInfo.RelationId.Length >= 12)
                    //    jLen = 10;
                    //number = nHead.Substring(0, 12 - orderInfo.RelationId.Length) + orderInfo.RelationId;
                    if (orderInfo.RelationId.Length > 9)
                    {
                        string str1 = orderInfo.RelationId.ToString().Substring(orderInfo.RelationId.Length - 9);
                        number = nHead.Substring(0, 12 - str1.Length) + str1;
                    }
                    else
                        number = nHead.Substring(0, 12 - orderInfo.RelationId.Length) + orderInfo.RelationId;
                    LogServer.AddLogText("拼接后的运单号：" + number + "", orderInfo.RelationId);
                    //if (obj != null)
                    //{
                    //    number = obj.ToString();
                    //    if (obj.ToString() == "System.Byte[]")
                    //        number = Encoding.Default.GetString(obj as byte[]);
                    //}
                    #endregion
                    #region 旧版通过配置文件获取第三方供应商名称，新版查询根据CustomerID查询
                    int Huadong_Id = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Huadong_Id"]);
                    string Huadong_Name = System.Configuration.ConfigurationManager.AppSettings["Huadong_Name"];
                    if (customerId != 0)
                    {
                        Huadong_Id = customerId;
                        customerInfo = PDA_CustomerServer.GetThirdCustomers(customerId).First();
                        Huadong_Name = customerInfo.CustomerName;
                    }
                    #endregion
                    List<string> sqlList = new List<string>();
                    //更新关系ID
                    sql = string.Format("update huadong_tms_order set relationId='{0}' where (SHIPDETAILID = '{0}' or LEGCODE = '{0}' )", orderInfo.RelationId);
                    sql += customerId == 0 ? string.Empty : (" and customerId = " + customerId);
                    int count = _SqlHelp.ExecuteNonQuery(sql);
                    LogServer.AddLogText(string.Format("更新第三方运单relationId：{0},结果：{1}", sql, count), orderInfo.RelationId);
                    if (count == 0)
                    {
                        LogServer.AddLogText(string.Format("第三方运单不存在时，先插入一条空信息的运单，待第三方运单同步回来时更新运单信息", orderInfo.RelationId, orderInfo.OperateAt, customerId), orderInfo.RelationId);
                        //第三方运单不存在时，先插入一条空信息的运单，待第三方运单同步回来时更新运单信息
                        sqlList.Add(string.Format("INSERT INTO `coldchain_logistics_db`.`huadong_tms_order` ( `relationId`, `code`, `SRCEXPNO`, `ROADID`, `SHIPDETAILID`, `TOTALID`, `LEGCODE`, `SHIPMENTCODE`, `CONSIGNORCODE`, `CONSIGNORNAME`, `DEPTNO`, `DEPTNAME`, `CUSTOMERCODE`, `CUSTOMERNAME`, `AREAHOUSE`, `SALESMAN`, `TRANSMODEID`, `ERPTRANSMODENAME`, `OPERATIONTYPE`, `DEMANDARRIVETIME`, `TRANSPORTTYPE`, `ORDERINSTANCY`, `TRANSPORTCATEGORY`, `ROUTENO`, `TRANSDEADLINE`, `FROMGTRANSID`, `FROMGTRANSNAME`, `TOGTRANSID`, `TOGTRANSNAME`, `RECEIVEADDR`, `RECEIVEMAN`, `RECEIVEPHONE`, `CREDATE`, `INOUTFLAG`, `WMSROUTEWAVENO`, `PRINTTYPE`, `TOTALQUNTITY`, `WHOLEQUNTITY`, `PARTQUNTITY`, `JFQUNTITY`, `DESCRIPTION`, `EXTCOL0`, `EXTCOL1`, `EXTCOL2`, `EXTCOL3`, `EXTCOL4`, `EXTCOL5`, `EXTCOL6`, `EXTCOL7`, `EXTCOL8`, `EXTCOL9`, `EXTCOL10`, `EXTCOL11`, `EXTCOL12`, `EXTCOL13`, `EXTCOL14`, `EXTCOL15`, `EXTCOL16`, `EXTCOL17`, `EXTCOL18`, `EXTCOL19`, `senderOrg`, `senderPerson`, `senderTel`, `senderAddress`, `receiverOrg`, `receiverPerson`, `receiverTel`, `receiverAddress`, `SecretKey`, `CreateTime`, `customerId`) VALUES ( '{0}', '', '', '', '{0}', '', '{0}', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', 0, 0,0, 0, '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '未知', '{1}', '{2}');", orderInfo.RelationId, orderInfo.OperateAt, customerId));
                    }
                    LogServer.AddLogText("开始添加关联运单", orderInfo.RelationId);
                    //插入本系统自用的关联运单
                    sqlList.Add(string.Format("insert into huadong_tmsorder_waybillbase(relationId,number{2}) values('{0}', '{1}'{3})", orderInfo.RelationId, number, customerId != 0 ? ",customerId" : "", customerId != 0 ? "," + customerId : ""));
                    string updateReceiverIdSql = string.Empty;
                    if (customerInfo.LinkType == 2)
                    {
                        sql = string.Format("insert into waybill_base (number,senderId,senderOrg,senderPerson,senderTel,senderAddress,receiverId,receiverOrg,receiverPerson, receiverTel, receiverAddress, billingCount, stage, beginAt, signinAt, picPostbackAt, company) select '{4}' as number, {0} as senderId, '{1}' as senderOrg, senderPerson , senderTel , senderAddress ,null as receiverId, receiverOrg , receiverPerson , receiverTel , receiverAddress,(if(isnull(JFQUNTITY),0,JFQUNTITY)) as billingCount, 0 as stage, '{2}' as beginAt, null as signinAt, null as picPostbackAt, 1 as company from huadong_tms_order where (SHIPDETAILID = '{3}' or LEGCODE = '{3}') and customerId={5} limit 1", Huadong_Id, Huadong_Name, orderInfo.OperateAt, orderInfo.RelationId, number, customerId);
                        //宁波医药
                        updateReceiverIdSql =("update waybill_base set receiverId = (select Id from customer where BindReceiverOrg=receiverOrg order by lastUpdateTime desc limit 1) where number = '" + number + "'");
                    }
                    else
                    {
                        sql = string.Format("insert into waybill_base (number,senderId,senderOrg,senderPerson,senderTel,senderAddress,receiverId,receiverOrg,receiverPerson, receiverTel, receiverAddress, billingCount, stage, beginAt, signinAt, picPostbackAt, company) select '{4}' as number, {0} as senderId, '{1}' as senderOrg, EXTCOL12 as senderPerson, '' as senderTel, EXTCOL11 as senderAddress,null as receiverId, CUSTOMERNAME as receiverOrg, RECEIVEMAN as receiverPerson, RECEIVEPHONE as receiverTel, RECEIVEADDR as receiverAddress,0 as billingCount, 0 as stage, '{2}' as beginAt, null as signinAt, null as picPostbackAt, 1 as company from huadong_tms_order where SHIPDETAILID = '{3}' or LEGCODE = '{3}' limit 1", Huadong_Id, Huadong_Name, orderInfo.OperateAt, orderInfo.RelationId, number);
                        //大华东
                        updateReceiverIdSql =("update waybill_base set receiverId = (select Id from customer where fullName=receiverOrg order by lastUpdateTime desc limit 1) where number = '" + number + "'");
                    }
                    sqlList.Add(sql);
                    sqlList.Add(updateReceiverIdSql);
                    foreach (string item in sqlList)
                    {
                        LogServer.AddLogText(item, orderInfo.RelationId);
                    }
                    result = _SqlHelp.ExecuteTranstration(sqlList) ? 1 : 0;
                    LogServer.AddLogText(string.Format("执行结果：{0}", result), orderInfo.RelationId);
                    //回滚暂存表中的可用节点
                    if (result == 1)
                    {
                        HuaDongFcCoTable(orderInfo.RelationId);
                        HuaDongFcCoPictures(orderInfo.RelationId);
                    }
                    LogServer.AddLogText("创建第三方运单逻辑结束", orderInfo.RelationId);
                }
                else
                {
                    //判断运单操作时间先后，操作时间早的更新后的
                    if (Convert.ToDateTime(orderInfo.OperateAt) < IsExist.BeginAt)
                    {
                        sql = string.Format("update  waybill_base set beginAt='{0}' where number='{1}' ;", orderInfo.OperateAt, IsExist.Number);
                        int updatbeginAt = _SqlHelp.ExecuteNonQuery(sql);
                        LogServer.AddLogText(string.Format("sql:{0},结果:{1}", sql, updatbeginAt), orderInfo.RelationId);
                        result = 1;
                    }
                    else
                        result = 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// 根据发货单位名称从客户信息表查询匹配的客户ID
        /// </summary>
        /// <param name="receiverName">发货单位</param>
        /// <returns></returns>
        private static int? GetBindReceiverOrgId(string receiverName)
        {
            int? result = null;
            string sql = string.Format("select id from customer where BindReceiverOrg='{0}' limit 1", receiverName);
            object obj = _SqlHelp.ExecuteScalar(sql);
            if (obj is DBNull == false)
                result = Convert.ToInt32(obj);
            return result;
        }

        /// <summary>
        /// 第三方运单信息
        /// </summary>
        /// <param name="huadong"></param>
        /// <returns></returns>
        public static bool GethuadongTmsOrder(List<Model_Huadong_Tms_Order> huadong)
        {
            if (huadong.Count == 0)
                return false;
            bool result = false;
            try
            {
                int distinctCount = 0;
                foreach (Model_Huadong_Tms_Order order in huadong)
                {
                    //1.查询运单表中存在运单号的信息，
                    List<Model_Waybill_Base> way = QueryWaybill(order);
                    if (way == null)
                    {
                        //根据运单号查询华东医药托运订单信息
                        List<Model_Huadong_Tms_Order> huadongtms = GethuadongTms(order);
                        if (huadongtms.Count > 0)
                        {
                            foreach (Model_Huadong_Tms_Order rela in huadongtms)
                            {
                                //判断华东药托运订单的relationId字段是否为空
                                if (string.IsNullOrEmpty(rela.RelationId))
                                {
                                    //把运单号插入华东医药托运订单的relationId字段中
                                    int reslult = UpdateHuadong(order.RelationId, rela);
                                    if (reslult > 0)
                                    {
                                        //查询华东运单信息
                                        Model_Huadong_Tms_Order huadongtmsorder = Queryhuadongtmsorder(rela);
                                        //根据运单号查询的信息插入运单记录运单表信息
                                        if (AddWaybillBase(huadongtmsorder) > 0)
                                        {
                                            result = true;
                                            HuaDongFcCoTable(order.RelationId);
                                        }
                                        else
                                            result = false;
                                    }
                                    else
                                        return false;
                                }
                                else
                                    distinctCount++;
                            }
                        }
                        else
                            distinctCount++;
                    }
                    else
                        distinctCount++;
                }
                if (distinctCount == huadong.Count)
                    return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// 查询华东订单信息
        /// </summary>
        /// <param name="huadong">华东医药托运订单主体信息</param>
        /// <returns></returns>
        public static List<Model_Huadong_Tms_Order> GethuadongTms(Model_Huadong_Tms_Order huadong)
        {
            List<Model_Huadong_Tms_Order> list = new List<Model_Huadong_Tms_Order>();
            if (huadong != null)
            {
                string sql = "select id,relationId,code,SRCEXPNO,ROADID,SHIPDETAILID,TOTALID,LEGCODE,SHIPMENTCODE,OPERATIONTYPE,DEMANDARRIVETIME," +
                            "senderOrg,senderPerson,senderTel,senderAddress,receiverOrg,receiverPerson,receiverTel,receiverAddress " +
                            " from huadong_tms_order where (SHIPDETAILID = ?SHIPDETAILID or LEGCODE = ?LEGCODE ) ;";
                MySqlParameter[] para = new MySqlParameter[2];
                para[0] = new MySqlParameter("SHIPDETAILID", huadong.RelationId);
                para[1] = new MySqlParameter("LEGCODE", huadong.RelationId);
                list = _SqlHelp.ExecuteObjects<Model_Huadong_Tms_Order>(sql, para);
            }
            return list;

        }
        /// <summary>
        /// 插入华东医药托运订单的关联编号信息
        /// </summary>
        /// <param name="rela">华东医药托运订单主体信息</param>
        /// <returns></returns>
        public static int UpdateHuadong(string order, Model_Huadong_Tms_Order rela)
        {
            string sql = "update huadong_tms_order set relationId=?relationId where id=?id";
            MySqlParameter[] para = new MySqlParameter[2];
            para[0] = new MySqlParameter("relationId", order);
            para[1] = new MySqlParameter("id", rela.Id);
            int reslut = _SqlHelp.ExecuteNonQuery(sql, para);
            return reslut;
        }
        /// <summary>
        /// 查询华东运单的信息
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public static Model_Huadong_Tms_Order Queryhuadongtmsorder(Model_Huadong_Tms_Order order)
        {
            string sql = "select * from huadong_tms_order where id=?id";
            MySqlParameter[] para = new MySqlParameter[1];
            para[0] = new MySqlParameter("id", order.Id);
            Model_Huadong_Tms_Order huadongtmsorder = _SqlHelp.ExecuteObject<Model_Huadong_Tms_Order>(sql, para);
            return huadongtmsorder;
        }
        /// <summary>
        /// 添加运单信息
        /// </summary>
        /// <param name="huadongtms"></param>
        /// <returns></returns>
        public static int AddWaybillBase(Model_Huadong_Tms_Order huadongtms)
        {
            //从配置文件中读取寄货客户单位id和name
            int temp = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["Huadong_Id"]);
            string senderorg = System.Configuration.ConfigurationManager.AppSettings["Huadong_Name"];
            string receiverId = null;
            int billingCount = 0;
            int stage = 0;
            string SenderTel = "";
            DateTime beginAt = DateTime.Now;
            string signinAt = null;
            string picPostbackAt = null;
            long number = 1000000000;
            string sqlnumber = "select * from waybill_base where company=1;";
            List<Model_Waybill_Base> list = _SqlHelp.ExecuteObjects<Model_Waybill_Base>(sqlnumber);
            number = number + list.Count + 1;
            string HD = "99" + number;
            string sql = "insert into waybill_base(number,senderId,senderOrg,senderPerson,senderTel,senderAddress,receiverId,receiverOrg,receiverPerson,receiverTel,receiverAddress,billingCount,stage,beginAt,signinAt,picPostbackAt,company) " +
                          " values(?number,?senderId,?senderOrg,?senderPerson,?senderTel,?senderAddress,?receiverId,?receiverOrg,?receiverPerson,?receiverTel,?receiverAddress,?billingCount,?stage,?beginAt,?signinAt,?picPostbackAt,?company)";
            MySqlParameter[] para = new MySqlParameter[17];
            para[0] = new MySqlParameter("number", HD);
            para[1] = new MySqlParameter("senderId", temp);
            para[2] = new MySqlParameter("senderOrg", senderorg);
            para[3] = new MySqlParameter("senderPerson", huadongtms.EXTCOL12);//寄件人
            para[4] = new MySqlParameter("senderTel", SenderTel);//寄件人电话
            para[5] = new MySqlParameter("senderAddress", huadongtms.EXTCOL11);//寄件人地址
            para[6] = new MySqlParameter("receiverId", receiverId);//收货客户单位 id
            para[7] = new MySqlParameter("receiverOrg", huadongtms.CUSTOMERNAME);//收货单位
            para[8] = new MySqlParameter("receiverPerson", huadongtms.RECEIVEMAN);//收货人
            para[9] = new MySqlParameter("receiverTel", huadongtms.RECEIVEPHONE);//收货人电话
            para[10] = new MySqlParameter("receiverAddress", huadongtms.RECEIVEADDR);//收货人地址
            para[11] = new MySqlParameter("billingCount", billingCount);
            para[12] = new MySqlParameter("stage", stage);
            para[13] = new MySqlParameter("beginAt", beginAt);
            para[14] = new MySqlParameter("signinAt", signinAt);
            para[15] = new MySqlParameter("picPostbackAt", picPostbackAt);
            para[16] = new MySqlParameter("company", Enum_Company.ThirdParty);
            int result = 0;
            result = _SqlHelp.ExecuteNonQuery(sql, para);
            //插入关联表信息
            if (result > 0)
                result = AddhuadongWaybillbase(huadongtms, HD);
            return result;
        }
        /// <summary>
        /// 添加华东医药托运订单与惊尘运单关联表 信息
        /// </summary>
        /// <param name="waybillbase"></param>
        /// <returns></returns>
        public static int AddhuadongWaybillbase(Model_Huadong_Tms_Order waybillbase, string HD)
        {
            string sql = "insert into huadong_tmsorder_waybillbase(relationId,number) values(?relationId, ?number)";
            MySqlParameter[] para = new MySqlParameter[2];
            para[0] = new MySqlParameter("relationId", waybillbase.RelationId);
            para[1] = new MySqlParameter("number", HD);
            int result = _SqlHelp.ExecuteNonQuery(sql, para);
            return result;
        }

        /// <summary>
        /// 检查订单是否存在
        /// </summary>
        /// <param name="relationId"></param>
        /// <returns></returns>
        private static Model_Waybill_Base CheckThirdPartyNumberExist(string relationId)
        {
            string sql = string.Format("select * from waybill_base where number in (select number from huadong_tmsorder_waybillbase where relationId = '{0}') and number<>'' and number is not null", relationId);
            LogServer.AddLogText(sql, relationId);
            return _SqlHelp.ExecuteObject<Model_Waybill_Base>(sql);
        }


        /// <summary>
        /// 查询运单信息是否存在第三方信息
        /// </summary>
        /// <param name="huadong"></param>
        /// <returns></returns>
        public static List<Model_Waybill_Base> QueryWaybill(Model_Huadong_Tms_Order huadong)
        {
            List<Model_Waybill_Base> list = null;
            //Model_Huadong_Tmsorder_Waybillbase huadongtmsorder = new Model_Huadong_Tmsorder_Waybillbase();
            string distinctSql = string.Format("select * from huadong_tmsorder_waybillbase where relationId in('{0}')", huadong.RelationId);
            Model_Huadong_Tmsorder_Waybillbase huadongtmsorder = _SqlHelp.ExecuteObject<Model_Huadong_Tmsorder_Waybillbase>(distinctSql);
            if (huadongtmsorder != null)
            {
                string sql = "select id,number,senderId,senderOrg,senderPerson,senderTel,senderAddress,receiverId,receiverOrg," +
                            "receiverPerson,receiverTel,receiverAddress,billingCount,stage,beginAt,signinAt,picPostbackAt from waybill_base " +
                            " where number = ?number";
                MySqlParameter[] para = new MySqlParameter[1];
                para[0] = new MySqlParameter("number", huadongtmsorder.number);
                list = _SqlHelp.ExecuteObjects<Model_Waybill_Base>(sql, para);
            }
            return list;
        }

        /// <summary>
        /// 判断字符串是否为纯数字
        /// </summary>
        /// <param name="temp">字符串</param>
        /// <returns></returns>
        public static bool ChecNumber(string temp)
        {
            for (int i = 0; i < temp.Length; i++)
            {
                byte tempByte = Convert.ToByte(temp[i]);
                if ((tempByte < 48) || (tempByte > 57))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// 查询关联表
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        private static List<Model_Huadong_Tmsorder_Waybillbase> GetHuadongWaybillbase(List<string> numbers)
        {
            List<Model_Huadong_Tmsorder_Waybillbase> list = new List<Model_Huadong_Tmsorder_Waybillbase>();
            if (numbers.Count > 0)
            {
                string distinctSql = string.Format("select * from huadong_tmsorder_waybillbase where relationId in('{0}') limit 1", string.Join("','", numbers));
                list = _SqlHelp.ExecuteObjects<Model_Huadong_Tmsorder_Waybillbase>(distinctSql);
            }
            return list;
        }

        /// <summary>
        /// 查询运单中已存在的信息
        /// </summary>
        /// <param name="numbers"></param>
        /// <returns></returns>
        //private static List<Model_Waybill_Base> GetExistWaybills(List<string> numbers)
        //{
        //    List<Model_Waybill_Base> list = new List<Model_Waybill_Base>();
        //    if (numbers.Count > 0)
        //    {
        //        string distinctSql = string.Format("select * from waybill_base where number in('{0}')", string.Join("','", numbers));
        //        list = _SqlHelp.ExecuteObjects<Model_Waybill_Base>(distinctSql);
        //    }
        //    return list;
        //}
        private static List<Model_Waybill_Base> GetExistWaybills(string numbers)
        {
            List<Model_Waybill_Base> list = new List<Model_Waybill_Base>();
            if (numbers != null)
            {
                string distinctSql = "select * from waybill_base where number='" + numbers + "'";
                list = _SqlHelp.ExecuteObjects<Model_Waybill_Base>(distinctSql);
            }
            return list;
        }
        /// <summary>
        /// 第三方节点信息
        /// </summary>
        /// <param name="nodeList"></param>
        /// <returns></returns>
        public static bool GethuadongTmsOrderNode(List<Model_Waybill_Node> nodeList)
        {
            if (nodeList.Count == 0)
                return false;
            bool result = false;
            try
            {
                //查询关联表是否存在信息
                List<Model_Huadong_Tmsorder_Waybillbase> numberList = GetHuadongWaybillbase(nodeList.Select(l => l.BaseId).Distinct().ToList());
                if (numberList.Count == 0)
                {
                    try
                    {
                        //关联表不存在的信息保存在暂存表中
                        string remarks = "运单不存在";
                        string sql = string.Format("insert into unnecessary_node(baseId,operateAt,storageId,storageName,content,arrived,remarks,inserttime) values('{0}','{1}',{2},'{3}','{4}',{5},'{6}','{7}') ;",
                            nodeList[0].BaseId, nodeList[0].operateAt, nodeList[0].StorageId, nodeList[0].StorageName, nodeList[0].Content, (int)nodeList[0].Arrived, remarks, DateTime.Now);
                        _SqlHelp.ExecuteNonQuery(sql);
                    }
                    catch (Exception)
                    {
                    }
                    return true;
                }
                int receiveContinue = 0;
                //如果有多条数据，取第一条
                Model_Huadong_Tmsorder_Waybillbase item = numberList[0];
                List<Model_Waybill_Base> baseId = GetExistWaybills(item.number);
                if (baseId.Count == 0)
                    return true;
                foreach (Model_Waybill_Base waybase in baseId)
                {
                    //判断运单状态是否签收
                    if (waybase.Stage == Enum_WaybillStage.Received)
                    {
                        receiveContinue++;
                        continue;
                    }
                    foreach (Model_Waybill_Node node in nodeList)
                    {
                        if (GetOperateAt(waybase.Id, node.operateAt.ToString("yyyy-MM-dd HH:mm:ss")) > 0)
                        {
                            receiveContinue++;
                            continue;
                        }
                        int no = AddNode(node, waybase);
                        if (no > 0)
                            result = true;
                        else
                            result = false;
                    }
                }

                if (receiveContinue == nodeList.Count)
                    return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        //public static bool GethuadongTmsOrderNode(List<Model_Waybill_Node> nodeList)
        //{
        //    if (nodeList.Count == 0)
        //        return false;
        //    bool result = false;
        //    try
        //    {
        //        //查询关联表是否存在信息
        //        List<Model_Huadong_Tmsorder_Waybillbase> numberList = GetHuadongWaybillbase(nodeList.Select(l => l.BaseId).Distinct().ToList());
        //        if (numberList.Count == 0)
        //            return true;
        //        List<Model_Waybill_Base> baseId = GetExistWaybills(numberList.Select(l => l.number).Distinct().ToList());
        //        if (baseId.Count == 0)
        //            return true;
        //        int receiveContinue = 0;
        //            foreach (Model_Waybill_Node node in nodeList)
        //            {
        //                foreach (Model_Waybill_Base waybase in baseId)
        //                {
        //                    //if (waybase.Number == waynode.BaseId)
        //                    //{
        //                    //判断运单状态是否签收
        //                    //if (baseId.Find(l => l.Number == numberList.Find(n => n.relationId == node.BaseId).number).Stage==Enum_WaybillStage.Received) 
        //                    if (waybase.Stage == Enum_WaybillStage.Received)
        //                    {
        //                        receiveContinue++;
        //                        continue;
        //                    }
        //                    int no = AddNode(node, waybase);
        //                    if (no > 0)
        //                        result = true;
        //                    else
        //                        result = false;
        //                    //}
        //                }
        //            }

        //        if (receiveContinue == nodeList.Count)
        //            return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return result;
        //}

        public static int AddNode(Model_Waybill_Node nodeList, Model_Waybill_Base baseList)
        {
            string sql = "insert into waybill_node (baseId,operateAt,storageId,storageName,content,arrived) values(?baseId,?operateAt,?storageId,?storageName,?content,?arrived) ;";
            MySqlParameter[] para = new MySqlParameter[6];
            para[0] = new MySqlParameter("baseId", baseList.Id);
            para[1] = new MySqlParameter("operateAt", nodeList.operateAt);
            para[2] = new MySqlParameter("storageId", nodeList.StorageId);
            para[3] = new MySqlParameter("storageName", nodeList.StorageName);
            para[4] = new MySqlParameter("content", nodeList.Content);
            para[5] = new MySqlParameter("arrived", nodeList.Arrived);
            int result = 0;
            result = _SqlHelp.ExecuteNonQuery(sql, para);
            if (result > 0)
            {
                if (nodeList.Arrived == Enum_Arrived.HaveArrived)
                {
                    string upsql = "update waybill_base set stage=1 ,signinAt=?signinAt  where number=?number ;";
                    MySqlParameter[] par = new MySqlParameter[2];
                    par[0] = new MySqlParameter("signinAt", nodeList.operateAt);
                    par[1] = new MySqlParameter("number", baseList.Number);
                    result = _SqlHelp.ExecuteNonQuery(upsql, par);

                }
            }
            return result;
        }
        public static bool UploadWaybill_HuaDong(Model_Waybill_Postback_Pic huadongPostback, DateTime postbackTime, List<object> picList)
        {
            bool result = false;
            int picIndex = 0;//成功上传的图片数量
            if (picList.Count == 0)
                return false;
            if (string.IsNullOrEmpty(huadongPostback.PicName))
                return false;
            string[] picNameArr = huadongPostback.PicName.Split('|');
            if (picNameArr.Length != picList.Count)
                return false;
            //配置文件中的路径
            string filePath = string.Empty;
            string tempPath = System.Configuration.ConfigurationManager.AppSettings["PostbackPath"];
            if (string.IsNullOrEmpty(tempPath))
                return false;
            string timePath = DateTime.Now.ToString("yyyyMM");
            filePath = tempPath + "\\" + timePath;
            try
            {
                List<Model_Huadong_Tmsorder_Waybillbase> waybillbase = GetHuadongWaybillbase(new List<string> { huadongPostback.BaseId });
                if (waybillbase.Count() == 0)
                {
                    return true;
                }
                List<Model_Waybill_Base> numberList = GetExistWaybills(waybillbase[0].number).Distinct().ToList();
                if (numberList.Count == 0)
                    return true;//运单还未上传，无法上传签收图片
                if (!System.IO.Directory.Exists(filePath))
                    System.IO.Directory.CreateDirectory(filePath);
                StringBuilder sql = new StringBuilder("insert into waybill_postback_pic (baseId,picName) values ");
                for (int i = 0; i < picList.Count; i++)
                {
                    string fileName = numberList[0].Number + "_" + System.IO.Path.GetFileName(huadongPostback.PicName.Split('|')[i]);
                    MyTool.SaveImage(MyTool.GetGzipPicBytes(picList[i] as byte[]), filePath + "\\" + fileName);
                    sql.AppendFormat("('{0}','{1}'),", numberList[0].Id, timePath + "/" + fileName);
                    picIndex++;
                }
                sql.Length -= 1;
                List<string> sqlList = new List<string>();
                sqlList.Add(sql.ToString());
                sqlList.Add(string.Format("update waybill_base set picPostbackAt='{0}' where number='{1}'", postbackTime.ToString("yyyy-MM-dd HH:mm:ss"), numberList[0].Number));
                result = _SqlHelp.ExecuteTranstration(sqlList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (result == false && picIndex > 0)
                {
                    //失败时删除图片
                    for (int i = picIndex; i > 0; i--)
                    {
                        string fileName = huadongPostback.BaseId + "_" + System.IO.Path.GetFileName(huadongPostback.PicName.Split('|')[i - 1]);
                        System.IO.File.Delete(filePath + "\\" + fileName);
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 查询是否有相同的节点
        /// </summary>
        /// <param name="baseId"></param>
        /// <param name="operateAt"></param>
        /// <returns></returns>
        public static int GetOperateAt(int baseId, string operateAt)
        {
            string sql = string.Format("select count(*) from waybill_node where baseId={0} and operateAt='{1}'", baseId, operateAt);
            int result = Convert.ToInt32(_SqlHelp.ExecuteScalar(sql));
            return result;
        }
        /// <summary>
        /// 查询暂存表是否存在运单节点信息，存在保存到节点表中
        /// </summary>
        /// <param name="RelationId">运单号</param>
        public static void HuaDongFcCoTable(string RelationId)
        {
            try
            {
                LogServer.AddLogText("开始删除临时表超过一个月的信息", RelationId);
                //删除超过一个月的数据
                string sql = string.Format("delete from unnecessary_node where inserttime<'{0}' ;", DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd HH:mm:ss"));
                _SqlHelp.ExecuteNonQuery(sql);
                //查询关系表是否存在信息
                sql = string.Format("select * from huadong_tmsorder_waybillbase where relationId='{0}';", RelationId);
                LogServer.AddLogText(string.Format("查询关系表是否存在信息{0}", sql), RelationId);
                Model_Huadong_Tmsorder_Waybillbase huadongbase = _SqlHelp.ExecuteObject<Model_Huadong_Tmsorder_Waybillbase>(sql);
                if (huadongbase != null)
                {
                    //查询运单信息是否存在
                    sql = string.Format("select * from waybill_base where number='{0}'; ", huadongbase.number);
                    LogServer.AddLogText(string.Format("查询运单信息是否存在{0} ", sql), RelationId);
                    Model_Waybill_Base waybbase = _SqlHelp.ExecuteObject<Model_Waybill_Base>(sql);
                    if (waybbase != null)
                    {
                        //查询暂存表是否存在节点信息
                        sql = string.Format("select * from unnecessary_node where baseId='{0}' and operateAt>='{1}';", RelationId, waybbase.BeginAt.ToString("yyyy-MM-dd HH:mm:ss"));
                        LogServer.AddLogText(string.Format("查询暂存表是否存在节点信息{0}", sql), RelationId);
                        List<Model_UnnecessaryNode> waybnode = _SqlHelp.ExecuteObjects<Model_UnnecessaryNode>(sql);
                        if (waybnode.Count != 0)
                        {
                            sql = string.Empty;//清空sql语句
                            foreach (Model_UnnecessaryNode item in waybnode)
                            {
                                string tempSql = string.Format("insert into waybill_node (baseId,operateAt,storageId,storageName,content,arrived,parentStorageId,handleFlag,scanNumber,customerId,insertTime) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{10}','{8}','{9}') ; ",
                                   waybbase.Id, item.operateAt.ToString("yyyy-MM-dd HH:mm:ss"), item.StorageId, item.StorageName, item.Content, (int)item.Arrived, item.ParentStorageId, (item.CustomerId == 0 ? -1 : 0), item.CustomerId, item.InsertTime.ToString("yyyy-MM-dd HH:mm:ss"), item.BaseId);
                                LogServer.AddLogText("插入节点:" + tempSql, RelationId);
                                //查询到的信息保存到节点中
                                sql += tempSql;
                                //删除暂存表中的信息
                                tempSql = string.Format("delete from unnecessary_node where id={0} ;", item.Id);
                                LogServer.AddLogText("删除临时节点:" + tempSql, RelationId);
                                sql += tempSql;
                                if (item.Arrived == Enum_Arrived.HaveArrived)
                                {
                                    tempSql = string.Format("update waybill_base set stage=1 ,signinAt='{0}' where number='{1}';", item.operateAt, waybbase.Number);
                                    LogServer.AddLogText("更新运单运抵时间:" + tempSql, RelationId);
                                    sql += tempSql;
                                }
                            }
                            int result = _SqlHelp.ExecuteNonQuery(sql);
                            LogServer.AddLogText("查询到的信息保存到节点中，删除暂存表中的信息", RelationId);
                        }
                        else
                            LogServer.AddLogText("暂存表不存在节点信息", RelationId);
                    }
                    else
                        LogServer.AddLogText("运单信息不存在", RelationId);
                }
                else
                    LogServer.AddLogText("关系表信息不存在", RelationId);
            }
            catch (Exception ex)
            {
                LogServer.AddLogText("HuaDongFcCoTable异常：" + ex.Message, RelationId);
            }
        }
        /// <summary>
        /// 查询临时图片表是否存在图片信息，存在保存到图片表中
        /// </summary>
        /// <param name="RelationId"></param>
        public static void HuaDongFcCoPictures(string RelationId)
        {
            try
            {
                LogServer.AddLogText("开始删除临时图片表超过一个月的信息", RelationId);
                //删除超过一个月的数据
                string sql = string.Format("delete from temporarypictures where inserttime<'{0}' ;", DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd HH:mm:ss"));
                _SqlHelp.ExecuteNonQuery(sql);
                //查询关系表是否存在信息
                sql = string.Format("select * from huadong_tmsorder_waybillbase where relationId='{0}';", RelationId);
                Model_Huadong_Tmsorder_Waybillbase huadongbase = _SqlHelp.ExecuteObject<Model_Huadong_Tmsorder_Waybillbase>(sql);
                if (huadongbase != null)
                {
                    //查询运单信息是否存在
                    sql = string.Format("select * from waybill_base where number='{0}'; ", huadongbase.number);
                    Model_Waybill_Base waybbase = _SqlHelp.ExecuteObject<Model_Waybill_Base>(sql);
                    if (waybbase != null)
                    {
                        //查询临时图片表是否存在图片信息
                        sql = string.Format("select * from temporarypictures where baseId='{0}' and operateAt>='{1}';", RelationId, waybbase.BeginAt.ToString("yyyy-MM-dd HH:mm:ss"));
                        List<Model_TemporaryPictures> waybnode = _SqlHelp.ExecuteObjects<Model_TemporaryPictures>(sql);
                        if (waybnode.Count != 0)
                        {
                            sql = string.Empty;//清空sql语句
                            foreach (Model_TemporaryPictures item in waybnode)
                            {
                                string tempSql = string.Format("insert into waybill_postback_pic(baseId,picName) values({0},'{1}') ; ",
                                   waybbase.Id, item.PicName);
                                LogServer.AddLogText("插入图片:" + tempSql, RelationId);
                                //查询到的信息保存到节点中
                                sql += tempSql;
                                //删除暂存表中的信息
                                tempSql = string.Format("delete from temporarypictures where id={0} ;", item.id);
                                LogServer.AddLogText("删除临时图片:" + tempSql, RelationId);
                                sql += tempSql;
                                tempSql = string.Format("update waybill_base set picPostbackAt='{0}' where number='{1}';", item.operateAt, waybbase.Number);
                                LogServer.AddLogText("更新运单图片时间:" + tempSql, RelationId);
                                sql += tempSql;
                            }
                            int result = _SqlHelp.ExecuteNonQuery(sql);
                            LogServer.AddLogText("临时图片信息保存到图片表，删除临时图片表中的信息，结果:" + result, RelationId);
                        }
                        else
                            LogServer.AddLogText(string.Format("图片表不存在图片信息,sql:{0},结果:{1}", sql, waybnode.Count), RelationId);
                    }
                    else
                        LogServer.AddLogText(string.Format("运单信息不存在,sql:{0},结果:{1}", sql, waybbase == null ? "没有信息" : "1"), RelationId);
                }
                else
                    LogServer.AddLogText(string.Format("关联信息不存在,sql:{0},结果:{1}", sql, huadongbase == null ? "没有信息" : "1"), RelationId);
            }
            catch (Exception ex)
            {
                LogServer.AddLogText("HuaDongFcCoTable异常：" + ex.Message, RelationId);
            }
        }
    }
}
