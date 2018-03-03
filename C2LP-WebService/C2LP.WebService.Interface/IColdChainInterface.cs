using C2LP.WebService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace C2LP.WebService.Interface
{
    /// <summary>
    /// 冷链数据上报接口
    /// 明冰锋 2016年9月19日10:28:01
    /// </summary>
    [ServiceContract]
    public interface IColdChainInterface
    {
        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<bool> UploadRefHistDatas(List<Model_NodeHistoryData> refHDList);

        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<bool> UploadCarHistDatas(List<Model_NodeHistoryData> carHDList);
    }
}
