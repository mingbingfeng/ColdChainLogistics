using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//using C2LP.PDA.APP.ScannerAPI;
using System.Runtime.InteropServices;
using C2LP.PDA.Datas.BLL;
using DecodeApi.net;
using C2LP.PDA.Datas.Model;
using System.Collections;
using C2LP.PDA.APP.OrderInput;
using C2LP.PDA.APP.OrderInputForm;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace C2LP.PDA.APP
{
    public partial class FrmParent : Form
    {
        private unsafe bool Init()
        {
            
            //if (!Common.CheckProecess())
            //{
            //    if (MessageBox.Show("点否后退出程序,进入任务管理器查看是否有正在运行的本程序!", "确认您只打开了一次本程序吗?点是则继续运行!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)
            //        == DialogResult.No)
            //        return false;
            //}
            ParentForm = this;
            tmUpload.Enabled = false;//不使用这个计时器开启上报了

            Rectangle rect = new Rectangle();
            FullScreen.SetFullScreen(true, ref rect);//隐藏顶部任务栏
            pnlState.BackColor = Color.DeepSkyBlue;
            lblTime.Visible = true;
            lblTime.Location = new Point((this.Width - lblTime.Width) / 2 + 12, lblTime.Location.Y);
            lblAbout.Location = new Point((this.Width - lblAbout.Width) / 2 + 14, lblAbout.Location.Y);
            lblTime.Text = lblTime.Text = DateTime.Now.ToString("HH:mm");
            try
            {
                //初始化扫码枪
                this.DecodeApi = new DecodeApi.net.CDecodeApi();
                uint dwReturnValue;
                this.DecodeApi.aDecodeGetModuleType(&dwReturnValue);
                uint dwModuleType = dwReturnValue;
                if (dwModuleType == (uint)MODULE_TYPE.DCD_MODULE_TYPE_NONE)
                {
                    this.DecodeApi.Close();
                    Application.Exit();
                }
                this.DecodeApi.aDecodeSetDecodeEnable(1);
                this.DecodeApi.OnScanned += new DecodeApi.net.ScannerEventHandler(this.FormScanTest_OnScanned);
                DecodeApi.aDecodeSetBeepEnable(1);//声音
                DecodeApi.aDecodeSetVibratorEnable(1);//扫描失败时的震动
                DecodeApi.aDecodeSymSetEnableAll(1);


                //this.DecodeApi = new DecodeApi.net.CDecodeApi();
                ////this.DecodeApi.aDecodeSetBeepEnable(1);//开启声音
                ////this.DecodeApi.aDecodeSetVibratorEnable(1);//关闭震动
                ////this.DecodeApi.aDecodeSetDecodeEnable(1);
                //this.DecodeApi.OnScanned += new DecodeApi.net.ScannerEventHandler(this.FormScanTest_OnScanned);
                //初始化照相机
                CamSdk = new PM_CAMSDK();
                CamSdk.CamSetPreviewResolutionType((int)_Image_Type_.RESOLUTION_QVGA);
                CamSdk.CamSetCaptureResolutionType((int)_Image_Type_.RESOLUTION_QVGA);
                CamSdk.CamSetWindowPOS(0, 150, 382, 300);
                CamSdk.CamTargetWindow(this.Handle);
                CamSdk.CamSetCaptureDecodeDelay(1000);
            }
            catch
            {
            }
            return true;
        }


        public unsafe FrmParent()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 主窗体静态实例
        /// </summary>
        public static FrmParent ParentForm = null;
        /// <summary>
        /// 当前子窗体
        /// </summary>
        public UserControl CurrentUc = null;

        public CDecodeApi DecodeApi;

        public PM_CAMSDK CamSdk;

        /// <summary>
        /// 所有区域
        /// </summary>
        public List<MyRegion> _AllRegion = new List<MyRegion>();
        /// <summary>
        /// 所有客户
        /// </summary>
        public List<Customer> _AllCustomer = new List<Customer>();

        public DefaultProvinceInfo _DefaultProvince = new DefaultProvinceInfo();

        public Hashtable _TempScanNum = new Hashtable();
        public Hashtable _TempScanNum_Pic = new Hashtable();
        public void AddScanNum(string number, bool isPic)
        {
            if (isPic)
            {
                if (_TempScanNum_Pic.Count == 500)
                    _TempScanNum_Pic.Clear();
                _TempScanNum_Pic.Add(number, number);
            }
            else
            {
                if (_TempScanNum.Count == 500)
                    _TempScanNum.Clear();
                _TempScanNum.Add(number, number);
            }
        }

        public void CheckNumber(string number, bool isPic)
        {
            if (isPic)
            {
                if (_TempScanNum_Pic.ContainsKey(number))
                    throw new Exception("重复扫描");
            }
            else
            {
                if (_TempScanNum.ContainsKey(number))
                    throw new Exception("重复扫描");
            }
        }

        /// <summary>
        /// 窗体加载时 初始化时钟 并且 打开菜单窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmParent_Load(object sender, EventArgs e)
        {
            if (!Init())
            {
                Application.Exit();
                return;
            }
            LoadLocalData();
            //tmTime_Tick(sender, e);
            tmUpload.Interval = 3000;
            //if (! UCNodeScan._IsCameraActive)
            //    UCNodeScan._IsCameraActive = UnitechDSDll.OpenCamera(pbPreview.Handle, 3, 3, 400, 300, 1024, 768);
            OpenForm(PageState.Main);
            CheckInputPnl(false);
            CustomerServer.AddCustomersCountyId();
            HuadongTmsOrderServer.AddTMSOrderOptAt();
            BaseServer.AddTable_c2lp_optRecord();
            BaseServer.AddTable_c2lp_consignor();
            BaseServer.AddTable_c2lp_storage_scan();
            BaseServer.AddNode_ParentStorageId();
            BaseServer.AddLinkType();
            BaseServer.AddLinkRegex();
            BaseServer.AddConsignorId();
            BaseServer.AddColunmForScanRecord();
            BaseServer.AddColunmForScanRecord1();
            BaseServer.AddColunmForScanRecord2();

        }
        public Hashtable _ht_Sender = new Hashtable();
        public Hashtable _ht_Receiv = new Hashtable();
        public Hashtable _ht_Region = new Hashtable();
        public Hashtable _storage_Scan = new Hashtable();

        public UCCustomerInfo _UCSenderInfo;
        public UCCustomerInfo _UCRecevInfo;

        private delegate void LoadDelegate();
        public void LoadLocalData()
        {
            if (this.InvokeRequired)
            {
                LoadDelegate ld = new LoadDelegate(LoadLocalData);
                this.Invoke(ld);
            }
            else
            {
                CustomerServer.GetCustomerAndRegion(ref _ht_Sender, ref _ht_Receiv, ref _ht_Region);
                FrmParent.ParentForm._AllRegion = RegionServer.GetAllRegion();

                _storage_Scan = StorageServer.GetAllStorageScan();

                _UCSenderInfo = new UCCustomerInfo(true);
                _UCRecevInfo = new UCCustomerInfo(false);
                _UCSenderInfo.Parent = this;
                _UCRecevInfo.Parent = this;
                _UCSenderInfo.Dock = DockStyle.None;
                _UCRecevInfo.Dock = DockStyle.None;
                _UCSenderInfo.Visible = false;
                _UCRecevInfo.Visible = false;
                _UCSenderInfo.Load();
                _UCRecevInfo.Load();
            }
        }

        //public void LoadLocalData()
        //{
        //    try
        //    {
        //        Common.Init();
        //        FrmParent.ParentForm._AllCustomer = CustomerServer.GetAllCustomer(null);
        //        FrmParent.ParentForm._AllRegion = RegionServer.GetAllRegion();

        //        _DefaultProvince = new DefaultProvinceInfo();

        //        _DefaultProvince.ProvinceList = _AllRegion.Where(l => l.ParentId == 1).ToList();
        //        int[] provinceIdList = _AllCustomer.Where(l => l.Role == 2).Select(l => l.ProvinceId).Distinct().ToArray();
        //        _DefaultProvince.HaveSenderProvinceList = (from l in _AllRegion where provinceIdList.Contains(l.Id) select l).ToList();

        //        var v = _AllRegion.Where(l => l.Name == Common._DefaultProvince);
        //        IEnumerable<MyRegion> temp = _AllRegion.Where(l => l.Name == Common._DefaultProvince);
        //        if (temp == null || temp.Count() == 0)
        //            return;

        //        _DefaultProvince.DefaultRangion = temp.First();
        //        IEnumerable<MyRegion> temp2 = _AllRegion.Where(l => l.ParentId == _DefaultProvince.DefaultRangion.Id);
        //        if (temp2 == null || temp2.Count() == 0)
        //            return;
        //        List<MyRegion> r2 = temp2.ToList();
        //        foreach (MyRegion item_r2 in r2)
        //        {
        //            IEnumerable<MyRegion> temp3 = _AllRegion.Where(l => l.ParentId == item_r2.Id);
        //            if (temp3 == null || temp3.Count() == 0)
        //                continue;
        //            List<MyRegion> r3 = temp3.ToList();
        //            if (_DefaultProvince.DefaultChildRangion.ContainsKey(item_r2))
        //                _DefaultProvince.DefaultChildRangion[item_r2] = r3;
        //            else
        //                _DefaultProvince.DefaultChildRangion.Add(item_r2, r3);

        //            IEnumerable<Customer> temp4 = _AllCustomer.Where(l => l.CityId == item_r2.Id && l.Role == 2);
        //            if (temp4 == null || temp4.Count() == 0)
        //                continue;
        //            List<Customer> r2_sender = temp4.ToList();
        //            if (_DefaultProvince.RangionSenderCustomer.ContainsKey(item_r2))
        //                _DefaultProvince.RangionSenderCustomer[item_r2] = r2_sender;
        //            else
        //                _DefaultProvince.RangionSenderCustomer.Add(item_r2, r2_sender);

        //            IEnumerable<Customer> temp5 = _AllCustomer.Where(l => l.CityId == item_r2.Id && l.Role == 3);
        //            if (temp5 == null || temp5.Count() == 0)
        //                continue;
        //            List<Customer> r2_receive = temp5.ToList();
        //            if (_DefaultProvince.RangionReceiveCustomer.ContainsKey(item_r2))
        //                _DefaultProvince.RangionReceiveCustomer[item_r2] = r2_receive;
        //            else
        //                _DefaultProvince.RangionReceiveCustomer.Add(item_r2, r2_receive);
        //        }
        //    }
        //    catch
        //    {

        //    }

        //}

        public bool _isSelectLinkType2 = false;

        public unsafe void FormScanTest_OnScanned(string strScanData, uint nLen, uint barType, int errCode)
        {
            _WaitNum = 0;
            if (CurrentUc == null)
                return;
            string data = string.Empty;
            if (errCode == 0)
            {
                data = strScanData;
                //如果选择了LinkType=2的供应商(华东医药宁波公司)，就自动去掉运单号前面的0
                if (_isSelectLinkType2)
                {
                    string tempNumber = data;
                    for (int i = 0; i < data.Length; i++)
                    {
                        string c = data[i].ToString();
                        if (c == "0")
                            tempNumber = tempNumber.Remove(0, 1);
                        else
                            break;
                    }
                    data = tempNumber;
                }
                if (CurrentUc is UCNodeScan)
                    ((UCNodeScan)CurrentUc).SetNumber(data);
                else if (CurrentUc is UCThirdParty)
                    ((UCThirdParty)CurrentUc).SetNumber(data);
                //else if (CurrentUc is UCOrderInput)
                //    ((UCOrderInput)CurrentUc).SetNumber(data);
                else if (CurrentUc is UCOrderInput1)
                    ((UCOrderInput1)CurrentUc).SetNumber(data);
                else if (CurrentUc is UCCenterNodeScan)
                    ((UCCenterNodeScan)CurrentUc).SetNumber(data);
            }
            //else
            //{
            //    DecodeApi.aDecodeSymSetEnableAll(1);
            //    MessageBox.Show("扫描失败,请重试,若还不行,请重启机器!");
            //}
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
                //btnInput.Visible = !show;
            }
        }

        ///// <summary>
        ///// 显示/隐藏输入法
        ///// </summary>
        ///// <param name="show">True:显示；False:隐藏</param>
        //public void CheckInputPnl_Button(bool show)
        //{
        //    if (this.InvokeRequired)
        //    {
        //        CheckInputPnlDelegate cipd = new CheckInputPnlDelegate(CheckInputPnl);
        //        this.Invoke(cipd, show);
        //    }
        //    else
        //    {
        //        if (inputPnl.Enabled)
        //            inputPnl.Enabled = false;
        //        else
        //            inputPnl.Enabled = true;
        //        //inputPnl.Enabled = show;
        //    }
        //}

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
                if ((ps != PageState.Sync && ps != PageState.SetNumber && ps != PageState.UploadConfig) && DateTime.Now < DateTime.Parse("2017-01-01"))
                {
                    MessageBox.Show("系统时间异常,请通过[信息同步]界面同步服务器时间!", "请同步系统时间", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                    if (ps != PageState.Main)
                        ps = PageState.Main;
                }
                _TempScanNum.Clear();
                _TempScanNum_Pic.Clear();
                _WaitNum = 0;
                CheckAboutInfo(true);
                bool iscontinueUpload = true;//是否跳出后台上报线程
                int level = 1;
                switch (ps)
                {
                    case PageState.Main:
                        iscontinueUpload = false;
                        CurrentUc = new UCMain();
                        //if (UploadHelp._isSleep)
                        //    FrmParent.ParentForm.SetNewInfo("无上报数据,等待进入休眠...", false);
                        break;
                    case PageState.Sync:
                        iscontinueUpload = false;
                        CurrentUc = new UCSyncAll();
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
                        CurrentUc = new UCOrderInput1();
                        //CurrentUc = new UCOrderInput();
                        break;
                    case PageState.NodeScan:
                        CurrentUc = new UCNodeScan();
                        CheckAboutInfo(false);
                        break;
                    case PageState.UploadConfig:
                        iscontinueUpload = false;
                        CurrentUc = new UCUploadConfig();
                        break;
                    case PageState.ThirdParty:
                        CurrentUc = new UCThirdParty();
                        CheckAboutInfo(false);
                        break;
                    case PageState.WaitUploadNode:
                        iscontinueUpload = false;
                        CurrentUc = new UCNodeList();
                        break;
                    case PageState.CenterNode:
                        CurrentUc = new UCCenterNodeScan();
                        CheckAboutInfo(false);
                        break;
                    case PageState.ScanRecord:
                        CurrentUc = new UCScanRecord();
                        break;
                    case PageState.SearchCustomer:
                        CurrentUc = new UCSearchCustomer();
                        level = 2;
                        break;
                }
                UploadHelp._isContinue = iscontinueUpload;
                CurrentUc.Dock = DockStyle.Fill;
                CurrentUc.BringToFront();
                if (pnlMain.Controls.Count == 1)
                {
                    if (level == 2)
                        pnlMain.Controls[0].Visible = false;
                    else
                        pnlMain.Controls[0].Dispose();
                    //pnlMain.Controls.Remove(pnlMain.Controls[0]);
                }
                pnlMain.Controls.Add(CurrentUc);
                pnlMain.Tag = ps;
                CheckInputPnl(false);
            }
            return CurrentUc;
        }

        public void AddSaveOKNumber(int count)
        {
            lblAbout.Text = "共成功保存[" + count + "]单";
            Application.DoEvents();
        }

        private void CheckAboutInfo(bool isAbout)
        {
            if (isAbout)
            {
                lblAbout.Text = "上海思博源冷链科技有限公司\r\n   Copyright © 2016-2020";
                lblAbout.Font = new Font("Tahoma", 8, FontStyle.Regular);
            }
            else
            {
                lblAbout.Text = "共成功保存[0]单";
                lblAbout.Font = new Font("Tahoma", 14, FontStyle.Regular);
            }
        }

        public delegate void InputPnlChangeDelegate(bool isShow);
        public static event InputPnlChangeDelegate IputChangeEvent;
        private void inputPnl_EnabledChanged(object sender, EventArgs e)
        {
            if (IputChangeEvent != null)
                IputChangeEvent(inputPnl.Enabled);
        }


        /// <summary>
        /// 计时器定期启动上报，不使用此计时器了
        /// </summary>
        private void tmUpload_Tick(object sender, EventArgs e)
        {
            //等待主页面加载完参数
            if (Common._IsMainFormFirstShow)
                return;
            UploadHelp.StartUpload();
            //tmUpload.Enabled = false;
            //try
            //{
            //    if (!UploadHelp._isSleep)
            //        UploadHelp.StartUpload();
            //    //ThirdPartyHelp.StartUpload();
            //}
            //catch
            //{

            //}
            //finally
            //{
            //    if (!UploadHelp._isSleep)
            //        tmUpload.Enabled = true;
            //    else
            //        FrmParent.ParentForm.SetNewInfo("无上报数据,等待进入休眠...", false);
            //}
        }
        //去掉检测到不存在数据时进入休眠的功能，不使用此方法了
        public void EndSleep()
        {
            //if (UploadHelp._isSleep)
            //    UploadHelp._isSleep = false;
            //if (!tmUpload.Enabled)
            //    tmUpload.Enabled = true;
        }

        public void ResetReturnDelay()
        {
            _WaitNum = 0;
        }

        public int _WaitNum = 0;
        private void tmTime_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (Common._IsMainFormFirstShow)
                return;
            UploadHelp.StartUpload();

            if (UploadHelp._isContinue)
            {
                if (_WaitNum >= Common._AutoReturnDelay)
                {
                    foreach (Control item in pnlMain.Controls)
                    {
                        item.Dispose();
                    }
                    OpenForm(PageState.Main);
                }
                else
                {
                    _WaitNum++;
                    lblNumber.Text = (Common._AutoReturnDelay - _WaitNum).ToString();
                    if (!lblNumber.Visible)
                        lblNumber.Visible = true;
                }
            }
            else if (lblNumber.Visible)
                lblNumber.Visible = false;
        }

        public delegate void SetNewInfoDelegate(string msg, bool? isSuccess);
        /// <summary>
        /// 更新上报状态
        /// </summary>
        /// <param name="isSuccess"></param>
        /// <param name="msg"></param>
        public void SetNewInfo(string msg, bool? isSuccess)
        {
            if ((CurrentUc is UCMain) == false)
                return;
            if (this.InvokeRequired)
            {
                SetNewInfoDelegate d = new SetNewInfoDelegate(SetNewInfo);
                this.Invoke(d, msg, isSuccess);
            }
            else
            {
                (CurrentUc as UCMain).SetSyncInfo(msg);
                //if (isSuccess != null)
                //{
                //    if ((bool)isSuccess)
                //        pnlNews.BackColor = Color.Lime;
                //    else
                //        pnlNews.BackColor = Color.Red;
                //}
                //lblNews.Text = string.Format("[{0}]{1}", DateTime.Now.ToString("HH:mm:ss"), msg);
                ////if (lblNews.Text.Length > 35)
                ////{
                ////    if (lblNews.Height == 18)
                ////    {
                ////        pnlAbout.Height += 18;
                ////        lblAbout.Top += 18;
                ////        lblNews.Height += 18;
                ////    }
                ////}
                ////else if (lblNews.Height > 18)
                ////{
                ////    pnlAbout.Height -= 18;
                ////    lblAbout.Top -= 18;
                ////    lblNews.Height -= 18;
                ////}
            }
        }

        private void FrmParent_Closed(object sender, EventArgs e)
        {
            //Rectangle rect = new Rectangle();
            //FullScreen.SetFullScreen(false, ref rect);
            ////Scanner.GetScanner().Close();
            ////Scanner.GetScanner().Unregister();
            ////UnitechDSDll.CloseCamera();
            //SyncHelp.StopSync();
            //UploadHelp.StopUpload();

            //Application.DoEvents();
            //Common.WriteIsClose();
            FrmParent.ParentForm.DecodeApi.aDecodeSetDecodeEnable(0);
            this.DecodeApi.Close();

            //this.CamSdk.Close();
            //MessageBox.Show("释放资源 FrmParent");
        }

        //private void btnInput_Click(object sender, EventArgs e)
        //{
        //    CheckInputPnl_Button(true);
        //}
        private string tempStr = string.Empty;
        public void ShowTempInput(string title, string defaultValue, bool isPwdTextBox)
        {
            //Rectangle rect = new Rectangle();
            //FullScreen.SetFullScreen(true, ref rect);//隐藏顶部任务栏
            CheckInputPnl(true);
            lblTitle.Text = title;
            txtTempInput.Focus();
            tempStr = defaultValue;
            txtTempInput.Text = defaultValue;
            txtTempInput.SelectAll();
            pnlTempInput.Visible = true;
            pnlState.Visible = true;
            if (isPwdTextBox)
                txtTempInput.PasswordChar = '*';
            else
                txtTempInput.PasswordChar = new char();
        }

        //private void btnDelete_Click(object sender, EventArgs e)
        //{
        //    if (txtTempInput.SelectionLength > 0)
        //       txtTempInput.Text= txtTempInput.Text.Remove(txtTempInput.SelectionStart, txtTempInput.SelectionLength);
        //    else if(txtTempInput.Text.Length>0)
        //       txtTempInput.Text= txtTempInput.Text.Remove(txtTempInput.Text.Length - 1, 1);
        //    txtTempInput.Focus();
        //}
        public event EventHandler TempInputEvent;

        private void btnOK_Click(object sender, EventArgs e)
        {
            ResetReturnDelay();
            if (TempInputEvent != null)
                TempInputEvent(txtTempInput.Text.Trim(), null);
            CheckInputPnl(false);
            pnlTempInput.Visible = false;
            //pnlState.Visible = false;
        }

        public string GetTempInputStr()
        {
            return txtTempInput.Text.Trim();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            ResetReturnDelay();
            if (TempInputEvent != null)
                TempInputEvent(tempStr, null);
            CheckInputPnl(false);
            pnlTempInput.Visible = false;
            //pnlState.Visible = false;
            //Rectangle rect = new Rectangle();
            //FullScreen.SetFullScreen(false, ref rect);//显示顶部任务栏
        }

        private void pnlAbout_Click(object sender, EventArgs e)
        {
            ResetReturnDelay();
        }

        private void txtTempInput_TextChanged(object sender, EventArgs e)
        {
            ResetReturnDelay();
        }

        private void pnlTempInput_Click(object sender, EventArgs e)
        {
            ResetReturnDelay();
        }

        private void pnlTempInput_LostFocus(object sender, EventArgs e)
        {
            ResetReturnDelay();
        }

        private void pnlAbout_LostFocus(object sender, EventArgs e)
        {
            ResetReturnDelay();
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
        /// 待上报节点
        /// </summary>
        WaitUploadNode,
        /// <summary>
        /// 中间节点扫描
        /// </summary>
        CenterNode,
        /// <summary>
        /// 扫描记录
        /// </summary>
        ScanRecord,
        /// <summary>
        /// 搜索单位信息 
        /// </summary>
        SearchCustomer
    }
}
