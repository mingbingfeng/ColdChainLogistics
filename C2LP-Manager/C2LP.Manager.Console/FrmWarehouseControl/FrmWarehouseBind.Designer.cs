namespace C2LP.Manager.Console.FrmWarehouseControl
{
    partial class FrmWarehouseBind
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
            this.checDefault = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.cmbPDA = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // checDefault
            // 
            this.checDefault.AutoSize = true;
            this.checDefault.Checked = true;
            this.checDefault.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checDefault.Location = new System.Drawing.Point(237, 27);
            this.checDefault.Name = "checDefault";
            this.checDefault.Size = new System.Drawing.Size(72, 16);
            this.checDefault.TabIndex = 2;
            this.checDefault.Text = "默认设备";
            this.checDefault.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(62, 63);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(104, 23);
            this.btnSave.TabIndex = 3;
            this.btnSave.Text = "保 存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cmbPDA
            // 
            this.cmbPDA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPDA.FormattingEnabled = true;
            this.cmbPDA.Location = new System.Drawing.Point(64, 25);
            this.cmbPDA.Name = "cmbPDA";
            this.cmbPDA.Size = new System.Drawing.Size(167, 20);
            this.cmbPDA.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "PDA设备";
            // 
            // FrmWarehouseBind
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 102);
            this.Controls.Add(this.checDefault);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.cmbPDA);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmWarehouseBind";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "绑定PDA设置";
            this.Load += new System.EventHandler(this.FrmWarehouseBind_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox checDefault;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.ComboBox cmbPDA;
        private System.Windows.Forms.Label label3;
    }
}