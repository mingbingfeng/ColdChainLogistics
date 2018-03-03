package com.sby.c2lp.util;

/**
 * Created by zhaoyou on 1/7/15.
 */

import com.itextpdf.text.*;
import com.itextpdf.text.pdf.BaseFont;
import com.itextpdf.text.pdf.PdfPCell;
import com.itextpdf.text.pdf.PdfPTable;
import com.itextpdf.text.pdf.PdfWriter;
import com.sby.c2lp.model.VisitRecord;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import java.text.DecimalFormat;
import java.text.SimpleDateFormat;
import java.util.List;
import java.util.Map;

/**
 * This view class generates a PDF document 'on the fly' based on the data
 * contained in the model.
 * @author www.codejava.net
 *
 */
public class PDFBuilderCount extends AbstractITextPdfView {

    @Override
    protected void buildPdfDocument(Map<String, Object> model, Document doc,
                                    PdfWriter writer, HttpServletRequest request, HttpServletResponse response)
            throws Exception {
        DecimalFormat df = new DecimalFormat("#.0");
        doc.setPageSize(PageSize.A4.rotate());
        doc.newPage();
        BaseFont chineseBaseFont = BaseFont.createFont("STSong-Light", "UniGB-UCS2-H", BaseFont.NOT_EMBEDDED);
        Font font = new Font(chineseBaseFont, 12, Font.NORMAL);
        Font headFont2 = new Font(chineseBaseFont, 10, Font.NORMAL);
        SimpleDateFormat sdf=new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
        List<Map<String, Object>> list = (List<Map<String, Object>>) model.get("list");
        String head = "委托商："+ model.get("sender")+"\n起止时间："+(String)model.get("startTime")+" 到 "+(String)model.get("endTime");
        doc.add(new Paragraph(head,font));
        PdfPTable table = new PdfPTable(3);
        table.setWidthPercentage(100.0f);
        table.setSpacingBefore(10);
        PdfPCell cell = new PdfPCell();
        cell.setBackgroundColor(BaseColor.LIGHT_GRAY);
        cell.setPadding(5);
        cell = new PdfPCell(new Paragraph("委托商",font));
        cell.setHorizontalAlignment(Element.ALIGN_CENTER);//设置内容水平居中显示
        cell.setVerticalAlignment(Element.ALIGN_MIDDLE);
        table.addCell(cell);//增加单元格
        cell = new PdfPCell(new Paragraph("运单票数",font));
        cell.setHorizontalAlignment(Element.ALIGN_CENTER);//设置内容水平居中显示
        cell.setVerticalAlignment(Element.ALIGN_MIDDLE);
        table.addCell(cell);//增加单元格
        cell = new PdfPCell(new Paragraph("件数",font));
        cell.setHorizontalAlignment(Element.ALIGN_CENTER);//设置内容水平居中显示
        cell.setVerticalAlignment(Element.ALIGN_MIDDLE);
        table.addCell(cell);//增加单元格
        for(int i=0;i<list.size();i++){
            cell = new PdfPCell(new Paragraph((String)list.get(i).get("senderOrg"),font));
            cell.setHorizontalAlignment(Element.ALIGN_CENTER);//设置内容水平居中显示
            cell.setVerticalAlignment(Element.ALIGN_MIDDLE);
            table.addCell(cell);
            cell = new PdfPCell(new Paragraph(list.get(i).get("count").toString(),font));
            cell.setHorizontalAlignment(Element.ALIGN_CENTER);//设置内容水平居中显示
            cell.setVerticalAlignment(Element.ALIGN_MIDDLE);
            table.addCell(cell);
            cell = new PdfPCell(new Paragraph((String)list.get(i).get("sum").toString(),font));
            cell.setHorizontalAlignment(Element.ALIGN_CENTER);//设置内容水平居中显示
            cell.setVerticalAlignment(Element.ALIGN_MIDDLE);
            table.addCell(cell);
        }
        doc.add(table);
    }
}