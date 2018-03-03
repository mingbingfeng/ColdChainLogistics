namespace C2LP.Manager.Console.FrmWarehouseControl
{
    partial class FrmWarehouseUpdated
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
            this.label6 = new System.Windows.Forms.Label();
            this.cmbStorageType = new System.Windows.Forms.ComboBox();
            this.checDefault = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.cmbPDA = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtWarehouseName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rdbDisable = new System.Windows.Forms.RadioButton();
            this.rdbEnabled = new System.Windows.Forms.RadioButton();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(30, 87);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 16;
            this.label6.Text = "存储类型";
            // 
            // cmbStorageType
            // 
            this.cmbStorageType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStorageType.FormattingEnabled = true;
            this.cmbStorageType.Location = new System.Drawing.Point(108, 85);
            this.cmbStorageType.Name = "cmbStorageType";
            this.cmbStorageType.Size = new System.Drawing.Size(167, 20);
            this.cmbStorageType.TabIndex = 3;
            // 
            // checDefault
            // 
            this.checDefault.AutoSize = true;
            this.checDefault.Checked = true;
            this.checDefault.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checDefault.Location = new System.Drawing.Point(281, 50);
            this.checDefault.Name = "checDefault";
            this.checDefault.Size = new System.Drawing.Size(72, 16);
            this.checDefault.TabIndex = 5;
            this.checDefault.Text = "默认设备";
            this.checDefault.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(108, 158);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(104, 23);
            this.btnSave.TabIndex = 6;
            this.btnSave.Text = "添加";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cmbPDA
            // 
            this.cmbPDA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPDA.FormattingEnabled = true;
            this.cmbPDA.Location = new System.Drawing.Point(108, 48);
            this.cmbPDA.Name = "cmbPDA";
            this.cmbPDA.Size = new System.Drawing.Size(167, 20);
            this.cmbPDA.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "PDA设备";
            // 
            // txtWarehouseName
            // 
            this.txtWarehouseName.Location = new System.Drawing.Point(108, 12);
            this.txtWarehouseName.Name = "txtWarehouseName";
            this.txtWarehouseName.Size = new System.Drawing.Size(167, 21);
            this.txtWarehouseName.TabIndex = 1;
            this.txtWarehouseName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtWarehouseName_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 10;
            this.label2.Text = "仓库名称";
            // 
            // rdbDisable
            // 
            this.rdbDisable.AutoSize = true;
            this.rdbDisable.Location = new System.Drawing.Point(189, 120);
            this.rdbDisable.Name = "rdbDisable";
            this.rdbDisable.Size = new System.Drawing.Size(47, 16);
            this.rdbDisable.TabIndex = 4;
            this.rdbDisable.Text = "停用";
            this.rdbDisable.UseVisualStyleBackColor = true;
            // 
            // rdbEnabled
            // 
            this.rdbEnabled.AutoSize = true;
            this.rdbEnabled.Checked = true;
            this.rdbEnabled.Location = new System.Drawing.Point(108, 120);
            this.rdbEnabled.Name = "rdbEnabled";
            this.rdbEnabled.Size = new System.Drawing.Size(47, 16);
            this.rdbEnabled.TabIndex = 4;
            this.rdbEnabled.TabStop = true;
            this.rdbEnabled.Text = "启用";
            this.rdbEnabled.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(30, 122);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 17;
            this.label8.Text = "激活状态";
            // 
            // FrmWarehouseUpdated
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 193);
            this.Controls.Add(this.rdbDisable);
            this.Controls.Add(this.rdbEnabled);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cmbStorageType);
            this.Controls.Add(this.checDefault);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmbPDA);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtWarehouseName);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmWarehouseUpdated";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "仓库设置";
            this.Load += new System.EventHandler(this.FrmWarehouseUpdated_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbStorageType;
        private System.Windows.Forms.CheckBox checDefault;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cmbPDA;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtWarehouseName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rdbDisable;
        private System.Windows.Forms.RadioButton rdbEnabled;
        private System.Windows.Forms.Label label8;
    }
}