using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;

namespace C2LP.PDA.Datas.BLL
{
    public class BaseServer
    {
        private static DBHelper SqlHelp;

        protected static DBHelper _SqlHelp
        {
            get
            {
                if (SqlHelp == null )
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

        /// <summary>
        /// 重新设置数据库名称
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        public static void SetSqlHelpDbName(string dbName ){
            SqlHelp = new DBHelper(dbName);
        }

    }
}
