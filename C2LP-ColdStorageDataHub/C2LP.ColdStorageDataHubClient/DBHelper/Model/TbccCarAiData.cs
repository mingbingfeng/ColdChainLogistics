using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2LP.ColdStorageDataHubClient.DBHelper.Model
{
    /// <summary>
    /// 数据库实体 tbcchiststartup_projectId_1[车载启停记录]、tbcchistdata_car_ProjectId_1[车载历史数据]
    /// </summary>
    public class TbccCarStartUpAndAiData
    {
        public string ProjectNo { get; set; }

        public TbccCarStartUp StartUp { get; set; }

        public bool IsStartUpUpload { get; set; }

        public DateTime LastTime { get; set; }

        public List<TbccCarAiData> CarDatas { get; set; }

    }

    /// <summary>
    /// 数据库实体 tbcchiststartup_projectId_1[车载启停记录]
    /// </summary>
    public class TbccCarStartUp {
        public int Id { get; set; }

        public DateTime BeginTime { get; set; }

        public DateTime EndTime { get; set; }

        public int RecordInterval { get; set; }

        public string Carrier { get; set; }
        
        public int Finished { get; set; }
    }

    /// <summary>
    /// 数据库实体 tbcchistdata_car_ProjectId_1[车载历史数据]
    /// </summary>
    public class TbccCarAiData {
        public int Id { get; set; }

        public int ParentId { get; set; }

        public DateTime UpdateTime { get; set; }

        public decimal AI1 { get; set; }

        public decimal AI2 { get; set; }

        public decimal AI3 { get; set; }

        public decimal AI4 { get; set; }

        public int Latitude_dir { get; set; }

        public decimal Latitude { get; set; }

        public int Longitude_dir { get; set; }

        public decimal Longitude { get; set; }

        public int AlarmStatus { get; set; }
    }
}
