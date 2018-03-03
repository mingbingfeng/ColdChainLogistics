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

        [DllImport("Coredll.dll")]
        private static extern int Process32First(IntPtr hSnapshot, ref PROCESSENTRY32 lppe);

        [DllImport("Coredll.dll")]
        private static extern int Process32Next(IntPtr hSnapshot, ref PROCESSENTRY32 lppe);

        [DllImport("Coredll.dll", SetLastError = true)]
        private static extern IntPtr CreateToolhelp32Snapshot(uint dwFlags, uint th32ProcessID);

        [DllImport("Coredll.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr hSnapshot);

        private const uint TH32CS_SNAPPROCESS = 0x00000002;

        [StructLayout(LayoutKind.Sequential)]
        private struct PROCESSENTRY32 {
            public uint dwSize;
            public uint cntUsage;
            public uint th32ProcessID;
            public IntPtr th32DefaultHeapID;
            public uint th32ModuleID;
            public uint cntThreads;
            public uint th32ParentProcessID;
            public int pcPriClassBase;
            public uint dwFlags;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szExeFile;
        }

        public static bool IsProcessRunning(string applicationName){
            IntPtr handle = IntPtr.Zero;
            try
            {
                // Create snapshot of the processes
                handle = CreateToolhelp32Snapshot(TH32CS_SNAPPROCESS, 0);
                PROCESSENTRY32 info = new PROCESSENTRY32();
                info.dwSize = (uint)System.Runtime.InteropServices.
                              Marshal.SizeOf(typeof(PROCESSENTRY32));

                // Get the first process
                int first = Process32First(handle, ref info);
                // If we failed to get the first process, throw an exception
                if (first == 0)
                    throw new Exception("Cannot" +
                                                " find first process.");

                // While there's another process, retrieve it
                do
                {
                    if (string.Compare(info.szExeFile,
                          applicationName, true) == 0)
                    {
                        return true;
                    }
                }
                while (Process32Next(handle, ref info) != 0);
            }
            catch
            {
                throw;
            }
            finally
            {
                // Release handle of the snapshot
                CloseHandle(handle);
                handle = IntPtr.Zero;
            }
            return false;
        }
    }
    
}
