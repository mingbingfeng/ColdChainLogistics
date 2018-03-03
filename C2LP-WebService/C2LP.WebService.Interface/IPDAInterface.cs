using C2LP.WebService.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace C2LP.WebService.Interface
{
    /// <summary>
    /// PDA操作 接口
    /// 明冰锋 2016年7月28日11:38:22
    /// </summary>
    [ServiceContract]
    public interface IPDAInterface
    {
        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<Model_PDAInfo> GetPDAInfo(string num);//根据编号获取PDA信息

        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<List<string>> GetDestins(int pId);//获取指定设备绑定的所有目的地

        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<List<Model_ColdStorage>> GetStorages(int pId);//获取指定设备绑定的所有冷藏载体

        /// <summary>
        /// 此接口已停用
        /// </summary>
        /// <param name="huadongList"></param>
        /// <returns></returns>
        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<List<Model_Customer>> GetCustomers(int pId);//获取指定设备所负责区域的所有启用的发货单位和收货单位


        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<List<Model_Customer>> GetCustomersFromMaxId(int pId, int maxId);//获取指定设备所负责区域的所有启用的发货单位和收货单位

        /// <summary>
        /// 此接口已停用
        /// </summary>
        /// <param name="huadongList"></param>
        /// <returns></returns>
        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<List<PDA_Model_Region>> GetRegions(int pId);//获取指定设备所负责的区域集合

        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<List<PDA_Model_Region>> GetRegionsFromMaxId(int pId, int maxId);

        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<bool> SetPDANumber(string num);//设置PDA编号

        /// <summary>
        /// 此接口已停用
        /// </summary>
        /// <param name="huadongList"></param>
        /// <returns></returns>
        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<DateTime> GetServerTime();//获取服务器时间

        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<bool> UploadWaybill_Base(List<Model_Waybill_Base> waybillList);//上传运单

        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<bool> UploadWaybill_Node(List<Model_Waybill_Node> nodeList);//上传节点

        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<bool> UploadWaybill_Postback(Model_Waybill_Postback_Pic postback, DateTime postbackTime, List<object> picList);//

        /// <summary>
        /// 此接口已停用
        /// </summary>
        /// <param name="huadongList"></param>
        /// <returns></returns>
        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<bool> UploadHuadongTmsOrder(List<Model_Huadong_Tms_Order> huadongList);//上传运单号


        //2017年2月21日14:02:02 添加新的接口

        /// <summary>
        /// 获取指定时间后更新的客户信息
        /// </summary>
        /// <param name="pId">设备ID</param>
        /// <param name="getTime">获取时间</param>
        /// <param name="maxId">当前获取到的客户信息索引</param>
        /// <returns></returns>
        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<List<PDA_Model_Customer>> GetNewCustomers(int pId, DateTime getTime, int maxId);



        /// <summary>
        /// 获取指定时间后更新的区域信息
        /// </summary>
        /// <param name="pId">设备ID</param>
        /// <param name="getTime">获取时间</param>
        /// <param name="maxId">当前获取到的区域信息索引</param>
        /// <returns></returns>
        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<List<PDA_Model_Region>> GetNewRegions(int pId, DateTime getTime, int maxId);

        /// <summary>
        /// 获取服务器时间字符串
        /// </summary>
        /// <returns></returns>
        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<string> GetServerTimeStr();

        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<int> UploadThirdPartyOrder(Model_ThirdPartOrder orderInfo);

        //2017年3月29日13:53:44 添加新的接口支持通过运管平台同步的第三方上游发货单位

        /// <summary>
        /// 获取所有第三方客户
        /// </summary>
        /// <returns></returns>
        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<List<Model_ThirdCustomer>> GetThirdCustomers();

        //2017年3月29日14:15:54 添加新的接口支持PDA上报第三方运单时传回第三方客户ID
        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<int> UploadThirdOrderForCustomer(Model_ThirdPartOrder orderInfo, int customerId);

        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<bool> UploadNodeForCustomer(Model_Waybill_Node node, int customerId);//上传节点


        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<bool> UploadPostbackForCustomer(Model_Waybill_Postback_Pic postback, DateTime postbackTime, List<object> picList, int customerId);

        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<bool> UploadPostbackForCustomers(Model_Waybill_Postback_Pic postback, DateTime postbackTime, List<object> picList, int customerId);

        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<List<Model_ColdStorage>> GetStorageScan();

        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<bool> UploadNodeForStorage(Model_Waybill_Node node, int customerId, int? parentStorageId);
    }
}
