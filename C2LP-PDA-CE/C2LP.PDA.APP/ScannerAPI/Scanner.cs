using System;
using System.Threading;
using System.Runtime.InteropServices;
using Microsoft.WindowsCE.Forms;

namespace C2LP.PDA.APP.ScannerAPI
{

    /// <summary>
    /// Scanner 的摘要描述。
    /// </summary>
    /// 
    public delegate void GetBarcodeEventHandler(object sender, GetBarcodeEventArgs e);
    internal class ScannerMessageWindow : MessageWindow
    {
        internal const int WM_USER = 0x0400;
        internal const int WM_GET_BARCODE = WM_USER + 100;
        Scanner barcodeScanner = null;
        //int totalcount = 1;
        public ScannerMessageWindow(Scanner rScanner)
        {
            barcodeScanner = rScanner;

        }
        protected override void WndProc(ref Message msg)
        {
            switch (msg.Msg)
            {
                case WM_GET_BARCODE:
                    {
                        int len = 0;
                        uint type = 0;

                        if ((len = USI_API.USI_GetDataLength()) > 0)
                        {
                            byte[] buff = new byte[len];
                            USI_API.USI_ReadOK();
                            USI_API.USI_GetData(buff, len, ref type);

                            // v1.03 UTX_Answer 修改 (2D >> 显示汉字)
                            //string barcodeString = System.Text.Encoding.ASCII.GetString(bBuff, 0, bBuff.Length);
                            string barcodeString = System.Text.Encoding.Default.GetString(buff, 0, buff.Length);
                            ////todo..
                            //if(totalcount==1)
                            //barcodeString = "00123456001";
                            //else if(totalcount ==2)
                            //    barcodeString = "00123456002";
                            //else if (totalcount == 3)
                            //    barcodeString = "00123456003";
                            //else if (totalcount == 4)
                            //    barcodeString = "00123456004";
                            //else if (totalcount == 5)
                            //    barcodeString = "00123456005";
                            //totalcount++;

                            barcodeScanner.DoGetBarcode(barcodeString);
                        }
                    }
                    break;
            }
            // call the base class WndProc for default message handling
            base.WndProc(ref msg);
        }
    }

    public class GetBarcodeEventArgs : System.EventArgs
    {
        public GetBarcodeEventArgs()
        {

        }
        private string barcodeString = "";
        public string Barcode
        {
            get
            {
                return barcodeString;
            }
            set
            {
                barcodeString = value;
            }
        }
    }

    public class Scanner
    {
        public event GetBarcodeEventHandler OnGetBarcodeEvent;

        private ScannerMessageWindow scanMessageWindow;

        private static readonly Scanner scanner = new Scanner();

        public static Scanner GetScanner()
        {
            return scanner;
        }

        public void DoGetBarcode(string barcodeString)
        {
            GetBarcodeEventArgs barcodeEventArgs = new GetBarcodeEventArgs();
            barcodeEventArgs.Barcode = barcodeString;
            if (OnGetBarcodeEvent != null)
            {
                OnGetBarcodeEvent(this, barcodeEventArgs);
            }
        }

        private Scanner()
        {
            scanMessageWindow = new ScannerMessageWindow(this);
            USI_API.USI_Register(scanMessageWindow.Hwnd, ScannerMessageWindow.WM_GET_BARCODE);
        }

        public void Register()
        {
            USI_API.USI_Register(scanMessageWindow.Hwnd, ScannerMessageWindow.WM_GET_BARCODE);
        }

        public void Unregister()
        {
            USI_API.USI_Unregister();
        }

        ~Scanner()
        {
            USI_API.USI_Unregister();
        }

        /// <summary>
        /// 开启 Scanner
        /// </summary>
        public bool Open()
        {
            return USI_API.USI_EnableScan(true);
        }


        /// <summary>
        /// 关闭 Scanner
        /// </summary>
        /// <returns></returns>
        public bool Close()
        {
            return USI_API.USI_EnableScan(false);
        }
    }
}