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
using System.Text.RegularExpressions;

namespace C2LP.PDA.APP
{
    public partial class UCConsignors : UserControl
    {
        public UCConsignors()
        {
            InitializeComponent();
            lblResult.Dock = DockStyle.Fill;
            cboConsignors.Dock = DockStyle.Fill;
            lblResult.Visible = true;
            cboConsignors.Visible = false;
        }
        public Consignor _SelectConSignor;

        private bool _isScanOrder;
        public bool IsScanOrder
        {
            get { return _isScanOrder; }
            set
            {
                _isScanOrder = value;
                LoadConsignors();
            }
        }

        private void ResetReturnDelay()
        {
            FrmParent.ParentForm._WaitNum = 0;
        }

        private void LoadConsignors()
        {
            List<Consignor> list = ConsignorServer.GetAllConsignor();
            if (!IsScanOrder)
                list.Insert(0, new Consignor() { ConsignorId = 0, ConsignorName = "自运单", LinkType = 0, LinkRegex = @"\d{12}$" });
            else if (list.Count == 0)
            {
                MessageBox.Show("没有任何接入的第三方供应商,请联系管理员后台添加后再同步信息!", "无法创建第三方运单", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                //FrmParent.ParentForm.OpenForm(PageState.Main);
            }
            //list.Insert(0, new Consignor() { ConsignorId = -1, ConsignorName="请选择供应商" });
            list.Insert(0, new Consignor() { ConsignorId = -1, ConsignorName = "自动识别单号" });

            cboConsignors.ValueMember = "ConsignorId";
            cboConsignors.DisplayMember = "ConsignorName";
            cboConsignors.DataSource = list;
            lblResult.Visible = false;
            cboConsignors.Visible = true;
        }

        private void tmResultState_Tick(object sender, EventArgs e)
        {
            try
            {
                tmResultState.Enabled = false;
                lblResult.BackColor = Color.Silver;
                lblResult.Text = "扫描/录入第三方运单";
                lblResult.Visible = false;
                cboConsignors.Visible = true;
            }
            catch 
            {
                
            }
        }


        public void ShowResult(bool result, string exceptionStr)
        {
            if (result)
                lblResult.Text = "保存成功";
            else
                lblResult.Text = exceptionStr;
            lblResult.BackColor = result ? Color.Lime : Color.Red;
            tmResultState.Enabled = true;
            cboConsignors.Visible = false;
            lblResult.Visible = true;
        }

        private void cboConsignors_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetReturnDelay();
            _SelectConSignor = cboConsignors.SelectedItem as Consignor;
            if (_SelectConSignor.LinkType == 2)
                FrmParent.ParentForm._isSelectLinkType2 = true;
            else
                FrmParent.ParentForm._isSelectLinkType2 = false;
        }

        /// <summary>
        /// 还原当前选择的供应商
        /// </summary>
        public void SelectedConsignors() {
            _SelectConSignor = cboConsignors.SelectedItem as Consignor;
        }

        /// <summary>
        /// 根据正则自动匹配运单归属的供运商
        /// </summary>
        /// <param name="number"></param>
        public void AutoMatchConsignors(ref string number)
        {
            bool isMatch = false;
            foreach (Consignor item in cboConsignors.Items)
            {
                if (item.ConsignorId == -1 || string.IsNullOrEmpty(item.LinkRegex))
                    continue;
                if (Regex.IsMatch(number, item.LinkRegex))
                {
                    isMatch = true;
                    _SelectConSignor = item;
                    break;
                }
            }
            if (!isMatch)
                throw new Exception("无法识别单号供应商");
            else if (_SelectConSignor.LinkType == 2)
            {
                string tempNumber = number;
                for (int i = 0; i < number.Length; i++)
                {
                    string c = number[i].ToString();
                    if (c == "0")
                        tempNumber = tempNumber.Remove(0, 1);
                    else
                        break;
                }
                number = tempNumber;
            }

        }

        private void lblResult_TextChanged(object sender, EventArgs e)
        {
            ResetReturnDelay();
        }

        private void cboConsignors_GotFocus(object sender, EventArgs e)
        {
            ResetReturnDelay();
        }

        private void cboConsignors_LostFocus(object sender, EventArgs e)
        {
            ResetReturnDelay();
        }
    }
}
