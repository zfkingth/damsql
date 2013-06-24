namespace hammergo.DataManage
{
    partial class BatchChangeDate
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
            this.c1DateEdit2 = new DevExpress.XtraEditors.DateEdit();
            this.dateCMS2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.taskAppSelector1 = new hammergo.DataManage.TaskAppSelector();
            this.lblInfo = new DevExpress.XtraEditors.LabelControl();
            this.progressBar1 = new DevExpress.XtraEditors.ProgressBarControl();
            this.btnOut = new DevExpress.XtraEditors.SimpleButton();
            this.c1DateEdit1 = new DevExpress.XtraEditors.DateEdit();
            this.dateCMS1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pasteDateMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
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
            this.panelControl1.Controls.Add(this.c1DateEdit2);
            this.panelControl1.Controls.Add(this.labelControl2);
            this.panelControl1.Controls.Add(this.taskAppSelector1);
            this.panelControl1.Controls.Add(this.lblInfo);
            this.panelControl1.Controls.Add(this.progressBar1);
            this.panelControl1.Controls.Add(this.btnOut);
            this.panelControl1.Controls.Add(this.c1DateEdit1);
            this.panelControl1.Controls.Add(this.labelControl1);
            this.panelControl1.Location = new System.Drawing.Point(116, 234);
            this.panelControl1.Margin = new System.Windows.Forms.Padding(4);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(915, 586);
            this.panelControl1.TabIndex = 3;
            // 
            // c1DateEdit2
            // 
            this.c1DateEdit2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.c1DateEdit2.EditValue = null;
            this.c1DateEdit2.Location = new System.Drawing.Point(712, 318);
            this.c1DateEdit2.Margin = new System.Windows.Forms.Padding(4);
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
            this.c1DateEdit2.Size = new System.Drawing.Size(165, 25);
            this.c1DateEdit2.TabIndex = 44;
            this.c1DateEdit2.ToolTip = "如果起始日期为空，导出数据的起始日期将没有限制";
            this.c1DateEdit2.ParseEditValue += new DevExpress.XtraEditors.Controls.ConvertEditValueEventHandler(this.c1DateEdit2_ParseEditValue);
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
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click_1);
            // 
            // labelControl2
            // 
            this.labelControl2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelControl2.Location = new System.Drawing.Point(712, 279);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(45, 18);
            this.labelControl2.TabIndex = 43;
            this.labelControl2.Text = "新日期";
            // 
            // taskAppSelector1
            // 
            this.taskAppSelector1.Dock = System.Windows.Forms.DockStyle.Left;
            this.taskAppSelector1.Location = new System.Drawing.Point(2, 2);
            this.taskAppSelector1.Margin = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.taskAppSelector1.Name = "taskAppSelector1";
            this.taskAppSelector1.Size = new System.Drawing.Size(703, 534);
            this.taskAppSelector1.TabIndex = 42;
            this.taskAppSelector1.ShowDataEvent += new System.EventHandler<hammergo.Utility.AppSearchEventArgs>(this.taskAppSelector1_ShowDataEvent);
            // 
            // lblInfo
            // 
            this.lblInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblInfo.Location = new System.Drawing.Point(2, 536);
            this.lblInfo.Margin = new System.Windows.Forms.Padding(4);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(60, 18);
            this.lblInfo.TabIndex = 37;
            this.lblInfo.Text = "提示信息";
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(2, 554);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(911, 30);
            this.progressBar1.TabIndex = 38;
            // 
            // btnOut
            // 
            this.btnOut.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnOut.Location = new System.Drawing.Point(712, 364);
            this.btnOut.Margin = new System.Windows.Forms.Padding(4);
            this.btnOut.Name = "btnOut";
            this.btnOut.Size = new System.Drawing.Size(165, 30);
            this.btnOut.TabIndex = 34;
            this.btnOut.Text = "开始";
            this.btnOut.Click += new System.EventHandler(this.btnOut_Click);
            // 
            // c1DateEdit1
            // 
            this.c1DateEdit1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.c1DateEdit1.EditValue = null;
            this.c1DateEdit1.Location = new System.Drawing.Point(712, 232);
            this.c1DateEdit1.Margin = new System.Windows.Forms.Padding(4);
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
            this.c1DateEdit1.Size = new System.Drawing.Size(165, 25);
            this.c1DateEdit1.TabIndex = 32;
            this.c1DateEdit1.ToolTip = "如果起始日期为空，导出数据的起始日期将没有限制";
            this.c1DateEdit1.ParseEditValue += new DevExpress.XtraEditors.Controls.ConvertEditValueEventHandler(this.c1DateEdit1_ParseEditValue);
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
            this.labelControl1.Location = new System.Drawing.Point(712, 194);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 18);
            this.labelControl1.TabIndex = 2;
            this.labelControl1.Text = "原始日期";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // BatchChangeDate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelControl1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "BatchChangeDate";
            this.Size = new System.Drawing.Size(1171, 1054);
            this.Load += new System.EventHandler(this.BatchDelete_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
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
        private DevExpress.XtraEditors.ProgressBarControl progressBar1;
        private DevExpress.XtraEditors.LabelControl lblInfo;
        private DevExpress.XtraEditors.SimpleButton btnOut;
        private DevExpress.XtraEditors.DateEdit c1DateEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private TaskAppSelector taskAppSelector1;
        private System.Windows.Forms.ContextMenuStrip dateCMS1;
        private System.Windows.Forms.ToolStripMenuItem pasteDateMenuItem1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.DateEdit c1DateEdit2;
        private System.Windows.Forms.ContextMenuStrip dateCMS2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
    }
}
