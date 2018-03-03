namespace C2LP.Manager.Console.FrmWarehouseControl
{
    partial class FrmProbeEdit
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
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.checkActivation = new System.Windows.Forms.CheckBox();
            this.cmbProbeType = new System.Windows.Forms.ComboBox();
            this.txtProbeName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(97, 164);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(117, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "保 存";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(28, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 18;
            this.label4.Text = "激活状态";
            // 
            // checkActivation
            // 
            this.checkActivation.AutoSize = true;
            this.checkActivation.Checked = true;
            this.checkActivation.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkActivation.Location = new System.Drawing.Point(97, 101);
            this.checkActivation.Name = "checkActivation";
            this.checkActivation.Size = new System.Drawing.Size(48, 16);
            this.checkActivation.TabIndex = 3;
            this.checkActivation.Text = "激活";
            this.checkActivation.UseVisualStyleBackColor = true;
            // 
            // cmbProbeType
            // 
            this.cmbProbeType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbProbeType.FormattingEnabled = true;
            this.cmbProbeType.Location = new System.Drawing.Point(97, 62);
            this.cmbProbeType.Name = "cmbProbeType";
            this.cmbProbeType.Size = new System.Drawing.Size(166, 20);
            this.cmbProbeType.TabIndex = 2;
            // 
            // txtProbeName
            // 
            this.txtProbeName.Location = new System.Drawing.Point(97, 28);
            this.txtProbeName.Name = "txtProbeName";
            this.txtProbeName.Size = new System.Drawing.Size(166, 21);
            this.txtProbeName.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(28, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "探头类型";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(28, 31);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "探头名称";
            // 
            // FrmProbeEdit
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 211);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.checkActivation);
            this.Controls.Add(this.cmbProbeType);
            this.Controls.Add(this.txtProbeName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmProbeEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "仓库探头设置";
            this.Load += new System.EventHandler(this.FrmProbeEdit_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkActivation;
        private System.Windows.Forms.ComboBox cmbProbeType;
        private System.Windows.Forms.TextBox txtProbeName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}