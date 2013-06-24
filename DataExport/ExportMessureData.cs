using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using hammergo.GlobalConfig;
using hammergo.Utility;
using hammergo.Model;

namespace hammergo.DataExport
{
    public partial class ExportMessureData : DevExpress.XtraEditors.XtraUserControl, ICustomDispose, hammergo.Utility.IShowAppData
    {
        public ExportMessureData()
        {
            InitializeComponent();
        }

        private void pasteDateMenuItem1_Click(object sender, EventArgs e)
        {
            Utility.Utility.handlePasteInDateEdit(c1DateEdit1);
        }

        private void ExportMessureData_Load(object sender, EventArgs e)
        {
            appSelector1.initial();
           
            c1DateEdit1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            c1DateEdit1.Properties.DisplayFormat.FormatString = PubConstant.customString;
           
        }

        //string fullPath = "";

        //private void btnSel_Click(object sender, EventArgs e)
        //{
        //    if (folderImporter.ShowDialog(this) == DialogResult.OK)
        //    {
        //        fullPath = folderImporter.SelectedPath;

        //        btnOut.Enabled = true;

        //        lblInfo.Text = "路径: " + fullPath;
        //    }
        //}

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {



            try
            {

                // Get the BackgroundWorker that raised this event.
                BackgroundWorker worker = sender as BackgroundWorker;

                List<List<string>> keyList = new List<List<string>>(20);
                List<List<string>> valueList = new List<List<string>>(20);//数据一般不会超过20种

                List<string> applist = (List<string>)e.Argument;

                int count = applist.Count;

                for (int i = 0; i < count; i++)
                {
                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        string sn = applist[i];
                        AppIntegratedInfo appInfo = new AppIntegratedInfo(sn, 1, null, DateTime.MaxValue);

                        List<string> paNameList = new List<string>(appInfo.MessureParams.Count);

                        foreach (MessureParam mp in appInfo.MessureParams)
                        {
                            paNameList.Add(mp.ParamName);
                        }

                        //寻找是否具有相同的key
                        bool haveSame = false;
                        int keyIndex = 0;
                        for (; keyIndex < keyList.Count; keyIndex++)
                        {
                            List<string> keyItem = keyList[keyIndex];
                            if (paNameList.Count != keyItem.Count)
                            {
                                continue; //继续查找下一个keyItem
                            }

                            //数据相同,再次查找
                            int nameIndex = 0;
                            for (; nameIndex < paNameList.Count; nameIndex++)
                            {
                                if (keyItem.Contains(paNameList[nameIndex]) == false)
                                {
                                    break;//当前keyItem已不符合要求
                                }
                            }

                            if (nameIndex == paNameList.Count)//所有的项都已查找完
                            {
                                haveSame = true;
                                break;//已经找到退出主循环
                            }

                        }
                        if (haveSame == false)
                        {
                            keyList.Add(paNameList);
                            //添加valueList的项
                            List<string> groupedAppList = new List<string>(200);//一般仪器不超过200
                            valueList.Add(groupedAppList);

                        }
                        //不管是否成功，都会有项，来添加仪器的名称
                        valueList[keyIndex].Add(sn);//将仪器加入分组list



                        int percentComplete =
                            (int)((i + 1.0f) / count * 100);

                        if (percentComplete > highestPercentageReached)
                        {
                            highestPercentageReached = percentComplete;
                            worker.ReportProgress(percentComplete, "正在分析仪器的参数");
                        }

                    }
                }

                List<DataTable> tableList = new List<DataTable>(keyList.Count);

                highestPercentageReached = 0;
                int j = 0;
                for (int i = 0; i < valueList.Count; i++)
                {
                    List<string> groupedAppList = valueList[i];
                    //使用Table保存检索结果,最后输出到文件
                    //创建Table
                    List<string> paNameList = keyList[i];
                    DataTable table = createDataTableByPaNameList(paNameList);

                    foreach (string appName in groupedAppList)
                    {
                        DataRow row = table.NewRow();

                        FillMessureValueRow(row, appName, selTime, blurHours);
                        table.Rows.Add(row);

                        int percentComplete = (int)((j + 1.0f) / count * 100);
                        j++;

                        if (percentComplete > highestPercentageReached)
                        {
                            highestPercentageReached = percentComplete;
                            worker.ReportProgress(percentComplete, "正在查询数据");
                        }

                    }

                    table.AcceptChanges();



                    tableList.Add(table);

                }



                //数据输出到excel
                //hammergo.MyControls.Helper.OutPutTableListToExcel(tableList);

                if (exporter == null)
                {
                    exporter = new hammergo.ExportLib.ExcelExportMessureExport();
                }

                exporter.OutPutTableListToExcel(tableList);
        

                e.Result = "完成导出";
            }
            catch (Exception ex)
            {
                e.Result = ex.Message;
                Utility.Utility.log(ex);
            }

        }


        hammergo.ExportLib.ExcelExportMessureExport exporter = null;
        
          



        private DataTable createDataTableByPaNameList(List<string> paNameList)
        {
            DataTable table = new DataTable();
            table.Columns.Add("测点编号", typeof(string));
            table.Columns.Add(PubConstant.timeColumnName, typeof(DateTime));

            foreach (string name in paNameList)
            {
                DataColumn column = new DataColumn(name, typeof(double));
                ParamInfo paramInfo= PubConstant.ConfigData.DefaultParamsList.Find(delegate(ParamInfo item) { return item.Name == name; });
                //DataRow[] unitRows = ConnectionLib.Connection.Config.Tables["默认单位"].Select(string.Format("名称 = '{0}'", name));
                //if (unitRows.Length != 0)
                //{
                //    column.ExtendedProperties.Add(hammergo.MyControls.Helper.unitFeildName, unitRows[0]["单位"]);
                //    column.ExtendedProperties.Add(hammergo.MyControls.Helper.preciseFeildName, unitRows[0]["精度"]);

                //}

                if (paramInfo != null)
                {
                    column.ExtendedProperties.Add(hammergo.ExportLib.ExcelExportMessureExport.unitFeildName,paramInfo.UnitSymbol);
                    column.ExtendedProperties.Add(hammergo.ExportLib.ExcelExportMessureExport.preciseFeildName, paramInfo.Precision);

                }
                table.Columns.Add(column);
            }

            table.Columns.Add(PubConstant.remarkColumnName, typeof(string));

            table.PrimaryKey = new DataColumn[] { table.Columns[0] };

            return table;
        }



        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Text = e.ProgressPercentage.ToString();
            lblInfo.Text = e.UserState.ToString();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnOut.Enabled =  true;
            lblInfo.Text = e.Result.ToString();
        }


        private int highestPercentageReached = 0;

        private double blurHours = 0;

        private DateTime selTime;

        private void btnOut_Click(object sender, EventArgs e)
        {

            try
            {
                if (c1DateEdit1.EditValue ==null)
                {
                    throw new Exception("请选择时间");

                }
                selTime =c1DateEdit1.DateTime;

                blurHours =(double) calcEdit1.Value;

                btnOut.Enabled = false;
                // Reset the variable for percentage tracking.
                highestPercentageReached = 0;
                progressBar1.Text = highestPercentageReached.ToString();

                List<string> applist = new List<string>(appSelector1.lbcSelectedApps.Items.Count);
                // Start the asynchronous operation.
                foreach (string item in appSelector1.lbcSelectedApps.Items)
                {
                    applist.Add(item);
                }

                backgroundWorker1.RunWorkerAsync(applist);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "错误!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                btnOut.Enabled =true;
            }
        }



        /// <summary>
        /// 填充要导出仪器测量数据的行
        /// </summary>
        /// <param name="row">需要被填充的行</param>
        /// <param name="appName">仪器名称</param>
        /// <param name="paNameList">仪器测量参数名称列表</param>
        /// <param name="time">数据的时间</param>
        /// <param name="blurHours">模糊小时数</param>
        private  void FillMessureValueRow(DataRow row, string appName, DateTime time, double blurHours)
        {
            row.BeginEdit();
            row["测点编号"] = appName;

            AppIntegratedInfo appInfo = AppIntegratedInfo.getAppInfoNearTime(appName, time, blurHours / 24.0);

            

            if (appInfo!=null)
            {
                DateTime rightTime = appInfo.MessureValues[0].Date.Value;

                row[PubConstant.timeColumnName] = rightTime;

                if(appInfo.Remarks.Count>0)
                {
                    row[PubConstant.remarkColumnName]=appInfo.Remarks[0].RemarkText;
                }

                foreach (MessureParam mp in appInfo.MessureParams)
                {
                    MessureValue mv = appInfo.MessureValues.Find(delegate(MessureValue item) { return item.MessureParamID == mp.MessureParamID; });
                    if (mv != null)
                    {
                        if (mv.Val != null)
                        {
                            row[mp.ParamName] = mv.Val;
                        }
                    }
                }

               
             


                row.EndEdit();
            }


            return;

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

        #region IShowAppData 成员

        public event EventHandler<AppSearchEventArgs> ShowDataEvent;

        #endregion

        private void appSelector1_ShowDataEvent(object sender, AppSearchEventArgs e)
        {
            if (ShowDataEvent != null)
            {
                ShowDataEvent(sender, e);
            }
        }
    }
}
