using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using C2LP.Manager.Console.ConsoleServerWebReference;

namespace C2LP.Manager.Console.FrmWarehouseControl
{
    public partial class FrmWarehouseBind : Form
    {
        public FrmWarehouseBind()
        {
            InitializeComponent();
        }
        ConsoleServerWebReference.ConsoleServer cs = new ConsoleServerWebReference.ConsoleServer();
        public Model_ColdstoragePDA mcp;
        public FrmWarehouseList _ParentWare;
        private void FrmWarehouseBind_Load(object sender, EventArgs e)
        {
            
            PDALoad();
            IsDefault();
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
        public void IsDefault()
        {
            cmbPDA.Text = mcp.Namek__BackingField;
            if (mcp.isDefaultk__BackingField==Enum_IsDefault.Eefault)
            {
                checDefault.Checked = true;
            }
            else
            {
                checDefault.Checked = false;
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbPDA.Text.Trim()==string.Empty)
                {
                    MessageBox.Show("PDA设备不能为空");
                    return;
                }
                Model_ColdStorage mcs = new Model_ColdStorage();
                mcs.Idk__BackingField = mcp.StorageIdk__BackingField;
                mcs.StorageNamek__BackingField = mcp.StorageNamek__BackingField;
                mcs.StorageTypek__BackingField = mcp.StorageTypek__BackingField;
                mcs.Driverk__BackingField = mcp.Driverk__BackingField;
                mcs.DriverTelk__BackingField = mcp.DriverTelk__BackingField;
                mcs.Remarkk__BackingField = mcp.Remarkk__BackingField;
                // mcs.CreateAtk__BackingField = mcp.CreateAtk__BackingField;
                mcs.CreateAtk__BackingField = mcp.StorageCreateAtk__BackingField;
                int PDAid = Convert.ToInt32(cmbPDA.SelectedValue);
                int DefaultDevice;
                if (checDefault.Checked == true)
                {
                    //if (MessageBox.Show("是否默认该设备?", "Prompt", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //{
                        DefaultDevice = (int)Enum_IsDefault.Eefault;
                        Model_Storage_Device sto = new Model_Storage_Device();
                        sto.deviceIdk__BackingField = PDAid;
                        sto.isDefaultk__BackingField = DefaultDevice;
                        ResultModelOfModel_Storage_Deviced4FqxSXX st = cs.GetDefaultDevice(sto);
                        if (st.Code != 0)
                            MessageBox.Show("操作失败");
                    //}else
                    //    DefaultDevice = (int)Enum_IsDefault.NotDefault;
                }
                else
                    DefaultDevice = (int)Enum_IsDefault.NotDefault;
                ResultModelOfModel_ColdStoraged4FqxSXX coldstoreage = cs.EditColdstorage(mcs, DefaultDevice, true, mcp.StorageDeviceIdk__BackingField, true, PDAid, true, true, true);
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
    }
}
