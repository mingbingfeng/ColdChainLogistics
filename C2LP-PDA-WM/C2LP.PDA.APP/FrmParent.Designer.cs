using System.Drawing;
//using C2LP.PDA.APP.ScannerAPI;
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
            //UnitechDSDll.CloseCamera();
            SyncHelp.StopSync();
            UploadHelp.StopUpload();

            try
            {
                //this.DecodeApi.aDecodeSetDecodeEnable(0);
                //this.DecodeApi.Close();
            }
            catch
            {
            }
            try
            {
                this.CamSdk.Close();
            }
            catch 
            {
            }
            
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmParent));
            this.pnlState = new System.Windows.Forms.Panel();
            this.pnlTempInput = new System.Windows.Forms.Panel();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtTempInput = new System.Windows.Forms.TextBox();
            this.btnBack = new System.Windows.Forms.Button();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.pnlAbout = new System.Windows.Forms.Panel();
            this.lblNumber = new System.Windows.Forms.Label();
            this.lblAbout = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.inputPnl = new Microsoft.WindowsCE.Forms.InputPanel(this.components);
            this.tmUpload = new System.Windows.Forms.Timer();
            this.tmTime = new System.Windows.Forms.Timer();
            this.pnlState.SuspendLayout();
            this.pnlTempInput.SuspendLayout();
            this.pnlAbout.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlState
            // 
            this.pnlState.BackColor = System.Drawing.Color.Transparent;
            this.pnlState.Controls.Add(this.pnlTempInput);
            this.pnlState.Controls.Add(this.lblTime);
            this.pnlState.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlState.Location = new System.Drawing.Point(0, 0);
            this.pnlState.Name = "pnlState";
            this.pnlState.Size = new System.Drawing.Size(240, 24);
            // 
            // pnlTempInput
            // 
            this.pnlTempInput.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.pnlTempInput.Controls.Add(this.btnOK);
            this.pnlTempInput.Controls.Add(this.txtTempInput);
            this.pnlTempInput.Controls.Add(this.btnBack);
            this.pnlTempInput.Controls.Add(this.lblTitle);
            this.pnlTempInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTempInput.Location = new System.Drawing.Point(0, 0);
            this.pnlTempInput.Name = "pnlTempInput";
            this.pnlTempInput.Size = new System.Drawing.Size(240, 24);
            this.pnlTempInput.Visible = false;
            this.pnlTempInput.Click += new System.EventHandler(this.pnlTempInput_Click);
            this.pnlTempInput.LostFocus += new System.EventHandler(this.pnlTempInput_LostFocus);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.BackColor = System.Drawing.Color.Transparent;
            this.btnOK.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.btnOK.Location = new System.Drawing.Point(197, 3);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(19, 17);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "√";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtTempInput
            // 
            this.txtTempInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTempInput.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtTempInput.Location = new System.Drawing.Point(73, 2);
            this.txtTempInput.Name = "txtTempInput";
            this.txtTempInput.Size = new System.Drawing.Size(122, 19);
            this.txtTempInput.TabIndex = 1;
            this.txtTempInput.TextChanged += new System.EventHandler(this.txtTempInput_TextChanged);
            // 
            // btnBack
            // 
            this.btnBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBack.BackColor = System.Drawing.Color.Transparent;
            this.btnBack.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.btnBack.Location = new System.Drawing.Point(220, 3);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(17, 17);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "→";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblTitle.Location = new System.Drawing.Point(9, 4);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(65, 14);
            this.lblTitle.Text = "请输入：";
            // 
            // lblTime
            // 
            this.lblTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTime.ForeColor = System.Drawing.Color.White;
            this.lblTime.Location = new System.Drawing.Point(65, 3);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(130, 20);
            this.lblTime.Text = "2016-11-11 00:00";
            // 
            // pnlAbout
            // 
            this.pnlAbout.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.pnlAbout.Controls.Add(this.lblNumber);
            this.pnlAbout.Controls.Add(this.lblAbout);
            this.pnlAbout.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlAbout.Location = new System.Drawing.Point(0, 259);
            this.pnlAbout.Name = "pnlAbout";
            this.pnlAbout.Size = new System.Drawing.Size(240, 34);
            this.pnlAbout.Click += new System.EventHandler(this.pnlAbout_Click);
            this.pnlAbout.LostFocus += new System.EventHandler(this.pnlAbout_LostFocus);
            // 
            // lblNumber
            // 
            this.lblNumber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblNumber.Location = new System.Drawing.Point(3, 14);
            this.lblNumber.Name = "lblNumber";
            this.lblNumber.Size = new System.Drawing.Size(32, 20);
            // 
            // lblAbout
            // 
            this.lblAbout.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblAbout.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblAbout.ForeColor = System.Drawing.Color.White;
            this.lblAbout.Location = new System.Drawing.Point(57, 3);
            this.lblAbout.Name = "lblAbout";
            this.lblAbout.Size = new System.Drawing.Size(183, 29);
            this.lblAbout.Text = "上海思博源冷链科技有限公司\r\n       Copyright © 2016-2017";
            // 
            // pnlMain
            // 
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 24);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(240, 235);
            // 
            // inputPnl
            // 
            this.inputPnl.EnabledChanged += new System.EventHandler(this.inputPnl_EnabledChanged);
            // 
            // tmUpload
            // 
            this.tmUpload.Enabled = true;
            this.tmUpload.Interval = 3000;
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
            this.ClientSize = new System.Drawing.Size(240, 293);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.pnlAbout);
            this.Controls.Add(this.pnlState);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmParent";
            this.Text = "冷链物流V4.5";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.FrmParent_Load);
            this.Closed += new System.EventHandler(this.FrmParent_Closed);
            this.pnlState.ResumeLayout(false);
            this.pnlTempInput.ResumeLayout(false);
            this.pnlAbout.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel pnlAbout;
        private System.Windows.Forms.Label lblAbout;
        public System.Windows.Forms.Panel pnlMain;
        private Microsoft.WindowsCE.Forms.InputPanel inputPnl;
        private System.Windows.Forms.Timer tmUpload;
        private System.Windows.Forms.Timer tmTime;
        public System.Windows.Forms.Panel pnlState;
        public System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Panel pnlTempInput;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TextBox txtTempInput;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label lblNumber;
    }
}