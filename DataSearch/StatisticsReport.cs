using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Collections;
using hammergo.Model;
using hammergo.Utility;
using hammergo.GlobalConfig;

namespace hammergo.DataSearch
{
    public partial class StatisticsReport : DevExpress.XtraEditors.XtraUserControl, ICustomDispose, hammergo.Utility.IShowAppData
    {
        public StatisticsReport()
        {
            InitializeComponent();
        }

        private void StatisticsReport_Load(object sender, EventArgs e)
        {
            appSelector1.initial();
            initialPrecision();
            c1DateEdit1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            c1DateEdit1.Properties.DisplayFormat.FormatString = PubConstant.customString;

            c1DateEdit2.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            c1DateEdit2.Properties.DisplayFormat.FormatString = PubConstant.customString;


            c1DateEdit3.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            c1DateEdit3.Properties.DisplayFormat.FormatString = PubConstant.customString;


        }


        /// <summary>
        /// ���澫�ȵ�hash��
        /// </summary>
        private Hashtable precisionHT = null;
        private void initialPrecision()
        {
            precisionHT = new Hashtable(20);

            foreach (hammergo.GlobalConfig.ParamInfo pi in hammergo.GlobalConfig.PubConstant.ConfigData.DefaultParamsList)
            {
                if (precisionHT.Contains(pi.Name) == false)
                {
                    precisionHT.Add(pi.Name, pi.Precision);
                }
            }
        }

        private void appSelector1_SearchExeClick(object sender, hammergo.CommonControl.AppsEventArgs e)
        {

            try
            {

                List<string> listNames = e.AppNameList;
                if (listNames.Count != 0)
                {
                    checkDate();
                    //�������б�
                    List<string> paramNamelist = new List<string>();


                    //�ж��Ƿ������ͬ�Ĳ���,�����������ƺ͸���
                    //��Ҫ���п��ٵ��ж�


                    string filterVariable = textBoxFilterVariable.Text.Trim();

                    List<AppIntegratedInfo> appInfoList = new List<AppIntegratedInfo>(listNames.Count);
                    Utility.Utility.isSameAppResult(listNames, paramNamelist, filterVariable,appInfoList);

                    fetchData(paramNamelist, listNames);
                }



            }
            catch (Exception ex)
            {
                Utility.Utility.log(ex);
                XtraMessageBox.Show(this, ex.Message, "����!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }

        }

        hammergo.ExportLib.ExcelExportStatistics exporter = null;
        private void fetchData(List<string> paramNamelist, List<string> listNames)
        {


            CommonControl.ListSelector.nameList = paramNamelist;

            CommonControl.ListSelector colForm = new hammergo.CommonControl.ListSelector();

            if (colForm.ShowDialog(this) == DialogResult.OK)
            {
                string selectedName = hammergo.CommonControl.ListSelector.selectedName;

                List<string> fetchExtreamNameList = new List<string>(paramNamelist);

                fetchExtreamNameList.Remove(selectedName);

                DateTime?[] dates = new DateTime?[] { null, null };
                DataTable table = prepareFetch(dates, fetchExtreamNameList);

                DateTime? startDate = dates[0];
                DateTime? endDate = dates[1];

                foreach (string appName in listNames)
                {
                    setExtreamum(appName, startDate, endDate, fetchExtreamNameList, table);
                }

                DataTable outTable = prepareOutput(fetchExtreamNameList, paramNamelist, selectedName);

                fillOutTable(fetchExtreamNameList, paramNamelist, selectedName, outTable, table);

                if (exporter == null)
                {
                    exporter = new hammergo.ExportLib.ExcelExportStatistics();
                }
                string dateFormat = customTextBox.Text.Trim().ToLower();
                DateTime currentDate = c1DateEdit3.DateTime;
                exporter.outputExcel(dateFormat, currentDate, fetchExtreamNameList, paramNamelist, selectedName, outTable);
            }


        }

        private void fillOutTable(List<string> fetchExtreamNameList, List<string> paramNamelist, string selectedName, DataTable outTable, DataTable extreamTable)
        {
            string sn = ""; //�����


            //�Ƿ��в���Ҫ���Ҽ�ֵ����
            bool concomitancy = fetchExtreamNameList.Count == paramNamelist.Count ? false : true;

            DateTime currentDate = c1DateEdit3.DateTime;



            foreach (DataRow row in extreamTable.Rows)
            {
                sn = row["�����"].ToString();


                AppIntegratedInfo currentInfo = AppIntegratedInfo.getAppInfoNearTime(sn, currentDate, hammergo.GlobalConfig.PubConstant.ConfigData.ChangeRangeNumForMonth);

                AppIntegratedInfo lastYearInfo = AppIntegratedInfo.getAppInfoNearTime(sn, currentDate.AddYears(-1), hammergo.GlobalConfig.PubConstant.ConfigData.ChangeRangeNumForYear);


              
                DataRow outRow = outTable.NewRow();

                outRow["�����"] = sn;
                foreach (string paramName in fetchExtreamNameList)
                {

                    string maxName = "���" + paramName;
                    string minName = "��С" + paramName;
                    string maxTimeName = "ʱ��-" + paramName + "0";
                    string minTimeName = "ʱ��-" + paramName + "1";

                    object maxobject = row[maxName];
                    object minobject = row[minName];

                    //������ֵ����Сֵ,�Լ���ص�ʱ��
                    outRow[maxName] = maxobject;
                    outRow[minName] = minobject;

                    outRow[maxTimeName] = row[maxTimeName];
                    outRow[minTimeName] = row[minTimeName];

                    //���仯��

                    string changeName = paramName + "���";

                    string currentName = "��ǰ" + paramName;
                    string yearName = paramName + "��仯";


                    outRow[changeName] = substract(maxobject, minobject);//����˱��


                 

                    object currentObject = null;

                    CalculateParam calcParam= null;
                    CalculateValue calcValue = null;
                    if (currentInfo != null)
                    {
                        calcParam = currentInfo.CalcParams.Find(delegate(CalculateParam item) { return item.ParamName == paramName; });
                        calcValue = currentInfo.CalcValues.Find(delegate(CalculateValue item) { return item.CalculateParamID == calcParam.CalculateParamID; });

                        if (calcValue != null)
                        {
                            //��䵱ǰֵ
                            currentObject = calcValue.Val;
                            if (calcValue.Val != null)
                            {
                                outRow[currentName] = calcValue.Val;
                            }

                        }
                        //calcParam = currentInfo.CalcParams.Find(delegate(CalculateParam item) { return item.ParamName == selectedName; });
                        //if (calcParam != null)
                        //{
                        //    calcValue = currentInfo.CalcValues.Find(delegate(CalculateValue item) { return item.CalculateParamID == calcParam.CalculateParamID; });

                        //    if (calcValue != null)
                        //    {
                        //        //��䵱ǰֵ

                        //        outRow[selectedName] = calcValue.Val;

                        //    }
                        //}
                    }

                    object lastYearObject = null;
                    if (lastYearInfo != null)
                    {
                        calcParam = lastYearInfo.CalcParams.Find(delegate(CalculateParam item) { return item.ParamName == paramName; });
                        calcValue = lastYearInfo.CalcValues.Find(delegate(CalculateValue item) { return item.CalculateParamID == calcParam.CalculateParamID; });
                        if (calcValue.Val != null)
                        {
                            lastYearObject = calcValue.Val;
                        }
                    }

                   

                    //�����仯

                    outRow[yearName] = substract(currentObject, lastYearObject);


                    //�������ֵ
                    if (concomitancy)
                    {

                        //��䵱ǰ����ֵ

                        if (currentInfo != null)
                        {
                            calcParam = currentInfo.CalcParams.Find(delegate(CalculateParam item) { return item.ParamName == selectedName; });
                            if (calcParam != null)
                            {
                                calcValue = currentInfo.CalcValues.Find(delegate(CalculateValue item) { return item.CalculateParamID == calcParam.CalculateParamID; });

                                if (calcValue != null)
                                {
                                    //��䵱ǰֵ
                                    if (calcValue.Val != null)
                                    {
                                        outRow["��ǰ" + selectedName + "-" + paramName] = calcValue.Val;
                                    }

                                }
                            }


                           // outRow["��ǰ" + selectedName + "-" + paramName] = currentRow[selectedName];

                        }

                        //��������ֵ�����ڰ���ֵ

                        //���ֵ�����ڵ�ʱ��
                        object max = row[maxTimeName];
                        object min = row[minTimeName];

                        AppIntegratedInfo maxAppInfo = null;
                        AppIntegratedInfo minAppInfo = null;

                        if (max is DateTime)
                        {
                            DateTime maxDate = (DateTime)max;

                            maxAppInfo = AppIntegratedInfo.getAppInfoNearTime(sn, maxDate, 1);
                            //Utility.getDataNearTime(maxDate, sn, 2, true).Tables["���"];
                        }


                        if (min is DateTime)
                        {
                            DateTime minDate = (DateTime)min;


                            minAppInfo = AppIntegratedInfo.getAppInfoNearTime(sn, minDate, 1);
                            //Utility.getDataNearTime(minDate, sn, 2, true).Tables["���"];
                        }

                        

                        if (maxAppInfo != null )
                        {
                            calcParam = maxAppInfo.CalcParams.Find(delegate(CalculateParam item) { return item.ParamName == selectedName; });
                            calcValue = maxAppInfo.CalcValues.Find(delegate(CalculateValue item) { return item.CalculateParamID == calcParam.CalculateParamID; });

                            if (calcValue != null)
                                outRow[selectedName + "-" + paramName + "0"] = calcValue.Val;
                        }

                        if (minAppInfo != null )
                        {
                            calcParam = minAppInfo.CalcParams.Find(delegate(CalculateParam item) { return item.ParamName == selectedName; });
                            calcValue = minAppInfo.CalcValues.Find(delegate(CalculateValue item) { return item.CalculateParamID == calcParam.CalculateParamID; });

                            if (calcValue != null)
                                outRow[selectedName + "-" + paramName + "1"] = calcValue.Val;
                        }


                    }

                }

                outRow.EndEdit();
                outTable.Rows.Add(outRow);

            }

        }

        /// <summary>
        /// �����������ı�Ľṹ
        /// </summary>
        /// <param name="fetchExtreamNameList">��Ҫ����ֵ��������</param>
        /// <param name="nameList">�������������гɹ�������</param>
        /// <param name="selectedName">ѡ�еĲ���Ҫ����ֵ������</param>
        /// <returns></returns>
        private DataTable prepareOutput(List<string> fetchExtreamNameList, List<string> paramNamelist, string selectedName)
        {
            DataTable table = new DataTable("���");

            DataColumn column = new DataColumn("�����", typeof(string));

            table.Columns.Add(column);

            table.PrimaryKey = new DataColumn[] { column };



            if (fetchExtreamNameList.Count == paramNamelist.Count)
            {
                //ȫ������Ҫ����ֵ

                foreach (string val in fetchExtreamNameList)
                {
                    table.Columns.Add("���" + val, typeof(double));
                    table.Columns.Add("ʱ��-" + val + "0", typeof(DateTime));

                    table.Columns.Add("��С" + val, typeof(double));
                    table.Columns.Add("ʱ��-" + val + "1", typeof(DateTime));

                    table.Columns.Add(val + "���", typeof(double));

                    table.Columns.Add("��ǰ" + val, typeof(double));

                    table.Columns.Add(val + "��仯", typeof(double));




                }


            }
            else
            {
                //��һ����������Ҫ����ֵ

                foreach (string val in fetchExtreamNameList)
                {
                    table.Columns.Add(selectedName + "-" + val + "0", typeof(double));//��������ֵ
                    table.Columns.Add("���" + val, typeof(double));
                    table.Columns.Add("ʱ��-" + val + "0", typeof(DateTime));

                    table.Columns.Add(selectedName + "-" + val + "1", typeof(double));//������С��ֵ
                    table.Columns.Add("��С" + val, typeof(double));
                    table.Columns.Add("ʱ��-" + val + "1", typeof(DateTime));

                    table.Columns.Add(val + "���", typeof(double));

                    table.Columns.Add("��ǰ" + selectedName + "-" + val, typeof(double));//����ֵ

                    table.Columns.Add("��ǰ" + val, typeof(double));

                    table.Columns.Add(val + "��仯", typeof(double));


                }

            }





            return table;

        }


        private void setExtreamum(string appName, DateTime? startDate, DateTime? endDate, List<string> fetchExtreamNameList, DataTable table)
        {
            AppIntegratedInfo appInfo = new AppIntegratedInfo(appName, 0, startDate, endDate);
            DataRow row = table.NewRow();

            row["�����"] = appName;

            foreach (CalculateParam cp in appInfo.CalcParams)
            {
                if (fetchExtreamNameList.Contains(cp.ParamName))
                {
                    List<CalculateValue> values = appInfo.CalcValues.FindAll(delegate(CalculateValue item) { return item.CalculateParamID == cp.CalculateParamID; });

                    CalculateValue maxValue = null;
                    CalculateValue minValue = null;

                    foreach (CalculateValue cv in values)
                    {
                        if (cv.Val.HasValue && Utility.Utility.isErrorValue(cv.Val.Value)==false)
                        {
                            if (maxValue == null)
                            {
                                maxValue = cv;
                            }
                            else if (cv.Val > maxValue.Val)
                            {
                                maxValue = cv;
                            }

                            if (minValue == null)
                            {
                                minValue = cv;
                            }
                            else if (cv.Val < minValue.Val)
                            {
                                minValue = cv;
                            }
                        }
                    }

                    string name = cp.ParamName;

                    if (maxValue != null)
                    {
                        row["ʱ��-" + name + "0"] = maxValue.Date;
                        row["���" + name] = maxValue.Val;
                    }

                    if (minValue != null)
                    {
                        row["ʱ��-" + name + "1"] = minValue.Date;
                        row["��С" + name] = minValue.Val;
                    }


                }
            }

            row.EndEdit();

            table.Rows.Add(row);


            //DataSet ds = new DataSet();

            //string snCondition = string.Format("�����='{0}'", sn);
            //string queryString = "";

            //OleDbCommand cmd = new OleDbCommand(queryString, dbcon);


            //OleDbDataAdapter oda = new OleDbDataAdapter();
            //oda.SelectCommand = cmd;


            //oda.SelectCommand.CommandText = string.Format("select ID, ������,��������,���ʽ,С��λ��,������� from ������ where {0} order by �������", snCondition);

            //oda.Fill(ds, "������");

            //DataTable tempTable = new DataTable("temp");



            //DataRow row = table.NewRow();

            //row["�����"] = sn;

            //foreach (DataRow prow in ds.Tables["������"].Rows)
            //{
            //    string name = prow["������"].ToString();

            //    if (nameList.Contains(name))
            //    {
            //        tempTable.Rows.Clear();

            //        //���ֵ
            //        setQueryString(oda, start, end, "desc", (int)prow["ID"]);

            //        oda.Fill(tempTable);

            //        DataRow tempRow = null;

            //        if (tempTable.Rows.Count >= 1)
            //        {
            //            tempRow = tempTable.Rows[0];

            //            row["ʱ��-" + name + "0"] = tempRow["ʱ��"];
            //            row["���" + name] = tempRow["ֵ"];
            //        }


            //        tempTable.Rows.Clear();

            //        //��Сֵ
            //        setQueryString(oda, start, end, "asc", (int)prow["ID"]);

            //        oda.Fill(tempTable);

            //        if (tempTable.Rows.Count >= 1)
            //        {
            //            tempRow = tempTable.Rows[0];


            //            row["ʱ��-" + name + "1"] = tempRow["ʱ��"];
            //            row["��С" + name] = tempRow["ֵ"];
            //        }
            //    }
            //}


            //row.EndEdit();

            //table.Rows.Add(row);
        }

        private DataTable prepareFetch(DateTime?[] dates, List<string> fetchExtreamNameList)
        {
            DateTime dateTime;


            dateTime = c1DateEdit2.DateTime;

            dateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59, 0);

            //endDate = new MyDate(dateTime);
            dates[1] = dateTime;

            dateTime = c1DateEdit1.DateTime;

            dateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0, 0);

            //startDate = new MyDate(dateTime);
            dates[0] = dateTime;


            DataTable table = new DataTable("���");

            DataColumn column = new DataColumn("�����", typeof(string));

            table.Columns.Add(column);

            table.PrimaryKey = new DataColumn[] { column };

            foreach (string name in fetchExtreamNameList)
            {
                table.Columns.Add("ʱ��-" + name + "0", typeof(DateTime));

                table.Columns.Add("���" + name, typeof(double));

                table.Columns.Add("ʱ��-" + name + "1", typeof(DateTime));

                table.Columns.Add("��С" + name, typeof(double));

            }



            return table;
        }

        /// <summary>
        /// ����û�����������Ƿ�Ϻ�����,�׳��쳣
        /// </summary>
        /// <returns></returns>
        private void checkDate()
        {
            if (c1DateEdit1.EditValue == null || c1DateEdit2.EditValue == null || c1DateEdit3.EditValue == null)
            {
                throw new Exception("����������");
            }
            //�����������

            if (c1DateEdit1.DateTime > c1DateEdit2.DateTime)
            {
                throw new Exception("��ʼ���ڲ��ܴ��ڽ�������");
            }



        }

        /// <summary>
        /// ִ��obj1��ȥobj2�Ĳ���,��������ֵ,���쳣ֵ,��ն���
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        public  object substract(object obj1, object obj2)
        {
            object val = DBNull.Value;

            //���Ƿ���ڿ�ֵ
            if (obj1 is double && obj2 is double)
            {
                if (Utility.Utility.isErrorValue((double)obj1) || Utility.Utility.isErrorValue((double)obj1))
                {
                    //�����쳣ֵ
                    val = hammergo.GlobalConfig.PubConstant.ConfigData.ErrorValList[0];
                }
                else 
                {
                    //��û�п�ֵ��Ҳû���쳣ֵ ,ͬʱҲ��double���͵���

                    val = (double)obj1 - (double)obj2;
                }
            }

            return val;

        }




        public event EventHandler<hammergo.Utility.AppSearchEventArgs> ShowDataEvent;
        private void appSelector1_ShowDataEvent(object sender,hammergo.Utility.AppSearchEventArgs e)
        {
            if (ShowDataEvent != null)
            {
                ShowDataEvent(sender,e);
            }
        }

        private void pasteDateMenuItem1_Click(object sender, EventArgs e)
        {
            Utility.Utility.handlePasteInDateEdit(c1DateEdit1);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Utility.Utility.handlePasteInDateEdit(c1DateEdit2);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Utility.Utility.handlePasteInDateEdit(c1DateEdit3);
        }



        #region ICustomDispose ��Ա

        public void CustomDispose()
        {
            if (exporter != null)
            {
                exporter.QuitExcel();
            }
        }

        #endregion
    }
}
