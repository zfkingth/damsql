namespace hammergo.DataImport
{
    partial class ImportExcelData
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
            this.btnSel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOut = new DevExpress.XtraEditors.SimpleButton();
            this.RBSingle = new DevExpress.XtraEditors.RadioGroup();
            this.lblInfo = new DevExpress.XtraEditors.LabelControl();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.folderImporter = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.RBSingle.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSel
            // 
            this.btnSel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSel.Location = new System.Drawing.Point(585, 375);
            this.btnSel.Name = "btnSel";
            this.btnSel.Size = new System.Drawing.Size(70, 23);
            this.btnSel.TabIndex = 8;
            this.btnSel.Text = "ѡ��·��";
            this.btnSel.Click += new System.EventHandler(this.btnSel_Click);
            // 
            // btnOut
            // 
            this.btnOut.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnOut.Enabled = false;
            this.btnOut.Location = new System.Drawing.Point(585, 423);
            this.btnOut.Name = "btnOut";
            this.btnOut.Size = new System.Drawing.Size(70, 23);
            this.btnOut.TabIndex = 7;
            this.btnOut.Text = "��ʼ����";
            this.btnOut.Click += new System.EventHandler(this.btnOut_Click);
            // 
            // RBSingle
            // 
            this.RBSingle.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.RBSingle.EditValue = true;
            this.RBSingle.Location = new System.Drawing.Point(369, 375);
            this.RBSingle.Name = "RBSingle";
            this.RBSingle.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(true, "����Ŀ¼"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(false, "Ŀ¼��������Ŀ¼")});
            this.RBSingle.Size = new System.Drawing.Size(165, 71);
            this.RBSingle.TabIndex = 9;
            // 
            // lblInfo
            // 
            this.lblInfo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblInfo.Appearance.Options.UseTextOptions = true;
            this.lblInfo.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.lblInfo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblInfo.Location = new System.Drawing.Point(158, 471);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(709, 132);
            this.lblInfo.TabIndex = 10;
            this.lblInfo.Text = "���:";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            // 
            // ImportExcelData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.RBSingle);
            this.Controls.Add(this.btnSel);
            this.Controls.Add(this.btnOut);
            this.Name = "ImportExcelData";
            this.Size = new System.Drawing.Size(1024, 820);
            ((System.ComponentModel.ISupportInitialize)(this.RBSingle.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnSel;
        private DevExpress.XtraEditors.SimpleButton btnOut;
        private DevExpress.XtraEditors.RadioGroup RBSingle;
        private DevExpress.XtraEditors.LabelControl lblInfo;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.FolderBrowserDialog folderImporter;
    }
}
