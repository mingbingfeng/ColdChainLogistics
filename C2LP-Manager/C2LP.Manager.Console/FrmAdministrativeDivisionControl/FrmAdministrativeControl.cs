using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using C2LP.Manager.Console.ConsoleServerWebReference;

namespace C2LP.Manager.Console.FrmAdministrativeDivisionControl
{
    public partial class FrmAdministrativeControl : Form
    {
        ConsoleServerWebReference.ConsoleServer cs = new ConsoleServerWebReference.ConsoleServer();
        public FrmAdministrativeControl()
        {
            InitializeComponent();
        }
        private void FrmAdministrativeControl_Load(object sender, EventArgs e)
        {
            InitTvArea();
        }
        private void cmbProvince_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        private void AddChildNode()
        {
            try
            {
                //ResultModelOfArrayOfModel_Regiond4FqxSXX quan = cs.GetRegionInfo(0, true);
                ResultModelOfArrayOfModel_Regiond4FqxSXX quan = cs.GetRegionDateTime(0, true);
                if (quan.Code != 0)
                {
                    MessageBox.Show(quan.Message);
                }
                else
                {
                    treeView1.Nodes.Clear();
                    foreach (Model_Region dr in quan.Data)
                    {
                        //中国绑定，作为一级层次  
                        TreeNode tn_origine = new TreeNode();
                        tn_origine.Text = dr.Namek__BackingField;//绑定行政名称
                        tn_origine.Name = dr.Idk__BackingField.ToString();//绑定编号id
                        this.treeView1.Nodes.Add(tn_origine);
                        tn_origine.Tag = dr;
                        
                        //ResultModelOfArrayOfModel_Regiond4FqxSXX prov = cs.GetRegionInfo(dr.Idk__BackingField, true);
                        ResultModelOfArrayOfModel_Regiond4FqxSXX prov = cs.GetRegionDateTime(dr.Idk__BackingField, true);
                        if (prov.Code != 0)
                        {
                            MessageBox.Show(prov.Message);
                        }
                        else
                        {
                            for (int i = 0; i < prov.Data.Length; i++)
                            {
                                //省份绑定，作为二级层次  
                                TreeNode tn_prov = new TreeNode();
                                tn_prov.Text = prov.Data[i].Namek__BackingField;//绑定行政名称
                                tn_prov.Name = prov.Data[i].Idk__BackingField.ToString();//绑定编号id
                                tn_origine.Nodes.Add(tn_prov);
                                tn_prov.Tag = prov.Data[i];//tag绑定选中的省份节点信息

                                //ResultModelOfArrayOfModel_Regiond4FqxSXX city = cs.GetRegionInfo(prov.Data[i].Idk__BackingField, true);
                                ResultModelOfArrayOfModel_Regiond4FqxSXX city = cs.GetRegionDateTime(prov.Data[i].Idk__BackingField, true);
                                //城市绑定  三级
                                if (city.Data.Length > 0)
                                {
                                    for (int j = 0; j < city.Data.Length; j++)
                                    {
                                        TreeNode tn_sub = new TreeNode();
                                        tn_sub.Name = city.Data[j].Idk__BackingField.ToString();//绑定编号id
                                        tn_sub.Text = city.Data[j].Namek__BackingField;//绑定行政名称
                                        tn_prov.Nodes.Add(tn_sub);
                                        tn_sub.Tag = city.Data[j];//tag绑定的选中城市节点信息


                                    }
                                }
                            }

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
        /// 省市树形显示
        /// </summary>
        public void InitTvArea()
        {
            AddChildNode();
        }
        /// <summary>
        /// 查看选中行政区域信息的下一级信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkmenu_Click(object sender, EventArgs e)
        {
            try
            {
                //判断是否选中行政区域节点信息
                if (treeView1.SelectedNode == null)
                {
                    MessageBox.Show("请选择行政区域");
                    return;
                }
                if (treeView1.SelectedNode.Name != string.Empty && treeView1.SelectedNode.Text != string.Empty)
                {
                    Model_Region region = new Model_Region();
                    //MessageBox.Show(treeView1.SelectedNode.Name + "," + treeView1.SelectedNode.Text);
                    region.Idk__BackingField = Convert.ToInt32(treeView1.SelectedNode.Name);
                    region.Namek__BackingField = treeView1.SelectedNode.Text;
                    FrmSeeAdministrative see = new FrmSeeAdministrative();
                    see.region = region;
                    see.ShowDialog();
                }
                else
                {
                    MessageBox.Show("请选择行政区域");
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
        private void addmenu_Click(object sender, EventArgs e)
        {
            try
            {
                //判断是否选中行政区域节点信息
                if (treeView1.SelectedNode == null)
                {
                    MessageBox.Show("请选择行政区域");
                    return;
                }
                if (treeView1.SelectedNode.Name != string.Empty && treeView1.SelectedNode.Text != string.Empty)
                {
                    Model_Region region = new Model_Region();
                    region.Idk__BackingField = Convert.ToInt32(treeView1.SelectedNode.Name);
                    region.Namek__BackingField = treeView1.SelectedNode.Text;
                    FrmAdministrativeAE see = new FrmAdministrativeAE();
                    see._frmadministrativecontrol = this;
                    see.nanber = 1;//0修改，1添加,2修改
                    see.region = region;
                    see.ShowDialog();
                }
                else
                {
                    MessageBox.Show("请选择行政区域");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ecitmenu_Click(object sender, EventArgs e)
        {
            try
            {
                //判断是否选中行政区域节点信息
                if (treeView1.SelectedNode == null)
                {
                    MessageBox.Show("请选择行政区域");
                    return;
                }
                if (treeView1.SelectedNode.Name != string.Empty && treeView1.SelectedNode.Text != string.Empty)
                {
                    Model_Region region = treeView1.SelectedNode.Tag as Model_Region;
                    
                    FrmAdministrativeAE see = new FrmAdministrativeAE();
                    see._frmadministrativecontrol = this;
                    see.region = region;
                    see.nanber = 0;//0修改，1添加,2修改
                    see.ShowDialog();
                    
                }
                else
                {
                    MessageBox.Show("请选择行政区域");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void Refurbish()
        {
            AddChildNode();
        }

    }
}
