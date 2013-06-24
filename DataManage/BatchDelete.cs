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

namespace hammergo.DataManage
{
    public partial class BatchDelete : DevExpress.XtraEditors.XtraUserControl, hammergo.Utility.IShowAppData
    {
        public BatchDelete()
        {
            InitializeComponent();
        }

        private void BatchDelete_Load(object sender, EventArgs e)
        {
            taskAppSelector1.initial();

            c1DateEdit1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            c1DateEdit1.Properties.DisplayFormat.FormatString = PubConstant.customString;

            //c1DateEdit2.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            //c1DateEdit2.Properties.DisplayFormat.FormatString = PubConstant.customString;

        }

        #region IShowAppData ��Ա

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

        DateTime delTime;
        private int highestPercentageReached = 0;
        List<string> faultApps = null;
        private void btnOut_Click(object sender, EventArgs e)
        {
            try
            {
                if (c1DateEdit1.EditValue == null)
                {
                    throw new Exception("������ʱ��!");

                }


                if (XtraMessageBox.Show(this, "ȷ��Ҫɾ��ѡ�������ָ�����ڵ�������!", "ɾ������", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2)
                == DialogResult.OK)
                {
                    btnOut.Enabled = false;

                   
                    delTime = c1DateEdit1.DateTime;

                    faultApps = new List<string>(10);

                    ArrayList delApps = new ArrayList(50);
                    delApps.AddRange(taskAppSelector1.lbcSelectedApps.Items);

                    highestPercentageReached = 0;
                    progressBar1.Text = highestPercentageReached.ToString();


                    backgroundWorker1.RunWorkerAsync(delApps);

                    lblInfo.Text = ".......";



                   



                }
            }
            catch (Exception ex)
            {
                Utility.Utility.log(ex);
                XtraMessageBox.Show(this, ex.Message, "����!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                ArrayList delApps = e.Argument as ArrayList;
                if (delApps != null)
                {



                    int count = delApps.Count;

                    for (int i = 0; i < count; i++)
                    {
                        string appName = delApps[i] as string;

                        AppIntegratedInfo appInfo = new AppIntegratedInfo(appName,0,delTime, delTime);

                        if (appInfo.MessureValues.Count == 0&&appInfo.CalcValues.Count==0)
                        {
                            faultApps.Add(appName);

                        }
                        else
                        {

                            List<DateTime> delTimeList = new List<DateTime>(1);
                            delTimeList.Add(delTime);

                            Utility.UtilityUpdateData.deleteRecord(appInfo, delTimeList);
                        }


                        int percentComplete = (int)((i + 1.0f) / count * 100);

                        if (percentComplete > highestPercentageReached)
                        {
                            highestPercentageReached = percentComplete;
                            worker.ReportProgress(percentComplete, "����ɾ��......");
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                Utility.Utility.log(ex);
                XtraMessageBox.Show(ex.Message, "����!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
                 lblInfo.Text=string.Format("�������. ��{0}֧����δ�ܲ����ɹ�", faultApps.Count);
                foreach (string appName in faultApps)
                {
                    Utility.Utility.addAppNameInListBox(appName, taskAppSelector1.lbcSelectedApps);
                }
            }
            else
            {
                lblInfo.Text="�����ɹ�!";
            }

           
            btnOut.Enabled = true;
           
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
