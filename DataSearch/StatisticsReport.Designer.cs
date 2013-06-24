namespace hammergo.DataSearch
{
    partial class StatisticsReport
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
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.textBoxFilterVariable = new DevExpress.XtraEditors.TextEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.c1DateEdit3 = new DevExpress.XtraEditors.DateEdit();
            this.dateCMS3 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.c1DateEdit2 = new DevExpress.XtraEditors.DateEdit();
            this.dateCMS2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.checkBoxShowDate = new DevExpress.XtraEditors.CheckEdit();
            this.customTextBox = new DevExpress.XtraEditors.TextEdit();
            this.label16 = new System.Windows.Forms.Label();
            this.c1DateEdit1 = new DevExpress.XtraEditors.DateEdit();
            this.dateCMS1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.pasteDateMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.label11 = new System.Windows.Forms.Label();
            this.appSelector1 = new hammergo.CommonControl.AppSelector();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.textBoxFilterVariable.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DateEdit3.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DateEdit3.Properties)).BeginInit();
            this.dateCMS3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.c1DateEdit2.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DateEdit2.Properties)).BeginInit();
            this.dateCMS2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkBoxShowDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customTextBox.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DateEdit1.Properties.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DateEdit1.Properties)).BeginInit();
            this.dateCMS1.SuspendLayout();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(220, 820);
            this.xtraTabControl1.TabIndex = 2;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.textBoxFilterVariable);
            this.xtraTabPage1.Controls.Add(this.label3);
            this.xtraTabPage1.Controls.Add(this.c1DateEdit3);
            this.xtraTabPage1.Controls.Add(this.label2);
            this.xtraTabPage1.Controls.Add(this.c1DateEdit2);
            this.xtraTabPage1.Controls.Add(this.label1);
            this.xtraTabPage1.Controls.Add(this.checkBoxShowDate);
            this.xtraTabPage1.Controls.Add(this.customTextBox);
            this.xtraTabPage1.Controls.Add(this.label16);
            this.xtraTabPage1.Controls.Add(this.c1DateEdit1);
            this.xtraTabPage1.Controls.Add(this.label11);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(211, 785);
            this.xtraTabPage1.Text = "检索选项";
            // 
            // textBoxFilterVariable
            // 
            this.textBoxFilterVariable.Location = new System.Drawing.Point(99, 380);
            this.textBoxFilterVariable.Name = "textBoxFilterVariable";
            this.textBoxFilterVariable.Size = new System.Drawing.Size(94, 23);
            this.textBoxFilterVariable.TabIndex = 58;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(11, 378);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 27);
            this.label3.TabIndex = 57;
            this.label3.Text = "过滤成果";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // c1DateEdit3
            // 
            this.c1DateEdit3.EditValue = null;
            this.c1DateEdit3.Location = new System.Drawing.Point(15, 223);
            this.c1DateEdit3.Name = "c1DateEdit3";
            this.c1DateEdit3.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.c1DateEdit3.Properties.ContextMenuStrip = this.dateCMS3;
            this.c1DateEdit3.Properties.DisplayFormat.FormatString = "g";
            this.c1DateEdit3.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.c1DateEdit3.Properties.EditFormat.FormatString = "g";
            this.c1DateEdit3.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.c1DateEdit3.Properties.Mask.EditMask = "g";
            this.c1DateEdit3.Properties.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.c1DateEdit3.Size = new System.Drawing.Size(152, 23);
            this.c1DateEdit3.TabIndex = 56;
            // 
            // dateCMS3
            // 
            this.dateCMS3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2});
            this.dateCMS3.Name = "dateCMS";
            this.dateCMS3.Size = new System.Drawing.Size(153, 48);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem2.Text = "粘贴";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(12, 189);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(181, 19);
            this.label2.TabIndex = 55;
            this.label2.Text = "需要显示的某次观测数据的日期:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // c1DateEdit2
            // 
            this.c1DateEdit2.EditValue = null;
            this.c1DateEdit2.Location = new System.Drawing.Point(15, 151);
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
            this.c1DateEdit2.Size = new System.Drawing.Size(152, 23);
            this.c1DateEdit2.TabIndex = 54;
            // 
            // dateCMS2
            // 
            this.dateCMS2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.dateCMS2.Name = "dateCMS";
            this.dateCMS2.Size = new System.Drawing.Size(105, 26);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(104, 22);
            this.toolStripMenuItem1.Text = "粘贴";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(12, 117);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 19);
            this.label1.TabIndex = 53;
            this.label1.Text = "结束日期:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // checkBoxShowDate
            // 
            this.checkBoxShowDate.EditValue = true;
            this.checkBoxShowDate.Location = new System.Drawing.Point(13, 341);
            this.checkBoxShowDate.Name = "checkBoxShowDate";
            this.checkBoxShowDate.Properties.Caption = "显示时间";
            this.checkBoxShowDate.Size = new System.Drawing.Size(82, 22);
            this.checkBoxShowDate.TabIndex = 0;
            // 
            // customTextBox
            // 
            this.customTextBox.EditValue = "yyyy/MM/dd";
            this.customTextBox.Location = new System.Drawing.Point(15, 303);
            this.customTextBox.Name = "customTextBox";
            this.customTextBox.Size = new System.Drawing.Size(94, 23);
            this.customTextBox.TabIndex = 52;
            // 
            // label16
            // 
            this.label16.Location = new System.Drawing.Point(12, 261);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(128, 27);
            this.label16.TabIndex = 48;
            this.label16.Text = "自定义日期格式";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // c1DateEdit1
            // 
            this.c1DateEdit1.EditValue = null;
            this.c1DateEdit1.Location = new System.Drawing.Point(15, 79);
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
            this.c1DateEdit1.TabIndex = 31;
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
            // label11
            // 
            this.label11.Location = new System.Drawing.Point(12, 45);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(75, 19);
            this.label11.TabIndex = 30;
            this.label11.Text = "起始日期:";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // appSelector1
            // 
            this.appSelector1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.appSelector1.Location = new System.Drawing.Point(220, 0);
            this.appSelector1.Name = "appSelector1";
            this.appSelector1.Size = new System.Drawing.Size(804, 820);
            this.appSelector1.TabIndex = 3;
            this.appSelector1.SearchExeClick += new System.EventHandler<hammergo.CommonControl.AppsEventArgs>(this.appSelector1_SearchExeClick);
            this.appSelector1.ShowDataEvent += new System.EventHandler<hammergo.Utility.AppSearchEventArgs>(this.appSelector1_ShowDataEvent);
            // 
            // StatisticsReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.appSelector1);
            this.Controls.Add(this.xtraTabControl1);
            this.Name = "StatisticsReport";
            this.Size = new System.Drawing.Size(1024, 820);
            this.Load += new System.EventHandler(this.StatisticsReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.textBoxFilterVariable.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DateEdit3.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DateEdit3.Properties)).EndInit();
            this.dateCMS3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.c1DateEdit2.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DateEdit2.Properties)).EndInit();
            this.dateCMS2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkBoxShowDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customTextBox.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DateEdit1.Properties.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.c1DateEdit1.Properties)).EndInit();
            this.dateCMS1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraEditors.TextEdit customTextBox;
        private System.Windows.Forms.Label label16;
        private DevExpress.XtraEditors.CheckEdit checkBoxShowDate;
        private DevExpress.XtraEditors.DateEdit c1DateEdit1;
        private System.Windows.Forms.Label label11;
        private DevExpress.XtraEditors.DateEdit c1DateEdit3;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.DateEdit c1DateEdit2;
        private System.Windows.Forms.Label label1;
        private hammergo.CommonControl.AppSelector appSelector1;
        private DevExpress.XtraEditors.TextEdit textBoxFilterVariable;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ContextMenuStrip dateCMS1;
        private System.Windows.Forms.ToolStripMenuItem pasteDateMenuItem1;
        private System.Windows.Forms.ContextMenuStrip dateCMS2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ContextMenuStrip dateCMS3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
    }
}
