using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2LP.WebService.Model
{
    [Serializable]
    public class PDA_Model_Customer
    {
        public int id { get; set; }

        public string fullName { get; set; }

        public string contactPerson { get; set; }

        public string contactTel { get; set; }

        public string contactAddress { get; set; }

        public int provinceId { get; set; }

        public int cityId { get; set; }

        public int role { get; set; }

        public int countyId { get; set; }
    }
}
