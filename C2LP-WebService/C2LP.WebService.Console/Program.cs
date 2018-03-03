using C2LP.WebService.BLL.ColdChainBLL;
using C2LP.WebService.DataHandle;
using C2LP.WebService.HandleServer;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;
using System.Configuration;
using C2LP.WebService.Utility;
using C2LP.WebService.BLL.SmsBLL;

namespace C2LP.WebService.Console
{
    class Program
    {
        /// <summary>
        /// 退出指令
        /// </summary>
        private static Dictionary<string, string> _keyList = new Dictionary<string, string>();

        /// <summary>
        /// 程序集版本号
        /// </summary>
        private static string _Version = System.Reflection.Assembly.GetEntryAssembly().GetName().Version.ToString();

        /// <summary>
        /// 服务主机
        /// </summary>
        private static ServiceHost _host_console = null;
        private static ServiceHost _host_pda = null;
        private static ServiceHost _host_coldChain = null;

        //上报数据处理参数
        private static int _upload_space = 10;//处理间隔10秒
        private static int _upload_count = 100;//每次处理的数量
        private static System.Timers.Timer _upload_timer = null;//处理数据的计时器
        private static int _upload_lastPrecent = -1;//当前任务线程处理进度
        private static int _upload_threadCount = 3;//同时处理数据的线程数量

        private static System.Timers.Timer _smsSend_timer = null;//处理短信的计时器
        private static string _smsAddress = string.Empty;//短信服务地址
        private static int _smsSendInterval;//短信处理间隔
        private static string _projectNo = string.Empty;//短信发送的工程编号
        private static string _projectKey = string.Empty;//短信发送工程编号对应的鉴权码
        private static int _smsSend_lastPrecent = -1;
        public static string _smsTookModel = "您的运单【运单编号】已经从【发货单位】开始扫描装车，由【车牌号码】运输【惊尘物流】";
        public static string _smsArriveModel = "您的运单【运单编号】已经运抵【收货单位】现场，【车牌号码】正在进行卸货操作【惊尘物流】";

        private static void InitKeyList()
        {
            _keyList.Add("/exit", "退出本系统");
            _keyList.Add("/help", "查看所有命令");
            _keyList.Add("/sms on", "打开短信功能");
            _keyList.Add("/sms off", "关闭短信功能");
        }

        /// <summary>
        /// 加载配置文件中的上报处理参数
        /// </summary>
        private static void InitUplpadConfig()
        {
            string tempStr = ConfigurationManager.AppSettings["Upload_Space"];
            int tempValue = 0;
            if (int.TryParse(tempStr, out tempValue))
                _upload_space = tempValue;
            tempStr = ConfigurationManager.AppSettings["Upload_Count"];
            if (int.TryParse(tempStr, out tempValue))
                _upload_count = tempValue;
            tempStr = ConfigurationManager.AppSettings["Upload_ThreadCount"];
            if (int.TryParse(tempStr, out tempValue))
                _upload_threadCount = tempValue;
            if (_upload_threadCount <= 0)
                _upload_threadCount = 1;
            if (_upload_threadCount > 16)
                _upload_threadCount = 16;
        }

        /// <summary>
        /// 加载配置文件中的短信处理参数
        /// </summary>
        private static void InitSmsSendConfig()
        {
            int.TryParse(ConfigurationManager.AppSettings["SmsSendInterval"], out _smsSendInterval);
            _smsAddress = ConfigurationManager.AppSettings["SMSAddress"];
            _projectNo = ConfigurationManager.AppSettings["PrjNo"];
            _projectKey = ConfigurationManager.AppSettings["PrjKey"];
            string tempStr = ConfigurationManager.AppSettings["SmsTookModel"];
            if (!string.IsNullOrEmpty(tempStr))
                _smsTookModel = tempStr;
            tempStr = ConfigurationManager.AppSettings["SmsArriveModel"];
            if (!string.IsNullOrEmpty(tempStr))
                _smsArriveModel = tempStr;
        }

        static void Main(string[] args)
        {
            InitUplpadConfig();
            InitSmsSendConfig();
            System.Console.WriteLine(string.Format("数据交换接口服务平台 V{0}", _Version));
            //System.Console.WriteLine("最新更改：增加批次获取区域信息的接口");
            //System.Console.WriteLine("最新更改：判断第三方运单信息是否存在");
            //System.Console.WriteLine("最新更改：客服关联信息新增省市第三级县级");
            //System.Console.WriteLine("最新更改：暂存没有查询到运单的节点,待运单上报时查询大于运单创建时间的暂存节点进行恢复");
            //System.Console.WriteLine("最新更改：添加接口,通过PDA最后同步时间来获取新的客户信息和区域信息");
            //System.Console.WriteLine("最新更改：添加接口，通过运输运单号或是tms运单号查询华东运单信息");
            //System.Console.WriteLine("最新更改：添加接口，通过第三方运单号查询对应信息");
            //System.Console.WriteLine("最新更改：修复第三方运单和节点保存错误的问题，增加新的上报接口上报第三方运单");
            //System.Console.WriteLine("最新更改：添加接口，同步第三方上游发货单位运管信息");
            //System.Console.WriteLine("最新更改：添加接口，支持PDA上报数据时携带供应商ID以便后台区分第三方供应商");
            //System.Console.WriteLine("最新更改：客户账号接口，查询具体客户是否存在相同账号");
            //System.Console.WriteLine("最新更改：弃用的接口由于还有旧PDA在使用，则特殊处理第三方运单带入大华东客户CustomerID=669");
            //System.Console.WriteLine("最新更改：修改查询运单节点信息错误");
            //System.Console.WriteLine("最新更改：第三方运单号使用固定补偿，大华东991+，华东医药981+");
            //System.Console.WriteLine("最新更改：第三方运单号收货单位显示具体客户名称");
            //System.Console.WriteLine("最新更改：模糊查询第三方、自运单信息");
            //System.Console.WriteLine("最新更改：更新存在运单的操作时间为最早的时间");
            //System.Console.WriteLine("最新更改：更新上传图片丢失问题");
            //System.Console.WriteLine("最新更改：更新pda重复上传运单删除节点问题");
            //System.Console.WriteLine("最新更改：第三方运单上报运抵节点时提升上报至大华东的优先级");
            //System.Console.WriteLine("最新更改：运单管理增加区域选项功能,根据区域显示对应的下游客户");
            //System.Console.WriteLine("最新更改：增加新的节点上报接口，接收新参数[上一个冷藏载体]，保存节点的同时保存冷链数据上报处理进度信息");
            //System.Console.WriteLine("最新更改：插入节点暂存表时增加字段[ParentStorageId、CustomerId]");
            //System.Console.WriteLine("最新更改：插入节点暂存表时同时也处理上报进度信息");
            //System.Console.WriteLine("最新更改：客户信息表[Customer]增加字段BindReceiverOrg与华东医药的收货单位进行匹配,原来的FullName字段与大华东的收货单位进行匹配");
            //System.Console.WriteLine("最新更改：华东医药运单增加计件数量，创建运单时从列JFQUNTITY中获取");
            System.Console.WriteLine("最新更改：更改短信平台调用的接口[sendMsg2018]");
            System.Console.WriteLine("接口服务启动中......");
            StartServer(_host_console, typeof(ConsoleServer));
            StartServer(_host_pda, typeof(PDAServer));
            StartServer(_host_coldChain, typeof(ColdChainServer));

            _upload_timer = new System.Timers.Timer(1000);
            _upload_timer.Elapsed += _upload_timer_Elapsed;
            _upload_timer.Start();

            System.Console.WriteLine(string.Format("已监听上报数据处理进度,处理间隔[{0}]秒,每次处理数量[{1}]条", _upload_space, _upload_count));
            //System.Console.WriteLine("请输入Help查看操作命令......");

            if (_smsSendInterval > 0)
            {
                SmsSendHelper.Init(_smsAddress, _projectNo, _projectKey);
                _smsSend_timer = new System.Timers.Timer(1000);
                _smsSend_timer.Elapsed += _smsSend_timer_Elapsed;
                _smsSend_timer.Start();
                System.Console.WriteLine(string.Format("已监听短信发送处理,处理间隔[{0}秒/一条],短信服务器[{1}],工程编号[{2}]", _smsSendInterval, _smsAddress, _projectNo));
            }
            else
                System.Console.WriteLine(string.Format("短信处理未开启,配置[SmsSendInterval={0}]", _smsSendInterval));

            System.Console.WriteLine("短信处理开关：" + _SmsFlag + ";输入/sms on为打开;输入/sms off为关闭.");
            //while (true)
            //{
            //    System.Console.ReadLine();
            //}
            InitKeyList();
            EnterKeyConsole();
        }

        /// <summary>
        /// 启动接口服务
        /// </summary>
        private static void StartServer(ServiceHost _host, Type serverType)
        {
            _host = new ServiceHost(serverType);
            try
            {
                _host.Open();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("进程不具有此命名空间的访问权限"))
                    System.Console.WriteLine("启动权限不足,请以管理员身份运行!");
                else
                    System.Console.WriteLine("服务启动出错:" + ex.Message);
                for (int i = 0; i < 3; i++)
                {
                    System.Console.WriteLine(string.Format("程序将在{0}秒后退出...", 3 - i));
                    Thread.Sleep(1000);
                }
                Environment.Exit(0);
            }
            System.Console.WriteLine("服务已启动,准备就绪!");
            foreach (Uri item in _host.BaseAddresses)
            {
                System.Console.WriteLine(item.ToString());
            }
        }

        /// <summary>
        /// 输入正确的指令才退出控制台
        /// </summary>
        private static void EnterKeyConsole()
        {
            string inputStr = System.Console.ReadLine().ToLower();
            if (_keyList.ContainsKey(inputStr))
            {
                switch (inputStr)
                {
                    case "/exit":
                        Exit();
                        return;
                    case "/help":
                        Help();
                        break;
                    case "/sms on":
                        _SmsFlag = "on";
                        System.Console.WriteLine("短信功能已打开！");
                        break;
                    case "/sms off":
                        _SmsFlag = "off";
                        System.Console.WriteLine("短信功能已关闭！");
                        break;
                }
            }
            EnterKeyConsole();
        }

        private static void Exit()
        {
            System.Console.WriteLine("正在关闭接口服务......");
            if (_host_console != null && _host_console.State == CommunicationState.Opened)
                _host_console.Close();
            if (_host_pda != null && _host_pda.State == CommunicationState.Opened)
                _host_pda.Close();

        }

        private static void Help()
        {
            System.Console.WriteLine("****************************************************");
            foreach (string item in _keyList.Keys)
            {
                System.Console.WriteLine(string.Format("{0}:{1}", item.ToUpper(), _keyList[item]));
            }
            System.Console.WriteLine("****************************************************");
        }

        static string _SmsFlag = "off";
        #region 短信处理
        private static void _smsSend_timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _smsSend_timer.Stop();
            try
            {
                if (_SmsFlag == "off")
                {
                    _smsSend_lastPrecent = 101;
                    return;
                }
                Model.Model_SmsReord sms = SmsRecordServer.GetNextWaitSendSmsRecord();
                if (sms != null)
                {
                    _smsSend_lastPrecent = 1;
                    System.Console.WriteLine("");
                    string smsModel = sms.Arrived == Model.MyEnum.Enum_Arrived.InTransit ? _smsTookModel : _smsArriveModel;
                    SmsHandleHelper smsHelp = new SmsHandleHelper(new List<Model.Model_SmsReord>() { sms }, smsModel);
                    smsHelp.ThreadCount = 1;
                    smsHelp.OneCompleted += SmsHelp_OneCompleted;
                    smsHelp.AllCompleted += SmsHelp_AllCompleted;
                    smsHelp.Start();
                }
                else
                {
                    _smsSend_lastPrecent = 101;
                    System.Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "：没有查询到或跳过此次待发送的短信记录");
                }
            }
            catch (Exception ex)
            {
                _smsSend_lastPrecent = 101;
                System.Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "：启动短信发送器失败:" + ex.Message);
            }
            finally
            {
                while (_smsSend_lastPrecent != 101)
                    Thread.Sleep(500);
                _smsSend_timer.Interval = _smsSendInterval * 1000;
                _smsSend_timer.Start();
            }
        }

        private static void SmsHelp_AllCompleted(QueueThreadBase<Model.Model_SmsReord>.CompetedEventArgs obj)
        {
            if (_upload_lastPrecent != 101)
            {
                //System.Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "：本次所查询到的数据已处理完毕,正在退出所有线程......");
                _smsSend_lastPrecent = 101;
            }
        }

        private static void SmsHelp_OneCompleted(Model.Model_SmsReord arg1, QueueThreadBase<Model.Model_SmsReord>.CompetedEventArgs arg2)
        {
            string modelInfo = arg1.ToString();
            if (arg2.InnerException != null)
                modelInfo += string.Format(" Error【{0}】", arg2.InnerException.Message);

            System.Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "：" + modelInfo);
        }
        #endregion

        #region 冷链数据处理
        private static void _upload_timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _upload_timer.Stop();
            try
            {
                List<WebService.Model.Model_NodeHistoryData> dataList = CC_HistDataServer.GetWaitHandleDataList(_upload_count);
                if (dataList.Count > 0)
                {
                    _upload_lastPrecent = 1;
                    System.Console.WriteLine("");
                    System.Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "：开始处理数据,本次共查到" + dataList.Count + "条需要处理的数据");
                    DataHandleHelper dataHelp = new DataHandleHelper(dataList);
                    dataHelp.ThreadCount = _upload_threadCount;
                    dataHelp.OneCompleted += DataHelp_OneCompleted;
                    dataHelp.AllCompleted += DataHelp_AllCompleted;
                    dataHelp.Start();
                }
                else
                {
                    _upload_lastPrecent = 101;
                    System.Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "：没有查询到任何待处理的数据");
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "：启动数据处理器失败:" + ex.Message);
            }
            finally
            {
                while (_upload_lastPrecent != 101)
                    Thread.Sleep(500);
                _upload_lastPrecent = -1;
                _upload_timer.Interval = _upload_space * 1000;
                _upload_timer.Start();
            }

        }

        private static void DataHelp_OneCompleted(Model.Model_NodeHistoryData arg1, QueueThreadBase<Model.Model_NodeHistoryData>.CompetedEventArgs arg2)
        {
            string modelInfo = arg1.ToString();
            if (arg2.InnerException != null)
            {
                modelInfo += string.Format(" 上报失败:Message【{0}】", arg2.InnerException.Message);

                System.Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "：" + modelInfo);
            }
        }

        private static void DataHelp_AllCompleted(QueueThreadBase<Model.Model_NodeHistoryData>.CompetedEventArgs obj)
        {
            if (_upload_lastPrecent != 101)
            {
                System.Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "：本次所查询到的数据已处理完毕,正在退出所有线程......");
                _upload_lastPrecent = 101;
            }
        }
        #endregion
    }
}
