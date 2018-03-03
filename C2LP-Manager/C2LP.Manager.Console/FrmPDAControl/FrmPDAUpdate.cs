using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using C2LP.Manager.Console.ConsoleServerWebReference;

namespace C2LP.Manager.Console.FrmPDAControl
{
    public partial class FrmPDAUpdate : Form
    {
        ConsoleServerWebReference.ConsoleServer cs = new ConsoleServerWebReference.ConsoleServer();
        public Model_PDAInfo udtmp;
        public FrmPDAList _ParentPad;
        public FrmPDAUpdate()
        {
            InitializeComponent();
        }

        private void FrmPDAUpdate_Load(object sender, EventArgs e)
        {
            txtDeviceNumber.Text = udtmp.Numberk__BackingField;
            txtDeviceName.Text = udtmp.Namek__BackingField;
            if (udtmp.Activedk__BackingField == Enum_Active.Enabled)
                rdbEnabled.Checked = true;
            else
                rdbDisable.Checked = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDeviceNumber.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("设备编号不能为空");
                    return;
                }
                if (Encoding.Default.GetBytes(txtDeviceNumber.Text.Trim()).Length>10)
                {
                    MessageBox.Show("设备编号不能超过10");
                    return;
                }
                if (txtDeviceName.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("设备名称不能为空");
                    return;
                }
                if (Encoding.Default.GetBytes(txtDeviceName.Text.Trim()).Length>50)
                {
                    MessageBox.Show("设备名称不能超过50");
                    return;
                }
                Model_PDAInfo pda = new Model_PDAInfo();
                pda.Idk__BackingField = udtmp.Idk__BackingField;
                pda.Numberk__BackingField = txtDeviceNumber.Text.Trim();
                pda.Namek__BackingField = txtDeviceName.Text.Trim();
                pda.CreateAtk__BackingField = udtmp.CreateAtk__BackingField;
                if (rdbEnabled.Checked == true)
                    pda.Activedk__BackingField = Enum_Active.Enabled;
                else
                    pda.Activedk__BackingField = Enum_Active.Disable;
                ResultModelOfModel_PDAInfod4FqxSXX pdalist = cs.EditPDA(pda);
                if (pdalist.Code != 0)
                {
                    MessageBox.Show(pdalist.Message);
                }
                else
                {
                    MessageBox.Show("操作成功");
                    _ParentPad.PdaLoad();
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
