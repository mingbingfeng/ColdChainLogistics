﻿namespace C2LP.PDA.APP
{
    partial class UCUploadConfig
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
            this.label1 = new System.Windows.Forms.Label();
            this.nudTime = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.nudOrderCount = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.nudNodeCount = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.chbTime = new System.Windows.Forms.CheckBox();
            this.chbOrderCount = new System.Windows.Forms.CheckBox();
            this.chbNodeCount = new System.Windows.Forms.CheckBox();
            this.chbAddress = new System.Windows.Forms.CheckBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.cboDefaultProvince = new System.Windows.Forms.ComboBox();
            this.chbDefaultProvince = new System.Windows.Forms.CheckBox();
            this.label9 = new System.Windows.Forms.Label();
            this.nudAutoReturnDelay = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.chkAutoReturnDelay = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(8, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 20);
            this.label1.Text = "上报周期：";
            // 
            // nudTime
            // 
            this.nudTime.Location = new System.Drawing.Point(91, 86);
            this.nudTime.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudTime.Minimum = new decimal(new int[] {
            3,
            0,
            0,
            0});
            this.nudTime.Name = "nudTime";
            this.nudTime.Size = new System.Drawing.Size(69, 24);
            this.nudTime.TabIndex = 1;
            this.nudTime.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(166, 90);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 20);
            this.label2.Text = "秒";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.Location = new System.Drawing.Point(8, 373);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(162, 20);
            this.label3.Text = "每次查询运单数量：";
            this.label3.Visible = false;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label4.Location = new System.Drawing.Point(166, 400);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 20);
            this.label4.Text = "条";
            this.label4.Visible = false;
            // 
            // nudOrderCount
            // 
            this.nudOrderCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nudOrderCount.Location = new System.Drawing.Point(91, 396);
            this.nudOrderCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudOrderCount.Name = "nudOrderCount";
            this.nudOrderCount.Size = new System.Drawing.Size(69, 24);
            this.nudOrderCount.TabIndex = 1;
            this.nudOrderCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudOrderCount.Visible = false;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label5.Location = new System.Drawing.Point(8, 451);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(162, 20);
            this.label5.Text = "每次查询节点数量：";
            this.label5.Visible = false;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label6.Location = new System.Drawing.Point(166, 478);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 20);
            this.label6.Text = "条";
            this.label6.Visible = false;
            // 
            // nudNodeCount
            // 
            this.nudNodeCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.nudNodeCount.Location = new System.Drawing.Point(91, 474);
            this.nudNodeCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudNodeCount.Name = "nudNodeCount";
            this.nudNodeCount.Size = new System.Drawing.Size(69, 24);
            this.nudNodeCount.TabIndex = 1;
            this.nudNodeCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudNodeCount.Visible = false;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(8, 16);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(162, 20);
            this.label7.Text = "服务器地址：";
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(8, 48);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(175, 23);
            this.txtAddress.TabIndex = 8;
            this.txtAddress.GotFocus += new System.EventHandler(this.txtAddress_GotFocus);
            this.txtAddress.LostFocus += new System.EventHandler(this.txtAddress_LostFocus);
            // 
            // chbTime
            // 
            this.chbTime.Location = new System.Drawing.Point(184, 88);
            this.chbTime.Name = "chbTime";
            this.chbTime.Size = new System.Drawing.Size(58, 20);
            this.chbTime.TabIndex = 9;
            this.chbTime.Text = "修改";
            this.chbTime.CheckStateChanged += new System.EventHandler(this.chbTime_CheckStateChanged);
            // 
            // chbOrderCount
            // 
            this.chbOrderCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbOrderCount.Location = new System.Drawing.Point(184, 398);
            this.chbOrderCount.Name = "chbOrderCount";
            this.chbOrderCount.Size = new System.Drawing.Size(58, 20);
            this.chbOrderCount.TabIndex = 9;
            this.chbOrderCount.Text = "修改";
            this.chbOrderCount.Visible = false;
            this.chbOrderCount.CheckStateChanged += new System.EventHandler(this.chbOrderCount_CheckStateChanged);
            // 
            // chbNodeCount
            // 
            this.chbNodeCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chbNodeCount.Location = new System.Drawing.Point(184, 476);
            this.chbNodeCount.Name = "chbNodeCount";
            this.chbNodeCount.Size = new System.Drawing.Size(58, 20);
            this.chbNodeCount.TabIndex = 9;
            this.chbNodeCount.Text = "修改";
            this.chbNodeCount.Visible = false;
            this.chbNodeCount.CheckStateChanged += new System.EventHandler(this.chbNodeCount_CheckStateChanged);
            // 
            // chbAddress
            // 
            this.chbAddress.Location = new System.Drawing.Point(184, 48);
            this.chbAddress.Name = "chbAddress";
            this.chbAddress.Size = new System.Drawing.Size(58, 20);
            this.chbAddress.TabIndex = 9;
            this.chbAddress.Text = "修改";
            this.chbAddress.CheckStateChanged += new System.EventHandler(this.chbAddress_CheckStateChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(139, 201);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 25);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "返回";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(23, 201);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(72, 25);
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(8, 127);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(87, 20);
            this.label8.Text = "默认省份：";
            // 
            // cboDefaultProvince
            // 
            this.cboDefaultProvince.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.cboDefaultProvince.Location = new System.Drawing.Point(91, 124);
            this.cboDefaultProvince.Name = "cboDefaultProvince";
            this.cboDefaultProvince.Size = new System.Drawing.Size(92, 23);
            this.cboDefaultProvince.TabIndex = 19;
            // 
            // chbDefaultProvince
            // 
            this.chbDefaultProvince.Location = new System.Drawing.Point(184, 125);
            this.chbDefaultProvince.Name = "chbDefaultProvince";
            this.chbDefaultProvince.Size = new System.Drawing.Size(58, 20);
            this.chbDefaultProvince.TabIndex = 9;
            this.chbDefaultProvince.Text = "修改";
            this.chbDefaultProvince.CheckStateChanged += new System.EventHandler(this.chbDefaultProvince_CheckStateChanged);
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(8, 164);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(87, 20);
            this.label9.Text = "返回延迟：";
            // 
            // nudAutoReturnDelay
            // 
            this.nudAutoReturnDelay.Location = new System.Drawing.Point(91, 160);
            this.nudAutoReturnDelay.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.nudAutoReturnDelay.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudAutoReturnDelay.Name = "nudAutoReturnDelay";
            this.nudAutoReturnDelay.Size = new System.Drawing.Size(69, 24);
            this.nudAutoReturnDelay.TabIndex = 1;
            this.nudAutoReturnDelay.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(166, 166);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(17, 20);
            this.label10.Text = "秒";
            // 
            // chkAutoReturnDelay
            // 
            this.chkAutoReturnDelay.Location = new System.Drawing.Point(184, 164);
            this.chkAutoReturnDelay.Name = "chkAutoReturnDelay";
            this.chkAutoReturnDelay.Size = new System.Drawing.Size(58, 20);
            this.chkAutoReturnDelay.TabIndex = 30;
            this.chkAutoReturnDelay.Text = "修改";
            // 
            // UCUploadConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.nudAutoReturnDelay);
            this.Controls.Add(this.chkAutoReturnDelay);
            this.Controls.Add(this.cboDefaultProvince);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.chbAddress);
            this.Controls.Add(this.chbNodeCount);
            this.Controls.Add(this.chbOrderCount);
            this.Controls.Add(this.chbDefaultProvince);
            this.Controls.Add(this.chbTime);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.nudNodeCount);
            this.Controls.Add(this.nudOrderCount);
            this.Controls.Add(this.nudTime);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Name = "UCUploadConfig";
            this.Size = new System.Drawing.Size(249, 511);
            this.Click += new System.EventHandler(this.UCUploadConfig_Click);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudOrderCount;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nudNodeCount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.CheckBox chbTime;
        private System.Windows.Forms.CheckBox chbOrderCount;
        private System.Windows.Forms.CheckBox chbNodeCount;
        private System.Windows.Forms.CheckBox chbAddress;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cboDefaultProvince;
        private System.Windows.Forms.CheckBox chbDefaultProvince;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown nudAutoReturnDelay;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox chkAutoReturnDelay;
    }
}
