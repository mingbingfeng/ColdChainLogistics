using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace C2LP.PDA.Datas.Model
{
    public class Consignor
    {
        public int ConsignorId { get; set; }

        public string ConsignorName { get; set; }

        public int LinkType { get; set; }

        public string LinkRegex { get; set; }

        public Consignor() {
            LinkRegex = string.Empty;
            ConsignorName = string.Empty;
        }
    }
}
