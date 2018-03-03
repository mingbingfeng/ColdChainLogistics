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
    public partial class Prompt : Form
    {
        public Prompt()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            SetText("正在努力的处理您的请求，请稍等...");
        }
         delegate void SetTextHandler(string text);
        public void SetText(string text)
        {
            if (this.label1.InvokeRequired)
            {
                this.Invoke(new SetTextHandler(SetText), text);
            }
            else
            {
                this.label1.Text = text;
            }
        }

    }
}
