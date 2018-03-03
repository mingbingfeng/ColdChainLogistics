namespace C2LP.PDA.APP
{
    partial class UCConsignors
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
            this.lblResult = new System.Windows.Forms.Label();
            this.cboConsignors = new System.Windows.Forms.ComboBox();
            this.tmResultState = new System.Windows.Forms.Timer();
            this.SuspendLayout();
            // 
            // lblResult
            // 
            this.lblResult.BackColor = System.Drawing.Color.Silver;
            this.lblResult.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.lblResult.Location = new System.Drawing.Point(0, 0);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(249, 24);
            this.lblResult.Text = "正在加载第三方供应商";
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblResult.TextChanged += new System.EventHandler(this.lblResult_TextChanged);
            // 
            // cboConsignors
            // 
            this.cboConsignors.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.cboConsignors.Location = new System.Drawing.Point(109, 1);
            this.cboConsignors.Name = "cboConsignors";
            this.cboConsignors.Size = new System.Drawing.Size(100, 23);
            this.cboConsignors.TabIndex = 2;
            this.cboConsignors.Visible = false;
            this.cboConsignors.LostFocus += new System.EventHandler(this.cboConsignors_LostFocus);
            this.cboConsignors.SelectedIndexChanged += new System.EventHandler(this.cboConsignors_SelectedIndexChanged);
            this.cboConsignors.GotFocus += new System.EventHandler(this.cboConsignors_GotFocus);
            // 
            // tmResultState
            // 
            this.tmResultState.Interval = 666;
            this.tmResultState.Tick += new System.EventHandler(this.tmResultState_Tick);
            // 
            // UCConsignors
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.cboConsignors);
            this.Controls.Add(this.lblResult);
            this.Name = "UCConsignors";
            this.Size = new System.Drawing.Size(249, 24);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.ComboBox cboConsignors;
        public System.Windows.Forms.Timer tmResultState;
    }
}
