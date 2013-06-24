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
    public partial class ReCalculateForm : DevExpress.XtraEditors.XtraUserControl, hammergo.Utility.IShowAppData
    {
        public ReCalculateForm()
        {
            InitializeComponent();
        }

        private void BatchDelete_Load(object sender, EventArgs e)
        {
            taskAppSelector1.initial();

            c1DateEdit1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            c1DateEdit1.Properties.DisplayFormat.FormatString = PubConstant.customString;

            c1DateEdit2.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            c1DateEdit2.Properties.DisplayFormat.FormatString = PubConstant.customString;

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

      

        private void pasteDateMenuItem1_Click(object sender, EventArgs e)
        {
            Utility.Utility.handlePasteInDateEdit(c1DateEdit1);
        }

        DateTime startDate,endDate;
        private int highestPercentageReached = 0;
        List<string> faultApps = null;
       
        private void btnOut_Click(object sender, EventArgs e)
        {
            try
            {
                if (c1DateEdit1.EditValue == null||c1DateEdit2.EditValue==null)
                {
                    throw new Exception("请输入时间!");

                }

               
                if (XtraMessageBox.Show(this, "确定要重新计算测点的成果量吗?", "重新计算成果量", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
                == DialogResult.OK)
                {
                    btnOut.Enabled = false;

                   
                    startDate = c1DateEdit1.DateTime;
                    endDate = c1DateEdit2.DateTime;

                    faultApps = new List<string>(10);

                    ArrayList reApps = new ArrayList(50);
                    reApps.AddRange(taskAppSelector1.lbcSelectedApps.Items);

                    highestPercentageReached = 0;
                    progressBar1.Text = highestPercentageReached.ToString();


                    backgroundWorker1.RunWorkerAsync(reApps);

                    lblInfo.Text = ".......";



                   



                }
            }
            catch (Exception ex)
            {
                Utility.Utility.log(ex);
                XtraMessageBox.Show(this, ex.Message, "错误!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                btnOut.Enabled = true;
                lblInfo.Text = ex.Message;
            }
        }


        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // Get the BackgroundWorker that raised this event.
            BackgroundWorker worker = sender as BackgroundWorker;

            try
            {
                ArrayList reApps = e.Argument as ArrayList;
                if (reApps != null)
                {



                    int count = reApps.Count;

                    for (int i = 0; i < count; i++)
                    {
                        string appName = reApps[i] as string;

                      

                        AppIntegratedInfo appInfo = new AppIntegratedInfo(appName, 0,startDate,endDate);

                        //列出时间
                        List<DateTime> dateList=new List<DateTime>(30);
                        foreach(MessureValue mv in appInfo.MessureValues)
                        {
                            DateTime dt=mv.Date.Value;
                            if(dateList.Exists(delegate(DateTime item){ return item==dt; })==false)
                            {
                                dateList.Add(dt);
                            }
                        }
                        string calName=appInfo.App.CalculateName;
                        foreach (DateTime dt in dateList)
                        {
                            UtilityUpdateData.reCalcAppByDate(calName, dt);
                            UtilityUpdateData.reCalculateLink(calName, dt);
                        }

                   


                        int percentComplete = (int)((i + 1.0f) / count * 100);

                        if (percentComplete > highestPercentageReached)
                        {
                            highestPercentageReached = percentComplete;
                            worker.ReportProgress(percentComplete, "正在操作......");
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                Utility.Utility.log(ex);
                XtraMessageBox.Show(ex.Message, "错误!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Text = e.ProgressPercentage.ToString();
            lblInfo.Text = e.UserState.ToString();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            taskAppSelector1.lbcSelectedApps.Items.Clear();


            if (faultApps.Count != 0)
            {
                lblInfo.Text = string.Format("操作完成. 有{0}支仪器未能操作成功", faultApps.Count);
                foreach (string appName in faultApps)
                {
                    Utility.Utility.addAppNameInListBox(appName, taskAppSelector1.lbcSelectedApps);
                }
            }
            else
            {
                lblInfo.Text="操作成功!";
            }

           
            btnOut.Enabled = true;
           
        }


        private void toolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            Utility.Utility.handlePasteInDateEdit(c1DateEdit2);
        }
    }
}
