namespace C2LP.PDA.APP
{
    partial class UCConnect
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.rbtnWCDMA = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.lblSign = new System.Windows.Forms.Label();
            this.btnScan = new System.Windows.Forms.Button();
            this.lbGprs = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(177, 8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(56, 23);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "返回";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(19, 8);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(56, 23);
            this.btnConnect.TabIndex = 16;
            this.btnConnect.Text = "连接";
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // rbtnWCDMA
            // 
            this.rbtnWCDMA.Checked = true;
            this.rbtnWCDMA.Location = new System.Drawing.Point(19, 41);
            this.rbtnWCDMA.Name = "rbtnWCDMA";
            this.rbtnWCDMA.Size = new System.Drawing.Size(170, 20);
            this.rbtnWCDMA.TabIndex = 17;
            this.rbtnWCDMA.Text = "联通/移动[WCDMA]";
            // 
            // radioButton2
            // 
            this.radioButton2.Location = new System.Drawing.Point(19, 68);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(170, 20);
            this.radioButton2.TabIndex = 17;
            this.radioButton2.TabStop = false;
            this.radioButton2.Text = "电信[CDMA2000]";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.Location = new System.Drawing.Point(3, 321);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 20);
            this.label1.Text = "信号强度：";
            // 
            // lblSign
            // 
            this.lblSign.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblSign.Location = new System.Drawing.Point(75, 321);
            this.lblSign.Name = "lblSign";
            this.lblSign.Size = new System.Drawing.Size(53, 20);
            this.lblSign.Text = "未知[0]";
            // 
            // btnScan
            // 
            this.btnScan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnScan.Location = new System.Drawing.Point(129, 318);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(104, 23);
            this.btnScan.TabIndex = 23;
            this.btnScan.Text = "重新扫描信号";
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // lbGprs
            // 
            this.lbGprs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbGprs.Location = new System.Drawing.Point(3, 96);
            this.lbGprs.Name = "lbGprs";
            this.lbGprs.Size = new System.Drawing.Size(241, 210);
            this.lbGprs.TabIndex = 26;
            // 
            // UCConnect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.lbGprs);
            this.Controls.Add(this.btnScan);
            this.Controls.Add(this.lblSign);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.rbtnWCDMA);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.btnCancel);
            this.Name = "UCConnect";
            this.Size = new System.Drawing.Size(247, 356);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.RadioButton rbtnWCDMA;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblSign;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.ListBox lbGprs;
    }
}
