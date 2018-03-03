package com.sby.c2lp.model;
import java.sql.Timestamp;

/**
 * Created by wanghe on 2016/8/1.
 */
public class WaybillNode {
    private String number;
    private String senderOrg;
    private String receiverOrg;
    private Timestamp operateAt;
    private Integer storageId;
    private String content;
    private Long nodeid;
    private Integer arrived;
    private String storageName;

    public void setStorageName(String storageName) {
        this.storageName = storageName;
    }

    public String getStorageName() {
        return storageName;
    }

    public Integer getArrived() {
        return arrived;
    }

    public void setArrived(Integer arrived) {
        this.arrived = arrived;
    }

    public Long getNodeid() {
        return nodeid;
    }

    public void setNodeid(Long nodeid) {
        this.nodeid = nodeid;
    }

    public Integer getStorageId() {
        return storageId;
    }

    public void setStorageId(Integer storageId) {
        this.storageId = storageId;
    }

    public String getNumber() {
        return number;
    }

    public void setNumber(String number) {
        this.number = number;
    }

    public String getSenderOrg() {
        return senderOrg;
    }

    public void setSenderOrg(String senderOrg) {
        this.senderOrg = senderOrg;
    }

    public String getReceiverOrg() {
        return receiverOrg;
    }

    public void setReceiverOrg(String receiverOrg) {
        this.receiverOrg = receiverOrg;
    }

    public Timestamp getOperateAt() {
        return operateAt;
    }

    public void setOperateAt(Timestamp operateAt) {
        this.operateAt = operateAt;
    }

    public String getContent() {
        return content;
    }

    public void setContent(String content) {
        this.content = content;
    }

    public WaybillNode() {
    }

    public WaybillNode(String number, String senderOrg, String receiverOrg, Timestamp operateAt, Integer storageId, String content, Long nodeid, Integer arrived) {
        this.number = number;
        this.senderOrg = senderOrg;
        this.receiverOrg = receiverOrg;
        this.operateAt = operateAt;
        this.storageId = storageId;
        this.content = content;
        this.nodeid = nodeid;
        this.arrived = arrived;
    }

    @Override
    public String toString() {
        return "NumberQuery{" +
                "number='" + number + '\'' +
                ", senderOrg='" + senderOrg + '\'' +
                ", receiverOrg='" + receiverOrg + '\'' +
                ", operateAt=" + operateAt +
                ", storageId=" + storageId +
                ", content='" + content + '\'' +
                ", nodeid=" + nodeid +
                ", arrived=" + arrived +
                '}';
    }
}
