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
        /// ���excel�ļ�
        /// </summary>
        /// <param name="selName"></param>
        /// <param name="fileFullName"></param>
        public void outputExcel(string selName,DataTable resultTable,List<string> paramNameList, int groupCount,string suffixString,  Hashtable precisionHT)
        {
            initialExcel();
          
            int posCol = 1;//��ǰ�к�
            if (excel.Workbooks.Count == 0)
            {
                addWorkbook();
            }
            Excel.Worksheet ws = (Excel.Worksheet)excel.Workbooks[1].Worksheets[1];
            ws.Cells.Clear();
            setDefaultFormat(ws);
            mergeAndSetValue(ws, 1, posCol, 2, posCol, "�����", true);

            posCol++;

            mergeAndSetValue(ws, 1, posCol, 2, posCol, "�ɹ�����", true);


            //��Ҫ��ȡ�����Ϣ

            //ÿ����,�������ĸ���
            int cols = 2;
            if (selName == "��")
            {
                cols = 1;
            }

            //ÿ֧������Ҫ�ж����еĿռ�
            int hengCount = paramNameList.Count - cols + 1;

            //��Ҫ�������ļ�����������
            string[,] hengNames = new string[hengCount, 1];

            List<string> tempNameList = new List<string>(paramNameList.ToArray());

            if (cols == 2)
            {
                //���ڹ�����

                //Ҫȥ��������

                tempNameList.Remove(selName);


            }



            for (int i = 0; i < tempNameList.Count; i++)
            {
                hengNames[i, 0] = tempNameList[i].ToString();

            }




            //��ѯ���±仯,���Ǳ仯,��仯�϶����е�,ֻsuffixString������

            string[] colNames = new string[cols];

            string resultName = "�ɹ�";

            if (cols == 2)
            {
                colNames[0] = selName;
                colNames[1] = resultName;
            }
            else
            {
                colNames[0] = resultName;
            }

            string[] datas = new string[cols * (groupCount + 2)];//2��ʾһ���Ǳ仯�У���һ������仯�У����Զ��2��

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

            //��ȷ����������0

            DataRow row0 = resultTable.Rows[0];



            for (int i = 0; i < groupCount; i++)
            {
                object obj = row0[PubConstant.timeColumnName+(i)];

                dates.Add(obj);


            }

            //�ϲ�ʱ�䵥Ԫ��


          

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




            //�ϲ��仯����Ԫ��

            string[] changesuffixs = new string[] { suffixString, "��仯" };



            for (int i = cols * groupCount + startIndex, j = 0; i <=endIndex; i = i + cols)
            {
                cell1 = ws.Cells[1, i];
                cell2 = ws.Cells[1, i + cols - 1];

                ra = ws.Range[cell1, cell2];
                ra.Merge(false);

                ra.Value2 = changesuffixs[j / cols];

                j = j + cols;

            }


            //�����ĸ���

            Excel.Range ra1 = null, ra2 = null;

            for (int i = 0; i < resultTable.Rows.Count; i++)
            {
                DataRow row = resultTable.Rows[i];
                string tempSn = row["�����"].ToString();

                //�ϲ���ŵ�Ԫ��

                cell1 = ws.Cells[i * hengCount + startRowIndex, 1];//�ӵ����п�ʼ
                cell2 = ws.Cells[(i + 1) * hengCount + startRowIndex - 1, 1];

                ra = ws.Range[cell1, cell2];

                ra.Merge(false);
                ra.Value2 = tempSn;



                //����������



                ra1 = ws.Range[cell1, cell1];
                ra2 = ws.Range[cell2, cell2];

                ra = ws.Range[ra1.get_Offset(0, 1), ra2.get_Offset(0, 1)];

                ra.Value2 = hengNames;

                //��vals��������ݣ����ƽ���Ӧ�ĸ�ʽ



                for (int h = 0; h < hengCount; h++)
                {
                    int pos = 0;
                    int rowIndexVals = i * hengCount + h;//����*��ǰ�к�

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

                    //�仯

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

            //��vals�������excel��

            cell1 = ws.Cells[startRowIndex, startIndex];
            cell2 = ws.Cells[endRowIndex, endIndex]; //�ӵ��������п�ʼ

            ra = ws.Range[cell1, cell2];

            ra.Value2 = vals;


            //���õ�Ԫ���ʽ

            for (int i = startIndex, j = 0; i <= endIndex; i++, j++)
            {
                string name = colNames[j % cols];

                //���ǹ����еĻ�
                if (name != selName)
                {
                    name = hengNames[0, 0];
                }

                //�Ӿ��ȱ��л�þ���

                byte precision = 2;
                if (precisionHT.Contains(name))
                {
                   precision= (byte)precisionHT[name];
                }

                string numberFormatString = "0";
                if (precision > 0)
                {
                    numberFormatString += ".";//�ȼ�һ����
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
