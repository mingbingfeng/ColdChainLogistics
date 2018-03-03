using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using C2LP.Manager.Console.ConsoleServerWebReference;
using C2LP.Manager.Console.FrmVehicleMountedControl;

namespace C2LP.Manager.Console.FrmWarehouseControl
{
    public partial class FrmWarehouseList : Form
    {
        ConsoleServerWebReference.ConsoleServer cs = new ConsoleServerWebReference.ConsoleServer();
        public FrmWarehouseList()
        {
            InitializeComponent();
        }

        private void FrmWarehouseList_Load(object sender, EventArgs e)
        {
            PDALoad();
            StorageType();
            winFormPager1.OnPageChanged += new EventHandler(winFormPager1_OnPageChanged);
            dataGridView1.KeyDown += new KeyEventHandler(dataGridView1_KeyDown);
            WarehouseLoad();
            AutoSizeColumn(dataGridView1);
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

        public void StorageType()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Value");
            dt.Columns.Add("Name");
            DataRow dr = dt.NewRow();
            dr[0] = 1;
            dr[1] = "冷库";
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            //dr[0] = 2;
            //dr[1] = "车载";
            //dt.Rows.Add(dr);

            cmbStorageType.DisplayMember = "Name";
            cmbStorageType.ValueMember = "Value";
            cmbStorageType.DataSource = dt;
        }
        public void winFormPager1_OnPageChanged(object sender,EventArgs e)
        {
            WarehouseLoad();
        }
        public void WarehouseLoad()
        {
            try
            {
                string pageIndexAndCount = winFormPager1.PageIndex + "." + winFormPager1.PageSize;
                ResultModelOfArrayOfModel_ColdstoragePDAd4FqxSXX wh = cs.GetColdstoragePDAList(0, true, 1, true, pageIndexAndCount);
                ResultModelOfArrayOfModel_ColdstoragePDAd4FqxSXX count = cs.GetColdstoragePDAList(0, true, 1, true, null);
                winFormPager1.DrawControl(count.Data.Count<Model_ColdstoragePDA>());
                if (count.Data.Count<Model_ColdstoragePDA>() <= 0)
                    contextMenuStrip1.Enabled = false;
                else
                    contextMenuStrip1.Enabled = true;
                if (wh.Code!=0)
                {
                    MessageBox.Show(wh.Message);
                }
                else
                {
                    dataGridView1.AutoGenerateColumns = false;
                    dataGridView1.Rows.Clear();
                    foreach (Model_ColdstoragePDA item in wh.Data)
                    {
                        int rowIndex = dataGridView1.Rows.Add();
                        dataGridView1.Rows[rowIndex].Cells[0].Value = item.StorageIdk__BackingField;
                        dataGridView1.Rows[rowIndex].Cells[1].Value = item.StorageNamek__BackingField;
                        dataGridView1.Rows[rowIndex].Cells[2].Value = item.Namek__BackingField;
                        dataGridView1.Rows[rowIndex].Cells[3].Value = item.StorageCreateAtk__BackingField.ToString("yyyy-MM-dd HH:mm:ss");
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
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtWarehouseName.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("仓库名称不能为空");
                    return;
                }
                if (Encoding.Default.GetBytes(txtWarehouseName.Text.Trim()).Length>100)
                {
                    MessageBox.Show("仓库名称不能超过100");
                    return;
                }
                if (cmbPDA.Text.Trim()==string.Empty)
                {
                    MessageBox.Show("PDA设备不能为空");
                    return;
                }
                if (cmbStorageType.Text.Trim()==string.Empty)
                {
                    MessageBox.Show("存储类型不能为空");
                    return;
                }
                Model_ColdStorage mcs = new Model_ColdStorage();
                mcs.StorageNamek__BackingField = txtWarehouseName.Text.Trim();
                if (cmbStorageType.Text == "冷库")
                    mcs.StorageTypek__BackingField = Enum_StorageType.ColdStorage;
                else
                    mcs.StorageTypek__BackingField = Enum_StorageType.CarStorage;
                mcs.CreateAtk__BackingField = DateTime.Now;
                if (rdbEnabled.Checked == true)
                    mcs.Activedk__BackingField = Enum_Active.Enabled;
                else
                    mcs.Activedk__BackingField = Enum_Active.Disable;
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

                ResultModelOfModel_ColdStoraged4FqxSXX coldstoreage = cs.EditColdstorage(mcs, DefaultDevice, true, 0, true, PDAid, true, false, true);
                if (coldstoreage.Code != 0)
                {
                    MessageBox.Show(coldstoreage.Message);
                }
                else
                {
                    MessageBox.Show("操作成功");
                    getClear();
                    WarehouseLoad();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }
        public void getClear()
        {
            txtWarehouseName.Text = string.Empty;
            rdbEnabled.Checked = true;
            checDefault.Checked = true;
        }
        /// <summary>
        /// 探头管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmProbes_Click(object sender, EventArgs e)
        {
            Model_ColdstoragePDA coldpad = dataGridView1.SelectedRows[0].Tag as Model_ColdstoragePDA;
            FrmProbeList vmplist = new FrmProbeList();
            vmplist.mcp = coldpad;
            vmplist.ShowDialog();
        }
        /// <summary>
        /// 绑定PDA
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmBindingPDA_Click(object sender, EventArgs e)
        {
            Model_ColdstoragePDA coldpda = dataGridView1.SelectedRows[0].Tag as Model_ColdstoragePDA;
            FrmWarehouseBind warebind = new FrmWarehouseBind();
            warebind.mcp = coldpda;
            warebind._ParentWare = this;
            warebind.ShowDialog();
        }
        public void waLoad()
        {
            WarehouseLoad();
        }
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmUpdated_Click(object sender, EventArgs e)
        {
            Model_ColdstoragePDA coldpda = dataGridView1.SelectedRows[0].Tag as Model_ColdstoragePDA;
            FrmWarehouseUpdated upd = new FrmWarehouseUpdated();
            upd.mcp = coldpda;
            upd._ParentWare = this;
            upd.ShowDialog();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmDelete_Click(object sender, EventArgs e)
        {
            try
            {
                Model_ColdstoragePDA coldpda = dataGridView1.SelectedRows[0].Tag as Model_ColdstoragePDA;
                Model_ColdStorage coldstorage = new Model_ColdStorage();
                coldstorage.Idk__BackingField = coldpda.StorageIdk__BackingField;
                coldstorage.StorageNamek__BackingField = coldpda.StorageNamek__BackingField;
                coldstorage.StorageTypek__BackingField = coldpda.StorageTypek__BackingField;
                coldstorage.Driverk__BackingField = coldpda.Driverk__BackingField;
                coldstorage.DriverTelk__BackingField = coldpda.DriverTelk__BackingField;
                coldstorage.CreateAtk__BackingField = coldpda.StorageCreateAtk__BackingField;
                coldstorage.Remarkk__BackingField = coldpda.Remarkk__BackingField;
                coldstorage.Activedk__BackingField = Enum_Active.Disable;
                if (MessageBox.Show("是否确定删除？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    ResultModelOfModel_ColdStoraged4FqxSXX actiove = cs.EditColdstorage(coldstorage, Convert.ToInt32(coldpda.isDefaultk__BackingField), true, coldpda.StorageDeviceIdk__BackingField, true, coldpda.PDAIdk__BackingField, true, true, true);
                    if (actiove.Code != 0)
                    {
                        MessageBox.Show(actiove.Message);
                    }
                    else
                    {
                        MessageBox.Show("操作成功，冷库已停用");
                        WarehouseLoad();
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void txtWarehouseName_KeyUp(object sender, KeyEventArgs e)
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

        private void txtWarehouseName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 91 || e.KeyChar == 93)
                e.Handled = true;
            else
                e.Handled = false;
        }

        private void dataGridView1_KeyDown(object sender,KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
                e.Handled = true;
        }

        private void tsmColdChainData_Click(object sender, EventArgs e)
        {
            Model_ColdstoragePDA coldstoragepda = dataGridView1.SelectedRows[0].Tag as Model_ColdstoragePDA;
            ForColdChainData data = new ForColdChainData();
            data.coldstoragepda = coldstoragepda;
            data.ShowDialog();
        }

        //使用acceptbutton属性实现回车事件，会影响分页回车事件，
        //在首次按下某个键时发生回车事件
        private void txtEnter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSave_Click(null, null);
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
