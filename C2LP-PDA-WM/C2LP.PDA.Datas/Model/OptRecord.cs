using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace C2LP.PDA.Datas.Model
{
    public class OptRecord
    {
        public int Id { get; set; }

        public string OptTime { get; set; }

        public string OptType { get; set; }

        public string Content { get; set; }

        public string OptNumber { get; set; }

        public int OptCustomerId { get; set; }

        public int OptTypeId { get; set; }

        public OptRecord() {
            Id = 0;
            OptTime = string.Empty;
            OptType = string.Empty;
            Content = string.Empty;
        }
    }
}
