using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using C2LP.WebService.Model;
using C2LP.WebService.Utility;
using MySql.Data.MySqlClient;
using C2LP.WebService.Model.MyEnum;

namespace C2LP.WebService.BLL.ConsoleBLL
{
    public class CustomerUsersServer:BaseServer
    {
        public static List<Model_CustomerUser> CustomerUserQuery(int CustomerId,string pageIndexAndCount)
        {
            string sql = "";
            if (pageIndexAndCount != null)
            {
                //截取当前页数
                string page = pageIndexAndCount.Substring(0, pageIndexAndCount.LastIndexOf("."));
                //截取每页显示记录数
                string size = pageIndexAndCount.Substring(pageIndexAndCount.LastIndexOf(".") + 1, pageIndexAndCount.Length - (pageIndexAndCount.LastIndexOf(".") + 1));
                sql = "select * from customer_users where customerId=?customerId  limit " + ((Convert.ToInt32(page) - 1) * Convert.ToInt32(size)) + "," + size + ";";
            }
            else
                sql = "select * from customer_users where customerId=?customerId ;";
            MySqlParameter[] para = new MySqlParameter[1];
            para[0] =new  MySqlParameter("customerId", CustomerId);
            List<Model_CustomerUser> custuser = _SqlHelp.ExecuteObjects<Model_CustomerUser>(sql,para);
            
            return custuser;
        }
        public static Model_CustomerUser ChangePassWord(Model_CustomerUser customerUser)
        {
            string sql = "";
            if (customerUser.Id == 0)
            {
                //查询用户是否存在
                Model_CustomerUser user = GetUserName(customerUser);
                if (user != null)
                    throw new Exception("用户已存在");
            }
            if (customerUser.Id != 0)
            {
                //查询管理员账号是否存在一个
                Model_Customer custmer = CustomerServer.GetQueryCustomer(customerUser.CustomerId);
                if (custmer.Role == Enum_Role.Administrator && customerUser.Actived == Enum_Active.Disable)
                {
                    List<Model_CustomerUser> counts = GetActived(customerUser);
                    if (counts.Count <= 1)
                        throw new Exception("不能全部停用，至少要存在一个启用账号");
                }
                Model_CustomerUser use = GetPassword(customerUser);
                //if (use.CustomerId== customerUser.CustomerId && use.DisplayName== customerUser.DisplayName && use.UserName== customerUser.UserName && use.Password== customerUser.Password && use.CreateAt== customerUser.CreateAt && use.Actived== customerUser.Actived)
                //    throw new Exception("密码相同，请修改密码");
                if (use.UserName != customerUser.UserName)
                {
                    Model_CustomerUser user = GetUserName(customerUser);
                    if (user != null)
                        throw new Exception("用户已存在");
                }
                if (use.Password.ToUpper() == MyTool.UserMd5(customerUser.Password).ToUpper())
                    throw new Exception("用户密码已存在，请修改密码");
            }
            if (customerUser.Id == 0)
                sql = "insert into customer_users(customerId,username,password,displayName,createAt,actived) values(?customerId,?username,?password,?displayName,?createAt,?actived)";
            else
                sql = "update customer_users set displayName=?displayName,username=?username,password=?password,actived=?actived where id=?id";
            MySqlParameter[] para = new MySqlParameter[7];
            para[0] = new MySqlParameter("customerId", customerUser.CustomerId);
            para[1] = new MySqlParameter("username", customerUser.UserName);
            if (customerUser.Id != 0)
            {
                //判断是否修改过密码
                Model_CustomerUser use = GetPassword(customerUser);
                if (use.Password.ToUpper() == customerUser.Password.ToUpper())
                    para[2] = new MySqlParameter("password", customerUser.Password.ToUpper());
                else
                    para[2] = new MySqlParameter("password", MyTool.UserMd5(customerUser.Password).ToUpper());
            }
            else
                para[2] = new MySqlParameter("password", MyTool.UserMd5(customerUser.Password).ToUpper());
            para[3] = new MySqlParameter("displayName", customerUser.DisplayName);
            para[4] = new MySqlParameter("createAt", customerUser.CreateAt);
            para[5] = new MySqlParameter("actived", customerUser.Actived);
            para[6] = new MySqlParameter("id", customerUser.Id);


            int result = 0;
            if (customerUser.Id == 0)
                result = _SqlHelp.ExecuteNonQuery(sql, para);
            else
                result = _SqlHelp.ExecuteNonQuery(sql, para);
            if (result != 1)
                throw new Exception("操作失败");

            return customerUser;
        }
        /// <summary>
        /// //查询用户是否存在
        /// </summary>
        /// <param name="customerUser"></param>
        /// <returns></returns>
        public static Model_CustomerUser GetUserName(Model_CustomerUser customerUser)
        {
            string sql = "select * from customer_users where customerId=?customerId and username=?username ;";
            MySqlParameter[] pa = new MySqlParameter[2];
            pa[0] = new MySqlParameter("customerId", customerUser.CustomerId);
            pa[1] = new MySqlParameter("username", customerUser.UserName);
            Model_CustomerUser user = _SqlHelp.ExecuteObject<Model_CustomerUser>(sql, pa);
            return user;
        }
        /// <summary>
        /// 查询管理员账号是否存在一个启用
        /// </summary>
        /// <param name="customerUser"></param>
        /// <returns></returns>
        public static List<Model_CustomerUser> GetActived(Model_CustomerUser customerUser)
        {
            string sql = "select * from customer_users where customerId=?customerId and actived=?actived";
            MySqlParameter[] count = new MySqlParameter[2];
            count[0] = new MySqlParameter("customerId", customerUser.CustomerId);
            count[1] = new MySqlParameter("actived", Enum_Active.Enabled);
            List<Model_CustomerUser> counts = _SqlHelp.ExecuteObjects<Model_CustomerUser>(sql, count);
            return counts;
        }
        /// <summary>
        /// 查询密码
        /// </summary>
        /// <param name="customerUser"></param>
        /// <returns></returns>
        public static Model_CustomerUser GetPassword(Model_CustomerUser customerUser)
        {
            string sql = "select * from customer_users where id=?id ;";
            MySqlParameter[] paid = new MySqlParameter[1];
            paid[0] = new MySqlParameter("id", customerUser.Id);
            Model_CustomerUser use = _SqlHelp.ExecuteObject<Model_CustomerUser>(sql, paid);
            return use;
        }
    }
}
