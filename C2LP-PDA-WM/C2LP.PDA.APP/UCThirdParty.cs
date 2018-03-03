using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
//using C2LP.PDA.APP.ScannerAPI;
using C2LP.PDA.Datas.Model;
using C2LP.PDA.Datas.BLL;
using System.Threading;

namespace C2LP.PDA.APP
{
    public partial class UCThirdParty : UserControl
    {
        public UCThirdParty()
        {
            InitializeComponent();
            ucConsignors1.IsScanOrder = true;
            Thread th = new Thread(DoInit);
            th.IsBackground = true;
            th.Start();
        }
        private int _SaveOkCount = 0;
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
                Cursor.Current = Cursors.WaitCursor;
                if (string.IsNullOrEmpty(Common._Destination) || string.IsNullOrEmpty(Common._StorageName))
                    throw new Exception("请先设置设备的目的地并且绑定冷藏载体!");

                //LoadScanner();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                btnCancel_Click(null, null);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                cboNumMethod.SelectedIndex = 0;
            }
        }
        #region 初始化扫描器
        //private void LoadScanner()
        //{
        //    //Scanner.GetScanner().Open();
        //    //Scanner.GetScanner().OnGetBarcodeEvent += new GetBarcodeEventHandler(UCThirdParty_OnGetBarcodeEvent);
        //}
        //void UCThirdParty_OnGetBarcodeEvent(object sender, GetBarcodeEventArgs e) 
        //{
        //    string barode = e.Barcode.Replace("\0","");
        //    barode = barode.Replace("\r\n","");
        //    barode = barode.Replace("\r","");
        //    txtOrderNumber.Text = barode;
        //}
        public void SetNumber(string number)
        {
            txtOrderNumber.Text = number;
        }
        #endregion

        #region 返回到主窗体
        private void btnCancel_Click(object sender, EventArgs e)
        {
            ucConsignors1.tmResultState.Enabled=false;
            FrmParent.ParentForm.OpenForm(PageState.Main);
        }
        #endregion

        #region 保存运单号
        private void SaveNode()
        {
            bool result = false;
            string exceptionStr = string.Empty;
            string content = string.Empty;
            string number = txtOrderNumber.Text.Trim();
            DateTime dtNow = DateTime.Now;
            try
            {
                if (ucConsignors1._SelectConSignor.ConsignorId == -1)
                {
                    try
                    {
                        ucConsignors1.AutoMatchConsignors(ref number);
                    }
                    catch (Exception ex)
                    {
                        exceptionStr = ex.Message;
                        MessageBox.Show(exceptionStr, "请选择供应商", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                        return;
                    }
                }
                FrmParent.ParentForm.CheckNumber(number, false);
                string storageName = Common._StorageName;
                //添加扫描的运单号到pda数据库
                dtNow= HuadongTmsOrderServer.AddhuadongTmsOrder(number, storageName, Common._Destination, ucConsignors1._SelectConSignor.ConsignorId, ref content);
                result = true;

                _SaveOkCount++;
                FrmParent.ParentForm.AddSaveOKNumber(_SaveOkCount);
                FrmParent.ParentForm.EndSleep();
            }
            catch (Exception ex)
            {
                exceptionStr = "保存失败";
                if (ex.Message.Contains("重复扫描"))
                    exceptionStr = ex.Message;
            }
            finally
            {
                if (result)
                    FrmParent.ParentForm.AddScanNum(number, false);
                ShowResult(result, true, exceptionStr);
                Common.SaveOptRecord(exceptionStr == string.Empty ? "保存第三方运单成功" : ("保存第三方运单失败," + exceptionStr), content, dtNow, number, ucConsignors1._SelectConSignor.ConsignorId);
                ucConsignors1.SelectedConsignors();
            }

            //if (CheckInput())
            //{
            //    try
            //    {
            //        string number = txtOrderNumber.Text.Trim();
            //        string storageName = Common._StorageName;
            //        //添加扫描的运单号到pda数据库
            //        HuadongTmsOrderServer.AddhuadongTmsOrder(number, storageName);

            //        //MessageBox.Show("添加成功，您可以继续扫描添加！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
            //        txtOrderNumber.Text = string.Empty;
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message, "创建运单失败", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            //    }
            //}
        }

        /// <summary>
        /// 显示扫描结果
        /// </summary>
        /// <param name="result">保存结果</param>
        /// <param name="isNode">节点/图片</param>
        /// <param name="exceptionStr">失败异常信息</param>
        private void ShowResult(bool result, bool isNode, string exceptionStr)
        {
            string time = DateTime.Now.ToString("HH:mm:ss");
            if (result)
                txtLog.Text += string.Format("{0}:成功扫描[{2}]运单[{1}];\r\n", time, txtOrderNumber.Text.Trim(), ucConsignors1._SelectConSignor.ConsignorName);
            //else
            //    txtLog.Text += string.Format("{0}:保存编号[{1}]失败:" + exceptionStr, time, txtOrderNumber.Text.Trim());
            //if (result)
            //    lblResult.Text = "保存第三方运单成功";
            //else
            //    lblResult.Text = exceptionStr;
            //lblResult.BackColor = result ? Color.Lime : Color.Red;

            //Thread.Sleep(1000);
            txtOrderNumber.Text = string.Empty;
            txtOrderNumber.BackColor = Color.White;
            txtLog.SelectionStart = txtLog.Text.Split(new char[] { '\r', '\n' }).Length * 15;
            txtLog.ScrollToCaret();
            //tmResultState.Enabled = true;
            ucConsignors1.ShowResult(result, exceptionStr);
        }

        /// <summary>
        /// 验证输入
        /// </summary>
        /// <returns></returns>
        private bool CheckInput()
        {
            bool flag = true;
            if (txtOrderNumber.Text == string.Empty)
            {
                txtOrderNumber.BackColor = Color.Red;
                flag = false;
            }
            //if (txtOrderNumber.Text.Length != 12)
            //{
            //    txtOrderNumber.BackColor = Color.Red;
            //    flag = false;
            //}
            return flag;
        }
        #endregion

        private void control_GotFocus(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
            FrmParent.ParentForm.CheckInputPnl(true);
            if (sender == txtOrderNumber)
                CheckPanel(true);
            Control c = sender as Control;
            c.BackColor = Color.White;
        }

        private void control_LostFocus(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
            FrmParent.ParentForm.CheckInputPnl(false);
            if (sender == txtOrderNumber)
                CheckPanel(false);
        }
        private void CheckPanel(bool show)
        {

            if (show)
                this.AutoScrollPosition = new Point(this.Width, this.Height);
        }

        private void txtOrderNumber_TextChanged(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
            if (cboNumMethod.SelectedIndex != 0)
                return;
            //if (txtOrderNumber.Text.Trim().Length != 12 || !Common.ChecNumber(txtOrderNumber.Text))
            if (txtOrderNumber.Text.Trim() == string.Empty)
                txtOrderNumber.BackColor = Color.Red;
            else
            {
                txtOrderNumber.BackColor = Color.White;

                if (cboNumMethod.SelectedIndex == 1 && MessageBox.Show("确定要提交运单号为[" + txtOrderNumber.Text.Trim() + "]的节点吗?", "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                    return;

                SaveNode();

            }
        }

        private void cboNumMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
            btnConfirm.Enabled = false;
            txtOrderNumber.Text = string.Empty;
            txtOrderNumber.Enabled = cboNumMethod.SelectedIndex == 1;
            if (cboNumMethod.SelectedIndex == 1)
            {
                //if (tcScan.SelectedIndex == 0)
                btnConfirm.Enabled = true;
                txtOrderNumber.Focus();

            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (cboNumMethod.SelectedIndex == 1)
            {
                if (txtOrderNumber.Text.Trim() == string.Empty)
                    txtOrderNumber.BackColor = Color.Red;
                else
                {
                    txtOrderNumber.BackColor = Color.White;

                    if (cboNumMethod.SelectedIndex == 1 && MessageBox.Show("确定要提交运单号为[" + txtOrderNumber.Text.Trim() + "]的第三方运单吗?", "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                        return;

                    SaveNode();

                }
            }
        }

        private void tmResultState_Tick(object sender, EventArgs e)
        {
            try
            {
                tmResultState.Enabled = false;
                lblResult.BackColor = Color.Silver;
                lblResult.Text = "扫描/录入第三方运单";
            }
            catch
            {
            }
        }

        private void cboNumMethod_GotFocus(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
        }

        private void cboNumMethod_LostFocus(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
        }

        private void txtLog_TextChanged(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
        }

        private void txtLog_GotFocus(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
        }

        private void txtLog_LostFocus(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
        }

        private void pnlTop_Click(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
        }

        private void pnlTop_LostFocus(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
        }

        private void pnlTop_GotFocus(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
        }

    }
}
