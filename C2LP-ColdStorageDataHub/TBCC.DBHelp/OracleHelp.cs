﻿using System.Data.Common;
//using Oracle.DataAccess.Client;
using System.Data.OracleClient;

namespace DBHelp
{
    public class OracleHelp : AbstractDBHelp
    {
        #region Protected Method

        protected override DbDataAdapter GetDataAdapter(DbCommand command)
        {
            return new OracleDataAdapter(command as OracleCommand);
        }

        protected override DbConnection GetConnection(string connectionString)
        {
            return new OracleConnection(connectionString);
        }

        #endregion

        #region Public Mehtod

        public override DbParameter GetDbParameter(string key, object value)
        {
            return new OracleParameter(key, value);
        }

        public override SqlSourceType DataSqlSourceType
        {
            get { return SqlSourceType.Oracle; }
        }

        #endregion
    
    }
}
