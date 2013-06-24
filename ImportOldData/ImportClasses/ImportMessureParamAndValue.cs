using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using hammergo.Model;

namespace hammergo.ImportOldData.ImportClasses
{
    public class ImportMessureParamAndValue
    {
        ImportDataControl importDataControl = null;

        BackgroundWorker bgw = new BackgroundWorker();

        hammergo.BLL.MessureParamBLL mesParamBLL = new hammergo.BLL.MessureParamBLL();
        hammergo.BLL.MessureValueBLL mesValueBLL = new hammergo.BLL.MessureValueBLL();

        //OldDSTableAdapters.测量参数TableAdapter 测量参数TableAdapter1 = new OldDSTableAdapters.测量参数TableAdapter();

        dam3ModeDataSetTableAdapters.MessureParamTableAdapter 测量参数TableAdapter1 = new dam3ModeDataSetTableAdapters.MessureParamTableAdapter();

        //OldDSTableAdapters.测量值TableAdapter 测量值TableAdapter1 = new OldDSTableAdapters.测量值TableAdapter();

        dam3ModeDataSetTableAdapters.MessureValueTableAdapter 测量值TableAdapter1 = new dam3ModeDataSetTableAdapters.MessureValueTableAdapter();



        dam3ModeDataSet ds = null;

        DateTime startTime;




        private int highestPercentageReached = 0;
        private int handledCnt = 0;//已经处理的行数


        string bgwResult = "";

        public ImportMessureParamAndValue(ImportDataControl idc)
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

            测量参数TableAdapter1.Connection = importDataControl.oleDbCon;
            测量值TableAdapter1.Connection = importDataControl.oleDbCon;
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
                测量参数TableAdapter1.Fill(ds.MessureParam);


                /////////////////////////////


                //calculate time
                startTime = DateTime.Now;

                //test
               
                //
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
                    dam3ModeDataSet.MessureParamRow[] mpRows = row.GetChildRows("ApparatusMessureParam") as dam3ModeDataSet.MessureParamRow[];

                    importMessureRows(mpRows , row.AppName.ToUpper());


                    reportProgress();
                }
            }
            else
            {
                e.Cancel = true;
                bgwResult = "导入操作已被取消";
            }
        }

        private MessureParam createNewMessureParam(string appName, dam3ModeDataSet.MessureParamRow row)
        {
            int id = row.MessureParamID;
            MessureParam mesParam = mesParamBLL.GetModelBy_appName_ParamName(appName,row.ParamName);// mesParamBLL.GetModelBy_MessureParamID(id);

            if (mesParam == null)
            {
                mesParam = new MessureParam();

                mesParam.MessureParamID = Guid.NewGuid();
                mesParam.AppName = appName;


                if (!row.IsParamNameNull())
                {
                    mesParam.ParamName = row.ParamName;
                }

                if (!row.IsOrderNull())
                {
                    mesParam.Order = row.Order;
                }

                if (!row.IsDescriptionNull())
                {
                    mesParam.Description = row.Description;
                }



                if (!row.IsUnitSymbolNull())
                {
                    mesParam.UnitSymbol = row.UnitSymbol;
                }

                if (!row.IsPrecisionNumNull())
                {
                    mesParam.PrecisionNum = row.PrecisionNum;
                }

                if (!row.IsParamSymbolNull())
                {
                    mesParam.ParamSymbol = row.ParamSymbol;
                }

                mesParamBLL.Add(mesParam);
            }

            return mesParam;
        }

        /// <summary>
        /// 导入一支仪器的测量参数和测量值表
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="测点编号"></param>
        private void importMessureRows(dam3ModeDataSet.MessureParamRow[] rows, string 测点编号)
        {
            foreach (dam3ModeDataSet.MessureParamRow row in rows)
            {
                MessureParam mesParam = createNewMessureParam(测点编号, row);
                DateTime lastedDate = DateTime.MinValue;

                DateTime? maxDate = mesValueBLL.getMaxDate(mesParam.MessureParamID);

                if (maxDate.HasValue)
                {
                    lastedDate = maxDate.Value;
                }


                ds.MessureValue.Clear();
                测量值TableAdapter1.FillByIDandDate(ds.MessureValue, row.MessureParamID, lastedDate);

                //导入测量值
                foreach (dam3ModeDataSet.MessureValueRow valRow in row.GetChildRows("MessureParamMessureValue") as dam3ModeDataSet.MessureValueRow[])
                {
                    MessureValue messureValue = new MessureValue();

                    if (!valRow.IsValNull())
                    {
                        messureValue.Val = valRow.Val;
                    }
                    messureValue.Date = valRow.Date;
                    messureValue.MessureParamID = mesParam.MessureParamID;

                    mesValueBLL.Add(messureValue);
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
