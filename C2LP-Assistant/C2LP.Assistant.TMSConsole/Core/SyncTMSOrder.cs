using C2LP.Assistant.TMSConsole.BLL;
using C2LP.Assistant.TMSConsole.Logink;
using C2LP.Assistant.TMSConsole.Model.TMSOrder;
using com.wondersgroup.cuteinfo.csharp.client.common.pojo;
using com.wondersgroup.cuteinfo.csharp.client.common.service.ExchangeTransport;
using com.wondersgroup.cuteinfo.csharp.client.common.service.ExchangeTransport.pojo;
using com.wondersgroup.cuteinfo.csharp.client.utils;
using LoginkExchange;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Xml;

namespace C2LP.Assistant.TMSConsole.Core
{
    /// <summary>
    /// 同步TMS运单
    /// </summary>
    class SyncTMSOrder
    {
        SyncTMSOrder()
        {
            _bw = new BackgroundWorker();
            if (Utility._LinkType == "2")
            {
                //_bw.DoWork += _bw_DoWork_Logink_Test;//运管平台                
                _bw.DoWork += _bw_DoWork_Logink;//运管平台
            }
            else
                _bw.DoWork += _bw_DoWork;       //华东供应链

            _bw.RunWorkerCompleted += _bw_RunWorkerCompleted;
        }

        static SyncTMSOrder _Instance;
        /// <summary>
        /// 单例实例
        /// </summary>
        public static SyncTMSOrder Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new SyncTMSOrder();
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
            if (!int.TryParse(Utility._SyncTMSOrderInterval, out interval) || interval == 0)
                interval = 3;
            Thread.Sleep(interval * 1000);

            _bw.RunWorkerAsync();
        }

        #region 正式环境
        /// <summary>
        /// 运管平台订单同步 正式环境
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _bw_DoWork_Logink(object sender, DoWorkEventArgs e)
        {
            DateTime dtNow = DateTime.Now;
            string msg = dtNow.ToString() + ":【同步TMS运单】 ";
            MyEventReceiver evevnt = null;
            ExchangeTransport exchangeTransport = null;
            int handelCount = 0;
            try
            {
                List<string> senderCodeList = TMSOrderServer.GetAllLoginkSenderCode();
                if (senderCodeList.Count == 0)
                {
                    msg += "没有配置通过运管平台交换数据的上游发货单位";
                    return;
                }
                MyReceiveResponses response = LoginkHelp.Receive(Utility._SecurityURL, Utility._TransportURL, Utility._MyCode, Utility._MyPwd, ref exchangeTransport);
                if (response == null || response.getEventReceiverList() == null ||
                   (response != null && response.getEventReceiverList() != null && response.getEventReceiverList().Count == 0))
                {
                    msg += "没有数据接收";
                    return;
                }
                evevnt = response.getEventReceiverList()[0];
                string sendCode = evevnt.getEventsender();
                if (evevnt.getXmlcontent() != null)
                {
                    if (!senderCodeList.Contains(sendCode))
                    {
                        handelCount = 1;
                        msg += "[Code:" + sendCode + "]没有加入系统,不保存此订单数据";
                        string filePath = Utility.SaveXMLFile(evevnt.getXmlcontent(), dtNow, sendCode);
                        msg += " 已保存XML数据到[" + filePath + "]";
                        Utility.SaveErrLog(msg, "Order");
                        return;
                    }
                    try
                    {
                        List<M_TMSOrder> orders = Utility.ParseXMLToObjec<List<M_TMSOrder>>(evevnt.getXmlcontent());
                        TMSOrderServer.AddTMSOrders_Logink(orders, sendCode);
                        handelCount = 1;
                        Utility.SaveXMLRequestAndRespond(evevnt.getXmlcontent(), string.Empty, "Order", orders.Count.ToString());
                        msg += "成功同步" + orders.Count + "条数据 Sender[" + sendCode + "]";
                    }
                    catch (Exception ex)
                    {
                        string filePath = Utility.SaveXMLFile(evevnt.getXmlcontent(), dtNow, sendCode);
                        msg += ex.Message + " ,已保存XML数据到[" + filePath + "]";
                        SendEmail(new Exception(msg), filePath);
                    }
                }
            }
            catch (Exception ex)
            {

                msg += ex.Message + " ";
                if (ex.Message.Contains("订单解析失败"))
                    handelCount = 1;
                SendEmail(new Exception(msg ));
                Utility.SaveErrLog(ex.Message, "Order");
            }
            finally
            {
                if (evevnt != null && handelCount > 0)
                {
                    try
                    {
                        if (LoginkHelp.Confirm(exchangeTransport, evevnt.getEventid()))
                            msg += " 接收确认成功";
                        else
                            msg += " 接收确认失败";
                    }
                    catch (Exception ex)
                    {
                        msg += " " + ex.Message;
                    }
                }
                Console.WriteLine(msg.Replace("\n", ""));
            }
        }
        #endregion  

        #region Logink测试环境
        /// <summary>
        /// 运管平台订单同步 测试环境
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _bw_DoWork_Logink_Test(object sender, DoWorkEventArgs e)
        {
            DateTime dtNow = DateTime.Now;
            string msg = dtNow.ToString() + ":【同步TMS运单】 ";
            UReceiveResponse response = null;
            UUserToken _token = null;
            int handelCount = 0;
            try
            {
                List<string> senderCodeList = TMSOrderServer.GetAllLoginkSenderCode();
                if (senderCodeList.Count == 0)
                {
                    msg += "没有配置通过运管平台交换数据的上游发货单位";
                    return;
                }
                response = LoginkHelp.Receive_Test(Utility._SecurityURL, Utility._TransportURL, Utility._MyCode, Utility._MyPwd, ref _token);
                if (response == null || response.getEventReceiverList() == null ||
                   (response != null && response.getEventReceiverList() != null && response.getEventReceiverList().Count == 0))
                {
                    msg += "没有数据接收";
                    return;
                }
                UEventReceiver evevnt = response.getEventReceiverList()[0];
                string sendCode = evevnt.getEventsender();
                //如果返回内容中有键值对，则保存到错误日志中
                if (evevnt.getSimpledata() != null)
                {
                    StringBuilder dicStr = new StringBuilder();
                    foreach (KeyValuePair<string, string> item in evevnt.getSimpledata())
                    {
                        dicStr.AppendLine(string.Format("key={0},value={1}", item.Key, item.Value));
                    }
                    handelCount = 1;
                    Utility.SaveErrLog(dicStr.ToString(), "Order");
                    msg += "接收到的数据存在字典,已保存到ErrLog目录中";
                }
                //接收到的是附件
                if (evevnt.getAttachmentData() != null)
                {
                    handelCount = 1;
                    //保存附件到c盘
                    FileUtil.writeFile(evevnt.getAttachmentData(), "ErrLog/" + dtNow.ToString("yyyyMMdd_HH:mm:ss") + "_" + sendCode + evevnt.getFilename());
                    msg += "接收到的数据存在附件,已保存到ErrLog目录中";
                }
                if (evevnt.getXmlcontent() != null)
                {
                    if (!senderCodeList.Contains(sendCode))
                    {
                        handelCount = 1;
                        msg += "[Code:" + sendCode + "]没有加入系统,不保存此订单数据";
                        string filePath = Utility.SaveXMLFile(evevnt.getXmlcontent(), dtNow, sendCode);
                        msg += " 已保存XML数据到[" + filePath + "]";
                        Utility.SaveErrLog(msg, "Order");
                        return;
                    }
                    try
                    {
                        List<M_TMSOrder> orders = Utility.ParseXMLToObjec<List<M_TMSOrder>>(evevnt.getXmlcontent());
                        TMSOrderServer.AddTMSOrders_Logink(orders, sendCode);
                        handelCount = 1;
                        Utility.SaveXMLRequestAndRespond(evevnt.getXmlcontent(), string.Empty, "Order",orders.Count.ToString());
                        msg += "成功同步" + orders.Count + "条数据 Sender[" + sendCode + "]";
                    }
                    catch (Exception ex)
                    {
                        string filePath = Utility.SaveXMLFile(evevnt.getXmlcontent(), dtNow, sendCode);
                        msg += ex.Message + " ,已保存XML数据到[" + filePath + "]";
                        SendEmail(new Exception(msg), filePath);
                    }
                }
            }
            catch (Exception ex)
            {
                msg += ex.Message + " ";
                Utility.SaveErrLog(ex.Message, "Order");
            }
            finally
            {
                if (response != null && response.getEventReceiverList() != null && response.getEventReceiverList().Count > 0 && handelCount > 0)
                {
                    try
                    {
                        if (LoginkHelp.Confirm_Test(response, Utility._TransportURL, _token))
                            msg += " 接收确认成功";
                        else
                            msg += " 接收确认失败";
                    }
                    catch (Exception ex)
                    {
                        msg += " " + ex.Message;
                    }
                }
                Console.WriteLine(msg.Replace("\n", ""));
            }
        }
        #endregion

        private void _bw_DoWork(object sender, DoWorkEventArgs e)
        {
            DateTime dtNow = DateTime.Now;
            string msg = dtNow.ToString() + ":【同步TMS运单】 ";
            bool addResult = false;
            string xmlResult = string.Empty;
            try
            {
                StringBuilder param = new StringBuilder();
                param.Append("<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:ser=\"http://service.webservice.vtradex.com\" > ");
                param.Append("<soapenv:Header/>");
                param.Append("<soapenv:Body>");
                param.Append("<ser:shipmentXml>" + Utility._SecretKey + "</ser:shipmentXml>");
                param.Append("</soapenv:Body>");
                param.Append("</soapenv:Envelope>");
                xmlResult = HTTPHelper.SendHTTPRequest(param.ToString(), "shipmentXml", "shipmentXml");

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
                        Utility.AddLogText("同步TMS运单");
                        addResult = TMSOrderServer.AddTMSOrders(result._MESSAGEDETAIL);
                        msg += addResult ? "同步成功" : "同步失败";
                        Utility.AddLogText(addResult ? "同步成功" : "同步失败");
                        Utility.AddLogText("-------------------------------------------------");
                        Utility.SaveXMLRequestAndRespond(param.ToString(), xmlResult, "Order","n");
                    }
                    else
                        msg += "不处理";
                }
            }
            catch (Exception ex)
            {
                msg += ex.Message + " ";
                Utility.SaveErrLog(ex.Message, "Order");
                Utility.AddLogText(string.Format("_bw_DoWork-SyncTMSOrder:{0}",ex.Message));
            }
            finally
            {
                if (!addResult && !string.IsNullOrEmpty(xmlResult))
                {
                    string filePath = Utility.SaveXMLFile(xmlResult, dtNow);
                    if (filePath != string.Empty)
                        msg += "订单内容已保存到[" + filePath + "]";
                    SendEmail(new Exception(msg), filePath);
                }
                Console.WriteLine(msg.Replace("\n", ""));
            }
        }

        public void SendEmail(Exception ex, string filePath = null)
        {
            //string senderServerIp = "smtp.ym.163.com";
            //string toMailAddress = "zhaoyou@thermoberg.com";
            //string fromMailAddress = "staff@thermoberg.com";
            //string subjectInfo = "C2LP.Assistant.TMSConsole.SyncTMSOrder 订单同步";
            //string bodyInfo = "失败信息：" + ex.Message;
            //string mailUsername = "staff@thermoberg.com";
            //string mailPassword = "1q2w3e4r";
            //string mailPort = "25";
            //MyEmail email = new MyEmail(senderServerIp, toMailAddress, fromMailAddress, subjectInfo, bodyInfo, mailUsername, mailPassword, mailPort, false, true);
            //email.AddCC("2712250388@qq.com");
            //if (filePath != null)
            //    email.AddAttachments(filePath);
            //email.Send();
        }
    }
}
