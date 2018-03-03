//using C2LP.PDA.APP.ScannerAPI;
using System.IO;
using System.Reflection;
using System;
namespace C2LP.PDA.APP
{
    partial class UCCenterNodeScan
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
            this.pnlTop = new System.Windows.Forms.Panel();
            this.btnConfirm = new System.Windows.Forms.Button();
            this.cboNumMethod = new System.Windows.Forms.ComboBox();
            this.txtOrderNumber = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tpNode = new System.Windows.Forms.TabPage();
            this.lblDestin = new System.Windows.Forms.Label();
            this.lblContent = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chbIsArrive = new System.Windows.Forms.CheckBox();
            this.lblStorage = new System.Windows.Forms.Label();
            this.tcScan = new System.Windows.Forms.TabControl();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.pnlResult = new System.Windows.Forms.Panel();
            this.lblResult = new System.Windows.Forms.Label();
            this.tmResultState = new System.Windows.Forms.Timer();
            this.label4 = new System.Windows.Forms.Label();
            this.ucConsignors1 = new C2LP.PDA.APP.UCConsignors();
            this.cboStorageScan = new System.Windows.Forms.ComboBox();
            this.pnlTop.SuspendLayout();
            this.tpNode.SuspendLayout();
            this.tcScan.SuspendLayout();
            this.pnlResult.SuspendLayout();
            this.SuspendLayout();
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
            this.pnlTop.Size = new System.Drawing.Size(249, 26);
            this.pnlTop.LostFocus += new System.EventHandler(this.pnlTop_LostFocus);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirm.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnConfirm.Location = new System.Drawing.Point(182, 3);
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
            this.cboNumMethod.Location = new System.Drawing.Point(3, 4);
            this.cboNumMethod.Name = "cboNumMethod";
            this.cboNumMethod.Size = new System.Drawing.Size(66, 19);
            this.cboNumMethod.TabIndex = 2;
            this.cboNumMethod.SelectedIndexChanged += new System.EventHandler(this.cboNumMethod_SelectedIndexChanged);
            // 
            // txtOrderNumber
            // 
            this.txtOrderNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOrderNumber.Enabled = false;
            this.txtOrderNumber.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtOrderNumber.Location = new System.Drawing.Point(72, 3);
            this.txtOrderNumber.Name = "txtOrderNumber";
            this.txtOrderNumber.Size = new System.Drawing.Size(104, 19);
            this.txtOrderNumber.TabIndex = 1;
            this.txtOrderNumber.TextChanged += new System.EventHandler(this.txtOrderNumber_TextChanged);
            this.txtOrderNumber.GotFocus += new System.EventHandler(this.control_GotFocus);
            this.txtOrderNumber.LostFocus += new System.EventHandler(this.control_LostFocus);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.btnCancel.Location = new System.Drawing.Point(204, 129);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(37, 20);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "返回";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tpNode
            // 
            this.tpNode.BackColor = System.Drawing.SystemColors.Window;
            this.tpNode.Controls.Add(this.cboStorageScan);
            this.tpNode.Controls.Add(this.label4);
            this.tpNode.Controls.Add(this.btnCancel);
            this.tpNode.Controls.Add(this.lblDestin);
            this.tpNode.Controls.Add(this.lblContent);
            this.tpNode.Controls.Add(this.label3);
            this.tpNode.Controls.Add(this.label2);
            this.tpNode.Controls.Add(this.label1);
            this.tpNode.Controls.Add(this.chbIsArrive);
            this.tpNode.Controls.Add(this.lblStorage);
            this.tpNode.Location = new System.Drawing.Point(4, 25);
            this.tpNode.Name = "tpNode";
            this.tpNode.Size = new System.Drawing.Size(241, 150);
            this.tpNode.Text = "节点扫描";
            this.tpNode.Click += new System.EventHandler(this.tpNode_Click);
            // 
            // lblDestin
            // 
            this.lblDestin.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblDestin.Location = new System.Drawing.Point(103, 25);
            this.lblDestin.Name = "lblDestin";
            this.lblDestin.Size = new System.Drawing.Size(133, 13);
            this.lblDestin.Text = "Destin";
            // 
            // lblContent
            // 
            this.lblContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblContent.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblContent.Location = new System.Drawing.Point(9, 60);
            this.lblContent.Name = "lblContent";
            this.lblContent.Size = new System.Drawing.Size(221, 45);
            this.lblContent.Text = "Content";
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label3.Location = new System.Drawing.Point(9, 43);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 13);
            this.label3.Text = "节点内容预览↓";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(9, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 13);
            this.label2.Text = "目 的 地  :";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(9, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 13);
            this.label1.Text = "冷藏载体：";
            // 
            // chbIsArrive
            // 
            this.chbIsArrive.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.chbIsArrive.Location = new System.Drawing.Point(138, 43);
            this.chbIsArrive.Name = "chbIsArrive";
            this.chbIsArrive.Size = new System.Drawing.Size(90, 15);
            this.chbIsArrive.TabIndex = 0;
            this.chbIsArrive.Text = "运抵卸车";
            this.chbIsArrive.Visible = false;
            this.chbIsArrive.CheckStateChanged += new System.EventHandler(this.chbIsArrive_CheckStateChanged);
            // 
            // lblStorage
            // 
            this.lblStorage.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblStorage.Location = new System.Drawing.Point(103, 7);
            this.lblStorage.Name = "lblStorage";
            this.lblStorage.Size = new System.Drawing.Size(133, 13);
            this.lblStorage.Text = "Storage";
            // 
            // tcScan
            // 
            this.tcScan.Controls.Add(this.tpNode);
            this.tcScan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcScan.Location = new System.Drawing.Point(0, 50);
            this.tcScan.Name = "tcScan";
            this.tcScan.SelectedIndex = 0;
            this.tcScan.Size = new System.Drawing.Size(249, 179);
            this.tcScan.TabIndex = 2;
            this.tcScan.GotFocus += new System.EventHandler(this.tcScan_GotFocus);
            this.tcScan.LostFocus += new System.EventHandler(this.tcScan_LostFocus);
            this.tcScan.SelectedIndexChanged += new System.EventHandler(this.tcScan_SelectedIndexChanged);
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtLog.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtLog.Location = new System.Drawing.Point(0, 229);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(249, 42);
            this.txtLog.TabIndex = 20;
            this.txtLog.Text = "节点扫描:直接批量扫码,将会自动保存节点信息";
            this.txtLog.TextChanged += new System.EventHandler(this.txtLog_TextChanged);
            this.txtLog.GotFocus += new System.EventHandler(this.txtLog_GotFocus);
            this.txtLog.LostFocus += new System.EventHandler(this.txtLog_LostFocus);
            // 
            // pnlResult
            // 
            this.pnlResult.Controls.Add(this.ucConsignors1);
            this.pnlResult.Controls.Add(this.lblResult);
            this.pnlResult.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlResult.Location = new System.Drawing.Point(0, 26);
            this.pnlResult.Name = "pnlResult";
            this.pnlResult.Size = new System.Drawing.Size(249, 24);
            // 
            // lblResult
            // 
            this.lblResult.BackColor = System.Drawing.Color.Silver;
            this.lblResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblResult.Font = new System.Drawing.Font("Tahoma", 15F, System.Drawing.FontStyle.Regular);
            this.lblResult.Location = new System.Drawing.Point(0, 0);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(249, 24);
            this.lblResult.Text = "扫描/录入单号";
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tmResultState
            // 
            this.tmResultState.Interval = 666;
            this.tmResultState.Tick += new System.EventHandler(this.tmResultState_Tick);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.label4.Location = new System.Drawing.Point(9, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(102, 16);
            this.label4.Text = "上一个冷藏载体:";
            // 
            // ucConsignors1
            // 
            this.ucConsignors1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucConsignors1.Location = new System.Drawing.Point(0, 0);
            this.ucConsignors1.Name = "ucConsignors1";
            this.ucConsignors1.Size = new System.Drawing.Size(249, 24);
            this.ucConsignors1.TabIndex = 1;
            // 
            // cboStorageScan
            // 
            this.cboStorageScan.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboStorageScan.Location = new System.Drawing.Point(103, 104);
            this.cboStorageScan.Name = "cboStorageScan";
            this.cboStorageScan.Size = new System.Drawing.Size(127, 19);
            this.cboStorageScan.TabIndex = 9;
            // 
            // UCCenterNodeScan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tcScan);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.pnlResult);
            this.Controls.Add(this.pnlTop);
            this.Name = "UCCenterNodeScan";
            this.Size = new System.Drawing.Size(249, 271);
            this.pnlTop.ResumeLayout(false);
            this.tpNode.ResumeLayout(false);
            this.tcScan.ResumeLayout(false);
            this.pnlResult.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtOrderNumber;
        private System.Windows.Forms.TabPage tpNode;
        private System.Windows.Forms.TabControl tcScan;
        private System.Windows.Forms.CheckBox chbIsArrive;
        private System.Windows.Forms.Label lblContent;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblDestin;
        private System.Windows.Forms.Label lblStorage;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.ComboBox cboNumMethod;
        private System.Windows.Forms.Panel pnlResult;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Timer tmResultState;
        private System.Windows.Forms.Button btnConfirm;
        private UCConsignors ucConsignors1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboStorageScan;

    }

    //public class PicNameAndPath
    //{
    //    public string PicName { get; set; }

    //    public string PicPath { get; set; }
    //}
}
