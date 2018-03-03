namespace C2LP.Manager.Console.FrmVehicleMountedControl
{
    partial class FrmVehicleUpdate
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtRemark = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbStorageType = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.checDefault = new System.Windows.Forms.CheckBox();
            this.cmbPDA = new System.Windows.Forms.ComboBox();
            this.txtTelephone = new System.Windows.Forms.TextBox();
            this.txtPilot = new System.Windows.Forms.TextBox();
            this.txtLicensePlate = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rdbDisable = new System.Windows.Forms.RadioButton();
            this.rdbEnabled = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtRemark
            // 
            this.txtRemark.Location = new System.Drawing.Point(111, 193);
            this.txtRemark.Name = "txtRemark";
            this.txtRemark.Size = new System.Drawing.Size(167, 21);
            this.txtRemark.TabIndex = 23;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(68, 196);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 22;
            this.label7.Text = "备注";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(44, 94);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 21;
            this.label6.Text = "存储类型";
            // 
            // cmbStorageType
            // 
            this.cmbStorageType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStorageType.FormattingEnabled = true;
            this.cmbStorageType.Location = new System.Drawing.Point(111, 91);
            this.cmbStorageType.Name = "cmbStorageType";
            this.cmbStorageType.Size = new System.Drawing.Size(167, 20);
            this.cmbStorageType.TabIndex = 20;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(111, 266);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(104, 23);
            this.button1.TabIndex = 19;
            this.button1.Text = "保 存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // checDefault
            // 
            this.checDefault.AutoSize = true;
            this.checDefault.Checked = true;
            this.checDefault.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checDefault.Location = new System.Drawing.Point(284, 61);
            this.checDefault.Name = "checDefault";
            this.checDefault.Size = new System.Drawing.Size(72, 16);
            this.checDefault.TabIndex = 18;
            this.checDefault.Text = "默认设备";
            this.checDefault.UseVisualStyleBackColor = true;
            // 
            // cmbPDA
            // 
            this.cmbPDA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPDA.FormattingEnabled = true;
            this.cmbPDA.Location = new System.Drawing.Point(111, 59);
            this.cmbPDA.Name = "cmbPDA";
            this.cmbPDA.Size = new System.Drawing.Size(167, 20);
            this.cmbPDA.TabIndex = 17;
            // 
            // txtTelephone
            // 
            this.txtTelephone.Location = new System.Drawing.Point(111, 158);
            this.txtTelephone.Name = "txtTelephone";
            this.txtTelephone.Size = new System.Drawing.Size(167, 21);
            this.txtTelephone.TabIndex = 14;
            // 
            // txtPilot
            // 
            this.txtPilot.Location = new System.Drawing.Point(111, 124);
            this.txtPilot.Name = "txtPilot";
            this.txtPilot.Size = new System.Drawing.Size(167, 21);
            this.txtPilot.TabIndex = 15;
            // 
            // txtLicensePlate
            // 
            this.txtLicensePlate.Location = new System.Drawing.Point(111, 25);
            this.txtLicensePlate.Name = "txtLicensePlate";
            this.txtLicensePlate.Size = new System.Drawing.Size(167, 21);
            this.txtLicensePlate.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(32, 161);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "驾驶员电话";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(50, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 11;
            this.label4.Text = "PDA设备";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(32, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 12;
            this.label3.Text = "车载驾驶员";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 28);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 13;
            this.label2.Text = "车载系统车牌";
            // 
            // rdbDisable
            // 
            this.rdbDisable.AutoSize = true;
            this.rdbDisable.Location = new System.Drawing.Point(192, 229);
            this.rdbDisable.Name = "rdbDisable";
            this.rdbDisable.Size = new System.Drawing.Size(47, 16);
            this.rdbDisable.TabIndex = 25;
            this.rdbDisable.Text = "停用";
            this.rdbDisable.UseVisualStyleBackColor = true;
            // 
            // rdbEnabled
            // 
            this.rdbEnabled.AutoSize = true;
            this.rdbEnabled.Checked = true;
            this.rdbEnabled.Location = new System.Drawing.Point(111, 229);
            this.rdbEnabled.Name = "rdbEnabled";
            this.rdbEnabled.Size = new System.Drawing.Size(47, 16);
            this.rdbEnabled.TabIndex = 26;
            this.rdbEnabled.TabStop = true;
            this.rdbEnabled.Text = "启用";
            this.rdbEnabled.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(44, 231);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 24;
            this.label8.Text = "激活状态";
            // 
            // FrmVehicleUpdate
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 301);
            this.Controls.Add(this.rdbDisable);
            this.Controls.Add(this.rdbEnabled);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.txtRemark);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbStorageType);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.checDefault);
            this.Controls.Add(this.cmbPDA);
            this.Controls.Add(this.txtTelephone);
            this.Controls.Add(this.txtPilot);
            this.Controls.Add(this.txtLicensePlate);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmVehicleUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "车载设置";
            this.Load += new System.EventHandler(this.FrmVehicleUpdate_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtRemark;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbStorageType;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox checDefault;
        private System.Windows.Forms.ComboBox cmbPDA;
        private System.Windows.Forms.TextBox txtTelephone;
        private System.Windows.Forms.TextBox txtPilot;
        private System.Windows.Forms.TextBox txtLicensePlate;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rdbDisable;
        private System.Windows.Forms.RadioButton rdbEnabled;
        private System.Windows.Forms.Label label8;
    }
}