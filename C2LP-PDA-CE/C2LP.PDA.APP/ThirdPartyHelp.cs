using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using C2LP.PDA.Datas.BLL;
using System.Data;
using C2LP.PDA.APP.PDAWebReference;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;

namespace C2LP.PDA.APP
{
    public class ThirdPartyHelp
    {
        //上传pda数据库中的数据到后台
        /// <summary>
        /// 运行状态
        /// </summary>
        public static bool _isRuning = false;

        /// <summary>
        /// 后台执行线程
        /// </summary>
        private static Thread _th;
        
        /// <summary>
        /// 开始上传数据
        /// </summary>
        public static void StartUpload()
        {
            if (!_isRuning)
            {
                _isRuning = true;
                StartWork();
            }
        }
        /// <summary>
        /// 终止上传数据
        /// </summary>
        public static void StopUpload()
        {
            if (_isRuning)
            {
                if (_th != null)
                    _th.Abort();
                _isRuning = false;
            }
        }

        /// <summary>
        /// 开始线程
        /// </summary>
        private static void StartWork()
        {
            _th = new Thread(DoWork);
            _th.IsBackground = true;
            _th.Start();
        }
        /// <summary>
        /// 运行上传方法
        /// </summary>
        private static void DoWork()
        {
            try
            {
                UploadOrder();
            }
            catch (Exception)
            {

            }
            finally
            {
                //上传完后大概休息一分钟
                for (int i = 0; i < Common._UploadCycle * 10; i++)
                {
                    System.Threading.Thread.Sleep(97);
                    Application.DoEvents();
                }
                _isRuning = false;
            }
        }
        /// <summary>
        /// 上传运单号
        /// </summary>
        private static void UploadOrder()
        {
            ResultModelOfboolean result = new ResultModelOfboolean();
            List<Model_Huadong_Tms_Order> list = new List<Model_Huadong_Tms_Order>();
            try
            {
                //根据每次查询运单数量为条件查询几条信息
                DataTable dt = HuadongTmsOrderServer.GethuadongTmsOrder(Common._MaxUploadOrderCount);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Model_Huadong_Tms_Order huadong = new Model_Huadong_Tms_Order();
                        huadong.Idk__BackingField =Convert.ToInt32(row["id"]);
                        huadong.RelationIdk__BackingField = row["relationId"].ToString();
                        list.Add(huadong);
                    }
                    //后台接口，上传数据
                    result = Common._PdaServer.UploadHuadongTmsOrder(list.ToArray());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (result.Data)
                    HuadongTmsOrderServer.DeleteUploadHuadongTmsOrder(list.Select(l => l.RelationIdk__BackingField).ToList());

            }
        }
    }
}