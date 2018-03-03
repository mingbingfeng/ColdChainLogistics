using System;
using System.IO;
using System.Xml;

namespace C2LP.Assistant.TMSConsole.Model.TMSOrder
{
    [Serializable]
    public class XML
    {
        #region 没有订单
        public string MSGCODE { get; set; }

        public string MSGCONTENT { get; set; }

        public string MSGNAME { get; set; }
        #endregion

        public MESSAGEHEAD MESSAGEHEAD { get; set; }

        public string MESSAGEDETAIL { get; set; }
        public MESSAGEDETAIL.XML _MESSAGEDETAIL {
            get {
                try
                {
                    return Utility.ParseXMLToObjec<MESSAGEDETAIL.XML>(MESSAGEDETAIL);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

    }
    
}
