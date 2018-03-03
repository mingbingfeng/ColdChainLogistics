namespace C2LP.WebService.SetUploadStatusTool
{
    partial class FrmMain
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
            this.btnSearch = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnUpdate1 = new System.Windows.Forms.Button();
            this.btnUpdateAll = new System.Windows.Forms.Button();
            this.txt1 = new System.Windows.Forms.RichTextBox();
            this.btnUpdateAllArrived = new System.Windows.Forms.Button();
            this.btnSearchArrived = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(12, 22);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 0;
            this.btnSearch.Text = "查询";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 75);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1009, 351);
            this.dataGridView1.TabIndex = 1;
            // 
            // btnUpdate1
            // 
            this.btnUpdate1.Location = new System.Drawing.Point(181, 22);
            this.btnUpdate1.Name = "btnUpdate1";
            this.btnUpdate1.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate1.TabIndex = 2;
            this.btnUpdate1.Text = "修正当前行";
            this.btnUpdate1.UseVisualStyleBackColor = true;
            this.btnUpdate1.Click += new System.EventHandler(this.btnUpdate1_Click);
            // 
            // btnUpdateAll
            // 
            this.btnUpdateAll.Location = new System.Drawing.Point(297, 22);
            this.btnUpdateAll.Name = "btnUpdateAll";
            this.btnUpdateAll.Size = new System.Drawing.Size(75, 23);
            this.btnUpdateAll.TabIndex = 2;
            this.btnUpdateAll.Text = "修正所有行";
            this.btnUpdateAll.UseVisualStyleBackColor = true;
            this.btnUpdateAll.Click += new System.EventHandler(this.btnUpdateAll_Click);
            // 
            // txt1
            // 
            this.txt1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt1.Location = new System.Drawing.Point(12, 434);
            this.txt1.Name = "txt1";
            this.txt1.Size = new System.Drawing.Size(1009, 135);
            this.txt1.TabIndex = 3;
            this.txt1.Text = "";
            // 
            // btnUpdateAllArrived
            // 
            this.btnUpdateAllArrived.Location = new System.Drawing.Point(618, 21);
            this.btnUpdateAllArrived.Name = "btnUpdateAllArrived";
            this.btnUpdateAllArrived.Size = new System.Drawing.Size(111, 23);
            this.btnUpdateAllArrived.TabIndex = 4;
            this.btnUpdateAllArrived.Text = "修正所有运抵";
            this.btnUpdateAllArrived.UseVisualStyleBackColor = true;
            this.btnUpdateAllArrived.Click += new System.EventHandler(this.btnUpdateAllArrived_Click);
            // 
            // btnSearchArrived
            // 
            this.btnSearchArrived.Location = new System.Drawing.Point(519, 21);
            this.btnSearchArrived.Name = "btnSearchArrived";
            this.btnSearchArrived.Size = new System.Drawing.Size(75, 23);
            this.btnSearchArrived.TabIndex = 5;
            this.btnSearchArrived.Text = "查询运抵";
            this.btnSearchArrived.UseVisualStyleBackColor = true;
            this.btnSearchArrived.Click += new System.EventHandler(this.btnSearchArrived_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1033, 581);
            this.Controls.Add(this.btnSearchArrived);
            this.Controls.Add(this.btnUpdateAllArrived);
            this.Controls.Add(this.txt1);
            this.Controls.Add(this.btnUpdateAll);
            this.Controls.Add(this.btnUpdate1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnSearch);
            this.Name = "FrmMain";
            this.Text = "纠正上报进度数据";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnUpdate1;
        private System.Windows.Forms.Button btnUpdateAll;
        private System.Windows.Forms.RichTextBox txt1;
        private System.Windows.Forms.Button btnUpdateAllArrived;
        private System.Windows.Forms.Button btnSearchArrived;
    }
}

