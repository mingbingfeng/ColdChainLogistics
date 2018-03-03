using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2LP.WebService.Model
{
    [Serializable]
    public class Model_ThirdPartOrder
    {
        public int Id { get; set; }

        public string RelationId { get; set; }

        public string OperateAt { get; set; }
        
    }
}
