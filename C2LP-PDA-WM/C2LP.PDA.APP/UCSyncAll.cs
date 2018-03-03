using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace C2LP.PDA.APP
{
    public partial class UCSyncAll : UserControl
    {
        //Timer _tmReturnMain;
        public UCSyncAll()
        {
            InitializeComponent();
            SyncHelp.SyncEvent += new SyncHelp.SyncDelegate(SyncHelp_SyncEvent);
            SyncHelp.SyncFinish += new SyncHelp.SyncDelegate(SyncHelp_SyncFinish);
            Common.ValueChangeEvent += new Common.ValueChangeDelegate(Common_ValueChangeEvent);
            lblLastUpdateTime.Text = Common._LastSyncTime;
            //_tmReturnMain = new Timer();
            //_tmReturnMain.Interval = 1000;
            //_tmReturnMain.Tick += new EventHandler(_tmReturnMain_Tick);
        }

        void Common_ValueChangeEvent(C2LP.PDA.Datas.BLL.Enum_DicKey key, object value)
        {
            if (key == C2LP.PDA.Datas.BLL.Enum_DicKey.lastSyncTime && value != null && !string.IsNullOrEmpty(value.ToString()))
                SetLastTime(value.ToString());
        }

        void SetLastTime(string str)
        {
            if (lblLastUpdateTime.InvokeRequired)
            {
                AppendLog al = new AppendLog(SetLastTime);
                lblLastUpdateTime.Invoke(al, str);
            }
            else
                lblLastUpdateTime.Text = str;
        }

        void SyncHelp_SyncFinish(string syncInfo)
        {
            if (this.InvokeRequired)
            {
                AppendLog al = new AppendLog(SyncHelp_SyncFinish);
                txtLog.Invoke(al, syncInfo);
            }
            else
            {
                Common._IsMainFormFirstShow = false;
                txtLog.Text += syncInfo;
                txtLog.SelectionStart = txtLog.Text.Split(new char[] { '\r', '\n' }).Length * 20;
                txtLog.ScrollToCaret();
                if (syncInfo == "同步已完成!")
                {
                    if (string.IsNullOrEmpty(Common._StorageName))
                    {
                        DialogResult dr = MessageBox.Show("检测到当前[冷藏载体]无效或未绑定,是否前往进行设置?", "设置向导", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                        if (dr == DialogResult.Yes)
                        {
                            UCSetStorage uc = FrmParent.ParentForm.OpenForm(PageState.SetStorage) as UCSetStorage;
                            uc.Disposed += new EventHandler(uc_Disposed);
                        }
                        else if (string.IsNullOrEmpty(Common._Destination))
                        {
                            dr = MessageBox.Show("检测到当前[目的地]无效或未绑定,是否前往进行设置?", "设置向导", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                            if (dr == DialogResult.Yes)
                                FrmParent.ParentForm.OpenForm(PageState.SetDestin);
                        }
                    }
                }
                Common.SaveOptRecord(syncInfo, txtLog.Text, DateTime.Now, string.Empty, -1);
                //if (Common._IsMainFormFirstShow)
                //    AutoReturnMain(syncInfo);
                //else {
                btnStart.Enabled = true;
                btnStop.Enabled = false;
                btnCancel.Enabled = true;
                //}

            }
        }

        //void _tmReturnMain_Tick(object sender, EventArgs e)
        //{
        //    if (_interval <= 0)
        //    {
        //        _tmReturnMain.Enabled = false;
        //        if (FrmParent.ParentForm.CurrentUc ==this)
        //            FrmParent.ParentForm.OpenForm(PageState.Main);
        //        return;
        //    }
        //    SyncHelp_SyncEvent("\r\n" + _interval + "秒钟后自动返回主界面...");
        //    _interval--;

        //}

        //int _interval = 0;
        //void AutoReturnMain(string syncInfo)
        //{
        //    if (syncInfo == "同步已完成!")
        //        _interval = 2;
        //    else
        //        _interval = 5;
        //    _tmReturnMain.Enabled = true;
        //}

        void uc_Disposed(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Common._Destination))
            {
                DialogResult dr = MessageBox.Show("检测到当前[目的地]无效或未绑定,是否前往进行设置?", "设置向导", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
                if (dr == DialogResult.Yes)
                    FrmParent.ParentForm.OpenForm(PageState.SetDestin);

            }
        }

        private delegate void AppendLog(string info);
        void SyncHelp_SyncEvent(string syncInfo)
        {
            if (this.IsDisposed)
                return;
            if (string.IsNullOrEmpty(syncInfo))
                return;
            if (txtLog.InvokeRequired)
            {
                AppendLog al = new AppendLog(SyncHelp_SyncEvent);
                txtLog.Invoke(al, syncInfo);
            }
            else
            {
                txtLog.Text += syncInfo;
                txtLog.SelectionStart = txtLog.Text.Split(new char[] { '\r', '\n' }).Length * 20;
                txtLog.ScrollToCaret();
            }
        }

        public void StartSync(string str)
        {
            if (this.InvokeRequired)
            {
                AppendLog al = new AppendLog(StartSync);
                this.Invoke(al, str);
            }
            else
            {
                txtLog.Text = string.Empty;
                SyncHelp.StartSync();
                btnStart.Enabled = false;
                btnCancel.Enabled = false;
                btnStop.Enabled = true;
            }
        }


        private void btnStart_Click(object sender, EventArgs e)
        {
            //_tmReturnMain.Enabled = false;
            StartSync("");
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            //_tmReturnMain.Enabled = false;
            SyncHelp.StopSync();
            btnStop.Enabled = false;
            btnStart.Enabled = true;
            btnCancel.Enabled = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //_tmReturnMain.Enabled = false;
            FrmParent.ParentForm.OpenForm(PageState.Main);
        }

    }
}
