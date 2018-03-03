package com.sby.c2lp.model;

import java.sql.Timestamp;

/**
 * Created by wanghe on 2016/8/5.
 */
public class Aiinfo {
    private Long id;
    private Integer pointId;
    private Double data;
    private Integer isAlarm;
    private Timestamp dataTime;
    private Integer storageId;
    private String pointName;
    private Integer pointType;
    private Integer actived;

    public Long getId() {
        return id;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public Integer getPointId() {
        return pointId;
    }

    public void setPointId(Integer pointId) {
        this.pointId = pointId;
    }

    public Double getData() {
        return data;
    }

    public void setData(Double data) {
        this.data = data;
    }

    public Integer getIsAlarm() {
        return isAlarm;
    }

    public void setIsAlarm(Integer isAlarm) {
        this.isAlarm = isAlarm;
    }

    public Timestamp getDataTime() {
        return dataTime;
    }

    public void setDataTime(Timestamp dataTime) {
        this.dataTime = dataTime;
    }

    public Integer getStorageId() {
        return storageId;
    }

    public void setStorageId(Integer storageId) {
        this.storageId = storageId;
    }

    public String getPointName() {
        return pointName;
    }

    public void setPointName(String pointName) {
        this.pointName = pointName;
    }

    public Integer getPointType() {
        return pointType;
    }

    public void setPointType(Integer pointType) {
        this.pointType = pointType;
    }

    public Integer getActived() {
        return actived;
    }

    public void setActived(Integer actived) {
        this.actived = actived;
    }

    public Aiinfo() {
    }

    public Aiinfo(Long id, Integer pointId, Double data, Integer isAlarm, Timestamp dataTime, Integer storageId, String pointName, Integer pointType, Integer actived) {
        this.id = id;
        this.pointId = pointId;
        this.data = data;
        this.isAlarm = isAlarm;
        this.dataTime = dataTime;
        this.storageId = storageId;
        this.pointName = pointName;
        this.pointType = pointType;
        this.actived = actived;
    }

    public Aiinfo(Integer pointId, Integer storageId, String pointName, Integer pointType, Integer actived) {
        this.pointId = pointId;
        this.storageId = storageId;
        this.pointName = pointName;
        this.pointType = pointType;
        this.actived = actived;
    }

    @Override
    public String toString() {
        return "AiinfoHistory{" +
                "id=" + id +
                ", pointId=" + pointId +
                ", data=" + data +
                ", isAlarm=" + isAlarm +
                ", dataTime=" + dataTime +
                ", storageId=" + storageId +
                ", pointName='" + pointName + '\'' +
                ", pointType=" + pointType +
                ", actived=" + actived +
                '}';
    }
}
