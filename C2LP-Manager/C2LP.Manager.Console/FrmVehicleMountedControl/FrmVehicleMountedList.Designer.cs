namespace C2LP.Manager.Console.FrmVehicleMountedControl
{
    partial class FrmVehicleMountedList
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdbDisable = new System.Windows.Forms.RadioButton();
            this.rdbEnabled = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbStorageType = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.checDefault = new System.Windows.Forms.CheckBox();
            this.cmbPDA = new System.Windows.Forms.ComboBox();
            this.txtTelephone = new System.Windows.Forms.TextBox();
            this.txtPilot = new System.Windows.Forms.TextBox();
            this.txtLicensePlate = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmProbes = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmUpdated = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmColdChainData = new System.Windows.Forms.ToolStripMenuItem();
            this.winFormPager1 = new C2LP.Manager.Console.WinFormPager();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdbDisable);
            this.groupBox1.Controls.Add(this.rdbEnabled);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtRemark);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cmbStorageType);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.checDefault);
            this.groupBox1.Controls.Add(this.cmbPDA);
            this.groupBox1.Controls.Add(this.txtTelephone);
            this.groupBox1.Controls.Add(this.txtPilot);
            this.groupBox1.Controls.Add(this.txtLicensePlate);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(20, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(756, 172);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // rdbDisable
            // 
            this.rdbDisable.AutoSize = true;
            this.rdbDisable.Location = new System.Drawing.Point(223, 110);
            this.rdbDisable.Name = "rdbDisable";
            this.rdbDisable.Size = new System.Drawing.Size(47, 16);
            this.rdbDisable.TabIndex = 7;
            this.rdbDisable.Text = "停用";
            this.rdbDisable.UseVisualStyleBackColor = true;
            this.rdbDisable.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEnter_KeyDown);
            // 
            // rdbEnabled
            // 
            this.rdbEnabled.AutoSize = true;
            this.rdbEnabled.Checked = true;
            this.rdbEnabled.Location = new System.Drawing.Point(142, 110);
            this.rdbEnabled.Name = "rdbEnabled";
            this.rdbEnabled.Size = new System.Drawing.Size(47, 16);
            this.rdbEnabled.TabIndex = 7;
            this.rdbEnabled.TabStop = true;
            this.rdbEnabled.Text = "启用";
            this.rdbEnabled.UseVisualStyleBackColor = true;
            this.rdbEnabled.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEnter_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(49, 112);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 10;
            this.label8.Text = "车载状态";
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(513, 80);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(167, 21);
            this.txtRemark.TabIndex = 6;
            this.txtRemark.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEnter_KeyDown);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(423, 83);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 12);
            this.label7.TabIndex = 8;
            this.label7.Text = "备注(PAC地址)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(49, 83);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "存储类型";
            // 
            // cmbStorageType
            // 
            this.cmbStorageType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStorageType.FormattingEnabled = true;
            this.cmbStorageType.Location = new System.Drawing.Point(142, 80);
            this.cmbStorageType.Name = "cmbStorageType";
            this.cmbStorageType.Size = new System.Drawing.Size(167, 20);
            this.cmbStorageType.TabIndex = 5;
            this.cmbStorageType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEnter_KeyDown);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(141, 135);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "添加";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checDefault
            // 
            this.checDefault.AutoSize = true;
            this.checDefault.Checked = true;
            this.checDefault.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checDefault.Location = new System.Drawing.Point(315, 52);
            this.checDefault.Name = "checDefault";
            this.checDefault.Size = new System.Drawing.Size(72, 16);
            this.checDefault.TabIndex = 8;
            this.checDefault.Text = "默认设备";
            this.checDefault.UseVisualStyleBackColor = true;
            this.checDefault.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEnter_KeyDown);
            // 
            // cmbPDA
            // 
            this.cmbPDA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPDA.FormattingEnabled = true;
            this.cmbPDA.Location = new System.Drawing.Point(142, 50);
            this.cmbPDA.Name = "cmbPDA";
            this.cmbPDA.Size = new System.Drawing.Size(167, 20);
            this.cmbPDA.TabIndex = 3;
            this.cmbPDA.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEnter_KeyDown);
            // 
            // txtTelephone
            // 
            this.txtTelephone.Location = new System.Drawing.Point(513, 50);
            this.txtTelephone.Name = "txtTelephone";
            this.txtTelephone.Size = new System.Drawing.Size(167, 21);
            this.txtTelephone.TabIndex = 4;
            this.txtTelephone.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEnter_KeyDown);
            // 
            // txtPilot
            // 
            this.txtPilot.Location = new System.Drawing.Point(513, 20);
            this.txtPilot.Name = "txtPilot";
            this.txtPilot.Size = new System.Drawing.Size(167, 21);
            this.txtPilot.TabIndex = 2;
            this.txtPilot.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEnter_KeyDown);
            // 
            // txtLicensePlate
            // 
            this.txtLicensePlate.Location = new System.Drawing.Point(142, 20);
            this.txtLicensePlate.Name = "txtLicensePlate";
            this.txtLicensePlate.Size = new System.Drawing.Size(167, 21);
            this.txtLicensePlate.TabIndex = 1;
            this.txtLicensePlate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEnter_KeyDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(423, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "驾驶员电话";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(49, 53);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "PDA设备";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(423, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "车载驾驶员";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(49, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "车载系统车牌";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column6,
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column7,
            this.Column4,
            this.Column5});
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Location = new System.Drawing.Point(20, 193);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(756, 301);
            this.dataGridView1.TabIndex = 2;
            this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            // 
            // Column6
            // 
            this.Column6.DataPropertyName = "storageIdk__BackingField";
            this.Column6.HeaderText = "id";
            this.Column6.Name = "Column6";
            this.Column6.ReadOnly = true;
            this.Column6.Visible = false;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "storageNamek__BackingField";
            this.Column1.HeaderText = "车载车牌";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 150;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "driverk__BackingField";
            this.Column2.HeaderText = "驾驶员";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 150;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "driverTelk__BackingField";
            this.Column3.HeaderText = "驾驶员电话";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 150;
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "remarkk__BackingFiled";
            this.Column7.HeaderText = "备注";
            this.Column7.Name = "Column7";
            this.Column7.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "createAtk__BackingField";
            this.Column4.HeaderText = "添加时间";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 150;
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "Namek__BackingField";
            this.Column5.HeaderText = "绑定PDA名称";
            this.Column5.Name = "Column5";
            this.Column5.ReadOnly = true;
            this.Column5.Width = 150;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmProbes,
            this.tsmUpdated,
            this.tsmColdChainData});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 70);
            // 
            // tsmProbes
            // 
            this.tsmProbes.Name = "tsmProbes";
            this.tsmProbes.Size = new System.Drawing.Size(124, 22);
            this.tsmProbes.Text = "探头管理";
            this.tsmProbes.Click += new System.EventHandler(this.tsmProbes_Click);
            // 
            // tsmUpdated
            // 
            this.tsmUpdated.Name = "tsmUpdated";
            this.tsmUpdated.Size = new System.Drawing.Size(124, 22);
            this.tsmUpdated.Text = "更新";
            this.tsmUpdated.Click += new System.EventHandler(this.tsmUpdated_Click);
            // 
            // tsmColdChainData
            // 
            this.tsmColdChainData.Name = "tsmColdChainData";
            this.tsmColdChainData.Size = new System.Drawing.Size(124, 22);
            this.tsmColdChainData.Text = "冷链数据";
            this.tsmColdChainData.Click += new System.EventHandler(this.tsmColdChainData_Click);
            // 
            // winFormPager1
            // 
            this.winFormPager1.Location = new System.Drawing.Point(35, 500);
            this.winFormPager1.Name = "winFormPager1";
            this.winFormPager1.PageIndex = 1;
            this.winFormPager1.PageSize = 10;
            this.winFormPager1.RecordCount = 0;
            this.winFormPager1.Size = new System.Drawing.Size(705, 23);
            this.winFormPager1.TabIndex = 3;
            // 
            // FrmVehicleMountedList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 531);
            this.Controls.Add(this.winFormPager1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmVehicleMountedList";
            this.Load += new System.EventHandler(this.FrmVehicleMountedList_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checDefault;
        private System.Windows.Forms.ComboBox cmbPDA;
        private System.Windows.Forms.TextBox txtTelephone;
        private System.Windows.Forms.TextBox txtPilot;
        private System.Windows.Forms.TextBox txtLicensePlate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmProbes;
        private System.Windows.Forms.ToolStripMenuItem tsmUpdated;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbStorageType;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RadioButton rdbDisable;
        private System.Windows.Forms.RadioButton rdbEnabled;
        private WinFormPager winFormPager1;
        private System.Windows.Forms.ToolStripMenuItem tsmColdChainData;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
    }
}