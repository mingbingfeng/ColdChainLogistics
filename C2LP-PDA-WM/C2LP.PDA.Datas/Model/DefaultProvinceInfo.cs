using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace C2LP.PDA.Datas.Model
{
    public class DefaultProvinceInfo
    {
        public MyRegion DefaultRangion { get; set; }

        public Dictionary<MyRegion, List<MyRegion>> DefaultChildRangion { get; set; }

        public Dictionary<MyRegion, List<Customer>> RangionSenderCustomer { get; set; }

        public Dictionary<MyRegion, List<Customer>> RangionReceiveCustomer { get; set; }

        public List<MyRegion> ProvinceList { get; set; }

        public List<MyRegion> HaveSenderProvinceList { get; set; }

        public DefaultProvinceInfo()
        {
            DefaultRangion = new MyRegion();
            DefaultChildRangion = new Dictionary<MyRegion, List<MyRegion>>();
            RangionSenderCustomer = new Dictionary<MyRegion, List<Customer>>();
            RangionReceiveCustomer = new Dictionary<MyRegion, List<Customer>>();
            ProvinceList = new List<MyRegion>();
            HaveSenderProvinceList = new List<MyRegion>();
        }
    }
}
