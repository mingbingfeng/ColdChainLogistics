package com.sby.c2lp.model;

import java.sql.Timestamp;

/**
 * Created by wanghe on 2016/8/9.
 */
public class ColdStorage {
    private Integer id;
    private String storageName;
    private Integer storageType;
    private String driver;
    private String driverTel;
    private String remark;
    private Timestamp createAt;
    private Integer actived;
    private Double latitude;
    private Double longitude;

    public Double getLatitude() {
        return latitude;
    }

    public Double getLongitude() {
        return longitude;
    }

    public void setLatitude(Double latitude) {
        this.latitude = latitude;
    }

    public void setLongitude(Double longitude) {
        this.longitude = longitude;
    }

    public Integer getId() {
        return id;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public String getStorageName() {
        return storageName;
    }

    public void setStorageName(String storageName) {
        this.storageName = storageName;
    }

    public Integer getStorageType() {
        return storageType;
    }

    public void setStorageType(Integer storageType) {
        this.storageType = storageType;
    }

    public String getDriver() {
        return driver;
    }

    public void setDriver(String driver) {
        this.driver = driver;
    }

    public String getDriverTel() {
        return driverTel;
    }

    public void setDriverTel(String driverTel) {
        this.driverTel = driverTel;
    }

    public String getRemark() {
        return remark;
    }

    public void setRemark(String remark) {
        this.remark = remark;
    }

    public Timestamp getCreateAt() {
        return createAt;
    }

    public void setCreateAt(Timestamp createAt) {
        this.createAt = createAt;
    }

    public Integer getActived() {
        return actived;
    }

    public void setActived(Integer actived) {
        this.actived = actived;
    }

    public ColdStorage() {
    }

    public ColdStorage(Integer id, String storageName, Integer storageType, String driver, String driverTel, String remark, Timestamp createAt, Integer actived) {
        this.id = id;
        this.storageName = storageName;
        this.storageType = storageType;
        this.driver = driver;
        this.driverTel = driverTel;
        this.remark = remark;
        this.createAt = createAt;
        this.actived = actived;
    }
}
