using C2LP.Assistant.TMSConsole.BLL;
using C2LP.Assistant.TMSConsole.Core;
using System;
using System.Text;
using System.Threading;

namespace C2LP.Assistant.TMSConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(string.Format("-------------------------------欢迎使用上海思博源 TMS运单信息同步系统V{0}-------------------------------\n", Utility._ThisVersion));
            int isEnable = 0;
            //运单同步
            if (int.TryParse(Utility._SyncTMSOrderInterval, out isEnable) && isEnable > 0)
            {
                SyncTMSOrder.Instance.Start();
                Console.WriteLine(string.Format("[运单同步]已开启,同步频率[{0}]秒", Utility._SyncTMSOrderInterval));
                Thread.Sleep(1000);
            }
            else
                Console.WriteLine("[运单同步]未开启,配置文件中[SyncTMSOrderInterval=" + Utility._SyncTMSOrderInterval + "]");
            //新节点上报机制
            if (int.TryParse(Utility._SyncNodeUploadInterval_New, out isEnable) && isEnable > 0)
            {
                SyncNodeUpload_New.Instance.Start();
                Console.WriteLine(string.Format("[节点上报(新)]已开启,上报频率[{0}]秒", Utility._SyncNodeUploadInterval_New));
                Thread.Sleep(1000);
            }
            else
                Console.WriteLine("[节点上报(新)]未开启,配置文件中[SyncNodeUploadInterval_New=" + Utility._SyncNodeUploadInterval_New + "]");
            //节点上报
            if (int.TryParse(Utility._SyncNodeUploadInterval, out isEnable) && isEnable > 0)
            {
                SyncNodeUpload.Instance.Start();
                Console.WriteLine(string.Format("[节点上报]已开启,上报频率[{0}]秒", Utility._SyncNodeUploadInterval));
                Thread.Sleep(1000);
            }
            else
                Console.WriteLine("[节点上报]未开启,配置文件中[SyncNodeUploadInterval=" + Utility._SyncNodeUploadInterval + "]");
            //新机制 节点冷链数据上报
            if (int.TryParse(Utility._SyncNodeDataUploadInterval_New, out isEnable) && isEnable > 0)
            {
                if (int.TryParse(Utility._NodeDataUploadCount, out isEnable) == false)
                    Utility._NodeDataUploadCount = "200";//默认每次上报200条
                if (int.TryParse(Utility._StorageDataTimeOut, out isEnable) == false)
                    Utility._StorageDataTimeOut = "2";
                SyncNodeDataUpload_New.Instance.Start();
                Console.WriteLine(string.Format("[冷链数据上报(新)]已开启,上报频率[{0}]秒 每次上报{1}条", Utility._SyncNodeDataUploadInterval_New, Utility._NodeDataUploadCount));
                Thread.Sleep(1000);
            }
            else
                Console.WriteLine("[冷链数据上报(新)]未开启,配置文件中[SyncNodeDataUploadInterval=" + Utility._SyncNodeDataUploadInterval + "]");
            //节点冷链数据上报
            if (int.TryParse(Utility._SyncNodeDataUploadInterval, out isEnable) && isEnable > 0)
            {
                if (int.TryParse(Utility._NodeDataUploadCount, out isEnable) == false)
                    Utility._NodeDataUploadCount = "200";//默认每次上报200条
                if (int.TryParse(Utility._StorageDataTimeOut, out isEnable) == false)
                    Utility._StorageDataTimeOut = "30";//默认每次上报200条
                SyncNodeDataUpload.Instance.Start();
                Console.WriteLine(string.Format("[冷链数据上报]已开启,上报频率[{0}]秒 每次上报{1}条", Utility._SyncNodeDataUploadInterval, Utility._NodeDataUploadCount));
                Thread.Sleep(1000);
            }
            else
                Console.WriteLine("[冷链数据上报]未开启,配置文件中[SyncNodeDataUploadInterval=" + Utility._SyncNodeDataUploadInterval + "]");
            //重试运单上报
            if (int.TryParse(Utility._RetryFaildTMSOrderUploadInterval, out isEnable) && isEnable > 0)
            {
                SyncRetryFaildTMSOrder.Instance.Start();
                Console.WriteLine(string.Format("[重试运单同步]已开启,重试频率[{0}]秒", Utility._RetryFaildTMSOrderUploadInterval));
                Thread.Sleep(1000);
            }
            else
                Console.WriteLine("[重试运单同步]未开启,配置文件[RetryFaildTMSOrderUploadInterval=" + Utility._RetryFaildTMSOrderUploadInterval + "]");

            if (Utility._HandleScanLaterOrderTime < 0 || Utility._HandleScanLaterOrderTime > 23)
            {
                Console.WriteLine(string.Format("[自动检查先扫描后派单的运单并进行处理]配置为[{0}]无效参数,有效参数为[0-23] 仅启动程序时执行一次", Utility._HandleScanLaterOrderTime));
                TMSOrderServer.HandleScanLaterOrder(true);
            }
            else
            {
                Console.WriteLine(string.Format("[自动检查先扫描后派单的运单并进行处理]配置为[{0}] 每天[{0}]点钟自动执行一次", Utility._HandleScanLaterOrderTime));
                Timer _t = new Timer(DoHandleScanLaterOrder, 600000, 0, 600000);//每过10分钟检查一次今天是否运行过自动检查
            }
            Console.WriteLine();
            ReadCommand();
        }

        static DateTime? _checkTime = null;
        static void DoHandleScanLaterOrder(object state)
        {
            if (DateTime.Now.Hour == Utility._HandleScanLaterOrderTime)
            {
                if (_checkTime == null)
                {
                    _checkTime = DateTime.Now;
                    TMSOrderServer.HandleScanLaterOrder(true);
                }
                else
                    Console.WriteLine("[派单修复]今天已执行过自动修复:" + ((DateTime)_checkTime).ToString("yyyy-MM-dd HH:mm:ss"));
            }
            if (_checkTime != null)
            {
                if (((TimeSpan)(DateTime.Now - _checkTime)).TotalDays >= 1)
                    _checkTime = null;
            }
        }

        /// <summary>
        /// 接收控制台指令
        /// </summary>
        static void ReadCommand()
        {
            while (true)
            {
                Console.ReadLine();

            }
        }
    }
}
