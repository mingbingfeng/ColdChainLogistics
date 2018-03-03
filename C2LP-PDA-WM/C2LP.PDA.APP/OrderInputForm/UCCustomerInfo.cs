using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using C2LP.PDA.Datas.Model;
using System.Collections;
using C2LP.PDA.APP.OrderInputForm;

namespace C2LP.PDA.APP.OrderInput
{
    public partial class UCCustomerInfo : UserControl
    {
        public UCCustomerInfo()
        {
            InitializeComponent();
            cboPId.SelectedIndexChanged += new EventHandler(cboPId_SelectedIndexChanged);
            cboCId.SelectedIndexChanged += new EventHandler(cboCId_SelectedIndexChanged);
            cboAId.SelectedIndexChanged += new EventHandler(cboAId_SelectedIndexChanged);
            chkInput.CheckStateChanged += new EventHandler(chkInput_CheckStateChanged);
            cboCustomer.SelectedIndexChanged += new EventHandler(cboCustomer_SelectedIndexChanged);
        }
        public static bool _isLastClickSender = false;

        public UCCustomerInfo(bool isSender)
            : this()
        {
            IsSender = isSender;
        }

        private bool _isSender;
        public bool IsSender
        {
            get { return _isSender; }
            set
            {
                _isSender = value;
            }
        }

        private MyRegion _SelectPRegion;
        private MyRegion _SelectCRegion;
        private MyRegion _SelectARegion;
        private Hashtable _UseHashTable;
        public Customer _CustomerInfo;

        public void Load()
        {
            txtName.GotFocus -= new EventHandler(control_GotFocus);
            txtName.LostFocus -= new EventHandler(control_LostFocus);
            txtAddress.GotFocus -= new EventHandler(control_GotFocus);
            txtAddress.LostFocus -= new EventHandler(control_LostFocus);

            txtName.GotFocus += new EventHandler(control_GotFocus);
            txtName.LostFocus += new EventHandler(control_LostFocus);
            txtAddress.GotFocus += new EventHandler(control_GotFocus);
            txtAddress.LostFocus += new EventHandler(control_LostFocus);


            cboCustomer.GotFocus -= new EventHandler(control_GotFocus);
            cboCustomer.LostFocus -= new EventHandler(control_LostFocus);
            if (_isSender)
            {
                _UseHashTable = FrmParent.ParentForm._ht_Sender;
                //txtAddress.Width = 231;
            }
            else
            {
                //txtAddress.Width = 231 - 90;
                lblRegion.Text = "收货地：";
                lblCustomer.Text = "收货单位：";
                lblPerson.Text = "收货人：";
                lblPhone.Text = "收货电话：";
                chkInput.Visible = true;
                if (!chkInput.Checked)
                    _UseHashTable = FrmParent.ParentForm._ht_Receiv;
                else
                {
                    _UseHashTable = FrmParent.ParentForm._ht_Region;
                    cboCustomer.GotFocus += new EventHandler(control_GotFocus);
                    cboCustomer.LostFocus += new EventHandler(control_LostFocus);
                }
            }

            ICollection keys = _UseHashTable.Keys;
            MyRegion[] regions = new MyRegion[keys.Count];
            keys.CopyTo(regions, 0);
            cboPId.DisplayMember = "Name";
            cboPId.ValueMember = "Id";
            cboPId.DataSource = regions;
           
        }

        public void CheckDefaultProvince() {
            foreach (var item in cboPId.Items)
            {
                if (item.ToString() == Common._DefaultProvince)
                {
                    cboPId.SelectedItem = item;
                    break;
                }
            }
        }

        void chkInput_CheckStateChanged(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
            if (chkInput.Checked)
            {
                chkInput.Text = "自动/手动";
                cboCustomer.DropDownStyle = ComboBoxStyle.DropDown;
                cboCustomer.Text = "请手动输入单位信息...";
                txtPhone.Text = string.Empty;
                txtName.Text = string.Empty;
                txtAddress.Text = string.Empty;
            }
            else
            {
                cboCustomer.DropDownStyle = ComboBoxStyle.DropDownList;
                chkInput.Text = "手动/自动";
            }
            Load();
        }

        void cboAId_SelectedIndexChanged(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
            _SelectARegion = cboAId.SelectedItem as MyRegion;
            cboCustomer.DisplayMember = "FullName";
            cboCustomer.ValueMember = "Id";
            cboCustomer.DataSource = ((_UseHashTable[_SelectPRegion] as Hashtable)[_SelectCRegion] as Hashtable)[_SelectARegion];
        }

        void cboCId_SelectedIndexChanged(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
            _SelectCRegion = cboCId.SelectedItem as MyRegion;
            ICollection keys = ((_UseHashTable[_SelectPRegion] as Hashtable)[_SelectCRegion] as Hashtable).Keys;
            MyRegion[] regions = new MyRegion[keys.Count];
            keys.CopyTo(regions, 0);
            cboAId.DisplayMember = "Name";
            cboAId.ValueMember = "Id";
            cboAId.DataSource = regions;
        }

        void cboPId_SelectedIndexChanged(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
            _SelectPRegion = cboPId.SelectedItem as MyRegion;
            ICollection keys = (_UseHashTable[_SelectPRegion] as Hashtable).Keys;
            MyRegion[] regions = new MyRegion[keys.Count];
            keys.CopyTo(regions, 0);
            cboCId.DisplayMember = "Name";
            cboCId.ValueMember = "Id";
            cboCId.DataSource = regions;
        }

        void cboCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
            Customer selectCustomer = cboCustomer.SelectedItem as Customer;
            _CustomerInfo = new Customer();
            _CustomerInfo.Id = selectCustomer.Id;
            _CustomerInfo.FullName = selectCustomer.FullName;
            _CustomerInfo.ContactAddress = selectCustomer.ContactAddress;
            _CustomerInfo.ContactPerson = selectCustomer.ContactPerson;
            _CustomerInfo.ContactTel = selectCustomer.ContactTel;
            if (_CustomerInfo != null)
            {
                txtAddress.Text = _CustomerInfo.ContactAddress;
                txtName.Text = _CustomerInfo.ContactPerson;
                txtPhone.Text = _CustomerInfo.ContactTel;
            }
        }


        public bool CheckInput()
        {
            bool flag = true;
            if (txtAddress.Text.Length == 0 || txtAddress.Text.Length > 200)
            {
                txtAddress.BackColor = Color.Red;
                flag = false;
            }
            if (txtName.Text.Length == 0 || txtName.Text.Length > 50)
            {
                txtName.BackColor = Color.Red;
                flag = false;
            }
            if (txtPhone.Text.Length == 0 || txtPhone.Text.Length > 50)
            {
                txtPhone.BackColor = Color.Red;
                flag = false;
            }

            if (cboCustomer.Text == "请手动输入单位信息..." || cboCustomer.Text.Length == 0 || cboCustomer.Text.Length > 100)
            {
                cboCustomer.BackColor = Color.Red;
                flag = false;
            }
            if (flag && chkInput.Checked)
            {
                _CustomerInfo = new Customer();
                _CustomerInfo.Id = 0;
                _CustomerInfo.FullName = cboCustomer.Text.Trim();
                //_CustomerInfo.ContactAddress = txtAddress.Text.Trim();
                _CustomerInfo.ContactPerson = txtName.Text.Trim();
                _CustomerInfo.ContactTel = txtPhone.Text.Trim();
            }
            _CustomerInfo.ContactAddress = cboPId.Text + cboCId.Text + txtAddress.Text.Trim();
            return flag;
        }

        void ParentForm_TempInputEvent_Name(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
            txtName.Text = sender.ToString();
            FrmParent.ParentForm.TempInputEvent -= ParentForm_TempInputEvent_Name;
            this.Parent.Parent.Enabled = true;
        }



        private void control_GotFocus(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
            Control c = sender as Control;
            if (_isSender)
                FrmParent.ParentForm.CheckInputPnl(true);
            else
            {
                if (c == txtName)
                {
                    FrmParent.ParentForm.ShowTempInput(c.Tag.ToString(), c.Text.Trim(),false);
                    if (c == txtName)
                    {
                        FrmParent.ParentForm.TempInputEvent += new EventHandler(ParentForm_TempInputEvent_Name);
                        this.Parent.Parent.Enabled = false;
                        //this.Parent.Parent.GotFocus += new EventHandler(UCCustomerInfo_GotFocus);
                    }
                }
                else
                    FrmParent.ParentForm.CheckInputPnl(true);
            }
            //if (sender == txtName || sender == txtPhone || sender == nudCount)
            //    CheckPanel(true);
            c.BackColor = Color.White;
        }

        private void control_LostFocus(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
            Control c = sender as Control;
            if (_isSender || (!_isSender && (c != txtName)))
                FrmParent.ParentForm.CheckInputPnl(false);
            //if (sender == txtReceiverName || sender == txtReceiverPhone || sender == nudCount)
            //    CheckPanel(false);
        }

        private void lblCustomer_Click(object sender, EventArgs e)
        {
            _isLastClickSender = IsSender;
            FrmParent.ParentForm.OpenForm(PageState.SearchCustomer);
        }

        public void SelectCustomer(MyRegion r1,MyRegion r2,MyRegion r3,Customer c) {
            cboPId.SelectedItem = r1;
            cboCId.SelectedItem = r2;
            if(r3!=null)
            cboAId.SelectedItem = r3;
            else
                foreach (var item in cboAId.Items)
                {
                    if (item.ToString() == "未绑定单位")
                    {
                        cboAId.SelectedItem = item;
                    }
                }
            cboCustomer.SelectedItem = c;
        }
    }
}
