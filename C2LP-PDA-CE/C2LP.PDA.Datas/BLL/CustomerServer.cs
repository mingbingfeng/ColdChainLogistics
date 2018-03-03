using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using C2LP.PDA.Datas.Model;
using System.Data;
using System.Collections;

namespace C2LP.PDA.Datas.BLL
{
    public class CustomerServer : BaseServer
    {
        /// <summary>
        /// 更新客户信息
        /// </summary
        /// 
        /// <param name="valueStr">sql value数据字符</param>
        /// <returns></returns>
        public static int UpdateCustomers(string valueStr)
        {
            string sql = string.Empty;
            try
            {
                if (!string.IsNullOrEmpty(valueStr))
                {
                    sql += "insert into c2lp_customer (Id,fullName,contactPerson,contactTel,contactAddress,provinceId,cityId,role,countyId) values " + valueStr;
                    return _SqlHelp.ExecuteNonQuery(sql, System.Data.CommandType.Text);
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static int ClearCustomers()
        {
            string sql = "delete from c2lp_customer;";
            try
            {
                return _SqlHelp.ExecuteNonQuery(sql, CommandType.Text);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// 检查客户表是否存在CountyId字段，没有就添加
        /// </summary>
        public static void AddCustomersCountyId()
        {
            try
            {
                string sql = "select Count(*) from sqlite_master  where tbl_name='c2lp_customer' and sql like '%countyId%';";
                object obj = _SqlHelp.ExecuteScalar(sql, CommandType.Text);
                if (Convert.ToInt32(obj) == 0)
                {
                    sql = "alter table c2lp_customer add countyId int null";
                    _SqlHelp.ExecuteNonQuery(sql, CommandType.Text);
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// 获取所有客户
        /// </summary>
        /// <param name="role">2：发货单位；3：收货单位：其他：全部单位</param>
        /// <returns></returns>
        public static List<Customer> GetAllCustomer(string role)
        {
            List<Customer> list = new List<Customer>();
            try
            {
                string sql = "select Id,fullName,contactPerson,contactTel,contactAddress,provinceId,cityId,role,CountyId from c2lp_customer ";
                if (role != null && (role == "2" || role == "3"))
                    sql += "where role=" + role;

                DataSet ds = _SqlHelp.ExecuteDataSet(sql, System.Data.CommandType.Text);
                if (ds != null && ds.Tables.Count > 0)
                {
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        Customer c = new Customer();
                        c.Id = Convert.ToInt32(row["Id"]);
                        c.ContactAddress = row["ContactAddress"].ToString();
                        c.ContactPerson = row["ContactPerson"].ToString();
                        c.ContactTel = row["ContactTel"].ToString();
                        c.FullName = row["FullName"].ToString();
                        c.ProvinceId = Convert.ToInt32(row["ProvinceId"]);
                        c.CityId = Convert.ToInt32(row["CityId"]);
                        c.Role = Convert.ToInt32(row["Role"]);
                        c.CountyId = Convert.ToInt32(row["CountyId"]);
                        list.Add(c);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return list;
        }

        public static void GetCustomerAndRegion(ref Hashtable ht_Sender, ref Hashtable ht_Receiv, ref Hashtable ht_Region)
        {
            //Hashtable ht1 = new Hashtable();
            //Hashtable ht2 = new Hashtable();
            //Hashtable ht3 = new Hashtable();

            StringBuilder sb = new StringBuilder();
            StringBuilder sb1 = new StringBuilder();
            try
            {
                ht_Sender.Clear();
                ht_Receiv.Clear();
                ht_Region.Clear();

                List<MyRegion> rList = RegionServer.GetAllRegion();
                List<Customer> scList = GetAllCustomer("2");
                List<Customer> rcList = GetAllCustomer("3");

                //发货单位信息Hashtable ht1
                Hashtable ht_1_1 = new Hashtable();
                var scDic = scList.ToLookup(l => l.CountyId);
                foreach (var sc in scDic)
                {
                    if (sc.Key != 0)
                        ht_1_1.Add(rList.Find(l => l.Id == sc.Key), OrderByFullName(sc.ToArray()));
                    else
                    {
                        var scDicNotBind = sc.ToLookup(l => l.CityId);
                        foreach (var scNotBind in scDicNotBind)
                        {
                            ht_1_1.Add(rList.Find(l => l.Id == scNotBind.Key), OrderByFullName(scNotBind.ToArray()));
                        }
                    }
                }

                //收货单位信息Hashtable ht2
                Hashtable ht_2_1 = new Hashtable();
                var rcDic = rcList.ToLookup(l => l.CountyId);
                foreach (var rc in rcDic)
                {
                    if (rc.Key != 0)
                        ht_2_1.Add(rList.Find(l => l.Id == rc.Key), OrderByFullName(rc.ToArray()));
                    else
                    {
                        var rcDicNotBind = rc.ToLookup(l => l.CityId);
                        foreach (var rcNotBind in rcDicNotBind)
                        {
                            ht_2_1.Add(rList.Find(l => l.Id == rcNotBind.Key),OrderByFullName( rcNotBind.ToArray()));
                        }
                    }
                }

                //省市县级联Hashtable ht3
                foreach (var r1 in rList.Where(l => l.ParentId == 1))
                {
                    Hashtable ht_3_2 = new Hashtable();
                    Hashtable ht_2_2 = new Hashtable();
                    Hashtable ht_1_2 = new Hashtable();
                    IEnumerable r2List = rList.Where(l => l.ParentId == r1.Id);
                    foreach (MyRegion r2 in r2List)
                    {
                        Hashtable ht_3_1 = new Hashtable();

                        if (ht_1_1.ContainsKey(r2))
                        {
                            if (!ht_1_2.ContainsKey(r2))
                                ht_1_2.Add(r2, new Hashtable());
                            (ht_1_2[r2] as Hashtable).Add(new MyRegion() { Id = 0, Name = "未绑定单位", ParentId = r2.Id }, ht_1_1[r2]);
                        }

                        if (ht_2_1.ContainsKey(r2))
                        {
                            if (!ht_2_2.ContainsKey(r2))
                                ht_2_2.Add(r2, new Hashtable());
                            (ht_2_2[r2] as Hashtable).Add(new MyRegion() { Id = 0, Name = "未绑定单位", ParentId = r2.Id }, ht_2_1[r2]);
                        }

                        IEnumerable r3List = rList.Where(l => l.ParentId == r2.Id);
                        foreach (var r3 in r3List)
                        {
                            if (ht_1_1.ContainsKey(r3))
                            {
                                if (!ht_1_2.ContainsKey(r2))
                                    ht_1_2.Add(r2, new Hashtable());
                                (ht_1_2[r2] as Hashtable).Add(r3, ht_1_1[r3]);
                            }
                            if (ht_2_1.ContainsKey(r3))
                            {
                                if (!ht_2_2.ContainsKey(r2))
                                    ht_2_2.Add(r2, new Hashtable());
                                (ht_2_2[r2] as Hashtable).Add(r3, ht_2_1[r3]);
                            }
                            ht_3_1.Add(r3, new List<Customer>());
                        }
                        ht_3_2.Add(r2, ht_3_1);
                    }
                    if (ht_1_2.Count > 0)
                        ht_Sender.Add(r1, ht_1_2);
                    if (ht_2_2.Count > 0)
                        ht_Receiv.Add(r1, ht_2_2);
                    ht_Region.Add(r1, ht_3_2);
                }

                //foreach (MyRegion r in ht_Receiv.Keys)
                //{
                //    sb.AppendLine(r.Name);
                //    foreach (MyRegion r2 in (ht_Receiv[r] as Hashtable).Keys)
                //    {
                //        sb.AppendLine("    " + r2.Name);
                //        foreach (MyRegion r3 in ((ht_Receiv[r] as Hashtable)[r2] as Hashtable).Keys)
                //        {
                //            sb.AppendLine("        " + r3.Name);
                //            foreach (Customer c in ((ht_Receiv[r] as Hashtable)[r2] as Hashtable)[r3] as Customer[])
                //            {
                //                sb.AppendLine("            " + c.FullName);
                //            }
                //        }
                //    }
                //}

                //foreach (MyRegion r in ht_Sender.Keys)
                //{
                //    sb1.AppendLine(r.Name);
                //    foreach (MyRegion r2 in (ht_Sender[r] as Hashtable).Keys)
                //    {
                //        sb1.AppendLine("    " + r2.Name);
                //        foreach (MyRegion r3 in ((ht_Sender[r] as Hashtable)[r2] as Hashtable).Keys)
                //        {
                //            sb1.AppendLine("        " + r3.Name);
                //            foreach (Customer c in ((ht_Sender[r] as Hashtable)[r2] as Hashtable)[r3] as Customer[])
                //            {
                //                sb1.AppendLine("            " + c.FullName);
                //            }
                //        }
                //    }
                //}

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static List<Customer> OrderByFullName(Customer[] customers)
        {
            List<Customer> sortList = new List<Customer>();
            try
            {
                Dictionary<Customer, long> lName = new Dictionary<Customer, long>();
                List<long> lCnChar = new List<long>();
                long iCnChar;
                foreach (Customer c in customers)
                {
                    byte[] ZW = System.Text.Encoding.Default.GetBytes(c.FullName);
                    // get the array of byte from the single char
                    int i1 = (short)(ZW[0]);
                    int i2 = (short)(ZW[1]);
                    iCnChar = i1 * 256 + i2+(short)c.Id;
                    int count = 0;
                    while (lCnChar.Contains(iCnChar))
                    {
                        if (count == 100)
                            return customers.ToList();
                        iCnChar++;
                        count++;
                    }
                    lName.Add(c, iCnChar);
                    lCnChar.Add(iCnChar);
                }
                lCnChar.Sort();
                for (int i = 0; i < lCnChar.Count; i++)
                {
                    var keys = lName.Where(q => q.Value == lCnChar[i]).Select(q => q.Key);
                    sortList.Add(keys.FirstOrDefault());
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return sortList;
        }
    }
}
