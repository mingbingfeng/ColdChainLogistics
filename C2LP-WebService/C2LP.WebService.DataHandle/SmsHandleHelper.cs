using C2LP.WebService.BLL;
using C2LP.WebService.BLL.SmsBLL;
using C2LP.WebService.Model;
using C2LP.WebService.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2LP.WebService.DataHandle
{
    /// <summary>
    /// 短信发送线程
    /// </summary>
    public class SmsHandleHelper : QueueThreadBase<Model_SmsReord>
    {
        private string _SmsContentModel = string.Empty;

        public SmsHandleHelper(IEnumerable<Model_SmsReord> collection, string smsModel) : base(collection)
        {
            _SmsContentModel = smsModel;
        }

        protected override DoWorkResult DoWork(Model_SmsReord sms)
        {
            try
            {
                string smsContent = _SmsContentModel.Replace("【运单编号】", sms.Number).Replace("【发货单位】", sms.SenderOrg).Replace("【收货单位】", sms.ReceiverOrg).Replace("【车牌号码】", sms.StorageName);
                sms.SmsContent = smsContent;
                sms.SendTime = DateTime.Now;
                if (string.IsNullOrEmpty(sms.SmsReceiver) || sms.SmsReceiver.Length != 11)
                    sms.SendResult = "接收短信号码不正确";
                else
                    sms.SendResult = SmsSendHelper.SendSms(sms.SmsReceiver, sms.SmsContent);
            }
            catch (Exception ex)
            {
                sms.SendResult = ex.Message;
                //throw ex;
                //return DoWorkResult.AbortCurrentThread;//有异常,可以终止当前线程.当然.也可以继续,
                //return  DoWorkResult.AbortAllThread; //特殊情况下 ,有异常终止所有的线程...

            }
            finally
            {
                try
                {
                    SmsRecordServer.AddSendSmsRecord(sms);
                    //Console.WriteLine("发送短信："+sms.SmsContent+" 接收号码："+sms.SmsReceiver+" 发送结果："+sms.SendResult);
                }
                catch (Exception ex)
                {
                    LogServer.AddLogText("保存发送记录出错："+ex.Message,"SMS_"+DateTime.Now.ToString("yyyyMMdd"));
                }
            }
            //return base.DoWork(pendingValue);


            return DoWorkResult.ContinueThread;
        }
    }
}
