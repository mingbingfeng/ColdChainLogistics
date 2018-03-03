using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using C2LP.PDA.Datas.Model;
using System.Data;

namespace C2LP.PDA.Datas.BLL
{
    public class HuadongTmsOrderServer : BaseServer
    {
        #region 运单相关查询
        /// <summary>
        /// 添加运单号
        /// </summary>
        /// <param name="number">运单号</param>
        public static void AddhuadongTmsOrder(string number, string storageName, string destina)
        {
            try
            {
                //string sql =string.Format( "insert into huadong_tms_order(relationId) values('{0}') ;",number);
                //_SqlHelp.ExecuteNonQuery(sql, System.Data.CommandType.Text);
                int storageId = 0;
                List<ColdStorage> storage = StorageServer.GetStorageList(storageName);
                if (storage.Count() > 0)
                    storageId = storage[0].Id;
                string nowTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                StringBuilder sql = new StringBuilder("insert into huadong_tms_order(relationId,operateAt) ");
                sql.AppendLine("values('{0}','{1}') ;");
                sql.AppendLine("insert into c2lp_node (orderNumber,operateAt,storageId,storageName,content,arrived) values('{0}','{1}','{2}','{3}','{4}','{5}');");
                string content = string.Format("【{0}】 配送员{1}已收取快件 准备运往【{2}】{3}", storageName, (string.IsNullOrEmpty(storage[0].driver) ? "" : "【" + storage[0].driver + "】"), destina, (string.IsNullOrEmpty(storage[0].driverTel) ? "" : " 联系电话【" + storage[0].driverTel + "】"));
                string sqlStr = string.Format(sql.ToString(), number, nowTime, storageId, storageName, content, 1);
                _SqlHelp.ExecuteNonQuery(sqlStr, System.Data.CommandType.Text);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        /// <summary>
        /// 检查第三方运单表是否存在operateAt字段，没有就添加
        /// </summary>
        public static void AddTMSOrderOptAt()
        {
            try
            {
                string sql = "select Count(*) from sqlite_master  where tbl_name='huadong_tms_order' and sql like '%operateAt%';";
                object obj = _SqlHelp.ExecuteScalar(sql, CommandType.Text);
                if (Convert.ToInt32(obj) == 0)
                {
                    sql = "alter table huadong_tms_order add operateAt text null";
                    _SqlHelp.ExecuteNonQuery(sql, CommandType.Text);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 获取未上传运单的最大数量
        /// </summary>
        /// <param name="count">获取运单的最大数量</param>
        /// <returns></returns>
        public static DataTable GethuadongTmsOrder(int count)
        {
            try
            {
                string sql = "SELECT id,relationId,operateAt FROM huadong_tms_order WHERE id IN( select id from huadong_tms_order group by relationId  order by id limit " + count + ")";
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
        /// 删除上传成功信息
        /// </summary>
        /// <param name="huadonglist"></param>
        /// <returns></returns>
        public static bool DeleteUploadHuadongTmsOrder(List<string> huadonglist)
        {
            try
            {
                if (huadonglist.Count == 0)
                    return true;
                string sql =string.Format( "delete from huadong_tms_order where relationId in ('{0}') ",string.Join("','",huadonglist.ToArray()));
                return  _SqlHelp.ExecuteNonQuery(sql, System.Data.CommandType.Text)>=huadonglist.Count;
                
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region 节点信息
        
        #endregion
    }
}