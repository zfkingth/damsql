using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList.Nodes;
using hammergo.Model;
using DevExpress.XtraTreeList;

namespace hammergo.CommonControl
{
     

    public partial class AppSelector : DevExpress.XtraEditors.XtraUserControl
    {
        public AppSelector()
        {
            InitializeComponent();
        }

        public event EventHandler<hammergo.Utility.AppSearchEventArgs> ShowDataEvent;

        public event EventHandler<AppsEventArgs> SearchExeClick;

        hammergo.BLL.ApparatusBLL appBLL = null;


     


        TreeListNode currentNode = null;
        ProjectPart currentPart = null;
        private void partControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point point = new Point(e.X, e.Y);
                TreeListHitInfo hit = partControl1.CalcHitInfo(point);
                if (hit.Column != null && hit.Node != null)
                {


                    currentNode = hit.Node;


                    currentPart = partControl1.partCollection[currentNode.Id] as ProjectPart;
                    if (currentNode.Nodes.Count == 0)
                    {
                        setAppsByPartInListBox(currentPart);
                    }
                    else
                    {

                        apparatusBindingSource.DataSource = null;
                    }




                }
            }
        }

        private void setAppsByPartInListBox(ProjectPart part)
        {
            //lbcApps.DataSource = part.Apps;
            //lbcApps.DisplayMember = "AppName";
            if (part != null)
            {
                apparatusBindingSource.DataSource = appBLL.GetListByProjectPartID(part.ProjectPartID.Value);
            }
        }

        private void setAppsByPartInListBox(Guid? partID)
        {

            apparatusBindingSource.DataSource = appBLL.GetListByProjectPartID(partID.Value);

        }

        private void partControl1_SearchItemClick()
        {
            Guid? partID = null;

            string strInput = Utility.Utility.InputBox("查找测点", "输入测点编号", "",null);

            if (strInput != null)
            {
                Apparatus app = appBLL.GetModelBy_AppName(strInput);



                if (app != null && app.ProjectPartID != null)
                {
                    partID = app.ProjectPartID;

                    TreeListNode findNode = partControl1.FindNodeByKeyID(partID.Value);

                    if (findNode != null)
                    {
                        currentNode = findNode;
                        currentPart = partControl1.partCollection[currentNode.Id] as ProjectPart;
                        TreeListNode parentNode = findNode;
                        do
                        {
                            parentNode.Expanded = true;
                            parentNode = parentNode.ParentNode;

                        } while (parentNode != null);

                        partControl1.Selection.Clear();
                        findNode.Selected = true;

                        setAppsByPartInListBox(partID);


                    }
                }




            }
        }

        public void initial()
        {
            appBLL = new hammergo.BLL.ApparatusBLL();
            partControl1.initial();
            partControl1.disableEdit();
            
           
        }

        bool _exeButtonEnable = true;

        public bool ExeButtonEnable
        {
            get
            {
                return _exeButtonEnable;
            }
            set
            {
                _exeButtonEnable = value;
                ExeButton.Enabled = _exeButtonEnable;
            }
        }

        public string ExeButtonText
        {
            get
            {
                return ExeButton.Text;
            }
            set
            {
                ExeButton.Text = value;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            foreach (Apparatus app in lbcApps.SelectedItems)
            {
                Utility.Utility.addAppNameInListBox(app.AppName,lbcSelectedApps);
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
            foreach (Apparatus app in apparatusBindingSource)
            {
                Utility.Utility.addAppNameInListBox(app.AppName,lbcSelectedApps);
            }
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            string name = snBox.Text.Trim();
            if (appBLL.ExistsBy_AppName(name))
            {
                Utility.Utility.addAppNameInListBox(name, lbcSelectedApps);
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            if (SearchExeClick != null)
            {
                List<string> names = new List<string>(100);

                foreach (object obj in lbcSelectedApps.Items)
                {
                    names.Add(obj.ToString());
                }
                SearchExeClick(sender,new AppsEventArgs(names));
            }
        }

        private void 原始数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (lbcSelectedApps.SelectedItems.Count > 0)
            {
                if (ShowDataEvent != null)
                {

                    ShowDataEvent(sender,new hammergo.Utility.AppSearchEventArgs( lbcSelectedApps.SelectedValue.ToString()));
                }
            }
        }

        private void menuItemCopy_Click(object sender, EventArgs e)
        {
            Utility.Utility.copyItemInListBox(lbcSelectedApps);
        }

        private void menuItemPaste_Click(object sender, EventArgs e)
        {
            Utility.Utility.pasteItemInListBox(lbcSelectedApps);
        }

        private void lbcApps_DoubleClick(object sender, EventArgs e)
        {
            simpleButton1_Click(null, null);


        }

        private void lbcSelectedApps_DoubleClick(object sender, EventArgs e)
        {
            simpleButton2_Click(null, null);
        }

        private void 所有测点ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Apparatus app in appBLL.GetList())
            {
                Utility.Utility.addAppNameInListBox(app.AppName, lbcSelectedApps);
            }
            
        }


        bool _showAllMenuVisible = false;

        public bool ShowAllAppMenuVisible
        {
            get
            {
                return _showAllMenuVisible;
            }
            set
            {
                _showAllMenuVisible = value;
                所有测点ToolStripMenuItem.Visible = _showAllMenuVisible;
            }
        }
       
    }
}
