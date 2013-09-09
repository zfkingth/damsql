using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using hammergo.Model;
using DevExpress.XtraTreeList;
using hammergo.Utility;
using System.Collections;
using DevExpress.XtraGrid.Views.Grid;
using hammergo.GlobalConfig;

namespace hammergo.DataSearch
{
    public partial class MonthReport : DevExpress.XtraEditors.XtraUserControl, ICustomDispose, hammergo.Utility.IShowAppData
    {

        public MonthReport()
        {
            InitializeComponent();
        }

        public event EventHandler<hammergo.Utility.AppSearchEventArgs> ShowDataEvent;

        private void MonthReport_Load(object sender, EventArgs e)
        {
            appSelector1.initial();
            initialPrecision();
            dateEditBase.DateTime = System.DateTime.Now;
            dateEditBase.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            dateEditBase.Properties.DisplayFormat.FormatString = PubConstant.customString;

            GridView gv = gridControlDates.MainView as GridView;
            gv.Columns[0].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            gv.Columns[0].DisplayFormat.FormatString = PubConstant.customString;

            this.repositoryItemDateEdit4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.repositoryItemDateEdit4.DisplayFormat.FormatString = PubConstant.customString;
            this.repositoryItemDateEdit4.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.repositoryItemDateEdit4.EditFormat.FormatString = PubConstant.customString;


        }



        private void appSelector1_ShowDataEvent(object sender, hammergo.Utility.AppSearchEventArgs e)
        {
            if (ShowDataEvent != null)
            {
                ShowDataEvent(sender, e);
            }
        }

        private void appSelector1_SearchExeClick(object sender, hammergo.CommonControl.AppsEventArgs e)
        {

            try
            {
                gridControlResult.SuspendLayout();
                List<string> listNames = e.AppNameList;
                if (listNames.Count != 0)
                {
                    //参数名列表
                    List<string> paramNamelist = new List<string>();


                    //判断是否具有相同的参数,包括参数名称和个数
                    //需要进行快速的判断


                    string filterVariable = textBoxFilterVariable.Text.Trim();

                    List<AppIntegratedInfo> appInfoList = new List<AppIntegratedInfo>(listNames.Count);
                    Utility.Utility.isSameAppResult(listNames, paramNamelist, filterVariable,appInfoList);



                    gridControlResult.DataSource = searchData(paramNamelist,appInfoList);

                    gridControlResult.MainView.PopulateColumns();
                }
                GridView gv = gridControlResult.MainView as GridView;

                formatGridView(gv);
                setDateColumnVisible(gv);
                gv.OptionsView.ColumnAutoWidth = false;
                gv.BestFitMaxRowCount = 1;
                gv.BestFitColumns();
                gridControlResult.ResumeLayout();
         

            }
            catch (Exception ex)
            {
                Utility.Utility.log(ex);
                XtraMessageBox.Show(this, ex.Message, "错误!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }



        }


        /// <summary>
        /// 数据组数，每一个时间都是一组
        /// </summary>
        int groupCount = 0;
        List<string> paramNameList = null;
        private DataTable searchData(List<string> paramNameList,List<AppIntegratedInfo> appInfoList)
        {
            // List<DataTable> tableList = new List<DataTable>(100);

            DataTable resultTable = null;
            this.groupCount = 0;
            this.paramNameList = paramNameList;

            if (rYue.Checked)
            {
                string numString = ddlLatest.Text;
                groupCount = int.Parse(numString);

                resultTable = createResultTableSchema(paramNameList, groupCount);

                DateTime dateTime = dateEditBase.DateTime;

                dateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59, 0);

                //DateTime? endDate = ddateTime

                foreach (AppIntegratedInfo appInfo  in appInfoList)
                {
                    // AppResultInfo ari = UtilityGetData.getResult(app, yueNum, null, endDate, true);
                    //= new AppIntegratedInfo(appName, groupCount, null, dateTime);
                    appInfo.Reset(groupCount, null, dateTime);

                    DataRow row = resultTable.NewRow();
                    row[appFiledString] = appInfo.appName;

                    resultTable.Rows.Add(row);

                    if (appInfo.CalcValues.Count != 0)
                    {
                        appInfo.CalcValues.Sort(new Utility.CalculateValueReserveComparer());
                        //需要对calcValues进行排序，规则是以时间顺序排列
                        for (int i = 0; i < paramNameList.Count; i++)
                        {
                            string paramName = paramNameList[i];

                            CalculateParam cp = appInfo.CalcParams.Find(delegate(CalculateParam item) { return item.ParamName == paramName; });
                            //cp 肯定存在，因为查询是根据cp来做的，而且在查询之前有对所有测点的参数进行检查
                            List<CalculateValue> listValues = appInfo.CalcValues.FindAll(delegate(CalculateValue item) { return item.CalculateParamID == cp.CalculateParamID; });

                            for (int j = 0; j < listValues.Count; j++)
                            {
                                //从最后一组数据开始填充
                                string feildName = paramName + (groupCount - 1 - j);
                                if (listValues[j].Val != null)
                                {
                                    row[feildName] = listValues[j].Val;
                                }

                                string timeName = PubConstant.timeColumnName + (groupCount - 1 - j);
                                row[timeName] = listValues[j].Date;
                            }


                        }
                    }


                }


            }
            else if (rDate.Checked)
            {

                CaseInsensitiveComparer comparer = new CaseInsensitiveComparer();

                SortedList valueList = new SortedList(comparer, 6);

                if (dSDates.Dates.Rows.Count == 0)
                {
                    throw new Exception("没有选择时间,请在'日期时间'里的表中选取时间!");
                }

                foreach (DSDates.DatesRow row in dSDates.Dates.Rows)
                {
                    valueList.Add(row.日期时间, null);
                }

                groupCount = valueList.Count;
                resultTable = createResultTableSchema(paramNameList, groupCount);

                foreach (AppIntegratedInfo appItem in appInfoList)
                {
                    DataRow row = resultTable.NewRow();
                    row[appFiledString] = appItem.appName;

                    resultTable.Rows.Add(row);


                    for (int j = 0; j < groupCount; j++)
                    {
                        DateTime dateTime = (DateTime)valueList.GetKey(j);
                        AppIntegratedInfo appInfo = appItem.getNearTimeData(dateTime, PubConstant.ConfigData.DaysNumForNear); //AppIntegratedInfo.getAppInfoNearTime(appName, dateTime, PubConstant.ConfigData.DaysNumForNear);
                        if (appInfo != null)
                        {
                            for (int i = 0; i < paramNameList.Count; i++)
                            {
                                string paramName = paramNameList[i];

                                CalculateParam cp = appInfo.CalcParams.Find(delegate(CalculateParam item) { return item.ParamName == paramName; });
                                //cp 肯定存在，因为查询是根据cp来做的，而且在查询之前有对所有测点的参数进行检查
                                CalculateValue cv = appInfo.CalcValues.Find(delegate(CalculateValue item) { return item.CalculateParamID == cp.CalculateParamID; });

                                if (cv != null)
                                {
                                    string feildName = paramName + j;
                                    if (cv.Val != null)
                                    {
                                        row[feildName] = cv.Val;
                                    }

                                    string timeName = PubConstant.timeColumnName + j;
                                    row[timeName] = cv.Date;
                                }



                            }
                        }
                        else
                        {
                            string timeName = PubConstant.timeColumnName + j;
                            row[timeName] = dateTime;
                        }
                    }

                }


            }

            setChanges(resultTable, paramNameList, appInfoList, groupCount, suffixString);
            resultTable.AcceptChanges();


            return resultTable;
            //(gridControlResult.MainView as DevExpress.XtraGrid.Views.Grid.GridView).BestFitColumns();




        }


        /// <summary>
        /// 根据参数名创建结果表的构架
        /// </summary>
        /// <param name="paramNameList">参数列表</param>
        /// <param name="datesCnt">时间列表的个数,每个时间对应一组数据</param>
        private DataTable createResultTableSchema(List<string> paramNameList, int datesCnt)
        {
            if (checkBoxMonthChange.Checked)
                suffixString = "月变化";
            else suffixString = "变化";


            DataTable resultTable = new DataTable("result");

            DataColumn column = new DataColumn(appFiledString, typeof(string));

            resultTable.Columns.Add(column);

            resultTable.PrimaryKey = new DataColumn[] { column };


            htColumnNameToParamNameMaps = new Hashtable(datesCnt + paramNameList.Count * 2);
            //产生列
            for (int i = 0; i < datesCnt; i++)
            {
                column = new DataColumn(PubConstant.timeColumnName + i, typeof(DateTime));
                resultTable.Columns.Add(column);

                foreach (string s in paramNameList)
                {
                    column = new DataColumn(s + i, typeof(double));
                    htColumnNameToParamNameMaps.Add(s + i, s);
                    resultTable.Columns.Add(column);
                }


            }

            foreach (string s in paramNameList)
            {
                column = new DataColumn(s + suffixString, typeof(double));
                htColumnNameToParamNameMaps.Add(s + suffixString, s);
                resultTable.Columns.Add(column);
            }

            foreach (string s in paramNameList)
            {
                column = new DataColumn(s + "年变化", typeof(double));
                htColumnNameToParamNameMaps.Add(s + "年变化", s);
                resultTable.Columns.Add(column);
            }

            return resultTable;
        }


        /// <summary>
        /// 检索结果表里的列表与测点计算数据的参数名对表关系表
        /// </summary>
        Hashtable htColumnNameToParamNameMaps = null;

        private string suffixString = "";
        private const string appFiledString = "测点编号";


        private void setChanges(DataTable resultTable, List<string> paramNameList, List<AppIntegratedInfo> appInfoList, int groupCount, string suffixString)
        {
            string numString = ddlBaseColumnIndex.Text.Trim();
            int colNum = int.Parse(numString);

            for (int i = 0; i < appInfoList.Count; i++)
            {
                string appName = appInfoList[i].appName;
                DataRow row = resultTable.Rows[i];
                AppIntegratedInfo contrastAppInfo = null;
                object lastTimeValue = row[PubConstant.timeColumnName + (groupCount - 1)];
                if (checkBoxMonthChange.Checked == false)
                {
                    //求相对变化
                    if (colNum >= groupCount)
                    {
                        throw new Exception("指定的求变化化超出有效范围!");
                    }
                    object timeValue = row[PubConstant.timeColumnName + colNum];
                    if (timeValue != null && timeValue != System.DBNull.Value)
                    {

                        DateTime time = (DateTime)timeValue;
                        //因为往row里填的是实际的日期，所以可以使用精确的查找
                        //contrastAppInfo = new AppIntegratedInfo(appName, 1, time, time);
                        contrastAppInfo=appInfoList[i].getNearTimeData(time,0);
                        
                        
                    }
                }
                else
                {

                    if (lastTimeValue != null && lastTimeValue != System.DBNull.Value)
                    {
                        DateTime time = (DateTime)lastTimeValue;
                        DateTime preConcertTime = time.AddMonths(-1);
                        //contrastAppInfo = AppIntegratedInfo.getAppInfoNearTime(appName, preConcertTime, hammergo.GlobalConfig.PubConstant.ConfigData.ChangeRangeNumForMonth);

                        contrastAppInfo = appInfoList[i].getNearTimeData(preConcertTime, hammergo.GlobalConfig.PubConstant.ConfigData.ChangeRangeNumForMonth);
                    }

                }

                calcChanges(contrastAppInfo, suffixString, paramNameList, row, groupCount);

                if (lastTimeValue != null && lastTimeValue != System.DBNull.Value)
                {
                    DateTime time = (DateTime)lastTimeValue;
                    DateTime preConcertTime = time.AddYears(-1);
                    //contrastAppInfo = AppIntegratedInfo.getAppInfoNearTime(appName, preConcertTime, hammergo.GlobalConfig.PubConstant.ConfigData.ChangeRangeNumForYear);
                    contrastAppInfo = appInfoList[i].getNearTimeData(preConcertTime, hammergo.GlobalConfig.PubConstant.ConfigData.ChangeRangeNumForYear);
                    calcChanges(contrastAppInfo, "年变化", paramNameList, row, groupCount);
                }


            }
        }


        /// <summary>
        /// 求变化
        /// </summary>
        /// <param name="appInfo">需要对比数据的信息</param>
        /// <param name="suffixString">变化后缀</param>
        /// <param name="paramNameList">参数名称</param>
        /// <param name="row">结果表里的行</param>
        /// <param name="groupCount">数组组的个数,每个时间为一组</param>
        private void calcChanges(AppIntegratedInfo appInfo, string suffixString, List<string> paramNameList, DataRow row, int groupCount)
        {
            if (appInfo != null)
            {
                foreach (string paramName in paramNameList)
                {
                    CalculateParam calcParam = appInfo.CalcParams.Find(delegate(CalculateParam item) { return item.ParamName == paramName; });

                    CalculateValue calcValue = appInfo.CalcValues.Find(delegate(CalculateValue item) { return item.CalculateParamID == calcParam.CalculateParamID; });

                    if (calcValue != null)
                    {


                        object lastVal = row[paramName + (groupCount - 1)];

                        if (lastVal != DBNull.Value)
                        {
                            if (calcValue.Val == null)
                            {
                                row[paramName + suffixString] = DBNull.Value;
                            }
                            else
                            {
                                double valChild = calcValue.Val.Value;
                                double lastValue = (double)lastVal;
                                if (Utility.Utility.isErrorValue(valChild) || Utility.Utility.isErrorValue(lastValue))//是错误值
                                {
                                    row[paramName + suffixString] = hammergo.GlobalConfig.PubConstant.ConfigData.ErrorValList[0];
                                }
                                else
                                {
                                    //byte precisionNum = defaultPrecision; 
                                    //if (precisionHT.ContainsKey(calcParam.ParamName))
                                    //{
                                    //    precisionNum = (byte)precisionHT[calcParam.ParamName];
                                    //}
                                    //else if(calcParam.PrecisionNum.HasValue)
                                    //{
                                    //    precisionNum = calcParam.PrecisionNum.Value;
                                    //}
                                    row[paramName + suffixString] = lastValue - valChild;
                                }
                            }
                        }
                    }

                }
            }
        }




        private void gridView2_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.ColumnType == typeof(double))
            {
                e.DisplayText = convertDisplayText(e.CellValue, e.Column);

            }
        }

        private string convertDisplayText(object cellValue, DevExpress.XtraGrid.Columns.GridColumn column)
        {

            string ret = "";
            if (cellValue == null || cellValue.ToString().Trim().Length == 0)
            {
                ret = hammergo.GlobalConfig.PubConstant.ConfigData.NoDataConvertString;
            }
            else if (cellValue.ToString() == double.NaN.ToString())
            {
                ret = "";
            }
            else if (Utility.Utility.isErrorValue((double)cellValue))
            {
                ret = hammergo.GlobalConfig.PubConstant.ConfigData.ErrorConvertString;
            }
            else if (column.Tag != null)
            {

                byte precision = (byte)column.Tag;

                double val = (double)cellValue;
                val = Utility.Utility.round(val, precision);
                ret = val.ToString(string.Format("f{0}", precision));
            }

            return ret;
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




        //默认2位小数
        private const byte defaultPrecision = 2;
        private void formatGridView(GridView gv)
        {
           
            foreach (DevExpress.XtraGrid.Columns.GridColumn column in gv.Columns)
            {

                if (column.ColumnType == typeof(double))
                {
                    column.Tag = defaultPrecision;
                    if (htColumnNameToParamNameMaps.ContainsKey(column.FieldName))
                    {
                        string paramName = htColumnNameToParamNameMaps[column.FieldName] as string;
                        if (precisionHT.ContainsKey(paramName))
                        {
                            byte precisionNum = (byte)precisionHT[paramName];
                            column.Tag = precisionNum;
                        }
                    }

                }

                if (column.ColumnType == typeof(DateTime))
                {
                    //if (dateTimeWidth == 0)
                    //{
                    //    dateTimeWidth = TextRenderer.MeasureText("2009-12-22", gridView2.Appearance.Row.Font).Width ;
                    //}
                    //column.Width = dateTimeWidth;

                    column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    column.DisplayFormat.FormatString = PubConstant.shortString;


                }
                else
                {
                   // column.Width = TextRenderer.MeasureText(column.Caption + "   ", gridView2.Appearance.HeaderPanel.Font).Width;
                }

            }
        }

        private void setDateColumnVisible(GridView gv)
        {
            foreach (DevExpress.XtraGrid.Columns.GridColumn column in gv.Columns)
            {
                if (column.ColumnType == typeof(DateTime))
                {
                    column.Visible = checkBoxShowDate.Checked;
                }
            }

        }

        private void 复制Item_Click(object sender, EventArgs e)
        {
            GridView gv = gridControlResult.MainView as GridView;

            System.Text.StringBuilder strTemp = new System.Text.StringBuilder(2560); ; //string to be copied to the clipboard

            DevExpress.XtraGrid.Views.Base.GridCell[] cells = gv.GetSelectedCells();



            //最小索引和最大索引
            int visibleIndexMin = int.MaxValue;
            int visibleIndexMax = int.MinValue;

            foreach (DevExpress.XtraGrid.Views.Base.GridCell cell in cells)
            {
                int visibleIndex = cell.Column.VisibleIndex;

                if (visibleIndexMin > visibleIndex)
                {
                    visibleIndexMin = visibleIndex;
                }

                if (visibleIndexMax < visibleIndex)
                {
                    visibleIndexMax = visibleIndex;
                }
            }
            const string CellDelimiter = "\t";
            const string LineDelimiter = "\r\n";

            int cellIndex = 0;
            foreach (int selRowHandle in gv.GetSelectedRows())
            {
                //行
                for (int j = visibleIndexMin; j <= visibleIndexMax; j++)
                {
                    //列
                    if (cells[cellIndex].Column.VisibleIndex == j)
                    {
                        DevExpress.XtraGrid.Views.Base.GridCell cell = cells[cellIndex];
                        if (cell.Column.ColumnType == typeof(double))
                        {
                            strTemp.Append(convertDisplayText(gv.GetRowCellValue(selRowHandle, cell.Column), cell.Column));
                        }
                        else
                        {
                            strTemp.Append(gv.GetRowCellDisplayText(selRowHandle, cell.Column));
                        }

                        cellIndex++;
                    }

                    strTemp.Append(CellDelimiter);


                }

                // strTemp.Append(LineDelimiter);
                strTemp.Replace(CellDelimiter, LineDelimiter, strTemp.Length - 1, 1);
            }

            string outstring = strTemp.ToString();


            System.Windows.Forms.Clipboard.SetDataObject(outstring, true, 3, 300);


        }

        private void checkBoxShowDate_CheckedChanged(object sender, EventArgs e)
        {
            setDateColumnVisible(gridControlResult.MainView as GridView);
        }

        DevExpress.XtraGrid.Columns.GridColumn currentColumn = null;
        private void gridCMS_Opening(object sender, CancelEventArgs e)
        {
            GridView gv = gridControlResult.MainView as GridView;
            currentColumn = null;
            bool changeMenuEnable = false;
            bool precisionMenuEnable = false;
            if (gv.SelectedRowsCount > 0)
            {
                DevExpress.XtraGrid.Views.Base.GridCell cell = gv.GetSelectedCells()[0];
                if (cell.Column.ColumnType == typeof(double))
                {
                    //可设置小数位数
                    precisionMenuEnable = true;

                    currentColumn = cell.Column;

                    foreach (string paramName in paramNameList)
                    {
                        if (currentColumn.FieldName == paramName + (groupCount - 1))
                        {
                            changeMenuEnable = true;
                            break;
                        }
                    }





                }
            }

            计算变化ToolStripMenuItem.Enabled = changeMenuEnable;
            小数位数ToolStripMenuItem.Enabled = precisionMenuEnable;
        }


        private void pasteDateMenuItem1_Click(object sender, EventArgs e)
        {
            Utility.Utility.handlePasteInDateEdit(dateEditBase);
        }




        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Utility.Utility.handlePasteInGridView(gridControlDates.MainView as GridView);

        }

        private void 增加ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentColumn != null)
            {
                string paramName = htColumnNameToParamNameMaps[currentColumn.FieldName] as string;

                if (paramName != null)
                {
                    object obj = precisionHT[paramName];
                    if (obj != null && obj is byte)
                    {
                        byte val = (byte)obj;
                        val++;
                        precisionHT[paramName] = val;

                        formatGridView(gridControlResult.MainView as GridView);
                        gridControlResult.Refresh();
                    }
                }
            }
        }

        private void 基准值ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentColumn != null)
            {
                //小于等于基准值，才求变化量
                double baseVal = (double)BaseValueControl.Value;
                string paramName = htColumnNameToParamNameMaps[currentColumn.FieldName] as string;

                if (paramName != null)
                {
                    DataTable resultTable = gridControlResult.DataSource as DataTable;
                    foreach (DataRow row in resultTable.Rows)
                    {
                        object val = row[currentColumn.FieldName];
                        if (val != null && val is double)
                        {
                            double dv = (double)val;
                            if (dv > baseVal)
                            {
                                row[paramName + suffixString] = double.NaN;
                                row[paramName + "年变化"] = double.NaN;
                            }
                        }
                    }
                }
            }
        }

        private void 基准值ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (currentColumn != null)
            {
                //小于等于基准值，才求变化量
                double baseVal = (double)BaseValueControl.Value;
                string paramName = htColumnNameToParamNameMaps[currentColumn.FieldName] as string;

                if (paramName != null)
                {
                    DataTable resultTable = gridControlResult.DataSource as DataTable;
                    foreach (DataRow row in resultTable.Rows)
                    {
                        object val = row[currentColumn.FieldName];
                        if (val != null && val is double)
                        {
                            double dv = (double)val;
                            if (dv < baseVal)
                            {
                                row[paramName + suffixString] = double.NaN;
                                row[paramName + "年变化"] = double.NaN;
                            }
                        }
                    }
                }
            }
        }

        private void 减少ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (currentColumn != null)
            {
                string paramName = htColumnNameToParamNameMaps[currentColumn.FieldName] as string;

                if (paramName != null)
                {
                    object obj = precisionHT[paramName];
                    if (obj != null && obj is byte)
                    {
                        byte val = (byte)obj;
                        if (val > 0)
                        {
                            val--;
                            precisionHT[paramName] = val;

                            formatGridView(gridControlResult.MainView as GridView);
                            gridControlResult.Refresh();
                        }
                    }
                }
            }
        }

        private void 导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CommonControl.ListSelector.nameList = paramNameList;

            CommonControl.ListSelector colForm = new hammergo.CommonControl.ListSelector();

            if (colForm.ShowDialog(this) == DialogResult.OK)
            {
                string selectedName = hammergo.CommonControl.ListSelector.selectedName;


                outputExcel(selectedName);

            }
        }

        hammergo.ExportLib.ExcelExportDataSearch exporter = null;
        private void outputExcel(string selectedName)
        {
            if (exporter == null)
            {
                exporter = new hammergo.ExportLib.ExcelExportDataSearch();
            }

            exporter.outputExcel(selectedName, gridControlResult.DataSource as DataTable, paramNameList, groupCount, suffixString, precisionHT);
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

        internal DataTable GetResult(List<string> appNameList, string filterTypeName, string filterName)
        {
            DataTable resultTable = null;

            if (appNameList.Count != 0)
            {
                //参数名列表
                List<string> paramNamelist = new List<string>();


                //判断是否具有相同的参数,包括参数名称和个数
                //需要进行快速的判断

                string filterVariable = "";

                AppIntegratedInfo appInfo = new AppIntegratedInfo(appNameList[0], 0, null, null);
                if (appInfo.AppType != null)
                {
                    if (appInfo.AppType.TypeName.Equals(filterTypeName, StringComparison.CurrentCultureIgnoreCase))
                    {
                        filterVariable = filterName;
                    }
                }

                List<AppIntegratedInfo> appInfoList = new List<AppIntegratedInfo>(appNameList.Count);
                Utility.Utility.isSameAppResult(appNameList, paramNamelist, filterVariable,appInfoList);



                resultTable = searchData(paramNamelist,appInfoList);

                addStatRow(resultTable);
            }

            return resultTable;

        }


        DataRow maxRow = null;
        DataRow minRow = null;
        /// <summary>
        /// 添加统计信息相关的行
        /// </summary>
        /// <param name="table"></param>
        private void addStatRow(DataTable table)
        {
            //			if(table.Rows.Count<=1)return;//只有一行数据就不用统计了



            maxRow = table.NewRow();
            maxRow["测点编号"] = "最大值";


            minRow = table.NewRow();
            minRow["测点编号"] = "最小值";

            maxRow.BeginEdit();
            minRow.BeginEdit();


            recalcStatRow(maxRow, minRow, table);

            maxRow.EndEdit();
            minRow.EndEdit();
            table.Rows.Add(maxRow);
            table.Rows.Add(minRow);

            table.AcceptChanges();





        }

        /// <summary>
        /// 产生用于统计的哈希表
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        private Hashtable createStatHastTable(DataTable table)
        {

            Hashtable ht = new Hashtable(15);


            //初始化
            foreach (DataColumn column in table.Columns)
            {
                if (column.DataType.Equals(typeof(double)))
                {
                    //需要统计的列

                    string columnName = column.ColumnName;
                    string maxName = columnName + "max";
                    string minName = columnName + "min";

                    //变化量的最大值和最小值是根据绝对值来对得的



                    //object 数组一个是值，另一个表示该值所在的行
                    ht.Add(maxName, new object[] { double.NegativeInfinity, null });
                    ht.Add(minName, new object[] { double.PositiveInfinity, null });

                }
            }

            foreach (DataRow row in table.Rows)
            {

                foreach (DataColumn column in table.Columns)
                {
                    if (column.DataType.Equals(typeof(double)))
                    {
                        //需要统计的列

                        string columnName = column.ColumnName;
                        string maxName = columnName + "max";
                        string minName = columnName + "min";



                        if (row[columnName] is double == false)
                            continue;

                        double val = (double)row[columnName];

                        //排队渗透压力大于0的情况

                        if (columnName == "渗透压力" + (groupCount - 1).ToString())
                        {

                            if (val > 0)
                            {
                                row["渗透压力" + suffixString] = PubConstant.ConfigData.ErrorValList[0];
                                row["渗透压力年变化"] = PubConstant.ConfigData.ErrorValList[0];

                                continue;
                            }
                        }

                        if (Utility.Utility.isErrorValue(val) == false)
                        {
                            //非错误值才进行比较
                            object[] maxobjs = (object[])ht[maxName];
                            object[] minobjs = (object[])ht[minName];

                            double max = (double)maxobjs[0];

                            double min = (double)minobjs[0];


                            if (val > max)
                            {

                                maxobjs[0] = val;

                                maxobjs[1] = row;

                            }
                            if (val < min)
                            {

                                minobjs[0] = val;

                                minobjs[1] = row;
                            }
                        }



                    }
                }

            }

            return ht;
        }


        //用于统计的hashtable
        Hashtable statHastTable = null;
        private void recalcStatRow(DataRow maxRow, DataRow minRow, DataTable table)
        {

            statHastTable = createStatHastTable(table);


            foreach (DataColumn column in table.Columns)
            {
                if (column.DataType.Equals(typeof(double)))
                {
                    //需要统计的列

                    string columnName = column.ColumnName;
                    string maxName = columnName + "max";
                    string minName = columnName + "min";

                    maxRow[columnName] = ((object[])statHastTable[maxName])[0];

                    minRow[columnName] = ((object[])statHastTable[minName])[0];

                    ////设置颜色标记信息

                    //DataRow maxInRow = (DataRow)((object[])statHastTable[maxName])[1];

                    //DataRow minInRow = (DataRow)((object[])statHastTable[minName])[1];


                }
            }


        }

        private string handleColumn(string columnName, int precision)
        {
            return string.Format(columnName + ": {0}({2})~{1}({3})  ", ((double)minRow[columnName]).ToString("f" + precision),
                ((double)maxRow[columnName]).ToString("f" + precision), getMinSn(columnName), getMaxSn(columnName));

        }

        private string getMinSn(string columnName)
        {
            string celltip = "出错";
            string key = columnName + "min";
            if (statHastTable.ContainsKey(key))
            {
                object[] objs = (object[])statHastTable[key];
                DataRow row = (DataRow)objs[1];
                if (row != null)
                    celltip = row["测点编号"].ToString();
            }

            return celltip;
        }

        private string getMaxSn(string columnName)
        {
            string celltip = "出错";
            string key = columnName + "max";
            if (statHastTable.ContainsKey(key))
            {
                object[] objs = (object[])statHastTable[key];
                DataRow row = (DataRow)objs[1];
                if (row != null)
                    celltip = row["测点编号"].ToString();
            }

            return celltip;

        }


        internal void outputResultTable(DataTable resultTable, string taskName)
        {

            System.Text.StringBuilder sb = new System.Text.StringBuilder(200);

            sb.Append(taskName).Append(" ");


            string suffer = (groupCount - 1).ToString();


            foreach (string name in paramNameList)
            {

                int precision = 4;

                hammergo.GlobalConfig.ParamInfo pi = PubConstant.ConfigData.DefaultParamsList.Find(delegate(hammergo.GlobalConfig.ParamInfo item) { return item.Name == name; });


                if (pi != null)
                {
                    precision = pi.Precision;
                }

                sb.Append(handleColumn(name + suffer, precision));
                sb.Append(handleColumn(name + suffixString, precision));
                sb.Append(handleColumn(name + "年变化", precision));


            }

            System.IO.StreamWriter sw = new System.IO.StreamWriter("Results.txt", true, System.Text.Encoding.UTF8);

            sw.WriteLine(sb.ToString());
            sw.WriteLine();//加一行，以免数据太多，容易混淆

            sw.Close();
        }
    }
}
