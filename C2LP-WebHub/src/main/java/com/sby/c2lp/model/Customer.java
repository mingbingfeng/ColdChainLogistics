package com.sby.c2lp.model;

import java.sql.Timestamp;

/**
 * Created by zhaoyou on 9/5/16.
 */
public class Customer {
    private Integer id;
    private String fullName;
    private String contactPerson;
    private String contactTel;
    private String contactAddress;
    private Integer provinceId;
    private String provinceName;
    private Integer cityId;
    private String cityName;
    private String account;
    private Integer role;
    private Integer actived;
    private Timestamp createAt;
    private String remark;

    public Integer getId() {
        return id;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public String getFullName() {
        return fullName;
    }

    public void setFullName(String fullName) {
        this.fullName = fullName;
    }

    public String getContactPerson() {
        return contactPerson;
    }

    public void setContactPerson(String contactPerson) {
        this.contactPerson = contactPerson;
    }

    public String getContactTel() {
        return contactTel;
    }

    public void setContactTel(String contactTel) {
        this.contactTel = contactTel;
    }

    public String getContactAddress() {
        return contactAddress;
    }

    public void setContactAddress(String contactAddress) {
        this.contactAddress = contactAddress;
    }

    public Integer getProvinceId() {
        return provinceId;
    }

    public void setProvinceId(Integer provinceId) {
        this.provinceId = provinceId;
    }

    public String getProvinceName() {
        return provinceName;
    }

    public void setProvinceName(String provinceName) {
        this.provinceName = provinceName;
    }

    public Integer getCityId() {
        return cityId;
    }

    public void setCityId(Integer cityId) {
        this.cityId = cityId;
    }

    public String getCityName() {
        return cityName;
    }

    public void setCityName(String cityName) {
        this.cityName = cityName;
    }

    public String getAccount() {
        return account;
    }

    public void setAccount(String account) {
        this.account = account;
    }

    public Integer getRole() {
        return role;
    }

    public void setRole(Integer role) {
        this.role = role;
    }

    public Integer getActived() {
        return actived;
    }

    public void setActived(Integer actived) {
        this.actived = actived;
    }

    public Timestamp getCreateAt() {
        return createAt;
    }

    public void setCreateAt(Timestamp createAt) {
        this.createAt = createAt;
    }

    public String getRemark() {
        return remark;
    }

    public void setRemark(String remark) {
        this.remark = remark;
    }

    public Customer() {
    }

    public Customer(Integer id, String fullName, String contactPerson, String contactTel, String contactAddress, Integer provinceId, String provinceName, Integer cityId, String cityName, String account, Integer role, Integer actived, Timestamp createAt, String remark) {
        this.id = id;
        this.fullName = fullName;
        this.contactPerson = contactPerson;
        this.contactTel = contactTel;
        this.contactAddress = contactAddress;
        this.provinceId = provinceId;
        this.provinceName = provinceName;
        this.cityId = cityId;
        this.cityName = cityName;
        this.account = account;
        this.role = role;
        this.actived = actived;
        this.createAt = createAt;
        this.remark = remark;
    }

    @Override
    public String toString() {
        return "Customer{" +
                "id=" + id +
                ", fullName='" + fullName + '\'' +
                ", contactPerson='" + contactPerson + '\'' +
                ", contactTel='" + contactTel + '\'' +
                ", contactAddress='" + contactAddress + '\'' +
                ", provinceId=" + provinceId +
                ", provinceName='" + provinceName + '\'' +
                ", cityId=" + cityId +
                ", cityName='" + cityName + '\'' +
                ", account='" + account + '\'' +
                ", role=" + role +
                ", actived=" + actived +
                ", createAt=" + createAt +
                ", remark='" + remark + '\'' +
                '}';
    }
}
