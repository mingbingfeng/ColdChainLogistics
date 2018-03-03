package com.sby.c2lp.dao;

import com.sby.c2lp.model.WaybillBase;
import com.sby.c2lp.model.WaybillNode;

import java.sql.Timestamp;
import java.util.List;

/**
 * Created by wanghe on 2016/8/1.
 */
public interface NumberQueryDao {
    public List<WaybillNode> findNumberList(String number,Integer customerId,Integer role);

    public WaybillBase sanfangnumber(String number,Integer customerId,Integer role);

    public WaybillNode findstartTime(String number,Long id);

    public WaybillNode findendTime(String number,Timestamp operateAt);
}
