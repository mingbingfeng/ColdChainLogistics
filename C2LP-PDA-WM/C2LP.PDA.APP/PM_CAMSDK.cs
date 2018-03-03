using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

enum _Image_Type_
{
    RESOLUTION_QQVGA = 0,
    RESOLUTION_QCIF = 1,
    RESOLUTION_QVGA = 2,
    RESOLUTION_CIF = 3,
    RESOLUTION_VGA = 4,
    RESOLUTION_NTSC = 5,
    RESOLUTION_D1 = 6,
    RESOLUTION_WVGA = 7,
    RESOLUTION_SVGA = 8,
    RESOLUTION_XGA = 9,
    RESOLUTION_HD = 10,
    RESOLUTION_SXGA = 11,
    RESOLUTION_FULLHD = 12,
    RESOLUTION_XVGA = 13,
    RESOLUTION_QXGA = 14,
    RESOLUTION_5MP = 15,
    RESOLUTION_8MP = 16,
    RESOLUTION_MEGA5 = 17

};
enum _Camera_type_
{
    CAM_CAMERA_TYPE_NONE = 0,
    CAM_CAMERA_TYPE_OV3640 = 1,
    CAM_CAMERA_TYPE_MT9T111 = 2,
    CAM_CAMERA_TYPE_S5K4ECGX = 3,
    CAM_CAMERA_TYPE_MT9E013 = 4,
    CAM_CAMERA_TYPE_AR0833 = 5,
};

enum _S5K4ECGX_WHITEBALANCE
{
    S5K4ECGX_DEFAULT = (0),
    S5K4ECGX_SUNSHINE = (1),
    S5K4ECGX_CLOUDY = (2),
    S5K4ECGX_FLUORESCENCE = (3),
    S5K4ECGX_INCANDESCENCE = (4)
}


namespace C2LP.PDA.APP
{
    //public struct CAMERAINFO
    //{
    //    public string wcReso;
    //    public int nIndex;

    //    public CAMERAINFO(string wcReso, int nIndex)
    //    {
    //        this.wcReso = wcReso;
    //        this.nIndex = nIndex;
    //    }
    //}

    public class PM_CAMSDK
    {
        public bool bCAMSDK = false;

        public unsafe PM_CAMSDK()
        {
            bCAMSDK = aCamInit();
            if (bCAMSDK)
            {

            }
            else
            {
                aCamDeInit();
            }
        }

        ~PM_CAMSDK()
        {
        }

        public void Close()
        {
            aCamDeInit();
        }

        [DllImport("PM_CAMSDK.dll")]
        public static extern bool aCamInit();

        [DllImport("PM_CAMSDK.dll")]
        public static extern bool aCamDeInit();

        [DllImport("PM_CAMSDK.dll")]
        public static extern bool aCamPreview(bool bPreviewState);

        [DllImport("PM_CAMSDK.dll")]
        public static extern bool aCamCapture([MarshalAs(UnmanagedType.LPWStr)]String Filename);

        [DllImport("PM_CAMSDK.dll")]
        public static extern bool aCamTargetWindow(IntPtr hWnd);

        [DllImport("PM_CAMSDK.dll")]
        public static extern bool aCamSetWindowPOS(int nX, int nY, int nWidth, int nHeight);

        [DllImport("PM_CAMSDK.dll")]
        public static extern bool aCamGetWindowPOS(IntPtr pX, IntPtr pY, IntPtr pWidth, IntPtr pHeight);

        [DllImport("PM_CAMSDK.dll")]
        public static extern bool aCamSetPreviewResolutionType(int dwPreviewRSType);

        [DllImport("PM_CAMSDK.dll")]
        public static extern bool aCamGetPreviewResolutionType(IntPtr pPreviewRSType);

        [DllImport("PM_CAMSDK.dll")]
        public static extern bool aCamSetCaptureResolutionType(int dwCaptureRSType); 

        [DllImport("PM_CAMSDK.dll")]
        public static extern bool aCamGetCaptureResolutionType(IntPtr pCaptureRSType); 

        [DllImport("PM_CAMSDK.dll")]
        public static extern bool aCamSetCaptureDecodeDelay(int dwDelayMillisecond); 

        [DllImport("PM_CAMSDK.dll")]
        public static extern bool aCamGetCameraInfo(IntPtr pCameraType);

        [DllImport("PM_CAMSDK.dll")]
        public static extern bool aCamSetZoommagnification(int nValue); 

        [DllImport("PM_CAMSDK.dll")]
        public static extern bool aCamGetZoomMinMaxRange(IntPtr pMin, IntPtr pMax); 

        [DllImport("PM_CAMSDK.dll")]
        public static extern bool aCamSetBrightness(int nBrightlevel);

        [DllImport("PM_CAMSDK.dll")]
        public static extern bool aCamGetBrightness(IntPtr pBrightlevel);

        [DllImport("PM_CAMSDK.dll")]
        public static extern bool aCamGetBrightnessRange(IntPtr pMin, IntPtr pMax, IntPtr pStep);

        [DllImport("PM_CAMSDK.dll")]
        public static extern bool aCamSetWhiteBalance(int nWhitebalanceMode);

        [DllImport("PM_CAMSDK.dll")]
        public static extern bool aCamGetWhiteBalance(IntPtr nWhitebalanceMode);

        [DllImport("PM_CAMSDK.dll")]
        public static extern bool aCamVideoRecord(bool bRecordState);

        [DllImport("PM_CAMSDK.dll")]
        public static extern bool aCamSetVideoFilename([MarshalAs(UnmanagedType.LPWStr)]String pFilename);

        [DllImport("PM_CAMSDK.dll")]
        public static extern bool aCamMakeThumbnailImage([MarshalAs(UnmanagedType.LPWStr)]String Filename);

        [DllImport("PM_CAMSDK.dll")]
        public static extern bool aCamSetFlashLightOn(bool bOn);

        [DllImport("PM_CAMSDK.dll")]
        public static extern bool aCamGetFlashLightOn(IntPtr dwState);
       
        public bool CamPreview(bool bPreviewState)
        {
            return aCamPreview((bool)bPreviewState);
        }

        public bool CamCapture([MarshalAs(UnmanagedType.LPWStr)]String Filename)
        {
            return aCamCapture(Filename);
        }

        public bool CamTargetWindow(IntPtr hWnd)
        {
            return aCamTargetWindow(hWnd);
        }

        public bool CamSetWindowPOS(int nX, int nY, int nWidth, int nHeight)
        {
            return aCamSetWindowPOS((int)nX, (int) nY, (int) nWidth, (int) nHeight);
        }

        public bool CamGetWindowPOS(IntPtr pX, IntPtr pY, IntPtr pWidth, IntPtr pHeight)
        {
            return aCamGetWindowPOS( pX, pY, pWidth, pHeight);
        }

        public bool CamSetPreviewResolutionType(int dwPreviewRSType)
        {
            return aCamSetPreviewResolutionType(dwPreviewRSType);
        }

        public bool CamGetPreviewResolutionType(IntPtr pPreviewRSType)
        {
            return aCamGetPreviewResolutionType((IntPtr) pPreviewRSType);
        }

        public bool CamSetCaptureResolutionType(int dwCaptureRSType)
        {
            return aCamSetCaptureResolutionType((int)dwCaptureRSType);
        }

        public bool CamGetCaptureResolutionType(IntPtr pCaptureRSType)
        {
            return aCamGetCaptureResolutionType((IntPtr) pCaptureRSType);
        }

        public bool CamSetCaptureDecodeDelay(int dwDelayMillisecond)
        {
            return aCamSetCaptureDecodeDelay((int)dwDelayMillisecond); 
        }

        public bool CamGetCameraInfo(IntPtr pCameraType)
        {
            return aCamGetCameraInfo((IntPtr) pCameraType);
        }

        public bool CamSetZoommagnification(int nValue)
        {
            return aCamSetZoommagnification((int) nValue); 
        }

        public bool CamGetZoomMinMaxRange(IntPtr pMin, IntPtr pMax)
        {
            return aCamGetZoomMinMaxRange((IntPtr) pMin, (IntPtr) pMax);
        }

        public bool CamSetBrightness(int nBrightlevel)
        {
            return aCamSetBrightness((int) nBrightlevel);
        }

        public bool CamGetBrightness(IntPtr pBrightlevel)
        {
            return aCamGetBrightness((IntPtr) pBrightlevel);
        }

        public bool CamGetBrightnessRange(IntPtr pMin, IntPtr pMax, IntPtr pStep)
        {
            return aCamGetBrightnessRange((IntPtr) pMin, (IntPtr) pMax, (IntPtr) pStep);
        }

        public bool CamSetWhiteBalance(int nWhitebalanceMode)
        {
            return aCamSetWhiteBalance((int) nWhitebalanceMode);
        }

        public bool CamGetWhiteBalance(IntPtr nWhitebalanceMode)
        {
            return aCamGetWhiteBalance((IntPtr)nWhitebalanceMode);
        }

        public bool CamVideoRecord(bool bRecordState)
        {
            return aCamVideoRecord((bool)bRecordState);
        }

        public bool CamSetVideoFilename([MarshalAs(UnmanagedType.LPWStr)]String pFilename)
        {
            return aCamSetVideoFilename(pFilename);
        }

        public bool CamMakeThumbnailImage([MarshalAs(UnmanagedType.LPWStr)]String Filename)
        {
            return aCamMakeThumbnailImage(Filename);
        }

        public bool CamSetFlashLightOn(bool bOn)
        {
            return aCamSetFlashLightOn((bool)bOn);
        }

        public bool CamGetFlashLightOn(IntPtr dwState)
        {
            return aCamGetFlashLightOn(dwState);
        }

    }
}
