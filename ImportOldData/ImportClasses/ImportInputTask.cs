using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using hammergo.Model;

namespace hammergo.ImportOldData.ImportClasses
{
    public class ImportInputTask
    {
        ImportDataControl importDataControl = null;

        BackgroundWorker bgw = new BackgroundWorker();

        hammergo.BLL.TaskAppratusBLL taskAppBLL = new hammergo.BLL.TaskAppratusBLL();
        hammergo.BLL.AppCollectionBLL appColBLL = new hammergo.BLL.AppCollectionBLL();
        hammergo.BLL.TaskTypeBLL taskTypeBLL = new hammergo.BLL.TaskTypeBLL();


        //OldDSTableAdapters.输入任务TableAdapter 输入任务TableAdapter1 = new OldDSTableAdapters.输入任务TableAdapter();

        dam3ModeDataSetTableAdapters.AppCollectionTableAdapter appCollectionTableAdapter = new dam3ModeDataSetTableAdapters.AppCollectionTableAdapter();


        dam3ModeDataSetTableAdapters.TaskTypeTableAdapter taskTypeTableAdapter = new dam3ModeDataSetTableAdapters.TaskTypeTableAdapter();

        dam3ModeDataSetTableAdapters.TaskAppratusTableAdapter taskAppTableAdapter = new dam3ModeDataSetTableAdapters.TaskAppratusTableAdapter();


        //OldDSTableAdapters.任务仪器TableAdapter 任务仪器TableAdapter1 = new OldDSTableAdapters.任务仪器TableAdapter();

        dam3ModeDataSet ds = null;

        DateTime startTime;


        private int highestPercentageReached = 0;
        private int handledCnt = 0;//已经处理的行数


        string bgwResult = "";

        public ImportInputTask(ImportDataControl idc)
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

            appCollectionTableAdapter.Connection = importDataControl.oleDbCon;
            taskTypeTableAdapter.Connection = importDataControl.oleDbCon;
            taskAppTableAdapter.Connection = importDataControl.oleDbCon;

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

        //TaskType taskType = null;
        readonly string typeName = hammergo.GlobalConfig.PubConstant.inputTaskName;
        void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                //输入任务TableAdapter1.Fill(ds.输入任务);
                //任务仪器TableAdapter1.Fill(ds.任务仪器);

                appCollectionTableAdapter.Fill(ds.AppCollection);
                taskTypeTableAdapter.Fill(ds.TaskType);
                taskAppTableAdapter.Fill(ds.TaskAppratus);



                //在TaskType Table中添加"输入任务"作为一个record

                //taskType = taskTypeBLL.GetModelBy_TypeName(typeName);
                //if (taskType == null)
                //{
                //    //尚未添加
                //    int maxID = 0;
                //    int? id = taskTypeBLL.getMaxTaskTypeID();
                //    if (id.HasValue)
                //    {
                //        maxID = id.Value + 1;
                //    }
                //    taskType = new TaskType();
                //    taskType.TaskTypeID = maxID;
                //    taskType.TypeName = typeName;
                //    taskTypeBLL.Add(taskType);
                //}

                //calculate time
                startTime = DateTime.Now;
                /////////////////////////
                importRows(e);

                handledCnt = ds.TaskAppratus.Rows.Count;

                reportProgress();

                bgwResult = "导入成功!";
            }
            catch (Exception ex)
            {
                bgwResult = string.Format("旧数据库 {0} 编号:{1} 名称：{2} \n {3}", typeName, currentid, currentName, ex.Message);
            }
            ;
        }


        string currentName = "";
        int currentid = 0;
        private void importRows(DoWorkEventArgs e)
        {
            if (bgw.CancellationPending == false)
            {
                //导入taskType
                foreach (dam3ModeDataSet.TaskTypeRow tr in ds.TaskType)
                {
                    TaskType taskType = new TaskType();
                    taskType.TaskTypeID = tr.TaskTypeID;
                    taskType.TypeName = tr.TypeName;
                    taskTypeBLL.Add(taskType);

                    //导入taskAppcolection
                    foreach (dam3ModeDataSet.AppCollectionRow row in tr.GetChildRows("TaskTypeAppCollection") as dam3ModeDataSet.AppCollectionRow[])
                    {
                        AppCollection appCollection = new AppCollection();

                        if (!row.IsCollectionNameNull())
                        {
                            appCollection.CollectionName = row.CollectionName.Trim();
                            //currentName = row.任务名称;
                        }

                        if (!row.IsOrderNull())
                        {
                            appCollection.Order = row.Order;
                        }

                        if (!row.IsDescriptionNull())
                        {
                            appCollection.Description = row.Description;
                        }



                        appCollection.TaskTypeID = taskType.TaskTypeID;

                        //int maxid = 0;
                        //int? id = appColBLL.getMaxAppCollectionID();
                        //if (id.HasValue)
                        //{
                        //    maxid = id.Value + 1;
                        //}

                        appCollection.AppCollectionID = Guid.NewGuid();

                        appColBLL.Add(appCollection);

                        importConstantRows(row.GetChildRows("AppCollectionTaskAppratus") as dam3ModeDataSet.TaskAppratusRow[], appCollection);
                        reportProgress();
                    }
                }
               
            }
            else
            {
                e.Cancel = true;
                bgwResult = "导入操作已被取消";
            }
        }

        private void importConstantRows(dam3ModeDataSet.TaskAppratusRow[] rows, AppCollection appCollection)
        {
            foreach (dam3ModeDataSet.TaskAppratusRow row in rows)
            {
                TaskAppratus taskAppratus = new TaskAppratus();

                taskAppratus.AppCollectionID = appCollection.AppCollectionID;

                taskAppratus.AppName = row.appName.ToUpper();

                //int? id = taskAppBLL.getMaxTaskAppratusID();

                //int maxid = 0;
                //if (id.HasValue)
                //{
                //    maxid = id.Value + 1;
                //}

                //taskAppratus.TaskAppratusID = maxid;


                if (!row.IsOrderNull())
                {
                    taskAppratus.Order = row.Order;
                }

                taskAppBLL.Add(taskAppratus);
            }
        }



        private void reportProgress()
        {
            int percentComplete =
            (int)((handledCnt + 1.0f) / ds.TaskAppratus.Rows.Count * 100);

            handledCnt++;

            if (percentComplete > highestPercentageReached)
            {
                highestPercentageReached = percentComplete;
                bgw.ReportProgress(percentComplete);
            }
        }
    }
}
