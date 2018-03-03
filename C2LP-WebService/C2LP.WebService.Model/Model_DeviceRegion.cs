using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2LP.WebService.Model
{
    [Serializable]
    public  class Model_DeviceRegion
    {
        public int Id { get; set; }

        public int RegionId { get; set; }

        public int DeviceId { get; set; }
    }
}
