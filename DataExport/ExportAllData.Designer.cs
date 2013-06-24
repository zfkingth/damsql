namespace hammergo.DataExport
{
    partial class ExportAllData
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
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.btnSel = new DevExpress.XtraEditors.SimpleButton();
            this.c1DateEdit2 = new DevExpress.XtraEditors.DateEdit();
            this.dateCMS2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.appSelector1 = new hammergo.CommonControl.AppSelector();
            this.progressBar1 = new DevExpress.XtraEditors.ProgressBarControl();
            this.lblInfo = new DevExpress.XtraEditors.LabelControl();
            this.btnOut = new DevExpress.XtraEditors.SimpleButton();
            this.c1DateEdit1 = new DevExpress.XtraEditors.DateEdit();
            this.dateCMS1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pasteDateMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.folderImporter = new System.Windows.Forms.FolderBrowserDialog();
            this.appData1 = new hammergo.DataManage.AppData();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DateEdit2.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DateEdit2.Properties)).BeginInit();
            this.dateCMS2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBar1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DateEdit1.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DateEdit1.Properties)).BeginInit();
            this.dateCMS1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panelControl1.Controls.Add(this.radioGroup1);
            this.panelControl1.Controls.Add(this.labelControl3);
            this.panelControl1.Controls.Add(this.btnSel);
            this.panelControl1.Controls.Add(this.c1DateEdit2);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.appSelector1);
            this.panelControl1.Controls.Add(this.progressBar1);
            this.panelControl1.Controls.Add(this.lblInfo);
            this.panelControl1.Controls.Add(this.btnOut);
            this.panelControl1.Controls.Add(this.c1DateEdit1);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Location = new System.Drawing.Point(117, 111);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(937, 763);
            this.panelControl1.TabIndex = 2;
            // 
            // radioGroup1
            // 
            this.radioGroup1.EditValue = 0;
            this.radioGroup1.Location = new System.Drawing.Point(321, 575);
            this.radioGroup1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "Excel"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "Txt(纯文本)")});
            this.radioGroup1.Size = new System.Drawing.Size(451, 36);
            this.radioGroup1.TabIndex = 44;
            // 
            // labelControl3
            // 
            this.labelControl3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelControl3.Location = new System.Drawing.Point(211, 580);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(95, 18);
            this.labelControl3.TabIndex = 43;
            this.labelControl3.Text = "输出文件格式:";
            // 
            // btnSel
            // 
            this.btnSel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSel.Location = new System.Drawing.Point(366, 639);
            this.btnSel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSel.Name = "btnSel";
            this.btnSel.Size = new System.Drawing.Size(80, 30);
            this.btnSel.TabIndex = 42;
            this.btnSel.Text = "选择路径";
            this.btnSel.Click += new System.EventHandler(this.btnSel_Click);
            // 
            // c1DateEdit2
            // 
            this.c1DateEdit2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.c1DateEdit2.EditValue = null;
            this.c1DateEdit2.Location = new System.Drawing.Point(579, 510);
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
            this.c1DateEdit2.Size = new System.Drawing.Size(147, 25);
            this.c1DateEdit2.TabIndex = 41;
            this.c1DateEdit2.ToolTip = "如果结束日期为空，导出数据的结束日期将没有限制";
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
            // labelControl2
            // 
            this.labelControl2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelControl2.Location = new System.Drawing.Point(487, 522);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(65, 18);
            this.labelControl2.TabIndex = 40;
            this.labelControl2.Text = "结束日期:";
            // 
            // appSelector1
            // 
            this.appSelector1.Dock = System.Windows.Forms.DockStyle.Top;
            this.appSelector1.ExeButtonEnable = false;
            this.appSelector1.ExeButtonText = "数据检索";
            this.appSelector1.Location = new System.Drawing.Point(2, 2);
            this.appSelector1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.appSelector1.Name = "appSelector1";
            this.appSelector1.ShowAllAppMenuVisible = true;
            this.appSelector1.Size = new System.Drawing.Size(933, 446);
            this.appSelector1.TabIndex = 39;
            this.appSelector1.ShowDataEvent += new System.EventHandler<hammergo.Utility.AppSearchEventArgs>(this.appSelector1_ShowDataEvent);
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(2, 731);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(933, 30);
            this.progressBar1.TabIndex = 38;
            // 
            // lblInfo
            // 
            this.lblInfo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblInfo.Location = new System.Drawing.Point(67, 694);
            this.lblInfo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(60, 18);
            this.lblInfo.TabIndex = 37;
            this.lblInfo.Text = "提示信息";
            // 
            // btnOut
            // 
            this.btnOut.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnOut.Enabled = false;
            this.btnOut.Location = new System.Drawing.Point(491, 639);
            this.btnOut.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOut.Name = "btnOut";
            this.btnOut.Size = new System.Drawing.Size(80, 30);
            this.btnOut.TabIndex = 34;
            this.btnOut.Text = "开始导出";
            this.btnOut.ToolTip = "将选定测点数据到单独的excel中";
            this.btnOut.Click += new System.EventHandler(this.btnOut_Click);
            // 
            // c1DateEdit1
            // 
            this.c1DateEdit1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.c1DateEdit1.EditValue = null;
            this.c1DateEdit1.Location = new System.Drawing.Point(304, 510);
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
            this.c1DateEdit1.Size = new System.Drawing.Size(147, 25);
            this.c1DateEdit1.TabIndex = 32;
            this.c1DateEdit1.ToolTip = "如果起始日期为空，导出数据的起始日期将没有限制";
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
            // labelControl1
            // 
            this.labelControl1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelControl1.Location = new System.Drawing.Point(211, 518);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(65, 18);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "起始日期:";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // appData1
            // 
            this.appData1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.appData1.Location = new System.Drawing.Point(0, 897);
            this.appData1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.appData1.Name = "appData1";
            this.appData1.ShowInputPanel = true;
            this.appData1.Size = new System.Drawing.Size(1170, 157);
            this.appData1.TabIndex = 3;
            this.appData1.Visible = false;
            // 
            // ExportAllData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.appData1);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "ExportAllData";
            this.Size = new System.Drawing.Size(1170, 1054);
            this.Load += new System.EventHandler(this.ExportAllData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DateEdit2.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DateEdit2.Properties)).EndInit();
            this.dateCMS2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.progressBar1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DateEdit1.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DateEdit1.Properties)).EndInit();
            this.dateCMS1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private hammergo.CommonControl.AppSelector appSelector1;
        private DevExpress.XtraEditors.ProgressBarControl progressBar1;
        private DevExpress.XtraEditors.LabelControl lblInfo;
        private DevExpress.XtraEditors.SimpleButton btnOut;
        private DevExpress.XtraEditors.DateEdit c1DateEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit c1DateEdit2;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ContextMenuStrip dateCMS1;
        private System.Windows.Forms.ToolStripMenuItem pasteDateMenuItem1;
        private System.Windows.Forms.FolderBrowserDialog folderImporter;
        private DevExpress.XtraEditors.SimpleButton btnSel;
        private System.Windows.Forms.ContextMenuStrip dateCMS2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private hammergo.DataManage.AppData appData1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
    }
}
