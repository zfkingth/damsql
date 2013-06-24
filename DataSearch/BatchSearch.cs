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
                    //ֻ�г�ʼ����ʱ����Ҫ����
                    monthReport.rDate.Checked = true;

                }

                monthReport.dSDates.Clear();

                monthReport.checkBoxMonthChange.Checked = checkBoxMonthChange.Checked;

                if (c1DateEdit1.EditValue == null)
                {
                    throw new Exception("��ǰ���ڲ���Ϊ��");
                }

                if (c1DateEdit1.EditValue != null)
                {
                    DSDates.DatesRow row = monthReport.dSDates.Dates.NewDatesRow();
                    row.BeginEdit();
                    row.����ʱ�� = c1DateEdit1.DateTime;
                    row.EndEdit();
                    monthReport.dSDates.Dates.AddDatesRow(row);
                }

                if (c1DateEdit2.EditValue != null)
                {
                    DSDates.DatesRow row = monthReport.dSDates.Dates.NewDatesRow();
                    row.BeginEdit();
                    row.����ʱ�� = c1DateEdit2.DateTime;
                    row.EndEdit();
                    monthReport.dSDates.Dates.AddDatesRow(row);
                }
                monthReport.dSDates.AcceptChanges();

                if (checkBoxMonthChange.Checked == false && monthReport.dSDates.Dates.Count != 2)
                {
                    throw new Exception("��������±仯(����Ա仯)���������������!");
                }



                //���
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

                //�������ļ�
                //�����ǰ�ļ�¼
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
                XtraMessageBox.Show(this, wex.Message, "����!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
                            worker.ReportProgress(percentComplete, "���ڼ���......");
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


            //�򿪼��±�
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo = new System.Diagnostics.ProcessStartInfo("notepad", "Results.txt");
            p.Start();

            outputException(exceptionTaskList);

            lblInfo.Text = "�������";

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

                    builer.Append(aname).Append(" �����쳣").Append("\r\n");


                }


                sw = new StreamWriter("����.txt", false, System.Text.Encoding.UTF8);
                sw.Write(builer);
                sw.Close();

                System.Diagnostics.Process p = new System.Diagnostics.Process();
                p.StartInfo = new System.Diagnostics.ProcessStartInfo("notepad", "����.txt");
                p.Start();

            }


        }

        private void ���Ҽ���ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            string appColName = Utility.Utility.InputBox("���Ҽ���", "��������Ҫ���ҵļ�ȫ������:", "", null);

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
                    XtraMessageBox.Show(this, "�����ڴ˼���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }

            }
        }

        private void ɾ������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show(this, "�˲��������ϣ�����������ݿ���ɾ�������в�����Ϣ!\nȷ��Ҫɾ����Щ������?", "ɾ������!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2
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


        private void ���뼯��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string collectionName = Utility.Utility.InputBox("�½�����", "�������¼��ϵ�����", "", null);
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

                            //��ӵ����ݿ���
                            appColBLL.Add(appCollection);

                            //��ӵ������б���
                             appCollectionBindingSource.Add(appCollection);


                            appCollection.TrackingState = Tracking.TrackingInfo.Unchanged;
                            hammergo.Utility.Utility.selectRow(appCollection,PGrid.MainView as GridView);


                        }
                        else
                        {
                            throw new Exception(string.Format("����'{0}'�Ѵ���", collectionName));

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "����", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
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
                        throw new Exception("�������ظ�����������");
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

        private void ����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //gridView2.CopyToClipboard();

            Utility.Utility.copyGridSelection(gridView2);
        }

        private void �Ƴ�ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show(this, "nȷ��Ҫ�Ƴ���Щ�����?", "�Ƴ�!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2
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
        private void ճ��ToolStripMenuItem_Click(object sender, EventArgs e)
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
