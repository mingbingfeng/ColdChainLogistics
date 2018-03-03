package com.sby.c2lp.model;

import java.sql.Timestamp;

/**
 * Created by zhaoyou on 9/5/16.
 */
public class UserInfo {
    // 用户包含对应的Customer实体,当做一个属性.
    private Customer customer;
    private Integer id;
    private Integer customerId;
    private String username;
    private String password;
    private String displayName;
    private Timestamp createAt;
    private Integer actived;

    public Customer getCustomer() {
        return customer;
    }

    public void setCustomer(Customer customer) {
        this.customer = customer;
    }


    public Integer getId() {
        return id;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public Integer getCustomerId() {
        return customerId;
    }

    public void setCustomerId(Integer customerId) {
        this.customerId = customerId;
    }

    public String getUsername() {
        return username;
    }

    public void setUsername(String username) {
        this.username = username;
    }

    public String getPassword() {
        return password;
    }

    public void setPassword(String password) {
        this.password = password;
    }

    public String getDisplayName() {
        return displayName;
    }

    public void setDisplayName(String displayName) {
        this.displayName = displayName;
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

    public UserInfo() {
    }

    public UserInfo(Customer customer, Integer id, Integer customerId, String username, String password, String displayName, Timestamp createAt, Integer actived) {
        this.customer = customer;
        this.id = id;
        this.customerId = customerId;
        this.username = username;
        this.password = password;
        this.displayName = displayName;
        this.createAt = createAt;
        this.actived = actived;
    }

    @Override
    public String toString() {
        return "UserInfo{" +
                "customer=" + customer +
                ", id=" + id +
                ", customerId=" + customerId +
                ", username='" + username + '\'' +
                ", password='" + password + '\'' +
                ", displayName='" + displayName + '\'' +
                ", createAt=" + createAt +
                ", actived=" + actived +
                '}';
    }
}
