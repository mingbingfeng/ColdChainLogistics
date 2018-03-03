using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using C2LP.PDA.Datas.Model;
using System.Threading;
using C2LP.PDA.Datas.BLL;
//using C2LP.PDA.APP.ScannerAPI;
using System.Reflection;
using System.IO;

namespace C2LP.PDA.APP
{
    public partial class UCCenterNodeScan : UserControl
    {
        public UCCenterNodeScan()
        {
            InitializeComponent();
            ucConsignors1.IsScanOrder = false;
            //线程执行初始化
            Thread th = new Thread(DoInit);
            th.IsBackground = true;
            th.Start();
        }
        private int _SaveOkCount = 0;

        /// <summary>
        /// 冷藏载体信息
        /// </summary>
        private ColdStorage _StorageInfo;
        /// <summary>
        /// 相机初始化结果
        /// </summary>
        public static bool _IsCameraActive;
        /// <summary>
        /// 当前添加的图片数量
        /// </summary>
        public int _PicCount = 1;

        #region 初始化
        #region 线程执行初始化
        private void DoInit()
        {
            if (this.InvokeRequired)
            {
                C2LP.PDA.APP.UCOrderInput.InitDelegate idl = new UCOrderInput.InitDelegate(Init);
                this.Invoke(idl);
            }
            else
                Init();
        }

        private void Init()
        {
            try
            {
                Application.DoEvents();
                Cursor.Current = Cursors.WaitCursor;

                if (string.IsNullOrEmpty(Common._Destination) || string.IsNullOrEmpty(Common._StorageName))
                    throw new Exception("请先设置设备的目的地并且绑定冷藏载体!");
                LoadStorageInfo();
                LoadNodeInfo();
                LoadStorageScanInfo();
                //chbIsArrive.Checked = true;
                //chbIsArrive.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                btnCancel_Click(null, null);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
            }
        }
        #endregion

        #region 加载界面信息
        /// <summary>
        /// 加载载体信息
        /// </summary>
        private void LoadStorageInfo()
        {
            try
            {
                List<ColdStorage> list = StorageServer.GetStorageList(Common._StorageName);
                if (list.Count() == 0)
                    throw new Exception("本地数据不存在该冷藏载体!");
                _StorageInfo = list[0];
            }
            catch (Exception ex)
            {
                throw new Exception("加载冷藏载体信息失败:" + ex.Message);
            }
        }

        /// <summary>
        /// 加载运单载体信息
        /// </summary>
        private void LoadStorageScanInfo()
        {
            try
            {
                if (FrmParent.ParentForm._storage_Scan.Keys.Count == 0)
                    throw new Exception("本地数据不存在任何冷藏载体[扫描],请重新同步信息!");
                cboStorageScan.Items.Clear();
                cboStorageScan.DisplayMember = "storageName";
                cboStorageScan.ValueMember = "Id";
                List<ColdStorage> list = new List<ColdStorage>();
                list.Add(new ColdStorage() { Id = 0, storageName = "扫描前请选择", storageType=-1 });
                foreach (ColdStorage item in FrmParent.ParentForm._storage_Scan.Values)
                {
                    list.Add(item);
                }
                list= list.OrderBy(l => l.storageType).ToList();
                cboStorageScan.DataSource = list;
            }
            catch (Exception ex)
            {
                throw new Exception("加载冷藏载体[扫描]信息失败:" + ex.Message);
            }
        }

        /// <summary>
        /// 显示界面信息
        /// </summary>
        private void LoadNodeInfo()
        {
            try
            {
                lblStorage.Text = Common._StorageName + " [" + (_StorageInfo.storageType == 2 ? "车载" : "冷库") + "]";
                lblDestin.Text = Common._Destination;
                //chbIsArrive.Visible = _StorageInfo.storageType == 2;
                string content = string.Empty;
                if (_StorageInfo.storageType == 1)
                    content = string.Format("【{0}】 中转入库{1}{2}", Common._StorageName, string.IsNullOrEmpty(_StorageInfo.driver) ? "" : " 联系人【" + _StorageInfo.driver + "】", string.IsNullOrEmpty(_StorageInfo.driverTel) ? "" : " 联系电话【" + _StorageInfo.driverTel + "】");
                else
                    content = string.Format("【{0}】 配送员{1}已出发 准备运往【{2}】{3}", Common._StorageName, string.IsNullOrEmpty(_StorageInfo.driver) ? "" : "【" + _StorageInfo.driver + "】", Common._Destination, string.IsNullOrEmpty(_StorageInfo.driverTel) ? "" : " 联系电话【" + _StorageInfo.driverTel + "】");
                lblContent.Text = content;
                cboNumMethod.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                throw new Exception("显示基本信息失败:" + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                ucConsignors1.tmResultState.Enabled = false;
                string parentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase.ToString()) + "\\tempPic\\";
                Directory.Delete(parentPath, true);
            }
            catch
            {

            }
            FrmParent.ParentForm.OpenForm(PageState.Main);
        }

        private void chbIsArrive_CheckStateChanged(object sender, EventArgs e)
        {
            if (chbIsArrive.Checked)
                lblContent.Text = string.Format("【{0}】 运抵卸车", Common._StorageName);
            else
                lblContent.Text = string.Format("【{0}】 配送员{1}已出发 准备运往【{2}】{3}", Common._StorageName, string.IsNullOrEmpty(_StorageInfo.driver) ? "" : "【" + _StorageInfo.driver + "】", Common._Destination, string.IsNullOrEmpty(_StorageInfo.driverTel) ? "" : " 联系电话【" + _StorageInfo.driverTel + "】");
        }
        #endregion

        #region 初始化扫描器

        public void SetNumber(string number)
        {
            txtOrderNumber.Text = number;
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
                if (tcScan.SelectedIndex == 0)
                {
                    if (cboNumMethod.SelectedIndex == 1 && MessageBox.Show("确定要提交运单号为[" + txtOrderNumber.Text.Trim() + "]的节点吗?", "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                        return;

                    SaveNode();
                }
            }

        }
        #endregion
        #endregion

        #region 保存节点
        /// <summary>
        /// 显示/隐藏 录入条码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboNumMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
            btnConfirm.Enabled = false;
            txtOrderNumber.Text = string.Empty;
            txtOrderNumber.Enabled = cboNumMethod.SelectedIndex == 1;
            if (cboNumMethod.SelectedIndex == 1)
            {
                if (tcScan.SelectedIndex == 0)
                    btnConfirm.Enabled = true;
                txtOrderNumber.Focus();

            }
        }


        /// <summary>
        /// 保存节点到本地数据库
        /// </summary>
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
                    if (cboStorageScan.SelectedValue.ToString() == "0")
                    {
                        exceptionStr = "请选择上一个冷藏载体";
                        MessageBox.Show(exceptionStr, "请选择冷藏载体", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                        return;
                    }
                    //exceptionStr = "请选择供应商";
                    //MessageBox.Show("请选择是自运单还是第三方单", "请选择供应商", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                    //return;
                }
                FrmParent.ParentForm.CheckNumber(number, false);
                int parentStorageId = Convert.ToInt32(cboStorageScan.SelectedValue);
                dtNow = WaybillServer.AddNode(number, _StorageInfo.Id, _StorageInfo.storageName, lblContent.Text.Trim(), chbIsArrive.Checked, ucConsignors1._SelectConSignor.ConsignorId, parentStorageId, ref content);
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
                Common.SaveOptRecord(exceptionStr == string.Empty ? "保存中间节点成功[" + cboStorageScan.Text + "]" : ("保存中间节点失败[" + cboStorageScan.Text + "]," + exceptionStr), content, dtNow, number, ucConsignors1._SelectConSignor.ConsignorId);
                ucConsignors1.SelectedConsignors();
            }
        }

        #endregion

        #region 保存结果
        /// <summary>
        /// 初始化保存结果
        /// </summary>
        private void tmResultState_Tick(object sender, EventArgs e)
        {
            try
            {
                tmResultState.Enabled = false;
                lblResult.BackColor = Color.Silver;
                lblResult.Text = "扫描/录入单号";
            }
            catch
            {
            }
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
            string name = (isNode ? "节点" : "图片");
            if (result)
                txtLog.Text += string.Format("\r\n{0}:成功扫描[{3}]运单[{1}]的{2};", time, txtOrderNumber.Text.Trim(), name, ucConsignors1._SelectConSignor.ConsignorName);
            //else
            //    txtLog.Text += string.Format("{0}:保存编号[{1}]的{2}失败:" + exceptionStr, time, txtOrderNumber.Text.Trim(), name);
            //if (result)
            //    lblResult.Text = "保存节点成功";
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

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (cboNumMethod.SelectedIndex == 1)
            {
                if (txtOrderNumber.Text.Trim() == string.Empty)
                    txtOrderNumber.BackColor = Color.Red;
                else
                {
                    txtOrderNumber.BackColor = Color.White;
                    if (tcScan.SelectedIndex == 0)
                    {
                        if (cboNumMethod.SelectedIndex == 1 && MessageBox.Show("确定要提交运单号为[" + txtOrderNumber.Text.Trim() + "]的节点吗?", "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                            return;

                        SaveNode();
                    }
                }
            }
        }

        private void pnlTop_LostFocus(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
        }

        private void tpNode_Click(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
        }

        private void tcScan_SelectedIndexChanged(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
        }

        private void tcScan_GotFocus(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
        }

        private void tcScan_LostFocus(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
        }

        private void txtLog_GotFocus(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
        }

        private void txtLog_TextChanged(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
        }

        private void txtLog_LostFocus(object sender, EventArgs e)
        {
            FrmParent.ParentForm.ResetReturnDelay();
        }
    }
}
