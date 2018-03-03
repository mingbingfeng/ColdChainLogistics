using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Xml;
using C2LP.Assistant.TMSConsole.BLL;
using C2LP.Assistant.TMSConsole.Model.TMSOrder;
using System.IO;

namespace C2LP.Assistant.TMSConsole.Core
{
    public class SyncRetryFaildTMSOrder
    {
        SyncRetryFaildTMSOrder()
        {
            _bw = new BackgroundWorker();
            _bw.DoWork += _bw_DoWork;
            _bw.RunWorkerCompleted += _bw_RunWorkerCompleted;
        }

        static SyncRetryFaildTMSOrder _Instance;
        /// <summary>
        /// 单例实例
        /// </summary>
        public static SyncRetryFaildTMSOrder Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new SyncRetryFaildTMSOrder();
                return _Instance;
            }
        }

        /// <summary>
        /// 后台工作线程
        /// </summary>
        BackgroundWorker _bw;

        /// <summary>
        /// 开始TMS运单同步
        /// </summary>
        public void Start()
        {
            if (!_bw.IsBusy)
                _bw.RunWorkerAsync();
        }

        /// <summary>
        /// 结束一轮后继续开始
        /// </summary>
        private void _bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            int interval;
            if (!int.TryParse(Utility._RetryFaildTMSOrderUploadInterval, out interval) || interval == 0)
                interval = 3;
            Thread.Sleep(interval * 1000);

            _bw.RunWorkerAsync();
        }

        private void _bw_DoWork(object sender, DoWorkEventArgs e)
        {
            DateTime dtNow = DateTime.Now;
            string msg = dtNow.ToString() + ":【重试运单同步】 ";
            bool addResult = false;
            string xmlResult = string.Empty;
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "SyncTMSOrderFaild";
            string fileName = string.Empty;
            try
            {
                if (Directory.Exists(filePath))
                {
                    string[] files = Directory.GetFiles(filePath);
                    if (files.Count() > 0)
                    {
                        int rIndex = new Random().Next(files.Count());
                        fileName = files[rIndex];
                    }
                }
                if (fileName == string.Empty)
                {
                    addResult = true;
                    msg += "没有同步失败的TMS运单";
                    return;
                }
                msg += "文件名：" + Path.GetFileName(fileName) + " ";
                try
                {
                    XmlDocument xdoc = new XmlDocument();
                    xdoc.Load(fileName);
                    xmlResult = xdoc.InnerXml; 
                }
                catch (Exception ex)
                {
                    throw new Exception("加载xml文件失败：" + ex.Message);
                }
                XML result = Utility.ParseXMLToObjec<XML>(xmlResult);
                if (result.MSGCODE != null)
                {
                    msg += result.MSGCONTENT;
                    addResult = true;
                }
                else
                {
                    msg += result.MESSAGEHEAD.FILENAME + " " + result.MESSAGEHEAD.FILEFUNCTION + " ";
                    if (result.MESSAGEHEAD.FILEFUNCTION == "ADD")
                    {
                        Utility.AddLogText("-------------------------------------------------");
                        Utility.AddLogText("开始运单同步");
                        addResult = TMSOrderServer.AddTMSOrders(result._MESSAGEDETAIL);
                        msg += addResult ? "同步成功" : "同步失败";
                        Utility.AddLogText(addResult ? "同步成功" : "同步失败");
                        Utility.AddLogText("-------------------------------------------------");
                    }
                    else
                    {
                        addResult = true;
                        msg += "不处理";
                    }
                }
            }
            catch (Exception ex)
            {
                msg += ex.Message + " ";
                Utility.AddLogText(string.Format("_bw_DoWork-SyncRetryFaildTMSOrder:{0}",ex.Message));
            }
            finally
            {
                if (addResult && !string.IsNullOrEmpty(fileName))
                {
                    try
                    {
                        File.Delete(fileName);
                    }
                    catch
                    {
                    }
                    Thread.Sleep(1000);
                }
                Console.WriteLine(msg.Replace("\n", ""));
            }
        }
    }
}
