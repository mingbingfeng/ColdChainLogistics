using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using C2LP.Manager.Console.ConsoleServerWebReference;
using System.IO;
using static System.Windows.Forms.ListViewItem;
using System.Collections;

namespace C2LP.Manager.Console.FrmWaybillControl
{
    public partial class FrmSignPicture : Form
    {
        public FrmSignPicture()
        {
            InitializeComponent();
        }
        ConsoleServerWebReference.ConsoleServer cs = new ConsoleServerWebReference.ConsoleServer();
        public Model_Waybill_Base mwb;
        public Model_Waybill_Node mwn;
        string s = System.Configuration.ConfigurationSettings.AppSettings["picName"];
        //声明字典
        //Dictionary<string, string> map = new Dictionary<string, string>();
        Hashtable map = new Hashtable(); //file创建一个Hashtable实例
        private void FrmSignPicture_Load(object sender, EventArgs e)
        {
            getComboxImage();
            cmbImage.SelectedIndexChanged += cmbImage_SelectedIndexChanged;
           
            
        }
        public void getComboxImage()
        {
            try
            {
                ResultModelOfArrayOfModel_Waybill_Postback_Picd4FqxSXX pic = cs.GetWaybillPostbackPic(Convert.ToInt32(mwn.BaseIdk__BackingField), true);
                if (pic.Code != 0)
                {
                    MessageBox.Show(pic.Message);
                }
                else
                {
                    cmbImage.ValueMember = "idk__BackingField";
                    cmbImage.DisplayMember = "picNamek__BackingField";
                    cmbImage.DataSource = pic.Data;
                    
                    if (pic.Data.Length > 0)
                    {
                        huoQuTuPian(s + pic.Data[0].PicNamek__BackingField);
                    }
                    else
                    {
                        label2.Text = "没有图片";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string a = listView1.SelectedItems[0].SubItems[5].Text.ToString() ;
            //if (listView1.SelectedIndices!=null &&listView1.SelectedIndices.Count>0)
            //{
            //    try
            //    {
            //        ListView.SelectedIndexCollection c = listView1.SelectedIndices;
            //        Image image = listView1.Items[c[0]].Tag as Image;
            //       // MessageBox.Show(image.ToString());

            //        string name = listView1.Items[c[0]].Text;
            //        //picPicture.Image = Image.FromStream(System.Net.WebRequest.Create(s + name).GetResponse().GetResponseStream());
            //        picPicture.Image = image; 
            //    }
            //    catch (Exception)
            //    {
            //        picPicture.Image = null;
            //    }
            //}
           
        }

        private void cmbImage_SelectedIndexChanged(object sender, EventArgs e)
        {

            Model_Waybill_Postback_Pic obj = cmbImage.SelectedItem as Model_Waybill_Postback_Pic;

            huoQuTuPian(s + obj.PicNamek__BackingField);
        }
        public void huoQuTuPian(string name)
        {
            try
            {
                label2.Visible = false;
                
                //将图片添加进map中，缓存图片
                if (!map.Contains(name))
                {
                    Image image = Image.FromStream(System.Net.WebRequest.Create(name).GetResponse().GetResponseStream());
                    map.Add(name, image);
                }
                picPicture.Image = (Image)map[name];
                
            }
            catch (Exception)
            {
                picPicture.Image = null;
                label2.Visible = true;
                label2.Text = "没有图片";
            }
        }
        /// <summary>
        /// 图片随着窗体的拉伸而放大
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void picPicture_SizeChanged(object sender, EventArgs e)
        {
            this.picPicture.Size = new Size(this.Width-80,this.Height-100);
              
        }

        private void btnRefurbish_Click(object sender, EventArgs e)
        {
            getComboxImage();
        }
    }
}
