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
    public partial class FrmConsigneeMaintenanceID : Form
    {
        ConsoleServerWebReference.ConsoleServer cs = new ConsoleServerWebReference.ConsoleServer();
        public Model_Customer Customers { set; get; }
        public FrmConsigneeMaintenanceID()
        {
            InitializeComponent();
        }

        private void FrmConsigneeMaintenanceID_Load(object sender, EventArgs e)
        {
            lbName.Text= Customers.FullNamek__BackingField + "账号列表";
            winFormPager1.OnPageChanged += new EventHandler(winFormPager1_OnPageChanged);
            CoumterUserLoad();
            AutoSizeColumn(dataGridView1);
        }
        public void winFormPager1_OnPageChanged(object sender,EventArgs e)
        {
            CoumterUserLoad();
        }
        public void CoumterUserLoad()
        {
            try
            {
                if (Customers != null)
                {
                    string pageIndexAndCoun = winFormPager1.PageIndex+"."+winFormPager1.PageSize;
                    ResultModelOfArrayOfModel_CustomerUserd4FqxSXX customeruserlist = cs.GetCustomerUserList(Customers.Idk__BackingField, true, pageIndexAndCoun);
                    ResultModelOfArrayOfModel_CustomerUserd4FqxSXX count = cs.GetCustomerUserList(Customers.Idk__BackingField, true, null);
                    winFormPager1.DrawControl(count.Data.Count<Model_CustomerUser>());
                    if (customeruserlist.Code != 0)
                    {
                        MessageBox.Show(customeruserlist.Message);
                    }
                    else
                    {
                        dataGridView1.Rows.Clear();
                        foreach (Model_CustomerUser item in customeruserlist.Data)
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
            Model_Customer mcust = new  Model_Customer();
            mcust.Rolek__BackingField = Customers.Rolek__BackingField;
            //
            mcust.FullNamek__BackingField = Customers.FullNamek__BackingField;
            mcust.Idk__BackingField = Customers.Idk__BackingField;
            //
            Model_CustomerUser custuser = dataGridView1.SelectedRows[0].Tag as Model_CustomerUser;
            FrmAccountsEdit edit = new FrmAccountsEdit();
            edit._ParentMaint = this;
            edit.customeruser = custuser;
            //edit.UserName = Customers.FullNamek__BackingField;
            //edit.ID = Customers.Idk__BackingField.ToString();
            edit.mcust = mcust;
            edit.ShowDialog();
            CoumterUserLoad();
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
                Model_CustomerUser custuser = dataGridView1.SelectedRows[0].Tag as Model_CustomerUser;
                custuser.Activedk__BackingField = Enum_Active.Enabled;
                ResultModelOfModel_CustomerUserd4FqxSXX editcustuser = cs.EditCustomerUser(custuser);
                if (editcustuser.Code != 0)
                {
                    MessageBox.Show(editcustuser.Message);
                }
                else
                {
                    MessageBox.Show("操作成功");
                    CoumterUserLoad();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                Model_CustomerUser custuser = dataGridView1.SelectedRows[0].Tag as Model_CustomerUser;
                custuser.Activedk__BackingField = Enum_Active.Disable;
                ResultModelOfModel_CustomerUserd4FqxSXX editcustuser = cs.EditCustomerUser(custuser);
                if (editcustuser.Code != 0)
                {
                    MessageBox.Show(editcustuser.Message);
                }
                else
                {
                    MessageBox.Show("操作成功");
                    CoumterUserLoad();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            Model_Customer mcust = new Model_Customer();
            mcust.Rolek__BackingField = Customers.Rolek__BackingField;
            //
            mcust.Idk__BackingField = Customers.Idk__BackingField;
            mcust.FullNamek__BackingField = Customers.FullNamek__BackingField;
            //
            FrmAccountsEdit edit = new FrmAccountsEdit();
            edit._ParentMaint = this;
            //edit.ID = Customers.Idk__BackingField.ToString();
            //edit.UserName = Customers.FullNamek__BackingField;
            edit.mcust= mcust;
            edit.ShowDialog();
            CoumterUserLoad();
        }
        public void getRenew()
        {
            CoumterUserLoad();
        }
        /// <summary>
        /// 密码显示为*号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //获取显示*号的列的索引
            if (e.ColumnIndex==3)
            {
                if (e.Value!=null && e.Value.ToString().Length>0)
                {
                    e.Value = new string('*', e.Value.ToString().Length);
                }
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            ResultModelOfArrayOfModel_CustomerUserd4FqxSXX count = cs.GetCustomerUserList(Customers.Idk__BackingField, true, null);
            if (count.Data.Count<Model_CustomerUser>() <= 0)
            {
                contextMenuStrip1.Enabled = false;
                return;
            }
            else
                contextMenuStrip1.Enabled = true;
            Model_CustomerUser mctu = dataGridView1.SelectedRows[0].Tag as Model_CustomerUser;
            if (mctu.Activedk__BackingField == Enum_Active.Enabled)
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
