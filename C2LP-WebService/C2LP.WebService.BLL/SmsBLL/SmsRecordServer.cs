using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2LP.WebService.BLL.SmsBLL
{
    public class SmsRecordServer : BaseServer
    {
        public static string _SendOrgList
        {
            get
            {
                string temp = string.Empty;
                try
                {
                    temp = System.IO.File.ReadAllText("SendOrgList.txt");
                }
                catch
                {
                }
                return temp;
            }
        }
        /// <summary>
        /// 查询下一个待发送的短信记录
        /// waitsendsms中的数据由waybill_node表中的触发器生成
        /// </summary>
        /// <returns></returns>
        public static Model.Model_SmsReord GetNextWaitSendSmsRecord()
        {
            Model.Model_SmsReord sms = null;
            string sql = "select s.*,w.number,w.SenderOrg,w.ReceiverOrg,w.senderTel,w.receiverTel,N.StorageName from waitsendsms s left join waybill_base w  on s.baseid = w.id left join waybill_node n on n.Id=s.NodeId where CreateAt>DATE_SUB(CURDATE(), INTERVAL 1 DAY) order by createAt desc limit 1";
            try
            {
                sms = _SqlHelp.ExecuteObject<Model.Model_SmsReord>(sql);
                string orgName = string.Empty;
                if (sms != null)
                {
                    if (sms.Arrived == Model.MyEnum.Enum_Arrived.InTransit)
                    {
                        sms.SmsReceiver = sms.ReceiverTel;
                        orgName = sms.ReceiverOrg;
                    }
                    else
                    {
                        sms.SmsReceiver = sms.SenderTel;
                        orgName = sms.SenderOrg;
                    }
                    if (!_SendOrgList.Contains(orgName+"、"))
                    {
                        sms.SendTime = DateTime.Now;
                        sms.SendResult = "此客户暂不接收短信";
                        sms.SmsContent = sms.ToString();
                        AddSendSmsRecord(sms);
                        Console.WriteLine(orgName + " " + sms.SendResult);
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("查询短信记录出错[" + ex.Message + "]");
            }
            return sms;
        }

        /// <summary>
        /// 添加短信发送记录并删除待发送记录
        /// </summary>
        /// <param name="sms"></param>
        /// <returns></returns>
        public static bool AddSendSmsRecord(Model.Model_SmsReord sms)
        {
            bool result = false;
            try
            {
                List<string> sqlList = new List<string>();
                sqlList.Add("delete from waitsendsms where Id = " + sms.Id);
                sqlList.Add(string.Format("insert into SmsRecord(BaseId,NodeId,Arrived,CreateAt,SmsReceiver,SmsContent,SendTime,SmsSendResult) Values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')", sms.BaseId, sms.NodeId, (int)sms.Arrived, sms.CreateAt.ToString("yyyy-MM-dd HH:mm:ss"), sms.SmsReceiver, sms.SmsContent, sms.SendTime.ToString("yyyy-MM-dd HH:mm:ss"), sms.SendResult));
                result = _SqlHelp.ExecuteTranstration(sqlList);
            }
            catch (Exception ex)
            {
                throw new Exception("保存发送记录失败[" + ex.Message + "]");
            }
            return result;
        }
    }
}
