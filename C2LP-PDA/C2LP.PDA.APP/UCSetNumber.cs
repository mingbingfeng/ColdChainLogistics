using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using C2LP.PDA.APP.PDAWebReference;
using System.Runtime.InteropServices;

namespace C2LP.PDA.APP
{
    public partial class UCSetNumber : UserControl
    {
        public string _newNumber = string.Empty;
        public UCSetNumber()
        {
            InitializeComponent();
            //if (string.IsNullOrEmpty(Common._PDANumber))
            //    btnExit.Visible = true;
            //else
            //    btnExit.Visible = false;
        }
        public UCSetNumber(string number)
            : this()
        {
            if (!string.IsNullOrEmpty(number))
            {
                _newNumber = number;
                txtPDANumber.Text = number;
                txtPDANumber.SelectAll();
            }
            if (FrmParent.ParentForm.lblTime.Visible)
                btnShowHideTop.Tag = "1";
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FrmParent.ParentForm.OpenForm(PageState.Main);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!CheckInput())
                return;
            if (!CheckPDAPassword())
                return;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                ResultModelOfboolean numerEixst = Common._PdaServer.SetPDANumber(txtPDANumber.Text.Trim());
                if (numerEixst.Code != 0)
                    throw new Exception(numerEixst.Message);
                if (!numerEixst.Data)
                    throw new Exception("服务器未配置此编号的设备信息!");
                Common._PDANumber = txtPDANumber.Text.Trim();
                _newNumber = Common._PDANumber;
                UCSyncAll uc = FrmParent.ParentForm.OpenForm(PageState.Sync) as UCSyncAll;
                uc.StartSync(this.Name);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "操作失败!", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                txtPDANumber.Focus();
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        /// <summary>
        /// 确认操作并检查输入
        /// </summary>
        /// <returns></returns>
        private bool CheckInput()
        {
            if (txtPDANumber.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入您将要设定的PDA编号!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                return false;
            }
            if (txtPDANumber.Text.Trim() == _newNumber)
            {
                MessageBox.Show("您没有设定新的PDA编号,请勿重复提交!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                return false;
            }
            DialogResult dr = MessageBox.Show("重新设定编号后将跳转到信息同步页面进行自动同步,是否要继续提交?", "请在网络良好的情况下执行此操作!", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button3);
            if (dr == DialogResult.No)
                return false;

            return true;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if (CheckPDAPassword())
            {
                DialogResult dr = MessageBox.Show("确定要退出本系统吗?", "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Yes)
                    Application.Exit();
            }
        }

        private void txtPDANumber_GotFocus(object sender, EventArgs e)
        {
            FrmParent.ParentForm.CheckInputPnl(true);
        }

        private void txtPDANumber_LostFocus(object sender, EventArgs e)
        {
            FrmParent.ParentForm.CheckInputPnl(false);
        }

        private void UCSetNumber_Click(object sender, EventArgs e)
        {
            btnOK.Focus();
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            if (CheckPDAPassword())
                FrmParent.ParentForm.OpenForm(PageState.UploadConfig);
        }

        private bool CheckPDAPassword()
        {
            if (txtPassWord.Text.Trim() == "010203")
                return true;
            else
            {
                MessageBox.Show("请输入正确的PDA密码!");
                return false;
            }
        }

        private void btnShowHideTop_Click(object sender, EventArgs e)
        {
            if (CheckPDAPassword())
            {
                Rectangle rect = new Rectangle();
                if (btnShowHideTop.Tag == null)
                {
                    FullScreen.SetFullScreen(true, ref rect);//隐藏顶部任务栏
                    FrmParent.ParentForm.pnlState.BackColor = Color.DeepSkyBlue;
                    FrmParent.ParentForm.lblTime.Visible = true;
                    btnShowHideTop.Tag = "1";
                }
                else
                {
                    FullScreen.SetFullScreen(false, ref rect);//显示顶部任务了
                    FrmParent.ParentForm.pnlState.BackColor = Color.Transparent;
                    FrmParent.ParentForm.lblTime.Visible = false;
                    btnShowHideTop.Tag = null;
                }
            }
        }

    }
}
