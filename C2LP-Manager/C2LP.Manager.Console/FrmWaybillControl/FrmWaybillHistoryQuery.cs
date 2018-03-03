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
    public partial class FrmWaybillHistoryQuery : Form
    {
        public FrmWaybillHistoryQuery()
        {
            InitializeComponent();
        }
        ConsoleServerWebReference.ConsoleServer cs = new ConsoleServerWebReference.ConsoleServer();
        
        private void FrmWaybillHistoryQuery_Load(object sender, EventArgs e)
        {
            dtpStartTime.CustomFormat = " ";
            dtpEndTime.CustomFormat = " ";
            winFormPager1.OnPageChanged += new EventHandler(winFormPager1_OnPageChanged);
            dataGridView1.KeyDown += new KeyEventHandler(dataGridView1_KeyDown);
            //deliverGoods();
            //receivingGoods();
            waybillLoad();
            deliverGoods();
            //receivingGoods();
            zoneOptions();
            AutoSizeColumn(dataGridView1);
        }
        public void winFormPager1_OnPageChanged(object sender, EventArgs e)
        {
            waybillLoad();
        }
        public void waybillLoad()
        {
            string waybillNumber = null; //运单号
            string startTime = null; //起始时间
            string endTime = null; //结束时间
            string pageIndexAndCount = winFormPager1.PageIndex + "." + winFormPager1.PageSize;  //分页参数
            if (txtNumber.Text.Trim() != string.Empty)
                waybillNumber = txtNumber.Text.Trim();
            if (dtpStartTime.Text.ToString() != " ")
                startTime = dtpStartTime.Value.ToString("yyyy-MM-dd HH:mm:ss");
            if (dtpEndTime.Text.ToString() != " ")
                endTime = dtpEndTime.Value.ToString("yyyy-MM-dd HH:mm:ss");

            //if (cmbdeliverGoods.Text != string.Empty && cmbReceivingGoods.Text != string.Empty)
            //{
            //    if (cmbdeliverGoods.Text != "全部" && cmbReceivingGoods.Text != "全部")
            //    {
            //        MessageBox.Show("请选择一个单位，上游发货单位或者下游收货单位！");
            //        return;
            //    }
            //}
            int senderId = 0; //上客户ID
            int receiverId = 0;
            if (cmbdeliverGoods.Text != string.Empty)
            {
                if (cmbdeliverGoods.Text != "全部")
                    senderId = (int)cmbdeliverGoods.SelectedValue;

            }
            if (cmbReceivingGoods.Text != string.Empty)
            {
                if (cmbReceivingGoods.Text != "全部")
                    receiverId = funll.FirstOrDefault(q => q.Value == cmbReceivingGoods.Text).Key; 
            }
            waybillbaseLoad(waybillNumber, pageIndexAndCount, startTime, endTime, senderId, receiverId);
        }
        /// <summary>
        /// 加载运单主体信息
        /// </summary>
        public void waybillbaseLoad(string waybillNumber, string pageIndexAndCount, string startTime, string endTime, int senderId,int receiverId)
        {
            try
            {
                if (startTime != null && endTime != null)
                {
                    if (Convert.ToDateTime(startTime) > Convert.ToDateTime(endTime))
                    {
                        MessageBox.Show("开始时间不能大于结束时间");
                        return;
                    }
                }

                ResultModelOfint result = cs.GetQueryClientsListCount(waybillNumber, startTime, endTime, senderId, true, receiverId,true);
                winFormPager1.DrawControl(result.Data);
                if (result.Data <= 0)
                    contextMenuStrip1.Enabled = false;
                else
                    contextMenuStrip1.Enabled = true;
                ResultModelOfArrayOfModel_Waybill_Based4FqxSXX waybill = cs.GetQueryClientsList(waybillNumber, pageIndexAndCount, startTime, endTime, senderId, true, receiverId,true);
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
                        dataGridView1.Rows[rowIndex].Tag = item;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 根据运单编号查询信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            //手动改变时间，回车后获取不到改变时间。要给按钮设置焦点。
            btnQuery.Focus();
            //初始化当前页为第1页
            winFormPager1.PageIndex = 1;
            waybillLoad();
        }
        /// <summary>
        /// 加载客户名称
        /// </summary>
        public void deliverGoods()
        {
            try
            {
                ResultModelOfArrayOfModel_Customerd4FqxSXX customername = cs.GetCustomerList(Enum_Role.Sender, true, 0, true, 0, true, 0, true, null);
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
                    cmbdeliverGoods.DisplayMember = "fullNamek__BackingField";
                    cmbdeliverGoods.ValueMember = "idk__BackingField";
                    cmbdeliverGoods.DataSource = list;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 下游客户
        /// </summary>
        public void receivingGoods()
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
                    //cmbReceivingGoods.DisplayMember = "fullNamek__BackingField";
                    //cmbReceivingGoods.ValueMember = "idk__BackingField";
                    //cmbReceivingGoods.DataSource = list;
                    //fullNameList = list.Select(l => l.FullNamek__BackingField).ToList();
                    
                    foreach (Model_Customer item in list)
                    {
                        funll.Add(item.Idk__BackingField, item.FullNamek__BackingField);
                    }
                    
                    cmbReceivingGoods.Items.AddRange(funll.Values.ToArray());
                    cmbReceivingGoods.Text = "全部";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 详细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmDetailed_Click(object sender, EventArgs e)
        {
            Model_Waybill_Base waybill = dataGridView1.SelectedRows[0].Tag as Model_Waybill_Base;
            FrmWaybillShow waybillshow = new FrmWaybillShow();
            waybillshow.waybill_base = waybill;
            waybillshow.ShowDialog();
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
                btnQuery_Click(null, null);
            }
        }

        private void dtpStartTime_ValueChanged(object sender, EventArgs e)
        {
            this.dtpStartTime.Format = DateTimePickerFormat.Custom;
            this.dtpStartTime.CustomFormat = "yyyy-MM-dd HH:mm:ss";
        }

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
        
        List<string> fullNameList = new List<string>();
        Dictionary<int, string> funll = null;
        /// <summary>
        /// 收货单位搜索功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbReceivingGoods_TextUpdate(object sender, EventArgs e)
        {
            cmbReceivingGoods.Items.Clear();
            fullNameList.Clear();
            foreach (var item in funll.Values)
            {
                if (item.Contains(cmbReceivingGoods.Text))
                {
                    fullNameList.Add(item);
                }
            }
            if (fullNameList.Count == 0)
                fullNameList.Add("");
            cmbReceivingGoods.Items.AddRange(fullNameList.ToArray());
            this.cmbReceivingGoods.SelectionStart = this.cmbReceivingGoods.Text.Length;
            Cursor = Cursors.Default;
            this.cmbReceivingGoods.DroppedDown = true;
        }

        #region 增加区域，根据选择区域改变下游客户
        /// <summary>
        /// 增减区域选项
        /// </summary>
        public void zoneOptions()
        {
            try
            {
                List<Model_Region> regionlist = new List<Model_Region>();
                ResultModelOfArrayOfModel_Regiond4FqxSXX zo = cs.GetZoneOptions();
                if (zo.Code != 0)
                {
                    MessageBox.Show(zo.Message);
                }
                else
                {
                    foreach (Model_Region item in zo.Data)
                    {
                        Model_Region region = new Model_Region();
                        region.Idk__BackingField = item.Idk__BackingField;
                        region.Namek__BackingField = item.Namek__BackingField;
                        regionlist.Add(region);
                    }
                }
                cmbQY.DisplayMember = "Namek__BackingField";
                cmbQY.ValueMember = "Idk__BackingField";
                cmbQY.DataSource = regionlist;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void City()
        {
            try
            {
                int a = 0;
                if (cmbQY.Text != string.Empty)
                {
                    if(cmbQY.Text!="中国")
                        a = Convert.ToInt32(cmbQY.SelectedValue);
                }
                List<Model_Region> regionlist = new List<Model_Region>();
                ResultModelOfArrayOfModel_Regiond4FqxSXX cit = cs.GetCity(a,true);
                if (cit.Code != 0)
                {
                    MessageBox.Show(cit.Message);
                }
                else
                {
                    foreach (Model_Region item in cit.Data)
                    {
                        Model_Region region = new Model_Region();
                        region.Idk__BackingField = item.Idk__BackingField;
                        region.Namek__BackingField = item.Namek__BackingField;
                        regionlist.Add(region);
                    }
                }
                cmbCity.DisplayMember = "Namek__BackingField";
                cmbCity.ValueMember = "Idk__BackingField";
                cmbCity.DataSource = regionlist;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void receivingGoodss()
        {
            try
            {
                string a = "1";
                if (cmbCity.Text != string.Empty)
                {
                    a = cmbCity.SelectedValue.ToString();
                }
                
                ResultModelOfArrayOfModel_Customerd4FqxSXX customername = cs.GetDownstreamQZList(a);
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
                    funll = new Dictionary<int, string>();
                    foreach (Model_Customer item in list)
                    {
                        funll.Add(item.Idk__BackingField, item.FullNamek__BackingField);
                    }
                    cmbReceivingGoods.Items.Clear();
                    cmbReceivingGoods.Items.AddRange(funll.Values.ToArray());
                    cmbReceivingGoods.Text = "全部";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cmbQY_SelectedIndexChanged(object sender, EventArgs e)
        {
            City();
        }
        private void cmbCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            receivingGoodss();
        }
        #endregion


    }
}
