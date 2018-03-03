using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using C2LP.PDA.Datas.BLL;
using C2LP.PDA.Datas.Model;

namespace C2LP.PDA.APP.OrderInputForm
{
    public partial class UCOrderInput1 : UserControl
    {
        public UCOrderInput1()
        {
            InitializeComponent();
            try
            {
                FrmParent.ParentForm._UCSenderInfo.Parent = pnlS;
                FrmParent.ParentForm._UCRecevInfo.Parent = pnlR;
                FrmParent.ParentForm._UCSenderInfo.Dock = DockStyle.Fill;
                FrmParent.ParentForm._UCRecevInfo.Dock = DockStyle.Fill;
                FrmParent.ParentForm._UCSenderInfo.Visible = true;
                FrmParent.ParentForm._UCRecevInfo.Visible = true;
                FrmParent.ParentForm._UCSenderInfo.CheckDefaultProvince();
                FrmParent.ParentForm._UCRecevInfo.CheckDefaultProvince();
            }
            catch
            {
                MessageBox.Show("加载出错,请重启程序!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            }

        }

        private void txtOrderNumber_TextChanged(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
            lblNumLength.Text = txtOrderNumber.Text.Trim().Length.ToString() + "位";
            if (txtOrderNumber.Text.Length != 12 || !Common.ChecNumber(txtOrderNumber.Text))
                txtOrderNumber.BackColor = Color.Red;
            else
                txtOrderNumber.BackColor = Color.White;
        }

        public void SetNumber(string number)
        {
            txtOrderNumber.Text = number;
        }

        /// <summary>
        /// 验证输入
        /// </summary>
        /// <returns></returns>
        private bool CheckInput()
        {
            bool flag = true;
            if (txtOrderNumber.Text.Trim().Length != 12)
            {
                txtOrderNumber.Focus();
                txtOrderNumber.BackColor = Color.Red;
                flag = false;
            }


            return flag;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
            if (CheckInput() && FrmParent.ParentForm._UCRecevInfo.CheckInput() && FrmParent.ParentForm._UCSenderInfo.CheckInput())
            {
                string saveErr = string.Empty;
                string content = string.Empty;
                DateTime orderTime = DateTime.Now;
                try
                {
                    string number = txtOrderNumber.Text.Trim();
                    FrmParent.ParentForm.CheckNumber(number, false);
                    Customer sc = FrmParent.ParentForm._UCSenderInfo._CustomerInfo;
                    Customer rc = FrmParent.ParentForm._UCRecevInfo._CustomerInfo;
                    int sId =(int) sc.Id;// Convert.ToInt32(cboSenderCustomer.SelectedValue);
                    string sOrg = sc.FullName;// cboSenderCustomer.Text.Trim();
                    string sPerson = sc.ContactPerson;// txtSenderName.Text.Trim();
                    string sTel = sc.ContactTel;// txtSenderPhone.Text.Trim();
                    string sAddress = sc.ContactAddress;// cboSenderPId.Text + cboSenderCId.Text + txtSenderAddress.Text.Trim();
                    string rId = rc.Id == 0 ? "NULL" : rc.Id.ToString();// cboReceiverCustomer.SelectedIndex < 1 ? "NULL" : cboReceiverCustomer.SelectedValue.ToString();
                    string rOrg = rc.FullName;// cboReceiverCustomer.Text.Trim();
                    string rPerson = rc.ContactPerson;// txtReceiverName.Text.Trim();
                    string rTel = rc.ContactTel;// txtReceiverPhone.Text.Trim();
                    string rAddress = rc.ContactAddress;// cboReceiverPId.Text + cboReceiverCId.Text + txtReceiverAddress.Text.Trim();
                    int bCount = (int)nudCount.Value;
                    string storageName = Common._StorageName;
                    orderTime =WaybillServer.AddOrder(number, sId, sOrg, sPerson, sTel, sAddress, rId, rOrg, rPerson, rTel, rAddress, bCount, storageName,Common._Destination, ref content);
                    FrmParent.ParentForm.EndSleep();
                }
                catch (Exception ex)
                {
                    saveErr = ex.Message;
                    MessageBox.Show(ex.Message, "创建运单失败", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                }
                finally
                {
                    txtOrderNumber.Focus();
                    Common.SaveOptRecord(saveErr == string.Empty ? "创建运单成功" : ("创建运单失败:" + saveErr), content, orderTime, txtOrderNumber.Text.Trim(),0);
                    if (saveErr == string.Empty)
                    {
                        FrmParent.ParentForm.AddScanNum(txtOrderNumber.Text.Trim(), false);
                        MessageBox.Show("添加成功,您可以继续扫码添加!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                    }
                    txtOrderNumber.Text = string.Empty;
                }
            }
        }

        private void SaveCustomerUserControl()
        {
            FrmParent.ParentForm._UCSenderInfo.Visible = false;
            FrmParent.ParentForm._UCRecevInfo.Visible = false;
            FrmParent.ParentForm._UCRecevInfo.Parent = FrmParent.ParentForm;
            FrmParent.ParentForm._UCSenderInfo.Parent = FrmParent.ParentForm;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FrmParent.ParentForm.OpenForm(PageState.Main);
        }

        private void txtOrderNumber_LostFocus(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
        }

        private void txtOrderNumber_GotFocus(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
        }

        private void pnlS_Click(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
        }

        private void pnlR_Click(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
        }

        private void nudCount_GotFocus(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
        }

        private void nudCount_LostFocus(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
        }

        private void UCOrderInput1_Click(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
        }

        private void nudCount_KeyDown(object sender, KeyEventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
        }

        private void txtOrderNumber_KeyDown(object sender, KeyEventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
        }

    }
}
