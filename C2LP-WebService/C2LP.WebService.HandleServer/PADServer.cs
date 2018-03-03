using C2LP.WebService.Interface;
using System;
using System.Collections.Generic;
using C2LP.WebService.Model;
using C2LP.WebService.BLL.PDABLL;
using System.Reflection;
using System.Data;
using C2LP.WebService.BLL;

namespace C2LP.WebService.HandleServer
{
    /// <summary>
    /// PDA接口服务实现
    /// 明冰锋
    /// 2016年8月2日14:05:25
    /// </summary>
    public class PDAServer : IPDAInterface
    {
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
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.AppendLine("----------------------------------" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "：发现异常信息如下----------------------------------");
            sb.AppendLine("【Exception】" + ex.Message);
            sb.AppendLine("【TargetSite】" + ex.TargetSite);
            sb.AppendLine("【StackTrace】" + ex.StackTrace);
            if (ex.InnerException != null)
            {
                sb.AppendLine("【InnerException】" + ex.InnerException.Message);
                sb.AppendLine("【InnerExceptionTargetSite】" + ex.InnerException.TargetSite);
                sb.AppendLine("【InnerExceptionStackTrace】" + ex.InnerException.StackTrace);
            }
            sb.AppendLine("---------------------------------------------------------------------------------------------------------");
            Console.WriteLine(sb);
        }

        /// <summary>
        /// PDA端设置编号时检测编号是否存在
        /// </summary>
        /// <param name="num">设备编号</param>
        /// <returns></returns>
        public ResultModel<bool> SetPDANumber(string num)
        {
            ResultModel<bool> result = new ResultModel<bool>();
            try
            {
                result.Data = PDA_DeviceServer.CheckNumberExist(num);
            }
            catch (Exception ex)
            {
                HandleExcepthin(result, ex);
            }
            return result;
        }

        /// <summary>
        /// 根据编号获取PDA设备信息
        /// </summary>
        /// <param name="num">设备编号</param>
        /// <returns></returns>
        public ResultModel<Model_PDAInfo> GetPDAInfo(string num)
        {
            ResultModel<Model_PDAInfo> result = new ResultModel<Model_PDAInfo>();
            try
            {
                result.Data = PDA_DeviceServer.GetPDAInfoByNum(num);
            }
            catch (Exception ex)
            {
                HandleExcepthin(result, ex);
            }
            return result;
        }

        /// <summary>
        /// 根据设备ID获取所有关联的目的地
        /// </summary>
        /// <param name="pId">设备ID</param>
        /// <returns></returns>
        public ResultModel<List<string>> GetDestins(int pId)
        {
            ResultModel<List<string>> result = new ResultModel<List<string>>();
            try
            {
                result.Data = PDA_DestinServer.GetDistins(pId);
            }
            catch (Exception ex)
            {
                HandleExcepthin(result, ex);
            }
            return result;
        }

        /// <summary>
        /// 根据设备ID获取所有关联的冷藏载体
        /// </summary>
        /// <param name="pId">设备Id</param>
        /// <returns></returns>
        public ResultModel<List<Model_ColdStorage>> GetStorages(int pId)
        {
            ResultModel<List<Model_ColdStorage>> result = new ResultModel<List<Model_ColdStorage>>();
            try
            {
                result.Data = PDA_StorageServer.GetStoragesByPId(pId);
            }
            catch (Exception ex)
            {
                HandleExcepthin(result, ex);
            }
            return result;
        }

        /// <summary>
        /// 根据设备ID获取所有关联的客户信息 弃用
        /// </summary>
        /// <param name="pId">设备ID</param>
        /// <returns></returns>
        public ResultModel<List<Model_Customer>> GetCustomers(int pId)
        {
            ResultModel<List<Model_Customer>> result = new ResultModel<List<Model_Customer>>();
            try
            {
                result.Data = PDA_CustomerServer.GetCustomerByPId(pId);
            }
            catch (Exception ex)
            {
                HandleExcepthin(result, ex);
            }
            return result;
        }

        /// <summary>
        /// 根据设备ID获取所有关联的客户信息 弃用
        /// </summary>
        /// <param name="pId">设备ID</param>
        /// <returns></returns>
        public ResultModel<List<Model_Customer>> GetCustomersFromMaxId(int pId, int maxId)
        {
            ResultModel<List<Model_Customer>> result = new ResultModel<List<Model_Customer>>();
            try
            {
                result.Data = PDA_CustomerServer.GetCustomerByPId(pId, maxId);
            }
            catch (Exception ex)
            {
                HandleExcepthin(result, ex);
            }
            return result;
        }

        //弃用
        public ResultModel<List<PDA_Model_Region>> GetRegionsFromMaxId(int pId, int maxId)
        {
            ResultModel<List<PDA_Model_Region>> result = new ResultModel<List<PDA_Model_Region>>();
            try
            {
                result.Data = PDA_RangeServer.GetRangeByPId(pId, maxId);
            }
            catch (Exception ex)
            {
                HandleExcepthin(result, ex);
            }
            return result;
        }

        /// <summary>
        /// 根据设备ID获取其负责区域 弃用
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        public ResultModel<List<PDA_Model_Region>> GetRegions(int pId)
        {
            ResultModel<List<PDA_Model_Region>> result = new ResultModel<List<PDA_Model_Region>>();
            try
            {
                result.Data = PDA_RangeServer.GetRangeByPId(pId);
            }
            catch (Exception ex)
            {
                HandleExcepthin(result, ex);
            }
            return result;
        }

        /// <summary>
        /// 获取服务器当前时间 弃用
        /// </summary>
        /// <returns></returns>
        public ResultModel<DateTime> GetServerTime()
        {
            ResultModel<DateTime> result = new ResultModel<DateTime>();
            result.Data = DateTime.Now;
            return result;
        }

        /// <summary>
        /// 上传运单信息
        /// </summary>
        /// <param name="waybillList">运单集合</param>
        /// <returns></returns>
        public ResultModel<bool> UploadWaybill_Base(List<Model_Waybill_Base> waybillList)
        {
            ResultModel<bool> result = new ResultModel<bool>();
            try
            {
                result.Data = PDA_WaybillServer.UploadWaybill_Base(waybillList);
            }
            catch (Exception ex)
            {
                HandleExcepthin(result, ex);
            }
            return result;
        }

        /// <summary>
        /// 上传节点信息 弃用
        /// </summary>
        /// <param name="nodeList">节点集合</param>
        /// <returns></returns>
        public ResultModel<bool> UploadWaybill_Node(List<Model_Waybill_Node> nodeList)
        {
            ResultModel<bool> result = new ResultModel<bool>();
            try
            {
                foreach (Model_Waybill_Node item in nodeList)
                {
                    if (PDA_HuadongTmsOrderServer.ChecNumber(item.BaseId) && item.BaseId.Length == 12)
                        result.Data = PDA_WaybillServer.UploadWaybill_Node(nodeList);
                    else
                        //result.Data = PDA_HuadongTmsOrderServer.GethuadongTmsOrderNode(nodeList);
                        result.Data = PDA_WaybillServer.UploadWaybill_Node(nodeList[0], 669,null); 
                }
                //  result.Data = PDA_WaybillServer.UploadWaybill_Node(nodeList);
            }
            catch (Exception ex)
            {
                HandleExcepthin(result, ex);
            }
            return result;
        }

        /// <summary>
        /// 上传签收图片 弃用
        /// </summary>
        /// <param name="postback">签收信息</param>
        /// <param name="picList">图片集合</param>
        /// <returns></returns>
        public ResultModel<bool> UploadWaybill_Postback(Model_Waybill_Postback_Pic postback, DateTime postbackTime, List<object> picList)
        {
            ResultModel<bool> result = new ResultModel<bool>();
            try
            {
                if (PDA_HuadongTmsOrderServer.ChecNumber(postback.BaseId) && postback.BaseId.Length == 12)
                    result.Data = PDA_WaybillServer.UploadWaybill_Postback(postback, postbackTime, picList);
                else
                    //result.Data = PDA_HuadongTmsOrderServer.UploadWaybill_HuaDong(postback, postbackTime, picList);
                    result.Data = PDA_WaybillServer.UploadWaybill_Postback(postback, postbackTime, picList, 669);
            }
            catch (Exception ex)
            {
                HandleExcepthin(result, ex);
            }
            return result;
        }

        /// <summary>
        /// 上传运单信息（第三方） 弃用
        /// </summary>
        /// <param name="huadongList"></param>
        /// <returns></returns>
        public ResultModel<bool> UploadHuadongTmsOrder(List<Model_Huadong_Tms_Order> huadongList)
        {
            ResultModel<bool> result = new ResultModel<bool>();
            try
            {
                result.Data = PDA_HuadongTmsOrderServer.GethuadongTmsOrder(huadongList);
            }
            catch (Exception ex)
            {
                HandleExcepthin(result, ex);
            }
            return result;
        }

        //弃用
        public ResultModel<int> UploadThirdPartyOrder(Model_ThirdPartOrder orderInfo)
        {
            ResultModel<int> result = new ResultModel<int>();
            try
            {
                //result.Data = PDA_HuadongTmsOrderServer.UploadThirdPartyOrder(orderInfo);
                result.Data = PDA_HuadongTmsOrderServer.UploadThirdPartyOrder(orderInfo, 669);
            }
            catch (Exception ex)
            {
                HandleExcepthin(result, ex);
            }
            return result;
        }

        /// <summary>
        /// 获取指定时间后更新的客户信息
        /// </summary>
        /// <param name="pId">设备ID</param>
        /// <param name="getTime">获取时间</param>
        /// <param name="maxId">当前获取到的客户信息索引</param>
        /// <returns></returns>
        public ResultModel<List<PDA_Model_Customer>> GetNewCustomers(int pId, DateTime getTime, int maxId)
        {
            ResultModel<List<PDA_Model_Customer>> result = new ResultModel<List<PDA_Model_Customer>>();
            try
            {
                result.Data = PDA_CustomerServer.GetNewCustomers(getTime, maxId);
            }
            catch (Exception ex)
            {
                HandleExcepthin(result, ex);
            }
            return result;
        }

        /// <summary>
        /// 获取指定时间后更新的区域信息
        /// </summary>
        /// <param name="pId">设备ID</param>
        /// <param name="getTime">获取时间</param>
        /// <param name="maxId">当前获取到的区域信息索引</param>
        /// <returns></returns>
        public ResultModel<List<PDA_Model_Region>> GetNewRegions(int pId, DateTime getTime, int maxId)
        {
            ResultModel<List<PDA_Model_Region>> result = new ResultModel<List<PDA_Model_Region>>();
            try
            {
                result.Data = PDA_RangeServer.GetNewRegions(getTime, maxId);
            }
            catch (Exception ex)
            {
                HandleExcepthin(result, ex);
            }
            return result;
        }

        /// <summary>
        /// 获取服务器时间字符串
        /// </summary>
        /// <returns></returns>
        public ResultModel<string> GetServerTimeStr()
        {
            ResultModel<string> result = new ResultModel<string>();
            result.Data = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            return result;
        }

        //2017年3月30日10:13:38 添加新的接口，使PDA支持多个第三方上游发货单位的接入

        /// <summary>
        /// 获取所有第三方客户
        /// </summary>
        /// <returns></returns>
        public ResultModel<List<Model_ThirdCustomer>> GetThirdCustomers()
        {
            ResultModel<List<Model_ThirdCustomer>> result = new ResultModel<List<Model_ThirdCustomer>>();
            try
            {
                result.Data = PDA_CustomerServer.GetThirdCustomers();
            }
            catch (Exception ex)
            {
                HandleExcepthin(result, ex);
            }
            return result;
        }

        //上报第三方运单
        public ResultModel<int> UploadThirdOrderForCustomer(Model_ThirdPartOrder orderInfo, int customerId)
        {
            if (orderInfo != null)
                LogServer.AddLogText("--------------------------------------------------------------", orderInfo.RelationId);
            else
                LogServer.AddLogText("实体为空", "XXX");
            ResultModel<int> result = new ResultModel<int>();
            try
            {
                LogServer.AddLogText(string.Format("上报第三方运单{0},customerId:{1}", orderInfo.RelationId, customerId), orderInfo.RelationId);
                result.Data = PDA_HuadongTmsOrderServer.UploadThirdPartyOrder(orderInfo, customerId);
                LogServer.AddLogText("退出运单逻辑", orderInfo.RelationId);
            }
            catch (Exception ex)
            {
                HandleExcepthin(result, ex);
                LogServer.AddLogText("运单错误", orderInfo.RelationId);
            }
            finally {
                if (orderInfo != null)
                    LogServer.AddLogText("--------------------------------------------------------------", orderInfo.RelationId);
            }
            return result;
        }

        //上报节点统一入口，如果是自运单，则CustomerID传入0
        public ResultModel<bool> UploadNodeForCustomer(Model_Waybill_Node node, int customerId)
        {
            if(node!=null)
                LogServer.AddLogText("--------------------------------------------------------------", node.BaseId);
            else
            LogServer.AddLogText("实体为空", "XXX");
            ResultModel <bool> result = new ResultModel<bool>();
            try
            {
                LogServer.AddLogText(string.Format("上报第三方运单节点{0},customerId:{1}", node.BaseId, customerId), node.BaseId);
                result.Data = PDA_WaybillServer.UploadWaybill_Node(node, customerId,null);
                LogServer.AddLogText("退出节点逻辑", node.BaseId);
            }
            catch (Exception ex)
            {
                HandleExcepthin(result, ex);
                LogServer.AddLogText("节点错误", node.BaseId);
            }
            finally
            {
                if (node != null)
                    LogServer.AddLogText("--------------------------------------------------------------", node.BaseId);
            }
            return result;
        }

        //上报签收图片统一入口，如果是自运单，则CustomerID传入0
        public ResultModel<bool> UploadPostbackForCustomer(Model_Waybill_Postback_Pic postback, DateTime postbackTime, List<object> picList, int customerId)
        {
            if (postback != null)
                LogServer.AddLogText("-------------------------------------------------------------------", postback.BaseId);
            else
                LogServer.AddLogText("实体类为空","xxxx");
            ResultModel<bool> result = new ResultModel<bool>();
            try
            {
                LogServer.AddLogText(string.Format("上传图片{0},DateTime:{1},customerId:{2}",postback.BaseId,postbackTime,customerId),postback.BaseId);
                result.Data = PDA_WaybillServer.UploadWaybill_Postback(postback, postbackTime, picList, customerId);
                LogServer.AddLogText("退出保存图片逻辑",postback.BaseId);
            }
            catch (Exception ex)
            {
                HandleExcepthin(result, ex);
                LogServer.AddLogText("图片错误",postback.BaseId);
            }
            finally
            {
                if (postback != null)
                    LogServer.AddLogText("--------------------------------------------------------------", postback.BaseId);
            }
            return result;
        }
        //上报签收图片统一入口，如果是自运单，则CustomerID传入0
        public ResultModel<bool> UploadPostbackForCustomers(Model_Waybill_Postback_Pic postback, DateTime postbackTime, List<object> picList, int customerId)
        {
            if (postback != null)
                LogServer.AddLogText("-------------------------------------------------------------------", postback.BaseId);
            else
                LogServer.AddLogText("实体类为空", "xxxx");
            ResultModel<bool> result = new ResultModel<bool>();
            try
            {
                LogServer.AddLogText(string.Format("上传图片{0},DateTime:{1},customerId:{2}", postback.BaseId, postbackTime, customerId), postback.BaseId);
                result.Data = PDA_WaybillServer.UploadWaybill_Postbacks(postback, postbackTime, picList, customerId);
                LogServer.AddLogText("退出保存图片逻辑", postback.BaseId);
            }
            catch (Exception ex)
            {
                HandleExcepthin(result, ex);
                LogServer.AddLogText("图片错误", postback.BaseId);
            }
            finally
            {
                if (postback != null)
                    LogServer.AddLogText("--------------------------------------------------------------", postback.BaseId);
            }
            return result;
        }

        //2017年7月26日14:25:02 添加新的接口 获取所有冷藏载体信息

        /// <summary>
        ///     
        /// </summary>
        /// <returns></returns>
        public ResultModel<List<Model_ColdStorage>> GetStorageScan()
        {
            ResultModel<List<Model_ColdStorage>> result = new ResultModel<List<Model_ColdStorage>>();
            try
            {
                result.Data = PDA_StorageServer.GetStorageScan();
            }
            catch (Exception ex)
            {
                HandleExcepthin(result, ex);
            }
            return result;
        }

        public ResultModel<bool> UploadNodeForStorage(Model_Waybill_Node node, int customerId,int? parentStorageId)
        {
            if (node != null)
                LogServer.AddLogText("--------------------------------------------------------------", node.BaseId);
            else
                LogServer.AddLogText("实体为空", "XXX");
            ResultModel<bool> result = new ResultModel<bool>();
            try
            {
                LogServer.AddLogText(string.Format("上报第三方运单节点{0},customerId:{1},parentStorageId", node.BaseId, customerId, parentStorageId), node.BaseId);
                result.Data = PDA_WaybillServer.UploadWaybill_Node(node, customerId, parentStorageId);
                LogServer.AddLogText("退出节点逻辑", node.BaseId);
            }
            catch (Exception ex)
            {
                HandleExcepthin(result, ex);
                LogServer.AddLogText("节点错误", node.BaseId);
            }
            finally
            {
                if (node != null)
                    LogServer.AddLogText("--------------------------------------------------------------", node.BaseId);
            }
            return result;
        }
    }
}
