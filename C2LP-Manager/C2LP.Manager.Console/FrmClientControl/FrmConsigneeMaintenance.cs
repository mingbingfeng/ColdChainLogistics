using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using C2LP.Manager.Console.ConsoleServerWebReference;

namespace C2LP.Manager.Console.FrmClientControl
{
    public partial class FrmConsigneeMaintenance : Form
    {
        ConsoleServerWebReference.ConsoleServer cs = new ConsoleServerWebReference.ConsoleServer();
        public FrmConsigneeMaintenance()
        {
            InitializeComponent();
        }

        private void FrmConsigneeMaintenance_Load(object sender, EventArgs e)
        {
            Province();
            City();
            County();
            cmbProvince.SelectedIndexChanged += dataGridView1_SelectionChanged;
            cmbCity.SelectedIndexChanged += cmbCity_SelectedIndexChanged;
            cmbCounty.SelectedIndexChanged += cmbCounty_SelectedIndexChanged;
            winFormPager1.OnPageChanged += new EventHandler(winFormPager1_OnPageChanged);
            dataGridView1.KeyDown += new KeyEventHandler(dataGridView1_KeyDown);
            // customerLoad();
            AvGue();
            AutoSizeColumn(dataGridView1);
        }
        public void winFormPager1_OnPageChanged(object sender, EventArgs e)
        {
            //if (txtDownstream.Text.Trim() == string.Empty)
            //{
            //    customerLoad();
            //}
            //else
            //{
            //    AvGue();
            //}
            AvGue();
        }
        public void customerLoad()
        {
            string pageIndexAndCount = winFormPager1.PageIndex + "." + winFormPager1.PageSize;
            if (cmbProvince.Text=="中国" && cmbCity.Text== "中国")
                CustomerLoad(0,0,pageIndexAndCount);
            else
                CustomerLoad(Convert.ToInt32(cmbProvince.SelectedValue), Convert.ToInt32(cmbCity.SelectedValue), pageIndexAndCount);
        }
        public void CustomerLoad(int provinceId,int cityId, string pageIndexAndCount)
        {
            try
            {
                //ResultModelOfArrayOfModel_Customerd4FqxSXX count = cs.GetCustomerListByRole(Enum_Role.Receiver, true, provinceId, true, cityId, true, null);
                //winFormPager1.DrawControl(count.Data.Count<Model_Customer>());
                ResultModelOfint result = cs.GetCustomerListByRoleCount(Enum_Role.Receiver, true, provinceId, true, cityId, true);
                winFormPager1.DrawControl(result.Data);
                ResultModelOfArrayOfModel_Customerd4FqxSXX customer = cs.GetCustomerListByRole(Enum_Role.Receiver, true, provinceId, true, cityId, true, pageIndexAndCount);
                //if (count.Data.Count<Model_Customer>() <= 0)
                if (result.Data <= 0)
                    contextMenuStrip1.Enabled = false;
                else
                    contextMenuStrip1.Enabled = true; 
                if (customer.Code != 0)
                {
                    MessageBox.Show(customer.Message);
                }
                else
                {
                    dataGridView1.AutoGenerateColumns = false;
                    dataGridView1.Rows.Clear();
                    foreach (Model_Customer item in customer.Data)
                    {
                        int rowIndex = dataGridView1.Rows.Add();
                        dataGridView1.Rows[rowIndex].Cells[0].Value = item.Idk__BackingField;
                        dataGridView1.Rows[rowIndex].Cells[1].Value = item.FullNamek__BackingField;
                        dataGridView1.Rows[rowIndex].Cells[2].Value = item.ContactPersonk__BackingField;
                        dataGridView1.Rows[rowIndex].Cells[3].Value = item.Accountk__BackingField;
                        dataGridView1.Rows[rowIndex].Cells[4].Value = item.ContactTelk__BackingField;
                        dataGridView1.Rows[rowIndex].Cells[5].Value = item.ProvinceNamek__BackingField + item.CityNamek__BackingField;
                        dataGridView1.Rows[rowIndex].Cells[6].Value = item.ContactAddressk__BackingField;
                        dataGridView1.Rows[rowIndex].Cells[7].Value = item.CreateAtk__BackingField.ToString("yyyy-MM-dd HH:mm:ss");
                        if (item.Activedk__BackingField == Enum_Active.Enabled)
                            dataGridView1.Rows[rowIndex].Cells[8].Value = "启用";
                        else
                            dataGridView1.Rows[rowIndex].Cells[8].Value = "停用";
                        dataGridView1.Rows[rowIndex].Cells[9].Value = item.Remarkk__BackingField.Split('|')[1];
                        dataGridView1.Rows[rowIndex].Tag = item;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        #region 省市
        public void Province()
        {
            try
            {
                ResultModelOfArrayOfModel_Regiond4FqxSXX prov = cs.GetRegionInfo(1, true);
                if (prov.Code != 0)
                {
                    MessageBox.Show(prov.Message);
                }
                else
                {
                    List<Model_Region> regions = new List<Model_Region>();
                    Model_Region regs = new Model_Region();
                    regs.Idk__BackingField = 0;
                    regs.Namek__BackingField = "中国";
                    regions.Add(regs);
                    foreach (Model_Region item in prov.Data)
                    {
                        Model_Region reg = new Model_Region();
                        reg.Idk__BackingField = item.Idk__BackingField;
                        reg.Namek__BackingField = item.Namek__BackingField;
                        reg.ParentIdk__BackingField = item.ParentIdk__BackingField;
                        regions.Add(reg);
                    }
                    cmbProvince.DisplayMember = "Namek__BackingField";
                    cmbProvince.ValueMember = "Idk__BackingField";
                    cmbProvince.DataSource = regions;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            //txtDownstream.Text = string.Empty;
            City();
        }
        public void City()
        {
            ResultModelOfArrayOfModel_Regiond4FqxSXX city = null;
            try
            {
                if (Convert.ToInt32(cmbProvince.SelectedValue) == 1)
                    city = cs.GetRegionInfo(0, true);
                else
                    city = cs.GetRegionInfo(Convert.ToInt32(cmbProvince.SelectedValue), true);
                if (city.Code != 0)
                {
                    MessageBox.Show(city.Message);
                }
                else
                {
                    cmbCity.DisplayMember = "Namek__BackingField";
                    cmbCity.ValueMember = "Idk__BackingField";
                    cmbCity.DataSource = city.Data;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }
        private void cmbCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////初始化当前页为1
            //winFormPager1.PageIndex = 1;
            ////customerLoad();
            ////txtDownstream.Text = string.Empty;
            //AvGue();
            County();
        }
        public void County()
        {
            ResultModelOfArrayOfModel_Regiond4FqxSXX county = null;
            try
            {
                if (Convert.ToInt32(cmbCity.SelectedValue) == 1)
                    county = cs.GetRegionInfo(0, true);
                else
                    county = cs.GetRegionInfo(Convert.ToInt32(cmbCity.SelectedValue), true);
                if (county.Code != 0)
                {
                    MessageBox.Show(county.Message);
                }
                else
                {
                    cmbCounty.DisplayMember = "Namek__BackingField";
                    cmbCounty.ValueMember = "Idk__BackingField";
                    cmbCounty.DataSource = county.Data;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cmbCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            //初始化当前页为1
            winFormPager1.PageIndex = 1;
            AvGue();
        }
        #endregion


        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmEdit_Click(object sender, EventArgs e)
        {
            Model_Customer customer = dataGridView1.SelectedRows[0].Tag as Model_Customer;
            FrmConsigneeMaintenanceNew maintenance = new FrmConsigneeMaintenanceNew();
            maintenance.Customers = customer;
            maintenance._ParentMaintenace = this;
            maintenance.ShowDialog();
            //customerLoad();
            AvGue();
        }
        /// <summary>
        /// 账号管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmAccount_Click(object sender, EventArgs e)
        {
            Model_Customer customer = dataGridView1.SelectedRows[0].Tag as Model_Customer;
            FrmConsigneeMaintenanceID maintenanceid = new FrmConsigneeMaintenanceID();
            maintenanceid.Customers = customer;
            maintenanceid.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmConsigneeMaintenanceNew maintenance = new FrmConsigneeMaintenanceNew();
            maintenance._ParentMaintenace = this;
            maintenance.ShowDialog();
            //customerLoad();
            AvGue();
        }
        public void PC()
        {
            //customerLoad();
            AvGue();
        }
        /// <summary>
        /// 停用状态修改只读状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            Model_Customer customer = dataGridView1.SelectedRows[0].Tag as Model_Customer;
            if (customer.Activedk__BackingField==Enum_Active.Enabled)
            {
                tsmDisable.Enabled = true;
            }
            else
            {
                tsmDisable.Enabled = false;
            }
        }
        /// <summary>
        /// 停用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmDisable_Click(object sender, EventArgs e)
        {
            //获取选中的整行数据
            Model_Customer editCustomer = dataGridView1.SelectedRows[0].Tag as Model_Customer;
            editCustomer.Activedk__BackingField = Enum_Active.Disable;
            try
            {
                ResultModelOfModel_Customerd4FqxSXX customer = cs.EditCustomer(editCustomer);
                if (customer.Code != 0)
                {
                    MessageBox.Show(customer.Message);
                }
                else
                {
                    MessageBox.Show("操作成功");
                    //customerLoad();
                    AvGue();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 禁用全选（ctrl+a）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_KeyDown(object sender,KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
                e.Handled = true;
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
            AvGue();
            
        }

        public void AvGue()
        {
            try
            {
                string fullname = null;
                int provid = 0;
                int cityid = 0;
                int countyid = 0;
                if (txtDownstream.Text.Trim() != string.Empty)
                {
                    fullname = txtDownstream.Text.Trim();
                }
                if (cmbProvince.Text != "中国" && cmbCity.Text != "中国" && cmbCounty.Text!="中国")
                {
                    provid = Convert.ToInt32(cmbProvince.SelectedValue);
                    cityid = Convert.ToInt32(cmbCity.SelectedValue);
                    countyid = Convert.ToInt32(cmbCounty.SelectedValue);
                }
                string pageIndexAndCount = winFormPager1.PageIndex + "." + winFormPager1.PageSize;
                Model_Customer customeravgue = new Model_Customer();
                customeravgue.FullNamek__BackingField = fullname;
                customeravgue.ProvinceIdk__BackingField = provid;
                customeravgue.CityIdk__BackingField = cityid;
                customeravgue.CountyIdk__BackingField = countyid;
                
                //ResultModelOfint avguecount = cs.GetVagueQueryCount(customeravgue);
                ResultModelOfint avguecount = cs.GetConsigneeCountyCount(customeravgue);
                winFormPager1.DrawControl(avguecount.Data);
                //ResultModelOfArrayOfModel_Customerd4FqxSXX avgue = cs.GetVagueQuery(customeravgue, pageIndexAndCount);
                ResultModelOfArrayOfModel_Customerd4FqxSXX avgue = cs.GetConsigneeCounty(customeravgue, pageIndexAndCount);
                if (avguecount.Data <= 0)
                    contextMenuStrip1.Enabled = false;
                else
                    contextMenuStrip1.Enabled = true;
                if (avgue.Code != 0)
                {
                    MessageBox.Show(avgue.Message);
                }
                else
                {
                    dataGridView1.AutoGenerateColumns = false;
                    dataGridView1.Rows.Clear();
                    foreach (Model_Customer item in avgue.Data)
                    {
                        int rowIndex = dataGridView1.Rows.Add();
                        dataGridView1.Rows[rowIndex].Cells[0].Value = item.Idk__BackingField;
                        dataGridView1.Rows[rowIndex].Cells[1].Value = item.FullNamek__BackingField;
                        dataGridView1.Rows[rowIndex].Cells[2].Value = item.ContactPersonk__BackingField;
                        dataGridView1.Rows[rowIndex].Cells[3].Value = item.Accountk__BackingField;
                        dataGridView1.Rows[rowIndex].Cells[4].Value = item.ContactTelk__BackingField;
                        dataGridView1.Rows[rowIndex].Cells[5].Value = item.ProvinceNamek__BackingField +"-"+ item.CityNamek__BackingField+"-"+item.CountyNamek__BackingField;
                        dataGridView1.Rows[rowIndex].Cells[6].Value = item.ContactAddressk__BackingField;
                        dataGridView1.Rows[rowIndex].Cells[7].Value = item.CreateAtk__BackingField.ToString("yyyy-MM-dd HH:mm:ss");
                        if (item.Activedk__BackingField == Enum_Active.Enabled)
                            dataGridView1.Rows[rowIndex].Cells[8].Value = "启用";
                        else
                            dataGridView1.Rows[rowIndex].Cells[8].Value = "停用";
                        dataGridView1.Rows[rowIndex].Cells[9].Value = item.Remarkk__BackingField.Split('|')[1];
                        dataGridView1.Rows[rowIndex].Tag = item;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
    }
}
