using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C2LP.WebService.Utility;
using C2LP.WebService.Model;
using MySql.Data.MySqlClient;

namespace C2LP.WebService.BLL.ConsoleBLL
{
    public class WaybillBaseServer:BaseServer
    {
        #region

        public static List<Model_Waybill_Base> GetWaybillLists(string waybillNumber, string pageIndexAndCount, string startTime, string endTime, int customerId, int roles = 0)
        {
            List<Model_Waybill_Base> list = null;
            if (roles == 0)
            {
                list= GetWaybillListRole( waybillNumber, pageIndexAndCount, startTime, endTime, customerId);
            }
            else
            {
                list = GetWaybillListReceiver( waybillNumber, pageIndexAndCount, startTime, endTime, customerId);
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="waybillNumber">可选参数[指定运单号查询]</param>
        /// <param name="pageIndexAndCount">可选参数[分页参数,格式为"页索引.每页数量".例如:1.50,表示每页显示50条,当前查询第1页]</param>
        /// <param name="startTime">可选参数[查询运单号的起始时间]</param>
        /// <param name="endTime">可选参数[查询运单号的结束时间]</param>
        /// <param name="customerId">可选参数[按客户ID筛选]</param>
        /// <returns></returns>
        public static List<Model_Waybill_Base> GetWaybillListRole(string waybillNumber, string pageIndexAndCount , string startTime , string endTime , int customerId )
        {
            Model_Customer cust = null;
            if (customerId != 0)
            {
                string role = "select role from customer where id=" + customerId + ";";
                 cust = _SqlHelp.ExecuteObject<Model_Customer>(role);
                if (cust==null)
                   throw new Exception("操作失败");   
            }
            string sql = "select id,number,senderId,senderOrg,senderPerson,senderTel,senderAddress,"+
                    "receiverId,receiverOrg,receiverPerson,receiverTel,receiverAddress,"+
                    "billingCount,stage,beginAt,signinAt,picPostbackAt,company from waybill_base  ";
            
            //运单编号
            if (waybillNumber != null && customerId == 0 && startTime == null && endTime == null)
            {
                    sql += "where  number=?number ";
            }
            //运单编号和客户名称
            if (waybillNumber != null && customerId != 0 && startTime == null && endTime == null)
            {
                if(cust.Role==Model.MyEnum.Enum_Role.Sender)
                    sql += "where  number=?number and senderId=?senderId  ";
                else if (cust.Role == Model.MyEnum.Enum_Role.Receiver)
                    sql += "where  number=?number and receiverId=?receiverId ";
            }
            //运单编号、客户名称、开始时间
            if (waybillNumber != null && customerId != 0 && startTime != null && endTime == null)
            {
                if (cust.Role == Model.MyEnum.Enum_Role.Sender)
                    sql += "where  number=?number and senderId=?senderId and beginAt>=?beginAt ";
                else if (cust.Role == Model.MyEnum.Enum_Role.Receiver)
                    sql += "where  number=?number and receiverId=?receiverId and beginAt>=?beginAt ";
            }
            //运单编号、客户名称、开始时间、结束时间
            if (waybillNumber != null && customerId != 0 && startTime != null && endTime != null)
            {
                if (cust.Role == Model.MyEnum.Enum_Role.Sender)
                    sql += "where  number=?number and  senderId=?senderId and beginAt>=?beginAt and signinAt<=?signinAt ";
                else if (cust.Role == Model.MyEnum.Enum_Role.Receiver)
                    sql += "where  number=?number and receiverId=?receiverId  and beginAt>=?beginAt and signinAt<=?signinAt ";
            }
            //运单编号、开始时间
            if (waybillNumber != null && customerId == 0 && startTime != null && endTime == null)
            {
                    sql += "where  number=?number  and beginAt>=?beginAt ";
            }
            //运单编号、结束时间
            if (waybillNumber != null && customerId == 0 && startTime == null && endTime != null)
            {
                    sql += "where  number=?number and signinAt<=?signinAt ";
            }
            //客户名称
            if (waybillNumber == null && customerId != 0 && startTime == null && endTime == null)
            {
                if (cust.Role == Model.MyEnum.Enum_Role.Sender)
                    sql += "where    senderId=?senderId ";
                else if (cust.Role == Model.MyEnum.Enum_Role.Receiver)
                    sql += "where   receiverId=?receiverId   ";
            }
            //客户名称、开始时间
            if (waybillNumber == null && customerId != 0 && startTime != null && endTime == null)
            {
                if (cust.Role == Model.MyEnum.Enum_Role.Sender)
                    sql += "where   senderId=?senderId and beginAt>=?beginAt ";
                else if (cust.Role == Model.MyEnum.Enum_Role.Receiver)
                    sql += "where  receiverId=?receiverId  and beginAt>=?beginAt ";
            }
            //客户名称、结束时间
            if (waybillNumber == null && customerId != 0 && startTime == null && endTime != null)
            {
                if (cust.Role == Model.MyEnum.Enum_Role.Sender)
                    sql += "where   senderId=?senderId and signinAt<=?signinAt ";
                else if (cust.Role == Model.MyEnum.Enum_Role.Receiver)
                    sql += "where  receiverId=?receiverId  and signinAt<=?signinAt ";
            }
            //客户名称、开始时间、结束时间
            if (waybillNumber == null && customerId != 0 && startTime != null && endTime != null)
            {
                if (cust.Role == Model.MyEnum.Enum_Role.Sender)
                    sql += "where  senderId=?senderId and beginAt>=?beginAt and signinAt<=?signinAt ";
                else if (cust.Role == Model.MyEnum.Enum_Role.Receiver)
                    sql += "where receiverId=?receiverId  and beginAt>=?beginAt and signinAt<=?signinAt ";
            }
            //开始时间
            if (waybillNumber == null && customerId == 0 && startTime != null && endTime == null)
            {
                    sql += "where   beginAt>=?beginAt ";
            }
            //开始时间、结束时间
            if (waybillNumber == null && customerId == 0 && startTime != null && endTime != null)
            {
                    sql += "where beginAt>=?beginAt and signinAt<=?signinAt ";
            }
            //结束时间
            if (waybillNumber == null && customerId == 0 && startTime == null && endTime != null)
            {
                    sql += "where  signinAt<=?signinAt ";
            }
            //运单编号、客户名称、结束时间
            if (waybillNumber != null && endTime != null && customerId != 0 && startTime == null)
            {
                if (cust.Role == Model.MyEnum.Enum_Role.Sender)
                    sql += " where number=?number and signinAt<=?signinAt and  senderId=?senderId ";
                else if (cust.Role == Model.MyEnum.Enum_Role.Receiver)
                    sql += " where number=?number and signinAt<=?signinAt and receiverId=?receiverId  ";
            }
            //运单编号、开始时间、结束时间
            if (waybillNumber != null && customerId == 0 && startTime != null && endTime != null)
            {
                 sql += "where  number=?number and beginAt>=?beginAt and signinAt<=?signinAt ";
            }
            if (pageIndexAndCount != null)
            {
                string page = pageIndexAndCount.Substring(0, pageIndexAndCount.LastIndexOf("."));
                string size = pageIndexAndCount.Substring(pageIndexAndCount.LastIndexOf(".") + 1, pageIndexAndCount.Length - (pageIndexAndCount.LastIndexOf(".") + 1));
                sql += " order by beginAt desc limit " + ((Convert.ToInt32(page) - 1) * Convert.ToInt32(size)) + "," + size + ";";
            }
            else
                sql += " ;";
            
            MySqlParameter[] para = new MySqlParameter[5];
            para[0] = new MySqlParameter("number", waybillNumber);
            para[1] = new MySqlParameter("beginAt", startTime);
            para[2] = new MySqlParameter("signinAt", endTime);
            para[3] = new MySqlParameter("receiverId", customerId);
            para[4] = new MySqlParameter("senderId", customerId);

            List<Model_Waybill_Base> list = _SqlHelp.ExecuteObjects<Model_Waybill_Base>(sql, para);

            return list;
        }

        public static List<Model_Waybill_Base> GetWaybillListReceiver(string waybillNumber, string pageIndexAndCount, string startTime, string endTime, int customerId)
        {
            string sql = " select id,number,receiverOrg,receiverPerson,receiverTel,receiverAddress,beginAt,billingCount"+
                " from waybill_base where receiverId in(select id from customer where role=3)  ";

            //客户名称
            if ( customerId != 0 && startTime == null && endTime == null)
            {
                    sql += " and receiverId=?receiverId   ";
            }
            //客户名称、开始时间
            if ( customerId != 0 && startTime != null && endTime == null)
            {
                    sql += " and  receiverId=?receiverId  and beginAt>=?beginAt ";
            }
            //客户名称、结束时间
            if ( customerId != 0 && startTime == null && endTime != null)
            {
                    sql += " and  receiverId=?receiverId  and signinAt<=?signinAt ";
            }
            //客户名称、开始时间、结束时间
            if ( customerId != 0 && startTime != null && endTime != null)
            {
                    sql += " and receiverId=?receiverId  and beginAt>=?beginAt and signinAt<=?signinAt ";
            }
            //开始时间
            if ( customerId == 0 && startTime != null && endTime == null)
            {
                sql += " and   beginAt>=?beginAt ";
            }
            //开始时间、结束时间
            if ( customerId == 0 && startTime != null && endTime != null)
            {
                sql += " and beginAt>=?beginAt and signinAt<=?signinAt ";
            }
            //结束时间
            if ( customerId == 0 && startTime == null && endTime != null)
            {
                sql += " and  signinAt<=?signinAt ";
            }
            if (pageIndexAndCount != null)
            {
                string page = pageIndexAndCount.Substring(0, pageIndexAndCount.LastIndexOf("."));
                string size = pageIndexAndCount.Substring(pageIndexAndCount.LastIndexOf(".") + 1, pageIndexAndCount.Length - (pageIndexAndCount.LastIndexOf(".") + 1));
                sql += " order by beginAt desc limit " + ((Convert.ToInt32(page) - 1) * Convert.ToInt32(size)) + "," + size + ";";
            }
            else
                sql += " ;";

            MySqlParameter[] para = new MySqlParameter[3];
            para[0] = new MySqlParameter("beginAt", startTime);
            para[1] = new MySqlParameter("signinAt", endTime);
            para[2] = new MySqlParameter("receiverId", customerId);

            List<Model_Waybill_Base> list = _SqlHelp.ExecuteObjects<Model_Waybill_Base>(sql, para);
            return list;
        }
        #endregion
        #region 运单总数
        public static int GetWaybillListCount(string waybillNumber, string startTime , string endTime , int customerId , int roles )
        {
            int result=0;
            if (roles ==0)
                result= GetWaybillHistoryQueryCount(waybillNumber, startTime, endTime, customerId);
            else
                result= GetWaybillStatementCount(waybillNumber, startTime, endTime, customerId);
            return result;
        }
        public static int GetWaybillHistoryQueryCount(string waybillNumber, string startTime, string endTime, int customerId)
        {
            Model_Customer cust = null;
            if (customerId != 0)
            {
                string role = "select role from customer where id=" + customerId + ";";
                cust = _SqlHelp.ExecuteObject<Model_Customer>(role);
                if (cust == null)
                    throw new Exception("操作失败");

            }
            string sql = "select count(*) from waybill_base  ";
            //运单编号
            if (waybillNumber != null && customerId == 0 && startTime == null && endTime == null)
            {
                sql += "where  number=?number ";
            }
            //运单编号和客户名称
            if (waybillNumber != null && customerId != 0 && startTime == null && endTime == null)
            {
                if (cust.Role == Model.MyEnum.Enum_Role.Sender)
                    sql += "where  number=?number and senderId=?senderId  ";
                else if (cust.Role == Model.MyEnum.Enum_Role.Receiver)
                    sql += "where  number=?number and receiverId=?receiverId ";
            }
            //运单编号、客户名称、开始时间
            if (waybillNumber != null && customerId != 0 && startTime != null && endTime == null)
            {
                if (cust.Role == Model.MyEnum.Enum_Role.Sender)
                    sql += "where  number=?number and senderId=?senderId and beginAt>=?beginAt ";
                else if (cust.Role == Model.MyEnum.Enum_Role.Receiver)
                    sql += "where  number=?number and receiverId=?receiverId and beginAt>=?beginAt ";
            }
            //运单编号、客户名称、开始时间、结束时间
            if (waybillNumber != null && customerId != 0 && startTime != null && endTime != null)
            {
                if (cust.Role == Model.MyEnum.Enum_Role.Sender)
                    sql += "where  number=?number and  senderId=?senderId and beginAt>=?beginAt and signinAt<=?signinAt ";
                else if (cust.Role == Model.MyEnum.Enum_Role.Receiver)
                    sql += "where  number=?number and receiverId=?receiverId  and beginAt>=?beginAt and signinAt<=?signinAt ";
            }
            //运单编号、开始时间
            if (waybillNumber != null && customerId == 0 && startTime != null && endTime == null)
            {
                sql += "where  number=?number  and beginAt>=?beginAt ";
            }
            //运单编号、结束时间
            if (waybillNumber != null && customerId == 0 && startTime == null && endTime != null)
            {
                sql += "where  number=?number and signinAt<=?signinAt ";
            }
            //客户名称
            if (waybillNumber == null && customerId != 0 && startTime == null && endTime == null)
            {
                if (cust.Role == Model.MyEnum.Enum_Role.Sender)
                    sql += "where    senderId=?senderId ";
                else if (cust.Role == Model.MyEnum.Enum_Role.Receiver)
                    sql += "where   receiverId=?receiverId   ";
            }
            //客户名称、开始时间
            if (waybillNumber == null && customerId != 0 && startTime != null && endTime == null)
            {
                if (cust.Role == Model.MyEnum.Enum_Role.Sender)
                    sql += "where   senderId=?senderId and beginAt>=?beginAt ";
                else if (cust.Role == Model.MyEnum.Enum_Role.Receiver)
                    sql += "where  receiverId=?receiverId  and beginAt>=?beginAt ";
            }
            //客户名称、结束时间
            if (waybillNumber == null && customerId != 0 && startTime == null && endTime != null)
            {
                if (cust.Role == Model.MyEnum.Enum_Role.Sender)
                    sql += "where   senderId=?senderId and signinAt<=?signinAt ";
                else if (cust.Role == Model.MyEnum.Enum_Role.Receiver)
                    sql += "where  receiverId=?receiverId  and signinAt<=?signinAt ";
            }
            //客户名称、开始时间、结束时间
            if (waybillNumber == null && customerId != 0 && startTime != null && endTime != null)
            {
                if (cust.Role == Model.MyEnum.Enum_Role.Sender)
                    sql += "where  senderId=?senderId and beginAt>=?beginAt and signinAt<=?signinAt ";
                else if (cust.Role == Model.MyEnum.Enum_Role.Receiver)
                    sql += "where receiverId=?receiverId  and beginAt>=?beginAt and signinAt<=?signinAt ";
            }
            //开始时间
            if (waybillNumber == null && customerId == 0 && startTime != null && endTime == null)
            {
                sql += "where   beginAt>=?beginAt ";
            }
            //开始时间、结束时间
            if (waybillNumber == null && customerId == 0 && startTime != null && endTime != null)
            {
                sql += "where beginAt>=?beginAt and signinAt<=?signinAt ";
            }
            //结束时间
            if (waybillNumber == null && customerId == 0 && startTime == null && endTime != null)
            {
                sql += "where  signinAt<=?signinAt ";
            }
            //运单编号、客户名称、结束时间
            if (waybillNumber != null && endTime != null && customerId != 0 && startTime == null)
            {
                if (cust.Role == Model.MyEnum.Enum_Role.Sender)
                    sql += " where number=?number and signinAt<=?signinAt and  senderId=?senderId ";
                else if (cust.Role == Model.MyEnum.Enum_Role.Receiver)
                    sql += " where number=?number and signinAt<=?signinAt and receiverId=?receiverId  ";
            }
            //运单编号、开始时间、结束时间
            if (waybillNumber != null && customerId == 0 && startTime != null && endTime != null)
            {
                sql += "where  number=?number and beginAt>=?beginAt and signinAt<=?signinAt ";
            }
            
            sql += " ;";

            MySqlParameter[] para = new MySqlParameter[5];
            para[0] = new MySqlParameter("number", waybillNumber);
            para[1] = new MySqlParameter("beginAt", startTime);
            para[2] = new MySqlParameter("signinAt", endTime);
            para[3] = new MySqlParameter("receiverId", customerId);
            para[4] = new MySqlParameter("senderId", customerId);

            return Convert.ToInt32(_SqlHelp.ExecuteScalar(sql, para));
            
        }
        public static int GetWaybillStatementCount(string waybillNumber, string startTime, string endTime, int customerId)
        {
            string sql = " select count(*) from waybill_base where receiverId in(select id from customer where role=3)  ";
            //客户名称
            if (customerId != 0 && startTime == null && endTime == null)
            {
                sql += " and receiverId=?receiverId   ";
            }
            //客户名称、开始时间
            if (customerId != 0 && startTime != null && endTime == null)
            {
                sql += " and  receiverId=?receiverId  and beginAt>=?beginAt ";
            }
            //客户名称、结束时间
            if (customerId != 0 && startTime == null && endTime != null)
            {
                sql += " and  receiverId=?receiverId  and signinAt<=?signinAt ";
            }
            //客户名称、开始时间、结束时间
            if (customerId != 0 && startTime != null && endTime != null)
            {
                sql += " and receiverId=?receiverId  and beginAt>=?beginAt and signinAt<=?signinAt ";
            }
            //开始时间
            if (customerId == 0 && startTime != null && endTime == null)
            {
                sql += " and   beginAt>=?beginAt ";
            }
            //开始时间、结束时间
            if (customerId == 0 && startTime != null && endTime != null)
            {
                sql += " and beginAt>=?beginAt and signinAt<=?signinAt ";
            }
            //结束时间
            if (customerId == 0 && startTime == null && endTime != null)
            {
                sql += " and  signinAt<=?signinAt ";
            }
            sql += " ;";

            MySqlParameter[] para = new MySqlParameter[3];
            para[0] = new MySqlParameter("beginAt", startTime);
            para[1] = new MySqlParameter("signinAt", endTime);
            para[2] = new MySqlParameter("receiverId", customerId);
            return Convert.ToInt32(_SqlHelp.ExecuteScalar(sql,para));
        }
        #endregion
        /// <summary>
        /// 查询物流节点
        /// </summary>
        /// <param name="waybillNumber"></param>
        /// <param name="operateAt"></param>
        /// <param name="pageIndexAndCount"></param>
        /// <returns></returns>
        public static List<Model_Waybill_Node> GetWaybillNodeLists(string waybillNumber ,string operateAt, string pageIndexAndCount )
        {
            string sql = "";
            string page = "";
            string size = "";
            if (pageIndexAndCount!=null)
            {
                page = pageIndexAndCount.Substring(0,pageIndexAndCount.LastIndexOf("."));
                size = pageIndexAndCount.Substring(pageIndexAndCount.LastIndexOf(".") + 1, pageIndexAndCount.Length - (pageIndexAndCount.LastIndexOf(".") + 1));
            }
            if (waybillNumber != null)
            {
                if(pageIndexAndCount!=null)
                    sql = "select id, convert(baseId, CHAR) as baseId ,operateAt,storageId,storageName,content,arrived from waybill_node where baseId=?baseId order by operateAt desc limit " + ((Convert.ToInt32(page)-1)*Convert.ToInt32(size))+","+size+";";
                else
                    sql = "select id, convert(baseId, CHAR) as baseId ,operateAt,storageId,storageName,content,arrived from waybill_node where baseId=?baseId ;";
            }
            if (waybillNumber != null && operateAt != null && pageIndexAndCount==null)
                sql = "select id, convert(baseId, CHAR) as baseId ,operateAt,storageId,storageName,content,arrived from waybill_node where baseId=?baseId and operateAt>?operateAt  order by operateAt asc limit 1;";
            MySqlParameter[] para = new MySqlParameter[2];
            para[0] = new MySqlParameter("baseId", waybillNumber);
            para[1] = new MySqlParameter("operateAt", operateAt);
            List<Model_Waybill_Node> list = _SqlHelp.ExecuteObjects<Model_Waybill_Node>(sql,para);
            return list;
        }
        //判断冷链数据是车载还是仓库
        public static List<String[]> GetWaybillNodeModel_AiInfo(int storageId, string beginTime, string endTime, string pageIndexAndCount)
        {
            //string sql = "select storageType from coldstorage where id=?id;";
            //MySqlParameter[] para = new MySqlParameter[1];
            //para[0] = new MySqlParameter("id", storageId);
            //Model_ColdStorage coldstorage = _SqlHelp.ExecuteObject<Model_ColdStorage>(sql, para);
            //if (coldstorage.StorageType == Model.MyEnum.Enum_StorageType.ColdStorage)
                return GetWaybillNodeHistDataListLengKu(storageId, beginTime, endTime, pageIndexAndCount);
            //else
             //   return GetWaybillNodeHistDataLists(storageId, beginTime, endTime, pageIndexAndCount);

        }
        /// <summary>
        /// 查询冷链数据
        /// </summary>
        /// <param name="storageId">当前节点货物的载体[仓库或车载ID]</param>
        /// <param name="beginTime">节点开始时间</param>
        /// <param name="endTime">可选参数[下一个节点开始时间]</param>
        /// <param name="pageIndexAndCount">分页</param>
        /// <returns></returns>
        public static List<String[]> GetWaybillNodeHistDataLists(int storageId, string beginTime, string endTime, string pageIndexAndCount)
        {
            string sql = "";
            //根据运单物流节点中的冷库、车载存id查询探头
            sql = "select *,pointName as PpointName from aiinfo where storageId=?storageId";
            MySqlParameter[] ainn = new MySqlParameter[1];
            ainn[0] = new MySqlParameter("storageId", storageId);
            List<Model_AiInfo> mailist = _SqlHelp.ExecuteObjects<Model_AiInfo>(sql, ainn);
            
            sql = "SELECT t1.dataTime  , ";
            foreach (Model_AiInfo item in mailist)
            {
                sql += "max(case t1.pointName when '" + item.PpointName + "' then t1.data else null end) as '" + item.PpointName + "',";
            }

            sql += "min(case t1.isAlarm when 0 then 0 else 1 end) as 'isAlarm' " +
            "FROM " +
            "(select a.pointName, a.pointId, d.data, d.datatime, d.isAlarm from aiinfo a " +
            "join history_data_"+storageId+" d on a.pointId = d.pointId " +
            "where d.dataTime between '"+beginTime+"' and  '"+endTime+ "') t1 GROUP BY t1.dataTime";
            if (pageIndexAndCount != null)
            {
                string page = pageIndexAndCount.Substring(0, pageIndexAndCount.LastIndexOf("."));
                string size = pageIndexAndCount.Substring(pageIndexAndCount.LastIndexOf(".") + 1, pageIndexAndCount.Length - (pageIndexAndCount.LastIndexOf(".") + 1));
                sql += " limit "+((Convert.ToInt32(page)-1)*Convert.ToInt32(size))+","+size+";";
            }
            else
                sql += " ;";
             List<String[]> list = _SqlHelp.Execute4ListOfObject(sql);

            return list;
        }
        public static List<String[]> GetWaybillNodeHistDataListLengKu(int storageId, string beginTime, string endTime, string pageIndexAndCount)
        {
            string sql = "";
            //根据运单物流节点中的冷库、车载存id查询探头
            sql = "select *,pointName as PpointName from aiinfo where storageId=?storageId and (pointType=1 or pointType=2)";
            MySqlParameter[] ainn = new MySqlParameter[1];
            ainn[0] = new MySqlParameter("storageId", storageId);
            List<Model_AiInfo> mailist = _SqlHelp.ExecuteObjects<Model_AiInfo>(sql, ainn);

            sql = "SELECT t1.dataTime  , ";
            foreach (Model_AiInfo item in mailist)
            {
                sql += "max(case t1.pointName when '" + item.PpointName + "' then t1.data else null end) as '" + item.PpointName + "',";
            }

            sql += "min(case t1.isAlarm when 0 then 0 else 1 end) as 'isAlarm' " +
            "FROM " +
            "(select a.pointName, a.pointId, d.data, d.datatime, d.isAlarm from aiinfo a " +
            "join history_data_" + storageId + " d on a.pointId = d.pointId " +
            "where (a.pointType=1 or a.pointType=2) and  d.dataTime between '" + beginTime + "' and  '" + endTime + "') t1 GROUP BY t1.dataTime";
            if (pageIndexAndCount != null)
            {
                string page = pageIndexAndCount.Substring(0, pageIndexAndCount.LastIndexOf("."));
                string size = pageIndexAndCount.Substring(pageIndexAndCount.LastIndexOf(".") + 1, pageIndexAndCount.Length - (pageIndexAndCount.LastIndexOf(".") + 1));
                sql += " limit " + ((Convert.ToInt32(page) - 1) * Convert.ToInt32(size)) + "," + size + ";";
            }
            else
                sql += " ;";
            List<String[]> list = _SqlHelp.Execute4ListOfObject(sql);

            return list;
        }
        /// <summary>
        /// 根据运单物流id查询签收图片
        /// </summary>
        /// <param name="BaseId"></param>
        /// <returns></returns>
        public static List<Model_Waybill_Postback_Pic> GetWaybillPostbackPics(int BaseId)
        {
            string sql = "";
            if (BaseId!=0)
            {
                sql = "select id, concat(baseId,'') as baseId,picName from waybill_postback_pic where baseId=?baseId ;";
            }
            MySqlParameter[] para = new MySqlParameter[1];
            para[0] = new MySqlParameter("baseId", BaseId);
            List<Model_Waybill_Postback_Pic> list = _SqlHelp.ExecuteObjects<Model_Waybill_Postback_Pic>(sql,para);
            return list;
        }

        /// <summary>
        /// 查询冷链数据总数
        /// </summary>
        /// <param name="storageId"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public static int GetWaybillNodeHistDataCount(int storageId, string beginTime, string endTime)
        {
            string sql = "";
            //根据运单物流节点中的冷库、车载存id查询探头
            sql = "select *,pointName as PpointName from aiinfo where storageId=?storageId and (pointType=1 or pointType=2)";
            MySqlParameter[] ainn = new MySqlParameter[1];
            ainn[0] = new MySqlParameter("storageId", storageId);
            List<Model_AiInfo> mailist = _SqlHelp.ExecuteObjects<Model_AiInfo>(sql, ainn);

            sql = "select count(*) from (SELECT t1.dataTime  , ";
            foreach (Model_AiInfo item in mailist)
            {
                sql += "max(case t1.pointName when '" + item.PpointName + "' then t1.data else null end) as '" + item.PpointName + "',";
            }
            sql += "min(case t1.isAlarm when 0 then 0 else 1 end) as 'isAlarm' " +
            "FROM " +
            "(select a.pointName, a.pointId, d.data, d.datatime, d.isAlarm from aiinfo a " +
            "join history_data_" + storageId + " d on a.pointId = d.pointId " +
            "where (a.pointType=1 or a.pointType=2) and  d.dataTime between '" + beginTime + "' and  '" + endTime + "') t1 GROUP BY t1.dataTime) a ;";
            
            return Convert.ToInt32(_SqlHelp.ExecuteScalar(sql)) ;
        }

        #region 根据第三方运单号查询显示相对应的运单信息
        /// <summary>
        /// 
        /// </summary>
        /// <param name="waybillNumber">运单号</param>
        /// <param name="pageIndexAndCount">分页1.50</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="customerId">客户id</param>
        /// <param name="roles">角色 0上游发货单位 1</param>
        /// <returns></returns>
        public static List<Model_Waybill_Base> GetWaybillThirdPartyList(string waybillNumber, string pageIndexAndCount, string startTime, string endTime, int customerId, int roles)
        {
            List<Model_Waybill_Base> list = null;
            if (roles == 0)
                list = GetThirdPartyTwo(waybillNumber, pageIndexAndCount, startTime, endTime, customerId);
            if (list.Count == 0)
            {
                Model_Huadong_Tmsorder_Waybillbase mhuadong = GetHuadongWaybillbase(waybillNumber);
                if (mhuadong != null)
                {
                    if (roles == 0)
                        list = GetThirdPartyTwo(mhuadong.number, pageIndexAndCount, startTime, endTime, customerId);
                }
            }
            return list;
        }
        /// <summary>
        /// 查询发货单位或是收货单位运单信息
        /// </summary>
        /// <param name="waybillNumber">运单号</param>
        /// <param name="pageIndexAndCount">分页1.50</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="customerId">客户id</param>
        /// <returns></returns>
        public static List<Model_Waybill_Base> GetThirdPartyTwo(string waybillNumber, string pageIndexAndCount, string startTime, string endTime, int customerId)
        {
            Model_Customer cust = null;
            if (customerId != 0)
            {
                string role = "select role from customer where id=" + customerId + ";";
                cust = _SqlHelp.ExecuteObject<Model_Customer>(role);
                if (cust == null)
                    throw new Exception("操作失败");
            }
            string sql = "select id,number,senderId,senderOrg,senderPerson,senderTel,senderAddress,receiverId,receiverOrg,receiverPerson,receiverTel,receiverAddress," +
                    "billingCount,stage,beginAt,signinAt,picPostbackAt,company from waybill_base  ";

            //运单编号
            if (waybillNumber != null && customerId == 0 && startTime == null && endTime == null)
            {
                sql += "where  number=?number ";
            }
            //运单编号和客户名称
            if (waybillNumber != null && customerId != 0 && startTime == null && endTime == null)
            {
                if (cust.Role == Model.MyEnum.Enum_Role.Sender)
                    sql += "where  number=?number and senderId=?senderId  ";
                else if (cust.Role == Model.MyEnum.Enum_Role.Receiver)
                    sql += "where  number=?number and receiverId=?receiverId ";
            }
            //运单编号、客户名称、开始时间
            if (waybillNumber != null && customerId != 0 && startTime != null && endTime == null)
            {
                if (cust.Role == Model.MyEnum.Enum_Role.Sender)
                    sql += "where  number=?number and senderId=?senderId and beginAt>=?beginAt ";
                else if (cust.Role == Model.MyEnum.Enum_Role.Receiver)
                    sql += "where  number=?number and receiverId=?receiverId and beginAt>=?beginAt ";
            }
            //运单编号、客户名称、开始时间、结束时间
            if (waybillNumber != null && customerId != 0 && startTime != null && endTime != null)
            {
                if (cust.Role == Model.MyEnum.Enum_Role.Sender)
                    sql += "where  number=?number and  senderId=?senderId and beginAt>=?beginAt and signinAt<=?signinAt ";
                else if (cust.Role == Model.MyEnum.Enum_Role.Receiver)
                    sql += "where  number=?number and receiverId=?receiverId  and beginAt>=?beginAt and signinAt<=?signinAt ";
            }
            //运单编号、开始时间
            if (waybillNumber != null && customerId == 0 && startTime != null && endTime == null)
            {
                sql += "where  number=?number  and beginAt>=?beginAt ";
            }
            //运单编号、结束时间
            if (waybillNumber != null && customerId == 0 && startTime == null && endTime != null)
            {
                sql += "where  number=?number and signinAt<=?signinAt ";
            }
            //客户名称
            if (waybillNumber == null && customerId != 0 && startTime == null && endTime == null)
            {
                if (cust.Role == Model.MyEnum.Enum_Role.Sender)
                    sql += "where    senderId=?senderId ";
                else if (cust.Role == Model.MyEnum.Enum_Role.Receiver)
                    sql += "where   receiverId=?receiverId   ";
            }
            //客户名称、开始时间
            if (waybillNumber == null && customerId != 0 && startTime != null && endTime == null)
            {
                if (cust.Role == Model.MyEnum.Enum_Role.Sender)
                    sql += "where   senderId=?senderId and beginAt>=?beginAt ";
                else if (cust.Role == Model.MyEnum.Enum_Role.Receiver)
                    sql += "where  receiverId=?receiverId  and beginAt>=?beginAt ";
            }
            //客户名称、结束时间
            if (waybillNumber == null && customerId != 0 && startTime == null && endTime != null)
            {
                if (cust.Role == Model.MyEnum.Enum_Role.Sender)
                    sql += "where   senderId=?senderId and signinAt<=?signinAt ";
                else if (cust.Role == Model.MyEnum.Enum_Role.Receiver)
                    sql += "where  receiverId=?receiverId  and signinAt<=?signinAt ";
            }
            //客户名称、开始时间、结束时间
            if (waybillNumber == null && customerId != 0 && startTime != null && endTime != null)
            {
                if (cust.Role == Model.MyEnum.Enum_Role.Sender)
                    sql += "where  senderId=?senderId and beginAt>=?beginAt and signinAt<=?signinAt ";
                else if (cust.Role == Model.MyEnum.Enum_Role.Receiver)
                    sql += "where receiverId=?receiverId  and beginAt>=?beginAt and signinAt<=?signinAt ";
            }
            //开始时间
            if (waybillNumber == null && customerId == 0 && startTime != null && endTime == null)
            {
                sql += "where   beginAt>=?beginAt ";
            }
            //开始时间、结束时间
            if (waybillNumber == null && customerId == 0 && startTime != null && endTime != null)
            {
                sql += "where beginAt>=?beginAt and signinAt<=?signinAt ";
            }
            //结束时间
            if (waybillNumber == null && customerId == 0 && startTime == null && endTime != null)
            {
                sql += "where  signinAt<=?signinAt ";
            }
            //运单编号、客户名称、结束时间
            if (waybillNumber != null && endTime != null && customerId != 0 && startTime == null)
            {
                if (cust.Role == Model.MyEnum.Enum_Role.Sender)
                    sql += " where number=?number and signinAt<=?signinAt and  senderId=?senderId ";
                else if (cust.Role == Model.MyEnum.Enum_Role.Receiver)
                    sql += " where number=?number and signinAt<=?signinAt and receiverId=?receiverId  ";
            }
            //运单编号、开始时间、结束时间
            if (waybillNumber != null && customerId == 0 && startTime != null && endTime != null)
            {
                sql += "where  number=?number and beginAt>=?beginAt and signinAt<=?signinAt ";
            }
            if (pageIndexAndCount != null)
            {
                string page = pageIndexAndCount.Substring(0, pageIndexAndCount.LastIndexOf("."));
                string size = pageIndexAndCount.Substring(pageIndexAndCount.LastIndexOf(".") + 1, pageIndexAndCount.Length - (pageIndexAndCount.LastIndexOf(".") + 1));
                sql += " order by beginAt desc limit " + ((Convert.ToInt32(page) - 1) * Convert.ToInt32(size)) + "," + size + ";";
            }
            else
                sql += " ;";

            MySqlParameter[] para = new MySqlParameter[5];
            para[0] = new MySqlParameter("number", waybillNumber);
            para[1] = new MySqlParameter("beginAt", startTime);
            para[2] = new MySqlParameter("signinAt", endTime);
            para[3] = new MySqlParameter("receiverId", customerId);
            para[4] = new MySqlParameter("senderId", customerId);

            List<Model_Waybill_Base> list = _SqlHelp.ExecuteObjects<Model_Waybill_Base>(sql, para);

            return list;
        }
        /// <summary>
        /// 根据base表运单号查询关系表信息
        /// </summary>
        /// <param name="waybillNumber"></param>
        /// <returns></returns>
        public static Model_Huadong_Tmsorder_Waybillbase GetHuadongWaybillbase(string waybillNumber)
        {
            string sql = string.Format("select * from huadong_tmsorder_waybillbase where relationId='{0}' ",waybillNumber);
            Model_Huadong_Tmsorder_Waybillbase huadong = _SqlHelp.ExecuteObject<Model_Huadong_Tmsorder_Waybillbase>(sql);
            return huadong;
        }
  
        public static int GetWaybillThirdPartyListCount(string waybillNumber, string startTime, string endTime, int customerId, int roles)
        {
            int result = 0;
            if (roles == 0)
                result = GetThirdPartyListTwoCount(waybillNumber, startTime, endTime, customerId);
            if (result==0)
            {
                Model_Huadong_Tmsorder_Waybillbase mhuadong = GetHuadongWaybillbase(waybillNumber);
                if (mhuadong != null)
                {
                    if (roles == 0)
                        result = GetThirdPartyListTwoCount(mhuadong.number, startTime, endTime, customerId);
                }
            }
            return result;
        }
        /// <summary>
        /// 查询发货单位或是收货单位运单信息总数
        /// </summary>
        /// <param name="waybillNumber"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public static int GetThirdPartyListTwoCount(string waybillNumber, string startTime, string endTime, int customerId)
        {
            Model_Customer cust = null;
            if (customerId != 0)
            {
                string role = "select role from customer where id=" + customerId + ";";
                cust = _SqlHelp.ExecuteObject<Model_Customer>(role);
                if (cust == null)
                    throw new Exception("操作失败");
            }
            string sql = "select count(*) from waybill_base  ";
            //运单编号
            if (waybillNumber != null && customerId == 0 && startTime == null && endTime == null)
            {
                sql += "where  number=?number ";
            }
            //运单编号和客户名称
            if (waybillNumber != null && customerId != 0 && startTime == null && endTime == null)
            {
                if (cust.Role == Model.MyEnum.Enum_Role.Sender)
                    sql += "where  number=?number and senderId=?senderId  ";
                else if (cust.Role == Model.MyEnum.Enum_Role.Receiver)
                    sql += "where  number=?number and receiverId=?receiverId ";
            }
            //运单编号、客户名称、开始时间
            if (waybillNumber != null && customerId != 0 && startTime != null && endTime == null)
            {
                if (cust.Role == Model.MyEnum.Enum_Role.Sender)
                    sql += "where  number=?number and senderId=?senderId and beginAt>=?beginAt ";
                else if (cust.Role == Model.MyEnum.Enum_Role.Receiver)
                    sql += "where  number=?number and receiverId=?receiverId and beginAt>=?beginAt ";
            }
            //运单编号、客户名称、开始时间、结束时间
            if (waybillNumber != null && customerId != 0 && startTime != null && endTime != null)
            {
                if (cust.Role == Model.MyEnum.Enum_Role.Sender)
                    sql += "where  number=?number and  senderId=?senderId and beginAt>=?beginAt and signinAt<=?signinAt ";
                else if (cust.Role == Model.MyEnum.Enum_Role.Receiver)
                    sql += "where  number=?number and receiverId=?receiverId  and beginAt>=?beginAt and signinAt<=?signinAt ";
            }
            //运单编号、开始时间
            if (waybillNumber != null && customerId == 0 && startTime != null && endTime == null)
            {
                sql += "where  number=?number  and beginAt>=?beginAt ";
            }
            //运单编号、结束时间
            if (waybillNumber != null && customerId == 0 && startTime == null && endTime != null)
            {
                sql += "where  number=?number and signinAt<=?signinAt ";
            }
            //客户名称
            if (waybillNumber == null && customerId != 0 && startTime == null && endTime == null)
            {
                if (cust.Role == Model.MyEnum.Enum_Role.Sender)
                    sql += "where    senderId=?senderId ";
                else if (cust.Role == Model.MyEnum.Enum_Role.Receiver)
                    sql += "where   receiverId=?receiverId   ";
            }
            //客户名称、开始时间
            if (waybillNumber == null && customerId != 0 && startTime != null && endTime == null)
            {
                if (cust.Role == Model.MyEnum.Enum_Role.Sender)
                    sql += "where   senderId=?senderId and beginAt>=?beginAt ";
                else if (cust.Role == Model.MyEnum.Enum_Role.Receiver)
                    sql += "where  receiverId=?receiverId  and beginAt>=?beginAt ";
            }
            //客户名称、结束时间
            if (waybillNumber == null && customerId != 0 && startTime == null && endTime != null)
            {
                if (cust.Role == Model.MyEnum.Enum_Role.Sender)
                    sql += "where   senderId=?senderId and signinAt<=?signinAt ";
                else if (cust.Role == Model.MyEnum.Enum_Role.Receiver)
                    sql += "where  receiverId=?receiverId  and signinAt<=?signinAt ";
            }
            //客户名称、开始时间、结束时间
            if (waybillNumber == null && customerId != 0 && startTime != null && endTime != null)
            {
                if (cust.Role == Model.MyEnum.Enum_Role.Sender)
                    sql += "where  senderId=?senderId and beginAt>=?beginAt and signinAt<=?signinAt ";
                else if (cust.Role == Model.MyEnum.Enum_Role.Receiver)
                    sql += "where receiverId=?receiverId  and beginAt>=?beginAt and signinAt<=?signinAt ";
            }
            //开始时间
            if (waybillNumber == null && customerId == 0 && startTime != null && endTime == null)
            {
                sql += "where   beginAt>=?beginAt ";
            }
            //开始时间、结束时间
            if (waybillNumber == null && customerId == 0 && startTime != null && endTime != null)
            {
                sql += "where beginAt>=?beginAt and signinAt<=?signinAt ";
            }
            //结束时间
            if (waybillNumber == null && customerId == 0 && startTime == null && endTime != null)
            {
                sql += "where  signinAt<=?signinAt ";
            }
            //运单编号、客户名称、结束时间
            if (waybillNumber != null && endTime != null && customerId != 0 && startTime == null)
            {
                if (cust.Role == Model.MyEnum.Enum_Role.Sender)
                    sql += " where number=?number and signinAt<=?signinAt and  senderId=?senderId ";
                else if (cust.Role == Model.MyEnum.Enum_Role.Receiver)
                    sql += " where number=?number and signinAt<=?signinAt and receiverId=?receiverId  ";
            }
            //运单编号、开始时间、结束时间
            if (waybillNumber != null && customerId == 0 && startTime != null && endTime != null)
            {
                sql += "where  number=?number and beginAt>=?beginAt and signinAt<=?signinAt ";
            }
            sql += " ;";

            MySqlParameter[] para = new MySqlParameter[5];
            para[0] = new MySqlParameter("number", waybillNumber);
            para[1] = new MySqlParameter("beginAt", startTime);
            para[2] = new MySqlParameter("signinAt", endTime);
            para[3] = new MySqlParameter("receiverId", customerId);
            para[4] = new MySqlParameter("senderId", customerId);

            return Convert.ToInt32(_SqlHelp.ExecuteScalar(sql, para));

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="waybillNumber">运单号</param>
        /// <param name="pageIndexAndCount">分页1.50</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="customerId">客户id</param>
        /// <param name="roles">角色 0上游发货单位 1</param>
        /// <returns></returns>
        public static List<Model_Waybill_Base> GetWaybillThirdPartyVagueList(string waybillNumber, string pageIndexAndCount, string startTime, string endTime, int customerId)
        {
            Model_Customer cust = null;
            if (customerId != 0)
            {
                string role = "select role from customer where id=" + customerId + ";";
                cust = _SqlHelp.ExecuteObject<Model_Customer>(role);
                if (cust == null)
                    throw new Exception("操作失败");
            }
            
            string sql = "select w.id,w.number,w.senderId,w.senderOrg,w.senderPerson,w.senderTel,w.senderAddress,w.receiverId,w.receiverOrg,w.receiverPerson,w.receiverTel,w.receiverAddress," +
                    "w.billingCount,w.stage,w.beginAt,w.signinAt,w.picPostbackAt,w.company from waybill_base as w  ";
            
            //运单编号
            if (waybillNumber != null && customerId == 0 && startTime == null && endTime == null)
            {
                sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in(select number from huadong_tmsorder_waybillbase where relationId like '%{0}%')) ", waybillNumber);
            }
            //运单编号和客户名称
            if (waybillNumber != null && customerId != 0 && startTime == null && endTime == null)
            {
                if (cust.Role == Model.MyEnum.Enum_Role.Sender)
                    sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in(select number from huadong_tmsorder_waybillbase where relationId like '%{0}%')) and w.senderId={1}  ", waybillNumber, customerId);
                else if (cust.Role == Model.MyEnum.Enum_Role.Receiver)
                    sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in(select number from huadong_tmsorder_waybillbase where relationId like '%{0}%')) and w.receiverId={1} ", waybillNumber, customerId);
            }
            //运单编号、客户名称、开始时间
            if (waybillNumber != null && customerId != 0 && startTime != null && endTime == null)
            {
                if (cust.Role == Model.MyEnum.Enum_Role.Sender)
                    sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in(select number from huadong_tmsorder_waybillbase where relationId like '%{0}%')) and w.senderId={1} and w.beginAt>='{2}' ", waybillNumber, customerId, startTime);
                else if (cust.Role == Model.MyEnum.Enum_Role.Receiver)
                    sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in(select number from huadong_tmsorder_waybillbase where relationId like '%{0}%')) and w.receiverId={1} and w.beginAt>='{2}' ", waybillNumber, customerId, startTime);
            }
            //运单编号、客户名称、开始时间、结束时间
            if (waybillNumber != null && customerId != 0 && startTime != null && endTime != null)
            {
                if (cust.Role == Model.MyEnum.Enum_Role.Sender)
                    sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in(select number from huadong_tmsorder_waybillbase where relationId like '%{0}%')) and  w.senderId={1} and w.beginAt>='{2}' and w.signinAt<='{3}' ", waybillNumber, customerId, startTime, endTime);
                else if (cust.Role == Model.MyEnum.Enum_Role.Receiver)
                    sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in(select number from huadong_tmsorder_waybillbase where relationId like '%{0}%')) and w.receiverId={1} and w.beginAt>='{2}' and w.signinAt<='{3}' ", waybillNumber, customerId, startTime, endTime);
            }
            //运单编号、开始时间
            if (waybillNumber != null && customerId == 0 && startTime != null && endTime == null)
            {
                sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in(select number from huadong_tmsorder_waybillbase where relationId like '%{0}%')) and w.beginAt>='{1}' ", waybillNumber, startTime);
            }
            //运单编号、结束时间
            if (waybillNumber != null && customerId == 0 && startTime == null && endTime != null)
            {
                sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in(select number from huadong_tmsorder_waybillbase where relationId like '%{0}%')) and w.signinAt<='{1}' ", waybillNumber, endTime);
            }
            //客户名称
            if (waybillNumber == null && customerId != 0 && startTime == null && endTime == null)
            {
                if (cust.Role == Model.MyEnum.Enum_Role.Sender)
                    sql += string.Format("where w.senderId={0} ", customerId);
                else if (cust.Role == Model.MyEnum.Enum_Role.Receiver)
                    sql += string.Format("where w.receiverId={0} ", customerId);
            }
            //客户名称、开始时间
            if (waybillNumber == null && customerId != 0 && startTime != null && endTime == null)
            {
                if (cust.Role == Model.MyEnum.Enum_Role.Sender)
                    sql += string.Format("where w.senderId={0} and w.beginAt>='{1}' ", customerId, startTime);
                else if (cust.Role == Model.MyEnum.Enum_Role.Receiver)
                    sql += string.Format("where w.receiverId={0} and w.beginAt>='{1}' ", customerId, startTime);
            }
            //客户名称、结束时间
            if (waybillNumber == null && customerId != 0 && startTime == null && endTime != null)
            {
                if (cust.Role == Model.MyEnum.Enum_Role.Sender)
                    sql += string.Format("where w.senderId={0} and w.signinAt<='{1}' ", customerId, endTime);
                else if (cust.Role == Model.MyEnum.Enum_Role.Receiver)
                    sql += string.Format("where  w.receiverId={0} and w.signinAt<='{1}' ", customerId, endTime);
            }
            //客户名称、开始时间、结束时间
            if (waybillNumber == null && customerId != 0 && startTime != null && endTime != null)
            {
                if (cust.Role == Model.MyEnum.Enum_Role.Sender)
                    sql += string.Format("where  w.senderId={0} and w.beginAt>='{1}' and w.signinAt<='{2}' ", customerId, startTime, endTime);
                else if (cust.Role == Model.MyEnum.Enum_Role.Receiver)
                    sql += string.Format("where w.receiverId={0} and w.beginAt>='{1}' and w.signinAt<='{2}' ", customerId, startTime, endTime);
            }
            //开始时间
            if (waybillNumber == null && customerId == 0 && startTime != null && endTime == null)
            {
                sql += string.Format("where w.beginAt>='{0}' ", startTime);
            }
            //开始时间、结束时间
            if (waybillNumber == null && customerId == 0 && startTime != null && endTime != null)
            {
                sql += string.Format("where w.beginAt>='{0}' and w.signinAt<='{1}' ", startTime, endTime);
            }
            //结束时间
            if (waybillNumber == null && customerId == 0 && startTime == null && endTime != null)
            {
                sql += string.Format("where w.signinAt<='{0}' ", endTime);
            }
            //运单编号、客户名称、结束时间
            if (waybillNumber != null && endTime != null && customerId != 0 && startTime == null)
            {
                if (cust.Role == Model.MyEnum.Enum_Role.Sender)
                    sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in(select number from huadong_tmsorder_waybillbase where relationId like '%{0}%')) and w.signinAt<='{1}' and  w.senderId={2} ", waybillNumber, endTime, customerId);
                else if (cust.Role == Model.MyEnum.Enum_Role.Receiver)
                    sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in(select number from huadong_tmsorder_waybillbase where relationId like '%{0}%')) and w.signinAt<='{1}' and w.receiverId={2} ", waybillNumber, endTime, customerId);
            }
            //运单编号、开始时间、结束时间
            if (waybillNumber != null && customerId == 0 && startTime != null && endTime != null)
            {
                sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in(select number from huadong_tmsorder_waybillbase where relationId like '%{0}%')) and w.beginAt>='{1}' and w.signinAt<='{2}' ", waybillNumber, startTime, endTime);
            }
            if (pageIndexAndCount != null)
            {
                string page = pageIndexAndCount.Substring(0, pageIndexAndCount.LastIndexOf("."));
                string size = pageIndexAndCount.Substring(pageIndexAndCount.LastIndexOf(".") + 1, pageIndexAndCount.Length - (pageIndexAndCount.LastIndexOf(".") + 1));
                sql += " order by w.beginAt desc limit " + ((Convert.ToInt32(page) - 1) * Convert.ToInt32(size)) + "," + size + ";";
            }
            else
                sql += " ;";
            
            List<Model_Waybill_Base> list = _SqlHelp.ExecuteObjects<Model_Waybill_Base>(sql);

            return list;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="waybillNumber">运单号</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="customerId">客户id</param>
        /// <param name="roles">角色 0上游发货单位 1</param>
        /// <returns></returns>
        public static int GetWaybillThirdPartyListVagueCount(string waybillNumber, string startTime, string endTime, int customerId)
        {
            Model_Customer cust = null;
            if (customerId != 0)
            {
                string role = "select role from customer where id=" + customerId + ";";
                cust = _SqlHelp.ExecuteObject<Model_Customer>(role);
                if (cust == null)
                    throw new Exception("操作失败");
            }
            string sql = string.Format ("select count(*) from waybill_base as w ");
            //运单编号
            if (waybillNumber != null && customerId == 0 && startTime == null && endTime == null)
            {
                sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in( select number from huadong_tmsorder_waybillbase where relationId like '%{0}%' )) ", waybillNumber);
            }
            //运单编号和客户名称
            if (waybillNumber != null && customerId != 0 && startTime == null && endTime == null)
            {
                if (cust.Role == Model.MyEnum.Enum_Role.Sender)
                    sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in( select number from huadong_tmsorder_waybillbase where relationId like '%{0}%' )) and w.senderId={1}  ", waybillNumber, customerId);
                else if (cust.Role == Model.MyEnum.Enum_Role.Receiver)
                    sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in( select number from huadong_tmsorder_waybillbase where relationId like '%{0}%' )) and w.receiverId={1} ", waybillNumber, customerId);
            }
            //运单编号、客户名称、开始时间
            if (waybillNumber != null && customerId != 0 && startTime != null && endTime == null)
            {
                if (cust.Role == Model.MyEnum.Enum_Role.Sender)
                    sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in( select number from huadong_tmsorder_waybillbase where relationId like '%{0}%' )) and w.senderId={1} and w.beginAt>='{2}' ", waybillNumber, customerId, startTime);
                else if (cust.Role == Model.MyEnum.Enum_Role.Receiver)
                    sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in( select number from huadong_tmsorder_waybillbase where relationId like '%{0}%' )) and w.receiverId={1} and w.beginAt>='{2}' ", waybillNumber, customerId, startTime);
            }
            //运单编号、客户名称、开始时间、结束时间
            if (waybillNumber != null && customerId != 0 && startTime != null && endTime != null)
            {
                if (cust.Role == Model.MyEnum.Enum_Role.Sender)
                    sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in( select number from huadong_tmsorder_waybillbase where relationId like '%{0}%' )) and w.senderId={1} and w.beginAt>='{2}' and w.signinAt<='{3}' ", waybillNumber, customerId, startTime, endTime);
                else if (cust.Role == Model.MyEnum.Enum_Role.Receiver)
                    sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in( select number from huadong_tmsorder_waybillbase where relationId like '%{0}%' )) and w.receiverId={1} and w.beginAt>='{2}' and w.signinAt<='{3}' ", waybillNumber, customerId, startTime, endTime);
            }
            //运单编号、开始时间
            if (waybillNumber != null && customerId == 0 && startTime != null && endTime == null)
            {
                sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in( select number from huadong_tmsorder_waybillbase where relationId like '%{0}%' ))  and w.beginAt>='{1}' ", waybillNumber, startTime);
            }
            //运单编号、结束时间
            if (waybillNumber != null && customerId == 0 && startTime == null && endTime != null)
            {
                sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in( select number from huadong_tmsorder_waybillbase where relationId like '%{0}%' )) and w.signinAt<='{1}' ", waybillNumber, endTime);
            }
            //客户名称
            if (waybillNumber == null && customerId != 0 && startTime == null && endTime == null)
            {
                if (cust.Role == Model.MyEnum.Enum_Role.Sender)
                    sql += string.Format("where w.senderId={0} ", customerId);
                else if (cust.Role == Model.MyEnum.Enum_Role.Receiver)
                    sql += string.Format("where w.receiverId={0} ", customerId);
            }
            //客户名称、开始时间
            if (waybillNumber == null && customerId != 0 && startTime != null && endTime == null)
            {
                if (cust.Role == Model.MyEnum.Enum_Role.Sender)
                    sql += string.Format("where w.senderId={0} and w.beginAt>='{1}' ", customerId, startTime);
                else if (cust.Role == Model.MyEnum.Enum_Role.Receiver)
                    sql += string.Format("where w.receiverId={0} and w.beginAt>='{1}' ", customerId, startTime);
            }
            //客户名称、结束时间
            if (waybillNumber == null && customerId != 0 && startTime == null && endTime != null)
            {
                if (cust.Role == Model.MyEnum.Enum_Role.Sender)
                    sql += string.Format("where w.senderId={0} and w.signinAt<='{1}' ", customerId, endTime);
                else if (cust.Role == Model.MyEnum.Enum_Role.Receiver)
                    sql += string.Format("where w.receiverId={0} and w.signinAt<='{1}' ", customerId, endTime);
            }
            //客户名称、开始时间、结束时间
            if (waybillNumber == null && customerId != 0 && startTime != null && endTime != null)
            {
                if (cust.Role == Model.MyEnum.Enum_Role.Sender)
                    sql += string.Format("where w.senderId={0} and w.beginAt>='{1}' and w.signinAt<='{2}' ", customerId, startTime, endTime);
                else if (cust.Role == Model.MyEnum.Enum_Role.Receiver)
                    sql += string.Format("where w.receiverId={0} and w.beginAt>='{1}' and w.signinAt<='{2}' ", customerId, startTime, endTime);
            }
            //开始时间
            if (waybillNumber == null && customerId == 0 && startTime != null && endTime == null)
            {
                sql += string.Format("where w.beginAt>='{0}' ", startTime);
            }
            //开始时间、结束时间
            if (waybillNumber == null && customerId == 0 && startTime != null && endTime != null)
            {
                sql += string.Format("where w.beginAt>='{0}' and w.signinAt<='{1}' ", startTime, endTime);
            }
            //结束时间
            if (waybillNumber == null && customerId == 0 && startTime == null && endTime != null)
            {
                sql += string.Format("where w.signinAt<='{0}' ", endTime);
            }
            //运单编号、客户名称、结束时间
            if (waybillNumber != null && endTime != null && customerId != 0 && startTime == null)
            {
                if (cust.Role == Model.MyEnum.Enum_Role.Sender)
                    sql += string.Format(" left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in( select number from huadong_tmsorder_waybillbase where relationId like '%{0}%' )) and w.signinAt<='{1}' and  w.senderId={2} ", waybillNumber, endTime, customerId);
                else if (cust.Role == Model.MyEnum.Enum_Role.Receiver)
                    sql += string.Format(" left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in( select number from huadong_tmsorder_waybillbase where relationId like '%{0}%' )) and w.signinAt<='{1}' and w.receiverId={2} ", waybillNumber, endTime, customerId);
            }
            //运单编号、开始时间、结束时间
            if (waybillNumber != null && customerId == 0 && startTime != null && endTime != null)
            {
                sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in( select number from huadong_tmsorder_waybillbase where relationId like '%{0}%' )) and w.beginAt>='{1}' and w.signinAt<='{2}' ", waybillNumber, startTime, endTime);
            }
            sql += " ;";
            

            return Convert.ToInt32(_SqlHelp.ExecuteScalar(sql));

        }

        #endregion

        #region 根据区域显示下游客户
        /// <summary>
        /// 查询客户
        /// </summary>
        /// <param name="waybillNumber">运单号</param>
        /// <param name="pageIndexAndCount">分页1.10</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="senderId">上游客户id</param>
        /// <param name="receiverId">下游客户id</param>
        /// <returns></returns>
        public static List<Model_Waybill_Base> GetQueryClientsList(string waybillNumber, string pageIndexAndCount, string startTime, string endTime, int senderId,int receiverId)
        {
            string sql = "select w.id,w.number,w.senderId,w.senderOrg,w.senderPerson,w.senderTel,w.senderAddress,w.receiverId,w.receiverOrg,w.receiverPerson,w.receiverTel,w.receiverAddress," +
                    "w.billingCount,w.stage,w.beginAt,w.signinAt,w.picPostbackAt,w.company from waybill_base as w  ";

            //运单编号
            if (waybillNumber != null && senderId == 0 && receiverId == 0 && startTime == null && endTime == null)
            {
                sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in(select number from huadong_tmsorder_waybillbase where relationId like '%{0}%')) ", waybillNumber);
            }
            //运单编号和上游客户名称
            if (waybillNumber != null && senderId != 0 && receiverId == 0 && startTime == null && endTime == null)
            {
                sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in(select number from huadong_tmsorder_waybillbase where relationId like '%{0}%')) and w.senderId={1}  ", waybillNumber, senderId);
            }
            //运单编号和下游客户名称
            if (waybillNumber != null && senderId == 0 && receiverId != 0 && startTime == null && endTime == null)
            {
                sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in(select number from huadong_tmsorder_waybillbase where relationId like '%{0}%')) and w.receiverId={1}  ", waybillNumber, receiverId);
            }
            //运单编号、上游客户名称、下游客户名称
            if (waybillNumber != null && senderId != 0 && receiverId != 0 && startTime == null && endTime == null)
            {
                sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in(select number from huadong_tmsorder_waybillbase where relationId like '%{0}%')) and w.senderId={1} and w.receiverId={2}  ", waybillNumber, senderId, receiverId);
            }
            //运单编号、上游客户名称、下游客户名称、开始时间
            if (waybillNumber != null && senderId != 0 && receiverId != 0 && startTime != null && endTime == null)
            {
                sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in(select number from huadong_tmsorder_waybillbase where relationId like '%{0}%')) and w.senderId={1} and w.receiverId={2} and w.beginAt>='{3}' ", waybillNumber, senderId, receiverId, startTime);
            }
            //运单编号、上游客户名称、下游客户名称、开始时间、结束时间
            if (waybillNumber != null && senderId != 0 && receiverId != 0 && startTime != null && endTime != null)
            {
                sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in(select number from huadong_tmsorder_waybillbase where relationId like '%{0}%')) and  w.senderId={1} and  w.receiverId={2} and w.beginAt>='{3}' and w.signinAt<='{4}' ", waybillNumber, senderId, receiverId, startTime, endTime);

            }
            //运单编号、上游客户名称、开始时间
            if (waybillNumber != null && senderId != 0 && receiverId == 0 && startTime != null && endTime == null)
            {
                sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in(select number from huadong_tmsorder_waybillbase where relationId like '%{0}%')) and w.senderId={1} and w.beginAt>='{2}' ", waybillNumber, senderId, startTime);

            }
            //运单编号、上游客户名称、结束时间
            if (waybillNumber != null && endTime != null && senderId != 0 && receiverId == 0 && startTime == null)
            {
                sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in(select number from huadong_tmsorder_waybillbase where relationId like '%{0}%')) and w.signinAt<='{1}' and w.senderId={2} ", waybillNumber, endTime, senderId);
            }
            //运单编号、上游客户名称、开始时间、结束时间
            if (waybillNumber != null && senderId != 0 && receiverId == 0 && startTime != null && endTime != null)
            {
                sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in(select number from huadong_tmsorder_waybillbase where relationId like '%{0}%')) and  w.senderId={1} and w.beginAt>='{2}' and w.signinAt<='{3}' ", waybillNumber, senderId, startTime, endTime);

            }
            //运单编号、开始时间
            if (waybillNumber != null && senderId == 0 && receiverId == 0 && startTime != null && endTime == null)
            {
                sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in(select number from huadong_tmsorder_waybillbase where relationId like '%{0}%')) and w.beginAt>='{1}' ", waybillNumber, startTime);
            }
            //运单编号、结束时间
            if (waybillNumber != null && senderId == 0 && receiverId == 0 && startTime == null && endTime != null)
            {
                sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in(select number from huadong_tmsorder_waybillbase where relationId like '%{0}%')) and w.signinAt<='{1}' ", waybillNumber, endTime);
            }

            //上游客户名称
            if (waybillNumber == null && senderId != 0 && receiverId == 0 && startTime == null && endTime == null)
            {
                sql += string.Format("where w.senderId={0} ", senderId);
            }
            //上游客户名称、下游客户名称
            if (waybillNumber == null && senderId != 0 && receiverId != 0 && startTime == null && endTime == null)
            {
                sql += string.Format("where w.senderId={0} and w.receiverId={1} ", senderId, receiverId);
            }
            //上游客户名称、开始时间
            if (waybillNumber == null && senderId != 0 && receiverId == 0 && startTime != null && endTime == null)
            {
                sql += string.Format("where w.senderId={0} and w.beginAt>='{1}' ", senderId, startTime);
            }
            //上游客户名称、结束时间
            if (waybillNumber == null && senderId != 0 && receiverId == 0 && startTime == null && endTime != null)
            {
                sql += string.Format("where w.senderId={0} and w.signinAt<='{1}' ", senderId, endTime);
            }
            //上游客户名称、开始时间、结束时间
            if (waybillNumber == null && senderId != 0 && receiverId == 0 && startTime != null && endTime != null)
            {
                sql += string.Format("where  w.senderId={0} and w.beginAt>='{1}' and w.signinAt<='{2}' ", senderId, startTime, endTime);
            }
            //上游客户名称、下游客户名称、开始时间
            if (waybillNumber == null && senderId != 0 && receiverId != 0 && startTime != null && endTime == null)
            {
                sql += string.Format("where w.senderId={0} and w.receiverId={1} and w.beginAt>='{2}' ", senderId, receiverId, startTime);
            }
            //上游客户名称、下游客户名称、结束时间
            if (waybillNumber == null && senderId != 0 && receiverId != 0 && startTime == null && endTime != null)
            {
                sql += string.Format("where w.senderId={0} and w.receiverId={1} and w.signinAt<='{2}' ", senderId, receiverId, endTime);
            }
            //上游客户名称、下游客户名称、开始时间、结束时间
            if (waybillNumber == null && senderId != 0 && receiverId != 0 && startTime != null && endTime != null)
            {
                sql += string.Format("where w.senderId={0} and w.receiverId={1} and w.beginAt>='{2}' and  w.signinAt<='{3}' ", senderId, receiverId, startTime, endTime);
            }
            //下游客户名称
            if (waybillNumber == null && senderId == 0 && receiverId != 0 && startTime == null && endTime == null)
            {
                sql += string.Format("where w.receiverId={0} ", receiverId);
            }
            //下游客户名称、开始时间
            if (waybillNumber == null && senderId == 0 && receiverId != 0 && startTime != null && endTime == null)
            {
                sql += string.Format("where w.receiverId={0} and w.beginAt>='{1}' ", receiverId, startTime);
            }
            //下游客户名称、结束时间
            if (waybillNumber == null && senderId == 0 && receiverId != 0 && startTime == null && endTime != null)
            {
                sql += string.Format("where w.receiverId={0} and w.signinAt<='{1}' ", receiverId, endTime);
            }
            //下游客户名称、开始时间、结束时间
            if (waybillNumber == null && senderId == 0 && receiverId != 0 && startTime != null && endTime != null)
            {
                sql += string.Format("where  w.receiverId={0} and w.beginAt>='{1}' and w.signinAt<='{2}' ", receiverId, startTime, endTime);
            }
            //开始时间
            if (waybillNumber == null && senderId == 0 && receiverId == 0 && startTime != null && endTime == null)
            {
                sql += string.Format("where w.beginAt>='{0}' ", startTime);
            }
            //开始时间、结束时间
            if (waybillNumber == null && senderId == 0 && receiverId == 0 && startTime != null && endTime != null)
            {
                sql += string.Format("where w.beginAt>='{0}' and w.signinAt<='{1}' ", startTime, endTime);
            }
            //结束时间
            if (waybillNumber == null && senderId == 0 && receiverId == 0 && startTime == null && endTime != null)
            {
                sql += string.Format("where w.signinAt<='{0}' ", endTime);
            }
            //运单编号、下游客户名称、开始时间
            if (waybillNumber != null && senderId == 0 && receiverId != 0 && startTime != null && endTime == null)
            {
                sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in(select number from huadong_tmsorder_waybillbase where relationId like '%{0}%')) and w.beginAt>='{1}' and  w.receiverId={2} ", waybillNumber, startTime, receiverId);

            }
            //运单编号、下游客户名称、结束时间
            if (waybillNumber != null && endTime != null && senderId == 0 && receiverId != 0 && startTime == null)
            {
                sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in(select number from huadong_tmsorder_waybillbase where relationId like '%{0}%')) and w.signinAt<='{1}' and w.receiverId={2} ", waybillNumber, endTime, receiverId);
            }
            //运单编号、下游客户名称、开始时间,结束时间
            if (waybillNumber != null && senderId == 0 && receiverId != 0 && startTime != null && endTime != null)
            {
                sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in(select number from huadong_tmsorder_waybillbase where relationId like '%{0}%')) and w.receiverId={1} and w.beginAt>='{2}' and w.signinAt<='{3}'  ", waybillNumber, receiverId, startTime, endTime);

            }
            //运单编号、开始时间、结束时间
            if (waybillNumber != null && senderId == 0 && receiverId == 0 && startTime != null && endTime != null)
            {
                sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in(select number from huadong_tmsorder_waybillbase where relationId like '%{0}%')) and w.beginAt>='{1}' and w.signinAt<='{2}' ", waybillNumber, startTime, endTime);
            }
            if (pageIndexAndCount != null)
            {
                string page = pageIndexAndCount.Substring(0, pageIndexAndCount.LastIndexOf("."));
                string size = pageIndexAndCount.Substring(pageIndexAndCount.LastIndexOf(".") + 1, pageIndexAndCount.Length - (pageIndexAndCount.LastIndexOf(".") + 1));
                sql += " order by w.beginAt desc limit " + ((Convert.ToInt32(page) - 1) * Convert.ToInt32(size)) + "," + size + ";";
            }
            else
                sql += " ;";

            List<Model_Waybill_Base> list = _SqlHelp.ExecuteObjects<Model_Waybill_Base>(sql);

            return list;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="waybillNumber">运单号</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="customerId">客户id</param>
        /// <param name="roles">角色 0上游发货单位 1</param>
        /// <returns></returns>
        public static int GetQueryClientsListCount(string waybillNumber, string startTime, string endTime, int senderId,int receiverId)
        {
            string sql = "select count(*) from waybill_base as w ";

            //运单编号
            if (waybillNumber != null && senderId == 0 && receiverId == 0 && startTime == null && endTime == null)
            {
                sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in(select number from huadong_tmsorder_waybillbase where relationId like '%{0}%')) ", waybillNumber);
            }
            //运单编号和上游客户名称
            if (waybillNumber != null && senderId != 0 && receiverId == 0 && startTime == null && endTime == null)
            {
                sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in(select number from huadong_tmsorder_waybillbase where relationId like '%{0}%')) and w.senderId={1}  ", waybillNumber, senderId);
            }
            //运单编号和下游客户名称
            if (waybillNumber != null && senderId == 0 && receiverId != 0 && startTime == null && endTime == null)
            {
                sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in(select number from huadong_tmsorder_waybillbase where relationId like '%{0}%')) and w.receiverId={1}  ", waybillNumber, receiverId);
            }
            //运单编号、上游客户名称、下游客户名称
            if (waybillNumber != null && senderId != 0 && receiverId != 0 && startTime == null && endTime == null)
            {
                sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in(select number from huadong_tmsorder_waybillbase where relationId like '%{0}%')) and w.senderId={1} and w.receiverId={2}  ", waybillNumber, senderId, receiverId);
            }
            //运单编号、上游客户名称、下游客户名称、开始时间
            if (waybillNumber != null && senderId != 0 && receiverId != 0 && startTime != null && endTime == null)
            {
                sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in(select number from huadong_tmsorder_waybillbase where relationId like '%{0}%')) and w.senderId={1} and w.receiverId={2} and w.beginAt>='{3}' ", waybillNumber, senderId, receiverId, startTime);
            }
            //运单编号、上游客户名称、下游客户名称、开始时间、结束时间
            if (waybillNumber != null && senderId != 0 && receiverId != 0 && startTime != null && endTime != null)
            {
                sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in(select number from huadong_tmsorder_waybillbase where relationId like '%{0}%')) and  w.senderId={1} and  w.receiverId={2} and w.beginAt>='{3}' and w.signinAt<='{4}' ", waybillNumber, senderId, receiverId, startTime, endTime);

            }
            //运单编号、上游客户名称、开始时间
            if (waybillNumber != null && senderId != 0 && receiverId == 0 && startTime != null && endTime == null)
            {
                sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in(select number from huadong_tmsorder_waybillbase where relationId like '%{0}%')) and w.senderId={1} and w.beginAt>='{2}' ", waybillNumber, senderId, startTime);

            }
            //运单编号、上游客户名称、结束时间
            if (waybillNumber != null && endTime != null && senderId != 0 && receiverId == 0 && startTime == null)
            {
                sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in(select number from huadong_tmsorder_waybillbase where relationId like '%{0}%')) and w.signinAt<='{1}' and w.senderId={2} ", waybillNumber, endTime, senderId);
            }
            //运单编号、上游客户名称、开始时间、结束时间
            if (waybillNumber != null && senderId != 0 && receiverId == 0 && startTime != null && endTime != null)
            {
                sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in(select number from huadong_tmsorder_waybillbase where relationId like '%{0}%')) and  w.senderId={1} and w.beginAt>='{2}' and w.signinAt<='{3}' ", waybillNumber, senderId, startTime, endTime);

            }
            //运单编号、开始时间
            if (waybillNumber != null && senderId == 0 && receiverId == 0 && startTime != null && endTime == null)
            {
                sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in(select number from huadong_tmsorder_waybillbase where relationId like '%{0}%')) and w.beginAt>='{1}' ", waybillNumber, startTime);
            }
            //运单编号、结束时间
            if (waybillNumber != null && senderId == 0 && receiverId == 0 && startTime == null && endTime != null)
            {
                sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in(select number from huadong_tmsorder_waybillbase where relationId like '%{0}%')) and w.signinAt<='{1}' ", waybillNumber, endTime);
            }
            
            //上游客户名称
            if (waybillNumber == null && senderId != 0 && receiverId == 0 && startTime == null && endTime == null)
            {
                sql += string.Format("where w.senderId={0} ", senderId);
            }
            //上游客户名称、下游客户名称
            if (waybillNumber == null && senderId != 0 && receiverId != 0 && startTime == null && endTime == null)
            {
                sql += string.Format("where w.senderId={0} and w.receiverId={1} ", senderId, receiverId);
            }
            //上游客户名称、开始时间
            if (waybillNumber == null && senderId != 0 && receiverId == 0 && startTime != null && endTime == null)
            {
                sql += string.Format("where w.senderId={0} and w.beginAt>='{1}' ", senderId, startTime);
            }
            //上游客户名称、结束时间
            if (waybillNumber == null && senderId != 0 && receiverId == 0 && startTime == null && endTime != null)
            {
                sql += string.Format("where w.senderId={0} and w.signinAt<='{1}' ", senderId, endTime);
            }
            //上游客户名称、开始时间、结束时间
            if (waybillNumber == null && senderId != 0 && receiverId == 0 && startTime != null && endTime != null)
            {
                sql += string.Format("where  w.senderId={0} and w.beginAt>='{1}' and w.signinAt<='{2}' ", senderId, startTime, endTime);
            }
            //上游客户名称、下游客户名称、开始时间
            if (waybillNumber == null && senderId != 0 && receiverId != 0 && startTime != null && endTime == null)
            {
                sql += string.Format("where w.senderId={0} and w.receiverId={1} and w.beginAt>='{2}' ", senderId, receiverId, startTime);
            }
            //上游客户名称、下游客户名称、结束时间
            if (waybillNumber == null && senderId != 0 && receiverId != 0 && startTime == null && endTime != null)
            {
                sql += string.Format("where w.senderId={0} and w.receiverId={1} and w.signinAt<='{2}' ", senderId, receiverId, endTime);
            }
            //上游客户名称、下游客户名称、开始时间、结束时间
            if (waybillNumber == null && senderId != 0 && receiverId != 0 && startTime != null && endTime != null)
            {
                sql += string.Format("where w.senderId={0} and w.receiverId={1} and w.beginAt>='{2}' and  w.signinAt<='{3}' ", senderId, receiverId, startTime, endTime);
            }
            //下游客户名称
            if (waybillNumber == null && senderId == 0 && receiverId != 0 && startTime == null && endTime == null)
            {
                sql += string.Format("where w.receiverId={0} ", receiverId);
            }
            //下游客户名称、开始时间
            if (waybillNumber == null && senderId == 0 && receiverId != 0 && startTime != null && endTime == null)
            {
                sql += string.Format("where w.receiverId={0} and w.beginAt>='{1}' ", receiverId, startTime);
            }
            //下游客户名称、结束时间
            if (waybillNumber == null && senderId == 0 && receiverId != 0 && startTime == null && endTime != null)
            {
                sql += string.Format("where w.receiverId={0} and w.signinAt<='{1}' ", receiverId, endTime);
            }
            //下游客户名称、开始时间、结束时间
            if (waybillNumber == null && senderId == 0 && receiverId != 0 && startTime != null && endTime != null)
            {
                sql += string.Format("where  w.receiverId={0} and w.beginAt>='{1}' and w.signinAt<='{2}' ", receiverId, startTime, endTime);
            }
            //开始时间
            if (waybillNumber == null && senderId == 0 && receiverId == 0 && startTime != null && endTime == null)
            {
                sql += string.Format("where w.beginAt>='{0}' ", startTime);
            }
            //开始时间、结束时间
            if (waybillNumber == null && senderId == 0 && receiverId == 0 && startTime != null && endTime != null)
            {
                sql += string.Format("where w.beginAt>='{0}' and w.signinAt<='{1}' ", startTime, endTime);
            }
            //结束时间
            if (waybillNumber == null && senderId == 0 && receiverId == 0 && startTime == null && endTime != null)
            {
                sql += string.Format("where w.signinAt<='{0}' ", endTime);
            }
            //运单编号、下游客户名称、开始时间
            if (waybillNumber != null && senderId == 0 && receiverId != 0 && startTime != null && endTime == null)
            {
                sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in(select number from huadong_tmsorder_waybillbase where relationId like '%{0}%')) and w.beginAt>='{1}' and  w.receiverId={2} ", waybillNumber, startTime, receiverId);

            }
            //运单编号、下游客户名称、结束时间
            if (waybillNumber != null && endTime != null && senderId == 0 && receiverId != 0 && startTime == null)
            {
                sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in(select number from huadong_tmsorder_waybillbase where relationId like '%{0}%')) and w.signinAt<='{1}' and w.receiverId={2} ", waybillNumber, endTime, receiverId);
            }
            //运单编号、下游客户名称、开始时间,结束时间
            if (waybillNumber != null && senderId == 0 && receiverId != 0 && startTime != null && endTime != null)
            {
                sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in(select number from huadong_tmsorder_waybillbase where relationId like '%{0}%')) and w.receiverId={1} and w.beginAt>='{2}' and w.signinAt<='{3}'  ", waybillNumber, receiverId, startTime, endTime);

            }
            //运单编号、开始时间、结束时间
            if (waybillNumber != null && senderId == 0 && receiverId == 0 && startTime != null && endTime != null)
            {
                sql += string.Format("left join huadong_tmsorder_waybillbase as h on w.number=h.number where (w.number like '%{0}%' or w.number in(select number from huadong_tmsorder_waybillbase where relationId like '%{0}%')) and w.beginAt>='{1}' and w.signinAt<='{2}' ", waybillNumber, startTime, endTime);
            }
            sql += " ;";
            return Convert.ToInt32(_SqlHelp.ExecuteScalar(sql));
        }

        #endregion
    }
}
