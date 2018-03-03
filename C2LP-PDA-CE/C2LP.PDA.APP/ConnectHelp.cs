using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.WindowsCE.Forms;

namespace C2LP.PDA.APP
{
    public class ConnectHelp
    {
        [DllImport("CE380_WCDMA.dll", CharSet = CharSet.Auto, EntryPoint = "WCDMA_Connect")]
        public static extern int WCDMA_Connect(long hwnd);
        [DllImport("CE380_WCDMA.dll", CharSet = CharSet.Auto, EntryPoint = "WCDMA_UnConnect")]
        public static extern void WCDMA_UnConnect();
        [DllImport("CE380_WCDMA.dll", CharSet = CharSet.Auto, EntryPoint = "WCDMA_ReadCSQ")]
        public static extern uint WCDMA_ReadCSQ();

        [DllImport("CE380_CDMA2000.dll", CharSet = CharSet.Auto, EntryPoint = "CDMA2000_Connect")]
        public static extern int CDMA2000_Connect(long hwnd);
        [DllImport("CE380_CDMA2000.dll", CharSet = CharSet.Auto, EntryPoint = "CDMA2000_UnConnect")]
        public static extern void CDMA2000_UnConnect();
        [DllImport("CE380_CDMA2000.dll", CharSet = CharSet.Auto, EntryPoint = "CDMA2000_ReadCSQ")]
        public static extern uint CDMA2000_ReadCSQ();

        #region Memory Management
        [DllImport("coredll")]
        extern public static IntPtr LocalAlloc(int flags, int size);
        [DllImport("coredll")]
        extern public static IntPtr LocalFree(IntPtr pMem);

        const int LMEM_ZEROINIT = 0x40;

        #endregion


        #region IPHLPAPI P/Invokes
        [DllImport("iphlpapi")]
        extern public static IntPtr IcmpCreateFile();

        [DllImport("iphlpapi")]
        extern public static bool IcmpCloseHandle(IntPtr h);

        [DllImport("iphlpapi")]
        extern public static uint IcmpSendEcho(
                         IntPtr IcmpHandle,
                         uint DestinationAddress,
                         byte[] RequestData,
                         short RequestSize,
                         IntPtr /*IP_OPTION_INFORMATION*/ RequestOptions,
                         byte[] ReplyBuffer,
                         int ReplySize,
                         int Timeout);

        #endregion

        [DllImport("coredll")]
        extern static int GetLastError();

        public static HT380_WCDMA_PROCWND wcdma_procwnd;
        public static int i_type = 1;
        public static bool isConencting = false;

        public static int Connnet()
        {
            isConencting = true;
            try
            {
                int ret = G3_Connect(wcdma_procwnd.Hwnd.ToInt32());
                if (ret == -4)
                    throw new Exception("串口通讯失败");
                //else if (ret == 1)
                //    throw new Exception("已连接到网络");
                return ret;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //finally
            //{
            //    isConencting = false;
            //}

        }

        private static int G3_Connect(long hwnd)
        {
            if (i_type == 1)
                return WCDMA_Connect(hwnd);
            else
                return CDMA2000_Connect(hwnd);
        }

        public static void G3_UnConnect()
        {
            if (i_type == 1)
                WCDMA_UnConnect();
            else
                CDMA2000_UnConnect();
        }

        public static uint G3_ReadCSQ()
        {
            if (i_type == 1)
                return WCDMA_ReadCSQ();
            else
                return CDMA2000_ReadCSQ();
        }
    }


    public class ICMP_ECHO_REPLY
    {
        public ICMP_ECHO_REPLY(int size) { data = new byte[size]; }
        byte[] data;
        public byte[] _Data { get { return data; } }
        public int Address { get { return BitConverter.ToInt32(data, 0); } }
        public int Status { get { return BitConverter.ToInt32(data, 4); } }
        public int RoundTripTime { get { return BitConverter.ToInt32(data, 8); } }
        public short DataSize { get { return BitConverter.ToInt16(data, 0xc); } set { BitConverter.GetBytes(value).CopyTo(data, 0xc); } }
        public IntPtr Data { get { return new IntPtr(BitConverter.ToInt32(data, 0x10)); } set { BitConverter.GetBytes(value.ToInt32()).CopyTo(data, 0x10); } }
        public byte Ttl { get { return data[0x14]; } }
        public byte Tos { get { return data[0x15]; } }
        public byte Flags { get { return data[0x16]; } }
        public byte OptionsSize { get { return data[0x17]; } }
        public IntPtr OptionsData { get { return new IntPtr(BitConverter.ToInt32(data, 0x18)); } set { BitConverter.GetBytes(value.ToInt32()).CopyTo(data, 0x18); } }
    }

    public class HT380_WCDMA_PROCWND : MessageWindow
    {
        private object msgForm = null;

        public HT380_WCDMA_PROCWND(object form)
        {
            this.msgForm = form;
        }

        const int WM_RASDIALEVENT = 0xCCCD;
        const int RASCS_PAUSED = 0x1000;
        const int RASCS_DONE = 0x2000;

        enum tagRASCONNSTATE
        {
            RASCS_OpenPort = 0,
            RASCS_PortOpened,
            RASCS_ConnectDevice,
            RASCS_DeviceConnected,
            RASCS_AllDevicesConnected,
            RASCS_Authenticate,
            RASCS_AuthNotify,
            RASCS_AuthRetry,
            RASCS_AuthCallback,
            RASCS_AuthChangePassword,
            RASCS_AuthProject,
            RASCS_AuthLinkSpeed,
            RASCS_AuthAck,
            RASCS_ReAuthenticate,
            RASCS_Authenticated,
            RASCS_PrepareForCallback,
            RASCS_WaitForModemReset,
            RASCS_WaitForCallback,
            RASCS_Projected,

            RASCS_Interactive = RASCS_PAUSED,
            RASCS_RetryAuthentication,
            RASCS_CallbackSetByCaller,
            RASCS_PasswordExpired,

            RASCS_Connected = RASCS_DONE,
            RASCS_Disconnected
        };

        protected override void WndProc(ref Microsoft.WindowsCE.Forms.Message m)
        {
            if (m.Msg == WM_RASDIALEVENT)
            {
                switch ((tagRASCONNSTATE)m.WParam)
                {
                    case tagRASCONNSTATE.RASCS_OpenPort:
                        break;
                    case tagRASCONNSTATE.RASCS_PortOpened:
                        VirtualConsole(false, "端口已打开", 0);
                        break;
                    case tagRASCONNSTATE.RASCS_ConnectDevice:
                        VirtualConsole(false, "正在连接设备", 0);
                        break;
                    case tagRASCONNSTATE.RASCS_DeviceConnected:
                        VirtualConsole(false, "设备已连接", 0);
                        break;
                    case tagRASCONNSTATE.RASCS_AllDevicesConnected:
                        VirtualConsole(false, "物理连接已建立", 0);
                        break;
                    case tagRASCONNSTATE.RASCS_Authenticate:
                        VirtualConsole(false, "正在验证...", 0);
                        break;
                    case tagRASCONNSTATE.RASCS_AuthNotify:
                        VirtualConsole(false, "验证出错", 0);
                        break;
                    case tagRASCONNSTATE.RASCS_AuthRetry:
                        VirtualConsole(false, "正在重新验证...", 0);
                        break;
                    case tagRASCONNSTATE.RASCS_AuthCallback:
                        break;
                    case tagRASCONNSTATE.RASCS_AuthChangePassword:
                        break;
                    case tagRASCONNSTATE.RASCS_AuthProject:
                        break;
                    case tagRASCONNSTATE.RASCS_AuthLinkSpeed:
                        break;
                    case tagRASCONNSTATE.RASCS_AuthAck:
                        break;
                    case tagRASCONNSTATE.RASCS_ReAuthenticate:
                        VirtualConsole(false, "正在重新验证...", 0);
                        break;
                    case tagRASCONNSTATE.RASCS_Authenticated:
                        VirtualConsole(false, "验证已通过", 0);
                        break;
                    case tagRASCONNSTATE.RASCS_PrepareForCallback:
                        break;
                    case tagRASCONNSTATE.RASCS_WaitForModemReset:
                        break;
                    case tagRASCONNSTATE.RASCS_WaitForCallback:
                        break;
                    case tagRASCONNSTATE.RASCS_Projected:
                        break;
                    case tagRASCONNSTATE.RASCS_Interactive:
                        break;
                    case tagRASCONNSTATE.RASCS_RetryAuthentication:
                        break;
                    case tagRASCONNSTATE.RASCS_CallbackSetByCaller:
                        break;
                    case tagRASCONNSTATE.RASCS_PasswordExpired:
                        break;
                    case tagRASCONNSTATE.RASCS_Connected:
                        uint sign = ConnectHelp.G3_ReadCSQ();
                        string stemp = "网络已连接,信号强度：" + sign.ToString();
                        VirtualConsole(false, stemp, 0);
                       ConnectHelp. isConencting = false;
                        break;
                    case tagRASCONNSTATE.RASCS_Disconnected:
                        VirtualConsole(false, "网络连接失败", 0);
                        ConnectHelp.isConencting = false;
                        break;
                    default:
                        base.WndProc(ref m);//一定要调用基类函数，以便系统处理其它消息。
                        break;
                }
            }
            base.WndProc(ref m);//一定要调用基类函数，以便系统处理其它消息。
        }

        void VirtualConsole(bool clearBz, string s, int ii)
        {
            try
            {
                //(msgForm as UCConnect).VirtualConsole(clearBz, s, ii);
                var type = msgForm.GetType();
                if (type.FullName == "C2LP.PDA.APP.UCConnect")
                    ((C2LP.PDA.APP.UCConnect)msgForm).VirtualConsole(clearBz, s, ii);
                else
                    FrmParent.ParentForm.SetNewInfo( s, true);
                //type.InvokeMember("VirtualConsole", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance, null,msgForm,new object[]{ clearBz, s, ii});
            }
            catch 
            {
            }
        }

    }
}
