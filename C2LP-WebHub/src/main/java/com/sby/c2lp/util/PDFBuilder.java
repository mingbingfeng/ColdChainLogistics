package com.sby.c2lp.util;

/**
 * Created by zhaoyou on 1/7/15.
 * Created by zhaoyou on 1/7/15.
 */

import com.itextpdf.text.*;
import com.itextpdf.text.pdf.BaseFont;
import com.itextpdf.text.pdf.PdfPCell;
import com.itextpdf.text.pdf.PdfPTable;
import com.itextpdf.text.pdf.PdfWriter;
import com.sby.c2lp.model.Aiinfo;

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
public class PDFBuilder extends AbstractITextPdfView {

    @Override
    protected void buildPdfDocument(Map<String, Object> model, Document doc,
                                    PdfWriter writer, HttpServletRequest request, HttpServletResponse response)
            throws Exception {
        DecimalFormat df = new DecimalFormat("#.0");
        doc.setPageSize(PageSize.A4.rotate());
        doc.newPage();
        SimpleDateFormat sdf=new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
        List<Aiinfo> aiList = (List<Aiinfo>) model.get("aiList");
        List<Object[]> aiinfoHistoryList=(List<Object[]>) model.get("aiinfoHistoryList");
        BaseFont chineseBaseFont = BaseFont.createFont("STSong-Light", "UniGB-UCS2-H", BaseFont.NOT_EMBEDDED);
        Font font = new Font(chineseBaseFont, 12, Font.NORMAL);
        Font headFont2 = new Font(chineseBaseFont, 10, Font.NORMAL);
        String head = "冷库名/车牌号："+model.get("storageName")+"\n运单编号："+model.get("number")+"\n发收货单位："+model.get("senderOrg")+" 到 "+model.get("receiverOrg")+"\n起止时间："+sdf.format(model.get("startTime"))+" 到 "+sdf.format(model.get("endTime"));
        doc.add(new Paragraph(head,font));
        PdfPTable table = new PdfPTable(aiList.size()+2);
        table.setWidthPercentage(100.0f);
        table.setSpacingBefore(10);

        PdfPCell cell = new PdfPCell();
        cell.setBackgroundColor(BaseColor.LIGHT_GRAY);
        cell.setPadding(5);
        String[] header=new String[aiList.size()+2];
        header[0]="记录时间";
        for(int i=0;i<aiList.size();i++){
            header[i+1]=aiList.get(i).getPointName();
        }
        header[aiList.size()+1]="报警状态";
        for(int i=0;i<header.length;i++){
            cell = new PdfPCell(new Paragraph(header[i],font));//建立一个单元格
            cell.setHorizontalAlignment(Element.ALIGN_CENTER);//设置内容水平居中显示
            cell.setVerticalAlignment(Element.ALIGN_MIDDLE);
            table.addCell(cell);//增加单元格
        }

        for(int i=0;i<aiinfoHistoryList.size();i++){
            Object[] tt=(Object[]) aiinfoHistoryList.get(i);
            for(int j=0; j<tt.length;j++){
                if (j == 0) {                   // 时间字段
                    cell = new PdfPCell(new Paragraph(tt[j].toString(),headFont2));
                    cell.setHorizontalAlignment(Element.ALIGN_CENTER);
                    cell.setVerticalAlignment(Element.ALIGN_MIDDLE);
                    table.addCell(cell);
                } else if (j == tt.length - 1) {  // 报警状态
                    String tips = tt[j].equals(1) ? "报警" : "正常";
                    cell = new PdfPCell(new Paragraph(tips,headFont2));
                    cell.setHorizontalAlignment(Element.ALIGN_CENTER);
                    cell.setVerticalAlignment(Element.ALIGN_MIDDLE);
                    table.addCell(cell);
                } else {                        // 温湿度值
                    String v = "--";
                    if (tt[j] != null) {
                        Double ttt= Double.parseDouble(df.format(tt[j]));
                        if (ttt != -300) v = ttt.toString();
                    }
                    //添加温湿度值
                    cell = new PdfPCell(new Paragraph(v,headFont2));
                    cell.setHorizontalAlignment(Element.ALIGN_CENTER);
                    cell.setVerticalAlignment(Element.ALIGN_MIDDLE);
                    table.addCell(cell);
                }
            }
        }
        cell = new PdfPCell(new Paragraph("",headFont2));
        cell.setHorizontalAlignment(Element.ALIGN_CENTER);
        cell.setVerticalAlignment(Element.ALIGN_MIDDLE);
        //cell.setBackgroundColor(Color.gray);
        table.addCell(cell);//增加单元格
        doc.add(table);
    }


}