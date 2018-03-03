using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using C2LP.Manager.Console.ConsoleServerWebReference;

namespace C2LP.Manager.Console.FrmClientControl
{
    public partial class FrmCooperativeClientID : Form
    {
        public FrmCooperativeClientID()
        {
            InitializeComponent();
        }
        //定义变量接收FrmCooperativeClientList窗体传过来的值
        //public string ID { set; get; }
        //public string fullName { set; get; }
        public Model_Customer customs;
        ConsoleServerWebReference.ConsoleServer cs = new ConsoleServerWebReference.ConsoleServer();
        private void FrmCooperativeClientID_Load(object sender, EventArgs e)
        {
            //lbName.Text = fullName + "客户的账号列表";
            lbName.Text = customs.FullNamek__BackingField + "客户的账号列表";
            winFormPager1.OnPageChanged += new EventHandler(winFormPager1_OnPageChanged);
            CustomerUserLoad();
            AutoSizeColumn(dataGridView1);
        }
        public void winFormPager1_OnPageChanged(object sender,EventArgs e)
        {
            CustomerUserLoad();
        }
        public void CustomerUserLoad()
        {
            try
            {
                string pageIndexAndCount = winFormPager1.PageIndex + "." + winFormPager1.PageSize;
                ResultModelOfArrayOfModel_CustomerUserd4FqxSXX customeruser = cs.GetCustomerUserList(customs.Idk__BackingField, true, pageIndexAndCount);
                ResultModelOfArrayOfModel_CustomerUserd4FqxSXX count = cs.GetCustomerUserList(customs.Idk__BackingField, true, null);
                winFormPager1.DrawControl(count.Data.Count<Model_CustomerUser>());
                if (customeruser.Code != 0)
                {
                    MessageBox.Show(customeruser.Message);
                }
                else
                {
                    dataGridView1.Rows.Clear();
                    dataGridView1.AutoGenerateColumns = false;
                    foreach (Model_CustomerUser item in customeruser.Data)
                    {
                        int rowIndex = dataGridView1.Rows.Add();
                        dataGridView1.Rows[rowIndex].Cells[0].Value = item.Idk__BackingField;
                        dataGridView1.Rows[rowIndex].Cells[1].Value = item.UserNamek__BackingField;
                        dataGridView1.Rows[rowIndex].Cells[2].Value = item.DisplayNamek__BackingField;
                        dataGridView1.Rows[rowIndex].Cells[3].Value = item.Passwordk__BackingField;
                        dataGridView1.Rows[rowIndex].Cells[4].Value = item.CreateAtk__BackingField.ToString("yyyy-MM-dd HH:mm:ss");
                        if (item.Activedk__BackingField == Enum_Active.Enabled)
                            dataGridView1.Rows[rowIndex].Cells[5].Value = "启用";

                        else
                            dataGridView1.Rows[rowIndex].Cells[5].Value = "停用";

                        dataGridView1.Rows[rowIndex].Tag = item;

                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmEdit_Click(object sender, EventArgs e)
        {
            Model_CustomerUser customeruser = dataGridView1.SelectedRows[0].Tag as Model_CustomerUser;
            Model_Customer mcus = new Model_Customer();
            mcus.Rolek__BackingField = customs.Rolek__BackingField;
            //
            mcus.Idk__BackingField = customs.Idk__BackingField;
            mcus.FullNamek__BackingField = customs.FullNamek__BackingField;
            //
            FrmAccountsEdit edit = new FrmAccountsEdit();
            edit._ParentClient = this;
            edit.customeruser = customeruser;
            //edit.UserName = fullName;
            //edit.ID = ID;
            edit.mcust = mcus;
            edit.ShowDialog();
            CustomerUserLoad();
        }
        /// <summary>
        /// dataGridView密码列显示星号*
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // 把第4列显示*号，*号的个数和实际数据的长度相同
            if (e.ColumnIndex == 3)
            {
                if (e.Value != null && e.Value.ToString().Length > 0)
                {
                    e.Value = new string('*', e.Value.ToString().Length);
                }
            }
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Model_Customer mcus = new Model_Customer();
            mcus.Rolek__BackingField = customs.Rolek__BackingField;
            //
            mcus.Idk__BackingField = customs.Idk__BackingField;
            mcus.FullNamek__BackingField = customs.FullNamek__BackingField;
            //
            FrmAccountsEdit edit = new FrmAccountsEdit();
            edit._ParentClient = this;
            //edit.UserName = fullName;
            //edit.ID = ID;
            edit.mcust = mcus;
            edit.ShowDialog();
            CustomerUserLoad();
        }
        public void getRenew()
        {
            CustomerUserLoad();
        }
       
        /// <summary>
        /// 启用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmEnable_Click(object sender, EventArgs e)
        {
            try
            {
                Model_CustomerUser customeruser = dataGridView1.SelectedRows[0].Tag as Model_CustomerUser;
                customeruser.Activedk__BackingField = Enum_Active.Enabled;
                ResultModelOfModel_CustomerUserd4FqxSXX edit = cs.EditCustomerUser(customeruser);
                if (edit.Code != 0)
                {
                    MessageBox.Show(edit.Message);
                }
                else
                {
                    MessageBox.Show("操作成功");
                    CustomerUserLoad();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 停用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmDisable_Click(object sender, EventArgs e)
        {
            try
            {
                Model_CustomerUser customeruser = dataGridView1.SelectedRows[0].Tag as Model_CustomerUser;
                customeruser.Activedk__BackingField = Enum_Active.Disable;
                ResultModelOfModel_CustomerUserd4FqxSXX edit = cs.EditCustomerUser(customeruser);
                if (edit.Code != 0)
                {
                    MessageBox.Show(edit.Message);
                }
                else
                {
                    MessageBox.Show("操作成功");
                    CustomerUserLoad();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 根据状态显示隐藏启用停用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            ResultModelOfArrayOfModel_CustomerUserd4FqxSXX count = cs.GetCustomerUserList(customs.Idk__BackingField, true, null);
            if (count.Data.Count<Model_CustomerUser>() <= 0)
            {
                contextMenuStrip1.Enabled = false;
                return;
            }
            else
                contextMenuStrip1.Enabled = true;
            Model_CustomerUser custuse = dataGridView1.SelectedRows[0].Tag as Model_CustomerUser;
            if (custuse.Activedk__BackingField==Enum_Active.Enabled)
            {
                tsmEnable.Enabled = false;
                tsmDisable.Enabled = true;
            }
            else
            {
                tsmEnable.Enabled = true;
                tsmDisable.Enabled = false;
            }
        }
        /// <summary>
        /// 使DataGridView的列自适应宽度
        /// </summary>
        /// <param name="dgViewFiles"></param>
        private void AutoSizeColumn(DataGridView dgViewFiles)
        {
            int width = 0;
            //使列自使用宽度
            //对于DataGridView的每一个列都调整
            for (int i = 0; i < dgViewFiles.Columns.Count; i++)
            {
                //将每一列都调整为自动适应模式
                dgViewFiles.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);
                //记录整个DataGridView的宽度
                width += dgViewFiles.Columns[i].Width;
            }
            //判断调整后的宽度与原来设定的宽度的关系，如果是调整后的宽度大于原来设定的宽度，
            //则将DataGridView的列自动调整模式设置为显示的列即可，
            //如果是小于原来设定的宽度，将模式改为填充。
            if (width > dgViewFiles.Size.Width)
            {
                dgViewFiles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            }
            else
            {
                dgViewFiles.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            //冻结某列 从左开始 0，1，2
            dgViewFiles.Columns[1].Frozen = true;
        }

    }
}
