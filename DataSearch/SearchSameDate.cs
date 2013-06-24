using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using hammergo.Utility;
using hammergo.GlobalConfig;
using System.Collections;
using DevExpress.XtraGrid.Views.Grid;

namespace hammergo.DataSearch
{
    public partial class SearchSameDate : DevExpress.XtraEditors.XtraUserControl, IShowAppData
    {
        public SearchSameDate()
        {
            InitializeComponent();
        }

        void SearchSameDate_Load(object sender, System.EventArgs e)
        {
            appSelector1.initial();
            initialPrecision();
            c1DateEdit1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            c1DateEdit1.Properties.DisplayFormat.FormatString = PubConstant.customString;

            c1DateEdit2.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            c1DateEdit2.Properties.DisplayFormat.FormatString = PubConstant.customString;
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

       

      


        public event EventHandler<hammergo.Utility.AppSearchEventArgs> ShowDataEvent;
 

        private void appSelector1_ShowDataEvent(object sender, AppSearchEventArgs e)
        {
            if (ShowDataEvent != null)
            {
                ShowDataEvent(sender, e);
            }
        }

        private void formatGridView(GridView gv)
        {
            
            foreach (DevExpress.XtraGrid.Columns.GridColumn column in gv.Columns)
            {

   

                if (column.ColumnType == typeof(DateTime))
                {
                    //if (dateTimeWidth == 0)
                    //{
                    //    dateTimeWidth = TextRenderer.MeasureText("2009-12-22", gridView2.Appearance.Row.Font).Width ;
                    //}
                    //column.Width = dateTimeWidth;

                    column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    column.DisplayFormat.FormatString = PubConstant.customString;


                }
            

            }
        }

        private void appSelector1_SearchExeClick(object sender, CommonControl.AppsEventArgs e)
        {
            try
            {
               
                List<string> listNames = e.AppNameList;
                if (listNames.Count != 0)
                {
                    DataTable rt = new DataTable();

                    DateTime? startDate = c1DateEdit1.EditValue as DateTime?;
                    DateTime? endDate = c1DateEdit2.EditValue as DateTime?;

                    double timeSlice =(double) timesliceEdit.Value  ;

                    var apps =
                        (from appName in listNames
                        select new AppIntegratedInfo(appName, 0, startDate, endDate)).ToList();

                    int cnt = apps.Count();
                    //搜寻最大时间，和最小时间
                    List<DateTime> minTimeList = new List<DateTime>(10);
                    List<DateTime> maxTimeList = new List<DateTime>(10);

                    foreach (var item in apps)
                    {
                        if (item.CalcValues.Count == 0)
                        {
                            throw new Exception(item.appName + " 没有数据");
                        }

                        var times =
                            from calcValue in item.CalcValues
                            select calcValue.Date.Value;
                        minTimeList.Add(times.Min());
                        maxTimeList.Add(times.Max());
                       
                    }

                    //最小时间
                    DateTime minTime = minTimeList.Max();
                    DateTime maxTime = maxTimeList.Min();

                    if (minTime > maxTime)
                    {
                        throw new Exception("没有共同时间的数据");
                    }

                    //构建表格结构
                    //时间
                    rt.Columns.Add("时间", typeof(DateTime));

                    foreach(var item in apps)
                    {
                        foreach (var cp in item.CalcParams)
                        {
                            rt.Columns.Add(item.appName + "." + cp.ParamName, typeof(double));
                        }
                    }

                    DateTime it = minTime;
                    do
                    {
                        //终止时刻
                        DateTime nextTime = it.Add(TimeSpan.FromDays(timeSlice));

                        //创建数据行
                        DataRow newRow = rt.NewRow();
                        int appIndex = 0;

                        DateTime? findedtime = null;
                        //在两个时刻之间寻找数据
                        foreach (var app in apps)
                        {
                            //遍历测点

                            //搜寻数据
                            var cvGroups =
                                (from cv in app.CalcValues
                                where cv.Date.Value >= it && cv.Date.Value < nextTime
                                 group cv by cv.Date.Value).ToList();;
                            //用分组将多组符合条件的数据分开

                           

                            if (cvGroups.Count() > 0)
                            {
                                
                                //找到了数据，考虑一个时间片段中有多组数据
                                //只选择其中一个数据的时间

                                foreach (var cv in cvGroups.First())
                                {

                                    //搜寻对应的成果参数
                                    var cpNames =
                                        from cp in app.CalcParams
                                        where cp.CalculateParamID.Equals(cv.CalculateParamID)
                                        select cp.ParamName;
                                    newRow[app.appName + "." + cpNames.First()] = cv.Val;

                                    //具体的时间保存,只保存一次
                                    if (findedtime == null)
                                    {
                                        findedtime = cv.Date;
                                    }
                                }
                            }
                            else
                            {
                                //没找到数据，打断当前循环，进入下一个时间片搜索
                                break;

                            }

                            appIndex++;   
                        }

                        //全部的数据都有，才添加新的行
                        if (appIndex == apps.Count())
                        {
                            newRow["时间"] = findedtime;
                            rt.Rows.Add(newRow);
                            
                        }
                        it = nextTime;
                    } while (it <=maxTime);


                    gridControlResult.DataSource = rt;

                    gridControlResult.MainView.PopulateColumns();

                    formatGridView(gridControlResult.MainView as GridView);
                }
              

            }
            catch (Exception ex)
            {
                Utility.Utility.log(ex);
                XtraMessageBox.Show(this, ex.Message, "错误!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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

        private void c1DateEdit1_ParseEditValue(object sender, DevExpress.XtraEditors.Controls.ConvertEditValueEventArgs e)
        {
            if (e.Value != null)
            {
                DateTime i = (DateTime)e.Value;
                e.Value = new DateTime(i.Year, i.Month, i.Day, i.Hour, i.Minute, 0);
            }
        }

        private void 复制Item_Click(object sender, EventArgs e)
        {

            GridView gv = gridControlResult.MainView as GridView;

            gv.CopyToClipboard();

            //System.Text.StringBuilder strTemp = new System.Text.StringBuilder(2560); ; //string to be copied to the clipboard

            //DevExpress.XtraGrid.Views.Base.GridCell[] cells = gv.GetSelectedCells();



            ////最小索引和最大索引
            //int visibleIndexMin = int.MaxValue;
            //int visibleIndexMax = int.MinValue;

            //foreach (DevExpress.XtraGrid.Views.Base.GridCell cell in cells)
            //{
            //    int visibleIndex = cell.Column.VisibleIndex;

            //    if (visibleIndexMin > visibleIndex)
            //    {
            //        visibleIndexMin = visibleIndex;
            //    }

            //    if (visibleIndexMax < visibleIndex)
            //    {
            //        visibleIndexMax = visibleIndex;
            //    }
            //}
            //const string CellDelimiter = "\t";
            //const string LineDelimiter = "\r\n";

            //int cellIndex = 0;
            //foreach (int selRowHandle in gv.GetSelectedRows())
            //{
            //    //行
            //    for (int j = visibleIndexMin; j <= visibleIndexMax; j++)
            //    {
            //        //列
            //        if (cells[cellIndex].Column.VisibleIndex == j)
            //        {
            //            DevExpress.XtraGrid.Views.Base.GridCell cell = cells[cellIndex];
            //            if (cell.Column.ColumnType == typeof(double))
            //            {
            //                strTemp.Append(gv.GetRowCellValue(selRowHandle, cell.Column));
            //            }
            //            else
            //            {
            //                strTemp.Append(gv.GetRowCellDisplayText(selRowHandle, cell.Column));
            //            }

            //            cellIndex++;
            //        }

            //        strTemp.Append(CellDelimiter);


            //    }

            //    // strTemp.Append(LineDelimiter);
            //    strTemp.Replace(CellDelimiter, LineDelimiter, strTemp.Length - 1, 1);
            //}

            //string outstring = strTemp.ToString();


            //System.Windows.Forms.Clipboard.SetDataObject(outstring, true, 3, 300);

        }



        ExportLib.GridViewExport exporter = null;

        private void 导出ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridView view = gridControlResult.MainView as GridView;
            exporter = new ExportLib.GridViewExport(view, "检索结果");
            exporter.sbExportToXLS_Click(sender, e);
           
        }

      
       

    }
}
