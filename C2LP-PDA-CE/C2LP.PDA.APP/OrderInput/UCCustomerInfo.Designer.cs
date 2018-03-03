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
            this.cboCustomer = new System.Windows.Forms.ComboBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.cboAId = new System.Windows.Forms.ComboBox();
            this.cboCId = new System.Windows.Forms.ComboBox();
            this.cboPId = new System.Windows.Forms.ComboBox();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblPhone = new System.Windows.Forms.Label();
            this.lblPerson = new System.Windows.Forms.Label();
            this.lblCustomer = new System.Windows.Forms.Label();
            this.lblRegion = new System.Windows.Forms.Label();
            this.chkInput = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // cboCustomer
            // 
            this.cboCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cboCustomer.Location = new System.Drawing.Point(67, 58);
            this.cboCustomer.Name = "cboCustomer";
            this.cboCustomer.Size = new System.Drawing.Size(160, 23);
            this.cboCustomer.TabIndex = 16;
            // 
            // txtAddress
            // 
            this.txtAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtAddress.Location = new System.Drawing.Point(3, 31);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(224, 23);
            this.txtAddress.TabIndex = 15;
            // 
            // cboAId
            // 
            this.cboAId.DisplayMember = "Name";
            this.cboAId.Location = new System.Drawing.Point(167, 4);
            this.cboAId.Name = "cboAId";
            this.cboAId.Size = new System.Drawing.Size(60, 23);
            this.cboAId.TabIndex = 14;
            this.cboAId.Tag = "22";
            this.cboAId.ValueMember = "Id";
            // 
            // cboCId
            // 
            this.cboCId.DisplayMember = "Name";
            this.cboCId.Location = new System.Drawing.Point(109, 4);
            this.cboCId.Name = "cboCId";
            this.cboCId.Size = new System.Drawing.Size(60, 23);
            this.cboCId.TabIndex = 13;
            this.cboCId.Tag = "2";
            this.cboCId.ValueMember = "Id";
            // 
            // cboPId
            // 
            this.cboPId.DisplayMember = "Name";
            this.cboPId.Location = new System.Drawing.Point(67, 4);
            this.cboPId.Name = "cboPId";
            this.cboPId.Size = new System.Drawing.Size(44, 23);
            this.cboPId.TabIndex = 12;
            this.cboPId.ValueMember = "Id";
            // 
            // txtPhone
            // 
            this.txtPhone.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPhone.Location = new System.Drawing.Point(67, 113);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(160, 23);
            this.txtPhone.TabIndex = 18;
            // 
            // txtName
            // 
            this.txtName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtName.Location = new System.Drawing.Point(67, 86);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(160, 23);
            this.txtName.TabIndex = 17;
            // 
            // lblPhone
            // 
            this.lblPhone.Location = new System.Drawing.Point(3, 116);
            this.lblPhone.Name = "lblPhone";
            this.lblPhone.Size = new System.Drawing.Size(80, 20);
            this.lblPhone.Text = "发货电话：";
            // 
            // lblPerson
            // 
            this.lblPerson.Location = new System.Drawing.Point(3, 89);
            this.lblPerson.Name = "lblPerson";
            this.lblPerson.Size = new System.Drawing.Size(65, 20);
            this.lblPerson.Text = "发货人：";
            // 
            // lblCustomer
            // 
            this.lblCustomer.Location = new System.Drawing.Point(3, 61);
            this.lblCustomer.Name = "lblCustomer";
            this.lblCustomer.Size = new System.Drawing.Size(80, 20);
            this.lblCustomer.Text = "发货单位：";
            // 
            // lblRegion
            // 
            this.lblRegion.Location = new System.Drawing.Point(3, 7);
            this.lblRegion.Name = "lblRegion";
            this.lblRegion.Size = new System.Drawing.Size(65, 20);
            this.lblRegion.Text = "发货地：";
            // 
            // chkInput
            // 
            this.chkInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkInput.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular);
            this.chkInput.Location = new System.Drawing.Point(137, 33);
            this.chkInput.Name = "chkInput";
            this.chkInput.Size = new System.Drawing.Size(88, 20);
            this.chkInput.TabIndex = 23;
            this.chkInput.Tag = "0";
            this.chkInput.Text = "手动/自动";
            this.chkInput.Visible = false;
            // 
            // UCCustomerInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.chkInput);
            this.Controls.Add(this.cboCustomer);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.cboAId);
            this.Controls.Add(this.cboCId);
            this.Controls.Add(this.cboPId);
            this.Controls.Add(this.txtPhone);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblPhone);
            this.Controls.Add(this.lblPerson);
            this.Controls.Add(this.lblCustomer);
            this.Controls.Add(this.lblRegion);
            this.Name = "UCCustomerInfo";
            this.Size = new System.Drawing.Size(250, 140);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cboCustomer;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.ComboBox cboAId;
        private System.Windows.Forms.ComboBox cboCId;
        private System.Windows.Forms.ComboBox cboPId;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblPhone;
        private System.Windows.Forms.Label lblPerson;
        private System.Windows.Forms.Label lblCustomer;
        private System.Windows.Forms.Label lblRegion;
        private System.Windows.Forms.CheckBox chkInput;
    }
}
