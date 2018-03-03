using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace C2LP.WebService.BLL
{
    public class LogServer
    {
        #region 日志
        /// <summary>
        /// 显示信息到文本框和创建日志记录
        /// </summary>
        /// <param name="msg"></param>
        public static void AddLogText(string msg,string number)
        {
            try
            {
                msg = string.Format("{0}:{1}{2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), msg, "\r\n");
                //System.Console.WriteLine(msg);
                //记录到日志中
                SaveLogText(msg, number);

            }
            catch (Exception ex)
            {
                //System.Console.WriteLine("保存日志失败" + ex.Message + "," + msg);
                SaveLogText("保存日志失败," + ex.Message + "," + msg, number);
            }
        }
        /// <summary>
        /// 保存记录到日志中
        /// </summary>
        /// <param name="msg"></param>
        private static void SaveLogText(string msg,string number)
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
                        string filePath = currentPath + "\\" + DateTime.Now.ToString("HH") +"_"+ number + (retryCount == 0 ? "" : "_" + retryCount) + ".txt";
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
                //LogServer.AddLogText("创建log文件失败" + ex.Message);
            }
        }
        #endregion
    }
}
