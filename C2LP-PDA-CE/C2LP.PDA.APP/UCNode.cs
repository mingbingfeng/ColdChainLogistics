using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using C2LP.PDA.APP.PDAWebReference;

namespace C2LP.PDA.APP
{
    public partial class UCNode : UserControl
    {

        public UCNode(Model_Waybill_Node node)
        {
            InitializeComponent();

            SetNumber(node.BaseIdk__BackingField);
            lblOperateAt.Text = node.operateAtk__BackingField.ToString("yyyy-MM-dd HH:mm");
            lblNodeContent.Text = node.Contentk__BackingField;
        }
        //private int MyHeight = 73;

        //private void UCNode_Resize(object sender, EventArgs e)
        //{
        //    lblNodeContent.Height += this.Height - MyHeight;
        //}

        private void SetNumber(string txt)
        {
            while (txt.Length > 3 || (txt.Length < 6 && txt.Length > 3))
            {
                txtNodeNumber.Text += txt[0];
                txt = txt.Remove(0, 1);
                if (txtNodeNumber.Text.Replace(" ","").Length % 3 == 0)
                    txtNodeNumber.Text += " ";
            }
            txtNodeNumber.Text += txt;
        }
    }
}
