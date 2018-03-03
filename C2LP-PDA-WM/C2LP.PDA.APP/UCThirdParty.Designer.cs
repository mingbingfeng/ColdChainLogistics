//using C2LP.PDA.APP.ScannerAPI;
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
            ////移除当前的扫描响应事件处理方法
            //Scanner.GetScanner().OnGetBarcodeEvent -= UCThirdParty_OnGetBarcodeEvent;
            ////使扫描不可用,即不可发红外光
            //Scanner.GetScanner().Close();
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
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlTop = new System.Windows.Forms.Panel();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.cboNumMethod = new System.Windows.Forms.ComboBox();
            this.txtOrderNumber = new System.Windows.Forms.TextBox();
            this.pnlResult = new System.Windows.Forms.Panel();
            this.ucConsignors1 = new C2LP.PDA.APP.UCConsignors();
            this.lblResult = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.tmResultState = new System.Windows.Forms.Timer();
            this.pnlTop.SuspendLayout();
            this.pnlResult.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnCancel.Location = new System.Drawing.Point(182, 199);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(52, 19);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "返回";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.Transparent;
            this.pnlTop.Controls.Add(this.btnConfirm);
            this.pnlTop.Controls.Add(this.cboNumMethod);
            this.pnlTop.Controls.Add(this.txtOrderNumber);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(249, 28);
            this.pnlTop.Click += new System.EventHandler(this.pnlTop_Click);
            this.pnlTop.GotFocus += new System.EventHandler(this.pnlTop_GotFocus);
            this.pnlTop.LostFocus += new System.EventHandler(this.pnlTop_LostFocus);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirm.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnConfirm.Location = new System.Drawing.Point(182, 4);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(52, 19);
            this.btnConfirm.TabIndex = 3;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // cboNumMethod
            // 
            this.cboNumMethod.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboNumMethod.Items.Add("扫描条码");
            this.cboNumMethod.Items.Add("手工录入");
            this.cboNumMethod.Location = new System.Drawing.Point(3, 5);
            this.cboNumMethod.Name = "cboNumMethod";
            this.cboNumMethod.Size = new System.Drawing.Size(66, 19);
            this.cboNumMethod.TabIndex = 2;
            this.cboNumMethod.LostFocus += new System.EventHandler(this.cboNumMethod_LostFocus);
            this.cboNumMethod.SelectedIndexChanged += new System.EventHandler(this.cboNumMethod_SelectedIndexChanged);
            this.cboNumMethod.GotFocus += new System.EventHandler(this.cboNumMethod_GotFocus);
            // 
            // txtOrderNumber
            // 
            this.txtOrderNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOrderNumber.Enabled = false;
            this.txtOrderNumber.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtOrderNumber.Location = new System.Drawing.Point(72, 4);
            this.txtOrderNumber.Name = "txtOrderNumber";
            this.txtOrderNumber.Size = new System.Drawing.Size(104, 19);
            this.txtOrderNumber.TabIndex = 1;
            this.txtOrderNumber.TextChanged += new System.EventHandler(this.txtOrderNumber_TextChanged);
            this.txtOrderNumber.GotFocus += new System.EventHandler(this.control_GotFocus);
            this.txtOrderNumber.LostFocus += new System.EventHandler(this.control_LostFocus);
            // 
            // pnlResult
            // 
            this.pnlResult.Controls.Add(this.ucConsignors1);
            this.pnlResult.Controls.Add(this.lblResult);
            this.pnlResult.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlResult.Location = new System.Drawing.Point(0, 28);
            this.pnlResult.Name = "pnlResult";
            this.pnlResult.Size = new System.Drawing.Size(249, 24);
            // 
            // ucConsignors1
            // 
            this.ucConsignors1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucConsignors1.IsScanOrder = true;
            this.ucConsignors1.Location = new System.Drawing.Point(0, 0);
            this.ucConsignors1.Name = "ucConsignors1";
            this.ucConsignors1.Size = new System.Drawing.Size(249, 24);
            this.ucConsignors1.TabIndex = 1;
            // 
            // lblResult
            // 
            this.lblResult.BackColor = System.Drawing.Color.Silver;
            this.lblResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblResult.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.lblResult.Location = new System.Drawing.Point(0, 0);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(249, 24);
            this.lblResult.Text = "扫描/录入第三方运单";
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnCancel);
            this.panel1.Controls.Add(this.txtLog);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 52);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(249, 219);
            // 
            // txtLog
            // 
            this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtLog.Location = new System.Drawing.Point(0, 0);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(249, 199);
            this.txtLog.TabIndex = 0;
            this.txtLog.TextChanged += new System.EventHandler(this.txtLog_TextChanged);
            this.txtLog.GotFocus += new System.EventHandler(this.txtLog_GotFocus);
            this.txtLog.LostFocus += new System.EventHandler(this.txtLog_LostFocus);
            // 
            // tmResultState
            // 
            this.tmResultState.Interval = 666;
            this.tmResultState.Tick += new System.EventHandler(this.tmResultState_Tick);
            // 
            // UCThirdParty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlResult);
            this.Controls.Add(this.pnlTop);
            this.Name = "UCThirdParty";
            this.Size = new System.Drawing.Size(249, 271);
            this.pnlTop.ResumeLayout(false);
            this.pnlResult.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.ComboBox cboNumMethod;
        private System.Windows.Forms.TextBox txtOrderNumber;
        private System.Windows.Forms.Panel pnlResult;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Timer tmResultState;
        private UCConsignors ucConsignors1;
    }
}
