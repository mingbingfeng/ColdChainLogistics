namespace C2LP.ColdStorageDataHubClient
{
    partial class FrmBindPort
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.tvAiList = new System.Windows.Forms.TreeView();
            this.dgvBindList = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvBindList)).BeginInit();
            this.SuspendLayout();
            // 
            // tvAiList
            // 
            this.tvAiList.Dock = System.Windows.Forms.DockStyle.Left;
            this.tvAiList.HideSelection = false;
            this.tvAiList.HotTracking = true;
            this.tvAiList.Location = new System.Drawing.Point(0, 0);
            this.tvAiList.Name = "tvAiList";
            this.tvAiList.ShowNodeToolTips = true;
            this.tvAiList.Size = new System.Drawing.Size(163, 420);
            this.tvAiList.TabIndex = 0;
            this.tvAiList.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvAiList_AfterSelect);
            // 
            // dgvBindList
            // 
            this.dgvBindList.AllowUserToAddRows = false;
            this.dgvBindList.AllowUserToDeleteRows = false;
            this.dgvBindList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvBindList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvBindList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvBindList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dgvBindList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvBindList.Location = new System.Drawing.Point(162, 0);
            this.dgvBindList.Name = "dgvBindList";
            this.dgvBindList.RowHeadersVisible = false;
            this.dgvBindList.RowTemplate.Height = 23;
            this.dgvBindList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvBindList.Size = new System.Drawing.Size(337, 377);
            this.dgvBindList.TabIndex = 1;
            this.dgvBindList.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvBindList_CellValueChanged);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "仓库";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "探头";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.HeaderText = "绑定ID";
            this.Column3.Name = "Column3";
            // 
            // btnSave
            // 
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSave.Location = new System.Drawing.Point(178, 387);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(414, 387);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "关闭";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // FrmBindPort
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(501, 420);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.dgvBindList);
            this.Controls.Add(this.tvAiList);
            this.Name = "FrmBindPort";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "探头绑定";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmBindPort_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgvBindList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvAiList;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        public System.Windows.Forms.DataGridView dgvBindList;
    }
}

