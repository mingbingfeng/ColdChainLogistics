using C2LP.Assistant.TMSConsole.Logink;
using C2LP.Assistant.TMSConsole.Model;
using C2LP.Assistant.TMSConsole.Model.NodeDataUpload;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace C2LP.Assistant.TMSConsole.BLL
{
    class DataUploadServer
    {
        /// <summary>
        /// 更新订单温湿度上报进度
        /// </summary>
        /// <param name="relation">包含运单信息以及运单温湿度数据上报的进度</param>
        /// <returns></returns>
        public static bool UpdataUploadDataNode(RelationModel relation)//, ref List<long> _ignoreTempRelationList)
        {
            bool isArrived = false;
            //试图获取当前运单的下一个节点
            NodeInfo nodeInfo = NodeUploadServer.GetNextUploadNodeInfo(relation.CurrentUploadDataNodeId, relation.Number);
            //如果存在下一个节点并且当前上报进度等于下一个节点的创建时间，则更新上报进度的节点
            if (nodeInfo != null && DateTime.Parse(relation.CurrentUploadDataTime) >= DateTime.Parse(nodeInfo.OperateAt))
            {
                relation.CurrentUploadDataNodeId = nodeInfo.Id;
            }
            //查询当前节点是否已经运抵
            if (relation.CurrentUploadDataNodeId != -1)
            {
                string sql = "select arrived from waybill_node where id = " + relation.CurrentUploadDataNodeId;
                object arrived = DbHelperMySQL.GetSingle(sql);
                if (arrived == null)
                    throw new Exception("获取节点信息失败");
                isArrived = (arrived.ToString() == "2" && (relation.CurrentUploadNodeId == 0));
            }
            //if (isArrived)
            //    _ignoreTempRelationList.Remove(relation.Id);
            return UpdateCurrentRelationUploadDataNode(relation, isArrived);
            //}
            //else {

            //return UpdateCurrentRelationUploadDataNode(relation, false);
            //}
            //查询当前节点冷藏载体的下一条温湿度数据，如果大于下一个节点的时间，则进入下一个节点
            //string sql = "select StorageId from waybill_Node where id = " + relation.CurrentUploadDataNodeId;
            //object storageId = DbHelperMySQL.GetSingle(sql);
            //if (storageId == null)
            //    throw new Exception(string.Format("获取下一节点失败:R【{0}】 O【{1}】 N【{2}】", relation.RelationId, relation.Number, relation.CurrentUploadDataNodeId));
            //sql = string.Format("select dataTime from history_data_{0} where dataTime > '{1}' limit 1", storageId, relation.CurrentUploadDataTime);
            //object dataTime = DbHelperMySQL.GetSingle(sql);
            //if (dataTime != null && Convert.ToDateTime(dataTime) > DateTime.Parse(relation.CurrentUploadDataTime))
            //{
            //    if (nodeInfo != null)
            //        relation.CurrentUploadDataNodeId = nodeInfo.Id;
            //    else
            //    {
            //        //查询当前节点是否已经运抵
            //        sql = "select arrived from waybill_node where id = " + relation.CurrentUploadDataNodeId;
            //        object arrived = DbHelperMySQL.GetSingle(sql);
            //        if (arrived == null)
            //            throw new Exception("获取节点信息失败");
            //        isArrived = arrived.ToString() == "2";
            //        if (isArrived)
            //            _ignoreTempRelationList.Remove(relation.Id);
            //    }
            //}
            //else
            //    _ignoreTempRelationList.Add(relation.Id);
            //return UpdateCurrentRelationUploadDataNode(relation, isArrived);
        }

        /// <summary>
        /// 更新冷链数据上报处理进度
        /// </summary>
        /// <param name="progress"></param>
        /// <returns></returns>
        public static bool UpdateUploadProgress(UploadDataProgress progress, bool haveData = true)
        {
            int handleFlag = 0;
            string sql = string.Empty;
            try
            {
                if (haveData && progress.endNodeTime > progress.uploadProgress)
                {
                    sql = string.Format("update uploadDataProgress set lastHandleTime='{0}',uploadProgress='{1}' where Id = {2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), progress.uploadProgress.ToString("yyyy-MM-dd HH:mm:ss"), progress.Id);
                    DbHelperMySQL.ExecuteSql(sql);
                    return true;//如果还有数据则不更新标记
                }
                handleFlag = progress.endNodeTime <= progress.uploadProgress ? 1 : 0;
                sql = string.Format("update uploadDataProgress set handleFlag='{0}',lastHandleTime='{1}',uploadProgress='{3}' where Id = {2}", handleFlag, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), progress.Id, progress.uploadProgress.ToString("yyyy-MM-dd HH:mm:ss"));
                return DbHelperMySQL.ExecuteSql(sql) == 1;
            }
            finally
            {
                try
                {
                    if (Utility._LinkType == "2" && handleFlag == 1)
                    {
                        //检查是否所有节点与冷链数据都已经上报完成
                        sql = string.Format("select count(*)-p.c as count from waybill_node n INNER JOIN (select Count(*) c from uploaddataprogress where relationId='{0}' and handleFlag=1) p  where n.scannumber='{0}';", progress.relationId);
                        int count = Convert.ToInt32(DbHelperMySQL.GetSingle(sql));
                        if (count == 1)
                        {
                            //数据已上报完成
                            M_TMSEnd end = new M_TMSEnd() { Arrived = true, OrderNo = progress.relationId, JcOrderNo = GetJcOrderNoForProgress(progress.relationId) };
                            string xmlStr = Utility.ParseXMLToString(end);
                            string receiverCode = TMSOrderServer.GetAllLoginkSenderCode("2")[0];
                            LoginkHelp.Send(Utility._SecurityURL, Utility._TransportURL, Utility._MyCode, Utility._MyPwd, receiverCode, xmlStr, ActionType.JTWL_ENTRUST_TRANS_BookingNoteStatus);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("上报TMSEnd失败:" + ex.Message);
                }
            }
        }

        public static bool UpdateCurrentRelationUploadDataNode(RelationModel relation, bool isArrived)
        {
            if (relation.CurrentUploadDataNodeId == -1 || string.IsNullOrEmpty(relation.CurrentUploadDataTime))
                return true;
            string sql = string.Format("update huadong_tmsorder_waybillbase set CurrentUploadDataNodeId = '{0}',CurrentUploadDataTime='{1}'  where id='{2}'", isArrived ? 0 : relation.CurrentUploadDataNodeId, relation.CurrentUploadDataTime, relation.Id);
            try
            {
                bool result = DbHelperMySQL.ExecuteSql(sql) > 0;
                UpdateHandleTHTime(relation.Id, result);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("RelationId[" + relation.RelationId + "] 更新节点冷链数据进度出错：" + ex.Message + " SQL[" + sql + "]");
            }
            finally
            {
                if (isArrived && Utility._LinkType == "2")
                {
                    try
                    {
                        //数据已上报完成
                        M_TMSEnd end = new M_TMSEnd() { Arrived = true, OrderNo = relation.RelationId, JcOrderNo = relation.Number };
                        string xmlStr = Utility.ParseXMLToString(end);
                        string receiverCode = TMSOrderServer.GetAllLoginkSenderCode(relation.CustomerId.ToString())[0];
                        LoginkHelp.Send(Utility._SecurityURL, Utility._TransportURL, Utility._MyCode, Utility._MyPwd, receiverCode, xmlStr, ActionType.JTWL_ENTRUST_TRANS_BookingNoteStatus);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("上报TMSEnd失败[" + ex.Message + "]");
                    }
                }
            }
        }

        public static bool UpdateHandleTHTime(long relationId, bool handleSuccess)
        {
            try
            {
                string sql = string.Format("update huadong_tmsorder_waybillbase set handleTHLastTime='{0}' {1} where id='{2}'", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), (handleSuccess ? "" : ",handleTHcount=ifnull(handleTHcount,0)+1"), relationId);
                return DbHelperMySQL.ExecuteSql(sql) > 0;
            }
            catch (Exception ex)
            {
                throw new Exception("更新处理次数/时间出错：" + ex.Message);
            }
        }

        public static Model.NodeDataUpload.XML GetNextWaitUploadDatasProgress(List<string> senderCodeList, ref List<M_TMSData> loginkData, out UploadDataProgress progress)
        {
            Model.NodeDataUpload.XML result = null;
            progress = null;
            try
            {
                progress = GetNextWaitUploadDataProgress(senderCodeList);
                if (progress != null)
                {
                    DataTable dt = GetStorageData(0, progress.uploadProgress, progress.endNodeTime.AddDays(int.Parse(Utility._StorageDataTimeOut)), progress.storageId);
                    if (dt == null)
                    {
                        string info = string.Format("PId【{0}】 RId【{1}】 Time【{2}】", progress.Id, progress.relationId, progress.uploadProgress.ToString("yyyy-MM-dd HH:mm:ss"));
                        if ((DateTime.Now - progress.uploadProgress).TotalDays > int.Parse(Utility._StorageDataTimeOut))
                        {
                            progress.uploadProgress = DateTime.Now; //progress.endNodeTime;
                            UpdateUploadProgress(progress, false);
                            throw new Exception(info + "放弃等待当前载体温湿度,已过" + int.Parse(Utility._StorageDataTimeOut) + "*24小时");
                        }
                        else
                        {
                            UpdateUploadProgress(progress, false);
                            throw new Exception(info + "没有新的载体温湿度.");
                        }
                    }
                    result = new Model.NodeDataUpload.XML();
                    result.CONTENTLIST = new List<CONTENT>();
                    result.CONTENTLIST.Add(new CONTENT());
                    result.CONTENTLIST[0].DETAILLIST = new List<DETAIL>();
                    DETAIL dModel = new DETAIL();
                    dModel.ECNO = progress.shipmentCode;
                    dModel.LEGNO = progress.relationId;
                    Utility.AddLogText(string.Format("dModel:ECNO:{0},LEGNO:{1},RelationId:{2}", dModel.ECNO, dModel.LEGNO, progress.relationId));
                    if (progress.storageType == 1)
                        dModel.WAREHOUSECODE = progress.storageName;
                    else if (progress.storageType == 2)
                        dModel.LICENSENO = progress.storageName;
                    //GetStorageByNodeId((int)relation.CurrentUploadDataNodeId, ref dModel);
                    loginkData = new List<M_TMSData>();
                    foreach (DataRow row in dt.Rows)
                    {
                        string temp = row["t"] is DBNull ? string.Empty : row["t"].ToString();
                        string hump = "-300.0";
                        try
                        {

                           hump= row["h"] is DBNull ? string.Empty : row["h"].ToString();
                        }
                        catch 
                        {
                            
                        }
                        if (temp == "System.Byte[]")
                            temp = System.Text.Encoding.Default.GetString(row["t"] as byte[]);
                        if (hump == "System.Byte[]")
                            hump = System.Text.Encoding.Default.GetString(row["h"] as byte[]);

                        DETAIL detail = new DETAIL();
                        detail.ECNO = dModel.ECNO;
                        detail.LEGNO = dModel.LEGNO;
                        detail.LICENSENO = dModel.LICENSENO;
                        detail.WAREHOUSECODE = dModel.WAREHOUSECODE;
                        detail.TRACKTIME = Convert.ToDateTime(row["datatime"]).ToString("yyyy-MM-dd HH:mm:ss");

                        detail.TEMPREATURE = temp;//.Replace("-300.0", "").Replace("-300", "");
                        detail.HUMIDITY = hump;//.Replace("-300.0", "").Replace("-300", "");
                        try
                        {
                            detail.LONGITUDE = row["lo"] is DBNull ? string.Empty : row["lo"].ToString();
                        }
                        catch
                        {
                        }
                        try
                        {
                            detail.LATITUDE = row["la"] is DBNull ? string.Empty : row["la"].ToString();
                        }
                        catch
                        {
                        }
                        Utility.AddLogText(string.Format("detail:ECNO:{0},LEGNO:{1}", detail.ECNO, detail.LEGNO));
                        //result.CONTENTLIST[0].DETAILLIST.Add(detail);

                        M_TMSData data = new M_TMSData();
                        data.Latitude = detail.LATITUDE == string.Empty ? null : detail.LATITUDE.ToString();
                        data.Longitude = detail.LONGITUDE == string.Empty ? null : detail.LONGITUDE.ToString();
                        data.RecordTime = Convert.ToDateTime(row["datatime"]);
                        data.OrderNo = progress.relationId;
                        data.JcOrderNo = GetJcOrderNoForProgress(progress.relationId);
                        data.JcNodeId = GetJcNodeIdForProgress(progress.relationId, progress.storageId, progress.nodeTime.ToString("yyyy-MM-dd HH:mm:ss"));
                        Type t = data.GetType();
                        for (int i = 0; i < temp.Split('|').Count(); i++)
                        {
                            t.GetProperty("T" + (i + 1)).SetValue(data, temp.Split('|')[i].ToString(), null);
                        }
                        for (int i = 0; i < hump.Split('|').Count(); i++)
                        {
                            t.GetProperty("RH" + (i + 1)).SetValue(data, hump.Split('|')[i].ToString(), null);
                        }
                        //loginkData.Add(data);
                        progress.uploadProgress = DateTime.Parse(detail.TRACKTIME);
                        if (DateTime.Parse(detail.TRACKTIME) > progress.endNodeTime)
                        {
                            progress.uploadProgress = progress.endNodeTime;
                            if (loginkData.Count == 0)
                            {
                                bool isUpdate = DataUploadServer.UpdateUploadProgress(progress);
                                throw new Exception("仅一条末尾数据且大于节点结束时间，直接更新此进度：" + (isUpdate ? "成功" : "失败"));
                            }
                            break;
                        }
                        else
                        {
                            loginkData.Add(data);
                            result.CONTENTLIST[0].DETAILLIST.Add(detail);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("获取冷链数据失败:" + ex.Message);
            }
            return result;
        }

        private static string GetJcOrderNoForProgress(string relationId)
        {
            string jcOrderNo = relationId;
            try
            {
                string sql = string.Format("select number from huadong_tmsorder_waybillbase where relationId = '{0}'", relationId);
                jcOrderNo = DbHelperMySQL.GetSingle(sql).ToString();
            }
            catch
            {
            }
            return jcOrderNo;
        }

        private static long GetJcNodeIdForProgress(string scanNumber, int storageId, string operateAt)
        {
            long jcNodeId = -1;
            try
            {
                string sql = string.Format("select id from waybill_node where scanNumber='{0}' and storageId={1} and operateAt='{2}' limit 1", scanNumber, storageId, operateAt);
                jcNodeId = Convert.ToInt64(DbHelperMySQL.GetSingle(sql));
            }
            catch
            {
            }
            return jcNodeId;
        }

        public static Model.NodeDataUpload.XML GetNextWaitUploadDatas(List<string> senderCodeList, ref List<M_TMSData> loginkData, out RelationModel relation)
        {
            Model.NodeDataUpload.XML result = null;
            relation = new RelationModel();
            try
            {
                relation = GetNextWaitUploadDataOrderRelation(senderCodeList);//, ref _ignoreTempRelationList);
                if (relation != null)
                {
                    NodeInfo nodeInfo = NodeUploadServer.GetNextUploadNodeInfo(relation.CurrentUploadDataNodeId, relation.Number);
                    if (nodeInfo != null)
                    {
                        //从未上报过节点数据
                        if (string.IsNullOrEmpty(relation.CurrentUploadDataTime) || relation.CurrentUploadDataNodeId == null || relation.CurrentUploadDataNodeId == -1)
                        {
                            relation.CurrentUploadDataTime = nodeInfo.OperateAt;
                            relation.CurrentUploadDataNodeId = nodeInfo.Id;
                            //更新冷链数据上报进度
                            bool isUpdataRelation = UpdateCurrentRelationUploadDataNode(relation, false);
                            string info = string.Format("R【{0}】 O【{1}】 N【{2}】 T【{3}】", relation.RelationId, relation.Number, relation.CurrentUploadDataNodeId, relation.CurrentUploadDataTime);
                            throw new Exception(info + (isUpdataRelation ? "更新为首节点成功." : "更新为首节点失败."));
                            //throw new Exception("首个节点暂不上报数据 " );
                        }
                        else
                        {
                            //获取待上报数据
                            DataTable dt = GetStorageData((int)relation.CurrentUploadDataNodeId, DateTime.Parse(relation.CurrentUploadDataTime), DateTime.Parse(nodeInfo.OperateAt).AddDays(1));//结束时间+30分钟改为24小时
                            if (dt == null)
                            {
                                string info = string.Format("R【{0}】 O【{1}】 N【{2}】 T【{3}】", relation.RelationId, relation.Number, relation.CurrentUploadDataNodeId, relation.CurrentUploadDataTime);
                                if ((DateTime.Now - DateTime.Parse(relation.CurrentUploadDataTime)).TotalDays > int.Parse(Utility._StorageDataTimeOut))
                                {
                                    relation.CurrentUploadDataTime = nodeInfo.OperateAt;
                                    relation.CurrentUploadDataNodeId = nodeInfo.Id;
                                    bool isUpdataRelation = UpdateCurrentRelationUploadDataNode(relation, false);
                                    throw new Exception(info + "放弃等待当前载体温湿度,已过" + int.Parse(Utility._StorageDataTimeOut) + "*24小时 更新为下一节点" + (isUpdataRelation ? "成功." : "失败."));
                                }
                                else
                                {
                                    UpdateHandleTHTime(relation.Id, false);
                                    //_ignoreTempRelationList.Add(relation.Id);
                                    throw new Exception(info + "没有新的载体温湿度.");
                                }
                            }
                            result = new Model.NodeDataUpload.XML();
                            result.CONTENTLIST = new List<CONTENT>();
                            result.CONTENTLIST.Add(new CONTENT());
                            result.CONTENTLIST[0].DETAILLIST = new List<DETAIL>();
                            DETAIL dModel = new DETAIL();
                            dModel.ECNO = NodeUploadServer.GetShipmentCodeByTMSOrder(relation.RelationId);
                            dModel.LEGNO = relation.RelationId;
                            Utility.AddLogText(string.Format("dModel:ECNO:{0},LEGNO:{1},RelationId:{2}", dModel.ECNO, dModel.LEGNO, relation.RelationId));
                            GetStorageByNodeId((int)relation.CurrentUploadDataNodeId, ref dModel);
                            loginkData = new List<M_TMSData>();
                            foreach (DataRow row in dt.Rows)
                            {
                                string temp = row["t"] is DBNull ? string.Empty : row["t"].ToString();
                                string hump = string.Empty;
                                try
                                {
                                    hump = row["h"] is DBNull ? string.Empty : row["h"].ToString();
                                }
                                catch 
                                {
                                }
                                if (temp == "System.Byte[]")
                                    temp = System.Text.Encoding.Default.GetString(row["t"] as byte[]);
                                if (hump == "System.Byte[]")
                                    hump = System.Text.Encoding.Default.GetString(row["h"] as byte[]);

                                DETAIL detail = new DETAIL();
                                detail.ECNO = dModel.ECNO;
                                detail.LEGNO = dModel.LEGNO;
                                detail.LICENSENO = dModel.LICENSENO;
                                detail.WAREHOUSECODE = dModel.WAREHOUSECODE;
                                detail.TRACKTIME = Convert.ToDateTime(row["datatime"]).ToString("yyyy-MM-dd HH:mm:ss");

                                detail.TEMPREATURE = temp.Replace("-300.0", "").Replace("-300", "");
                                detail.HUMIDITY = hump.Replace("-300.0", "").Replace("-300", "");
                                try
                                {
                                    detail.LONGITUDE = row["lo"] is DBNull ? string.Empty : row["lo"].ToString();
                                }
                                catch
                                {
                                }
                                try
                                {
                                    detail.LATITUDE = row["la"] is DBNull ? string.Empty : row["la"].ToString();
                                }
                                catch
                                {
                                }
                                Utility.AddLogText(string.Format("detail:ECNO:{0},LEGNO:{1}", detail.ECNO, detail.LEGNO));
                                result.CONTENTLIST[0].DETAILLIST.Add(detail);

                                M_TMSData data = new M_TMSData();
                                data.Latitude = detail.LATITUDE == string.Empty ? null : detail.LATITUDE.ToString();
                                data.Longitude = detail.LONGITUDE == string.Empty ? null : detail.LONGITUDE.ToString();
                                data.RecordTime = Convert.ToDateTime(row["datatime"]);
                                data.OrderNo = relation.RelationId;
                                data.JcOrderNo = relation.Number;
                                data.JcNodeId = (int)relation.CurrentUploadDataNodeId;
                                Type t = data.GetType();
                                for (int i = 0; i < temp.Split('|').Count(); i++)
                                {
                                    t.GetProperty("T" + (i + 1)).SetValue(data, temp.Split('|')[i].ToString(), null);
                                }
                                for (int i = 0; i < hump.Split('|').Count(); i++)
                                {
                                    t.GetProperty("RH" + (i + 1)).SetValue(data, hump.Split('|')[i].ToString(), null);
                                }
                                loginkData.Add(data);
                                relation.CurrentUploadDataTime = detail.TRACKTIME;
                                if (DateTime.Parse(detail.TRACKTIME) >= DateTime.Parse(nodeInfo.OperateAt))
                                    break;
                            }
                        }
                    }
                    else
                        UpdataUploadDataNode(relation);//, ref _ignoreTempRelationList);
                }
            }
            catch (Exception ex)
            {
                //if (relation != null)
                //    _ignoreTempRelationList.Add(relation.Id);
                throw new Exception(ex.Message);
            }
            return result;
        }

        /// <summary>
        /// 查询需要上报的节点数据
        /// </summary>
        /// <param name="nodeId">当前上报数据的节点ID</param>
        /// <param name="startTime">数据开始时间</param>
        /// <param name="endTime">数据结束时间</param>
        /// <returns></returns>
        private static DataTable GetStorageData(int nodeId, DateTime startTime, DateTime endTime, int? storageId = null)
        {
            string sql = string.Empty;
            if (storageId == null)
            {
                sql = "select StorageId from waybill_Node where id = " + nodeId;
                object obj = DbHelperMySQL.GetSingle(sql);
                if (obj == null)
                    throw new Exception("获取节点冷藏载体失败:[NodeId]" + nodeId);
                storageId = Convert.ToInt32(obj);
            }
            sql = string.Format("select * from aiinfo where storageId = '" + storageId + "' and actived=0 order by pointType");
            StringBuilder dataSql = new StringBuilder("select * from (select t1.dataTime,CONCAT(");

            try
            {
                bool haveH = false;
                string notNullPoint = string.Empty;
                using (MySqlDataReader reader = DbHelperMySQL.ExecuteReader(sql))
                {
                    while (reader.Read())
                    {
                        int pointType = Convert.ToInt32(reader["pointType"]);
                        int pointId = Convert.ToInt32(reader["pointId"]);
                        switch (pointType)
                        {
                            case 1:
                                dataSql.AppendLine("max(case t1.pointId when '" + pointId + "' then ROUND(t1.data,1) end),'|',");
                                if (!notNullPoint.Contains("t is not null"))
                                    notNullPoint += " t is not null";
                                break;
                            case 2:
                                //if (haveH)
                                //    break;
                                haveH = true;
                                if (!dataSql.ToString().Contains(") as 't'"))
                                {
                                    dataSql.Length -= 7;
                                    dataSql.Append(") as 't',CONCAT(");
                                }
                                dataSql.AppendLine("max(case t1.pointId when '" + pointId + "' then ROUND(t1.data,1) end),'|',");
                                //dataSql.AppendLine(") as 't',max(case t1.pointType when 2 then ROUND(t1.data,1) end) as 'h'");
                                if (!notNullPoint.Contains("h is not null"))
                                    notNullPoint += " and h is not null";
                                break;
                            case 3:
                                if (haveH)
                                {
                                    if (!dataSql.ToString().Contains(") as 'h'"))
                                    {
                                        dataSql.Length -= 7;
                                        dataSql.Append(") as 'h'");
                                    }
                                }
                                else if (!dataSql.ToString().Contains(") as 't'"))
                                {
                                    dataSql.Length -= 7;
                                    dataSql.Append(") as 't'");
                                }
                                dataSql.AppendLine(",max(case t1.pointType when 3 then ROUND(t1.data,4) end) as 'lo'");
                                if (!notNullPoint.Contains("lo is not null"))
                                    notNullPoint += " and lo is not null";
                                break;
                            case 4:
                                if (haveH)
                                {
                                    if (!dataSql.ToString().Contains(") as 'h'"))
                                    {
                                        dataSql.Length -= 7;
                                        dataSql.Append(") as 'h'");
                                    }
                                }
                                else if (!dataSql.ToString().Contains(") as 't'"))
                                {
                                    dataSql.Length -= 7;
                                    dataSql.Append(") as 't'");
                                }
                                dataSql.AppendLine(",max(case t1.pointType when 4 then ROUND(t1.data,4) end) as 'la'");
                                if (!notNullPoint.Contains("la is not null"))
                                    notNullPoint += " and la is not null";
                                break;
                        }
                    }
                }
                if (haveH && !dataSql.ToString().Contains(") as 'h'"))
                {
                    dataSql.Length -= 7;
                    dataSql.Append(") as 'h'");
                }
                //startTime.ToString("yyyy-MM-dd HH:mm:ss")
                //endTime.ToString("yyyy-MM-dd HH:mm:ss")
                dataSql.AppendLine(string.Format(" from (select a.pointId,a.pointType, d.data, d.datatime from aiinfo a join history_data_{0} d on a.pointId = d.pointId where d.datatime > '{1}' and d.datatime<= '{2}') t1 GROUP BY t1.dataTime", storageId, startTime.ToString("yyyy-MM-dd HH:mm:ss"), endTime.ToString("yyyy-MM-dd HH:mm:ss")));
                dataSql.AppendLine(") t2 where " + notNullPoint + " order by dataTime limit " + Utility._NodeDataUploadCount);
                DataSet ds = DbHelperMySQL.Query(dataSql.ToString());
                if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                    return null;
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + "[" + dataSql.ToString() + "]");
            }
        }

        /// <summary>
        /// 获取指定节点的关联的冷藏载体信息
        /// </summary>
        /// <param name="nodeId">指定节点ID</param>
        /// <returns></returns>
        private static void GetStorageByNodeId(int nodeId, ref DETAIL d)
        {
            try
            {
                string sql = "select n.*,s.StorageType,s.Driver,s.DriverTel from waybill_Node n left join coldStorage s on n.storageId=s.id where n.id='" + nodeId + "'";
                string storageName = string.Empty;
                int storageType = 0;
                using (MySqlDataReader reader = DbHelperMySQL.ExecuteReader(sql))
                {
                    if (reader.Read())
                    {

                        storageName = reader["storageName"].ToString();
                        storageType = Convert.ToInt32(reader["storageType"]);
                    }
                }
                if (storageType == 1)
                    d.WAREHOUSECODE = storageName;
                else if (storageType == 2)
                    d.LICENSENO = storageName;
            }
            catch (Exception ex)
            {
                throw new Exception("获取节点时间失败:" + ex.Message);
            }
        }

        /// <summary>
        /// 获取一条冷链数据上报进度
        /// </summary>
        /// <param name="senderCodeList"></param>
        /// <returns></returns>
        private static UploadDataProgress GetNextWaitUploadDataProgress(List<string> senderCodeList)
        {
            UploadDataProgress progress = null;
            string sql = "select * from (select p.*,o.ShipmentCode from uploadDataProgress p left join huadong_tms_order o on p.relationId = o.SHIPDETAILID or p.relationId=o.LEGCODE where p.handleFlag = -1 and endnodeTime is not null and 【SecretKey】  order by p.nodetime limit 1)a left JOIN coldStorage b on a.StorageId = b.id";
            string secretkeySql = "SecretKey ='" + Utility._SecretKey + "'  and SHIPMENTCODE<>''";
            if (senderCodeList != null)
                secretkeySql = string.Format("SecretKey in ('{0}')", string.Join("','", senderCodeList));
            sql = sql.Replace("【SecretKey】", secretkeySql);
            using (MySqlDataReader reader = DbHelperMySQL.ExecuteReader(sql))
            {
                if (reader.Read())
                {
                    progress = new UploadDataProgress();
                    progress.Id = Convert.ToInt32(reader["Id"]);
                    progress.relationId = reader["relationId"].ToString();
                    progress.storageId = Convert.ToInt32(reader["storageId"]);
                    progress.storageName = reader["storageName"].ToString();
                    progress.nodeTime = Convert.ToDateTime(reader["nodeTime"]);
                    progress.endNodeTime = Convert.ToDateTime(reader["endNodeTime"]);
                    progress.uploadProgress = Convert.ToDateTime(reader["uploadProgress"]);
                    progress.shipmentCode = reader["shipmentCode"].ToString();
                    progress.storageType = Convert.ToInt32(reader["storageType"]);
                }
            }
            if (progress == null)
            {
                int count = 0;
                try
                {
                    //本轮检测完毕，重置标记，开始下一轮检测
                    sql = "update uploadDataProgress p inner join (select relationId from  huadong_tms_order where 【SecretKey】) as o set handleflag = -1 where handleflag=0 and p.relationId in(o.relationId)";
                    sql = sql.Replace("【SecretKey】", secretkeySql);
                    count = DbHelperMySQL.ExecuteSql(sql);
                    Console.WriteLine("共" + count + "条处理进度已处理完一轮,已重置进度标记成功.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("共" + count + "条处理进度已处理完一轮,重置进度标记时失败：" + ex.Message);
                }
            }
            return progress;
        }

        public static RelationModel GetNextWaitUploadDataOrderRelation(List<string> senderCodeList)//, ref List<long> _ignoreTempRelationList)
        {
            RelationModel result = null;
            string sql = "select * from huadong_tmsorder_waybillbase where (currentUploadDataNodeId<>0 or currentUploadDataNodeId is null) and relationId in (select relationId from huadong_tms_order where 【where】 and relationId is not null) 【ignore】  limit 1";
            //if (_ignoreTempRelationList.Count > 0)
            //    sql = sql.Replace("【ignore】", string.Format("and id not in({0})", string.Join(",", _ignoreTempRelationList)));
            //else
            //    sql = sql.Replace("【ignore】", "");
            //排除集合改为使用权重列和id排序
            sql = sql.Replace("【ignore】", " order by handleTHCount,id desc ");
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
                        result.CurrentUploadDataNodeId = reader["CurrentUploadDataNodeId"] is DBNull ? -1 : Convert.ToInt32(reader["CurrentUploadDataNodeId"]);
                        result.CurrentUploadDataTime = reader["CurrentUploadDataTime"] is DBNull ? string.Empty : Convert.ToDateTime(reader["CurrentUploadDataTime"]).ToString("yyyy-MM-dd HH:mm:ss");
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
            string sql = "select count(*) from huadong_tmsorder_waybillbase where (CurrentUploadDataNodeId is null or CurrentUploadDataNodeId=-1 or CurrentUploadDataNodeId<>0)  and relationId in (select relationId from huadong_tms_order where 【where】 and relationId is not null)";
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
