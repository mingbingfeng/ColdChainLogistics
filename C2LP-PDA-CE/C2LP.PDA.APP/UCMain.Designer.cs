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
            this.btnExit = new System.Windows.Forms.Button();
            this.btnConfig = new System.Windows.Forms.Button();
            this.btnThirdParty = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnWaitUploadNode = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSync
            // 
            this.btnSync.Location = new System.Drawing.Point(13, 104);
            this.btnSync.Name = "btnSync";
            this.btnSync.Size = new System.Drawing.Size(100, 47);
            this.btnSync.TabIndex = 25;
            this.btnSync.Text = "信息同步";
            this.btnSync.Click += new System.EventHandler(this.btnSync_Click);
            // 
            // btnDestination
            // 
            this.btnDestination.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDestination.BackColor = System.Drawing.Color.Transparent;
            this.btnDestination.Location = new System.Drawing.Point(80, 62);
            this.btnDestination.Name = "btnDestination";
            this.btnDestination.Size = new System.Drawing.Size(152, 23);
            this.btnDestination.TabIndex = 24;
            // 
            // btnStorage
            // 
            this.btnStorage.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStorage.BackColor = System.Drawing.Color.Transparent;
            this.btnStorage.Location = new System.Drawing.Point(80, 36);
            this.btnStorage.Name = "btnStorage";
            this.btnStorage.Size = new System.Drawing.Size(152, 23);
            this.btnStorage.TabIndex = 23;
            // 
            // btnNumber
            // 
            this.btnNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNumber.BackColor = System.Drawing.Color.Transparent;
            this.btnNumber.Location = new System.Drawing.Point(80, 11);
            this.btnNumber.Name = "btnNumber";
            this.btnNumber.Size = new System.Drawing.Size(152, 23);
            this.btnNumber.TabIndex = 22;
            // 
            // lblLastSyncTime
            // 
            this.lblLastSyncTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblLastSyncTime.Location = new System.Drawing.Point(94, 364);
            this.lblLastSyncTime.Name = "lblLastSyncTime";
            this.lblLastSyncTime.Size = new System.Drawing.Size(134, 20);
            this.lblLastSyncTime.Text = "从未同步过";
            // 
            // lblTitleSync
            // 
            this.lblTitleSync.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblTitleSync.Location = new System.Drawing.Point(22, 364);
            this.lblTitleSync.Name = "lblTitleSync";
            this.lblTitleSync.Size = new System.Drawing.Size(80, 20);
            this.lblTitleSync.Text = "最后同步：";
            // 
            // lblTitleDestination
            // 
            this.lblTitleDestination.Location = new System.Drawing.Point(10, 64);
            this.lblTitleDestination.Name = "lblTitleDestination";
            this.lblTitleDestination.Size = new System.Drawing.Size(75, 20);
            this.lblTitleDestination.Text = "目 的 地：";
            // 
            // lblTitleStorage
            // 
            this.lblTitleStorage.Location = new System.Drawing.Point(10, 40);
            this.lblTitleStorage.Name = "lblTitleStorage";
            this.lblTitleStorage.Size = new System.Drawing.Size(75, 20);
            this.lblTitleStorage.Text = "PDA绑定：";
            // 
            // lblTitleNumber
            // 
            this.lblTitleNumber.Location = new System.Drawing.Point(10, 16);
            this.lblTitleNumber.Name = "lblTitleNumber";
            this.lblTitleNumber.Size = new System.Drawing.Size(75, 20);
            this.lblTitleNumber.Text = "PDA编号：";
            // 
            // btnOrderInput
            // 
            this.btnOrderInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOrderInput.Location = new System.Drawing.Point(132, 104);
            this.btnOrderInput.Name = "btnOrderInput";
            this.btnOrderInput.Size = new System.Drawing.Size(100, 47);
            this.btnOrderInput.TabIndex = 31;
            this.btnOrderInput.Text = "运单录入";
            // 
            // btnNodeScan
            // 
            this.btnNodeScan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNodeScan.Location = new System.Drawing.Point(132, 174);
            this.btnNodeScan.Name = "btnNodeScan";
            this.btnNodeScan.Size = new System.Drawing.Size(100, 47);
            this.btnNodeScan.TabIndex = 31;
            this.btnNodeScan.Text = "节点扫描";
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnExit.Location = new System.Drawing.Point(132, 330);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(72, 20);
            this.btnExit.TabIndex = 37;
            this.btnExit.Text = "退出";
            this.btnExit.Visible = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnConfig
            // 
            this.btnConfig.Location = new System.Drawing.Point(13, 244);
            this.btnConfig.Name = "btnConfig";
            this.btnConfig.Size = new System.Drawing.Size(100, 47);
            this.btnConfig.TabIndex = 43;
            this.btnConfig.Text = "上报配置";
            this.btnConfig.Visible = false;
            // 
            // btnThirdParty
            // 
            this.btnThirdParty.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnThirdParty.Location = new System.Drawing.Point(13, 174);
            this.btnThirdParty.Name = "btnThirdParty";
            this.btnThirdParty.Size = new System.Drawing.Size(100, 47);
            this.btnThirdParty.TabIndex = 49;
            this.btnThirdParty.Text = "第三方";
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConnect.Location = new System.Drawing.Point(112, 150);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(20, 25);
            this.btnConnect.TabIndex = 43;
            this.btnConnect.Text = "拨号连接";
            // 
            // btnWaitUploadNode
            // 
            this.btnWaitUploadNode.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnWaitUploadNode.Location = new System.Drawing.Point(112, 222);
            this.btnWaitUploadNode.Name = "btnWaitUploadNode";
            this.btnWaitUploadNode.Size = new System.Drawing.Size(20, 25);
            this.btnWaitUploadNode.TabIndex = 43;
            this.btnWaitUploadNode.Text = "待上报节点";
            // 
            // UCMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.btnWaitUploadNode);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.btnThirdParty);
            this.Controls.Add(this.btnConfig);
            this.Controls.Add(this.btnExit);
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
            this.Size = new System.Drawing.Size(249, 393);
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
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnConfig;
        private System.Windows.Forms.Button btnThirdParty;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnWaitUploadNode;
    }
}
