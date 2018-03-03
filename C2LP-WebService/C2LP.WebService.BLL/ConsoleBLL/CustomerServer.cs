using C2LP.WebService.Model;
using C2LP.WebService.Model.MyEnum;
using C2LP.WebService.Utility;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2LP.WebService.BLL.ConsoleBLL
{
    /// <summary>
    /// 客户相关业务类
    /// </summary>
    public class CustomerServer : BaseServer
    {
        #region
        /// <summary>
        /// 检测顶级企业的登陆用户是否存在
        /// </summary>
        /// <param name="userName">登陆名</param>
        /// <param name="password">登陆密码</param>
        /// <returns></returns>
        public static Model_Customer CheckCustomerUser(string userName, string password)
        {
            string sql = "select * from customer where role = 1 and actived = 0 limit 1;";
            Model_Customer customer = _SqlHelp.ExecuteObject<Model_Customer>(sql);
            if (customer == null)
                throw new Exception("顶级企业不存在!");
            sql = "select * from customer_users where userName=?p1 and customerId = ?p2 limit 1;";
            MySqlParameter[] p = new MySqlParameter[2];
            p[0] = new MySqlParameter("p1", userName);
            p[1] = new MySqlParameter("p2", customer.Id);
            Model_CustomerUser user = _SqlHelp.ExecuteObject<Model_CustomerUser>(sql,p);
            if (user == null)
                throw new Exception(string.Format("用户名'{0}'不存在!",userName));
            if (user.Password.ToUpper() != MyTool.UserMd5(password).ToUpper())
                throw new Exception("密码不正确!");
            if (user.Actived== Model.MyEnum.Enum_Active.Disable)
                throw new Exception("用户已停用");
            
            return customer;
        }
        /// <summary>
        /// 查询全部客户
        /// </summary>
        /// <param name="customerId">当customerId=0时查询全部客户</param>
        /// <returns></returns>
        public static List<Model_Customer> CustomerQuery(Enum_Role role, int customerId)
        {
            string sql= "select * from customer ";
            if (customerId == 0)
            {
                if (role==Enum_Role.Sender) {
                    sql += " where role=2;";
                }
                if (role == Enum_Role.Receiver)
                {
                    sql += " where role=3;";
                }
                
            }
            //if (customerId > 0)
            //    sql = "select * from customer where  id=?id ";

            //MySqlParameter[] para = new MySqlParameter[1];
            //para[0] = new MySqlParameter("id", customerId);
            List<Model_Customer> customer = _SqlHelp.ExecuteObjects<Model_Customer>(sql);
            return customer;
        }

        public static Model_Customer CustomerNew(Model_Customer customer) {

            string sql="";
            if (customer.Id == 0)
            {
                Model_Customer user = GetAccount(customer);
                if (user != null)
                    throw new Exception("客户账号已存在，必须唯一");
            }
            else
            {
                //查询登陆账号是否存在
                sql = "select * from customer where id=?id";
                MySqlParameter[] pa = new MySqlParameter[1];
                pa[0] = new MySqlParameter("id", customer.Id);
                Model_Customer user = _SqlHelp.ExecuteObject<Model_Customer>(sql, pa);
                if (user.Account != customer.Account)
                {
                    Model_Customer usesr = GetAccount(customer);
                    if (usesr != null)
                        throw new Exception("客户账号已存在，必须唯一");
                }
            }

            if (customer.Id == 0)
            {
                sql = "insert into customer(fullName,contactPerson,contactTel,contactAddress,provinceId,provinceName,cityId,cityName,account,role,actived,createAt,remark)" +
                "values(?fullName,?contactPerson,?contactTel,?contactAddress,?provinceId,?provinceName,?cityId,?cityName,?account,?role,?actived,?createAt,?remark)";
            }
            else
            {
                sql = "update customer set fullName=?fullName,contactPerson=?contactPerson,contactTel=?contactTel,contactAddress=?contactAddress,provinceId=?provinceId,provinceName=?provinceName,cityId=?cityId,cityName=?cityName,account=?account,actived=?actived,remark=?remark" +
                            " where id =?id";
            }
            MySqlParameter[] para = new MySqlParameter[14];
            para[0] = new MySqlParameter("fullName", customer.FullName);
            para[1] = new MySqlParameter("contactPerson", customer.ContactPerson);
            para[2] = new MySqlParameter("contactTel", customer.ContactTel);
            para[3] = new MySqlParameter("contactAddress", customer.ContactAddress);
            para[4] = new MySqlParameter("provinceId", customer.ProvinceId);
            para[5] = new MySqlParameter("provinceName", customer.ProvinceName);
            para[6] = new MySqlParameter("cityId", customer.CityId);
            para[7] = new MySqlParameter("cityName", customer.CityName);
            para[8] = new MySqlParameter("account", customer.Account);
            para[9] = new MySqlParameter("role", customer.Role);
            para[10] = new MySqlParameter("actived", customer.Actived);
            para[11] = new MySqlParameter("createAt", customer.CreateAt);
            para[12] = new MySqlParameter("remark", customer.Remark);
            para[13] = new MySqlParameter("id",customer.Id);
            int result = 0;
            if (customer.Id == 0)
                result = _SqlHelp.ExecuteNonQuery(sql, para);
            else
                result = _SqlHelp.ExecuteNonQuery(sql, para);
            if (result != 1)
                throw new Exception("操作失败!");
            return customer;

        }
        public static Model_Customer GetAccount(Model_Customer customer)
        {
            //查询登陆账号是否存在
            string sql = "select * from customer where account=?account";
            MySqlParameter[] pa = new MySqlParameter[1];
            pa[0] = new MySqlParameter("account", customer.Account);
            Model_Customer user = _SqlHelp.ExecuteObject<Model_Customer>(sql, pa);
            return user;
        }
        /// <summary>
        /// 根据角色查询数据
        /// </summary>
        /// <param name="role"></param>
        /// <param name="provinceId"></param>
        /// <param name="cityId"></param>
        /// <param name="pageIndexAndCount"></param>
        /// <returns></returns>
        public static List<Model_Customer> GetCustomerListByRoles(Enum_Role role,int provinceId,int cityId,string pageIndexAndCount)
        {
            string sql = "select  id,fullName,contactperson,contactTel,contactAddress,"+
                           "provinceId,provinceName,cityId,cityName,account,"+
                           "role,actived,createAt,concat_ws('|',if(isnull(remark),'',remark),if(isnull(BindReceiverOrg),'',BindReceiverOrg)) remark from customer  where  role=?role";
            //管理
            if (role == Enum_Role.Administrator)
                sql += "  limit 1 ";
            //发货单位
            if (role == Enum_Role.Sender)
                if (provinceId != 0 && cityId != 0)
                        sql += "  and provinceId=?provinceId and cityId=?cityId ";
            //收货单位
            if (role == Enum_Role.Receiver)
                if (provinceId != 0 && cityId != 0)
                        sql += "  and provinceId=?provinceId and cityId=?cityId ";
            if (pageIndexAndCount != null)
            {
                //截取当前页数
              string  page = pageIndexAndCount.Substring(0, pageIndexAndCount.LastIndexOf("."));
                //截取每页显示记录数
              string  size = pageIndexAndCount.Substring(pageIndexAndCount.LastIndexOf(".") + 1, pageIndexAndCount.Length - (pageIndexAndCount.LastIndexOf(".") + 1));
                sql += "  order by  createAt desc limit " + ((Convert.ToInt32(page) - 1) * Convert.ToInt32(size)) + "," + size + ";";
            }
            else
                sql += " ;";
            MySqlParameter[] para = new MySqlParameter[3];
            para[0] = new MySqlParameter("role",role);
            para[1] = new MySqlParameter("provinceId", provinceId);
            para[2] = new MySqlParameter("cityId", cityId);
            List<Model_Customer> list = _SqlHelp.ExecuteObjects<Model_Customer>(sql,para);

            return list;
        }
        /// <summary>
        /// 查询客户总数
        /// </summary>
        /// <param name="role"></param>
        /// <param name="provinceId"></param>
        /// <param name="cityId"></param>
        /// <returns></returns>
        public static int GetCustomerListByRoleCount(Enum_Role role, int provinceId, int cityId)
        {
            string sql = "select  count(*)  from customer  where  role=?role";
            //管理
            if (role == Enum_Role.Administrator)
                sql += "  limit 1 ";
            //发货单位
            if (role == Enum_Role.Sender)
                if (provinceId != 0 && cityId != 0)
                    sql += "  and provinceId=?provinceId and cityId=?cityId ";
            //收货单位
            if (role == Enum_Role.Receiver)
                if (provinceId != 0 && cityId != 0)
                    sql += "  and provinceId=?provinceId and cityId=?cityId ";

                sql += " ;";
            MySqlParameter[] para = new MySqlParameter[3];
            para[0] = new MySqlParameter("role", role);
            para[1] = new MySqlParameter("provinceId", provinceId);
            para[2] = new MySqlParameter("cityId", cityId);
            return Convert.ToInt32( _SqlHelp.ExecuteScalar(sql, para));
            
        }

        /// <summary>
        /// 根据客户表的id查询，并返回值给CustomerUserServer
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        public static Model_Customer GetQueryCustomer(int customerId)
        {
            string sql = "select * from customer where id=?id";
            MySqlParameter[] para = new MySqlParameter[1];
            para[0] = new MySqlParameter("id", customerId);
            Model_Customer customer = _SqlHelp.ExecuteObject<Model_Customer>(sql,para);
            return customer;
        }


        /// <summary>
        /// 模糊
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="pageIndexAndCount"></param>
        /// <returns></returns>
        public static List<Model_Customer> GetCustomerVageue(Model_Customer customer,string pageIndexAndCount)
        {
            if (customer == null)
                throw new Exception("没有客户信息");

            string sql = "select * from customer where role = 3 ";
            if (customer.ProvinceId != 0 && customer.CityId != 0)
            {
                sql += " and provinceId=?provinceId and cityId=?cityId ";
            }
            if (customer.FullName!=null)
            {
                sql += " and fullName like '%"+ customer.FullName + "%'";
            }
            
            if (pageIndexAndCount!=null)
            {
                //截取当前页数
                string page = pageIndexAndCount.Substring(0, pageIndexAndCount.LastIndexOf("."));
                //截取每页显示记录数
                string size = pageIndexAndCount.Substring(pageIndexAndCount.LastIndexOf(".") + 1, pageIndexAndCount.Length - (pageIndexAndCount.LastIndexOf(".") + 1));
                sql += "  order by  createAt desc limit " + ((Convert.ToInt32(page) - 1) * Convert.ToInt32(size)) + "," + size + ";";
            }
            sql += " ;";
            MySqlParameter[] para = new MySqlParameter[2];
            para[0] = new MySqlParameter("provinceId", customer.ProvinceId);
            para[1] = new MySqlParameter("cityId", customer.CityId);
            List<Model_Customer> cust = _SqlHelp.ExecuteObjects<Model_Customer>(sql,para);
            return cust;

        }

        public static int GetVagueQueryCount(Model_Customer customer)
        {
            if (customer==null)
            {
                throw new Exception("没有客户信息");
            }
            string sql = "select count(*) from customer where role = 3 ";
            if (customer.ProvinceId != 0 && customer.CityId != 0)
            {
                sql += " and provinceId=?provinceId and cityId=?cityId ";
            }
            if (customer.FullName != null)
            {
                sql += " and fullName like '%" + customer.FullName + "%'";
            }
            sql += " ;";
            MySqlParameter[] para = new MySqlParameter[2];
            para[0] = new MySqlParameter("provinceId", customer.ProvinceId);
            para[1] = new MySqlParameter("cityId", customer.CityId);

            int result =Convert.ToInt32( _SqlHelp.ExecuteScalar(sql, para));
            return result;
        }
        #endregion

        #region 新增县级（区域）
        /// <summary>
        /// 新增县级（区域）
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public static Model_Customer GetCustomerCounty(Model_Customer customer)
        {
            string sql = "";
            if (customer.Id == 0)
            {
                Model_Customer user = GetAccount(customer);
                if (user != null)
                    throw new Exception("客户账号已存在，必须唯一");
            }
            else
            {
                //查询登陆账号是否存在
                sql = "select * from customer where id=?id";
                MySqlParameter[] pa = new MySqlParameter[1];
                pa[0] = new MySqlParameter("id", customer.Id);
                Model_Customer user = _SqlHelp.ExecuteObject<Model_Customer>(sql, pa);
                if (user.Account != customer.Account)
                {
                    Model_Customer usesr = GetAccount(customer);
                    if (usesr != null)
                        throw new Exception("客户账号已存在，必须唯一");
                }
            }
            if (customer.Id == 0)
            {
                sql = "insert into customer(fullName,contactPerson,contactTel,contactAddress,provinceId,provinceName,cityId,cityName,countyId,countyName,account,role,actived,createAt,remark)" +
                "values(?fullName,?contactPerson,?contactTel,?contactAddress,?provinceId,?provinceName,?cityId,?cityName,?countyId,?countyName,?account,?role,?actived,?createAt,?remark)";
            }
            else
            {
                sql = "update customer set fullName=?fullName,contactPerson=?contactPerson,contactTel=?contactTel,contactAddress=?contactAddress,provinceId=?provinceId,provinceName=?provinceName,cityId=?cityId,cityName=?cityName,countyId=?countyId,countyName=?countyName,account=?account,actived=?actived,remark=?remark" +
                       " where id =?id";
            }
            MySqlParameter[] para = new MySqlParameter[16];
            para[0] = new MySqlParameter("fullName", customer.FullName);
            para[1] = new MySqlParameter("contactPerson", customer.ContactPerson);
            para[2] = new MySqlParameter("contactTel", customer.ContactTel);
            para[3] = new MySqlParameter("contactAddress", customer.ContactAddress);
            para[4] = new MySqlParameter("provinceId", customer.ProvinceId);
            para[5] = new MySqlParameter("provinceName", customer.ProvinceName);
            para[6] = new MySqlParameter("cityId", customer.CityId);
            para[7] = new MySqlParameter("cityName", customer.CityName);
            para[8] = new MySqlParameter("account", customer.Account);
            para[9] = new MySqlParameter("role", customer.Role);
            para[10] = new MySqlParameter("actived", customer.Actived);
            para[11] = new MySqlParameter("createAt", customer.CreateAt);
            para[12] = new MySqlParameter("remark", customer.Remark);
            para[13] = new MySqlParameter("id", customer.Id);
            para[14] = new MySqlParameter("countyId", customer.CountyId);
            para[15] = new MySqlParameter("countyName", customer.CountyName);
            int result = 0;
            if (customer.Id == 0)
                result = _SqlHelp.ExecuteNonQuery(sql, para);
            else
                result = _SqlHelp.ExecuteNonQuery(sql, para);
            if (result != 1)
                throw new Exception("操作失败!");
            return customer;

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
        public static List<Model_Customer> GetCustomerListByCounty(Enum_Role role, int provinceId, int cityId,int countyId, string pageIndexAndCount)
        {
            string sql = "select  id,fullName,contactperson,contactTel,contactAddress," +
                           "provinceId,provinceName,cityId,cityName,countyId,countyName,account," +
                           "role,actived,createAt,concat_ws('|',if(isnull(remark),'',remark),if(isnull(BindReceiverOrg),'',BindReceiverOrg)) remark from customer  where  role=?role";
            //管理
            if (role == Enum_Role.Administrator)
                sql += "  limit 1 ";
            //发货单位
            if (role == Enum_Role.Sender)
                if (provinceId != 0 && cityId != 0 && countyId!=0)
                    sql += "  and provinceId=?provinceId and cityId=?cityId and countyId=?countyId ";
            //收货单位
            if (role == Enum_Role.Receiver)
                if (provinceId != 0 && cityId != 0 && countyId!=0)
                    sql += "  and provinceId=?provinceId and cityId=?cityId and countyId=?countyId ";
            if (pageIndexAndCount != null)
            {
                //截取当前页数
                string page = pageIndexAndCount.Substring(0, pageIndexAndCount.LastIndexOf("."));
                //截取每页显示记录数
                string size = pageIndexAndCount.Substring(pageIndexAndCount.LastIndexOf(".") + 1, pageIndexAndCount.Length - (pageIndexAndCount.LastIndexOf(".") + 1));
                sql += "  order by  createAt desc limit " + ((Convert.ToInt32(page) - 1) * Convert.ToInt32(size)) + "," + size + ";";
            }
            else
                sql += " ;";
            MySqlParameter[] para = new MySqlParameter[4];
            para[0] = new MySqlParameter("role", role);
            para[1] = new MySqlParameter("provinceId", provinceId);
            para[2] = new MySqlParameter("cityId", cityId);
            para[3] = new MySqlParameter("countyId", countyId);
            List<Model_Customer> list = _SqlHelp.ExecuteObjects<Model_Customer>(sql, para);

            return list;
        }
        /// <summary>
        /// 根据省市县查询客户总数
        /// </summary>
        /// <param name="role"></param>
        /// <param name="provinceId"></param>
        /// <param name="cityId"></param>
        /// <param name="countyId"></param>
        /// <returns></returns>
        public static int GetCustomerListByCountyCount(Enum_Role role, int provinceId, int cityId,int countyId)
        {
            string sql = "select  count(*)  from customer  where  role=?role";
            //管理
            if (role == Enum_Role.Administrator)
                sql += "  limit 1 ";
            //发货单位
            if (role == Enum_Role.Sender)
                if (provinceId != 0 && cityId != 0 && countyId!=0)
                    sql += "  and provinceId=?provinceId and cityId=?cityId and countyId=?countyId ";
            //收货单位
            if (role == Enum_Role.Receiver)
                if (provinceId != 0 && cityId != 0 && countyId != 0)
                    sql += "  and provinceId=?provinceId and cityId=?cityId and countyId=?countyId ";

            sql += " ;";
            MySqlParameter[] para = new MySqlParameter[4];
            para[0] = new MySqlParameter("role", role);
            para[1] = new MySqlParameter("provinceId", provinceId);
            para[2] = new MySqlParameter("cityId", cityId);
            para[3] = new MySqlParameter("countyId",countyId);
            return Convert.ToInt32(_SqlHelp.ExecuteScalar(sql, para));

        }
        /// <summary>
        /// 根据省市县或是客户名称查询收货单位
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="pageIndexAndCount"></param>
        /// <returns></returns>
        public static List<Model_Customer> GetConsigneeCounty(Model_Customer customer, string pageIndexAndCount)
        {
            if (customer == null)
                throw new Exception("没有客户信息");
            string sql = "select id,fullName,contactperson,contactTel,contactAddress," +
                           "provinceId,provinceName,cityId,cityName,countyId,countyName,account," +
                           "role,actived,createAt,concat_ws('|',if(isnull(remark),'',remark),if(isnull(BindReceiverOrg),'',BindReceiverOrg)) remark from customer where role = 3 ";
            if (customer.ProvinceId != 0 && customer.CityId != 0 && customer.CountyId!=0)
            {
                sql += " and provinceId=?provinceId and cityId=?cityId and countyId=?countyId ";
            }
            if (customer.FullName != null)
            {
                sql += " and fullName like '%" + customer.FullName + "%'";
            }
            if (pageIndexAndCount != null)
            {
                //截取当前页数
                string page = pageIndexAndCount.Substring(0, pageIndexAndCount.LastIndexOf("."));
                //截取每页显示记录数
                string size = pageIndexAndCount.Substring(pageIndexAndCount.LastIndexOf(".") + 1, pageIndexAndCount.Length - (pageIndexAndCount.LastIndexOf(".") + 1));
                sql += "  order by  createAt desc limit " + ((Convert.ToInt32(page) - 1) * Convert.ToInt32(size)) + "," + size + ";";
            }
            sql += " ;";
            MySqlParameter[] para = new MySqlParameter[3];
            para[0] = new MySqlParameter("provinceId", customer.ProvinceId);
            para[1] = new MySqlParameter("cityId", customer.CityId);
            para[2] = new MySqlParameter("countyId",customer.CountyId);
            List<Model_Customer> cust = _SqlHelp.ExecuteObjects<Model_Customer>(sql, para);
            return cust;
        }
        /// <summary>
        /// 根据省市县或是客户名称查询收货单位总数
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public static int GetConsigneeCountyCount(Model_Customer customer)
        {
            if (customer == null)
            {
                throw new Exception("没有客户信息");
            }
            string sql = "select count(*) from customer where role = 3 ";
            if (customer.ProvinceId != 0 && customer.CityId != 0 && customer.CountyId!=0 )
            {
                sql += " and provinceId=?provinceId and cityId=?cityId and countyId=?countyId ";
            }
            if (customer.FullName != null)
            {
                sql += " and fullName like '%" + customer.FullName + "%'";
            }
            sql += " ;";
            MySqlParameter[] para = new MySqlParameter[3];
            para[0] = new MySqlParameter("provinceId", customer.ProvinceId);
            para[1] = new MySqlParameter("cityId", customer.CityId);
            para[2] = new MySqlParameter("countyId",customer.CountyId);
            int result = Convert.ToInt32(_SqlHelp.ExecuteScalar(sql, para));
            return result;
        }
        #endregion

        #region 更新时间
        public static bool UpdateCustomer(Model_Customer customer, string bindReceiverOrg) {
            string sql = "";
            if (customer.Id == 0)
            {
                Model_Customer user = GetAccount(customer);
                if (user != null)
                    throw new Exception("客户账号已存在，必须唯一");
            }
            else
            {
                //查询登陆账号是否存在
                sql = "select * from customer where id=?id";
                MySqlParameter[] pa = new MySqlParameter[1];
                pa[0] = new MySqlParameter("id", customer.Id);
                Model_Customer user = _SqlHelp.ExecuteObject<Model_Customer>(sql, pa);
                if (user.Account != customer.Account)
                {
                    Model_Customer usesr = GetAccount(customer);
                    if (usesr != null)
                        throw new Exception("客户账号已存在，必须唯一");
                }
            }
            if (string.IsNullOrEmpty(bindReceiverOrg))
                bindReceiverOrg = null;
            if (customer.Id == 0)
            {
                sql = "insert into customer(fullName,contactPerson,contactTel,contactAddress,provinceId,provinceName,cityId,cityName,countyId,countyName,account,role,actived,createAt,remark,lastUpdateTime,bindReceiverOrg)" +
                "values(?fullName,?contactPerson,?contactTel,?contactAddress,?provinceId,?provinceName,?cityId,?cityName,?countyId,?countyName,?account,?role,?actived,?createAt,?remark,?lastUpdateTime,?bindReceiverOrg)";
            }
            else
            {
                sql = "update customer set fullName=?fullName,contactPerson=?contactPerson,contactTel=?contactTel,contactAddress=?contactAddress,provinceId=?provinceId,provinceName=?provinceName,cityId=?cityId,cityName=?cityName,countyId=?countyId,countyName=?countyName,account=?account,actived=?actived,remark=?remark,lastUpdateTime=?lastUpdateTime,bindReceiverOrg=?bindReceiverOrg" +
                       " where id =?id";
            }
            MySqlParameter[] para = new MySqlParameter[18];
            para[0] = new MySqlParameter("fullName", customer.FullName);
            para[1] = new MySqlParameter("contactPerson", customer.ContactPerson);
            para[2] = new MySqlParameter("contactTel", customer.ContactTel);
            para[3] = new MySqlParameter("contactAddress", customer.ContactAddress);
            para[4] = new MySqlParameter("provinceId", customer.ProvinceId);
            para[5] = new MySqlParameter("provinceName", customer.ProvinceName);
            para[6] = new MySqlParameter("cityId", customer.CityId);
            para[7] = new MySqlParameter("cityName", customer.CityName);
            para[8] = new MySqlParameter("account", customer.Account);
            para[9] = new MySqlParameter("role", customer.Role);
            para[10] = new MySqlParameter("actived", customer.Actived);
            para[11] = new MySqlParameter("createAt", customer.CreateAt);
            para[12] = new MySqlParameter("remark", customer.Remark);
            para[13] = new MySqlParameter("id", customer.Id);
            para[14] = new MySqlParameter("countyId", customer.CountyId);
            para[15] = new MySqlParameter("countyName", customer.CountyName);
            para[16] = new MySqlParameter("lastUpdateTime", DateTime.Now);
            para[17] = new MySqlParameter("bindReceiverOrg",bindReceiverOrg);
            int result = 0;
            //if (customer.Id == 0)
            //    result = _SqlHelp.ExecuteNonQuery(sql, para);
            //else
                result = _SqlHelp.ExecuteNonQuery(sql, para);
            if (result != 1)
                throw new Exception("操作失败!");
            return true;
        }

        /// <summary>
        /// 新增和修改客户信息更新时间
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public static Model_Customer GetCustomerUpdateTime(Model_Customer customer)
        {
            string sql = "";
            if (customer.Id == 0)
            {
                Model_Customer user = GetAccount(customer);
                if (user != null)
                    throw new Exception("客户账号已存在，必须唯一");
            }
            else
            {
                //查询登陆账号是否存在
                sql = "select * from customer where id=?id";
                MySqlParameter[] pa = new MySqlParameter[1];
                pa[0] = new MySqlParameter("id", customer.Id);
                Model_Customer user = _SqlHelp.ExecuteObject<Model_Customer>(sql, pa);
                if (user.Account != customer.Account)
                {
                    Model_Customer usesr = GetAccount(customer);
                    if (usesr != null)
                        throw new Exception("客户账号已存在，必须唯一");
                }
            }
            if (customer.Id == 0)
            {
                sql = "insert into customer(fullName,contactPerson,contactTel,contactAddress,provinceId,provinceName,cityId,cityName,countyId,countyName,account,role,actived,createAt,remark,lastUpdateTime)" +
                "values(?fullName,?contactPerson,?contactTel,?contactAddress,?provinceId,?provinceName,?cityId,?cityName,?countyId,?countyName,?account,?role,?actived,?createAt,?remark,?lastUpdateTime)";
            }
            else
            {
                sql = "update customer set fullName=?fullName,contactPerson=?contactPerson,contactTel=?contactTel,contactAddress=?contactAddress,provinceId=?provinceId,provinceName=?provinceName,cityId=?cityId,cityName=?cityName,countyId=?countyId,countyName=?countyName,account=?account,actived=?actived,remark=?remark,lastUpdateTime=?lastUpdateTime" +
                       " where id =?id";
            }
            MySqlParameter[] para = new MySqlParameter[17];
            para[0] = new MySqlParameter("fullName", customer.FullName);
            para[1] = new MySqlParameter("contactPerson", customer.ContactPerson);
            para[2] = new MySqlParameter("contactTel", customer.ContactTel);
            para[3] = new MySqlParameter("contactAddress", customer.ContactAddress);
            para[4] = new MySqlParameter("provinceId", customer.ProvinceId);
            para[5] = new MySqlParameter("provinceName", customer.ProvinceName);
            para[6] = new MySqlParameter("cityId", customer.CityId);
            para[7] = new MySqlParameter("cityName", customer.CityName);
            para[8] = new MySqlParameter("account", customer.Account);
            para[9] = new MySqlParameter("role", customer.Role);
            para[10] = new MySqlParameter("actived", customer.Actived);
            para[11] = new MySqlParameter("createAt", customer.CreateAt);
            para[12] = new MySqlParameter("remark", customer.Remark);
            para[13] = new MySqlParameter("id", customer.Id);
            para[14] = new MySqlParameter("countyId", customer.CountyId);
            para[15] = new MySqlParameter("countyName", customer.CountyName);
            para[16] = new MySqlParameter("lastUpdateTime",DateTime.Now);
            int result = 0;
            if (customer.Id == 0)
                result = _SqlHelp.ExecuteNonQuery(sql, para);
            else
                result = _SqlHelp.ExecuteNonQuery(sql, para);
            if (result != 1)
                throw new Exception("操作失败!");
            return customer;

        }
        #endregion

        #region 根据区域显示客户
        public static List<Model_Customer> CustomerDownstreamQZ(string zoneOption)
        {
            string sql = "select * from customer where role=3 ";
            if (!string.IsNullOrEmpty(zoneOption))
            {
                if(zoneOption!="1")
                    sql += string.Format(" and cityId={0} ;", zoneOption);
                
            }
           
            List<Model_Customer> customer = _SqlHelp.ExecuteObjects<Model_Customer>(sql);
            return customer;
        }
        #endregion
    }
}
