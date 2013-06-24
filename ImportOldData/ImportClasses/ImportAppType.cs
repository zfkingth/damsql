using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using hammergo.Model;

namespace hammergo.ImportOldData.ImportClasses
{
    public class ImportAppType
    {
        ImportDataControl importDataControl = null;

        BackgroundWorker bgw = new BackgroundWorker();

        hammergo.BLL.ApparatusTypeBLL typeBLL = new hammergo.BLL.ApparatusTypeBLL();
        hammergo.BLL.ApparatusBLL appBLL = new hammergo.BLL.ApparatusBLL();

         dam3ModeDataSetTableAdapters.ApparatusTypeTableAdapter 仪器类型TableAdapter1 = new  dam3ModeDataSetTableAdapters.ApparatusTypeTableAdapter();

        dam3ModeDataSet ds = null;

        DateTime startTime;

        
       
        private int highestPercentageReached = 0;
        private int handledCnt = 0;//已经处理的行数


        string bgwResult = "";

        public ImportAppType(ImportDataControl idc)
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

            仪器类型TableAdapter1.Connection = importDataControl.oleDbCon;
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
                仪器类型TableAdapter1.Fill(ds.ApparatusType);


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
                bgwResult = ex.Message;
            }
            ;
        }



        private void importRows(DoWorkEventArgs e)
        {
            if (bgw.CancellationPending == false)
            {
                foreach (dam3ModeDataSet.ApparatusTypeRow row in ds.ApparatusType.Rows)
                {
                    ApparatusType appType = new ApparatusType();

                    appType.TypeName = row.TypeName;

                    //int? maxID = typeBLL.getMaxApparatusTypeID();
                    //if (maxID.HasValue)
                    //{
                    //    appType.ApparatusTypeID = maxID.Value + 1;
                    //}
                    //else
                    //{
                    //    appType.ApparatusTypeID = 0;
                    //}
                    appType.ApparatusTypeID = Guid.NewGuid();

                    typeBLL.Add(appType);
                    importApparatusRows(row.GetChildRows("ApparatusTypeApparatus") as dam3ModeDataSet.ApparatusRow[], appType.ApparatusTypeID);
                }
            }
            else
            {
                e.Cancel = true;
                bgwResult = "导入操作已被取消";
            }
        }

        private void importApparatusRows(dam3ModeDataSet.ApparatusRow[] rows, Guid? appTypeID)
        {
            foreach (dam3ModeDataSet.ApparatusRow row in rows)
            {
                Apparatus app = appBLL.GetModelBy_AppName(row.AppName.ToUpper());

                if (app != null)
                {
                    app.AppTypeID = appTypeID;

                    appBLL.Update(app);

                    reportProgress();
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
