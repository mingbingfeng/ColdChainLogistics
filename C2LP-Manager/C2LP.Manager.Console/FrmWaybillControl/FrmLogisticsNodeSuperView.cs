using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using C2LP.Manager.Console.ConsoleServerWebReference;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Text.RegularExpressions;
using System.Threading;

namespace C2LP.Manager.Console.FrmWaybillControl
{
    public partial class FrmLogisticsNodeSuperView : Form
    {
        ConsoleServerWebReference.ConsoleServer cs = new ConsoleServerWebReference.ConsoleServer();
        public Model_Waybill_Base waybase;
        public Model_Waybill_Node node;
        private static Prompt waitForm;
        public FrmLogisticsNodeSuperView()
        {
            InitializeComponent();
        }

        private void FrmLogisticsNodeSuperView_Load(object sender, EventArgs e)
        {
            //NodeViewLoad();
            //winFormPager1.OnPageChanged += new EventHandler(winFormPager1_OnPageChanged);
            //GetColdChainData();
        }
        public void NodeViewLoad()
        {
            try
            {
                lbWaybillNumber.Text = waybase.Numberk__BackingField;
                lbWarehouseName.Text = "【" + node.StorageNamek__BackingField + "】";
                lbStartTime.Text = node.operateAtk__BackingField.ToString("yyyy-MM-dd HH:mm:ss");
                //获取开始时间的下面的时间
                ResultModelOfArrayOfModel_Waybill_Noded4FqxSXX nodedate = cs.GetWaybillNodeList(node.BaseIdk__BackingField.ToString(), node.operateAtk__BackingField.ToString(), null);
                if (nodedate.Code != 0)
                {
                    MessageBox.Show(nodedate.Message);
                }
                else
                {
                    foreach (Model_Waybill_Node item in nodedate.Data)
                    {
                        lbEndTime.Text = item.operateAtk__BackingField.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void winFormPager1_OnPageChanged(object sender,EventArgs e)
        {
            GetColdChainData();
        }
        public void GetColdChainData()
        {
            try
            {
                pictureBox1.Show();
                if (lbStartTime.Text.Trim() == null && lbEndTime.Text.Trim() == null) return;
                //获取显示的标题行
                ResultModelOfArrayOfModel_AiInfod4FqxSXX biaoti = cs.GetAiInfoByStorageId(node.StorageIdk__BackingField, true,null);
                if (biaoti.Code != 0)
                {
                    MessageBox.Show(biaoti.Message);
                }
                else
                {
                    dataGridView2.Columns.Clear();
                    //dataGridView2.Columns[0].HeaderText = "记录时间";
                    dataGridView2.Columns.Add("记录时间", "记录时间");
                    foreach (Model_AiInfo item in biaoti.Data)
                    {
                        dataGridView2.Columns.Add(item.PpointNamek__BackingField, item.PpointNamek__BackingField);
                    }
                    dataGridView2.Columns.Add("报警状态", "报警状态");

                }
                //线程 调用提示文字框
                //waitForm = null;
                //Thread waitThread = new Thread(new ThreadStart(this._ShowWaitForm));
                //waitThread.Start();
                //Thread.Sleep(100);
                
                string pageIndexAndCount = winFormPager1.PageIndex + "." + winFormPager1.PageSize;
                ResultModelOfint result = cs.GetWaybillNodeHistDataCount(node.StorageIdk__BackingField, true, lbStartTime.Text.Trim(), lbEndTime.Text.Trim());
                winFormPager1.DrawControl(result.Data);
                //根据冷库id、开始时间、结束时间（下一个开始时间）查询冷链数据
                ResultModelOfArrayOfArrayOfstringuHEDJ7Dj nodedatalist = cs.GetWaybillNodeHistDataList(node.StorageIdk__BackingField, true, lbStartTime.Text.Trim(), lbEndTime.Text.Trim(),pageIndexAndCount);
                //ResultModelOfArrayOfArrayOfstringuHEDJ7Dj count = cs.GetWaybillNodeHistDataList(node.StorageIdk__BackingField, true, lbStartTime.Text.Trim(), lbEndTime.Text.Trim(), null);
                //winFormPager1.DrawControl(count.Data.Count<String[]>());
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
                    dataGridView2.Rows.Clear();
                    foreach (String[] item in nodedatalist.Data)
                    {
                        int rowIndex = dataGridView2.Rows.Add();
                        for (int i = 0; i < item.Length; i++)
                        {
                            if (item[i] != item[item.Length - 1])
                            {
                                //如果是第一列设置宽度和时间格式
                                if (item[i] == item[0])
                                {
                                    dataGridView2.Columns[i].Width = 130;
                                    dataGridView2.Columns[i].Resizable = DataGridViewTriState.False;
                                    dataGridView2.Rows[rowIndex].Cells[i].Value = DateTime.Parse(item[i]).ToString("yyyy-MM-dd HH:mm:ss");
                                }
                                else
                                {
                                    //如果不为空和非负浮点数的值显示一位小数点后的数字
                                    if(item[i].ToString()!=string.Empty && IsInteger(item[i].ToString())==true)
                                        dataGridView2.Rows[rowIndex].Cells[i].Value = Math.Round(Convert.ToDecimal(item[i]), 1);
                                    else
                                        dataGridView2.Rows[rowIndex].Cells[i].Value = "- -";
                                }
                            }
                            else
                            {
                                if (item[item.Length - 1] == 0.ToString())
                                    dataGridView2.Rows[rowIndex].Cells[i].Value = "正常";
                                else
                                    dataGridView2.Rows[rowIndex].Cells[i].Value = "报警";
                            }
                            dataGridView2.Rows[rowIndex].Tag = item;
                        }
                        dataGridView2.Rows[rowIndex].Tag = item;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        public static bool IsInteger(string vehicleNumber)
        {
            string express = @"^\d+(\.\d+)?$";
            return Regex.IsMatch(vehicleNumber, express);
        }
        /// <summary>
        /// 导出PDF
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportPDF_Click(object sender, EventArgs e)
        {
            ConvertGridViewToPdfs();
        }
        
        public void ConvertGridViewToPdfs()
        {
            try
            {
                int rowCount = dataGridView2.Rows.Count;//获取dataGridView1的行数
                if (rowCount == 0)
                {
                    MessageBox.Show("没有数据可供导出。。。", "Prompt", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                string fileName = string.Empty;
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.DefaultExt = ".pdf";
                saveFileDialog.Filter = "PDF文件(*.pdf)|*.pdf";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    fileName = saveFileDialog.FileName;
                    string fontPath = string.Empty;
                    fontPath = AppDomain.CurrentDomain.BaseDirectory + "SIMHEI.TTF";
                    if (File.Exists(fontPath) == false)
                    {
                        MessageBox.Show("SIMHEI.TTF字体缺失，请安装字体后将其拷贝到程序根目录", "导出失败", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    //设置页面大小
                    iTextSharp.text.Rectangle pageSize = new iTextSharp.text.Rectangle(794f, 716f);
                    pageSize.BackgroundColor = new iTextSharp.text.Color(0xFF, 0xFF, 0xDE);
                    //BaseFont bfHei = BaseFont.CreateFont(@"C:\Windows\Fonts\simfang.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                    //iTextSharp.text.Font font = new iTextSharp.text.Font(bfHei, 10);
                    //创建PDF文档中的字体 
                    BaseFont baseFont = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
                    //根据字体路径和字体大小属性创建字体
                    iTextSharp.text.Font font = new iTextSharp.text.Font(baseFont, 12);
                    //第一步，创建一个 iTextSharp.text.Document对象的实例
                    Document document = new Document(pageSize, 36f, 72f, 108f, 180f);
                    //第二步，为该Document创建一个Writer实例
                    PdfWriter.GetInstance(document, new FileStream(fileName, FileMode.Create));
                    //第三步，打开当前Document 
                    document.Open();
                    Paragraph para = null;
                    //添加标题和导出数据时间段
                    para = new Paragraph(lbWarehouseName.Text.Trim(), new iTextSharp.text.Font(baseFont, 20));
                    para.Alignment = Element.ALIGN_CENTER;
                    document.Add(para);
                    para = new Paragraph(string.Format("监控时间：{0}至{1}",lbStartTime.Text.Trim(),lbEndTime.Text.Trim()), font);
                    para.Alignment = Element.ALIGN_LEFT;
                    document.Add(para);
                    //第四步，为当前Document添加内容： 
                    //document.Add(new Paragraph(""));
                    //PdfPTable table = new PdfPTable(widths);
                    PdfPTable table = new PdfPTable(dataGridView2.Columns.Count);
                    Single[] sin = new Single[dataGridView2.Columns.Count];
                    for (int i = 0; i < dataGridView2.Columns.Count; i++)
                    {
                        //表格宽度
                        if (i == 0)
                        {
                            sin[i] = 20;
                        }
                        else
                            sin[i] = 10;
                    }
                    table.TotalWidth = document.Right - document.Left;
                    table.SetWidths(sin);
                    table.LockedWidth = true;
                    ResultModelOfArrayOfModel_AiInfod4FqxSXX biaoti = cs.GetAiInfoByStorageId(node.StorageIdk__BackingField, true, null);
                    if (biaoti.Code != 0) MessageBox.Show(biaoti.Message);
                    else
                    {
                        table.AddCell(new Phrase("记录时间", font));
                        foreach (Model_AiInfo item in biaoti.Data)
                        {
                            table.AddCell(new Phrase(item.PpointNamek__BackingField, font));
                        }
                        table.AddCell(new Phrase("报警状态", font));
                    }
                    #region 导出一条
                    //String[] str = dataGridView2.SelectedRows[0].Tag as String[];
                    //for (int i = 0; i < str.Length; i++)
                    //{
                    //    if (str[i] != str[str.Length - 1])
                    //    {
                    //        if (str[i] == str[0])
                    //        {
                    //            table.AddCell(new Phrase(DateTime.Parse(str[i]).ToString("yyyy-MM-dd HH:mm:ss"), font));
                    //        }
                    //        else
                    //        {
                    //            if (str[i].Length > 0)
                    //                table.AddCell(new Phrase(Math.Round(Convert.ToDecimal(str[i]), 1).ToString(), font));
                    //            else
                    //                table.AddCell(new Phrase(str[i], font));
                    //        }
                    //    }
                    //    else
                    //    {
                    //        if (str[i] == 0.ToString())

                    //            table.AddCell(new Phrase("正常", font));
                    //        else
                    //            table.AddCell(new Phrase("报警", font));
                    //    }
                    //} 
                    #endregion
                    ResultModelOfArrayOfArrayOfstringuHEDJ7Dj nodedatalist = cs.GetWaybillNodeHistDataList(node.StorageIdk__BackingField, true, lbStartTime.Text.Trim(), lbEndTime.Text.Trim(), null);
                    if (nodedatalist.Code != 0)
                    {
                        MessageBox.Show(nodedatalist.Message);
                    }
                    else
                    {
                        foreach (String[] str in nodedatalist.Data)
                        {
                            for (int i = 0; i < str.Length; i++)
                            {
                                if (str[i] != str[str.Length - 1])
                                {
                                    if (str[i] == str[0])
                                    {
                                        table.AddCell(new Phrase(DateTime.Parse(str[i]).ToString("yyyy-MM-dd HH:mm:ss"), font));
                                    }
                                    else
                                    {
                                        //如果不为空和非负浮点数的值显示一位小数点后的数字
                                        if (str[i].ToString() != string.Empty && IsInteger(str[i].ToString()) == true)
                                            table.AddCell(new Phrase(Math.Round(Convert.ToDecimal(str[i]), 1).ToString(), font));
                                        else
                                            table.AddCell(new Phrase("- -", font));
                                    }
                                }
                                else
                                {
                                    if (str[i] == 0.ToString())
                                        table.AddCell(new Phrase("正常", font));
                                    else
                                        table.AddCell(new Phrase("报警", font));
                                }
                            }
                        }
                    }
                    document.Add(table);
                    //第五步，关闭Document 
                    document.Close();
                    MessageBox.Show("导出文件成功");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"error",MessageBoxButtons.OK,MessageBoxIcon.Error);
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

        private void FrmLogisticsNodeSuperView_Shown(object sender, EventArgs e)
        {
            NodeViewLoad();
            winFormPager1.OnPageChanged += new EventHandler(winFormPager1_OnPageChanged);
            GetColdChainData();
        }
    }
}
