using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using C2LP.PDA.Datas.BLL;
using C2LP.PDA.APP.PDAWebReference;

namespace C2LP.PDA.APP
{
    public partial class UCNodeList : UserControl
    {
        public UCNodeList()
        {
            InitializeComponent();
            _WaitTime = _DefaultWaitTime;
        }
        private int _WaitTime ;
        private int _DefaultWaitTime = Common._UploadCycle;

        private void LoadNodeList()
        {
            try
            {
                btnSearch.Enabled = false;
                btnCancel.Enabled = false;
                Cursor.Current = Cursors.WaitCursor;

                pnlNodeList.Controls.Clear();
                Application.DoEvents();
                List<Model_Waybill_Node> list = new List<Model_Waybill_Node>();
                DataTable dt = WaybillServer.GetWaitUploadNode(100);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Model_Waybill_Node n = new Model_Waybill_Node();
                        n.BaseIdk__BackingField = row["ordernumber"].ToString();
                        n.Contentk__BackingField = row["content"].ToString();
                        n.Idk__BackingField = Convert.ToInt32(row["id"]);
                        n.operateAtk__BackingField = Convert.ToDateTime(row["operateAt"]);
                        n.StorageIdk__BackingField = Convert.ToInt32(row["storageId"]);
                        n.StorageNamek__BackingField = row["storageName"].ToString();
                        n.Arrivedk__BackingField = (Enum_Arrived)Enum.ToObject(typeof(Enum_Arrived), Convert.ToInt32(row["Arrived"]));
                        list.Add(n);
                    }
                }
                if (list.Count > 0)
                {
                    for (int i = list.Count - 1; i >= 0; i--)
                    {
                        UCNode c = new UCNode(list[i]);
                        c.Dock = DockStyle.Top;
                        pnlNodeList.Controls.Add(c);
                    }
                }
                else
                    MessageBox.Show("未查询到待上报的节点！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1);
            }
            catch (Exception ex)
            {
                MessageBox.Show("查询失败：" + ex.Message, "查询失败", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                btnCancel.Enabled = true;
                btnSearch.Text = "刷新(" + _WaitTime.ToString() + ")";
                timer1.Enabled = true;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadNodeList();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            timer1.Dispose();
            FrmParent.ParentForm.OpenForm(PageState.Main);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            _WaitTime--;
            if (_WaitTime <= 0)
            {
                timer1.Enabled = false;
                _WaitTime = _DefaultWaitTime;
                btnSearch.Text = "刷新";
                btnSearch.Enabled = true;
            }
            else
                btnSearch.Text = "刷新(" + _WaitTime.ToString() + ")";

        }
    }
}
