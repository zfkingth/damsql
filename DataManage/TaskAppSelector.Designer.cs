namespace hammergo.DataManage
{
    partial class TaskAppSelector
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
            this.components = new System.ComponentModel.Container();
            this.taskGrid = new DevExpress.XtraGrid.GridControl();
            this.appCollectionBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.gridTasks = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTrackingState = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAppCollectionID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOrder = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCollectionName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTaskTypeID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lbcApps = new DevExpress.XtraEditors.ListBoxControl();
            this.taskAppratusBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panelButtons = new DevExpress.XtraEditors.PanelControl();
            this.simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.lbcSelectedApps = new DevExpress.XtraEditors.ListBoxControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItemPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.原始数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.taskGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.appCollectionBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTasks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbcApps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.taskAppratusBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelButtons)).BeginInit();
            this.panelButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lbcSelectedApps)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // taskGrid
            // 
            this.taskGrid.DataSource = this.appCollectionBindingSource;
            this.taskGrid.Dock = System.Windows.Forms.DockStyle.Left;
            this.taskGrid.EmbeddedNavigator.Name = "";
            this.taskGrid.Location = new System.Drawing.Point(0, 0);
            this.taskGrid.MainView = this.gridTasks;
            this.taskGrid.Name = "taskGrid";
            this.taskGrid.Size = new System.Drawing.Size(209, 417);
            this.taskGrid.TabIndex = 1;
            this.taskGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridTasks});
            // 
            // appCollectionBindingSource
            // 
            this.appCollectionBindingSource.DataSource = typeof(hammergo.Model.AppCollection);
            this.appCollectionBindingSource.CurrentChanged += new System.EventHandler(this.appCollectionBindingSource_CurrentChanged);
            // 
            // gridTasks
            // 
            this.gridTasks.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTrackingState,
            this.colAppCollectionID,
            this.colOrder,
            this.colCollectionName,
            this.colDescription,
            this.colTaskTypeID});
            this.gridTasks.GridControl = this.taskGrid;
            this.gridTasks.Name = "gridTasks";
            this.gridTasks.OptionsBehavior.Editable = false;
            this.gridTasks.OptionsView.ShowGroupPanel = false;
            this.gridTasks.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colCollectionName, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // colTrackingState
            // 
            this.colTrackingState.Caption = "TrackingState";
            this.colTrackingState.FieldName = "TrackingState";
            this.colTrackingState.Name = "colTrackingState";
            // 
            // colAppCollectionID
            // 
            this.colAppCollectionID.Caption = "AppCollectionID";
            this.colAppCollectionID.FieldName = "AppCollectionID";
            this.colAppCollectionID.Name = "colAppCollectionID";
            // 
            // colOrder
            // 
            this.colOrder.Caption = "Order";
            this.colOrder.FieldName = "Order";
            this.colOrder.Name = "colOrder";
            // 
            // colCollectionName
            // 
            this.colCollectionName.Caption = "任务名称";
            this.colCollectionName.FieldName = "CollectionName";
            this.colCollectionName.Name = "colCollectionName";
            this.colCollectionName.Visible = true;
            this.colCollectionName.VisibleIndex = 0;
            // 
            // colDescription
            // 
            this.colDescription.Caption = "Description";
            this.colDescription.FieldName = "Description";
            this.colDescription.Name = "colDescription";
            // 
            // colTaskTypeID
            // 
            this.colTaskTypeID.Caption = "TaskTypeID";
            this.colTaskTypeID.FieldName = "TaskTypeID";
            this.colTaskTypeID.Name = "colTaskTypeID";
            // 
            // lbcApps
            // 
            this.lbcApps.DataSource = this.taskAppratusBindingSource;
            this.lbcApps.DisplayMember = "AppName";
            this.lbcApps.Dock = System.Windows.Forms.DockStyle.Left;
            this.lbcApps.Location = new System.Drawing.Point(209, 0);
            this.lbcApps.Name = "lbcApps";
            this.lbcApps.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbcApps.Size = new System.Drawing.Size(161, 417);
            this.lbcApps.TabIndex = 5;
            this.lbcApps.ValueMember = "AppName";
            this.lbcApps.DoubleClick += new System.EventHandler(this.lbcApps_DoubleClick);
            // 
            // taskAppratusBindingSource
            // 
            this.taskAppratusBindingSource.DataSource = typeof(hammergo.Model.TaskAppratus);
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.simpleButton4);
            this.panelButtons.Controls.Add(this.simpleButton3);
            this.panelButtons.Controls.Add(this.simpleButton2);
            this.panelButtons.Controls.Add(this.simpleButton1);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelButtons.Location = new System.Drawing.Point(370, 0);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(82, 417);
            this.panelButtons.TabIndex = 6;
            // 
            // simpleButton4
            // 
            this.simpleButton4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.simpleButton4.Location = new System.Drawing.Point(7, 262);
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Size = new System.Drawing.Size(68, 21);
            this.simpleButton4.TabIndex = 7;
            this.simpleButton4.Text = "全选";
            this.simpleButton4.Click += new System.EventHandler(this.simpleButton4_Click);
            // 
            // simpleButton3
            // 
            this.simpleButton3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.simpleButton3.Location = new System.Drawing.Point(7, 219);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(68, 21);
            this.simpleButton3.TabIndex = 6;
            this.simpleButton3.Text = "清空";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.simpleButton2.Location = new System.Drawing.Point(7, 176);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(68, 21);
            this.simpleButton2.TabIndex = 5;
            this.simpleButton2.Text = "<=";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.simpleButton1.Location = new System.Drawing.Point(7, 133);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(68, 21);
            this.simpleButton1.TabIndex = 4;
            this.simpleButton1.Text = "=>";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // lbcSelectedApps
            // 
            this.lbcSelectedApps.ContextMenuStrip = this.contextMenuStrip1;
            this.lbcSelectedApps.DisplayMember = "AppName";
            this.lbcSelectedApps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbcSelectedApps.Location = new System.Drawing.Point(452, 0);
            this.lbcSelectedApps.Name = "lbcSelectedApps";
            this.lbcSelectedApps.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbcSelectedApps.Size = new System.Drawing.Size(163, 417);
            this.lbcSelectedApps.TabIndex = 7;
            this.lbcSelectedApps.DoubleClick += new System.EventHandler(this.lbcSelectedApps_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemPaste,
            this.menuItemCopy,
            this.原始数据ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(135, 70);
            // 
            // menuItemPaste
            // 
            this.menuItemPaste.Name = "menuItemPaste";
            this.menuItemPaste.Size = new System.Drawing.Size(134, 22);
            this.menuItemPaste.Text = "粘贴";
            this.menuItemPaste.Click += new System.EventHandler(this.menuItemPaste_Click);
            // 
            // menuItemCopy
            // 
            this.menuItemCopy.Name = "menuItemCopy";
            this.menuItemCopy.Size = new System.Drawing.Size(134, 22);
            this.menuItemCopy.Text = "复制";
            this.menuItemCopy.Click += new System.EventHandler(this.menuItemCopy_Click);
            // 
            // 原始数据ToolStripMenuItem
            // 
            this.原始数据ToolStripMenuItem.Name = "原始数据ToolStripMenuItem";
            this.原始数据ToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.原始数据ToolStripMenuItem.Text = "原始数据";
            this.原始数据ToolStripMenuItem.Click += new System.EventHandler(this.原始数据ToolStripMenuItem_Click);
            // 
            // TaskAppSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbcSelectedApps);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.lbcApps);
            this.Controls.Add(this.taskGrid);
            this.Name = "TaskAppSelector";
            this.Size = new System.Drawing.Size(615, 417);
            this.Load += new System.EventHandler(this.TaskAppSelector_Load);
            ((System.ComponentModel.ISupportInitialize)(this.taskGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.appCollectionBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTasks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbcApps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.taskAppratusBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelButtons)).EndInit();
            this.panelButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lbcSelectedApps)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl taskGrid;
        private DevExpress.XtraGrid.Views.Grid.GridView gridTasks;
        private DevExpress.XtraGrid.Columns.GridColumn colTrackingState;
        private DevExpress.XtraGrid.Columns.GridColumn colAppCollectionID;
        private DevExpress.XtraGrid.Columns.GridColumn colOrder;
        private DevExpress.XtraGrid.Columns.GridColumn colCollectionName;
        private DevExpress.XtraGrid.Columns.GridColumn colDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colTaskTypeID;
        private DevExpress.XtraEditors.ListBoxControl lbcApps;
        private DevExpress.XtraEditors.PanelControl panelButtons;
        private DevExpress.XtraEditors.SimpleButton simpleButton4;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        public DevExpress.XtraEditors.ListBoxControl lbcSelectedApps;
        private System.Windows.Forms.BindingSource appCollectionBindingSource;
        private System.Windows.Forms.BindingSource taskAppratusBindingSource;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuItemPaste;
        private System.Windows.Forms.ToolStripMenuItem menuItemCopy;
        private System.Windows.Forms.ToolStripMenuItem 原始数据ToolStripMenuItem;
    }
}
