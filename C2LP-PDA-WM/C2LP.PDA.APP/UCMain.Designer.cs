namespace C2LP.PDA.APP
{
    partial class UCMain
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
            Common.ValueChangeEvent -= new Common.ValueChangeDelegate(Common_ValueChangeEvent);
            RemoeveControlEvent();
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
            this.btnSync = new System.Windows.Forms.Button();
            this.btnDestination = new System.Windows.Forms.Button();
            this.btnStorage = new System.Windows.Forms.Button();
            this.btnNumber = new System.Windows.Forms.Button();
            this.lblLastSyncTime = new System.Windows.Forms.Label();
            this.lblTitleSync = new System.Windows.Forms.Label();
            this.lblTitleDestination = new System.Windows.Forms.Label();
            this.lblTitleStorage = new System.Windows.Forms.Label();
            this.lblTitleNumber = new System.Windows.Forms.Label();
            this.btnOrderInput = new System.Windows.Forms.Button();
            this.btnNodeScan = new System.Windows.Forms.Button();
            this.btnThirdParty = new System.Windows.Forms.Button();
            this.btnWaitUploadNode = new System.Windows.Forms.Button();
            this.btnCenterNode = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblNews = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSync
            // 
            this.btnSync.Location = new System.Drawing.Point(10, 96);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(89, 35);
            this.btnSync.TabIndex = 25;
            this.btnSync.Text = "信息同步";
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // btnDestination
            // 
            this.btnDestination.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDestination.BackColor = System.Drawing.Color.Transparent;
            this.btnDestination.Location = new System.Drawing.Point(80, 53);
            this.btnDestination.Name = "btnDestination";
            this.btnDestination.Size = new System.Drawing.Size(152, 23);
            this.btnDestination.TabIndex = 24;
            // 
            // btnStorage
            // 
            this.btnStorage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStorage.BackColor = System.Drawing.Color.Transparent;
            this.btnStorage.Location = new System.Drawing.Point(80, 27);
            this.btnStorage.Name = "btnStorage";
            this.btnStorage.Size = new System.Drawing.Size(152, 23);
            this.btnStorage.TabIndex = 23;
            // 
            // btnNumber
            // 
            this.btnNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNumber.BackColor = System.Drawing.Color.Transparent;
            this.btnNumber.Location = new System.Drawing.Point(80, 2);
            this.btnNumber.Name = "btnNumber";
            this.btnNumber.Size = new System.Drawing.Size(152, 23);
            this.btnNumber.TabIndex = 22;
            // 
            // lblLastSyncTime
            // 
            this.lblLastSyncTime.Location = new System.Drawing.Point(82, 222);
            this.lblLastSyncTime.Name = "lblLastSyncTime";
            this.lblLastSyncTime.Size = new System.Drawing.Size(134, 14);
            this.lblLastSyncTime.Text = "从未同步过";
            // 
            // lblTitleSync
            // 
            this.lblTitleSync.Location = new System.Drawing.Point(10, 222);
            this.lblTitleSync.Name = "lblTitleSync";
            this.lblTitleSync.Size = new System.Drawing.Size(80, 14);
            this.lblTitleSync.Text = "最后同步：";
            // 
            // lblTitleDestination
            // 
            this.lblTitleDestination.Location = new System.Drawing.Point(10, 55);
            this.lblTitleDestination.Name = "lblTitleDestination";
            this.lblTitleDestination.Size = new System.Drawing.Size(75, 20);
            this.lblTitleDestination.Text = "目 的 地：";
            // 
            // lblTitleStorage
            // 
            this.lblTitleStorage.Location = new System.Drawing.Point(10, 31);
            this.lblTitleStorage.Name = "lblTitleStorage";
            this.lblTitleStorage.Size = new System.Drawing.Size(75, 20);
            this.lblTitleStorage.Text = "PDA绑定：";
            // 
            // lblTitleNumber
            // 
            this.lblTitleNumber.Location = new System.Drawing.Point(10, 7);
            this.lblTitleNumber.Name = "lblTitleNumber";
            this.lblTitleNumber.Size = new System.Drawing.Size(75, 20);
            this.lblTitleNumber.Text = "PDA编号：";
            // 
            // btnOrderInput
            // 
            this.btnOrderInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOrderInput.Location = new System.Drawing.Point(143, 96);
            this.btnOrderInput.Name = "btnOrderInput";
            this.btnOrderInput.Size = new System.Drawing.Size(89, 35);
            this.btnOrderInput.TabIndex = 31;
            this.btnOrderInput.Text = "运单录入";
            // 
            // btnNodeScan
            // 
            this.btnNodeScan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNodeScan.Enabled = false;
            this.btnNodeScan.Location = new System.Drawing.Point(143, 180);
            this.btnNodeScan.Name = "btnNodeScan";
            this.btnNodeScan.Size = new System.Drawing.Size(89, 35);
            this.btnNodeScan.TabIndex = 31;
            this.btnNodeScan.Text = "运抵卸车";
            // 
            // btnThirdParty
            // 
            this.btnThirdParty.Location = new System.Drawing.Point(10, 138);
            this.btnThirdParty.Name = "btnThirdParty";
            this.btnThirdParty.Size = new System.Drawing.Size(89, 35);
            this.btnThirdParty.TabIndex = 49;
            this.btnThirdParty.Text = "第三方运单";
            // 
            // btnWaitUploadNode
            // 
            this.btnWaitUploadNode.Location = new System.Drawing.Point(10, 180);
            this.btnWaitUploadNode.Name = "btnWaitUploadNode";
            this.btnWaitUploadNode.Size = new System.Drawing.Size(89, 35);
            this.btnWaitUploadNode.TabIndex = 55;
            this.btnWaitUploadNode.Text = "待上报节点";
            // 
            // btnCenterNode
            // 
            this.btnCenterNode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCenterNode.Location = new System.Drawing.Point(143, 138);
            this.btnCenterNode.Name = "btnCenterNode";
            this.btnCenterNode.Size = new System.Drawing.Size(89, 35);
            this.btnCenterNode.TabIndex = 49;
            this.btnCenterNode.Text = "节点扫描";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.lblNews);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 253);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(249, 18);
            // 
            // lblNews
            // 
            this.lblNews.BackColor = System.Drawing.Color.Transparent;
            this.lblNews.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNews.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            this.lblNews.ForeColor = System.Drawing.Color.Black;
            this.lblNews.Location = new System.Drawing.Point(0, 0);
            this.lblNews.Name = "lblNews";
            this.lblNews.Size = new System.Drawing.Size(249, 18);
            // 
            // UCMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btnWaitUploadNode);
            this.Controls.Add(this.btnCenterNode);
            this.Controls.Add(this.btnThirdParty);
            this.Controls.Add(this.btnNodeScan);
            this.Controls.Add(this.btnOrderInput);
            this.Controls.Add(this.btnSync);
            this.Controls.Add(this.btnDestination);
            this.Controls.Add(this.btnStorage);
            this.Controls.Add(this.btnNumber);
            this.Controls.Add(this.lblLastSyncTime);
            this.Controls.Add(this.lblTitleSync);
            this.Controls.Add(this.lblTitleDestination);
            this.Controls.Add(this.lblTitleStorage);
            this.Controls.Add(this.lblTitleNumber);
            this.Name = "UCMain";
            this.Size = new System.Drawing.Size(249, 271);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSync;
        private System.Windows.Forms.Button btnDestination;
        private System.Windows.Forms.Button btnStorage;
        private System.Windows.Forms.Button btnNumber;
        private System.Windows.Forms.Label lblLastSyncTime;
        private System.Windows.Forms.Label lblTitleSync;
        private System.Windows.Forms.Label lblTitleDestination;
        private System.Windows.Forms.Label lblTitleStorage;
        private System.Windows.Forms.Label lblTitleNumber;
        private System.Windows.Forms.Button btnOrderInput;
        private System.Windows.Forms.Button btnNodeScan;
        private System.Windows.Forms.Button btnThirdParty;
        private System.Windows.Forms.Button btnWaitUploadNode;
        private System.Windows.Forms.Button btnCenterNode;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblNews;
    }
}
