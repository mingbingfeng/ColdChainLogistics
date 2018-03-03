using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2LP.WebService.Model
{
    /// <summary>
    /// 实体类：上游发货单位运管信息管理表 
    /// </summary>
    [Serializable]
    public class Model_ThirdCustomer
    {

        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public int LinkType { get; set; }

        public string LinkRegex { get; set; }

        public Model_ThirdCustomer() {
            CustomerName = string.Empty;
            LinkRegex = string.Empty;
        }
    }
}
