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
    public partial class FrmPDADestination : Form
    {
        public FrmPDADestination()
        {
            InitializeComponent();
        }
        ConsoleServerWebReference.ConsoleServer cs = new ConsoleServerWebReference.ConsoleServer();
        public Model_PDAInfo mp;
        private void FrmPDADestination_Load(object sender, EventArgs e)
        {
            PDADest();
            AutoSizeColumn(dataGridView1);
        }
        public void PDADest()
        {
            try
            {
                ResultModelOfArrayOfModel_Destinationd4FqxSXX Destination = cs.GetPDADestinationList(mp.Idk__BackingField, true, 0, true, null);
                if (Destination.Code != 0)
                {
                    MessageBox.Show(Destination.Message);
                }
                else
                {
                    dataGridView1.AutoGenerateColumns = false;
                    dataGridView1.DataSource = Destination.Data;
                    if (Destination.Data.Count<Model_Destination>() == 0)
                        btnDelete.Enabled = false;
                    else
                        btnDelete.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDestination.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("目的地不能为空");
                    return;
                }
                if (Encoding.Default.GetBytes(txtDestination.Text.Trim()).Length > 20)
                {
                    MessageBox.Show("目的地不能超过20");
                    return;
                }
                Model_Destination destination = new Model_Destination();
                destination.Addressk__BackingField = txtDestination.Text.Trim();
                destination.DeviceIdk__BackingField = mp.Idk__BackingField;
                ResultModelOfModel_Destinationd4FqxSXX dest = cs.EditPDADestination(destination);
                if (dest.Code != 0)
                {
                    MessageBox.Show(dest.Message);
                }
                else
                {
                    MessageBox.Show("操作成功");
                    txtDestination.Text = string.Empty;
                    PDADest();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int pdaId = Convert.ToInt32(dataGridView1.SelectedCells[0].Value);

                if (MessageBox.Show("确定是否删除？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Model_Destination destination = new Model_Destination();
                    destination.Idk__BackingField = pdaId;
                    ResultModelOfModel_Destinationd4FqxSXX dest = cs.EditPDADestination(destination);
                    if (dest.Code!=0)
                    {
                        MessageBox.Show(dest.Message);
                    }
                    else
                    {
                        MessageBox.Show("操作成功！");
                        PDADest();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK,MessageBoxIcon.Error);
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
