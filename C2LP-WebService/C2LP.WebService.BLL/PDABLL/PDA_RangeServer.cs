using C2LP.WebService.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace C2LP.WebService.BLL.PDABLL
{
    public class PDA_RangeServer : BaseServer
    {
        /// <summary>
        /// 根据设备ID获取其负责区域信息
        /// </summary>
        /// <param name="pId">设备ID</param>
        /// <returns></returns>
        public static List<PDA_Model_Region> GetRangeByPId(int pId)
        {
            List<PDA_Model_Region> list = new List<PDA_Model_Region>();
            try
            {
                string sql = "select regionId from region_device where deviceId=?id";
                MySqlParameter[] para = new MySqlParameter[1];
                para[0] = new MySqlParameter("id", pId);
                List<Model_DeviceRegion> rList = _SqlHelp.ExecuteObjects<Model_DeviceRegion>(sql, para);
                List<int> rIdList = rList.Select(l => l.RegionId).ToList();
                sql = "select id,`name`,parentId from `region` ";
                if (rIdList != null && rIdList.Count > 0 && rIdList[0] != 1)
                {
                    string parentSql = sql + " where id in(" + string.Join(",", rIdList) + ") or parentId in (" + string.Join(",", rIdList) + ") order by id,parentId";
                    list = _SqlHelp.ExecuteObjects<PDA_Model_Region>(parentSql);
                    List<int> idList = (from l in list where l.ParentId != 1 select l.Id).ToList();
                    parentSql = sql + " where parentId in (" + string.Join(",", idList) + ") order by id,parentId";
                    list.AddRange(_SqlHelp.ExecuteObjects<PDA_Model_Region>(parentSql));
                }
                else
                    list = _SqlHelp.ExecuteObjects<PDA_Model_Region>(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }

        public static List<PDA_Model_Region> GetRangeByPId(int pId, int maxId)
        {
            List<PDA_Model_Region> list = new List<PDA_Model_Region>();
            int maxLenth = 250;
            PDA_Model_Region[] resul;
            try
            {
                string sql = "select regionId from region_device where deviceId=?id";
                MySqlParameter[] para = new MySqlParameter[1];
                para[0] = new MySqlParameter("id", pId);
                List<Model_DeviceRegion> rList = _SqlHelp.ExecuteObjects<Model_DeviceRegion>(sql, para);
                List<int> rIdList = rList.Select(l => l.RegionId).ToList();
                sql = "select id,`name`,parentId from `region` ";
                if (rIdList != null && rIdList.Count > 0 && rIdList[0] != 1)
                {
                    string parentSql = sql + " where id in(" + string.Join(",", rIdList) + ") or parentId in (" + string.Join(",", rIdList) + ") order by id,parentId";
                    list = _SqlHelp.ExecuteObjects<PDA_Model_Region>(parentSql);
                    List<int> idList = (from l in list where l.ParentId != 1 select l.Id).ToList();
                    parentSql = sql + " where parentId in (" + string.Join(",", idList) + ") order by id,parentId";
                    list.AddRange(_SqlHelp.ExecuteObjects<PDA_Model_Region>(parentSql));
                }
                else
                    list = _SqlHelp.ExecuteObjects<PDA_Model_Region>(sql);
                List<PDA_Model_Region> tempList = list.Where(l => l.Id > maxId).ToList();
                if (tempList.Count < maxLenth)
                    maxLenth = tempList.Count;
                if (maxLenth == 0)
                    return new List<PDA_Model_Region>();
                resul = new PDA_Model_Region[maxLenth];
                tempList.CopyTo(0, resul, 0, maxLenth);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resul.ToList();
        }

        /// <summary>
        /// 获取指定时间后更新的区域信息
        /// </summary>
        /// <param name="getTime">获取时间</param>
        /// <param name="maxId">当前获取到的区域信息索引</param>
        /// <returns></returns>
        public static List<PDA_Model_Region> GetNewRegions(DateTime getTime, int maxId)
        {
            List<PDA_Model_Region> list = new List<PDA_Model_Region>();
            try
            {
                string sql = string.Format("select id,`name`,parentId from region where lastUpdateTime>'{0}' and id>{1} limit {2}", getTime.ToString("yyyy-MM-dd HH:mm:ss"), maxId, 250);
                list = _SqlHelp.ExecuteObjects<PDA_Model_Region>(sql);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }
    }
}
