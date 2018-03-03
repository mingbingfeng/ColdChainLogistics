using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2LP.Assistant.TMSConsole.Model
{
    class NodeInfo
    {
        public int Id { get; set; }

        public int BaseId { get; set; }

        public string OperateAt { get; set; }

        public int StorageId { get; set; }

        public string StorageName { get; set; }

        public string Content { get; set; }

        public int Arrived { get; set; }

        //StorageInf
        public int StorageType { get; set; }

        public string Driver { get; set; }

        public string DriverTel { get; set; }


        public string ScanNumber { get; set; }

        public int ParentStorageId { get; set; }

        public string ShipmentCode { get; set; }

        public int CustomerId { get; set; }
    }
}
