using System.Drawing;
using C2LP.PDA.APP.ScannerAPI;
using System;
namespace C2LP.PDA.APP
{
    partial class FrmParent
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            Rectangle rect = new Rectangle();
            FullScreen.SetFullScreen(false, ref rect);
            //Scanner.GetScanner().Close();
            Scanner.GetScanner().Unregister();
            UnitechDSDll.CloseCamera();
            SyncHelp.StopSync();
            UploadHelp.StopUpload();
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlState = new System.Windows.Forms.Panel();
            this.lblTime = new System.Windows.Forms.Label();
            this.pnlAbout = new System.Windows.Forms.Panel();
            this.pnlNews = new System.Windows.Forms.Panel();
            this.lblNews = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pbPreview = new System.Windows.Forms.PictureBox();
            this.lblAbout = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.inputPnl = new Microsoft.WindowsCE.Forms.InputPanel(this.components);
            this.tmUpload = new System.Windows.Forms.Timer();
            this.tmTime = new System.Windows.Forms.Timer();
            this.pnlState.SuspendLayout();
            this.pnlAbout.SuspendLayout();
            this.pnlNews.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlState
            // 
            this.pnlState.BackColor = System.Drawing.Color.Transparent;
            this.pnlState.Controls.Add(this.lblTime);
            this.pnlState.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlState.Location = new System.Drawing.Point(0, 0);
            this.pnlState.Name = "pnlState";
            this.pnlState.Size = new System.Drawing.Size(245, 24);
            // 
            // lblTime
            // 
            this.lblTime.Location = new System.Drawing.Point(65, 3);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(114, 20);
            this.lblTime.Text = "2016-11-11 00:00";
            this.lblTime.Visible = false;
            // 
            // pnlAbout
            // 
            this.pnlAbout.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.pnlAbout.Controls.Add(this.pnlNews);
            this.pnlAbout.Controls.Add(this.pbPreview);
            this.pnlAbout.Controls.Add(this.lblAbout);
            this.pnlAbout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlAbout.Location = new System.Drawing.Point(0, 378);
            this.pnlAbout.Name = "pnlAbout";
            this.pnlAbout.Size = new System.Drawing.Size(245, 34);
            // 
            // pnlNews
            // 
            this.pnlNews.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlNews.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.pnlNews.Controls.Add(this.lblNews);
            this.pnlNews.Controls.Add(this.label1);
            this.pnlNews.Location = new System.Drawing.Point(0, 0);
            this.pnlNews.Name = "pnlNews";
            this.pnlNews.Size = new System.Drawing.Size(245, 0);
            this.pnlNews.Visible = false;
            // 
            // lblNews
            // 
            this.lblNews.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNews.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblNews.Location = new System.Drawing.Point(57, 4);
            this.lblNews.Name = "lblNews";
            this.lblNews.Size = new System.Drawing.Size(188, 18);
            this.lblNews.Text = "无";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(3, 4);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 18);
            this.label1.Text = "上报消息：";
            // 
            // pbPreview
            // 
            this.pbPreview.BackColor = System.Drawing.Color.Transparent;
            this.pbPreview.Location = new System.Drawing.Point(204, 3);
            this.pbPreview.Name = "pbPreview";
            this.pbPreview.Size = new System.Drawing.Size(25, 23);
            this.pbPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbPreview.Visible = false;
            // 
            // lblAbout
            // 
            this.lblAbout.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblAbout.ForeColor = System.Drawing.Color.White;
            this.lblAbout.Location = new System.Drawing.Point(41, 3);
            this.lblAbout.Name = "lblAbout";
            this.lblAbout.Size = new System.Drawing.Size(161, 29);
            this.lblAbout.Text = "上海思博源冷链科技有限公司\r\n    Copyright © 2016-2020";
            // 
            // pnlMain
            // 
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 24);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(245, 354);
            // 
            // inputPnl
            // 
            this.inputPnl.EnabledChanged += new System.EventHandler(this.inputPnl_EnabledChanged);
            // 
            // tmUpload
            // 
            this.tmUpload.Enabled = true;
            this.tmUpload.Interval = 3000;
            this.tmUpload.Tick += new System.EventHandler(this.tmUpload_Tick);
            // 
            // tmTime
            // 
            this.tmTime.Enabled = true;
            this.tmTime.Interval = 1000;
            this.tmTime.Tick += new System.EventHandler(this.tmTime_Tick);
            // 
            // FrmParent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(245, 412);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlAbout);
            this.Controls.Add(this.pnlState);
            this.Name = "FrmParent";
            this.Text = "冷链物流系统V1.9";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmParent_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.FrmParent_Closing);
            this.pnlState.ResumeLayout(false);
            this.pnlAbout.ResumeLayout(false);
            this.pnlNews.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel pnlAbout;
        private System.Windows.Forms.Label lblAbout;
        private System.Windows.Forms.Panel pnlMain;
        private Microsoft.WindowsCE.Forms.InputPanel inputPnl;
        public System.Windows.Forms.PictureBox pbPreview;
        private System.Windows.Forms.Timer tmUpload;
        private System.Windows.Forms.Timer tmTime;
        public System.Windows.Forms.Panel pnlState;
        public System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Panel pnlNews;
        private System.Windows.Forms.Label lblNews;
        private System.Windows.Forms.Label label1;
    }
}