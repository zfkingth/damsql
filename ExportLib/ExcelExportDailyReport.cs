using System;
using System.Collections.Generic;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Collections;
using System.Data;
using hammergo.GlobalConfig;
using hammergo.Utility;
using System.Linq;
using System.Data.Linq;


namespace hammergo.ExportLib
{
    public class ExcelExportDailyReport : ExcelExportBase
    {

        public void outputExcel(string dateFormat, List<string> paramNamelist, Dictionary<string, int> preDic, System.Data.DataTable outTable)
        {
            initialExcel();
            if (excel.Workbooks.Count == 0)
            {
                addWorkbook();
            }
            Excel.Worksheet ws = (Excel.Worksheet)excel.Workbooks[1].Worksheets[1];
            ws.Cells.Clear();
            setDefaultFormat(ws);

            //导出
            int colsCount = 0;//总的excel中的列数
            constructExcel(ws, dateFormat, paramNamelist, preDic, outTable, ref colsCount);
            object[,] datas = constructDatas(outTable);

            Excel.Range ra = setArrayValue(ws, datas, _startRowIndex + 2, _startColIndex);

            formatWorksheet(ws, ra);
            ra.Columns.AutoFit();
            setExcelVisible(true);


        }


        /// <summary>
        /// 输出内容在Excel中的起始行号
        /// </summary>
        const int _startRowIndex = 2;
        /// <summary>
        /// 输出内容在Excel中的起始列号
        /// </summary>
        const int _startColIndex = 2;
        /// <summary>
        /// 设置表头
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="dateFormat"></param>
        /// <param name="paramNamelist"></param>
        /// <param name="preDic"></param>
        /// <param name="table"></param>
        /// <param name="colsCount"></param>
        private void constructExcel(Excel.Worksheet ws, string dateFormat, List<string> paramNamelist, Dictionary<string, int> preDic, DataTable table, ref int colsCount)
        {

            NumericString numericStrings = new NumericString(dateFormat);

            int colsPerGroup = paramNamelist.Count;



            //设置起始的行号和列号，把第一行，第一列空出来
            int posCol = _startColIndex;//当前列号
            int posRow = _startRowIndex;

            mergeAndSetValue(ws, posRow, posCol, posRow + 1, posCol, "测点编号", true);

            posCol++;

            mergeAndSetValue(ws, posRow, posCol, posRow + 1, posCol, "日期", true).EntireColumn.NumberFormat = numericStrings.TimeFormat;

            posCol++;

            //合并成果,只占一行，n，列
            mergeAndSetValue(ws, posRow, posCol, posRow, posCol + paramNamelist.Count - 1, "成果", true);

            //处理变量
            for (int i = 0; i < paramNamelist.Count; i++)
            {
                string paramName = paramNamelist[i];
                Excel.Range ra = mergeAndSetValue(ws, posRow + 1, posCol + i, posRow + 1, posCol + i, paramName, false);
                ra.EntireColumn.NumberFormat = NumericString.getNumbericString(preDic[paramName]);

            }
            posCol += paramNamelist.Count;

            mergeAndSetValue(ws, posRow, posCol, posRow, posCol + paramNamelist.Count - 1, "较上次变化量", true);

            for (int i = 0; i < paramNamelist.Count; i++)
            {
                string paramName = paramNamelist[i];
                Excel.Range ra = mergeAndSetValue(ws, posRow + 1, posCol + i, posRow + 1, posCol + i, paramName, false);
                ra.EntireColumn.NumberFormat = NumericString.getNumbericString(preDic[paramName]);

            }
            posCol += paramNamelist.Count;

            colsCount = posCol - _startColIndex;


            ws.Range[ws.Cells[_startRowIndex, _startColIndex], ws.Cells[_startRowIndex + 1, _startColIndex + colsCount - 1]].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            //ws.get_Range(ws.Cells[1, 1], ws.Cells[2, colsCount]).Borders.LineStyle = Excel.XlLineStyle.xlContinuous;



        }

        private object[,] constructDatas(DataTable outTable)
        {
            //统计需要多少行
            object[,] datas = new object[outTable.Rows.Count, outTable.Columns.Count];


            for (int i = 0; i < outTable.Rows.Count; i++)
            {
                for (int j = 0; j < outTable.Columns.Count; j++)
                {
                    //从row中拷贝
                    datas[i, j] = handleValue(outTable.Rows[i][j]);
                }



            }



            return datas;

        }


        private void formatWorksheet(Excel.Worksheet ws, Excel.Range ra)
        {
            ws.Columns.Font.Size = 10;
            ws.Columns.Font.Name = "Times New Roman";
            ws.Columns.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;//居中对齐
            ws.Columns.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

            ra.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

        }


    }
}
