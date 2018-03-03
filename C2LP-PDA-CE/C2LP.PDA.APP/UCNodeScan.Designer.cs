using C2LP.PDA.APP.ScannerAPI;
using System.IO;
using System.Reflection;
using System;
using System.Windows.Forms;
namespace C2LP.PDA.APP
{
    partial class UCNodeScan
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
            //UnitechDSDll.PreviewStop();

            ////移除当前的扫描响应事件处理方法
            //Scanner.GetScanner().OnGetBarcodeEvent -= Scanner_OnGetBarcodeEvent;
            ////使扫描不可用,即不可发红外光
            //Scanner.GetScanner().Close();

            //FrmParent.ParentForm.KeyDown -= new KeyEventHandler(ParentForm_KeyDown);
            USI_API_HT380W.Barcode1D_free();
            try
            {
                FrmParent.ParentForm.pbPreview.Visible = false;
                FrmParent.ParentForm.pbPreview.Parent = FrmParent.ParentForm.pnlAbout;
            }
            catch
            {

            }
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
            this.tpPicture = new System.Windows.Forms.TabPage();
            this.picCaptrue = new System.Windows.Forms.PictureBox();
            this.btnCancel2 = new System.Windows.Forms.Button();
            this.btnCamera = new System.Windows.Forms.Button();
            this.btnSavePic = new System.Windows.Forms.Button();
            this.lbPicList = new System.Windows.Forms.ListBox();
            this.btnRemovePic = new System.Windows.Forms.Button();
            this.btnAddPic = new System.Windows.Forms.Button();
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
            this.pnlTop.SuspendLayout();
            this.tpPicture.SuspendLayout();
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
            this.pnlTop.Size = new System.Drawing.Size(249, 32);
            // 
            // btnConfirm
            // 
            this.btnConfirm.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirm.Location = new System.Drawing.Point(191, 5);
            this.btnConfirm.Name = "btnConfirm";
            this.btnConfirm.Size = new System.Drawing.Size(52, 20);
            this.btnConfirm.TabIndex = 3;
            this.btnConfirm.Text = "确定";
            this.btnConfirm.Click += new System.EventHandler(this.btnConfirm_Click);
            // 
            // cboNumMethod
            // 
            this.cboNumMethod.Items.Add("扫描条码");
            this.cboNumMethod.Items.Add("手工录入");
            this.cboNumMethod.Location = new System.Drawing.Point(3, 5);
            this.cboNumMethod.Name = "cboNumMethod";
            this.cboNumMethod.Size = new System.Drawing.Size(76, 23);
            this.cboNumMethod.TabIndex = 2;
            this.cboNumMethod.SelectedIndexChanged += new System.EventHandler(this.cboNumMethod_SelectedIndexChanged);
            // 
            // txtOrderNumber
            // 
            this.txtOrderNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtOrderNumber.Location = new System.Drawing.Point(80, 4);
            this.txtOrderNumber.Name = "txtOrderNumber";
            this.txtOrderNumber.Size = new System.Drawing.Size(104, 23);
            this.txtOrderNumber.TabIndex = 1;
            this.txtOrderNumber.TextChanged += new System.EventHandler(this.txtOrderNumber_TextChanged);
            this.txtOrderNumber.GotFocus += new System.EventHandler(this.control_GotFocus);
            this.txtOrderNumber.LostFocus += new System.EventHandler(this.control_LostFocus);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(184, 166);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(52, 24);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "返回";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // tpPicture
            // 
            this.tpPicture.BackColor = System.Drawing.SystemColors.Window;
            this.tpPicture.Controls.Add(this.picCaptrue);
            this.tpPicture.Controls.Add(this.btnCancel2);
            this.tpPicture.Controls.Add(this.btnCamera);
            this.tpPicture.Controls.Add(this.btnSavePic);
            this.tpPicture.Controls.Add(this.lbPicList);
            this.tpPicture.Controls.Add(this.btnRemovePic);
            this.tpPicture.Controls.Add(this.btnAddPic);
            this.tpPicture.Location = new System.Drawing.Point(4, 25);
            this.tpPicture.Name = "tpPicture";
            this.tpPicture.Size = new System.Drawing.Size(241, 193);
            this.tpPicture.Text = "签收拍照";
            // 
            // picCaptrue
            // 
            this.picCaptrue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.picCaptrue.Location = new System.Drawing.Point(3, 3);
            this.picCaptrue.Name = "picCaptrue";
            this.picCaptrue.Size = new System.Drawing.Size(188, 158);
            this.picCaptrue.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            // 
            // btnCancel2
            // 
            this.btnCancel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel2.Location = new System.Drawing.Point(178, 167);
            this.btnCancel2.Name = "btnCancel2";
            this.btnCancel2.Size = new System.Drawing.Size(52, 24);
            this.btnCancel2.TabIndex = 2;
            this.btnCancel2.Text = "返回";
            this.btnCancel2.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnCamera
            // 
            this.btnCamera.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCamera.Location = new System.Drawing.Point(98, 167);
            this.btnCamera.Name = "btnCamera";
            this.btnCamera.Size = new System.Drawing.Size(54, 24);
            this.btnCamera.TabIndex = 0;
            this.btnCamera.Text = "拍摄";
            this.btnCamera.Visible = false;
            this.btnCamera.Click += new System.EventHandler(this.btnCamera_Click);
            // 
            // btnSavePic
            // 
            this.btnSavePic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnSavePic.Location = new System.Drawing.Point(17, 167);
            this.btnSavePic.Name = "btnSavePic";
            this.btnSavePic.Size = new System.Drawing.Size(52, 24);
            this.btnSavePic.TabIndex = 0;
            this.btnSavePic.Text = "保存";
            this.btnSavePic.Click += new System.EventHandler(this.btnSavePic_Click);
            // 
            // lbPicList
            // 
            this.lbPicList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbPicList.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.lbPicList.Location = new System.Drawing.Point(197, 3);
            this.lbPicList.Name = "lbPicList";
            this.lbPicList.Size = new System.Drawing.Size(42, 100);
            this.lbPicList.TabIndex = 1;
            this.lbPicList.SelectedIndexChanged += new System.EventHandler(this.lbPicList_SelectedIndexChanged);
            // 
            // btnRemovePic
            // 
            this.btnRemovePic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemovePic.Enabled = false;
            this.btnRemovePic.Location = new System.Drawing.Point(197, 111);
            this.btnRemovePic.Name = "btnRemovePic";
            this.btnRemovePic.Size = new System.Drawing.Size(41, 21);
            this.btnRemovePic.TabIndex = 0;
            this.btnRemovePic.Text = "-";
            this.btnRemovePic.Click += new System.EventHandler(this.btnRemovePic_Click);
            // 
            // btnAddPic
            // 
            this.btnAddPic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddPic.Location = new System.Drawing.Point(197, 131);
            this.btnAddPic.Name = "btnAddPic";
            this.btnAddPic.Size = new System.Drawing.Size(41, 21);
            this.btnAddPic.TabIndex = 0;
            this.btnAddPic.Text = "+";
            this.btnAddPic.Click += new System.EventHandler(this.btnAddPic_Click);
            // 
            // tpNode
            // 
            this.tpNode.BackColor = System.Drawing.SystemColors.Window;
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
            this.tpNode.Size = new System.Drawing.Size(241, 193);
            this.tpNode.Text = "节点扫描";
            // 
            // lblDestin
            // 
            this.lblDestin.Location = new System.Drawing.Point(95, 37);
            this.lblDestin.Name = "lblDestin";
            this.lblDestin.Size = new System.Drawing.Size(133, 20);
            this.lblDestin.Text = "Destin";
            // 
            // lblContent
            // 
            this.lblContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblContent.Location = new System.Drawing.Point(9, 97);
            this.lblContent.Name = "lblContent";
            this.lblContent.Size = new System.Drawing.Size(221, 96);
            this.lblContent.Text = "Content";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(9, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 20);
            this.label3.Text = "节点内容预览↓";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(9, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 20);
            this.label2.Text = "目 的 地  :";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(9, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 20);
            this.label1.Text = "冷藏载体：";
            // 
            // chbIsArrive
            // 
            this.chbIsArrive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chbIsArrive.Location = new System.Drawing.Point(138, 67);
            this.chbIsArrive.Name = "chbIsArrive";
            this.chbIsArrive.Size = new System.Drawing.Size(90, 20);
            this.chbIsArrive.TabIndex = 0;
            this.chbIsArrive.Text = "运抵卸车";
            this.chbIsArrive.CheckStateChanged += new System.EventHandler(this.chbIsArrive_CheckStateChanged);
            // 
            // lblStorage
            // 
            this.lblStorage.Location = new System.Drawing.Point(95, 7);
            this.lblStorage.Name = "lblStorage";
            this.lblStorage.Size = new System.Drawing.Size(133, 20);
            this.lblStorage.Text = "Storage";
            // 
            // tcScan
            // 
            this.tcScan.Controls.Add(this.tpNode);
            this.tcScan.Controls.Add(this.tpPicture);
            this.tcScan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcScan.Location = new System.Drawing.Point(0, 74);
            this.tcScan.Name = "tcScan";
            this.tcScan.SelectedIndex = 0;
            this.tcScan.Size = new System.Drawing.Size(249, 222);
            this.tcScan.TabIndex = 2;
            this.tcScan.SelectedIndexChanged += new System.EventHandler(this.tcScan_SelectedIndexChanged);
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtLog.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtLog.Location = new System.Drawing.Point(0, 296);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(249, 60);
            this.txtLog.TabIndex = 20;
            this.txtLog.Text = "节点扫描:直接批量扫码,将会自动保存节点信息\r\n签收拍照:请点击按钮【+】拍照后提交节点信息\r\n";
            // 
            // pnlResult
            // 
            this.pnlResult.Controls.Add(this.lblResult);
            this.pnlResult.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlResult.Location = new System.Drawing.Point(0, 32);
            this.pnlResult.Name = "pnlResult";
            this.pnlResult.Size = new System.Drawing.Size(249, 42);
            // 
            // lblResult
            // 
            this.lblResult.BackColor = System.Drawing.Color.Silver;
            this.lblResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblResult.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Regular);
            this.lblResult.Location = new System.Drawing.Point(0, 0);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(249, 42);
            this.lblResult.Text = "扫描/录入单号";
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // tmResultState
            // 
            this.tmResultState.Interval = 1500;
            this.tmResultState.Tick += new System.EventHandler(this.tmResultState_Tick);
            // 
            // UCNodeScan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.tcScan);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.pnlResult);
            this.Controls.Add(this.pnlTop);
            this.Name = "UCNodeScan";
            this.Size = new System.Drawing.Size(249, 356);
            this.pnlTop.ResumeLayout(false);
            this.tpPicture.ResumeLayout(false);
            this.tpNode.ResumeLayout(false);
            this.tcScan.ResumeLayout(false);
            this.pnlResult.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtOrderNumber;
        private System.Windows.Forms.TabPage tpPicture;
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
        private System.Windows.Forms.Button btnSavePic;
        private System.Windows.Forms.ListBox lbPicList;
        private System.Windows.Forms.Button btnRemovePic;
        private System.Windows.Forms.Button btnAddPic;
        private System.Windows.Forms.Button btnCamera;
        private System.Windows.Forms.Panel pnlResult;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Timer tmResultState;
        private System.Windows.Forms.Button btnConfirm;
        private System.Windows.Forms.Button btnCancel2;
        private PictureBox picCaptrue;

    }

    public class PicNameAndPath
    {
        public string PicName { get; set; }

        public string PicPath { get; set; }
    }
}
