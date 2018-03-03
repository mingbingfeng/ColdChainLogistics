package com.sby.c2lp.util;

import com.itextpdf.text.Document;
import com.itextpdf.text.pdf.PdfWriter;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import com.itextpdf.text.*;
import com.itextpdf.text.pdf.BaseFont;
import com.itextpdf.text.pdf.PdfPCell;
import com.itextpdf.text.pdf.PdfPTable;
import com.itextpdf.text.pdf.PdfWriter;

import java.text.SimpleDateFormat;
import java.util.Map;

/**
 * Created by wanghe on 2016/8/16.
 */
public class historicaldataPDFBuilder {

    protected void buildPdfDocument(Map<String, Object> model, Document doc, PdfWriter writer, HttpServletRequest request, HttpServletResponse response) throws Exception {
        doc.setPageSize(PageSize.A4.rotate());
        doc.newPage();
        SimpleDateFormat sdf=new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
        BaseFont chineseBaseFont = BaseFont.createFont("STSong-Light", "UniGB-UCS2-H", BaseFont.NOT_EMBEDDED);
        Font font = new Font(chineseBaseFont, 12, Font.NORMAL);
        Font headFont2 = new Font(chineseBaseFont, 10, Font.NORMAL);
        String head = "运单编号："+model.get("number")+"\n发收货单位："+model.get("senderOrg")+" 到 "+model.get("receiverOrg");
        doc.add(new Paragraph(head,font));
    }
}
