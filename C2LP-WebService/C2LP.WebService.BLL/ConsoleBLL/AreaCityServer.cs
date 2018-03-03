using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C2LP.WebService.Model;
using C2LP.WebService.Utility;
using MySql.Data.MySqlClient;

namespace C2LP.WebService.BLL.ConsoleBLL
{
    public class AreaCityServer : BaseServer
    {
        
        public static List<Model_Region> AreaCitys(int parentId)
        {
            string sql = "select id,name,parentId from region where parentId=?parentId;";
            MySqlParameter[] para = new MySqlParameter[1];
            para[0] = new MySqlParameter("parentId", parentId);
            List<Model_Region> region = new List<Model_Region>();
            //Model_Region reg = new Model_Region();
            //reg.Id = 0;
            //reg.Name = "全部";
            //region.Add(reg);
            region.AddRange(_SqlHelp.ExecuteObjects<Model_Region>(sql, para));
            return region;
        }
        #region 更新时间
        public static List<Model_Region> GetRegionDateTime(int parentId)
        {
            string sql = "select * from region where parentId=?parentId;";
            MySqlParameter[] para = new MySqlParameter[1];
            para[0] = new MySqlParameter("parentId", parentId);
            List<Model_Region> region = new List<Model_Region>();

            region = _SqlHelp.ExecuteObjects<Model_Region>(sql, para);
            return region;
        }

        /// <summary>
        /// 添加/修改行政区域信息
        /// </summary>
        /// <param name="mregion"></param>
        /// <returns></returns>
        public static int RegionEdit(Model_Region mregion)
        {
            if (mregion==null)
            {
                throw new Exception("没有行政区域信息");
            }
             
            string sql = "";
            int result = 0;
            int max = 0;
            sql = string.Format("select max(id) from region ;");
            max=Convert.ToInt32( _SqlHelp.ExecuteScalar(sql));
            max = max + 1;
            if (mregion.Id==0)
            {
                 sql = string.Format("insert into region(id,`code`,`name`,parentId,`level`,`order`,nameEnglish,nameShortEnglish,lastUpdateTime)  values({0},'{1}','{2}',{3},{4},{5},'{6}','{7}','{8}'); ",
                    max,mregion.Code,mregion.Name,mregion.ParentId,mregion.Level,mregion.Order,mregion.NameEnglish,mregion.NameShortEnglish,DateTime.Now);
            }
            else
            {
                sql = string.Format("update region set `code`='{0}',`name`='{1}',parentId={2},`level`={3},`order`={4},nameEnglish='{5}',nameShortEnglish='{6}',lastUpdateTime='{7}' where id={8} ;",
                    mregion.Code,mregion.Name,mregion.ParentId,mregion.Level,mregion.Order,mregion.NameEnglish,mregion.NameShortEnglish, DateTime.Now,mregion.Id);
            }
            if (mregion.Id==0)
                result = _SqlHelp.ExecuteNonQuery(sql);
            else
                result = _SqlHelp.ExecuteNonQuery(sql);
            
            if (result!=1)
            {
                throw new Exception("行政区域更新失败");
            }
            return result;
        }
        #endregion

        #region 2017-7-25 增加区域选项，并根据区域显示下游客户
        public static List<Model_Region> GetRegionCount()
        {
            string sql = string.Format("select * from region where parentId in(0,1) order by id;");
            List<Model_Region> regionlist = _SqlHelp.ExecuteObjects<Model_Region>(sql);
            return regionlist;
        }
        public static List<Model_Region> GetCity(int parentId)
        {
            string sql = string.Format("select * from region where parentId={0} order by id;", parentId);
            List<Model_Region> regionlist = _SqlHelp.ExecuteObjects<Model_Region>(sql);
            return regionlist;
        }
        #endregion
    }
}
