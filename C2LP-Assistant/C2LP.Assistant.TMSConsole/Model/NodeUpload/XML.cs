using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2LP.Assistant.TMSConsole.Model.NodeUpload
{
    /// <summary>
    /// 节点和节点冷链数据上报的模版
    /// </summary>
    [Serializable]
    public class XML
    {
        public MESSAGEHEAD MESSAGEHEAD { get; set; }

        public string MESSAGEDETAIL { get; set; }


        #region 上报节点返回信息
        public string MSGCODE { get; set; }

        public string MSGCONTENT { get; set; }

        public string MSGNAME { get; set; }
        #endregion

    }
}
