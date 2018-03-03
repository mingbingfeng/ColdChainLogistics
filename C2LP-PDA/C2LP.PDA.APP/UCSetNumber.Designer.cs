namespace C2LP.PDA.APP
{
    partial class UCSetNumber
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
            this.label1 = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.txtPDANumber = new System.Windows.Forms.TextBox();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnConfig = new System.Windows.Forms.Button();
            this.txtPassWord = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnShowHideTop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(17, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(215, 20);
            this.label1.Text = "请输入将要设定的PDA编号：";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(137, 173);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(62, 23);
            this.btnCancel.TabIndex = 4;
            this.btnCancel.Text = "返回";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(41, 173);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(62, 23);
            this.btnOK.TabIndex = 5;
            this.btnOK.Text = "提交";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtPDANumber
            // 
            this.txtPDANumber.Location = new System.Drawing.Point(17, 50);
            this.txtPDANumber.Name = "txtPDANumber";
            this.txtPDANumber.Size = new System.Drawing.Size(215, 23);
            this.txtPDANumber.TabIndex = 2;
            this.txtPDANumber.GotFocus += new System.EventHandler(this.txtPDANumber_GotFocus);
            this.txtPDANumber.LostFocus += new System.EventHandler(this.txtPDANumber_LostFocus);
            // 
            // btnExit
            // 
            this.btnExit.ForeColor = System.Drawing.Color.Red;
            this.btnExit.Location = new System.Drawing.Point(128, 221);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(71, 23);
            this.btnExit.TabIndex = 6;
            this.btnExit.Text = "退出系统";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnConfig
            // 
            this.btnConfig.Location = new System.Drawing.Point(41, 221);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(71, 23);
            this.btnConfig.TabIndex = 44;
            this.btnConfig.Text = "参数配置";
            this.btnConfig.Click += new System.EventHandler(this.btnConfig_Click);
            // 
            // txtPassWord
            // 
            this.txtPassWord.Location = new System.Drawing.Point(17, 120);
            this.txtPassWord.Name = "txtPassWord";
            this.txtPassWord.PasswordChar = '*';
            this.txtPassWord.Size = new System.Drawing.Size(215, 23);
            this.txtPassWord.TabIndex = 46;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(17, 97);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(215, 20);
            this.label2.Text = "请输入PDA密码：";
            // 
            // btnShowHideTop
            // 
            this.btnShowHideTop.Location = new System.Drawing.Point(59, 266);
            this.btnShowHideTop.Name = "btnShowHideTop";
            this.btnShowHideTop.Size = new System.Drawing.Size(114, 23);
            this.btnShowHideTop.TabIndex = 49;
            this.btnShowHideTop.Text = "显示/隐藏任务了";
            this.btnShowHideTop.Click += new System.EventHandler(this.btnShowHideTop_Click);
            // 
            // UCSetNumber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.btnShowHideTop);
            this.Controls.Add(this.txtPassWord);
            this.Controls.Add(this.btnConfig);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtPDANumber);
            this.Name = "UCSetNumber";
            this.Size = new System.Drawing.Size(249, 294);
            this.Click += new System.EventHandler(this.UCSetNumber_Click);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtPDANumber;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnConfig;
        private System.Windows.Forms.TextBox txtPassWord;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnShowHideTop;
    }
}
