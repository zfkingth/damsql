using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using hammergo.Model;


namespace hammergo.ImportOldData.ImportClasses
{
    public class ImportRemarks
    {
        ImportDataControl importDataControl = null;

        BackgroundWorker bgw = new BackgroundWorker();

        hammergo.BLL.RemarkBLL remarkBLL = new hammergo.BLL.RemarkBLL();


        //OldDSTableAdapters.备注TableAdapter 备注TableAdapter1 = new OldDSTableAdapters.备注TableAdapter();

        dam3ModeDataSetTableAdapters.RemarkTableAdapter 备注TableAdapter1 = new dam3ModeDataSetTableAdapters.RemarkTableAdapter();

        dam3ModeDataSet ds = null;

        DateTime startTime;



        private int highestPercentageReached = 0;
        private int handledCnt = 0;//已经处理的行数


        string bgwResult = "";

        public ImportRemarks(ImportDataControl idc)
        {
            importDataControl = idc;
            ds = idc.ds;

            bgw.WorkerReportsProgress = true;
            bgw.WorkerSupportsCancellation = true;
            bgw.DoWork += new DoWorkEventHandler(bgw_DoWork);
            bgw.ProgressChanged += new ProgressChangedEventHandler(bgw_ProgressChanged);
            bgw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgw_RunWorkerCompleted);
        }

        public void startWork()
        {
            importDataControl.progressBar1.Position = highestPercentageReached = handledCnt = 0;




            // PersistLayer.Utility.openDBCon();

            备注TableAdapter1.Connection = importDataControl.oleDbCon;
            bgw.RunWorkerAsync();
        }
        void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                long seconds = (DateTime.Now.Ticks - startTime.Ticks) / TimeSpan.TicksPerSecond;
                importDataControl.lblResult.Text = bgwResult + string.Format(" 用时 {0}秒", seconds);

                importDataControl.setAllButtonEnable(true);


                // PersistLayer.Utility.closeDBCon();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(importDataControl, ex.Message, "错误!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            ;
        }

        void bgw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            importDataControl.progressBar1.Position = e.ProgressPercentage;
        }

        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                备注TableAdapter1.Fill(ds.Remark);




                //calculate time
                startTime = DateTime.Now;
                /////////////////////////
                importRows(e);

                handledCnt = ds.Apparatus.Rows.Count;

                reportProgress();

                bgwResult = "导入成功!";
            }
            catch (Exception ex)
            {
                Utility.Utility.log(ex);
                bgwResult = ex.Message;
            }
            ;
        }



        private void importRows(DoWorkEventArgs e)
        {
            if (bgw.CancellationPending == false)
            {
                foreach (dam3ModeDataSet.ApparatusRow row in ds.Apparatus.Rows)
                {
                    importRemarkRows(row.GetChildRows("ApparatusRemark") as dam3ModeDataSet.RemarkRow[], row.AppName.ToUpper());


                    reportProgress();
                }
            }
            else
            {
                e.Cancel = true;
                bgwResult = "导入操作已被取消";
            }
        }

        private void importRemarkRows(dam3ModeDataSet.RemarkRow[] rows, string appName)
        {
            foreach (dam3ModeDataSet.RemarkRow row in rows)
            {
                if (remarkBLL.ExistsBy_appName_Date(appName, row.Date) == false)
                {
                    Remark remark = new Remark();


                    remark.Date = row.Date;
                     

                    if (!row.IsRemarkTextNull())
                    {
                        remark.RemarkText = row.RemarkText.ToString().Trim();
                    }



                    remark.AppName = appName;


                    remarkBLL.Add(remark);
                }
            }
        }



        private void reportProgress()
        {
            int percentComplete =
            (int)((handledCnt + 1.0f) / ds.Apparatus.Rows.Count * 100);

            handledCnt++;

            if (percentComplete > highestPercentageReached)
            {
                highestPercentageReached = percentComplete;
                bgw.ReportProgress(percentComplete);
            }
        }
    }
}
