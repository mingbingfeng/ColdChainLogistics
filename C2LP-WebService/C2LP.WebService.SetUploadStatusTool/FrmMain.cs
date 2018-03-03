using C2LP.WebService.SetUploadStatusTool.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace C2LP.WebService.SetUploadStatusTool
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = MyServer.GetAllNullProgress();
        }

        private void btnUpdate1_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridView1.SelectedRows[0];
            string id = row.Cells[0].Value.ToString();
            string relationId = row.Cells[1].Value.ToString();
            string storageId = row.Cells[2].Value.ToString();
            string nodeTime = DateTime.Parse(row.Cells[5].Value.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
            bool result = MyServer.UpdateProgress(relationId, storageId, nodeTime);
            txt1.Text+= "["+id+"]更新成功\r\n";
        }

        private void btnUpdateAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string id = row.Cells[0].Value.ToString();
                string relationId = row.Cells[1].Value.ToString();
                string storageId = row.Cells[2].Value.ToString();
                string nodeTime = DateTime.Parse(row.Cells[5].Value.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                bool result = MyServer.UpdateProgress(relationId, storageId, nodeTime);
                txt1.Text += "[" + id + "]更新成功\r\n";
                Thread.Sleep(100);
                Application.DoEvents();
            }
        }

        private void btnUpdateAllArrived_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string id = row.Cells[0].Value.ToString();
                string relationId = row.Cells[1].Value.ToString();
                string storageId = row.Cells[2].Value.ToString();
                bool result = MyServer.UpdateArrivedProgress(storageId,relationId, id);
                txt1.Text += "[" + id + "]更新成功\r\n";
                Thread.Sleep(100);
                Application.DoEvents();
            }
        }

        private void btnSearchArrived_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = MyServer.GetAllNullProgress_Arrived();
        }
    }
}
