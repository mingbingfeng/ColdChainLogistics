using C2LP.Manager.Console.ConsoleServerWebReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace C2LP.Manager.Console.FrmClientControl
{
    public partial class FrmCooperativeClientList : Form
    {
        ConsoleServerWebReference.ConsoleServer cs = new ConsoleServerWebReference.ConsoleServer();
        public FrmCooperativeClientList()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 新建合作客户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            FrmCooperativeClientNew clinetnew = new FrmCooperativeClientNew();
            clinetnew._ParentClientList = this;
            clinetnew.ShowDialog();
            getCustomer();
        }
        public void getRenew()
        {
            getCustomer();
        }
        private void FrmCooperativeClientList_Load(object sender, EventArgs e)
        {
            getProvince();
            getCityLevel();
            GetCounty();
            cmbProvince.SelectedIndexChanged += cmbProvince_SelectedIndexChanged;
            cmbCity.SelectedIndexChanged += cmbCity_SelectedIndexChanged;
            cmbCounty.SelectedIndexChanged += cmbCounty_SelectedIndexChanged;
            winFormPager1.OnPageChanged += new EventHandler(winFormPager1_OnPageChanged);
            dataGridView1.KeyDown += new KeyEventHandler(dataGridView1_KeyDown);
            //绑定数据
            getCustomer();
            AutoSizeColumn(dataGridView1);
        }
        public void winFormPager1_OnPageChanged(object sender, EventArgs e)
        {
            getCustomer();
        }
        public void getCustomer()
        {
            string pageIndexAndCount = winFormPager1.PageIndex + "." + winFormPager1.PageSize;
            if (cmbProvince.Text=="中国" && cmbCity.Text== "中国")
                getCustomerLoad(0, 0,0, pageIndexAndCount);
            else
                getCustomerLoad(Convert.ToInt32(cmbProvince.SelectedValue), Convert.ToInt32(cmbCity.SelectedValue), Convert.ToInt32(cmbCounty.SelectedValue), pageIndexAndCount);
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        public void getCustomerLoad(int provinceId, int cityId,int county, string pageIndexAndCount)
        {
            try
            {
                //ResultModelOfArrayOfModel_Customerd4FqxSXX count = cs.GetCustomerListByRole(Enum_Role.Sender, true, provinceId, true, cityId, true, null);
                //ResultModelOfint result = cs.GetCustomerListByRoleCount(Enum_Role.Sender, true, provinceId, true, cityId, true);
                //winFormPager1.DrawControl(count.Data.Count<Model_Customer>());
                //ResultModelOfArrayOfModel_Customerd4FqxSXX customer = cs.GetCustomerListByRole(Enum_Role.Sender, true, provinceId, true, cityId, true, pageIndexAndCount);
                ResultModelOfint result = cs.GetCustomerListByCountyCount(Enum_Role.Sender, true, provinceId, true, cityId, true, county, true);
                winFormPager1.DrawControl(result.Data);
                ResultModelOfArrayOfModel_Customerd4FqxSXX customer = cs.GetCustomerListByCounty(Enum_Role.Sender, true, provinceId, true, cityId, true,county,true, pageIndexAndCount);
                //if (count.Data.Count<Model_Customer>()<=0)
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
                        dataGridView1.Rows[rowIndex].Cells[5].Value = item.ProvinceNamek__BackingField +"-"+ item.CityNamek__BackingField+"-"+item.CountyNamek__BackingField;
                        dataGridView1.Rows[rowIndex].Cells[6].Value = item.ContactAddressk__BackingField;
                        dataGridView1.Rows[rowIndex].Cells[7].Value = item.CreateAtk__BackingField.ToString("yyyy-MM-dd HH:mm:ss");
                        if(item.Activedk__BackingField==Enum_Active.Enabled)
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
        #region 省份
        /// <summary>
        /// 省份
        /// </summary>
        public void getProvince()
        {
            try
            {
                ResultModelOfArrayOfModel_Regiond4FqxSXX prov = cs.GetRegionInfo(1, true);
                Model_Region reg = new Model_Region();
                List<Model_Region> regions = new List<Model_Region>();
                reg.Idk__BackingField = 0;
                reg.Namek__BackingField = "中国";
                regions.Add(reg);
                foreach (Model_Region item in prov.Data)
                {
                    Model_Region regs = new Model_Region();
                    regs.Idk__BackingField = item.Idk__BackingField;
                    regs.Namek__BackingField = item.Namek__BackingField;
                    regs.ParentIdk__BackingField = item.ParentIdk__BackingField;
                    regions.Add(regs);
                }
                if (prov.Code != 0)
                {
                    MessageBox.Show(prov.Message);
                }
                else
                {
                    cmbProvince.DisplayMember = "Namek__BackingField";
                    cmbProvince.ValueMember = "Idk__BackingField";
                    cmbProvince.DataSource = regions;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void cmbProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            getCityLevel();
        }
        /// <summary>
        /// 市级
        /// </summary>
        public void getCityLevel()
        {
            ResultModelOfArrayOfModel_Regiond4FqxSXX city = null;
            try
            {
                if (Convert.ToInt32(cmbProvince.SelectedValue) == 1)
                    city = cs.GetRegionInfo(0, true);
                else
                    city = cs.GetRegionInfo(Convert.ToInt32(cmbProvince.SelectedValue), true);
                if (city.Code != 0)
                    MessageBox.Show(city.Message);
                else
                {
                    cmbCity.DisplayMember = "Namek__BackingField";
                    cmbCity.ValueMember = "Idk__BackingField";
                    cmbCity.DataSource = city.Data;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cmbCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            ////初始化当前页为1
            //winFormPager1.PageIndex = 1;
            //getCustomer();
            GetCounty();
        }
        /// <summary>
        /// 县级
        /// </summary>
        public void GetCounty()
        {
            ResultModelOfArrayOfModel_Regiond4FqxSXX county = null;
            try
            {
                if (Convert.ToInt32(cmbCity.SelectedValue) == 1)
                    county = cs.GetRegionInfo(0, true);
                else
                    county = cs.GetRegionInfo(Convert.ToInt32(cmbCity.SelectedValue), true);
                if (county.Code != 0)
                    MessageBox.Show(county.Message);
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
            getCustomer();
        }
        #endregion



        public static string Id;
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmEdit_Click(object sender, EventArgs e)
        {
            //string id =dataGridView1.SelectedCells[0].Value.ToString();
            Model_Customer cust = dataGridView1.SelectedRows[0].Tag as Model_Customer;
            FrmCooperativeClientNew clinetnew = new FrmCooperativeClientNew();
            //clinetnew.ID = id.Trim() ;
            clinetnew.cust = cust;
            clinetnew._ParentClientList = this;
            clinetnew.ShowDialog();
            getCustomer();
        }
        /// <summary>
        /// 停用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmDisable_Click(object sender, EventArgs e)
        {
           // string id = dataGridView1.SelectedCells[0].Value.ToString();
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
                    getCustomer();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 账号管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmAccount_Click(object sender, EventArgs e)
        {
            //string Id = dataGridView1.SelectedCells[0].Value.ToString();
            //string Name = dataGridView1.SelectedCells[1].Value.ToString();
            Model_Customer customs = dataGridView1.SelectedRows[0].Tag as Model_Customer;
            FrmCooperativeClientID clientid = new FrmCooperativeClientID();
            //clientid.ID = Id.Trim();
            //clientid.fullName = Name.Trim();
            clientid.customs = customs;
            clientid.ShowDialog();
        }
        /// <summary>
        /// 判断启用/停用修改只读状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            Model_Customer customs = dataGridView1.SelectedRows[0].Tag as Model_Customer;
            if (customs.Activedk__BackingField==Enum_Active.Enabled)
            {
                tsmDisable.Enabled = true;
            }
            else
            {
                tsmDisable.Enabled = false;
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

       
    }
}
