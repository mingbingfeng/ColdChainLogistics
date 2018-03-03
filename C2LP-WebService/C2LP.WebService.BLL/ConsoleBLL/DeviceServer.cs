using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C2LP.WebService.Model;
using C2LP.WebService.Utility;
using MySql.Data.MySqlClient;
using System.Data;

namespace C2LP.WebService.BLL.ConsoleBLL
{
    public class DeviceServer : BaseServer
    {
        public static List<Model_PDAInfo> GetPDALists(int pdaNumber, string pageIndexAndCount)
        {
            string sql = "";
            if (pdaNumber == 0)
                sql = "select * from device_info ";

            if (pdaNumber != 0)
                sql = "select * from device_info where id=?id ;";

            if (pageIndexAndCount != null)
            {
                //截取当前页数
                string page = pageIndexAndCount.Substring(0, pageIndexAndCount.LastIndexOf("."));
                //截取每页显示记录数
                string size = pageIndexAndCount.Substring(pageIndexAndCount.LastIndexOf(".") + 1, pageIndexAndCount.Length - (pageIndexAndCount.LastIndexOf(".") + 1));
                sql += "  order by createAt desc limit " + ((Convert.ToInt32(page) - 1) * Convert.ToInt32(size)) + "," + size + ";";
            }
            else
                sql += " ;";
            MySqlParameter[] para = new MySqlParameter[1];
            para[0] = new MySqlParameter("id", pdaNumber);
            List<Model_PDAInfo> list = _SqlHelp.ExecuteObjects<Model_PDAInfo>(sql, para);
            return list;
        }

        public static Model_PDAInfo EditPDAs(Model_PDAInfo pdaInfo)
        {
            string sql = "";
            //判断PDA设备编号 (系统唯一)是否是唯一的
            if (pdaInfo.Id == 0)
            {
                Model_PDAInfo mp = GetPDAOnly(pdaInfo);
                if (mp != null)
                    throw new Exception("设备编号已存在");
            }
            else
            {
                sql = "select * from device_info where id=?id";
                MySqlParameter[] pdapara = new MySqlParameter[1];
                pdapara[0] = new MySqlParameter("id", pdaInfo.Id);
                Model_PDAInfo mp = _SqlHelp.ExecuteObject<Model_PDAInfo>(sql, pdapara);
                if (mp.Number != pdaInfo.Number)
                {
                    Model_PDAInfo mpda = GetPDAOnly(pdaInfo);
                    if (mpda != null)
                        throw new Exception("设备编号已存在");
                }
                    
            }
            if (pdaInfo.Id == 0)
            {
                sql = "insert into device_info(number,name,createAt,actived) values(?number,?name,?createAt,?actived) ;";
            }
            else
            {
                sql = "update device_info set number=?number,name=?name,createAt=?createAt,actived=?actived where id=?id ;";
            }

            MySqlParameter[] para = new MySqlParameter[5];
            para[0] = new MySqlParameter("number", pdaInfo.Number);
            para[1] = new MySqlParameter("name", pdaInfo.Name);
            para[2] = new MySqlParameter("createAt", pdaInfo.CreateAt);
            para[3] = new MySqlParameter("actived", pdaInfo.Actived);
            para[4] = new MySqlParameter("id", pdaInfo.Id);

            int result = 0;
            if (pdaInfo.Id == 0)
                result = _SqlHelp.ExecuteNonQuery(sql, para);
            else
                result = _SqlHelp.ExecuteNonQuery(sql, para);
            if (result != 1)
            {
                throw new Exception("操作失败");
            }
            return pdaInfo;
        }
        public static Model_PDAInfo GetPDAOnly(Model_PDAInfo pdaInfo)
        {
            string sql = "select * from device_info where number=?number";
            MySqlParameter[] pdapara = new MySqlParameter[1];
            pdapara[0] = new MySqlParameter("number", pdaInfo.Number);
            Model_PDAInfo pda = _SqlHelp.ExecuteObject<Model_PDAInfo>(sql, pdapara);
            return pda;
        }
        /// <summary>
        /// 显示目的地信息
        /// </summary>
        /// <param name="deviceId"></param>
        /// <param name="pdaNumber"></param>
        /// <param name="pageIndexAndCount"></param>
        /// <returns></returns>
        public static List<Model_Destination> GetPDADestinationLists(int deviceId ,int pdaNumber, string pageIndexAndCount)
        {
            string sql = "";
            if (deviceId!=0)
            {
                sql = "select * from device_destination where deviceId=?deviceId ;";
            }
            MySqlParameter[] para = new MySqlParameter[1];
            para[0] = new MySqlParameter("deviceId", deviceId);

            List<Model_Destination> list = _SqlHelp.ExecuteObjects<Model_Destination>(sql, para);

            return list;
        }
        /// <summary>
        /// 添加PDA 设备目的地
        /// </summary>
        /// <param name="destination"></param>
        /// <returns></returns>
        public static Model_Destination EditPDADestinations(Model_Destination destination)
        {
            string sql = "";
            if (destination.Id == 0)
                sql = "insert into device_destination(deviceId,address) values(?deviceId,?address); ";
            else
                sql = "delete from device_destination where id=?id ;";
            MySqlParameter[] para = new MySqlParameter[3];
            para[0] = new MySqlParameter("deviceId",destination.DeviceId);
            para[1] = new MySqlParameter("address",destination.Address);
            para[2] = new MySqlParameter("id",destination.Id);
            int result = _SqlHelp.ExecuteNonQuery(sql,para);
            if (result!=1)
            {
                throw new Exception("操作失败");
            }
            return destination;
        }
    }
}
