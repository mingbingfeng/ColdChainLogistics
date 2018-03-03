//using C2LP.PDA.APP.ScannerAPI;
using System;
namespace C2LP.PDA.APP
{
    partial class UCOrderInput
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
            ////移除当前的扫描响应事件处理方法
            //Scanner.GetScanner().OnGetBarcodeEvent -= Scanner_OnGetBarcodeEvent;
            ////使扫描不可用,即不可发红外光
            //Scanner.GetScanner().Close();
            FrmParent.IputChangeEvent -= new FrmParent.InputPnlChangeDelegate(FrmParent_IputChangeEvent);
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
            this.txtOrderNumber = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cboSenderPId = new System.Windows.Forms.ComboBox();
            this.cboSenderCId = new System.Windows.Forms.ComboBox();
            this.cboSenderAId = new System.Windows.Forms.ComboBox();
            this.txtSenderAddress = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboSenderCustomer = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtSenderName = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtSenderPhone = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txtReceiverName = new System.Windows.Forms.TextBox();
            this.txtReceiverPhone = new System.Windows.Forms.TextBox();
            this.txtReceiverAddress = new System.Windows.Forms.TextBox();
            this.cboReceiverCustomer = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.nudCount = new System.Windows.Forms.NumericUpDown();
            this.label16 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.cboReceiverPId = new System.Windows.Forms.ComboBox();
            this.cboReceiverCId = new System.Windows.Forms.ComboBox();
            this.cboReceiverAId = new System.Windows.Forms.ComboBox();
            this.pnlInput = new System.Windows.Forms.Panel();
            this.lblNumLength = new System.Windows.Forms.Label();
            this.tmLoad = new System.Windows.Forms.Timer();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(10, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 13);
            this.label1.Text = "运单号：";
            // 
            // txtOrderNumber
            // 
            this.txtOrderNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOrderNumber.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtOrderNumber.Location = new System.Drawing.Point(74, 4);
            this.txtOrderNumber.Name = "txtOrderNumber";
            this.txtOrderNumber.Size = new System.Drawing.Size(140, 19);
            this.txtOrderNumber.TabIndex = 1;
            this.txtOrderNumber.TextChanged += new System.EventHandler(this.txtOrderNumber_TextChanged);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(10, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 15);
            this.label2.Text = "发货地：";
            // 
            // cboSenderPId
            // 
            this.cboSenderPId.DisplayMember = "Name";
            this.cboSenderPId.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboSenderPId.Location = new System.Drawing.Point(74, 24);
            this.cboSenderPId.Name = "cboSenderPId";
            this.cboSenderPId.Size = new System.Drawing.Size(59, 19);
            this.cboSenderPId.TabIndex = 4;
            this.cboSenderPId.ValueMember = "Id";
            // 
            // cboSenderCId
            // 
            this.cboSenderCId.DisplayMember = "Name";
            this.cboSenderCId.Enabled = false;
            this.cboSenderCId.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboSenderCId.Location = new System.Drawing.Point(131, 24);
            this.cboSenderCId.Name = "cboSenderCId";
            this.cboSenderCId.Size = new System.Drawing.Size(55, 19);
            this.cboSenderCId.TabIndex = 4;
            this.cboSenderCId.Tag = "2";
            this.cboSenderCId.ValueMember = "Id";
            this.cboSenderCId.SelectedIndexChanged += new System.EventHandler(this.cboSenderCId_SelectedIndexChanged);
            // 
            // cboSenderAId
            // 
            this.cboSenderAId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSenderAId.DisplayMember = "Name";
            this.cboSenderAId.Enabled = false;
            this.cboSenderAId.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboSenderAId.Location = new System.Drawing.Point(181, 24);
            this.cboSenderAId.Name = "cboSenderAId";
            this.cboSenderAId.Size = new System.Drawing.Size(60, 19);
            this.cboSenderAId.TabIndex = 4;
            this.cboSenderAId.Tag = "22";
            this.cboSenderAId.ValueMember = "Id";
            // 
            // txtSenderAddress
            // 
            this.txtSenderAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSenderAddress.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtSenderAddress.Location = new System.Drawing.Point(10, 43);
            this.txtSenderAddress.Name = "txtSenderAddress";
            this.txtSenderAddress.Size = new System.Drawing.Size(231, 19);
            this.txtSenderAddress.TabIndex = 7;
            this.txtSenderAddress.GotFocus += new System.EventHandler(this.control_GotFocus);
            this.txtSenderAddress.LostFocus += new System.EventHandler(this.control_LostFocus);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label5.Location = new System.Drawing.Point(10, 64);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 14);
            this.label5.Text = "发货单位：";
            // 
            // cboSenderCustomer
            // 
            this.cboSenderCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSenderCustomer.Enabled = false;
            this.cboSenderCustomer.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboSenderCustomer.Location = new System.Drawing.Point(74, 63);
            this.cboSenderCustomer.Name = "cboSenderCustomer";
            this.cboSenderCustomer.Size = new System.Drawing.Size(167, 19);
            this.cboSenderCustomer.TabIndex = 9;
            this.cboSenderCustomer.SelectedIndexChanged += new System.EventHandler(this.cboSenderCustomer_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label6.Location = new System.Drawing.Point(10, 83);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 14);
            this.label6.Text = "发货人：";
            // 
            // txtSenderName
            // 
            this.txtSenderName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSenderName.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtSenderName.Location = new System.Drawing.Point(74, 82);
            this.txtSenderName.Name = "txtSenderName";
            this.txtSenderName.Size = new System.Drawing.Size(167, 19);
            this.txtSenderName.TabIndex = 1;
            this.txtSenderName.GotFocus += new System.EventHandler(this.control_GotFocus);
            this.txtSenderName.LostFocus += new System.EventHandler(this.control_LostFocus);
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label7.Location = new System.Drawing.Point(10, 103);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 16);
            this.label7.Text = "发货电话：";
            // 
            // txtSenderPhone
            // 
            this.txtSenderPhone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSenderPhone.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtSenderPhone.Location = new System.Drawing.Point(74, 102);
            this.txtSenderPhone.Name = "txtSenderPhone";
            this.txtSenderPhone.Size = new System.Drawing.Size(167, 19);
            this.txtSenderPhone.TabIndex = 1;
            this.txtSenderPhone.LostFocus += new System.EventHandler(this.control_LostFocus);
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label8.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.label8.Location = new System.Drawing.Point(10, 116);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(239, 20);
            this.label8.Text = "—————————————————————";
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label9.Location = new System.Drawing.Point(10, 127);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 16);
            this.label9.Text = "收货地：";
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label10.Location = new System.Drawing.Point(10, 167);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 16);
            this.label10.Text = "收货单位：";
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label11.Location = new System.Drawing.Point(10, 186);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 16);
            this.label11.Text = "收货人：";
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label12.Location = new System.Drawing.Point(10, 206);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 16);
            this.label12.Text = "收货电话：";
            // 
            // txtReceiverName
            // 
            this.txtReceiverName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReceiverName.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtReceiverName.Location = new System.Drawing.Point(74, 185);
            this.txtReceiverName.Name = "txtReceiverName";
            this.txtReceiverName.Size = new System.Drawing.Size(167, 19);
            this.txtReceiverName.TabIndex = 1;
            this.txtReceiverName.GotFocus += new System.EventHandler(this.control_GotFocus);
            this.txtReceiverName.LostFocus += new System.EventHandler(this.control_LostFocus);
            // 
            // txtReceiverPhone
            // 
            this.txtReceiverPhone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReceiverPhone.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtReceiverPhone.Location = new System.Drawing.Point(74, 205);
            this.txtReceiverPhone.Name = "txtReceiverPhone";
            this.txtReceiverPhone.Size = new System.Drawing.Size(167, 19);
            this.txtReceiverPhone.TabIndex = 1;
            this.txtReceiverPhone.LostFocus += new System.EventHandler(this.control_LostFocus);
            // 
            // txtReceiverAddress
            // 
            this.txtReceiverAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtReceiverAddress.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtReceiverAddress.Location = new System.Drawing.Point(10, 145);
            this.txtReceiverAddress.Name = "txtReceiverAddress";
            this.txtReceiverAddress.Size = new System.Drawing.Size(231, 19);
            this.txtReceiverAddress.TabIndex = 7;
            this.txtReceiverAddress.GotFocus += new System.EventHandler(this.control_GotFocus);
            this.txtReceiverAddress.LostFocus += new System.EventHandler(this.control_LostFocus);
            // 
            // cboReceiverCustomer
            // 
            this.cboReceiverCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboReceiverCustomer.Enabled = false;
            this.cboReceiverCustomer.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboReceiverCustomer.Location = new System.Drawing.Point(74, 165);
            this.cboReceiverCustomer.Name = "cboReceiverCustomer";
            this.cboReceiverCustomer.Size = new System.Drawing.Size(167, 19);
            this.cboReceiverCustomer.TabIndex = 9;
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label15.Location = new System.Drawing.Point(10, 229);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(23, 16);
            this.label15.Text = "共";
            // 
            // nudCount
            // 
            this.nudCount.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.nudCount.Location = new System.Drawing.Point(30, 226);
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
            this.nudCount.TabIndex = 20;
            this.nudCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label16.Location = new System.Drawing.Point(81, 229);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(23, 16);
            this.label16.Text = "件";
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Enabled = false;
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnCancel.Location = new System.Drawing.Point(196, 226);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(45, 20);
            this.btnCancel.TabIndex = 22;
            this.btnCancel.Text = "返 回";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.Enabled = false;
            this.btnSubmit.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnSubmit.Location = new System.Drawing.Point(139, 226);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(47, 20);
            this.btnSubmit.TabIndex = 22;
            this.btnSubmit.Text = "保 存";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // cboReceiverPId
            // 
            this.cboReceiverPId.DisplayMember = "Name";
            this.cboReceiverPId.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboReceiverPId.Location = new System.Drawing.Point(74, 126);
            this.cboReceiverPId.Name = "cboReceiverPId";
            this.cboReceiverPId.Size = new System.Drawing.Size(59, 19);
            this.cboReceiverPId.TabIndex = 4;
            this.cboReceiverPId.ValueMember = "Id";
            // 
            // cboReceiverCId
            // 
            this.cboReceiverCId.DisplayMember = "Name";
            this.cboReceiverCId.Enabled = false;
            this.cboReceiverCId.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboReceiverCId.Location = new System.Drawing.Point(129, 126);
            this.cboReceiverCId.Name = "cboReceiverCId";
            this.cboReceiverCId.Size = new System.Drawing.Size(56, 19);
            this.cboReceiverCId.TabIndex = 4;
            this.cboReceiverCId.Tag = "3";
            this.cboReceiverCId.ValueMember = "Id";
            this.cboReceiverCId.SelectedIndexChanged += new System.EventHandler(this.cboSenderCId_SelectedIndexChanged);
            // 
            // cboReceiverAId
            // 
            this.cboReceiverAId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboReceiverAId.DisplayMember = "Name";
            this.cboReceiverAId.Enabled = false;
            this.cboReceiverAId.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboReceiverAId.Location = new System.Drawing.Point(181, 126);
            this.cboReceiverAId.Name = "cboReceiverAId";
            this.cboReceiverAId.Size = new System.Drawing.Size(60, 19);
            this.cboReceiverAId.TabIndex = 4;
            this.cboReceiverAId.Tag = "33";
            this.cboReceiverAId.ValueMember = "Id";
            // 
            // pnlInput
            // 
            this.pnlInput.Location = new System.Drawing.Point(3, 407);
            this.pnlInput.Name = "pnlInput";
            this.pnlInput.Size = new System.Drawing.Size(231, 10);
            this.pnlInput.Visible = false;
            // 
            // lblNumLength
            // 
            this.lblNumLength.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblNumLength.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblNumLength.Location = new System.Drawing.Point(216, 7);
            this.lblNumLength.Name = "lblNumLength";
            this.lblNumLength.Size = new System.Drawing.Size(28, 13);
            this.lblNumLength.Text = "12位";
            // 
            // tmLoad
            // 
            this.tmLoad.Interval = 1000;
            this.tmLoad.Tick += new System.EventHandler(this.tmLoad_Tick);
            // 
            // UCOrderInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.txtSenderPhone);
            this.Controls.Add(this.lblNumLength);
            this.Controls.Add(this.pnlInput);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.nudCount);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.cboReceiverCustomer);
            this.Controls.Add(this.cboSenderCustomer);
            this.Controls.Add(this.txtReceiverAddress);
            this.Controls.Add(this.txtSenderAddress);
            this.Controls.Add(this.cboReceiverAId);
            this.Controls.Add(this.cboSenderAId);
            this.Controls.Add(this.cboReceiverCId);
            this.Controls.Add(this.cboSenderCId);
            this.Controls.Add(this.cboReceiverPId);
            this.Controls.Add(this.cboSenderPId);
            this.Controls.Add(this.txtReceiverPhone);
            this.Controls.Add(this.txtReceiverName);
            this.Controls.Add(this.txtSenderName);
            this.Controls.Add(this.txtOrderNumber);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label8);
            this.Name = "UCOrderInput";
            this.Size = new System.Drawing.Size(249, 425);
            this.Click += new System.EventHandler(this.UCOrderInput_Click);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtOrderNumber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboSenderPId;
        private System.Windows.Forms.ComboBox cboSenderCId;
        private System.Windows.Forms.ComboBox cboSenderAId;
        private System.Windows.Forms.TextBox txtSenderAddress;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboSenderCustomer;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSenderName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtSenderPhone;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txtReceiverName;
        private System.Windows.Forms.TextBox txtReceiverPhone;
        private System.Windows.Forms.TextBox txtReceiverAddress;
        private System.Windows.Forms.ComboBox cboReceiverCustomer;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.NumericUpDown nudCount;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.ComboBox cboReceiverPId;
        private System.Windows.Forms.ComboBox cboReceiverCId;
        private System.Windows.Forms.ComboBox cboReceiverAId;
        private System.Windows.Forms.Panel pnlInput;
        private System.Windows.Forms.Label lblNumLength;
        private System.Windows.Forms.Timer tmLoad;
    }
}
