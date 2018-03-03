using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace C2LP.PDA.Datas.Model
{
    public class Customer
    {
        public int? Id { get; set; }

        public string FullName { get; set; }

        public string ContactPerson { get; set; }

        public string ContactTel { get; set; }

        public string ContactAddress { get; set; }

        public int ProvinceId { get; set; }

        public int CityId { get; set; }

        public int Role { get; set; }

        public int CountyId { get; set; }
    }
}
