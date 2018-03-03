using System;

namespace C2LP.Assistant.TMSConsole.Logink
{
    public class M_TMSNode
    {

        public string OrderNo { get; set; }

        public string JcOrderNo { get; set; }

        public long JcNodeId { get; set; }

        public DateTime TrackTime { get; set; }

        public string TrackPerson { get; set; }

        public string TrackInfo { get; set; }

        public string TrackType { get; set; }

        public string StorageName { get; set; }

        public int Arrived { get; set; }
    }
}
