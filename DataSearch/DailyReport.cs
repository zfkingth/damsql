using System.Linq;
using System.Data.Linq;
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
    public partial class DailyReport : DevExpress.XtraEditors.XtraUserControl, ICustomDispose, hammergo.Utility.IShowAppData
    {
        public DailyReport()
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
                    Utility.Utility.isSameAppResult(listNames, paramNamelist, filterVariable, appInfoList);

                    fetchData(paramNamelist, appInfoList);
                }



            }
            catch (Exception ex)
            {
                Utility.Utility.log(ex);
                XtraMessageBox.Show(this, ex.Message, "错误!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }

        }

        hammergo.ExportLib.ExcelExportStatistics exporter = null;
        private void fetchData(List<string> paramNamelist, List<AppIntegratedInfo> appInfoList)
        {





            List<string> fetchExtreamNameList = new List<string>(paramNamelist);


            DateTime?[] dates = new DateTime?[] { null, null };
            DataTable rt = CreateTableSchema(dates, fetchExtreamNameList);



            DateTime startDate = dates[0].Value;
            DateTime endDate = dates[1].Value;

            double timeSlice = (double)timesliceEdit.Value;

            //查询数据
            foreach (AppIntegratedInfo appInfo in appInfoList)
            {
                appInfo.Reset(0, startDate, endDate);
                DateTime it = startDate;
                do
                {
                    //终止时刻
                    DateTime nextTime = it.Add(TimeSpan.FromDays(timeSlice));

                    //创建数据行
                    var vals = (from i in appInfo.CalcValues
                               where i.Date.Value >= it && i.Date.Value < nextTime
                               select i).ToList<CalculateValue>();
                    if (vals.Count() > 0)
                    {
                        //有数据
                        DataRow row = rt.NewRow();
                        row["测点编号"] = appInfo.appName;
                        row["日期"] = vals[1].Date;

                        foreach (string paramName in fetchExtreamNameList)
                        {
                            CalculateParam calParam = (from i in appInfo.CalcParams
                                                      where i.ParamName == paramName
                                                      select i).FirstOrDefault();
                            //find val
                            CalculateValue val = (from i in vals
                                                  where i.CalculateParamID == calParam.CalculateParamID
                                                  select i).FirstOrDefault();
                            if (val != null)
                            {
                                row[paramName] = val.Val;
                            }

                        }
                        rt.Rows.Add(row);
                      
                        
                        
                    }
                  

           
                    it = nextTime;
                } while (it <= endDate);

            }

    


        }

        private DataTable CreateTableSchema(DateTime?[] dates, List<string> fetchExtreamNameList)
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

            column = new DataColumn("日期", typeof(DateTime));
            table.Columns.Add(column);

            foreach (string name in fetchExtreamNameList)
            {
                column = new DataColumn(name, typeof(double));
                table.Columns.Add(column);
            }

            foreach (string name in fetchExtreamNameList)
            {
                column = new DataColumn(name + "变化", typeof(double));
                table.Columns.Add(column);
            }


            return table;
        }

        /// <summary>
        /// 检查用户输入的日期是否合乎规则,抛出异常
        /// </summary>
        /// <returns></returns>
        private void checkDate()
        {
            if (c1DateEdit1.EditValue == null || c1DateEdit2.EditValue == null)
            {
                throw new Exception("请输入日期");
            }
            //检查其它规则

            if (c1DateEdit1.DateTime > c1DateEdit2.DateTime)
            {
                throw new Exception("起始日期不能大于结束日期");
            }



        }

        /// <summary>
        /// 执行obj1减去obj2的操作,返回正常值,或异常值,或空对象
        /// </summary>
        /// <param name="obj1"></param>
        /// <param name="obj2"></param>
        /// <returns></returns>
        public object substract(object obj1, object obj2)
        {
            object val = DBNull.Value;

            //看是否存在空值
            if (obj1 is double && obj2 is double)
            {
                if (Utility.Utility.isErrorValue((double)obj1) || Utility.Utility.isErrorValue((double)obj1))
                {
                    //存在异常值
                    val = hammergo.GlobalConfig.PubConstant.ConfigData.ErrorValList[0];
                }
                else
                {
                    //既没有空值，也没有异常值 ,同时也是double类型的数

                    val = (double)obj1 - (double)obj2;
                }
            }

            return val;

        }




        public event EventHandler<hammergo.Utility.AppSearchEventArgs> ShowDataEvent;
        private void appSelector1_ShowDataEvent(object sender, hammergo.Utility.AppSearchEventArgs e)
        {
            if (ShowDataEvent != null)
            {
                ShowDataEvent(sender, e);
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
