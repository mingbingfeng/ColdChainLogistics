using C2LP.WebService.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace C2LP.WebService.BLL.PDABLL
{
    public class PDA_DeviceServer : BaseServer
    {
        /// <summary>
        /// 检查设备编号是否存在并且唯一
        /// </summary>
        /// <param name="number">设备编号</param>
        /// <returns></returns>
        public static bool CheckNumberExist(string number)
        {
            try
            {
                string sql = "select id,number,name,createAt,actived from device_info where number=?num";
                MySqlParameter[] para = new MySqlParameter[1];
                para[0] = new MySqlParameter("num", number);
                List< Model_PDAInfo> result = _SqlHelp.ExecuteObjects<Model_PDAInfo>(sql, para);
                if (result != null)
                {
                    if (result.Count == 1)
                    {
                        if (result[0].Actived == Model.MyEnum.Enum_Active.Disable)
                            throw new Exception("该设备信息未激活或已被删除!");
                        return true;
                    }
                    if (result.Count > 1)
                        throw new Exception("服务器存重复的的设备记录,请联系管理员.");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return false;
        }

        /// <summary>
        /// 根据编号查询指定设备信息
        /// </summary>
        /// <param name="num">设备编号</param>
        /// <returns></returns>
        public static Model_PDAInfo GetPDAInfoByNum(string num) {
            try
            {
                string sql = "select id,number,name from device_info where number=?num and actived=0";
                MySqlParameter[] para = new MySqlParameter[1];
                para[0] = new MySqlParameter("num", num);
                Model_PDAInfo model = _SqlHelp.ExecuteObject<Model_PDAInfo>(sql, para);
                if (model == null)
                    throw new Exception("该设备信息未激活或已被删除!");
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
