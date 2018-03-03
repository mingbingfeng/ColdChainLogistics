namespace C2LP.PDA.APP
{
    partial class UCScanRecord
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
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.txtNumber = new System.Windows.Forms.TextBox();
            this.chkNumber = new System.Windows.Forms.CheckBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.cboCustomer = new System.Windows.Forms.ComboBox();
            this.cboType = new System.Windows.Forms.ComboBox();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.txtRecord = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnClearScanRecord = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
            this.chkSearch = new System.Windows.Forms.CheckBox();
            this.pnlSearch.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlSearch
            // 
            this.pnlSearch.BackColor = System.Drawing.Color.Transparent;
            this.pnlSearch.Controls.Add(this.txtNumber);
            this.pnlSearch.Controls.Add(this.chkNumber);
            this.pnlSearch.Controls.Add(this.btnSearch);
            this.pnlSearch.Controls.Add(this.cboCustomer);
            this.pnlSearch.Controls.Add(this.cboType);
            this.pnlSearch.Controls.Add(this.dtpEnd);
            this.pnlSearch.Controls.Add(this.dtpStart);
            this.pnlSearch.Controls.Add(this.label3);
            this.pnlSearch.Controls.Add(this.label2);
            this.pnlSearch.Controls.Add(this.label1);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Location = new System.Drawing.Point(0, 0);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Size = new System.Drawing.Size(249, 98);
            // 
            // txtNumber
            // 
            this.txtNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtNumber.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtNumber.Location = new System.Drawing.Point(112, 74);
            this.txtNumber.Name = "txtNumber";
            this.txtNumber.Size = new System.Drawing.Size(127, 19);
            this.txtNumber.TabIndex = 11;
            this.txtNumber.TextChanged += new System.EventHandler(this.txtNumber_TextChanged);
            this.txtNumber.GotFocus += new System.EventHandler(this.txtNumber_GotFocus);
            this.txtNumber.LostFocus += new System.EventHandler(this.txtNumber_LostFocus);
            // 
            // chkNumber
            // 
            this.chkNumber.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.chkNumber.Location = new System.Drawing.Point(6, 73);
            this.chkNumber.Name = "chkNumber";
            this.chkNumber.Size = new System.Drawing.Size(120, 20);
            this.chkNumber.TabIndex = 12;
            this.chkNumber.Text = "仅根据单号查询";
            this.chkNumber.CheckStateChanged += new System.EventHandler(this.chkNumber_CheckStateChanged);
            // 
            // btnSearch
            // 
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Location = new System.Drawing.Point(192, 3);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(47, 49);
            this.btnSearch.TabIndex = 9;
            this.btnSearch.Text = "查询";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // cboCustomer
            // 
            this.cboCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboCustomer.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboCustomer.Location = new System.Drawing.Point(135, 53);
            this.cboCustomer.Name = "cboCustomer";
            this.cboCustomer.Size = new System.Drawing.Size(104, 19);
            this.cboCustomer.TabIndex = 8;
            this.cboCustomer.SelectedIndexChanged += new System.EventHandler(this.cboCustomer_SelectedIndexChanged);
            this.cboCustomer.GotFocus += new System.EventHandler(this.cboCustomer_GotFocus);
            // 
            // cboType
            // 
            this.cboType.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboType.Items.Add("全部操作");
            this.cboType.Items.Add("第三方运单");
            this.cboType.Items.Add("运单录入");
            this.cboType.Items.Add("节点扫描");
            this.cboType.Items.Add("运抵卸车");
            this.cboType.Items.Add("签收拍照");
            this.cboType.Items.Add("其它");
            this.cboType.Location = new System.Drawing.Point(59, 53);
            this.cboType.Name = "cboType";
            this.cboType.Size = new System.Drawing.Size(70, 19);
            this.cboType.TabIndex = 8;
            this.cboType.LostFocus += new System.EventHandler(this.cboType_LostFocus);
            this.cboType.SelectedIndexChanged += new System.EventHandler(this.cboType_SelectedIndexChanged);
            // 
            // dtpEnd
            // 
            this.dtpEnd.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dtpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpEnd.Location = new System.Drawing.Point(59, 28);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(127, 24);
            this.dtpEnd.TabIndex = 4;
            this.dtpEnd.ValueChanged += new System.EventHandler(this.dtpEnd_ValueChanged);
            this.dtpEnd.GotFocus += new System.EventHandler(this.dtpEnd_GotFocus);
            this.dtpEnd.LostFocus += new System.EventHandler(this.dtpEnd_LostFocus);
            // 
            // dtpStart
            // 
            this.dtpStart.CalendarFont = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.dtpStart.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dtpStart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStart.Location = new System.Drawing.Point(59, 3);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(127, 24);
            this.dtpStart.TabIndex = 3;
            this.dtpStart.ValueChanged += new System.EventHandler(this.dtpStart_ValueChanged);
            this.dtpStart.GotFocus += new System.EventHandler(this.dtpStart_GotFocus);
            this.dtpStart.LostFocus += new System.EventHandler(this.dtpStart_LostFocus);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label3.Location = new System.Drawing.Point(1, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 20);
            this.label3.Text = "筛选条件:";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(1, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 20);
            this.label2.Text = "结束时间:";
            this.label2.ParentChanged += new System.EventHandler(this.label2_ParentChanged);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(1, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 20);
            this.label1.Text = "开始时间:";
            this.label1.ParentChanged += new System.EventHandler(this.label1_ParentChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.txtRecord);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 98);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(249, 196);
            // 
            // txtRecord
            // 
            this.txtRecord.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtRecord.Font = new System.Drawing.Font("Tahoma", 6F, System.Drawing.FontStyle.Regular);
            this.txtRecord.Location = new System.Drawing.Point(0, 0);
            this.txtRecord.Multiline = true;
            this.txtRecord.Name = "txtRecord";
            this.txtRecord.ReadOnly = true;
            this.txtRecord.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtRecord.Size = new System.Drawing.Size(249, 172);
            this.txtRecord.TabIndex = 3;
            this.txtRecord.WordWrap = false;
            this.txtRecord.TextChanged += new System.EventHandler(this.txtRecord_TextChanged);
            this.txtRecord.GotFocus += new System.EventHandler(this.txtRecord_GotFocus);
            this.txtRecord.LostFocus += new System.EventHandler(this.txtRecord_LostFocus);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.Add(this.btnClearScanRecord);
            this.panel3.Controls.Add(this.btnReturn);
            this.panel3.Controls.Add(this.chkSearch);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 172);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(249, 24);
            // 
            // btnClearScanRecord
            // 
            this.btnClearScanRecord.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearScanRecord.Location = new System.Drawing.Point(139, 0);
            this.btnClearScanRecord.Name = "btnClearScanRecord";
            this.btnClearScanRecord.Size = new System.Drawing.Size(47, 24);
            this.btnClearScanRecord.TabIndex = 1;
            this.btnClearScanRecord.Text = "删除";
            this.btnClearScanRecord.Click += new System.EventHandler(this.btnClearScanRecord_Click);
            // 
            // btnReturn
            // 
            this.btnReturn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReturn.Location = new System.Drawing.Point(192, 0);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(47, 24);
            this.btnReturn.TabIndex = 1;
            this.btnReturn.Text = "返回";
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            // 
            // chkSearch
            // 
            this.chkSearch.Checked = true;
            this.chkSearch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSearch.Dock = System.Windows.Forms.DockStyle.Left;
            this.chkSearch.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.chkSearch.Location = new System.Drawing.Point(0, 0);
            this.chkSearch.Name = "chkSearch";
            this.chkSearch.Size = new System.Drawing.Size(129, 24);
            this.chkSearch.TabIndex = 0;
            this.chkSearch.Text = "显示/隐藏查询选项";
            this.chkSearch.CheckStateChanged += new System.EventHandler(this.chkSearch_CheckStateChanged);
            // 
            // UCScanRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.pnlSearch);
            this.Name = "UCScanRecord";
            this.Size = new System.Drawing.Size(249, 294);
            this.pnlSearch.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlSearch;
        private System.Windows.Forms.DateTimePicker dtpEnd;
        private System.Windows.Forms.DateTimePicker dtpStart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboType;
        private System.Windows.Forms.ComboBox cboCustomer;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox txtNumber;
        private System.Windows.Forms.CheckBox chkNumber;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox chkSearch;
        private System.Windows.Forms.TextBox txtRecord;
        private System.Windows.Forms.Button btnReturn;
        private System.Windows.Forms.Button btnClearScanRecord;

    }
}
