using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using C2LP.Manager.Console.ConsoleServerWebReference;

namespace C2LP.Manager.Console.FrmAdministrativeDivisionControl
{
    public partial class FrmSeeAdministrative : Form
    {
        ConsoleServerWebReference.ConsoleServer cs = new ConsoleServerWebReference.ConsoleServer();
        public FrmSeeAdministrative()
        {
            InitializeComponent();
        }
        //
        public Model_Region region;

        private void FrmSeeAdministrative_Load(object sender, EventArgs e)
        {
            if (region != null)
                this.Text = region.Namek__BackingField+"下级行政区";
            RegionShow();
            AutoSizeColumn(dataGridView1);
        }
        #region 显示行政
        public void RegionShow()
        {
            try
            {
                if (region!=null)
                {
                    ResultModelOfArrayOfModel_Regiond4FqxSXX reg = cs.GetRegionDateTime(region.Idk__BackingField,true);
                    if (reg.Code!=0)
                    {
                        MessageBox.Show(reg.Message);
                    }
                    else
                    {
                        dataGridView1.Rows.Clear();
                        foreach (Model_Region item in reg.Data)
                        {
                            int rowIndex = dataGridView1.Rows.Add();
                            dataGridView1.Rows[rowIndex].Cells[0].Value = item.Idk__BackingField;
                            dataGridView1.Rows[rowIndex].Cells[1].Value = item.Namek__BackingField;
                            dataGridView1.Rows[rowIndex].Tag = item;
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        #endregion
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

        private void ecitmenu_Click(object sender, EventArgs e)
        {
            Model_Region modelregion = dataGridView1.SelectedRows[0].Tag as Model_Region;
            //MessageBox.Show(modelregion .Idk__BackingField+ ","+modelregion.Namek__BackingField);
            FrmAdministrativeAE adminae = new FrmAdministrativeAE();
            adminae._frmseeadministratve = this;
            adminae.region = modelregion;
            adminae.nanber = 2;//0修改，1添加，2当前窗体
            adminae.ShowDialog();
        }
        public void Refurbish()
        {
            RegionShow();
        }
    }
}
