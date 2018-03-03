using C2LP.WebService.BLL.ConsoleBLL;
using C2LP.WebService.Interface;
using C2LP.WebService.Model;
using C2LP.WebService.Model.MyEnum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2LP.WebService.HandleServer
{
    /// <summary>
    /// 后台管理系统接口实现
    /// </summary>
    public class ConsoleServer : IConsoleInterface
    {

        /// <summary>
        /// 登录验证
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        public ResultModel<Model_Customer> Login(string userName, string password)
        {
            ResultModel<Model_Customer> result = new ResultModel<Model_Customer>();
            try
            {
                result.Data = CustomerServer.CheckCustomerUser(userName, password);

            }
            catch (Exception ex)
            {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 获取客户列表
        /// </summary>
        /// <param name="customerId">可选参数[客户ID,为0时默认查询所有客户]</param>
        /// <param name="provinceId">可选参数[所在省份 region Id,为0时默认查询所有省份]</param>
        /// <param name="cityId">可选参数[所在省份 region Id,为0时默认查询所有城市]</param>
        /// <param name="pageIndexAndCount">可选参数[分页参数,无需分页则不填,格式为"页索引.每页数量".例如:1.50,表示每页显示50条,当前查询第1页]</param>
        /// <returns></returns>
        public ResultModel<List<Model_Customer>> GetCustomerList(Enum_Role role, int customerId = 0, int provinceId = 0, int cityId = 0, string pageIndexAndCount = null)
        {
            ResultModel<List<Model_Customer>> result = new ResultModel<List<Model_Customer>>();
            try
            {
                result.Data = CustomerServer.CustomerQuery(role, customerId);
            }
            catch (Exception ex)
            {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 根据客户角色获取客户列表
        /// </summary>
        /// <param name="role">客户角色</param>
        /// <param name="provinceId">可选参数[所在省份 region Id,为0时默认查询所有省份]</param>
        /// <param name="cityId">可选参数[所在省份 region Id,为0时默认查询所有城市]</param>
        /// <param name="pageIndexAndCount">可选参数[分页参数,无需分页则不填,格式为"页索引.每页数量".例如:1.50,表示每页显示50条,当前查询第1页]</param>
        /// <returns></returns>
        public ResultModel<List<Model_Customer>> GetCustomerListByRole(Enum_Role role, int provinceId = 0, int cityId = 0, string pageIndexAndCount = null)
        {
            ResultModel<List<Model_Customer>> result = new ResultModel<List<Model_Customer>>();
            try
            {
                result.Data = CustomerServer.GetCustomerListByRoles(role, provinceId, cityId, pageIndexAndCount);
            }
            catch (Exception ex)
            {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 查询客户信息总数
        /// </summary>
        /// <param name="role">客户角色</param>
        /// <param name="provinceId">可选参数[所在省份 region Id,为0时默认查询所有省份]</param>
        /// <param name="cityId">可选参数[所在省份 region Id,为0时默认查询所有城市]</param>
        /// <returns></returns>
        public ResultModel<int> GetCustomerListByRoleCount(Enum_Role role, int provinceId = 0, int cityId = 0)
        {
            ResultModel<int> result = new ResultModel<int>();
            try
            {
                result.Data = CustomerServer.GetCustomerListByRoleCount(role,provinceId,cityId);
            }
            catch (Exception ex)
            {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 编辑客户信息
        /// </summary>
        /// <param name="customerInfo">客户信息</param>
        /// <returns></returns>
        public ResultModel<Model_Customer> EditCustomer(Model_Customer customerInfo)
        {
            ResultModel<Model_Customer> result = new ResultModel<Model_Customer>();
            try
            {

                result.Data = CustomerServer.CustomerNew(customerInfo);

            }
            catch (Exception ex)
            {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 获取指定企业下的登录账户列表
        /// </summary>
        /// <param name="customerId">可选参数[企业ID,为0时默认查询所有企业的用户]</param>
        /// <param name="pageIndexAndCount">可选参数[分页参数,无需分页则不填,格式为"页索引.每页数量".例如:1.50,表示每页显示50条,当前查询第1页]</param>
        /// <returns></returns>
        public ResultModel<List<Model_CustomerUser>> GetCustomerUserList(int customerId = 0, string pageIndexAndCount = null)
        {
            ResultModel<List<Model_CustomerUser>> result = new ResultModel<List<Model_CustomerUser>>();
            try
            {
                result.Data = CustomerUsersServer.CustomerUserQuery(customerId, pageIndexAndCount);
            }
            catch (Exception ex)
            {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 编辑指定用户信息
        /// </summary>
        /// <param name="customerUserInfo">用户信息</param>
        /// <returns></returns>
        public ResultModel<Model_CustomerUser> EditCustomerUser(Model_CustomerUser customerUserInfo)
        {
            ResultModel<Model_CustomerUser> reslut = new ResultModel<Model_CustomerUser>();
            try
            {
                reslut.Data = CustomerUsersServer.ChangePassWord(customerUserInfo);
            }
            catch (Exception ex)
            {
                reslut.Code = 1;
                reslut.Message = ex.Message;
            }
            return reslut;
        }

        /// <summary>
        /// 获取运单号列表
        /// </summary>
        /// <param name="waybillNumber">可选参数[指定运单号查询]</param>
        /// <param name="pageIndexAndCount">可选参数[分页参数,格式为"页索引.每页数量".例如:1.50,表示每页显示50条,当前查询第1页]</param>
        /// <param name="startTime">可选参数[查询运单号的起始时间]</param>
        /// <param name="endTime">可选参数[查询运单号的结束时间]</param>
        /// <param name="customerId">可选参数[按客户ID筛选]</param>
        /// <returns></returns>
        public ResultModel<List<Model_Waybill_Base>> GetWaybillList(string waybillNumber = null, string pageIndexAndCount = null, string startTime = null, string endTime = null, int customerId = 0, int roles = 0)
        {
            ResultModel<List<Model_Waybill_Base>> result = new ResultModel<List<Model_Waybill_Base>>();
            try
            {
                result.Data = WaybillBaseServer.GetWaybillLists(waybillNumber, pageIndexAndCount, startTime, endTime, customerId, roles);
            }
            catch (Exception ex)
            {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 查询运单信息总数
        /// </summary>
        /// <param name="waybillNumber"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="customerId"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        public ResultModel<int> GetWaybillListCount(string waybillNumber = null, string startTime = null, string endTime = null, int customerId = 0, int roles = 0)
        {
            ResultModel<int> result = new ResultModel<int>();
            try
            {
                result.Data = WaybillBaseServer.GetWaybillListCount(waybillNumber, startTime, endTime, customerId, roles);
            }
            catch (Exception ex)
            {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 获取指定运单的所有物流节点
        /// </summary>
        /// <param name="waybillNumber">可选参数[运单号,不填时查询所有运单号的节点信息]</param>
        /// <param name="pageIndexAndCount">可选参数[分页参数,格式为"页索引.每页数量".例如:1.50,表示每页显示50条,当前查询第1页]</param>
        /// <param name="operateAt">运单物流节点的操作时间</param>
        /// <returns></returns>
        public ResultModel<List<Model_Waybill_Node>> GetWaybillNodeList(string waybillNumber = null, string operateAt = null, string pageIndexAndCount = null)
        {
            ResultModel<List<Model_Waybill_Node>> result = new ResultModel<List<Model_Waybill_Node>>();
            try
            {
                result.Data = WaybillBaseServer.GetWaybillNodeLists(waybillNumber, operateAt, pageIndexAndCount);

            }
            catch (Exception ex)
            {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 根据节点时间获取冷链数据
        /// </summary>
        /// <param name="storageId">当前节点货物的载体[仓库或车载ID]</param>
        /// <param name="beginTime">节点开始时间</param>
        /// <param name="endTime">可选参数[下一个节点开始时间]</param>
        /// <returns></returns>
        public ResultModel<List<String[]>> GetWaybillNodeHistDataList(int storageId, string beginTime, string endTime = null, string pageIndexAndCount = null)
        {
            ResultModel<List<String[]>> result = new ResultModel<List<String[]>>();
            try
            {
                //result.Data = WaybillBaseServer.GetWaybillNodeHistDataLists(storageId,beginTime,endTime, pageIndexAndCount);
                result.Data = WaybillBaseServer.GetWaybillNodeModel_AiInfo(storageId, beginTime, endTime, pageIndexAndCount);
            }
            catch (Exception ex)
            {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 查询冷链数据总数
        /// </summary>
        /// <param name="storageId"></param>
        /// <param name="beginTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public ResultModel<int> GetWaybillNodeHistDataCount(int storageId, string beginTime, string endTime = null)
        {
            ResultModel<int> result = new ResultModel<int>();
            try
            {
                result.Data = WaybillBaseServer.GetWaybillNodeHistDataCount(storageId, beginTime, endTime);
            }
            catch (Exception ex)
            {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 根据运单物流id查询图片
        /// </summary>
        /// <param name="BaseId">运单物流id</param>
        /// <returns></returns>
        public ResultModel<List<Model_Waybill_Postback_Pic>> GetWaybillPostbackPic(int BaseId)
        {
            ResultModel<List<Model_Waybill_Postback_Pic>> result = new ResultModel<List<Model_Waybill_Postback_Pic>>();
            try
            {
                result.Data = WaybillBaseServer.GetWaybillPostbackPics(BaseId);
            }
            catch (Exception ex)
            {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 获取PDA列表
        /// </summary>
        /// <param name="pdaNumber">可选参数[PDA编号,为0时默认查询所有PDA信息]</param>
        /// <param name="pageIndexAndCount">可选参数[分页参数,格式为"页索引.每页数量".例如:1.50,表示每页显示50条,当前查询第1页]</param>
        /// <returns></returns>
        public ResultModel<List<Model_PDAInfo>> GetPDAList(int pdaNumber = 0, string pageIndexAndCount = null)
        {
            ResultModel<List<Model_PDAInfo>> result = new ResultModel<List<Model_PDAInfo>>();
            try
            {
                result.Data = DeviceServer.GetPDALists(pdaNumber, pageIndexAndCount);
            }
            catch (Exception ex)
            {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 编辑或删除PDA信息
        /// Id不为0时表示编辑PDA信息
        /// Id为-1时通过PDA编号删除该PDA
        /// Id为0时表示添加PDA信息
        /// </summary>
        /// <param name="pdaInfo">PDA信息</param>
        /// <returns></returns>
        public ResultModel<Model_PDAInfo> EditPDA(Model_PDAInfo pdaInfo)
        {
            ResultModel<Model_PDAInfo> result = new ResultModel<Model_PDAInfo>();
            try
            {
                result.Data = DeviceServer.EditPDAs(pdaInfo);
            }
            catch (Exception ex)
            {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return result;
        }


        /// <summary>
        /// 获取指定类型的冷链载体
        /// </summary>
        /// <param name="storageId">可选参数[指定冷库载体的ID,为0则默认在所有冷库载体筛选后续条件]</param>
        /// <param name="storageType">可选参数[冷链载体类型(1:冷库;2:车载)]</param>
        /// <param name="pageIndexAndCount">可选参数[分页参数,格式为"页索引.每页数量".例如:1.50,表示每页显示50条,当前查询第1页]</param>
        /// <returns></returns>
        public ResultModel<List<Model_ColdstoragePDA>> GetColdstoragePDAList(int storageId = 0, int storageType = 0, string pageIndexAndCount = null)
        {
            ResultModel<List<Model_ColdstoragePDA>> result = new ResultModel<List<Model_ColdstoragePDA>>();
            try
            {
                result.Data = ColdstorageServer.GetColdStorages(storageId, storageType, pageIndexAndCount);
            }
            catch (Exception ex)
            {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 编辑冷藏载体信息
        /// storageID不为0时表示编辑或删除
        /// storageID为0时表示添加
        /// </summary>
        /// <param name="coldstorageInfo">冷藏载体信息</param>
        /// <param name="pdaId">绑定的PDAId</param>
        /// <param name="isDeleteStorage">True:storageID不为0时执行删除操作;False:storageID为0时不删除执行添加操作</param>
        /// <param name="defaultDevice">0不是默认，1是默认</param>
        /// <returns></returns>
        public ResultModel<Model_ColdStorage> EditColdstorage(Model_ColdStorage coldstorageInfo, int defaultDevice, int storageDeviceId = 0, int pdaId = 0, bool isDeleteStorage = false)
        {
            ResultModel<Model_ColdStorage> result = new ResultModel<Model_ColdStorage>();
            try
            {
                result.Data = ColdstorageServer.EditColdstorages(coldstorageInfo, defaultDevice, storageDeviceId, pdaId, isDeleteStorage);
            }
            catch (Exception ex)
            {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 根据冷库/车载获取其所有的AI信息
        /// </summary>
        /// <param name="storageId">冷库/车载ID</param>
        /// <param name="pageIndexAndCount">可选参数[分页参数,格式为"页索引.每页数量".例如:1.50,表示每页显示50条,当前查询第1页]</param>
        /// <returns></returns>
        public ResultModel<List<Model_AiInfo>> GetAiInfoByStorageId(int storageId, string pageIndexAndCount = null)
        {
            ResultModel<List<Model_AiInfo>> result = new ResultModel<List<Model_AiInfo>>();
            try
            {
                result.Data = AiinFoServer.GetAiInfoByStorageIds(storageId, pageIndexAndCount);
            }
            catch (Exception ex)
            {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 编辑AI信息
        /// pointId为0时表示添加
        /// pointId不为0时表示删除或编辑
        /// </summary>
        /// <param name="aiInfo">AI信息</param>
        /// <param name="IsDeleteAi">True:pointID不为0时执行删除操作;False:pointID为0时不删除执行添加操作</param>
        /// <returns></returns>
        public ResultModel<Model_AiInfo> EditAiInfo(Model_AiInfo aiInfo, bool IsDeleteAi = false)
        {
            ResultModel<Model_AiInfo> result = new ResultModel<Model_AiInfo>();
            try
            {
                result.Data = AiinFoServer.EditAiInfos(aiInfo, IsDeleteAi);
            }
            catch (Exception ex)
            {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 获取行政区县信息
        /// </summary>
        /// <param name="parentId">可选参数[上级Id]</param>
        /// <returns></returns>
        public ResultModel<List<Model_Region>> GetRegionInfo(int parentId = 0)
        {
            ResultModel<List<Model_Region>> result = new ResultModel<List<Model_Region>>();
            try
            {
                result.Data = AreaCityServer.AreaCitys(parentId);
            }
            catch (Exception ex)
            {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 根据pda设备id更改关系表中的默认绑定
        /// </summary>
        /// <param name="storageDevice">关系主体信息</param>
        /// <returns></returns>
        public ResultModel<Model_Storage_Device> GetDefaultDevice(Model_Storage_Device storageDevice)
        {
            ResultModel<Model_Storage_Device> result = new ResultModel<Model_Storage_Device>();
            try
            {
                result.Data = StorageDeviceServer.GetDefaultDevices(storageDevice);
            }
            catch (Exception ex)
            {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 获取PDA 设备目的地 信息
        /// </summary>
        /// <param name="deviceId">device_destination的id </param>
        /// <param name="pdaNumber">可选参数[PDA编号,为0时默认查询所有PDA信息]</param>
        /// <param name="pageIndexAndCount">可选参数[分页参数,格式为"页索引.每页数量".例如:1.50,表示每页显示50条,当前查询第1页]</param>
        /// <returns></returns>
        public ResultModel<List<Model_Destination>> GetPDADestinationList(int deviceId = 0, int pdaNumber = 0, string pageIndexAndCount = null)
        {
            ResultModel<List<Model_Destination>> result = new ResultModel<List<Model_Destination>>();
            try
            {
                result.Data = DeviceServer.GetPDADestinationLists(deviceId, pdaNumber, pageIndexAndCount);
            }
            catch (Exception ex)
            {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 编辑PDA目的地信息
        /// </summary>
        /// <param name="destination">PDA目的地主体</param>
        /// <returns></returns>
        public ResultModel<Model_Destination> EditPDADestination(Model_Destination destination)
        {
            ResultModel<Model_Destination> result = new ResultModel<Model_Destination>();
            try
            {
                result.Data = DeviceServer.EditPDADestinations(destination);
            }
            catch (Exception ex)
            {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 查询华东信息
        /// </summary>
        /// <param name="pageIndexAndCount">可选参数[分页参数,格式为"页索引.每页数量".例如:1.50,表示每页显示50条,当前查询第1页]</param>
        /// <returns></returns>
        public ResultModel<List<Model_Huadong_Tms_Order>> GetHuadongQuery(string pageIndexAndCount = null) {
            ResultModel<List<Model_Huadong_Tms_Order>> result = new ResultModel<List<Model_Huadong_Tms_Order>>();
            try
            {
                result.Data = HuadongTmsOrderServer.GethuadongTmsOrder(pageIndexAndCount);
            }
            catch (Exception ex)
            {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 查询华东信息总条数
        /// </summary>
        /// <returns></returns>
        public ResultModel<int> GethuadongTmsOrderCount()
        {
            ResultModel<int> result = new ResultModel<int>();
            try
            {
                result.Data = HuadongTmsOrderServer.GethuadongTmsOrderCount();
            }
            catch (Exception ex)
            {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return result;
        }

        #region 模糊查询下游收货单位信息
        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="customer">客户</param>
        /// <param name="pageIndexAndCount">分页</param>
        /// <returns></returns>
        public ResultModel<List<Model_Customer>> GetVagueQuery(Model_Customer customer, string pageIndexAndCount = null)
        {
            ResultModel<List<Model_Customer>> result = new ResultModel<List<Model_Customer>>();
            try
            {
                result.Data =CustomerServer.GetCustomerVageue(customer, pageIndexAndCount);
            }
            catch (Exception ex)
            {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 模糊查询总数
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public ResultModel<int> GetVagueQueryCount(Model_Customer customer)
        {
            ResultModel<int> result = new ResultModel<int> ();
            try
            {
                result.Data = CustomerServer.GetVagueQueryCount(customer);
            }
            catch (Exception ex)
            {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return result;
        }
        #endregion

        #region 新增县级
        /// <summary>
        /// 新增县级
        /// </summary>
        /// <param name="customerInfo">客户表主体</param>
        /// <returns></returns>
        public ResultModel<Model_Customer> GetCustomerCounty(Model_Customer customerInfo)
        {
            ResultModel<Model_Customer> result = new ResultModel<Model_Customer>();
            try
            {
                result.Data = CustomerServer.GetCustomerCounty(customerInfo);
            }
            catch (Exception ex)
            {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 根据省市县查询客户信息
        /// </summary>
        /// <param name="role"></param>
        /// <param name="provinceId"></param>
        /// <param name="cityId"></param>
        /// <param name="county"></param>
        /// <param name="pageIndexAndCount"></param>
        /// <returns></returns>
        public ResultModel<List<Model_Customer>> GetCustomerListByCounty(Enum_Role role, int provinceId = 0, int cityId = 0, int county = 0, string pageIndexAndCount = null)
        {
            ResultModel<List<Model_Customer>> result = new ResultModel<List<Model_Customer>>();
            try
            {
                result.Data = CustomerServer.GetCustomerListByCounty(role, provinceId, cityId, county, pageIndexAndCount);
            }
            catch (Exception ex)
            {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 根据省市县查询客户总数
        /// </summary>
        /// <param name="role"></param>
        /// <param name="provinceId"></param>
        /// <param name="cityId"></param>
        /// <param name="county"></param>
        /// <returns></returns>
        public ResultModel<int> GetCustomerListByCountyCount(Enum_Role role, int provinceId = 0, int cityId = 0, int county = 0)
        {
            ResultModel<int> result = new ResultModel<int>();
            try
            {
                result.Data = CustomerServer.GetCustomerListByCountyCount(role, provinceId, cityId, county);
            }
            catch (Exception ex)
            {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 根据省市县或是客户名称查询收货单位
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="pageIndexAndCount"></param>
        /// <returns></returns>
        public ResultModel<List<Model_Customer>> GetConsigneeCounty(Model_Customer customer, string pageIndexAndCount = null)
        {
            ResultModel<List<Model_Customer>> result = new ResultModel<List<Model_Customer>>();
            try
            {
                result.Data = CustomerServer.GetConsigneeCounty(customer, pageIndexAndCount);
            }
            catch (Exception ex)
            {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 根据省市县或是客户名称查询收货单位总数
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public ResultModel<int> GetConsigneeCountyCount(Model_Customer customer)
        {
            ResultModel<int> result = new ResultModel<int>();
            try
            {
                result.Data = CustomerServer.GetConsigneeCountyCount(customer);
            }
            catch (Exception ex)
            {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return result;
        }
        #endregion

        #region 更新时间
        /// <summary>
        ///  新增和修改客户信息更新时间
        /// </summary>
        /// <param name="customerInfo"></param>
        /// <returns></returns>
        public ResultModel<Model_Customer> GetCustomerUpdateTime(Model_Customer customerInfo)
        {
            ResultModel<Model_Customer> result = new ResultModel<Model_Customer>();
            try
            {
                result.Data = CustomerServer.GetCustomerUpdateTime(customerInfo);
            }
            catch (Exception ex)
            {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 新增或更新客户信息
        /// </summary>
        /// <param name="customerInfo">客户信息</param>
        /// <param name="bindReceiverOrg">与第三方运单收货单位关联的名称</param>
        /// <returns></returns>
        public ResultModel<bool> UpdateCustomer(Model_Customer customerInfo, string bindReceiverOrg)
        {
            ResultModel<bool> result = new ResultModel<bool>();
            try
            {
                result.Data = CustomerServer.UpdateCustomer(customerInfo, bindReceiverOrg);
            }
            catch (Exception ex)
            {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 根据父级编号查询行政信息
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public ResultModel<List<Model_Region>> GetRegionDateTime(int parentId = 0)
        {
            ResultModel<List<Model_Region>> result = new ResultModel<List<Model_Region>>();
            try
            {
                result.Data = AreaCityServer.GetRegionDateTime(parentId);
            }
            catch (Exception ex)
            {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return result;
        }

        /// <summary>
        /// 添加/修改行政区域信息
        /// </summary>
        /// <param name="mregion"></param>
        /// <returns></returns>
        public ResultModel<int> RegionEdit(Model_Region mregion)
        {
            ResultModel<int> result = new ResultModel<int>();
            try
            {
                result.Data = AreaCityServer.RegionEdit(mregion);
            }
            catch (Exception ex)
            {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return result;
        }
        #endregion

        #region 根据运输任务单号或是tms运单号查询华东信息
        /// <summary>
        /// 根据运输任务单号或是tms运单号查询华东信息
        /// </summary>
        /// <param name="SHIPDETAILID">运输任务单号</param>
        /// <param name="LEGCODE">tms运单号</param>
        /// <param name="pageIndexAndCount">分页1.10</param>
        /// <returns></returns>
        public ResultModel<List<Model_Huadong_Tms_Order>> GetHuadongWaybillNumberQuery(string SHIPDETAILID, string pageIndexAndCount = null)
        {
            ResultModel<List<Model_Huadong_Tms_Order>> result = new ResultModel<List<Model_Huadong_Tms_Order>>();
            try
            {
                result.Data = HuadongTmsOrderServer.GetHuadongWaybillNumberQuerys(SHIPDETAILID, pageIndexAndCount);
            }
            catch (Exception ex)
            {
                result.Code = 1;
                result.Message=ex.Message;
            }
            return result;
        }

        public ResultModel<int> GethuadongWaybillNumberCount(string SHIPDETAILID)
        {
            ResultModel<int> result = new ResultModel<int>();
            try
            {
                result.Data = HuadongTmsOrderServer.GethuadongWaybillNumberCounts(SHIPDETAILID);
            }
            catch (Exception ex)
            {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 模糊查询第三方运单
        /// </summary>
        /// <param name="SHIPDETAILID"></param>
        /// <param name="pageIndexAndCount"></param>
        /// <returns></returns>
        public ResultModel<List<Model_Huadong_Tms_Order>> GetHuadongWaybillVagueQuery(string SHIPDETAILID, string pageIndexAndCount = null)
        {
            ResultModel<List<Model_Huadong_Tms_Order>> result = new ResultModel<List<Model_Huadong_Tms_Order>>();
            try
            {
                result.Data = HuadongTmsOrderServer.GetHuadongWaybillVagueQuerys(SHIPDETAILID, pageIndexAndCount);
            }
            catch (Exception ex)
            {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return result;
        }

        public ResultModel<int> GethuadongWaybillVagueCount(string SHIPDETAILID)
        {
            ResultModel<int> result = new ResultModel<int>();
            try
            {
                result.Data = HuadongTmsOrderServer.GethuadongWaybillVagueCounts(SHIPDETAILID);
            }
            catch (Exception ex)
            {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return result;
        }
        #endregion

        #region 根据第三方运单号查询显示相对应的运单信息

        public ResultModel<List<Model_Waybill_Base>> GetWaybillThirdPartyList(string waybillNumber = null, string pageIndexAndCount = null, string startTime = null, string endTime = null, int customerId = 0, int roles = 0)
        {
            ResultModel<List<Model_Waybill_Base>> result = new ResultModel<List<Model_Waybill_Base>>();
            try
            {
                result.Data = WaybillBaseServer.GetWaybillThirdPartyList(waybillNumber, pageIndexAndCount, startTime, endTime, customerId, roles);
            }
            catch (Exception ex)
            {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return result;
        }

        public ResultModel<int> GetWaybillThirdPartyListCount(string waybillNumber = null, string startTime = null, string endTime = null, int customerId = 0, int roles = 0)
        {
            ResultModel<int> result = new ResultModel<int>();
            try
            {
                result.Data = WaybillBaseServer.GetWaybillThirdPartyListCount(waybillNumber,startTime,endTime,customerId,roles);
            }
            catch (Exception ex)
            {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 模糊查询
        /// </summary>
        /// <param name="waybillNumber"></param>
        /// <param name="pageIndexAndCount"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="customerId"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        public ResultModel<List<Model_Waybill_Base>> GetWaybillThirdPartyVagueList(string waybillNumber = null, string pageIndexAndCount = null, string startTime = null, string endTime = null, int customerId = 0)
        {
            ResultModel<List<Model_Waybill_Base>> result = new ResultModel<List<Model_Waybill_Base>>();
            try
            {
                result.Data = WaybillBaseServer.GetWaybillThirdPartyVagueList(waybillNumber, pageIndexAndCount, startTime, endTime, customerId);
            }
            catch (Exception ex)
            {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 模糊查询总数
        /// </summary>
        /// <param name="waybillNumber"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="customerId"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        public ResultModel<int> GetWaybillThirdPartyListVagueCount(string waybillNumber = null, string startTime = null, string endTime = null, int customerId = 0)
        {
            ResultModel<int> result = new ResultModel<int>();
            try
            {
                result.Data = WaybillBaseServer.GetWaybillThirdPartyListVagueCount(waybillNumber, startTime, endTime, customerId);
            }
            catch (Exception ex)
            {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return result;
        }
        #endregion

        #region 2017-7-25 增加区域选项，并根据区域显示下游客户
        /// <summary>
        /// 区域信息
        /// </summary>
        /// <returns></returns>
        public ResultModel<List<Model_Region>> GetZoneOptions()
        {
            ResultModel<List<Model_Region>> result = new ResultModel<List<Model_Region>>();
            try
            {
                result.Data = AreaCityServer.GetRegionCount();
            }
            catch (Exception ex)
            {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return result;
        }
        public ResultModel<List<Model_Region>> GetCity(int parentid = 0)
        {
            ResultModel<List<Model_Region>> result = new ResultModel<List<Model_Region>>();
            try
            {
                result.Data = AreaCityServer.GetCity(parentid);
            }
            catch (Exception ex)
            {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 根据区域显示下游客户
        /// </summary>
        /// <param name="zoneOption">区域id</param>
        /// <returns></returns>
        public ResultModel<List<Model_Customer>> GetDownstreamQZList( string zoneOption = null)
        {
            ResultModel<List<Model_Customer>> result = new ResultModel<List<Model_Customer>>();
            try
            {
                result.Data = CustomerServer.CustomerDownstreamQZ(zoneOption);
            }
            catch (Exception ex)
            {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return result;
        }
        
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
        public ResultModel<List<Model_Waybill_Base>> GetQueryClientsList(string waybillNumber = null, string pageIndexAndCount = null, string startTime = null, string endTime = null, int senderId = 0, int receiverId = 0)
        {
            ResultModel<List<Model_Waybill_Base>> result = new ResultModel<List<Model_Waybill_Base>>();
            try
            {
                result.Data = WaybillBaseServer.GetQueryClientsList(waybillNumber, pageIndexAndCount, startTime, endTime, senderId, receiverId);
            }
            catch (Exception ex)
            {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// 模糊查询总数
        /// </summary>
        /// <param name="waybillNumber"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="customerId"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        public ResultModel<int> GetQueryClientsListCount(string waybillNumber = null, string startTime = null, string endTime = null, int senderId = 0, int receiverId = 0)
        { 
            ResultModel<int> result = new ResultModel<int>();
            try
            {
                result.Data = WaybillBaseServer.GetQueryClientsListCount(waybillNumber, startTime, endTime, senderId, receiverId);
            }
            catch (Exception ex)
            {
                result.Code = 1;
                result.Message = ex.Message;
            }
            return result;
        }
        #endregion
    }
}
