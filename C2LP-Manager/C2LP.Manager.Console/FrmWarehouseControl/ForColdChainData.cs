using C2LP.Manager.Console.ConsoleServerWebReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace C2LP.Manager.Console.FrmWarehouseControl
{
    public partial class ForColdChainData : Form
    {
        public ForColdChainData()
        {
            InitializeComponent();
        }
        ConsoleServerWebReference.ConsoleServer cs = new ConsoleServerWebReference.ConsoleServer();
        public Model_ColdstoragePDA coldstoragepda;
        private static Prompt waitForm;

        private void ForColdChainData_Load(object sender, EventArgs e)
        {
            dtpStartTime.CustomFormat = " ";
            dtpEndTime.CustomFormat = " ";
            pictureBox1.Hide();
            winFormPager1.OnPageChanged += new EventHandler(winFormPager1_OnPageChanged);
            //GetColdChainData();
        }
        private void winFormPager1_OnPageChanged(object sender, EventArgs e)
        {
            GetColdChainData();
        }

        public void GetColdChainData()
        {
            string pageIndexAndCount = winFormPager1.PageIndex + "." + winFormPager1.PageSize;  //分页参数
            string startTime = null; //起始时间
            string endTime = null; //结束时间
            if(dtpStartTime.Text.ToString() !=" ")
                startTime = dtpStartTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
            if(dtpEndTime.Text.ToString() !=" ")
                endTime = dtpEndTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
            
            GetColdChainDatas(startTime, endTime, pageIndexAndCount);

        }
        public void GetColdChainDatas(string startTime,string endTime,string pageIndexAndCount)
        {
            try
            {
                
                if (coldstoragepda == null) return;
                if (startTime != null && endTime != null)
                {
                    if (Convert.ToDateTime(startTime) > Convert.ToDateTime(endTime))
                    {
                        MessageBox.Show("开始时间不能大于结束时间！");
                        return;
                    }
                    pictureBox1.Show();
                    //获取显示的标题行
                    ResultModelOfArrayOfModel_AiInfod4FqxSXX biaoti = cs.GetAiInfoByStorageId(coldstoragepda.StorageIdk__BackingField, true, null);
                    if (biaoti.Code != 0)
                    {
                        MessageBox.Show(biaoti.Message);
                    }
                    else
                    {
                        dataGridView1.Columns.Clear();
                        dataGridView1.Columns.Add("记录时间", "记录时间");
                        foreach (Model_AiInfo item in biaoti.Data)
                        {
                            dataGridView1.Columns.Add(item.PpointNamek__BackingField, item.PpointNamek__BackingField);
                        }
                        dataGridView1.Columns.Add("报警状态", "报警状态");

                    }
                    //线程 调用提示文字框
                    //waitForm = null;
                    //Thread waitThread = new Thread(new ThreadStart(this._ShowWaitForm));
                    //waitThread.Start();
                    //Thread.Sleep(100);

                    //根据冷库id、开始时间、结束时间（下一个开始时间）查询冷链数据
                    //ResultModelOfArrayOfArrayOfstringuHEDJ7Dj count = cs.GetWaybillNodeHistDataList(coldstoragepda.StorageIdk__BackingField, true, startTime, endTime, null);
                    //winFormPager1.DrawControl(count.Data.Count<String[]>());
                    ResultModelOfint result = cs.GetWaybillNodeHistDataCount(coldstoragepda.StorageIdk__BackingField, true, startTime, endTime);
                    winFormPager1.DrawControl(result.Data);
                    ResultModelOfArrayOfArrayOfstringuHEDJ7Dj nodedatalist = cs.GetWaybillNodeHistDataList(coldstoragepda.StorageIdk__BackingField, true, startTime, endTime, pageIndexAndCount);
                    
                    if (nodedatalist.Code != 0)
                    {
                        MessageBox.Show(nodedatalist.Message);
                    }
                    else
                    {
                        //Thread.Sleep(100);
                        //if (waitThread != null)
                        //{
                        //    waitForm.Close(); //waitThread.Abort();  
                        //}
                        pictureBox1.Hide();
                        dataGridView1.Rows.Clear();
                        foreach (String[] item in nodedatalist.Data)
                        {
                            int rowIndex = dataGridView1.Rows.Add();
                            for (int i = 0; i < item.Length; i++)
                            {
                                //判断是否为最后一列
                                if (item[i] != item[item.Length - 1])
                                {
                                    //设置第一列的时间宽度
                                    if (item[i] == item[0])
                                    {
                                        dataGridView1.Columns[i].Width = 130;
                                        //dataGridView1.Columns[i].Resizable = DataGridViewTriState.False;
                                        dataGridView1.Rows[rowIndex].Cells[i].Value = DateTime.Parse(item[i]).ToString("yyyy-MM-dd HH:mm:ss");
                                    }
                                    else
                                    {
                                        //显示一位小数,不为空和非负浮点数
                                        if (item[i] != string.Empty && IsInteger(item[i])==true)
                                            dataGridView1.Rows[rowIndex].Cells[i].Value = Math.Round(Convert.ToDecimal(item[i]), 1);
                                        else
                                            dataGridView1.Rows[rowIndex].Cells[i].Value = "- -";
                                    }
                                }
                                else
                                {
                                    if (item[item.Length - 1] == "0")
                                        dataGridView1.Rows[rowIndex].Cells[i].Value = "正常";
                                    else
                                        dataGridView1.Rows[rowIndex].Cells[i].Value = "报警";
                                }
                            }
                        }
                    }

                }
                else
                {
                        MessageBox.Show("请选择开始时间和结束时间！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        //判断浮点数是否为非负浮点数，如1.11
        public static bool IsInteger(string vehicleNumber)
        {
            string express = @"^\d+(\.\d+)?$";
            return Regex.IsMatch(vehicleNumber, express);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //手动改变时间，回车后获取不到改变时间。要给按钮设置焦点。
            button1.Focus();
            winFormPager1.PageIndex = 1;
            GetColdChainData();
        }

        //使用acceptbutton属性实现回车事件，会影响分页回车事件，
        //在首次按下某个键时发生回车事件
        private void txtEnter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(null, null);
            }
        }
        
        private void _ShowWaitForm()
        {
            try
            {
                waitForm = new Prompt();
                waitForm.ShowDialog();   
            }
            catch (ThreadAbortException)
            {
                waitForm.Close();
                Thread.ResetAbort();
            }
        }

        private void ForColdChainData_Shown(object sender, EventArgs e)
        {
            //winFormPager1.OnPageChanged += new EventHandler(winFormPager1_OnPageChanged);
            //GetColdChainData();

        }
        //时间控件开始显示时间为空，
        private void dtpStartTime_ValueChanged(object sender, EventArgs e)
        {
            //设置显示日期和时间的格式
            dtpStartTime.Format = DateTimePickerFormat.Custom;
            //设置自定义时间为空
            dtpStartTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
        }
        //时间控件开始显示时间为空，
        private void dtpEndTime_ValueChanged(object sender, EventArgs e)
        {
            dtpEndTime.Format = DateTimePickerFormat.Custom;
            dtpEndTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
        }
    }
}
