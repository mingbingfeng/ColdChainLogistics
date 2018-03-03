using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using C2LP.PDA.Datas.BLL;
using System.Threading;

namespace C2LP.PDA.APP
{
    public partial class UCMain : UserControl
    {
        public const string _numberTitle = "请设置PDA编号";
        public const string _storageTitle = "请绑定冷库或车载";
        public const string _destinationTitle = "请设置目的地";

        public UCMain()
        {
            InitializeComponent();
            Common.ValueChangeEvent += new Common.ValueChangeDelegate(Common_ValueChangeEvent);
            AddControlEvent();
            btnNumber.Text = _numberTitle;
            btnDestination.Text = _destinationTitle;
            btnStorage.Text = _storageTitle;
            Thread th = new Thread(InitPDALocalInfo);
            th.IsBackground = true;
            th.Start();
        }

        void Common_ValueChangeEvent(Enum_DicKey key, object value)
        {
            if (value == null)
                return;
            switch (key)
            {
                case Enum_DicKey.pdaNumber:
                    value = value.ToString() == string.Empty ? _numberTitle : value;
                    SetControlText(btnNumber, Common._PDANumber, true);
                    break;
                case Enum_DicKey.storageName:
                    value = value.ToString() == string.Empty ? _storageTitle : value;
                    SetControlText(btnStorage, Common._StorageName, true);
                    break;
                case Enum_DicKey.destination:
                    value = value.ToString() == string.Empty ? _destinationTitle : value;
                    SetControlText(btnDestination, Common._Destination, true);
                    break;
                case Enum_DicKey.lastSyncTime:
                    if (value.ToString() != string.Empty)
                        SetControlText(lblLastSyncTime, Common._LastSyncTime, false);
                    break;
                case Enum_DicKey.pdaId:
                    break;
                default:
                    break;
            }
        }

        public void RemoeveControlEvent()
        {
            btnNumber.Click -= new EventHandler(btnNumber_Click);
            btnStorage.Click -= new EventHandler(btnStorage_Click);
            btnDestination.Click -= new EventHandler(btnDestination_Click);
            btnOrderInput.Click -= new EventHandler(btnOrderInput_Click);
            btnNodeScan.Click -= new EventHandler(btnNodeScan_Click);
            btnConfig.Click -= new EventHandler(btnConfig_Click);
            btnThirdParty.Click += new EventHandler(btnThirdParty_Click);
            btnWaitUploadNode.Click -= new EventHandler(btnWaitUploadNode_Click);
        }


        public void AddControlEvent()
        {
            btnNumber.Click += new EventHandler(btnNumber_Click);
            btnStorage.Click += new EventHandler(btnStorage_Click);
            btnDestination.Click += new EventHandler(btnDestination_Click);
            btnOrderInput.Click += new EventHandler(btnOrderInput_Click);
            btnNodeScan.Click += new EventHandler(btnNodeScan_Click);
            btnConfig.Click += new EventHandler(btnConfig_Click);
            btnThirdParty.Click += new EventHandler(btnThirdParty_Click);
            btnConnect.Click += new EventHandler(btnConnect_Click);
            btnWaitUploadNode.Click += new EventHandler(btnWaitUploadNode_Click);
        }

        void btnConnect_Click(object sender, EventArgs e)
        {
            FrmParent.ParentForm.OpenForm(PageState.Connect);
        }

        void btnConfig_Click(object sender, EventArgs e)
        {
            FrmParent.ParentForm.OpenForm(PageState.UploadConfig);
        }

        void btnNodeScan_Click(object sender, EventArgs e)
        {
            FrmParent.ParentForm.OpenForm(PageState.NodeScan);
        }

        void btnOrderInput_Click(object sender, EventArgs e)
        {
            FrmParent.ParentForm.OpenForm(PageState.OrderInout);
        }

        void btnDestination_Click(object sender, EventArgs e)
        {
            FrmParent.ParentForm.OpenForm(PageState.SetDestin);
        }

        void btnStorage_Click(object sender, EventArgs e)
        {
            FrmParent.ParentForm.OpenForm(PageState.SetStorage);
        }


        void btnNumber_Click(object sender, EventArgs e)
        {
            FrmParent.ParentForm.OpenForm(PageState.SetNumber);
        }

        void btnThirdParty_Click(object sender, EventArgs e)
        {
            FrmParent.ParentForm.OpenForm(PageState.ThirdParty);
        }

        void btnWaitUploadNode_Click(object sender, EventArgs e)
        {
            FrmParent.ParentForm.OpenForm(PageState.WaitUploadNode);
        }

        /// <summary>
        /// 初始化PDA本地信息
        /// </summary>
        private void InitPDALocalInfo()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                Common.Init();
                //Common._PDANumber = DictionaryServer.GetPDAInfo(Enum_DicKey.pdaNumber);
                if (string.IsNullOrEmpty(Common._PDANumber))
                {
                    FrmParent.ParentForm.OpenForm(PageState.SetNumber);
                    MessageBox.Show("在使用前,请先设置本机对应服务器的设备编号!", "欢迎使用思博源冷链物流系统", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                }
                else if (Common._IsMainFormFirstShow)
                {
                    //UCSyncAll uc = FrmParent.ParentForm.OpenForm(PageState.Sync) as UCSyncAll;
                    //MessageBox.Show("在使用前,最好先同步一次最新资料,不过您也可以停止同步!", "欢迎使用思博源冷链物流系统", MessageBoxButtons.OK, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                    //uc.StartSync(this.Name);
                }
                Common._IsMainFormFirstShow = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "加载参数失败", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
            }
            finally {
                Cursor.Current = Cursors.Default;
            }
        }


        #region 线程异步设置控件的Text属性
        private delegate void MyDelagete(Control con, string text, bool changeSize);
        public void SetControlText(Control con, string text, bool changeSize)
        {
            try
            {
                if (string.IsNullOrEmpty(text))
                    return;
                if (con.InvokeRequired)
                {
                    MyDelagete md = new MyDelagete(SetControlText);
                    con.Invoke(md, con, text, changeSize);
                }
                else
                {
                    if (changeSize)
                    {
                        if (text.Length > 9)
                            con.Font = new Font(con.Font.Name, con.Font.Size - 2, con.Font.Style);
                        if (text.Length > 12)
                            con.Font = new Font(con.Font.Name, con.Font.Size - 1, con.Font.Style);
                        if (text.Length > 18)
                            con.Font = new Font(con.Font.Name, con.Font.Size - 1, con.Font.Style);
                    }
                    con.Text = text;
                }
            }
            catch
            {
            }
        }
        #endregion

        private void btnSync_Click(object sender, EventArgs e)
        {
            CheckNumber();
            FrmParent.ParentForm.OpenForm(PageState.Sync);
        }

        private bool CheckNumber()
        {
            if (string.IsNullOrEmpty(Common._PDANumber))
            {
                MessageBox.Show("请先设置PDA编号!", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Hand, MessageBoxDefaultButton.Button1);
                return false;
            }
            return true;
        }

        private void FrmMain_Closing(object sender, CancelEventArgs e)
        {
            //Rectangle rect = new Rectangle();
            //FullScreen.SetFullScreen(false, ref rect);//隐藏

            this.Dispose(true);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            FrmParent.ParentForm.Dispose();
            Application.Exit();
        }


    }
}
