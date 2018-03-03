namespace C2LP.PDA.APP.OrderInputForm
{
    partial class UCOrderInput1
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
            SaveCustomerUserControl();
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
            this.lblNumLength = new System.Windows.Forms.Label();
            this.txtOrderNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlS = new System.Windows.Forms.Panel();
            this.label8 = new System.Windows.Forms.Label();
            this.pnlR = new System.Windows.Forms.Panel();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.nudCount = new System.Windows.Forms.NumericUpDown();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblNumLength
            // 
            this.lblNumLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNumLength.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblNumLength.Location = new System.Drawing.Point(216, 5);
            this.lblNumLength.Name = "lblNumLength";
            this.lblNumLength.Size = new System.Drawing.Size(28, 13);
            this.lblNumLength.Text = "12位";
            // 
            // txtOrderNumber
            // 
            this.txtOrderNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOrderNumber.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtOrderNumber.Location = new System.Drawing.Point(74, 2);
            this.txtOrderNumber.Name = "txtOrderNumber";
            this.txtOrderNumber.Size = new System.Drawing.Size(140, 19);
            this.txtOrderNumber.TabIndex = 4;
            this.txtOrderNumber.TextChanged += new System.EventHandler(this.txtOrderNumber_TextChanged);
            this.txtOrderNumber.GotFocus += new System.EventHandler(this.txtOrderNumber_GotFocus);
            this.txtOrderNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOrderNumber_KeyDown);
            this.txtOrderNumber.LostFocus += new System.EventHandler(this.txtOrderNumber_LostFocus);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(10, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.Text = "运单号：";
            // 
            // pnlS
            // 
            this.pnlS.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlS.BackColor = System.Drawing.Color.Transparent;
            this.pnlS.Location = new System.Drawing.Point(0, 22);
            this.pnlS.Name = "pnlS";
            this.pnlS.Size = new System.Drawing.Size(249, 100);
            this.pnlS.Click += new System.EventHandler(this.pnlS_Click);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label8.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.label8.Location = new System.Drawing.Point(10, 117);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(239, 20);
            this.label8.Text = "—————————————————————";
            // 
            // pnlR
            // 
            this.pnlR.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlR.BackColor = System.Drawing.Color.Transparent;
            this.pnlR.Location = new System.Drawing.Point(0, 128);
            this.pnlR.Name = "pnlR";
            this.pnlR.Size = new System.Drawing.Size(249, 100);
            this.pnlR.Click += new System.EventHandler(this.pnlR_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnSubmit.Location = new System.Drawing.Point(139, 234);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(47, 20);
            this.btnSubmit.TabIndex = 26;
            this.btnSubmit.Text = "保 存";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnCancel.Location = new System.Drawing.Point(196, 234);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(45, 20);
            this.btnCancel.TabIndex = 27;
            this.btnCancel.Text = "返 回";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // nudCount
            // 
            this.nudCount.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.nudCount.Location = new System.Drawing.Point(30, 234);
            this.nudCount.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.nudCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCount.Name = "nudCount";
            this.nudCount.Size = new System.Drawing.Size(45, 20);
            this.nudCount.TabIndex = 25;
            this.nudCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCount.GotFocus += new System.EventHandler(this.nudCount_GotFocus);
            this.nudCount.LostFocus += new System.EventHandler(this.nudCount_LostFocus);
            this.nudCount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.nudCount_KeyDown);
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label16.Location = new System.Drawing.Point(81, 237);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(23, 16);
            this.label16.Text = "件";
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label15.Location = new System.Drawing.Point(10, 237);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(23, 16);
            this.label15.Text = "共";
            // 
            // UCOrderInput1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.nudCount);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.pnlR);
            this.Controls.Add(this.pnlS);
            this.Controls.Add(this.lblNumLength);
            this.Controls.Add(this.txtOrderNumber);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label8);
            this.Name = "UCOrderInput1";
            this.Size = new System.Drawing.Size(249, 287);
            this.Click += new System.EventHandler(this.UCOrderInput1_Click);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblNumLength;
        private System.Windows.Forms.TextBox txtOrderNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlS;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel pnlR;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.NumericUpDown nudCount;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
    }
}
