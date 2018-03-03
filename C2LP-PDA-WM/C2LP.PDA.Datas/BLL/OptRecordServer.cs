using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using C2LP.PDA.Datas.Model;

namespace C2LP.PDA.Datas.BLL
{
    public class OptRecordServer : BaseServer
    {
        /// <summary>
        /// 添加操作记录
        /// </summary>
        /// <param name="opt"></param>
        public static void AddOptRecord(OptRecord opt)
        {
            string sql = string.Format("insert into c2lp_optRecord (OptTime,OptType,'Content','optNumber','optCustomerId','optTypeId') values('{0}','{1}','{2}','{3}','{4}','{5}');", opt.OptTime, opt.OptType, opt.Content, opt.OptNumber, opt.OptCustomerId, opt.OptTypeId);
            try
            {
                _SqlHelp.ExecuteNonQuery(sql, System.Data.CommandType.Text);
            }
            catch
            {

            }
        }

        public static void GetOptCount(out int sum, ref DateTime dtStart,ref DateTime dtEnd) {
            try
            {
                string sql = "select Count(*) from c2lp_optRecord";
                sum = Convert.ToInt32(_SqlHelp.ExecuteScalar(sql, System.Data.CommandType.Text));
                if (sum == 0)
                    return;
                sql = "select Min(optTime) from c2lp_optRecord";
                dtStart = Convert.ToDateTime(_SqlHelp.ExecuteScalar(sql, System.Data.CommandType.Text));
                sql = "select Max(optTime) from c2lp_optRecord";
                dtEnd = Convert.ToDateTime(_SqlHelp.ExecuteScalar(sql, System.Data.CommandType.Text));
            }
            catch (Exception ex)
            {
                throw new Exception("查询记录失败:"+ex.Message);
            }
        }

        public static void ClearOptRecord() {
            try
            {
                string sql = "delete from c2lp_optRecord;update sqlite_sequence set seq=0 where name='c2lp_optRecord';";
                _SqlHelp.ExecuteNonQuery(sql, System.Data.CommandType.Text);
            }
            catch (Exception ex)
            {
                throw new Exception("清空操作记录失败:" + ex.Message);
            }
        }

        public static void ClearOptRecordTime(string dtStart,string dtEnd)
        {
            try
            {
                string sql = string.Format("delete from c2lp_optRecord where OptTime>'{0}' and OptTime<'{1}';",dtStart,dtEnd);
                _SqlHelp.ExecuteNonQuery(sql, System.Data.CommandType.Text);
            }
            catch (Exception ex)
            {
                throw new Exception("清空操作记录失败:" + ex.Message);
            }
        }
    }
}
