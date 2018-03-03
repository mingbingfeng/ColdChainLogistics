package com.sby.c2lp.model;

import java.sql.Timestamp;

/**
 * Created by wanghe on 2016/8/4.
 */
public class WaybillBase {
    private Integer id;
    private String number;
    private Integer senderId;
    private String  senderOrg;
    private String senderPerson;
    private String senderTel;
    private String senderAddress;
    private Integer receiverId;
    private String receiverOrg;
    private String receiverPerson;
    private String receiverTel;
    private String receiverAddress;
    private Integer billingCount;
    private Integer stage;
    private Timestamp beginAt;
    private Timestamp signinAt;
    private Timestamp picPostbackAt;
    private Long baseId;
    private String picName;
    private HuadongTtmsorderWaybillbase huadongTtmsorderWaybillbase;
    public Long getBaseId() {
        return baseId;
    }

    public void setBaseId(Long baseId) {
        this.baseId = baseId;
    }

    public String getPicName() {
        return picName;
    }

    public void setPicName(String picName) {
        this.picName = picName;
    }

    public Integer getId() {
        return id;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public String getNumber() {
        return number;
    }

    public void setNumber(String number) {
        this.number = number;
    }

    public Integer getSenderId() {
        return senderId;
    }

    public void setSenderId(Integer senderId) {
        this.senderId = senderId;
    }

    public String getSenderOrg() {
        return senderOrg;
    }

    public void setSenderOrg(String senderOrg) {
        this.senderOrg = senderOrg;
    }

    public String getSenderPerson() {
        return senderPerson;
    }

    public void setSenderPerson(String senderPerson) {
        this.senderPerson = senderPerson;
    }

    public String getSenderTel() {
        return senderTel;
    }

    public void setSenderTel(String senderTel) {
        this.senderTel = senderTel;
    }

    public String getSenderAddress() {
        return senderAddress;
    }

    public void setSenderAddress(String senderAddress) {
        this.senderAddress = senderAddress;
    }

    public Integer getReceiverId() {
        return receiverId;
    }

    public void setReceiverId(Integer receiverId) {
        this.receiverId = receiverId;
    }

    public String getReceiverOrg() {
        return receiverOrg;
    }

    public void setReceiverOrg(String receiverOrg) {
        this.receiverOrg = receiverOrg;
    }

    public String getReceiverPerson() {
        return receiverPerson;
    }

    public void setReceiverPerson(String receiverPerson) {
        this.receiverPerson = receiverPerson;
    }

    public String getReceiverTel() {
        return receiverTel;
    }

    public void setReceiverTel(String receiverTel) {
        this.receiverTel = receiverTel;
    }

    public String getReceiverAddress() {
        return receiverAddress;
    }

    public void setReceiverAddress(String receiverAddress) {
        this.receiverAddress = receiverAddress;
    }

    public Integer getBillingCount() {
        return billingCount;
    }

    public void setBillingCount(Integer billingCount) {
        this.billingCount = billingCount;
    }

    public Integer getStage() {
        return stage;
    }

    public void setStage(Integer stage) {
        this.stage = stage;
    }

    public Timestamp getBeginAt() {
        return beginAt;
    }

    public void setBeginAt(Timestamp beginAt) {
        this.beginAt = beginAt;
    }

    public Timestamp getSigninAt() {
        return signinAt;
    }

    public void setSigninAt(Timestamp signinAt) {
        this.signinAt = signinAt;
    }

    public Timestamp getPicPostbackAt() {
        return picPostbackAt;
    }

    public void setPicPostbackAt(Timestamp picPostbackAt) {
        this.picPostbackAt = picPostbackAt;
    }

    public HuadongTtmsorderWaybillbase getHuadongTtmsorderWaybillbase() {
        return huadongTtmsorderWaybillbase;
    }

    public void setHuadongTtmsorderWaybillbase(HuadongTtmsorderWaybillbase huadongTtmsorderWaybillbase) {
        this.huadongTtmsorderWaybillbase = huadongTtmsorderWaybillbase;
    }

    public WaybillBase() {
    }

    public WaybillBase(Integer id, String number, Integer senderId, String senderOrg, String senderPerson, String senderTel, String senderAddress, Integer receiverId, String receiverPerson, String receiverOrg, String receiverTel, String receiverAddress, Integer billingCount, Integer stage, Timestamp beginAt, Timestamp signinAt, Timestamp picPostbackAt) {
        this.id = id;
        this.number = number;
        this.senderId = senderId;
        this.senderOrg = senderOrg;
        this.senderPerson = senderPerson;
        this.senderTel = senderTel;
        this.senderAddress = senderAddress;
        this.receiverId = receiverId;
        this.receiverPerson = receiverPerson;
        this.receiverOrg = receiverOrg;
        this.receiverTel = receiverTel;
        this.receiverAddress = receiverAddress;
        this.billingCount = billingCount;
        this.stage = stage;
        this.beginAt = beginAt;
        this.signinAt = signinAt;
        this.picPostbackAt = picPostbackAt;
    }

    public WaybillBase(String picName, Long baseId, Timestamp picPostbackAt, Timestamp signinAt, Integer stage, Timestamp beginAt, Integer billingCount, String receiverAddress, String receiverTel, String receiverPerson, String receiverOrg, Integer receiverId, String senderAddress, String senderTel, String senderPerson, String senderOrg, Integer senderId, String number, Integer id) {
        this.picName = picName;
        this.baseId = baseId;
        this.picPostbackAt = picPostbackAt;
        this.signinAt = signinAt;
        this.stage = stage;
        this.beginAt = beginAt;
        this.billingCount = billingCount;
        this.receiverAddress = receiverAddress;
        this.receiverTel = receiverTel;
        this.receiverPerson = receiverPerson;
        this.receiverOrg = receiverOrg;
        this.receiverId = receiverId;
        this.senderAddress = senderAddress;
        this.senderTel = senderTel;
        this.senderPerson = senderPerson;
        this.senderOrg = senderOrg;
        this.senderId = senderId;
        this.number = number;
        this.id = id;
    }

    @Override
    public String toString() {
        return "WaybillBase{" +
                "id=" + id +
                ", number='" + number + '\'' +
                ", senderId=" + senderId +
                ", senderOrg='" + senderOrg + '\'' +
                ", senderPerson='" + senderPerson + '\'' +
                ", senderTel='" + senderTel + '\'' +
                ", senderAddress='" + senderAddress + '\'' +
                ", receiverId=" + receiverId +
                ", receiverOrg='" + receiverOrg + '\'' +
                ", receiverPerson='" + receiverPerson + '\'' +
                ", receiverTel='" + receiverTel + '\'' +
                ", receiverAddress='" + receiverAddress + '\'' +
                ", billingCount=" + billingCount +
                ", stage=" + stage +
                ", beginAt=" + beginAt +
                ", signinAt=" + signinAt +
                ", picPostbackAt=" + picPostbackAt +
                ", baseId=" + baseId +
                ", picName='" + picName + '\'' +
                '}';
    }
}
