using C2LP.WebService.Model;
using C2LP.WebService.Utility;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using C2LP.WebService.Model.MyEnum;

namespace C2LP.WebService.BLL.PDABLL
{
    public class PDA_WaybillServer : BaseServer
    {
        /// <summary>
        /// 查询系统中已存在的指定运单信息
        /// </summary>
        /// <param name="numbers">指定运单号</param>
        /// <returns></returns>
        private static List<Model_Waybill_Base> GetExistWaybills(List<string> numbers)
        {
            List<Model_Waybill_Base> list = new List<Model_Waybill_Base>();
            if (numbers.Count > 0)
            {
                string distinctSql = string.Format("select * from waybill_base where number in('{0}')", string.Join("','", numbers));
                list = _SqlHelp.ExecuteObjects<Model_Waybill_Base>(distinctSql);
            }
            return list;
        }

        /// <summary>
        /// 插入运单
        /// </summary>
        /// <param name="waybillList">运单集合</param>
        /// <returns></returns>
        public static bool UploadWaybill_Base(List<Model_Waybill_Base> waybillList)
        {
            if (waybillList.Count == 0)
                return false;
            bool result = false;
            try
            {
                List<Model_Waybill_Base> existList = GetExistWaybills(waybillList.Select(l => l.Number).Distinct().ToList());
                StringBuilder sql = new StringBuilder("insert into waybill_base (number,senderId,senderOrg,senderPerson,senderTel,senderAddress,receiverId,receiverOrg,receiverPerson,receiverTel,receiverAddress,billingCount,stage,beginAt,company) values ");
                int distinctCount = 0;//重复运单的数量
                foreach (Model_Waybill_Base item in waybillList)
                {
                    if (existList.Where(l => l.Number == item.Number).Count() > 0)
                        distinctCount++;
                    else
                        sql.AppendFormat("('{0}','{1}','{2}','{3}','{4}','{5}',{6},'{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}'),", item.Number, item.SenderId, item.SenderOrg, item.SenderPerson, item.SenderTel, item.SenderAddress, item.ReceiverId == 0 ? "NULL" : item.ReceiverId.ToString(), item.ReceiverOrg, item.ReceiverPerson, item.ReceiverTel, item.ReceiverAddress, item.BillingCount, (int)item.Stage, item.BeginAt.ToString("yyyy-MM-dd HH:mm:ss"), 0);
                }
                if (distinctCount == waybillList.Count)
                    return true;//如果全部重复，则返回True，通知PDA删除运单
                sql.Length -= 1;
                result = _SqlHelp.ExecuteNonQuery(sql.ToString()) == waybillList.Count - distinctCount;
                //运单未上报，节点已存在，把节点信息添加到waybill_base表中并删除unnecessary_node表的节点信息.
                GetOldNodeByWaybill(waybillList);
                GetWaybillPictures(waybillList);
                string cwMsg = "(上报运单)Number=" + waybillList.First().Number + ";Sender=" + waybillList.First().SenderOrg + ";Receiver=" + waybillList.First().ReceiverOrg + ";BeginAt=" + waybillList.First().BeginAt.ToString("yyyy-MM-dd HH:mm:ss");
                Console.WriteLine(cwMsg);
                LogServer.AddLogText(cwMsg, waybillList.First().Number);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public static bool UploadWaybill_Node(Model_Waybill_Node node, int customerId, int? parentStorageId)
        {
            bool saveUploadDataProgress = false;//是否保存到冷链数据上报进度表
            bool isFirstNode = false;
            try
            {
                if (node != null)
                {
                    LogServer.AddLogText(string.Format("进入创建运单节点逻辑{0},操作时间{1}", node.BaseId, node.operateAt), node.BaseId);
                    string cwMsg = "(上报节点)NodeNumber=" + node.BaseId + ";Content=" + node.Content + ";CustomerId=" + customerId + ";ParentStorageId=" + (parentStorageId == null ? "无" : parentStorageId.ToString());
                    Console.WriteLine(cwMsg);

                    LogServer.AddLogText(cwMsg, node.BaseId);
                    if (!string.IsNullOrEmpty(node.BaseId) && node.BaseId.Contains(@"\"))
                    {
                        node.BaseId = node.BaseId.Replace(@"\", "").Replace("/", "");
                        Console.WriteLine("替换运单号中的斜杠:" + node.BaseId);
                        LogServer.AddLogText("替换运单号中的斜杠:" + node.BaseId, node.BaseId);
                    }
                }
                string sql = string.Empty;
                string number = node.BaseId;//PDA传过来的BaseId就是运单号
                //Customer=0为自运单，否则为第三方运单
                if (customerId != 0)
                {
                    //第三方运单需要查询关系表获取转换后的运单号
                    sql = "select number from huadong_tmsorder_waybillbase where relationid='" + number + "' and customerId=" + customerId;
                    number = _SqlHelp.ExecuteScalar<string>(sql);
                    LogServer.AddLogText(string.Format("第三方运单需要查询关系表获取转换后的运单号{0},结果：{1}", sql, number), node.BaseId);
                }
                else
                    LogServer.AddLogText("自运单 :", node.BaseId);
                Model_Waybill_Base waybillBase = null;//根据运单号获取到BaseId
                sql = "select * from waybill_base where number = '" + number + "' order by id desc limit 1";
                LogServer.AddLogText("查询运单是否存在:" + sql, node.BaseId);
                waybillBase = _SqlHelp.ExecuteObject<Model_Waybill_Base>(sql);
                if (waybillBase == null)
                {
                    string remark = "运单[" + (string.IsNullOrEmpty(number) ? node.BaseId : number) + "]不存在";
                    remark += "传入的CustomerId=" + customerId;
                    sql = string.Format("insert into unnecessary_node(baseId,operateAt,storageId,storageName,content,arrived,remarks,inserttime,parentStorageId,customerId) values('{0}', '{1}', {2}, '{3}', '{4}', {5}, '{6}','{7}','{8}','{9}');",
                            node.BaseId, node.operateAt.ToString("yyyy-MM-dd HH:mm:ss"), node.StorageId, node.StorageName, node.Content, (int)node.Arrived, remark, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), parentStorageId, customerId);
                    int insertResult = _SqlHelp.ExecuteNonQuery(sql);
                    saveUploadDataProgress = insertResult > 0;
                    LogServer.AddLogText(remark + ":" + sql + " 执行结果:" + insertResult, node.BaseId);
                    return true;//运单不存在，将存入暂存表
                }
                else
                    LogServer.AddLogText("当前运单存在,开始添加节点", node.BaseId);
                if (waybillBase.BeginAt == node.operateAt)
                    isFirstNode = true;
                //查询该运单已有的所有节点
                List<Model_Waybill_Node> base_NodeList = new List<Model_Waybill_Node>();
                sql = "select id, convert(baseId, CHAR) as baseId ,operateAt,storageId,storageName,content,arrived from waybill_node where baseId = " + waybillBase.Id;
                base_NodeList = _SqlHelp.ExecuteObjects<Model_Waybill_Node>(sql);
                LogServer.AddLogText("查询该运单已有的所有节点:" + sql + ",查询到的节点条数：" + base_NodeList.Count + "", node.BaseId);
                foreach (Model_Waybill_Node item in base_NodeList)
                {
                    //存在内容一致，时间一致的节点时丢弃
                    if (item.operateAt == node.operateAt)
                    {
                        LogServer.AddLogText(string.Format("内容一致，时间一致的节点丢弃;节点ID[{0}] 节点时间[{1}] 节点内容[{2}]", item.Id, item.operateAt.ToString("yyyy-MM-dd HH:mm:ss"), item.Content), node.BaseId);
                        return true;
                    }
                    if (item.Arrived == Enum_Arrived.HaveArrived)
                    {
                        if (node.Arrived == Enum_Arrived.HaveArrived) //存在运抵节点时，并且当前上报的也是运抵节点则丢弃
                        {
                            LogServer.AddLogText("已存在运抵节点，当前也是运抵节点，弃之", node.BaseId);
                            return true;
                        }
                        else if (node.operateAt >= item.operateAt)    //存在运抵节点时，当前节点虽然不是运抵节点但是创建时间比运抵时间大的也丢弃
                        {
                            LogServer.AddLogText("已存在运抵节点，当前节点时间大于运抵节点时间，弃之", node.BaseId);
                            return true;
                        }
                    }
                }
                List<string> sqlList = new List<string>();
                sqlList.Add(string.Format("insert into waybill_node (baseId,operateAt,storageId,storageName,content,arrived,parentStorageId,handleFlag,scanNumber,CustomerId,insertTime) values ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}','{6}','{7}','{8}','{9}','{10}');", waybillBase.Id, node.operateAt.ToString("yyyy-MM-dd HH:mm:ss"), node.StorageId, node.StorageName.Replace("[默认]", ""), node.Content, (int)node.Arrived, (parentStorageId==null?-1:parentStorageId), (parentStorageId == null ? -1 : (customerId == 0 ? -1 : 0)),node.BaseId, customerId,DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
                if (node.Arrived == Enum_Arrived.HaveArrived)
                {
                    sqlList.Add(string.Format("update waybill_base set stage=1 ,signinAt='{0}' where number='{1}';", node.operateAt.ToString("yyyy-MM-dd HH:mm:ss"), number));
                    //运抵时更新此单的上报优先级（第三方运单）
                    //if (customerId != 0)
                    //    sqlList.Add(string.Format("update huadong_tmsorder_waybillbase set handleCount = null,handleLastTime=now() where number = '{0}';", number));
                }
                foreach (string item in sqlList)
                {
                    LogServer.AddLogText(string.Format("添加节点:{0}", item), node.BaseId);
                }
                bool result = _SqlHelp.ExecuteTranstration(sqlList);
                LogServer.AddLogText("添加结果：" + result.ToString(), node.BaseId);
                saveUploadDataProgress = result;
                return result;
            }
            catch (Exception ex)
            {
                LogServer.AddLogText("添加节点失败:" + ex.Message, node.BaseId);
                throw ex;
            }
            finally
            {
                try
                {
                    if (customerId != 0 && parentStorageId != null && saveUploadDataProgress)
                        SaveUploadDataProgress(node, (int)parentStorageId, isFirstNode);
                }
                catch (Exception ex)
                {
                    LogServer.AddLogText("添加冷链数据上报进度失败,但未影响该节点上报:" + ex.Message, node.BaseId);
                }
            }
        }

        /// <summary>
        /// 添加冷链数据上报进度信息
        /// </summary>
        /// <param name="nodeInfo">节点信息</param>
        /// <param name="parentStorageId"></param>
        private static void SaveUploadDataProgress(Model_Waybill_Node nodeInfo, int parentStorageId, bool isFirstNode)
        {
            List<string> sqlList = new List<string>();
            try
            {
                //向上查询 关联节点更新上一个记录的结束时间
                string updateParent = string.Format("update uploadDataProgress set endNodeTime = '{0}' where relationId='{1}' and nodeTime<'{0}' and storageId = {2};", nodeInfo.operateAt.ToString("yyyy-MM-dd HH:mm:ss"), nodeInfo.BaseId, parentStorageId);
                if (nodeInfo.Arrived == Enum_Arrived.HaveArrived)
                {
                    sqlList.Add(updateParent);
                }
                else
                {
                    //插入新的记录
                    string insertSql = string.Format("INSERT INTO `uploadDataProgress` ( `relationId`, `storageId`, `storageName`, `nodeParentStorageId`, `nodeTime`, `endNodeTime`, `uploadProgress`, `lastHandleTime`, `handleFlag`) VALUES ( '{0}', '{1}', '{2}', '{3}', '{4}', {5}, '{4}', '{6}','{7}');", nodeInfo.BaseId, nodeInfo.StorageId, nodeInfo.StorageName, parentStorageId, nodeInfo.operateAt.ToString("yyyy-MM-dd HH:mm:ss"), "【ENDNODETIME】", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 0);
                    //向下查询 关联节点更新当前记录的结束时间
                    string selectSql = string.Format("select nodeTime from uploadDataProgress where relationId='{0}' and nodeTime>'{1}' and nodeParentStorageId = {2} order by  nodeTime LIMIT 1", nodeInfo.BaseId, nodeInfo.operateAt.ToString("yyyy-MM-dd HH:mm:ss"), nodeInfo.StorageId);
                    object objNodeTime = _SqlHelp.ExecuteScalar(selectSql);
                    string endNodeTime = "NULL";
                    if (objNodeTime != null)
                        endNodeTime = "'" + Convert.ToDateTime(objNodeTime).ToString("yyyy-MM-dd HH:mm:ss") + "'";
                    insertSql = insertSql.Replace("【ENDNODETIME】", endNodeTime);
                    sqlList.Add(insertSql);
                    if (!isFirstNode)
                        sqlList.Add(updateParent);
                }
                _SqlHelp.ExecuteTranstration(sqlList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                foreach (string item in sqlList)
                {
                    LogServer.AddLogText(string.Format("添加上报处理进度:{0}", item), nodeInfo.BaseId);
                }
            }
        }

        /// <summary>
        /// 插入运单节点
        /// </summary>
        /// <param name="nodeList">节点集合</param>
        /// <returns></returns>
        public static bool UploadWaybill_Node(List<Model_Waybill_Node> nodeList)
        {
            if (nodeList.Count == 0)
                return false;
            bool result = false;
            try
            {
                List<Model_Waybill_Base> numberList = GetExistWaybills(nodeList.Select(l => l.BaseId).Distinct().ToList());
                //if (numberList.Count == 0)
                //    return true;
                if (numberList.Count == 0)
                {
                    try
                    {
                        //运单信息不存在，节点信息保存在unnecessary_node表中
                        string remark = "运单不存在";
                        string unnode = string.Format("insert into unnecessary_node(baseId,operateAt,storageId,storageName,content,arrived,remarks,inserttime) values('{0}', '{1}', {2}, '{3}', '{4}', {5}, '{6}','{7}');",
                            nodeList[0].BaseId, nodeList[0].operateAt, nodeList[0].StorageId, nodeList[0].StorageName, nodeList[0].Content, (int)nodeList[0].Arrived, remark, DateTime.Now);
                        _SqlHelp.ExecuteNonQuery(unnode);
                    }
                    catch
                    {
                    }
                    return true;
                }
                StringBuilder sql = new StringBuilder("insert into waybill_node (baseId,operateAt,storageId,storageName,content,arrived) values ");
                StringBuilder arrivedSql = new StringBuilder();
                int receiveContinue = 0;
                foreach (Model_Waybill_Node item in nodeList)
                {
                    if (numberList.Where(l => l.Number == item.BaseId).Count() > 0)
                    {
                        int baseId = numberList.Find(l => l.Number == item.BaseId).Id;
                        if (numberList.Find(l => l.Number == item.BaseId).Stage == Model.MyEnum.Enum_WaybillStage.Received)
                        {
                            receiveContinue++;
                            continue;
                        }
                        if (GetOperateAt(baseId, item.operateAt.ToString("yyyy-MM-dd HH:mm:ss")) > 0)
                        {
                            receiveContinue++;
                            continue;
                        }
                        sql.AppendFormat("('{0}','{1}','{2}','{3}','{4}','{5}'),", baseId, item.operateAt, item.StorageId, item.StorageName, item.Content, (int)item.Arrived);
                        if (item.Arrived == Model.MyEnum.Enum_Arrived.HaveArrived)
                        {
                            arrivedSql.AppendFormat("update waybill_base set stage=1 ,signinAt='{0}' where number='{1}';", item.operateAt, item.BaseId);
                        }
                    }

                }
                if (receiveContinue == nodeList.Count)
                    return true;
                sql.Length -= 1;
                List<string> sqlList = new List<string>();
                sqlList.Add(sql.ToString());
                if (arrivedSql.Length > 0)
                    sqlList.Add(arrivedSql.ToString());
                result = _SqlHelp.ExecuteTranstration(sqlList);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// 上传图片，插入签收拍照信息
        /// </summary>
        /// <param name="postback">签收拍照信息</param>
        /// <param name="postbackTime">签收拍照时间</param>
        /// <param name="picList">图片列表</param>
        /// <returns></returns>
        public static bool UploadWaybill_Postback(Model_Waybill_Postback_Pic postback, DateTime postbackTime, List<object> picList, int customerId = 0)
        {
            LogServer.AddLogText(string.Format("开始进入保存图片逻辑{0}，DateTime:{1}", postback.BaseId, postbackTime), postback.BaseId);
            bool result = false;
            int picIndex = 0;//成功上传的图片数量
            if (picList.Count == 0)
            {
                LogServer.AddLogText(string.Format("图片列表{0}", picList.Count), postback.BaseId);
                return false;
            }
            if (string.IsNullOrEmpty(postback.PicName))
            {
                LogServer.AddLogText(string.Format("图片名称{0}", postback.PicName), postback.BaseId);
                return false;
            }
            string[] picNameArr = postback.PicName.Split('|');
            if (picNameArr.Length != picList.Count)
            {
                LogServer.AddLogText(string.Format("picNameArr:{0},picList:{1}", picNameArr.Length, picList.Count), postback.BaseId);
                return false;
            }
            //配置文件中的路径
            string filePath = string.Empty;
            string tempPath = System.Configuration.ConfigurationManager.AppSettings["PostbackPath"];
            if (string.IsNullOrEmpty(tempPath))
            {
                LogServer.AddLogText(string.Format("配置文件路径:{0}", tempPath), postback.BaseId);
                return false;
            }
            string timePath = DateTime.Now.ToString("yyyyMM");
            filePath = tempPath + "\\" + timePath;
            try
            {
                string sql = string.Empty;
                string number = postback.BaseId;//PDA传过来的BaseId就是运单号
                //Customer=0为自运单，否则为第三方运单
                if (customerId != 0)
                {
                    //第三方运单需要查询关系表获取转换后的运单号
                    sql = "select number from huadong_tmsorder_waybillbase where relationid='" + number + "' and customerId=" + customerId;
                    number = _SqlHelp.ExecuteScalar<string>(sql);
                    LogServer.AddLogText(string.Format("查询关联表，huadong_tmsorder_waybillbase:{0},结果:{1}", sql, number), postback.BaseId);
                }
                Model_Waybill_Base waybillBase = null;//根据运单号获取到BaseId
                sql = "select * from waybill_base where number = '" + number + "' order by id desc limit 1";
                waybillBase = _SqlHelp.ExecuteObject<Model_Waybill_Base>(sql);
                if (waybillBase == null)
                {
                    LogServer.AddLogText(string.Format("查询运单，waybill_base:{0},结果:{1}", sql, waybillBase == null ? "未查询到信息" : waybillBase.Number), postback.BaseId);
                    return true;//运单还未上传，无法上传签收图片
                }
                //List<Model_Waybill_Base> numberList = GetExistWaybills(new List<string> { postback.BaseId }).Distinct().ToList();
                //if (numberList.Count == 0)
                //    return true;//运单还未上传，无法上传签收图片
                if (!System.IO.Directory.Exists(filePath))
                    System.IO.Directory.CreateDirectory(filePath);
                StringBuilder sb = new StringBuilder("insert into waybill_postback_pic (baseId,picName) values ");
                for (int i = 0; i < picList.Count; i++)
                {
                    string fileName = postback.BaseId + "_" + System.IO.Path.GetFileName(postback.PicName.Split('|')[i]);
                    MyTool.SaveImage(MyTool.GetGzipPicBytes(picList[i] as byte[]), filePath + "\\" + fileName);
                    sb.AppendFormat("('{0}','{1}'),", waybillBase.Id, timePath + "/" + fileName);
                    picIndex++;
                }
                sb.Length -= 1;
                List<string> sqlList = new List<string>();
                sqlList.Add(sb.ToString());
                sqlList.Add(string.Format("update waybill_base set picPostbackAt='{0}' where number='{1}'", postbackTime.ToString("yyyy-MM-dd HH:mm:ss"), postback.BaseId));
                result = _SqlHelp.ExecuteTranstration(sqlList);
                foreach (string item in sqlList)
                {
                    LogServer.AddLogText(string.Format("sql:{0}", item), postback.BaseId);
                }
                LogServer.AddLogText(string.Format("保存结果：{0}", result), postback.BaseId);
            }
            catch (Exception ex)
            {
                LogServer.AddLogText("保存图片失败:" + ex.Message, postback.BaseId);
                throw ex;
            }
            finally
            {
                if (result == false && picIndex > 0)
                {
                    //失败时删除图片
                    for (int i = picIndex; i > 0; i--)
                    {
                        string fileName = postback.BaseId + "_" + System.IO.Path.GetFileName(postback.PicName.Split('|')[i - 1]);
                        System.IO.File.Delete(filePath + "\\" + fileName);
                        LogServer.AddLogText(string.Format("失败时删除图片，路径:{0}", filePath + "\\" + fileName), postback.BaseId);
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 运行节点是否存在
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
        /// 查询运单以前是否存在节点信息，没有超过一个月的节点信息保存到节点表中，超过时间的节点信息删除。
        /// </summary>
        /// <param name="waybillList"></param>
        public static void GetOldNodeByWaybill(List<Model_Waybill_Base> waybillList)
        {
            string sql = string.Format("delete from unnecessary_node where inserttime < '{0}'", DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd HH:mm:ss"));
            try
            {
                _SqlHelp.ExecuteNonQuery(sql);
                List<Model_Waybill_Base> numberList = GetExistWaybills(waybillList.Select(l => l.Number).Distinct().ToList());
                foreach (Model_Waybill_Base item in waybillList)
                {
                    sql = string.Format("select * from unnecessary_node where  baseId='{0}' and operateAt>'{1}';", item.Number, item.BeginAt.ToString("yyyy-MM-dd HH:mm:ss"));
                    List<Model_UnnecessaryNode> waybillnode = _SqlHelp.ExecuteObjects<Model_UnnecessaryNode>(sql);
                    if (waybillnode.Count != 0)
                    {
                        int baseId = numberList.Find(l => l.Number == item.Number).Id;
                        string number = numberList.Find(l => l.Number == item.Number).Number;
                        sql = string.Empty;
                        foreach (Model_UnnecessaryNode node in waybillnode)
                        {
                            sql += string.Format("insert into waybill_node (baseId,operateAt,storageId,storageName,content,arrived,parentStorageId,handleFlag,scanNumber,customerId,insertTime) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{10}','{8}','{9}') ;",
                                    baseId, node.operateAt.ToString("yyyy-MM-dd HH:mm:ss"), node.StorageId, node.StorageName, node.Content, (int)node.Arrived,node.ParentStorageId,(node.CustomerId==0?-1:0),node.CustomerId,node.InsertTime.ToString("yyyy-MM-dd HH:mm:ss"),node.BaseId);
                            sql += string.Format("delete from unnecessary_node where id={0}; ", node.Id);
                            if (node.Arrived == Enum_Arrived.HaveArrived)
                                sql += string.Format("update waybill_base set stage=1 ,signinAt='{0}' where number='{1}';", node.operateAt.ToString("yyyy-MM-dd HH:mm:ss"), number);
                            LogServer.AddLogText(string.Format("sql:{0}", sql), item.Number);
                        }
                        int result = _SqlHelp.ExecuteNonQuery(sql);
                        LogServer.AddLogText("结果:" + result, item.Number);
                    }
                    else
                        LogServer.AddLogText(string.Format("自运单临时节点信息不存在,unnecessary_node:{0},结果:{1}", sql, waybillnode.Count), item.Number);
                }
            }
            catch
            {
            }
        }

        ///// <summary>
        ///// 查询运单以前是否存在节点信息，没有超过一个月的节点信息保存到节点表中，超过时间的节点信息删除。
        ///// </summary>
        ///// <param name="waybillList"></param>
        ///// <returns></returns>
        //public static int GetNode(List<Model_Waybill_Base> waybillList)
        //{
        //    int result = 0;
        //    try
        //    {
        //        //是否存在运单信息
        //        List<Model_Waybill_Base> numberList = GetExistWaybills(waybillList.Select(l => l.Number).Distinct().ToList());
        //        foreach (Model_Waybill_Base item in waybillList)
        //        {
        //            //查询unnecessary_node是否存在节点信息
        //            string sql = string.Format("select * from unnecessary_node where baseId='{0}' ;", item.Number);
        //            List<Model_Waybill_Node> waybillnode = _SqlHelp.ExecuteObjects<Model_Waybill_Node>(sql);
        //            if (waybillnode.Count != 0)
        //            {
        //                //获取id
        //                int baseId = numberList.Find(l => l.Number == item.Number).Id;
        //                foreach (Model_Waybill_Node node in waybillnode)
        //                {
        //                    //节点信息超过一个月就删除
        //                    if (node.operateAt.AddMonths(1) > item.BeginAt)
        //                    {
        //                        sql = string.Format("insert into waybill_node (baseId,operateAt,storageId,storageName,content,arrived) values('{0}','{1}','{2}','{3}','{4}','{5}') ;",
        //                        baseId, node.operateAt, node.StorageId, node.StorageName, node.Content, (int)node.Arrived);
        //                        result = _SqlHelp.ExecuteNonQuery(sql);
        //                        //添加成功后删除节点
        //                        if (result > 0)
        //                        {
        //                            sql = string.Format("delete from unnecessary_node where id={0} ", node.Id);
        //                            _SqlHelp.ExecuteNonQuery(sql);
        //                        }
        //                    }
        //                    else
        //                    {
        //                        sql = string.Format("delete from unnecessary_node where id={0} ", node.Id);
        //                        _SqlHelp.ExecuteNonQuery(sql);
        //                    }


        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return result;
        //}

        /// <summary>
        /// 上传图片，插入签收拍照信息
        /// </summary>
        /// <param name="postback">签收拍照信息</param>
        /// <param name="postbackTime">签收拍照时间</param>
        /// <param name="picList">图片列表</param>
        /// <returns></returns>
        public static bool UploadWaybill_Postbacks(Model_Waybill_Postback_Pic postback, DateTime postbackTime, List<object> picList, int customerId = 0)
        {
            LogServer.AddLogText(string.Format("开始进入保存图片逻辑{0}，DateTime:{1}", postback.BaseId, postbackTime), postback.BaseId);
            bool result = false;
            int picIndex = 0;//成功上传的图片数量
            if (picList.Count == 0)
            {
                LogServer.AddLogText(string.Format("图片列表{0}", picList.Count), postback.BaseId);
                return false;
            }
            if (string.IsNullOrEmpty(postback.PicName))
            {
                LogServer.AddLogText(string.Format("图片名称{0}", postback.PicName), postback.BaseId);
                return false;
            }
            string[] picNameArr = postback.PicName.Split('|');
            if (picNameArr.Length != picList.Count)
            {
                LogServer.AddLogText(string.Format("picNameArr:{0},picList:{1}", picNameArr.Length, picList.Count), postback.BaseId);
                return false;
            }
            //配置文件中的路径
            string filePath = string.Empty;
            string tempPath = System.Configuration.ConfigurationManager.AppSettings["PostbackPath"];
            if (string.IsNullOrEmpty(tempPath))
            {
                LogServer.AddLogText(string.Format("配置文件路径:{0}", tempPath), postback.BaseId);
                return false;
            }
            string timePath = DateTime.Now.ToString("yyyyMM");
            filePath = tempPath + "\\" + timePath;
            try
            {
                string sql = string.Empty;
                string number = postback.BaseId;//PDA传过来的BaseId就是运单号
                //Customer=0为自运单，否则为第三方运单
                if (customerId != 0)
                {
                    //第三方运单需要查询关系表获取转换后的运单号
                    sql = "select number from huadong_tmsorder_waybillbase where relationid='" + number + "' and customerId=" + customerId;
                    number = _SqlHelp.ExecuteScalar<string>(sql);
                    LogServer.AddLogText(string.Format("查询关联表，huadong_tmsorder_waybillbase:{0},结果:{1}", sql, number), postback.BaseId);
                    if (string.IsNullOrEmpty(number))
                    {
                        string remark = "运单[" + (string.IsNullOrEmpty(number) ? postback.BaseId : number) + "]不存在";
                        remark += "传入的CustomerId=" + customerId;
                        if (!System.IO.Directory.Exists(filePath))
                            System.IO.Directory.CreateDirectory(filePath);
                        StringBuilder pic = new StringBuilder("insert into temporarypictures(baseId,operateAt,PicName,remarks,inserttime) values ");
                        for (int i = 0; i < picList.Count; i++)
                        {
                            string fileName = postback.BaseId + "_" + System.IO.Path.GetFileName(postback.PicName.Split('|')[i]);
                            MyTool.SaveImage(MyTool.GetGzipPicBytes(picList[i] as byte[]), filePath + "\\" + fileName);
                            pic.AppendFormat("('{0}', '{1}', '{2}', '{3}', '{4}'),", postback.BaseId, postbackTime.ToString("yyyy-MM-dd HH:mm:ss"), timePath + "/" + fileName, remark, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            //picIndex++;
                        }
                        pic.Length -= 1;
                        List<string> sqlpicList = new List<string>();
                        sqlpicList.Add(pic.ToString());
                        bool pictures = _SqlHelp.ExecuteTranstration(sqlpicList);
                        LogServer.AddLogText(string.Format("sql:{0},结果:{1}", sql, pictures), postback.BaseId);
                        return true;
                    }
                }
                Model_Waybill_Base waybillBase = null;//根据运单号获取到BaseId
                sql = "select * from waybill_base where number = '" + number + "' order by id desc limit 1";
                waybillBase = _SqlHelp.ExecuteObject<Model_Waybill_Base>(sql);
                if (waybillBase == null)
                {
                    string remark = "运单[" + (string.IsNullOrEmpty(number) ? postback.BaseId : number) + "]不存在";
                    remark += "传入的CustomerId=" + customerId;
                    if (!System.IO.Directory.Exists(filePath))
                        System.IO.Directory.CreateDirectory(filePath);
                    StringBuilder pic = new StringBuilder("insert into temporarypictures(baseId,operateAt,PicName,remarks,inserttime) values ");
                    for (int i = 0; i < picList.Count; i++)
                    {
                        string fileName = postback.BaseId + "_" + System.IO.Path.GetFileName(postback.PicName.Split('|')[i]);
                        MyTool.SaveImage(MyTool.GetGzipPicBytes(picList[i] as byte[]), filePath + "\\" + fileName);
                        pic.AppendFormat("('{0}', '{1}', '{2}', '{3}', '{4}'),", postback.BaseId, postbackTime.ToString("yyyy-MM-dd HH:mm:ss"), timePath + "/" + fileName, remark, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        //picIndex++;
                    }
                    pic.Length -= 1;
                    List<string> sqlpicList = new List<string>();
                    sqlpicList.Add(pic.ToString());
                    bool pictures = _SqlHelp.ExecuteTranstration(sqlpicList);
                    LogServer.AddLogText(string.Format("查询运单，waybill_base:{0},结果:{1}", sql, waybillBase == null ? "未查询到信息" : waybillBase.Number), postback.BaseId);
                    return true;//运单还未上传，无法上传签收图片
                }
                //List<Model_Waybill_Base> numberList = GetExistWaybills(new List<string> { postback.BaseId }).Distinct().ToList();
                //if (numberList.Count == 0)
                //    return true;//运单还未上传，无法上传签收图片
                if (!System.IO.Directory.Exists(filePath))
                    System.IO.Directory.CreateDirectory(filePath);
                StringBuilder sb = new StringBuilder("insert into waybill_postback_pic (baseId,picName) values ");
                for (int i = 0; i < picList.Count; i++)
                {
                    string fileName = postback.BaseId + "_" + System.IO.Path.GetFileName(postback.PicName.Split('|')[i]);
                    MyTool.SaveImage(MyTool.GetGzipPicBytes(picList[i] as byte[]), filePath + "\\" + fileName);
                    sb.AppendFormat("('{0}','{1}'),", waybillBase.Id, timePath + "/" + fileName);
                    picIndex++;
                }
                sb.Length -= 1;
                List<string> sqlList = new List<string>();
                sqlList.Add(sb.ToString());
                sqlList.Add(string.Format("update waybill_base set picPostbackAt='{0}' where number='{1}'", postbackTime.ToString("yyyy-MM-dd HH:mm:ss"), waybillBase.Number));
                result = _SqlHelp.ExecuteTranstration(sqlList);
                foreach (string item in sqlList)
                {
                    LogServer.AddLogText(string.Format("sql:{0}", item), postback.BaseId);
                }
                LogServer.AddLogText(string.Format("保存结果：{0}", result), postback.BaseId);
            }
            catch (Exception ex)
            {
                LogServer.AddLogText("保存图片失败:" + ex.Message, postback.BaseId);
                throw ex;
            }
            finally
            {
                if (result == false && picIndex > 0)
                {
                    //失败时删除图片
                    for (int i = picIndex; i > 0; i--)
                    {
                        string fileName = postback.BaseId + "_" + System.IO.Path.GetFileName(postback.PicName.Split('|')[i - 1]);
                        System.IO.File.Delete(filePath + "\\" + fileName);
                        LogServer.AddLogText(string.Format("失败时删除图片，路径:{0}", filePath + "\\" + fileName), postback.BaseId);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 查询临时图片表是否存在图片信息,存在插入图片表中
        /// </summary>
        /// <param name="waybillList"></param>
        public static void GetWaybillPictures(List<Model_Waybill_Base> waybillList)
        {
            string Exnumber = string.Empty;
            string sql = string.Format("delete from temporarypictures where inserttime < '{0}'", DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd HH:mm:ss"));
            try
            {
                _SqlHelp.ExecuteNonQuery(sql);
                List<Model_Waybill_Base> numberList = GetExistWaybills(waybillList.Select(l => l.Number).Distinct().ToList());
                foreach (Model_Waybill_Base item in waybillList)
                {
                    Exnumber = item.Number;
                    sql = string.Format("select * from temporarypictures where  baseId='{0}' and operateAt>'{1}';", item.Number, item.BeginAt.ToString("yyyy-MM-dd HH:mm:ss"));
                    List<Model_TemporaryPictures> waybillpictures = _SqlHelp.ExecuteObjects<Model_TemporaryPictures>(sql);
                    if (waybillpictures.Count != 0)
                    {
                        int baseId = numberList.Find(l => l.Number == item.Number).Id;
                        string number = numberList.Find(l => l.Number == item.Number).Number;
                        sql = string.Empty;
                        foreach (Model_TemporaryPictures pic in waybillpictures)
                        {
                            sql += string.Format("insert into waybill_postback_pic (baseId,picName) values({0},'{1}'); ", baseId, pic.PicName);
                            sql += string.Format("delete from temporarypictures where id={0}; ", pic.id);
                            sql += string.Format("update waybill_base set picPostbackAt='{0}' where number='{1}';", pic.operateAt, number);
                        }
                        int result = _SqlHelp.ExecuteNonQuery(sql);
                        LogServer.AddLogText(string.Format("sql:{0},结果:{1}", sql, result), item.Number);
                    }
                    else
                        LogServer.AddLogText(string.Format("临时图片表不存在图片信息,sql:{0},结果:{1}", sql, waybillpictures.Count), item.Number);
                }
            }
            catch (Exception ex)
            {
                LogServer.AddLogText("GetWaybillPictures异常错误" + ex.Message, Exnumber);
            }
        }

    }
}
