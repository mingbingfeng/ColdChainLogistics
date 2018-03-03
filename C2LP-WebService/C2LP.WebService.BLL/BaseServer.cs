using C2LP.WebService.Utility;
using System.Configuration;

namespace C2LP.WebService.BLL
{
    public class BaseServer
    {
        /// <summary>
        /// 是否更新数据库帮助类的连接字符串
        /// </summary>
        public static bool IsUpdateSqlHelp = false;

        private static MySqlHelper SqlHelp;

        protected static MySqlHelper _SqlHelp
        {
            get
            {
                if (SqlHelp == null || IsUpdateSqlHelp)
                {
                    ConfigurationManager.RefreshSection("connectionStrings");
                    string connStr = ConfigurationManager.ConnectionStrings["MySql"].ConnectionString;
                    SqlHelp = new MySqlHelper(connStr);
                }
                return SqlHelp;
            }

        }
    }
}
