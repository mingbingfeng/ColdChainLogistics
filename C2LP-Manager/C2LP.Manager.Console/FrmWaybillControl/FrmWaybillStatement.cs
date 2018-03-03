using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using C2LP.Manager.Console.ConsoleServerWebReference;
using System.Reflection;
using Excel;

namespace C2LP.Manager.Console.FrmWaybillControl
{
    public partial class FrmWaybillStatement : Form
    {
        public FrmWaybillStatement()
        {
            InitializeComponent();
        }
        ConsoleServerWebReference.ConsoleServer cs = new ConsoleServerWebReference.ConsoleServer();
        private string waybillNumber = null; //运单号
        private string startTime = null; //起始时间
        private string endTime = null; //结束时间
        private int customerId = 0; //客户ID
        private void FrmWaybillStatement_Load(object sender, EventArgs e)
        {
            dtpEndTime.CustomFormat = " ";
            dtpStartTime.CustomFormat = " ";
            winFormPager1.OnPageChanged += new EventHandler(winFormPager1_OnPageChanged);
            dataGridView1.KeyDown += new KeyEventHandler(dataGridView1_KeyDown);
            waybillLoad();
            customerNameLoad();
            AutoSizeColumn(dataGridView1);
        }
        public void winFormPager1_OnPageChanged(object sender, EventArgs e)
        {
            waybillLoad();
        }
        public void waybillLoad()
        {
            string pageIndexAndCount = winFormPager1.PageIndex + "." + winFormPager1.PageSize; //分页参数
            if (dtpStartTime.Text.ToString() != " ")
                startTime = dtpStartTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
            if (dtpEndTime.Text.ToString() != " ")
                endTime = dtpEndTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
            if (cmbCustomerName.SelectedValue != null)
                customerId = (int)cmbCustomerName.SelectedValue;
            waybillbaseLoad(waybillNumber, pageIndexAndCount, startTime, endTime, customerId);
        }
        public void waybillbaseLoad(string waybillNumber, string pageIndexAndCount, string startTime, string endTime, int customerId)
        {
            try
            {
                if (startTime != null && endTime != null)
                {
                    if (Convert.ToDateTime(startTime) > Convert.ToDateTime(endTime))
                    {
                        MessageBox.Show("起始时间不能大于结束时间");
                        return;
                    }
                }
                //ResultModelOfArrayOfModel_Waybill_Based4FqxSXX count = cs.GetWaybillList(waybillNumber, null, startTime, endTime, customerId, true, 3, true);
                //winFormPager1.DrawControl(count.Data.Count<Model_Waybill_Base>());
                ResultModelOfint result = cs.GetWaybillListCount(waybillNumber, startTime, endTime, customerId, true, 3, true);
                winFormPager1.DrawControl(result.Data);
                ResultModelOfArrayOfModel_Waybill_Based4FqxSXX waybill = cs.GetWaybillList(waybillNumber, pageIndexAndCount, startTime, endTime, customerId, true, 3, true);
                if (waybill.Code != 0)
                {
                    MessageBox.Show(waybill.Message);
                }
                else
                {
                    dataGridView1.AutoGenerateColumns = false;
                    dataGridView1.Rows.Clear();
                    foreach (Model_Waybill_Base item in waybill.Data)
                    {
                        int rowIndex = dataGridView1.Rows.Add();
                        dataGridView1.Rows[rowIndex].Cells[0].Value = item.Numberk__BackingField;
                        dataGridView1.Rows[rowIndex].Cells[1].Value = item.ReceiverOrgk__BackingField;
                        dataGridView1.Rows[rowIndex].Cells[2].Value = item.ReceiverPersonk__BackingField;
                        dataGridView1.Rows[rowIndex].Cells[3].Value = item.ReceiverTelk__BackingField;
                        dataGridView1.Rows[rowIndex].Cells[4].Value = item.ReceiverAddressk__BackingField;
                        dataGridView1.Rows[rowIndex].Cells[5].Value = item.BeginAtk__BackingField.ToString("yyyy-MM-dd HH:mm:ss");
                        dataGridView1.Rows[rowIndex].Cells[6].Value = item.BillingCountk__BackingField;
                        dataGridView1.Rows[rowIndex].Tag = item;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void customerNameLoad()
        {
            try
            {
                ResultModelOfArrayOfModel_Customerd4FqxSXX customername = cs.GetCustomerList(Enum_Role.Receiver, true, 0, true, 0, true, 0, true, null);
                if (customername.Code != 0)
                {
                    MessageBox.Show(customername.Message);
                }
                else
                {
                    List<Model_Customer> list = new List<Model_Customer>();
                    Model_Customer msc = new Model_Customer();
                    msc.Idk__BackingField = 0;
                    msc.FullNamek__BackingField = "全部";
                    list.Add(msc);
                    foreach (Model_Customer item in customername.Data)
                    {
                        Model_Customer mc = new Model_Customer();
                        mc.Idk__BackingField = item.Idk__BackingField;
                        mc.FullNamek__BackingField = item.FullNamek__BackingField;
                        list.Add(mc);
                    }
                    cmbCustomerName.DisplayMember = "fullNamek__BackingField";
                    cmbCustomerName.ValueMember = "idk__BackingField";
                    cmbCustomerName.DataSource = list;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btSave_Click(object sender, EventArgs e)
        {
            //手动改变时间，回车后获取不到改变时间。要给按钮设置焦点。
            btSave.Focus();
            winFormPager1.PageIndex = 1;
            waybillLoad();
        }
        
        /// <summary>
        /// 打印报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReport_Click(object sender, EventArgs e)
        {
            reportForm();
        }

        public void reportForm()
        {
            int rowCount = dataGridView1.Rows.Count;//获取dataGridView1的行数
            int columnsCount = dataGridView1.Columns.Count;//获取dataGridView1的列数
            //判断是否存在值
            if (rowCount == 0)
            {
                MessageBox.Show("没有数据可供导出。。。", "Prompt", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            //定义个变量接收文件名字
            string fileName = string.Empty;
            //new 一个对象用于提示用户选择保存文件位置
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "导出Excel (*.xls)|*.xls";
            saveFileDialog.FilterIndex = 0;//筛选器的索引
            saveFileDialog.RestoreDirectory = true;//对话框关闭前是否还原当前目录
            saveFileDialog.CreatePrompt = true;//指定文件不存在，提示用户是否允许创建该文件
            saveFileDialog.Title = "导出文件保存路径";//设置对话框标题
            //当前时间
            DateTime now = DateTime.Now;
            saveFileDialog.FileName = now.Year.ToString().PadLeft(2)
            + now.Month.ToString().PadLeft(2, '0')
            + now.Day.ToString().PadLeft(2, '0') + "-"
            + now.Hour.ToString().PadLeft(2, '0')
            + now.Minute.ToString().PadLeft(2, '0')
            + now.Second.ToString().PadLeft(2, '0');

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                fileName = saveFileDialog.FileName;
                Missing miss = Missing.Value;
                Excel.Application excel = new Excel.Application();
                excel.Application.Workbooks.Add(true);
                excel.Visible = false;//若是true，则在导出的时候会显示EXcel界面。
                if (excel == null)
                {
                    MessageBox.Show("Excel无法启动", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                Workbooks workbooks = excel.Workbooks;
                Workbook workbook = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
                Worksheet worksheet = (Worksheet)workbook.Worksheets[1];//取得sheet1  
                                                                        //写入标题  
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    worksheet.Cells[1, i + 1] = dataGridView1.Columns[i].HeaderText;
                }
                #region 导出一页
                ////写入数值  
                //for (int r = 0; r < dataGridView1.Rows.Count; r++)
                //{
                //    for (int i = 0; i < dataGridView1.ColumnCount; i++)
                //    {
                //        worksheet.Cells[r + 2, i + 1] = dataGridView1.Rows[r].Cells[i].Value;
                //    }
                //    System.Windows.Forms.Application.DoEvents();
                //} 
                #endregion
                ResultModelOfArrayOfModel_Waybill_Based4FqxSXX waybill = cs.GetWaybillList(waybillNumber, null, startTime, endTime, customerId, true, 3, true);
                if (waybill.Code != 0)
                {
                    MessageBox.Show(waybill.Message);
                }
                else
                {
                    //写入数值  
                    for (int r = 0; r < waybill.Data.Length; r++)
                    {
                        //在字段前加单引号是为了防止自动转化格式，如057410007009导出后转化格式前面的0不会消失
                        worksheet.Cells[r + 2, 1] ="'"+ waybill.Data[r].Numberk__BackingField;
                        worksheet.Cells[r + 2, 2] = waybill.Data[r].ReceiverOrgk__BackingField;
                        worksheet.Cells[r + 2, 3] = waybill.Data[r].ReceiverPersonk__BackingField;
                        worksheet.Cells[r + 2, 4] = waybill.Data[r].ReceiverTelk__BackingField;
                        worksheet.Cells[r + 2, 5] = waybill.Data[r].ReceiverAddressk__BackingField;
                        worksheet.Cells[r + 2, 6] = waybill.Data[r].BeginAtk__BackingField;
                        worksheet.Cells[r + 2, 7] = waybill.Data[r].BillingCountk__BackingField;
                        System.Windows.Forms.Application.DoEvents();
                    }
                }
                worksheet.Columns.EntireColumn.AutoFit();//列宽自适应  
                if (fileName != "")
                {
                    try
                    {
                        workbook.Saved = true;
                        workbook.SaveCopyAs(fileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("导出文件时出错,文件可能正被打开！\n" + ex.Message);
                    }
                }
                excel.Quit();
                GC.Collect();//强行销毁
                MessageBox.Show(fileName + "的简明资料保存成功", "提示", MessageBoxButtons.OK);
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
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
                btSave_Click(null,null);
            }
        }
        //时间控件开始显示时间为空，
        private void dtpStartTime_ValueChanged(object sender, EventArgs e)
        {
            this.dtpStartTime.Format = DateTimePickerFormat.Custom;
            this.dtpStartTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
        }
        //时间控件开始显示时间为空，
        private void dtpEndTime_ValueChanged(object sender, EventArgs e)
        {
            this.dtpEndTime.Format = DateTimePickerFormat.Custom;
            this.dtpEndTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
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
