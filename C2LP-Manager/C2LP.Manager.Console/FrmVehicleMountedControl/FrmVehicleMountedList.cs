using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using C2LP.Manager.Console.ConsoleServerWebReference;
using System.Text.RegularExpressions;

namespace C2LP.Manager.Console.FrmVehicleMountedControl
{
    public partial class FrmVehicleMountedList : Form
    {
        ConsoleServerWebReference.ConsoleServer cs = new ConsoleServerWebReference.ConsoleServer();
        public FrmVehicleMountedList()
        {
            InitializeComponent();
        }

        private void FrmVehicleMountedList_Load(object sender, EventArgs e)
        {
            PDALoad();
            StorageType();
            winFormPager1.OnPageChanged += new EventHandler(winFormPager1_OnPageChanged);
            dataGridView1.KeyDown += new KeyEventHandler(dataGridView1_KeyDown);
            MountedListLoad();
            AutoSizeColumn(dataGridView1);
        }
        public void winFormPager1_OnPageChanged(object sender, EventArgs e)
        {
            MountedListLoad();
        }
        public void MountedListLoad()
        {
            try
            {
                string pageIndexAndCount = winFormPager1.PageIndex + "." + winFormPager1.PageSize;
                ResultModelOfArrayOfModel_ColdstoragePDAd4FqxSXX coldpdalist = cs.GetColdstoragePDAList(0, true, 2, true, pageIndexAndCount);
                ResultModelOfArrayOfModel_ColdstoragePDAd4FqxSXX count = cs.GetColdstoragePDAList(0, true, 2, true, null);
                winFormPager1.DrawControl(count.Data.Count<Model_ColdstoragePDA>());
                if (count.Data.Count<Model_ColdstoragePDA>() <= 0)
                    contextMenuStrip1.Enabled = false;
                else
                    contextMenuStrip1.Enabled = true;
                if (coldpdalist.Code != 0)
                {
                    MessageBox.Show(coldpdalist.Message);
                }
                else
                {
                    dataGridView1.AutoGenerateColumns = false;
                    dataGridView1.Rows.Clear();
                    foreach (Model_ColdstoragePDA item in coldpdalist.Data)
                    {
                        int rowIndex = dataGridView1.Rows.Add();
                        dataGridView1.Rows[rowIndex].Cells[0].Value = item.StorageIdk__BackingField;
                        dataGridView1.Rows[rowIndex].Cells[1].Value = item.StorageNamek__BackingField;
                        dataGridView1.Rows[rowIndex].Cells[2].Value = item.Driverk__BackingField;
                        dataGridView1.Rows[rowIndex].Cells[3].Value = item.DriverTelk__BackingField;
                        dataGridView1.Rows[rowIndex].Cells[4].Value = item.Remarkk__BackingField; 
                        dataGridView1.Rows[rowIndex].Cells[5].Value = item.StorageCreateAtk__BackingField.ToString("yyyy-MM-dd HH:mm:ss");
                        dataGridView1.Rows[rowIndex].Cells[6].Value = item.Namek__BackingField;
                        dataGridView1.Rows[rowIndex].Tag = item;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// pda设备
        /// </summary>
        public void PDALoad()
        {
            try
            {
                ResultModelOfArrayOfModel_PDAInfod4FqxSXX pda = cs.GetPDAList(0, true, null);
                if (pda.Code != 0)
                {
                    MessageBox.Show(pda.Message);
                }
                else
                {
                    cmbPDA.DisplayMember = "namek__BackingField";
                    cmbPDA.ValueMember = "idk__BackingField";
                    cmbPDA.DataSource = pda.Data;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 冷库/车载
        /// </summary>
        public void StorageType()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Value");
            dt.Columns.Add("Name");
            DataRow dr = dt.NewRow();
            //dr[0] = 1;
            //dr[1] = "冷库";
            //dt.Rows.Add(dr);
            //dr = dt.NewRow();
            dr[0] = 2;
            dr[1] = "车载";
            dt.Rows.Add(dr);
            cmbStorageType.DisplayMember = "Name";
            cmbStorageType.ValueMember = "Value";
            cmbStorageType.DataSource = dt;
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtLicensePlate.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("车载系统车牌不能为空");
                    return;
                }
                if (Encoding.Default.GetBytes(txtLicensePlate.Text.Trim()).Length>100)
                {
                    MessageBox.Show("车载系统车牌不能超过100");
                    return;
                }
                if (Encoding.Default.GetBytes(txtPilot.Text.Trim()).Length>50)
                {
                    MessageBox.Show("车载驾驶员不能超过50");
                    return;
                }
                if (cmbPDA.Text==string.Empty)
                {
                    MessageBox.Show("PDA设备不能为空");
                    return;
                }
                if (Encoding.Default.GetBytes(txtTelephone.Text.Trim()).Length>50)
                {
                    MessageBox.Show("驾驶员电话不能超过50");
                    return;
                }
                if (txtTelephone.Text.Trim() != string.Empty)
                {
                    if (!IsTelephone(txtTelephone.Text.Trim()))
                    {
                        MessageBox.Show("你输入不是手机/电话号码！");
                        return;
                    }
                }
                if (cmbStorageType.Text==string.Empty)
                {
                    MessageBox.Show("存储类型不能为空");
                    return;
                }
                if (Encoding.Default.GetBytes(txtRemark.Text.Trim()).Length>200)
                {
                    MessageBox.Show("备注不能超过200");
                    return;
                }
                Model_ColdStorage cold = new Model_ColdStorage();

                if (cmbStorageType.Text == "冷库")
                    cold.StorageTypek__BackingField = Enum_StorageType.ColdStorage;
                else
                    cold.StorageTypek__BackingField = Enum_StorageType.CarStorage;
                if (IsVehicleNumber(txtLicensePlate.Text.Trim()))
                    cold.StorageNamek__BackingField = txtLicensePlate.Text.Trim();
                else
                {
                    MessageBox.Show("你输入的不是车牌号");
                    return;
                }
                cold.Driverk__BackingField = txtPilot.Text.Trim();
                cold.DriverTelk__BackingField = txtTelephone.Text.Trim();
                cold.Remarkk__BackingField = txtRemark.Text.Trim();
                cold.CreateAtk__BackingField = DateTime.Now;
                if (rdbEnabled.Checked == true)
                    cold.Activedk__BackingField = Enum_Active.Enabled;
                else
                    cold.Activedk__BackingField = Enum_Active.Disable;

                int PDAid = Convert.ToInt32(cmbPDA.SelectedValue);
                int DefaultDevice;
                if (checDefault.Checked == true)
                {
                        DefaultDevice = (int)Enum_IsDefault.Eefault;
                        Model_Storage_Device sto = new Model_Storage_Device();
                        sto.deviceIdk__BackingField = PDAid;
                        sto.isDefaultk__BackingField = DefaultDevice;
                        ResultModelOfModel_Storage_Deviced4FqxSXX st = cs.GetDefaultDevice(sto);
                        if (st.Code != 0)
                            MessageBox.Show("操作失败");
                    
                }
                else
                    DefaultDevice = (int)Enum_IsDefault.NotDefault;
                ResultModelOfModel_ColdStoraged4FqxSXX coldstorage = cs.EditColdstorage(cold, DefaultDevice, true, 0, true, PDAid, true, false, true);
                if (coldstorage.Code != 0)
                {
                    MessageBox.Show(coldstorage.Message);
                }
                else
                {
                    MessageBox.Show("操作成功");
                    qingkong();
                    MountedListLoad();
                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        public void qingkong()
        {
            txtLicensePlate.Text = string.Empty;
            txtPilot.Text = string.Empty;
            txtTelephone.Text = string.Empty;
            txtRemark.Text = string.Empty;
            rdbEnabled.Checked = true;
            checDefault.Checked = true;
        }
        //验证电话号码的主要代码如下：
        public bool IsTelephone(string txtPhone)
        {
            //验证电话号码
            bool Telephone = System.Text.RegularExpressions.Regex.IsMatch(txtPhone, @"^(\d{3,4}-)?\d{6,8}$");
            bool Telephones = System.Text.RegularExpressions.Regex.IsMatch(txtPhone, @"^(\d{3,4})?\d{6,8}$");
            // 验证手机号码
            bool Handset = System.Text.RegularExpressions.Regex.IsMatch(txtPhone, @"^[1]+[3,5]+\d{9}");
            if (Telephone || Handset || Telephones)
                return true;
            else
                return false;
        }
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //    //    DateTime time = (DateTime)dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
            //    //    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = time.ToString("yyyy-MM-dd HH:mm:ss");
            //    //Model_ColdStorage pdaid = dataGridView1.SelectedRows[0].DataBoundItem as Model_ColdStorage;
            
        }
        /// <summary>
        /// 探头管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmProbes_Click(object sender, EventArgs e)
        {
            Model_ColdstoragePDA coldpda = dataGridView1.SelectedRows[0].Tag as Model_ColdstoragePDA;
            FrmVMProbeList probe = new FrmVMProbeList();
            probe.mcp = coldpda;
            probe.ShowDialog();
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmUpdated_Click(object sender, EventArgs e)
        {
            Model_ColdstoragePDA coldpad = dataGridView1.SelectedRows[0].Tag as Model_ColdstoragePDA;
            FrmVehicleUpdate upda = new FrmVehicleUpdate();
            upda.coldpda = coldpad;
            upda._Parentmounted = this;
            upda.ShowDialog();
        }
        public void ColdPDALoad()
        {
            MountedListLoad();
        }

        private void txtLicensePlate_KeyUp(object sender, KeyEventArgs e)
        {
            //txtWarehouseName.Text.Contains("[");
            //if (Keys.OemOpenBrackets != e.KeyCode)
            //{
            //    e.Handled = true;
            //}
            //else
            //{
            //    MessageBox.Show("不能输入中括号");
            //}
        }

        private void txtLicensePlate_KeyPress(object sender, KeyPressEventArgs e)
        {
        }
        /// <summary>
        /// 验证车牌号
        /// </summary>
        /// <param name="vehicleNumber"></param>
        /// <returns></returns>
        public static bool IsVehicleNumber(string vehicleNumber)
        {
            string express = @"^[京津沪渝冀豫云辽黑湘皖鲁新苏浙赣鄂桂甘晋蒙陕吉闽贵粤青藏川宁琼使领A-Z]{1}[A-Z]{1}[警京津沪渝冀豫云辽黑湘皖鲁新苏浙赣鄂桂甘晋蒙陕吉闽贵粤青藏川宁琼]{0,1}[A-Z0-9]{4}[A-Z0-9挂学警港澳]{1}$";
            return Regex.IsMatch(vehicleNumber, express);
        }

        private void dataGridView1_KeyDown(object sender,KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
                e.Handled = true;
        }

        //使用acceptbutton属性实现回车事件，会影响分页回车事件，
        //在首次按下某个键时发生回车事件
        private void txtEnter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(null, null);
            }
        }

        private void tsmColdChainData_Click(object sender, EventArgs e)
        {
            Model_ColdstoragePDA coldstoragepda = dataGridView1.SelectedRows[0].Tag as Model_ColdstoragePDA;
            FrmWarehouseControl.ForColdChainData data = new FrmWarehouseControl.ForColdChainData();
            data.coldstoragepda = coldstoragepda;
            data.ShowDialog();
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
