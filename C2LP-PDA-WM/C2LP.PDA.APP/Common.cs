using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using C2LP.PDA.Datas.BLL;
using C2LP.PDA.Datas.Model;
using System.IO;
using System.Reflection;

namespace C2LP.PDA.APP
{
    public class Common
    {
        public delegate void ValueChangeDelegate(Enum_DicKey key, object value);
        public static event ValueChangeDelegate ValueChangeEvent;

        /// <summary>
        /// 初始化PDA基本参数
        /// </summary>
        public static void Init()
        {
            if (string.IsNullOrEmpty(PDANumber))
                PDANumber = DictionaryServer.GetPDAInfo(Enum_DicKey.pdaNumber);
            if (ValueChangeEvent != null)
                ValueChangeEvent(Enum_DicKey.pdaNumber, PDANumber);

            if (string.IsNullOrEmpty(StorageName))
                StorageName = DictionaryServer.GetPDAInfo(Enum_DicKey.storageName);
            if (ValueChangeEvent != null)
                ValueChangeEvent(Enum_DicKey.storageName, StorageName);
            if (string.IsNullOrEmpty(Destination))
                Destination = DictionaryServer.GetPDAInfo(Enum_DicKey.destination);
            if (ValueChangeEvent != null)
                ValueChangeEvent(Enum_DicKey.destination, Destination);
            if (string.IsNullOrEmpty(LastSyncTime))
            {
                LastSyncTime = DictionaryServer.GetPDAInfo(Enum_DicKey.lastSyncTime);
                if (string.IsNullOrEmpty(LastSyncTime))
                    _LastSyncTime = "2017-01-01 00:00";
            }
            if (ValueChangeEvent != null)
                ValueChangeEvent(Enum_DicKey.lastSyncTime, LastSyncTime);

            //上报周期
            if (DefaultProvince != "浙江省")
            {
                string tempStr = DictionaryServer.GetPDAInfo(Enum_DicKey.defaultProvince);
                if (!string.IsNullOrEmpty(tempStr))
                    DefaultProvince = tempStr;
                else
                    _DefaultProvince = "浙江省";
            }
            //上报周期
            if (UploadCycle != 3)
            {
                string tempCount = DictionaryServer.GetPDAInfo(Enum_DicKey.uploadCycle);
                if (!string.IsNullOrEmpty(tempCount))
                    UploadCycle = int.Parse(tempCount);
                else
                    _UploadCycle = 10;
            }

            //运单最大查询数量
            if (MaxUploadOrderCount != 1)
            {
                string tempCount = DictionaryServer.GetPDAInfo(Enum_DicKey.maxUploadOrderCount);
                if (!string.IsNullOrEmpty(tempCount))
                    MaxUploadOrderCount = int.Parse(tempCount);
                else
                    _MaxUploadOrderCount = 1;
            }


            //节点最大查询数量
            if (MaxUploadNodeCount != 1)
            {
                string tempCount = DictionaryServer.GetPDAInfo(Enum_DicKey.maxUploadNodeCount);
                if (!string.IsNullOrEmpty(tempCount))
                    MaxUploadNodeCount = int.Parse(tempCount);
                else
                    _MaxUploadNodeCount = 1;
            }

            if (string.IsNullOrEmpty(WebServiceAddress))
            {
                string tempAddress = DictionaryServer.GetPDAInfo(Enum_DicKey.webServiceAddress);
                if (!string.IsNullOrEmpty(tempAddress))
                    WebServiceAddress = tempAddress;
                else
                    _WebServiceAddress = "www1.huadongbio.com:8005";
            }

            //操作界面n秒后无操作则自动返回主界面
            if (AutoReturnDelay != 120)
            {
                string tempCount = DictionaryServer.GetPDAInfo(Enum_DicKey.autoReturnDelay);
                if (!string.IsNullOrEmpty(tempCount))
                    AutoReturnDelay = int.Parse(tempCount);
                else
                    _AutoReturnDelay = 120;
            }

        }

        /// <summary>
        /// 主页是否第一次显示
        /// </summary>
        public static bool _IsMainFormFirstShow = true;

        private static string PDANumber;
        /// <summary>
        /// pda编号
        /// </summary>
        public static string _PDANumber
        {
            get { return PDANumber; }
            set
            {
                PDANumber = value;
                DictionaryServer.SetPDAInfo(Enum_DicKey.pdaNumber, value);
                if (ValueChangeEvent != null)
                    ValueChangeEvent(Enum_DicKey.pdaNumber, value);
            }
        }

        private static int PId;
        /// <summary>
        /// pdaID
        /// </summary>
        public static int _PId
        {
            get { return PId; }
            set
            {
                PId = value;
                DictionaryServer.SetPDAInfo(Enum_DicKey.pdaId, value);
                if (ValueChangeEvent != null)
                    ValueChangeEvent(Enum_DicKey.pdaId, value);
            }
        }

        private static string Destination;
        /// <summary>
        /// 目的地
        /// </summary>
        public static string _Destination
        {
            get { return Destination; }
            set
            {
                Destination = value;
                DictionaryServer.SetPDAInfo(Enum_DicKey.destination, value);
                if (ValueChangeEvent != null)
                    ValueChangeEvent(Enum_DicKey.destination, value);
            }
        }


        private static string StorageName;
        /// <summary>
        /// 冷藏载体名称
        /// </summary>
        public static string _StorageName
        {
            get { return StorageName; }
            set
            {
                StorageName = value;
                DictionaryServer.SetPDAInfo(Enum_DicKey.storageName, value);
                if (ValueChangeEvent != null)
                    ValueChangeEvent(Enum_DicKey.storageName, value);
            }
        }


        /// <summary>
        /// 最后一次同步的时间
        /// </summary>
        private static string LastSyncTime;
        public static string _LastSyncTime
        {
            get { return LastSyncTime; }
            set
            {
                LastSyncTime = value;
                if (ValueChangeEvent != null)
                    DictionaryServer.SetPDAInfo(Enum_DicKey.lastSyncTime, value.ToString());
                ValueChangeEvent(Enum_DicKey.lastSyncTime, value);
            }
        }

        private static PDAWebReference.PDAServer PdaServer;
        public static PDAWebReference.PDAServer _PdaServer
        {
            get
            {
                if (PdaServer == null)
                    PdaServer = new C2LP.PDA.APP.PDAWebReference.PDAServer();
                PdaServer.Url = string.Format("http://{0}/PDAServer/ws", _WebServiceAddress);
                return PdaServer;
            }
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

        private static int UploadCycle = 10;
        /// <summary>
        /// 上报周期
        /// </summary>
        public static int _UploadCycle
        {
            get { return UploadCycle; }
            set
            {
                UploadCycle = value;
                DictionaryServer.SetPDAInfo(Enum_DicKey.uploadCycle, value);
            }
        }

        private static int MaxUploadOrderCount = 1;
        /// <summary>
        /// 运单最大查询数量
        /// </summary>
        public static int _MaxUploadOrderCount
        {
            get { return MaxUploadOrderCount; }
            set
            {
                MaxUploadOrderCount = value;
                DictionaryServer.SetPDAInfo(Enum_DicKey.maxUploadOrderCount, value);
            }
        }

        private static int MaxUploadNodeCount = 1;
        /// <summary>
        /// 节点最大查询数量
        /// </summary>
        public static int _MaxUploadNodeCount
        {
            get { return MaxUploadNodeCount; }
            set
            {
                MaxUploadNodeCount = value;
                DictionaryServer.SetPDAInfo(Enum_DicKey.maxUploadNodeCount, value);
            }
        }

        private static string WebServiceAddress;
        /// <summary>
        /// WebService服务器地址
        /// </summary>
        public static string _WebServiceAddress
        {
            get { return WebServiceAddress; }
            set
            {
                WebServiceAddress = value;
                DictionaryServer.SetPDAInfo(Enum_DicKey.webServiceAddress, value);
            }
        }

        private static string DefaultProvince = "浙江省";
        /// <summary>
        /// 目的地
        /// </summary>
        public static string _DefaultProvince
        {
            get { return DefaultProvince; }
            set
            {
                DefaultProvince = value;
                DictionaryServer.SetPDAInfo(Enum_DicKey.defaultProvince, value);
                if (ValueChangeEvent != null)
                    ValueChangeEvent(Enum_DicKey.defaultProvince, value);
            }
        }

        private static int AutoReturnDelay = 120;
        /// <summary>
        /// 操作界面n秒后无操作则自动返回主界面
        /// </summary>
        public static int _AutoReturnDelay
        {
            get { return Common.AutoReturnDelay; }
            set
            {
                AutoReturnDelay = value;
                DictionaryServer.SetPDAInfo(Enum_DicKey.autoReturnDelay, value);
            }
        }


        /// <summary>
        /// 保存操作记录
        /// </summary>
        /// <param name="optType">操作类型</param>
        /// <param name="content">操作内容</param>
        public static void SaveOptRecord(string optType, string content, DateTime orderTime, string number, int customerId)
        {
            if (!string.IsNullOrEmpty(optType) && (optType.Contains("重复扫描") || optType.Contains("扫描前请选择")))
                return;
            OptRecord record = new OptRecord();
            record.OptTime = orderTime.ToString("yyyy-MM-dd HH:mm:ss");
            record.OptType = optType;
            record.Content = content.Replace("'", "''");

            record.OptNumber = number;
            record.OptCustomerId = customerId;
            record.OptTypeId = GetOptTypeId(optType);
            OptRecordServer.AddOptRecord(record);
        }

        private static int GetOptTypeId(string optType)
        {
            if (optType.Contains("第三方"))
                return 1;
            if (optType.Contains("创建运单"))
                return 2;
            if (optType.Contains("中间节点"))
                return 3;
            if (optType.Contains("运抵"))
                return 4;
            if (optType.Contains("签收图片"))
                return 5;
            return 6;
        }

        /// <summary>
        /// 检测是否有重复进程
        /// </summary>
        //public static bool CheckProecess()
        //{
        //    try
        //    {
        //        string filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase.ToString());
        //        filePath += "\\isOpen.txt";
        //        using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
        //        {

        //            string txt = string.Empty;
        //            using (StreamReader sr = new StreamReader(fs, Encoding.Default))
        //            {
        //                txt = sr.ReadToEnd();
        //            }
        //            if (string.IsNullOrEmpty(txt) || txt.Trim() == "0")
        //            {
        //                Write(filePath, "1");
        //                return true;
        //            }
        //        }

        //    }
        //    catch
        //    {

        //    }

        //    return false;
        //}

        //private static void Write(string path, string txt)
        //{
        //    try
        //    {
        //        FileStream fs = new FileStream(path, FileMode.Create);
        //        StreamWriter sw = new StreamWriter(fs);
        //        //开始写入
        //        sw.Write(txt);
        //        //清空缓冲区
        //        sw.Flush();
        //        //关闭流
        //        sw.Close();
        //        fs.Close();
        //    }
        //    catch
        //    {
        //    }
        //}

        //public static void WriteIsClose()
        //{

        //    string filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase.ToString());
        //    filePath += "\\isOpen.txt";
        //    Write(filePath, "0");
        //}
    }

}
