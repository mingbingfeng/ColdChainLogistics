using C2LP.WebService.BLL.ColdChainBLL;
using C2LP.WebService.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace C2LP.WebService.DataHandle
{
    /// <summary>
    /// 冷链数据处理线程
    /// </summary>
    public class DataHandleHelper : QueueThreadBase<Model_NodeHistoryData>
    {

        public DataHandleHelper(IEnumerable<Model_NodeHistoryData> collection) : base(collection)
        {
        }

        protected override DoWorkResult DoWork(Model_NodeHistoryData pendingValue)
        {
            try
            {
                Model_AiInfo aiInfo = CC_HistDataServer.CheckPointIdIsExist(pendingValue.PointId);
                if (aiInfo == null)
                    throw new Exception("指定的PointId不存在!");
                else
                {
                    bool result = CC_HistDataServer.InsertPointData(pendingValue, aiInfo.StorageId);
                    if (result == false)
                        throw new Exception("插入数据失败!");
                }
                return DoWorkResult.ContinueThread;
            }
            catch (Exception ex)
            {
                throw ex;
                //return DoWorkResult.AbortCurrentThread;//有异常,可以终止当前线程.当然.也可以继续,
                //return  DoWorkResult.AbortAllThread; //特殊情况下 ,有异常终止所有的线程...

            }
            //return base.DoWork(pendingValue);
        }
    }
}
