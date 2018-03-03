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
    public partial class FrmPDAList : Form
    {
        ConsoleServerWebReference.ConsoleServer cs = new ConsoleServerWebReference.ConsoleServer();
        public FrmPDAList()
        {
            InitializeComponent();
        }

        private void FrmPDAList_Load(object sender, EventArgs e)
        {
            winFormPager1.OnPageChanged += new EventHandler(winFormPager1_OnPageChanged);
            dataGridView1.KeyDown += new KeyEventHandler(dataGridView1_KeyDown);
            DeviceLoad();
            AutoSizeColumn(dataGridView1);
        }
        public void winFormPager1_OnPageChanged(object sender, EventArgs e)
        {
            DeviceLoad();
        }
        public void DeviceLoad()
        {
            int pdaNumber = 0;
            string pageIndexAndCount = null;
            try
            {
                pageIndexAndCount = winFormPager1.PageIndex + "." + winFormPager1.PageSize;
                ResultModelOfArrayOfModel_PDAInfod4FqxSXX pda = cs.GetPDAList(pdaNumber, true, pageIndexAndCount);
                ResultModelOfArrayOfModel_PDAInfod4FqxSXX count = cs.GetPDAList(pdaNumber, true, null);
                winFormPager1.DrawControl(count.Data.Count<Model_PDAInfo>());
                if (count.Data.Count<Model_PDAInfo>() <= 0)
                    contextMenuStrip1.Enabled = false;
                else
                    contextMenuStrip1.Enabled = true;
                if (pda.Code != 0)
                {
                    MessageBox.Show(pda.Message);
                }
                else
                {
                    dataGridView1.AutoGenerateColumns = false;
                    dataGridView1.Rows.Clear();
                    foreach (Model_PDAInfo item in pda.Data)
                    {
                        int rowIndex = dataGridView1.Rows.Add();
                        dataGridView1.Rows[rowIndex].Cells[0].Value = item.Idk__BackingField;
                        dataGridView1.Rows[rowIndex].Cells[1].Value = item.Numberk__BackingField;
                        dataGridView1.Rows[rowIndex].Cells[2].Value = item.Namek__BackingField;
                        dataGridView1.Rows[rowIndex].Cells[3].Value = item.CreateAtk__BackingField.ToString("yyyy-MM-dd HH:mm:ss");
                        dataGridView1.Rows[rowIndex].Tag = item;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                pda.Numberk__BackingField = txtDeviceNumber.Text.Trim();
                pda.Namek__BackingField = txtDeviceName.Text.Trim();
                pda.CreateAtk__BackingField = DateTime.Now;
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
                    getClear();
                    DeviceLoad();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        public void getClear()
        {
            txtDeviceNumber.Text = string.Empty;
            txtDeviceName.Text = string.Empty;
            rdbEnabled.Checked = true;
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmUpdate_Click(object sender, EventArgs e)
        {
            Model_PDAInfo mp = dataGridView1.SelectedRows[0].Tag as Model_PDAInfo;
            FrmPDAUpdate pdaupte = new FrmPDAUpdate();
            pdaupte.udtmp = mp;
            pdaupte._ParentPad = this;
            pdaupte.ShowDialog();
        }
        public void PdaLoad()
        {
            DeviceLoad();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Model_PDAInfo mp = dataGridView1.SelectedRows[0].Tag as Model_PDAInfo;
                mp.Activedk__BackingField = Enum_Active.Disable;
                if (MessageBox.Show("确定是否删除？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ResultModelOfModel_PDAInfod4FqxSXX pdalist = cs.EditPDA(mp);
                    if (pdalist.Code != 0)
                    {
                        MessageBox.Show(pdalist.Message);
                    }
                    else
                    {
                        MessageBox.Show("操作成功，PDA已停用");
                        DeviceLoad();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 绑定目的地
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmBindingDestination_Click(object sender, EventArgs e)
        {
            Model_PDAInfo mppdain = dataGridView1.SelectedRows[0].Tag as Model_PDAInfo;
            FrmPDADestination pdadt = new FrmPDADestination();
            pdadt.mp = mppdain;
            pdadt.ShowDialog();
        }

        private void dataGridView1_KeyDown(object sender,KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
                e.Handled = true;
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
