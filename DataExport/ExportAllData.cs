using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using hammergo.GlobalConfig;
using hammergo.Utility;
using DevExpress.XtraExport;
using DevExpress.XtraGrid.Export;

namespace hammergo.DataExport
{


    public partial class ExportAllData : DevExpress.XtraEditors.XtraUserControl, hammergo.Utility.IShowAppData
    {
        public ExportAllData()
        {
            InitializeComponent();
        }
        public delegate void ExportXLSDelegate(String myString,string appName);
        public ExportXLSDelegate xlsDelgate;

        public delegate void ExportTxtDelegate(String myString, string appName);
        public ExportTxtDelegate txtDelegate;

        public delegate void SearchDelegate(string appName, DateTime? startDate, DateTime? endDate);
        public SearchDelegate searchDelegate;

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

        private void pasteDateMenuItem1_Click(object sender, EventArgs e)
        {
            Utility.Utility.handlePasteInDateEdit(c1DateEdit1);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Utility.Utility.handlePasteInDateEdit(c1DateEdit2);
        }


        private int highestPercentageReached = 0;

        private void btnOut_Click(object sender, EventArgs e)
        {
            btnOut.Enabled = false;

            btnSel.Enabled = false;



            try
            {
                DateTime? startDate = null, endDate = null;
                if (c1DateEdit1.EditValue != null)
                {
                    startDate = c1DateEdit1.DateTime;
                }
                if (c1DateEdit2.EditValue != null)
                {
                    endDate = c1DateEdit2.DateTime;
                }

                List<string> appNamelist = new List<string>(appSelector1.lbcSelectedApps.Items.Count);
                // Start the asynchronous operation.
                foreach (string item in appSelector1.lbcSelectedApps.Items)
                {
                    appNamelist.Add(item);
                }

                highestPercentageReached = 0;
                progressBar1.Text = highestPercentageReached.ToString();


                backgroundWorker1.RunWorkerAsync(new ExportAllDataArgs(startDate, endDate, appNamelist));

                lblInfo.Text = ".......";
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "错误!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                lblInfo.Text = ex.Message;
                btnOut.Enabled = true;
                btnSel.Enabled = true;
            }
        }


        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            // Get the BackgroundWorker that raised this event.
            BackgroundWorker worker = sender as BackgroundWorker;

            try
            {
                ExportAllDataArgs args = e.Argument as ExportAllDataArgs;
                if (args != null)
                {


                    List<string> appNameList = args.AppNameList;
                    int count = appNameList.Count;

                    for (int i = 0; i < count; i++)
                    {
                        string appName = appNameList[i];

                        //exporter.export(fullPath, appName);
                        export(fullPath, appName, args.StartDate, args.EndDate);

                        int percentComplete = (int)((i + 1.0f) / count * 100);

                        if (percentComplete > highestPercentageReached)
                        {
                            highestPercentageReached = percentComplete;
                            worker.ReportProgress(percentComplete, "正在导出......");
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                Utility.Utility.log(ex);
                XtraMessageBox.Show(ex.Message, "错误!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }





        string[] suffixs = new string[] { "xls", "txt", "html", "xml" };
        private void export(string fullPath, string appName, DateTime? startDate, DateTime? endDate)
        {

            //appData1.search(appName, startDate, endDate, true);
            this.Invoke(searchDelegate, new object[] { appName, startDate, endDate });
            string fileName = String.Format(@"{0}\{1}.{2}", fullPath, appName, suffixs[radioGroup1.SelectedIndex]);
            //IExportProvider provider = null;

            switch (radioGroup1.SelectedIndex)
            {
                case 0:


                    this.Invoke(xlsDelgate, new Object[] { fileName,appName });
                    // appData1.gridControl1.MainView.ExportToText(fileName);
                    //appData1.Invoke(appData1.xlsDel, new Object[] { fileName });
                    break;
                case 1:

                    this.Invoke(txtDelegate, new Object[] { fileName, appName });
                    //appData1.gridControl1.MainView.ExportToText(fileName);
                    break;
                case 2:

                   
                    break;
                default: break;



            }




        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Text = e.ProgressPercentage.ToString();
            lblInfo.Text = e.UserState.ToString();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lblInfo.Text = "导出完成";
            btnOut.Enabled = true;
            btnSel.Enabled = true;
        }

        ExportLib.GridViewExport exporter;
        private void ExportAllData_Load(object sender, EventArgs e)
        {
            appSelector1.initial();

            exporter = new ExportLib.GridViewExport(appData1.gridControl1.MainView as DevExpress.XtraGrid.Views.Grid.GridView, "name");
            c1DateEdit1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            c1DateEdit1.Properties.DisplayFormat.FormatString = PubConstant.customString;

            c1DateEdit2.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            c1DateEdit2.Properties.DisplayFormat.FormatString = PubConstant.customString;

            xlsDelgate = delegate(string fileName,string appName)
            {
                DevExpress.XtraPrinting.XlsExportOptions opt = new DevExpress.XtraPrinting.XlsExportOptions();
                opt.SheetName = appName;
                appData1.gridControl1.MainView.ExportToXls(fileName,opt);
            };

            txtDelegate = delegate(string fileName, string appName)
            {
                appData1.gridControl1.MainView.ExportToText(fileName);
            };

            searchDelegate = delegate(string appName, DateTime? startDate, DateTime? endDate)
            {
                appData1.search(appName, startDate, endDate, true);
            };
        }



        #region IShowAppData 成员

        public event EventHandler<AppSearchEventArgs> ShowDataEvent;

        #endregion

        private void appSelector1_ShowDataEvent(object sender, AppSearchEventArgs e)
        {
            if (ShowDataEvent != null)
            {
                ShowDataEvent(sender, e);
            }
        }
    }
}
