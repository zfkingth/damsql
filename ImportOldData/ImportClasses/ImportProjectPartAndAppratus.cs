using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using hammergo.Model;

namespace hammergo.ImportOldData.ImportClasses
{
    public class ImportProjectPartAndAppratus
    {
        ImportDataControl importDataControl = null;

        BackgroundWorker bgw = new BackgroundWorker();

        hammergo.BLL.ProjectPartBLL partBLL = null;
        hammergo.BLL.ApparatusBLL appBLL = null;

        //OldDSTableAdapters.工程部位TableAdapter 工程部位TableAdapter1 = new OldDSTableAdapters.工程部位TableAdapter();

        dam3ModeDataSetTableAdapters.ProjectPartTableAdapter 工程部位TableAdapter1 = new dam3ModeDataSetTableAdapters.ProjectPartTableAdapter();
        //OldDS ds = null;
        dam3ModeDataSet ds = null;

        DateTime startTime;


        private int highestPercentageReached = 0;
        private int handledCnt = 0;//已经处理的行数


        string bgwResult = "";

        public ImportProjectPartAndAppratus(ImportDataControl idc)
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


            partBLL = new hammergo.BLL.ProjectPartBLL();
            appBLL = new hammergo.BLL.ApparatusBLL();
            工程部位TableAdapter1.Connection = importDataControl.oleDbCon;
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
                //工程部位TableAdapter1.Fill(ds.工程部位);

                工程部位TableAdapter1.Fill(ds.ProjectPart);

                //calculate time
                startTime = DateTime.Now;
                /////////////////////////
                importNote(-1, e, null);//the root node's parent is -1 in olde data

              


                //handledCnt = ds.工程部位.Rows.Count;
                handledCnt = ds.ProjectPart.Rows.Count;

                reportProgress();

                bgwResult = "导入成功!";
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null)
                {
                    bgwResult = ex.InnerException.Message;
                }
                else
                {
                    bgwResult = ex.Message;
                }
            }
            ;
        }

       


        //用的是递归方法
        private void importNote(int oldParentID, DoWorkEventArgs e, Guid? newParentID)
        {
            if (bgw.CancellationPending == false)
            {
                //OldDS.工程部位Row[] rows = ds.工程部位.Select(string.Format("{0} = {1}",

               dam3ModeDataSet.ProjectPartRow[] rows = ds.ProjectPart.Select(string.Format("{0} = {1}",

                ds.ProjectPart.ParentPartColumn.ColumnName, oldParentID)) as dam3ModeDataSet.ProjectPartRow[];


               foreach (dam3ModeDataSet.ProjectPartRow row in rows)
                {
                    ProjectPart prjPart = new ProjectPart();

                    prjPart.ProjectPartID = Guid.NewGuid();;//导入部位的行和新行不一致，数据类型不一样

                    prjPart.ParentPart = newParentID;

                    prjPart.PartName = row.PartName;

                    partBLL.Add(prjPart);

                    importApparatusRows(row.GetChildRows("ProjectPartApparatus") as dam3ModeDataSet.ApparatusRow[], prjPart);

                    reportProgress();

                    importNote(row.ProjectPartID, e, prjPart.ProjectPartID);
                }
            }
            else
            {
                e.Cancel = true;
                bgwResult = "导入操作已被取消";
            }
        }

        private void importApparatusRows(dam3ModeDataSet.ApparatusRow[] rows, ProjectPart part)
        {
            foreach (dam3ModeDataSet.ApparatusRow row in rows)
            {
                Apparatus app = new Apparatus();

                if (!row.IsProjectPartIDNull())
                    app.ProjectPartID = part.ProjectPartID;

                app.AppName = row.AppName;
                app.CalculateName = row.CalculateName;
                //if (appBLL.IsValidName(app.AppName))
                //{
                //    app.CalculateName = row.测点编号;
                //}
                //else
                //{
                //    app.CalculateName = "c" + Guid.NewGuid().ToString().Replace("-", "").Substring(0, 19);
                //}

                if (!row.IsXNull())
                    app.X = row.X.ToString();

                if (!row.IsYNull())
                    app.Y = row.Y.ToString();

                if (!row.IsZNull())
                    app.Z = row.Z.ToString();

                if (!row.IsBuriedTimeNull())
                    app.BuriedTime = row.BuriedTime;

                if (!row.IsOtherInfoNull())
                    app.OtherInfo = row.OtherInfo;

                appBLL.Add(app);
            }
        }



        private void reportProgress()
        {
            int percentComplete =
            (int)((handledCnt + 1.0f) / ds.ProjectPart.Rows.Count * 100);

            handledCnt++;

            if (percentComplete > highestPercentageReached)
            {
                highestPercentageReached = percentComplete;
                bgw.ReportProgress(percentComplete);
            }
        }
    }
}
