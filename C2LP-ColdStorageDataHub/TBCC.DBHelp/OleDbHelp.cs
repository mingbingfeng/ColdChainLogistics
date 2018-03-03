using System.Data.Common;
using System.Data.OleDb;

namespace DBHelp
{
    public class OleDbHelp : AbstractDBHelp
    {
        #region Protected Method

        protected override DbDataAdapter GetDataAdapter(DbCommand command)
        {
            //return new OracleDataAdapter(command as OracleCommand);
            return new OleDbDataAdapter(command as OleDbCommand);
        }

        protected override DbConnection GetConnection(string connectionString)
        {
            //return new OracleConnection(connectionString);
            return new OleDbConnection(connectionString);
        }

        #endregion

        #region Public Mehtod

        public override DbParameter GetDbParameter(string key, object value)
        {
            //return new OracleParameter(key, value);
            return new OleDbParameter(key, value);
        }

        public override SqlSourceType DataSqlSourceType
        {
            get { return SqlSourceType.Oracle; }
        }

        #endregion
    
    }
}
