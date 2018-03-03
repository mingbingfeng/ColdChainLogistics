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
    public partial class FrmConsigneeMaintenanceNew : Form
    {
        ConsoleServerWebReference.ConsoleServer cs = new ConsoleServerWebReference.ConsoleServer();
        public Model_Customer Customers { set; get; }
        public FrmConsigneeMaintenance _ParentMaintenace;
        public FrmConsigneeMaintenanceNew()
        {
            InitializeComponent();
        }

        private void FrmConsigneeMaintenanceNew_Load(object sender, EventArgs e)
        {
            Province();
            City();
            County();
            cmbProvince.SelectedIndexChanged += cmbProvince_SelectedIndexChanged;
            cmbCity.SelectedIndexChanged += cmbCity_SelectedIndexChanged;
            CustomersLoad();
        }
        public void CustomersLoad()
        {
            if (Customers!=null)
            {
                txtUserName.Text = Customers.FullNamek__BackingField;
                txtAccount.Text = Customers.Accountk__BackingField;
                txtAddress.Text = Customers.ContactAddressk__BackingField;
                txtConsignee.Text = Customers.ContactPersonk__BackingField;
                txtPhone.Text = Customers.ContactTelk__BackingField;
                //txtRemarks.Text = Customers.Remarkk__BackingField;
                cmbProvince.Text = Customers.ProvinceNamek__BackingField;
                cmbCity.Text = Customers.CityNamek__BackingField;
                cmbCounty.Text = Customers.CountyNamek__BackingField;
                if (Customers.Activedk__BackingField == Enum_Active.Enabled)
                    rdbEnable.Checked = true;
                else
                    rdbDisable.Checked = true;
                string[] remarkArr = Customers.Remarkk__BackingField.Split('|');
                txtRemarks.Text = remarkArr[0];
                txtBindReceiverOrg.Text = remarkArr[1];
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
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void cmbCity_SelectedIndexChanged(object sender, EventArgs e)
        {
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
        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUserName.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("收货单位全称不能为空");
                    return;
                }
                if (Encoding.Default.GetBytes(txtUserName.Text.Trim()).Length>100)
                {
                    MessageBox.Show("收货单位全称不能超过100");
                    return;
                }
                if (txtAccount.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("收货单位账号不能为空");
                    return;
                }
                if (Encoding.Default.GetBytes(txtAccount.Text.Trim()).Length>50)
                {
                    MessageBox.Show("收货单位账号不能超过50");
                    return;
                }
                //if (txtConsignee.Text.Trim() == string.Empty)
                //{
                //    MessageBox.Show("收货人不能为空");
                //    return;
                //}
                if (Encoding.Default.GetBytes(txtConsignee.Text.Trim()).Length>50)
                {
                    MessageBox.Show("收货人不能超过50");
                    return;
                }
                //if (txtPhone.Text.Trim() == string.Empty)
                //{
                //    MessageBox.Show("收货人电话不能为空");
                //    return;
                //}
                
                if (Encoding.Default.GetBytes(txtPhone.Text.Trim()).Length>50)
                {
                    MessageBox.Show("收货人电话不能超过50");
                    return;
                }
                if (txtPhone.Text.Trim() != string.Empty)
                {
                    if (!IsTelephone(txtPhone.Text.Trim()))
                    {
                        MessageBox.Show("你输入不是手机/电话号码！");
                        return;
                    }
                }
                if (cmbProvince.Text.Trim()==string.Empty)
                {
                    MessageBox.Show("省份不能为空");
                    return;
                }
                if (cmbProvince.Text.Trim() == "中国")
                {
                    MessageBox.Show("请选择省份");
                    return;
                }
                if (cmbCity.Text.Trim()==string.Empty)
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
                //if (txtAddress.Text.Trim() == string.Empty)
                //{
                //    MessageBox.Show("联系地址不能为空");
                //    return;
                //}
                if (Encoding.Default.GetBytes(txtAddress.Text.Trim()).Length>200)
                {
                    MessageBox.Show("联系地址不能超过200");
                    return;
                }
                if (Encoding.Default.GetBytes(txtRemarks.Text.Trim()).Length>200)
                {
                    MessageBox.Show("备注不能超过200");
                    return;
                }
                Model_Customer customer = new Model_Customer();
                if (Customers == null)
                    customer.Idk__BackingField = 0;
                else
                    customer.Idk__BackingField = Customers.Idk__BackingField;
                customer.FullNamek__BackingField = txtUserName.Text.Trim();
                customer.Accountk__BackingField = txtAccount.Text.Trim();
                customer.ContactPersonk__BackingField = txtConsignee.Text.Trim();
                customer.ContactTelk__BackingField = txtPhone.Text.Trim();
                customer.ProvinceIdk__BackingField = Convert.ToInt32(cmbProvince.SelectedValue.ToString());
                customer.ProvinceNamek__BackingField = cmbProvince.Text;
                customer.CityIdk__BackingField = Convert.ToInt32(cmbCity.SelectedValue.ToString());
                customer.CityNamek__BackingField = cmbCity.Text;
                customer.CountyIdk__BackingField = Convert.ToInt32(cmbCounty.SelectedValue.ToString());
                customer.CountyNamek__BackingField = cmbCounty.Text;
                customer.ContactAddressk__BackingField = txtAddress.Text.Trim();
                customer.Remarkk__BackingField = txtRemarks.Text.Trim();
                if (rdbEnable.Checked == true)
                    customer.Activedk__BackingField = Enum_Active.Enabled;
                else
                    customer.Activedk__BackingField = Enum_Active.Disable;
                //默认下游收货单位
                customer.Rolek__BackingField = Enum_Role.Receiver;
                if (Customers == null)
                    customer.CreateAtk__BackingField = DateTime.Now;

                //ResultModelOfModel_Customerd4FqxSXX cust = cs.EditCustomer(customer);
                //ResultModelOfModel_Customerd4FqxSXX cust = cs.GetCustomerCounty(customer);
                //ResultModelOfModel_Customerd4FqxSXX cust = cs.GetCustomerUpdateTime(customer);
                string bindReceiverOrg = null;
                if (txtBindReceiverOrg.Text.Trim().Length > 0)
                    bindReceiverOrg = txtBindReceiverOrg.Text.Trim();
                ResultModelOfboolean cust = cs.UpdateCustomer(customer, bindReceiverOrg);
                if (cust.Code != 0)
                {
                    MessageBox.Show(cust.Message);
                }
                else
                {
                    MessageBox.Show("操作成功");
                    _ParentMaintenace.PC();
                    
                        this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
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

        
    }
}
