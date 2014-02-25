namespace hammergo.DataExport
{
    partial class AppendExport
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
            this.lblInfo = new DevExpress.XtraEditors.LabelControl();
            this.RBSingle = new DevExpress.XtraEditors.RadioGroup();
            this.btnSel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOut = new DevExpress.XtraEditors.SimpleButton();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.folderImporter = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.RBSingle.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblInfo
            // 
            this.lblInfo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblInfo.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.lblInfo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblInfo.Location = new System.Drawing.Point(158, 392);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(709, 132);
            this.lblInfo.TabIndex = 14;
            this.lblInfo.Text = "结果:";
            // 
            // RBSingle
            // 
            this.RBSingle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.RBSingle.EditValue = true;
            this.RBSingle.Location = new System.Drawing.Point(369, 296);
            this.RBSingle.Name = "RBSingle";
            this.RBSingle.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(true, "单层目录"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(false, "目录及所有子目录")});
            this.RBSingle.Size = new System.Drawing.Size(165, 71);
            this.RBSingle.TabIndex = 13;
            // 
            // btnSel
            // 
            this.btnSel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSel.Location = new System.Drawing.Point(585, 296);
            this.btnSel.Name = "btnSel";
            this.btnSel.Size = new System.Drawing.Size(70, 23);
            this.btnSel.TabIndex = 12;
            this.btnSel.Text = "选择路径";
            this.btnSel.Click += new System.EventHandler(this.btnSel_Click);
            // 
            // btnOut
            // 
            this.btnOut.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnOut.Enabled = false;
            this.btnOut.Location = new System.Drawing.Point(585, 344);
            this.btnOut.Name = "btnOut";
            this.btnOut.Size = new System.Drawing.Size(70, 23);
            this.btnOut.TabIndex = 11;
            this.btnOut.Text = "开始导出";
            this.btnOut.Click += new System.EventHandler(this.btnOut_Click);
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // AppendExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.RBSingle);
            this.Controls.Add(this.btnSel);
            this.Controls.Add(this.btnOut);
            this.Name = "AppendExport";
            this.Size = new System.Drawing.Size(1024, 820);
            this.Load += new System.EventHandler(this.AppendExport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.RBSingle.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblInfo;
        private DevExpress.XtraEditors.RadioGroup RBSingle;
        private DevExpress.XtraEditors.SimpleButton btnSel;
        private DevExpress.XtraEditors.SimpleButton btnOut;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.FolderBrowserDialog folderImporter;
    }
}
