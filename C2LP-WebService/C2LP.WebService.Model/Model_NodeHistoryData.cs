using C2LP.WebService.Model.MyEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2LP.WebService.Model
{
    /// <summary>
    /// 实体类：指定节点的冷链数据
    /// </summary>
    [Serializable]
    public class Model_NodeHistoryData
    {
        /// <summary>
        /// 自动增长标示Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 	探头标示Id
        /// </summary>
        public int PointId { get; set; }

        /// <summary>
        /// 探头对应的数据，由于需要存放经纬度， 所以当前数据精度需要满足其位数要求。非法值为 -300
        /// </summary>
        public decimal Data { get; set; }

        /// <summary>
        /// 当前探头对应的值的报警状态 0 正常 1 报警 对应上面的值如果为非法值，则这个状态默认为 0 正常
        /// </summary>
        public Enum_HdAlarm IsAlarm { get; set; }

        /// <summary>
        /// 数据记录的时间 。同一个冷库或者车载，同时上来的探头必须保证时间一致
        /// </summary>
        public DateTime DataTime { get; set; }

        public override string ToString()
        {
            string str = string.Format("PointId【{0}】 Data【{1}】 IsAlarm【{2}】 DataTime【{3}】",PointId,Data,IsAlarm,DataTime.ToString("yyyy-MM-dd HH:mm:ss"));
            return str;
        }
    }
}
