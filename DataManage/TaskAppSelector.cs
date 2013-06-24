using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using hammergo.Model;
using hammergo.GlobalConfig;

namespace hammergo.DataManage
{
    public partial class TaskAppSelector : DevExpress.XtraEditors.XtraUserControl
    {
        public TaskAppSelector()
        {
            InitializeComponent();
        }


        hammergo.BLL.TaskAppratusBLL taskAppBLL = null;
        hammergo.BLL.TaskTypeBLL taskTypeBLL = null;
        hammergo.BLL.AppCollectionBLL appCollectionBLL = null;


        private void TaskAppSelector_Load(object sender, EventArgs e)
        {
           

           
        }

        public void initial()
        {
            taskAppBLL = new hammergo.BLL.TaskAppratusBLL();
            taskTypeBLL = new hammergo.BLL.TaskTypeBLL();
            appCollectionBLL = new hammergo.BLL.AppCollectionBLL();

            TaskType type = taskTypeBLL.GetModelBy_TypeName(PubConstant.inputTaskName);
            if (type != null)
            {
                appCollectionBindingSource.DataSource = appCollectionBLL.GetListBytaskTypeID(type.TaskTypeID.Value);

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

                   
                }
            }
        }

        public void selectTask(Guid appCollectionID)
        {
            for(int i=0;i< appCollectionBindingSource.Count;i++)
            {
                AppCollection collection=appCollectionBindingSource[i] as AppCollection;

                if (collection.AppCollectionID.Value == appCollectionID)
                {
                    appCollectionBindingSource.Position = i;
                    break;
                }
            }

            //全部选中
            simpleButton4_Click(null,null);

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            foreach (TaskAppratus app in lbcApps.SelectedItems)
            {
                Utility.Utility.addAppNameInListBox(app.AppName, lbcSelectedApps);
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            List<object> list = new List<object>(10);
            foreach (object obj in lbcSelectedApps.SelectedItems)
            {
                list.Add(obj);
            }

            foreach (object obj in list)
            {
                lbcSelectedApps.Items.Remove(obj);
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            lbcSelectedApps.Items.Clear();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            foreach (TaskAppratus app in taskAppratusBindingSource)
            {
                Utility.Utility.addAppNameInListBox(app.AppName, lbcSelectedApps);
            }
        }

        private void lbcApps_DoubleClick(object sender, EventArgs e)
        {
            simpleButton1_Click(null, null);
        }

        private void lbcSelectedApps_DoubleClick(object sender, EventArgs e)
        {
            simpleButton2_Click(null, null);
        }

        private void menuItemPaste_Click(object sender, EventArgs e)
        {
            Utility.Utility.pasteItemInListBox(lbcSelectedApps);
        }

        private void menuItemCopy_Click(object sender, EventArgs e)
        {
            Utility.Utility.copyItemInListBox(lbcSelectedApps);
        }

        public event EventHandler<hammergo.Utility.AppSearchEventArgs> ShowDataEvent;
        private void 原始数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lbcSelectedApps.SelectedItems.Count > 0)
            {
                if (ShowDataEvent != null)
                {

                    ShowDataEvent(sender, new hammergo.Utility.AppSearchEventArgs(lbcSelectedApps.SelectedValue.ToString()));
                }
            }
        }

      
    }
}
