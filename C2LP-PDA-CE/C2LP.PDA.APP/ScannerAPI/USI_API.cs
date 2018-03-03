using System;
using System.Runtime.InteropServices;

namespace C2LP.PDA.APP.ScannerAPI
{
    public class USI_API
    {
        [DllImport("usi.dll")] //打开初始化scanner
        public static extern bool USI_Register(System.IntPtr hwnd, int msgID);
        [DllImport("usi.dll")] //关闭scanner
        public static extern void USI_Unregister();
        [DllImport("usi.dll")] //reset scanner
        public static extern bool USI_Reset();
        [DllImport("usi.dll")] //Get Error Code
        public static extern long USI_GetError();
        [DllImport("usi.dll")] //Get Error Code
        public static extern long USI_GetLastSysError(string buff, uint blen);
        [DllImport("usi.dll")] //Get scan Data
        public static extern int USI_GetData(byte[] buff, int blen, ref uint type);
        [DllImport("usi.dll")] //Get Length of scanned data
        public static extern int USI_GetDataLength();
        [DllImport("usi.dll")] //Reset Scan Data
        public static extern void USI_ResetData();
        [DllImport("usi.dll")]
        public static extern void USI_ReadOK();
        [DllImport("usi.dll")]
        public static extern bool USI_SaveCurrentSettings();
        [DllImport("usi.dll")]
        public static extern bool USI_SaveSettingsToFile(string filename);
        [DllImport("usi.dll")]
        public static extern bool USI_LoadSettingsFromFile(string filename, bool formulaOnly);
        [DllImport("usi.dll")]
        public static extern bool USI_StartAutoScan(int interval); //ms
        [DllImport("usi.dll")]
        public static extern void USI_StopAutoScan();
        [DllImport("usi.dll")]
        public static extern bool USI_IsAutoScanning();
        [DllImport("usi.dll")]
        public static extern bool USI_EnableScan(bool enable);

    }
}