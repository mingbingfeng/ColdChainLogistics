using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using C2LP.PDA.APP.ScannerAPI;
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
            Thread th = new Thread(DoInit);
            th.IsBackground = true;
            th.Start();
            
        }
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

                LoadScanner();
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
        #region 初始化扫描器
        private void LoadScanner()
        {
            try
            {
                Scanner.GetScanner().Open();
                Scanner.GetScanner().OnGetBarcodeEvent += new GetBarcodeEventHandler(UCThirdParty_OnGetBarcodeEvent);
            }
            catch 
            {
                
            }
        }
        void UCThirdParty_OnGetBarcodeEvent(object sender, GetBarcodeEventArgs e) 
        {
            string barode = e.Barcode.Replace("\0","");
            barode = barode.Replace("\r\n","");
            barode = barode.Replace("\r","");
            txtOrderNumber.Text = barode;
        }
        #endregion

        #region 返回到主窗体
        private void btnCancel_Click(object sender, EventArgs e)
        {
            FrmParent.ParentForm.OpenForm(PageState.Main);
        }
        #endregion

        #region 保存运单号
        private void button1_Click(object sender, EventArgs e)
        {
            if (CheckInput())
            {
                try
                {
                    string number = txtOrderNumber.Text.Trim();
                    string storageName = Common._StorageName;
                    //添加扫描的运单号到pda数据库
                    HuadongTmsOrderServer.AddhuadongTmsOrder(number, storageName,Common._Destination);
                    
                    MessageBox.Show("添加成功，您可以继续扫描添加！","操作提示",MessageBoxButtons.OK,MessageBoxIcon.Asterisk,MessageBoxDefaultButton.Button1);
                    txtOrderNumber.Text = string.Empty;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message,"创建运单失败",MessageBoxButtons.OK,MessageBoxIcon.Hand,MessageBoxDefaultButton.Button1);
                }
            }
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
            FrmParent.ParentForm.CheckInputPnl(true);
            if (sender == txtOrderNumber)
                CheckPanel(true);
            Control c = sender as Control;
            c.BackColor = Color.White;
        }

        private void control_LostFocus(object sender, EventArgs e)
        {
            FrmParent.ParentForm.CheckInputPnl(false);
            if (sender == txtOrderNumber )
                CheckPanel(false);
        }
        private void CheckPanel(bool show)
        {
            
            if (show)
                this.AutoScrollPosition = new Point(this.Width, this.Height);
        }

        private void txtOrderNumber_TextChanged(object sender, EventArgs e)
        {
            lblNumLength.Text = txtOrderNumber.Text.Trim().Length.ToString() + "位";
            //if (txtOrderNumber.Text.Length != 12 )
            //    txtOrderNumber.BackColor = Color.Red;
            //else
            //    txtOrderNumber.BackColor = Color.White;
        }
    }
}
