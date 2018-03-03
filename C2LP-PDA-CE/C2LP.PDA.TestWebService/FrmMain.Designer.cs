namespace C2LP.PDA.TestWebService
{
    partial class FrmMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.label1 = new System.Windows.Forms.Label();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.nudSpace = new System.Windows.Forms.NumericUpDown();
            this.rbtnGetTime = new System.Windows.Forms.RadioButton();
            this.rbtnUploadOrder = new System.Windows.Forms.RadioButton();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblRequestCount = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblResult = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(19, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 20);
            this.label1.Text = "服务地址：";
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(103, 12);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(150, 23);
            this.txtUrl.TabIndex = 1;
            this.txtUrl.Text = "220.248.66.105:8002";
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(181, 48);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(72, 24);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "开始";
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(19, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 20);
            this.label2.Text = "请求间隔：";
            // 
            // nudSpace
            // 
            this.nudSpace.Location = new System.Drawing.Point(103, 48);
            this.nudSpace.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudSpace.Name = "nudSpace";
            this.nudSpace.Size = new System.Drawing.Size(72, 24);
            this.nudSpace.TabIndex = 4;
            this.nudSpace.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // rbtnGetTime
            // 
            this.rbtnGetTime.Checked = true;
            this.rbtnGetTime.Location = new System.Drawing.Point(19, 78);
            this.rbtnGetTime.Name = "rbtnGetTime";
            this.rbtnGetTime.Size = new System.Drawing.Size(133, 20);
            this.rbtnGetTime.TabIndex = 5;
            this.rbtnGetTime.Text = "获取服务器时间";
            // 
            // rbtnUploadOrder
            // 
            this.rbtnUploadOrder.Location = new System.Drawing.Point(158, 78);
            this.rbtnUploadOrder.Name = "rbtnUploadOrder";
            this.rbtnUploadOrder.Size = new System.Drawing.Size(95, 20);
            this.rbtnUploadOrder.TabIndex = 5;
            this.rbtnUploadOrder.TabStop = false;
            this.rbtnUploadOrder.Text = "上报运单";
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(19, 104);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(234, 154);
            this.txtLog.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(4, 261);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 20);
            this.label3.Text = "请求次数：";
            // 
            // lblRequestCount
            // 
            this.lblRequestCount.Location = new System.Drawing.Point(85, 261);
            this.lblRequestCount.Name = "lblRequestCount";
            this.lblRequestCount.Size = new System.Drawing.Size(60, 20);
            this.lblRequestCount.Text = "0";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(145, 261);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 20);
            this.label4.Text = "成功率：";
            // 
            // lblResult
            // 
            this.lblResult.Location = new System.Drawing.Point(210, 261);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(60, 20);
            this.lblResult.Text = "0%";
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(270, 281);
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.lblRequestCount);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.rbtnUploadOrder);
            this.Controls.Add(this.rbtnGetTime);
            this.Controls.Add(this.nudSpace);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "FrmMain";
            this.Text = "TestWebService";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown nudSpace;
        private System.Windows.Forms.RadioButton rbtnGetTime;
        private System.Windows.Forms.RadioButton rbtnUploadOrder;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblRequestCount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblResult;
    }
}

