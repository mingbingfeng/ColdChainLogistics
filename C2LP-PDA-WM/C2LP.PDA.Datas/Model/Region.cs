using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace C2LP.PDA.Datas.Model
{
    public class MyRegion
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ParentId { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
