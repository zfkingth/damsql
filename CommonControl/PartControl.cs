using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using DevExpress.XtraEditors;
using hammergo.Model;

namespace hammergo.CommonControl
{
    public delegate void SearchDelegate();

    public partial class PartControl : DevExpress.XtraTreeList.TreeList
    {
        public event SearchDelegate SearchItemClick = null;
        public PartControl()
        {
            InitializeComponent();


        }

        public void initial()
        {
            appBLL = new hammergo.BLL.ApparatusBLL();
            prjBLL = new hammergo.BLL.ProjectPartBLL();

            partCollection = new BindingList<ProjectPart>(prjBLL.GetList());
            this.DataSource = partCollection;
            partCollection.ListChanged += new ListChangedEventHandler(partCollection_ListChanged);

            if (GlobalConfig.PubConstant.userPower != GlobalConfig.PubConstant.power_admin)
            {
                barButtonItem1.Visibility = barButtonItem2.Visibility = barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                this.AllowDrop = false;
            }

            //折叠所有节点
            this.CollapseAll();
            //expand第一层节点
            if (this.Nodes.Count != 0)
            {
                this.Nodes[0].Expanded = true;
            }

        }

        void partCollection_ListChanged(object sender, ListChangedEventArgs e)
        {

           
            switch (e.ListChangedType)
            {
                case ListChangedType.ItemChanged: 
                    ProjectPart item = ((BindingList<ProjectPart>)sender)[e.NewIndex]; 
                    prjBLL.Update(item); break;
                //case ListChangedType.ItemAdded: prjBLL.Add(item); break;
                // case ListChangedType.ItemDeleted: prjBLL.Delete(item); break;
                default: break;
            }
        }

        private void PartControl_AfterExpand(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {

            this.BestFitColumns();
        }
        public BindingList<ProjectPart> partCollection = null;
        public hammergo.BLL.ApparatusBLL appBLL = null;
        public hammergo.BLL.ProjectPartBLL prjBLL = null;

        private void PartControl_DragDrop(object sender, DragEventArgs e)
        {



            TreeListNode node = null;

            try
            {


                node = e.Data.GetData(typeof(DevExpress.XtraTreeList.Nodes.TreeListNode)) as TreeListNode;

                if (allowEdit && node != null)
                {

                    //get the target oject
                    Point point = new Point(e.X, e.Y);
                    TreeListHitInfo hit = this.CalcHitInfo(this.PointToClient(point));
                    if (hit.Column != null && hit.Node != null)
                    {

                        ProjectPart pp = partCollection[hit.Node.Id] as ProjectPart;
                        if (appBLL.GetCountByProjectPartID(pp.ProjectPartID.Value) != 0)
                        {
                            //  e.Effect = DragDropEffects.None;


                            //  XtraMessageBox.Show(this, "无法将节点拖拽至有仪器的节点上", "提示!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            throw new Exception("无法将节点拖拽至有仪器的节点上");

                        }

                        foreach (TreeListNode tnode in hit.Node.Nodes)
                        {
                            if (node.GetDisplayText(0).Equals(tnode.GetDisplayText(0)))
                            {

                                throw new Exception("同一节点下部位名称不能重复!");
                            }
                        }

                        //this.SaveNodesData();

                    }
                    else
                    {
                        e.Effect = DragDropEffects.None;
                    }



                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
            catch (Exception ex)
            {
                e.Effect = DragDropEffects.None;
                XtraMessageBox.Show(this, ex.Message, "操作错误!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PartControl_GetSelectImage(object sender, GetSelectImageEventArgs e)
        {
            if (e.Node.Nodes.Count != 0)
            {
                if (e.Node.Expanded == false)
                {
                    e.NodeImageIndex = 0;
                }
                else
                {
                    e.NodeImageIndex = 2;
                }
            }
            else
            {
                e.NodeImageIndex = 1;
            }
        }

        TreeListNode operateNode = null;
        /// <summary>
        /// 右键单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PartControl_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //if (allowEdit)
                //{

                //通过disableEdit设置barItem的可视化性
                Point point = new Point(e.X, e.Y);
                TreeListHitInfo hit = this.CalcHitInfo(point);
                if (hit.Column != null && hit.Node != null)
                {
                    //ProjectPart pp = xpCollection1[hit.Node.Id] as ProjectPart;


                    operateNode = hit.Node;

                    popupMenuTreeList.ShowPopup(this.PointToScreen(point));

                }

                //}
            }
        }

        private void PartControl_NodesReloaded(object sender, EventArgs e)
        {

            if (this.Selection.Count == 0)
            {
                if (this.Nodes.Count > 0)
                {
                    this.Nodes[0].Expanded = true;
                }


            }
        }

        bool treelistAllowEdit = false;
        /// <summary>
        /// 在所有editor显示,实例化之前调用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PartControl_ShowingEditor(object sender, CancelEventArgs e)
        {
            e.Cancel = !treelistAllowEdit;
            treelistAllowEdit = false;
        }

        /// <summary>
        /// 工程部位的editor
        /// </summary>
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEditForPartControl;
        /// <summary>
        /// 不在Control 的initial中添加,以避免产生重名的editor
        /// </summary>
        private void initialTextEdit()
        {
            this.repositoryItemTextEditForPartControl = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();

            this.colPartName.ColumnEdit = this.repositoryItemTextEditForPartControl;

            // 
            // repositoryItemTextEditForPartControl
            // 
            this.repositoryItemTextEditForPartControl.AutoHeight = false;
            this.repositoryItemTextEditForPartControl.Name = "repositoryItemTextEditForPartControl";


            this.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEditForPartControl});

        }


        /// <summary>
        /// 重命名部位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (repositoryItemTextEditForPartControl == null)
            {
                initialTextEdit();
            }

            this.FocusedNode = operateNode;
            this.FocusedColumn = this.Columns[0];
            treelistAllowEdit = true;
            this.ShowEditor();
        }

       
        /// <summary>
        /// 添加部位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ProjectPart part = null;
            ProjectPart childPart = null;
            try
            {
                part = partCollection[operateNode.Id] as ProjectPart;
                if (appBLL.GetCountByProjectPartID(part.ProjectPartID.Value) > 0)
                {
                    XtraMessageBox.Show(this, "此节点有与之关联的仪器，无法在此节点下添加部位!", "添加部位", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string newPartName = "新增部位";
                    //查找'新增部位'是否已存在
                    foreach (TreeListNode node in operateNode.Nodes)
                    {
                        if (node.GetDisplayText(0).Equals(newPartName))
                        {
                            throw new Exception(newPartName + "在当前节点下已存在!");
                        }
                    }

                    childPart = new ProjectPart();
                    childPart.ParentPart = part.ProjectPartID;
                    childPart.PartName = newPartName;

                    //int prjID = 0;
                    //int? maxID = prjBLL.getMaxProjectPartID();
                    //if (maxID.HasValue)
                    //{
                    //    prjID = maxID.Value + 1;
                    //}

                    //childPart.ProjectPartID = prjID;

                    childPart.ProjectPartID = Guid.NewGuid();

                    prjBLL.Add(childPart);

                    partCollection.Add(childPart);
                    operateNode.Expanded = true;
                    if (partCollection.Count == 2)
                    {
                        //根结点下面的第一个结点
                        this.RefreshDataSource();
                       
                    }

                    //this.ExpandAll();

                }
            }
            catch (Exception ex)
            {
                string msg = "";
                if (ex.InnerException != null)
                {
                    msg = ex.InnerException.Message;
                }
                else
                {
                    msg = ex.Message;
                }
                XtraMessageBox.Show(this, msg, "错误!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 删除部位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ProjectPart part = null;
            try
            {
                part = partCollection[operateNode.Id] as ProjectPart;
                if (appBLL.GetCountByProjectPartID(part.ProjectPartID.Value) != 0)
                {
                    throw new Exception("有仪器与此部位关联,无法删除!");
                }

                if (prjBLL.GetCountByParentPart(part.ProjectPartID.Value) != 0)
                {
                    throw new Exception("存在子节点,无法删除!");
                }

                if (XtraMessageBox.Show(this, "确定要删除部位 '" + part.PartName + "' 吗?", "删除工程部位", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                    == DialogResult.Yes)
                {

                    int id = operateNode.Id;
                    operateNode = operateNode.ParentNode;
                    this.FocusedNode = operateNode;
                    this.Selection.Clear();
                    partCollection.RemoveAt(id);
                    prjBLL.Delete(part.ProjectPartID.Value);
                    

                }
            }
            catch (Exception ex)
            {
                string msg = "";
                if (ex.InnerException != null)
                {
                    msg = ex.InnerException.Message;
                }
                else
                {
                    msg = ex.Message;
                }
                XtraMessageBox.Show(this, msg, "错误!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PartControl_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            TreeList tl = null;
            TextEdit te = null;
            try
            {
                tl = sender as TreeList;
                te = tl.ActiveEditor as TextEdit;

                if (te != null)//only a edit column,not need judge the column  tl.FocusedColumn.FieldName == "PartName"
                {
                    if (operateNode.ParentNode != null)
                    {
                        foreach (TreeListNode node in operateNode.ParentNode.Nodes)
                        {
                            if (node.GetDisplayText(0).Equals(te.Text))
                            {

                                throw new Exception("同一节点下部位名称不能重复!");
                            }
                        }
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

        private void popupMenuTreeList_BeforePopup(object sender, CancelEventArgs e)
        {
            ProjectPart part = null;


            part = partCollection[operateNode.Id] as ProjectPart;
            int appCnt = appBLL.GetCountByProjectPartID(part.ProjectPartID.Value);

            barButtonItem2.Enabled = (appCnt > 0) ? false : true;
            if (appCnt > 0 || operateNode.Nodes.Count > 0)
            {
                barButtonItem3.Enabled = false;
            }
            else
            {
                barButtonItem3.Enabled = true;
            }

            barButtonItem4.Enabled = (operateNode.Nodes.Count > 0) ? false : true;


        }


        bool allowEdit = true;
        public void disableEdit()
        {
            allowEdit = false;
            barButtonItem1.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barButtonItem2.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            barButtonItem3.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
            AllowDrop = false;

        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (SearchItemClick != null)
            {
               SearchItemClick();

            }
        }



    }
}