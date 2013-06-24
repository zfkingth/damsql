namespace hammergo.AppManage
{
    partial class AllAppManage
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.partControl1 = new hammergo.CommonControl.PartControl();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip();
            this.复制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.原始数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关联仪器ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置仪器参数ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.修改测点编号ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.所有仪器ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加仪器Item = new System.Windows.Forms.ToolStripMenuItem();
            this.删除仪器ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.导出Item = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTxt = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiXml = new System.Windows.Forms.ToolStripMenuItem();
            this.bsApp = new System.Windows.Forms.BindingSource();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colZ = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colY = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colX = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAppTypeID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemLookUpEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.bsType = new System.Windows.Forms.BindingSource();
            this.colAppName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProjectPartID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCalculateName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBuriedTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOtherInfo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.dxErrorProvider1 = new DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider();
            ((System.ComponentModel.ISupportInitialize)(this.partControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsApp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // partControl1
            // 
            this.partControl1.AllowDrop = true;
            this.partControl1.BestFitVisibleOnly = true;
            this.partControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.partControl1.KeyFieldName = "ProjectPartID";
            this.partControl1.Location = new System.Drawing.Point(0, 0);
            this.partControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.partControl1.Name = "partControl1";
            this.partControl1.OptionsBehavior.DragNodes = true;
            this.partControl1.OptionsView.AutoWidth = false;
            this.partControl1.ParentFieldName = "ParentPart";
            this.partControl1.Size = new System.Drawing.Size(289, 1050);
            this.partControl1.TabIndex = 0;
            this.partControl1.SearchItemClick += new hammergo.CommonControl.SearchDelegate(this.partControl1_SearchItemClick);
            this.partControl1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.partControl1_MouseClick);
            // 
            // gridControl1
            // 
            this.gridControl1.ContextMenuStrip = this.contextMenuStrip1;
            this.gridControl1.DataSource = this.bsApp;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControl1.Location = new System.Drawing.Point(289, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemLookUpEdit1});
            this.gridControl1.Size = new System.Drawing.Size(885, 1050);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.复制ToolStripMenuItem,
            this.原始数据ToolStripMenuItem,
            this.关联仪器ToolStripMenuItem,
            this.设置仪器参数ToolStripMenuItem,
            this.修改测点编号ToolStripMenuItem,
            this.所有仪器ToolStripMenuItem,
            this.添加仪器Item,
            this.删除仪器ToolStripMenuItem,
            this.导出Item});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(169, 220);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // 复制ToolStripMenuItem
            // 
            this.复制ToolStripMenuItem.Name = "复制ToolStripMenuItem";
            this.复制ToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
            this.复制ToolStripMenuItem.Text = "复制";
            this.复制ToolStripMenuItem.Click += new System.EventHandler(this.复制ToolStripMenuItem_Click);
            // 
            // 原始数据ToolStripMenuItem
            // 
            this.原始数据ToolStripMenuItem.Name = "原始数据ToolStripMenuItem";
            this.原始数据ToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
            this.原始数据ToolStripMenuItem.Text = "原始数据";
            this.原始数据ToolStripMenuItem.Click += new System.EventHandler(this.仪器数据ToolStripMenuItem_Click);
            // 
            // 关联仪器ToolStripMenuItem
            // 
            this.关联仪器ToolStripMenuItem.Name = "关联仪器ToolStripMenuItem";
            this.关联仪器ToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
            this.关联仪器ToolStripMenuItem.Text = "设置关联仪器";
            this.关联仪器ToolStripMenuItem.ToolTipText = "将仪器指定到当前部位";
            this.关联仪器ToolStripMenuItem.Click += new System.EventHandler(this.关联仪器ToolStripMenuItem_Click);
            // 
            // 设置仪器参数ToolStripMenuItem
            // 
            this.设置仪器参数ToolStripMenuItem.Name = "设置仪器参数ToolStripMenuItem";
            this.设置仪器参数ToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
            this.设置仪器参数ToolStripMenuItem.Text = "设置仪器参数";
            this.设置仪器参数ToolStripMenuItem.Click += new System.EventHandler(this.设置仪器参数ToolStripMenuItem_Click);
            // 
            // 修改测点编号ToolStripMenuItem
            // 
            this.修改测点编号ToolStripMenuItem.Name = "修改测点编号ToolStripMenuItem";
            this.修改测点编号ToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
            this.修改测点编号ToolStripMenuItem.Text = "修改测点编号";
            this.修改测点编号ToolStripMenuItem.Click += new System.EventHandler(this.修改测点编号ToolStripMenuItem_Click);
            // 
            // 所有仪器ToolStripMenuItem
            // 
            this.所有仪器ToolStripMenuItem.Name = "所有仪器ToolStripMenuItem";
            this.所有仪器ToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
            this.所有仪器ToolStripMenuItem.Text = "显示所有仪器";
            this.所有仪器ToolStripMenuItem.ToolTipText = "显示所有仪器";
            this.所有仪器ToolStripMenuItem.Click += new System.EventHandler(this.所有仪器ToolStripMenuItem_Click);
            // 
            // 添加仪器Item
            // 
            this.添加仪器Item.Name = "添加仪器Item";
            this.添加仪器Item.Size = new System.Drawing.Size(168, 24);
            this.添加仪器Item.Text = "添加仪器";
            this.添加仪器Item.Click += new System.EventHandler(this.添加仪器Item_Click);
            // 
            // 删除仪器ToolStripMenuItem
            // 
            this.删除仪器ToolStripMenuItem.Name = "删除仪器ToolStripMenuItem";
            this.删除仪器ToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
            this.删除仪器ToolStripMenuItem.Text = "删除仪器";
            this.删除仪器ToolStripMenuItem.Click += new System.EventHandler(this.删除仪器ToolStripMenuItem_Click);
            // 
            // 导出Item
            // 
            this.导出Item.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiExcel,
            this.tsmiTxt,
            this.tsmiXml});
            this.导出Item.Name = "导出Item";
            this.导出Item.Size = new System.Drawing.Size(168, 24);
            this.导出Item.Text = "导出";
            // 
            // tsmiExcel
            // 
            this.tsmiExcel.Name = "tsmiExcel";
            this.tsmiExcel.Size = new System.Drawing.Size(152, 24);
            this.tsmiExcel.Text = "Excel";
            this.tsmiExcel.Click += new System.EventHandler(this.tsmiExcel_Click);
            // 
            // tsmiTxt
            // 
            this.tsmiTxt.Name = "tsmiTxt";
            this.tsmiTxt.Size = new System.Drawing.Size(152, 24);
            this.tsmiTxt.Text = "Txt";
            this.tsmiTxt.Click += new System.EventHandler(this.tsmiTxt_Click);
            // 
            // tsmiXml
            // 
            this.tsmiXml.Name = "tsmiXml";
            this.tsmiXml.Size = new System.Drawing.Size(152, 24);
            this.tsmiXml.Text = "Xml";
            this.tsmiXml.Click += new System.EventHandler(this.tsmiXml_Click);
            // 
            // bsApp
            // 
            this.bsApp.DataSource = typeof(hammergo.Model.Apparatus);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colZ,
            this.colY,
            this.colX,
            this.colAppTypeID,
            this.colAppName,
            this.colProjectPartID,
            this.colCalculateName,
            this.colBuriedTime,
            this.colOtherInfo});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gridView1.OptionsView.ShowDetailButtons = false;
            this.gridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.ShowAlways;
            this.gridView1.OptionsView.ShowFooter = true;
            this.gridView1.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowOnlyInEditor;
            this.gridView1.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colAppName, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridView1.RowUpdated += new DevExpress.XtraGrid.Views.Base.RowObjectEventHandler(this.gridView1_RowUpdated);
            this.gridView1.ValidatingEditor += new DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventHandler(this.gridView1_ValidatingEditor);
            // 
            // colZ
            // 
            this.colZ.Caption = "Z";
            this.colZ.FieldName = "Z";
            this.colZ.Name = "colZ";
            this.colZ.Visible = true;
            this.colZ.VisibleIndex = 5;
            this.colZ.Width = 94;
            // 
            // colY
            // 
            this.colY.Caption = "Y";
            this.colY.FieldName = "Y";
            this.colY.Name = "colY";
            this.colY.Visible = true;
            this.colY.VisibleIndex = 4;
            this.colY.Width = 91;
            // 
            // colX
            // 
            this.colX.Caption = "X";
            this.colX.FieldName = "X";
            this.colX.Name = "colX";
            this.colX.Visible = true;
            this.colX.VisibleIndex = 3;
            this.colX.Width = 86;
            // 
            // colAppTypeID
            // 
            this.colAppTypeID.Caption = "仪器类型";
            this.colAppTypeID.ColumnEdit = this.repositoryItemLookUpEdit1;
            this.colAppTypeID.FieldName = "AppTypeID";
            this.colAppTypeID.Name = "colAppTypeID";
            this.colAppTypeID.Visible = true;
            this.colAppTypeID.VisibleIndex = 2;
            this.colAppTypeID.Width = 186;
            // 
            // repositoryItemLookUpEdit1
            // 
            this.repositoryItemLookUpEdit1.AutoHeight = false;
            this.repositoryItemLookUpEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemLookUpEdit1.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("ApparatusTypeID", "ApparatusTypeID", 126, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Near),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TypeName", "类型名称", 72, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near)});
            this.repositoryItemLookUpEdit1.DataSource = this.bsType;
            this.repositoryItemLookUpEdit1.DisplayMember = "TypeName";
            this.repositoryItemLookUpEdit1.Name = "repositoryItemLookUpEdit1";
            this.repositoryItemLookUpEdit1.NullText = "[未设置仪器类型]";
            this.repositoryItemLookUpEdit1.PopupFormMinSize = new System.Drawing.Size(80, 0);
            this.repositoryItemLookUpEdit1.PopupWidth = 87;
            this.repositoryItemLookUpEdit1.ValueMember = "ApparatusTypeID";
            // 
            // bsType
            // 
            this.bsType.DataSource = typeof(hammergo.Model.ApparatusType);
            // 
            // colAppName
            // 
            this.colAppName.Caption = "测点编号";
            this.colAppName.FieldName = "AppName";
            this.colAppName.Name = "colAppName";
            this.colAppName.OptionsColumn.AllowEdit = false;
            this.colAppName.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "AppName", "共 {0} 支仪器")});
            this.colAppName.Visible = true;
            this.colAppName.VisibleIndex = 0;
            this.colAppName.Width = 186;
            // 
            // colProjectPartID
            // 
            this.colProjectPartID.Caption = "部位编号";
            this.colProjectPartID.FieldName = "ProjectPartID";
            this.colProjectPartID.Name = "colProjectPartID";
            this.colProjectPartID.Width = 100;
            // 
            // colCalculateName
            // 
            this.colCalculateName.Caption = "计算编号";
            this.colCalculateName.FieldName = "CalculateName";
            this.colCalculateName.Name = "colCalculateName";
            this.colCalculateName.Visible = true;
            this.colCalculateName.VisibleIndex = 1;
            this.colCalculateName.Width = 186;
            // 
            // colBuriedTime
            // 
            this.colBuriedTime.Caption = "埋设时间";
            this.colBuriedTime.FieldName = "BuriedTime";
            this.colBuriedTime.Name = "colBuriedTime";
            this.colBuriedTime.Visible = true;
            this.colBuriedTime.VisibleIndex = 6;
            this.colBuriedTime.Width = 143;
            // 
            // colOtherInfo
            // 
            this.colOtherInfo.Caption = "其它信息";
            this.colOtherInfo.FieldName = "OtherInfo";
            this.colOtherInfo.Name = "colOtherInfo";
            this.colOtherInfo.Visible = true;
            this.colOtherInfo.VisibleIndex = 7;
            this.colOtherInfo.Width = 516;
            // 
            // dxErrorProvider1
            // 
            this.dxErrorProvider1.ContainerControl = this;
            // 
            // AllAppManage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.partControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "AllAppManage";
            this.Size = new System.Drawing.Size(1174, 1050);
            this.Load += new System.EventHandler(this.AllAppManage_Load);
            ((System.ComponentModel.ISupportInitialize)(this.partControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bsApp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemLookUpEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dxErrorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private hammergo.CommonControl.PartControl partControl1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.BindingSource bsApp;
        private DevExpress.XtraGrid.Columns.GridColumn colZ;
        private DevExpress.XtraGrid.Columns.GridColumn colY;
        private DevExpress.XtraGrid.Columns.GridColumn colX;
        private DevExpress.XtraGrid.Columns.GridColumn colAppTypeID;
        private DevExpress.XtraGrid.Columns.GridColumn colAppName;
        private DevExpress.XtraGrid.Columns.GridColumn colProjectPartID;
        private DevExpress.XtraGrid.Columns.GridColumn colCalculateName;
        private DevExpress.XtraGrid.Columns.GridColumn colBuriedTime;
        private DevExpress.XtraGrid.Columns.GridColumn colOtherInfo;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 复制ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 原始数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关联仪器ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置仪器参数ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 所有仪器ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除仪器ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 导出Item;
        private System.Windows.Forms.ToolStripMenuItem tsmiExcel;
        private System.Windows.Forms.ToolStripMenuItem tsmiTxt;
        private System.Windows.Forms.ToolStripMenuItem tsmiXml;
        private System.Windows.Forms.BindingSource bsType;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repositoryItemLookUpEdit1;
        private DevExpress.XtraEditors.DXErrorProvider.DXErrorProvider dxErrorProvider1;
        private System.Windows.Forms.ToolStripMenuItem 添加仪器Item;
        private System.Windows.Forms.ToolStripMenuItem 修改测点编号ToolStripMenuItem;
    }
}
