using C2LP.WebService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2LP.WebService.BLL.ColdChainBLL
{
    public class CC_HistDataServer : BaseServer
    {
        /// <summary>
        /// 查询指定PointId是否存在
        /// </summary>
        /// <param name="pointId"></param>
        /// <returns></returns>
        public static Model_AiInfo CheckPointIdIsExist(int pointId)
        {
            string sql = "select * from aiinfo where pointId='" + pointId.ToString() + "'";
            try
            {
                return _SqlHelp.ExecuteObject<Model_AiInfo>(sql);
            }
            catch
            {
                throw new Exception("查询出错或者指定PointId不存在!");
            }
        }

        /// <summary>
        /// 插入历史数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="coldstorage_ID"></param>
        /// <returns></returns>
        public static bool InsertPointData(Model_NodeHistoryData data, int coldstorage_ID)
        {
            bool result = false;
            string tableName = "history_data_" + coldstorage_ID;
            string sql = string.Format("insert into {0} (pointId,`data`,`isAlarm`,`dataTime`) values('{1}','{2}','{3}','{4}')", tableName, data.PointId, data.Data, (int)data.IsAlarm, data.DataTime.ToString("yyyy-MM-dd HH:mm:ss"));
            try
            {
                result = _SqlHelp.ExecuteNonQuery(sql) == 1;
            }
            catch (Exception ex)
            {
                throw new Exception("插入数据时出错:" + ex.Message);
            }
            finally
            {
                try
                {
                    string stateSql = string.Empty;
                    if (result)
                        stateSql = string.Format("delete from history_data_waithandle where id='{0}'", data.Id);
                    else
                        stateSql ="update history_data_waithandle set retryCount=retryCount+1";
                    _SqlHelp.ExecuteNonQuery(stateSql);
                }
                catch (Exception ex)
                {
                    throw new Exception("更新临时数据状态时出错:" + ex.Message);
                }
            }
            return result;
        }

        /// <summary>
        /// 将数据加入临时表
        /// </summary>
        /// <param name="dataList"></param>
        /// <returns></returns>
        public static bool InsertDataToTempTable(List<Model_NodeHistoryData> dataList)
        {
            try
            {
                StringBuilder sql = new StringBuilder("insert into history_data_waithandle (pointId,`data`,`isAlarm`,`dataTime`,`retryCount`) values");
                for (int i = 0; i < dataList.Count; i++)
                {
                    Model_NodeHistoryData data = dataList[i];
                    sql.AppendLine(string.Format("('{0}','{1}','{2}','{3}',0)", data.PointId, data.Data, (int)data.IsAlarm, data.DataTime.ToString("yyyy-MM-dd HH:mm:ss")));
                    if (i != dataList.Count - 1)
                        sql.Append(",");
                }
                return _SqlHelp.ExecuteNonQuery(sql.ToString()) == dataList.Count;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取指定数量的待处理数据
        /// </summary>
        /// <param name="limit">指定数量</param>
        /// <returns></returns>
        public static List<Model_NodeHistoryData> GetWaitHandleDataList(int limit)
        {
            List<Model_NodeHistoryData> dataList = new List<Model_NodeHistoryData>();
            try
            {
                string sql = string.Format("select * from history_data_waithandle order by retryCount,id limit {0}", limit);
                dataList = _SqlHelp.ExecuteObjects<Model_NodeHistoryData>(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataList;
        }
        //private static string CreatePointDataTable(int coldstorage_ID)
        //{
        //    string tableName = "history_data_" + coldstorage_ID;
        //    string sql = string.Format("CREATE TABLE `{0}` (  `id` bigint(11) NOT NULL AUTO_INCREMENT COMMENT '自动增长标示Id',  `pointId` int(11) NOT NULL COMMENT '探头标示Id',  `data` decimal(9, 4) NOT NULL,  `isAlarm` int(11) NOT NULL DEFAULT '0' COMMENT '报警状态 0 正常 1 报警',  `dataTime` datetime NOT NULL COMMENT '数据记录的时间',  PRIMARY KEY(`id`, `dataTime`),  KEY `datatime` (`dataTime`) USING BTREE) ENGINE = InnoDB AUTO_INCREMENT = 1 DEFAULT CHARSET = utf8 PARTITION BY RANGE(year(dataTime)) SUBPARTITION BY HASH(month(dataTime))SUBPARTITIONS 12(PARTITION y2016 VALUES LESS THAN(2017) ENGINE = InnoDB, PARTITION y2018 VALUES LESS THAN(2018) ENGINE = InnoDB, PARTITION y2019 VALUES LESS THAN(2019) ENGINE = InnoDB, PARTITION y2020 VALUES LESS THAN(2020) ENGINE = InnoDB, PARTITION y2021 VALUES LESS THAN(2021) ENGINE = InnoDB, PARTITION y2022 VALUES LESS THAN(2022) ENGINE = InnoDB, PARTITION y2023 VALUES LESS THAN(2023) ENGINE = InnoDB, PARTITION y2024 VALUES LESS THAN(2024) ENGINE = InnoDB, PARTITION y2025 VALUES LESS THAN(2025) ENGINE = InnoDB, PARTITION ymax VALUES LESS THAN MAXVALUE ENGINE = InnoDB); ", tableName);
        //    try
        //    {
        //        _SqlHelp.ExecuteNonQuery(sql);
        //    }
        //    catch
        //    {

        //    }
        //    return tableName;
        //}
    }
}
