using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using C2LP.ColdStorageDataHubClient.DBHelper.Model;
using System.Data;
//using TBCC.Ext.Fdap4zgswDataHub.HttpInteface.InterfaceModel;

namespace C2LP.ColdStorageDataHubClient.DBHelper.BLL
{
    public class AiInfoServer : BaseServer
    {
        /// <summary>
        /// 获取仓库的所有点信息
        /// </summary>
        public static List<TbccRefAiInfo> GetRefAiInfo()
        {
            List<TbccRefAiInfo> list = null;
            if (!CheckTableExist("tbccAiInfo"))
                return GetRefSensorInfo();

            string sql = "select ProjectID,NetId,RefId,PortNo,PortName,DataType from tbccAiInfo order by datatype,NetId, refid,portNo";
            try
            {
                list = _DBHelper.ReadEntityList<TbccRefAiInfo>(sql);
            }
            catch
            {
                //查询不到则返回NULL，默认为5100系统
            }
            return list;
        }

        /// <summary>
        /// 获取5100系统的探头信息
        /// </summary>
        /// <returns></returns>
        private static List<TbccRefAiInfo> GetRefSensorInfo()
        {
            if (!CheckTableExist("tbccsensorinfo"))
                return null;
            string sql = "select SensorId, ProjectID ,refid,TName,RhName,SensorMode from tbccsensorinfo order by RefId,SensorId";
            List<TbccRefAiInfo> aiList = null;
            try
            {
                List<TbccRefSensorInfo> list = _DBHelper.ReadEntityList<TbccRefSensorInfo>(sql);
                if (list != null && list.Count > 0)
                {
                    aiList = new List<TbccRefAiInfo>();
                    foreach (TbccRefSensorInfo sensor in list)
                    {
                        TbccRefAiInfo ai = new TbccRefAiInfo();
                        //温度AIPortNo = SensorID;
                        if (sensor.SensorMode == 0 || sensor.SensorMode == 1)
                        {
                            ai = new TbccRefAiInfo();
                            ai.ProjectID = sensor.ProjectId;
                            ai.DataType = AiDataTypeEnum.温度;
                            ai.PortName = sensor.TName;
                            ai.PortNo = sensor.SensorId;
                            ai.RefId = sensor.refId;
                            aiList.Add(ai);
                        }
                        //湿度AIPortNo = SensorId+0.5
                        if (sensor.SensorMode == 0 || sensor.SensorMode == 2)
                        {
                            ai = new TbccRefAiInfo();
                            ai.ProjectID = sensor.ProjectId;
                            ai.DataType = AiDataTypeEnum.湿度;
                            ai.PortName = sensor.RhName;
                            ai.PortNo = sensor.SensorId + 0.5f;
                            ai.RefId = sensor.refId;
                            aiList.Add(ai);
                        }
                    }
                }
            }
            catch
            {
            }
            return aiList;
        }
        /// <summary>
        /// 获取车载的所有项目
        /// </summary>
        /// <returns></returns>
        public static List<TbccCarPrjType> GetCarAiInfo()
        {
            List<TbccCarPrjType> list = null;
            string sql = "select ID,CarProjectId,CarProjectName,ProjectAuthCode from TbccCarPrjType";
            try
            {
                list = _DBHelper.ReadEntityList<TbccCarPrjType>(sql);
            }
            catch
            {

            }
            return list;
        }

        /// <summary>
        /// 获取保温箱的所有项目
        /// </summary>
        /// <returns></returns>
        //public static List<TbccBoxPrjType> GetBoxAiInfo()
        //{
        //    List<TbccBoxPrjType> list = null;
        //    string sql = "select ID,BoxProjectId,BoxProjectName,projectAuthCode,projectCode from TbccBoxPrjType";
        //    try
        //    {
        //        list = _DBHelper.ReadEntityList<TbccBoxPrjType>(sql);
        //    }
        //    catch
        //    {

        //    }
        //    return list;
        //}

        /// <summary>
        /// 获取所有Ai关联
        /// </summary>
        /// <returns></returns>
        public static List<AiInfoModel> GetAllAiRelation()
        {
            List<AiInfoModel> list = new List<AiInfoModel>();
            try
            {
                if (!CheckTableExist("c2lp_fdapairelation"))
                {
                    Createc2lp_fdapairelation();
                    return list;
                }
                string sql = "select * from c2lp_fdapairelation";
                list = _DBHelper.ReadEntityList<AiInfoModel>(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }

        /// <summary>
        /// 删除所有Fdap Ai关系项
        /// </summary>
        public static void DeleteAllAiRelation(string projectId, int netId, int refId)
        {
            try
            {
                //    if (refId == 0)
                //        _DBHelper.ExecuteNonQuery("truncate table c2lp_fdapairelation where  `LinkProjectNo`='" + projectId + "'");
                //    else
                _DBHelper.ExecuteNonQuery("delete from c2lp_fdapairelation where `LinkProjectNo`='" + projectId + "' and LinkRefId = '" + refId + "' and LinkNetId='" + netId + "'");
            }
            catch
            {

            }
        }

        public static bool InsertAiRelation(List<AiInfoModel> insertList, string projectId, int netId, int refId)
        {
            try
            {
                if (insertList.Count == 0)
                    return true;
                DeleteAllAiRelation(projectId, netId, refId);
                StringBuilder sql = new StringBuilder();
                foreach (AiInfoModel item in insertList.Where(a => a.LinkProjectNo == projectId && a.LinkNetId == netId && a.LinkRefId == refId))
                {
                    if (item.aiNumber != 0)
                        sql.AppendLine(string.Format("INSERT INTO `c2lp_fdapairelation` (`aiNumber`,`LinkProjectNo`, `LinkNetId`, `LinkRefId`, `LinkPortNo`) VALUES ( '{0}', '{1}', '{2}', '{3}', '{4}');", item.aiNumber, item.LinkProjectNo, item.LinkNetId, item.LinkRefId, item.LinkPortNo));
                    //else
                    //    sql.AppendLine(string.Format("update `c2lp_fdapairelation` set aiNumber='{0}' where id='{1}';",item.aiNumber,item.Id));
                }
                if (sql.Length == 0)
                    sql.AppendLine(string.Format("delete from `c2lp_uploadtime` where ProjectNo='{0}' and netid='{1}';", projectId, netId));
                else
                    sql.AppendLine(CheckUploadTimeIsExist(projectId, netId));
                return _DBHelper.BatchExecuteNonQuery(sql.ToString().Split(Environment.NewLine.ToCharArray(),StringSplitOptions.RemoveEmptyEntries).ToList());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 创建Fdap Ai关系表
        /// </summary>
        public static void Createc2lp_fdapairelation()
        {
            string sql = "CREATE TABLE `c2lp_fdapairelation` (  `Id` int(11) NOT NULL AUTO_INCREMENT,  `aiNumber` int(11) NOT NULL COMMENT '探头编号',     `LinkProjectNo` varchar(10) NOT NULL COMMENT '关联本系统的 项目编号',  `LinkNetId` int(11) DEFAULT NULL COMMENT '关联本系统的 网络编号',  `LinkRefId` int(11) DEFAULT NULL COMMENT '关联本系统的 冷库编号',  `LinkPortNo` float NOT NULL COMMENT '关联本系统的 点编号',  PRIMARY KEY(`Id`)) ENGINE = InnoDB DEFAULT CHARSET = utf8; ";
            try
            {
                _DBHelper.ExecuteNonQuery(sql);
            }
            catch
            {

            }
        }

        /// <summary>
        /// 获取指定枚举类型的键值对
        /// </summary>
        /// <param name="dicTableName">表名</param>
        /// <returns></returns>
        public static Dictionary<string, string> GetDicValue(string dicTableName, string key = null)
        {
            try
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                List<string> fieldList = new List<string>() { "key", "value" };
                string sql = string.Format("select `key`,`value` from {0} ", dicTableName);
                if (key != null)
                    sql += string.Format(" where `key`='{0}'", key);
                List<Dictionary<string, object>> tempDic = _DBHelper.GetDictionaryList(sql, fieldList);
                foreach (Dictionary<string, object> item in tempDic)
                {
                    dic.Add(item[fieldList[0]].ToString(), item[fieldList[1]].ToString());
                }
                return dic;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 查询指定工程的指定设备的指定仓库名称
        /// </summary>
        /// <param name="prjNo">工程编号</param>
        /// <param name="netId">网络编号</param>
        /// <param name="refId">仓库编号</param>
        /// <returns></returns>
        public static string GetRefName(string prjNo, int netId, int refId)
        {
            string sql = string.Format("select refName from tbccrefinfo where ProjectId='{0}' and  refId={2} ", prjNo, netId, refId);
            if (netId != 0)
                sql += " and NetId=" + netId.ToString();
            object rs = null;
            try
            {
                rs = _DBHelper.ExecuteScalar(sql);
            }
            catch
            {

            }
            return rs == null ? "#" + refId.ToString() : rs.ToString();
        }

        /// <summary>
        /// 获取所有已绑定关系的AI的最后上报时间
        /// </summary>
        /// <returns></returns>
        public static DataTable GetAllUploadTime()
        {
            try
            {
                string sql = "select * from c2lp_uploadtime;";
                DataSet ds = _DBHelper.ExecuteQuery(sql);
                if (ds == null || ds.Tables.Count == 0)
                    return null;
                else
                    return ds.Tables[0];
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Table 'tbcc_lsc_db.c2lp_uploadtime' doesn't exist")) { 
                    CreateC2lp_UploadTime();
                    return null;
                }
                else
                    throw ex;
            }
        }

        /// <summary>
        /// 检查指定工程的指定设备的指定仓库是否存在上报进度的记录
        /// </summary>
        /// <param name="projectNo"></param>
        /// <param name="netId"></param>
        /// <returns></returns>
        public static string CheckUploadTimeIsExist(string projectNo, int netId)
        {
            string sql = string.Format("select * from c2lp_uploadtime where projectNo='{0}' and netId='{1}'", projectNo, netId);
            try
            {
                DataSet ds = _DBHelper.ExecuteQuery(sql);
                if (ds == null || ds.Tables.Count == 0)
                    CreateC2lp_UploadTime();
                else if (ds.Tables[0].Rows.Count > 0)
                    return string.Empty;
                return string.Format("insert into c2lp_uploadtime (ProjectNo,NetId,LastUploadTime) values('{0}','{1}','{2}')", projectNo, netId, DateTime.Now.ToString("yyyy-MM-dd HH:mm"));
            }
            catch
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 更新上报进度
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        public static bool UpdateUploadLastTime(int Id, DateTime time) {
            string sql = string.Format("update c2lp_uploadtime set LastUploadTime='{0}' where Id='{1}'",time.ToString("yyyy-MM-dd HH:mm"),Id);
            try
            {
                int count = _DBHelper.ExecuteNonQuery(sql);
                return count >= 1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 创建上报时间表
        /// </summary>
        public static void CreateC2lp_UploadTime()
        {
            string sql = "CREATE TABLE `c2lp_uploadtime` (  `Id` int(11) NOT NULL AUTO_INCREMENT,  `ProjectNo` varchar(10) NOT NULL,  `NetId` int(11) NOT NULL,`LastUploadTime` datetime NOT NULL,  PRIMARY KEY(`Id`)) ENGINE = InnoDB DEFAULT CHARSET = utf8; ";
            try
            {
                _DBHelper.ExecuteNonQuery(sql);
            }
            catch
            {

            }
        }

    }


}
