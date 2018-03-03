using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace C2LP.Manager.MyControls
{
    public partial class UCPaging : UserControl
    {
        /// <summary>
        /// 在需要查询新的数据时触发
        /// </summary>
        public event EventHandler Paging_SearchData;
        public UCPaging()
        {
            InitializeComponent();
            PageIndex = 1;
            PageSize = 2;
            nudPageIndex.GotFocus += NudPageIndex_GotFocus;
            nudPageIndex.LostFocus += NudPageIndex_LostFocus;
        }

        private void NudPageIndex_LostFocus(object sender, EventArgs e)
        {
            nudPageIndex.ReadOnly = true;
            RegistPaging_SearchData();
        }

        private void NudPageIndex_GotFocus(object sender, EventArgs e)
        {
            nudPageIndex.ReadOnly = false;
        }

        /// <summary>
        /// 页索引 从1开始
        /// </summary>
        public int PageIndex
        {
            get
            {
                return Convert.ToInt32(nudPageIndex.Value);
            }

            set
            {
                nudPageIndex.Value = value;
            }
        }

        /// <summary>
        /// 每页数量
        /// </summary>
        public int PageSize
        {
            get
            {
                return Convert.ToInt32(cboPageSize.Text);
            }

            set
            {
                PageCount = DataCount / PageSize;
                cboPageSize.Text = value.ToString();
            }
        }

        /// <summary>
        /// 总页数
        /// </summary>
        private int PageCount
        {
            get
            {
                return Convert.ToInt32(txtPageCount.Text);
            }

            set
            {
                txtPageCount.Text = value.ToString();
            }
        }

        public string PageIndexAndCount
        {
            get
            {
                string temp = string.Empty;
                return PageIndex + "." + PageCount;
            }
        }

        /// <summary>
        /// 数据总数量
        /// </summary>
        public int DataCount {
            get
            {
                return Convert.ToInt32(txtDataCount.Text);
            }

            set
            {
                PageCount = value / PageSize;
                txtDataCount.Text = value.ToString();
            }
        }

        /// <summary>
        /// 注册事件
        /// </summary>
        private void RegistPaging_SearchData() {
            if (Paging_SearchData != null)
                Paging_SearchData(this, new EventArgs());
        }

        private void CheckButtonEnable() {
            if (PageIndex <= 1)
                btnUp.Enabled = false;
            else
                btnUp.Enabled = true;
            if (PageIndex >= PageCount)
                btnDown.Enabled = false;
            else
                btnDown.Enabled = true;
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            PageIndex++;
            CheckButtonEnable();
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            PageIndex--;
            CheckButtonEnable();
        }

        private void cboPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {

            RegistPaging_SearchData();

            CheckButtonEnable();
        }
    }
}
