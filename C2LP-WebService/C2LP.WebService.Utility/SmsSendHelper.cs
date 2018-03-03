using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2LP.WebService.Utility
{
    public class SmsSendHelper
    {
        private static SmsWebReference.PhoneCaller smsClient ;
        private static string _ProjectNo;
        private static string _ProjectKey;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="smsAddress"></param>
        /// <param name="prjNo"></param>
        /// <param name="prjKey"></param>
        public static void Init(string smsAddress, string prjNo, string prjKey) {
            _ProjectNo = prjNo;
            _ProjectKey = prjKey;
            smsClient = new SmsWebReference.PhoneCaller();
            smsClient.Url =string.Format("http://{0}/Services/TelephoneService", smsAddress) ;
        }

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="prjNo"></param>
        /// <param name="prjKey"></param>
        /// <param name="number"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string SendSms( string number, string message)
        {
            string result = "发送失败";
            bool sendResult = false;
            bool sendResultSpec = true;
            try
            {
                smsClient.sendMsg2018("Thermoberg", _ProjectNo, _ProjectKey, number, message, "226651", out sendResult, out sendResultSpec);
                //smsClient.sendMessageAuthCode("Thermoberg", _ProjectNo, _ProjectKey, number, message, out sendResult, out sendResultSpec);
                if (sendResult)
                    result = "发送成功";
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }
    }
}
