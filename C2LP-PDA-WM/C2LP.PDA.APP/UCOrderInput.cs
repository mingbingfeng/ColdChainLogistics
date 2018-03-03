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
using System.Threading;
//using C2LP.PDA.APP.ScannerAPI;

namespace C2LP.PDA.APP
{
    public partial class UCOrderInput : UserControl
    {
        public UCOrderInput()
        {
            InitializeComponent();
            FrmParent.IputChangeEvent += new FrmParent.InputPnlChangeDelegate(FrmParent_IputChangeEvent);
            tmLoad.Interval = 1000;
            tmLoad.Enabled = true;
        }

        void FrmParent_IputChangeEvent(bool isShow)
        {
            if (!isShow && pnlInput.Visible)
                pnlInput.Visible = false;
        }
        /// <summary>
        /// 所有区域
        /// </summary>
        private List<MyRegion> _AllRegion = new List<MyRegion>();

        /// <summary>
        /// 所有客户
        /// </summary>
        private List<Customer> _AllCustomer = new List<Customer>();

        /// <summary>
        /// 所有发货客户所在城市Id
        /// </summary>
        private List<int> _SenderCityIdList = new List<int>();

        #region 初始化线程
        public delegate void InitDelegate();
        private void DoInit()
        {
            if (this.InvokeRequired)
            {
                InitDelegate idl = new InitDelegate(Init);
                this.Invoke(idl);
            }
            else
            {
                Init();
            }
        }

        private void Init()
        {
            try
            {
                Application.DoEvents();
                //Cursor.Current = Cursors.WaitCursor;
                if (string.IsNullOrEmpty(Common._Destination) || string.IsNullOrEmpty(Common._StorageName))
                    throw new Exception("请先设置设备的目的地并且绑定冷藏载体!");

                LoadCustomer();
                Application.DoEvents();
                LoadRegion();
                //LoadScanner();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                btnCancel_Click(null, null);
            }
            finally
            {
                txtOrderNumber.Focus();
                FrmParent.ParentForm.CheckInputPnl(false);
                //Cursor.Current = Cursors.Default;
                btnSubmit.Enabled = true;
                btnCancel.Enabled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FrmParent.ParentForm.OpenForm(PageState.Main);
        }
        #endregion

        #region 加载省市级联
        private void LoadRegion()
        {
            try
            {
                bool sBind = false;
                bool rBind = false;

                _AllRegion = FrmParent.ParentForm._AllRegion;// RegionServer.GetAllRegion();
                //_AllRegion = RegionServer.GetAllRegion();
                if (_AllRegion.Count == 0)
                {
                    throw new Exception("设备中没有任何区域信息,请联系后台添加后重新同步信息!");
                }
                List<MyRegion> pList = (from l in _AllRegion where l.ParentId == 1 select l).ToList();
                cboSenderPId.DisplayMember = "Name";
                cboSenderPId.ValueMember = "Id";
                //获取所有客户所在的省ID集合
                //int[] provinceIdList = _AllCustomer.Where(l => l.Role == 2).Select(l => l.ProvinceId).Distinct().ToArray();
                _SenderCityIdList = _AllCustomer.Where(l => l.Role == 2).Select(l => l.CityId).Distinct().ToList();
                //cboSenderPId.DataSource = (from l in _AllRegion where provinceIdList.Contains(l.Id) select l).ToList();
                if ((FrmParent.ParentForm._DefaultProvince.HaveSenderProvinceList.Count > 0 && FrmParent.ParentForm._DefaultProvince.HaveSenderProvinceList.First().Name == Common._DefaultProvince) || FrmParent.ParentForm._DefaultProvince.HaveSenderProvinceList.Where(l => l.Name == Common._DefaultProvince).Count() == 0)
                {
                    sBind = true;
                    cboSenderPId.SelectedIndexChanged += cboPId_SelectedIndexChanged;
                }
                cboSenderPId.DataSource = FrmParent.ParentForm._DefaultProvince.HaveSenderProvinceList;
                //else
                //{
                //    int[] provinceIdList = _AllCustomer.Where(l => l.Role == 2).Select(l => l.ProvinceId).Distinct().ToArray();
                //    cboSenderPId.DataSource = (from l in _AllRegion where provinceIdList.Contains(l.Id) select l).ToList();
                //}

                cboReceiverPId.DisplayMember = "Name";
                cboReceiverPId.ValueMember = "Id";
                //cboReceiverPId.DataSource = (from l in _AllRegion where l.ParentId == 1 select l).ToList();
                if ((FrmParent.ParentForm._DefaultProvince.ProvinceList.Count > 0 && FrmParent.ParentForm._DefaultProvince.ProvinceList.First().Name == Common._DefaultProvince) || FrmParent.ParentForm._DefaultProvince.ProvinceList.Where(l => l.Name == Common._DefaultProvince).Count() == 0)
                {
                    rBind = true;
                    cboReceiverPId.SelectedIndexChanged += cboPId_SelectedIndexChanged;
                }
                //else
                //    cboReceiverPId.DataSource = (from l in _AllRegion where l.ParentId == 1 select l).ToList();

                cboReceiverPId.DataSource = FrmParent.ParentForm._DefaultProvince.ProvinceList;
                if (!sBind)
                    cboSenderPId.SelectedIndexChanged += cboPId_SelectedIndexChanged;
                if (!rBind)
                    cboReceiverPId.SelectedIndexChanged += cboPId_SelectedIndexChanged;


                if (cboSenderPId.SelectedItem.ToString() == Common._DefaultProvince)
                {
                    if (!sBind)
                        cboPId_SelectedIndexChanged(cboSenderPId, null);
                }
                else
                    foreach (var item in cboSenderPId.Items)
                    {
                        if (item.ToString() == Common._DefaultProvince)
                        {
                            cboSenderPId.SelectedItem = item;
                            break;
                        }
                    }



                if (cboReceiverPId.SelectedItem.ToString() == Common._DefaultProvince)
                {
                    if (!rBind)
                        cboPId_SelectedIndexChanged(cboReceiverPId, null);
                }
                else
                    foreach (var item in cboReceiverPId.Items)
                    {
                        if (item.ToString() == Common._DefaultProvince)
                        {
                            cboReceiverPId.SelectedItem = item;
                            break;
                        }
                    }

            }
            catch (Exception ex)
            {
                throw new Exception("加载区域信息失败:" + ex.Message);
            }
            finally
            {
                cboSenderAId.SelectedIndexChanged += cboSenderAId_SelectedIndexChanged;
                cboReceiverAId.SelectedIndexChanged += cboSenderAId_SelectedIndexChanged;
                cboReceiverCustomer.SelectedIndexChanged += cboReceiverCustomer_SelectedIndexChanged;
                Customer c = cboReceiverCustomer.SelectedItem as Customer;
                if (c != null)
                {
                    txtReceiverAddress.Text = c.ContactAddress;
                    txtReceiverName.Text = c.ContactPerson;
                    txtReceiverPhone.Text = c.ContactTel;
                }
            }
        }

        private void cboPId_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbo = sender as ComboBox;
            ComboBox childCbo;
            int parentId = 1;
            if (cbo == cboSenderPId)
            {
                parentId = Convert.ToInt32(cboSenderPId.SelectedValue);
                childCbo = cboSenderCId;
            }
            else
            {
                parentId = Convert.ToInt32(cboReceiverPId.SelectedValue);
                childCbo = cboReceiverCId;
            }
            childCbo.Enabled = true;
            childCbo.DisplayMember = "Name";
            childCbo.ValueMember = "Id";
            List<MyRegion> list = new List<MyRegion>();
            if (cbo == cboSenderPId)
            {
                if (cbo.SelectedItem.ToString() == Common._DefaultProvince)
                {
                    list = FrmParent.ParentForm._DefaultProvince.DefaultChildRangion.Keys.ToList();
                    list = list.Where(l => _SenderCityIdList.Contains(l.Id)).ToList();
                }
                else
                    list = (from l in _AllRegion where _SenderCityIdList.Contains(l.Id) && l.ParentId == parentId select l).ToList();
                childCbo.DataSource = list;
            }
            else
            {
                if (cbo.SelectedItem.ToString() == Common._DefaultProvince)
                    list = FrmParent.ParentForm._DefaultProvince.DefaultChildRangion.Keys.ToList();
                else
                    list = (from l in _AllRegion where l.ParentId == parentId select l).ToList();
                childCbo.DataSource = list;
            }
        }

        private void cboSenderCId_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbo = sender as ComboBox;
            ComboBox childCbo;
            ComboBox parentCbo;
            int parentId = 1;

            if (sender == cboSenderCId)
            {
                parentId = Convert.ToInt32(cboSenderCId.SelectedValue);
                childCbo = cboSenderAId;
                parentCbo = cboSenderPId;
            }
            else
            {
                parentId = Convert.ToInt32(cboReceiverCId.SelectedValue);
                childCbo = cboReceiverAId;
                parentCbo = cboReceiverPId;
            }
            childCbo.Enabled = true;
            childCbo.DisplayMember = "Name";
            childCbo.ValueMember = "Id";
            List<MyRegion> list = new List<MyRegion>();
            MyRegion mr = cbo.SelectedItem as MyRegion;
            //if (parentCbo.SelectedItem.ToString() == Common._DefaultProvince && mr != null && FrmParent.ParentForm._DefaultProvince.DefaultChildRangion.ContainsKey(mr))
            //    list = FrmParent.ParentForm._DefaultProvince.DefaultChildRangion[mr];
            //else
            list = (from l in _AllRegion where l.ParentId == parentId select l).ToList();
            childCbo.DataSource = list;
            ChangeCityCustomer(cbo.Tag.ToString());
        }

        private void cboSenderAId_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cbo = sender as ComboBox;
            int parentId = 1;
            if (sender == cboSenderAId)
                parentId = Convert.ToInt32(cboSenderCId.SelectedValue);
            else
                parentId = Convert.ToInt32(cboReceiverCId.SelectedValue);
            ChangeCityCustomer(cbo.Tag.ToString());
        }
        #endregion

        #region 加载客户
        private void LoadCustomer()
        {
            try
            {
                _AllCustomer = FrmParent.ParentForm._AllCustomer;// CustomerServer.GetAllCustomer(null);
                //_AllCustomer = CustomerServer.GetAllCustomer(null);
                if (_AllCustomer.Count == 0)
                    throw new Exception("设备中没有任何客户信息,请联系后台添加后重新同步信息!");

            }
            catch (Exception ex)
            {
                throw new Exception("加载客户信息失败:" + ex.Message);
            }
        }

        private void ChangeCityCustomer(string role)
        {
            ComboBox cbo;
            List<Customer> citiCustomer = new List<Customer>();
            if (role == "2")
            {
                cbo = cboSenderCustomer;
                MyRegion mr = cboSenderCId.SelectedItem as MyRegion;
                if (cboSenderPId.SelectedItem.ToString() == Common._DefaultProvince && mr != null && FrmParent.ParentForm._DefaultProvince.RangionSenderCustomer.ContainsKey(mr))
                    citiCustomer = FrmParent.ParentForm._DefaultProvince.RangionSenderCustomer[mr];
                else
                    citiCustomer = (from l in _AllCustomer where l.Role == 2 & l.CityId == Convert.ToInt32(cboSenderCId.SelectedValue) select l).ToList();
            }
            else if (role == "22")
            {
                citiCustomer = (from l in _AllCustomer where l.Role == 2 & l.CountyId == Convert.ToInt32(cboSenderAId.SelectedValue) select l).ToList();
                cbo = cboSenderCustomer;
            }
            else if (role == "3")
            {
                cbo = cboReceiverCustomer;
                MyRegion mr = cboReceiverCId.SelectedItem as MyRegion;
                if (cboSenderPId.SelectedItem.ToString() == Common._DefaultProvince && mr != null && FrmParent.ParentForm._DefaultProvince.RangionReceiveCustomer.ContainsKey(mr))
                    citiCustomer = FrmParent.ParentForm._DefaultProvince.RangionReceiveCustomer[mr];
                else
                    citiCustomer = (from l in _AllCustomer where l.Role == 3 & l.CityId == Convert.ToInt32(cboReceiverCId.SelectedValue) select l).ToList();
            }
            else if (role == "33")
            {
                citiCustomer = (from l in _AllCustomer where l.Role == 3 & l.CountyId == Convert.ToInt32(cboReceiverAId.SelectedValue) select l).ToList();
                cbo = cboReceiverCustomer;
            }
            else
                return;
            cbo.DisplayMember = "FullName";
            cbo.ValueMember = "Id";
            if (cbo == cboSenderCustomer)
                if ((citiCustomer == null || citiCustomer.Count == 0))
                    cbo.Enabled = false;
                else
                    cbo.Enabled = true;
            else
            {
                cbo.Enabled = true;
                if (citiCustomer.Count == 0 || (citiCustomer.Count > 0 && citiCustomer.First().FullName != "手动输入"))
                    citiCustomer.Insert(0, new Customer() { Id = null, FullName = "手动输入" });
            }
            cbo.DataSource = citiCustomer;
        }

        private void cboReceiverCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboReceiverCustomer.SelectedIndex == 0)
            {
                FrmParent.ParentForm.CheckInputPnl(true);
                cboReceiverCustomer.DropDownStyle = ComboBoxStyle.DropDown;
                cboReceiverCustomer.SelectAll();
            }
            else
            {
                FrmParent.ParentForm.CheckInputPnl(false);
                cboReceiverCustomer.DropDownStyle = ComboBoxStyle.DropDownList;
                Customer c = cboReceiverCustomer.SelectedItem as Customer;
                if (c != null)
                {
                    txtReceiverAddress.Text = c.ContactAddress;
                    txtReceiverName.Text = c.ContactPerson;
                    txtReceiverPhone.Text = c.ContactTel;
                }
            }
        }

        private void cboSenderCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            Customer c = cboSenderCustomer.SelectedItem as Customer;
            if (c != null)
            {
                txtSenderAddress.Text = c.ContactAddress;
                txtSenderName.Text = c.ContactPerson;
                txtSenderPhone.Text = c.ContactTel;
            }
        }
        #endregion

        #region 输入框
        private void control_GotFocus(object sender, EventArgs e)
        {
            FrmParent.ParentForm.CheckInputPnl(true);
            if (sender == txtReceiverName || sender == txtReceiverPhone || sender == nudCount)
                CheckPanel(true);
            Control c = sender as Control;
            c.BackColor = Color.White;
        }

        private void control_LostFocus(object sender, EventArgs e)
        {
            FrmParent.ParentForm.CheckInputPnl(false);
            if (sender == txtReceiverName || sender == txtReceiverPhone || sender == nudCount)
                CheckPanel(false);
        }

        private void UCOrderInput_Click(object sender, EventArgs e)
        {
            btnSubmit.Focus();
        }

        private void CheckPanel(bool show)
        {
            pnlInput.Visible = show;
            if (show)
                this.AutoScrollPosition = new Point(this.Width, this.Height);
        }
        #endregion

        #region 初始化扫描器
        //private void LoadScanner()
        //{
        //    //Scanner.GetScanner().Open();
        //    //Scanner.GetScanner().OnGetBarcodeEvent += new GetBarcodeEventHandler(Scanner_OnGetBarcodeEvent);
        //    //MessageBox.Show("红外扫描已激活,请按键盘扫描键进行扫码!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
        //}

        //void Scanner_OnGetBarcodeEvent(object sender, GetBarcodeEventArgs e)
        //{
        //    string barcode = e.Barcode.Replace("\0", "");
        //    barcode = barcode.Replace("\r\n", "");
        //    barcode = barcode.Replace("\r", "");
        //    txtOrderNumber.Text = barcode;
        //}
        public void SetNumber(string number)
        {
            txtOrderNumber.Text = number;
        }

        private void txtOrderNumber_TextChanged(object sender, EventArgs e)
        {
            lblNumLength.Text = txtOrderNumber.Text.Trim().Length.ToString() + "位";
            if (txtOrderNumber.Text.Length != 12 || !Common.ChecNumber(txtOrderNumber.Text))
                txtOrderNumber.BackColor = Color.Red;
            else
                txtOrderNumber.BackColor = Color.White;
        }
        #endregion

        #region 保存运单信息
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
            if (txtSenderAddress.Text.Length == 0 || txtSenderAddress.Text.Length > 200)
            {
                txtSenderAddress.BackColor = Color.Red;
                flag = false;
            }
            if (txtSenderName.Text.Length == 0 || txtSenderName.Text.Length > 50)
            {
                txtSenderName.BackColor = Color.Red;
                flag = false;
            }
            if (txtSenderPhone.Text.Length == 0 || txtSenderPhone.Text.Length > 50)
            {
                txtSenderPhone.BackColor = Color.Red;
                flag = false;
            }

            if (txtReceiverAddress.Text.Length > 200)
            {
                txtReceiverAddress.BackColor = Color.Red;
                flag = false;
            }
            if ((cboReceiverCustomer.SelectedIndex <= 0 && (cboReceiverCustomer.Text == "手动输入" || cboReceiverCustomer.Text.Length == 0 || cboReceiverCustomer.Text.Length > 100)))
            {
                cboReceiverCustomer.BackColor = Color.Red;
                flag = false;
            }
            if (txtReceiverName.Text.Length > 50)
            {
                txtReceiverName.BackColor = Color.Red;
                flag = false;
            }
            if (txtReceiverPhone.Text.Length > 200)
            {
                txtReceiverPhone.BackColor = Color.Red;
                flag = false;
            }

            return flag;
        }

        /// <summary>
        /// 保存运单信息
        /// </summary>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (CheckInput())
            {
                string saveErr = string.Empty;
                string content = string.Empty;
                DateTime dtNow = DateTime.Now;
                try
                {
                    string number = txtOrderNumber.Text.Trim();
                    FrmParent.ParentForm.CheckNumber(number, false);
                    int sId = Convert.ToInt32(cboSenderCustomer.SelectedValue);
                    string sOrg = cboSenderCustomer.Text.Trim();
                    string sPerson = txtSenderName.Text.Trim();
                    string sTel = txtSenderPhone.Text.Trim();
                    string sAddress = cboSenderPId.Text + cboSenderCId.Text + txtSenderAddress.Text.Trim();
                    string rId = cboReceiverCustomer.SelectedIndex < 1 ? "NULL" : cboReceiverCustomer.SelectedValue.ToString();
                    string rOrg = cboReceiverCustomer.Text.Trim();
                    string rPerson = txtReceiverName.Text.Trim();
                    string rTel = txtReceiverPhone.Text.Trim();
                    string rAddress = cboReceiverPId.Text + cboReceiverCId.Text + txtReceiverAddress.Text.Trim();
                    int bCount = (int)nudCount.Value;
                    string storageName = Common._StorageName;
                   dtNow= WaybillServer.AddOrder(number, sId, sOrg, sPerson, sTel, sAddress, rId, rOrg, rPerson, rTel, rAddress, bCount, storageName, Common._Destination, ref content);

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
                    Common.SaveOptRecord(saveErr == string.Empty ? "创建运单成功" : ("创建运单失败:" + saveErr), content, dtNow, txtOrderNumber.Text.Trim(),0);
                    if (saveErr == string.Empty)
                    {
                        FrmParent.ParentForm.AddScanNum(txtOrderNumber.Text.Trim(), false);
                        MessageBox.Show("添加成功,您可以继续扫码添加!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                    }
                    txtOrderNumber.Text = string.Empty;
                }
            }
        }
        #endregion

        private void tmLoad_Tick(object sender, EventArgs e)
        {
            tmLoad.Enabled = false;
            Thread th = new Thread(DoInit);
            th.IsBackground = true;
            th.Start();
        }

        private void cboReceiverCustomer_GotFocus(object sender, EventArgs e)
        {

            FrmParent.ParentForm.CheckInputPnl(true);
        }

        private void cboReceiverCustomer_LostFocus(object sender, EventArgs e)
        {

            FrmParent.ParentForm.CheckInputPnl(false);
        }

    }
}
