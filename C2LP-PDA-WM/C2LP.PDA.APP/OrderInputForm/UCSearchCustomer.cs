using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using C2LP.PDA.APP.OrderInput;
using C2LP.PDA.Datas.Model;
using System.Threading;

namespace C2LP.PDA.APP.OrderInputForm
{
    public partial class UCSearchCustomer : UserControl
    {
        public UCSearchCustomer()
        {
            InitializeComponent();
            cboCustomerType.SelectedIndex = UCCustomerInfo._isLastClickSender ? 0 : 1;
            InitRangeData(FrmParent.ParentForm._ht_Region);
        }
        private Dictionary<int, MyRegion> _Rangelist = new Dictionary<int, MyRegion>();
        private List<Customer> _list = new List<Customer>();
        private bool _isStopSearch = false;

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FrmParent.ParentForm.pnlMain.Controls[0].Visible = true;
            this.Dispose();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (btnSearch.Text == "查找")
            {
                lvList.Items.Clear();
                btnSearch.Text = "停止";
                btnOK.Enabled = false;
                btnCancel.Enabled = false;
                cboCustomerType.Enabled = false;
                Thread th = new Thread(DoSearch);
                th.IsBackground = true;
                th.Start();
            }
            else
            {
                _isStopSearch = true;
            }

        }

        public delegate void SearchDelegate();
        private void DoSearch()
        {
            if (this.InvokeRequired)
            {
                SearchDelegate idl = new SearchDelegate(Search);
                this.Invoke(idl);
            }
            else
                Search();
        }

        private void Search()
        {
            try
            {
                _isStopSearch = false;
                List<Customer> searchList = _list;
                if (txtKeyword.Text.Trim().Length > 0)
                    searchList = _list.Where(l => l.FullName.Contains(txtKeyword.Text.Trim())).ToList();
                foreach (Customer item in searchList)
                {
                    if (_isStopSearch)
                        break;
                    ListViewItem lvi = new ListViewItem(item.FullName);
                    lvi.SubItems.Add(item.CountyId == 0 ? "未绑定" : _Rangelist[item.CountyId].Name);
                    lvi.SubItems.Add(_Rangelist[item.CityId].Name);
                    lvi.SubItems.Add(_Rangelist[item.ProvinceId].Name);
                    lvi.Tag = item;
                    lvList.Items.Add(lvi);
                    Application.DoEvents();
                }
            }
            catch
            {

            }
            finally
            {
                btnSearch.Text = "查找";
                btnOK.Enabled = true;
                btnCancel.Enabled = true;
                cboCustomerType.Enabled = true;

            }

        }

        private void cboCustomerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            _list.Clear();
            lvList.Items.Clear();
            System.Collections.Hashtable ht = cboCustomerType.SelectedIndex == 0 ? FrmParent.ParentForm._ht_Sender : FrmParent.ParentForm._ht_Receiv;
            InitCustomerData(ht);
        }

        private void InitRangeData(object obj)
        {
            if (obj is System.Collections.Hashtable)
            {
                foreach (var item in (obj as System.Collections.Hashtable).Keys)
                {
                    MyRegion m = (item as MyRegion);
                    if (!_Rangelist.ContainsKey(m.Id))
                        _Rangelist.Add(m.Id, m);
                    InitRangeData((obj as System.Collections.Hashtable)[item]);
                }
            }
            //else
            //{
            //    foreach (var item in obj as List<MyRegion>)
            //    {
            //        _Rangelist.Add(item.Id,item);
            //    }
            //}
        }

        private void InitCustomerData(object obj)
        {
            if (obj is System.Collections.Hashtable)
            {
                foreach (var item in (obj as System.Collections.Hashtable).Values)
                {
                    InitCustomerData(item);
                }
            }
            else
            {
                foreach (var item in obj as List<Customer>)
                {
                    _list.Add(item);
                }
            }


        }

        private void txtKeyword_GotFocus(object sender, EventArgs e)
        {
            FrmParent.ParentForm.CheckInputPnl(true);
        }

        private void txtKeyword_LostFocus(object sender, EventArgs e)
        {
            FrmParent.ParentForm.CheckInputPnl(false);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (lvList.SelectedIndices.Count == 0)
            {
                MessageBox.Show("请在列表中选择您要选定的一项.");
                return;
            }
            Customer c = lvList.Items[lvList.SelectedIndices[0]].Tag as Customer;
            if (cboCustomerType.SelectedIndex == 0)
                FrmParent.ParentForm._UCSenderInfo.SelectCustomer(_Rangelist[c.ProvinceId], _Rangelist[c.CityId], c.CountyId == 0 ? null : _Rangelist[c.CountyId],c);
            else
                FrmParent.ParentForm._UCRecevInfo.SelectCustomer(_Rangelist[c.ProvinceId], _Rangelist[c.CityId], c.CountyId == 0 ? null : _Rangelist[c.CountyId], c);
            FrmParent.ParentForm.pnlMain.Controls[0].Visible = true;
            this.Dispose();

            //lvi.SubItems.Add(item.CountyId == 0 ? "未绑定" : _Rangelist[item.CountyId].Name);
            //lvi.SubItems.Add(_Rangelist[item.CityId].Name);
            //lvi.SubItems.Add(_Rangelist[item.ProvinceId].Name);

        }
    }
}
