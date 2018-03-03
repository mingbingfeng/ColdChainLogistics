using C2LP.Assistant.TMSConsole.Logink;
using C2LP.Assistant.TMSConsole.Model;
using C2LP.Assistant.TMSConsole.Model.NodeUpload.MESSAGEDETAIL;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace C2LP.Assistant.TMSConsole.BLL
{
    class NodeUploadServer
    {
        /// <summary>
        /// 更新当前上报节点
        /// </summary>
        /// <param name="relation"></param>
        /// <returns></returns>
        public static bool UpdateCurrentUploadNodeId(RelationModel relation, bool isArrived, List<string> senderCodeList)
        {
            bool result = false;
            string sql = string.Empty;
            if (!isArrived)
                sql = string.Format("update huadong_tmsorder_waybillbase set currentUploadNodeId = '{0}' where id='{1}';", relation.CurrentUploadNodeId, relation.Id);
            else
            {
                string innerInto = "select min(handleTHCount)-1 as c from huadong_tmsorder_waybillbase where (currentUploadDataNodeId<>0 or currentUploadDataNodeId is null) and relationId in (select relationId from huadong_tms_order where 【where】 and relationId is not null)";
                string where = "SecretKey ='" + Utility._SecretKey + "'";
                if (senderCodeList != null)//如果不为空，则表示查询运管平台同步过来的订单
                    where = string.Format("SecretKey in ('{0}')", string.Join("','", senderCodeList));
                innerInto = innerInto.Replace("【where】", where);
                sql = string.Format("update huadong_tmsorder_waybillbase a INNER JOIN ({0}) b set a.currentUploadNodeId=0, a.handleTHCount=b.c where a.id = '{1}';", innerInto, relation.Id);
            }
            try
            {
                result = DbHelperMySQL.ExecuteSql(sql) > 0;
                UpdateHandleTime(relation.Id, result);
            }
            catch (Exception ex)
            {
                throw new Exception("更新当前上报节点出错：" + ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 上报成功后更新处理标记为1
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public static bool UpdateNodeHandleFlag(int nodeId)
        {
            string sql = "update waybill_node set handleflag = 1 where id = " + nodeId;
            return DbHelperMySQL.ExecuteSql(sql) >= 1;
        }

        public static bool UpdateHandleTime(long relationId, bool handleSuccess)
        {
            try
            {
                string sql = string.Format("update huadong_tmsorder_waybillbase set handleLastTime='{0}' {1} where id='{2}'", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), (handleSuccess ? "" : ",handlecount=ifnull(handlecount,0)+1"), relationId);
                return DbHelperMySQL.ExecuteSql(sql) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("更新处理次数/时间出错：" + ex.Message);
            }
        }


        /// <summary>
        /// 上上报机制 节点上报
        /// </summary>
        /// <param name="senderCodeList"></param>
        /// <param name="loginkNode"></param>
        /// <param name="nodeInfo"></param>
        /// <returns></returns>
        public static XML GetWaitUploadNode(List<string> senderCodeList, ref M_TMSNode loginkNode, ref NodeInfo nodeInfo)
        {
            XML result = null;
            try
            {
                nodeInfo = GetWaitUploadNode(senderCodeList);
                if (nodeInfo != null)
                {
                    result = new XML();
                    result.CONTENTLIST = new List<CONTENT>();
                    result.CONTENTLIST.Add(new CONTENT());
                    result.CONTENTLIST[0].DETAILLIST = new List<DETAIL>();

                    string driverInfo = ((nodeInfo.Driver == null ? string.Empty : nodeInfo.Driver) + " " + (nodeInfo.DriverTel == null ? string.Empty : nodeInfo.DriverTel)).Trim();
                    string trackType = "17";
                    if (nodeInfo.ParentStorageId == 0)
                        trackType = "15";//启运
                    else if (nodeInfo.Arrived == 2)
                        trackType = "18";//正常签收
                    else if (nodeInfo.StorageType == 1)
                        trackType = "16";//到中转站
                    else
                        trackType = "17";//中转站驾驶员已接单出发
                                         //if (senderCodeList != null)
                                         //{
                    loginkNode = new M_TMSNode();
                    loginkNode.JcOrderNo = nodeInfo.ScanNumber;
                    loginkNode.JcNodeId = nodeInfo.Id;
                    loginkNode.TrackPerson = driverInfo;
                    loginkNode.TrackInfo = nodeInfo.Content;
                    loginkNode.TrackTime = DateTime.Parse(nodeInfo.OperateAt);
                    loginkNode.Arrived = nodeInfo.Arrived;
                    loginkNode.OrderNo = nodeInfo.ScanNumber;
                    loginkNode.TrackType = trackType;
                    loginkNode.StorageName = nodeInfo.StorageName;
                    //}
                    //else
                    //{
                    DETAIL d = new DETAIL();
                    d.ECNO = nodeInfo.ShipmentCode;
                    d.CECNO = nodeInfo.ScanNumber;
                    d.LEGNO = nodeInfo.ScanNumber;
                    d.TRACKTIME = DateTime.Parse(nodeInfo.OperateAt).ToString("yyyy-MM-dd HH:mm:ss");
                    d.TRACKPERSON = driverInfo;
                    d.TRACKINFO = nodeInfo.Content;
                    d.TRACKTYPE = trackType;
                    result.CONTENTLIST[0].DETAILLIST.Add(d);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("获取待上报节点失败:"+ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 获取第一个需要上报的运单的下一个需要上报的节点
        /// </summary>
        /// <returns></returns>
        public static XML GetFirstWaitUploadNode(List<string> senderCodeList, ref M_TMSNode loginkNode, out RelationModel relation)
        {
            XML result = null;
            try
            {
                Utility.AddLogText(string.Format("开始查询下一个上报节点"));
                relation = GetFirstWaitUploadNodeOrderRelation(senderCodeList);//, ref _ignoreTempRelationList);
                if (relation != null)
                    Utility.AddLogText(string.Format("CECNO:{0},LEGNO:{1}", relation.Number, relation.RelationId));
                if (relation != null)
                {
                    NodeInfo nodeInfo = GetNextUploadNodeInfo(relation.CurrentUploadNodeId, relation.Number);
                    if (nodeInfo != null)
                    {
                        string driverInfo = ((nodeInfo.Driver == null ? string.Empty : nodeInfo.Driver) + " " + (nodeInfo.DriverTel == null ? string.Empty : nodeInfo.DriverTel)).Trim();
                        string trackType = "17";
                        if (relation.CurrentUploadNodeId == null || relation.CurrentUploadNodeId == -1)
                            trackType = "15";//启运
                        else if (nodeInfo.Arrived == 2)
                            trackType = "18";//正常签收
                        else if (nodeInfo.StorageType == 1)
                            trackType = "16";//到中转站
                        else
                            trackType = "17";//中转站驾驶员已接单出发
                                             //if (senderCodeList != null)
                                             //{
                        loginkNode = new M_TMSNode();
                        loginkNode.JcOrderNo = relation.Number;
                        loginkNode.JcNodeId = nodeInfo.Id;
                        loginkNode.TrackPerson = driverInfo;
                        loginkNode.TrackInfo = nodeInfo.Content;
                        loginkNode.TrackTime = DateTime.Parse(nodeInfo.OperateAt);
                        loginkNode.Arrived = nodeInfo.Arrived;
                        loginkNode.OrderNo = relation.RelationId;
                        loginkNode.TrackType = trackType;
                        loginkNode.StorageName = nodeInfo.StorageName.Replace("[默认]", string.Empty);
                        //}
                        //else
                        //{
                        result = new XML();
                        result.CONTENTLIST = new List<CONTENT>();
                        result.CONTENTLIST.Add(new CONTENT());
                        result.CONTENTLIST[0].DETAILLIST = new List<DETAIL>();
                        DETAIL d = new DETAIL();
                        d.ECNO = GetShipmentCodeByTMSOrder(relation.RelationId);
                        d.CECNO = relation.Number;
                        d.LEGNO = relation.RelationId;
                        d.TRACKTIME = DateTime.Parse(nodeInfo.OperateAt).ToString("yyyy-MM-dd HH:mm:ss");
                        d.TRACKPERSON = driverInfo;
                        d.TRACKINFO = nodeInfo.Content;
                        d.TRACKTYPE = trackType;
                        result.CONTENTLIST[0].DETAILLIST.Add(d);
                        relation.CurrentUploadDataTime = nodeInfo.StorageName.Replace("[默认]", string.Empty);//此字段暂存车牌号
                        ////}
                        relation.CurrentUploadNodeId = nodeInfo.Id;
                        Utility.AddLogText(string.Format("ECNO:{0},CECNO:{1}", d.ECNO, d.CECNO));
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.AddLogText("GetFirstWaitUploadNode异常：" + ex.Message);
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// 获取指定NodeId的下一个节点信息
        /// </summary>
        /// <param name="nodeId">节点ID</param>
        /// <param name="number">内部订单号</param>
        /// <returns></returns>
        public static NodeInfo GetNextUploadNodeInfo(int? nodeId, string number)
        {
            NodeInfo n = null;
            int baseId = 0;
            string sql = string.Format("select Id from waybill_base where number = '{0}' order by id desc limit 1", number);
            try
            {
                baseId = Convert.ToInt32(DbHelperMySQL.GetSingle(sql));
            }
            catch (Exception e)
            {
                throw new Exception("获取内部订单Id失败：" + e.Message);
            }
            try
            {
                int realNodeId = nodeId == null ? -1 : (int)nodeId;
                string where = ">(SELECT operateat from waybill_node where id='" + realNodeId + "')";
                if (realNodeId == -1)
                    where = string.Format("=(select beginAt from waybill_base where number = '{0}' order by id desc limit 1)", number);
                sql = string.Format("select n.*,s.StorageType,s.Driver,s.DriverTel from waybill_Node n left join coldStorage s on n.storageId=s.id where n.baseid='{0}' and n.operateat{1} order by arrived ,operateat limit 1", baseId, where);

                using (MySqlDataReader reader = DbHelperMySQL.ExecuteReader(sql))
                {
                    if (reader.Read())
                    {
                        n = new NodeInfo();
                        n.Id = Convert.ToInt32(reader["Id"]);
                        n.BaseId = Convert.ToInt32(reader["BaseId"]);
                        n.OperateAt = Convert.ToDateTime(reader["OperateAt"]).ToString("yyyy-MM-dd HH:mm:ss"); //reader["OperateAt"].ToString();
                        n.StorageId = Convert.ToInt32(reader["StorageId"]);
                        n.StorageName = reader["StorageName"].ToString();
                        n.Content = reader["Content"].ToString();
                        n.Arrived = Convert.ToInt32(reader["Arrived"]);
                        n.StorageType = Convert.ToInt32(reader["StorageType"]);
                        n.Driver = reader["Driver"].ToString();
                        n.DriverTel = reader["DriverTel"].ToString();
                        n.CustomerId = Convert.ToInt32(reader["StorageType"]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("获取节点信息失败：" + ex.Message);
            }
            return n;
        }

        /// <summary>
        /// 根据关系ID获取TMS运单的派车单号
        /// </summary>
        /// <param name="relationId">关系ID</param>
        /// <returns></returns>
        public static string GetShipmentCodeByTMSOrder(string relationId)
        {
            string result = string.Empty;
            string sql = string.Format("select ShipmentCode from huadong_tms_order where relationId = '{0}' limit 1", relationId);
            Utility.AddLogText(sql);
            try
            {
                object obj = DbHelperMySQL.GetSingle(sql);
                if (obj != null)
                    result = obj.ToString();
                Utility.AddLogText("根据关系ID获取TMS运单的派车单号:" + result);
            }
            catch (Exception ex)
            {
                Utility.AddLogText("获取TMS派车单号失败：" + ex.Message);
                throw new Exception("获取TMS派车单号失败：" + ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 获取带上报的节点 运管平台固定拿一条，大华东拿配置的条数
        /// </summary>
        /// <param name="senderCodeList"></param>
        /// <returns></returns>
        private static NodeInfo GetWaitUploadNode(List<string> senderCodeList)
        {
            NodeInfo n = null;
            string sql = "select * from ( select n.*,o.ShipmentCode from waybill_node n left join huadong_tms_order o on n.scanNumber = o.SHIPDETAILID or n.scanNumber=o.LEGCODE where n.handleFlag = 0 and n.scanNumber is not null and 【SecretKey】 order by n.id desc limit 1) a left JOIN coldStorage b on a.StorageId = b.id";
            if (senderCodeList == null)
                sql= sql.Replace("【SecretKey】", "SecretKey ='" + Utility._SecretKey + "'  and SHIPMENTCODE<>''");
            else
                sql= sql.Replace("【SecretKey】", string.Format("SecretKey in ('{0}')", string.Join("','", senderCodeList)));

            using (MySqlDataReader reader = DbHelperMySQL.ExecuteReader(sql))
            {
                if (reader.Read())
                {
                    n = new NodeInfo();
                    n.Id = Convert.ToInt32(reader["Id"]);
                    n.BaseId = Convert.ToInt32(reader["BaseId"]);
                    n.OperateAt = Convert.ToDateTime(reader["OperateAt"]).ToString("yyyy-MM-dd HH:mm:ss"); //reader["OperateAt"].ToString();
                    n.StorageId = Convert.ToInt32(reader["StorageId"]);
                    n.StorageName = reader["StorageName"].ToString().Replace("[默认]", string.Empty);
                    n.Content = reader["Content"].ToString();
                    n.Arrived = Convert.ToInt32(reader["Arrived"]);
                    n.StorageType = Convert.ToInt32(reader["StorageType"]);
                    n.Driver = reader["Driver"].ToString();
                    n.DriverTel = reader["DriverTel"].ToString();
                    n.ScanNumber = reader["ScanNumber"].ToString();
                    n.ParentStorageId = Convert.ToInt32(reader["ParentStorageId"]);
                    n.ShipmentCode = reader["ShipmentCode"].ToString();
                    n.CustomerId = Convert.ToInt32(reader["CustomerId"]);
                }
            }
            return n;
        }

        /// <summary>
        /// 获取一个需要上报节点的订单关系
        /// </summary>
        /// <returns></returns>
        private static RelationModel GetFirstWaitUploadNodeOrderRelation(List<string> senderCodeList)
        {
            RelationModel result = null;
            string sql = "select * from huadong_tmsorder_waybillbase where (currentUploadNodeId<>0 or currentUploadNodeId is null) and relationId in (select relationId from huadong_tms_order where 【where】 and relationId is not null) 【ignore】 limit 1";
            //if (_ignoreTempRelationList.Count > 0)
            //    sql = sql.Replace("【ignore】", string.Format("and id not in({0})", string.Join(",", _ignoreTempRelationList)));
            //else
            //sql = sql.Replace("【ignore】", "");
            //排除集合改为使用权重列和id排序
            sql = sql.Replace("【ignore】", " order by handleCount,id desc ");
            string where = "SecretKey ='" + Utility._SecretKey + "'";
            if (senderCodeList != null)//如果不为空，则表示查询运管平台同步过来的订单
                where = string.Format("SecretKey in ('{0}')", string.Join("','", senderCodeList));
            sql = sql.Replace("【where】", where);
            try
            {
                using (MySqlDataReader reader = DbHelperMySQL.ExecuteReader(sql))
                {
                    if (reader.Read())
                    {
                        result = new RelationModel();
                        result.Id = Convert.ToInt32(reader["Id"]);
                        result.RelationId = reader["RelationId"].ToString();
                        result.Number = reader["Number"].ToString();
                        result.CurrentUploadNodeId = reader["CurrentUploadNodeId"] is DBNull ? -1 : Convert.ToInt32(reader["CurrentUploadNodeId"]);
                        //result.CurrentUploadDataNodeId = reader["CurrentUploadDataNodeId"] is DBNull ? -1 : Convert.ToInt32(reader["CurrentUploadDataNodeId"]);
                        result.CustomerId = reader["CustomerId"] is DBNull ? 0 : Convert.ToInt32(reader["CustomerId"]);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("获取运单关系失败：" + ex.Message);
            }
            //int count = _ignoreTempRelationList.Count;
            //if (result == null && _ignoreTempRelationList.Count >= GetRelationCount(senderCodeList))
            //{
            //    _ignoreTempRelationList.Clear();
            //    throw new Exception("已检测完所有上报信息,共[" + count + "]条,将重新开始");
            //}
            return result;
        }

        private static int GetRelationCount(List<string> senderCodeList)
        {
            int result = 0;
            string sql = "select count(*) from huadong_tmsorder_waybillbase where (currentUploadNodeId is null or currentUploadNodeId=-1 or currentUploadNodeId<>0) and relationId in (select relationId from huadong_tms_order where 【where】 and relationId is not null)";
            string where = "SecretKey ='" + Utility._SecretKey + "'";
            if (senderCodeList != null)//如果不为空，则表示查询运管平台同步过来的订单
                where = string.Format("SecretKey in ('{0}')", string.Join("','", senderCodeList));
            sql = sql.Replace("【where】", where);
            try
            {
                result = Convert.ToInt32(DbHelperMySQL.GetSingle(sql));
            }
            catch
            {
            }
            return result;
        }
    }
}
