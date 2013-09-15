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
        /// 保存精度的hash表
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
                    //参数名列表
                    List<string> paramNamelist = new List<string>();


                    //判断是否具有相同的参数,包括参数名称和个数
                    //需要进行快速的判断


                    string filterVariable = textBoxFilterVariable.Text.Trim();

                    List<AppIntegratedInfo> appInfoList = new List<AppIntegratedInfo>(listNames.Count);
                    Utility.Utility.isSameAppResult(listNames, paramNamelist, filterVariable,appInfoList);

                    fetchData(paramNamelist, listNames);
                }



            }
            catch (Exception ex)
            {
                Utility.Utility.log(ex);
                XtraMessageBox.Show(this, ex.Message, "错误!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
            string sn = ""; //测点编号


            //是否有不需要查找极值的列
            bool concomitancy = fetchExtreamNameList.Count == paramNamelist.Count ? false : true;

            DateTime currentDate = c1DateEdit3.DateTime;



            foreach (DataRow row in extreamTable.Rows)
            {
                sn = row["测点编号"].ToString();


                AppIntegratedInfo currentInfo = AppIntegratedInfo.getAppInfoNearTime(sn, currentDate, hammergo.GlobalConfig.PubConstant.ConfigData.ChangeRangeNumForMonth);

                AppIntegratedInfo lastYearInfo = AppIntegratedInfo.getAppInfoNearTime(sn, currentDate.AddYears(-1), hammergo.GlobalConfig.PubConstant.ConfigData.ChangeRangeNumForYear);


              
                DataRow outRow = outTable.NewRow();

                outRow["测点编号"] = sn;
                foreach (string paramName in fetchExtreamNameList)
                {

                    string maxName = "最大" + paramName;
                    string minName = "最小" + paramName;
                    string maxTimeName = "时间-" + paramName + "0";
                    string minTimeName = "时间-" + paramName + "1";

                    object maxobject = row[maxName];
                    object minobject = row[minName];

                    //填充最大值和最小值,以及相关的时间
                    outRow[maxName] = maxobject;
                    outRow[minName] = minobject;

                    outRow[maxTimeName] = row[maxTimeName];
                    outRow[minTimeName] = row[minTimeName];

                    //填充变化列

                    string changeName = paramName + "变幅";

                    string currentName = "当前" + paramName;
                    string yearName = paramName + "年变化";


                    outRow[changeName] =Utility.Utility.substract(maxobject, minobject);//求得了变幅


                 

                    object currentObject = null;

                    CalculateParam calcParam= null;
                    CalculateValue calcValue = null;
                    if (currentInfo != null)
                    {
                        calcParam = currentInfo.CalcParams.Find(delegate(CalculateParam item) { return item.ParamName == paramName; });
                        calcValue = currentInfo.CalcValues.Find(delegate(CalculateValue item) { return item.CalculateParamID == calcParam.CalculateParamID; });

                        if (calcValue != null)
                        {
                            //填充当前值
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
                        //        //填充当前值

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

                   

                    //填充年变化

                    outRow[yearName] = Utility.Utility.substract(currentObject, lastYearObject);


                    //处理伴随值
                    if (concomitancy)
                    {

                        //填充当前伴随值

                        if (currentInfo != null)
                        {
                            calcParam = currentInfo.CalcParams.Find(delegate(CalculateParam item) { return item.ParamName == selectedName; });
                            if (calcParam != null)
                            {
                                calcValue = currentInfo.CalcValues.Find(delegate(CalculateValue item) { return item.CalculateParamID == calcParam.CalculateParamID; });

                                if (calcValue != null)
                                {
                                    //填充当前值
                                    if (calcValue.Val != null)
                                    {
                                        outRow["当前" + selectedName + "-" + paramName] = calcValue.Val;
                                    }

                                }
                            }


                           // outRow["当前" + selectedName + "-" + paramName] = currentRow[selectedName];

                        }

                        //填充伴随最值出现在伴随值

                        //最大值出现在的时间
                        object max = row[maxTimeName];
                        object min = row[minTimeName];

                        AppIntegratedInfo maxAppInfo = null;
                        AppIntegratedInfo minAppInfo = null;

                        if (max is DateTime)
                        {
                            DateTime maxDate = (DateTime)max;

                            maxAppInfo = AppIntegratedInfo.getAppInfoNearTime(sn, maxDate, 1);
                            //Utility.getDataNearTime(maxDate, sn, 2, true).Tables["组合"];
                        }


                        if (min is DateTime)
                        {
                            DateTime minDate = (DateTime)min;


                            minAppInfo = AppIntegratedInfo.getAppInfoNearTime(sn, minDate, 1);
                            //Utility.getDataNearTime(minDate, sn, 2, true).Tables["组合"];
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
        /// 构成最后输出的表的结构
        /// </summary>
        /// <param name="fetchExtreamNameList">需要求最值的列名表</param>
        /// <param name="nameList">该类仪器的所有成果列名表</param>
        /// <param name="selectedName">选中的不需要求最值的列名</param>
        /// <returns></returns>
        private DataTable prepareOutput(List<string> fetchExtreamNameList, List<string> paramNamelist, string selectedName)
        {
            DataTable table = new DataTable("组合");

            DataColumn column = new DataColumn("测点编号", typeof(string));

            table.Columns.Add(column);

            table.PrimaryKey = new DataColumn[] { column };



            if (fetchExtreamNameList.Count == paramNamelist.Count)
            {
                //全部都需要求最值

                foreach (string val in fetchExtreamNameList)
                {
                    table.Columns.Add("最大" + val, typeof(double));
                    table.Columns.Add("时间-" + val + "0", typeof(DateTime));

                    table.Columns.Add("最小" + val, typeof(double));
                    table.Columns.Add("时间-" + val + "1", typeof(DateTime));

                    table.Columns.Add(val + "变幅", typeof(double));

                    table.Columns.Add("当前" + val, typeof(double));

                    table.Columns.Add(val + "年变化", typeof(double));




                }


            }
            else
            {
                //有一个列名不需要求最值

                foreach (string val in fetchExtreamNameList)
                {
                    table.Columns.Add(selectedName + "-" + val + "0", typeof(double));//伴随最大的值
                    table.Columns.Add("最大" + val, typeof(double));
                    table.Columns.Add("时间-" + val + "0", typeof(DateTime));

                    table.Columns.Add(selectedName + "-" + val + "1", typeof(double));//伴随最小的值
                    table.Columns.Add("最小" + val, typeof(double));
                    table.Columns.Add("时间-" + val + "1", typeof(DateTime));

                    table.Columns.Add(val + "变幅", typeof(double));

                    table.Columns.Add("当前" + selectedName + "-" + val, typeof(double));//伴随值

                    table.Columns.Add("当前" + val, typeof(double));

                    table.Columns.Add(val + "年变化", typeof(double));


                }

            }





            return table;

        }


        private void setExtreamum(string appName, DateTime? startDate, DateTime? endDate, List<string> fetchExtreamNameList, DataTable table)
        {
            AppIntegratedInfo appInfo = new AppIntegratedInfo(appName, 0, startDate, endDate);
            DataRow row = table.NewRow();

            row["测点编号"] = appName;

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
                        row["时间-" + name + "0"] = maxValue.Date;
                        row["最大" + name] = maxValue.Val;
                    }

                    if (minValue != null)
                    {
                        row["时间-" + name + "1"] = minValue.Date;
                        row["最小" + name] = minValue.Val;
                    }


                }
            }

            row.EndEdit();

            table.Rows.Add(row);


            //DataSet ds = new DataSet();

            //string snCondition = string.Format("测点编号='{0}'", sn);
            //string queryString = "";

            //OleDbCommand cmd = new OleDbCommand(queryString, dbcon);


            //OleDbDataAdapter oda = new OleDbDataAdapter();
            //oda.SelectCommand = cmd;


            //oda.SelectCommand.CommandText = string.Format("select ID, 参数名,参数符号,表达式,小数位数,计算序号 from 计算量 where {0} order by 计算序号", snCondition);

            //oda.Fill(ds, "计算量");

            //DataTable tempTable = new DataTable("temp");



            //DataRow row = table.NewRow();

            //row["测点编号"] = sn;

            //foreach (DataRow prow in ds.Tables["计算量"].Rows)
            //{
            //    string name = prow["参数名"].ToString();

            //    if (nameList.Contains(name))
            //    {
            //        tempTable.Rows.Clear();

            //        //最大值
            //        setQueryString(oda, start, end, "desc", (int)prow["ID"]);

            //        oda.Fill(tempTable);

            //        DataRow tempRow = null;

            //        if (tempTable.Rows.Count >= 1)
            //        {
            //            tempRow = tempTable.Rows[0];

            //            row["时间-" + name + "0"] = tempRow["时间"];
            //            row["最大" + name] = tempRow["值"];
            //        }


            //        tempTable.Rows.Clear();

            //        //最小值
            //        setQueryString(oda, start, end, "asc", (int)prow["ID"]);

            //        oda.Fill(tempTable);

            //        if (tempTable.Rows.Count >= 1)
            //        {
            //            tempRow = tempTable.Rows[0];


            //            row["时间-" + name + "1"] = tempRow["时间"];
            //            row["最小" + name] = tempRow["值"];
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


            DataTable table = new DataTable("组合");

            DataColumn column = new DataColumn("测点编号", typeof(string));

            table.Columns.Add(column);

            table.PrimaryKey = new DataColumn[] { column };

            foreach (string name in fetchExtreamNameList)
            {
                table.Columns.Add("时间-" + name + "0", typeof(DateTime));

                table.Columns.Add("最大" + name, typeof(double));

                table.Columns.Add("时间-" + name + "1", typeof(DateTime));

                table.Columns.Add("最小" + name, typeof(double));

            }



            return table;
        }

        /// <summary>
        /// 检查用户输入的日期是否合乎规则,抛出异常
        /// </summary>
        /// <returns></returns>
        private void checkDate()
        {
            if (c1DateEdit1.EditValue == null || c1DateEdit2.EditValue == null || c1DateEdit3.EditValue == null)
            {
                throw new Exception("请输入日期");
            }
            //检查其它规则

            if (c1DateEdit1.DateTime > c1DateEdit2.DateTime)
            {
                throw new Exception("起始日期不能大于结束日期");
            }



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



        #region ICustomDispose 成员

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
