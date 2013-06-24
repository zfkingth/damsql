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

            //����
            int colsCount = 0;//�ܵ�excel�е�����
            constructExcel(ws, dateFormat, currentDate, fetchExtreamNameList, paramNamelist, selectedName, ref colsCount);
            object[,] datas = constructDatas(fetchExtreamNameList, selectedName, outTable, colsCount);

            Excel.Range ra = putAndFormatExcel(ws, datas, 3, 1);


            formatWorksheet(ws, ra);

        }

        private void constructExcel(Excel.Worksheet ws, string dateFormat, DateTime currentDate, List<string> fetchExtreamNameList, List<string> paramNameList, string selectedName, ref int colsCount)
        {
           

            NumericString numericStrings = new NumericString(fetchExtreamNameList, paramNameList, selectedName, dateFormat);


            int colsPerGroup = 3;//ÿ������,һ������ֵ,һ���ɹ�ֵ,��һ��������

            if (fetchExtreamNameList.Count == paramNameList.Count)
            {
                //ֻ�гɹ�,û�а���ֵ
                colsPerGroup = 2;
            }

            //���ò������
            int posCol = 1;//��ǰ�к�

            mergeAndSetValue(ws, 1, posCol, 2, posCol, "�����", true);

            posCol++;

            mergeAndSetValue(ws, 1, posCol, 2, posCol, "�ɹ�����", true);

            posCol++;

            handleExtreamGroup(ws, "���ֵ", colsPerGroup, posCol, selectedName, numericStrings);

            posCol += colsPerGroup;


            handleExtreamGroup(ws, "��Сֵ", colsPerGroup, posCol, selectedName, numericStrings);

            posCol += colsPerGroup;

            mergeAndSetValue(ws, 1, posCol, 2, posCol, new object[2, 1] { { "����" }, { "�ɹ�" } }, false).EntireColumn.NumberFormat = numericStrings.ResultNameFormat;


            posCol++;

          


            string currentString = string.Format("��ǰֵ({0})", currentDate.ToString(PubConstant.shortString));

            mergeAndSetValue(ws, 1, posCol, 1, posCol + colsPerGroup - 1, currentString, true);


            if (colsPerGroup == 3)
            {

                mergeAndSetValue(ws, 2, posCol, 2, posCol, selectedName, false).EntireColumn.NumberFormat = numericStrings.SelectedNameFormat;
                mergeAndSetValue(ws, 2, posCol + 1, 2, posCol + 1, "�ɹ�", false).EntireColumn.NumberFormat = numericStrings.ResultNameFormat;
                mergeAndSetValue(ws, 2, posCol + 2, 2, posCol + 2, "��仯", false).EntireColumn.NumberFormat = numericStrings.ResultNameFormat;

            }
            else if (colsPerGroup == 2)
            {
                mergeAndSetValue(ws, 2, posCol, 2, posCol, "�ɹ�", false).EntireColumn.NumberFormat = numericStrings.ResultNameFormat;
                mergeAndSetValue(ws, 2, posCol + 1, 2, posCol + 1, "��仯", false).EntireColumn.NumberFormat = numericStrings.ResultNameFormat;

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
                mergeAndSetValue(ws, 2, posCol + 1, 2, posCol + 1, "�ɹ�", false).EntireColumn.NumberFormat = numericStrings.ResultNameFormat;
                mergeAndSetValue(ws, 2, posCol + 2, 2, posCol + 2, "����", false).EntireColumn.NumberFormat = numericStrings.TimeFormat;

            }
            else if (colsPerGroup == 2)
            {
                mergeAndSetValue(ws, 2, posCol, 2, posCol, "�ɹ�", false).EntireColumn.NumberFormat = numericStrings.ResultNameFormat;
                mergeAndSetValue(ws, 2, posCol + 1, 2, posCol + 1, "����", false).EntireColumn.NumberFormat = numericStrings.TimeFormat;

            }


        }

        private object[,] constructDatas(List<string> fetchExtreamNameList, string selectedName, DataTable outTable, int colsCount)
        {
            //ͳ����Ҫ������
            int rowsCount = outTable.Rows.Count * fetchExtreamNameList.Count;
            object[,] datas = new object[rowsCount, colsCount];

            int rowIndexInDatas = 0;

            for (int rowIndex = 0; rowIndex < outTable.Rows.Count; rowIndex++)
            {
                DataRow row = outTable.Rows[rowIndex];

                int colPosInRow = 1;//��һ��Ϊ�����

                for (int i = 0; i < fetchExtreamNameList.Count; i++)
                {
                    string colname = fetchExtreamNameList[i].ToString();

                    datas[rowIndexInDatas, 0] = row["�����"].ToString();//"�����"
                    datas[rowIndexInDatas, 1] = colname;

                    for (int j = 2; j < colsCount; j++)
                    {
                        //��row�п���
                        datas[rowIndexInDatas, j] = handleValue(row[colPosInRow++]);

                    }

                    rowIndexInDatas++;//��һ�����ݾ���䵽��һ����,������outTable��Ľṹ�����
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
            ws.Columns.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;//���ж���
            ws.Columns.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

            ra.Borders.LineStyle = Excel.XlLineStyle.xlContinuous;

        }


    }
}
