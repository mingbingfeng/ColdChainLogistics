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
    public partial class FrmAdministrativeAE : Form
    {
        ConsoleServerWebReference.ConsoleServer cs = new ConsoleServerWebReference.ConsoleServer();
        public FrmAdministrativeAE()
        {
            InitializeComponent();
        }
        public Model_Region region;
        public FrmAdministrativeControl _frmadministrativecontrol;
        public FrmSeeAdministrative _frmseeadministratve;
        public int nanber;//0修改，1添加,2查看页面修改
        private void FrmAdministrativeAE_Load(object sender, EventArgs e)
        {
            if (region != null)
            {
                if (nanber == 0 || nanber == 2)
                {
                    this.Text = region.Namek__BackingField + "行政区";
                    RegionShow();
                }
                else
                    this.Text = region.Namek__BackingField + "下级行政区";
            }
            
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            RegionEdit();
        }
        #region 显示行政
        
        public void RegionShow()
        {
            try
            {
                if (region != null)
                {
                    txtname.Text = region.Namek__BackingField;
                    txtcode.Text = region.Codek__BackingField;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void RegionEdit()
        {
            try
            {
                if (region!=null)
                {
                    if (RegionDate())
                    {
                        Model_Region regions = new Model_Region();
                        if (nanber == 0 || nanber==2)
                        {
                            //修改
                            regions.Idk__BackingField = region.Idk__BackingField;
                            regions.ParentIdk__BackingField = region.ParentIdk__BackingField;
                        }
                        else
                        {
                            //添加
                            regions.ParentIdk__BackingField = region.Idk__BackingField;
                        }
                        regions.Codek__BackingField = txtcode.Text.Trim();
                        regions.Namek__BackingField = txtname.Text.Trim();

                        regions.Levelk__BackingField = 0;
                        regions.Orderk__BackingField = 0;
                        regions.NameEnglishk__BackingField = string.Empty;
                        regions.NameShortEnglishk__BackingField = string.Empty;
                        ResultModelOfint regint = cs.RegionEdit(regions);
                        if (regint.Code != 0)
                        {
                            MessageBox.Show(regint.Message);
                        }
                        else
                        {
                            MessageBox.Show("操作成功");
                            if (nanber == 0 || nanber==1)
                                _frmadministrativecontrol.Refurbish();
                            if(nanber==2)
                                _frmseeadministratve.Refurbish();
                            this.Close();
                        }
                    } 
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public bool RegionDate()
        {
            bool result = true;
            if (txtname.Text.Trim() == string.Empty)
            {
                MessageBox.Show("行政区域的名称不能为空");
                return false;
            }
            if (txtcode.Text.Trim() == string.Empty)
            {
                MessageBox.Show("行政代码不能为空，不存在可以随意填写");
                return false;
            }
            return result;
        }
        #endregion
    }
}
