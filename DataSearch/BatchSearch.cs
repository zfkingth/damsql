using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using hammergo.GlobalConfig;
using System.IO;
using DevExpress.XtraGrid.Views.Grid;

namespace hammergo.DataSearch
{
    public partial class BatchSearch : DevExpress.XtraEditors.XtraUserControl
    {
        public BatchSearch()
        {
            InitializeComponent();
        }

        hammergo.BLL.TaskAppratusBLL taskAppBLL = null;
        hammergo.BLL.TaskTypeBLL taskTypeBLL = null;
        hammergo.BLL.AppCollectionBLL appColBLL = null;
        hammergo.Model.TaskType taskType = null;
        Tracking.TrackedList<hammergo.Model.AppCollection> appColList = null;

        private void BatchSearch_Load(object sender, EventArgs e)
        {
            c1DateEdit1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            c1DateEdit1.Properties.DisplayFormat.FormatString = PubConstant.customString;

            c1DateEdit2.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            c1DateEdit2.Properties.DisplayFormat.FormatString = PubConstant.customString;

            taskAppBLL = new hammergo.BLL.TaskAppratusBLL();
            taskTypeBLL = new hammergo.BLL.TaskTypeBLL();
            appColBLL = new hammergo.BLL.AppCollectionBLL();

            taskType = taskTypeBLL.GetModelBy_TypeName(PubConstant.searchTaskName);

            appColList = appColBLL.GetListBytaskTypeID(taskType.TaskTypeID.Value);
            appCollectionBindingSource.DataSource = appColList;

        }

        private void appCollectionBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            if (appCollectionBindingSource.Current != null)
            {
                hammergo.Model.AppCollection appCol = appCollectionBindingSource.Current as hammergo.Model.AppCollection;

                if (appCol != null)
                {
                    taskAppratusBindingSource.DataSource = taskAppBLL.GetListByappCollectionID(appCol.AppCollectionID.Value);
                }
            }
        }

        StreamWriter sw = null;

        MonthReport monthReport = null;

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                btnSearch.Enabled = false;
                if (monthReport == null)
                {
                    monthReport = new MonthReport();
                    //只有初始化的时候需要设置
                    monthReport.rDate.Checked = true;

                }

                monthReport.dSDates.Clear();

                monthReport.checkBoxMonthChange.Checked = checkBoxMonthChange.Checked;

                if (c1DateEdit1.EditValue == null)
                {
                    throw new Exception("当前日期不能为空");
                }

                if (c1DateEdit1.EditValue != null)
                {
                    DSDates.DatesRow row = monthReport.dSDates.Dates.NewDatesRow();
                    row.BeginEdit();
                    row.日期时间 = c1DateEdit1.DateTime;
                    row.EndEdit();
                    monthReport.dSDates.Dates.AddDatesRow(row);
                }

                if (c1DateEdit2.EditValue != null)
                {
                    DSDates.DatesRow row = monthReport.dSDates.Dates.NewDatesRow();
                    row.BeginEdit();
                    row.日期时间 = c1DateEdit2.DateTime;
                    row.EndEdit();
                    monthReport.dSDates.Dates.AddDatesRow(row);
                }
                monthReport.dSDates.AcceptChanges();

                if (checkBoxMonthChange.Checked == false && monthReport.dSDates.Dates.Count != 2)
                {
                    throw new Exception("如果不求月变化(求相对变化)，两个日期项必填!");
                }



                //序号
                int start = 0;
                int end = int.MaxValue;

                try
                {
                    start = int.Parse(txtStart.Text.Trim());
                }
                catch (Exception)
                {
                }

                try
                {
                    end = int.Parse(txtEnd.Text.Trim());
                }
                catch (Exception)
                {
                }

                string filterTypeName = txtFilterTypeName.Text.Trim();
                string filterName = txtFilterName.Text.Trim();

                //处理结果文件
                //清除以前的记录
                sw = new StreamWriter("Results.txt", false, System.Text.Encoding.UTF8);
                sw.Close();



                List<hammergo.Model.AppCollection> matchAppColList = appColList.FindAll(delegate(hammergo.Model.AppCollection item) { return item.Order >= start && item.Order <= end; });

                matchAppColList.Sort(new Utility.AppCollectionComparer());


                highestPercentageReached = 0;
                progressBar1.Text = highestPercentageReached.ToString();

                BatchSearchArgs args = new BatchSearchArgs(matchAppColList, filterTypeName, filterName);

                backgroundWorker1.RunWorkerAsync(args);


            }
            catch (Exception wex)
            {
                XtraMessageBox.Show(this, wex.Message, "错误!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                btnSearch.Enabled = true;
            }





        }

        int highestPercentageReached = 0;
        List<string> exceptionTaskList = null;
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // Get the BackgroundWorker that raised this event.
            BackgroundWorker worker = sender as BackgroundWorker;

            try
            {

                BatchSearchArgs args = e.Argument as BatchSearchArgs;


                List<hammergo.Model.AppCollection> matchAppColList = args.MatchAppColList;

                exceptionTaskList = new List<string>(10);

                int count = matchAppColList.Count;

                for (int i = 0; i < count; i++)
                {


                    hammergo.Model.AppCollection appCol = matchAppColList[i];

                    try
                    {
                        Tracking.TrackedList<hammergo.Model.TaskAppratus> taskApps = taskAppBLL.GetListByappCollectionID(appCol.AppCollectionID.Value);

                        List<string> appNameList = new List<string>();

                        foreach (hammergo.Model.TaskAppratus taskApp in taskApps)
                        {
                            appNameList.Add(taskApp.AppName);
                        }

                        DataTable resultTable = monthReport.GetResult(appNameList, args.FilterTypeName, args.FilterName);
                        monthReport.outputResultTable(resultTable, appCol.CollectionName);

                        int percentComplete = (int)((i + 1.0f) / count * 100);

                        if (percentComplete > highestPercentageReached)
                        {
                            highestPercentageReached = percentComplete;
                            worker.ReportProgress(percentComplete, "正在检索......");
                        }

                    }
                    catch (Exception)
                    {
                        exceptionTaskList.Add(appCol.CollectionName);
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


            //打开记事本
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo = new System.Diagnostics.ProcessStartInfo("notepad", "Results.txt");
            p.Start();

            outputException(exceptionTaskList);

            lblInfo.Text = "检索完成";

            btnSearch.Enabled = true;


        }

        private void outputException(List<string> taskNameList)
        {
            if (taskNameList.Count != 0)
            {

                System.Text.StringBuilder builer = new System.Text.StringBuilder(5120);
                for (int i = 0; i < taskNameList.Count; i++)
                {
                    string aname = taskNameList[i];

                    builer.Append(aname).Append(" 存在异常").Append("\r\n");


                }


                sw = new StreamWriter("错误.txt", false, System.Text.Encoding.UTF8);
                sw.Write(builer);
                sw.Close();

                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo = new System.Diagnostics.ProcessStartInfo("notepad", "错误.txt");
                p.Start();

            }


        }

        private void 查找集合ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string appColName = Utility.Utility.InputBox("查找集合", "请输入需要查找的集全的名称:", "", null);

            if (appColName != null)
            {
                Tracking.TrackedList<hammergo.Model.AppCollection> list = appCollectionBindingSource.DataSource as Tracking.TrackedList<hammergo.Model.AppCollection>;

                hammergo.Model.AppCollection appCol = list.Find(delegate(hammergo.Model.AppCollection item) { return item.CollectionName.Trim() == appColName.Trim(); });

                if (appCol != null)
                {

                    GridView gv = PGrid.MainView as GridView;

                    Utility.Utility.selectRow(appCol, gv);
                }
                else
                {
                    XtraMessageBox.Show(this, "不存在此集合", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }

            }
        }

        private void 删除集合ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show(this, "此操作将集合，但不会从数据库中删除集合中测点的信息!\n确认要删除这些集合吗?", "删除集合!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2
                                  ) == DialogResult.Yes)
            {

                GridView gv = PGrid.MainView as GridView;
                int[] indexes = gv.GetSelectedRows();

                hammergo.Model.AppCollection[] delCols = new hammergo.Model.AppCollection[indexes.Length];

                for (int i = 0; i < indexes.Length; i++)
                {
                    hammergo.Model.AppCollection appCol = gv.GetRow(indexes[i]) as hammergo.Model.AppCollection;
                    delCols[i] = appCol;
                }


                for (int i = 0; i < delCols.Length; i++)
                {

                    hammergo.Model.AppCollection appCol = delCols[i];

                    appColBLL.Delete(appCol);

                    appCollectionBindingSource.Remove(appCol);

                }
            }
        }

     

        private int getMaxOrderInList(Tracking.TrackedList<hammergo.Model.AppCollection> list)
        {

            int maxOrder = int.MinValue;
            foreach (hammergo.Model.AppCollection appCol in list)
            {
                if (appCol.Order != null)
                {
                    if (appCol.Order > maxOrder)
                    {
                        maxOrder = appCol.Order.Value;
                    }
                }
            }

            return maxOrder;
        }


        //int appMaxID = 0;
        //private int getNextAppColID()
        //{
        //    if (appMaxID == 0)
        //    {
        //        int? dbMaxID = appColBLL.getMaxAppCollectionID();

        //        if (dbMaxID != null)
        //        {
        //            appMaxID = dbMaxID.Value + 1;
        //        }
        //    }
        //    else
        //    {
        //        appMaxID++;
        //    }

        //    return appMaxID;
        //}


        private void 插入集合ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string collectionName = Utility.Utility.InputBox("新建集合", "请输入新集合的名称", "", null);
                if (collectionName != null)
                {
                    collectionName = collectionName.Trim();
                    if (collectionName.Length != 0)
                    {
                        if (appColList.Exists(delegate(hammergo.Model.AppCollection item) { return item.CollectionName.Trim() == collectionName; })
                            == false)
                        {
                            hammergo.Model.AppCollection appCollection = new hammergo.Model.AppCollection();
                            appCollection.TaskTypeID = taskType.TaskTypeID;
                            appCollection.CollectionName = collectionName;
                            appCollection.AppCollectionID = Guid.NewGuid();
                            appCollection.Order = getCurrentOrder()+1;

                            //添加到数据库中
                            appColBLL.Add(appCollection);

                            //添加到缓存列表中
                             appCollectionBindingSource.Add(appCollection);


                            appCollection.TrackingState = Tracking.TrackingInfo.Unchanged;
                            hammergo.Utility.Utility.selectRow(appCollection,PGrid.MainView as GridView);


                        }
                        else
                        {
                            throw new Exception(string.Format("集合'{0}'已存在", collectionName));

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private int getCurrentOrder()
        {
            hammergo.Model.AppCollection appCol = gridView1.GetFocusedRow() as hammergo.Model.AppCollection;
            if (appCol != null && appCol.Order.HasValue)
            {
                return appCol.Order.Value;
            }
            else
            {
                return 0;
            }
        }

        private void gridView1_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {


            GridView gv = null;
            TextEdit te = null;
            try
            {
                gv = sender as GridView;

                if (gv.FocusedColumn.FieldName == "CollectionName")
                {
                    te = gv.ActiveEditor as TextEdit;

                    object row = gv.GetRow(gv.FocusedRowHandle);
                    if (appColList.Exists(delegate(hammergo.Model.AppCollection item) { return item.CollectionName == e.Value.ToString() && item != row; }))
                    {
                        throw new Exception("不能有重复的任务名称");
                    }




                }
               
            }
            catch (Exception ex)
            {
                e.Valid = false;
                e.ErrorText = ex.Message;
                this.dxErrorProvider1.SetError(te, ex.Message, DevExpress.XtraEditors.DXErrorProvider.ErrorType.Critical);

            }
        }

        private void gridView1_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            hammergo.Model.AppCollection appCollection = e.Row as hammergo.Model.AppCollection;

            appColBLL.Update(appCollection);
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //gridView2.CopyToClipboard();

            Utility.Utility.copyGridSelection(gridView2);
        }

        private void 移除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show(this, "n确认要移除这些测点吗?", "移除!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2
                                 ) == DialogResult.Yes)
            {

                GridView gv = CGrid.MainView as GridView;
                int[] indexes = gv.GetSelectedRows();

                hammergo.Model.TaskAppratus[] taskApps = new hammergo.Model.TaskAppratus[indexes.Length];

                for (int i = 0; i < indexes.Length; i++)
                {
                    hammergo.Model.TaskAppratus taskApp = gv.GetRow(indexes[i]) as hammergo.Model.TaskAppratus;
                    taskApps[i] = taskApp;
                }


                for (int i = 0; i < taskApps.Length; i++)
                {

                    hammergo.Model.TaskAppratus taskApp = taskApps[i];

                    taskAppBLL.Delete(taskApp);

                    taskAppratusBindingSource.Remove(taskApp);



                }
            }
            
        }

        //int max = 0;
        //private int getNextTaskAppID()
        //{
        //    if (max == 0)
        //    {
        //        int? dbMaxID = taskAppBLL.getMaxTaskAppratusID();

        //        if (dbMaxID != null)
        //        {
        //            max = dbMaxID.Value + 1;
        //        }
        //    }
        //    else
        //    {
        //        max++;
        //    }

        //    return max;
        //}

        hammergo.BLL.ApparatusBLL appBLL = null;
        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string clipString = (string)System.Windows.Forms.Clipboard.GetDataObject().GetData(typeof(string));

            if (clipString == null || clipString.Length == 0) return;

            if (appBLL == null)
            {
                appBLL = new hammergo.BLL.ApparatusBLL();
            }


            string[] sns = clipString.Split(new char[] { '\n', '\r', '\t' });

            hammergo.Tracking.TrackedList<hammergo.Model.TaskAppratus> taskApps = taskAppratusBindingSource.DataSource as hammergo.Tracking.TrackedList<hammergo.Model.TaskAppratus>;

           hammergo.Model.AppCollection currentCollection=  appCollectionBindingSource.Current as hammergo.Model.AppCollection;

            if (taskApps!=null&&currentCollection!=null)
            {
                Guid taskId = currentCollection.AppCollectionID.Value;

                
                int index=1;

                for (int i = 0; i < sns.Length; i++)
                {
                    string appName=sns[i];
                    if (appName.Trim().Length != 0)
                    {

                        if (taskApps.Exists(delegate(hammergo.Model.TaskAppratus item) { return item.AppName == appName; }) == false
                            &&appBLL.ExistsBy_AppName(appName)==true )
                        {
                            hammergo.Model.TaskAppratus taskApp = new hammergo.Model.TaskAppratus();
                            taskApp.AppName = appName;
                            taskApp.AppCollectionID = taskId;
                            taskApp.Order = index++;
                           // taskApp.TaskAppratusID = getNextTaskAppID();

                            taskAppratusBindingSource.Add(taskApp);
                        }
                    }
                }

                taskAppBLL.UpdateList(taskApps);

              
            }

        }

        private void pasteDateMenuItem1_Click(object sender, EventArgs e)
        {
            Utility.Utility.handlePasteInDateEdit(c1DateEdit1);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Utility.Utility.handlePasteInDateEdit(c1DateEdit2);
        }





    }
}
