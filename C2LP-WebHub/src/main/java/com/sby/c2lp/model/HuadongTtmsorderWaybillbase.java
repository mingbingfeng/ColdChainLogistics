package com.sby.c2lp.model;

import java.sql.Timestamp;

/**
 * Created by wanghe on 2017/3/3.
 */
public class HuadongTtmsorderWaybillbase {
    private String relationId;             //托运订单编号
    private String number;                 //运单编号。系统唯一 引用  waybill_base
    private Long currentUploadNodeId;    //Y	-1	最后上报的节点ID。-1：未上传；NodeID：当前已上报截止的节点ID；0：上报完成
    private Long currentUploadDataNodeId;	//Y	-1	最后上报节点数据的节点ID。-1：未上传；NodeID：当前已上节点数据报截止的节点ID；0：上报完成
    private Timestamp currentUploadDataTime;    //最后上报节点中数据的时间进度标记

    public String getRelationId() {
        return relationId;
    }

    public void setRelationId(String relationId) {
        this.relationId = relationId;
    }

    public Timestamp getCurrentUploadDataTime() {
        return currentUploadDataTime;
    }

    public void setCurrentUploadDataTime(Timestamp currentUploadDataTime) {
        this.currentUploadDataTime = currentUploadDataTime;
    }

    public Long getCurrentUploadDataNodeId() {
        return currentUploadDataNodeId;
    }

    public void setCurrentUploadDataNodeId(Long currentUploadDataNodeId) {
        this.currentUploadDataNodeId = currentUploadDataNodeId;
    }

    public String getNumber() {
        return number;
    }

    public void setNumber(String number) {
        this.number = number;
    }

    public Long getCurrentUploadNodeId() {
        return currentUploadNodeId;
    }

    public void setCurrentUploadNodeId(Long currentUploadNodeId) {
        this.currentUploadNodeId = currentUploadNodeId;
    }

    public HuadongTtmsorderWaybillbase() {
    }

    public HuadongTtmsorderWaybillbase(String relationId, String number, Long currentUploadNodeId, Long currentUploadDataNodeId, Timestamp currentUploadDataTime) {
        this.relationId = relationId;
        this.number = number;
        this.currentUploadNodeId = currentUploadNodeId;
        this.currentUploadDataNodeId = currentUploadDataNodeId;
        this.currentUploadDataTime = currentUploadDataTime;
    }
}
