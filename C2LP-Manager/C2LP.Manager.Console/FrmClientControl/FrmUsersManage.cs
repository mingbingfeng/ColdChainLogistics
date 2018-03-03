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
    public partial class FrmUsersManage : Form
    {
        ConsoleServerWebReference.ConsoleServer cs = new ConsoleServerWebReference.ConsoleServer();
        public FrmUsersManage()
        {
            InitializeComponent();
        }

        private void FrmUsersManage_Load(object sender, EventArgs e)
        {
            getProvince();
            getCity();
            GetCounty();
            cmbProvince.SelectedIndexChanged += cmbProvince_SelectedIndexChanged;
            cmbCity.SelectedIndexChanged += cmbCity_SelectedIndexChanged;
            winFormPager1.OnPageChanged += new EventHandler(winFormPager1_OnPageChanged);
            dataGridView1.KeyDown += new KeyEventHandler(dataGridView1_KeyDown);
            getCustomerLoad();
            AutoSizeColumn(dataGridView1);
        }
        #region 省市
        /// <summary>
        /// 省份
        /// </summary>
        public void getProvince()
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
                    Model_Region reg = new Model_Region();
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
            getCity();
        }
        public void getCity()
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
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void cmbCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCounty();
        }
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
        #endregion

        //public int cumerid { set; get; }
        //public string cumername { set; get; }
        //
        Model_Customer cus = new Model_Customer();
        public void getCustomerLoad()
        {
            try
            {
                //ResultModelOfArrayOfModel_Customerd4FqxSXX byroel = cs.GetCustomerListByRole(Enum_Role.Administrator, true, 0, true, 0, true, null);
                ResultModelOfArrayOfModel_Customerd4FqxSXX byroel = cs.GetCustomerListByCounty(Enum_Role.Administrator, true, 0, true, 0, true,0,true, null);
                if (byroel.Code != 0)
                {
                    MessageBox.Show(byroel.Message);
                }
                else
                {
                    foreach (Model_Customer item in byroel.Data)
                    {
                        txtCustomerFullName.Text = item.FullNamek__BackingField;
                        txtCustomerAccount.Text = item.Accountk__BackingField;
                        txtContacts.Text = item.ContactPersonk__BackingField;
                        txtContactNumber.Text = item.ContactTelk__BackingField;
                        cmbProvince.Text = item.ProvinceNamek__BackingField;
                        cmbCity.Text = item.CityNamek__BackingField;
                        cmbCounty.Text = item.CountyNamek__BackingField;
                        txtContactAddress.Text = item.ContactAddressk__BackingField;
                        txtRemarks.Text = item.Remarkk__BackingField;
                        //cumerid = item.Idk__BackingField;
                        //cumername = item.FullNamek__BackingField;
                        //
                        cus.Idk__BackingField = item.Idk__BackingField;
                        cus.FullNamek__BackingField = item.FullNamek__BackingField;
                        cus.CreateAtk__BackingField = item.CreateAtk__BackingField;
                        //
                        winFormPager1_PageChanged(item);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        public void winFormPager1_OnPageChanged(object sender, EventArgs e)
        {
            getCustomerLoad();
        }

        public void winFormPager1_PageChanged(Model_Customer item)
        {
            try
            {
                string pageIndexAndCount = winFormPager1.PageIndex + "." + winFormPager1.PageSize;
                ResultModelOfArrayOfModel_CustomerUserd4FqxSXX custuserlist = cs.GetCustomerUserList(item.Idk__BackingField, true, pageIndexAndCount);
                ResultModelOfArrayOfModel_CustomerUserd4FqxSXX count = cs.GetCustomerUserList(item.Idk__BackingField, true, null);
                winFormPager1.DrawControl(count.Data.Count<Model_CustomerUser>());
                if (custuserlist.Code != 0)
                {
                    MessageBox.Show(custuserlist.Message);
                }
                else
                {
                    dataGridView1.Rows.Clear();
                    dataGridView1.AutoGenerateColumns = false;
                    foreach (Model_CustomerUser userlist in custuserlist.Data)
                    {
                        int rowIndex = dataGridView1.Rows.Add();
                        dataGridView1.Rows[rowIndex].Cells[0].Value = userlist.Idk__BackingField;
                        dataGridView1.Rows[rowIndex].Cells[1].Value = userlist.UserNamek__BackingField;
                        dataGridView1.Rows[rowIndex].Cells[2].Value = userlist.DisplayNamek__BackingField;
                        dataGridView1.Rows[rowIndex].Cells[3].Value = userlist.Passwordk__BackingField;
                        dataGridView1.Rows[rowIndex].Cells[4].Value = userlist.CreateAtk__BackingField.ToString("yyyy-MM-dd HH:mm:ss");
                        if (userlist.Activedk__BackingField == Enum_Active.Enabled)
                            dataGridView1.Rows[rowIndex].Cells[5].Value = "启用";
                        else
                            dataGridView1.Rows[rowIndex].Cells[5].Value = "停用";
                        dataGridView1.Rows[rowIndex].Tag = userlist;

                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmEdit_Click(object sender, EventArgs e)
        {
            Model_Customer mcust = new Model_Customer();
            mcust.Rolek__BackingField = Enum_Role.Administrator;
            //
            mcust.Idk__BackingField = cus.Idk__BackingField;
            mcust.FullNamek__BackingField = cus.FullNamek__BackingField;
            //
            Model_CustomerUser cususer = dataGridView1.SelectedRows[0].Tag as Model_CustomerUser;
            FrmAccountsEdit edit = new FrmAccountsEdit();
            edit._ParentFrm = this;
            edit.customeruser = cususer;
            //edit.ID = cumerid.ToString();
            //edit.UserName = cumername;
            edit.mcust = mcust;
            edit.ShowDialog();
            getCustomerLoad();
        }
        /// <summary>
        /// 启用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmEnable_Click(object sender, EventArgs e)
        {
            Model_CustomerUser editcususer = dataGridView1.SelectedRows[0].Tag as Model_CustomerUser;
            editcususer.Activedk__BackingField = Enum_Active.Enabled;
            ResultModelOfModel_CustomerUserd4FqxSXX edituser = cs.EditCustomerUser(editcususer);
            if (edituser.Code != 0)
                MessageBox.Show(edituser.Message);
            else
            {
                MessageBox.Show("操作成功");
                getCustomerLoad();
            }
        }
        /// <summary>
        /// 停用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmDisable_Click(object sender, EventArgs e)
        {
            Model_CustomerUser editcususer = dataGridView1.SelectedRows[0].Tag as Model_CustomerUser;
            editcususer.Activedk__BackingField = Enum_Active.Disable;
            ResultModelOfModel_CustomerUserd4FqxSXX  edituser=cs.EditCustomerUser(editcususer);
            if (edituser.Code!=0)
            {
                MessageBox.Show(edituser.Message);
                getCustomerLoad();
            }
            else
            {
                MessageBox.Show("操作成功");
                getCustomerLoad();
            }
        }
        /// <summary>
        /// 密码用*显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellFormatting(object sender,DataGridViewCellFormattingEventArgs e)
        {
            //获取当前密码列的索引，密码列在4位
            if (e.ColumnIndex==3)
            {
                if (e.Value!=null && e.Value.ToString().Length>0)
                {
                    e.Value = new string('*',e.Value.ToString().Length);
                }
            }
        }
        
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Model_Customer mcust = new Model_Customer();
            mcust.Rolek__BackingField = Enum_Role.Administrator;
            //
            mcust.Idk__BackingField = cus.Idk__BackingField;
            mcust.FullNamek__BackingField = cus.FullNamek__BackingField;
            //
            FrmAccountsEdit edit = new FrmAccountsEdit();
            edit._ParentFrm = this;
            //edit.ID =cumerid.ToString();
            //edit.UserName = cumername;
            edit.mcust = mcust;
            edit.ShowDialog();
            getCustomerLoad();
        }
        //新增/更新页面上一个窗体刷新，参数看是否要传参数
        //AddRow(Model_CustomerUser user)
        public void getRenew() {
            getCustomerLoad();
        }
        
        /// <summary>
        /// 判断状态显示停用/启用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            Model_CustomerUser editcususer = dataGridView1.SelectedRows[0].Tag as Model_CustomerUser;
            if (editcususer.Activedk__BackingField == Enum_Active.Enabled)
            {
                tsmEnable.Enabled = false;
                tsmDisable.Enabled = true;
            }
            else
            {
                tsmEnable.Enabled = true;
                tsmDisable.Enabled = false;
            }
        }
        /// <summary>
        /// 禁用ctrl+a
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_KeyDown(object sender,KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
                e.Handled = true;
        }
        /// <summary>
        /// 修改惊尘管理员信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQueDing_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCustomerFullName.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("客户全称不能为空");
                    return;
                }
                if (Encoding.Default.GetBytes(txtCustomerFullName.Text.Trim()).Length > 100)
                {
                    MessageBox.Show("客户全称不能超过100");
                    return;
                }
                if (txtCustomerAccount.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("客户账号不能为空");
                    return;
                }
                if (Encoding.Default.GetBytes(txtCustomerAccount.Text.Trim()).Length > 50)
                {
                    MessageBox.Show("客户账号不能超过50");
                    return;
                }
                if (txtContacts.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("联系人不能为空");
                    return;
                }
                if (Encoding.Default.GetBytes(txtContacts.Text.Trim()).Length > 50)
                {
                    MessageBox.Show("联系人不能超过50");
                    return;
                }
                if (txtContactNumber.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("联系电话不能为空");
                    return;
                }
                if (Encoding.Default.GetBytes(txtContactNumber.Text.Trim()).Length > 50)
                {
                    MessageBox.Show("联系电话不能超过50");
                    return;
                }
                if (!IsTelephone(txtContactNumber.Text.Trim()))
                {
                    MessageBox.Show("你输入的不是手机/电话号码！");
                    return;
                }
                if (cmbProvince.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("省份不能为空");
                    return;
                }
                if (cmbProvince.Text.Trim() == "中国")
                {
                    MessageBox.Show("请选择省份");
                    return;
                }
                if (cmbCity.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("城市不能为空");
                    return;
                }
                if (cmbCity.Text.Trim() == "中国")
                {
                    MessageBox.Show("请选择城市");
                    return;
                }
                if (cmbCounty.Text.Trim()==string.Empty)
                {
                    MessageBox.Show("县城不能为空");
                    return;
                }
                if (cmbCounty.Text.Trim()=="中国")
                {
                    MessageBox.Show("请选择县城");
                    return;
                }
                if (txtContactAddress.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("联系地址不能为空");
                    return;
                }
                if (Encoding.Default.GetBytes(txtContactAddress.Text.Trim()).Length > 200)
                {
                    MessageBox.Show("联系地址不能超过200");
                    return;
                }
                if (Encoding.Default.GetBytes(txtRemarks.Text.Trim()).Length > 200)
                {
                    MessageBox.Show("备注不能超过200");
                    return;
                }

                Model_Customer customer = new Model_Customer();
                customer.Idk__BackingField = cus.Idk__BackingField;
                customer.FullNamek__BackingField = txtCustomerFullName.Text;
                customer.ContactPersonk__BackingField = txtContacts.Text;
                customer.ContactTelk__BackingField = txtContactNumber.Text;
                customer.ContactAddressk__BackingField = txtContactAddress.Text;
                customer.ProvinceIdk__BackingField = Convert.ToInt32(cmbProvince.SelectedValue);
                customer.ProvinceNamek__BackingField = cmbProvince.Text;
                customer.CityIdk__BackingField = Convert.ToInt32(cmbCity.SelectedValue);
                customer.CityNamek__BackingField = cmbCity.Text;
                customer.CountyIdk__BackingField = Convert.ToInt32(cmbCounty.SelectedValue);
                customer.CountyNamek__BackingField = cmbCounty.Text;
                customer.Accountk__BackingField = txtCustomerAccount.Text;
                customer.Rolek__BackingField = cus.Rolek__BackingField;
                customer.Activedk__BackingField = cus.Activedk__BackingField;
                customer.CreateAtk__BackingField = cus.CreateAtk__BackingField;
                customer.Remarkk__BackingField = txtRemarks.Text;

                //ResultModelOfModel_Customerd4FqxSXX save = cs.EditCustomer(customer);
                //ResultModelOfModel_Customerd4FqxSXX save = cs.GetCustomerCounty(customer);
                ResultModelOfModel_Customerd4FqxSXX save = cs.GetCustomerUpdateTime(customer);
                if (save.Code!=0)
                {
                    MessageBox.Show(save.Message);
                }
                else
                {
                    MessageBox.Show("操作成功");
                    getCustomerLoad();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Prompt",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            
        }

        //验证电话号码的主要代码如下：
        public bool IsTelephone(string txtPhone)
        {
            //验证电话号码 
            bool Telephone = System.Text.RegularExpressions.Regex.IsMatch(txtPhone, @"^(\d{3,4}-)?\d{6,8}$");
            bool Telephones = System.Text.RegularExpressions.Regex.IsMatch(txtPhone, @"^(\d{3,4})?\d{6,8}$");
            // 验证手机号码
            bool Handset = System.Text.RegularExpressions.Regex.IsMatch(txtPhone, @"^[1]+[3,5]+\d{9}");
            if (Telephone || Handset || Telephones)
                return true;
            else
                return false;
        }

        //使用acceptbutton属性实现回车事件，会影响分页回车事件，
        //在首次按下某个键时发生回车事件
        private void txtEnter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnQueDing_Click(null, null);
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
