using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using hammergo.Model;

namespace hammergo.ImportOldData.ImportClasses
{
    public class ImportConstantParam
    {
        ImportDataControl importDataControl = null;

        BackgroundWorker bgw = new BackgroundWorker();


        hammergo.BLL.ConstantParamBLL constantBLL = new hammergo.BLL.ConstantParamBLL();

        //OldDSTableAdapters.常量参数TableAdapter 常量参数TableAdapter1 = new OldDSTableAdapters.常量参数TableAdapter();

        dam3ModeDataSetTableAdapters.ConstantParamTableAdapter 常量参数TableAdapter1 = new dam3ModeDataSetTableAdapters.ConstantParamTableAdapter();

        dam3ModeDataSet ds = null;

        DateTime startTime;




        private int highestPercentageReached = 0;
        private int handledCnt = 0;//已经处理的行数


        string bgwResult = "";

        public ImportConstantParam(ImportDataControl idc)
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

            常量参数TableAdapter1.Connection = importDataControl.oleDbCon;
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
                常量参数TableAdapter1.Fill(ds.ConstantParam);
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
                foreach (dam3ModeDataSet.ApparatusRow row in ds.Apparatus.Rows)
                {
                    importConstantRows(row.GetChildRows("ApparatusConstantParam") as dam3ModeDataSet.ConstantParamRow[], row.AppName.ToUpper());
                    reportProgress();
                }
            }
            else
            {
                e.Cancel = true;
                bgwResult = "导入操作已被取消";
            }
        }

        private void importConstantRows(dam3ModeDataSet.ConstantParamRow[] rows, string appName)
        {
            foreach (dam3ModeDataSet.ConstantParamRow row in rows)
            {
                ConstantParam cp = new ConstantParam();
                cp.AppName = appName;

                cp.ConstantParamID = Guid.NewGuid();
                if (!row.IsParamNameNull())
                {
                    cp.ParamName = row.ParamName;
                }

                if (!row.IsOrderNull())
                {
                    cp.Order = row.Order;
                }

                if (!row.IsDescriptionNull())
                {
                    cp.Description = row.Description;
                }

                if (!row.IsValNull())
                {
                    cp.Val = row.Val;
                }

                if (!row.IsUnitSymbolNull())
                {
                    cp.UnitSymbol = row.UnitSymbol;
                }

                if (!row.IsPrecisionNumNull())
                {
                    cp.PrecisionNum = row.PrecisionNum;
                }

                if (!row.IsParamSymbolNull())
                {
                    cp.ParamSymbol = row.ParamSymbol;
                }


                constantBLL.Add(cp);
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
