using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C2LP.WebService.Model;
using C2LP.WebService.Utility;
using MySql.Data.MySqlClient;
using System.Data;
using System.Threading;

namespace C2LP.WebService.BLL.ConsoleBLL
{
    public class ColdstorageServer:BaseServer
    {
       
        public static List<Model_ColdstoragePDA> GetColdStorages(int storageId, int storageType, string pageIndexAndCount)
        {
            string sql = "";
            if (storageId == 0)
            {
                sql = "select cs.id as StorageId,cs.storageName,cs.storageType,cs.driver,cs.driverTel,cs.remark,cs.createAt as storageCreateAt,cs.actived as storageActived," +
                            "sd.id as StorageDeviceId,sd.isDefault," +
                            "di.id as PDAid,di.number,di.name,di.createAt as deviceCreateAt,di.actived as deviceActived " +
                            " from coldstorage as cs "+
                            " inner join storage_device as sd on cs.id = sd.storageId "+
                            " inner join device_info as di on sd.deviceId = di.id "+
                            " where storageType=?storageType order by cs.createAt desc  ";
            }
            if (pageIndexAndCount != null)
            {
                //截取当前页数
                string page = pageIndexAndCount.Substring(0, pageIndexAndCount.LastIndexOf("."));
                //截取每页显示记录数
                string size = pageIndexAndCount.Substring(pageIndexAndCount.LastIndexOf(".") + 1, pageIndexAndCount.Length - (pageIndexAndCount.LastIndexOf(".") + 1));
                sql += " limit " + ((Convert.ToInt32(page) - 1) * Convert.ToInt32(size)) + "," + size + ";";
            }
            else
                sql += " ;";
            MySqlParameter[] para = new MySqlParameter[1];
            para[0] = new MySqlParameter("storageType",storageType);
            List<Model_ColdstoragePDA> list = _SqlHelp.ExecuteObjects<Model_ColdstoragePDA>(sql,para);

            return list;
        }
        
        /// <summary>
        /// 新增/更新
        /// </summary>
        /// <param name="coldstorageInfo"></param>
        /// <param name="pdaId"></param>
        /// <param name="isDeleteStorage">True:storageID不为0时执行删除操作;False:storageID为0时不删除执行添加操作</param>
        /// <param name="defaultDevice">0不是默认，1是默认</param>
        /// <returns></returns>
        public static Model_ColdStorage EditColdstorages(Model_ColdStorage coldstorageInfo, int defaultDevice,int storageDeviceId, int pdaId, bool isDeleteStorage)
        {
            string sql = "";
            
            //判断修改的时候冷库/车载车牌的名称是否变更过
            if (coldstorageInfo.Id != 0 && isDeleteStorage == true)
            {
                sql = "select * from coldstorage where id=?id ;";
                MySqlParameter[] coldid = new MySqlParameter[1];
                coldid[0] = new MySqlParameter("id", coldstorageInfo.Id);
                Model_ColdStorage co = _SqlHelp.ExecuteObject<Model_ColdStorage>(sql, coldid);
                if (co.StorageName != coldstorageInfo.StorageName)
                {
                    Model_ColdStorage SN = GetStorName(coldstorageInfo);
                    if (SN != null)
                        throw new Exception("仓库名称/车载系统车牌已存在");
                }
            }
            if (coldstorageInfo.Id == 0 && isDeleteStorage == false)
            {
                Model_ColdStorage SN = GetStorName(coldstorageInfo);
                if (SN != null)
                    throw new Exception("仓库名称/车载系统车牌已存在");
            }

            if (coldstorageInfo.Id == 0 && isDeleteStorage == false)
            {
                sql = "insert into coldstorage(storageName,storagetype,driver,driverTel,remark,createAt,actived) " +
                    "values(?storageName,?storagetype,?driver,?driverTel,?remark,?createAt,?actived);select LAST_INSERT_ID(); ";
            }
            if (coldstorageInfo.Id != 0 && isDeleteStorage == true)
            {
                sql = "update coldstorage set storageName=?storageName,storageType=?storageType,driver=?driver,driverTel=?driverTel,remark=?remark,actived=?actived " +
                        "where id =?id ;";
            }
            MySqlParameter[] para = new MySqlParameter[8];
            para[0] = new MySqlParameter("storageName", coldstorageInfo.StorageName);
            para[1] = new MySqlParameter("storagetype", coldstorageInfo.StorageType);
            para[2] = new MySqlParameter("driver", coldstorageInfo.Driver);
            para[3] = new MySqlParameter("driverTel", coldstorageInfo.DriverTel);
            para[4] = new MySqlParameter("remark", coldstorageInfo.Remark);
            para[5] = new MySqlParameter("createAt", coldstorageInfo.CreateAt);
            para[6] = new MySqlParameter("actived",coldstorageInfo.Actived);
            para[7] = new MySqlParameter("id", coldstorageInfo.Id);
            //返回添加数据的id
            ulong result = 0;
            if (coldstorageInfo.Id == 0)
            {
                result = (ulong)_SqlHelp.ExecuteScalar(sql, para);
                if (result <= 0)
                    throw new Exception("操作失败");
                //线程 动态创建数据表
                Thread t = new Thread(new ParameterizedThreadStart(ColdstorageServer.GetThread));
                t.Start(result);
            }
            else
            {
                int resu = _SqlHelp.ExecuteNonQuery(sql, para);
                if (resu != 1)
                    throw new Exception("操作失败");
                else
                    result = (ulong)coldstorageInfo.Id;
            }


            if (coldstorageInfo.Id == 0 && isDeleteStorage == false)
                sql = "insert into storage_device(storageId,deviceId,isDefault) values(?storageId,?deviceId,?isDefault)";
            else
                sql = "update storage_device set storageId=?storageId,deviceId=?deviceId,isDefault=?isDefault " +
                        "where id =?id ; ";
            
            MySqlParameter[] pa = new MySqlParameter[4];
            pa[0] = new MySqlParameter("storageId", result);
            pa[1] = new MySqlParameter("deviceId", pdaId);
            pa[2] = new MySqlParameter("isDefault", defaultDevice);

            if (coldstorageInfo.Id != 0 && isDeleteStorage == true)
            {
                //获取冷库，车载与PDA设备关联的id
                foreach (Model_Storage_Device item in GetStoDevi(storageDeviceId))
                {
                    pa[3] = new MySqlParameter("id", item.id);
                }
            }
            else
                pa[3] = new MySqlParameter("id",null);
            int stodev = 0;
            stodev = _SqlHelp.ExecuteNonQuery(sql, pa);
            if (stodev != 1)
                throw new Exception("操作失败");

            return coldstorageInfo;
        }

        public static Model_ColdStorage GetStorName(Model_ColdStorage coldstorageInfo)
        {
            //判断冷库/车载车牌的名称是否存在
            string sql = "select * from coldstorage where storageName=?storageName ";
            MySqlParameter[] p = new MySqlParameter[1];
            p[0] = new MySqlParameter("storageName", coldstorageInfo.StorageName);
            Model_ColdStorage coldstoragename = _SqlHelp.ExecuteObject<Model_ColdStorage>(sql, p);
            
            return coldstoragename;
        }

        
        public static List<Model_Storage_Device> GetStoDevi(int storageDeviceId)
        {
            //根据冷库/车载信息和pda信息查询相关关联信息
            string sql = "select * from storage_device where id=?id ";
            MySqlParameter[] sto = new MySqlParameter[1];
            sto[0] = new MySqlParameter("id", storageDeviceId);
            List<Model_Storage_Device> dt = _SqlHelp.ExecuteObjects<Model_Storage_Device>(sql, sto);
            if (dt == null)
                throw new Exception("冷库信息对应得关联表没有数据");

            return dt;
        }

        public static void GetThread(object id)
        {
            string sql = "DROP TABLE IF EXISTS `history_data_"+id+"`;" +
                        "CREATE TABLE `history_data_"+id+"` (" +
                         " `id` bigint(11) NOT NULL AUTO_INCREMENT COMMENT '自动增长标示Id',"+
                         " `pointId` int(11) NOT NULL COMMENT '探头标示Id',"+
                         " `data` decimal(9, 4) NOT NULL,"+
                         " `isAlarm` int(11) NOT NULL DEFAULT '0' COMMENT '报警状态 0 正常 1 报警',"+
                         " `dataTime` datetime NOT NULL COMMENT '数据记录的时间',"+
                         " PRIMARY KEY(`id`, `dataTime`),"+
                         " KEY `datatime` (`dataTime`) USING BTREE"+
                       " ) ENGINE = InnoDB AUTO_INCREMENT = 1 DEFAULT CHARSET = utf8"+
                       " PARTITION BY RANGE(year(dataTime))"+
                       " SUBPARTITION BY HASH(month(dataTime))"+
                       " SUBPARTITIONS 12"+
                        "(PARTITION y2016 VALUES LESS THAN(2017) ENGINE = InnoDB,"+
                        " PARTITION y2018 VALUES LESS THAN(2018) ENGINE = InnoDB,"+
                        " PARTITION y2019 VALUES LESS THAN(2019) ENGINE = InnoDB,"+
                        " PARTITION y2020 VALUES LESS THAN(2020) ENGINE = InnoDB,"+
                        " PARTITION y2021 VALUES LESS THAN(2021) ENGINE = InnoDB,"+
                        " PARTITION y2022 VALUES LESS THAN(2022) ENGINE = InnoDB,"+
                        " PARTITION y2023 VALUES LESS THAN(2023) ENGINE = InnoDB,"+
                        " PARTITION y2024 VALUES LESS THAN(2024) ENGINE = InnoDB,"+
                        " PARTITION y2025 VALUES LESS THAN(2025) ENGINE = InnoDB,"+
                        " PARTITION ymax VALUES LESS THAN MAXVALUE ENGINE = InnoDB);";
            
            

            int result = _SqlHelp.ExecuteNonQuery(sql);
             
            
        }
    }
}
