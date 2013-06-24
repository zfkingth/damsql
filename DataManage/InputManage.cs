using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using hammergo.GlobalConfig;
using hammergo.Model;
using DevExpress.XtraGrid.Views.Grid;

namespace hammergo.DataManage
{
    public partial class InputManage : DevExpress.XtraEditors.XtraUserControl
    {
        public InputManage()
        {
            InitializeComponent();
        }

        hammergo.BLL.TaskAppratusBLL taskAppBLL = null;
        hammergo.BLL.TaskTypeBLL taskTypeBLL = null;
        hammergo.BLL.AppCollectionBLL appCollectionBLL = null;
        hammergo.BLL.ApparatusBLL appBLL = null;

        TaskType taskType = null;
        private void InputManage_Load(object sender, EventArgs e)
        {
            taskAppBLL = new hammergo.BLL.TaskAppratusBLL();
            taskTypeBLL = new hammergo.BLL.TaskTypeBLL();
            appCollectionBLL = new hammergo.BLL.AppCollectionBLL();
            appBLL = new hammergo.BLL.ApparatusBLL();

             taskType = taskTypeBLL.GetModelBy_TypeName(PubConstant.inputTaskName);
            if (taskType != null)
            {
                appCollectionBindingSource.DataSource = appCollectionBLL.GetListBytaskTypeID(taskType.TaskTypeID.Value);

            }
        }

        private void appCollectionBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            if (appCollectionBindingSource.Current != null)
            {

                hammergo.Model.AppCollection appCol = appCollectionBindingSource.Current as hammergo.Model.AppCollection;

                if (appCol != null)
                {

                    taskAppratusBindingSource.DataSource = taskAppBLL.GetListByappCollectionID(appCol.AppCollectionID.Value);

                    gridApps.FocusedRowHandle = 0;
                }
            }
        }

        private void ����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //gridApps.CopyToClipboard();
            Utility.Utility.copyGridSelection(gridApps);
        }

        private void ɾ��ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show(this, "�˲������Ƴ�ѡ������!\nȷ��Ҫ�Ƴ���Щ������?", "�Ƴ�����!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2
                                  ) == DialogResult.Yes)
            {
                int[] indexes = gridApps.GetSelectedRows();
                TaskAppratus[] delApps = new TaskAppratus[indexes.Length];
                for (int i = 0; i < indexes.Length; i++)
                {
                    TaskAppratus app = gridApps.GetRow(indexes[i]) as TaskAppratus;
                    delApps[i] = app;
                }

                for (int i = 0; i < delApps.Length; i++)
                {

                    TaskAppratus app = delApps[i];


                    taskAppratusBindingSource.Remove(app);


                }

                hammergo.Tracking.TrackedList<TaskAppratus> list = taskAppratusBindingSource.DataSource as hammergo.Tracking.TrackedList<TaskAppratus>;

                taskAppBLL.UpdateList(list);

            }
        }

      





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

            hammergo.Model.AppCollection currentCollection = appCollectionBindingSource.Current as hammergo.Model.AppCollection;

            if (taskApps != null && currentCollection != null)
            {
                Guid taskId = currentCollection.AppCollectionID.Value;


                int index = taskApps.Count + 1;

                for (int i = 0; i < sns.Length; i++)
                {
                    string appName = sns[i];
                    if (appName.Trim().Length != 0)
                    {

                        if (taskApps.Exists(delegate(hammergo.Model.TaskAppratus item) { return item.AppName == appName; }) == false
                            && appBLL.ExistsBy_AppName(appName) == true)
                        {
                            hammergo.Model.TaskAppratus taskApp = new hammergo.Model.TaskAppratus();
                            taskApp.AppName = appName;
                            taskApp.AppCollectionID = taskId;
                            taskApp.Order = index++;
                            //taskApp.TaskAppratusID = getNextTaskAppID();

                            taskAppratusBindingSource.Add(taskApp);
                        }
                    }
                }

                taskAppBLL.UpdateList(taskApps);


            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //gridTasks.CopyToClipboard();
            Utility.Utility.copyGridSelection(gridTasks);
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

            AppCollection task = gridTasks.GetFocusedRow() as AppCollection;

            if (task != null)
            {

                if (XtraMessageBox.Show(this, string.Format("ɾ����������  '{0}' ��?", task.CollectionName), "ɾ����������!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2
                                     ) == DialogResult.Yes)
                {

                    appCollectionBindingSource.Remove(task);


                    hammergo.Tracking.TrackedList<AppCollection> list = appCollectionBindingSource.DataSource as hammergo.Tracking.TrackedList<AppCollection>;

                    appCollectionBLL.UpdateList(list);

                }
            }
        }

      

        private int getCurrentOrder()
        {
            hammergo.Model.AppCollection appCol = gridTasks.GetFocusedRow() as hammergo.Model.AppCollection;
            if (appCol != null && appCol.Order.HasValue)
            {
                return appCol.Order.Value;
            }
            else
            {
                return 0;
            }
        }


        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            try
            {
                string collectionName = Utility.Utility.InputBox("�½�����", "�����������������", "", null);
                if (collectionName != null)
                {
                    hammergo.Tracking.TrackedList<AppCollection> appColList = appCollectionBindingSource.DataSource as hammergo.Tracking.TrackedList<AppCollection>;

                    collectionName = collectionName.Trim();
                    if (collectionName.Length != 0)
                    {
                        if (appColList.Exists(delegate(hammergo.Model.AppCollection item) { return item.CollectionName.Trim() == collectionName; })
                            == false)
                        {
                            hammergo.Model.AppCollection appCollection = new hammergo.Model.AppCollection();
                            appCollection.TaskTypeID = taskType.TaskTypeID;
                            appCollection.CollectionName = collectionName;
                            appCollection.AppCollectionID = Guid.NewGuid(); //getNextAppColID();
                            appCollection.Order = getCurrentOrder() + 1;

                            //��ӵ����ݿ���
                            appCollectionBLL.Add(appCollection);

                            //��ӵ������б���
                            appCollectionBindingSource.Add(appCollection);


                            appCollection.TrackingState = Tracking.TrackingInfo.Unchanged;
                            hammergo.Utility.Utility.selectRow(appCollection, gridTasks);


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

        private void gridTasks_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {


            GridView gv = null;
            TextEdit te = null;
            try
            {
                gv = sender as GridView;

                if (gv.FocusedColumn.FieldName == "CollectionName")
                {
                    te = gv.ActiveEditor as TextEdit;

                    hammergo.Tracking.TrackedList<AppCollection> appColList = appCollectionBindingSource.DataSource as hammergo.Tracking.TrackedList<AppCollection>;


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

        private void gridTasks_RowUpdated(object sender, DevExpress.XtraGrid.Views.Base.RowObjectEventArgs e)
        {
            hammergo.Model.AppCollection appCollection = e.Row as hammergo.Model.AppCollection;

            appCollectionBLL.Update(appCollection);
        }
    }
}
