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
using C2LP.PDA.Datas.BLL;

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
            FrmParent.ParentForm.ResetReturnDelay();
            if (!CheckPDAPassword("010203"))
                return;
            if (!CheckInput())
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
                txtPDANumber.Focus();
                return false;
            }
            if (txtPDANumber.Text.Trim() == _newNumber)
            {
                txtPDANumber.SelectAll();
                txtPDANumber.Focus();
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
            if (CheckPDAPassword("010203"))
            {
                DialogResult dr = MessageBox.Show("确定要退出本系统吗?", "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Yes)
                {
                    Rectangle rect = new Rectangle();
                    FullScreen.SetFullScreen(false, ref rect);
                    //UnitechDSDll.CloseCamera();
                    SyncHelp.StopSync();
                    UploadHelp.StopUpload();
                    //Common. WriteIsClose();
                    FrmParent.ParentForm.DecodeApi.aDecodeSetDecodeEnable(0);
                    FrmParent.ParentForm.DecodeApi.Close();
                    FrmParent.ParentForm.CamSdk.Close();
                    Application.Exit();
                }
            }
        }

        private void txtPDANumber_GotFocus(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
            FrmParent.ParentForm.CheckInputPnl(true);
        }

        private void txtPDANumber_LostFocus(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
            FrmParent.ParentForm.CheckInputPnl(false);
        }

        private void UCSetNumber_Click(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
            btnOK.Focus();
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
            if (CheckPDAPassword("010203"))
                FrmParent.ParentForm.OpenForm(PageState.UploadConfig);
        }

        private bool CheckPDAPassword(string pwd)
        {
            FrmParent.ParentForm.ResetReturnDelay();
            bool isRight = false;
            if (pwd.Contains(","))
                isRight = pwd.Split(',').Contains(txtPassWord.Text.Trim());
            else
                isRight = txtPassWord.Text.Trim() == pwd;
            if (isRight)
                return true;
            else
            {
                txtPassWord.SelectAll();
                txtPassWord.Focus();
                MessageBox.Show("请输入正确的PDA密码!");
                return false;
            }
        }

        private void btnShowHideTop_Click(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
            if (CheckPDAPassword("010203"))
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

        private void btnScanRecord_Click(object sender, EventArgs e)
        {
            //if (CheckPDAPassword("010203,040506"))
            //{
                //UCScanRecord._InputPWD = txtPassWord.Text.Trim();
                FrmParent.ParentForm.ResetReturnDelay();
                FrmParent.ParentForm.OpenForm(PageState.ScanRecord);
            //}
        }

        private void btnClearScanRecord_Click(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
            if (CheckPDAPassword("040506"))
            {
                try
                {
                    int sum = 0;
                    DateTime dtStart = DateTime.Now;
                    DateTime dtEnd = DateTime.Now;
                    OptRecordServer.GetOptCount(out sum, ref dtStart, ref dtEnd);
                    if (sum == 0)
                    {
                        MessageBox.Show("没有待清除的记录!");
                        return;
                    }
                    DialogResult dr = MessageBox.Show(string.Format("共{0}条操作记录:时间从{1}至{2}.", sum, dtStart.ToString("yyyy-MM-dd HH:mm:ss"), dtEnd.ToString("yyyy-MM-dd HH:mm:ss")), "确定要清除所有操作记录吗？", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                    if (dr == DialogResult.Yes)
                    {
                        OptRecordServer.ClearOptRecord();
                        MessageBox.Show("清除成功！");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void txtPassWord_TextChanged(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
        }

        private void txtPassWord_GotFocus(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
        }

        private void txtPassWord_LostFocus(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
        }

    }
}
