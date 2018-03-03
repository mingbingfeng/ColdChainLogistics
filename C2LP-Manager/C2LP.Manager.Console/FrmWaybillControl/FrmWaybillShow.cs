using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using C2LP.Manager.Console.ConsoleServerWebReference;

namespace C2LP.Manager.Console.FrmWaybillControl
{
    public partial class FrmWaybillShow : Form
    {
        public FrmWaybillShow()
        {
            InitializeComponent();
        }
        ConsoleServerWebReference.ConsoleServer cs = new ConsoleServerWebReference.ConsoleServer();
        public Model_Waybill_Base waybill_base;
        private void FrmWaybillShow_Load(object sender, EventArgs e)
        {
            winFormPager1.OnPageChanged += new EventHandler(winFormPager1_OnPageChanged);
            WaybillLoad();
            WaybillNodeLoad();
            AutoSizeColumn(dgvWaybill_Node);
        }
        //显示运单详细
        public void WaybillLoad()
        {
            lbMailUnit.Text = waybill_base.SenderOrgk__BackingField;
            lbSender.Text = waybill_base.SenderPersonk__BackingField;
            lbTelephone.Text = waybill_base.SenderTelk__BackingField;
            lbConsigneeUnit.Text = waybill_base.ReceiverOrgk__BackingField;
            lbConsignee.Text = waybill_base.ReceiverPersonk__BackingField;
            lbPhone.Text = waybill_base.ReceiverTelk__BackingField;
            lbConsigneeAddress.Text = waybill_base.ReceiverAddressk__BackingField;
            if (waybill_base.Stagek__BackingField == Enum_WaybillStage.Transporting)
                lbWaybillState.Text = "运输中";
            else 
                lbWaybillState.Text = "已签收";
            
            lbWaybillNumber.Text = waybill_base.Numberk__BackingField;
        }
        public void winFormPager1_OnPageChanged(object sender, EventArgs e)
        {
            WaybillNodeLoad();
        }
        //显示物流节点信息
        public void WaybillNodeLoad()
        {
            try
            {
                if (waybill_base.Idk__BackingField.ToString() == null) return;
                string pageIndexAndCount=winFormPager1.PageIndex + "." + winFormPager1.PageSize;
                ResultModelOfArrayOfModel_Waybill_Noded4FqxSXX waybill_node = cs.GetWaybillNodeList(waybill_base.Idk__BackingField.ToString(),null, pageIndexAndCount);
                ResultModelOfArrayOfModel_Waybill_Noded4FqxSXX count = cs.GetWaybillNodeList(waybill_base.Idk__BackingField.ToString(), null, null);
                winFormPager1.DrawControl(count.Data.Count<Model_Waybill_Node>());
                if (waybill_node.Code != 0)
                {
                    MessageBox.Show(waybill_node.Message);
                }
                else
                {
                    dgvWaybill_Node.AutoGenerateColumns = false;
                    dgvWaybill_Node.Rows.Clear();
                    Model_Waybill_Node node = new Model_Waybill_Node();
                    foreach (Model_Waybill_Node item in waybill_node.Data)
                    {
                        int rowIndex = dgvWaybill_Node.Rows.Add();
                        dgvWaybill_Node.Rows[rowIndex].Cells[0].Value = item.operateAtk__BackingField.ToString("yyyy-MM-dd HH:mm:ss"); ;
                        //node.Contentk__BackingField = "【" + item.StorageNamek__BackingField + "】" + item.Contentk__BackingField;
                        node.Contentk__BackingField =  item.Contentk__BackingField;
                        dgvWaybill_Node.Rows[rowIndex].Cells[1].Value = node.Contentk__BackingField;

                        dgvWaybill_Node.Rows[rowIndex].Tag = item;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void dgvWaybill_Node_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //Model_Waybill_Node node = dgvWaybill_Node.SelectedRows[0].Tag as Model_Waybill_Node;
            //if (node.Arrivedk__BackingField == Enum_Arrived.InTransit)
            //{
            //    tsmColdChainData.Enabled = true;
            //    tsmSignPicture.Enabled = false;
            //}
            //else 
            //{
            //    tsmColdChainData.Enabled = false;
            //    tsmSignPicture.Enabled = true;
            //}
        }
        /// <summary>
        /// 冷链数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmColdChainData_Click(object sender, EventArgs e)
        {
            try
            {
                Model_Waybill_Node node = dgvWaybill_Node.SelectedRows[0].Tag as Model_Waybill_Node;
                //获取开始时间的下面的时间
                ResultModelOfArrayOfModel_Waybill_Noded4FqxSXX nodedate = cs.GetWaybillNodeList(node.BaseIdk__BackingField.ToString(), node.operateAtk__BackingField.ToString(), null);
                if (nodedate.Code != 0)
                {
                    MessageBox.Show(nodedate.Message);
                }
                else
                {
                    if (nodedate.Data.Length <= 0)
                    {
                        MessageBox.Show("没有下一条物流节点....");
                        return;
                    }
                }
                Model_Waybill_Base waybase = waybill_base;
                FrmLogisticsNodeSuperView nodesuper = new FrmLogisticsNodeSuperView();
                nodesuper.waybase = waybase;
                nodesuper.node = node;
                nodesuper.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 签收图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmSignPicture_Click(object sender, EventArgs e)
        {
            Model_Waybill_Base waybase = waybill_base;
            Model_Waybill_Node node = dgvWaybill_Node.SelectedRows[0].Tag as Model_Waybill_Node;
            FrmSignPicture sp = new FrmSignPicture();
            sp.mwb = waybase;
            sp.mwn = node;
            sp.ShowDialog();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            ResultModelOfArrayOfModel_Waybill_Noded4FqxSXX count = cs.GetWaybillNodeList(waybill_base.Idk__BackingField.ToString(), null, null);
            if (count.Data.Count<Model_Waybill_Node>() <= 0)
            {
                contextMenuStrip1.Enabled = false;
                return;
            }
            Model_Waybill_Node node = dgvWaybill_Node.SelectedRows[0].Tag as Model_Waybill_Node;
            if (node.Arrivedk__BackingField == Enum_Arrived.InTransit)
            {
                tsmColdChainData.Enabled = true;
                tsmSignPicture.Enabled = false;
            }
            else
            {
                tsmColdChainData.Enabled = false;
                tsmSignPicture.Enabled = true;
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
        /// <summary>
        /// 刷新节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefurbish_Click(object sender, EventArgs e)
        {
            WaybillNodeLoad();
        }
    }
}
