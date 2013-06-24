namespace hammergo.CommonControl
{
    partial class AppSelector
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
            this.lbcApps = new DevExpress.XtraEditors.ListBoxControl();
            this.apparatusBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panelButtons = new DevExpress.XtraEditors.PanelControl();
            this.ExeButton = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton6 = new DevExpress.XtraEditors.SimpleButton();
            this.snBox = new DevExpress.XtraEditors.TextEdit();
            this.simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.lbcSelectedApps = new DevExpress.XtraEditors.ListBoxControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItemPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.原始数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.所有测点ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.partControl1 = new hammergo.CommonControl.PartControl();
            this.panelSelectedApps = new DevExpress.XtraEditors.PanelControl();
            this.panelApps = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.lbcApps)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.apparatusBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelButtons)).BeginInit();
            this.panelButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.snBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbcSelectedApps)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.partControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelSelectedApps)).BeginInit();
            this.panelSelectedApps.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelApps)).BeginInit();
            this.panelApps.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbcApps
            // 
            this.lbcApps.DataSource = this.apparatusBindingSource;
            this.lbcApps.DisplayMember = "AppName";
            this.lbcApps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbcApps.Location = new System.Drawing.Point(2, 2);
            this.lbcApps.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lbcApps.Name = "lbcApps";
            this.lbcApps.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbcApps.Size = new System.Drawing.Size(185, 442);
            this.lbcApps.TabIndex = 4;
            this.lbcApps.ValueMember = "AppName";
            this.lbcApps.DoubleClick += new System.EventHandler(this.lbcApps_DoubleClick);
            // 
            // apparatusBindingSource
            // 
            this.apparatusBindingSource.DataSource = typeof(hammergo.Model.Apparatus);
            // 
            // panelButtons
            // 
            this.panelButtons.Controls.Add(this.ExeButton);
            this.panelButtons.Controls.Add(this.simpleButton6);
            this.panelButtons.Controls.Add(this.snBox);
            this.panelButtons.Controls.Add(this.simpleButton4);
            this.panelButtons.Controls.Add(this.simpleButton3);
            this.panelButtons.Controls.Add(this.simpleButton2);
            this.panelButtons.Controls.Add(this.simpleButton1);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelButtons.Location = new System.Drawing.Point(631, 0);
            this.panelButtons.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(94, 446);
            this.panelButtons.TabIndex = 3;
            // 
            // ExeButton
            // 
            this.ExeButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.ExeButton.Location = new System.Drawing.Point(8, 264);
            this.ExeButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.ExeButton.Name = "ExeButton";
            this.ExeButton.Size = new System.Drawing.Size(78, 27);
            this.ExeButton.TabIndex = 10;
            this.ExeButton.Text = "数据检索";
            this.ExeButton.Click += new System.EventHandler(this.simpleButton5_Click);
            // 
            // simpleButton6
            // 
            this.simpleButton6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.simpleButton6.Location = new System.Drawing.Point(8, 377);
            this.simpleButton6.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.simpleButton6.Name = "simpleButton6";
            this.simpleButton6.Size = new System.Drawing.Size(78, 27);
            this.simpleButton6.TabIndex = 9;
            this.simpleButton6.Text = "添加";
            this.simpleButton6.Click += new System.EventHandler(this.simpleButton6_Click);
            // 
            // snBox
            // 
            this.snBox.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.snBox.Location = new System.Drawing.Point(8, 319);
            this.snBox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.snBox.Name = "snBox";
            this.snBox.Size = new System.Drawing.Size(78, 25);
            this.snBox.TabIndex = 8;
            // 
            // simpleButton4
            // 
            this.simpleButton4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.simpleButton4.Location = new System.Drawing.Point(8, 208);
            this.simpleButton4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Size = new System.Drawing.Size(78, 27);
            this.simpleButton4.TabIndex = 7;
            this.simpleButton4.Text = "全选";
            this.simpleButton4.Click += new System.EventHandler(this.simpleButton4_Click);
            // 
            // simpleButton3
            // 
            this.simpleButton3.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.simpleButton3.Location = new System.Drawing.Point(8, 153);
            this.simpleButton3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(78, 27);
            this.simpleButton3.TabIndex = 6;
            this.simpleButton3.Text = "清空";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.simpleButton2.Location = new System.Drawing.Point(8, 98);
            this.simpleButton2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(78, 27);
            this.simpleButton2.TabIndex = 5;
            this.simpleButton2.Text = "<=";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.simpleButton1.Location = new System.Drawing.Point(8, 42);
            this.simpleButton1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(78, 27);
            this.simpleButton1.TabIndex = 4;
            this.simpleButton1.Text = "=>";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // lbcSelectedApps
            // 
            this.lbcSelectedApps.ContextMenuStrip = this.contextMenuStrip1;
            this.lbcSelectedApps.DisplayMember = "AppName";
            this.lbcSelectedApps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbcSelectedApps.Location = new System.Drawing.Point(2, 2);
            this.lbcSelectedApps.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lbcSelectedApps.Name = "lbcSelectedApps";
            this.lbcSelectedApps.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbcSelectedApps.Size = new System.Drawing.Size(185, 442);
            this.lbcSelectedApps.TabIndex = 1;
            this.lbcSelectedApps.DoubleClick += new System.EventHandler(this.lbcSelectedApps_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemPaste,
            this.menuItemCopy,
            this.原始数据ToolStripMenuItem,
            this.所有测点ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(135, 92);
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
            // 所有测点ToolStripMenuItem
            // 
            this.所有测点ToolStripMenuItem.Name = "所有测点ToolStripMenuItem";
            this.所有测点ToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.所有测点ToolStripMenuItem.Text = "所有测点";
            this.所有测点ToolStripMenuItem.Visible = false;
            this.所有测点ToolStripMenuItem.Click += new System.EventHandler(this.所有测点ToolStripMenuItem_Click);
            // 
            // partControl1
            // 
            this.partControl1.AllowDrop = true;
            this.partControl1.BestFitVisibleOnly = true;
            this.partControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.partControl1.KeyFieldName = "ProjectPartID";
            this.partControl1.Location = new System.Drawing.Point(0, 0);
            this.partControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.partControl1.Name = "partControl1";
            this.partControl1.OptionsBehavior.DragNodes = true;
            this.partControl1.OptionsView.AutoWidth = false;
            this.partControl1.ParentFieldName = "ParentPart";
            this.partControl1.Size = new System.Drawing.Size(442, 446);
            this.partControl1.TabIndex = 0;
            this.partControl1.SearchItemClick += new hammergo.CommonControl.SearchDelegate(this.partControl1_SearchItemClick);
            this.partControl1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.partControl1_MouseClick);
            // 
            // panelSelectedApps
            // 
            this.panelSelectedApps.Controls.Add(this.lbcSelectedApps);
            this.panelSelectedApps.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelSelectedApps.Location = new System.Drawing.Point(725, 0);
            this.panelSelectedApps.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelSelectedApps.Name = "panelSelectedApps";
            this.panelSelectedApps.Size = new System.Drawing.Size(189, 446);
            this.panelSelectedApps.TabIndex = 5;
            // 
            // panelApps
            // 
            this.panelApps.Controls.Add(this.lbcApps);
            this.panelApps.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelApps.Location = new System.Drawing.Point(442, 0);
            this.panelApps.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelApps.Name = "panelApps";
            this.panelApps.Size = new System.Drawing.Size(189, 446);
            this.panelApps.TabIndex = 6;
            // 
            // AppSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.partControl1);
            this.Controls.Add(this.panelApps);
            this.Controls.Add(this.panelButtons);
            this.Controls.Add(this.panelSelectedApps);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "AppSelector";
            this.Size = new System.Drawing.Size(914, 446);
            ((System.ComponentModel.ISupportInitialize)(this.lbcApps)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.apparatusBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelButtons)).EndInit();
            this.panelButtons.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.snBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lbcSelectedApps)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.partControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelSelectedApps)).EndInit();
            this.panelSelectedApps.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelApps)).EndInit();
            this.panelApps.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.ListBoxControl lbcApps;
        private DevExpress.XtraEditors.PanelControl panelButtons;
        private DevExpress.XtraEditors.SimpleButton ExeButton;
        private DevExpress.XtraEditors.SimpleButton simpleButton6;
        private DevExpress.XtraEditors.TextEdit snBox;
        private DevExpress.XtraEditors.SimpleButton simpleButton4;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        public DevExpress.XtraEditors.ListBoxControl lbcSelectedApps;
        private PartControl partControl1;
        private System.Windows.Forms.BindingSource apparatusBindingSource;
        private DevExpress.XtraEditors.PanelControl panelSelectedApps;
        private DevExpress.XtraEditors.PanelControl panelApps;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuItemPaste;
        private System.Windows.Forms.ToolStripMenuItem menuItemCopy;
        private System.Windows.Forms.ToolStripMenuItem 原始数据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 所有测点ToolStripMenuItem;
    }
}
