using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using C2LP.PDA.Datas.BLL;

namespace C2LP.PDA.APP
{
    public partial class UCSetDestin : UserControl
    {
        public UCSetDestin()
        {
            InitializeComponent();
            InitDestinList();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FrmParent.ParentForm.OpenForm(PageState.Main);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string selectDestin = cboDestins.Text;
            if (selectDestin == null)
            {
                MessageBox.Show("请选择一个目的地作为当前设备的目的地!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                return;
            }
            Common._Destination = selectDestin;
            btnCancel_Click(sender, e);
        }

        private void InitDestinList() {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                cboDestins.Items.Clear();
                List<string> list = DestinServer.GetDestins();;
                foreach (string item in list)
                {
                   int index = cboDestins.Items.Add(item);
                   if (item == Common._Destination)
                       cboDestins.SelectedIndex = index;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "加载失败", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }
            finally {
                Cursor.Current = Cursors.Default;
            }
        }
    }
}
