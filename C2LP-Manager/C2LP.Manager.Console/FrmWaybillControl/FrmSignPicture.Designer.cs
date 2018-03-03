namespace C2LP.Manager.Console.FrmWaybillControl
{
    partial class FrmSignPicture
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
            this.picPicture = new System.Windows.Forms.PictureBox();
            this.cmbImage = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnRefurbish = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // picPicture
            // 
            this.picPicture.Location = new System.Drawing.Point(30, 43);
            this.picPicture.Name = "picPicture";
            this.picPicture.Size = new System.Drawing.Size(586, 333);
            this.picPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picPicture.TabIndex = 0;
            this.picPicture.TabStop = false;
            // 
            // cmbImage
            // 
            this.cmbImage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbImage.FormattingEnabled = true;
            this.cmbImage.Location = new System.Drawing.Point(110, 17);
            this.cmbImage.Name = "cmbImage";
            this.cmbImage.Size = new System.Drawing.Size(250, 20);
            this.cmbImage.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "请选择图片";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(292, 177);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "label2";
            // 
            // btnRefurbish
            // 
            this.btnRefurbish.Location = new System.Drawing.Point(383, 15);
            this.btnRefurbish.Name = "btnRefurbish";
            this.btnRefurbish.Size = new System.Drawing.Size(75, 23);
            this.btnRefurbish.TabIndex = 4;
            this.btnRefurbish.Text = "刷新";
            this.btnRefurbish.UseVisualStyleBackColor = true;
            this.btnRefurbish.Click += new System.EventHandler(this.btnRefurbish_Click);
            // 
            // FrmSignPicture
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 393);
            this.Controls.Add(this.btnRefurbish);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbImage);
            this.Controls.Add(this.picPicture);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "FrmSignPicture";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "签收图片";
            this.Load += new System.EventHandler(this.FrmSignPicture_Load);
            this.SizeChanged += new System.EventHandler(this.picPicture_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.picPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox picPicture;
        private System.Windows.Forms.ComboBox cmbImage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnRefurbish;
    }
}