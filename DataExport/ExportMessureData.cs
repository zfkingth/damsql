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
using hammergo.Model;

namespace hammergo.DataExport
{
    public partial class ExportMessureData : DevExpress.XtraEditors.XtraUserControl, ICustomDispose, hammergo.Utility.IShowAppData
    {
        public ExportMessureData()
        {
            InitializeComponent();
        }

        private void pasteDateMenuItem1_Click(object sender, EventArgs e)
        {
            Utility.Utility.handlePasteInDateEdit(c1DateEdit1);
        }

        private void ExportMessureData_Load(object sender, EventArgs e)
        {
            appSelector1.initial();
           
            c1DateEdit1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            c1DateEdit1.Properties.DisplayFormat.FormatString = PubConstant.customString;
           
        }

        //string fullPath = "";

        //private void btnSel_Click(object sender, EventArgs e)
        //{
        //    if (folderImporter.ShowDialog(this) == DialogResult.OK)
        //    {
        //        fullPath = folderImporter.SelectedPath;

        //        btnOut.Enabled = true;

        //        lblInfo.Text = "·��: " + fullPath;
        //    }
        //}

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {



            try
            {

                // Get the BackgroundWorker that raised this event.
                BackgroundWorker worker = sender as BackgroundWorker;

                List<List<string>> keyList = new List<List<string>>(20);
                List<List<string>> valueList = new List<List<string>>(20);//����һ�㲻�ᳬ��20��

                List<string> applist = (List<string>)e.Argument;

                int count = applist.Count;

                for (int i = 0; i < count; i++)
                {
                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;
                    }
                    else
                    {
                        string sn = applist[i];
                        AppIntegratedInfo appInfo = new AppIntegratedInfo(sn, 1, null, DateTime.MaxValue);

                        List<string> paNameList = new List<string>(appInfo.MessureParams.Count);

                        foreach (MessureParam mp in appInfo.MessureParams)
                        {
                            paNameList.Add(mp.ParamName);
                        }

                        //Ѱ���Ƿ������ͬ��key
                        bool haveSame = false;
                        int keyIndex = 0;
                        for (; keyIndex < keyList.Count; keyIndex++)
                        {
                            List<string> keyItem = keyList[keyIndex];
                            if (paNameList.Count != keyItem.Count)
                            {
                                continue; //����������һ��keyItem
                            }

                            //������ͬ,�ٴβ���
                            int nameIndex = 0;
                            for (; nameIndex < paNameList.Count; nameIndex++)
                            {
                                if (keyItem.Contains(paNameList[nameIndex]) == false)
                                {
                                    break;//��ǰkeyItem�Ѳ�����Ҫ��
                                }
                            }

                            if (nameIndex == paNameList.Count)//���е���Ѳ�����
                            {
                                haveSame = true;
                                break;//�Ѿ��ҵ��˳���ѭ��
                            }

                        }
                        if (haveSame == false)
                        {
                            keyList.Add(paNameList);
                            //���valueList����
                            List<string> groupedAppList = new List<string>(200);//һ������������200
                            valueList.Add(groupedAppList);

                        }
                        //�����Ƿ�ɹ�������������������������
                        valueList[keyIndex].Add(sn);//�������������list



                        int percentComplete =
                            (int)((i + 1.0f) / count * 100);

                        if (percentComplete > highestPercentageReached)
                        {
                            highestPercentageReached = percentComplete;
                            worker.ReportProgress(percentComplete, "���ڷ��������Ĳ���");
                        }

                    }
                }

                List<DataTable> tableList = new List<DataTable>(keyList.Count);

                highestPercentageReached = 0;
                int j = 0;
                for (int i = 0; i < valueList.Count; i++)
                {
                    List<string> groupedAppList = valueList[i];
                    //ʹ��Table����������,���������ļ�
                    //����Table
                    List<string> paNameList = keyList[i];
                    DataTable table = createDataTableByPaNameList(paNameList);

                    foreach (string appName in groupedAppList)
                    {
                        DataRow row = table.NewRow();

                        FillMessureValueRow(row, appName, selTime, blurHours);
                        table.Rows.Add(row);

                        int percentComplete = (int)((j + 1.0f) / count * 100);
                        j++;

                        if (percentComplete > highestPercentageReached)
                        {
                            highestPercentageReached = percentComplete;
                            worker.ReportProgress(percentComplete, "���ڲ�ѯ����");
                        }

                    }

                    table.AcceptChanges();



                    tableList.Add(table);

                }



                //���������excel
                //hammergo.MyControls.Helper.OutPutTableListToExcel(tableList);

                if (exporter == null)
                {
                    exporter = new hammergo.ExportLib.ExcelExportMessureExport();
                }

                exporter.OutPutTableListToExcel(tableList);
        

                e.Result = "��ɵ���";
            }
            catch (Exception ex)
            {
                e.Result = ex.Message;
                Utility.Utility.log(ex);
            }

        }


        hammergo.ExportLib.ExcelExportMessureExport exporter = null;
        
          



        private DataTable createDataTableByPaNameList(List<string> paNameList)
        {
            DataTable table = new DataTable();
            table.Columns.Add("�����", typeof(string));
            table.Columns.Add(PubConstant.timeColumnName, typeof(DateTime));

            foreach (string name in paNameList)
            {
                DataColumn column = new DataColumn(name, typeof(double));
                ParamInfo paramInfo= PubConstant.ConfigData.DefaultParamsList.Find(delegate(ParamInfo item) { return item.Name == name; });
                //DataRow[] unitRows = ConnectionLib.Connection.Config.Tables["Ĭ�ϵ�λ"].Select(string.Format("���� = '{0}'", name));
                //if (unitRows.Length != 0)
                //{
                //    column.ExtendedProperties.Add(hammergo.MyControls.Helper.unitFeildName, unitRows[0]["��λ"]);
                //    column.ExtendedProperties.Add(hammergo.MyControls.Helper.preciseFeildName, unitRows[0]["����"]);

                //}

                if (paramInfo != null)
                {
                    column.ExtendedProperties.Add(hammergo.ExportLib.ExcelExportMessureExport.unitFeildName,paramInfo.UnitSymbol);
                    column.ExtendedProperties.Add(hammergo.ExportLib.ExcelExportMessureExport.preciseFeildName, paramInfo.Precision);

                }
                table.Columns.Add(column);
            }

            table.Columns.Add(PubConstant.remarkColumnName, typeof(string));

            table.PrimaryKey = new DataColumn[] { table.Columns[0] };

            return table;
        }



        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Text = e.ProgressPercentage.ToString();
            lblInfo.Text = e.UserState.ToString();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            btnOut.Enabled =  true;
            lblInfo.Text = e.Result.ToString();
        }


        private int highestPercentageReached = 0;

        private double blurHours = 0;

        private DateTime selTime;

        private void btnOut_Click(object sender, EventArgs e)
        {

            try
            {
                if (c1DateEdit1.EditValue ==null)
                {
                    throw new Exception("��ѡ��ʱ��");

                }
                selTime =c1DateEdit1.DateTime;

                blurHours =(double) calcEdit1.Value;

                btnOut.Enabled = false;
                // Reset the variable for percentage tracking.
                highestPercentageReached = 0;
                progressBar1.Text = highestPercentageReached.ToString();

                List<string> applist = new List<string>(appSelector1.lbcSelectedApps.Items.Count);
                // Start the asynchronous operation.
                foreach (string item in appSelector1.lbcSelectedApps.Items)
                {
                    applist.Add(item);
                }

                backgroundWorker1.RunWorkerAsync(applist);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "����!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                btnOut.Enabled =true;
            }
        }



        /// <summary>
        /// ���Ҫ���������������ݵ���
        /// </summary>
        /// <param name="row">��Ҫ��������</param>
        /// <param name="appName">��������</param>
        /// <param name="paNameList">�����������������б�</param>
        /// <param name="time">���ݵ�ʱ��</param>
        /// <param name="blurHours">ģ��Сʱ��</param>
        private  void FillMessureValueRow(DataRow row, string appName, DateTime time, double blurHours)
        {
            row.BeginEdit();
            row["�����"] = appName;

            AppIntegratedInfo appInfo = AppIntegratedInfo.getAppInfoNearTime(appName, time, blurHours / 24.0);

            

            if (appInfo!=null)
            {
                DateTime rightTime = appInfo.MessureValues[0].Date.Value;

                row[PubConstant.timeColumnName] = rightTime;

                if(appInfo.Remarks.Count>0)
                {
                    row[PubConstant.remarkColumnName]=appInfo.Remarks[0].RemarkText;
                }

                foreach (MessureParam mp in appInfo.MessureParams)
                {
                    MessureValue mv = appInfo.MessureValues.Find(delegate(MessureValue item) { return item.MessureParamID == mp.MessureParamID; });
                    if (mv != null)
                    {
                        if (mv.Val != null)
                        {
                            row[mp.ParamName] = mv.Val;
                        }
                    }
                }

               
             


                row.EndEdit();
            }


            return;

        }




        #region ICustomDispose ��Ա

        public void CustomDispose()
        {
            if (exporter != null)
            {
                exporter.QuitExcel();
            }
        }

        #endregion

        #region IShowAppData ��Ա

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
