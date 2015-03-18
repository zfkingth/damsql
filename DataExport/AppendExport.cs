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
using System.Collections;
using hammergo.Model;

namespace hammergo.DataExport
{
    public partial class AppendExport : DevExpress.XtraEditors.XtraUserControl
    {
        public AppendExport()
        {
            InitializeComponent();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            //BackgroundWorker worker = sender as BackgroundWorker;

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
                XtraMessageBox.Show(ex.Message, "����!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

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

            FileInfo[] datFiles;//dat�ļ�����


            FileInfo[] txtFiles;//txt�ļ�����

            DirectoryInfo dir = new DirectoryInfo(directory);
            datFiles = dir.GetFiles("*.dat");
            txtFiles = dir.GetFiles("*.txt");
            if (datFiles.Length + txtFiles.Length == 0)
            {

                return;
            }

            FileInfo exInfo = null;

            try
            {
                foreach (FileInfo info in txtFiles)
                {
                    exInfo = info;

                    outPutTxt(info);
                    backgroundWorker1.ReportProgress(0, "�������ݵ�: " + exInfo.FullName);
                }



                foreach (FileInfo info in datFiles)
                {
                    exInfo = info;

                    outPutDat(info);
                    backgroundWorker1.ReportProgress(0, "�������ݵ�: " + exInfo.FullName);
                }

            }
            catch (Exception ex)
            {
                Utility.Utility.log(ex);
                if (exInfo != null)
                    throw new Exception(String.Format("{0}\n{1}", exInfo.FullName, ex.Message));
                else
                    throw new Exception(ex.Message);

            }
        }


        const string blankConst = "    ";
        private void outPutDat(FileInfo info)
        {
            string firstline;
            string id = getIDFromFile(info, out firstline);


            if (appBLL.ExistsBy_AppName(id) == false)
                return;

            var tempApp = appBLL.GetModelBy_AppName(id);
            var type = typeBLL.GetModelBy_ApparatusTypeID(tempApp.AppTypeID.Value);

            bool appendTime = true;
            if (type.TypeName == "��ѹ��")
            {
                appendTime = false;
            }



            AppIntegratedInfo appInfo = new AppIntegratedInfo(id, 0, null, null);
            appInfo.MessureParams.Sort(new MessureDisplayComparer());

            SortedList<DateTime, object> vlist = new SortedList<DateTime, object>(20);
            //��˳��ʱ����ӵ�vlist��
            foreach (MessureValue mv in appInfo.MessureValues)
            {

                DateTime time = mv.Date.Value;
                if (vlist.ContainsKey(time) == false)
                {
                    vlist.Add(time, null);
                }
            }

            using (StreamWriter sw = new StreamWriter(info.FullName, false))
            {
                sw.WriteLine(firstline);

                for (int i = 0; i < vlist.Count; i++)
                {

                    DateTime time = vlist.Keys[i];


                    string outputString = "";

                    if (appendTime)
                    {
                        outputString = time.ToString(string.Format("yyyyMMdd{0}HHmmss", " "));
                    }
                    else
                    {

                        outputString = time.ToString("yyyyMMdd");
                    }

                    outputString = outputString.Remove(0, 2);


                    foreach (MessureParam mp in appInfo.MessureParams)
                    {

                        //DataRow[] rows = table.Select(string.Format("ʱ��=#{0}#", time.ToString(customString)), "��� asc");

                        MessureValue mv = appInfo.MessureValues.Find(delegate(MessureValue item) { return item.Date.Value == time && item.MessureParamID == mp.MessureParamID; });

                        outputString += blankConst;

                        string valString = " ";
                        if (mv != null)
                        {
                            object val = mv.Val;

                            if (val is double)
                            {
                                if (Utility.Utility.isErrorValue((double)val))
                                {
                                    valString = val.ToString();
                                }
                                else
                                {
                                    int precision = defaultPrecision;

                                    byte? temp = mp.PrecisionNum as byte?;
                                    if (temp.HasValue && temp.Value >= 0)
                                    {
                                        precision = temp.Value;
                                    }

                                    valString = Utility.Utility.round((double)val, precision).ToString(string.Format("f{0}", precision));


                                }


                            }

                        }
                        outputString += valString;



                    }

                    sw.WriteLine(outputString);
                    sw.Flush();



                }
                sw.Close();
            }
        }

        /// <summary>
        /// ��ȡ�ļ����һ��
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>

        private static string getLastLine(string path)
        {

            using (StreamReader sr = new StreamReader(path))
            {
                string current = "", pre = "";
                while ((current = sr.ReadLine()) != null && current.Length != 0)
                {
                    pre = current;
                }
                sr.Close();
                return pre;
            }

        }





        hammergo.BLL.ApparatusBLL appBLL;
        private void outPutTxt(FileInfo info)
        {
            string firstline;
            string id = getIDFromFile(info, out firstline);


            if (appBLL.ExistsBy_AppName(id) == false)
                return;

            var tempApp = appBLL.GetModelBy_AppName(id);
            var type = typeBLL.GetModelBy_ApparatusTypeID(tempApp.AppTypeID.Value);

            bool appendTime = true;
            if (type.TypeName == "��ѹ��")
            {
                appendTime = false;
            }

            //�����ݿ��в�ѯ

            //�����

            AppIntegratedInfo appInfo = new AppIntegratedInfo(id, 0, null, null);

            //����

            appInfo.CalcParams.Sort(new CalculdateDisplayComparer());


            SortedList<DateTime, object> vlist = new SortedList<DateTime, object>(20);
            //��˳��ʱ����ӵ�vlist��
            foreach (CalculateValue cv in appInfo.CalcValues)
            {

                DateTime time = cv.Date.Value;
                if (vlist.ContainsKey(time) == false)
                {
                    vlist.Add(time, null);
                }
            }


            using (StreamWriter sw = new StreamWriter(info.FullName, false))
            {

                sw.WriteLine(firstline);
                for (int i = 0; i < vlist.Count; i++)
                {
                    DateTime time = vlist.Keys[i];


                    string outputString = "";

                    if (appendTime)
                    {
                        outputString = time.ToString(string.Format("yyyyMMdd{0}HHmmss", " "));
                    }
                    else
                    {

                        outputString = time.ToString("yyyyMMdd");
                    }
                    outputString = outputString.Remove(0, 2);



                    //DataRow[] rows = table.Select(string.Format("ʱ��=#{0}#", time.ToString(customString)), "��� asc");

                    foreach (CalculateParam cp in appInfo.CalcParams)
                    {

                        CalculateValue cv = appInfo.CalcValues.Find(delegate(CalculateValue item) { return item.Date.Value == time && item.CalculateParamID == cp.CalculateParamID; });


                        outputString += blankConst;

                        string valString = " ";
                        if (cv != null)
                        {
                            object val = cv.Val;


                            if (val is double)
                            {
                                if (Utility.Utility.isErrorValue((double)val))
                                {
                                    valString = val.ToString();
                                }
                                else
                                {
                                    int precision = defaultPrecision;

                                    byte? temp = cp.PrecisionNum as byte?;
                                    if (temp.HasValue && temp.Value >= 0)
                                    {
                                        precision = temp.Value;
                                    }

                                    valString = Utility.Utility.round((double)val, precision).ToString(string.Format("f{0}", precision));


                                }


                            }

                        }
                        outputString += valString;


                    }

                    sw.WriteLine(outputString);
                    sw.Flush();



                }
                sw.Close();
            }
        }

        const int defaultPrecision = 2;

        private static int getС��λ��(string numString)
        {
            int index = numString.IndexOf('.');
            if (index == -1)
                return 0;

            return numString.Length - 1 - index;
        }

        private static string getIDFromFile(FileInfo info, out  string firstline)
        {

            using (StreamReader sr = new StreamReader(info.FullName))
            {
                firstline = sr.ReadLine().Trim();
                string[] ss = firstline.Split(new char[] { ' ', '\t' });
                sr.Close();
                return ss[0];
            }

        }





        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            lblInfo.Text = e.UserState.ToString();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lblInfo.Text = "�������";
            btnOut.Enabled = true;
            btnSel.Enabled = true;
        }

        string fullPath = "";
        private void btnSel_Click(object sender, EventArgs e)
        {
            if (folderImporter.ShowDialog(this) == DialogResult.OK)
            {
                fullPath = folderImporter.SelectedPath;

                btnOut.Enabled = true;

                lblInfo.Text = "·��: " + fullPath;
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
                if (RBSingle.SelectedIndex == 0)
                {

                    backgroundWorker1.RunWorkerAsync(fullPath);

                }
                else
                {

                    backgroundWorker1.RunWorkerAsync(new DirectoryInfo(fullPath));


                }
                lblInfo.Text = "�����ɹ�!";
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "����!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                lblInfo.Text = ex.Message;
                btnOut.Enabled = true;
                btnSel.Enabled = true;
            }
        }

        hammergo.BLL.ApparatusTypeBLL typeBLL;
        private void AppendExport_Load(object sender, EventArgs e)
        {
            appBLL = new hammergo.BLL.ApparatusBLL();
            typeBLL = new BLL.ApparatusTypeBLL();
        }




    }
}
