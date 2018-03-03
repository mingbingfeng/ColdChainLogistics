using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace C2LP.WebService.Utility
{
    /// <summary>
    /// MySqlHelper扩展(依赖AutoMapper.dll)
    /// </summary>
    public sealed partial class MySqlHelper
    {
        #region 实例方法

        public T ExecuteObject<T>(string commandText, params MySqlParameter[] parms)
        {
            return ExecuteObject<T>(this.ConnectionString, commandText, parms);
        }

        public List<T> ExecuteObjects<T>(string commandText, params MySqlParameter[] parms)
        {
            return ExecuteObjects<T>(this.ConnectionString, commandText, parms);
        }

        #endregion

        #region 静态方法

        public static T ExecuteObject<T>(string connectionString, string commandText, params MySqlParameter[] parms)
        {
            try
            {
                //DataTable dt = ExecuteDataTable(connectionString, commandText, parms);
                //return AutoMapper.Mapper.DynamicMap<List<T>>(dt.CreateDataReader()).FirstOrDefault();
                using (MySqlDataReader reader = ExecuteDataReader(connectionString, commandText, parms))
                {
                    return AutoMapper.Mapper.DynamicMap<List<T>>(reader).FirstOrDefault();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public static List<T> ExecuteObjects<T>(string connectionString, string commandText, params MySqlParameter[] parms)
        {
            //DataTable dt = ExecuteDataTable(connectionString, commandText, parms);
            //return AutoMapper.Mapper.DynamicMap<List<T>>(dt.CreateDataReader());
            using (MySqlDataReader reader = ExecuteDataReader(connectionString, commandText, parms))
            {
                return AutoMapper.Mapper.DynamicMap<List<T>>(reader);
            }
        }



        public List<String[]> Execute4ListOfObject(string commandText)
        {
            List<String[]> list = new List<String[]>();

            MySqlDataReader dataReader = ExecuteDataReader(commandText);

            while (dataReader.Read())
            {
                String[] obj = new String[dataReader.FieldCount];


                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    if (dataReader[i] != null)
                        obj[i] = dataReader[i].ToString();
                    else
                        obj[i] = string.Empty;
                }

                list.Add(obj);
            }

            if (dataReader != null) { dataReader.Close(); }

            return list;

        }

        #endregion
    }


}
