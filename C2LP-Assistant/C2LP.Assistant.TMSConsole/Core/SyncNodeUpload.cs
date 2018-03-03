using C2LP.Assistant.TMSConsole.BLL;
using C2LP.Assistant.TMSConsole.Logink;
using C2LP.Assistant.TMSConsole.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;

namespace C2LP.Assistant.TMSConsole.Core
{
    /// <summary>
    /// 上报节点信息
    /// </summary>
    class SyncNodeUpload
    {
        SyncNodeUpload()
        {
            _bw = new BackgroundWorker();
            _bw.DoWork += _bw_DoWork;
            //_bw.DoWork += _bw_DoWork_Test;
            _bw.RunWorkerCompleted += _bw_RunWorkerCompleted;
        }

        static SyncNodeUpload _Instance;
        /// <summary>
        /// 单例实例
        /// </summary>
        public static SyncNodeUpload Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new SyncNodeUpload();
                return _Instance;
            }
        }

        /// <summary>
        /// 后台工作线程
        /// </summary>
        BackgroundWorker _bw;

        /// <summary>
        /// 开始节点上报
        /// </summary>
        public void Start()
        {
            if (!_bw.IsBusy)
                _bw.RunWorkerAsync();
        }


        private void _bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            int interval;
            if (!int.TryParse(Utility._SyncNodeUploadInterval, out interval) || interval == 0)
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
                CONTENTTYPE = "CarrierTrackRecord",
                FILENAME = string.Empty,
                FILEFUNCTION = "ADD"
            }
        };

        /// <summary>
        /// 暂时忽略掉的关系
        /// </summary>
        //private List<int> _ignoreTempRelationList = new List<int>();

        #region Logink正式环境
        private void _bw_DoWork(object sender, DoWorkEventArgs e)
        {
            DateTime dtNow = DateTime.Now;
            string msg = dtNow.ToString() + ":【节点上报】 ";
            string xmlResult = string.Empty;
            RelationModel relation = new RelationModel();
            try
            {
                List<string> senderCodeList = null;
                M_TMSNode loginkNode = null;
                if (Utility._LinkType == "2")
                {
                    senderCodeList = TMSOrderServer.GetAllLoginkSenderCode();
                    if (senderCodeList.Count == 0)
                    {
                        msg += "没有配置通过运管平台交换数据的上游发货单位";
                        return;
                    }
                }
                Model.NodeUpload.MESSAGEDETAIL.XML details = NodeUploadServer.GetFirstWaitUploadNode(senderCodeList, ref loginkNode, out relation);

                if (relation != null && relation.Id != 0)
                    msg += string.Format("Relation【{0}】 Number【{1}】 CustomerId【{3}】 当前上报节点Id【{2}】 ", relation.RelationId, relation.Number, relation.CurrentUploadNodeId, relation.CustomerId);
                if (details == null)
                {
                    if (relation != null && relation.Id != 0)// && !_ignoreTempRelationList.Contains(relation.Id))
                        NodeUploadServer.UpdateHandleTime(relation.Id, false); //_ignoreTempRelationList.Add(relation.Id);
                    msg += "暂无需要上报的节点";
                    return;
                }
                //运管平台上报
                if (senderCodeList != null)
                {
                    string nodeXml = Utility.ParseXMLToString(loginkNode);
                    string receiverCode = TMSOrderServer.GetAllLoginkSenderCode(relation.CustomerId.ToString())[0];
                    LoginkHelp.Send(Utility._SecurityURL, Utility._TransportURL, Utility._MyCode, Utility._MyPwd, receiverCode, nodeXml, ActionType.LOGINK_CN_TRANSPORT_PREBOOKING);
                    msg += "上报成功 Receiver[" + receiverCode + "]";
                    bool isArrived = false;
                    if (details.CONTENTLIST[0].DETAILLIST[0].TRACKTYPE == "18")//正常签收
                    {
                        isArrived = true;
                        msg += " 当前为签收节点 ";
                    }
                    bool isUpdate = NodeUploadServer.UpdateCurrentUploadNodeId(relation, isArrived, senderCodeList);
                    Utility.SaveXMLRequestAndRespond(nodeXml, string.Empty, "Node", relation.RelationId);
                    msg += isUpdate ? "更新当前上报节点成功" : "更新当前上报节点失败";
                    return;
                }
                #region 大华东供应链节点上报处理
                _UploadXML.MESSAGEHEAD.SENDTIME = dtNow.ToString("yyyy-MM-dd HH:mm:ss");
                _UploadXML.MESSAGEHEAD.FILENAME = dtNow.ToString("yyyyMMddHHmmss");
                _UploadXML.MESSAGEDETAIL = "<![CDATA[" + Utility.ParseXMLToString<Model.NodeUpload.MESSAGEDETAIL.XML>(details) + "]]>";
                string xmlRequest = Utility.ParseXMLToString<Model.NodeUpload.XML>(_UploadXML).Replace("<", "&lt;").Replace(">", "&gt;");
                Utility.AddLogText("上传前：" + xmlRequest);
                StringBuilder param = new StringBuilder();
                param.Append("<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:ser=\"http://service.webservice.vtradex.com\" > ");
                param.Append("<soapenv:Header/>");
                param.Append("<soapenv:Body>");
                param.Append("<ser:tmsShipmentInTransitXml>" + xmlRequest + "</ser:tmsShipmentInTransitXml>");
                param.Append("</soapenv:Body>");
                param.Append("</soapenv:Envelope>");
                xmlResult = HTTPHelper.SendHTTPRequest(param.ToString(), "tmsShipmentInTransitXml", "tmsShipmentInTransitXml");
                Model.NodeUpload.XML uploadResult = Utility.ParseXMLToObjec<Model.NodeUpload.XML>(xmlResult);
                if (uploadResult.MSGCODE == "0" || string.IsNullOrEmpty(uploadResult.MSGCODE))
                    msg += "上报失败" + " " + uploadResult.MSGCONTENT;
                else if (uploadResult.MSGCODE == "1")
                {
                    msg += "上报成功 ";
                    try
                    {
                        bool isArrived = false;
                        if (details.CONTENTLIST[0].DETAILLIST[0].TRACKTYPE == "18")//正常签收
                        {
                            isArrived = true;
                            msg += " 当前为签收节点 ";
                        }
                        bool isUpdate = NodeUploadServer.UpdateCurrentUploadNodeId(relation, isArrived, senderCodeList);
                        msg += isUpdate ? "更新当前上报节点成功" : "更新当前上报节点失败";
                        if (isArrived && isUpdate)
                            UploadCarrierArrived(details.CONTENTLIST[0].DETAILLIST[0], relation.CurrentUploadDataTime);
                    }
                    catch (Exception ex1)
                    {
                        msg += ex1.Message;
                    }
                }
                Utility.SaveXMLRequestAndRespond(param.ToString(), xmlResult, "Node", relation.RelationId);
                #endregion
            }
            catch (Exception ex)
            {
                msg += ex.Message;
                Utility.SaveErrLog(ex.Message, "Node");
                if (!ex.Message.Contains("重新开始") && !ex.Message.Contains("成功"))
                {
                    if(relation!=null)
                    NodeUploadServer.UpdateHandleTime(relation.Id, false);
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
        //    string msg = dtNow.ToString() + ":【节点上报】 ";
        //    string xmlResult = string.Empty;
        //    RelationModel relation = new RelationModel();
        //    try
        //    {
        //        List<string> senderCodeList = null;
        //        M_TMSNode loginkNode = null;
        //        if (Utility._LinkType == "2")
        //        {
        //            senderCodeList = TMSOrderServer.GetAllLoginkSenderCode();
        //            if (senderCodeList.Count == 0)
        //            {
        //                msg += "没有配置通过运管平台交换数据的上游发货单位";
        //                return;
        //            }
        //        }
        //        Model.NodeUpload.MESSAGEDETAIL.XML details = NodeUploadServer.GetFirstWaitUploadNode(senderCodeList, ref loginkNode, ref _ignoreTempRelationList, out relation);

        //        if (relation != null && relation.Id != 0)
        //            msg += string.Format("Relation【{0}】 Number【{1}】 CustomerId【{3}】 当前上报节点Id【{2}】 ", relation.RelationId, relation.Number, relation.CurrentUploadNodeId, relation.CustomerId);
        //        if (details == null)
        //        {
        //            if (relation != null && relation.Id != 0 && !_ignoreTempRelationList.Contains(relation.Id))
        //                _ignoreTempRelationList.Add(relation.Id);
        //            msg += "暂无需要上报的节点";
        //            return;
        //        }
        //        //运管平台上报
        //        if (senderCodeList != null)
        //        {
        //            string nodeXml = Utility.ParseXMLToString(loginkNode);
        //            string receiverCode = TMSOrderServer.GetAllLoginkSenderCode()[0];
        //            LoginkHelp.Send_Test(Utility._SecurityURL, Utility._TransportURL, Utility._MyCode, Utility._MyPwd, receiverCode, nodeXml, ActionType.LOGINK_CN_TRANSPORT_PREBOOKING);
        //            msg += "上报成功 Receiver[" + receiverCode + "]";
        //            bool isArrived = false;
        //            if (details.CONTENTLIST[0].DETAILLIST[0].TRACKTYPE == "18")//正常签收
        //            {
        //                isArrived = true;
        //                msg += " 当前为签收节点 ";
        //            }
        //            bool isUpdate = NodeUploadServer.UpdateCurrentUploadNodeId(relation, isArrived);
        //            Utility.SaveXMLRequestAndRespond(nodeXml, string.Empty, "Node", relation.RelationId);
        //            msg += isUpdate ? "更新当前上报节点成功" : "更新当前上报节点失败";
        //            return;
        //        }
        //        #region 大华东供应链节点上报处理
        //        _UploadXML.MESSAGEHEAD.SENDTIME = dtNow.ToString("yyyy-MM-dd HH:mm:ss");
        //        _UploadXML.MESSAGEHEAD.FILENAME = dtNow.ToString("yyyyMMddHHmmss");
        //        _UploadXML.MESSAGEDETAIL = "<![CDATA[" + Utility.ParseXMLToString<Model.NodeUpload.MESSAGEDETAIL.XML>(details) + "]]>";
        //        string xmlRequest = Utility.ParseXMLToString<Model.NodeUpload.XML>(_UploadXML).Replace("<", "&lt;").Replace(">", "&gt;");
        //        StringBuilder param = new StringBuilder();
        //        param.Append("<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:ser=\"http://service.webservice.vtradex.com\" > ");
        //        param.Append("<soapenv:Header/>");
        //        param.Append("<soapenv:Body>");
        //        param.Append("<ser:tmsShipmentInTransitXml>" + xmlRequest + "</ser:tmsShipmentInTransitXml>");
        //        param.Append("</soapenv:Body>");
        //        param.Append("</soapenv:Envelope>");
        //        xmlResult = HTTPHelper.SendHTTPRequest(param.ToString(), "tmsShipmentInTransitXml", "tmsShipmentInTransitXml");
        //        Model.NodeUpload.XML uploadResult = Utility.ParseXMLToObjec<Model.NodeUpload.XML>(xmlResult);
        //        if (uploadResult.MSGCODE == "0" || string.IsNullOrEmpty(uploadResult.MSGCODE))
        //            msg += "上报失败" + " " + uploadResult.MSGCONTENT;
        //        else if (uploadResult.MSGCODE == "1")
        //        {
        //            msg += "上报成功 ";
        //            try
        //            {
        //                bool isArrived = false;
        //                if (details.CONTENTLIST[0].DETAILLIST[0].TRACKTYPE == "18")//正常签收
        //                {
        //                    isArrived = true;
        //                    msg += " 当前为签收节点 ";
        //                }
        //                bool isUpdate = NodeUploadServer.UpdateCurrentUploadNodeId(relation, isArrived);
        //                msg += isUpdate ? "更新当前上报节点成功" : "更新当前上报节点失败";
        //                if (isArrived && isUpdate)
        //                    UploadCarrierArrived(details.CONTENTLIST[0].DETAILLIST[0], relation.CurrentUploadDataTime);
        //            }
        //            catch (Exception ex1)
        //            {
        //                msg += ex1.Message;
        //            }
        //        }
        //        Utility.SaveXMLRequestAndRespond(param.ToString(), xmlResult, "Node", relation.RelationId);
        //        #endregion


        //    }
        //    catch (Exception ex)
        //    {
        //        msg += ex.Message;
        //        Utility.SaveErrLog(ex.Message, "Node");
        //        if (!ex.Message.Contains("重新开始") && !ex.Message.Contains("成功"))
        //            SendEmail(new Exception(msg + "[" + ex.Message + "]"));
        //    }
        //    finally
        //    {
        //        Console.WriteLine(msg.Replace("\n", ""));
        //    }
        //}
        #endregion

        private static Model.NodeUpload.XML _ArrivedXML = new Model.NodeUpload.XML()
        {
            MESSAGEHEAD = new Model.MESSAGEHEAD()
            {
                SENDER = Utility._SecretKey,
                FILETYPE = "XML",
                CONTENTTYPE = "CarrierArrived",
                SENDTIME = string.Empty,
                FILENAME = string.Empty,
                FILEFUNCTION = "ADD"
            }
        };

        private static string GetShipmentCode(string orderNo) {
            string result = string.Empty;
            string sql = "select SHIPMENTCODE from huadong_tms_order where relationId = '"+ orderNo + "' and SecretKey not like '%未知%' order by CreateTime desc limit 1";
            try
            {
                Console.WriteLine(sql);
                result = DbHelperMySQL.GetSingle(sql).ToString();
            }
            catch
            {
            }
            return result;
        }

        public static void UploadCarrierArrived(C2LP.Assistant.TMSConsole.Model.NodeUpload.MESSAGEDETAIL.DETAIL arriedNodeDetail, string VEHICLENO)
        {
            DateTime dtNow = DateTime.Now;
            //string msg = dtNow.ToString() + ":【签收节点】 ";
            string xmlResult = string.Empty;
            StringBuilder param = new StringBuilder();
            try
            {
                Model.NodeUpload.SignOrder.XML details = new Model.NodeUpload.SignOrder.XML();
                Model.NodeUpload.SignOrder.DETAIL d = new Model.NodeUpload.SignOrder.DETAIL();
                d.SHIPMENTCODE = GetShipmentCode(arriedNodeDetail.LEGNO);
                Console.WriteLine("SHIPMENTCODE:"+d.SHIPMENTCODE);
                d.CECNO = arriedNodeDetail.CECNO;
                d.LEGCODE = arriedNodeDetail.LEGNO;
                d.SHIPDETAILID = arriedNodeDetail.ECNO;
                d.SIGNTIME = arriedNodeDetail.TRACKTIME;
                d.VEHICLENO = VEHICLENO;
                d.SIGNPERSON = arriedNodeDetail.TRACKPERSON;
                details.CONTENTLIST = new List<Model.NodeUpload.SignOrder.CONTENT>();
                details.CONTENTLIST.Add(new Model.NodeUpload.SignOrder.CONTENT());
                details.CONTENTLIST[0].DETAILLIST = new List<Model.NodeUpload.SignOrder.DETAIL>();
                details.CONTENTLIST[0].DETAILLIST.Add(d);
                _ArrivedXML.MESSAGEHEAD.SENDTIME = dtNow.ToString("yyyy-MM-dd HH:mm:ss");
                _ArrivedXML.MESSAGEHEAD.FILENAME = dtNow.ToString("yyyyMMddHHmmss");
                _ArrivedXML.MESSAGEDETAIL = "<![CDATA[" + Utility.ParseXMLToString<Model.NodeUpload.SignOrder.XML>(details) + "]]>";
                string xmlRequest = Utility.ParseXMLToString<Model.NodeUpload.XML>(_ArrivedXML).Replace("<", "&lt;").Replace(">", "&gt;");
                param.Append("<soapenv:Envelope xmlns:soapenv=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:ser=\"http://service.webservice.vtradex.com\" > ");
                param.Append("<soapenv:Header/>");
                param.Append("<soapenv:Body>");
                param.Append("<ser:tmsShipmentQianshouXml>" + xmlRequest + "</ser:tmsShipmentQianshouXml>");
                param.Append("</soapenv:Body>");
                param.Append("</soapenv:Envelope>");
                xmlResult = HTTPHelper.SendHTTPRequest(param.ToString(), "setTmsShipmentQianshou", "tmsShipmentQianshouXml");
                Model.NodeUpload.XML uploadResult = Utility.ParseXMLToObjec<Model.NodeUpload.XML>(xmlResult);
                if (uploadResult.MSGCODE == "0" || string.IsNullOrEmpty(uploadResult.MSGCODE))
                    throw new Exception(" 签收节点上报失败SHIPMENTCODE="+d.SHIPMENTCODE + "" + " " + uploadResult.MSGCONTENT);
            }
            catch (Exception ex)
            {
                Utility.SaveErrLog(ex.Message, "Arrived");
                throw ex;
            }
            finally
            {
                Utility.SaveXMLRequestAndRespond(param.ToString(), xmlResult, "Arrived", arriedNodeDetail.CECNO + "_" + arriedNodeDetail.LEGNO + "_" + arriedNodeDetail.ECNO);
            }

        }

        public void SendEmail(Exception ex, string filePath = null)
        {
            //string senderServerIp = "smtp.ym.163.com";
            //string toMailAddress = "zhaoyou@thermoberg.com";
            //string fromMailAddress = "staff@thermoberg.com";
            //string subjectInfo = "C2LP.Assistant.TMSConsole.SyncNodeUpload 节点上报失败";
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
