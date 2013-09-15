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
                    Utility.Utility.isSameAppResult(listNames, paramNamelist, filterVariable, appInfoList);

                    DataTable resultTable = fetchData(paramNamelist, appInfoList);
                    //process precision
                    Dictionary<string, int> preDic = getPrecisionDic(appInfoList);

                    if (exporter == null)
                    {
                        exporter = new hammergo.ExportLib.ExcelExportDailyReport();
                    }
                  
                    exporter.outputExcel(null,paramNamelist,preDic,resultTable);


                }



            }
            catch (Exception ex)
            {
                Utility.Utility.log(ex);
                XtraMessageBox.Show(this, ex.Message, "����!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }

        }

        /// <summary>
        /// ��ȡС��λ��������
        /// </summary>
        /// <param name="appInfoList">����֮ǰ��ȷ��appInfoList�е�Ԫ�ظ�ʽ��Ϊ0</param>
        /// <returns></returns>
        private Dictionary<string, int> getPrecisionDic(List<AppIntegratedInfo> appInfoList)
        {
            Dictionary<string, int> preDic = new Dictionary<string, int>(10);
            foreach (var param in appInfoList[0].CalcParams)
            {
                int precision = 2;//Ĭ�ϱ���2ΪС��
                if (param.PrecisionNum != null && param.PrecisionNum.HasValue && param.PrecisionNum.Value >= 0)
                {
                    precision = param.PrecisionNum.Value;
                }

                preDic.Add(param.ParamName, precision);
            }

            return preDic;
        }

        hammergo.ExportLib.ExcelExportDailyReport exporter = null;
        private DataTable fetchData(List<string> paramNamelist, List<AppIntegratedInfo> appInfoList)
        {





            List<string> fetchExtreamNameList = new List<string>(paramNamelist);


            DateTime?[] dates = new DateTime?[] { null, null };
            DataTable rt = CreateTableSchema(dates, fetchExtreamNameList);



            DateTime startDate = dates[0].Value;
            DateTime endDate = dates[1].Value;

            double timeSlice = (double)timesliceEdit.Value;

            //��ѯ����
            foreach (AppIntegratedInfo appInfo in appInfoList)
            {
                appInfo.Reset(0, startDate, endDate);
                DateTime it = startDate;
                bool firstRowFlag = true;
                //��һ�����ݵ�ǰһ������
                AppIntegratedInfo aheadAppInfo = new AppIntegratedInfo(appInfo.appName, 1, null, endDate);
                do
                {
                    //��ֹʱ��
                    DateTime nextTime = it.Add(TimeSpan.FromDays(timeSlice));

                    //����������
                    var vals = (from i in appInfo.CalcValues
                                where i.Date.Value >= it && i.Date.Value < nextTime
                                select i).ToList<CalculateValue>();
                    if (vals.Count() > 0)
                    {
                        //������
                        DataRow row = rt.NewRow();
                        row["�����"] = appInfo.appName;
                        row["����"] = vals[1].Date;

                        foreach (string paramName in fetchExtreamNameList)
                        {
                            CalculateParam calParam = (from i in appInfo.CalcParams
                                                       where i.ParamName == paramName
                                                       select i).FirstOrDefault();
                            //find val
                            CalculateValue val = (from i in vals
                                                  where i.CalculateParamID == calParam.CalculateParamID
                                                  select i).FirstOrDefault();
                            if (val != null && val.Val.HasValue)
                            {
                                row[paramName] = val.Val;
                            }
                            //�Ѿ�����˶�Ӧ���ڵ�����
                            //������仯
                            object lastValue = null;
                            if (firstRowFlag)
                            {
                                CalculateValue aheadVal = (from i in aheadAppInfo.CalcValues
                                                           where i.CalculateParamID == calParam.CalculateParamID
                                                           select i).FirstOrDefault();
                                if (aheadVal != null && aheadVal.Val.HasValue)
                                {
                                    lastValue = aheadVal.Val.Value;
                                }
                            }
                            else
                            {
                                //�ҵ�table�����һ��
                                DataRow aheadRow = rt.Rows[rt.Rows.Count - 1];
                                lastValue = aheadRow[paramName];
                            }

                            row[paramName + "�仯"] = Utility.Utility.substract(row[paramName], lastValue);
                        }
                        rt.Rows.Add(row);

                        //��һ�����ݴ������
                        firstRowFlag = false;



                    }



                    it = nextTime;
                } while (it <= endDate);

            }



            return rt;
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


            DataTable table = new DataTable("���");

            DataColumn column = new DataColumn("�����", typeof(string));

            table.Columns.Add(column);

            column = new DataColumn("����", typeof(DateTime));
            table.Columns.Add(column);

            foreach (string name in fetchExtreamNameList)
            {
                column = new DataColumn(name, typeof(double));
                table.Columns.Add(column);
            }

            foreach (string name in fetchExtreamNameList)
            {
                column = new DataColumn(name + "�仯", typeof(object));
                table.Columns.Add(column);
            }


            return table;
        }

        /// <summary>
        /// ����û�����������Ƿ�Ϻ�����,�׳��쳣
        /// </summary>
        /// <returns></returns>
        private void checkDate()
        {
            if (c1DateEdit1.EditValue == null || c1DateEdit2.EditValue == null)
            {
                throw new Exception("����������");
            }
            //�����������

            if (c1DateEdit1.DateTime > c1DateEdit2.DateTime)
            {
                throw new Exception("��ʼ���ڲ��ܴ��ڽ�������");
            }



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
