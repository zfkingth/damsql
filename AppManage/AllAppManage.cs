using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTreeList;
using hammergo.Model;
using DevExpress.XtraTreeList.Nodes;
using hammergo.CommonControl;
using DevExpress.XtraGrid.Views.Grid;
using hammergo.Tracking;

namespace hammergo.AppManage
{
    public partial class AllAppManage : DevExpress.XtraEditors.XtraUserControl, hammergo.Utility.IShowAppData
    {
        public AllAppManage()
        {
            InitializeComponent();
        }

        public event EventHandler<hammergo.Utility.AppSearchEventArgs> ShowDataEvent;

        hammergo.BLL.ApparatusBLL appBLL = null;

        hammergo.BLL.ApparatusTypeBLL typeBLL = null;

        TrackedList<Apparatus> allAppList = null;
        ExportLib.GridViewExport exporter = null;
        private void AllAppManage_Load(object sender, EventArgs e)
        {
            partControl1.initial();
            appBLL = partControl1.appBLL;
            typeBLL = new hammergo.BLL.ApparatusTypeBLL();

            allAppList = appBLL.GetList();

            bsApp.DataSource = allAppList;
            bsType.DataSource = typeBLL.GetList();

            exporter = new ExportLib.GridViewExport(gridView1, "仪器");

            //设置权限

            if (GlobalConfig.PubConstant.userPower != GlobalConfig.PubConstant.power_admin)
            {
                关联仪器ToolStripMenuItem.Visible = 添加仪器Item.Visible = 删除仪器ToolStripMenuItem.Visible = false;
            }

        }


        ProjectPart currentPart = null;
        TreeListNode currentNode = null;
        private void partControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point point = new Point(e.X, e.Y);
                TreeListHitInfo hit = partControl1.CalcHitInfo(point);
                if (hit.Column != null && hit.Node != null)
                {
                    gridView1.ClearSelection();

                    currentNode = hit.Node;


                    currentPart = partControl1.partCollection[currentNode.Id] as ProjectPart;

                    setAppsByPartInGrid(currentPart);

                }
            }
        }


        private void setAppsByPartInGrid(ProjectPart part)
        {
            if (part != null)
            {
                Guid? findPartId = part.ProjectPartID;
                bsApp.DataSource = allAppList.FindAll(delegate(Apparatus app) { return app.ProjectPartID == findPartId; });
            }


        }

        private void setAppsByPartInGrid(Guid? partID)
        {
            bsApp.DataSource = allAppList.FindAll(delegate(Apparatus app) { return app.ProjectPartID == partID; });
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //gridView1.CopyToClipboard();
            Utility.Utility.copyGridSelection(gridView1);
        }

        private void 仪器数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] indexes = gridView1.GetSelectedRows();
            if (indexes.Length > 0)
            {
                Apparatus app = gridView1.GetRow(indexes[0]) as Apparatus;

                if (ShowDataEvent != null)
                {
                    ShowDataEvent(sender, new hammergo.Utility.AppSearchEventArgs(app.AppName));
                }


            }

        }

        private void 关联仪器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>(10);
            gridControl1.SuspendLayout();

            PasteAppForm paf = new PasteAppForm(list, "选择关联此部位的仪器!");


            if (paf.ShowDialog() == DialogResult.OK)
            {

                foreach (string s in list)
                {
                    Apparatus findApp = allAppList.Find(delegate(Apparatus app) { return app.AppName == s.ToUpper(); });

                    if (findApp != null)
                    {


                        findApp.ProjectPartID = currentPart.ProjectPartID;
                        appBLL.Update(findApp);


                    }

                }
            }
            gridControl1.ResumeLayout();
            setAppsByPartInGrid(currentPart);
        }

        private void 所有仪器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            bsApp.DataSource = allAppList;
        }

        private void 删除仪器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show(this, "此操作将删除选中仪器及其所有数据，且不可恢复!\n确认要删除这些仪器吗?", "删除仪器!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2
                                   ) == DialogResult.Yes)
            {
                int[] indexes = gridView1.GetSelectedRows();
                Apparatus[] delApps = new Apparatus[indexes.Length];
                for (int i = 0; i < indexes.Length; i++)
                {
                    Apparatus app = gridView1.GetRow(indexes[i]) as Apparatus;
                    delApps[i] = app;
                }


                for (int i = 0; i < delApps.Length; i++)
                {

                    Apparatus app = delApps[i];

                    appBLL.Delete(app);

                    bsApp.Remove(app);


                    if (bsApp.DataSource != allAppList)
                    {
                        allAppList.Remove(app);
                    }

                }
            }
        }


        void tsmiXml_Click(object sender, EventArgs e)
        {
            exporter.sbExportToXML_Click(sender, e);

        }

    

        void tsmiTxt_Click(object sender, EventArgs e)
        {
            exporter.sbExportToTXT_Click(sender, e);
        }

        void tsmiExcel_Click(object sender, EventArgs e)
        {
            exporter.sbExportToXLS_Click(sender, e);

        }

        private void 设置仪器参数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int[] indexes = gridView1.GetSelectedRows();
            if (indexes.Length > 0)
            {
                Apparatus app = gridView1.GetRow(indexes[0]) as Apparatus;

                //  AppParamsManage apm = new AppParamsManage(app);
                ParamsForm apm = new ParamsForm(app.AppName);

                DialogResult result = apm.ShowDialog();

            }
        }

        private void gridView1_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName == "CalculateName")
            {
                if (appBLL.IsValidName(e.Value.ToString()) == false)
                {
                    throw new Exception("计算编号只能由英文和数字组成，而且必英文打头");
                }


            }
        }

        private void gridView1_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            GridView gv = null;
            TextEdit te = null;
            try
            {
                gv = sender as GridView;

                if (gv.FocusedColumn.FieldName == "CalculateName")
                {
                    te = gv.ActiveEditor as TextEdit;
                    if (appBLL.IsValidName(e.Value.ToString()) == false)
                    {
                        throw new Exception("计算编号只能由英文和数字组成，而且必英文打头");
                    }

                    if (allAppList.Exists(delegate(Apparatus app) { return app.CalculateName.Equals(e.Value.ToString(), StringComparison.InvariantCultureIgnoreCase); }))
                    {
                        throw new Exception("存在重名的计算编号");
                    }


                }
                else if (gv.FocusedColumn.FieldName == "AppName")
                {
                    te = gv.ActiveEditor as TextEdit;

                    if (allAppList.Exists(delegate(Apparatus app) { return app.AppName.Equals(e.Value.ToString(), StringComparison.InvariantCultureIgnoreCase); }))
                    {
                        throw new Exception("存在重名的测点编号");
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
            appBLL.Update(e.Row as Apparatus);
        }

        private void 添加仪器Item_Click(object sender, EventArgs e)
        {
            try
            {
                string newAppName = Utility.Utility.InputBox("输入", "请输入仪器名称", "newApp", null);

                if (newAppName != null)
                {
                    newAppName = newAppName.Trim();

                    if (appBLL.ExistsBy_AppName(newAppName))
                    {
                        throw new Exception("该名称已存在");
                    }

                    string newCalcName = Utility.Utility.InputBox("输入", "请输入仪器计算名称(在其它仪器需要引用此仪器的数据时使用,只能由英文和数字组成)", newAppName, null);

                    if (newCalcName != null)
                    {
                        newCalcName = newCalcName.Trim();

                        if (appBLL.IsValidName(newCalcName) == false)
                        {
                            throw new Exception("计算名称只能由字母和数字组成,而且必须以字母打头!");
                        }


                        if (appBLL.ExistsBy_CalculateName(newCalcName))
                        {
                            throw new Exception("该计算名称已存在");
                        }

                        //输入克隆测点的名称
                        string cloneName = Utility.Utility.InputBox("输入", "请输入模板仪器名称(不使用模板点击'取消')", "clone name", null);
                        if (cloneName == null)
                        {
                            //不使用模板,直接添加

                            Apparatus newApp = new Apparatus();
                            newApp.AppName = newAppName;
                            newApp.CalculateName = newCalcName;
                            newApp.ProjectPartID = currentPart.ProjectPartID;

                            appBLL.Add(newApp);

                            bsApp.Add(newApp);
                            if (bsApp.DataSource != allAppList)
                            {
                                allAppList.Add(newApp);
                            }


                            // setAppsByPartInGrid(currentPart);

                        }
                        else
                        {
                            //使用模板
                            Apparatus modeApp = null;
                            modeApp = appBLL.GetModelBy_AppName(cloneName);
                            if (modeApp == null)
                            {
                                throw new Exception("模板仪器不存在!");
                            }


                            Apparatus newApp = new Apparatus();
                            newApp.AppName = newAppName;
                            newApp.CalculateName = newCalcName;
                            newApp.ProjectPartID = currentPart.ProjectPartID;
                            newApp.AppTypeID = modeApp.AppTypeID;

                            hammergo.Utility.AppIntegratedInfo appInfo = new hammergo.Utility.AppIntegratedInfo(cloneName, 1, null, null);

                            hammergo.Tracking.TrackedList<MessureParam> newMesParams = new TrackedList<MessureParam>(10);
                            hammergo.Tracking.TrackedList<CalculateParam> newCalcParams = new TrackedList<CalculateParam>(10);
                            hammergo.Tracking.TrackedList<ConstantParam> newConsParams = new TrackedList<ConstantParam>(10);

                            newConsParams.Tracking = newMesParams.Tracking = newCalcParams.Tracking = true;//保证更新成功

                            foreach (ConstantParam item in appInfo.ConstantParams)
                            {
                                ConstantParam newItem = new ConstantParam();
                                newItem.ConstantParamID = Guid.NewGuid();

                                newItem.AppName = newAppName;
                                newItem.Description = item.Description;
                                newItem.Order = item.Order;
                                newItem.ParamName = item.ParamName;
                                newItem.ParamSymbol = item.ParamSymbol;
                                newItem.PrecisionNum = item.PrecisionNum;
                                newItem.UnitSymbol = item.UnitSymbol;

                                newItem.Val = item.Val;

                                newConsParams.Add(newItem);

                            }

                            foreach (MessureParam item in appInfo.MessureParams)
                            {
                                MessureParam newItem = new MessureParam();
                                newItem.MessureParamID = Guid.NewGuid();
                                newItem.AppName = newAppName;
                                newItem.Description = item.Description;
                                newItem.Order = item.Order;
                                newItem.ParamName = item.ParamName;
                                newItem.ParamSymbol = item.ParamSymbol;
                                newItem.PrecisionNum = item.PrecisionNum;
                                newItem.UnitSymbol = item.UnitSymbol;

                                newMesParams.Add(newItem);
                            }

                            foreach (CalculateParam item in appInfo.CalcParams)
                            {
                                CalculateParam newItem = new CalculateParam();
                                newItem.CalculateParamID = Guid.NewGuid();;
                                newItem.AppName = newAppName;
                                newItem.Description = item.Description;
                                newItem.Order = item.Order;
                                newItem.ParamName = item.ParamName;
                                newItem.ParamSymbol = item.ParamSymbol;
                                newItem.PrecisionNum = item.PrecisionNum;
                                newItem.UnitSymbol = item.UnitSymbol;
                                newItem.CalculateExpress = item.CalculateExpress;
                                newItem.CalculateOrder = item.CalculateOrder;
                                newCalcParams.Add(newItem);
                            }

                          

                            using (System.Data.IDbConnection connection = hammergo.ConnectionPool.Pool.GetOpenConnection())
                            {
                                //connection.Open();

                                // Start a local transaction.
                                System.Data.IDbTransaction trans = connection.BeginTransaction();


                                try
                                {
                                    appBLL.Add(newApp, trans);
                                    constBLL.UpdateList(newConsParams, trans);
                                    mesBLL.UpdateList(newMesParams, trans);
                                    calcBLL.UpdateList(newCalcParams, trans);

                                    trans.Commit();
                                    //Console.WriteLine("Both records are written to database.");
                                }
                                catch (Exception ex)
                                {


                                    // Attempt to roll back the transaction.

                                    trans.Rollback();
                                    throw ex;
                                }
                            }



                            bsApp.Add(newApp);


                            if (bsApp.DataSource != allAppList)
                            {
                                allAppList.Add(newApp);
                            }

                            gridView1.ClearSelection();

                            Utility.Utility.selectRow(newApp, gridView1);



                        }

                    }
                }

            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            }
        }


        hammergo.BLL.MessureParamBLL mesBLL = new hammergo.BLL.MessureParamBLL();
        hammergo.BLL.CalculateParamBLL calcBLL = new hammergo.BLL.CalculateParamBLL();
        hammergo.BLL.ConstantParamBLL constBLL = new hammergo.BLL.ConstantParamBLL();

        //int nextConstID = 0;
        //private int getNextConstID()
        //{
        //    if (nextConstID == 0)
        //    {
        //        int? maxID = constBLL.getMaxConstantParamID();
        //        nextConstID = maxID.HasValue ? maxID.Value + 1 : 0;
        //    }

        //    return nextConstID++;
        //}


        //int nextMesID = 0;

        //private int getNextMesID()
        //{
        //    if (nextMesID == 0)
        //    {

        //        int? maxID = mesBLL.getMaxMessureParamID();
        //        nextMesID = maxID.HasValue ? maxID.Value + 1 : 0;
        //    }
        //    return nextMesID++;


        //}

        //int nextCalcID = 0;
        //private int getNextCalcID()
        //{
        //    if (nextCalcID == 0)
        //    {
        //        int? maxID = calcBLL.getMaxCalculateParamID();
        //        nextCalcID = maxID.HasValue ? maxID.Value + 1 : 0;
        //    }

        //    return nextCalcID++;
        //}


        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (currentNode != null && currentNode.Nodes.Count == 0)
            {
                //添加仪器Item.Enabled = true;
                setItemEnable(true);
            }
            else
            {
                setItemEnable(false);
            }


        }

        private void setItemEnable(bool enable)
        {
            添加仪器Item.Enabled = enable;
            关联仪器ToolStripMenuItem.Enabled = enable;
            //设置仪器参数ToolStripMenuItem.Enabled = enable;
            // 删除仪器ToolStripMenuItem.Enabled = enable;
            //复制ToolStripMenuItem.Enabled = enable;
            //原始数据ToolStripMenuItem.Enabled = enable;
            //导出Item.Enabled = enable;
        }

        private void partControl1_SearchItemClick()
        {
            Guid? partID = null;

            string strInput = Utility.Utility.InputBox("查找测点", "输入测点编号", "", null);

            if (strInput != null)
            {
                Apparatus app = allAppList.Find(delegate(Apparatus item) { return item.AppName == strInput; });



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

                        gridView1.ClearSelection();

                        setAppsByPartInGrid(partID);

                        Utility.Utility.selectRow(app, gridView1);
                    }
                }




            }

        }

        private void 修改测点编号ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                int[] indexes = gridView1.GetSelectedRows();
                if (indexes.Length > 0)
                {
                    Apparatus app = gridView1.GetRow(indexes[0]) as Apparatus;

                    string newName = Utility.Utility.InputBox("输入", "请输入新的测点编号名称", null, null);

                    if (newName != null)
                    {
                        newName = newName.Trim().ToUpper();
                        if (newName.Length != 0 && newName.Equals(app.AppName, StringComparison.InvariantCultureIgnoreCase)==false)
                        {

                            if (allAppList.Exists(delegate(Apparatus item) { return item.AppName.Equals(newName, StringComparison.InvariantCultureIgnoreCase); }))
                            {
                                throw new Exception("存在重名的计算编号");
                            }

                            appBLL.UpdateBy_AppName(app.AppName, newName);
                            //成功更新数据库
                            app.AppName = newName;
                            app.TrackingState = Tracking.TrackingInfo.Unchanged;

                        }
                    }


                }
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            }
        }







    }
}
