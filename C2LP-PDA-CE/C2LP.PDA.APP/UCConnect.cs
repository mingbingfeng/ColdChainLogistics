using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;                 //调用Sleep函数，必须引入这个命名空间
using System.Runtime.InteropServices;   //调用动态库，必须引入这个命名空间
using Microsoft.WindowsCE.Forms;
using System.Net;
using System.Net.Sockets;        //相应Windows消息，必须引入这个命名空间

namespace C2LP.PDA.APP
{
    public partial class UCConnect : UserControl
    {
        public UCConnect()
        {
            InitializeComponent();
            //lbGprs.GotFocus+=new EventHandler(lbGprs_GotFocus);
            //lbGprs.Focus();
        }
        //[DllImport("CE380_WCDMA.dll", CharSet = CharSet.Auto, EntryPoint = "WCDMA_Connect")]
        //public static extern int WCDMA_Connect(long hwnd);
        //[DllImport("CE380_WCDMA.dll", CharSet = CharSet.Auto, EntryPoint = "WCDMA_UnConnect")]
        //public static extern void WCDMA_UnConnect();
        //[DllImport("CE380_WCDMA.dll", CharSet = CharSet.Auto, EntryPoint = "WCDMA_ReadCSQ")]
        //public static extern uint WCDMA_ReadCSQ();

        //[DllImport("CE380_CDMA2000.dll", CharSet = CharSet.Auto, EntryPoint = "CDMA2000_Connect")]
        //public static extern int CDMA2000_Connect(long hwnd);
        //[DllImport("CE380_CDMA2000.dll", CharSet = CharSet.Auto, EntryPoint = "CDMA2000_UnConnect")]
        //public static extern void CDMA2000_UnConnect();
        //[DllImport("CE380_CDMA2000.dll", CharSet = CharSet.Auto, EntryPoint = "CDMA2000_ReadCSQ")]
        //public static extern uint CDMA2000_ReadCSQ();

        //#region Memory Management
        //[DllImport("coredll")]
        //extern public static IntPtr LocalAlloc(int flags, int size);
        //[DllImport("coredll")]
        //extern public static IntPtr LocalFree(IntPtr pMem);

        //const int LMEM_ZEROINIT = 0x40;

        //#endregion


        //#region IPHLPAPI P/Invokes
        //[DllImport("iphlpapi")]
        //extern public static IntPtr IcmpCreateFile();

        //[DllImport("iphlpapi")]
        //extern public static bool IcmpCloseHandle(IntPtr h);

        //[DllImport("iphlpapi")]
        //extern public static uint IcmpSendEcho(
        //                 IntPtr IcmpHandle,
        //                 uint DestinationAddress,
        //                 byte[] RequestData,
        //                 short RequestSize,
        //                 IntPtr /*IP_OPTION_INFORMATION*/ RequestOptions,
        //                 byte[] ReplyBuffer,
        //                 int ReplySize,
        //                 int Timeout);

        //#endregion

        //[DllImport("coredll")]
        //extern static int GetLastError();

        //private HT380_WCDMA_PROCWND wcdma_procwnd;
        public int i_type
        {
            get
            {
                return GetIType();
            }
        }

        public delegate int GetITypeDelegate();
        private int GetIType()
        {
            if (this.InvokeRequired)
            {
                GetITypeDelegate d = new GetITypeDelegate(GetIType);
                return (int)this.Invoke(d);
            }
            else
            {
                return rbtnWCDMA.Checked ? 1 : 2;
            }
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {
            FrmParent.ParentForm.OpenForm(PageState.Main);
        }



        //private void lbGprs_GotFocus(object sender, EventArgs e)
        //{
        //    lbGprs.GotFocus -= new EventHandler(lbGprs_GotFocus);
        //    EnableNoButton();
        //    //Thread th = new Thread(DoTestPing);
        //    //th.IsBackground = true;
        //    //th.Start();

        //}

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                EnableNoButton();
                if (ConnectHelp.isConencting)
                {
                    VirtualConsole(false, "后台正在自动重连中,无需手动拨号", 0);
                    return;
                }
                ConnectHelp.i_type = i_type;
                ConnectHelp.wcdma_procwnd = new HT380_WCDMA_PROCWND(this);
                VirtualConsole(true, "正在下电...请稍候！", 0);
                ConnectHelp.G3_UnConnect();
                //开启GPRS模块
                VirtualConsole(false, "正在上电及拨号，请稍候...", 0);
                int ret = ConnectHelp.Connnet();
                //if (ret == -4)
                //{
                //    VirtualConsole(false, "串口通讯失败", 0);
                //    EnableNoButton();
                //}
                //else if (ret == 1)
                //{
                //    VirtualConsole(false, "已连接到网络", 0);
                //}
            }
            catch (Exception ex)
            {
                VirtualConsole(false, ex.Message, 0);
            }
            finally
            {
                EnableYesButton();
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
                if (!this.Visible)
                    return;
                if (clearBz)
                    lbGprs.Items.Clear();

                lbGprs.Items.Add(s);
                lbGprs.Refresh();
            }
        }

        public delegate void ButtonEnableDelegate();
        public void EnableYesButton()
        {
            if (this.InvokeRequired)
            {
                ButtonEnableDelegate d = new ButtonEnableDelegate(EnableYesButton);
                this.Invoke(d);
            }
            else
            {
                btnConnect.Enabled = true;
                btnCancel.Enabled = true;
                btnScan.Enabled = true;
            }
        }

        public void EnableNoButton()
        {
            if (this.InvokeRequired)
            {
                ButtonEnableDelegate d = new ButtonEnableDelegate(EnableNoButton);
                this.Invoke(d);
            }
            else
            {
                btnConnect.Enabled = false;
                btnCancel.Enabled = false;
                btnScan.Enabled = false;
            }
        }

        public delegate void SetSignDelegate(int sign);
        public void SetSign(int sign)
        {
            if (this.InvokeRequired)
            {
                SetSignDelegate d = new SetSignDelegate(SetSign);
                this.Invoke(d, sign);
            }
            else
            {
                string txt = "无[0]";
                if (sign > 0 && sign <= 10)
                    txt = "弱[" + sign.ToString() + "]";
                else if (sign > 10 && sign <= 20)
                    txt = "中[" + sign.ToString() + "]";
                else
                    txt = "强[" + sign.ToString() + "]";
                lblSign.Text = txt;
            }
        }

        private void btnScan_Click(object sender, EventArgs e)
        {
            EnableNoButton();
            try
            {
                uint sign = ConnectHelp.G3_ReadCSQ();
                SetSign((int)sign);
            }
            catch (Exception ex)
            {
                VirtualConsole(false, ex.Message, 0);
            }
            finally
            {
                EnableYesButton();
            }
        }

    }


    //-----------------------------------------------------------------------------------------------------------------------------------------

    //public class ICMP_ECHO_REPLY
    //{
    //    public ICMP_ECHO_REPLY(int size) { data = new byte[size]; }
    //    byte[] data;
    //    public byte[] _Data { get { return data; } }
    //    public int Address { get { return BitConverter.ToInt32(data, 0); } }
    //    public int Status { get { return BitConverter.ToInt32(data, 4); } }
    //    public int RoundTripTime { get { return BitConverter.ToInt32(data, 8); } }
    //    public short DataSize { get { return BitConverter.ToInt16(data, 0xc); } set { BitConverter.GetBytes(value).CopyTo(data, 0xc); } }
    //    public IntPtr Data { get { return new IntPtr(BitConverter.ToInt32(data, 0x10)); } set { BitConverter.GetBytes(value.ToInt32()).CopyTo(data, 0x10); } }
    //    public byte Ttl { get { return data[0x14]; } }
    //    public byte Tos { get { return data[0x15]; } }
    //    public byte Flags { get { return data[0x16]; } }
    //    public byte OptionsSize { get { return data[0x17]; } }
    //    public IntPtr OptionsData { get { return new IntPtr(BitConverter.ToInt32(data, 0x18)); } set { BitConverter.GetBytes(value.ToInt32()).CopyTo(data, 0x18); } }
    //}

    //public class HT380_WCDMA_PROCWND : MessageWindow
    //{
    //    private UserControl msgForm = null;

    //    public HT380_WCDMA_PROCWND(UserControl form)
    //    {
    //        this.msgForm = form;
    //    }

    //    const int WM_RASDIALEVENT = 0xCCCD;
    //    const int RASCS_PAUSED = 0x1000;
    //    const int RASCS_DONE = 0x2000;

    //    enum tagRASCONNSTATE
    //    {
    //        RASCS_OpenPort = 0,
    //        RASCS_PortOpened,
    //        RASCS_ConnectDevice,
    //        RASCS_DeviceConnected,
    //        RASCS_AllDevicesConnected,
    //        RASCS_Authenticate,
    //        RASCS_AuthNotify,
    //        RASCS_AuthRetry,
    //        RASCS_AuthCallback,
    //        RASCS_AuthChangePassword,
    //        RASCS_AuthProject,
    //        RASCS_AuthLinkSpeed,
    //        RASCS_AuthAck,
    //        RASCS_ReAuthenticate,
    //        RASCS_Authenticated,
    //        RASCS_PrepareForCallback,
    //        RASCS_WaitForModemReset,
    //        RASCS_WaitForCallback,
    //        RASCS_Projected,

    //        RASCS_Interactive = RASCS_PAUSED,
    //        RASCS_RetryAuthentication,
    //        RASCS_CallbackSetByCaller,
    //        RASCS_PasswordExpired,

    //        RASCS_Connected = RASCS_DONE,
    //        RASCS_Disconnected
    //    };

    //    protected override void WndProc(ref Microsoft.WindowsCE.Forms.Message m)
    //    {
    //        if (m.Msg == WM_RASDIALEVENT)
    //        {
    //            switch ((tagRASCONNSTATE)m.WParam)
    //            {
    //                case tagRASCONNSTATE.RASCS_OpenPort:
    //                    break;
    //                case tagRASCONNSTATE.RASCS_PortOpened:
    //                    VirtualConsole(false, "端口已打开", 0);
    //                    break;
    //                case tagRASCONNSTATE.RASCS_ConnectDevice:
    //                    VirtualConsole(false, "正在连接设备", 0);
    //                    break;
    //                case tagRASCONNSTATE.RASCS_DeviceConnected:
    //                    VirtualConsole(false, "设备已连接", 0);
    //                    break;
    //                case tagRASCONNSTATE.RASCS_AllDevicesConnected:
    //                    VirtualConsole(false, "物理连接已建立", 0);
    //                    break;
    //                case tagRASCONNSTATE.RASCS_Authenticate:
    //                    VirtualConsole(false, "正在验证...", 0);
    //                    break;
    //                case tagRASCONNSTATE.RASCS_AuthNotify:
    //                    VirtualConsole(false, "验证出错", 0);
    //                    break;
    //                case tagRASCONNSTATE.RASCS_AuthRetry:
    //                    VirtualConsole(false, "正在重新验证...", 0);
    //                    break;
    //                case tagRASCONNSTATE.RASCS_AuthCallback:
    //                    break;
    //                case tagRASCONNSTATE.RASCS_AuthChangePassword:
    //                    break;
    //                case tagRASCONNSTATE.RASCS_AuthProject:
    //                    break;
    //                case tagRASCONNSTATE.RASCS_AuthLinkSpeed:
    //                    break;
    //                case tagRASCONNSTATE.RASCS_AuthAck:
    //                    break;
    //                case tagRASCONNSTATE.RASCS_ReAuthenticate:
    //                    VirtualConsole(false, "正在重新验证...", 0);
    //                    break;
    //                case tagRASCONNSTATE.RASCS_Authenticated:
    //                    VirtualConsole(false, "验证已通过", 0);
    //                    break;
    //                case tagRASCONNSTATE.RASCS_PrepareForCallback:
    //                    break;
    //                case tagRASCONNSTATE.RASCS_WaitForModemReset:
    //                    break;
    //                case tagRASCONNSTATE.RASCS_WaitForCallback:
    //                    break;
    //                case tagRASCONNSTATE.RASCS_Projected:
    //                    break;
    //                case tagRASCONNSTATE.RASCS_Interactive:
    //                    break;
    //                case tagRASCONNSTATE.RASCS_RetryAuthentication:
    //                    break;
    //                case tagRASCONNSTATE.RASCS_CallbackSetByCaller:
    //                    break;
    //                case tagRASCONNSTATE.RASCS_PasswordExpired:
    //                    break;
    //                case tagRASCONNSTATE.RASCS_Connected:
    //                    //g_strgprs = _T("G");
    //                    VirtualConsole(false, "网络已连接", 0);
    //                    uint sign = (msgForm as UCConnect).G3_ReadCSQ();
    //                    string stemp = "信号强度：" + sign.ToString();
    //                    (msgForm as UCConnect).SetSign((int)sign);
    //                    VirtualConsole(false, stemp, 0);
    //                    VirtualConsole(false, "--------------------------------------------", 0);
    //                    VirtualConsole(false, "连接完毕！成功", 0);
    //                    (msgForm as UCConnect).EnableYesButton();
    //                    //Sleep(2000);
    //                    //gprs_finsh = true ;
    //                    //m_pLoginDlg->Title_bottom("拨号已完成");
    //                    break;
    //                case tagRASCONNSTATE.RASCS_Disconnected:
    //                    //g_strgprs = _T("");
    //                    VirtualConsole(false, "网络已断开", 0);
    //                    VirtualConsole(false, "--------------------------------------------", 0);
    //                    VirtualConsole(false, "测试完毕！失败", 0);
    //                    (msgForm as UCConnect).EnableYesButton();
    //                    //(msgForm as UCConnect).G3_UnConnect();
    //                    //(msgForm as UCConnect).EnableNoButton();
    //                    //m_pLoginDlg->Title_bottom("拨号已完成");
    //                    //gprs_finsh = true ;
    //                    break;
    //                default:
    //                    base.WndProc(ref m);//一定要调用基类函数，以便系统处理其它消息。
    //                    break;
    //            }
    //        }
    //        base.WndProc(ref m);//一定要调用基类函数，以便系统处理其它消息。
    //    }

    //    void VirtualConsole(bool clearBz, string s, int ii)
    //    {
    //        (msgForm as UCConnect).VirtualConsole(clearBz, s, ii);
    //    }

    //}
}
