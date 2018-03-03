using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using System.Data;

namespace C2LP.PDA.Datas.BLL
{
    public class BaseServer
    {
        public static object _lockObj = new object();

        private static DBHelper SqlHelp;

        public static DBHelper _SqlHelp
        {
            get
            {
                lock (_lockObj)
                {
                    if (SqlHelp == null)
                    {
                        //string filePath = "\\Flash Storage\\Program";
                        //string filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName);
                        string filePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase.ToString());
                        filePath += "\\datas.db";
                        //string filePath = @"file:E:\develop\ColdChainLogistics\C2LP-PDA\C2LP.PDA.APP\bin\Debug\datas.db";
                        SqlHelp = new DBHelper(filePath);
                    }
                    return SqlHelp;
                }

            }

        }

        /// <summary>
        /// 重新设置数据库名称
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        public static void SetSqlHelpDbName(string dbName)
        {
            SqlHelp = new DBHelper(dbName);
        }

        /// <summary>
        /// 动态添加冷藏载体表,用于扫描中间节点时指定上一个冷藏载体
        /// </summary>
        public static void AddTable_c2lp_storage_scan() {
            try
            {
                string sql = "SELECT COUNT(*) FROM sqlite_master where type='table' and name='c2lp_storage_scan'";
                object obj = _SqlHelp.ExecuteScalar(sql, System.Data.CommandType.Text);
                if (Convert.ToInt32(obj) == 0)
                {
                    sql = "CREATE TABLE 'main'.'c2lp_storage_scan' ('storageId' INTEGER NOT NULL,'storageName'  TEXT NOT NULL,'storageType'  INTEGER NOT NULL, PRIMARY KEY ('storageId'));";
                    _SqlHelp.ExecuteNonQuery(sql, System.Data.CommandType.Text);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 动态添加操作记录表
        /// </summary>
        public static void AddTable_c2lp_optRecord()
        {
            try
            {
                string sql = "SELECT COUNT(*) FROM sqlite_master where type='table' and name='c2lp_optRecord'";
                object obj = _SqlHelp.ExecuteScalar(sql, System.Data.CommandType.Text);
                if (Convert.ToInt32(obj) == 0)
                {
                    sql = "CREATE TABLE 'main'.'c2lp_optRecord' ('Id'  INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL,'OptTime'  TEXT NOT NULL,'OptType'  TEXT NOT NULL,'Content'  TEXT NOT NULL);";
                    _SqlHelp.ExecuteNonQuery(sql, System.Data.CommandType.Text);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 动态添加接入上游单位表
        /// </summary>
        public static void AddTable_c2lp_consignor() {
            try
            {
                string sql = "SELECT COUNT(*) FROM sqlite_master where type='table' and name='c2lp_consignor'";
                object obj = _SqlHelp.ExecuteScalar(sql, System.Data.CommandType.Text);
                if (Convert.ToInt32(obj) == 0)
                {
                    sql = "CREATE TABLE 'main'.'c2lp_consignor' ('consignorId'  INTEGER PRIMARY KEY NOT NULL,'consignorName'  TEXT NOT NULL);";
                    _SqlHelp.ExecuteNonQuery(sql, System.Data.CommandType.Text);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 检查节点信息表c2lp_node中是否存在parentStorageId字段，没有就添加
        /// </summary>
        public static void AddNode_ParentStorageId()
        {
            try
            {
                string sqlExsit = "select Count(*) from sqlite_master  where tbl_name='c2lp_node' and sql like '%parentStorageId%';";
                object obj = _SqlHelp.ExecuteScalar(sqlExsit, CommandType.Text);
                if (Convert.ToInt32(obj) == 0)
                {
                    string sql = "alter table c2lp_node add parentStorageId int null";
                    _SqlHelp.ExecuteNonQuery(sql, CommandType.Text);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 检查上游单位表c2lp_consignor中是否存在linkType字段，没有就添加
        /// </summary>
        public static void AddLinkType() {
            try
            {
                string sqlExsit = "select Count(*) from sqlite_master  where tbl_name='c2lp_consignor' and sql like '%linkType%';";
                object obj = _SqlHelp.ExecuteScalar(sqlExsit, CommandType.Text);
                if (Convert.ToInt32(obj) == 0)
                {
                    string sql = "alter table c2lp_consignor add linkType int null";
                    _SqlHelp.ExecuteNonQuery(sql, CommandType.Text);
                }
            }
            catch 
            {
            }
        }

        /// <summary>
        /// 检查上游单位表c2lp_consignor中是否存在linkRegex字段，没有就添加
        /// </summary>
        public static void AddLinkRegex()
        {
            try
            {
                string sqlExsit = "select Count(*) from sqlite_master  where tbl_name='c2lp_consignor' and sql like '%linkRegex%';";
                object obj = _SqlHelp.ExecuteScalar(sqlExsit, CommandType.Text);
                if (Convert.ToInt32(obj) == 0)
                {
                    string sql = "alter table c2lp_consignor add linkRegex TEXT null";
                    _SqlHelp.ExecuteNonQuery(sql, CommandType.Text);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 检查上游单位表c2lp_optRecord中是否存在OptNumber字段，没有就添加
        /// </summary>
        public static void AddColunmForScanRecord()
        {
            try
            {
                string sqlExsit = "select Count(*) from sqlite_master  where tbl_name='c2lp_optRecord' and sql like '%OptNumber%';";
                object obj = _SqlHelp.ExecuteScalar(sqlExsit, CommandType.Text);
                if (Convert.ToInt32(obj) == 0)
                {
                    string sql = "alter table c2lp_optRecord add OptNumber TEXT null";
                    _SqlHelp.ExecuteNonQuery(sql, CommandType.Text);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 检查上游单位表c2lp_optRecord中是否存在OptCustomerId字段，没有就添加
        /// </summary>
        public static void AddColunmForScanRecord1()
        {
            try
            {
                string sqlExsit = "select Count(*) from sqlite_master  where tbl_name='c2lp_optRecord' and sql like '%OptCustomerId%';";
                object obj = _SqlHelp.ExecuteScalar(sqlExsit, CommandType.Text);
                if (Convert.ToInt32(obj) == 0)
                {
                    string sql = "alter table c2lp_optRecord add OptCustomerId INTEGER null";
                    _SqlHelp.ExecuteNonQuery(sql, CommandType.Text);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 检查上游单位表c2lp_optRecord中是否存在OptTypeId字段，没有就添加
        /// </summary>
        public static void AddColunmForScanRecord2()
        {
            try
            {
                string sqlExsit = "select Count(*) from sqlite_master  where tbl_name='c2lp_optRecord' and sql like '%OptTypeId%';";
                object obj = _SqlHelp.ExecuteScalar(sqlExsit, CommandType.Text);
                if (Convert.ToInt32(obj) == 0)
                {
                    string sql = "alter table c2lp_optRecord add OptTypeId INTEGER null";
                    _SqlHelp.ExecuteNonQuery(sql, CommandType.Text);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 检查各个信息表是否存在consignorId字段，没有就添加
        /// </summary>
        public static void AddConsignorId()
        {
            try
            {
                string[] tabelNameArr = new string[] { "c2lp_node", "c2lp_postback", "huadong_tms_order" };
                string sqlExsit = "select Count(*) from sqlite_master  where tbl_name='[TableName]' and sql like '%consignorId%';";
                string sql = string.Empty;
                foreach (string tabelName in tabelNameArr)
                {
                    sqlExsit = sqlExsit.Replace("[TableName]", tabelName);
                    object obj = _SqlHelp.ExecuteScalar(sqlExsit, CommandType.Text);
                    if (Convert.ToInt32(obj) == 0)
                    {
                        sql = "alter table " + tabelName + " add ConsignorId int null";
                            _SqlHelp.ExecuteNonQuery(sql, CommandType.Text);
                    }
                    sqlExsit = sqlExsit.Replace(tabelName,"[TableName]");
                }
                
            }
            catch
            {
            }
        }
    }
}
