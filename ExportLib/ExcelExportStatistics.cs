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
    public class ExcelExportStatistics : ExcelExportBase
    {

        public void outputExcel(string dateFormat, DateTime currentDate,List<string> fetchExtreamNameList, List<string> paramNamelist, string selectedName, System.Data.DataTable outTable)
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
            constructExcel(ws, dateFormat, currentDate, fetchExtreamNameList, paramNamelist, selectedName, ref colsCount);
            object[,] datas = constructDatas(fetchExtreamNameList, selectedName, outTable, colsCount);

            Excel.Range ra = putAndFormatExcel(ws, datas, 3, 1);


            formatWorksheet(ws, ra);

        }

        private void constructExcel(Excel.Worksheet ws, string dateFormat, DateTime currentDate, List<string> fetchExtreamNameList, List<string> paramNameList, string selectedName, ref int colsCount)
        {
           

            NumericString numericStrings = new NumericString(fetchExtreamNameList, paramNameList, selectedName, dateFormat);


            int colsPerGroup = 3;//每组三列,一个伴随值,一个成果值,再一个是日期

            if (fetchExtreamNameList.Count == paramNameList.Count)
            {
                //只有成果,没有伴随值
                colsPerGroup = 2;
            }

            //设置测点编号列
            int posCol = 1;//当前列号

            mergeAndSetValue(ws, 1, posCol, 2, posCol, "测点编号", true);

            posCol++;

            mergeAndSetValue(ws, 1, posCol, 2, posCol, "成果名称", true);

            posCol++;

            handleExtreamGroup(ws, "最大值", colsPerGroup, posCol, selectedName, numericStrings);

            posCol += colsPerGroup;


            handleExtreamGroup(ws, "最小值", colsPerGroup, posCol, selectedName, numericStrings);

            posCol += colsPerGroup;

            mergeAndSetValue(ws, 1, posCol, 2, posCol, new object[2, 1] { { "年变幅" }, { "成果" } }, false).EntireColumn.NumberFormat = numericStrings.ResultNameFormat;


            posCol++;

          


            string currentString = string.Format("当前值({0})", currentDate.ToString(PubConstant.shortString));

            mergeAndSetValue(ws, 1, posCol, 1, posCol + colsPerGroup - 1, currentString, true);


            if (colsPerGroup == 3)
            {

                mergeAndSetValue(ws, 2, posCol, 2, posCol, selectedName, false).EntireColumn.NumberFormat = numericStrings.SelectedNameFormat;
                mergeAndSetValue(ws, 2, posCol + 1, 2, posCol + 1, "成果", false).EntireColumn.NumberFormat = numericStrings.ResultNameFormat;
                mergeAndSetValue(ws, 2, posCol + 2, 2, posCol + 2, "年变化", false).EntireColumn.NumberFormat = numericStrings.ResultNameFormat;

            }
            else if (colsPerGroup == 2)
            {
                mergeAndSetValue(ws, 2, posCol, 2, posCol, "成果", false).EntireColumn.NumberFormat = numericStrings.ResultNameFormat;
                mergeAndSetValue(ws, 2, posCol + 1, 2, posCol + 1, "年变化", false).EntireColumn.NumberFormat = numericStrings.ResultNameFormat;

            }

            posCol += colsPerGroup;

            colsCount = posCol - 1;


            ws.Range[ws.Cells[1, 1], ws.Cells[2, colsCount]].Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            //ws.get_Range(ws.Cells[1, 1], ws.Cells[2, colsCount]).Borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            
           

        }

        private void handleExtreamGroup(Excel.Worksheet ws, object val, int colsPerGroup, int posCol, string selectedName, NumericString numericStrings)
        {
            mergeAndSetValue(ws, 1, posCol, 1, posCol + colsPerGroup - 1, val, true);



            if (colsPerGroup == 3)
            {

                mergeAndSetValue(ws, 2, posCol, 2, posCol, selectedName, false).EntireColumn.NumberFormat = numericStrings.SelectedNameFormat;
                mergeAndSetValue(ws, 2, posCol + 1, 2, posCol + 1, "成果", false).EntireColumn.NumberFormat = numericStrings.ResultNameFormat;
                mergeAndSetValue(ws, 2, posCol + 2, 2, posCol + 2, "日期", false).EntireColumn.NumberFormat = numericStrings.TimeFormat;

            }
            else if (colsPerGroup == 2)
            {
                mergeAndSetValue(ws, 2, posCol, 2, posCol, "成果", false).EntireColumn.NumberFormat = numericStrings.ResultNameFormat;
                mergeAndSetValue(ws, 2, posCol + 1, 2, posCol + 1, "日期", false).EntireColumn.NumberFormat = numericStrings.TimeFormat;

            }


        }

        private object[,] constructDatas(List<string> fetchExtreamNameList, string selectedName, DataTable outTable, int colsCount)
        {
            //统计需要多少行
            int rowsCount = outTable.Rows.Count * fetchExtreamNameList.Count;
            object[,] datas = new object[rowsCount, colsCount];

            int rowIndexInDatas = 0;

            for (int rowIndex = 0; rowIndex < outTable.Rows.Count; rowIndex++)
            {
                DataRow row = outTable.Rows[rowIndex];

                int colPosInRow = 1;//第一列为测点编号

                for (int i = 0; i < fetchExtreamNameList.Count; i++)
                {
                    string colname = fetchExtreamNameList[i].ToString();

                    datas[rowIndexInDatas, 0] = row["测点编号"].ToString();//"测点编号"
                    datas[rowIndexInDatas, 1] = colname;

                    for (int j = 2; j < colsCount; j++)
                    {
                        //从row中拷贝
                        datas[rowIndexInDatas, j] = handleValue(row[colPosInRow++]);

                    }

                    rowIndexInDatas++;//下一次数据就填充到下一行里,这里与outTable表的结构紧耦合
                }
            }



            return datas;

        }

        private Excel.Range putAndFormatExcel(Excel.Worksheet ws, object[,] datas, int startRowIndex, int startColIndex)
        {
            int rowsCount = datas.GetLength(0);
            int colsCount = datas.GetLength(1);

            Excel.Range ra = mergeAndSetValue(ws, startRowIndex, startColIndex, startRowIndex + rowsCount - 1, startColIndex + colsCount - 1, datas, false);

            setExcelVisible(true);
            //wb.Activate();
            return ra;
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
