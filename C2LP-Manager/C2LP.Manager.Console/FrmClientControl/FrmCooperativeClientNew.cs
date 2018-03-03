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
    public partial class FrmCooperativeClientNew : Form
    {
        ConsoleServerWebReference.ConsoleServer cs = new ConsoleServerWebReference.ConsoleServer();
        public FrmCooperativeClientList _ParentClientList;
        public Model_Customer cust;
        public FrmCooperativeClientNew()
        {
            InitializeComponent();
        }

        private void FrmCooperativeClientNew_Load(object sender, EventArgs e)
        {
            getProvince();
            getCityLevel();
            GetCounty();
            cmbProvince.SelectedIndexChanged += cmbProvince_SelectedIndexChanged;
            cmbCity.SelectedIndexChanged += cmbCity_SelectedIndexChanged;
            getCustomer();
        }
        public void getCustomer()
        {
            if (cust != null)
            {
                txtUserName.Text = cust.FullNamek__BackingField;
                txtNumber.Text = cust.Accountk__BackingField;
                txtContact.Text = cust.ContactPersonk__BackingField;
                txtPhone.Text = cust.ContactTelk__BackingField;
                cmbProvince.Text = cust.ProvinceNamek__BackingField;
                cmbCity.Text = cust.CityNamek__BackingField;
                cmbCounty.Text = cust.CountyNamek__BackingField;
                txtAddress.Text = cust.ContactAddressk__BackingField;
                //txtRemarks.Text = cust.Remarkk__BackingField;
                if (cust.Activedk__BackingField == 0)
                    rdbEnabled.Checked = true;
                else
                    rdbDisable.Checked = true;
                string[] remarkArr = cust.Remarkk__BackingField.Split('|');
                txtRemarks.Text = remarkArr[0];
                txtBindReceiverOrg.Text = remarkArr[1];
            }
        }
        #region 省份
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
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
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

        private void cmbProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
            getCityLevel();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUserName.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("客户名称不能为空");
                    return;
                }
                if (Encoding.Default.GetBytes(txtUserName.Text.Trim()).Length > 100)
                {
                    MessageBox.Show("客户名称的长度不能超过100");
                    return;
                }
                if (txtNumber.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("客户账号不能为空");
                    return;
                }
                if (Encoding.Default.GetBytes(txtNumber.Text.Trim()).Length > 50)
                {
                    MessageBox.Show("客户账号的长度不能超过50");
                    return;
                }
                if (txtContact.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("联系人不能为空");
                    return;
                }
                if (Encoding.Default.GetBytes(txtContact.Text.Trim()).Length > 50)
                {
                    MessageBox.Show("联系人的名字不能超过50");
                    return;
                }
                if (txtPhone.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("联系电话不能为空");
                    return;
                }
                
                if (Encoding.Default.GetBytes(txtPhone.Text.Trim()).Length > 50)
                {
                    MessageBox.Show("联系电话号码的长度不能超过50");
                    return;
                }
                if (!IsTelephone(txtPhone.Text.Trim()))
                {
                    MessageBox.Show("你输入不是手机/电话号码！");
                    return;
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
                if (cmbCounty.Text.Trim() ==string.Empty)
                {
                    MessageBox.Show("县城不能为空");
                    return;
                }
                if (cmbCounty.Text.Trim()=="中国")
                {
                    MessageBox.Show("请选择县城");
                    return;
                }
                if (txtAddress.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("联系地址不能为空");
                    return;
                }
                if (Encoding.Default.GetBytes(txtAddress.Text.Trim()).Length > 200)
                {
                    MessageBox.Show("联系地址的长度不能超过200");
                    return;
                }
                if (Encoding.Default.GetBytes(txtRemarks.Text.Trim()).Length > 200)
                {
                    MessageBox.Show("备注的长度不能超过200");
                    return;
                }
                Model_Customer customer = new Model_Customer();
                
                if (cust!=null)
                    customer.Idk__BackingField = cust.Idk__BackingField;
                else
                    customer.Idk__BackingField = 0;
                customer.FullNamek__BackingField = txtUserName.Text.Trim();
                customer.Accountk__BackingField = txtNumber.Text.Trim();
                customer.ContactPersonk__BackingField = txtContact.Text.Trim();
                customer.ContactTelk__BackingField = txtPhone.Text.Trim();
                customer.ProvinceIdk__BackingField = Convert.ToInt32(cmbProvince.SelectedValue);
                customer.ProvinceNamek__BackingField = cmbProvince.Text;
                customer.CityIdk__BackingField = Convert.ToInt32(cmbCity.SelectedValue);
                customer.CityNamek__BackingField = cmbCity.Text;
                customer.CountyIdk__BackingField =Convert.ToInt32(cmbCounty.SelectedValue);
                customer.CountyNamek__BackingField = cmbCounty.Text;
                customer.ContactAddressk__BackingField = txtAddress.Text.Trim();
                customer.Remarkk__BackingField = txtRemarks.Text.Trim();
                if (rdbEnabled.Checked == true)
                    customer.Activedk__BackingField = Enum_Active.Enabled;
                else
                    customer.Activedk__BackingField = Enum_Active.Disable;
                //默认为发货单位
                customer.Rolek__BackingField = Enum_Role.Sender;
                if (cust==null)
                    customer.CreateAtk__BackingField = DateTime.Now;
                //ResultModelOfModel_Customerd4FqxSXX save = cs.EditCustomer(customer);
                //ResultModelOfModel_Customerd4FqxSXX save = cs.GetCustomerCounty(customer);
                //ResultModelOfModel_Customerd4FqxSXX save=cs.GetCustomerUpdateTime(customer);
                string bindReceiverOrg = null;
                if (txtBindReceiverOrg.Text.Trim().Length > 0)
                    bindReceiverOrg = txtBindReceiverOrg.Text.Trim();
                ResultModelOfboolean save = cs.UpdateCustomer(customer, bindReceiverOrg);
                if (save.Code != 0)
                {
                    MessageBox.Show(save.Message);
                }
                else
                {
                    MessageBox.Show("操作成功");
                    _ParentClientList.getRenew();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private void btnUndo_Click(object sender, EventArgs e)
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

        private void cmbCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCounty();
        }
    }
}
