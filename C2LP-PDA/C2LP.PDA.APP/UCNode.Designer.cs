namespace C2LP.PDA.APP
{
    partial class UCNode
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
            this.txtNodeNumber = new System.Windows.Forms.TextBox();
            this.lblOperateAt = new System.Windows.Forms.Label();
            this.lblNodeContent = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtNodeNumber
            // 
            this.txtNodeNumber.BackColor = System.Drawing.Color.White;
            this.txtNodeNumber.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNodeNumber.Location = new System.Drawing.Point(3, 3);
            this.txtNodeNumber.Name = "txtNodeNumber";
            this.txtNodeNumber.ReadOnly = true;
            this.txtNodeNumber.Size = new System.Drawing.Size(128, 23);
            this.txtNodeNumber.TabIndex = 0;
            this.txtNodeNumber.WordWrap = false;
            // 
            // lblOperateAt
            // 
            this.lblOperateAt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblOperateAt.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular);
            this.lblOperateAt.Location = new System.Drawing.Point(129, 3);
            this.lblOperateAt.Name = "lblOperateAt";
            this.lblOperateAt.Size = new System.Drawing.Size(114, 23);
            this.lblOperateAt.Text = "2017-02-03 16:59";
            this.lblOperateAt.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblNodeContent
            // 
            this.lblNodeContent.BackColor = System.Drawing.Color.White;
            this.lblNodeContent.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblNodeContent.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblNodeContent.Location = new System.Drawing.Point(0, 23);
            this.lblNodeContent.Name = "lblNodeContent";
            this.lblNodeContent.Size = new System.Drawing.Size(241, 42);
            // 
            // UCNode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lblNodeContent);
            this.Controls.Add(this.lblOperateAt);
            this.Controls.Add(this.txtNodeNumber);
            this.Name = "UCNode";
            this.Size = new System.Drawing.Size(241, 65);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtNodeNumber;
        private System.Windows.Forms.Label lblOperateAt;
        private System.Windows.Forms.Label lblNodeContent;
    }
}
