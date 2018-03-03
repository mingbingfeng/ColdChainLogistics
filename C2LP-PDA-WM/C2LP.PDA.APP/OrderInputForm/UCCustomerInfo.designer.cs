namespace C2LP.PDA.APP.OrderInput
{
    partial class UCCustomerInfo
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
            this.chkInput = new System.Windows.Forms.CheckBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.cboCustomer = new System.Windows.Forms.ComboBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.cboAId = new System.Windows.Forms.ComboBox();
            this.cboCId = new System.Windows.Forms.ComboBox();
            this.cboPId = new System.Windows.Forms.ComboBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblPerson = new System.Windows.Forms.Label();
            this.lblRegion = new System.Windows.Forms.Label();
            this.lblCustomer = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chkInput
            // 
            this.chkInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkInput.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.chkInput.Location = new System.Drawing.Point(162, 22);
            this.chkInput.Name = "chkInput";
            this.chkInput.Size = new System.Drawing.Size(80, 16);
            this.chkInput.TabIndex = 23;
            this.chkInput.Tag = "0";
            this.chkInput.Text = "手动/自动";
            this.chkInput.Visible = false;
            // 
            // txtPhone
            // 
            this.txtPhone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPhone.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtPhone.Location = new System.Drawing.Point(73, 80);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(167, 19);
            this.txtPhone.TabIndex = 29;
            this.txtPhone.Tag = "收货电话：";
            // 
            // cboCustomer
            // 
            this.cboCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboCustomer.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboCustomer.Location = new System.Drawing.Point(73, 41);
            this.cboCustomer.Name = "cboCustomer";
            this.cboCustomer.Size = new System.Drawing.Size(167, 19);
            this.cboCustomer.TabIndex = 34;
            // 
            // txtAddress
            // 
            this.txtAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAddress.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtAddress.Location = new System.Drawing.Point(9, 21);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(231, 19);
            this.txtAddress.TabIndex = 33;
            this.txtAddress.Tag = "";
            // 
            // cboAId
            // 
            this.cboAId.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboAId.DisplayMember = "Name";
            this.cboAId.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboAId.Location = new System.Drawing.Point(180, 2);
            this.cboAId.Name = "cboAId";
            this.cboAId.Size = new System.Drawing.Size(60, 19);
            this.cboAId.TabIndex = 32;
            this.cboAId.Tag = "22";
            this.cboAId.ValueMember = "Id";
            // 
            // cboCId
            // 
            this.cboCId.DisplayMember = "Name";
            this.cboCId.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboCId.Location = new System.Drawing.Point(130, 2);
            this.cboCId.Name = "cboCId";
            this.cboCId.Size = new System.Drawing.Size(55, 19);
            this.cboCId.TabIndex = 30;
            this.cboCId.Tag = "2";
            this.cboCId.ValueMember = "Id";
            // 
            // cboPId
            // 
            this.cboPId.DisplayMember = "Name";
            this.cboPId.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.cboPId.Location = new System.Drawing.Point(73, 2);
            this.cboPId.Name = "cboPId";
            this.cboPId.Size = new System.Drawing.Size(59, 19);
            this.cboPId.TabIndex = 31;
            this.cboPId.ValueMember = "Id";
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.txtName.Location = new System.Drawing.Point(73, 60);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(167, 19);
            this.txtName.TabIndex = 28;
            this.txtName.Tag = "收货人：";
            // 
            // lblPhone
            // 
            this.lblPhone.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblPhone.Location = new System.Drawing.Point(9, 81);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(65, 16);
            this.lblPhone.Text = "发货电话：";
            // 
            // lblPerson
            // 
            this.lblPerson.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblPerson.Location = new System.Drawing.Point(9, 61);
            this.lblPerson.Name = "lblPerson";
            this.lblPerson.Size = new System.Drawing.Size(65, 14);
            this.lblPerson.Text = "发货人：";
            // 
            // lblRegion
            // 
            this.lblRegion.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblRegion.Location = new System.Drawing.Point(9, 2);
            this.lblRegion.Name = "lblRegion";
            this.lblRegion.Size = new System.Drawing.Size(65, 15);
            this.lblRegion.Text = "发货地：";
            // 
            // lblCustomer
            // 
            this.lblCustomer.BackColor = System.Drawing.Color.Transparent;
            this.lblCustomer.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.lblCustomer.Location = new System.Drawing.Point(0, 40);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(72, 20);
            this.lblCustomer.TabIndex = 39;
            this.lblCustomer.Text = "发货单位：";
            this.lblCustomer.Click += new System.EventHandler(this.lblCustomer_Click);
            // 
            // UCCustomerInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.lblCustomer);
            this.Controls.Add(this.chkInput);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.cboCustomer);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.cboAId);
            this.Controls.Add(this.cboCId);
            this.Controls.Add(this.cboPId);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.lblPerson);
            this.Controls.Add(this.lblRegion);
            this.Name = "UCCustomerInfo";
            this.Size = new System.Drawing.Size(249, 100);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox chkInput;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.ComboBox cboCustomer;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.ComboBox cboAId;
        private System.Windows.Forms.ComboBox cboCId;
        private System.Windows.Forms.ComboBox cboPId;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblPerson;
        private System.Windows.Forms.Label lblRegion;
        private System.Windows.Forms.Button lblCustomer;
    }
}
