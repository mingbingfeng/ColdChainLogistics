using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2LP.Assistant.TMSConsole.Model.NodeUpload.MESSAGEDETAIL
{
    [Serializable]
    public class DETAIL
    {
        public string ECNO { get; set; }

        public string CECNO { get; set; }

        public string LEGNO { get; set; }

        public string TRACKTIME { get; set; }

        public string TRACKPERSON { get; set; }

        public string TRACKINFO { get; set; }

        public string TRACKTYPE { get; set; }
    }
}
