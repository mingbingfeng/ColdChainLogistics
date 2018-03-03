using C2LP.Manager.Console.FrmAdministrativeDivisionControl;
using C2LP.Manager.Console.FrmClientControl;
using C2LP.Manager.Console.FrmPDAControl;
using C2LP.Manager.Console.FrmVehicleMountedControl;
using C2LP.Manager.Console.FrmWarehouseControl;
using C2LP.Manager.Console.FrmWaybillControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace C2LP.Manager.Console
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            
        }
        public string UserName { get; set; }
        private void MainForm_Load(object sender, EventArgs e)
        {
            //ClientControls();
            this.toolStripStatusLabel2.Text = "登陆用户：" + UserName;
            this.toolStripStatusLabel3.Text = "当前时间："+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            this.timer1.Interval = 1000;
            this.timer1.Start();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            ClientControls();
        }
        public void ClientControls()
        {
            tabControl2.TabPages.Clear();
            tabControl2.TabPages.Add("上游发货单位维护", "上游发货单位维护");
            FrmCooperativeClientList clientlist = new FrmCooperativeClientList();
            clientlist.TopLevel = false;
            clientlist.Dock = DockStyle.Fill;
            clientlist.FormBorderStyle = FormBorderStyle.None;
            clientlist.Show();
            tabControl2.TabPages["上游发货单位维护"].Controls.Add(clientlist);
            
            tabControl2.TabPages.Add("下游收货单位维护", "下游收货单位维护");
            FrmConsigneeMaintenance maintenance = new FrmConsigneeMaintenance();
            maintenance.TopLevel = false;
            maintenance.Dock = DockStyle.Fill;
            maintenance.FormBorderStyle = FormBorderStyle.None;
            maintenance.Show();
            tabControl2.TabPages["下游收货单位维护"].Controls.Add(maintenance);

            tabControl2.TabPages.Add("惊尘账号管理", "惊尘账号管理");
            FrmUsersManage manage = new FrmUsersManage();
            manage.TopLevel = false;
            manage.Dock = DockStyle.Fill;
            manage.FormBorderStyle = FormBorderStyle.None;
            manage.Show();
            tabControl2.TabPages["惊尘账号管理"].Controls.Add(manage);
        }
        private void button6_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            tabControl2.TabPages.Clear();
            tabControl2.TabPages.Add("运单历史查询","运单历史查询");
            FrmWaybillHistoryQuery history = new FrmWaybillHistoryQuery();
            history.TopLevel = false;
            history.Dock = DockStyle.Fill;
            history.FormBorderStyle = FormBorderStyle.None;
            history.Show();
            tabControl2.TabPages["运单历史查询"].Controls.Add(history);
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            tabControl2.TabPages.Clear();
            tabControl2.TabPages.Add("仓库列表", "仓库列表");
            FrmWarehouseList house = new FrmWarehouseList();
            house.TopLevel = false;
            house.Dock = DockStyle.Fill;
            house.FormBorderStyle = FormBorderStyle.None;
            house.Show();
            tabControl2.TabPages["仓库列表"].Controls.Add(house);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            tabControl2.TabPages.Clear();
            tabControl2.TabPages.Add("车载列表", "车载列表");
            FrmVehicleMountedList mounted = new FrmVehicleMountedList();
            mounted.TopLevel = false;
            mounted.Dock = DockStyle.Fill;
            mounted.FormBorderStyle = FormBorderStyle.None;
            mounted.Show();
            tabControl2.TabPages["车载列表"].Controls.Add(mounted);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            tabControl2.TabPages.Clear();
            tabControl2.TabPages.Add("PDA列表", "PDA列表");
            FrmPDAList pda = new FrmPDAList();
            pda.TopLevel = false;
            pda.Dock = DockStyle.Fill;
            pda.FormBorderStyle = FormBorderStyle.None;
            pda.Show();
            tabControl2.TabPages["PDA列表"].Controls.Add(pda);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            tabControl2.TabPages.Clear();
            tabControl2.TabPages.Add("行政区域管理", "行政区域管理");
            FrmAdministrativeControl adimcontrol = new FrmAdministrativeControl();
            adimcontrol.TopLevel = false;
            adimcontrol.Dock = DockStyle.Fill;
            adimcontrol.FormBorderStyle = FormBorderStyle.None;
            adimcontrol.Show();
            tabControl2.TabPages["行政区域管理"].Controls.Add(adimcontrol);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            tabControl2.TabPages.Clear();
            tabControl2.TabPages.Add("运单报表统计", "运单报表统计");
            FrmWaybillStatement waybillstate = new FrmWaybillStatement();
            waybillstate.TopLevel = false;
            waybillstate.Dock = DockStyle.Fill;
            waybillstate.FormBorderStyle = FormBorderStyle.None;
            waybillstate.Show();
            tabControl2.TabPages["运单报表统计"].Controls.Add(waybillstate);
        }

        private void tsmEdition_Click(object sender, EventArgs e)
        {
            ForAbout forabout = new ForAbout();
            forabout.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.toolStripStatusLabel3.Text = "当前时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }
        /// <summary>
        /// 第三方
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnThirdParty_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            tabControl2.TabPages.Clear();
            tabControl2.TabPages.Add("10", "第三方");
            FrmThirdParty thirdparty = new FrmThirdParty();
            thirdparty.TopLevel = false;
            thirdparty.Dock = DockStyle.Fill;
            thirdparty.FormBorderStyle = FormBorderStyle.None;
            thirdparty.Show();
            tabControl2.TabPages["10"].Controls.Add(thirdparty);
        }
    }
}
