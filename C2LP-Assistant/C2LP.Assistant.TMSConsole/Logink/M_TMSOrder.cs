using System;

namespace C2LP.Assistant.TMSConsole.Logink
{
    public class M_TMSOrder
    {
        public int id { get; set; }
        public string orderNo { get; set; }

        public string senderOrg { get; set; }
        public string senderPerson { get; set; }
        public string senderTel { get; set; }
        public string senderAddress { get; set; }


        public string receiverOrg { get; set; }
        public string receiverPerson { get; set; }
        public string receiverTel { get; set; }
        public string receiverAddress { get; set; }

        public DateTime beginAt { get; set; }
        public long billingCount { get; set; }
        //public DateTime signinAt { get; set; }
        //public DateTime picPostbackAt { get; set; }
        //public DateTime insertTime { get; set; }
        //public DateTime updateTime { get; set; }


        //public DateTime orderStatus { get; set; }
    }
}
