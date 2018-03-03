namespace C2LP.Manager.Console.FrmWarehouseControl
{
    partial class FrmWarehouseList
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
            this.label6 = new System.Windows.Forms.Label();
            this.cmbStorageType = new System.Windows.Forms.ComboBox();
            this.checDefault = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.cmbPDA = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtWarehouseName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmProbes = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmBindingPDA = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmUpdated = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmDelete = new System.Windows.Forms.ToolStripMenuItem();
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
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.cmbStorageType);
            this.groupBox1.Controls.Add(this.checDefault);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.cmbPDA);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtWarehouseName);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(20, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(754, 177);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            // 
            // rdbDisable
            // 
            this.rdbDisable.AutoSize = true;
            this.rdbDisable.Location = new System.Drawing.Point(194, 124);
            this.rdbDisable.Name = "rdbDisable";
            this.rdbDisable.Size = new System.Drawing.Size(47, 16);
            this.rdbDisable.TabIndex = 4;
            this.rdbDisable.Text = "停用";
            this.rdbDisable.UseVisualStyleBackColor = true;
            this.rdbDisable.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEnter_KeyDown);
            // 
            // rdbEnabled
            // 
            this.rdbEnabled.AutoSize = true;
            this.rdbEnabled.Checked = true;
            this.rdbEnabled.Location = new System.Drawing.Point(107, 124);
            this.rdbEnabled.Name = "rdbEnabled";
            this.rdbEnabled.Size = new System.Drawing.Size(47, 16);
            this.rdbEnabled.TabIndex = 4;
            this.rdbEnabled.TabStop = true;
            this.rdbEnabled.Text = "启用";
            this.rdbEnabled.UseVisualStyleBackColor = true;
            this.rdbEnabled.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEnter_KeyDown);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(35, 126);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 13;
            this.label8.Text = "仓库状态";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(35, 92);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "存储类型";
            // 
            // cmbStorageType
            // 
            this.cmbStorageType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStorageType.FormattingEnabled = true;
            this.cmbStorageType.Location = new System.Drawing.Point(107, 89);
            this.cmbStorageType.Name = "cmbStorageType";
            this.cmbStorageType.Size = new System.Drawing.Size(167, 20);
            this.cmbStorageType.TabIndex = 3;
            this.cmbStorageType.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEnter_KeyDown);
            // 
            // checDefault
            // 
            this.checDefault.AutoSize = true;
            this.checDefault.Checked = true;
            this.checDefault.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checDefault.Location = new System.Drawing.Point(280, 56);
            this.checDefault.Name = "checDefault";
            this.checDefault.Size = new System.Drawing.Size(72, 16);
            this.checDefault.TabIndex = 5;
            this.checDefault.Text = "默认设备";
            this.checDefault.UseVisualStyleBackColor = true;
            this.checDefault.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEnter_KeyDown);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(107, 148);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(104, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "添加";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cmbPDA
            // 
            this.cmbPDA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPDA.FormattingEnabled = true;
            this.cmbPDA.Location = new System.Drawing.Point(107, 54);
            this.cmbPDA.Name = "cmbPDA";
            this.cmbPDA.Size = new System.Drawing.Size(167, 20);
            this.cmbPDA.TabIndex = 2;
            this.cmbPDA.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEnter_KeyDown);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 57);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "PDA设备";
            // 
            // txtWarehouseName
            // 
            this.txtWarehouseName.Location = new System.Drawing.Point(107, 20);
            this.txtWarehouseName.Name = "txtWarehouseName";
            this.txtWarehouseName.Size = new System.Drawing.Size(167, 21);
            this.txtWarehouseName.TabIndex = 1;
            this.txtWarehouseName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEnter_KeyDown);
            this.txtWarehouseName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtWarehouseName_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "仓库名称";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Location = new System.Drawing.Point(20, 200);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(754, 296);
            this.dataGridView1.TabIndex = 2;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "storageIdk__BackingField";
            this.Column1.HeaderText = "仓库编号";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 185;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "storageNamek__BackingField";
            this.Column2.HeaderText = "仓库名称";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 185;
            // 
            // Column3
            // 
            this.Column3.DataPropertyName = "namek__BackingField";
            this.Column3.HeaderText = "绑定PDA名称";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            this.Column3.Width = 185;
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "createAtk__BackingField";
            this.Column4.HeaderText = "添加时间";
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            this.Column4.Width = 185;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmProbes,
            this.tsmBindingPDA,
            this.tsmUpdated,
            this.tsmDelete,
            this.tsmColdChainData});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 114);
            // 
            // tsmProbes
            // 
            this.tsmProbes.Name = "tsmProbes";
            this.tsmProbes.Size = new System.Drawing.Size(124, 22);
            this.tsmProbes.Text = "探头管理";
            this.tsmProbes.Click += new System.EventHandler(this.tsmProbes_Click);
            // 
            // tsmBindingPDA
            // 
            this.tsmBindingPDA.Name = "tsmBindingPDA";
            this.tsmBindingPDA.Size = new System.Drawing.Size(124, 22);
            this.tsmBindingPDA.Text = "绑定PDA";
            this.tsmBindingPDA.Click += new System.EventHandler(this.tsmBindingPDA_Click);
            // 
            // tsmUpdated
            // 
            this.tsmUpdated.Name = "tsmUpdated";
            this.tsmUpdated.Size = new System.Drawing.Size(124, 22);
            this.tsmUpdated.Text = "更新";
            this.tsmUpdated.Click += new System.EventHandler(this.tsmUpdated_Click);
            // 
            // tsmDelete
            // 
            this.tsmDelete.Name = "tsmDelete";
            this.tsmDelete.Size = new System.Drawing.Size(124, 22);
            this.tsmDelete.Text = "删除";
            this.tsmDelete.Click += new System.EventHandler(this.tsmDelete_Click);
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
            this.winFormPager1.Size = new System.Drawing.Size(693, 23);
            this.winFormPager1.TabIndex = 3;
            // 
            // FrmWarehouseList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 531);
            this.Controls.Add(this.winFormPager1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmWarehouseList";
            this.Load += new System.EventHandler(this.FrmWarehouseList_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox checDefault;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cmbPDA;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtWarehouseName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmProbes;
        private System.Windows.Forms.ToolStripMenuItem tsmBindingPDA;
        private System.Windows.Forms.ToolStripMenuItem tsmUpdated;
        private System.Windows.Forms.ToolStripMenuItem tsmDelete;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbStorageType;
        private System.Windows.Forms.RadioButton rdbDisable;
        private System.Windows.Forms.RadioButton rdbEnabled;
        private System.Windows.Forms.Label label8;
        private WinFormPager winFormPager1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.ToolStripMenuItem tsmColdChainData;
    }
}