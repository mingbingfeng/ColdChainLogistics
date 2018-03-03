package com.sby.c2lp.dao.impl;

import com.sby.c2lp.dao.ColdStorageDao;
import com.sby.c2lp.model.ColdStorage;
import com.sby.c2lp.model.Customer;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.dao.DataAccessException;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.jdbc.core.ResultSetExtractor;
import org.springframework.jdbc.core.RowMapper;
import org.springframework.stereotype.Repository;

import javax.sql.DataSource;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.List;
import java.util.Map;

/**
 * Created by wanghe on 2016/8/9.
 */
@Repository
public class ColdStorageDaoImpl implements ColdStorageDao {
    private JdbcTemplate template;

    @Autowired
    public void setDataSource(DataSource dataSource) {
        this.template = new JdbcTemplate(dataSource);
    }
    @Override
    public ColdStorage coldObject(Integer id) {
        String sql="select * from coldstorage where id=?";
        return template.query(sql,new Object[]{id},new ResultSetExtractor<ColdStorage>() {
            @Override
            public ColdStorage extractData(ResultSet rs) throws SQLException, DataAccessException {
                if (rs.next()) {
                    ColdStorage coldStorage = new ColdStorage();
                    coldStorage.setId(rs.getInt("id"));
                    coldStorage.setStorageName(rs.getString("storageName"));
                    coldStorage.setStorageType(rs.getInt("storageType"));
                    coldStorage.setDriver(rs.getString("driver"));
                    coldStorage.setDriverTel(rs.getString("driverTel"));
                    coldStorage.setRemark(rs.getString("remark"));
                    coldStorage.setCreateAt(rs.getTimestamp("createAt"));
                    coldStorage.setActived(rs.getInt("actived"));
                    coldStorage.setLatitude(rs.getDouble("latitude"));
                    coldStorage.setLongitude(rs.getDouble("longitude"));
                    return coldStorage;
                } else {
                    return null;
                }
            }
        });
    }

    public List<ColdStorage> AllCars (){
        String sql="select * from coldstorage where storageType=2 ORDER BY storageName";
        return template.query(sql, new RowMapper<ColdStorage>() {
            @Override
            public ColdStorage mapRow(ResultSet rs, int i) throws SQLException {
                ColdStorage c = new ColdStorage();
                c.setId(rs.getInt("id"));
                c.setStorageName(rs.getString("storageName"));
                return c;
            }
        });
    }

}
