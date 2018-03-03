using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;

namespace C2LP.PDA.APP
{
    public static class FullScreen
    {
        public const int SPI_SETWORKAREA = 47;
        public const int SPI_GETWORKAREA = 48;
        public const int SW_HIDE = 0x00;
        public const int SW_SHOW = 0x0001;
        public const int SPIF_UPDATEINIFILE = 0x01;
        [DllImport("coredll.dll", EntryPoint = "FindWindow")]
        private static extern IntPtr FindWindow(string lpWindowName, string lpClassName);
        [DllImport("coredll.dll", EntryPoint = "ShowWindow")]
        private static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);
        [DllImport("coredll.dll", EntryPoint = "SystemParametersInfo")]
        private static extern int SystemParametersInfo(int uAction, int uParam, ref Rectangle lpvParam, int fuWinIni);

        //[DllImport("coredll.dll", EntryPoint = "SipShowIM")]
        //private static extern long SipShowIM(long flags);

        /// <summary>
        /// 设置全屏或取消全屏
        /// </summary>
        /// <param name="fullscreen">true:全屏 false:恢复</param>
        /// <param name="rectOld">设置的时候，此参数返回原始尺寸，恢复时用此参数设置恢复</param>
        /// <returns>设置结果</returns>
        public static bool SetFullScreen(bool fullscreen, ref Rectangle rectOld)
        {
            IntPtr Hwnd = FindWindow("HHTaskBar", null);
            if (Hwnd == IntPtr.Zero) return false;
            if (fullscreen)
            {
                ShowWindow(Hwnd, SW_HIDE);
                Rectangle rectFull = Screen.PrimaryScreen.Bounds;
                SystemParametersInfo(SPI_GETWORKAREA, 0, ref rectOld, SPIF_UPDATEINIFILE);//get
                SystemParametersInfo(SPI_SETWORKAREA, 0, ref rectFull, SPIF_UPDATEINIFILE);//set
            }
            else
            {
                ShowWindow(Hwnd, SW_SHOW);
                SystemParametersInfo(SPI_SETWORKAREA, 0, ref rectOld, SPIF_UPDATEINIFILE);
            }
            return true;
        }

        //public static void SetIM(bool show) {
        //    SipShowIM(show ? 1 : 0);
        //}

    }
}
