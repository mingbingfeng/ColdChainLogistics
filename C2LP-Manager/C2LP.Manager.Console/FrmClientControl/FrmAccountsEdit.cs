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
    public partial class FrmAccountsEdit : Form
    {
        public FrmUsersManage _ParentFrm;
        public FrmConsigneeMaintenanceID _ParentMaint;
        public FrmCooperativeClientID _ParentClient;
        public FrmAccountsEdit()
        {
            InitializeComponent();
        }
        ConsoleServerWebReference.ConsoleServer cs = new ConsoleServerWebReference.ConsoleServer();
        //定义对象接收FrmCooperativeClientID窗体传的值
        public Model_CustomerUser customeruser { set; get; }
        public Model_Customer mcust { get; set; }
        //public string UserName { set; get; }
        //public string ID { set; get; }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmAccountsEdit_Load(object sender, EventArgs e)
        {
            lbName.Text = mcust.FullNamek__BackingField + "客户下用户信息维护";
            //lbName.Text = UserName + "客户下用户信息维护";
            CumoterUserLoad();
        }
        public void CumoterUserLoad()
        {
            if (customeruser !=null )
            {
                txtDisplayName.Text = customeruser.DisplayNamek__BackingField;
                txtUserName.Text = customeruser.UserNamek__BackingField;
                txtPassWord.Text = customeruser.Passwordk__BackingField;
                if (customeruser.Activedk__BackingField == Enum_Active.Enabled)
                    rdbEnabled.Checked = true;
                else
                    rdbDisable.Checked = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtDisplayName.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("显示名称不能为空");
                    return;
                }
                if (Encoding.Default.GetBytes(txtDisplayName.Text.Trim()).Length > 100)
                {
                    MessageBox.Show("显示名称长度不能超过100");
                    return;
                }
                if (txtUserName.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("用户名称不能为空");
                    return;
                }
                if (Encoding.Default.GetBytes(txtUserName.Text.Trim()).Length > 50)
                {
                    MessageBox.Show("用户名称长度不能超过50");
                    return;
                }
                if (txtPassWord.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("用户密码不能为空");
                    return;
                }
                if (Encoding.Default.GetBytes(txtPassWord.Text.Trim()).Length > 200)
                {
                    MessageBox.Show("用户密码不能超过200");
                    return;
                }
                Model_CustomerUser user = new Model_CustomerUser();
                if (customeruser == null)
                    user.Idk__BackingField = 0;
                else
                    user.Idk__BackingField = Convert.ToInt32(customeruser.Idk__BackingField.ToString().Trim());
                user.DisplayNamek__BackingField = txtDisplayName.Text.Trim();
                user.UserNamek__BackingField = txtUserName.Text.Trim();
                user.Passwordk__BackingField = txtPassWord.Text.Trim();
                if (rdbEnabled.Checked == true)
                    user.Activedk__BackingField = Enum_Active.Enabled;
                else
                    user.Activedk__BackingField = Enum_Active.Disable;
                if (customeruser == null)
                    user.CreateAtk__BackingField = DateTime.Now;
                else
                    user.CreateAtk__BackingField = customeruser.CreateAtk__BackingField;
                //user.CustomerIdk__BackingField = Convert.ToInt32(ID.ToString().Trim());
                user.CustomerIdk__BackingField = mcust.Idk__BackingField;
                ResultModelOfModel_CustomerUserd4FqxSXX edituser = cs.EditCustomerUser(user);
                if (edituser.Code != 0)
                {
                    MessageBox.Show(edituser.Message);
                }
                else
                {
                    MessageBox.Show("操作成功");
                    if (mcust.Rolek__BackingField == Enum_Role.Administrator)
                        _ParentFrm.getRenew();
                    else if (mcust.Rolek__BackingField == Enum_Role.Sender)
                        _ParentClient.getRenew();
                    else
                        _ParentMaint.getRenew();
                     this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        
    }
}
