using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Text.RegularExpressions;

namespace C2LP.PDA.APP
{
    public partial class UCUploadConfig : UserControl
    {
        public UCUploadConfig()
        {
            InitializeComponent();
            LoadUploadConfig();
        }

        /// <summary>
        /// 加载配置
        /// </summary>
        private void LoadUploadConfig(){
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                while (Common._IsMainFormFirstShow)
                {
                    Thread.Sleep(100);
                    Application.DoEvents();
                }
                nudTime.Value = Common._UploadCycle;
                nudOrderCount.Value = Common._MaxUploadOrderCount;
                nudNodeCount.Value = Common._MaxUploadNodeCount;
                txtAddress.Text = Common._WebServiceAddress;

                nudTime.Enabled = false;
                nudOrderCount.Enabled = false;
                nudNodeCount.Enabled = false;
                txtAddress.Enabled = false;

                chbAddress.Checked = false;
                chbNodeCount.Checked = false;
                chbOrderCount.Checked = false;
                chbTime.Checked = false;
            }
            catch (Exception ex)
            {
                btnCancel_Click(null, null);
                MessageBox.Show(ex.Message, "加载参数失败", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }

        //返回
        private void btnCancel_Click(object sender, EventArgs e)
        {
            FrmParent.ParentForm.OpenForm(PageState.Main);
        }

        private void chbTime_CheckStateChanged(object sender, EventArgs e)
        {
            nudTime.Enabled = chbTime.Checked;
            if (!nudTime.Enabled)
                nudTime.Value = Common._UploadCycle;
        }

        private void chbOrderCount_CheckStateChanged(object sender, EventArgs e)
        {
            nudOrderCount.Enabled = chbOrderCount.Checked;
            if (!nudOrderCount.Enabled)
                nudOrderCount.Value = Common._MaxUploadOrderCount;
        }

        private void chbNodeCount_CheckStateChanged(object sender, EventArgs e)
        {
            nudNodeCount.Enabled = chbNodeCount.Checked;
            if (!nudNodeCount.Enabled)
                nudNodeCount.Value = Common._MaxUploadNodeCount;
        }

        private void chbAddress_CheckStateChanged(object sender, EventArgs e)
        {
            txtAddress.Enabled = chbAddress.Checked;
            if (!txtAddress.Enabled)
                txtAddress.Text = Common._WebServiceAddress;
        }

        //保存
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //服务器地址不进行验证
                //string regStr = @"\d{2,3}([.]\d{1,3}){3}:\d{2,5}";
                //if (!Regex.IsMatch(txtAddress.Text.Trim(),regStr))
                //{
                //    MessageBox.Show("服务器地址有误，请按照格式(IP:端口)[127.0.0.1:82]来填写", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                //    txtAddress.Focus();
                //    return;
                //}

                if (chbTime.Checked)
                    Common._UploadCycle = Convert.ToInt32(nudTime.Value);

                if (chbOrderCount.Checked)
                    Common._MaxUploadOrderCount = Convert.ToInt32(nudOrderCount.Value);

                if (chbNodeCount.Checked)
                    Common._MaxUploadNodeCount = Convert.ToInt32(nudNodeCount.Value);

                if (chbAddress.Checked)
                    Common._WebServiceAddress = txtAddress.Text.Trim();
                LoadUploadConfig();
                MessageBox.Show("保存成功!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "保存失败", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1); ;
            }
        }

        private void txtAddress_GotFocus(object sender, EventArgs e)
        {
            FrmParent.ParentForm.CheckInputPnl(true);
        }

        private void txtAddress_LostFocus(object sender, EventArgs e)
        {
            FrmParent.ParentForm.CheckInputPnl(false);
        }

        private void UCUploadConfig_Click(object sender, EventArgs e)
        {
            btnSave.Focus();
        }
    }
}
