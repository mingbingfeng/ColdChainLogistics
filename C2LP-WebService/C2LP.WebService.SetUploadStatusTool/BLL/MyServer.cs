using C2LP.WebService.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace C2LP.WebService.SetUploadStatusTool.BLL
{
    public class MyServer : BaseServer
    {
        public static DataTable GetAllNullProgress() {
            string sql = "select * from uploaddataprogress where endnodetime is null";
            return _SqlHelp.ExecuteDataTable(sql);
        }

        public static DataTable GetAllNullProgress_Arrived()
        {
            string sql = "select * from uploaddataprogress where endnodetime is null and nodeparentstorageid<>0 ";
            return _SqlHelp.ExecuteDataTable(sql);
        }

        public static bool UpdateProgress(string relationId,string storageId,string nodeTime) {
            string sql = string.Format("update uploaddataprogress p inner join (select nodetime from uploaddataprogress where relationId='{0}' and nodeparentstorageid = '{1}' and nodetime >'{2}' order by nodetime limit 1 ) a set endnodetime = a.nodetime where relationId = '{0}' and storageId='{1}' and p.nodetime='{2}' and endnodetime is null",relationId,storageId,nodeTime);
            return _SqlHelp.ExecuteNonQuery(sql) ==1;
        }

        public static bool UpdateArrivedProgress(string storageId,string relationId,string id) {
            string sql = string.Format("update uploaddataprogress p inner join( select operateat from waybill_node where parentstorageid = {0} and scannumber = '{1}' and arrived= 2 and storageid = {0}) a set endnodetime = a.operateat where id = {2}",storageId,relationId,id);
            return _SqlHelp.ExecuteNonQuery(sql) == 1;
        }
    }
}
