using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using C2LP.ColdStorageDataHubClient.DBHelper.Model;
//using TBCC.Ext.Fdap4zgswDataHub.HttpInteface.InterfaceModel;
//using TBCC.Ext.Fdap4zgswDataHub.QueryClass;

namespace C2LP.ColdStorageDataHubClient.DBHelper.BLL
{
    public class TbccDataServer : BaseServer
    {
        #region 查询仓库历史/实时数据
        private const string _AiColm = "Id,AI1,AI2,AI3,AI4,AI5,AI6,AI7,AI8,AI9,AI10,AI11,AI12,AI13,AI14,AI15,AI16,AI17,AI18,AI19,AI20,AI21,AI22,AI23,AI24,AI25,AI26,AI27,AI28,AI29,AI30,AI31,AI32";

        private const string _AlarmColm = ",Ref1_RefAlarmState,Ref2_RefAlarmState,Ref3_RefAlarmState,Ref4_RefAlarmState,Ref5_RefAlarmState,Ref6_RefAlarmState,Ref7_RefAlarmState,Ref8_RefAlarmState,Ref9_RefAlarmState,Ref10_RefAlarmState,Ref11_RefAlarmState,Ref12_RefAlarmState,Ref13_RefAlarmState,Ref14_RefAlarmState,Ref15_RefAlarmState,Ref16_RefAlarmState";

        private const string _RefHistSql = "SELECT [CLMS] date_format(RecordTime,'%Y-%c-%d %H:%i') as 'RecordTime',projectId FROM ( select a.sensorid,a.tname,a.RhName,a.projectId,b.TVALUE,b.RHVALUE,b.RecordTime ,b.TUpDwLimit_Alarm,b.RhUpDwLimit_Alarm from tbccsensorinfo as a left join [TableName] as b on a.sensorid=b.SensorId where a.SensorId in([SensorIdList]) and RecordTime>='[UploadLastTime]' order by recordtime limit [LIMIT] ) AS A GROUP BY recordtime having (unix_timestamp(RecordTime)%[NormalSpace]=0) or (unix_timestamp(RecordTime)%[AlarmSpace]=0) order by RecordTime;";


        ///// <summary>
        ///// 获取指定数量的未上报数据-仓库历史
        ///// </summary>
        ///// <param name="aiList">需要上报的AI集合</param>
        ///// <param name="queryCount">查询数量</param>
        ///// <param name="alarmSpace">报警间隔</param>
        ///// <param name="normalSpace">正常间隔</param>
        ///// <returns></returns>
        //public static List<TbccRefAiData> GetHistDataList_Ref(List<AiInfoModel> aiList, int queryCount, int alarmSpace, int normalSpace)
        //{
        //    List<TbccRefAiData> list = new List<TbccRefAiData>();
        //    //所有需要查询的设备Id
        //    var netIdList = (from l in aiList select new { l.LinkProjectNo, l.LinkNetId }).Distinct();
        //    string sql = string.Empty;
        //    foreach (var item in netIdList)
        //    {
        //        try
        //        {
        //            //当前设备关联的库区
        //            List<string> refCondition = (from l in aiList where l.LinkNetId == item.LinkNetId && l.LinkProjectNo == item.LinkProjectNo select string.Format("Ref{0}_RefAlarmState <>1", l.LinkRefId)).Distinct().ToList();
        //            string refConditionStr = string.Join(" or ", refCondition);
        //            string tableName = string.Format("tbcchddata_{0}_{1}", item.LinkProjectNo, item.LinkNetId);
        //            //添加上报状态的列
        //            AddUploadStateColumn(tableName);
        //            sql = string.Format("select DISTINCT(HDATE),{0},'{6}' as NetId,'{7}' as ProjectId from {1} where fdap4zgswUploadState=0 and ((({2}) and unix_timestamp(HDATE) %{3}=0 ) or unix_timestamp(HDATE)%{4}=0) ORDER BY hDate limit {5}", _AiColm + _AlarmColm, tableName, refConditionStr, alarmSpace, normalSpace, queryCount, item.LinkNetId, item.LinkProjectNo);
        //            List<TbccRefAiData> tempHdList = _DBHelper.ReadEntityList<TbccRefAiData>(sql);
        //            list.AddRange(tempHdList);
        //            Thread.Sleep(100);
        //        }
        //        catch (Exception ex)
        //        {
        //            //QueryHelp.RegisterUploadFailEvent(new UploadClass.UploadFailEventArgs("GetHistDataList_Ref", ex.Message, sql));
        //        }
        //    }
        //    return list;
        //}

        ///// <summary>
        ///// 获取指定数量的未上报数据-5100仓库历史
        ///// </summary>
        ///// <param name="aiList">需要上报的AI集合</param>
        ///// <param name="queryCount">查询数量</param>
        ///// <param name="alarmSpace">报警间隔</param>
        ///// <param name="normalSpace">正常间隔</param>
        //public static List<TbccRefAiData_5100> GetHistDataList_Ref_5100(List<AiInfoModel> aiList, int queryCount, int alarmSpace, int normalSpace, string productStr)
        //{
        //    List<TbccRefAiData_5100> list = new List<TbccRefAiData_5100>();
        //    string sql = string.Empty;
        //    try
        //    {
        //        _DBHelper.ExecuteNonQuery(productStr);
        //        if (aiList.Count > 0)
        //        {
        //            List<int> aiPortList = (from l in aiList select (int)l.LinkPortNo).Distinct().ToList();
        //            sql = string.Format("call GetFdap4zgswHistData_Ref({0},{1},{2},'{3}')", queryCount * aiPortList.Count(), alarmSpace, normalSpace, string.Join(",", aiPortList));
        //            list = _DBHelper.ReadEntityList<TbccRefAiData_5100>(sql);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //QueryHelp.RegisterUploadFailEvent(new UploadClass.UploadFailEventArgs("GetHistDataList_Ref_5100", ex.Message, sql));
        //    }
        //    finally
        //    {
        //        _DBHelper.ExecuteNonQuery("DROP PROCEDURE IF EXISTS GetFdap4zgswHistData_Ref;");
        //    }
        //    return list;
        //}

        ///// <summary>
        ///// 获取所有实时数据-仓库-5100仓库历史
        ///// </summary>
        ///// <returns></returns>
        //public static List<TbccRefAiData_5100> GetRealDataList_Ref_5100(List<AiInfoModel> aiList)
        //{
        //    List<TbccRefAiData_5100> list = new List<TbccRefAiData_5100>();
        //    string sql = string.Empty;
        //    try
        //    {
        //        if (aiList.Count > 0)
        //        {
        //            List<int> aiPortList = (from l in aiList select (int)l.LinkPortNo).Distinct().ToList();
        //            if (aiPortList.Count == 0)
        //                return list;
        //            sql = string.Format("select updateTime,SensorId,TValue,TUpDwLimit_Alarm,RhValue,RhUpDwLimit_Alarm from tbccrealdata_sensor where sensorId in ({0});", string.Join(",", aiPortList));
        //            list = _DBHelper.ReadEntityList<TbccRefAiData_5100>(sql);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //QueryHelp.RegisterUploadFailEvent(new UploadClass.UploadFailEventArgs("GetRealDataList_Ref_5100", ex.Message, sql));
        //    }
        //    return list;
        //}

        ///// <summary>
        ///// 获取所有实时数据-仓库
        ///// </summary>
        ///// <returns></returns>
        //public static List<TbccRefAiData> GetRealDataList_Ref(List<AiInfoModel> aiList)
        //{
        //    List<TbccRefAiData> list = new List<TbccRefAiData>();
        //    string sql = string.Empty;
        //    try
        //    {
        //        if (aiList.Count > 0)
        //        {
        //            sql = string.Format("select ProjectId,NetId,UpdateTime,{0} from tbccrealdata_ref", _AiColm);
        //            list = _DBHelper.ReadEntityList<TbccRefAiData>(sql);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //QueryHelp.RegisterUploadFailEvent(new UploadClass.UploadFailEventArgs("GetRealDataList_Ref", ex.Message, sql));
        //    }
        //    return list;
        //}

        ///// <summary>
        ///// 更新上报后的仓库历史数据
        ///// </summary>
        ///// <param name="netHdId">历史数据Id集合字典</param>
        ///// <param name="projectNo">工程编号</param>
        ///// <returns></returns>
        //public static int UpdateFdapUploadState_Ref(ILookup<int, TbccRefAiData> netHdId, string projectNo)
        //{
        //    StringBuilder sql = new StringBuilder();
        //    try
        //    {
        //        foreach (var netDatas in netHdId)
        //        {
        //            List<int> hdIdList = (from l in netDatas select l.id).ToList();
        //            if (hdIdList.Count == 0)
        //                continue;
        //            sql.AppendFormat("update tbcchddata_{0}_{1} set fdap4zgswUploadState=1 where id in ({2});", projectNo, netDatas.Key, string.Join(",", hdIdList));
        //        }
        //        if (sql.Length == 0)
        //            return 0;
        //        return _DBHelper.ExecuteNonQuery(sql.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        //QueryHelp.RegisterUploadFailEvent(new UploadClass.UploadFailEventArgs("UpdateFdapUploadState_Ref", ex.Message, sql.ToString()));
        //        return 0;
        //    }
        //}

        ///// <summary>
        ///// 更新上报后的仓库历史数据_5100
        ///// </summary>
        ///// <param name="hdIdList">历史数据Id集合字典</param>
        ///// <param name="projectNo">工程编号</param>
        ///// <param name="tableDate">历史数据表的日期</param>
        ///// <returns></returns>
        //public static int UpdateFdapUploadState_Ref_5100(Dictionary<int, List<int>> hdIdList, string projectNo, DateTime tableDate)
        //{
        //    StringBuilder sql = new StringBuilder();
        //    try
        //    {
        //        foreach (var item in hdIdList)
        //        {
        //            if (item.Value == null || item.Value.Count == 0)
        //                continue;
        //            sql.AppendFormat("update tbcchddata_sensor_{0}_{1} set fdap4zgswUploadState={2} where id in ({3});", projectNo, tableDate.ToString("yyyyMM"), item.Key, string.Join(",", item.Value));
        //        }
        //        if (sql.Length == 0)
        //            return 0;
        //        return _DBHelper.ExecuteNonQuery(sql.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        //QueryHelp.RegisterUploadFailEvent(new UploadClass.UploadFailEventArgs("UpdateFdapUploadState_Ref_5100", ex.Message, sql.ToString()));
        //        return 0;
        //    }
        //}
        #endregion

        #region 查询车载历史/实时数据
        /// <summary>
        /// 获取所有车载的启停记录和指定数量历史数据
        /// </summary>
        /// <param name="dicStartUpId">所有车载最后一次上报的启停</param>
        /// <param name="dicLastTime">所有车载最后一次上报的记录时间</param>
        /// <param name="queryCount">查询数量</param>
        /// <returns></returns>
        public static List<TbccCarStartUpAndAiData> GetHistDataList_Car(Dictionary<string, int> dicStartUpId, Dictionary<string, DateTime> dicLastTime, int queryCount)
        {
            List<TbccCarStartUpAndAiData> list = new List<TbccCarStartUpAndAiData>();
            string currentInfo = string.Empty;
            try
            {
                foreach (string carPrjNo in dicStartUpId.Keys)
                {
                    currentInfo = carPrjNo;
                    TbccCarStartUpAndAiData prjData = new TbccCarStartUpAndAiData();
                    prjData.ProjectNo = carPrjNo;
                    int startUpId = dicStartUpId[carPrjNo];
                    prjData.LastTime = dicLastTime[carPrjNo];
                    TbccCarStartUp prjStarUp = GetCarStartUp(carPrjNo, startUpId);
                    List<TbccCarAiData> prjHDList = GetCarHDList(carPrjNo, startUpId, prjData.LastTime, queryCount);
                    if (prjStarUp == null || prjHDList.Count == 0)
                    {
                        prjStarUp = GetCarNextFinishStartUp(carPrjNo, startUpId);
                        if (prjStarUp != null)
                        {
                            prjData.IsStartUpUpload = true;
                            prjData.LastTime = prjStarUp.BeginTime.AddSeconds(-1);
                            prjHDList = GetCarHDList(carPrjNo, prjStarUp.Id, prjData.LastTime, queryCount);
                        }
                    }

                    prjData.StartUp = prjStarUp;
                    prjData.CarDatas = prjHDList;
                    list.Add(prjData);
                    Thread.Sleep(100);
                }
            }
            catch (Exception ex)
            {
                //QueryHelp.RegisterUploadFailEvent(new UploadClass.UploadFailEventArgs("GetHistDataList_Car", ex.Message, currentInfo));

            }
            return list;
        }

        /// <summary>
        /// 获取指定车载工程的指定启停记录
        /// </summary>
        /// <param name="prjNo">指定工程编号</param>
        /// <param name="startUpId">指定启停ID</param>
        /// <returns></returns>
        private static TbccCarStartUp GetCarStartUp(string prjNo, int startUpId)
        {
            TbccCarStartUp tcsu = null;
            string sql = string.Format("select * from tbcchiststartup_{0}_1 where Id={1} and finished=1 ", prjNo, startUpId);
            try
            {
                List<TbccCarStartUp> list = _DBHelper.ReadEntityList<TbccCarStartUp>(sql);
                if (list.Count > 0)
                    tcsu = list[0];
            }
            catch (Exception ex)
            {
                //QueryHelp.RegisterUploadFailEvent(new UploadClass.UploadFailEventArgs("GetCarStartUp", ex.Message, sql));
            }
            return tcsu;
        }


        /// <summary>
        /// 获取下一个启停记录
        /// </summary>
        /// <param name="prjNo">车载项目编号</param>
        /// <param name="startUpId">当前启停Id</param>
        /// <returns></returns>
        private static TbccCarStartUp GetCarNextFinishStartUp(string prjNo, int startUpId)
        {
            TbccCarStartUp tcsu = null;
            string sql = string.Format("select * from tbcchiststartup_{0}_1 where Id>{1} and finished=1 order by id limit 1", prjNo, startUpId);
            try
            {
                List<TbccCarStartUp> list = _DBHelper.ReadEntityList<TbccCarStartUp>(sql);
                if (list.Count > 0)
                    tcsu = list[0];
            }
            catch (Exception ex)
            {
                //QueryHelp.RegisterUploadFailEvent(new UploadClass.UploadFailEventArgs("GetCarNextFinishStartUp", ex.Message, sql));
            }
            return tcsu;
        }

        /// <summary>
        /// 获取指定工程的指定启停的指定时间之后的车载历史数据
        /// </summary>
        /// <param name="prjNo">指定工程编号</param>
        /// <param name="startUpId">指定启停Id</param>
        /// <param name="lastTime">开始时间</param>
        /// <param name="queryCount">查询数量</param>
        /// <returns></returns>
        private static List<TbccCarAiData> GetCarHDList(string prjNo, int startUpId, DateTime lastTime, int queryCount)
        {
            List<TbccCarAiData> list = new List<TbccCarAiData>();
            string sql = string.Format("select * from tbcchistdata_car_{0}_1 where ParentId={1} and UpdateTime>'{2}' order by UpdateTime limit {3}", prjNo, startUpId, lastTime.ToString("yyyy-MM-dd HH:mm:ss"), queryCount);
            try
            {
                list = _DBHelper.ReadEntityList<TbccCarAiData>(sql);
            }
            catch (Exception ex)
            {
                //QueryHelp.RegisterUploadFailEvent(new UploadClass.UploadFailEventArgs("GetCarHDList", ex.Message, sql));
            }
            return list;
        }

        /// <summary>
        /// 获取所有实时数据-车载
        /// </summary>
        /// <returns></returns>
        //public static Dictionary<string, SRCar.CarRealView_new> GetRealDataList_Car(List<string> prjList)
        //{
        //    Dictionary<string, SRCar.CarRealView_new> dic = new Dictionary<string, SRCar.CarRealView_new>();
        //    SRCar.AllService client = new SRCar.AllService();
        //    client.Timeout = 50000;
        //    List<TbccCarPrjType> carPrjs = AiInfoServer.GetCarAiInfo();
        //    foreach (TbccCarPrjType car in carPrjs.Where(l => prjList.Contains(l.CarProjectId)))
        //    {
        //        try
        //        {
        //            SRCar.CarRealView_new real = client.getCarRealData_sy(car.ProjectAuthCode, car.CarProjectId);
        //            dic.Add(car.CarProjectId, real);
        //            Thread.Sleep(100);
        //        }
        //        catch (Exception ex)
        //        {
        //            //QueryHelp.RegisterUploadFailEvent(new UploadClass.UploadFailEventArgs("GetRealDataList_Car", ex.Message, car.CarProjectName));
        //        }
        //    }

        //    return dic;
        //}


        #endregion

        //#region 查询保温箱历史/实时数据
        ///// <summary>
        ///// 获取所有实时数据-保温箱
        ///// </summary>
        ///// <returns></returns>
        ////public static Dictionary<string, SRBox.BoxRealDataView> GetRealDataList_Box(List<string> prjList)
        ////{
        ////    Dictionary<string, SRBox.BoxRealDataView> dic = new Dictionary<string, SRBox.BoxRealDataView>();
        ////    SRBox.loadOperate client = new SRBox.loadOperate();
        ////    client.Timeout = 50000;
        ////    List<TbccBoxPrjType> boxPrjs = AiInfoServer.GetBoxAiInfo();
        ////    foreach (TbccBoxPrjType box in boxPrjs.Where(l => prjList.Contains(l.BoxProjectId)))
        ////    {
        ////        try
        ////        {
        ////            SRBox.BoxRealDataView real = client.getBoxRealData(box.BoxProjectName);
        ////            dic.Add(box.BoxProjectId, real);
        ////            Thread.Sleep(100);
        ////        }
        ////        catch (Exception ex)
        ////        {
        ////            //QueryHelp.RegisterUploadFailEvent(new UploadClass.UploadFailEventArgs("GetRealDataList_Box", ex.Message, box.BoxProjectName));
        ////        }
        ////    }

        ////    return dic;
        ////}

        ///// <summary>
        ///// 获取所有历史数据-保温箱
        ///// </summary>
        ///// <param name="prjIdList">指定工程Id列表</param>
        ///// <param name="queryCount">查询数量</param>
        ///// <returns></returns>
        //public static List<TbccBoxAiData> GetHistDataList_Box(List<string> prjIdList, int queryCount)
        //{
        //    List<TbccBoxAiData> list = new List<TbccBoxAiData>();
        //    string sql = string.Empty;
        //    foreach (string prjId in prjIdList)
        //    {
        //        try
        //        {
        //            string tableName = string.Format("tbcchistdata_box_{0}_1", prjId);
        //            //添加上报状态的列
        //            AddUploadStateColumn(tableName);
        //            string clms = "Id,AI1,AI2,AI3,AI4";
        //            sql = string.Format("select DISTINCT(UpdateTime),{0},'{1}' as ProjectId from {2} where fdap4zgswUploadState=0 ORDER BY UpdateTime limit {3}", clms, prjId, tableName, queryCount);
        //            List<TbccBoxAiData> tempList = _DBHelper.ReadEntityList<TbccBoxAiData>(sql);
        //            list.AddRange(tempList);
        //            Thread.Sleep(100);
        //        }
        //        catch (Exception ex)
        //        {
        //            //QueryHelp.RegisterUploadFailEvent(new UploadClass.UploadFailEventArgs("GetHistDataList_Box", ex.Message, sql));
        //        }
        //    }
        //    return list;
        //}

        ///// <summary>
        ///// 更新上报后的保温箱历史数据
        ///// </summary>
        ///// <param name="prjHdId">历史数据Id集合字典</param>
        ///// <returns></returns>
        //public static int UpdateFdapUploadState_Box(ILookup<string, TbccBoxAiData> prjHdId)
        //{
        //    StringBuilder sql = new StringBuilder();
        //    try
        //    {
        //        foreach (var prjDatas in prjHdId)
        //        {
        //            List<int> hdIdList = (from l in prjDatas select l.Id).ToList();
        //            if (hdIdList.Count == 0)
        //                continue;
        //            sql.AppendFormat("update tbcchistdata_box_{0}_1 set fdap4zgswUploadState=1 where id in ({1});", prjDatas.Key, string.Join(",", hdIdList));
        //        }
        //        if (sql.Length == 0)
        //            return 0;
        //        return _DBHelper.ExecuteNonQuery(sql.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        //QueryHelp.RegisterUploadFailEvent(new UploadClass.UploadFailEventArgs("GetHistDataList_Box", ex.Message, sql.ToString()));
        //        return 0;
        //    }
        //}
        //#endregion
    }
}

