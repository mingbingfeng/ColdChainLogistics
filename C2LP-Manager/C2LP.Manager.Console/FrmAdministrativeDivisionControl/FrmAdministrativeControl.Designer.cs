namespace C2LP.Manager.Console.FrmAdministrativeDivisionControl
{
    partial class FrmAdministrativeControl
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
            this.components = new System.ComponentModel.Container();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.checkmenu = new System.Windows.Forms.ToolStripMenuItem();
            this.addmenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ecitmenu = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // treeView1
            // 
            this.treeView1.ContextMenuStrip = this.contextMenuStrip1;
            this.treeView1.Location = new System.Drawing.Point(20, 15);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(755, 504);
            this.treeView1.TabIndex = 2;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkmenu,
            this.addmenu,
            this.ecitmenu});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(185, 70);
            // 
            // checkmenu
            // 
            this.checkmenu.Name = "checkmenu";
            this.checkmenu.Size = new System.Drawing.Size(184, 22);
            this.checkmenu.Text = "查看下一级行政区域";
            this.checkmenu.Click += new System.EventHandler(this.checkmenu_Click);
            // 
            // addmenu
            // 
            this.addmenu.Name = "addmenu";
            this.addmenu.Size = new System.Drawing.Size(184, 22);
            this.addmenu.Text = "新增下一级行政区域";
            this.addmenu.Click += new System.EventHandler(this.addmenu_Click);
            // 
            // ecitmenu
            // 
            this.ecitmenu.Name = "ecitmenu";
            this.ecitmenu.Size = new System.Drawing.Size(184, 22);
            this.ecitmenu.Text = "编辑当前行政区域";
            this.ecitmenu.Click += new System.EventHandler(this.ecitmenu_Click);
            // 
            // FrmAdministrativeControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 531);
            this.Controls.Add(this.treeView1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmAdministrativeControl";
            this.Load += new System.EventHandler(this.FrmAdministrativeControl_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem checkmenu;
        private System.Windows.Forms.ToolStripMenuItem addmenu;
        private System.Windows.Forms.ToolStripMenuItem ecitmenu;
    }
}