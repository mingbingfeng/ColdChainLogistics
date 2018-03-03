using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2LP.WebService.Model
{
    [Serializable]
    public class PDA_Model_Region
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int ParentId { get; set; }
    }
}
