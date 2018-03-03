using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace C2LP.PDA.Datas.Model
{
    public class ColdStorage
    {
        public int Id { get; set; }

        public string storageName { get; set; }

        public int storageType { get; set; }

        public string driver { get; set; }

        public string driverTel { get; set; }

        public string remark { get; set; }

        public string createAt { get; set; }
    }
}
