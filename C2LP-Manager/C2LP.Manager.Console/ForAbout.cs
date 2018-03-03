using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace C2LP.Manager.Console
{
    public partial class ForAbout : Form
    {
        public ForAbout()
        {
            InitializeComponent();
        }

        private void ForAbout_Load(object sender, EventArgs e)
        {
            LabelProductName.Text = "产品名称："+"惊尘冷链物流追溯系统";
            LabelVersion.Text = "版本："+"1.1.2";
            LabelCopyright.Text = "版权："+ "宁波惊尘冷链物流公司";
            LabelCompanyName.Text = "公司名称："+ "宁波惊尘冷链物流公司";

        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
