using com.wondersgroup.cuteinfo.csharp.client.common.pojo;
using com.wondersgroup.cuteinfo.csharp.client.common.service.ExchangeTransport;
using com.wondersgroup.cuteinfo.csharp.client.common.service.ExchangeTransport.pojo;
using com.wondersgroup.cuteinfo.csharp.client.Iinterface.impl;
using LoginkExchange;
using System;
using System.Collections.Generic;
using System.Xml;

namespace C2LP.Assistant.TMSConsole.Logink
{
    public enum ActionType
    {
        /// <summary>
        /// 普通货物运输预约单 托运方=>承运方
        /// </summary>
        LOGINK_CN_TRANSPORT_PREBOOKING,

        /// <summary>
        /// 普通运输托运状态变化单 承运方=>托运方
        /// </summary>
        JTWL_ENTRUST_TRANS_BookingNoteStatus
    }
    public class LoginkHelp
    {

        private static ExchangeTransport exchangeTransport = new ExchangeTransport();

        /// <summary>
        /// 正式环境发送数据
        /// </summary>
        public static void Send(string securityURL, string targetUrl, string userCode, string userPwd, string toCode, string dataStr, ActionType actionType)
        {
            try
            {
                exchangeTransport = new ExchangeTransport(userCode, userPwd, "ECE91161D6670E60E040A8C0970C6ACD");
                exchangeTransport.AuthServiceUrl = securityURL;
                exchangeTransport.ServiceUrl = targetUrl;
                exchangeTransport.Authenticate();
                if (!exchangeTransport.IsAuthValid)
                    throw new Exception("认证失败[" + userCode + "]");
                string strEventID = exchangeTransport.GetEventId();    //调用函数即可，事件ID会自动生成
                string strBaseEncodedData = exchangeTransport.Base64Encode(dataStr);
                strBaseEncodedData = exchangeTransport.Base64Encode(strBaseEncodedData);
                string strResult = exchangeTransport.Send(toCode, strEventID, actionType.ToString(), strBaseEncodedData);
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(strResult);
                XmlNodeList nodes = xml.GetElementsByTagName("ns3:SendResult");
                if (nodes.Count > 0)
                {
                    string Result = nodes[0].InnerText;
                    if (Result == "false")
                        throw new Exception(strResult);
                }
                else
                    throw new Exception(strResult);
            }
            catch (Exception ex)
            {
                throw new Exception("接收者编号:[" + toCode + "] 发送出错[" + ex.Message + "]");
            }
        }

        /// <summary>
        /// 正式环境接收数据
        /// </summary>
        public static MyReceiveResponses Receive(string securityURL, string targetUrl, string userCode, string userPwd, ref ExchangeTransport exchangeTransport)
        {
            try
            {
                exchangeTransport = new ExchangeTransport(userCode, userPwd, "ECE91161D6670E60E040A8C0970C6ACD");
                exchangeTransport.AuthServiceUrl = securityURL;
                exchangeTransport.ServiceUrl = targetUrl;
                exchangeTransport.Authenticate();
                if (!exchangeTransport.IsAuthValid)
                    throw new Exception("认证失败[" + userCode + "]");
                string strResult = exchangeTransport.Receive("1");
                return new MyReceiveResponses(strResult);
            }
            catch (Exception ex)
            {
                throw new Exception("接收出错[" + ex.Message + "]");
            }
        }

        /// <summary>
        /// 正式环境接收确认
        /// </summary>
        public static bool Confirm(ExchangeTransport exchangeTransport, string eventId)
        {
            try
            {
                string strResult = exchangeTransport.ReceiveConfirm(eventId);
                XmlDocument xml = new XmlDocument();
                xml.LoadXml(strResult);
                XmlNodeList nodes = xml.GetElementsByTagName("ns3:confirmResult");
                if (nodes.Count > 0)
                {
                    string Result = nodes[0].InnerText;
                    if (Result == "false")
                        throw new Exception(strResult);
                }
                else
                    throw new Exception(strResult);
            }
            catch (UCommonFaultType ex)
            {
                throw new Exception(string.Format("接收确认失败[失败代码:{0} 失败内容:{1}]", ex.getCodeField().Trim(), ex.getErrorMessageField().Trim()));
            }
            catch (Exception ex)
            {
                throw new Exception("调用接收确认异常:" + ex.GetBaseException());
            }
            return true;
        }

        #region 测试环境
        public static void Send_Test(string securityURL, string targetUrl, string userCode, string userPwd, string toCode, string dataStr, ActionType actionType)
        {
            try
            {
                UserSecurityImpl imp = new UserSecurityImpl(securityURL);
                UUserToken _token = imp.authenticate(userCode, userPwd);
                UExchangeTransportImpl iet = new UExchangeTransportImpl(targetUrl);
                string[] toAddress = new string[] { toCode };                //接收者地址
                //string actionType = "LOGINK_CN_TRANSPORT_PREBOOKING";           //接收者地址
                int sendType = 2;             //定义好发送事件类型   1:简单值(即键值对)(类型为 Dictionary ) 2: xml文档，类型为 string
                string sendData = dataStr;        //定义好数据信息 sendData 类型参照 sendType 的定义(即1为 Dictionary, 2为 string);
                USendRequest sendRequest = new USendRequest(toAddress);
                sendRequest.addSendEvent(actionType.ToString(), sendType, sendData);
                String eventId = sendRequest.getExchangeEvent().getEventId();
                USendResponse sendResponse = iet.send(_token, sendRequest);
                if (!sendResponse.SendResult)
                {
                    throw new Exception(string.Format("发送失败[事件编号:{0} 异常代码:{1} 异常消息:{2} 接收者编号:{3}]", eventId, sendResponse.Fault.getCodeField().Trim(), sendResponse.Fault.getErrorMessageField().Trim(), toCode));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("接收者编号:[" + toCode + "] 发送出错[" + ex.Message + "]");
            }
        }

        /// <summary>
        /// 固定接收一条数据
        /// </summary>
        /// <param name="securityURL"></param>
        /// <param name="targetUrl"></param>
        /// <param name="userCode"></param>
        /// <param name="userPwd"></param>
        /// <returns></returns>
        public static UReceiveResponse Receive_Test(string securityURL, string targetUrl, string userCode, string userPwd, ref UUserToken _token)
        {
            try
            {
                UserSecurityImpl imp = new UserSecurityImpl(securityURL);
                _token = imp.authenticate(userCode, userPwd);
                UExchangeTransportImpl ueti = new UExchangeTransportImpl(targetUrl);
                UReceiveRequest receiveRequest = new UReceiveRequest(userCode, 1);
                try
                {
                    UReceiveResponse response = ueti.receive(_token, receiveRequest);
                    return response;
                }
                catch (UCommonFaultType ex)
                {
                    throw new Exception(string.Format("接收失败[失败代码:{0} 失败内容:{1}]", ex.getCodeField().Trim(), ex.getErrorMessageField().Trim()));
                }
                catch (Exception ex)
                {
                    throw new Exception("接收出现错误或者没有数据接收:" + ex.GetBaseException());
                }
            }
            catch (Exception ex)
            {
                throw new Exception("接收出错[" + ex.Message + "]");
            }
        }

        public static bool Confirm_Test(UReceiveResponse response, string targetUrl, UUserToken _token)
        {
            try
            {
                UExchangeTransportImpl ueti = new UExchangeTransportImpl(targetUrl);
                UConfirmReceiveResponse ConfirmResponse = ueti.confirm(_token, response.getConfirmReceiveRequest());
                if (ConfirmResponse != null)
                {
                    if (ConfirmResponse.getConfirmResultField() == "true")
                    {
                        return true;
                    }
                }
            }
            catch (UCommonFaultType ex)
            {
                throw new Exception(string.Format("接收确认失败[失败代码:{0} 失败内容:{1}]", ex.getCodeField().Trim(), ex.getErrorMessageField().Trim()));
            }
            catch (Exception ex)
            {
                throw new Exception("调用接收确认异常:" + ex.GetBaseException());
            }
            return false;
        }
        #endregion
    }

    public class MyReceiveResponses
    {
        private string _XML;
        private XmlDocument _XmlDoc;

        public MyReceiveResponses(string xmlStr)
        {
            try
            {
                _XML = xmlStr.Trim();
                _XmlDoc = new XmlDocument();
                _XmlDoc.LoadXml(_XML);
            }
            catch (Exception ex)
            {
                throw new Exception("LoadXml出错[" + ex.Message + "] XmlString[" + xmlStr + "]");
            }
        }

        private List<MyEventReceiver> _EventReceiverList;
        public List<MyEventReceiver> getEventReceiverList()
        {
            if (_EventReceiverList == null)
            {
                _EventReceiverList = new List<MyEventReceiver>();
                string tagName = "ns3:ReceiveExchangeEvent";
                foreach (XmlNode item in _XmlDoc.GetElementsByTagName(tagName))
                {
                    _EventReceiverList.Add(new MyEventReceiver(item,_XML));
                }
            }
            return _EventReceiverList;
        }
    }

    public class MyEventReceiver
    {
        private XmlNode _XmlNode;
        private string _ParentXml;

        public MyEventReceiver(XmlNode xmlNode,string parentXml)
        {
            _XmlNode = xmlNode;
            _ParentXml = parentXml;
        }

        private string _EventSender;
        public string getEventsender()
        {
            if (_EventSender == null)
            {
                _EventSender = string.Empty;
                string attName = "EventSender";
                _EventSender = _XmlNode.Attributes[attName].Value;
            }
            return _EventSender;
        }

        private string _EventId;
        public string getEventid()
        {
            if (_EventId == null)
            {
                _EventId = string.Empty;
                _EventId = GetEventId();
            }
            return _EventId;
        }

        private string _Xmlcontent;
        public string getXmlcontent()
        {
            if (_Xmlcontent == null)
            {
                _Xmlcontent = string.Empty;
                _Xmlcontent = GetXMLContent();
            }
            return _Xmlcontent;
        }

        private string GetXMLContent(XmlNode node = null)
        {
            XmlNode currNode = node == null ? _XmlNode : node;
            foreach (XmlNode item in currNode.ChildNodes)
            {
                if (item.Name == "Base64EncodedData")
                {
                    try
                    {
                        byte[] base64EncodedBytes = System.Convert.FromBase64String(item.InnerText);
                        string temp = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
                        base64EncodedBytes = System.Convert.FromBase64String(temp);
                        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("订单解析失败["+ex.Message+ "] XML["+_ParentXml+"]");
                    }
                }
                else
                    return GetXMLContent(item);
            }
            return string.Empty;
        }

        private string GetEventId(XmlNode node = null)
        {
            XmlNode currNode = node == null ? _XmlNode : node;
            foreach (XmlNode item in currNode.ChildNodes)
            {
                foreach (XmlAttribute att in item.Attributes)
                {
                    if (att.Name == "EventID")
                        return att.Value;
                }
                return GetEventId(item);
            }
            return string.Empty;
        }
    }
}
