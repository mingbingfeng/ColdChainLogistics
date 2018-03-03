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
    public partial class FrmProbeList : Form
    {
        ConsoleServerWebReference.ConsoleServer cs = new ConsoleServerWebReference.ConsoleServer();
        public Model_ColdstoragePDA mcp;
        public FrmProbeList()
        {
            InitializeComponent();
        }

        private void FrmProbeList_Load(object sender, EventArgs e)
        {
            lbColdStorage.Text = "【" + mcp.StorageNamek__BackingField + "】探头列表";
            Probe();
            winFormPager1.OnPageChanged += new EventHandler(winFormPager1_OnPageChanged);
            AillLoad();
            AutoSizeColumn(dataGridView1);
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
        public void winFormPager1_OnPageChanged(object sender,EventArgs e)
        {
            AillLoad();
        }
        public void AillLoad()
        {
            try
            {
                string pageIndexAndCount = winFormPager1.PageIndex + "." + winFormPager1.PageSize;
                ResultModelOfArrayOfModel_AiInfod4FqxSXX aiin = cs.GetAiInfoByStorageId(mcp.StorageIdk__BackingField, true, pageIndexAndCount);
                ResultModelOfArrayOfModel_AiInfod4FqxSXX count = cs.GetAiInfoByStorageId(mcp.StorageIdk__BackingField, true, null);
                winFormPager1.DrawControl(count.Data.Count<Model_AiInfo>());
                if (count.Data.Count<Model_AiInfo>() <= 0)
                    contextMenuStrip1.Enabled = false;
                else
                    contextMenuStrip1.Enabled = true;
                if (aiin.Code != 0)
                {
                    MessageBox.Show(aiin.Message);
                }
                else
                {
                    dataGridView1.Rows.Clear();
                    dataGridView1.AutoGenerateColumns = false;
                    foreach (Model_AiInfo item in aiin.Data)
                    {
                        int rowIndex = dataGridView1.Rows.Add();
                        dataGridView1.Rows[rowIndex].Cells[0].Value = item.PointIdk__BackingField;
                        dataGridView1.Rows[rowIndex].Cells[1].Value = item.PpointNamek__BackingField;
                        if (item.PointTypek__BackingField == Enum_PointType.Temp)
                            dataGridView1.Rows[rowIndex].Cells[2].Value = "温度";
                        else if (item.PointTypek__BackingField == Enum_PointType.Hump)
                            dataGridView1.Rows[rowIndex].Cells[2].Value = "湿度";
                        else if (item.PointTypek__BackingField == Enum_PointType.Longitude)
                            dataGridView1.Rows[rowIndex].Cells[2].Value = "经度";
                        else if (item.PointTypek__BackingField == Enum_PointType.Latitude)
                            dataGridView1.Rows[rowIndex].Cells[2].Value = "纬度";
                        if (item.Activedk__BackingField == Enum_Active.Enabled)
                            dataGridView1.Rows[rowIndex].Cells[3].Value = "激活";
                        else
                            dataGridView1.Rows[rowIndex].Cells[3].Value = "待激活";
                        dataGridView1.Rows[rowIndex].Tag = item;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtProbeName.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("探头名称不能为空");
                    return;
                }
                if (Encoding.Default.GetBytes(txtProbeName.Text.Trim()).Length>50)
                {
                    MessageBox.Show("探头名称不能超过50");
                    return;
                }
                if (cmbProbeType.Text.Trim()==string.Empty)
                {
                    MessageBox.Show("探头类型不能为空");
                    return;
                }
                Model_AiInfo aiin = new Model_AiInfo();
                aiin.PpointNamek__BackingField = txtProbeName.Text.Trim();
                if (cmbProbeType.Text == "温度")
                    aiin.PointTypek__BackingField = Enum_PointType.Temp;
                else if (cmbProbeType.Text == "湿度")
                    aiin.PointTypek__BackingField = Enum_PointType.Hump;
                else if (cmbProbeType.Text == "经度")
                    aiin.PointTypek__BackingField = Enum_PointType.Longitude;
                else if (cmbProbeType.Text == "纬度")
                    aiin.PointTypek__BackingField = Enum_PointType.Latitude;
                if (checkActivation.Checked == true)
                    aiin.Activedk__BackingField = Enum_Active.Enabled;
                else
                    aiin.Activedk__BackingField = Enum_Active.Disable;
                aiin.StorageIdk__BackingField = mcp.StorageIdk__BackingField;

                ResultModelOfModel_AiInfod4FqxSXX editaiin = cs.EditAiInfo(aiin, false, true);
                if (editaiin.Code != 0)
                {
                    MessageBox.Show(editaiin.Message);
                }
                else
                {
                    MessageBox.Show("操作成功");
                    getClear();
                    AillLoad();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        public void getClear()
        {
            txtProbeName.Text = string.Empty;
            checkActivation.Checked = true;
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmEdit_Click(object sender, EventArgs e)
        {
            Model_AiInfo mai = dataGridView1.SelectedRows[0].Tag as Model_AiInfo;
            FrmProbeEdit eidt = new FrmProbeEdit();
            eidt.mai = mai;
            eidt._ParentFrm = this;
            eidt.ShowDialog();
        }
        public void FPB()
        {
            AillLoad();
        }
        /// <summary>
        /// 修改状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Model_AiInfo mai = dataGridView1.SelectedRows[0].Tag as Model_AiInfo;
                mai.Activedk__BackingField = Enum_Active.Disable;
                if (MessageBox.Show("是否确定删除", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ResultModelOfModel_AiInfod4FqxSXX editma = cs.EditAiInfo(mai, true, true);
                    if (editma.Code != 0)
                    {
                        MessageBox.Show(editma.Message);
                    }
                    else
                    {
                        MessageBox.Show("操作成功，冷库探头已停用");
                        AillLoad();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        //使用acceptbutton属性实现回车事件，会影响分页回车事件，
        //在首次按下某个键时发生回车事件
        private void txtEnter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSave_Click(null, null);
            }
        }
        /// <summary>
        /// 使DataGridView的列自适应宽度
        /// </summary>
        /// <param name="dgViewFiles"></param>
        private void AutoSizeColumn(DataGridView dgViewFiles)
        {
            int width = 0;
            //使列自使用宽度
            //对于DataGridView的每一个列都调整
            for (int i = 0; i < dgViewFiles.Columns.Count; i++)
            {
                //将每一列都调整为自动适应模式
                dgViewFiles.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);
                //记录整个DataGridView的宽度
                width += dgViewFiles.Columns[i].Width;
            }
            //判断调整后的宽度与原来设定的宽度的关系，如果是调整后的宽度大于原来设定的宽度，
            //则将DataGridView的列自动调整模式设置为显示的列即可，
            //如果是小于原来设定的宽度，将模式改为填充。
            if (width > dgViewFiles.Size.Width)
            {
                dgViewFiles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            }
            else
            {
                dgViewFiles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            //冻结某列 从左开始 0，1，2
            dgViewFiles.Columns[1].Frozen = true;
        }

    }
}
