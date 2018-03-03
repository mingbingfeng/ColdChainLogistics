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

        public void Load()
        {
            if (_isSender)
                _UseHashTable = FrmParent.ParentForm._ht_Sender;
            else
            {
                lblRegion.Text = "收货地：";
                lblCustomer.Text = "收货单位：";
                lblPerson.Text = "收货人：";
                lblPhone.Text = "收货电话：";
                chkInput.Visible = true;
                if (!chkInput.Checked )
                    _UseHashTable = FrmParent.ParentForm._ht_Receiv;
                else
                    _UseHashTable = FrmParent.ParentForm._ht_Region;
            }
            ICollection keys = _UseHashTable.Keys;
            MyRegion[] regions = new MyRegion[keys.Count];
            keys.CopyTo(regions, 0);
            cboPId.DisplayMember = "Name";
            cboPId.ValueMember = "Id";
            cboPId.DataSource = regions;
        }

        void chkInput_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkInput.Checked)
            {
                cboCustomer.DropDownStyle = ComboBoxStyle.DropDown;
                cboCustomer.Text = "请手动输入单位信息...";
                txtPhone.Text = string.Empty;
                txtName.Text = string.Empty;
                txtAddress.Text = string.Empty;
            }
            else 
                cboCustomer.DropDownStyle = ComboBoxStyle.DropDownList;
            
            Load();
        }

        void cboAId_SelectedIndexChanged(object sender, EventArgs e)
        {
            _SelectARegion = cboAId.SelectedItem as MyRegion;
            cboCustomer.DisplayMember = "FullName";
            cboCustomer.ValueMember = "Id";
            cboCustomer.DataSource = ((_UseHashTable[_SelectPRegion] as Hashtable)[_SelectCRegion] as Hashtable)[_SelectARegion];
        }

        void cboCId_SelectedIndexChanged(object sender, EventArgs e)
        {
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
            Customer selectCustomer = cboCustomer.SelectedItem as Customer;
            if (selectCustomer != null) {
                txtAddress.Text = selectCustomer.ContactAddress;
                txtName.Text = selectCustomer.ContactPerson;
                txtPhone.Text = selectCustomer.ContactTel;
            }
        }
    }
}
