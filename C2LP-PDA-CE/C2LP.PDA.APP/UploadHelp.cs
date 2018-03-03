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
    public class UploadHelp
    {

        /// <summary>
        /// 运行状态
        /// </summary>
        public static bool _isRuning = false;

        /// <summary>
        /// 后台执行同步的线程
        /// </summary>
        private static Thread _th;

        /// <summary>
        /// 开始同步
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
        /// 终止同步
        /// </summary>
        public static void StopUpload()
        {
            if (_isRuning)
            {
                if (_th != null)
                {
                    try
                    {
                        _th.Abort();
                    }
                    catch
                    {
                    }
                }
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

        private static void DoWork()
        {
            try
            {
                //等待拨号完成后开始同步
                while (ConnectHelp.isConencting)
                {
                    if (!_isRuning)
                        return;
                    System.Threading.Thread.Sleep(666);
                }
                UploadOrder();
                Thread.Sleep(1500);
                UploadThirdOrder();
                Thread.Sleep(1500);
                UploadNode();
                Thread.Sleep(1500);
                UploadPic();
            }
            catch (Exception ex)
            {
                Thread.Sleep(1500);
                //throw ex;
                //if (ex.Message.Contains("无法连接到远程服务器") || ex.Message.Contains("未能建立与网络的连接"))
                //    FrmParent.ParentForm.ReConnect();
                //else
                    FrmParent.ParentForm.SetNewInfo("上报遇到问题,等待下次重新上报 " + ex.Message, false);
            }
            finally
            {
                //System.Threading.Thread.Sleep(Common._UploadCycle * 1000);
                //上传完后休息大概一分钟
                for (int i = 0; i <= Common._UploadCycle * 10; i++)
                {
                    if (!_isRuning)
                        break;
                    System.Threading.Thread.Sleep(100);
                    //Application.DoEvents();
                }
                _isRuning = false;
            }
        }

        /// <summary>
        /// 上传运单
        /// </summary>
        private static void UploadOrder()
        {
            ResultModelOfboolean result = new ResultModelOfboolean();
            List<Model_Waybill_Base> list = new List<Model_Waybill_Base>();
            string msg = string.Empty;
            string err = string.Empty;
            try
            {
                DataTable dt = WaybillServer.GetNotUploadOrders(Common._MaxUploadOrderCount);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Model_Waybill_Base b = new Model_Waybill_Base();
                        b.Numberk__BackingField = row["number"].ToString();
                        b.SenderIdk__BackingField = Convert.ToInt32(row["senderId"]);
                        b.SenderOrgk__BackingField = row["senderOrg"].ToString();
                        b.SenderPersonk__BackingField = row["senderPerson"].ToString();
                        b.SenderTelk__BackingField = row["senderTel"].ToString();
                        b.SenderAddressk__BackingField = row["senderAddress"].ToString();
                        b.ReceiverIdk__BackingField = row["receiverId"] is DBNull ? 0 : Convert.ToInt32((row["receiverId"]));
                        b.ReceiverOrgk__BackingField = row["receiverOrg"].ToString();
                        b.ReceiverPersonk__BackingField = row["receiverPerson"].ToString();
                        b.ReceiverTelk__BackingField = row["receiverTel"].ToString();
                        b.ReceiverAddressk__BackingField = row["receiverAddress"].ToString();
                        b.BillingCountk__BackingField = Convert.ToInt32(row["billingCount"]);
                        b.Stagek__BackingField = (Enum_WaybillStage)Enum.ToObject(typeof(Enum_WaybillStage), Convert.ToInt32(row["stage"]));
                        b.BeginAtk__BackingField = Convert.ToDateTime(row["beginAt"]);
                        list.Add(b);
                        msg = b.Numberk__BackingField;
                    }
                    FrmParent.ParentForm.SetNewInfo("正在上报运单" + msg + ",请稍候...", null);
                    result = Common._PdaServer.UploadWaybill_Base(list.ToArray());
                }
            }
            catch (Exception ex)
            {
                err = ex.Message;
                throw ex;
            }
            finally
            {
                if (result.Data)
                    WaybillServer.DeleteUploadSuccessOrders(list.Select(l => l.Numberk__BackingField).ToList());
                else if (string.IsNullOrEmpty(err))
                    err = result.Message;
                err = err == null ? string.Empty : err;
                FrmParent.ParentForm.SetNewInfo(string.Format("{0}{1} {2}", (msg.Length == 0 ? "没有待上报的运单." : "上报运单"), msg, (err.Length == 0 ? (msg.Length == 0 ? "" : "成功") : "失败 " + err)), err.Length == 0);
            }
        }

        /// <summary>
        /// 上传第三方运单号
        /// </summary>
        private static void UploadThirdOrder()
        {
            ResultModelOfboolean result = new ResultModelOfboolean();
            List<Model_Huadong_Tms_Order> list = new List<Model_Huadong_Tms_Order>();
            string msg = string.Empty;
            string err = string.Empty;
            try
            {
                //根据每次查询运单数量为条件查询几条信息
                DataTable dt = HuadongTmsOrderServer.GethuadongTmsOrder(Common._MaxUploadOrderCount);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Model_Huadong_Tms_Order huadong = new Model_Huadong_Tms_Order();
                        huadong.Idk__BackingField = Convert.ToInt32(row["id"]);
                        huadong.RelationIdk__BackingField = row["relationId"].ToString();
                        list.Add(huadong);
                        msg = huadong.RelationIdk__BackingField;
                    }
                    FrmParent.ParentForm.SetNewInfo("正在上报第三方运单" + msg + ",请稍候...", null);
                    //后台接口，上传数据
                    result = Common._PdaServer.UploadHuadongTmsOrder(list.ToArray());
                }
            }
            catch (Exception ex)
            {
                err = ex.Message;
                throw ex;
            }
            finally
            {
                if (result.Data)
                    HuadongTmsOrderServer.DeleteUploadHuadongTmsOrder(list.Select(l => l.RelationIdk__BackingField).ToList());
                else if (string.IsNullOrEmpty(err))
                    err = result.Message;
                err = err == null ? string.Empty : err;
                FrmParent.ParentForm.SetNewInfo(string.Format("{0}{1} {2}", (msg.Length == 0 ? "没有待上报的第三方运单." : "上报第三方运单"), msg, (err.Length == 0 ? (msg.Length == 0 ? "" : "成功") : "失败 " + err)), err.Length == 0);
            }
        }

        /// <summary>
        /// 上传节点
        /// </summary>
        private static void UploadNode()
        {
            ResultModelOfboolean result = new ResultModelOfboolean();
            List<Model_Waybill_Node> list = new List<Model_Waybill_Node>();
            string msg = string.Empty;
            string err = string.Empty;
            try
            {
                DataTable dt = WaybillServer.GetNotUploadNode(Common._MaxUploadNodeCount);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Model_Waybill_Node n = new Model_Waybill_Node();
                        n.BaseIdk__BackingField = row["ordernumber"].ToString();
                        n.Contentk__BackingField = row["content"].ToString();
                        n.Idk__BackingField = Convert.ToInt32(row["id"]);
                        n.operateAtk__BackingField = Convert.ToDateTime(row["operateAt"]);
                        n.StorageIdk__BackingField = Convert.ToInt32(row["storageId"]);
                        n.StorageNamek__BackingField = row["storageName"].ToString();
                        n.Arrivedk__BackingField = (Enum_Arrived)Enum.ToObject(typeof(Enum_Arrived), Convert.ToInt32(row["Arrived"]));
                        list.Add(n);
                        msg = n.BaseIdk__BackingField;
                    }
                    FrmParent.ParentForm.SetNewInfo("正在上报节点" + msg + ",请稍候...", null);
                    result = Common._PdaServer.UploadWaybill_Node(list.ToArray());
                }
            }
            catch (Exception ex)
            {
                err = ex.Message;
                throw ex;
            }
            finally
            {
                if (result.Data)
                    WaybillServer.DeleteUploadSuccessNode(list.Select(l => l.Idk__BackingField.ToString()).ToList());
                else if (string.IsNullOrEmpty(err))
                    err = result.Message;
                err = err == null ? string.Empty : err;
                FrmParent.ParentForm.SetNewInfo(string.Format("{0}{1} {2}", (msg.Length == 0 ? "没有待上报的节点." : "上报节点"), msg, (err.Length == 0 ? (msg.Length == 0 ? "" : "成功") : "失败 " + err)), err.Length == 0);
            }
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        private static void UploadPic()
        {
            ResultModelOfboolean result = new ResultModelOfboolean();
            Model_Waybill_Postback_Pic picModel = new Model_Waybill_Postback_Pic();
            DateTime postbackTime = DateTime.Now;
            List<object> picBytesList = new List<object>();
            string msg = string.Empty;
            string err = string.Empty;
            try
            {
                DataTable dt = WaybillServer.GetPostBackPic();
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    picModel.Idk__BackingField = Convert.ToInt32(row["id"]);
                    picModel.BaseIdk__BackingField = row["baseId"].ToString();
                    picModel.PicNamek__BackingField = row["picName"].ToString();
                    postbackTime = Convert.ToDateTime(row["postbackTime"]);
                    //获取图片信息
                    foreach (string picPath in picModel.PicNamek__BackingField.Split('|'))
                    {
                        picBytesList.Add(GetGZipPicBytes(picPath));
                    }

                    msg = picModel.PicNamek__BackingField.Split('\\').Last();
                    FrmParent.ParentForm.SetNewInfo("正在上报图片" + msg + ",请稍候...", null);
                    result = Common._PdaServer.UploadWaybill_Postback(picModel, postbackTime, true, picBytesList.ToArray());
                }
            }
            catch (Exception ex)
            {
                try
                {
                    if (ex.Message.Contains("未能找到文件"))
                    {
                        WaybillServer.DeleteUploadSuccessPostback(picModel.Idk__BackingField);
                    }
                }
                catch
                {

                }
                err = ex.Message;
                throw ex;
            }
            finally
            {
                if (result.Data && picBytesList.Count > 0)
                {
                    //删除图片
                    foreach (string picPath in picModel.PicNamek__BackingField.Split('|'))
                    {
                        File.Delete(picPath);
                    }
                    //删除数据
                    WaybillServer.DeleteUploadSuccessPostback(picModel.Idk__BackingField);
                }
                else if (string.IsNullOrEmpty(err))
                    err = result.Message;
                err = err == null ? string.Empty : err;
                FrmParent.ParentForm.SetNewInfo(string.Format("{0}{1} {2}", (msg.Length == 0 ? "没有待上报的图片." : "上报图片"), msg, (err.Length == 0 ? (msg.Length == 0 ? "" : "成功") : "失败 " + err)), err.Length == 0);
            }
        }

        /// <summary>
        /// 获取压缩后的图片字节
        /// </summary>
        /// <param name="picPath"></param>
        /// <returns></returns>
        private static byte[] GetGZipPicBytes(string picPath)
        {
            FileInfo imgFile = new FileInfo(picPath);
            byte[] imgByte = null;
            using (FileStream imgStream = imgFile.OpenRead())
            {
                byte[] bytes = new byte[imgFile.Length];
                imgStream.Read(bytes, 0, Convert.ToInt32(imgFile.Length));
                using (MemoryStream cms = new MemoryStream())
                {
                    using (GZipStream gzip = new GZipStream(cms, CompressionMode.Compress))
                    {
                        gzip.Write(bytes, 0, bytes.Length);
                    }
                    imgByte = cms.ToArray();
                }
            }
            return imgByte;
        }
    }
}
