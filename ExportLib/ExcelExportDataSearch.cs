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
    public class ExcelExportDataSearch:ExcelExportBase
    {
        /// <summary>
        /// 输出excel文件
        /// </summary>
        /// <param name="selName"></param>
        /// <param name="fileFullName"></param>
        public void outputExcel(string selName,DataTable resultTable,List<string> paramNameList, int groupCount,string suffixString,  Hashtable precisionHT)
        {
            initialExcel();
          
            int posCol = 1;//当前列号
            if (excel.Workbooks.Count == 0)
            {
                addWorkbook();
            }
            Excel.Worksheet ws = (Excel.Worksheet)excel.Workbooks[1].Worksheets[1];
            ws.Cells.Clear();
            setDefaultFormat(ws);
            mergeAndSetValue(ws, 1, posCol, 2, posCol, "测点编号", true);

            posCol++;

            mergeAndSetValue(ws, 1, posCol, 2, posCol, "成果名称", true);


            //先要获取表的信息

            //每纵列,物理量的个数
            int cols = 2;
            if (selName == "无")
            {
                cols = 1;
            }

            //每支仪器需要有多少行的空间
            int hengCount = paramNameList.Count - cols + 1;

            //需要制作横表的计算量的名称
            string[,] hengNames = new string[hengCount, 1];

            List<string> tempNameList = new List<string>(paramNameList.ToArray());

            if (cols == 2)
            {
                //存在公共列

                //要去掉公共列

                tempNameList.Remove(selName);


            }



            for (int i = 0; i < tempNameList.Count; i++)
            {
                hengNames[i, 0] = tempNameList[i].ToString();

            }




            //查询是月变化,还是变化,年变化肯定是有的,只suffixString就是了

            string[] colNames = new string[cols];

            string resultName = "成果";

            if (cols == 2)
            {
                colNames[0] = selName;
                colNames[1] = resultName;
            }
            else
            {
                colNames[0] = resultName;
            }

            string[] datas = new string[cols * (groupCount + 2)];//2表示一个是变化列，另一个是年变化列，所以多出2列

            for (int i = 0; i < datas.Length; )
            {
                for (int j = 0; j < cols; j++)
                {
                    datas[i] = colNames[j];

                    i++;

                }
            }

          

            int startIndex = 3;
            int endIndex = 3 + datas.Length - 1;

            int snCount = resultTable.Rows.Count;

            int startRowIndex = 3;



            object[,] vals = new object[snCount * hengCount, endIndex - startIndex + 1];

            int endRowIndex = vals.GetLength(0) + startIndex - 1;


            object cell1 = ws.Cells[2, startIndex];
            object cell2 = ws.Cells[2, endIndex];

            Excel.Range ra = ws.Range[cell1, cell2];

            ra.Value2 = datas;

     

            ArrayList dates = new ArrayList(10);

            //已确保行数大于0

            DataRow row0 = resultTable.Rows[0];



            for (int i = 0; i < groupCount; i++)
            {
                object obj = row0[PubConstant.timeColumnName+(i)];

                dates.Add(obj);


            }

            //合并时间单元格


          

            for (int i = startIndex; i <= cols * groupCount + startIndex - 1; i = i + cols)
            {
                cell1 = ws.Cells[1, i];
                cell2 = ws.Cells[1, i + cols - 1];

                ra = ws.Range[cell1, cell2];
                ra.Merge(false);

                object obj = dates[(i - startIndex) / cols];
                if (obj != null && obj != DBNull.Value)
                {
                    ra.Value2 = obj;
                    ra.NumberFormat = PubConstant.shortString;
                }


            }




            //合并变化量单元格

            string[] changesuffixs = new string[] { suffixString, "年变化" };



            for (int i = cols * groupCount + startIndex, j = 0; i <=endIndex; i = i + cols)
            {
                cell1 = ws.Cells[1, i];
                cell2 = ws.Cells[1, i + cols - 1];

                ra = ws.Range[cell1, cell2];
                ra.Merge(false);

                ra.Value2 = changesuffixs[j / cols];

                j = j + cols;

            }


            //仪器的个数

            Excel.Range ra1 = null, ra2 = null;

            for (int i = 0; i < resultTable.Rows.Count; i++)
            {
                DataRow row = resultTable.Rows[i];
                string tempSn = row["测点编号"].ToString();

                //合并编号单元格

                cell1 = ws.Cells[i * hengCount + startRowIndex, 1];//从第三行开始
                cell2 = ws.Cells[(i + 1) * hengCount + startRowIndex - 1, 1];

                ra = ws.Range[cell1, cell2];

                ra.Merge(false);
                ra.Value2 = tempSn;



                //填充横向列名



                ra1 = ws.Range[cell1, cell1];
                ra2 = ws.Range[cell2, cell2];

                ra = ws.Range[ra1.get_Offset(0, 1), ra2.get_Offset(0, 1)];

                ra.Value2 = hengNames;

                //往vals里填充数据，并计较相应的格式



                for (int h = 0; h < hengCount; h++)
                {
                    int pos = 0;
                    int rowIndexVals = i * hengCount + h;//列数*当前行号

                    for (int j = 0; j < groupCount; j++)
                    {
                        for (int k = 0; k < cols; k++)
                        {
                            string colNameInSource = colNames[k];

                            if (colNameInSource != selName)
                            {
                                colNameInSource = hengNames[h, 0];
                            }

                            colNameInSource += j;



                            object val = handleValue(row[colNameInSource]);

                            vals[rowIndexVals, pos++] = val;
                        }

                    }

                    //变化

                    for (int j = 0; j < changesuffixs.Length; j++)
                    {
                        for (int k = 0; k < cols; k++)
                        {
                            string colNameInSource = colNames[k];

                            if (colNameInSource != selName)
                            {
                                colNameInSource = hengNames[h, 0];
                            }

                            colNameInSource += changesuffixs[j];

                            object val = handleValue(row[colNameInSource]);

                            vals[rowIndexVals, pos++] = val;
                        }
                    }
                }


            }

            //把vals数组放入excel中

            cell1 = ws.Cells[startRowIndex, startIndex];
            cell2 = ws.Cells[endRowIndex, endIndex]; //从第三行三列开始

            ra = ws.Range[cell1, cell2];

            ra.Value2 = vals;


            //设置单元格格式

            for (int i = startIndex, j = 0; i <= endIndex; i++, j++)
            {
                string name = colNames[j % cols];

                //不是公共列的话
                if (name != selName)
                {
                    name = hengNames[0, 0];
                }

                //从精度比中获得精度

                byte precision = 2;
                if (precisionHT.Contains(name))
                {
                   precision= (byte)precisionHT[name];
                }

                string numberFormatString = "0";
                if (precision > 0)
                {
                    numberFormatString += ".";//先加一个点
                    for (int p = 0; p < precision; p++)
                    {
                        numberFormatString += "0";
                    }

                }

                cell1 = ws.Cells[startRowIndex, i];
                cell2 = ws.Cells[endRowIndex, i];
                ra = ws.Range[cell1, cell2];

                ra.NumberFormat = numberFormatString;
            }



            //closeAndSaveWorkbook(wb,fileFullName);
            ws.Columns.AutoFit();
            ws.Activate();
            setExcelVisible(true, excel);
        }
    }
}
