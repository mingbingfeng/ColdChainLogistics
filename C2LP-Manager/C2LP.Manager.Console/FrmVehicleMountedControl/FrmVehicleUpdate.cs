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
    public partial class FrmVehicleUpdate : Form
    {
        ConsoleServerWebReference.ConsoleServer cs = new ConsoleServerWebReference.ConsoleServer();
        public Model_ColdstoragePDA coldpda;
        public FrmVehicleMountedList _Parentmounted;
        public FrmVehicleUpdate()
        {
            InitializeComponent();
        }

        private void FrmVehicleUpdate_Load(object sender, EventArgs e)
        {
            PDALoad();
            StorageType();
            Display();
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

        public void Display()
        {
            txtLicensePlate.Text = coldpda.StorageNamek__BackingField;
            txtPilot.Text = coldpda.Driverk__BackingField;
            txtTelephone.Text = coldpda.DriverTelk__BackingField;
            txtRemark.Text = coldpda.Remarkk__BackingField;
            cmbPDA.Text = coldpda.Namek__BackingField;
            if (coldpda.StorageTypek__BackingField == Enum_StorageType.ColdStorage)
                cmbStorageType.Text = "冷库";
            else
                cmbStorageType.Text = "车载";
            if (coldpda.isDefaultk__BackingField == Enum_IsDefault.Eefault)
                checDefault.Checked = true;
            else
                checDefault.Checked = false;
            if (coldpda.StorageActivedk__BackingField == Enum_Active.Enabled)
                rdbEnabled.Checked = true;
            else
                rdbDisable.Checked = true;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtLicensePlate.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("车载系统车牌不能为空");
                    return;
                }
                if (Encoding.Default.GetBytes(txtLicensePlate.Text.Trim()).Length > 100)
                {
                    MessageBox.Show("车载系统车牌不能超过100");
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
                if (Encoding.Default.GetBytes(txtPilot.Text.Trim()).Length > 50)
                {
                    MessageBox.Show("车载驾驶员不能超过50");
                    return;
                }
                if (Encoding.Default.GetBytes(txtTelephone.Text.Trim()).Length > 50)
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
                if (Encoding.Default.GetBytes(txtRemark.Text.Trim()).Length > 200)
                {
                    MessageBox.Show("备注不能超过200");
                    return;
                }
                Model_ColdStorage coldpdalist = new Model_ColdStorage();
                coldpdalist.Idk__BackingField = coldpda.StorageIdk__BackingField;
                if (IsVehicleNumber(txtLicensePlate.Text.Trim()))
                    coldpdalist.StorageNamek__BackingField = txtLicensePlate.Text;
                else
                {
                    MessageBox.Show("你输入的不是车牌号");
                    return;
                }
                if (cmbStorageType.Text == "冷库")
                    coldpdalist.StorageTypek__BackingField = Enum_StorageType.ColdStorage;
                else
                    coldpdalist.StorageTypek__BackingField = Enum_StorageType.CarStorage;
                coldpdalist.Driverk__BackingField = txtPilot.Text;
                coldpdalist.DriverTelk__BackingField = txtTelephone.Text;
                coldpdalist.Remarkk__BackingField = txtRemark.Text;
                if (rdbEnabled.Checked == true)
                    coldpdalist.Activedk__BackingField = Enum_Active.Enabled;
                else
                    coldpdalist.Activedk__BackingField = Enum_Active.Disable;
                int pdaid = Convert.ToInt32(cmbPDA.SelectedValue);
                int isDefault;
                if (checDefault.Checked == true)
                {
                    //if (MessageBox.Show("是否默认该设备?", "Prompt", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    //{
                        isDefault = (int)Enum_IsDefault.Eefault;
                        Model_Storage_Device sto = new Model_Storage_Device();
                        sto.deviceIdk__BackingField = pdaid;
                        sto.isDefaultk__BackingField = isDefault;
                        ResultModelOfModel_Storage_Deviced4FqxSXX st = cs.GetDefaultDevice(sto);
                        if (st.Code != 0)
                            MessageBox.Show("操作失败");
                    //}
                    //else
                    //    isDefault = (int)Enum_IsDefault.NotDefault;
                }
                else
                    isDefault = (int)Enum_IsDefault.NotDefault;
                ResultModelOfModel_ColdStoraged4FqxSXX edit = cs.EditColdstorage(coldpdalist, isDefault, true, coldpda.StorageDeviceIdk__BackingField, true, pdaid, true, true, true);
                if (edit.Code != 0)
                {
                    MessageBox.Show(edit.Message);
                }
                else
                {
                    MessageBox.Show("操作成功");
                    _Parentmounted.ColdPDALoad();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
                
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
    }
}
