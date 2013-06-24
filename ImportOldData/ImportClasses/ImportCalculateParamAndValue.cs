using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using hammergo.Model;


namespace hammergo.ImportOldData.ImportClasses
{
    public class ImportCalculateParamAndValue
    {
        ImportDataControl importDataControl = null;

        BackgroundWorker bgw = new BackgroundWorker();

        hammergo.BLL.CalculateParamBLL calcParamBLL = new hammergo.BLL.CalculateParamBLL();
        hammergo.BLL.CalculateValueBLL calcValueBLL = new hammergo.BLL.CalculateValueBLL();

        //OldDSTableAdapters.计算量TableAdapter 计算量TableAdapter1 = new OldDSTableAdapters.计算量TableAdapter();

        dam3ModeDataSetTableAdapters.CalculateParamTableAdapter 计算量TableAdapter1 = new dam3ModeDataSetTableAdapters.CalculateParamTableAdapter();

        //OldDSTableAdapters.计算值TableAdapter 计算值TableAdapter1 = new OldDSTableAdapters.计算值TableAdapter();

        dam3ModeDataSetTableAdapters.CalculateValueTableAdapter 计算值TableAdapter1 = new dam3ModeDataSetTableAdapters.CalculateValueTableAdapter();


        dam3ModeDataSet ds = null;

        DateTime startTime;




        private int highestPercentageReached = 0;
        private int handledCnt = 0;//已经处理的行数


        string bgwResult = "";

        public ImportCalculateParamAndValue(ImportDataControl idc)
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

            计算量TableAdapter1.Connection = importDataControl.oleDbCon;
            计算值TableAdapter1.Connection = importDataControl.oleDbCon;
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
                计算量TableAdapter1.Fill(ds.CalculateParam);


                /////////////////////////////


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
                    importCalcRows(row.GetChildRows("ApparatusCalculateParam") as dam3ModeDataSet.CalculateParamRow[], row.AppName.ToUpper());


                    reportProgress();
                }
            }
            else
            {
                e.Cancel = true;
                bgwResult = "导入操作已被取消";
            }
        }

        private CalculateParam createNewCalculateParam(string appName, dam3ModeDataSet.CalculateParamRow row)
        {

            CalculateParam cp = calcParamBLL.GetModelBy_appName_ParamName(appName,row.ParamName);// calcParamBLL.GetModelBy_CalculateParamID(id);


            if (cp == null)
            {
                cp = new CalculateParam();
                cp.CalculateParamID = Guid.NewGuid();
                cp.AppName = appName;

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

                if (!row.IsCalculateExpressNull())
                {
                    cp.CalculateExpress = row.CalculateExpress;
                }

                if (!row.IsCalculateOrderNull())
                {
                    cp.CalculateOrder = row.CalculateOrder;
                }

                calcParamBLL.Add(cp);
            }

            return cp;
        }


        /// <summary>
        /// 导入一支仪器的计算量和计算值表
        /// </summary>
        /// <param name="rows"></param>
        /// <param name="测点编号"></param>
        private void importCalcRows(dam3ModeDataSet.CalculateParamRow[] rows, string 测点编号)
        {
            foreach (dam3ModeDataSet.CalculateParamRow row in rows)
            {
                CalculateParam cp = createNewCalculateParam(测点编号, row);
                DateTime lastedDate = DateTime.MinValue;

                DateTime? maxDate = calcValueBLL.getMaxDate(cp.CalculateParamID);

                if (maxDate.HasValue)
                {
                    lastedDate = maxDate.Value;
                }


                ds.CalculateValue.Clear();
                // 计算值TableAdapter1.FillByID(ds.计算值, row.ID);
                计算值TableAdapter1.FillByIDandDate(ds.CalculateValue, row.CalculateParamID, lastedDate);

                //导入计算值
                foreach (dam3ModeDataSet.CalculateValueRow valRow in row.GetChildRows("CalculateParamCalculateValue") as dam3ModeDataSet.CalculateValueRow[])
                {
                    CalculateValue calcValue = new CalculateValue();

                    if (!valRow.IsValNull())
                    {
                        calcValue.Val = valRow.Val;
                    }
                    calcValue.Date = valRow.Date;
                    calcValue.CalculateParamID = cp.CalculateParamID;
                    calcValueBLL.Add(calcValue);
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
