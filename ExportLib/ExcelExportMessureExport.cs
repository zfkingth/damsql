using System;
using System.Collections.Generic;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Collections;
using System.Data;
using hammergo.GlobalConfig;


namespace hammergo.ExportLib
{
    public class ExcelExportMessureExport : ExcelExportBase
    {
        public static  string unitFeildName = "unit";
        public static  string preciseFeildName = "precise";

        /// <summary>
        /// ��һϵ��DataTable�������������Excel�ļ���
        /// </summary>
        /// <param name="tableList">���ݱ�</param>
        public  void OutPutTableListToExcel(List<DataTable> tableList)
        {
            initialExcel();

            if (excel.Workbooks.Count == 0)
            {
                addWorkbook();
            }
            Excel.Worksheet ws = (Excel.Worksheet)excel.Workbooks[1].Worksheets[1];
            ws.Cells.Clear();
            setDefaultFormat(ws);


            int rowIndex = 5, colIndex = 2;//com�������ʼ������1
            foreach (DataTable table in tableList)
            {
                int excelRowCnt = table.Rows.Count + 1;//��ͷ��ռ��һ��
                int excelColCnt = table.Columns.Count;

                object[,] datas = new object[excelRowCnt, excelColCnt];
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    DataColumn column = table.Columns[i];
                    datas[0, i] = column.ColumnName;

                    object obj = column.ExtendedProperties[unitFeildName];
                    if (obj != null)
                    {
                        datas[0, i] += string.Format("({0})", obj);
                    }

                    obj = column.ExtendedProperties[preciseFeildName];
                    if (obj != null)
                    {
                        if (column.DataType == typeof(double))
                        {

                            Excel.Range dateRange = ws.Range[ws.Cells[rowIndex + 1, colIndex + i], ws.Cells[rowIndex + 1 + table.Rows.Count - 1, colIndex + i]];

                            dateRange.NumberFormat = Utility.Utility.getNumbericString((byte)obj);
                        }
                    }

                    if (column.DataType == typeof(DateTime))
                    {
                        //����Ϊ����
                        Excel.Range dateRange = ws.Range[ws.Cells[rowIndex + 1, colIndex + i], ws.Cells[rowIndex + 1 + table.Rows.Count - 1, colIndex + i]];

                        dateRange.NumberFormat = PubConstant.customString;
                    }
                }

                for (int i = 0; i < table.Rows.Count; i++)
                {

                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        datas[i + 1, j] = table.Rows[i][j];
                    }
                }

                Excel.Range ra = setArrayValue(ws, datas, rowIndex, colIndex);
                ra.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

                rowIndex += excelRowCnt + 4;//�ճ�4��
            }

            ws.Columns.AutoFit();
            ws.Activate();
            setExcelVisible(true, excel);



        }
    }
}
