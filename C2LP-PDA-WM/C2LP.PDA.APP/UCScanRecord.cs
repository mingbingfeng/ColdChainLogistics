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
using C2LP.PDA.Datas;
using System.Threading;
using System.Data.SQLite;

namespace C2LP.PDA.APP
{
    public partial class UCScanRecord : UserControl
    {
        public UCScanRecord()
        {
            InitializeComponent();
            Init();
        }
        //public static string _InputPWD = string.Empty;
        private void Init()
        {
            LoadConsignor();
            //chkNumber.Checked = true;
            cboType.SelectedIndex = 0;
            dtpStart.Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " 00:00");
            dtpEnd.Value = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-dd") + " 23:59");
            txtRecord.ScrollBars = ScrollBars.Both;
        }

        private void chkSearch_CheckStateChanged(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
            pnlSearch.Visible = chkSearch.Checked;
        }

        private void chkNumber_CheckStateChanged(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
            cboCustomer.Enabled = !chkNumber.Checked;
            cboType.Enabled = !chkNumber.Checked;
        }

        private Dictionary<int, string> _dicConsignor = new Dictionary<int, string>();
        private void LoadConsignor()
        {
            _dicConsignor.Clear();
            List<Consignor> list = ConsignorServer.GetAllConsignor();
            _dicConsignor.Add(0, "自运单");
            foreach (Consignor item in list)
            {
                if (!_dicConsignor.ContainsKey(item.ConsignorId))
                    _dicConsignor.Add(item.ConsignorId, item.ConsignorName);
            }
            list.Insert(0, new Consignor() { ConsignorId = 0, ConsignorName = "自运单" });
            list.Insert(0, new Consignor() { ConsignorId = -1, ConsignorName = "全部供应商" });
            cboCustomer.DisplayMember = "ConsignorName";
            cboCustomer.ValueMember = "ConsignorId";
            cboCustomer.DataSource = list;
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            _isStop = true;
            while (_searching)
            {
                Thread.Sleep(100);
                Application.DoEvents();
            }
            FrmParent.ParentForm.OpenForm(PageState.SetNumber);
        }

        private bool _searching = false;
        private void btnSearch_Click(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
            if (btnSearch.Text == "查询")
            {
                string where = string.Format(" where optNumber is not null and optCustomerId is not null and optTime >='{0}' and optTime <= '{1}' and optNumber like '%{2}%'", dtpStart.Value.ToString("yyyy-MM-dd HH:mm:ss"), dtpEnd.Value.ToString("yyyy-MM-dd HH:mm:ss"), txtNumber.Text.Trim());
                if (!chkNumber.Checked)
                {
                    if (cboType.SelectedIndex > 0)
                        where += " and optTypeId = " + cboType.SelectedIndex;
                    if (cboCustomer.SelectedIndex > 0)
                        where += " and optCustomerId = " + cboCustomer.SelectedValue;
                }
                _where = where;
                btnSearch.Text = "停止";
                Thread th = new Thread(DoSearch);
                th.IsBackground = true;
                th.Start();
            }
            else
            {
                _isStop = true;
                btnSearch.Text = "查询";
            }

        }

        string _where = string.Empty;
        private bool _isStop = false;
        private void DoSearch()
        {
            try
            {
                _searching = true;
                _isStop = false;
                SetRecordText(null,txtRecord);
                
                string sql = "select Count(*) from c2lp_optRecord " + _where;
                int sum = Convert.ToInt32(BaseServer._SqlHelp.ExecuteScalar(sql, CommandType.Text));
                if (sum == 0)
                {
                    MessageBox.Show("没有查到符合条件的相关记录!");
                    return;
                }
                SetRecordText("共查到" + sum + "条记录.", txtRecord);
                sql = sql.Replace("Count(*)", "optTime,optNumber,optType,optCustomerId") + " order by optTime desc ,optTypeId,optCustomerId";
                using (System.Data.Common.DbDataReader r = BaseServer._SqlHelp.ExecuteReader(sql, CommandType.Text))
                {
                    while (r.Read())
                    {
                        if (_isStop)
                        {
                            SetRecordText("已手动停止显示剩余记录.", txtRecord);
                            break;
                        }
                        DateTime optTime = r.GetDateTime(0);
                        string optNumber = r.GetString(1);
                        string optType = r.GetString(2);
                        int optCustomerId = r.GetInt32(3);
                        string optCustomer = "";
                        if (_dicConsignor.ContainsKey(optCustomerId))
                            optCustomer = _dicConsignor[optCustomerId];
                        SetRecordText(string.Format("{0} {1} {2} {3}", optTime.ToString("MM-dd HH:mm:ss"), optNumber, optType, optCustomer), txtRecord);
                    }
                }
            }
            catch (Exception ex)
            {
                SetRecordText(ex.Message, txtRecord);
            }
            finally
            {
                _searching = false;
                SetRecordText("查询完毕.", txtRecord);
                SetRecordText("CNM", btnSearch);
            }
        }

        private delegate void SetTextDelegate(string record, Control con);
        private void SetRecordText(string record, Control con)
        {
            if (con.InvokeRequired)
            {
                SetTextDelegate d = new SetTextDelegate(SetRecordText);
                this.Invoke(d, record,con);
                
            }
            else
            {
                if (record == null)
                {
                    con.Text = string.Empty;
                    btnClearScanRecord.Enabled = false;
                    btnReturn.Enabled = false;
                }
                else
                {
                    if (record == "CNM")
                    {
                        con.Text = "查询";
                        btnClearScanRecord.Enabled = true;
                        btnReturn.Enabled = true;
                    }
                    else
                        con.Text += record + "\r\n";
                }
            }
        }

        private void txtNumber_GotFocus(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
            FrmParent.ParentForm.CheckInputPnl(true);
            if (sender == txtNumber)
                CheckPanel(true);
            Control c = sender as Control;
            c.BackColor = Color.White;
        }
        private void CheckPanel(bool show)
        {
            if (show)
                this.AutoScrollPosition = new Point(this.Width, this.Height);
        }

        private void txtNumber_LostFocus(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
            FrmParent.ParentForm.CheckInputPnl(false);
            if (sender == txtNumber)
                CheckPanel(false);
        }

        private void DeleteScanRecord() {
            try
            {
                FrmParent.ParentForm.ResetReturnDelay();
                //int sum = 0;
                string dtStart = dtpStart.Value.ToString("yyyy-MM-dd HH:mm:ss");
                string dtEnd = dtpEnd.Value.ToString("yyyy-MM-dd HH:mm:ss");
                //OptRecordServer.GetOptCount(out sum, ref dtStart, ref dtEnd);
                //if (sum == 0)
                //{
                //    MessageBox.Show("没有待清除的记录!");
                //    return;
                //}
                if (string.IsNullOrEmpty(dtStart) && string.IsNullOrEmpty(dtEnd))
                {
                    return;
                }
                DialogResult dr = MessageBox.Show(string.Format("确定要删除{0}到{1}的信息？", dtStart, dtEnd), "确定要清除操作记录吗？", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (dr == DialogResult.Yes)
                {
                    OptRecordServer.ClearOptRecordTime(dtStart, dtEnd);
                    MessageBox.Show("清除成功！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClearScanRecord_Click(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ShowTempInput("请输入密码：",string.Empty,true);
            FrmParent.ParentForm.TempInputEvent += new EventHandler(ParentForm_TempInputEvent_Name);
            this.Parent.Enabled = false;
        }

        void ParentForm_TempInputEvent_Name(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
            string pwd = sender.ToString();
            FrmParent.ParentForm.TempInputEvent -= ParentForm_TempInputEvent_Name;
            this.Parent.Enabled = true;

            if (pwd != "040506")
            {
                MessageBox.Show("您输入的密码无效,无法删除!");
                return;
            }
            DeleteScanRecord();
        }

        private void dtpStart_GotFocus(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
        }

        private void dtpStart_LostFocus(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
        }

        private void dtpStart_ValueChanged(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
        }

        private void dtpEnd_GotFocus(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
        }

        private void dtpEnd_LostFocus(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
        }

        private void dtpEnd_ValueChanged(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
        }

        private void cboType_SelectedIndexChanged(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
        }

        private void cboCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
        }

        private void txtNumber_TextChanged(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
        }

        private void txtRecord_GotFocus(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
        }

        private void txtRecord_LostFocus(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
        }

        private void txtRecord_TextChanged(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
        }

        private void cboCustomer_GotFocus(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
        }

        private void cboType_LostFocus(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
        }

        private void label2_ParentChanged(object sender, EventArgs e)
        {

        }

        private void label1_ParentChanged(object sender, EventArgs e)
        {

        }
    }
}
