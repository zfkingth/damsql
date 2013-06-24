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
using System.Collections;
using hammergo.Model;

namespace hammergo.DataManage
{
    public partial class DataObserve : DevExpress.XtraEditors.XtraUserControl, hammergo.Utility.IShowAppData
    {
        public DataObserve()
        {
            InitializeComponent();
        }

        private void BatchDelete_Load(object sender, EventArgs e)
        {
            taskAppSelector1.initial();

            c1DateEdit1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            c1DateEdit1.Properties.DisplayFormat.FormatString = PubConstant.customString;



        }

        #region IShowAppData 成员

        public event EventHandler<hammergo.Utility.AppSearchEventArgs> ShowDataEvent;

        #endregion

        private void taskAppSelector1_ShowDataEvent(object sender, hammergo.Utility.AppSearchEventArgs e)
        {
            if (ShowDataEvent != null)
            {
                ShowDataEvent(sender, e);
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //Utility.Utility.handlePasteInDateEdit(c1DateEdit2);
        }

        private void pasteDateMenuItem1_Click(object sender, EventArgs e)
        {
            Utility.Utility.handlePasteInDateEdit(c1DateEdit1);
        }

        public void selectTask(Guid appCollectionID,DateTime date)
        {
            taskAppSelector1.selectTask(appCollectionID);
            c1DateEdit1.DateTime = date;

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                string result = "";
                // Get the BackgroundWorker that raised this event.
                BackgroundWorker worker = sender as BackgroundWorker;
                List<string> applist = (List<string>)e.Argument;


                if (btn == btnCheckExist)
                {

                    result = doworkCheckExist(applist, worker, e);
                }
                else
                {
                    result = doworkCheckChange(applist, worker, e);
                }

                e.Result = result;
            }
            catch (Exception ex)
            {
                e.Result = ex.Message;
              
                Utility.Utility.log(ex);
                XtraMessageBox.Show(ex.Message, "错误!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
              
            
            }
        }

        /// <summary>
        /// 用来检验数据的个数
        /// </summary>
        const int dataNum = 4;
        const string suffixChange = "变化", suffixStd = "标准差";
        private string doworkCheckChange(List<string> applist, BackgroundWorker worker, DoWorkEventArgs e)
        {
            string result = "";

            SortedDictionary<string, byte> precisionDic = new SortedDictionary<string, byte>();
            

            dt = new DataTable("result");

            dt.Columns.Add(PubConstant.appColumnName, typeof(string));


            dt.PrimaryKey = new DataColumn[] { dt.Columns[0] };

           
            int count = applist.Count;

            if (usePreciseTime)
            {
                aroundHour = 0;
            }

            for (int i = 0; i < count; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                }
                else
                {

                    int percentComplete =
                        (int)((i + 1.0f) / count * 100);

                    if (percentComplete > highestPercentageReached)
                    {
                        highestPercentageReached = percentComplete;
                        worker.ReportProgress(percentComplete);
                    }


                    string sn = applist[i];
                    double xigema = 0;

                    double chazhi = 0;

                    //获取最接近的一次数据
                    AppIntegratedInfo appInfo = AppIntegratedInfo.getAppInfoNearTime(sn, selectedDate, aroundHour / 24f);


                    if (appInfo!=null )
                    {
                        //查询最近的的四次数据
                        DateTime endDate=appInfo.CalcValues[0].Date.Value;

                        appInfo = new AppIntegratedInfo(sn, dataNum, null, endDate);
                        if (appInfo.CalcValues.Count == dataNum * appInfo.CalcParams.Count)
                        {
                            //数据要使是dbNull，也不能在数据库没有对应的记录
                            DataRow currentRow = null;
                            foreach (CalculateParam param in appInfo.CalcParams)
                            {
                                

                                //检索数据
                                List<CalculateValue> calcValues = appInfo.CalcValues.FindAll(delegate(CalculateValue item){ return item.CalculateParamID == param.CalculateParamID; });

                                calcValues.Sort(new CalculateValueComparer());

                                List<double> dataList = new List<double>(dataNum);

                                foreach (CalculateValue item in calcValues)
                                {
                                    if (item.Val !=null)
                                    {
                                        dataList.Add(item.Val.Value);
                                    }
                                }

                                if (checkByLastThreeData(dataList, v, ref xigema, ref chazhi) == false)
                                {
                                    if (dt.Columns.Contains(param.ParamName + suffixChange) == false)
                                    {
                                        //添加相应的变化列
                                        dt.Columns.Add(param.ParamName + suffixChange, typeof(double));

                                        byte defaultPrecision = 2;

                                        ParamInfo pi= PubConstant.ConfigData.DefaultParamsList.Find(delegate(ParamInfo item) { return item.Name == param.ParamName; });
                                        if (pi != null)
                                        {
                                            defaultPrecision = pi.Precision;
                                        }

                                        precisionDic.Add(param.ParamName, defaultPrecision);
                                    }

                                    //if (dt.Columns.Contains(param.ParamName + suffixStd) == false)
                                    //{
                                    //    //添加相应的标准列
                                    //    dt.Columns.Add(param.ParamName + suffixStd, typeof(double));
                                    //}

                                    if (currentRow == null)
                                    {
                                        currentRow = dt.NewRow();
                                        currentRow[PubConstant.appColumnName] = sn;
                                        dt.Rows.Add(currentRow);
                                    }



                                    currentRow[param.ParamName + suffixChange] = Utility.Utility.round(chazhi, precisionDic[param.ParamName]);
                                    //currentRow[param.ParamName + suffixStd] =Utility.Utility.round( xigema,2);
                                    
                                }



                            }
                        }
                    }

                  


                }
            }
            dt.AcceptChanges();


            if (dt.Rows.Count > 0)
                result = "下列仪器的数据变化偏大";
            else
                result = "检查完成,无异常情况!";


            return result;


        }

        public  bool checkByLastThreeData(List<double> list, double q, ref double xigema, ref double chazhi)
        {
            if (list.Count == 4)
            {
                double lastData = list[3];

                double sum = 0;
                for (int i = 0; i < 3; i++)
                {
                    sum += list[i];
                }

                double avg = sum / 3.0;

                double xiSum = 0;

                for (int i = 0; i < 3; i++)
                {
                    xiSum += Math.Pow(list[i] - avg, 2);
                }

                xigema = Math.Sqrt(xiSum / 2);

                double min = list[2] - q * xigema;
                double max = list[2] + q * xigema;

                chazhi = lastData-list[2];


                if (lastData < min || lastData > max)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }

            return true;
        }


        private string doworkCheckExist(List<string> applist, BackgroundWorker worker, DoWorkEventArgs e)
        {
            string result = "";
            dt = new DataTable("result");

            dt.Columns.Add(PubConstant.appColumnName, typeof(string));



            dt.PrimaryKey = new DataColumn[] { dt.Columns[0] };

            if (usePreciseTime)
            {
                aroundHour = 0;
            }
            DateTime startTime = selectedDate.AddHours(-aroundHour);
            DateTime endTime = selectedDate.AddHours(aroundHour);


            int count = applist.Count;

            for (int i = 0; i < count; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                }
                else
                {

                    int percentComplete =
                        (int)((i + 1.0f) / count * 100);

                    if (percentComplete > highestPercentageReached)
                    {
                        highestPercentageReached = percentComplete;
                        worker.ReportProgress(percentComplete);
                    }


                    string sn = applist[i];
                    bool exist = false;

                    AppIntegratedInfo appInfo = new AppIntegratedInfo(sn, 0, startTime, endTime);

                    if (appInfo.MessureValues.Count > 0)
                    {
                        exist = true;
                    }


                    if (exist == false)
                    {
                        DataRow row = dt.NewRow();

                        row[PubConstant.appColumnName] = sn;

                        row.EndEdit();
                        dt.Rows.Add(row);
                    }

                }
            }
            dt.AcceptChanges();





            if (dt.Rows.Count > 0)
            {

                result = "下列仪器在指定时间的数据不存在!";
            }
            else
            {

                result = "检查完成,无异常情况!";
            }


            return result;
        }

        DataTable dt = new DataTable();
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnCheckChange.Enabled = btnCheckExist.Enabled = true;
            lblInfo.Text = e.Result.ToString();

            gridControl1.DataSource = dt;


            formatGrid();
            
           

        }

        private void formatGrid()
        {
            gridView1.PopulateColumns();

            //foreach (DevExpress.XtraGrid.Columns.GridColumn column in gridView1.Columns)
            //{
            //    column.Width= TextRenderer.MeasureText(column.Caption,column.AppearanceCell.Font).Width + 10;
            //}
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Text = e.ProgressPercentage.ToString();
            //lblInfo.Text = e.UserState.ToString();
        }

      

        object btn = null;
        int highestPercentageReached = 0;
        double v = 2;
        float aroundHour;
        bool usePreciseTime = true;
        DateTime selectedDate;
        private void btnCheckExist_Click(object sender, EventArgs e)
        {
            try
            {

                btnCheckChange.Enabled = btnCheckExist.Enabled = false;
                // Reset the variable for percentage tracking.
                highestPercentageReached = 0;
                progressBar1.Text = highestPercentageReached.ToString();

                List<string> applist = new List<string>(taskAppSelector1.lbcSelectedApps.Items.Count);
                // Start the asynchronous operation.
                foreach (string item in taskAppSelector1.lbcSelectedApps.Items)
                {
                    applist.Add(item);
                }

                if (c1DateEdit1.EditValue == null)
                {


                    throw new ArgumentException("时间不能为空!");
                }

                selectedDate = c1DateEdit1.DateTime;

                v = double.Parse(textBox1.Text.Trim());
                if (v <= 0)
                {
                    throw new Exception("倍数必须大于0!");

                }

                aroundHour = float.Parse(txtAroundHour.Text.Trim());

                if (radioGroup1.SelectedIndex == 0)
                {
                    usePreciseTime = true;
                }
                else
                {
                    usePreciseTime = false;
                }


                btn = sender;
                backgroundWorker1.RunWorkerAsync(applist);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "错误!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                btnCheckChange.Enabled = btnCheckExist.Enabled = true;
            }

        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            string appName = row[PubConstant.appColumnName] as string;
            if (ShowDataEvent != null)
            {
                hammergo.Utility.AppSearchEventArgs e2 = new AppSearchEventArgs(appName);
                ShowDataEvent(sender, e2);
            }

        }

        private void c1DateEdit1_ParseEditValue(object sender, DevExpress.XtraEditors.Controls.ConvertEditValueEventArgs e)
        {
            if (e.Value != null)
            {
                DateTime i = (DateTime)e.Value;
                e.Value = new DateTime(i.Year, i.Month, i.Day, i.Hour, i.Minute, 0);
            }
        }




    }
}
