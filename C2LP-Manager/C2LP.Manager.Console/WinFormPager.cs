using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace C2LP.Manager.Console
{
    public partial class WinFormPager : UserControl
    {
        public WinFormPager()
        {
            InitializeComponent();
        }
        #region 分页字段和属性
        private int pageIndex = 1;

        /// 当前页面
        public virtual int PageIndex
        {
            get { return pageIndex; }
            set { pageIndex = value; }
        }
        private int pageSize = 10;

        /// 每页记录数
        public virtual int PageSize
        {
            get { return pageSize; }
            set { pageSize = value; }
        }
        private int recordCount = 0;

        /// 总记录数
        public virtual int RecordCount
        {
            get { return recordCount; }
            set { recordCount = value; }
        }
        private int pageCount = 0;

        /// 总页数
        public int PageCount
        {
            get
            {
                if (pageSize != 0)
                {
                    pageCount = GetPageCount();
                }
                return pageCount;
            }
        }

        /// 计算总页数
        private int GetPageCount()
        {
            if (PageSize == 0)
            {
                return 0;
            }
            int pageCount = RecordCount / PageSize;
            if (RecordCount % PageSize == 0)
            {
                pageCount = RecordCount / PageSize;
            }
            else
            {
                pageCount = RecordCount / PageSize + 1;
            }
            return pageCount;
        }
        #endregion


        /// 第一页
        private void btnFirstPage_Click(object sender, EventArgs e)
        {
            PageIndex = 1;
            DrawControl(true);
        }

        /// 上一页
        private void btnPreviousPage_Click(object sender, EventArgs e)
        {
            PageIndex = PageIndex - 1;
            if (PageIndex <=0)
                PageIndex = 1;
            DrawControl(true);
        }

        /// 下一页
        private void btnNextPage_Click(object sender, EventArgs e)
        {
            PageIndex = PageIndex + 1;
            if (PageIndex > PageCount)
                PageIndex = PageCount;
            DrawControl(true);
        }

        /// 最后一页
        private void btnLastPage_Click(object sender, EventArgs e)
        {
            PageIndex = PageCount;
            DrawControl(true);
        }
        public event EventHandler OnPageChanged;

        /// 外部调用
        public void DrawControl(int count)
        {
            recordCount = count;

            DrawControl(false);
        }

        public void DrawControl(bool callEvent)
        {
            lbTotalPages.Text = "总共" + PageCount.ToString() + "页,";
            lbCurrent.Text = RecordCount.ToString() + "条";
            txtPageNum.Text = PageIndex.ToString();
            if (callEvent && OnPageChanged != null)
            {
                OnPageChanged(this, null);//当前分页数字改变时，触发委托事件
            }
            SetFormCtrEnabled();
            if (PageCount == 1)//有且仅有一页
            {
                btnFirstPage.Enabled = false;
                btnPreviousPage.Enabled = false;
                btnNextPage.Enabled = false;
                btnLastPage.Enabled = false;
                btnGo.Enabled = false;
            }
            else if (PageIndex == 1)//第一页
            {
                btnFirstPage.Enabled = false;
                btnPreviousPage.Enabled = false;
            }
            else if (PageIndex == PageCount)//最后一页
            {
                btnNextPage.Enabled = false;
                btnLastPage.Enabled = false;
            }
            if (RecordCount==0)
            {
                btnFirstPage.Enabled = false;
                btnPreviousPage.Enabled = false;
                btnNextPage.Enabled = false;
                btnLastPage.Enabled = false;
                btnGo.Enabled = false;
            }
        }

        private void SetFormCtrEnabled()
        {
            btnFirstPage.Enabled = true;
            btnPreviousPage.Enabled = true;
            btnNextPage.Enabled = true;
            btnLastPage.Enabled = true;
            btnGo.Enabled = true;
        }

        /// 跳转
        private void btnGo_Click(object sender, EventArgs e)
        {
            int num = 0;
            if (int.TryParse(txtPageNum.Text.Trim(), out num) && num > 0)
            {
                PageIndex = num;
                DrawControl(true);
            }
        }

        private void WinFormPager_Load(object sender, EventArgs e)
        {
            
            GetCounts();
            cmbCount.SelectedIndexChanged += cmbCount_SelectedIndexChanged;
        }

        public void GetCounts()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Value");
            dt.Columns.Add("Name");
            DataRow dr = dt.NewRow();
            dr[0] = 1;
            dr[1] = 10;
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = 2;
            dr[1] = 30;
            dt.Rows.Add(dr);
            dr = dt.NewRow();
            dr[0] = 3;
            dr[1] = 50;
            dt.Rows.Add(dr);
            cmbCount.ValueMember = "Value";
            cmbCount.DisplayMember = "Name";
            cmbCount.DataSource = dt;
        }

        /// 改变现实条数
        private void cmbCount_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Size = Convert.ToInt32(cmbCount.Text);
            PageSize = Size;
            PageIndex = 1;
            DrawControl(true);
        }

        //enter键功能
        private void txtPageNum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode==Keys.Enter)
            {
                //跳转页数限制
                int num = 0;
                if (int.TryParse(txtPageNum.Text.Trim(), out num) && num > 0)
                {
                    if (num > PageCount)
                    {
                        txtPageNum.Text = PageCount.ToString();
                    }
                    else
                    {
                        txtPageNum.Text = num.ToString() ;
                    }
                    btnGo_Click(null, null);
                }else
                {
                    txtPageNum.Text = "1" ;
                }
            }
        }
    }
}
