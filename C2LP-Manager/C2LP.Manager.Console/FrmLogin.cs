using C2LP.Manager.Console.ConsoleServerWebReference;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace C2LP.Manager.Console
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                //引用交互接口
                ConsoleServerWebReference.ConsoleServer cs = new ConsoleServerWebReference.ConsoleServer();
                if (txtUserName.Text.Trim() == string.Empty) MessageBox.Show("用户名不能为空");
                else
                {
                    if (txtPassWord.Text.Trim() == string.Empty) MessageBox.Show("密码不能为空");
                    else
                    {
                        ResultModelOfModel_Customerd4FqxSXX aa = cs.Login(txtUserName.Text.Trim(), txtPassWord.Text.Trim());
                        if (aa.Code != 0)
                        {
                            MessageBox.Show(aa.Message);
                        }
                        else
                        {
                            txtPassWord.Text = string.Empty;
                            this.Hide();//隐藏窗体
                            MainForm query = new MainForm();
                            query.UserName = txtUserName.Text;
                            if (query.ShowDialog() == DialogResult.Cancel)
                                //this.Show();
                                this.Close();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }



        }

        
    }
}
