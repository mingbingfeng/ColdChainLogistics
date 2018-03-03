using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;

namespace C2LP.PDA.Datas.BLL
{
    public class DictionaryServer : BaseServer
    {
        /// <summary>
        /// 获取本机信息
        /// </summary>
        /// <returns></returns>
        public static string GetPDAInfo(Enum_DicKey dicKey)
        {
            string number = null;
            string sql = "select detail from c2lp_dictionary where name = '" + dicKey.ToString() + "';";
            try
            {
                object result = _SqlHelp.ExecuteScalar(sql, System.Data.CommandType.Text);
                if (result != null)
                    number = result.ToString();
            }
            catch
            {
                return null;
            }
            return number;
        }

        /// <summary>
        /// 设置本机信息
        /// </summary>
        public static void SetPDAInfo(Enum_DicKey dicKey, object value)
        {
            if (value == null)
                return;
            string sql = "delete from c2lp_dictionary where name='" + dicKey.ToString() + "';";
            try
            {
                //_SqlHelp.ExecuteNonQuery(sql, System.Data.CommandType.Text);
                sql += "insert into c2lp_dictionary values('" + dicKey.ToString() + "','" + value + "')";
                _SqlHelp.ExecuteNonQuery(sql, System.Data.CommandType.Text);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }



    public enum Enum_DicKey
    {
        pdaNumber,
        storageName,
        destination,
        lastSyncTime,
        pdaId,
        uploadCycle,
        maxUploadOrderCount,
        maxUploadNodeCount,
        webServiceAddress,
        defaultProvince,
        autoReturnDelay
    }
}
