using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using hammergo.Utility;

namespace hammergo.DataImport
{
    public partial class ImportExcelData : DevExpress.XtraEditors.XtraUserControl,ICustomDispose
    {
        public ImportExcelData()
        {
            InitializeComponent();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // Get the BackgroundWorker that raised this event.
            BackgroundWorker worker = sender as BackgroundWorker;

            try
            {

                if (e.Argument is string)
                {
                    handleSingleDir((string)e.Argument);
                }
                else
                {
                    handleTreeDir((DirectoryInfo)e.Argument);
                }
            }
            catch (Exception ex)
            {
                Utility.Utility.log(ex);
                XtraMessageBox.Show( ex.Message, "错误!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        ExcelImporter importer = new ExcelImporter();
        private void handleTreeDir(DirectoryInfo directoryInfo)
        {
            foreach (DirectoryInfo cdir in directoryInfo.GetDirectories())
            {
                handleTreeDir(cdir);
            }

            handleSingleDir(directoryInfo.FullName);
        }

        private void handleSingleDir(string directory)
        {

            FileInfo[] xlsFiles;
            DirectoryInfo dir = new DirectoryInfo(directory);
            xlsFiles = dir.GetFiles("*.xls");

            if (xlsFiles.Length == 0)
            {

                return;
            }

            FileInfo exInfo = null;

            try
            {

                foreach (FileInfo info in xlsFiles)
                {
                    exInfo = info;
                    backgroundWorker1.ReportProgress(0, "从" + exInfo.FullName + "导入数据: ");

                    importer.import(info.FullName);

                }

            }
            catch (Exception ex)
            {
                Utility.Utility.log(ex);
                if (exInfo != null)
                    throw new Exception(exInfo.FullName + "\n" + ex.Message);
                else
                    throw new Exception(ex.Message);

            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lblInfo.Text = e.UserState.ToString();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lblInfo.Text = "导入完成";
            btnOut.Enabled = true;
            btnSel.Enabled = true;
            //hammergo.MyControls.Helper.QuitExcel();
        }

        string fullPath = "";
        private void btnSel_Click(object sender, EventArgs e)
        {
            if (folderImporter.ShowDialog(this) == DialogResult.OK)
            {
                fullPath = folderImporter.SelectedPath;

                btnOut.Enabled = true;

                lblInfo.Text = "路径: " + fullPath;
            }
        }

        private void btnOut_Click(object sender, EventArgs e)
        {
            btnOut.Enabled = false;

            btnSel.Enabled = false;

            //System.Threading.Thread thread=new System.Threading.Thread(new System.Threading.ThreadStart(runThread));

            //thread.Start();

            try
            {
                if (RBSingle.SelectedIndex==0)
                {

                    backgroundWorker1.RunWorkerAsync(fullPath);

                }
                else
                {

                    backgroundWorker1.RunWorkerAsync(new DirectoryInfo(fullPath));


                }
                lblInfo.Text = "导入完成!";
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "错误!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                lblInfo.Text = ex.Message;
                btnOut.Enabled = true;
                btnSel.Enabled = true;
            }
        }

        #region ICustomDispose 成员

        public void CustomDispose()
        {
            if (importer != null && importer.excelHelper != null)
            {
                importer.excelHelper.Dispose();
                this.Dispose();

            }
        }

        #endregion
    }
}
