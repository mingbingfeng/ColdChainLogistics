package com.sby.c2lp.model;

import java.sql.Timestamp;

public class VisitRecord {
    private Integer id;
    private Integer customerId;
    private String fullName;
    private Timestamp visitTime;
    private String ip;
    private String userAgent;
    private Integer wechat;
    private Integer pc;

    public VisitRecord() {
    }

    public VisitRecord(Integer id, Integer customerId, String fullName, Timestamp visitTime, String ip, String userAgent) {
        this.id = id;
        this.customerId = customerId;
        this.fullName = fullName;
        this.visitTime = visitTime;
        this.ip = ip;
        this.userAgent = userAgent;
    }

    public Integer getWechat() {
        return wechat;
    }

    public void setWechat(Integer count) {
        this.wechat = count;
    }

    public Integer getPc() {
        return pc;
    }

    public void setPc(Integer count) {
        this.pc = count;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public void setCustomerId(Integer customerId) {
        this.customerId = customerId;
    }

    public void setFullName(String fullName) {
        this.fullName = fullName;
    }

    public void setVisitTime(Timestamp visitTime) {
        this.visitTime = visitTime;
    }

    public void setIp(String ip) {
        this.ip = ip;
    }

    public void setUserAgent(String userAgent) {
        this.userAgent = userAgent;
    }

    public Integer getId() {
        return id;
    }

    public Integer getCustomerId() {
        return customerId;
    }

    public String getFullName() {
        return fullName;
    }

    public Timestamp getVisitTime() {
        return visitTime;
    }

    public String getIp() {
        return ip;
    }

    public String getUserAgent() {
        return userAgent;
    }
}
