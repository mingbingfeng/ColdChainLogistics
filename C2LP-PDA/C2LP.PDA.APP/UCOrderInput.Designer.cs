using C2LP.PDA.APP.ScannerAPI;
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
            //移除当前的扫描响应事件处理方法
            Scanner.GetScanner().OnGetBarcodeEvent -= Scanner_OnGetBarcodeEvent;
            //使扫描不可用,即不可发红外光
            Scanner.GetScanner().Close();
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
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.Text = "运单号：";
            // 
            // txtOrderNumber
            // 
            this.txtOrderNumber.Location = new System.Drawing.Point(67, 4);
            this.txtOrderNumber.Name = "txtOrderNumber";
            this.txtOrderNumber.Size = new System.Drawing.Size(125, 23);
            this.txtOrderNumber.TabIndex = 1;
            this.txtOrderNumber.TextChanged += new System.EventHandler(this.txtOrderNumber_TextChanged);
            this.txtOrderNumber.GotFocus += new System.EventHandler(this.control_GotFocus);
            this.txtOrderNumber.LostFocus += new System.EventHandler(this.control_LostFocus);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(3, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 20);
            this.label2.Text = "发货地：";
            // 
            // cboSenderPId
            // 
            this.cboSenderPId.DisplayMember = "Name";
            this.cboSenderPId.Location = new System.Drawing.Point(67, 30);
            this.cboSenderPId.Name = "cboSenderPId";
            this.cboSenderPId.Size = new System.Drawing.Size(44, 23);
            this.cboSenderPId.TabIndex = 4;
            this.cboSenderPId.ValueMember = "Id";
            this.cboSenderPId.SelectedIndexChanged += new System.EventHandler(this.cboPId_SelectedIndexChanged);
            // 
            // cboSenderCId
            // 
            this.cboSenderCId.DisplayMember = "Name";
            this.cboSenderCId.Enabled = false;
            this.cboSenderCId.Location = new System.Drawing.Point(109, 30);
            this.cboSenderCId.Name = "cboSenderCId";
            this.cboSenderCId.Size = new System.Drawing.Size(60, 23);
            this.cboSenderCId.TabIndex = 4;
            this.cboSenderCId.Tag = "2";
            this.cboSenderCId.ValueMember = "Id";
            this.cboSenderCId.SelectedIndexChanged += new System.EventHandler(this.cboSenderCId_SelectedIndexChanged);
            // 
            // cboSenderAId
            // 
            this.cboSenderAId.DisplayMember = "Name";
            this.cboSenderAId.Enabled = false;
            this.cboSenderAId.Location = new System.Drawing.Point(167, 30);
            this.cboSenderAId.Name = "cboSenderAId";
            this.cboSenderAId.Size = new System.Drawing.Size(60, 23);
            this.cboSenderAId.TabIndex = 4;
            this.cboSenderAId.Tag = "22";
            this.cboSenderAId.ValueMember = "Id";
            this.cboSenderAId.SelectedIndexChanged += new System.EventHandler(this.cboSenderAId_SelectedIndexChanged);
            // 
            // txtSenderAddress
            // 
            this.txtSenderAddress.Location = new System.Drawing.Point(3, 57);
            this.txtSenderAddress.Name = "txtSenderAddress";
            this.txtSenderAddress.Size = new System.Drawing.Size(224, 23);
            this.txtSenderAddress.TabIndex = 7;
            this.txtSenderAddress.GotFocus += new System.EventHandler(this.control_GotFocus);
            this.txtSenderAddress.LostFocus += new System.EventHandler(this.control_LostFocus);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(3, 87);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 20);
            this.label5.Text = "发货单位：";
            // 
            // cboSenderCustomer
            // 
            this.cboSenderCustomer.Enabled = false;
            this.cboSenderCustomer.Location = new System.Drawing.Point(67, 84);
            this.cboSenderCustomer.Name = "cboSenderCustomer";
            this.cboSenderCustomer.Size = new System.Drawing.Size(160, 23);
            this.cboSenderCustomer.TabIndex = 9;
            this.cboSenderCustomer.SelectedIndexChanged += new System.EventHandler(this.cboSenderCustomer_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(3, 115);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 20);
            this.label6.Text = "发货人：";
            // 
            // txtSenderName
            // 
            this.txtSenderName.Location = new System.Drawing.Point(67, 112);
            this.txtSenderName.Name = "txtSenderName";
            this.txtSenderName.Size = new System.Drawing.Size(160, 23);
            this.txtSenderName.TabIndex = 1;
            this.txtSenderName.GotFocus += new System.EventHandler(this.control_GotFocus);
            this.txtSenderName.LostFocus += new System.EventHandler(this.control_LostFocus);
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(3, 142);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(80, 20);
            this.label7.Text = "发货电话：";
            // 
            // txtSenderPhone
            // 
            this.txtSenderPhone.Location = new System.Drawing.Point(67, 139);
            this.txtSenderPhone.Name = "txtSenderPhone";
            this.txtSenderPhone.Size = new System.Drawing.Size(160, 23);
            this.txtSenderPhone.TabIndex = 1;
            this.txtSenderPhone.LostFocus += new System.EventHandler(this.control_LostFocus);
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label8.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.label8.Location = new System.Drawing.Point(3, 157);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(237, 20);
            this.label8.Text = "—————————————————————";
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(3, 179);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 20);
            this.label9.Text = "收货地：";
            // 
            // label10
            // 
            this.label10.Location = new System.Drawing.Point(3, 235);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 20);
            this.label10.Text = "收货单位：";
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(3, 263);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 20);
            this.label11.Text = "收货人：";
            // 
            // label12
            // 
            this.label12.Location = new System.Drawing.Point(3, 291);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(80, 20);
            this.label12.Text = "收货电话：";
            // 
            // txtReceiverName
            // 
            this.txtReceiverName.Location = new System.Drawing.Point(67, 260);
            this.txtReceiverName.Name = "txtReceiverName";
            this.txtReceiverName.Size = new System.Drawing.Size(160, 23);
            this.txtReceiverName.TabIndex = 1;
            this.txtReceiverName.GotFocus += new System.EventHandler(this.control_GotFocus);
            this.txtReceiverName.LostFocus += new System.EventHandler(this.control_LostFocus);
            // 
            // txtReceiverPhone
            // 
            this.txtReceiverPhone.Location = new System.Drawing.Point(67, 288);
            this.txtReceiverPhone.Name = "txtReceiverPhone";
            this.txtReceiverPhone.Size = new System.Drawing.Size(160, 23);
            this.txtReceiverPhone.TabIndex = 1;
            this.txtReceiverPhone.LostFocus += new System.EventHandler(this.control_LostFocus);
            // 
            // txtReceiverAddress
            // 
            this.txtReceiverAddress.Location = new System.Drawing.Point(3, 204);
            this.txtReceiverAddress.Name = "txtReceiverAddress";
            this.txtReceiverAddress.Size = new System.Drawing.Size(224, 23);
            this.txtReceiverAddress.TabIndex = 7;
            this.txtReceiverAddress.GotFocus += new System.EventHandler(this.control_GotFocus);
            this.txtReceiverAddress.LostFocus += new System.EventHandler(this.control_LostFocus);
            // 
            // cboReceiverCustomer
            // 
            this.cboReceiverCustomer.Enabled = false;
            this.cboReceiverCustomer.Location = new System.Drawing.Point(67, 232);
            this.cboReceiverCustomer.Name = "cboReceiverCustomer";
            this.cboReceiverCustomer.Size = new System.Drawing.Size(160, 23);
            this.cboReceiverCustomer.TabIndex = 9;
            this.cboReceiverCustomer.SelectedIndexChanged += new System.EventHandler(this.cboReceiverCustomer_SelectedIndexChanged);
            // 
            // label15
            // 
            this.label15.Location = new System.Drawing.Point(3, 320);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(23, 20);
            this.label15.Text = "共";
            // 
            // nudCount
            // 
            this.nudCount.Location = new System.Drawing.Point(23, 316);
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
            this.nudCount.Size = new System.Drawing.Size(51, 24);
            this.nudCount.TabIndex = 20;
            this.nudCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudCount.GotFocus += new System.EventHandler(this.control_GotFocus);
            this.nudCount.LostFocus += new System.EventHandler(this.control_LostFocus);
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(75, 320);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(23, 20);
            this.label16.Text = "件";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(180, 316);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(47, 24);
            this.btnCancel.TabIndex = 22;
            this.btnCancel.Text = "返回";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(109, 316);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(47, 24);
            this.btnSubmit.TabIndex = 22;
            this.btnSubmit.Text = "保存";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // cboReceiverPId
            // 
            this.cboReceiverPId.DisplayMember = "Name";
            this.cboReceiverPId.Location = new System.Drawing.Point(67, 176);
            this.cboReceiverPId.Name = "cboReceiverPId";
            this.cboReceiverPId.Size = new System.Drawing.Size(44, 23);
            this.cboReceiverPId.TabIndex = 4;
            this.cboReceiverPId.ValueMember = "Id";
            this.cboReceiverPId.SelectedIndexChanged += new System.EventHandler(this.cboPId_SelectedIndexChanged);
            // 
            // cboReceiverCId
            // 
            this.cboReceiverCId.DisplayMember = "Name";
            this.cboReceiverCId.Enabled = false;
            this.cboReceiverCId.Location = new System.Drawing.Point(109, 176);
            this.cboReceiverCId.Name = "cboReceiverCId";
            this.cboReceiverCId.Size = new System.Drawing.Size(60, 23);
            this.cboReceiverCId.TabIndex = 4;
            this.cboReceiverCId.Tag = "3";
            this.cboReceiverCId.ValueMember = "Id";
            this.cboReceiverCId.SelectedIndexChanged += new System.EventHandler(this.cboSenderCId_SelectedIndexChanged);
            // 
            // cboReceiverAId
            // 
            this.cboReceiverAId.DisplayMember = "Name";
            this.cboReceiverAId.Enabled = false;
            this.cboReceiverAId.Location = new System.Drawing.Point(167, 176);
            this.cboReceiverAId.Name = "cboReceiverAId";
            this.cboReceiverAId.Size = new System.Drawing.Size(60, 23);
            this.cboReceiverAId.TabIndex = 4;
            this.cboReceiverAId.Tag = "33";
            this.cboReceiverAId.ValueMember = "Id";
            this.cboReceiverAId.SelectedIndexChanged += new System.EventHandler(this.cboSenderAId_SelectedIndexChanged);
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
            this.lblNumLength.Location = new System.Drawing.Point(196, 7);
            this.lblNumLength.Name = "lblNumLength";
            this.lblNumLength.Size = new System.Drawing.Size(38, 20);
            this.lblNumLength.Text = "12位";
            // 
            // UCOrderInput
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Transparent;
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
            this.Controls.Add(this.txtSenderPhone);
            this.Controls.Add(this.txtReceiverName);
            this.Controls.Add(this.txtSenderName);
            this.Controls.Add(this.txtOrderNumber);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
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
    }
}
