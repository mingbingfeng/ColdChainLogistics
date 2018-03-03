using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using C2LP.PDA.Datas.BLL;

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
                    LastSyncTime = DictionaryServer.GetPDAInfo(Enum_DicKey.lastSyncTime);
                if (ValueChangeEvent != null)
                    ValueChangeEvent(Enum_DicKey.lastSyncTime, LastSyncTime);

                //上报周期
                if (UploadCycle == 10)
                {
                    string tempCount = DictionaryServer.GetPDAInfo(Enum_DicKey.uploadCycle);
                    if (!string.IsNullOrEmpty(tempCount))
                        UploadCycle = int.Parse(tempCount);
                    else
                        _UploadCycle = 120;
                }

                //运单最大查询数量
                if (MaxUploadOrderCount == 1)
                {
                    string tempCount = DictionaryServer.GetPDAInfo(Enum_DicKey.maxUploadOrderCount);
                    if (!string.IsNullOrEmpty(tempCount))
                        MaxUploadOrderCount = int.Parse(tempCount);
                    else
                        _MaxUploadOrderCount = 1;
                }


                //节点最大查询数量
                if (MaxUploadNodeCount == 1)
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

        private static int UploadCycle=10;
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

        private static int MaxUploadOrderCount=1;
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

        private static int MaxUploadNodeCount=1;
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
    }

}
