using C2LP.PDA.APP.ScannerAPI;
namespace C2LP.PDA.APP
{
    partial class UCThirdParty
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
            //移除当前的扫描响应事件处理方法
            Scanner.GetScanner().OnGetBarcodeEvent -= UCThirdParty_OnGetBarcodeEvent;
            //使扫描不可用,即不可发红外光
            Scanner.GetScanner().Close();
            //FrmParent.IputChangeEvent -= new FrmParent.InputPnlChangeDelegate(FrmParent_IputChangeEvent);
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
            this.button1 = new System.Windows.Forms.Button();
            this.txtOrderNumber = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblNumLength = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(74, 63);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(47, 24);
            this.button1.TabIndex = 0;
            this.button1.Text = "保存";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txtOrderNumber
            // 
            this.txtOrderNumber.Location = new System.Drawing.Point(69, 5);
            this.txtOrderNumber.Name = "txtOrderNumber";
            this.txtOrderNumber.Size = new System.Drawing.Size(125, 23);
            this.txtOrderNumber.TabIndex = 1;
            this.txtOrderNumber.TextChanged += new System.EventHandler(this.txtOrderNumber_TextChanged);
            this.txtOrderNumber.GotFocus += new System.EventHandler(this.control_GotFocus);
            this.txtOrderNumber.LostFocus += new System.EventHandler(this.control_LostFocus);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(152, 63);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(47, 24);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "返回";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 20);
            this.label1.Text = "运单号:";
            // 
            // lblNumLength
            // 
            this.lblNumLength.Location = new System.Drawing.Point(203, 8);
            this.lblNumLength.Name = "lblNumLength";
            this.lblNumLength.Size = new System.Drawing.Size(41, 20);
            this.lblNumLength.Text = "0位";
            // 
            // UCThirdParty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.lblNumLength);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtOrderNumber);
            this.Controls.Add(this.button1);
            this.Name = "UCThirdParty";
            this.Size = new System.Drawing.Size(249, 150);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtOrderNumber;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblNumLength;
    }
}
