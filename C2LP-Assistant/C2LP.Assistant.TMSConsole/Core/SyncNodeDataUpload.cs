using C2LP.Assistant.TMSConsole.BLL;
using C2LP.Assistant.TMSConsole.Logink;
using C2LP.Assistant.TMSConsole.Model;
using C2LP.Assistant.TMSConsole.Model.NodeDataUpload;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace C2LP.Assistant.TMSConsole.Core
{
    /// <summary>
    /// 上报节点温湿度数据
    /// </summary>
    class SyncNodeDataUpload
    {
        SyncNodeDataUpload()
        {
            _bw = new BackgroundWorker();
            _bw.DoWork += _bw_DoWork;
            //_bw.DoWork += _bw_DoWork_Test;
            _bw.RunWorkerCompleted += _bw_RunWorkerCompleted;
        }

        static SyncNodeDataUpload _Instance;
        /// <summary>
        /// 单例实例
        /// </summary>
        public static SyncNodeDataUpload Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new SyncNodeDataUpload();
                return _Instance;
            }
        }

        /// <summary>
        /// 后台工作线程
        /// </summary>
        BackgroundWorker _bw;

        /// <summary>
        /// 开始上报
        /// </summary>
        public void Start()
        {
            if (!_bw.IsBusy)
                _bw.RunWorkerAsync();
        }

        private void _bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            int interval;
            if (!int.TryParse(Utility._SyncNodeDataUploadInterval, out interval) || interval == 0)
                interval = 3;
            Thread.Sleep(interval * 1000);

            _bw.RunWorkerAsync();
        }

        private static Model.NodeUpload.XML _UploadXML = new Model.NodeUpload.XML()
        {
            MESSAGEHEAD = new Model.MESSAGEHEAD()
            {
                SENDER = Utility._SecretKey,
                FILETYPE = "XML",
                CONTENTTYPE = "CarrierTempRecord",
                FILENAME = string.Empty,
                FILEFUNCTION = "ADD"
            }
        };
        /// <summary>
        /// 暂时忽略掉的关系
        /// </summary>
        //private List<long> _ignoreTempRelationList = new List<long>();


        #region Logink正式环境
        private void _bw_DoWork(object sender, DoWorkEventArgs e)
        {
            DateTime dtNow = DateTime.Now;
            string msg = dtNow.ToString() + ":【订单温湿度上报】 ";
            string xmlResult = string.Empty;
            RelationModel relation = new RelationModel();
            try
            {
                List<string> senderCodeList = null;
                List<M_TMSData> loginkData = null;
                if (Utility._LinkType == "2")
                {
                    senderCodeList = TMSOrderServer.GetAllLoginkSenderCode();
                    if (senderCodeList.Count == 0)
                    {
                        msg += "没有配置通过运管平台交换数据的上游发货单位";
                        return;
                    }
                }
                XML details = DataUploadServer.GetNextWaitUploadDatas(senderCodeList, ref loginkData, out relation);
                if (relation != null && relation.Id != 0)
                    msg += string.Format("R【{0}】 O【{1}】 N【{2}】 T【{3}】", relation.RelationId, relation.Number, relation.CurrentUploadDataNodeId, relation.CurrentUploadDataTime);
                if (details == null)
                {
                    if (relation != null && relation.Id != 0) // && !_ignoreTempRelationList.Contains(relation.Id))
                        DataUploadServer.UpdateHandleTHTime(relation.Id, false); //_ignoreTempRelationList.Add(relation.Id);
                    msg += "没有新的节点数据[relationId:" + relation.Id + "]";
                    return;
                }
                //运管平台上报
                if (senderCodeList != null)
                {
                    string dataXml = Utility.ParseXMLToString(loginkData);
                    string receiverCode = TMSOrderServer.GetAllLoginkSenderCode()[0];
                    LoginkHelp.Send(Utility._SecurityURL, Utility._TransportURL, Utility._MyCode, Utility._MyPwd, receiverCode, dataXml, ActionType.LOGINK_CN_TRANSPORT_PREBOOKING);
                    msg += "上报成功 Receiver[" + receiverCode + "]";
                    bool isUpdate = DataUploadServer.UpdataUploadDataNode(relation);//, ref _ignoreTempRelationList);
                    Utility.SaveXMLRequestAndRespond(dataXml, string.Empty, "Temperature", relation.RelationId);
                    msg += isUpdate ? "更新进度成功" : "更新进度失败";
                    return;
                }
                _UploadXML.MESSAGEHEAD.SENDTIME = dtNow.ToString("yyyy-MM-dd HH:mm:ss");
                _UploadXML.MESSAGEHEAD.FILENAME = dtNow.ToString("yyyyMMddHHmmss");
                _UploadXML.MESSAGEDETAIL = "<![CDATA[" + Utility.ParseXMLToString<Model.NodeDataUpload.XML>(details) + "]]>";
                string xmlRequest = Utility.ParseXMLToString<Model.NodeUpload.XML>(_UploadXML).Replace("<", "&lt;").Replace(">", "&gt;");
                StringBuilder param = new StringBuilder();
                param.Append("<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:ser=\"http://service.webservice.vtradex.com\" > ");
                param.Append("<soapenv:Header/>");
                param.Append("<soapenv:Body>");
                param.Append("<ser:temperatureXml1>" + xmlRequest + "</ser:temperatureXml1>");
                param.Append("</soapenv:Body>");
                param.Append("</soapenv:Envelope>");
                xmlResult = HTTPHelper.SendHTTPRequest(param.ToString(), "getTmsShipmentTemperature", "temperatureXml1");
                Model.NodeUpload.XML uploadResult = Utility.ParseXMLToObjec<Model.NodeUpload.XML>(xmlResult,param.ToString());
                if (uploadResult.MSGCODE == "0" || string.IsNullOrEmpty(uploadResult.MSGCODE))
                    msg += "上报失败 " + uploadResult.MSGCONTENT;
                else if (uploadResult.MSGCODE == "1")
                {
                    msg += "上报成功 ";
                    try
                    {
                        msg += DataUploadServer.UpdataUploadDataNode(relation) ? "更新进度成功." : "更新进度失败.";
                    }
                    catch (Exception ex1)
                    {
                        msg += ex1.Message;
                    }
                }
                Utility.SaveXMLRequestAndRespond(param.ToString(), xmlResult, "Temperature", relation.RelationId);
            }
            catch (Exception ex)
            {
                msg += ex.Message;
                Utility.SaveErrLog(ex.Message, "Temperature");
                //if (relation.Id != 0 )//&& _ignoreTempRelationList.Contains(relation.Id) == false)
                //    DataUploadServer.UpdateHandleTHTime(relation.Id, false); //_ignoreTempRelationList.Add(relation.Id);
                if (relation != null && relation.CurrentUploadDataNodeId != -1)
                    if (!ex.Message.Contains("重新开始") && !ex.Message.Contains("成功") && !ex.Message.Contains("没有新的载体温湿度"))
                    {
                        DataUploadServer.UpdateHandleTHTime(relation.Id, false);
                        SendEmail(new Exception(msg));
                    }
            }
            finally
            {
                Console.WriteLine(msg.Replace("\n", ""));
            }
        }
        #endregion

        #region Logink测试环境
        //private void _bw_DoWork_Test(object sender, DoWorkEventArgs e)
        //{
        //    DateTime dtNow = DateTime.Now;
        //    string msg = dtNow.ToString() + ":【订单温湿度上报】 ";
        //    string xmlResult = string.Empty;
        //    RelationModel relation = new RelationModel();
        //    try
        //    {
        //        List<string> senderCodeList = null;
        //        List<M_TMSData> loginkData = null;
        //        if (Utility._LinkType == "2")
        //        {
        //            senderCodeList = TMSOrderServer.GetAllLoginkSenderCode();
        //            if (senderCodeList.Count == 0)
        //            {
        //                msg += "没有配置通过运管平台交换数据的上游发货单位";
        //                return;
        //            }
        //        }
        //        XML details = DataUploadServer.GetNextWaitUploadDatas(senderCodeList, ref loginkData, ref _ignoreTempRelationList, out relation);
        //        if (relation != null && relation.Id != 0)
        //            msg += string.Format("R【{0}】 O【{1}】 N【{2}】 T【{3}】", relation.RelationId, relation.Number, relation.CurrentUploadDataNodeId, relation.CurrentUploadDataTime);
        //        if (details == null)
        //        {
        //            if (relation != null && relation.Id != 0 && !_ignoreTempRelationList.Contains(relation.Id))
        //                _ignoreTempRelationList.Add(relation.Id);
        //            msg += "没有新的节点数据[relationId:" + relation.Id + "]";
        //            return;
        //        }
        //        //运管平台上报
        //        if (senderCodeList != null)
        //        {
        //            string dataXml = Utility.ParseXMLToString(loginkData);
        //            string receiverCode = TMSOrderServer.GetAllLoginkSenderCode()[0];
        //            LoginkHelp.Send_Test(Utility._SecurityURL, Utility._TransportURL, Utility._MyCode, Utility._MyPwd, receiverCode, dataXml, ActionType.LOGINK_CN_TRANSPORT_PREBOOKING);
        //            msg += "上报成功 Receiver[" + receiverCode + "]";
        //            bool isUpdate = DataUploadServer.UpdataUploadDataNode(relation, ref _ignoreTempRelationList);
        //            Utility.SaveXMLRequestAndRespond(dataXml, string.Empty, "Temperature", relation.RelationId);
        //            msg += isUpdate ? "更新进度成功" : "更新进度失败";
        //            return;
        //        }
        //        _UploadXML.MESSAGEHEAD.SENDTIME = dtNow.ToString("yyyy-MM-dd HH:mm:ss");
        //        _UploadXML.MESSAGEHEAD.FILENAME = dtNow.ToString("yyyyMMddHHmmss");
        //        _UploadXML.MESSAGEDETAIL = "<![CDATA[" + Utility.ParseXMLToString<Model.NodeDataUpload.XML>(details) + "]]>";
        //        string xmlRequest = Utility.ParseXMLToString<Model.NodeUpload.XML>(_UploadXML).Replace("<", "&lt;").Replace(">", "&gt;");
        //        StringBuilder param = new StringBuilder();
        //        param.Append("<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:ser=\"http://service.webservice.vtradex.com\" > ");
        //        param.Append("<soapenv:Header/>");
        //        param.Append("<soapenv:Body>");
        //        param.Append("<ser:temperatureXml1>" + xmlRequest + "</ser:temperatureXml1>");
        //        param.Append("</soapenv:Body>");
        //        param.Append("</soapenv:Envelope>");
        //        xmlResult = HTTPHelper.SendHTTPRequest(param.ToString(), "getTmsShipmentTemperature", "temperatureXml1");
        //        Model.NodeUpload.XML uploadResult = Utility.ParseXMLToObjec<Model.NodeUpload.XML>(xmlResult);
        //        if (uploadResult.MSGCODE == "0" || string.IsNullOrEmpty(uploadResult.MSGCODE))
        //            msg += "上报失败 " + uploadResult.MSGCONTENT;
        //        else if (uploadResult.MSGCODE == "1")
        //        {
        //            msg += "上报成功 ";
        //            try
        //            {
        //                msg += DataUploadServer.UpdataUploadDataNode(relation, ref _ignoreTempRelationList) ? "更新进度成功." : "更新进度失败.";
        //            }
        //            catch (Exception ex1)
        //            {
        //                msg += ex1.Message;
        //            }
        //        }
        //        Utility.SaveXMLRequestAndRespond(param.ToString(), xmlResult, "Temperature", relation.RelationId);
        //    }
        //    catch (Exception ex)
        //    {
        //        msg += ex.Message;
        //        Utility.SaveErrLog(ex.Message, "Temperature");
        //        if (relation.Id != 0 && _ignoreTempRelationList.Contains(relation.Id) == false)
        //            _ignoreTempRelationList.Add(relation.Id);
        //        if (relation != null && relation.CurrentUploadDataNodeId != -1)
        //            SendEmail(new Exception(msg + "[" + ex.Message + "]"));
        //    }
        //    finally
        //    {
        //        Console.WriteLine(msg.Replace("\n", ""));
        //    }
        //}
        #endregion


        public void SendEmail(Exception ex, string filePath = null)
        {
            //string senderServerIp = "smtp.ym.163.com";
            //string toMailAddress = "zhaoyou@thermoberg.com";
            //string fromMailAddress = "staff@thermoberg.com";
            //string subjectInfo = "C2LP.Assistant.TMSConsole.SyncNodeDataUpload 节点冷链数据上报";
            //string bodyInfo = "失败信息：" + ex.Message;
            //string mailUsername = "staff@thermoberg.com";
            //string mailPassword = "1q2w3e4r";
            //string mailPort = "25";
            //MyEmail email = new MyEmail(senderServerIp, toMailAddress, fromMailAddress, subjectInfo, bodyInfo, mailUsername, mailPassword, mailPort, false, true);
            //email.AddCC("512145547@qq.com");
            //if (filePath != null)
            //    email.AddAttachments(filePath);
            //email.Send();
        }
    }

}

