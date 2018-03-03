
using DBHelp;
using System;
using System.Configuration;

namespace C2LP.ColdStorageDataHubClient.DBHelper.BLL
{
    public class BaseServer
    {

        private static IDBHelp DBHelper;
        /// <summary>
        /// 数据库操作类
        /// </summary>
        public static IDBHelp _DBHelper
        {
            get
            {
                if (DBHelper == null)
                {
                    DBHelper = new MySqlHelp();
                }

                DBHelper.ConnectionString = ConfigurationManager.ConnectionStrings["ServerMySql"].ConnectionString;
                return DBHelper;
            }

            set { DBHelper = value; }
        }

        /// <summary>
        /// 判断表是否存在
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns></returns>
        public static bool CheckTableExist(string tableName)
        {
            string sql = string.Format("select `TABLE_NAME` from `INFORMATION_SCHEMA`.`TABLES` where `TABLE_SCHEMA`='tbcc_lsc_db' and `TABLE_NAME`='{0}' ;", tableName);
            object obj = _DBHelper.ExecuteScalar(sql);
            return obj != null;
        }

        /// <summary>
        /// 测试连接
        /// </summary>
        /// <returns></returns>
        protected static bool TestConnect(string connStr)
        {
            return _DBHelper.TestConnect(connStr);
        }

        /// <summary>
        /// 保存字典值
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public static void SetDicValue(string key, object value)
        {
            try
            {
                string selectSql = string.Format("select `value` from fdapprojectinfo where `Key`='{0}'", key);
                string insertSql = string.Format("INSERT INTO `fdapprojectinfo` ( `Key`, `value`) VALUES ( '{0}', '{1}');", key, value);
                string updateSql = string.Format("UPDATE `fdapprojectinfo` SET `value`='{0}' WHERE (`Key`='{1}');", value, key);
                object selectValue = _DBHelper.ExecuteScalar(selectSql);
                if (selectValue == null)
                    _DBHelper.ExecuteNonQuery(insertSql);
                else if (value != selectValue)
                    _DBHelper.ExecuteNonQuery(updateSql);
            }
            catch
            {

            }
        }

        /// <summary>
        /// 添加上报标记列
        /// </summary>
        /// <param name="tableName">表名</param>
        public static bool AddUploadStateColumn(string tableName, DateTime dt)
        {
            try
            {
                string sql = "SELECT column_name FROM information_schema.columns WHERE table_schema='tbcc_lsc_db' AND table_name = '" + tableName + "' AND column_name = 'fdap4jcUploadState'";
                object isExist = _DBHelper.ExecuteScalar(sql);
                if (isExist == null)
                {
                    sql = string.Format("alter table {0} add column fdap4jcUploadState int null default 0;update {0} set fdap4jcUploadState=1 where updateTime<='{1}'", tableName, dt.ToString("yyyy-MM-dd HH:mm:ss"));

                    _DBHelper.ExecuteNonQuery(sql);
                    return true;
                }
            }
            catch
            {
            }
            return false;
        }


    }
}
