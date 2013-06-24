namespace hammergo.ImportOldData
{
    partial class ImportDataControl
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
            this.lblResult = new System.Windows.Forms.TextBox();
            this.simpleButton10 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton9 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton8 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton5 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton7 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton6 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton4 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton3 = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton2 = new DevExpress.XtraEditors.SimpleButton();
            this.progressBar1 = new DevExpress.XtraEditors.ProgressBarControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.lblOld = new DevExpress.XtraEditors.LabelControl();
            this.btnOld = new DevExpress.XtraEditors.SimpleButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.oleDbCon = new System.Data.OleDb.OleDbConnection();
            ((System.ComponentModel.ISupportInitialize)(this.progressBar1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblResult
            // 
            this.lblResult.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblResult.Location = new System.Drawing.Point(249, 432);
            this.lblResult.Multiline = true;
            this.lblResult.Name = "lblResult";
            this.lblResult.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.lblResult.Size = new System.Drawing.Size(492, 122);
            this.lblResult.TabIndex = 58;
            // 
            // simpleButton10
            // 
            this.simpleButton10.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.simpleButton10.Location = new System.Drawing.Point(594, 341);
            this.simpleButton10.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.simpleButton10.Name = "simpleButton10";
            this.simpleButton10.Size = new System.Drawing.Size(147, 35);
            this.simpleButton10.TabIndex = 57;
            this.simpleButton10.Text = "预留";
            // 
            // simpleButton9
            // 
            this.simpleButton9.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.simpleButton9.Location = new System.Drawing.Point(594, 270);
            this.simpleButton9.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.simpleButton9.Name = "simpleButton9";
            this.simpleButton9.Size = new System.Drawing.Size(120, 35);
            this.simpleButton9.TabIndex = 56;
            this.simpleButton9.Text = "导入备注";
            this.simpleButton9.Click += new System.EventHandler(this.simpleButton9_Click);
            // 
            // simpleButton8
            // 
            this.simpleButton8.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.simpleButton8.Location = new System.Drawing.Point(425, 341);
            this.simpleButton8.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.simpleButton8.Name = "simpleButton8";
            this.simpleButton8.Size = new System.Drawing.Size(147, 35);
            this.simpleButton8.TabIndex = 55;
            this.simpleButton8.Text = "预留";
            // 
            // simpleButton5
            // 
            this.simpleButton5.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.simpleButton5.Location = new System.Drawing.Point(249, 341);
            this.simpleButton5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.simpleButton5.Name = "simpleButton5";
            this.simpleButton5.Size = new System.Drawing.Size(147, 35);
            this.simpleButton5.TabIndex = 54;
            this.simpleButton5.Text = "导入任务集合";
            this.simpleButton5.Click += new System.EventHandler(this.simpleButton5_Click);
            // 
            // simpleButton7
            // 
            this.simpleButton7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.simpleButton7.Location = new System.Drawing.Point(249, 270);
            this.simpleButton7.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.simpleButton7.Name = "simpleButton7";
            this.simpleButton7.Size = new System.Drawing.Size(147, 35);
            this.simpleButton7.TabIndex = 52;
            this.simpleButton7.Text = "导入测量参数和值";
            this.simpleButton7.Click += new System.EventHandler(this.simpleButton7_Click);
            // 
            // simpleButton6
            // 
            this.simpleButton6.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.simpleButton6.Location = new System.Drawing.Point(425, 270);
            this.simpleButton6.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.simpleButton6.Name = "simpleButton6";
            this.simpleButton6.Size = new System.Drawing.Size(134, 35);
            this.simpleButton6.TabIndex = 53;
            this.simpleButton6.Text = "导入计算量和值";
            this.simpleButton6.Click += new System.EventHandler(this.simpleButton6_Click);
            // 
            // simpleButton4
            // 
            this.simpleButton4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.simpleButton4.Location = new System.Drawing.Point(594, 199);
            this.simpleButton4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.simpleButton4.Name = "simpleButton4";
            this.simpleButton4.Size = new System.Drawing.Size(120, 35);
            this.simpleButton4.TabIndex = 51;
            this.simpleButton4.Text = "导入常量参数";
            this.simpleButton4.Click += new System.EventHandler(this.simpleButton4_Click);
            // 
            // simpleButton3
            // 
            this.simpleButton3.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.simpleButton3.Location = new System.Drawing.Point(249, 199);
            this.simpleButton3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.simpleButton3.Name = "simpleButton3";
            this.simpleButton3.Size = new System.Drawing.Size(120, 35);
            this.simpleButton3.TabIndex = 50;
            this.simpleButton3.Text = "导入部位_仪器";
            this.simpleButton3.Click += new System.EventHandler(this.simpleButton3_Click);
            // 
            // simpleButton2
            // 
            this.simpleButton2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.simpleButton2.Location = new System.Drawing.Point(249, 129);
            this.simpleButton2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.simpleButton2.Name = "simpleButton2";
            this.simpleButton2.Size = new System.Drawing.Size(120, 35);
            this.simpleButton2.TabIndex = 49;
            this.simpleButton2.Text = "初始化导入";
            this.simpleButton2.Click += new System.EventHandler(this.simpleButton2_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.progressBar1.Location = new System.Drawing.Point(425, 131);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(385, 27);
            this.progressBar1.TabIndex = 48;
            this.progressBar1.UseWaitCursor = true;
            // 
            // labelControl2
            // 
            this.labelControl2.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labelControl2.Location = new System.Drawing.Point(425, 59);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(90, 18);
            this.labelControl2.TabIndex = 47;
            this.labelControl2.Text = "旧数据库路径";
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.simpleButton1.Location = new System.Drawing.Point(425, 199);
            this.simpleButton1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(120, 35);
            this.simpleButton1.TabIndex = 46;
            this.simpleButton1.Text = "导入仪器类型";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // lblOld
            // 
            this.lblOld.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblOld.Location = new System.Drawing.Point(577, 59);
            this.lblOld.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.lblOld.Name = "lblOld";
            this.lblOld.Size = new System.Drawing.Size(192, 18);
            this.lblOld.TabIndex = 45;
            this.lblOld.Text = "E:\\三峡程序\\dam3Mode.mdb";
            // 
            // btnOld
            // 
            this.btnOld.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnOld.Location = new System.Drawing.Point(249, 54);
            this.btnOld.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOld.Name = "btnOld";
            this.btnOld.Size = new System.Drawing.Size(120, 35);
            this.btnOld.TabIndex = 44;
            this.btnOld.Text = "选择旧数据库";
            this.btnOld.Click += new System.EventHandler(this.btnOld_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Access 2007文件(*.accdb)|*.accdb";
            this.openFileDialog1.RestoreDirectory = true;
            // 
            // ImportDataControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblResult);
            this.Controls.Add(this.simpleButton10);
            this.Controls.Add(this.simpleButton9);
            this.Controls.Add(this.simpleButton8);
            this.Controls.Add(this.simpleButton5);
            this.Controls.Add(this.simpleButton7);
            this.Controls.Add(this.simpleButton6);
            this.Controls.Add(this.simpleButton4);
            this.Controls.Add(this.simpleButton3);
            this.Controls.Add(this.simpleButton2);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.simpleButton1);
            this.Controls.Add(this.lblOld);
            this.Controls.Add(this.btnOld);
            this.Name = "ImportDataControl";
            this.Size = new System.Drawing.Size(1058, 608);
            ((System.ComponentModel.ISupportInitialize)(this.progressBar1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected internal System.Windows.Forms.TextBox lblResult;
        private DevExpress.XtraEditors.SimpleButton simpleButton10;
        private DevExpress.XtraEditors.SimpleButton simpleButton9;
        private DevExpress.XtraEditors.SimpleButton simpleButton8;
        private DevExpress.XtraEditors.SimpleButton simpleButton5;
        private DevExpress.XtraEditors.SimpleButton simpleButton7;
        private DevExpress.XtraEditors.SimpleButton simpleButton6;
        private DevExpress.XtraEditors.SimpleButton simpleButton4;
        private DevExpress.XtraEditors.SimpleButton simpleButton3;
        private DevExpress.XtraEditors.SimpleButton simpleButton2;
        protected internal DevExpress.XtraEditors.ProgressBarControl progressBar1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.LabelControl lblOld;
        private DevExpress.XtraEditors.SimpleButton btnOld;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        protected internal System.Data.OleDb.OleDbConnection oleDbCon;
    }
}
