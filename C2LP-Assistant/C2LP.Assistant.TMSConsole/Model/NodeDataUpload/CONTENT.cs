using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2LP.Assistant.TMSConsole.Model.NodeDataUpload
{
    [Serializable]
   public  class CONTENT
    {
        public string HEADER { get; set; }

        public List<DETAIL> DETAILLIST { get; set; }

        public CONTENT()
        {
            HEADER = string.Empty;
        }
    }
}
