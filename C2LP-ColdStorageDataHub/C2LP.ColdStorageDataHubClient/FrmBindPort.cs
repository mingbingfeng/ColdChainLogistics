using C2LP.ColdStorageDataHubClient.DBHelper.BLL;
using C2LP.ColdStorageDataHubClient.DBHelper.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace C2LP.ColdStorageDataHubClient
{
    public partial class FrmBindPort : Form
    {
        public FrmBindPort()
        {
            InitializeComponent();
            LoadTreeView();
        }
        private TreeNode refNodes;
        private TreeNode carNodes;
        //private TreeNode boxNodes;
        private void LoadTreeView()
        {
            //本系统的仓库点
            List<TbccRefAiInfo> refPrjList = AiInfoServer.GetRefAiInfo();
            //本系统的车载点
            List<TbccCarPrjType> carPrjList = AiInfoServer.GetCarAiInfo();
            //本系统的保温箱点
            //List<TbccBoxPrjType> boxPrjList = AiInfoServer.GetBoxAiInfo();

            refNodes = tvAiList.Nodes.Add("仓库");
            carNodes = tvAiList.Nodes.Add("车载");
            //boxNodes = tvAiList.Nodes.Add("小批零");

            if (refPrjList != null)
            {
                ILookup<string, TbccRefAiInfo> refLook = refPrjList.ToLookup(l => l.ProjectID);
                foreach (var prjItem in refLook)
                {
                    TreeNode prjNode = refNodes.Nodes.Add(prjItem.Key);
                    ILookup<int, TbccRefAiInfo> netLook = prjItem.ToLookup(l => l.NetId);
                    foreach (var netItem in netLook)
                    {
                        TreeNode netNode = prjNode.Nodes.Add(netItem.Key.ToString());
                        ILookup<int, TbccRefAiInfo> aiLook = netItem.ToLookup(l => l.RefId);
                        foreach (var aiItem in aiLook)
                        {
                            TreeNode refNode = netNode.Nodes.Add(AiInfoServer.GetRefName(prjItem.Key, netItem.Key, aiItem.Key));
                            refNode.Tag = aiItem.ToList();
                            //foreach (TbccRefAiInfo ai in aiItem)
                            //{
                            //    TreeNode aiNode = refNode.Nodes.Add(ai.PortName);
                            //    aiNode.Tag = ai;
                            //    aiNode.ToolTipText = "PortNo:" + ai.PortNo;
                            //}
                        }
                    }
                }
            }

            foreach (TbccCarPrjType carItem in carPrjList)
            {
                TreeNode carNode = carNodes.Nodes.Add(carItem.CarProjectName);
                carNode.ToolTipText = "ProjectNo:" + carItem.CarProjectId;
                carNode.Tag = carItem;
                //TreeNode carAi1 = carNode.Nodes.Add("AI1");
                //TreeNode carAi2 = carNode.Nodes.Add("AI2");
                //TreeNode carAi3 = carNode.Nodes.Add("AI3");

                //carAi1.Tag = new TbccCarPrjType() { CarProjectId = carItem.CarProjectId, SelectPortNo = 1 };
                //carAi2.Tag = new TbccCarPrjType() { CarProjectId = carItem.CarProjectId, SelectPortNo = 2 };
                //carAi3.Tag = new TbccCarPrjType() { CarProjectId = carItem.CarProjectId, SelectPortNo = 3 };
            }
            tvAiList.ExpandAll();
            //foreach (TbccBoxPrjType boxItem in boxPrjList)
            //{
            //    TreeNode boxNode = boxNodes.Nodes.Add(boxItem.BoxProjectName);
            //    boxNode.ToolTipText = "ProjectNo:" + boxItem.BoxProjectId;
            //    TreeNode boxAi1 = boxNode.Nodes.Add("AI1");
            //    boxAi1.Tag = new TbccBoxPrjType() { BoxProjectId = boxItem.BoxProjectId, SelectPortNo = 1 };
            //}
            //SetTreeNodeVisib();
        }

        private TreeNode _LastSelectNode = null;
        private void tvAiList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (_LastSelectNode == tvAiList.SelectedNode)
                return;
            if (isUpdate)
            {
                //if (MessageBox.Show("当前配置尚未保存，确定要切换到其他库吗？", "操作提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                //    isUpdate = false;
                //}
                //else
                //    tvAiList.SelectedNode = _LastSelectNode;
                MessageBox.Show("请先保存当前配置！","操作提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                tvAiList.SelectedNode = _LastSelectNode;
            }
            dgvBindList.Rows.Clear();
            if (tvAiList.SelectedNode == null || tvAiList.SelectedNode.Tag == null)
                return;
            if (tvAiList.SelectedNode.Tag is TbccCarPrjType)
            {
                TbccCarPrjType carItem = tvAiList.SelectedNode.Tag as TbccCarPrjType;
                AiInfoModel aim1 = GetBindValue(carItem.CarProjectId, 0, 0, 1);
                AiInfoModel aim2 = GetBindValue(carItem.CarProjectId, 0, 0, 2);
                AiInfoModel aim3 = GetBindValue(carItem.CarProjectId, 0, 0, 3);
                AiInfoModel aim4 = GetBindValue(carItem.CarProjectId, 0, 0, 4);
                AiInfoModel aim5 = GetBindValue(carItem.CarProjectId, 0, 0, 5);
                AiInfoModel aim6 = GetBindValue(carItem.CarProjectId, 0, 0, 6);
                dgvBindList.Rows[dgvBindList.Rows.Add(tvAiList.SelectedNode.Text, "AI1", aim1.aiNumber)].Tag = aim1;
                dgvBindList.Rows[dgvBindList.Rows.Add(tvAiList.SelectedNode.Text, "AI2", aim2.aiNumber)].Tag = aim2;
                dgvBindList.Rows[dgvBindList.Rows.Add(tvAiList.SelectedNode.Text, "AI3", aim3.aiNumber)].Tag = aim3;
                dgvBindList.Rows[dgvBindList.Rows.Add(tvAiList.SelectedNode.Text, "AI4", aim4.aiNumber)].Tag = aim4;
                dgvBindList.Rows[dgvBindList.Rows.Add(tvAiList.SelectedNode.Text, "经度", aim5.aiNumber)].Tag = aim5;
                dgvBindList.Rows[dgvBindList.Rows.Add(tvAiList.SelectedNode.Text, "纬度", aim6.aiNumber)].Tag = aim6;

            }
            else
            {
                List<TbccRefAiInfo> aiList = tvAiList.SelectedNode.Tag as List<TbccRefAiInfo>;
                foreach (var item in aiList)
                {
                    AiInfoModel aim = GetBindValue(item.ProjectID, item.NetId, item.RefId, item.PortNo);
                    int rowIndex = dgvBindList.Rows.Add(tvAiList.SelectedNode.Text, item.PortName, aim.aiNumber);
                    dgvBindList.Rows[rowIndex].Tag = aim;
                }
            }
            _LastSelectNode = tvAiList.SelectedNode;
        }

        private AiInfoModel GetBindValue(string projectId, int netId, int refId, float portId)
        {
            AiInfoModel aim = FrmMain._relationList.Find(a => a.LinkProjectNo == projectId && a.LinkRefId == refId && a.LinkNetId == netId && a.LinkPortNo == portId);
            if (aim != null)
                return aim;
            else
                return new AiInfoModel() { aiNumber = 0, LinkPortNo = portId, LinkProjectNo = projectId, LinkNetId = netId, LinkRefId = refId };

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool isUpdate = false;
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.Enabled = false;
                if (isUpdate)
                {
                    string projectId = string.Empty;
                    int netId = 0;
                    int refId = 0;
                    if (tvAiList.SelectedNode.Tag is TbccCarPrjType)
                    {
                        TbccCarPrjType carItem = tvAiList.SelectedNode.Tag as TbccCarPrjType;
                        projectId = carItem.CarProjectId;
                    }
                    else
                    {
                        List<TbccRefAiInfo> aiList = tvAiList.SelectedNode.Tag as List<TbccRefAiInfo>;
                        if (aiList.Count == 0)
                        {
                            isUpdate = false;
                            return;
                        }
                        projectId = aiList[0].ProjectID;
                        netId = aiList[0].NetId;
                        refId = aiList[0].RefId;
                    }
                    AiInfoServer.InsertAiRelation(FrmMain._relationList, projectId, netId, refId);
                    MessageBox.Show("保存成功！");
                    isUpdate = false;
                    tvAiList_AfterSelect(sender, null);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "保存失败");
            }
            finally
            {
                this.Enabled = true;
            }
        }

        private void dgvBindList_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.RowIndex >= 0)
            {
                int tempNumber = 0;
                AiInfoModel aim = dgvBindList.Rows[e.RowIndex].Tag as AiInfoModel;
                if (!int.TryParse(dgvBindList[e.ColumnIndex, e.RowIndex].Value.ToString(), out tempNumber))
                {
                    dgvBindList[e.ColumnIndex, e.RowIndex].Value = aim.aiNumber;
                    return;
                }
                if (tempNumber == aim.aiNumber)
                    return;
                if (tempNumber != 0)
                {
                    AiInfoModel tempAim = FrmMain._relationList.Find(a => a.aiNumber == tempNumber);
                    if (tempAim != null)
                    {
                        MessageBox.Show(string.Format("该编号已被使用 ProjectId:[{0}] NetId:[{1}] RefId:[{2}] PortId[{3}]", tempAim.LinkProjectNo, tempAim.LinkNetId, tempAim.LinkRefId, tempAim.LinkPortNo),"操作提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dgvBindList[e.ColumnIndex, e.RowIndex].Value = aim.aiNumber;
                        return;
                    }
                }
                isUpdate = true;
                aim.aiNumber = tempNumber;
                dgvBindList.Rows[e.RowIndex].Tag = aim;
                if (!FrmMain._relationList.Contains(aim))
                    FrmMain._relationList.Add(aim);
            }
        }

        private void FrmBindPort_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isUpdate)
            {
                MessageBox.Show("请先保存当前配置！", "操作提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
        }
    }
}
