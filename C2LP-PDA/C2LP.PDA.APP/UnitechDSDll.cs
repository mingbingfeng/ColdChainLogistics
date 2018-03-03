using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using Microsoft.Win32;
using System.Diagnostics;

namespace C2LP.PDA.APP
{
    public class UnitechDSDll
    {
        //open camera. 
        //before use this API, please CoInitialize first
        //hVideoWnd:show graph, 
        //wndLeft, wndTop:graph show top left position.
        //wndWidth, wndHeight: graph show width and height.
        //picWidth:picture width, picHeight:picture height.
        //if has no resolution with picWidth and picHeight, it will be use default resolution.
        //UNITECH_CAMERA_DLL BOOL OpenCamera(HWND hVideoWnd, int wndLeft, int wndTop,
        //int wndWidth, int wndHeight, int picWidth, int picHeight);
        [DllImport("UnitechDSDll.dll")]
        public static extern bool OpenCamera(IntPtr hWnd, int wndLeft, int wndTop,
         int wndWidth, int wndHeight, int picWidth, int picHeight);

        //close camera.
        //after use this API, please CoUninitialize.
        //UNITECH_CAMERA_DLL void CloseCamera();
        [DllImport("UnitechDSDll.dll")]
        public static extern void CloseCamera();

        //Start graph Preview.
        //UNITECH_CAMERA_DLL BOOL PreviewStart();
        [DllImport("UnitechDSDll.dll")]
        public static extern bool PreviewStart();

        //Stop Graph Preview.
        //UNITECH_CAMERA_DLL BOOL PreviewStop();
        [DllImport("UnitechDSDll.dll")]
        public static extern bool PreviewStop();

        //Snap picture, strFileName is your picture file full Path name.
        //UNITECH_CAMERA_DLL BOOL SnapPicture(WCHAR *strFileName);
        [DllImport("UnitechDSDll.dll")]
        public static extern bool SnapPicture(string strFileName);

        //Zoom In or Out, Loops Zoom In -> Out --> In.
        //UNITECH_CAMERA_DLL BOOL ZoomIn();
        [DllImport("UnitechDSDll.dll")]
        public static extern bool ZoomIn();

        //Zoom In or Out, Loops Zoom In -> Out --> In.
        //UNITECH_CAMERA_DLL BOOL ZoomOut();
        [DllImport("UnitechDSDll.dll")]
        public static extern bool ZoomOut();

        //Brightness, Loops dark -> bright --> dark.
        //UNITECH_CAMERA_DLL BOOL Brightness();
        [DllImport("UnitechDSDll.dll")]
        public static extern bool Brightness();

        //WhiteBalance, Loops low noise -> high noise --> low noise.
        //UNITECH_CAMERA_DLL BOOL WhiteBalance();
        [DllImport("UnitechDSDll.dll")]
        public static extern bool WhiteBalance();

        //ColorEnable, Loops false -> true --> false.
        //UNITECH_CAMERA_DLL BOOL ColorEnable();
        [DllImport("UnitechDSDll.dll")]
        public static extern bool ColorEnable();

        //AutoFocus, looks like doesn't work.
        //UNITECH_CAMERA_DLL BOOL AutoFocus();
        [DllImport("UnitechDSDll.dll")]
        public static extern bool AutoFocus();

        //Start Record video, only support .asf, please set name like: "\\Unitech_videoTest.asf"  as you want.
        //looks like only support 320x240
        //UNITECH_CAMERA_DLL BOOL StartRecordVideo(WCHAR *strFileName);
        [DllImport("UnitechDSDll.dll")]
        public static extern bool StartRecordVideo(string strFileName);

        //Stop Record video.
        //UNITECH_CAMERA_DLL BOOL StopRecordVideo();
        [DllImport("UnitechDSDll.dll")]
        public static extern bool StopRecordVideo();

        //Explosure Control, looks like doesn't work.
        //UNITECH_CAMERA_DLL BOOL ExposureControl();
        [DllImport("UnitechDSDll.dll")]
        public static extern bool ExposureControl();

        //set Resolution, looks like doesn't work, please set Resolution in API OpenCamera.
        //UNITECH_CAMERA_DLL BOOL SetResolution(int width, int height);
        [DllImport("UnitechDSDll.dll")]
        public static extern bool SetResolution();

    }
}