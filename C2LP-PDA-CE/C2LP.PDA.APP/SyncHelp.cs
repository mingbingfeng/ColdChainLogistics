using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using C2LP.PDA.APP.PDAWebReference;
using System.Runtime.InteropServices;
using C2LP.PDA.Datas.BLL;

namespace C2LP.PDA.APP
{
    public class SyncHelp
    {
        /// <summary>
        /// 运行状态
        /// </summary>
        public static bool _isRuning = false;
        /// <summary>
        /// 
        /// </summary>
        public static StringBuilder _runLog = new StringBuilder();
        /// <summary>
        /// 后台执行同步的线程
        /// </summary>
        private static Thread _th;

        public delegate void SyncDelegate(string syncInfo);
        /// <summary>
        /// 事件 同步消息
        /// </summary>
        public static event SyncDelegate SyncEvent;
        /// <summary>
        /// 事件 同步结束
        /// </summary>
        public static event SyncDelegate SyncFinish;

        static int _SyncType = 0;
        /// <summary>
        /// 注册消息事件
        /// </summary>
        /// <param name="info"></param>
        private static void RegistSyncEvent(string info)
        {
            info = DateTime.Now.ToString("HH:mm.ss") + ":" + info + "\r\n";
            _runLog.AppendLine(info);
            if (SyncEvent != null)
                SyncEvent(info);
        }

        /// <summary>
        /// 开始同步
        /// </summary>
        public static void StartSync(int syncType)
        {
            if (!_isRuning)
            {
                _isRuning = true;
                _SyncType = syncType;
                StartWork();
            }
        }

        /// <summary>
        /// 终止同步
        /// </summary>
        public static void StopSync()
        {
            if (_isRuning)
            {
                if (_th != null)
                    _th.Abort();
                _isRuning = false;
            }
        }


        /// <summary>
        /// 开始线程
        /// </summary>
        private static void StartWork()
        {
            _th = new Thread(DoWork);
            _th.IsBackground = true;
            _th.Start();
        }

        /// <summary>
        /// 线程执行
        /// </summary>
        private static void DoWork()
        {
            try
            {
                RegistSyncEvent("正在获取服务器时间......");
                DateTime dtStart = DateTime.Now;
                ResultModelOfdateTime timeResult = Common._PdaServer.GetServerTime();
                DateTime dtEnd = DateTime.Now;
                UpdateSystemTime(timeResult, (dtEnd - dtStart).TotalSeconds);
                if (_SyncType == 1)
                {
                    if (SyncFinish != null)
                        SyncFinish("同步已完成!");
                    return;
                }
                RegistSyncEvent("正在获取PDA信息......");
                ResultModelOfModel_PDAInfod4FqxSXX pdaInfoResult = Common._PdaServer.GetPDAInfo(Common._PDANumber);
                UpdatePDAInfo(pdaInfoResult);
                RegistSyncEvent("正在获取目的地信息......");
                ResultModelOfArrayOfstringuHEDJ7Dj destinsResult = Common._PdaServer.GetDestins(pdaInfoResult.Data.Idk__BackingField, true);
                UpdateDestins(destinsResult);
                RegistSyncEvent("正在获取冷藏载体信息......");
                ResultModelOfArrayOfModel_ColdStoraged4FqxSXX storagesResult = Common._PdaServer.GetStorages(pdaInfoResult.Data.Idk__BackingField, true);
                UpdateStorages(storagesResult);
                RegistSyncEvent("正在获取客户信息......");
                UpdateCustomer(pdaInfoResult.Data.Idk__BackingField, 0, 0);
                RegistSyncEvent("正在获取区域信息......");
                //ResultModelOfArrayOfPDA_Model_Regiond4FqxSXX regionResult = Common._PdaServer.GetRegions(pdaInfoResult.Data.Idk__BackingField, true);
                UpdateRegion(pdaInfoResult.Data.Idk__BackingField, 0, 0);
                string lastTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //DictionaryServer.SetPDAInfo(Enum_DicKey.lastSyncTime, lastTime);
                Common._LastSyncTime = lastTime;
                if (SyncFinish != null)
                    SyncFinish("同步已完成!");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("ThreadAbortException"))
                    RegistSyncEvent("您已终止同步!");
                else
                    RegistSyncEvent("同步终止[" + ex.Message + "]");
                //if (ex.Message.Contains("无法连接到远程服务器") || ex.Message.Contains("未能建立与网络的连接"))
                //{
                //    FrmParent.ParentForm.ReConnect();
                //    RegistSyncEvent("网络异常,将自动重连,请稍后重试!");
                //}
                if (SyncFinish != null)
                    SyncFinish("同步未完成!");
            }
            finally
            {
                _isRuning = false;
                _runLog = new StringBuilder();
            }
        }

        #region 设置系统时钟
        /// <summary>
        /// 设置系统时间
        /// </summary>
        /// <param name="result"></param>
        private static void UpdateSystemTime(ResultModelOfdateTime result, double s)
        {
            if (result.Code != 0)
                throw new Exception(result.Message);
            try
            {
                SetSysTime(result.Data.AddSeconds(s));
                RegistSyncEvent("时间同步成功:" + result.Data.ToString("M/d HH:mm.ss"));
            }
            catch (Exception ex)
            {
                throw new Exception("设置系统时钟失败:" + ex.Message);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct SYSTEMTIME
        {
            public ushort wYear;
            public ushort wMonth;
            public ushort wDayOfWeek;
            public ushort wDay;
            public ushort wHour;
            public ushort wMinute;
            public ushort wSecond;
            public ushort wMilliseconds;
        }
        [DllImport("coredll.dll")]
        private static extern bool SetLocalTime(ref SYSTEMTIME lpSystemTime);
        [DllImport("coredll.dll")]
        private static extern bool GetLocalTime(ref SYSTEMTIME lpSystemTime);
        private static void SetSysTime(DateTime date)
        {
            SYSTEMTIME lpTime = new SYSTEMTIME();
            lpTime.wYear = Convert.ToUInt16(date.Year);
            lpTime.wMonth = Convert.ToUInt16(date.Month);
            lpTime.wDay = Convert.ToUInt16(date.Day);
            lpTime.wHour = Convert.ToUInt16(date.Hour);
            lpTime.wMinute = Convert.ToUInt16(date.Minute);
            lpTime.wSecond = Convert.ToUInt16(date.Second);
            SetLocalTime(ref lpTime);
        }
        #endregion

        #region 更新PDA信息
        /// <summary>
        /// 更新PDA信息
        /// </summary>
        /// <param name="result">pda信息</param>
        private static void UpdatePDAInfo(ResultModelOfModel_PDAInfod4FqxSXX result)
        {
            if (result.Code != 0)
            {
                if (result.Message.Contains("该设备信息未激活或已被删除"))
                {
                    RegistSyncEvent("已自动解除当前编号" + Common._PDANumber + "与设备的绑定关系!");
                    Common._PDANumber = string.Empty;
                }
                throw new Exception(result.Message);
            }
            try
            {
                Common._PId = result.Data.Idk__BackingField;
                //DictionaryServer.SetPDAInfo(Enum_DicKey.pdaId, result.Data.Idk__BackingField.ToString());
                RegistSyncEvent("更新成功:设备ID=" + result.Data.Idk__BackingField.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("更新PDA信息失败:" + ex.Message);
            }
        }
        #endregion

        #region 更新目的地信息
        /// <summary>
        /// 更新目的地信息
        /// </summary>
        /// <param name="result">目的地信息</param>
        private static void UpdateDestins(ResultModelOfArrayOfstringuHEDJ7Dj result)
        {
            if (result.Code != 0)
                throw new Exception(result.Message);
            try
            {
                int count = DestinServer.UpdateDestins(result.Data);
                string info = "更新成功:{0}新增{1}条";
                string addInfo = string.Empty;
                if (count != result.Data.Count())
                    addInfo = "删除" + (count - result.Data.Count()).ToString() + "条后";
                RegistSyncEvent(string.Format(info, addInfo, result.Data.Count().ToString()));
                if (!result.Data.Contains(Common._Destination) && !string.IsNullOrEmpty(Common._Destination))
                {
                    RegistSyncEvent("更新的目的地信息不包含当前设置的目的地[" + Common._Destination + "],同步完成后请回到主页设置目的地!");
                    Common._Destination = string.Empty;
                    //DictionaryServer.SetPDAInfo(Enum_DicKey.destination, "");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("更新目的地信息失败:" + ex.Message);
            }
        }
        #endregion

        #region 更新冷藏载体信息
        /// <summary>
        /// 更新冷藏载体信息
        /// </summary>
        /// <param name="result">冷藏载体信息</param>
        private static void UpdateStorages(ResultModelOfArrayOfModel_ColdStoraged4FqxSXX result)
        {
            if (result.Code != 0)
                throw new Exception(result.Message);
            try
            {
                string valueStr = GetStoragesStr(result.Data);
                int count = StorageServer.UpdateStorages(valueStr);
                string info = "更新成功:{0}新增{1}条";
                string addInfo = string.Empty;
                if (count != result.Data.Count())
                    addInfo = "删除" + (count - result.Data.Count()).ToString() + "条后";
                RegistSyncEvent(string.Format(info, addInfo, result.Data.Count().ToString()));
                if (result.Data.Where(l => l.StorageNamek__BackingField == Common._StorageName + "[默认]").Count() == 0 && !string.IsNullOrEmpty(Common._StorageName))
                {
                    RegistSyncEvent("更新的[冷藏载体]中不包含当前设置的冷藏载体[" + Common._StorageName + "],同步完成后请回到主页设置冷藏载体!");
                    Common._StorageName = string.Empty;
                    //DictionaryServer.SetPDAInfo(Enum_DicKey.storageName, "");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("更新冷藏载体信息失败:" + ex.Message);
            }
        }

        /// <summary>
        /// 拼接冷藏载体数据字符串
        /// </summary>
        /// <param name="storages">冷藏载体数据数组</param>
        /// <returns></returns>
        private static string GetStoragesStr(Model_ColdStorage[] storages)
        {
            StringBuilder valueSql = new StringBuilder();
            if (storages != null && storages.Length > 0)
            {
                foreach (Model_ColdStorage s in storages)
                {
                    valueSql.AppendFormat("('{0}','{1}','{2}','{3}','{4}','{5}','{6}'),", s.Idk__BackingField, s.StorageNamek__BackingField, s.StorageTypek__BackingField == Enum_StorageType.CarStorage ? 2 : 1,
                        s.Driverk__BackingField, s.DriverTelk__BackingField, s.Remarkk__BackingField, s.CreateAtk__BackingField.ToString("yyyy-MM-dd HH:mm:ss"));
                }
                valueSql.Length -= 1;
            }
            return valueSql.ToString();
        }
        #endregion

        #region 更新客户信息
        /// <summary>
        /// 更新客户信息
        /// </summary>
        /// <param name="result">客户信息数据</param>
        private static void UpdateCustomer(int pid, int maxId, int currCount)
        {
            if (maxId == 0)
            {
                CustomerServer.ClearCustomers();
                RegistSyncEvent("已清空原有客户信息...");
            }
            ResultModelOfArrayOfModel_Customerd4FqxSXX result = Common._PdaServer.GetCustomersFromMaxId(pid, true, maxId, true);
            if (result.Code != 0)
                throw new Exception(result.Message);
            try
            {
                if (result.Data.Length == 0)
                {
                    RegistSyncEvent("已经没有客户信息了.");
                    return;
                }
                string valueStr = GetCustomersStr(result.Data);
                int count = CustomerServer.UpdateCustomers(valueStr);
                string info = "新增客户{0}条,当前第{1}批";
                RegistSyncEvent(string.Format(info, result.Data.Count().ToString(), currCount + 1));

                if (result.Data.Count() != 0)
                {
                    Thread.Sleep(500);//递归分页获取
                    currCount += 1;
                    UpdateCustomer(pid, result.Data.Last().Idk__BackingField, currCount);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("更新客户信息失败:" + ex.Message);
            }
        }

        /// <summary>
        /// 拼接客户数据字符串
        /// </summary>
        /// <param name="storages">客户数据数组</param>
        /// <returns></returns>
        private static string GetCustomersStr(Model_Customer[] customers)
        {
            StringBuilder valueSql = new StringBuilder();
            if (customers != null && customers.Length > 0)
            {
                foreach (Model_Customer c in customers)
                {
                    int role = 1;
                    if (c.Rolek__BackingField == Enum_Role.Sender)
                        role = 2;
                    else if (c.Rolek__BackingField == Enum_Role.Receiver)
                        role = 3;
                    valueSql.AppendFormat("('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}'),", c.Idk__BackingField, c.FullNamek__BackingField, c.ContactPersonk__BackingField,
                        c.ContactTelk__BackingField, c.ContactAddressk__BackingField, c.ProvinceIdk__BackingField, c.CityIdk__BackingField, role, c.CountyIdk__BackingField);
                }
                valueSql.Length -= 1;
            }
            return valueSql.ToString();
        }
        #endregion

        #region 更新区域信息
        /// <summary>
        /// 更新客户信息
        /// </summary>
        /// <param name="result">客户信息数据</param>
        private static void UpdateRegion(int pid, int maxId, int currCount)
        {
            if (maxId == 0)
            {
                RegionServer.ClearRegions();
                RegistSyncEvent("已清空原有区域信息...");
            }
            ResultModelOfArrayOfPDA_Model_Regiond4FqxSXX result = Common._PdaServer.GetRegionsFromMaxId(pid, true, maxId, true);
            if (result.Code != 0)
                throw new Exception(result.Message);
            try
            {
                if (result.Data.Length == 0)
                {
                    RegistSyncEvent("已经没有区域信息了.");
                    return;
                }
                string valueStr = GetRegionsStr(result.Data);
                int count = RegionServer.UpdateRegions(valueStr);
                string info = "新增区域信息{0}条,当前第{1}批";
                RegistSyncEvent(string.Format(info, result.Data.Count().ToString(), currCount + 1));

                if (result.Data.Count() != 0)
                {
                    Thread.Sleep(500);//递归分页获取
                    currCount += 1;
                    UpdateRegion(pid, result.Data.Last().Idk__BackingField, currCount);
                }

            }
            catch (Exception ex)
            {
                throw new Exception("更新区域信息失败:" + ex.Message);
            }
        }

        /// <summary>
        /// 更新区域信息
        /// </summary>
        /// <param name="result">区域信息数据</param>
        //private static void UpdateRegion(ResultModelOfArrayOfPDA_Model_Regiond4FqxSXX result)
        //{
        //    if (result.Code != 0)
        //        throw new Exception(result.Message);
        //    try
        //    {
        //        string valueStr = GetRegionsStr(result.Data);
        //        int count = RegionServer.UpdateRegions(valueStr);
        //        string info = "更新成功:{0}新增{1}条";
        //        string addInfo = string.Empty;
        //        if (count != result.Data.Count())
        //            addInfo = "删除" + (count - result.Data.Count()).ToString() + "条后";
        //        RegistSyncEvent(string.Format(info, addInfo, result.Data.Count().ToString()));

        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("更新区域信息失败:" + ex.Message);
        //    }
        //}

        /// <summary>
        /// 拼接区域数据字符串
        /// </summary>
        /// <param name="storages">区域数据数组</param>
        /// <returns></returns>
        private static string GetRegionsStr(PDA_Model_Region[] region)
        {
            StringBuilder valueSql = new StringBuilder();
            if (region != null && region.Length > 0)
            {
                foreach (PDA_Model_Region r in region)
                {
                    valueSql.AppendFormat("('{0}','{1}','{2}'),", r.Idk__BackingField, r.Namek__BackingField, r.ParentIdk__BackingField);
                }
                valueSql.Length -= 1;
            }
            return valueSql.ToString();
        }
        #endregion
    }
}
