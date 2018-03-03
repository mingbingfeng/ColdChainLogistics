using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using C2LP.Manager.Console.ConsoleServerWebReference;

namespace C2LP.Manager.Console.FrmWaybillControl
{
    public partial class FrmThirdParty : Form
    {
        ConsoleServerWebReference.ConsoleServer cs = new ConsoleServerWebReference.ConsoleServer();
        public FrmThirdParty()
        {
            InitializeComponent();
        }

        private void FrmThirdParty_Load(object sender, EventArgs e)
        {
            winFormPager1.OnPageChanged += new EventHandler(winFormPager1_OnPageChanged);
            huaDong();
            AutoSizeColumn(dataGridView1);
        }
        private void winFormPager1_OnPageChanged(object sender, EventArgs e)
        {
            huaDong();
        }
        public void huaDong()
        {
            try
            {
                string SHIPDETAILID = null;
                if (txtNumber!=null)
                {
                    SHIPDETAILID = txtNumber.Text.Trim();
                }
                string pageIndexAndCount = winFormPager1.PageIndex + "." + winFormPager1.PageSize;
                //ResultModelOfArrayOfModel_Huadong_Tms_Orderd4FqxSXX count = cs.GetHuadongQuery(null);
                //ResultModelOfint result=cs.GethuadongTmsOrderCount();
                //cs.Timeout = 10000;
                //ResultModelOfArrayOfModel_Huadong_Tms_Orderd4FqxSXX huadong = cs.GetHuadongQuery(pageIndexAndCount);
                //ResultModelOfint result = cs.GethuadongWaybillNumberCount(SHIPDETAILID);
                ResultModelOfint result = cs.GethuadongWaybillVagueCount(SHIPDETAILID);
                winFormPager1.DrawControl(result.Data);
                //ResultModelOfArrayOfModel_Huadong_Tms_Orderd4FqxSXX huadong = cs.GetHuadongWaybillNumberQuery(SHIPDETAILID,pageIndexAndCount);
                ResultModelOfArrayOfModel_Huadong_Tms_Orderd4FqxSXX huadong = cs.GetHuadongWaybillVagueQuery(SHIPDETAILID, pageIndexAndCount);
                if (huadong.Code!=0)
                {
                    MessageBox.Show(huadong.Message);
                }
                else
                {
                    dataGridView1.AutoGenerateColumns = false;
                    dataGridView1.Rows.Clear();//清空数据
                    foreach (Model_Huadong_Tms_Order item in huadong.Data)
                    {
                        int rowIndex = dataGridView1.Rows.Add();
                        dataGridView1.Rows[rowIndex].Cells[0].Value = item.RelationIdk__BackingField;
                        dataGridView1.Rows[rowIndex].Cells[1].Value = item.Codek__BackingField;
                        dataGridView1.Rows[rowIndex].Cells[2].Value = item.SRCEXPNOk__BackingField;
                        dataGridView1.Rows[rowIndex].Cells[3].Value = item.ROADIDk__BackingField;
                        dataGridView1.Rows[rowIndex].Cells[4].Value = item.SHIPDETAILIDk__BackingField;
                        dataGridView1.Rows[rowIndex].Cells[5].Value = item.TOTALIDk__BackingField;
                        dataGridView1.Rows[rowIndex].Cells[6].Value = item.LEGCODEk__BackingField;
                        dataGridView1.Rows[rowIndex].Cells[7].Value = item.SHIPMENTCODEk__BackingField;
                        dataGridView1.Rows[rowIndex].Cells[8].Value = item.OPERATIONTYPEk__BackingField;
                        if (Convert.ToDateTime(item.DEMANDARRIVETIMEk__BackingField.ToString("yyyy-MM-dd")) > DateTime.MinValue && Convert.ToDateTime(item.DEMANDARRIVETIMEk__BackingField.ToString("yyyy-MM-dd")).Year!=1)
                            dataGridView1.Rows[rowIndex].Cells[9].Value = item.DEMANDARRIVETIMEk__BackingField;
                        else
                            dataGridView1.Rows[rowIndex].Cells[9].Value = null;
                        dataGridView1.Rows[rowIndex].Cells[10].Value = item.TOGTRANSNAMEk__BackingField;
                        dataGridView1.Rows[rowIndex].Cells[11].Value = item.RECEIVEMANk__BackingField;
                        dataGridView1.Rows[rowIndex].Cells[12].Value = item.RECEIVEPHONEk__BackingField;
                        dataGridView1.Rows[rowIndex].Cells[13].Value = item.RECEIVEADDRk__BackingField;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show(ex.Message,"error",MessageBoxButtons.OK,MessageBoxIcon.Error);
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

        private void btnQuery_Click(object sender, EventArgs e)
        {
            winFormPager1.PageIndex = 1;
            huaDong();
        }

        private void txtNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnQuery_Click(null, null);
            }
        }
    }
}
