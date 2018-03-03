namespace C2LP.ColdStorageDataHubClient
{
    partial class FrmMain
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
            this.gbUploadConfig = new System.Windows.Forms.GroupBox();
            this.btnAppConfig = new System.Windows.Forms.Button();
            this.btnEnd = new System.Windows.Forms.Button();
            this.btnBind = new System.Windows.Forms.Button();
            this.btnBegin = new System.Windows.Forms.Button();
            this.nudUploadLimit = new System.Windows.Forms.NumericUpDown();
            this.nudASpace = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.nudUploadInteval = new System.Windows.Forms.NumericUpDown();
            this.nudNSpace = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gbUploadTime = new System.Windows.Forms.GroupBox();
            this.dgvUploadTime = new System.Windows.Forms.DataGridView();
            this.CId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CProjectNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CNetId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CLastUploadTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.gbUploadLog = new System.Windows.Forms.GroupBox();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiOpenLogPath = new System.Windows.Forms.ToolStripMenuItem();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.gbUploadConfig.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudUploadLimit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudASpace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudUploadInteval)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNSpace)).BeginInit();
            this.gbUploadTime.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUploadTime)).BeginInit();
            this.gbUploadLog.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbUploadConfig
            // 
            this.gbUploadConfig.Controls.Add(this.btnAppConfig);
            this.gbUploadConfig.Controls.Add(this.btnEnd);
            this.gbUploadConfig.Controls.Add(this.btnBind);
            this.gbUploadConfig.Controls.Add(this.btnBegin);
            this.gbUploadConfig.Controls.Add(this.nudUploadLimit);
            this.gbUploadConfig.Controls.Add(this.nudASpace);
            this.gbUploadConfig.Controls.Add(this.label8);
            this.gbUploadConfig.Controls.Add(this.label4);
            this.gbUploadConfig.Controls.Add(this.nudUploadInteval);
            this.gbUploadConfig.Controls.Add(this.nudNSpace);
            this.gbUploadConfig.Controls.Add(this.label7);
            this.gbUploadConfig.Controls.Add(this.label3);
            this.gbUploadConfig.Controls.Add(this.label6);
            this.gbUploadConfig.Controls.Add(this.label2);
            this.gbUploadConfig.Controls.Add(this.label5);
            this.gbUploadConfig.Controls.Add(this.label1);
            this.gbUploadConfig.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbUploadConfig.Enabled = false;
            this.gbUploadConfig.Location = new System.Drawing.Point(0, 0);
            this.gbUploadConfig.Name = "gbUploadConfig";
            this.gbUploadConfig.Size = new System.Drawing.Size(804, 68);
            this.gbUploadConfig.TabIndex = 0;
            this.gbUploadConfig.TabStop = false;
            this.gbUploadConfig.Text = "上报参数";
            // 
            // btnAppConfig
            // 
            this.btnAppConfig.Location = new System.Drawing.Point(515, 39);
            this.btnAppConfig.Name = "btnAppConfig";
            this.btnAppConfig.Size = new System.Drawing.Size(75, 23);
            this.btnAppConfig.TabIndex = 7;
            this.btnAppConfig.Text = "修改配置";
            this.btnAppConfig.UseVisualStyleBackColor = true;
            this.btnAppConfig.Click += new System.EventHandler(this.btnAppConfig_Click);
            // 
            // btnEnd
            // 
            this.btnEnd.Location = new System.Drawing.Point(596, 39);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(75, 23);
            this.btnEnd.TabIndex = 5;
            this.btnEnd.Text = "停止";
            this.btnEnd.UseVisualStyleBackColor = true;
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // btnBind
            // 
            this.btnBind.Location = new System.Drawing.Point(515, 16);
            this.btnBind.Name = "btnBind";
            this.btnBind.Size = new System.Drawing.Size(75, 23);
            this.btnBind.TabIndex = 6;
            this.btnBind.Text = "绑定探头";
            this.btnBind.UseVisualStyleBackColor = true;
            this.btnBind.Click += new System.EventHandler(this.btnBind_Click);
            // 
            // btnBegin
            // 
            this.btnBegin.Location = new System.Drawing.Point(596, 16);
            this.btnBegin.Name = "btnBegin";
            this.btnBegin.Size = new System.Drawing.Size(75, 23);
            this.btnBegin.TabIndex = 4;
            this.btnBegin.Text = "开始";
            this.btnBegin.UseVisualStyleBackColor = true;
            this.btnBegin.Click += new System.EventHandler(this.btnBegin_Click);
            // 
            // nudUploadLimit
            // 
            this.nudUploadLimit.Enabled = false;
            this.nudUploadLimit.Location = new System.Drawing.Point(314, 39);
            this.nudUploadLimit.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.nudUploadLimit.Name = "nudUploadLimit";
            this.nudUploadLimit.ReadOnly = true;
            this.nudUploadLimit.Size = new System.Drawing.Size(64, 21);
            this.nudUploadLimit.TabIndex = 3;
            this.nudUploadLimit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // nudASpace
            // 
            this.nudASpace.Enabled = false;
            this.nudASpace.Location = new System.Drawing.Point(83, 39);
            this.nudASpace.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.nudASpace.Name = "nudASpace";
            this.nudASpace.ReadOnly = true;
            this.nudASpace.Size = new System.Drawing.Size(64, 21);
            this.nudASpace.TabIndex = 1;
            this.nudASpace.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(384, 43);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 12);
            this.label8.TabIndex = 0;
            this.label8.Text = "条";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(153, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "秒";
            // 
            // nudUploadInteval
            // 
            this.nudUploadInteval.Enabled = false;
            this.nudUploadInteval.Location = new System.Drawing.Point(314, 17);
            this.nudUploadInteval.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.nudUploadInteval.Name = "nudUploadInteval";
            this.nudUploadInteval.ReadOnly = true;
            this.nudUploadInteval.Size = new System.Drawing.Size(64, 21);
            this.nudUploadInteval.TabIndex = 2;
            this.nudUploadInteval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // nudNSpace
            // 
            this.nudNSpace.Enabled = false;
            this.nudNSpace.Location = new System.Drawing.Point(83, 17);
            this.nudNSpace.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.nudNSpace.Name = "nudNSpace";
            this.nudNSpace.ReadOnly = true;
            this.nudNSpace.Size = new System.Drawing.Size(64, 21);
            this.nudNSpace.TabIndex = 0;
            this.nudNSpace.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(384, 21);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 12);
            this.label7.TabIndex = 0;
            this.label7.Text = "秒";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(153, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "秒";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(243, 43);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 0;
            this.label6.Text = "查询数量：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "报警间隔：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(243, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 0;
            this.label5.Text = "上报间隔：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "正常间隔：";
            // 
            // gbUploadTime
            // 
            this.gbUploadTime.Controls.Add(this.dgvUploadTime);
            this.gbUploadTime.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbUploadTime.Location = new System.Drawing.Point(0, 68);
            this.gbUploadTime.Name = "gbUploadTime";
            this.gbUploadTime.Size = new System.Drawing.Size(804, 174);
            this.gbUploadTime.TabIndex = 1;
            this.gbUploadTime.TabStop = false;
            this.gbUploadTime.Text = "上报进度";
            // 
            // dgvUploadTime
            // 
            this.dgvUploadTime.AllowUserToAddRows = false;
            this.dgvUploadTime.AllowUserToDeleteRows = false;
            this.dgvUploadTime.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvUploadTime.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUploadTime.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CId,
            this.CProjectNo,
            this.CNetId,
            this.CLastUploadTime});
            this.dgvUploadTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUploadTime.Location = new System.Drawing.Point(3, 17);
            this.dgvUploadTime.Name = "dgvUploadTime";
            this.dgvUploadTime.ReadOnly = true;
            this.dgvUploadTime.RowHeadersVisible = false;
            this.dgvUploadTime.RowTemplate.Height = 23;
            this.dgvUploadTime.Size = new System.Drawing.Size(798, 154);
            this.dgvUploadTime.TabIndex = 0;
            this.dgvUploadTime.DataSourceChanged += new System.EventHandler(this.dgvUploadTime_DataSourceChanged);
            // 
            // CId
            // 
            this.CId.DataPropertyName = "Id";
            this.CId.HeaderText = "Id";
            this.CId.Name = "CId";
            this.CId.ReadOnly = true;
            this.CId.Visible = false;
            // 
            // CProjectNo
            // 
            this.CProjectNo.DataPropertyName = "ProjectNo";
            this.CProjectNo.HeaderText = "工程编号";
            this.CProjectNo.Name = "CProjectNo";
            this.CProjectNo.ReadOnly = true;
            // 
            // CNetId
            // 
            this.CNetId.DataPropertyName = "NetId";
            this.CNetId.HeaderText = "网络地址";
            this.CNetId.Name = "CNetId";
            this.CNetId.ReadOnly = true;
            // 
            // CLastUploadTime
            // 
            this.CLastUploadTime.DataPropertyName = "LastUploadTime";
            this.CLastUploadTime.HeaderText = "最后上报时间";
            this.CLastUploadTime.Name = "CLastUploadTime";
            this.CLastUploadTime.ReadOnly = true;
            // 
            // gbUploadLog
            // 
            this.gbUploadLog.Controls.Add(this.txtLog);
            this.gbUploadLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbUploadLog.Location = new System.Drawing.Point(0, 247);
            this.gbUploadLog.Name = "gbUploadLog";
            this.gbUploadLog.Size = new System.Drawing.Size(804, 165);
            this.gbUploadLog.TabIndex = 2;
            this.gbUploadLog.TabStop = false;
            this.gbUploadLog.Text = "上报记录";
            // 
            // txtLog
            // 
            this.txtLog.ContextMenuStrip = this.contextMenuStrip1;
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Location = new System.Drawing.Point(3, 17);
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.Size = new System.Drawing.Size(798, 145);
            this.txtLog.TabIndex = 0;
            this.txtLog.Text = "";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOpenLogPath});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(159, 26);
            // 
            // tsmiOpenLogPath
            // 
            this.tsmiOpenLogPath.Name = "tsmiOpenLogPath";
            this.tsmiOpenLogPath.Size = new System.Drawing.Size(158, 22);
            this.tsmiOpenLogPath.Text = "打开日志目录...";
            this.tsmiOpenLogPath.Click += new System.EventHandler(this.tsmiOpenLogPath_Click);
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 242);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(804, 5);
            this.splitter1.TabIndex = 3;
            this.splitter1.TabStop = false;
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(804, 412);
            this.Controls.Add(this.gbUploadLog);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.gbUploadTime);
            this.Controls.Add(this.gbUploadConfig);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "惊尘物流冷链数据上报系统V1.6";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.gbUploadConfig.ResumeLayout(false);
            this.gbUploadConfig.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudUploadLimit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudASpace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudUploadInteval)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudNSpace)).EndInit();
            this.gbUploadTime.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUploadTime)).EndInit();
            this.gbUploadLog.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbUploadConfig;
        private System.Windows.Forms.NumericUpDown nudASpace;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown nudNSpace;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbUploadTime;
        private System.Windows.Forms.DataGridView dgvUploadTime;
        private System.Windows.Forms.GroupBox gbUploadLog;
        private System.Windows.Forms.NumericUpDown nudUploadLimit;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown nudUploadInteval;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnEnd;
        private System.Windows.Forms.Button btnBegin;
        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.Button btnBind;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenLogPath;
        private System.Windows.Forms.Button btnAppConfig;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.DataGridViewTextBoxColumn CId;
        private System.Windows.Forms.DataGridViewTextBoxColumn CProjectNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CNetId;
        private System.Windows.Forms.DataGridViewTextBoxColumn CLastUploadTime;
    }
}