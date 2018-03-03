using C2LP.Manager.Console.ConsoleServerWebReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace C2LP.Manager.Console.FrmWarehouseControl
{
    public partial class FrmWarehouseUpdated : Form
    {
        public FrmWarehouseUpdated()
        {
            InitializeComponent();
        }
        ConsoleServerWebReference.ConsoleServer cs = new ConsoleServerWebReference.ConsoleServer();
        public Model_ColdstoragePDA mcp;
        public FrmWarehouseList _ParentWare;
        private void FrmWarehouseUpdated_Load(object sender, EventArgs e)
        {
            PDALoad();
            StorageType();
            UpdatedLoad();
        }
        /// <summary>
        /// pda设备
        /// </summary>
        public void PDALoad()
        {
            try
            {
                ResultModelOfArrayOfModel_PDAInfod4FqxSXX pda = cs.GetPDAList(0, true, null);
                if (pda.Code != 0)
                {
                    MessageBox.Show(pda.Message);
                }
                else
                {
                    cmbPDA.DisplayMember = "namek__BackingField";
                    cmbPDA.ValueMember = "idk__BackingField";
                    cmbPDA.DataSource = pda.Data;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        public void StorageType()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Value");
            dt.Columns.Add("Name");
            DataRow dr = dt.NewRow();
            dr[0] = 1;
            dr[1] = "冷库";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            //dr[0] = 2;
            //dr[1] = "车载";
            //dt.Rows.Add(dr);

            cmbStorageType.DisplayMember = "Name";
            cmbStorageType.ValueMember = "Value";
            cmbStorageType.DataSource = dt;
        }
        public void UpdatedLoad()
        {
            txtWarehouseName.Text = mcp.StorageNamek__BackingField;
            cmbPDA.Text = mcp.Namek__BackingField;
            if (mcp.StorageTypek__BackingField == Enum_StorageType.ColdStorage)
                cmbStorageType.Text = "冷库";
            else
                cmbStorageType.Text = "车载";
            if (mcp.isDefaultk__BackingField == Enum_IsDefault.Eefault)
                checDefault.Checked = true;
            else
                checDefault.Checked = false;
            if (mcp.StorageActivedk__BackingField == Enum_Active.Enabled)
                rdbEnabled.Checked = true;
            else
                rdbDisable.Checked = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtWarehouseName.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("仓库名称不能为空");
                    return;
                }
                if (Encoding.Default.GetBytes(txtWarehouseName.Text.Trim()).Length > 100)
                {
                    MessageBox.Show("仓库名称不能超过100");
                    return;
                }
                if (cmbPDA.Text.Trim()==string.Empty)
                {
                    MessageBox.Show("PDA设备不能为空");
                    return;
                }
                if (cmbStorageType.Text.Trim()==string.Empty)
                {
                    MessageBox.Show("存储类型不能为空");
                    return;
                }
                Model_ColdStorage mcodstorage = new Model_ColdStorage();
                mcodstorage.Idk__BackingField = mcp.StorageIdk__BackingField;
                mcodstorage.StorageNamek__BackingField = txtWarehouseName.Text;
                if (cmbStorageType.Text == "冷库")
                    mcodstorage.StorageTypek__BackingField = Enum_StorageType.ColdStorage;
                else
                    mcodstorage.StorageTypek__BackingField = Enum_StorageType.CarStorage;
                mcodstorage.Driverk__BackingField = mcp.Driverk__BackingField;
                mcodstorage.DriverTelk__BackingField = mcp.DriverTelk__BackingField;
                mcodstorage.Remarkk__BackingField = mcp.Remarkk__BackingField;
                mcodstorage.CreateAtk__BackingField = mcp.StorageCreateAtk__BackingField;
                if (rdbEnabled.Checked == true)
                    mcodstorage.Activedk__BackingField = Enum_Active.Enabled;
                else
                    mcodstorage.Activedk__BackingField = Enum_Active.Disable;
                int PDAid = Convert.ToInt32(cmbPDA.SelectedValue);
                int DefaultDevice;
                if (checDefault.Checked == true)
                {
                        DefaultDevice = (int)Enum_IsDefault.Eefault;
                        Model_Storage_Device sto = new Model_Storage_Device();
                        sto.deviceIdk__BackingField = PDAid;
                        sto.isDefaultk__BackingField = DefaultDevice;
                        ResultModelOfModel_Storage_Deviced4FqxSXX st = cs.GetDefaultDevice(sto);
                        if (st.Code != 0)
                            MessageBox.Show("操作失败");
                }
                else
                    DefaultDevice = (int)Enum_IsDefault.NotDefault;
                ResultModelOfModel_ColdStoraged4FqxSXX coldstoreage = cs.EditColdstorage(mcodstorage, DefaultDevice, true, mcp.StorageDeviceIdk__BackingField, true, PDAid, true, true, true);
                if (coldstoreage.Code != 0)
                {
                    MessageBox.Show(coldstoreage.Message);
                }
                else
                {
                    MessageBox.Show("操作成功");
                    _ParentWare.waLoad();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void txtWarehouseName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 91 || e.KeyChar == 93)
                e.Handled = true;
            else
                e.Handled = false;
        }
    }
}
