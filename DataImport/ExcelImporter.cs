using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data;
using hammergo.Utility;
using hammergo.GlobalConfig;
using System.Collections;
using hammergo.Model;

namespace hammergo.DataImport
{
    class ExcelImporter
    {
        internal ExcelHelper excelHelper = null;


        string fullPath = "";
        string workSheetName = "";
        /// <summary>
        /// ��excel�������ݵ����ݿ�
        /// </summary>
        /// <param name="fullPath">�ļ�����·��</param>
        public void import(string fullPath)
        {
            try
            {
                if (excelHelper == null)
                {
                    excelHelper = new ExcelHelper();
                }

                excelHelper.initialExcel();

                Excel.Workbook wb = excelHelper.openWorkbookWithoutDisplay(fullPath);


                this.fullPath = fullPath;
                foreach (Excel.Worksheet ws in wb.Sheets)
                {
                    //excel�����������
                    string appName = ws.Name;
                    workSheetName = ws.Name;
                    //�ɻ�ȡ�������
                    AppIntegratedInfo appInfo = new AppIntegratedInfo(appName, 1, null, DateTime.MaxValue);
                    if (appInfo.App != null)
                    {
                        //�������ܱ�
                        DataTable table = UtilityGetData.createDataTableSchema(appInfo);

                        //���������������е��кŶ�Ӧ��
                        Hashtable nameIndexMaps = createHashMap(appInfo);
                        //��excel�е���Ч���ݶ���������
                        object[,] allData = readData(ws, appInfo, nameIndexMaps);
                        //�������е����ݵ�table
                        fillDataTable(appInfo,allData, nameIndexMaps, table);

                        saveData(appInfo, table);
                    }
                }
            }
            finally
            {
                if (excelHelper != null)
                {
                   
                    excelHelper.QuitExcel();
                    excelHelper = null;
                }
            }

        }


        private Hashtable createHashMap(AppIntegratedInfo appInfo)
        {
            Hashtable ht = new Hashtable(10);
            ht.Add(PubConstant.timeColumnName, null);
            ht.Add(PubConstant.remarkColumnName, null);

            foreach (MessureParam item in appInfo.MessureParams)
            {
                ht.Add(item.ParamName, null);
            }

            foreach (CalculateParam item in appInfo.CalcParams)
            {
                ht.Add(item.ParamName, null);
            }

            return ht;
        }

        /// <summary>
        /// ��ȡ����ʱ��ʱ���б�
        /// </summary>
        List<DateTime> listDates = null;

        //excel�б�ͷ��ռ������
        const int tableHeaderRowsCnt = 1;
        /// <summary>
        /// ����ʱÿ�ζ�ȡ�����ݸ���
        /// </summary>
        const int cntPerRead = 50;
        /// <summary>
        /// excel��ʵ�����ݵ���ʼ�к�
        /// </summary>
        const int dataStartRow = 2, dataStartCol = 1;
        private object[,] readData(Microsoft.Office.Interop.Excel.Worksheet ws, AppIntegratedInfo appInfo, Hashtable nameIndexMaps)
        {
            object[,] allData = null;

            listDates = new List<DateTime>(100);
            int colsCnt = appInfo.MessureParams.Count + appInfo.CalcParams.Count + 1 + 1;//1�����ڣ�1����ע

            //��ȡ��ͷ
            object[,] tableHeader = excelHelper.getArrayValue(ws, dataStartRow - tableHeaderRowsCnt, dataStartCol, 1, colsCnt);



            for (int i = 1; i <= colsCnt; i++)
            {
                object obj = tableHeader[1, i];
                if (obj != null)
                {
                    string val = obj.ToString().Trim();

                    if (nameIndexMaps.ContainsKey(val))
                    {
                        nameIndexMaps[val] = i;
                    }
                    else
                    {
                        throw new Exception(string.Format("Excel�ļ�{0}�ı�{1}�е�'{2}'������'{3}'�Ĳ���������,�޷�����", fullPath, ws.Name, val, appInfo.appName));
                    }
                }
                else
                {
                    throw new Exception(string.Format("Excel�ļ�{0}�ı�{1}�еĵ�{2}�в���Ϊ��", fullPath, ws.Name, i));
                }
            }

            bool goLoop = true;
            object[,] dateData = null;
            //ȷ�����ݵ�����
            for (int j = 0; goLoop; j++)
            {

                dateData = excelHelper.getArrayValue(ws, dataStartRow + j * cntPerRead, dataStartCol, cntPerRead, 1);
                for (int i = 1; i <= cntPerRead; i++)
                {
                    object obj = dateData[i, 1];

                    if (obj != null)
                    {
                        DateTime? cDate = convertToDateTime(obj);
                        if (cDate != null)
                        {
                            //�����������ֵ��������һ����ת��
                            listDates.Add(cDate.Value);
                        }
                        else
                        {
                            throw new Exception(
                                string.Format("Excel�ļ�{0}�ı�{1}�У���{2}����������\n �����ݱ��������ڻ��ַ�������,������ַ�������,���ʽ������{3}��{4}",
                                fullPath, ws.Name, dataStartRow + j * cntPerRead + i - 1, PubConstant.shortString, PubConstant.customString));
                        }


                    }
                    else
                    {
                        goLoop = false;
                        break;//����ֻ����ô����
                    }
                }

            }



            allData = excelHelper.getArrayValue(ws, dataStartRow, dataStartCol, listDates.Count, colsCnt);//����ʱ����

            return allData;


        }

        private DateTime? convertToDateTime(object obj)
        {
            DateTime? date = null;
            if (obj is DateTime)
            {
                date = (DateTime)obj;
            }
            else if (obj is string)
            {
                string temp = obj.ToString().Trim();

                if (temp.Length == PubConstant.shortString.Length)
                {
                    try
                    {
                        date = DateTime.ParseExact(temp, PubConstant.shortString, null);
                    }
                    catch (Exception)
                    {
                    }
                }
                else if (temp.Length == PubConstant.customString.Length)
                {
                    try
                    {
                        date = DateTime.ParseExact(temp, PubConstant.customString, null);
                    }
                    catch (Exception)
                    {
                    }
                }


            }

            return date;
        }



        private void fillDataTable(AppIntegratedInfo appInfo, object[,] allData, Hashtable nameIndexMaps, DataTable table)
        {
            for (int i = 0; i < listDates.Count; i++)
            {
                DataRow row = table.NewRow();

                foreach (string key in nameIndexMaps.Keys)
                {
                    if (key == PubConstant.timeColumnName)
                    {
                        row[key] = listDates[i];
                    }
                    else if (key == PubConstant.remarkColumnName)
                    {
                        row[key] = allData[i + 1, (int)nameIndexMaps[key]];
                    }
                    else
                    {
                        object val = allData[i + 1, (int)nameIndexMaps[key]];//

                        double? ret = null;
                        if (val is string)
                        {
                            string temp = val.ToString().Trim();
                            try
                            {
                                ret = double.Parse(temp);
                            }
                            catch (Exception)
                            {
                                throw new Exception(string.Format("Excel�ļ�{0}�ı�{1}�У���{2}�е�'{3}'����������\n ", fullPath, workSheetName, i + 1 + tableHeaderRowsCnt, key));//i��0��ʼ�����л�ԭ�Ļ�Ҫ��1����excel�ĵ�һ���Ǳ�ͷ�����Ի�Ҫ��tableHeaderRowsCnt

                            }

                        }
                        else if (val is double)
                        {
                            ret = (double)val;
                        }

                        if (ret != null)
                        {
                           CalculateParam cp= appInfo.CalcParams.Find(delegate(CalculateParam item) { return item.ParamName == key; });
                           if (cp != null&&cp.PrecisionNum!=null)
                           {

                               ret = Utility.Utility.round(ret.Value, cp.PrecisionNum.Value);
                           }
                            
                            row[key] = ret;
                        }
                    }
                }

                row.EndEdit();
                table.Rows.Add(row);
            }

            table.AcceptChanges();
        }

        private void saveData(AppIntegratedInfo appInfo, DataTable table)
        {
            DateTime maxDate = DateTime.MinValue;
            if (appInfo.CalcValues.Count > 0)
            {
                maxDate = appInfo.CalcValues[0].Date.Value;

            }

            foreach (DataRow row in table.Rows)
            {
                DateTime date = (DateTime)row[PubConstant.timeColumnName];
                if (date > maxDate)
                {
                    appInfo.Tracking = false;
                    appInfo.MessureValues.Clear();
                    appInfo.CalcValues.Clear();
                    appInfo.Remarks.Clear();
                    appInfo.Tracking = true;

                    foreach (MessureParam item in appInfo.MessureParams)
                    {
                        MessureValue mv = new MessureValue();
                        mv.Date = date;
                        mv.MessureParamID = item.MessureParamID;
                        mv.Val = row[item.ParamName] as double?;

                        appInfo.MessureValues.Add(mv);
                    }

                    foreach (CalculateParam item in appInfo.CalcParams)
                    {
                        CalculateValue mv = new CalculateValue();
                        mv.Date = date;
                        mv.CalculateParamID = item.CalculateParamID;
                        mv.Val =  row[item.ParamName] as double?;

                        appInfo.CalcValues.Add(mv);
                    }

                    object remarkVal = row[PubConstant.remarkColumnName];
                    if (remarkVal != null && remarkVal.ToString().Trim().Length!=0)
                    {
                        Remark remark = new Remark();
                        remark.AppName = appInfo.appName;
                        remark.Date = date;
                        remark.RemarkText = remarkVal.ToString().Trim();
                        appInfo.Remarks.Add(remark);
                    }

                    appInfo.Update();

                }
            }
        }



    }
}
