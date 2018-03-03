using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using C2LP.Manager.Console.ConsoleServerWebReference;

namespace C2LP.Manager.Console.FrmVehicleMountedControl
{
    public partial class FrmVMProbeEdit : Form
    {
        ConsoleServerWebReference.ConsoleServer cs = new ConsoleServerWebReference.ConsoleServer();
        public Model_AiInfo mai;
        public FrmVMProbeList _ParentVMP;
        public FrmVMProbeEdit()
        {
            InitializeComponent();
        }

        private void FrmVMProbeEdit_Load(object sender, EventArgs e)
        {
            Probe();
            load();
        }
        public void Probe()
        {
            //1 温度 2 湿度 3 经度 4 纬度
            DataTable dt = new DataTable();
            dt.Columns.Add("Value");
            dt.Columns.Add("Name");
            DataRow dr = dt.NewRow();
            dr[0] = 1;
            dr[1] = "温度";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = 2;
            dr[1] = "湿度";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = 3;
            dr[1] = "经度";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = 4;
            dr[1] = "纬度";
            dt.Rows.Add(dr);
            cmbProbeType.DisplayMember = "Name";
            cmbProbeType.ValueMember = "Value";
            cmbProbeType.DataSource = dt;
        }
        public void load()
        {
            txtProbeName.Text = mai.PpointNamek__BackingField;
            if (mai.PointTypek__BackingField == Enum_PointType.Temp)
                cmbProbeType.Text = "温度";
            else if (mai.PointTypek__BackingField == Enum_PointType.Hump)
                cmbProbeType.Text = "湿度";
            else if (mai.PointTypek__BackingField == Enum_PointType.Longitude)
                cmbProbeType.Text = "经度";
            else if (mai.PointTypek__BackingField == Enum_PointType.Latitude)
                cmbProbeType.Text = "纬度";
            if (mai.Activedk__BackingField == Enum_Active.Enabled)
                checkActivation.Checked = true;
            else
                checkActivation.Checked = false;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtProbeName.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("探头名称不能为空");
                    return;
                }
                if (Encoding.Default.GetBytes(txtProbeName.Text.Trim()).Length > 50)
                {
                    MessageBox.Show("探头名称不能超过50");
                    return;
                }
                if (cmbProbeType.Text.Trim()==string.Empty)
                {
                    MessageBox.Show("探头类型不能为空");
                    return;
                }
                Model_AiInfo ma = new Model_AiInfo();
                ma.PointIdk__BackingField = mai.PointIdk__BackingField;
                ma.PpointNamek__BackingField = txtProbeName.Text.Trim();
                if (cmbProbeType.Text == "温度")
                    ma.PointTypek__BackingField = Enum_PointType.Temp;
                else if (cmbProbeType.Text == "湿度")
                    ma.PointTypek__BackingField = Enum_PointType.Hump;
                else if (cmbProbeType.Text == "经度")
                    ma.PointTypek__BackingField = Enum_PointType.Longitude;
                else if (cmbProbeType.Text == "纬度")
                    ma.PointTypek__BackingField = Enum_PointType.Latitude;
                if (checkActivation.Checked == true)
                    ma.Activedk__BackingField = Enum_Active.Enabled;
                else
                    ma.Activedk__BackingField = Enum_Active.Disable;
                ma.StorageIdk__BackingField = mai.StorageIdk__BackingField;
                ResultModelOfModel_AiInfod4FqxSXX edit = cs.EditAiInfo(ma, true, true);
                if (edit.Code != 0)
                {
                    MessageBox.Show(edit.Message);
                }
                else
                {
                    MessageBox.Show("操作成功");
                    _ParentVMP.getRenew();
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
