using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using C2LP.PDA.TestWebService.PDAWR;

namespace C2LP.PDA.TestWebService
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private string _url = "220.248.66.105:8002";
        private int _space = 1000;
        private Thread _th = null;
        private int _requestCount = 0;
        private decimal _result = 0;
        private bool _isRun = false;
        private PDAServer _server = null;

        private void btnStart_Click(object sender, EventArgs e)
        {


            if (btnStart.Text == "开始")
            {
                _server = new PDAServer();
                _server.Url = string.Format("http://{0}/PDAServer/ws", _url);
                _requestCount = 0;
                _result = 0;
                SetText("开始发送请求......");
                _url = txtUrl.Text.Trim();
                _space = (int)nudSpace.Value * 1000;
                btnStart.Text = "停止";
                if (rbtnGetTime.Checked)
                    _th = new Thread(DoGetTime);
                else
                    _th = new Thread(DoUploadOrder);
                _th.IsBackground = true;
                _th.Start();
            }
            else
            {
                _isRun = false;
                btnStart.Text = "开始";
                _th.Abort();
                SetText(string.Format("请求次数:{0} 请求成功率:{1}%", _requestCount, _result));
                SetText("------------------------------\r\n");
            }
        }

        private delegate void MyDelegate(string msg);
        private void SetText(string msg)
        {
            if (this.InvokeRequired)
            {
                MyDelegate md = new MyDelegate(SetText);
                this.Invoke(md, msg);
            }
            else
            {
                lblRequestCount.Text = _requestCount.ToString();
                lblResult.Text = _result.ToString() + "%";
                txtLog.Text += msg + "\r\n";
                txtLog.SelectionStart = txtLog.Text.Split(new char[] { '\r', '\n' }).Length * 20;
                txtLog.ScrollToCaret();
            }
        }

        private void DoGetTime()
        {
            try
            {
                _isRun = true;
                bool isReturn = false;
                int successCount = 0;
                while (_isRun)
                {
                    _requestCount++;
                    SetText("正在请求:GetServerTime");
                    string msg = "结果:[result] 耗时:[time]ms";
                    DateTime dtStart = DateTime.Now;
                    ResultModelOfdateTime result = new ResultModelOfdateTime();
                    try
                    {
                        result = _server.GetServerTime();
                        successCount++;
                        msg = msg.Replace("[result]", "成功 Code:" + result.Code);
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("Thread"))
                            isReturn = true;
                        msg = msg.Replace("[result]", ex.Message);
                    }
                    finally
                    {
                        DateTime dtEnd = DateTime.Now;
                        msg = msg.Replace("[time]", (dtEnd - dtStart).TotalMilliseconds.ToString());
                        if (successCount > 0)
                        {
                            _result = successCount / _requestCount * 100;
                            _result = Math.Round(_result, 2);
                        }
                        if (!isReturn)
                            SetText(msg);
                    }
                    Application.DoEvents();
                    Thread.Sleep(_space);
                }
            }
            catch
            {

            }
        }

        private void DoUploadOrder()
        {
            try
            {
                _isRun = true;
                bool isReturn = false;
                int successCount = 0;
                while (_isRun)
                {
                    _requestCount++;
                    SetText("正在请求:UploadWaybill_Base");
                    string msg = "结果:[result] 耗时:[time]ms";
                    List<Model_Waybill_Base> list = new List<Model_Waybill_Base>();
                    list.Add(new Model_Waybill_Base()
                    {
                        BeginAtk__BackingField = DateTime.Now,
                        BillingCountk__BackingField = 1,
                        Companyk__BackingField = Enum_Company.Administrator,
                        Numberk__BackingField = _requestCount.ToString(),
                        SenderIdk__BackingField = 2,
                        SenderOrgk__BackingField = "华东宁波医药",
                        SenderPersonk__BackingField = "管理员",
                        SenderTelk__BackingField = "075400001111",
                        SenderAddressk__BackingField = "北仑区",
                        ReceiverIdk__BackingField = 3,
                        ReceiverOrgk__BackingField = "温州人民医院",
                        ReceiverPersonk__BackingField = "仓库管理员",
                        ReceiverTelk__BackingField = "075422223333",
                        ReceiverAddressk__BackingField = "无",
                        Stagek__BackingField =  Enum_WaybillStage.Transporting

                    });

                    DateTime dtStart = DateTime.Now;
                    ResultModelOfboolean result = new ResultModelOfboolean();
                    try
                    {
                        result = _server.UploadWaybill_Base(list.ToArray());
                        successCount++;
                        msg = msg.Replace("[result]", "成功 Data:"+result.Data);
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("Thread"))
                            isReturn = true;
                        msg = msg.Replace("[result]", ex.Message);
                    }
                    finally
                    {
                        DateTime dtEnd = DateTime.Now;
                        msg = msg.Replace("[time]", (dtEnd - dtStart).TotalMilliseconds.ToString());
                        if (successCount > 0)
                        {
                            _result = successCount / _requestCount * 100;
                            _result = Math.Round(_result, 2);
                        }
                        if (!isReturn)
                            SetText(msg);
                    }
                    Application.DoEvents();
                    Thread.Sleep(_space);
                }

            }
            catch
            {

            }
        }
    }
}