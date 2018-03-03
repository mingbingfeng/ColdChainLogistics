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
using C2LP.PDA.APP.ScannerAPI;
using System.Reflection;
using System.IO;

namespace C2LP.PDA.APP
{
    public partial class UCNodeScan : UserControl
    {
        public UCNodeScan()
        {
            InitializeComponent();

            txtOrderNumber.Focus();
            txtOrderNumber.KeyDown += new KeyEventHandler(ParentForm_KeyDown);
            //移动相机到本页面
            //FrmParent.ParentForm.pbPreview.Visible = true;
            //FrmParent.ParentForm.pbPreview.Parent = tpPicture;
            //FrmParent.ParentForm.pbPreview.Location = new Point(3, 3);
            //FrmParent.ParentForm.pbPreview.Size = new Size(405, 300);
            //FrmParent.ParentForm.pbPreview.SendToBack();

            //线程执行初始化
            Thread th = new Thread(DoInit);
            th.IsBackground = true;
            th.Start();
        }

        void ParentForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F19)
            {
                //txtHideTempScan.Focus();
                string b = USI_API_HT380W.GetScanData();
                //MessageBox.Show(b);
                this.txtOrderNumber.Text = b;
            }
        }

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
        public int _PicCount = 0;

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
                LoadScanner();
                InitPhoto();
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
        /// 显示界面信息
        /// </summary>
        private void LoadNodeInfo()
        {
            try
            {
                lblStorage.Text = Common._StorageName + " [" + (_StorageInfo.storageType == 2 ? "车载" : "冷库") + "]";
                lblDestin.Text = Common._Destination;
                chbIsArrive.Visible = _StorageInfo.storageType == 2;
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
        private void LoadScanner()
        {
            try
            {
                //txtLog.Text += string.Format("相机已激活" + (_IsCameraActive ? "成功" : "失败"));
                //if (_IsCameraActive)
                //    txtLog.Text += ";[+]添加图片;[-]删除图片;\r\n";
                //else
                //    txtLog.Text += ",重启设备或许可以解决此问题!\r\n";

                //Scanner.GetScanner().Open();
                //Scanner.GetScanner().OnGetBarcodeEvent += new GetBarcodeEventHandler(Scanner_OnGetBarcodeEvent);
                USI_API_HT380W.Barcode1D_init();
                //txtLog.Text += ("红外扫描已激活,请按键盘扫描键进行扫码!\r\n");
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //void Scanner_OnGetBarcodeEvent(object sender, GetBarcodeEventArgs e)
        //{
        //    cboNumMethod.SelectedIndex = 0;
        //    string barcode = e.Barcode.Replace("\0", "");
        //    barcode = barcode.Replace("\r\n", "");
        //    barcode = barcode.Replace("\r", "");
        //    txtOrderNumber.Text = barcode;
        //}

        private void txtOrderNumber_TextChanged(object sender, EventArgs e)
        {
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
            btnConfirm.Enabled = false;
            txtOrderNumber.Text = string.Empty;
            txtOrderNumber.ReadOnly = !(cboNumMethod.SelectedIndex == 1);
            if (cboNumMethod.SelectedIndex == 1)
            {
                if (tcScan.SelectedIndex == 0)
                    btnConfirm.Enabled = true;
            }
            txtOrderNumber.Focus();
        }


        /// <summary>
        /// 保存节点到本地数据库
        /// </summary>
        private void SaveNode()
        {
            bool result = false;
            string exceptionStr = string.Empty;
            try
            {
                WaybillServer.AddNode(txtOrderNumber.Text.Trim(), _StorageInfo.Id, _StorageInfo.storageName, lblContent.Text.Trim(), chbIsArrive.Checked);
                result = true;
            }
            catch (Exception ex)
            {
                exceptionStr = "保存数据出错:[" + ex.Message + "]";
            }
            ShowResult(result, true, exceptionStr);
        }

        #endregion

        #region 签收拍照
        /// <summary>
        /// 初始化签收照片列表
        /// </summary>
        private void InitPhoto()
        {
            //_PicCount = 1;
            //FrmParent.ParentForm.pbPreview.Image = null;
            picCaptrue.Image = null;
            lbPicList.Items.Clear();
            lbPicList.DisplayMember = "PicName";
            lbPicList.ValueMember = "PicPath";

            //PicNameAndPath pic = new PicNameAndPath { PicName = "P" + _PicCount, PicPath = string.Empty };
            //lbPicList.Items.Add(pic);
            //lbPicList.SelectedItem = pic;

        }

        /// <summary>
        /// 显示图片/拍摄预览
        /// </summary>
        private void lbPicList_SelectedIndexChanged(object sender, EventArgs e)
        {
            PicNameAndPath pic = lbPicList.SelectedItem as PicNameAndPath;
            if (pic == null)
                return;
            if (!string.IsNullOrEmpty(pic.PicPath))
            {
                //bool flag = UnitechDSDll.PreviewStop();
                //FrmParent.ParentForm.pbPreview.Image = new Bitmap(pic.PicPath);
                picCaptrue.Image = new Bitmap(pic.PicPath);
                lbPicList.Enabled = true;
                //btnCamera.Text = "重拍";
            }
            //else
            //{
            //    btnCamera.Text = "拍摄";
            //    bool flag = UnitechDSDll.PreviewStart();
            //    lbPicList.Enabled = false;
            //}
        }

        private string jpgFileName = "capTmp.jpg";
        private string jpgFilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\pic\\";
        private string filePathExe = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\CameraDemo.exe";
        private string tempPicPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\tempPic\\";

        /// <summary>
        /// 添加图片
        /// </summary>
        private void btnAddPic_Click(object sender, EventArgs e)
        {
            //if (btnCamera.Text == "拍摄")
            //{
            //    txtLog.Text += "请先拍摄当前图片!\r\n";
            //    //MessageBox.Show("请先拍摄当前图片!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            //    return;
            //}
            //_PicCount++;
            //PicNameAndPath pic = new PicNameAndPath { PicName = "P" + _PicCount, PicPath = string.Empty };
            //lbPicList.Items.Add(pic);
            //lbPicList.SelectedItem = pic;

            try
            {
                btnAddPic.Enabled = false;
                //清空/创建缓存图片目录
                if (Directory.Exists(jpgFilePath))
                    Directory.Delete(jpgFilePath, true);
                Directory.CreateDirectory(jpgFilePath);
                if (!Directory.Exists(tempPicPath))
                    Directory.CreateDirectory(tempPicPath);
                System.Diagnostics.Process.Start(filePathExe, null).WaitForExit();
                if (File.Exists(jpgFilePath + jpgFileName))
                {
                    //已在拍照界面拍照
                    foreach (string fileName in Directory.GetFiles(jpgFilePath))
                    {
                        if (!fileName.Contains(jpgFileName))
                        {
                            _PicCount++;
                            string newTempPath = tempPicPath + Path.GetFileName(fileName);
                            File.Move(fileName, newTempPath);
                            PicNameAndPath pic = new PicNameAndPath { PicName = "P" + _PicCount, PicPath = newTempPath };
                            lbPicList.Items.Add(pic);
                        }
                        lbPicList.SelectedIndex = 0;
                        btnRemovePic.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("调用相机模块失败："+ex.Message);
            }
            finally {
                btnAddPic.Enabled = true;
            }
        }

        /// <summary>
        /// 删除图片
        /// </summary>
        private void btnRemovePic_Click(object sender, EventArgs e)
        {
            //if (lbPicList.Items.Count > 1)
            //{
            //if (btnCamera.Text == "拍摄")
            //{
            //    txtLog.Text += "请先拍摄当前图片!\r\n";
            //    //MessageBox.Show("请先拍摄当前图片!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            //    return;
            //}
            PicNameAndPath pic = lbPicList.SelectedItem as PicNameAndPath;
            if (pic != null)
                lbPicList.Items.Remove(pic);
            if (lbPicList.Items.Count == 0)
            {
                picCaptrue.Image = null;
                btnRemovePic.Enabled = false;
            }
            else
                lbPicList.SelectedIndex = lbPicList.Items.Count - 1;
            //}
        }

        /// <summary>
        /// 拍摄/重拍
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCamera_Click(object sender, EventArgs e)
        {
            //btnCamera.Enabled = false;
            //if (btnCamera.Text == "重拍")
            //{
            //    btnCamera.Text = "拍摄";
            //    UnitechDSDll.PreviewStart();
            //}
            //else
            //{
            //    Cursor.Current = Cursors.WaitCursor;
            //    PicNameAndPath pic = lbPicList.SelectedItem as PicNameAndPath;
            //    string parentPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase.ToString()) + "\\tempPic\\";
            //    if (!Directory.Exists(parentPath))
            //        Directory.CreateDirectory(parentPath);
            //    string filePath = parentPath + DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
            //    if (System.IO.File.Exists(filePath)) System.IO.File.Delete(filePath);
            //    if (UnitechDSDll.SnapPicture(filePath))
            //    {
            //        pic.PicPath = filePath;
            //        while (!File.Exists(pic.PicPath))
            //        {
            //            Thread.Sleep(300);
            //        }
            //        UnitechDSDll.PreviewStart();
            //        UnitechDSDll.PreviewStop();
            //        lbPicList_SelectedIndexChanged(sender, e);
            //    }
            //    Cursor.Current = Cursors.Default;
            //}
            //btnCamera.Enabled = true;
        }

        /// <summary>
        /// 保存签收图片
        /// </summary>
        private void btnSavePic_Click(object sender, EventArgs e)
        {
            //if (txtOrderNumber.Text.Trim().Length != 12)
            if (txtOrderNumber.Text.Trim() == string.Empty)
            {
                MessageBox.Show("请通过扫描或手工录入正确的运单编号！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                txtOrderNumber.Focus();
                return;
            }
            //if (btnCamera.Text == "拍摄")
            //{
            //    MessageBox.Show("请完成当前照片的拍摄或按[-]删除掉再保存！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            //    return;
            //}
            if (lbPicList.Items.Count == 0)
            {
                MessageBox.Show("请点击按钮【+】来拍摄签收图片！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                return;
            }
            if (MessageBox.Show(string.Format("编号[{0}] 图片[{1}]张", txtOrderNumber.Text.Trim(), lbPicList.Items.Count), "确定要保存吗?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                return;
            bool result = true;
            string exceptionStr = string.Empty;
            try
            {
                //检查缓存文件
                List<FileInfo> fileList = new List<FileInfo>();
                foreach (PicNameAndPath item in lbPicList.Items)
                {
                    try
                    {
                        fileList.Add(new FileInfo(item.PicPath));
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("获取缓存图片[" + item.PicName + "]出错:[" + ex.Message + "]");
                    }
                }
                //创建保存路径
                string savePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase.ToString()) + "\\postback\\";
                if (!Directory.Exists(savePath))
                    Directory.CreateDirectory(savePath);
                List<string> filePathList = new List<string>();
                //移动缓存文件
                foreach (FileInfo fi in fileList)
                {
                    try
                    {
                        string newFileName = savePath + fi.Name;
                        fi.MoveTo(newFileName);
                        filePathList.Add(newFileName);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("保存缓存图片[" + fi.Name + "]出错:[" + ex.Message + "]");
                    }
                }
                //保存文件路径
                try
                {
                    WaybillServer.AddPostBack(txtOrderNumber.Text.Trim(), filePathList);
                }
                catch (Exception ex)
                {
                    throw new Exception("保存数据出错:[" + ex.Message + "]");
                }
                InitPhoto();
            }
            catch (Exception ex)
            {
                result = false;
                exceptionStr = ex.Message;
            }
            finally
            {
                ShowResult(result, false, exceptionStr);
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
                tcScan_SelectedIndexChanged(sender, e);
            }
            catch
            {
            }
        }

        /// <summary>
        /// 切换 节点/图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tcScan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tcScan.SelectedIndex == 0)
            {
                lblResult.Text = "扫描/录入单号";
                if (cboNumMethod.SelectedIndex == 1)
                    btnConfirm.Enabled = true;
                else
                    btnConfirm.Enabled = false;
            }
            else
            {
                lblResult.Text = "拍摄/保存图片";
                btnConfirm.Enabled = false;
                //Cursor.Current = Cursors.WaitCursor;
                //UnitechDSDll.PreviewStop();
                //UnitechDSDll.PreviewStart();
                //Cursor.Current = Cursors.Default;
            }
            txtOrderNumber.Focus();
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
                txtLog.Text += string.Format("{0}:成功保存编号[{1}]的{2};\r\n", time, txtOrderNumber.Text.Trim(), name);
            else
                txtLog.Text += string.Format("{0}:保存编号[{1}]的{2}失败:" + exceptionStr, time, txtOrderNumber.Text.Trim(), name);
            lblResult.Text = "保存" + name + (result ? "成功" : "失败");
            lblResult.BackColor = result ? Color.Lime : Color.Red;

            txtOrderNumber.Text = string.Empty;
            txtOrderNumber.BackColor = Color.White;
            txtLog.SelectionStart = txtLog.Text.Split(new char[] { '\r', '\n' }).Length * 20;
            txtLog.ScrollToCaret();
            tmResultState.Enabled = true;
            Thread.Sleep(888);
            txtOrderNumber.Focus();
        }
        #endregion

        private void control_GotFocus(object sender, EventArgs e)
        {
            FrmParent.ParentForm.CheckInputPnl(true);
            if (sender == txtOrderNumber)
                CheckPanel(true);
            Control c = sender as Control;
            c.BackColor = Color.White;
        }

        private void control_LostFocus(object sender, EventArgs e)
        {
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
                        Application.DoEvents();
                        //txtHideTempScan.Text = string.Empty;
                    }
                }
            }
        }

    }
}
