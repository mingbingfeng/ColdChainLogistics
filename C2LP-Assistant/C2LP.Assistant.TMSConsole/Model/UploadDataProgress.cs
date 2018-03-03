using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2LP.Assistant.TMSConsole.Model
{
    class UploadDataProgress
    {
        public int Id { get; set; }

        public string relationId { get; set; }

        public int storageId { get; set; }

        public string storageName { get; set; }

        public DateTime nodeTime { get; set; }

        public DateTime endNodeTime { get; set; }

        public DateTime uploadProgress { get; set; }

        public string shipmentCode { get; set; }

        public int storageType { get; set; }
    }
}
