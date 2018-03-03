using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace C2LP.ColdStorageDataHubClient.DBHelper.Model
{
    /// <summary>
    /// 数据库实体 tbcchddata_projectId_netId[仓库历史数据]、tbccrealdata_ref[仓库实时数据]
    /// </summary>
    public class TbccRefAiData
    {
        public int id { get; set; }
        public DateTime hDate { get; set; }
        public float AI1 { get; set; }
        public float AI2 { get; set; }
        public float AI3 { get; set; }
        public float AI4 { get; set; }
        public float AI5 { get; set; }
        public float AI6 { get; set; }
        public float AI7 { get; set; }
        public float AI8 { get; set; }
        public float AI9 { get; set; }
        public float AI10 { get; set; }
        public float AI11 { get; set; }
        public float AI12 { get; set; }
        public float AI13 { get; set; }
        public float AI14 { get; set; }
        public float AI15 { get; set; }
        public float AI16 { get; set; }
        public float AI17 { get; set; }
        public float AI18 { get; set; }
        public float AI19 { get; set; }
        public float AI20 { get; set; }
        public float AI21 { get; set; }
        public float AI22 { get; set; }
        public float AI23 { get; set; }
        public float AI24 { get; set; }
        public float AI25 { get; set; }
        public float AI26 { get; set; }
        public float AI27 { get; set; }
        public float AI28 { get; set; }
        public float AI29 { get; set; }
        public float AI30 { get; set; }
        public float AI31 { get; set; }
        public float AI32 { get; set; }

        public int Ref1_RefAlarmState { get; set; }
        public int Ref2_RefAlarmState { get; set; }
        public int Ref3_RefAlarmState { get; set; }
        public int Ref4_RefAlarmState { get; set; }
        public int Ref5_RefAlarmState { get; set; }
        public int Ref6_RefAlarmState { get; set; }
        public int Ref7_RefAlarmState { get; set; }
        public int Ref8_RefAlarmState { get; set; }
        public int Ref9_RefAlarmState { get; set; }
        public int Ref10_RefAlarmState { get; set; }
        public int Ref11_RefAlarmState { get; set; }
        public int Ref12_RefAlarmState { get; set; }
        public int Ref13_RefAlarmState { get; set; }
        public int Ref14_RefAlarmState { get; set; }
        public int Ref15_RefAlarmState { get; set; }
        public int Ref16_RefAlarmState { get; set; }

        ///// <summary>
        ///// 上报标记：0表示未上报,1表示已上报
        ///// </summary>
        //public int fdap4zgswUploadState { get; set; }


        #region 实时数据特有列
        public string ProjectId { get; set; }

        public int NetId { get; set; }

        public DateTime UpdateTime { get; set; }
#endregion
    }
}
