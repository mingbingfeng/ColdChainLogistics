using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using C2LP.PDA.Datas.Model;
using System.Data;

namespace C2LP.PDA.Datas.BLL
{
    public class WaybillServer : BaseServer
    {
        #region 运单相关查询
        /// <summary>
        /// 创建运单
        /// </summary>
        /// <param name="number">运单号</param>
        /// <param name="sId">发货客户ID</param>
        /// <param name="sOrg">发货客户名称</param>
        /// <param name="sPerson">发货人姓名</param>
        /// <param name="sTel">发货人电话</param>
        /// <param name="sAddress">发货人地址</param>
        /// <param name="rId">收货客户ID,没有时写NULL</param>
        /// <param name="rOrg">收货客户名称</param>
        /// <param name="rPerson">收货人姓名</param>
        /// <param name="rTel">收货电话</param>
        /// <param name="rAddress">收货地址</param>
        /// <param name="bCount">计费数量</param>
        public static DateTime AddOrder(string number, int sId, string sOrg, string sPerson, string sTel, string sAddress,
            string rId, string rOrg, string rPerson, string rTel, string rAddress, int bCount, string storageName,string destina,ref string sqlStr)
        {
            DateTime dtNow = DateTime.Now;
            try
            {
                int storageId = 0;
                List<ColdStorage> storage = StorageServer.GetStorageList(storageName);
                if (storage.Count() > 0)
                    storageId = storage[0].Id;
                string nowTime = dtNow.ToString("yyyy-MM-dd HH:mm:ss");
                StringBuilder sql = new StringBuilder("insert into c2lp_order (number,senderId,senderOrg,senderPerson,senderTel,senderAddress,receiverId,receiverOrg,receiverPerson,receiverTel,receiverAddress,billingCount,stage,beginAt)");
                sql.AppendLine("values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}');");
                sql.AppendLine("insert into c2lp_node (orderNumber,operateAt,storageId,storageName,content,arrived,ConsignorId,parentStorageId) values('{0}','{13}','{14}','{17}','{15}','{16}',0,0);");
                string content = string.Format("【{0}】 配送员{1}已收取快件 准备运往【{2}】{3}", storageName, (string.IsNullOrEmpty(storage[0].driver) ? "" : "【" + storage[0].driver + "】"), destina, (string.IsNullOrEmpty(storage[0].driverTel) ? "" : " 联系电话【" + storage[0].driverTel + "】"));
                sqlStr = string.Format(sql.ToString(), number, sId, sOrg, sPerson, sTel, sAddress, rId, rOrg, rPerson, rTel, rAddress, bCount, 0, nowTime, storageId, content, 1, storageName);
                _SqlHelp.ExecuteNonQuery(sqlStr, System.Data.CommandType.Text);
                return dtNow;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取指定最大数量的未上传运单
        /// </summary>
        /// <param name="count">获取运单的最大数量</param>
        /// <returns></returns>
        public static DataTable GetNotUploadOrders(int count)
        {
            try
            {
                string sql = "SELECT id,number,senderId,senderOrg,senderPerson,senderTel,senderAddress,receiverId,receiverOrg,receiverPerson,receiverTel,receiverAddress,billingCount,stage,beginAt FROM c2lp_order WHERE id IN( select id from c2lp_order group by number  order by id limit " + count + ")";
                DataSet ds = _SqlHelp.ExecuteDataSet(sql, System.Data.CommandType.Text);
                if (ds != null && ds.Tables.Count == 1)
                    return ds.Tables[0];
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除上传成功的运单
        /// </summary>
        /// <param name="orderNumberList"></param>
        /// <returns></returns>
        public static bool DeleteUploadSuccessOrders(List<string> orderNumberList)
        {
            try
            {
                if (orderNumberList.Count == 0)
                    return true;
                string sql = string.Format("delete from c2lp_order where number in ('{0}')", string.Join("','", orderNumberList.ToArray()));
                return _SqlHelp.ExecuteNonQuery(sql, CommandType.Text) >= orderNumberList.Count;
            }
            catch
            {
                return false;
            }
        }
        #endregion


        /// <summary>
        /// 创建节点
        /// </summary>
        /// <param name="orderNumber">运单号</param>
        /// <param name="storageId">冷藏载体ID</param>
        /// <param name="content">节点内容</param>
        /// <param name="arrived">是否运达</param>
        public static DateTime AddNode(string orderNumber, int storageId, string storageName, string content, bool arrived, int ConsignorId,int parentStorageId, ref string sql)
        {
            DateTime dtNow = DateTime.Now;
            try
            {
                sql = string.Format("insert into c2lp_node (orderNumber,operateAt,storageId,storageName,content,arrived,ConsignorId,parentStorageId) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}');"
                    , orderNumber, dtNow.ToString("yyyy-MM-dd HH:mm:ss"), storageId, storageName, content, arrived ? 2 : 1, ConsignorId, parentStorageId);
                _SqlHelp.ExecuteNonQuery(sql, System.Data.CommandType.Text);
                return dtNow;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 获取指定最大数量的未上传节点
        /// </summary>
        /// <param name="count">获取节点的最大数量</param>
        /// <returns></returns>
        public static DataTable GetNotUploadNode(int count)
        {
            try
            {
                string sql = "SELECT n.* from c2lp_node n left join c2lp_order o on n.ordernumber=o.number left join huadong_tms_order h on n.orderNumber=h.relationId where o.number is null and h.relationId is null order by n.id limit " + count;
                DataSet ds = _SqlHelp.ExecuteDataSet(sql, System.Data.CommandType.Text);
                if (ds != null && ds.Tables.Count == 1)
                    return ds.Tables[0];
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取指定最大数量的未上传节点
        /// </summary>
        /// <param name="count">获取节点的最大数量</param>
        /// <returns></returns>
        public static DataTable GetWaitUploadNode(int count,out int sum)
        {
            try
            {
                sum = 0;
                string sql = "SELECT Count(*) from c2lp_node n left join c2lp_order o on n.ordernumber=o.number order by n.operateAt";
                object obj = _SqlHelp.ExecuteScalar(sql, CommandType.Text);
                if (obj != null)
                    sum = Convert.ToInt32(obj);
                sql = "SELECT n.* from c2lp_node n left join c2lp_order o on n.ordernumber=o.number order by n.operateAt limit " + count;
                DataSet ds = _SqlHelp.ExecuteDataSet(sql, System.Data.CommandType.Text);
                if (ds != null && ds.Tables.Count == 1)
                    return ds.Tables[0];
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除上传成功的运单
        /// </summary>
        /// <param name="orderNumberList"></param>
        /// <returns></returns>
        public static bool DeleteUploadSuccessNode(List<string> nodeIdList)
        {
            try
            {
                if (nodeIdList.Count == 0)
                    return true;
                string sql = string.Format("delete from c2lp_node where id in ('{0}')", string.Join("','", nodeIdList.ToArray()));
                return _SqlHelp.ExecuteNonQuery(sql, CommandType.Text) >= nodeIdList.Count;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 根据第三方运单号和创建时间删除揽件节点
        /// </summary>
        /// <param name="relationID"></param>
        /// <param name="optTime"></param>
        public static void DeleteThirdNodeByOptAt(string relationID, string optTime) {
            try
            {
                string sql = string.Format("delete from c2lp_node where orderNumber= '{0}' and operateAt='{1}'",relationID,optTime);
                _SqlHelp.ExecuteNonQuery(sql, CommandType.Text);
            }
            catch 
            {
                
            }
        }

        /// <summary>
        /// 保存签收图片路径
        /// </summary>
        /// <param name="orderNumber">运单号</param>
        /// <param name="filePathList">签收图片路径集合</param>
        public static DateTime AddPostBack(string orderNumber, List<string> filePathList,int ConsignorId,ref string sql)
        {
            try
            {
                DateTime dtNow = DateTime.Now;
                if (filePathList.Count > 0)
                {
                    sql = string.Format("insert into c2lp_postback(baseId,picName,postbackTime,ConsignorId) values('{0}','{1}','{2}','{3}');", orderNumber, string.Join("|", filePathList.ToArray()), dtNow.ToString("yyyy-MM-dd HH:mm:ss"), ConsignorId);
                    _SqlHelp.ExecuteNonQuery(sql.ToString(), System.Data.CommandType.Text);
                }
                    return dtNow;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取签收图片信息
        /// </summary>
        /// <returns></returns>
        public static DataTable GetPostBackPic()
        {
            try
            {
                //string sql = "SELECT n.* from c2lp_postback n left join c2lp_order o on n.baseid=o.number where o.number is null order by n.id limit 1;";
                string sql = "SELECT n.* from c2lp_postback n left join c2lp_order o on n.baseid=o.number left join huadong_tms_order h on n.baseId=h.relationId where o.number is null and h.relationId is null order by n.id limit 1;";
                DataSet ds = _SqlHelp.ExecuteDataSet(sql, System.Data.CommandType.Text);
                if (ds != null && ds.Tables.Count == 1)
                    return ds.Tables[0];
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 删除上传成功的签收图片信息
        /// </summary>
        /// <param name="orderNumberList"></param>
        /// <returns></returns>
        public static bool DeleteUploadSuccessPostback(int postbackId)
        {
            try
            {
                string sql = "delete from c2lp_postback where id = " + postbackId;
                return _SqlHelp.ExecuteNonQuery(sql, CommandType.Text) == 1;
            }
            catch
            {
                return false;
            }
        }
    }
}
