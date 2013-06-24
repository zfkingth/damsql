namespace hammergo.DataSearch
{
    partial class SearchSameDate
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
            this.dateCMS1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pasteDateMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.dateCMS2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.timesliceEdit = new DevExpress.XtraEditors.CalcEdit();
            this.label2 = new System.Windows.Forms.Label();
            this.c1DateEdit2 = new DevExpress.XtraEditors.DateEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.c1DateEdit1 = new DevExpress.XtraEditors.DateEdit();
            this.label11 = new System.Windows.Forms.Label();
            this.appSelector1 = new hammergo.CommonControl.AppSelector();
            this.gridControlResult = new DevExpress.XtraGrid.GridControl();
            this.gridCMS = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.复制Item = new System.Windows.Forms.ToolStripMenuItem();
            this.导出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.dateCMS1.SuspendLayout();
            this.dateCMS2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.timesliceEdit.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DateEdit2.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DateEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DateEdit1.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DateEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlResult)).BeginInit();
            this.gridCMS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dateCMS1
            // 
            this.dateCMS1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pasteDateMenuItem1});
            this.dateCMS1.Name = "dateCMS";
            this.dateCMS1.Size = new System.Drawing.Size(109, 28);
            // 
            // pasteDateMenuItem1
            // 
            this.pasteDateMenuItem1.Name = "pasteDateMenuItem1";
            this.pasteDateMenuItem1.Size = new System.Drawing.Size(108, 24);
            this.pasteDateMenuItem1.Text = "粘贴";
            this.pasteDateMenuItem1.Click += new System.EventHandler(this.pasteDateMenuItem1_Click);
            // 
            // dateCMS2
            // 
            this.dateCMS2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.dateCMS2.Name = "dateCMS";
            this.dateCMS2.Size = new System.Drawing.Size(109, 28);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(108, 24);
            this.toolStripMenuItem1.Text = "粘贴";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(251, 768);
            this.xtraTabControl1.TabIndex = 3;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.label4);
            this.xtraTabPage1.Controls.Add(this.timesliceEdit);
            this.xtraTabPage1.Controls.Add(this.label2);
            this.xtraTabPage1.Controls.Add(this.c1DateEdit2);
            this.xtraTabPage1.Controls.Add(this.label1);
            this.xtraTabPage1.Controls.Add(this.c1DateEdit1);
            this.xtraTabPage1.Controls.Add(this.label11);
            this.xtraTabPage1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(245, 737);
            this.xtraTabPage1.Text = "检索选项";
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(120, 281);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(71, 24);
            this.label4.TabIndex = 60;
            this.label4.Text = "天";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timesliceEdit
            // 
            this.timesliceEdit.EditValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.timesliceEdit.Location = new System.Drawing.Point(31, 281);
            this.timesliceEdit.Name = "timesliceEdit";
            this.timesliceEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.timesliceEdit.Properties.Mask.EditMask = "n";
            this.timesliceEdit.Size = new System.Drawing.Size(83, 25);
            this.timesliceEdit.TabIndex = 59;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(14, 243);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(207, 24);
            this.label2.TabIndex = 55;
            this.label2.Text = "时间粒度(最小的时间间隔)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // c1DateEdit2
            // 
            this.c1DateEdit2.EditValue = null;
            this.c1DateEdit2.Location = new System.Drawing.Point(17, 194);
            this.c1DateEdit2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.c1DateEdit2.Name = "c1DateEdit2";
            this.c1DateEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.c1DateEdit2.Properties.ContextMenuStrip = this.dateCMS2;
            this.c1DateEdit2.Properties.DisplayFormat.FormatString = "g";
            this.c1DateEdit2.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.c1DateEdit2.Properties.EditFormat.FormatString = "g";
            this.c1DateEdit2.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.c1DateEdit2.Properties.Mask.EditMask = "g";
            this.c1DateEdit2.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.c1DateEdit2.Size = new System.Drawing.Size(174, 25);
            this.c1DateEdit2.TabIndex = 54;
            this.c1DateEdit2.ParseEditValue += new DevExpress.XtraEditors.Controls.ConvertEditValueEventHandler(this.c1DateEdit1_ParseEditValue);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(14, 150);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(86, 24);
            this.label1.TabIndex = 53;
            this.label1.Text = "结束日期:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // c1DateEdit1
            // 
            this.c1DateEdit1.EditValue = null;
            this.c1DateEdit1.Location = new System.Drawing.Point(17, 102);
            this.c1DateEdit1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.c1DateEdit1.Name = "c1DateEdit1";
            this.c1DateEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.c1DateEdit1.Properties.ContextMenuStrip = this.dateCMS1;
            this.c1DateEdit1.Properties.DisplayFormat.FormatString = "g";
            this.c1DateEdit1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.c1DateEdit1.Properties.EditFormat.FormatString = "g";
            this.c1DateEdit1.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.c1DateEdit1.Properties.Mask.EditMask = "g";
            this.c1DateEdit1.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.c1DateEdit1.Size = new System.Drawing.Size(174, 25);
            this.c1DateEdit1.TabIndex = 31;
            this.c1DateEdit1.ParseEditValue += new DevExpress.XtraEditors.Controls.ConvertEditValueEventHandler(this.c1DateEdit1_ParseEditValue);
            // 
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(14, 58);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(86, 24);
            this.label11.TabIndex = 30;
            this.label11.Text = "起始日期:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // appSelector1
            // 
            this.appSelector1.Dock = System.Windows.Forms.DockStyle.Top;
            this.appSelector1.ExeButtonEnable = true;
            this.appSelector1.ExeButtonText = "数据检索";
            this.appSelector1.Location = new System.Drawing.Point(251, 0);
            this.appSelector1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.appSelector1.Name = "appSelector1";
            this.appSelector1.ShowAllAppMenuVisible = false;
            this.appSelector1.Size = new System.Drawing.Size(797, 414);
            this.appSelector1.TabIndex = 4;
            this.appSelector1.ShowDataEvent += new System.EventHandler<hammergo.Utility.AppSearchEventArgs>(this.appSelector1_ShowDataEvent);
            this.appSelector1.SearchExeClick += new System.EventHandler<hammergo.CommonControl.AppsEventArgs>(this.appSelector1_SearchExeClick);
            // 
            // gridControlResult
            // 
            this.gridControlResult.ContextMenuStrip = this.gridCMS;
            this.gridControlResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlResult.Location = new System.Drawing.Point(251, 414);
            this.gridControlResult.MainView = this.gridView1;
            this.gridControlResult.Name = "gridControlResult";
            this.gridControlResult.Size = new System.Drawing.Size(797, 354);
            this.gridControlResult.TabIndex = 5;
            this.gridControlResult.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridCMS
            // 
            this.gridCMS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.复制Item,
            this.导出ToolStripMenuItem});
            this.gridCMS.Name = "gridCMS";
            this.gridCMS.Size = new System.Drawing.Size(109, 52);
            // 
            // 复制Item
            // 
            this.复制Item.Name = "复制Item";
            this.复制Item.Size = new System.Drawing.Size(108, 24);
            this.复制Item.Text = "复制";
            this.复制Item.Click += new System.EventHandler(this.复制Item_Click);
            // 
            // 导出ToolStripMenuItem
            // 
            this.导出ToolStripMenuItem.Name = "导出ToolStripMenuItem";
            this.导出ToolStripMenuItem.Size = new System.Drawing.Size(108, 24);
            this.导出ToolStripMenuItem.Text = "导出";
            this.导出ToolStripMenuItem.Click += new System.EventHandler(this.导出ToolStripMenuItem_Click);
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControlResult;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.CopyToClipboardWithColumnHeaders = false;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gridView1.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.ShowAlways;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // SearchSameDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControlResult);
            this.Controls.Add(this.appSelector1);
            this.Controls.Add(this.xtraTabControl1);
            this.Name = "SearchSameDate";
            this.Size = new System.Drawing.Size(1048, 768);
            this.Load += new System.EventHandler(this.SearchSameDate_Load);
            this.dateCMS1.ResumeLayout(false);
            this.dateCMS2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.timesliceEdit.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DateEdit2.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DateEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DateEdit1.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DateEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlResult)).EndInit();
            this.gridCMS.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        

        #endregion

        private System.Windows.Forms.ContextMenuStrip dateCMS1;
        private System.Windows.Forms.ToolStripMenuItem pasteDateMenuItem1;
        private System.Windows.Forms.ContextMenuStrip dateCMS2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.DateEdit c1DateEdit2;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.DateEdit c1DateEdit1;
        private System.Windows.Forms.Label label11;
        private CommonControl.AppSelector appSelector1;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraEditors.CalcEdit timesliceEdit;
        private DevExpress.XtraGrid.GridControl gridControlResult;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.ContextMenuStrip gridCMS;
        private System.Windows.Forms.ToolStripMenuItem 复制Item;
        private System.Windows.Forms.ToolStripMenuItem 导出ToolStripMenuItem;
    }
}
