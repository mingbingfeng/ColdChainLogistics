using C2LP.WebService.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C2LP.WebService.Model;
using System.Reflection;
using C2LP.WebService.DataHandle;
using C2LP.WebService.BLL.ColdChainBLL;

namespace C2LP.WebService.HandleServer
{
    public class ColdChainServer : IColdChainInterface
    {
        //public delegate void CompletedDelegate(Model_NodeHistoryData arg1, QueueThreadBase<Model_NodeHistoryData>.CompetedEventArgs arg2, string remark);
        //public static event CompletedDelegate OneCompleted;

        /// <summary>
        /// 处理异常信息
        /// </summary>
        /// <param name="result">结果</param>
        /// <param name="ex">异常信息</param>
        private void HandleExcepthin(object result, Exception ex)
        {
            Type t = result.GetType();
            t.InvokeMember("Code", BindingFlags.SetProperty, null, result, new object[] { 1 });
            t.InvokeMember("Message", BindingFlags.SetProperty, null, result, new object[] { ex.Message });
        }

        /// <summary>
        /// 上报车载历史数据
        /// </summary>
        /// <param name="carHDList">车载历史数据集合</param>
        /// <returns></returns>
        public ResultModel<bool> UploadCarHistDatas(List<Model_NodeHistoryData> carHDList)
        {
            ResultModel<bool> result = new ResultModel<bool>();
            try
            {
                result.Data = CC_HistDataServer.InsertDataToTempTable(carHDList);
            }
            catch (Exception ex)
            {
                HandleExcepthin(result, ex);
            }
            return result;
        }

        /// <summary>
        /// 上报仓库历史数据
        /// </summary>
        /// <param name="refHDList">仓库历史数据集合</param>
        /// <returns></returns>
        public ResultModel<bool> UploadRefHistDatas(List<Model_NodeHistoryData> refHDList)
        {
            ResultModel<bool> result = new ResultModel<bool>();
            try
            {
                result.Data = CC_HistDataServer.InsertDataToTempTable(refHDList);
            }
            catch (Exception ex)
            {
                HandleExcepthin(result, ex);
            }
            return result;
        }
    }

}
