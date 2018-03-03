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
        public static void StartSync()
        {
            if (!_isRuning)
            {
                _isRuning = true;
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

        private static int _isReLoadLocalInfo = 0;
        /// <summary>
        /// 线程执行
        /// </summary>
        private static void DoWork()
        {
            try
            {
                //string lastTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                RegistSyncEvent("正在获取服务器时间......");
                DateTime dtStart = DateTime.Now;
                ResultModelOfstring timeResult = Common._PdaServer.GetServerTimeStr();
                DateTime dtEnd = DateTime.Now;
                UpdateSystemTime(timeResult, (dtEnd - dtStart).TotalSeconds);
                RegistSyncEvent("正在获取PDA信息......");
                ResultModelOfModel_PDAInfod4FqxSXX pdaInfoResult = Common._PdaServer.GetPDAInfo(Common._PDANumber);
                UpdatePDAInfo(pdaInfoResult);
                RegistSyncEvent("正在获取目的地信息......");
                ResultModelOfArrayOfstringuHEDJ7Dj destinsResult = Common._PdaServer.GetDestins(pdaInfoResult.Data.Idk__BackingField, true);
                UpdateDestins(destinsResult);
                RegistSyncEvent("正在获取冷藏载体信息......");
                ResultModelOfArrayOfModel_ColdStoraged4FqxSXX storagesResult = Common._PdaServer.GetStorages(pdaInfoResult.Data.Idk__BackingField, true);
                UpdateStorages(storagesResult);
                ResultModelOfArrayOfModel_ColdStoraged4FqxSXX storageScanResult = Common._PdaServer.GetStorageScan();
                UpdateStorageScan(storageScanResult);
                RegistSyncEvent("正在获取对接供应商......");
                ResultModelOfArrayOfModel_ThirdCustomerd4FqxSXX consignorResult = Common._PdaServer.GetThirdCustomers();
                UpdateConsignor(consignorResult);
                RegistSyncEvent("正在获取客户信息......");
                UpdateCustomer(pdaInfoResult.Data.Idk__BackingField, 0, 0);
                RegistSyncEvent("正在获取区域信息......");
                //ResultModelOfArrayOfPDA_Model_Regiond4FqxSXX regionResult = Common._PdaServer.GetRegions(pdaInfoResult.Data.Idk__BackingField, true);
                //UpdateRegion(regionResult);
                UpdateRegion(pdaInfoResult.Data.Idk__BackingField, 0, 0);
                //DictionaryServer.SetPDAInfo(Enum_DicKey.lastSyncTime, lastTime);

                Common._LastSyncTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                if (_isReLoadLocalInfo > 0)
                {
                    RegistSyncEvent("客户与区域信息变更,正在重新构造缓存......");
                    FrmParent.ParentForm.LoadLocalData();
                    RegistSyncEvent("重新缓存完毕");
                }
                if (SyncFinish != null)
                    SyncFinish("同步已完成!");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("ThreadAbortException"))
                    RegistSyncEvent("您已终止同步!");
                else
                    RegistSyncEvent("同步终止[" + ex.Message + "]");
                if (SyncFinish != null)
                    SyncFinish("同步未完成!");
            }
            finally
            {
                _isReLoadLocalInfo = 0;
                _isRuning = false;
                //_runLog = new StringBuilder();
            }
        }

        #region 设置系统时钟
        /// <summary>
        /// 设置系统时间
        /// </summary>
        /// <param name="result"></param>
        private static void UpdateSystemTime(ResultModelOfstring result, double s)
        {
            if (result.Code != 0)
                throw new Exception(result.Message);
            try
            {
                DateTime now = DateTime.Parse(result.Data).AddSeconds(s);
                SetSysTime(now);
                RegistSyncEvent("时间同步成功:" + now.ToString("M/d HH:mm.ss"));
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
                if (result.Data.Where(l => l.StorageNamek__BackingField .Contains( Common._StorageName)).Count() == 0 && !string.IsNullOrEmpty(Common._StorageName))
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



        private static void UpdateStorageScan(ResultModelOfArrayOfModel_ColdStoraged4FqxSXX result)
        {
            if (result.Code != 0)
                throw new Exception(result.Message);
            try
            {
                string valueStr = GetStorageScanStr(result.Data);
                int count = StorageServer.UpdateStorageScan(valueStr);
                string info = "更新成功:{0}新增{1}条";
                string addInfo = string.Empty;
                if (count != result.Data.Count())
                    addInfo = "删除" + (count - result.Data.Count()).ToString() + "条后";
                RegistSyncEvent(string.Format(info, addInfo, result.Data.Count().ToString()));
            }
            catch (Exception ex) 
            {
                throw new Exception("更新冷藏载体[扫描]信息失败:" + ex.Message);
            }
        }

        private static string GetStorageScanStr(Model_ColdStorage[] storages) {
            StringBuilder valueSql = new StringBuilder();
            if (storages != null && storages.Length > 0)
            {
                foreach (Model_ColdStorage s in storages)
                {
                    valueSql.AppendFormat("('{0}','{1}','{2}'),", s.Idk__BackingField, s.StorageNamek__BackingField, s.StorageTypek__BackingField == Enum_StorageType.CarStorage ? 2 : 1);
                }
                valueSql.Length -= 1;
            }
            return valueSql.ToString();
        }
        #endregion

        #region 更新对接客户
        private static void UpdateConsignor(ResultModelOfArrayOfModel_ThirdCustomerd4FqxSXX result)
        {
            if (result.Code != 0)
                throw new Exception(result.Message);
            try
            {
                string valueStr = GetConsignorStr(result.Data);
                int count = ConsignorServer.UpdateConsignor(valueStr);
                string info = "更新成功:{0}新增{1}条";
                string addInfo = string.Empty;
                if (count != result.Data.Count())
                    addInfo = "删除" + (count - result.Data.Count()).ToString() + "条后";
                RegistSyncEvent(string.Format(info, addInfo, result.Data.Count().ToString()));
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private static string GetConsignorStr(Model_ThirdCustomer[] consignors) {
            StringBuilder valueSql = new StringBuilder();
            if (consignors != null && consignors.Length > 0)
            {
                foreach (Model_ThirdCustomer item in consignors)
                {
                    valueSql.AppendFormat("('{0}','{1}','{2}','{3}'),",item.CustomerIdk__BackingField,item.CustomerNamek__BackingField,item.LinkTypek__BackingField,item.LinkRegexk__BackingField);
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
            //if (maxId == 0)
            //{
            //    CustomerServer.ClearCustomers();
            //    RegistSyncEvent("已清空原有客户信息...");
            //}
            //ResultModelOfArrayOfModel_Customerd4FqxSXX result = Common._PdaServer.GetCustomersFromMaxId(pid, true, maxId, true);
            ResultModelOfArrayOfPDA_Model_Customerd4FqxSXX result = Common._PdaServer.GetNewCustomers(pid, true, DateTime.Parse(Common._LastSyncTime), true, maxId, true);
            if (result.Code != 0)
                throw new Exception(result.Message);
            try
            {
                if (result.Data.Length == 0)
                {
                    RegistSyncEvent("已经没有需要更新的客户信息了.");
                    return;
                }
                string insertSql = string.Empty;
                string updateSql = string.Empty;
                GetCustomersStr(result.Data, out insertSql, out updateSql);
                CustomerServer.UpdateCustomers(ref insertSql, ref updateSql);
                string info = "新增客户{0}条,更新客户{1}条,当前第{2}批";
                RegistSyncEvent(string.Format(info, insertSql, updateSql, currCount + 1));
                _isReLoadLocalInfo += int.Parse(insertSql) + int.Parse(updateSql);
                if (result.Data.Count() != 0)
                {
                    Thread.Sleep(500);//递归分页获取
                    currCount += 1;
                    UpdateCustomer(pid, result.Data.Last().idk__BackingField, currCount);
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
        private static void GetCustomersStr(PDA_Model_Customer[] customers, out string insertSql, out string updateSql)
        {
            insertSql = string.Empty;
            StringBuilder valueSql = new StringBuilder("insert into c2lp_customer (Id,fullName,contactPerson,contactTel,contactAddress,provinceId,cityId,role,countyId) values ");
            StringBuilder updateValueSql = new StringBuilder();
            string updateModelSql = "update c2lp_customer set fullName = '{0}',contactPerson='{1}',contactTel='{2}',contactAddress='{3}',provinceId='{4}',cityId='{5}',role='{6}',countyId='{7}' where id = '{8}';";
            bool hasInsert = false;
            if (customers != null && customers.Length > 0)
            {
                //所有客户的ID
                List<string> cIdList = customers.ToList().Select(l => l.idk__BackingField.ToString()).ToList();
                List<int> needUpdateIdList = CustomerServer.GetUpdateCustomerIdList(cIdList);
                foreach (PDA_Model_Customer c in customers)
                {
                    int role = 1;
                    if (c.rolek__BackingField == 2)
                        role = 2;
                    else if (c.rolek__BackingField == 3)
                        role = 3;
                    if (needUpdateIdList.Contains(c.idk__BackingField))
                        updateValueSql.AppendLine(string.Format(updateModelSql, c.fullNamek__BackingField, c.contactPersonk__BackingField,
                        c.contactTelk__BackingField, c.contactAddressk__BackingField, c.provinceIdk__BackingField, c.cityIdk__BackingField, role, c.countyIdk__BackingField, c.idk__BackingField));
                    else
                    {
                        valueSql.AppendFormat("('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}'),", c.idk__BackingField, c.fullNamek__BackingField, c.contactPersonk__BackingField,
                            c.contactTelk__BackingField, c.contactAddressk__BackingField, c.provinceIdk__BackingField, c.cityIdk__BackingField, role, c.countyIdk__BackingField);
                        hasInsert = true;
                    }
                }
                valueSql.Length -= 1;
                valueSql.Append(";");
            }
            if (hasInsert)
                insertSql = valueSql.ToString();
            updateSql = updateValueSql.ToString();
        }
        #endregion

        #region 更新区域信息
        /// <summary>
        /// 更新客户信息
        /// </summary>
        /// <param name="result">客户信息数据</param>
        private static void UpdateRegion(int pid, int maxId, int currCount)
        {
            //if (maxId == 0)
            //{
            //    RegionServer.ClearRegions();
            //    RegistSyncEvent("已清空原有区域信息...");
            //}
            ResultModelOfArrayOfPDA_Model_Regiond4FqxSXX result = Common._PdaServer.GetNewRegions(pid, true, DateTime.Parse(Common._LastSyncTime), true, maxId, true);
            if (result.Code != 0)
                throw new Exception(result.Message);
            try
            {
                if (result.Data.Length == 0)
                {
                    RegistSyncEvent("已经没有需要更新的区域信息了.");
                    return;
                }
                string insertSql = string.Empty;
                string updateSql = string.Empty;
                GetRegionsStr(result.Data, out insertSql, out updateSql);
                int count = RegionServer.UpdateRegions(ref insertSql, ref updateSql);
                string info = "新增区域{0}条,更新区域{1}条,当前第{2}批";
                RegistSyncEvent(string.Format(info, insertSql, updateSql, currCount + 1));
                _isReLoadLocalInfo += int.Parse(insertSql) + int.Parse(updateSql);

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
        private static void GetRegionsStr(PDA_Model_Region[] region, out string insertSql, out string updateSql)
        {
            insertSql = string.Empty;
            StringBuilder valueSql = new StringBuilder("insert into c2lp_Region (Id,`name`,parentId) values ");
            StringBuilder updateValueSql = new StringBuilder();
            string updateModelSql = "update c2lp_Region set `name` = '{0}',parentId='{1}' where id = '{2}';";
            bool hasInsert = false;
            if (region != null && region.Length > 0)
            {
                //所有区域的ID
                List<string> cIdList = region.ToList().Select(l => l.Idk__BackingField.ToString()).ToList();
                List<int> needUpdateIdList = RegionServer.GetUpdateRegionIdList(cIdList);
                foreach (PDA_Model_Region r in region)
                {
                    if (needUpdateIdList.Contains(r.Idk__BackingField))
                        updateValueSql.AppendLine(string.Format(updateModelSql, r.Namek__BackingField, r.ParentIdk__BackingField, r.Idk__BackingField));
                    else
                    {
                        valueSql.AppendFormat("('{0}','{1}','{2}'),", r.Idk__BackingField, r.Namek__BackingField, r.ParentIdk__BackingField);
                        hasInsert = true;
                    }
                }
                valueSql.Length -= 1;
            }
            if (hasInsert)
                insertSql = valueSql.ToString();
            updateSql = updateValueSql.ToString();
        }
        #endregion
    }
}
