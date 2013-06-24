namespace hammergo.DataExport
{
    partial class ExportMessureData
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
            this.appSelector1 = new hammergo.CommonControl.AppSelector();
            this.progressBar1 = new DevExpress.XtraEditors.ProgressBarControl();
            this.lblInfo = new DevExpress.XtraEditors.LabelControl();
            this.btnOut = new DevExpress.XtraEditors.SimpleButton();
            this.calcEdit1 = new DevExpress.XtraEditors.CalcEdit();
            this.c1DateEdit1 = new DevExpress.XtraEditors.DateEdit();
            this.dateCMS1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pasteDateMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBar1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.calcEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DateEdit1.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DateEdit1.Properties)).BeginInit();
            this.dateCMS1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.panelControl1.Controls.Add(this.appSelector1);
            this.panelControl1.Controls.Add(this.progressBar1);
            this.panelControl1.Controls.Add(this.lblInfo);
            this.panelControl1.Controls.Add(this.btnOut);
            this.panelControl1.Controls.Add(this.calcEdit1);
            this.panelControl1.Controls.Add(this.c1DateEdit1);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Location = new System.Drawing.Point(102, 86);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(820, 648);
            this.panelControl1.TabIndex = 1;
            // 
            // appSelector1
            // 
            this.appSelector1.Dock = System.Windows.Forms.DockStyle.Top;
            this.appSelector1.ExeButtonEnable = false;
            this.appSelector1.ExeButtonText = "数据检索";
            this.appSelector1.Location = new System.Drawing.Point(2, 2);
            this.appSelector1.Name = "appSelector1";
            this.appSelector1.ShowAllAppMenuVisible = true;
            this.appSelector1.Size = new System.Drawing.Size(816, 347);
            this.appSelector1.TabIndex = 39;
            this.appSelector1.ShowDataEvent += new System.EventHandler<hammergo.Utility.AppSearchEventArgs>(this.appSelector1_ShowDataEvent);
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(2, 623);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(816, 23);
            this.progressBar1.TabIndex = 38;
            // 
            // lblInfo
            // 
            this.lblInfo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblInfo.Location = new System.Drawing.Point(59, 557);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(56, 17);
            this.lblInfo.TabIndex = 37;
            this.lblInfo.Text = "提示信息";
            // 
            // btnOut
            // 
            this.btnOut.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnOut.Location = new System.Drawing.Point(434, 501);
            this.btnOut.Name = "btnOut";
            this.btnOut.Size = new System.Drawing.Size(70, 23);
            this.btnOut.TabIndex = 34;
            this.btnOut.Text = "开始导出";
            this.btnOut.ToolTip = "将选定测点某个时刻的观测数据输出到excel中";
            this.btnOut.Click += new System.EventHandler(this.btnOut_Click);
            // 
            // calcEdit1
            // 
            this.calcEdit1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.calcEdit1.EditValue = new decimal(new int[] {
            35,
            0,
            0,
            65536});
            this.calcEdit1.Location = new System.Drawing.Point(434, 449);
            this.calcEdit1.Name = "calcEdit1";
            this.calcEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.calcEdit1.Size = new System.Drawing.Size(100, 23);
            this.calcEdit1.TabIndex = 33;
            // 
            // c1DateEdit1
            // 
            this.c1DateEdit1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.c1DateEdit1.EditValue = null;
            this.c1DateEdit1.Location = new System.Drawing.Point(434, 397);
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
            this.c1DateEdit1.Size = new System.Drawing.Size(152, 23);
            this.c1DateEdit1.TabIndex = 32;
            // 
            // dateCMS1
            // 
            this.dateCMS1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pasteDateMenuItem1});
            this.dateCMS1.Name = "dateCMS";
            this.dateCMS1.Size = new System.Drawing.Size(105, 26);
            // 
            // pasteDateMenuItem1
            // 
            this.pasteDateMenuItem1.Name = "pasteDateMenuItem1";
            this.pasteDateMenuItem1.Size = new System.Drawing.Size(104, 22);
            this.pasteDateMenuItem1.Text = "粘贴";
            this.pasteDateMenuItem1.Click += new System.EventHandler(this.pasteDateMenuItem1_Click);
            // 
            // labelControl2
            // 
            this.labelControl2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelControl2.Location = new System.Drawing.Point(234, 452);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(98, 17);
            this.labelControl2.TabIndex = 3;
            this.labelControl2.Text = "上下浮动小时数";
            // 
            // labelControl1
            // 
            this.labelControl1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelControl1.Location = new System.Drawing.Point(234, 403);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(103, 17);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "被导出数据日期:";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            // 
            // ExportMessureData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Name = "ExportMessureData";
            this.Size = new System.Drawing.Size(1024, 820);
            this.Load += new System.EventHandler(this.ExportMessureData_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.progressBar1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.calcEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DateEdit1.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DateEdit1.Properties)).EndInit();
            this.dateCMS1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit c1DateEdit1;
        private System.Windows.Forms.ContextMenuStrip dateCMS1;
        private System.Windows.Forms.ToolStripMenuItem pasteDateMenuItem1;
        private DevExpress.XtraEditors.CalcEdit calcEdit1;
        private DevExpress.XtraEditors.SimpleButton btnOut;
        private DevExpress.XtraEditors.LabelControl lblInfo;
        private DevExpress.XtraEditors.ProgressBarControl progressBar1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private hammergo.CommonControl.AppSelector appSelector1;


    }
}
