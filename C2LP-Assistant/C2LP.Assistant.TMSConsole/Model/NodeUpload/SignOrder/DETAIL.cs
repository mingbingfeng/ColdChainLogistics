using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2LP.Assistant.TMSConsole.Model.NodeUpload.SignOrder
{
    [Serializable]
    public class DETAIL
    {
        /// <summary>
        /// 承运商单号
        /// </summary>
        public string CECNO { get; set; }

        public string SHIPMENTCODE { get; set; }

        /// <summary>
        /// TMS运单号
        /// </summary>
        public string LEGCODE { get; set; }

        /// <summary>
        /// 任务单号
        /// </summary>
        public string SHIPDETAILID { get; set; }

        /// <summary>
        /// 车号
        /// </summary>
        public string VEHICLENO { get; set; }

        /// <summary>
        /// 巴枪操作人
        /// </summary>
        public string SIGNPERSON { get; set; }

        /// <summary>
        /// 巴枪签收时间
        /// </summary>
        public string SIGNTIME { get; set; }

        public string JSREASON { get; set; }
        public string BUSINESSTATUS { get; set; }
        public double XTQUNTITY { get; set; }
        public double THQUNTITY { get; set; }
        public double HCQUNTITY { get; set; }
        public string PICTURE { get; set; }

        public DETAIL() {
            BUSINESSTATUS = "20";
            XTQUNTITY = 0;
            THQUNTITY = 0;
            HCQUNTITY = 0;
            PICTURE = string.Empty;
            JSREASON = string.Empty;
        }
    }
}
