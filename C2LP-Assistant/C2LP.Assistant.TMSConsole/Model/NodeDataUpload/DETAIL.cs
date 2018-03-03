using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2LP.Assistant.TMSConsole.Model.NodeDataUpload
{
    [Serializable]
    public class DETAIL
    {
        public string ECNO { get; set; }
        public string LEGNO { get; set; }
        public string LICENSENO { get; set; }
        public string WAREHOUSECODE { get; set; }
        public string TRACKTIME { get; set; }
        public string TEMPREATURE { get; set; }
        public string HUMIDITY { get; set; }
        public string LONGITUDE { get; set; }
        public string LATITUDE { get; set; }

       public  DETAIL() {
            LICENSENO = string.Empty;
            WAREHOUSECODE = string.Empty;
            LONGITUDE = string.Empty;
            LATITUDE = string.Empty;
        }
        
    }

}
