using C2LP.WebService.Model;
using C2LP.WebService.Model.MyEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace C2LP.WebService.Interface
{
    /// <summary>
    /// 管理软件接口
    /// 明冰锋 2016年7月27日12:31:50
    /// </summary>
    /// <typeparam name="T">所需要返回值的基础类型</typeparam>
    [ServiceContract]
    public interface IConsoleInterface
    {
        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<Model_Customer> Login(string userName, string password);

        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<List<Model_Customer>> GetCustomerList(Enum_Role role, int customerId = 0, int provinceId = 0, int cityId = 0, string pageIndexAndCount = null);

        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<List<Model_Customer>> GetCustomerListByRole(Enum_Role role, int provinceId = 0, int cityId = 0, string pageIndexAndCount = null);

        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<int> GetCustomerListByRoleCount(Enum_Role role, int provinceId = 0, int cityId = 0);

        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<Model_Customer> EditCustomer(Model_Customer customerInfo);

        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<List<Model_CustomerUser>> GetCustomerUserList(int customerId = 0, string pageIndexAndCount = null);

        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<Model_CustomerUser> EditCustomerUser(Model_CustomerUser customerUserInfo);

        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<List<Model_Waybill_Base>> GetWaybillList(string waybillNumber = null, string pageIndexAndCount = null, string startTime = null, string endTime = null, int customerId = 0, int roles = 0);

        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<int> GetWaybillListCount(string waybillNumber = null, string startTime = null, string endTime = null, int customerId = 0, int roles = 0);

        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<List<Model_Waybill_Node>> GetWaybillNodeList(string waybillNumber = null, string operateAt = null, string pageIndexAndCount = null);


        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<List<String[]>> GetWaybillNodeHistDataList(int storageId, string beginTime, string endTime = null, string pageIndexAndCount = null);

        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<int> GetWaybillNodeHistDataCount(int storageId, string beginTime, string endTime = null);

        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<List<Model_Waybill_Postback_Pic>> GetWaybillPostbackPic(int BaseId);

        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<List<Model_PDAInfo>> GetPDAList(int pdaNumber = 0, string pageIndexAndCount = null);

        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<Model_PDAInfo> EditPDA(Model_PDAInfo pdaInfo);

        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<List<Model_ColdstoragePDA>> GetColdstoragePDAList(int storageId = 0, int storageType = 0, string pageIndexAndCount = null);

        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<Model_ColdStorage> EditColdstorage(Model_ColdStorage coldstorageInfo, int defaultDevice, int storageDeviceId = 0, int pdaId = 0, bool isDeleteStorage = false);

        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<List<Model_AiInfo>> GetAiInfoByStorageId(int storageId, string pageIndexAndCount = null);

        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<Model_AiInfo> EditAiInfo(Model_AiInfo aiInfo, bool IsDeleteAi = false);

        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<List<Model_Region>> GetRegionInfo(int parentId = 0);



        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<Model_Storage_Device> GetDefaultDevice(Model_Storage_Device storageDevice);

        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<List<Model_Destination>> GetPDADestinationList(int deviceId = 0, int pdaNumber = 0, string pageIndexAndCount = null);

        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<Model_Destination> EditPDADestination(Model_Destination destination);
        //华东信息接口
        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<List<Model_Huadong_Tms_Order>> GetHuadongQuery(string pageIndexAndCount = null);

        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<int> GethuadongTmsOrderCount();

        #region 模糊查询下游收货单位信息
        //模糊查询的接口
        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<List<Model_Customer>> GetVagueQuery(Model_Customer customer, string pageIndexAndCount = null);
        //模糊查询总数接口
        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<int> GetVagueQueryCount(Model_Customer customer);
        #endregion

        #region 增加县级/区域接口
        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<Model_Customer> GetCustomerCounty(Model_Customer customerInfo);

        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<List<Model_Customer>> GetCustomerListByCounty(Enum_Role role, int provinceId = 0, int cityId = 0, int county = 0, string pageIndexAndCount = null);

        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<int> GetCustomerListByCountyCount(Enum_Role role, int provinceId = 0, int cityId = 0, int county = 0);

        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<List<Model_Customer>> GetConsigneeCounty(Model_Customer customer, string pageIndexAndCount = null);

        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<int> GetConsigneeCountyCount(Model_Customer customer);
        #endregion

        #region 更新时间
        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<Model_Customer> GetCustomerUpdateTime(Model_Customer customerInfo);

        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<bool> UpdateCustomer(Model_Customer customerInfo,string bindReceiverOrg);

        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<List<Model_Region>> GetRegionDateTime(int parentId = 0);

        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<int> RegionEdit(Model_Region mregion);

        #endregion

        #region 根据运输任务单号或是tms运单号查询华东信息

        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<List<Model_Huadong_Tms_Order>> GetHuadongWaybillNumberQuery(string SHIPDETAILID, string pageIndexAndCount = null);

        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<int> GethuadongWaybillNumberCount(string SHIPDETAILID);

        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<List<Model_Huadong_Tms_Order>> GetHuadongWaybillVagueQuery(string SHIPDETAILID, string pageIndexAndCount = null);

        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<int> GethuadongWaybillVagueCount(string SHIPDETAILID);
        #endregion

        #region 根据第三方运单号查询显示相对应的运单信息

        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<List<Model_Waybill_Base>> GetWaybillThirdPartyList(string waybillNumber = null, string pageIndexAndCount = null, string startTime = null, string endTime = null, int customerId = 0, int roles = 0);

        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<int> GetWaybillThirdPartyListCount(string waybillNumber = null, string startTime = null, string endTime = null, int customerId = 0, int roles = 0);

        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<List<Model_Waybill_Base>> GetWaybillThirdPartyVagueList(string waybillNumber = null, string pageIndexAndCount = null, string startTime = null, string endTime = null, int customerId = 0);

        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<int> GetWaybillThirdPartyListVagueCount(string waybillNumber = null, string startTime = null, string endTime = null, int customerId = 0);
        #endregion

        #region 2017-7-25 增加区域选项，并根据区域显示下游客户
        /// <summary>
        /// 区域-省份
        /// </summary>
        /// <returns></returns>
        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<List<Model_Region>> GetZoneOptions();
        /// <summary>
        /// 城市
        /// </summary>
        /// <param name="parentid"></param>
        /// <returns></returns>
        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<List<Model_Region>> GetCity(int parentid=0);
        /// <summary>
        /// 根据区域显示下游客户
        /// </summary>
        /// <param name="zoneOption"></param>
        /// <returns></returns>
        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<List<Model_Customer>> GetDownstreamQZList(string zoneOption = null);
        /// <summary>
        /// 查询客户
        /// </summary>
        /// <param name="waybillNumber">运单号</param>
        /// <param name="pageIndexAndCount">分页1.10</param>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="senderId">上游客户id</param>
        /// <param name="receiverId">下游客户id</param>
        /// <returns></returns>
        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<List<Model_Waybill_Base>> GetQueryClientsList(string waybillNumber = null, string pageIndexAndCount = null, string startTime = null, string endTime = null, int senderId = 0,int receiverId=0);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="waybillNumber"></param>
        /// <param name="pageIndexAndCount"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="senderId"></param>
        /// <param name="receiverId"></param>
        /// <returns></returns>
        [WebInvoke(Method = "*", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.WrappedRequest)]
        ResultModel<int> GetQueryClientsListCount(string waybillNumber = null, string startTime = null, string endTime = null, int senderId = 0, int receiverId = 0);

        #endregion


    }
}
