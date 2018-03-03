namespace C2LP.Manager.Console.FrmWaybillControl
{
    partial class FrmWaybillShow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbPhone = new System.Windows.Forms.Label();
            this.lbTelephone = new System.Windows.Forms.Label();
            this.lbConsignee = new System.Windows.Forms.Label();
            this.lbSender = new System.Windows.Forms.Label();
            this.lbWaybillNumber = new System.Windows.Forms.Label();
            this.lbWaybillState = new System.Windows.Forms.Label();
            this.lbConsigneeAddress = new System.Windows.Forms.Label();
            this.lbConsigneeUnit = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lbMailUnit = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.dgvWaybill_Node = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmColdChainData = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmSignPicture = new System.Windows.Forms.ToolStripMenuItem();
            this.label21 = new System.Windows.Forms.Label();
            this.winFormPager1 = new C2LP.Manager.Console.WinFormPager();
            this.btnRefurbish = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWaybill_Node)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label1.Location = new System.Drawing.Point(20, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "运单详情";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbPhone);
            this.groupBox1.Controls.Add(this.lbTelephone);
            this.groupBox1.Controls.Add(this.lbConsignee);
            this.groupBox1.Controls.Add(this.lbSender);
            this.groupBox1.Controls.Add(this.lbWaybillNumber);
            this.groupBox1.Controls.Add(this.lbWaybillState);
            this.groupBox1.Controls.Add(this.lbConsigneeAddress);
            this.groupBox1.Controls.Add(this.lbConsigneeUnit);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.lbMailUnit);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label17);
            this.groupBox1.Controls.Add(this.label16);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(20, 35);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(654, 196);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // lbPhone
            // 
            this.lbPhone.AutoSize = true;
            this.lbPhone.Location = new System.Drawing.Point(535, 59);
            this.lbPhone.Name = "lbPhone";
            this.lbPhone.Size = new System.Drawing.Size(47, 12);
            this.lbPhone.TabIndex = 1;
            this.lbPhone.Text = "xxxxxxx";
            // 
            // lbTelephone
            // 
            this.lbTelephone.AutoSize = true;
            this.lbTelephone.Location = new System.Drawing.Point(535, 26);
            this.lbTelephone.Name = "lbTelephone";
            this.lbTelephone.Size = new System.Drawing.Size(47, 12);
            this.lbTelephone.TabIndex = 1;
            this.lbTelephone.Text = "xxxxxxx";
            // 
            // lbConsignee
            // 
            this.lbConsignee.AutoSize = true;
            this.lbConsignee.Location = new System.Drawing.Point(336, 59);
            this.lbConsignee.Name = "lbConsignee";
            this.lbConsignee.Size = new System.Drawing.Size(47, 12);
            this.lbConsignee.TabIndex = 1;
            this.lbConsignee.Text = "xxxxxxx";
            // 
            // lbSender
            // 
            this.lbSender.AutoSize = true;
            this.lbSender.Location = new System.Drawing.Point(336, 26);
            this.lbSender.Name = "lbSender";
            this.lbSender.Size = new System.Drawing.Size(47, 12);
            this.lbSender.TabIndex = 1;
            this.lbSender.Text = "xxxxxxx";
            // 
            // lbWaybillNumber
            // 
            this.lbWaybillNumber.AutoSize = true;
            this.lbWaybillNumber.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbWaybillNumber.Location = new System.Drawing.Point(89, 157);
            this.lbWaybillNumber.Name = "lbWaybillNumber";
            this.lbWaybillNumber.Size = new System.Drawing.Size(54, 12);
            this.lbWaybillNumber.TabIndex = 1;
            this.lbWaybillNumber.Text = "xxxxxxx";
            // 
            // lbWaybillState
            // 
            this.lbWaybillState.AutoSize = true;
            this.lbWaybillState.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbWaybillState.Location = new System.Drawing.Point(89, 129);
            this.lbWaybillState.Name = "lbWaybillState";
            this.lbWaybillState.Size = new System.Drawing.Size(54, 12);
            this.lbWaybillState.TabIndex = 1;
            this.lbWaybillState.Text = "xxxxxxx";
            // 
            // lbConsigneeAddress
            // 
            this.lbConsigneeAddress.AutoSize = true;
            this.lbConsigneeAddress.Location = new System.Drawing.Point(89, 92);
            this.lbConsigneeAddress.Name = "lbConsigneeAddress";
            this.lbConsigneeAddress.Size = new System.Drawing.Size(47, 12);
            this.lbConsigneeAddress.TabIndex = 1;
            this.lbConsigneeAddress.Text = "xxxxxxx";
            // 
            // lbConsigneeUnit
            // 
            this.lbConsigneeUnit.AutoSize = true;
            this.lbConsigneeUnit.Location = new System.Drawing.Point(89, 59);
            this.lbConsigneeUnit.Name = "lbConsigneeUnit";
            this.lbConsigneeUnit.Size = new System.Drawing.Size(47, 12);
            this.lbConsigneeUnit.TabIndex = 1;
            this.lbConsigneeUnit.Text = "xxxxxxx";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(464, 59);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 12);
            this.label10.TabIndex = 0;
            this.label10.Text = "收货人电话：";
            // 
            // lbMailUnit
            // 
            this.lbMailUnit.AutoSize = true;
            this.lbMailUnit.Location = new System.Drawing.Point(89, 26);
            this.lbMailUnit.Name = "lbMailUnit";
            this.lbMailUnit.Size = new System.Drawing.Size(47, 12);
            this.lbMailUnit.TabIndex = 1;
            this.lbMailUnit.Text = "xxxxxxx";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(277, 59);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(53, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "收货人：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(464, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "寄件人电话：";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(18, 157);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(65, 12);
            this.label17.TabIndex = 0;
            this.label17.Text = "运单编号：";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(18, 129);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(65, 12);
            this.label16.TabIndex = 0;
            this.label16.Text = "运单状态：";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(18, 92);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(77, 12);
            this.label14.TabIndex = 0;
            this.label14.Text = "收货人地址：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(18, 59);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "收货人单位：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(277, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "寄件人：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "寄件单位：";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label20.Location = new System.Drawing.Point(20, 245);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(76, 16);
            this.label20.TabIndex = 0;
            this.label20.Text = "物流节点";
            // 
            // dgvWaybill_Node
            // 
            this.dgvWaybill_Node.AllowUserToAddRows = false;
            this.dgvWaybill_Node.AllowUserToDeleteRows = false;
            this.dgvWaybill_Node.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWaybill_Node.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.dgvWaybill_Node.ContextMenuStrip = this.contextMenuStrip1;
            this.dgvWaybill_Node.Location = new System.Drawing.Point(20, 281);
            this.dgvWaybill_Node.Name = "dgvWaybill_Node";
            this.dgvWaybill_Node.ReadOnly = true;
            this.dgvWaybill_Node.RowHeadersVisible = false;
            this.dgvWaybill_Node.RowTemplate.Height = 23;
            this.dgvWaybill_Node.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvWaybill_Node.Size = new System.Drawing.Size(654, 219);
            this.dgvWaybill_Node.TabIndex = 2;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "operateAtk__BackingField";
            this.Column1.HeaderText = "时间";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 200;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "contentk__BackingField";
            this.Column2.HeaderText = "内容";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 450;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmColdChainData,
            this.tsmSignPicture});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 48);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // tsmColdChainData
            // 
            this.tsmColdChainData.Name = "tsmColdChainData";
            this.tsmColdChainData.Size = new System.Drawing.Size(124, 22);
            this.tsmColdChainData.Text = "冷链数据";
            this.tsmColdChainData.Click += new System.EventHandler(this.tsmColdChainData_Click);
            // 
            // tsmSignPicture
            // 
            this.tsmSignPicture.Name = "tsmSignPicture";
            this.tsmSignPicture.Size = new System.Drawing.Size(124, 22);
            this.tsmSignPicture.Text = "签收图片";
            this.tsmSignPicture.Click += new System.EventHandler(this.tsmSignPicture_Click);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(20, 266);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(653, 12);
            this.label21.TabIndex = 3;
            this.label21.Text = "---------------------------------------------------------------------------------" +
    "---------------------------";
            // 
            // winFormPager1
            // 
            this.winFormPager1.Location = new System.Drawing.Point(9, 506);
            this.winFormPager1.Name = "winFormPager1";
            this.winFormPager1.PageIndex = 1;
            this.winFormPager1.PageSize = 10;
            this.winFormPager1.RecordCount = 0;
            this.winFormPager1.Size = new System.Drawing.Size(682, 23);
            this.winFormPager1.TabIndex = 4;
            // 
            // btnRefurbish
            // 
            this.btnRefurbish.Location = new System.Drawing.Point(598, 244);
            this.btnRefurbish.Name = "btnRefurbish";
            this.btnRefurbish.Size = new System.Drawing.Size(75, 23);
            this.btnRefurbish.TabIndex = 5;
            this.btnRefurbish.Text = "刷新";
            this.btnRefurbish.UseVisualStyleBackColor = true;
            this.btnRefurbish.Click += new System.EventHandler(this.btnRefurbish_Click);
            // 
            // FrmWaybillShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 536);
            this.Controls.Add(this.btnRefurbish);
            this.Controls.Add(this.winFormPager1);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.dgvWaybill_Node);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmWaybillShow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "物流管理";
            this.Load += new System.EventHandler(this.FrmWaybillShow_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWaybill_Node)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbPhone;
        private System.Windows.Forms.Label lbTelephone;
        private System.Windows.Forms.Label lbConsignee;
        private System.Windows.Forms.Label lbSender;
        private System.Windows.Forms.Label lbWaybillNumber;
        private System.Windows.Forms.Label lbWaybillState;
        private System.Windows.Forms.Label lbConsigneeAddress;
        private System.Windows.Forms.Label lbConsigneeUnit;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lbMailUnit;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.DataGridView dgvWaybill_Node;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmColdChainData;
        private System.Windows.Forms.ToolStripMenuItem tsmSignPicture;
        private WinFormPager winFormPager1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Button btnRefurbish;
    }
}