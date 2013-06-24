namespace hammergo.DataManage
{
    partial class AppData
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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip();
            this.复制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置仪器参数ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.所有仪器ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.修改数据item = new System.Windows.Forms.ToolStripMenuItem();
            this.添加数据Item = new System.Windows.Forms.ToolStripMenuItem();
            this.删除仪器ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi导出 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTxt = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiXml = new System.Windows.Forms.ToolStripMenuItem();
            this.xmlDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.txtName = new DevExpress.XtraEditors.ComboBoxEdit();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.复制ToolStripMenuItem,
            this.设置仪器参数ToolStripMenuItem,
            this.所有仪器ToolStripMenuItem,
            this.修改数据item,
            this.添加数据Item,
            this.删除仪器ToolStripMenuItem,
            this.tsmi导出});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(169, 194);
            // 
            // 复制ToolStripMenuItem
            // 
            this.复制ToolStripMenuItem.Name = "复制ToolStripMenuItem";
            this.复制ToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
            this.复制ToolStripMenuItem.Text = "复制";
            this.复制ToolStripMenuItem.Click += new System.EventHandler(this.复制ToolStripMenuItem_Click);
            // 
            // 设置仪器参数ToolStripMenuItem
            // 
            this.设置仪器参数ToolStripMenuItem.Name = "设置仪器参数ToolStripMenuItem";
            this.设置仪器参数ToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
            this.设置仪器参数ToolStripMenuItem.Text = "设置仪器参数";
            this.设置仪器参数ToolStripMenuItem.Click += new System.EventHandler(this.设置仪器参数ToolStripMenuItem_Click);
            // 
            // 所有仪器ToolStripMenuItem
            // 
            this.所有仪器ToolStripMenuItem.Name = "所有仪器ToolStripMenuItem";
            this.所有仪器ToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
            this.所有仪器ToolStripMenuItem.Text = "显示所有数据";
            this.所有仪器ToolStripMenuItem.ToolTipText = "显示所有仪器";
            this.所有仪器ToolStripMenuItem.Click += new System.EventHandler(this.所有仪器ToolStripMenuItem_Click);
            // 
            // 修改数据item
            // 
            this.修改数据item.Name = "修改数据item";
            this.修改数据item.Size = new System.Drawing.Size(168, 24);
            this.修改数据item.Text = "修改数据";
            this.修改数据item.Click += new System.EventHandler(this.修改数据item_Click);
            // 
            // 添加数据Item
            // 
            this.添加数据Item.Name = "添加数据Item";
            this.添加数据Item.Size = new System.Drawing.Size(168, 24);
            this.添加数据Item.Text = "添加数据";
            this.添加数据Item.Click += new System.EventHandler(this.添加数据Item_Click);
            // 
            // 删除仪器ToolStripMenuItem
            // 
            this.删除仪器ToolStripMenuItem.Name = "删除仪器ToolStripMenuItem";
            this.删除仪器ToolStripMenuItem.Size = new System.Drawing.Size(168, 24);
            this.删除仪器ToolStripMenuItem.Text = "删除数据";
            this.删除仪器ToolStripMenuItem.Click += new System.EventHandler(this.删除数据ToolStripMenuItem_Click);
            // 
            // tsmi导出
            // 
            this.tsmi导出.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiExcel,
            this.tsmiTxt,
            this.tsmiXml,
            this.xmlDataToolStripMenuItem});
            this.tsmi导出.Name = "tsmi导出";
            this.tsmi导出.Size = new System.Drawing.Size(168, 24);
            this.tsmi导出.Text = "导出";
            // 
            // tsmiExcel
            // 
            this.tsmiExcel.Name = "tsmiExcel";
            this.tsmiExcel.Size = new System.Drawing.Size(174, 24);
            this.tsmiExcel.Text = "Excel";
            this.tsmiExcel.Click += new System.EventHandler(this.tsmiExcel_Click);
            // 
            // tsmiTxt
            // 
            this.tsmiTxt.Name = "tsmiTxt";
            this.tsmiTxt.Size = new System.Drawing.Size(174, 24);
            this.tsmiTxt.Text = "Txt";
            this.tsmiTxt.Click += new System.EventHandler(this.tsmiTxt_Click);
            // 
            // tsmiXml
            // 
            this.tsmiXml.Name = "tsmiXml";
            this.tsmiXml.Size = new System.Drawing.Size(174, 24);
            this.tsmiXml.Text = "Xml";
            this.tsmiXml.Click += new System.EventHandler(this.tsmiXml_Click);
            // 
            // xmlDataToolStripMenuItem
            // 
            this.xmlDataToolStripMenuItem.Name = "xmlDataToolStripMenuItem";
            this.xmlDataToolStripMenuItem.Size = new System.Drawing.Size(174, 24);
            this.xmlDataToolStripMenuItem.Text = "Xml and Data";
            this.xmlDataToolStripMenuItem.Click += new System.EventHandler(this.xmlDataToolStripMenuItem_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.txtName);
            this.panelControl1.Controls.Add(this.simpleButton1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(1170, 152);
            this.panelControl1.TabIndex = 5;
            // 
            // txtName
            // 
            this.txtName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtName.EditValue = "输入测点编号";
            this.txtName.Location = new System.Drawing.Point(449, 48);
            this.txtName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtName.Name = "txtName";
            this.txtName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.txtName.Size = new System.Drawing.Size(135, 25);
            this.txtName.TabIndex = 3;
            this.txtName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtName_KeyDown);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.simpleButton1.Location = new System.Drawing.Point(635, 48);
            this.simpleButton1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(86, 30);
            this.simpleButton1.TabIndex = 1;
            this.simpleButton1.Text = "查找";
            this.simpleButton1.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.ContextMenuStrip = this.contextMenuStrip1;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl1.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControl1.Location = new System.Drawing.Point(0, 152);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(1170, 835);
            this.gridControl1.TabIndex = 6;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Appearance.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 10F);
            this.gridView1.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridView1.Appearance.Row.Font = new System.Drawing.Font("Times New Roman", 12F);
            this.gridView1.Appearance.Row.Options.UseFont = true;
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridView1_CellValueChanged);
            // 
            // AppData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.panelControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "AppData";
            this.Size = new System.Drawing.Size(1170, 987);
            this.Load += new System.EventHandler(this.AppData_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 复制ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置仪器参数ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 所有仪器ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除仪器ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmi导出;
        private System.Windows.Forms.ToolStripMenuItem tsmiExcel;
        private System.Windows.Forms.ToolStripMenuItem tsmiTxt;
        private System.Windows.Forms.ToolStripMenuItem tsmiXml;
        private System.Windows.Forms.ToolStripMenuItem xmlDataToolStripMenuItem;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        public DevExpress.XtraEditors.ComboBoxEdit txtName;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private System.Windows.Forms.ToolStripMenuItem 修改数据item;
        private System.Windows.Forms.ToolStripMenuItem 添加数据Item;
        public DevExpress.XtraGrid.GridControl gridControl1;
    }
}
