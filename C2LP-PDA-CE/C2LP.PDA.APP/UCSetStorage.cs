using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using C2LP.PDA.Datas.BLL;
using C2LP.PDA.Datas.Model;
using System.Reflection;

namespace C2LP.PDA.APP
{
    public partial class UCSetStorage : UserControl
    {
        public UCSetStorage()
        {
            InitializeComponent();
            InitStorageList();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            ColdStorage select = (ColdStorage)cboStorage.SelectedItem;
            if (select == null)
            {
                MessageBox.Show("请选择一个车载或冷库作为当前设备的冷藏载体!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                return;
            }
            Common._StorageName = select.storageName.Replace("[默认]","");
            btnCancel_Click(sender, e);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FrmParent.ParentForm.OpenForm(PageState.Main);
        }

        private void InitStorageList()
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                cboStorage.Items.Clear();
                cboStorage.DisplayMember = "remark";
                cboStorage.ValueMember = "storageName";
                List<ColdStorage> list = StorageServer.GetStorageList(null);
                foreach (ColdStorage item in list)
                {
                    item.remark = string.Format("{0} [{1}] {2}", item.storageName, item.storageType == 1 ? "冷库" : "车载", item.driver);
                    int index = cboStorage.Items.Add(item);
                    if (item.storageName.Replace("[默认]", "") == Common._StorageName)
                        cboStorage.SelectedIndex = index;
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
