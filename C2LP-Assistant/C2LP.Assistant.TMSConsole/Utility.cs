using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace C2LP.Assistant.TMSConsole
{
    class Utility
    {
        /// <summary>
        /// 当前程序版本号
        /// </summary>
        public static readonly string _ThisVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

        /// <summary>
        /// 承运商密钥
        /// </summary>
        public static string _SecretKey = ConfigurationManager.AppSettings["SecretKey"];

        /// <summary>
        /// 接口服务器地址
        /// </summary>
        public static string _ServerURL = ConfigurationManager.AppSettings["ServerURL"];

        /// <summary>
        /// 同步下载TMS运单的间隔
        /// </summary>
        public static string _SyncTMSOrderInterval = ConfigurationManager.AppSettings["SyncTMSOrderInterval"];

        /// <summary>
        /// 节点上传的间隔
        /// </summary>
        public static string _SyncNodeUploadInterval = ConfigurationManager.AppSettings["SyncNodeUploadInterval"];

        /// <summary>
        /// 新的上报机制 节点上传的间隔
        /// </summary>
        public static string _SyncNodeUploadInterval_New = ConfigurationManager.AppSettings["SyncNodeUploadInterval_New"];

        /// <summary>
        /// 新的上报机制 数据上传的间隔
        /// </summary>
        public static string _SyncNodeDataUploadInterval_New = ConfigurationManager.AppSettings["SyncNodeDataUploadInterval_New"];

        /// <summary>
        /// 数据上传的间隔
        /// </summary>
        public static string _SyncNodeDataUploadInterval = ConfigurationManager.AppSettings["SyncNodeDataUploadInterval"];

        /// <summary>
        /// 每次上传的节点数据量
        /// </summary>
        public static string _NodeDataUploadCount = ConfigurationManager.AppSettings["NodeDataUploadCount"];

        /// <summary>
        /// 冷藏载体温湿度等待超时时长，单位天
        /// </summary>
        public static string _StorageDataTimeOut = ConfigurationManager.AppSettings["StorageDataTimeOut"];

        /// <summary>
        /// 重试解析失败目录的TMS运单 0：不重试;   >0：重试解析的间隔
        /// </summary>
        public static string _RetryFaildTMSOrderUploadInterval = ConfigurationManager.AppSettings["RetryFaildTMSOrderUploadInterval"];


        /// <summary>
        /// 惊尘与上游供应商对接的方式 1 大华东供应链 2 运管平台对接
        /// </summary>
        public static string _LinkType = ConfigurationManager.AppSettings["LinkType"];
        //当LinkType为2时，以下参数才生效
        /// <summary>
        /// 运管平台认证地址
        /// </summary>
        public static string _SecurityURL = ConfigurationManager.AppSettings["SecurityURL"];
        /// <summary>
        /// 运管平台数据交换地址
        /// </summary>
        public static string _TransportURL = ConfigurationManager.AppSettings["TransportURL"];
        /// <summary>
        /// 我的运管平台物流交换代码
        /// </summary>
        public static string _MyCode = ConfigurationManager.AppSettings["MyCode"];
        /// <summary>
        /// 我的运管平台密码
        /// </summary>
        public static string _MyPwd = ConfigurationManager.AppSettings["MyPwd"];

        private static int? HandleScanLaterOrderTime = null;
        /// <summary>
        /// 每天自动检查先扫描后派单的运单并进行处理
        /// 0-23：每天整点自动执行一次;
        /// 非0-23：仅启动程序时执行一次.
        /// </summary>
        public static int _HandleScanLaterOrderTime {
            get {
                if (HandleScanLaterOrderTime == null)
                {
                    try
                    {
                        HandleScanLaterOrderTime = int.Parse(ConfigurationManager.AppSettings["HandleScanLaterOrderTime"]);
                    }
                    catch 
                    {
                        HandleScanLaterOrderTime = -1;
                    }
                }
                return (int)HandleScanLaterOrderTime;
            }
        }

        private static string CheckXMLStr(string xmlText)
        {
            try
            {
                XmlDocument xxx = new XmlDocument();
                xxx.Load(new StringReader(xmlText));
            }
            catch (Exception ex)
            {
                if (ex is XmlException && ex.Message != null && ex.Message.Contains("上的开始标记“") && ex.Message.Contains("”与结束标记") && ex.Message.Contains("不匹配"))
                {
                    try
                    {
                        int startIndex = ex.Message.IndexOf("上的开始标记“");
                        int endIndex = ex.Message.IndexOf("”与结束标记");
                        string errText = ex.Message.Substring(startIndex + 7, endIndex - startIndex - 7);
                        xmlText = xmlText.Replace("<" + errText + ">", "&lt;" + errText + "&gt;");
                    }
                    catch
                    {
                        throw ex;
                    }
                    return CheckXMLStr(xmlText);
                }
                else if (ex is XmlException && ex.Message != null && ex.Message.Contains("名称不能以“") && ex.Message.Contains("开头。"))
                {
                    try
                    {
                        XmlException ex1 = ex as XmlException;
                        StringBuilder tempXmlText = new StringBuilder(xmlText);
                        xmlText = tempXmlText.Replace("<", "&lt;", ex1.LinePosition - 2, 1).ToString();
                    }
                    catch
                    {
                        throw ex;
                    }
                    return CheckXMLStr(xmlText);
                }
                throw ex;
            }
            return xmlText;
        }

        /// <summary>
        /// 解析XML为对象
        /// </summary>
        /// <typeparam name="T">实体模型</typeparam>
        /// <param name="xmlText">xml文本</param>
        /// <returns></returns>
        public static T ParseXMLToObjec<T>(string xmlText, string requestXml = null)
        {
            try
            {
                string newXmlText = CheckXMLStr(xmlText);
                T obj = default(T);
                XmlSerializer xs = new XmlSerializer(typeof(T));

                using (StringReader sr = new StringReader(newXmlText))
                {
                    obj = (T)xs.Deserialize(sr);
                }
                return obj;
            }
            catch (Exception ex)
            {
                string msg = "解析XML失败：" + ex.Message + ";XMLText[" + xmlText + "]";
                if (requestXml != null)
                    msg += " RequestXml[" + requestXml + "]";
                throw new Exception(msg);
            }
        }

        /// <summary>
        /// 解析对象为XML字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string ParseXMLToString<T>(T obj)
        {
            string xmlString = string.Empty;
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    xmlSerializer.Serialize(ms, obj);
                    xmlString = Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            catch (Exception ex)
            {
                throw new Exception("序列化XML失败：" + ex.Message);
            }

            return xmlString;
        }

        /// <summary>
        /// 将xml文件存为文件
        /// </summary>
        /// <param name="xmlText">xml文本</param>
        /// <returns></returns>
        public static string SaveXMLFile(string xmlText, DateTime dtNow, string parentPath = null)
        {
            string result = string.Empty;
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "SyncTMSOrderFaild/";
            if (parentPath != null)
                filePath += parentPath + "/";
            string fileName = dtNow.ToString("yyyyMMddHHmmss") + ".xml";
            try
            {
                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);
                using (FileStream fs = new FileStream(filePath + fileName, FileMode.CreateNew, FileAccess.ReadWrite))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.Write(xmlText);
                        result = filePath + fileName;
                    }
                }
            }
            catch
            {

            }
            return result;
        }

        public static void SaveXMLRequestAndRespond(string requestXml, string respondXml, string requestType, string number)
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "AccessRecord/";
            string fileName = requestType + "_" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + number + ".xml";
            try
            {
                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);
                using (FileStream fs = new FileStream(filePath + fileName, FileMode.CreateNew, FileAccess.ReadWrite))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.Write(requestXml.Replace("&lt;", "<").Replace("&gt;", ">"));
                        sw.WriteLine();
                        sw.WriteLine();
                        sw.WriteLine("-------------------------------------------------------------------------");
                        sw.WriteLine();
                        sw.Write(respondXml.Replace("&lt;", "<").Replace("&gt;", ">"));
                    }
                }
            }
            catch
            {

            }
        }

        public static void SaveErrLog(string msg, string requestType)
        {
            string filePath = AppDomain.CurrentDomain.BaseDirectory + "ErrLog/" + requestType + "/";
            string fileName = requestType + "_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            try
            {
                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);
                using (FileStream fs = new FileStream(filePath + fileName, FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.Write(DateTime.Now.ToString("HH:mm:ss") + msg);
                        sw.WriteLine();
                    }
                }
            }
            catch
            {

            }
        }

        #region log
        /// <summary>
        /// 显示信息到文本框和创建日志记录
        /// </summary>
        public static void AddLogText(string msg,bool showLog =false)
        {
            try
            {
                msg = string.Format("{0}:{1}{2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), msg, "\r\n");
                if (showLog)
                    System.Console.WriteLine(msg);
                //记录到日志中
                SaveLogText(msg);
            }
            catch (Exception ex)
            {
                //System.Console.WriteLine("保存日志失败" + ex.Message + "," + msg);
                SaveLogText("保存日志失败," + ex.Message + "," + msg);
            }
        }
        /// <summary>
        /// 保存记录到日志中
        /// </summary>
        private static void SaveLogText(string msg)
        {
            try
            {
                int _days = Convert.ToInt32(ConfigurationManager.AppSettings["Days"]);
                string currentPath = System.AppDomain.CurrentDomain.BaseDirectory + "log" + "\\" + DateTime.Now.ToString("yyyyMMdd");
                //判断路径是否存在
                if (!Directory.Exists(currentPath))
                {
                    Directory.CreateDirectory(currentPath);//创建新路径
                                                           //判断文件目录下的子目录数量是否大于天数

                }
                while (Directory.GetDirectories(System.AppDomain.CurrentDomain.BaseDirectory + "log").Length > (_days < 3 ? 3 : _days))
                {
                    //删除超过天数的文件
                    Directory.Delete(Directory.GetDirectories(System.AppDomain.CurrentDomain.BaseDirectory + "log")[0], true);
                }
                bool saveToFileSuccess = false;
                int retryCount = 0;
                while (!saveToFileSuccess && retryCount < 3)
                {
                    try
                    {
                        //拼接创建文本文档路径
                        string filePath = currentPath + "\\" + DateTime.Now.ToString("HH") + (retryCount == 0 ? "" : "_" + retryCount) + ".txt";
                        using (FileStream myFs = new FileStream(filePath, FileMode.Append)) //创建文本文档
                        {
                            using (StreamWriter mySw = new StreamWriter(myFs))
                            {
                                //在文本最后面写入记录
                                mySw.WriteLine(msg.Replace("\r\n", ""));
                                mySw.Flush();
                                break;
                            }
                        }
                    }
                    catch (Exception)
                    {
                        retryCount++;
                    }
                }
            }
            catch (Exception ex)
            {
                //Utility.AddLogText("创建log文件失败" + ex.Message);
            }
        }
        #endregion
    }
}
