using System.Data.Common;
using MySql.Data.MySqlClient;

namespace DBHelp
{
    public class MySqlHelp : AbstractDBHelp
    {
        #region Protected Method

        protected override DbDataAdapter GetDataAdapter(DbCommand command)
        {
            return new MySqlDataAdapter(command as MySqlCommand);
        }

        protected override DbConnection GetConnection(string connectionString)
        {
            return new MySqlConnection(connectionString);
        }

        #endregion

        #region Public Mehtod

        public override DbParameter GetDbParameter(string key, object value)
        {
            return new MySqlParameter(key, value);
        }

        public override SqlSourceType DataSqlSourceType
        {
            get { return SqlSourceType.MySql; }
        }

        #endregion

    }
}
