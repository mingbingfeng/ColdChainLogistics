using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;

namespace C2LP.PDA.APP
{
    public class BatteryControl
    {
        /// <summary>  
        /// 外边框  
        /// </summary>  
        private Point[] BatteryPolygon = new Point[8];
        /// <summary>  
        /// 是否充电状态  
        /// </summary>  
        private State AState = State.Normal;
        private int Percent = 0;
        private int iLeft = 30 + 2, iTop = 178, iWidth = 20, iHeight = 10;
        private int iRectWidth = 0;
        private Rectangle Rect = new Rectangle();
        private string text = "";
        private SYSTEM_POWER_STATUS_EX status = new SYSTEM_POWER_STATUS_EX();
        /// <summary>  
        /// 电池当前状态 Charge：充电中；UnderCharge：电量不足；Normal：电池正常使用.  
        /// </summary>  
        public enum State
        {
            /// <summary>  
            /// 充电中  
            /// </summary>  
            Charge,
            /// <summary>  
            /// 充电不足  
            /// </summary>  
            UnderCharge,
            /// <summary>  
            /// 正常状态  
            /// </summary>  
            Normal,
            /// <summary>  
            /// 充电完成  
            /// </summary>  
            ChargeFinally
        };
        private class SYSTEM_POWER_STATUS_EX
        {
            public byte ACLineStatus = 0;
            public byte BatteryFlag = 0;
            public byte BatteryLifePercent = 0;
            public byte Reserved1 = 0;
            public uint BatteryLifeTime = 0;
            public uint BatteryFullLifeTime = 0;
            public byte Reserved2 = 0;
            public byte BackupBatteryFlag = 0;
            public byte BackupBatteryLifePercent = 0;
            public byte Reserved3 = 0;
            public uint BackupBatteryLifeTime = 0;
            public uint BackupBatteryFullLifeTime = 0;
        }
        [DllImport("coredll")]
        private static extern int GetSystemPowerStatusEx(SYSTEM_POWER_STATUS_EX lpSystemPowerStatus, bool fUpdate);
        [DllImport("coredll")]
        public static extern void SystemIdleTimerReset();
        /// <summary>  
        /// 构造函数 在屏幕默认位置构建电池形状  
        /// </summary>  
        public void Battery()
        {
            SetPolygon();
        }
        /// <summary>  
        /// 构造函数 电池形状的X和Y坐标  
        /// </summary>  
        /// <param name="StartLeft">电池形状的X坐标</param>  
        /// <param name="StartTop">电池形状的Y坐标</param>  
        public void Battery(int StartLeft, int StartTop)
        {
            iLeft = StartLeft;
            iTop = StartTop;
            SetPolygon();
        }
        /// <summary>  
        /// 构造函数 根据X坐标、Y坐标、宽度、高度构造电池形状  
        /// </summary>  
        /// <param name="StartLeft">电池形状的X坐标</param>  
        /// <param name="StartTop">电池形状的Y坐标</param>  
        /// <param name="StartWidth">电池形状的宽度</param>  
        /// <param name="StartHeight">电池形状的高度</param>  
        public void Battery(int StartLeft, int StartTop, int StartWidth, int StartHeight)
        {
            iLeft = StartLeft;
            iTop = StartTop;
            iWidth = StartWidth;
            iHeight = StartHeight;
            SetPolygon();
        }
        /// <summary>  
        /// 设置电池形状  
        /// </summary>  
        void SetPolygon()
        {
            int Head = 2;
            int HeightLowHalf = (Height - Head) / 2;
            //外边框  
            BatteryPolygon[0].X = iLeft;
            BatteryPolygon[0].Y = iTop;
            BatteryPolygon[1].X = iLeft + iWidth;
            BatteryPolygon[1].Y = iTop;
            BatteryPolygon[2].X = iLeft + iWidth;
            BatteryPolygon[2].Y = iTop + HeightLowHalf;
            BatteryPolygon[3].X = iLeft + iWidth + Head;
            BatteryPolygon[3].Y = iTop + HeightLowHalf;
            BatteryPolygon[4].X = iLeft + iWidth + Head;
            BatteryPolygon[4].Y = iTop + HeightLowHalf + Head;
            BatteryPolygon[5].X = iLeft + iWidth;
            BatteryPolygon[5].Y = iTop + HeightLowHalf + Head;
            BatteryPolygon[6].X = iLeft + iWidth;
            BatteryPolygon[6].Y = iTop + HeightLowHalf + Head + HeightLowHalf;
            BatteryPolygon[7].X = iLeft;
            BatteryPolygon[7].Y = iTop + HeightLowHalf + Head + HeightLowHalf;
            //内矩形  
            Rect.X = BatteryPolygon[0].X + 2;
            Rect.Y = BatteryPolygon[0].Y + 2;
            Rect.Width = BatteryPolygon[6].X - BatteryPolygon[0].X - 3;
            Rect.Height = BatteryPolygon[6].Y - BatteryPolygon[0].Y - 3;
            iRectWidth = Rect.Width;
            GetBatteryState();
        }
        /// <summary>  
        /// 获取电池状态  
        /// </summary>  
        public void GetBatteryState()
        {
            if (GetSystemPowerStatusEx(status, false) == 1)
            {
                if (status.ACLineStatus == 1)
                {
                    //BatteryFlag = 128  充电完成  
                    if (status.BatteryFlag == 128)
                    //if (status.BatteryLifePercent >= 100)  
                    {
                        //status.BatteryLifePercent = 100;  //.BatteryFullLifeTime  
                        text = "充电完成...";
                        AState = State.ChargeFinally;
                    }  //BatteryFlag = 8 正在充电  
                    else
                    {
                        AState = State.Charge;
                        text = "充电中...";
                    }
                }
                else
                {
                    //BatteryFlag = 1 正在使用电池  
                    AState = State.Normal;
                    if (status.BatteryLifePercent > 100) status.BatteryLifePercent = 100;
                    text = status.BatteryLifePercent.ToString() + "%";
                }
                Percent = status.BatteryLifePercent;
                if (Percent <= 20)
                {
                    AState = State.UnderCharge;
                    text = "电量不足...";
                }
                Rect.Width = iRectWidth * ((Percent + 5) > 100 ? 100 : Percent + 8) / 100;
            }
        }
        /// <summary>  
        /// 电池形状X坐标  
        /// </summary>  
        public int Left
        {
            get { return iLeft; }
            set { iLeft = value; SetPolygon(); }
        }
        /// <summary>  
        /// 电池形状Y坐标  
        /// </summary>  
        public int Top
        {
            get { return iTop; }
            set { iTop = value; SetPolygon(); }
        }
        /// <summary>  
        /// 电池形状宽度  
        /// </summary>  
        public int Width
        {
            get { return iWidth; }
            set { iWidth = value; SetPolygon(); }
        }
        /// <summary>  
        /// 电池形状高度  
        /// </summary>  
        public int Height
        {
            get { return iHeight; }
            set { iHeight = value; SetPolygon(); }
        }
        /// <summary>  
        /// 电池电量百分比  
        /// </summary>  
        public int BatteryPercent
        {
            get { return Percent; }
        }
        /// <summary>  
        /// 电池状态  
        /// </summary>  
        public Rectangle BatteryState
        {
            get
            {
                GetBatteryState();
                return Rect;
            }
        }
        /// <summary>  
        /// 电池外边框  
        /// </summary>  
        public Point[] BatteryStateRect
        {
            get { return BatteryPolygon; }
            set { BatteryPolygon = value; }
        }
        /// <summary>  
        /// 当前电池状态，有充电、充电不足、正常 三种状态  
        /// </summary>  
        public State Status
        {
            get { return AState; }
        }
        /// <summary>  
        /// 电池显示的内容  
        /// </summary>  
        public string Text
        {
            get { return text; }
        }
    }
}