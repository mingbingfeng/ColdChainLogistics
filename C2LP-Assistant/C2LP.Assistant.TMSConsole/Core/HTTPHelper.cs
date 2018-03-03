using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;

namespace C2LP.Assistant.TMSConsole.Core
{
    class HTTPHelper
    {
        /// <summary>
        /// 获取TMS运单信息
        /// </summary>
        /// <returns></returns>
        public static string SendHTTPRequest(string param, string method,string returnNodeName)
        {
            string result = string.Empty;
            // webservice调用地址
            string url = Utility._ServerURL;
            try
            {
                // 创建HttpWebRequest对象
                HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
                // 设置POST调用方法
                httpRequest.Method = "POST";
                // 设置HTTP头ContentType
                httpRequest.ContentType = "text/xml;charset=UTF-8";
                // 设置HTTP头SOAPAction的值
                httpRequest.Headers.Add("Accept-Encoding:gzip, deflate");
                httpRequest.Headers.Add("SOAPAction", method);
                httpRequest.UserAgent = "Apache-HttpClient/4.1.1 (java 1.5)";
                httpRequest.KeepAlive = true;
                httpRequest.Timeout = 10000000;//设定超时时间
                // 调用内容
                byte[] bytes = Encoding.UTF8.GetBytes(param.ToString());
                // 设置HTTP头内容的长度
                //httpRequest.ContentLength = param.ToString().Length;
                using (Stream reqStream = httpRequest.GetRequestStream())
                {
                    reqStream.Write(bytes, 0, bytes.Length);
                    reqStream.Flush();
                }
                // HttpWebRequest发起调用
                using (HttpWebResponse myResponse = (HttpWebResponse)httpRequest.GetResponse())
                {
                    // StreamReader对象
                    StreamReader sr = new StreamReader(myResponse.GetResponseStream(), Encoding.UTF8);
                    // 返回结果
                    result = sr.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("调用接口[" + method + "]失败：" + ex.Message);
            }
            try
            {
                XmlDocument xdoc = new XmlDocument();
                using (StringReader stream = new StringReader(result))
                {
                    xdoc.Load(stream);
                }
                result = xdoc.DocumentElement["soapenv:Body"][returnNodeName + "Return"].InnerXml;
                result = result.Replace("&lt;", "<").Replace("&gt;", ">");
            }
            catch (Exception ex)
            {
                throw new Exception("解析接口返回字符串失败：" + ex.Message + " 返回内容[" + result + "]");
            }
            return result;
        }
    }
}
