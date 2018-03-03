using C2LP.ColdStorageDataHubClient.DBHelper.BLL;
using C2LP.ColdStorageDataHubClient.DBHelper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace C2LP.ColdStorageDataHubClient
{
    public class UploadHelper
    {
        private const string _AiColm = "Id,AI1,AI2,AI3,AI4,AI5,AI6,AI7,AI8,AI9,AI10,AI11,AI12,AI13,AI14,AI15,AI16,AI17,AI18,AI19,AI20,AI21,AI22,AI23,AI24,AI25,AI26,AI27,AI28,AI29,AI30,AI31,AI32";

        private const string _AlarmColm = ",Ref1_RefAlarmState,Ref2_RefAlarmState,Ref3_RefAlarmState,Ref4_RefAlarmState,Ref5_RefAlarmState,Ref6_RefAlarmState,Ref7_RefAlarmState,Ref8_RefAlarmState,Ref9_RefAlarmState,Ref10_RefAlarmState,Ref11_RefAlarmState,Ref12_RefAlarmState,Ref13_RefAlarmState,Ref14_RefAlarmState,Ref15_RefAlarmState,Ref16_RefAlarmState";

        private System.Timers.Timer _Tm;
        private string _ProjectNo = string.Empty;
        private int _NetId;
        private DateTime _LastUploadTime;
        private bool _isWorking;
        private string _RefTableName = string.Empty;
        private string _CarTableName = string.Empty;
        private string _CarStartUpTableName = string.Empty;
        private int _Inteval = 3000;
        private bool _stop = false;
        private Thread _thWork;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="projectNo"></param>
        /// <param name="netId"></param>
        /// <param name="lastUploadTime"></param>
        public UploadHelper(string projectNo, int netId, DateTime lastUploadTime)
        {
            _LastUploadTime = lastUploadTime;
            _ProjectNo = projectNo;
            _NetId = netId;
            _RefTableName = string.Format("tbcchddata_{0}_{1}", _ProjectNo, _NetId);
            _CarTableName = string.Format("tbcchistdata_car_{0}_1", _ProjectNo);
            _CarStartUpTableName = string.Format("tbcchiststartup_{0}_1", _ProjectNo);
            _Tm = new System.Timers.Timer();
            _Tm.Interval = 1000;//每隔3秒检查一次当前任务是否处理完成
            _Tm.Elapsed += _Tm_Elapsed;
        }

        /// <summary>
        /// 释放
        /// </summary>
        public void Disposed()
        {
            _stop = true;
            _Tm.Stop();
            try
            {
                if (_thWork != null)
                    _thWork.Abort();
                _thWork = null;
            }
            catch { }
        }

        /// <summary>
        /// 启动
        /// </summary>
        public void Start()
        {
            _stop = false;
            _Tm.Interval = _Inteval;
            _Tm.Start();
        }

        /// <summary>
        /// 停止
        /// </summary>
        public void Stop()
        {
            _stop = true;
            _Tm.Stop();
        }

        #region 执行定时任务
        private void _Tm_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (!_isWorking)
            {
                //if (_thWork != null && _thWork.ThreadState == ThreadState.Running)
                //    _thWork.Abort();
                //_thWork = null;
                //_thWork = new Thread(DoWork);
                //_thWork.Priority = ThreadPriority.Highest;
                //_thWork.Name = string.Format("上报线程[ProjectNo={0}][NetId={1}]", _ProjectNo, _NetId);
                //_thWork.IsBackground = true;

                //_thWork.Start();
                try
                {
                    _Tm.Stop();
                    DoWork();
                }
                catch (Exception ex)
                {
                    FrmMain._MainForm.AppendLogText(string.Format("异常[ProjectId={0}] [NetId={1}] [{2}]", _ProjectNo, _NetId, ex.Message)); ;
                }
                finally
                {
                    _Tm.Start();
                }

            }
        }


        private void DoWork()
        {
            if (_isWorking)
                return;
            else
            {
                try
                {
                    _isWorking = true;
                    if (_NetId != 0)
                    {
                        List<TbccRefAiData> refHdList = GetRefHistData();
                        if (refHdList.Count > 0)
                        {
                            if (UploadRefHdList(refHdList))
                                FrmMain._MainForm.UpdateLastTime(_ProjectNo, _NetId, _LastUploadTime);
                        }
                        else
                            FrmMain._MainForm.AppendLogText(string.Format("无数据[ProjectId={0}] [NetId={1}]", _ProjectNo, _NetId));
                    }
                    else
                    {
                        List<TbccCarAiData> carHdList = GetCarHistData();
                        if (carHdList.Count > 0)
                        {
                            if (UploadCarHdList(carHdList))
                                FrmMain._MainForm.UpdateLastTime(_ProjectNo, _NetId, _LastUploadTime);
                        }
                        else
                            FrmMain._MainForm.AppendLogText(string.Format("无数据[ProjectId={0}] [NetId={1}]", _ProjectNo, _NetId));
                    }
                }
                catch (Exception ex)
                {
                    FrmMain._MainForm.AppendLogText(string.Format("上报失败[ProjectId={0}] [NetId={1}]:{2}", _ProjectNo, _NetId, ex.Message));
                }
                finally
                {
                    //FrmMain._MainForm.AppendLogText(string.Format("本轮上报结束[ProjectId={0}] [NetId={1}] 下次上报时间大约在{2}秒后......", _ProjectNo, _NetId, FrmMain._uploadInteval));
                    //休息间隔
                    try
                    {
                        for (int i = 0; i < FrmMain._uploadInteval * 10; i++)
                        {
                            if (_stop)
                                break;
                            Thread.Sleep(100);
                        }
                    }
                    catch
                    {
                    }
                    finally
                    {
                        _isWorking = false;
                    }
                }

            }
        }
        #endregion

        #region 上报仓库历史数据
        /// <summary>
        /// 查询仓库历史数据
        /// </summary>
        /// <returns></returns>
        private List<TbccRefAiData> GetRefHistData()
        {
            try
            {
                //当前设备关联的库区
                List<string> refCondition = (from l in FrmMain._relationList where l.LinkNetId == _NetId && l.LinkProjectNo == _ProjectNo select string.Format("Ref{0}_RefAlarmState <>1", l.LinkRefId)).Distinct().ToList();
                string refConditionStr = string.Join(" or ", refCondition);
                string currentTimeWhere = string.Format(" and HDATE < '{0}' ", DateTime.Now.AddHours(2).ToString("yyyy-MM-dd HH:mm:ss"));
                string sql = string.Format("select DISTINCT(HDATE),{0},'{6}' as NetId,'{7}' as ProjectId from {1} where HDATE>'{8}' {9} and ((({2}) and unix_timestamp(HDATE) %{3}=0 ) or unix_timestamp(HDATE)%{4}=0) ORDER BY hDate limit {5}", _AiColm + _AlarmColm, _RefTableName, refConditionStr, FrmMain._aSpace, FrmMain._nSpace, FrmMain._uploadLimit, _NetId, _ProjectNo, _LastUploadTime, currentTimeWhere);
                return BaseServer._DBHelper.ReadEntityList<TbccRefAiData>(sql);
            }
            catch (Exception ex)
            {
                FrmMain._MainForm.AppendLogText(string.Format("查询仓库历史数据出错[ProjectId={0}] [NetId={1}]:{2}", _ProjectNo, _NetId, ex.Message));
            }
            return new List<TbccRefAiData>();
        }

        /// <summary>
        /// 上报仓库历史数据
        /// </summary>
        /// <param name="refHdList"></param>
        /// <returns></returns>
        private bool UploadRefHdList(List<TbccRefAiData> refHdList)
        {
            bool flag = false;
            try
            {
                List<WRUpload.Model_NodeHistoryData> uploadList = new List<WRUpload.Model_NodeHistoryData>();
                int index = 0;
                foreach (TbccRefAiData item in refHdList)
                {
                    index++;
                    Type t = item.GetType();
                    bool isAlarmSpace = ((TimeSpan)(item.hDate - DateTime.Parse("1970-1-1"))).TotalSeconds % FrmMain._nSpace != 0;
                    foreach (AiInfoModel aiInfo in FrmMain._relationList.Where(l => l.LinkProjectNo == _ProjectNo && l.LinkNetId == _NetId))
                    {
                        bool isLastAi = aiInfo == FrmMain._relationList.Where(l => l.LinkProjectNo == _ProjectNo && l.LinkNetId == _NetId).Last();
                        if (isAlarmSpace)
                        {
                            //如果是报警间隔的数据，判断当前Ai所在的库是否报警
                            int alarmState = Convert.ToInt32(t.InvokeMember("Ref" + aiInfo.LinkRefId.ToString() + "_RefAlarmState", System.Reflection.BindingFlags.GetProperty, null, item, null));
                            if (alarmState == 2)//如果当前库的报警状态正常则跳过此条数据
                                continue;
                        }
                        WRUpload.Model_NodeHistoryData rdm = new WRUpload.Model_NodeHistoryData();
                        rdm.Datak__BackingField = Convert.ToDecimal(t.InvokeMember("AI" + aiInfo.LinkPortNo, System.Reflection.BindingFlags.GetProperty, null, item, null));
                        if (rdm.Datak__BackingField == -200)
                            continue;
                        rdm.DataTimek__BackingField = item.hDate;//.ToString("yyyy-MM-dd HH:mm:ss");
                        rdm.PointIdk__BackingField = aiInfo.aiNumber;
                        rdm.IsAlarmk__BackingField = isAlarmSpace ? WRUpload.Enum_HdAlarm.Alarm : WRUpload.Enum_HdAlarm.Normal;
                        uploadList.Add(rdm);
                        //超过50条或者是剩余数据 则开始上报
                        if (uploadList.Count >= 50 || (index == refHdList.Count && isLastAi))
                        {
                            int retryCount = -1;
                            bool isUploadSuccess = false;
                            while (retryCount < 3)
                            {
                                retryCount++;
                                WRUpload.ResultModelOfboolean result = FrmMain._uploadClient.UploadRefHistDatas(uploadList.ToArray());
                                if (result.Code == 0)
                                {
                                    _LastUploadTime = item.hDate;
                                    uploadList.Clear();
                                    isUploadSuccess = true;
                                    break;
                                }
                                else
                                    Thread.Sleep(_Inteval);
                            }
                            if (isUploadSuccess == false)
                                throw new Exception(string.Format("重复三次均上报失败[ProjectId={0}] [NetId={1}]", _ProjectNo, _NetId));
                        }
                    }
                }
                flag = true;
            }
            catch (Exception ex)
            {
                FrmMain._MainForm.AppendLogText(string.Format("上报失败[ProjectId={0}] [NetId={1}]:{2}", _ProjectNo, _NetId, ex.Message));
            }
            return flag;
        }
        #endregion

        #region 上报车载历史数据
        /// <summary>
        /// 查询车载历史数据
        /// </summary>
        /// <returns></returns>
        private List<TbccCarAiData> GetCarHistData()
        {
            try
            {
                if (BaseServer.AddUploadStateColumn(_CarTableName, _LastUploadTime))
                    FrmMain._MainForm.AppendLogText(string.Format("添加上报状态并更新成功[ProjectId={0}] [NetId={1}]", _ProjectNo, _NetId));
                List<TbccCarAiData> carHistDataList = new List<TbccCarAiData>();
                //string sql = string.Format("select * from {0} where BeginTime<='{1}' order by BeginTime desc limit 1", _CarStartUpTableName, _LastUploadTime);
                //List<TbccCarStartUp> carStartUp = BaseServer._DBHelper.ReadEntityList<TbccCarStartUp>(sql);
                //if (carStartUp != null && carStartUp.Count > 0)
                //{
                //    string currentTimeWhere = string.Format(" and updateTime < '{0}' ", DateTime.Now.AddHours(2).ToString("yyyy-MM-dd HH:mm:ss"));
                //    currentTimeWhere = string.Format(" and parentId = '{0}'", carStartUp[0].Id);//更改策略为根据启停上报，无需根据当前时间判断
                //    sql = string.Format("select * from {0} where updateTime > '{1}' {3} order by updateTime limit {2}", _CarTableName, _LastUploadTime, FrmMain._uploadLimit, currentTimeWhere);
                //    carHistDataList = BaseServer._DBHelper.ReadEntityList<TbccCarAiData>(sql);
                //    //在上位机中启停数据已处理完，并且当前进度之后无数据，表示该启停数据已经完成
                //    if ((carHistDataList.Count == 0 && carStartUp[0].Finished == 1))
                //    {
                //        //获取下一个启停,去下一个启停中查询历史数据
                //        sql = string.Format("select * from {0} where id>{1} order by id limit 1", _CarStartUpTableName, carStartUp[0].Id);
                //        carStartUp = BaseServer._DBHelper.ReadEntityList<TbccCarStartUp>(sql);
                //        if (carStartUp != null && carStartUp.Count > 0)
                //        {
                //            currentTimeWhere = string.Format(" and parentId = '{0}'", carStartUp[0].Id);//更改策略为根据启停上报，无需根据当前时间判断
                //            sql = string.Format("select * from {0} where updateTime > '{1}' {3} order by updateTime limit {2}", _CarTableName, _LastUploadTime, FrmMain._uploadLimit, currentTimeWhere);
                //            carHistDataList = BaseServer._DBHelper.ReadEntityList<TbccCarAiData>(sql);
                //        }
                //    }
                //}

                string sql = string.Format("select * from {0} where fdap4jcUploadState=0 and updateTime>'2015-12-01 00:00' and updateTime<'{2}' order by updateTime limit {1};", _CarTableName, FrmMain._uploadLimit,DateTime.Now.AddHours(2));
                carHistDataList = BaseServer._DBHelper.ReadEntityList<TbccCarAiData>(sql);
                return carHistDataList;
            }
            catch (Exception ex)
            {
                FrmMain._MainForm.AppendLogText(string.Format("查询车载历史数据出错[ProjectId={0}] [NetId={1}]:{2}", _ProjectNo, _NetId, ex.Message));
            }
            return new List<TbccCarAiData>();
        }

        /// <summary>
        /// 更新车载历史数据表的上报状态
        /// </summary>
        /// <param name="idList"></param>
        /// <returns></returns>
        private bool UpdateUploadState(List<int> idList)
        {
            try
            {
                if (idList == null || idList.Count == 0)
                    return true;
                string sql = string.Format("update {0} set fdap4jcUploadState=1 where id in({1})", _CarTableName, string.Join(",", idList));
                return idList.Count == BaseServer._DBHelper.ExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                throw new Exception("更新上报状态失败:"+ex.Message);
            }
        }

        /// <summary>
        /// 上报车载历史数据
        /// </summary>
        /// <param name="carHdList"></param>
        private bool UploadCarHdList(List<TbccCarAiData> carHdList)
        {
            bool flag = false;
            try
            {
                List<WRUpload.Model_NodeHistoryData> uploadList = new List<WRUpload.Model_NodeHistoryData>();
                List<int> carHdIdList = new List<int>();
                int index = 0;
                foreach (TbccCarAiData item in carHdList)
                {
                    index++;
                    carHdIdList.Add(item.Id);
                    Type t = item.GetType();
                    foreach (AiInfoModel aiInfo in FrmMain._relationList.Where(l => l.LinkProjectNo == _ProjectNo && l.LinkNetId == _NetId))
                    {
                        bool isLastAi = aiInfo == FrmMain._relationList.Where(l => l.LinkProjectNo == _ProjectNo && l.LinkNetId == _NetId).Last();
                        WRUpload.Model_NodeHistoryData rdm = new WRUpload.Model_NodeHistoryData();
                        rdm.DataTimek__BackingField = item.UpdateTime;//.ToString("yyyy-MM-dd HH:mm:ss");
                        rdm.PointIdk__BackingField = aiInfo.aiNumber;
                        if (aiInfo.LinkPortNo < 5)
                            rdm.Datak__BackingField = Convert.ToDecimal(t.InvokeMember("AI" + aiInfo.LinkPortNo, System.Reflection.BindingFlags.GetProperty, null, item, null));
                        else if (aiInfo.LinkPortNo == 5)
                            rdm.Datak__BackingField = item.Longitude;
                        else if (aiInfo.LinkPortNo == 6)
                            rdm.Datak__BackingField = item.Latitude;

                        rdm.IsAlarmk__BackingField = item.AlarmStatus == 2 ? WRUpload.Enum_HdAlarm.Normal : WRUpload.Enum_HdAlarm.Alarm;
                        uploadList.Add(rdm);
                        //超过50条或者是剩余数据 则开始上报
                        if (uploadList.Count >= 50 || (index == carHdList.Count && isLastAi))
                        {
                            int retryCount = -1;
                            bool isUploadSuccess = false;
                            while (retryCount < 3)
                            {
                                retryCount++;
                                WRUpload.ResultModelOfboolean result = FrmMain._uploadClient.UploadCarHistDatas(uploadList.ToArray());
                                if (result.Code == 0)
                                {
                                    _LastUploadTime = item.UpdateTime;
                                    uploadList.Clear();
                                    isUploadSuccess = true;
                                    break;
                                }
                                else
                                    Thread.Sleep(_Inteval);
                            }
                            if (isUploadSuccess == false)
                                throw new Exception(string.Format("重复三次均上报失败[ProjectId={0}] [车载]", _ProjectNo));
                        }
                    }
                }
                UpdateUploadState(carHdIdList);
                flag = true;
            }
            catch (Exception ex)
            {
                FrmMain._MainForm.AppendLogText(string.Format("上报失败[ProjectId={0}] [车载]:{1}", _ProjectNo, ex.Message));
            }
            return flag;
        }
        #endregion
    }
}
