using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2LP.ColdStorageDataHubClient.DBHelper.Model
{
    public class TbccRefAiData_5100
    {
        public int Id { get; set; }

        public int SensorId { get; set; }

        public DateTime RecordTime { get; set; }

        public DateTime UpdateTime { get; set; }

        public float TValue { get; set; }

        public int TUpDwLimit_Alarm { get; set; }

        public float RhValue { get; set; }

        public int RhUpDwLimit_Alarm { get; set; }
        
    }
}
