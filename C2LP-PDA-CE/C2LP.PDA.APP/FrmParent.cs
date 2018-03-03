using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using C2LP.PDA.APP.ScannerAPI;
using System.Runtime.InteropServices;
using System.Threading;
using C2LP.PDA.Datas.BLL;
using System.Collections;
using C2LP.PDA.APP.OrderInput;

namespace C2LP.PDA.APP
{
    public partial class FrmParent : Form
    {
        public FrmParent()
        {
            InitializeComponent();
            Rectangle rect = new Rectangle();
            FullScreen.SetFullScreen(true, ref rect);//隐藏顶部任务栏
            pnlState.BackColor = Color.DeepSkyBlue;
            lblTime.Visible = true;
            lblTime.Location = new Point((this.Width - lblTime.Width) / 2 + 10, lblTime.Location.Y);
            lblAbout.Location = new Point((this.Width - lblAbout.Width) / 2 + 8, lblAbout.Location.Y);
            lblTime.Text = lblTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            ParentForm = this;
        }
        /// <summary>
        /// 主窗体静态实例
        /// </summary>
        public static FrmParent ParentForm = null;
        /// <summary>
        /// 当前子窗体
        /// </summary>
        public UserControl CurrentUc = null;

        public Hashtable _ht_Sender = new Hashtable();
        public Hashtable _ht_Receiv = new Hashtable();
        public Hashtable _ht_Region = new Hashtable();

        /// <summary>
        /// 窗体加载时 初始化时钟 并且 打开菜单窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmParent_Load(object sender, EventArgs e)
        {
            //tmTime_Tick(sender, e);
            tmUpload.Interval = 3000;
            //if (!UCNodeScan._IsCameraActive)
            //    UCNodeScan._IsCameraActive = UnitechDSDll.OpenCamera(pbPreview.Handle, 3, 3, 400, 300, 1024, 768);
            OpenForm(PageState.Main);
            CheckInputPnl(false);
            CustomerServer.AddCustomersCountyId();
            //CustomerServer.GetCustomerAndRegion(ref _ht_Sender, ref _ht_Receiv, ref _ht_Region);
            //UCSenderInfo.Load();
            //UCRecevInfo.Load();
            //UCSenderInfo.Visible = true;
            //UCRecevInfo.Visible = true;
        }


        public delegate void CheckInputPnlDelegate(bool show);

        /// <summary>
        /// 显示/隐藏输入法
        /// </summary>
        /// <param name="show">True:显示；False:隐藏</param>
        public void CheckInputPnl(bool show)
        {
            if (this.InvokeRequired)
            {
                CheckInputPnlDelegate cipd = new CheckInputPnlDelegate(CheckInputPnl);
                this.Invoke(cipd, show);
            }
            else
            {
                inputPnl.Enabled = show;
            }
        }



        public delegate UserControl OpenFormDelegate(PageState ps);
        /// <summary>
        /// 打开子窗体
        /// </summary>
        /// <param name="ps"></param>
        /// <returns></returns>
        public UserControl OpenForm(PageState ps)
        {
            if (this.InvokeRequired)
            {
                OpenFormDelegate ofd = new OpenFormDelegate(OpenForm);
                this.Invoke(ofd, ps);
            }
            else
            {
                if (ps != PageState.Sync && DateTime.Now < DateTime.Parse("2017-01-01"))
                {
                    MessageBox.Show("系统时间异常,请通过[信息同步]界面同步服务器时间!", "请同步系统时间", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                    if (ps != PageState.Main)
                        ps = PageState.Main;
                }
                this.TopMost = false;
                switch (ps)
                {
                    case PageState.Main:
                        CurrentUc = new UCMain();
                        break;
                    case PageState.Sync:
                        CurrentUc = new UCSyncAll();
                        Rectangle rect = new Rectangle();
                        FullScreen.SetFullScreen(true, ref rect);//隐藏顶部任务栏
                        break;
                    case PageState.SetNumber:
                        CurrentUc = new UCSetNumber(Common._PDANumber);
                        break;
                    case PageState.SetStorage:
                        CurrentUc = new UCSetStorage();
                        break;
                    case PageState.SetDestin:
                        CurrentUc = new UCSetDestin();
                        break;
                    case PageState.OrderInout:
                        CurrentUc = new UCOrderInput();
                        break;
                    case PageState.NodeScan:
                        CurrentUc = new UCNodeScan();
                        break;
                    case PageState.UploadConfig:
                        CurrentUc = new UCUploadConfig();
                        break;
                    case PageState.ThirdParty:
                        CurrentUc = new UCThirdParty();
                        break;
                    case PageState.Connect:
                        CurrentUc = new UCConnect();
                        break;
                    case PageState.WaitUploadNode:
                        CurrentUc = new UCNodeList();
                        break;
                }
                CurrentUc.Dock = DockStyle.Fill;
                if (pnlMain.Controls.Count == 1)
                {
                    pnlMain.Controls[0].Dispose();
                    //pnlMain.Controls.Remove(pnlMain.Controls[0]);
                }
                pnlMain.Controls.Add(CurrentUc);
                pnlMain.Tag = ps;
                CheckInputPnl(false);
            }
            ConnectHelp.wcdma_procwnd = new HT380_WCDMA_PROCWND(CurrentUc);
            return CurrentUc;
        }

        public delegate void InputPnlChangeDelegate(bool isShow);
        public static event InputPnlChangeDelegate IputChangeEvent;
        private void inputPnl_EnabledChanged(object sender, EventArgs e)
        {
            if (IputChangeEvent != null)
                IputChangeEvent(inputPnl.Enabled);
        }



        /// <summary>
        /// 计时器定期启动上报
        /// </summary>
        private void tmUpload_Tick(object sender, EventArgs e)
        {
            //等待主页面加载完参数
            if (Common._IsMainFormFirstShow)
                return;
            tmUpload.Enabled = false;
            try
            {
                UploadHelp.StartUpload();
                //ThirdPartyHelp.StartUpload();
            }
            catch
            {

            }
            finally
            {
                tmUpload.Enabled = true;
            }
        }

        private void tmTime_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void FrmParent_Closing(object sender, CancelEventArgs e)
        {
            Rectangle rect = new Rectangle();
            FullScreen.SetFullScreen(false, ref rect);
            //Scanner.GetScanner().Close();
            //Scanner.GetScanner().Unregister();
            //UnitechDSDll.CloseCamera();
            SyncHelp.StopSync();
            UploadHelp.StopUpload();
        }

        public delegate void SetNewInfoDelegate(string msg, bool? isSuccess);
        /// <summary>
        /// 更新上报状态
        /// </summary>
        /// <param name="isSuccess"></param>
        /// <param name="msg"></param>
        public void SetNewInfo(string msg, bool? isSuccess)
        {
            if (this.InvokeRequired)
            {
                SetNewInfoDelegate d = new SetNewInfoDelegate(SetNewInfo);
                this.Invoke(d, msg, isSuccess);
            }
            else
            {
                if (isSuccess != null)
                {
                    if ((bool)isSuccess)
                        pnlNews.BackColor = Color.Lime;
                    else
                        pnlNews.BackColor = Color.Red;
                }
                lblNews.Text = string.Format("[{0}]{1}", DateTime.Now.ToString("HH:mm:ss"), msg);
                if (lblNews.Text.Length > 35)
                {
                    if (lblNews.Height == 18)
                    {
                        pnlAbout.Height += 18;
                        lblAbout.Top += 18;
                        lblNews.Height += 18;
                    }
                }
                else if (lblNews.Height > 18)
                {
                    pnlAbout.Height -= 18;
                    lblAbout.Top -= 18;
                    lblNews.Height -= 18;
                }
            }
        }

        public delegate void VirtualConsoleDelegate(bool clearBz, string s, int ii);

        public void VirtualConsole(bool clearBz, string s, int ii)
        {
            if (this.InvokeRequired)
            {
                VirtualConsoleDelegate d = new VirtualConsoleDelegate(VirtualConsole);
                this.Invoke(d, clearBz, s, ii);
            }
            else
            {
                SetNewInfo(s, false);
            }
        }

        /// <summary>
        /// 后台进行自动重连
        /// </summary>
        public void ReConnect()
        {
            try
            {
                if (ConnectHelp.isConencting)
                    return;
                ConnectHelp.isConencting = true;
                SetNewInfo("网络异常,正在重连中,请稍候...", false);
                //ConnectHelp.wcdma_procwnd = new HT380_WCDMA_PROCWND(this);
                ConnectHelp.G3_UnConnect();
                ConnectHelp.Connnet();
            }
            catch (Exception ex)
            {
                SetNewInfo(ex.Message, false);
            }
        }
    }

    public enum PageState
    {
        /// <summary>
        /// 主界面菜单
        /// </summary>
        Main,
        /// <summary>
        /// 同步界面
        /// </summary>
        Sync,
        /// <summary>
        /// 设置编号
        /// </summary>
        SetNumber,
        /// <summary>
        /// 设置冷藏载体
        /// </summary>
        SetStorage,
        /// <summary>
        /// 设置目的地
        /// </summary>
        SetDestin,
        /// <summary>
        /// 运单录入
        /// </summary>
        OrderInout,
        /// <summary>
        /// 节点扫描
        /// </summary>
        NodeScan,
        /// <summary>
        /// 上报配置
        /// </summary>
        UploadConfig,
        /// <summary>
        /// 第三方
        /// </summary>
        ThirdParty,
        /// <summary>
        /// 拨号连接
        /// </summary>
        Connect,
        /// <summary>
        /// 待上报节点
        /// </summary>
        WaitUploadNode
    }
}
