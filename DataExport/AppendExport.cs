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

        private void outPutDat(FileInfo info)
        {
            string id = getIDFromFile(info);


            if (appBLL.ExistsBy_AppName(id) == false)
                return;

            string line = getLastLine(info.FullName);

            string[] ss = splitString(line, ' ');

            ArrayList blankList = getFilledBankString(line);


            ArrayList list = parseDateString(ss);

            DateTime date = (DateTime)list[0];
            bool tag = (bool)list[1];

            AppIntegratedInfo appInfo = new AppIntegratedInfo(id, 0, date.AddSeconds(1), null);
            appInfo.MessureParams.Sort(new MessureDisplayComparer());

            SortedList<DateTime, object> vlist = new SortedList<DateTime, object>(20);
            //��˳��ʱ�����ӵ�vlist��
            foreach (MessureValue mv in appInfo.MessureValues)
            {

                DateTime time = mv.Date.Value;
                if (vlist.ContainsKey(time) == false)
                {
                    vlist.Add(time,null);
                }
            }

            using (StreamWriter sw = new StreamWriter(info.FullName, true))
            {

                for (int i = 0; i < vlist.Count; i++)
                {
                    int blandPos = 0;
                    DateTime time = vlist.Keys[i];


                    string outputString = "";
                    if (tag == false)//�ڶ�������ҲΪʱ��
                    {
                        outputString = time.ToString(string.Format("yyyyMMdd{0}HHmmss", blankList[blandPos++]));



                    }
                    else
                    {
                        outputString = time.ToString("yyyyMMdd");

                    }

                    outputString = outputString.Remove(0, 2);


                    foreach (MessureParam mp in appInfo.MessureParams)
                    {

                      

                        //DataRow[] rows = table.Select(string.Format("ʱ��=#{0}#", time.ToString(customString)), "��� asc");

                        MessureValue mv = appInfo.MessureValues.Find(delegate(MessureValue item) { return item.Date.Value == time&&item.MessureParamID==mp.MessureParamID; });



                            outputString += blankList[blandPos++];

                            int bits = getС��λ��(ss[blandPos]);
                            //object val = row["ֵ"];
                            object val = mv.Val;

                            string valString;
                            if (val is double)
                            {
                                if (bits >= 0)
                                    valString = Utility.Utility.round((double)val, bits).ToString(string.Format("f{0}", bits));
                                else
                                    valString = val.ToString();

                                outputString += valString;
                            }


                        
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

        private static  string getLastLine(string path)
        {

            using (StreamReader sr = new StreamReader(path))
            {
                string current = "", pre = "";
                while ((current = sr.ReadLine()) != null)
                {
                    pre = current;
                }
                sr.Close();
                return pre;
            }

        }


        private static ArrayList getFilledBankString(string s)
        {

            int pre = -1;

            ArrayList list = new ArrayList(5);
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == ' ')
                {

                    if (pre < 0)
                        pre = i;
                }
                else
                {

                    if (pre >= 0)
                    {

                        list.Add(s.Substring(pre, i - pre));

                        pre = -1;
                    }
                }
            }

            return list;
        }

        /// <summary>
        /// ��һ������Ϊʱ�䣬�ڶ�������Ϊ��־
        /// </summary>
        /// <param name="ss"></param>
        /// <returns></returns>
        private  ArrayList parseDateString(string[] ss)
        {
            string dateString = ss[0];

            //9����
            if (dateString.StartsWith("9"))
            {
                dateString = "19" + dateString;
            }
            else
            {
                dateString = "20" + dateString;
            }


            string s1 = ss[1];

            bool tag;//��ʾ�ڶ���Ԫ�ز�Ϊʱ��
            if (isTimeString(s1))
                tag = false;
            else
                tag = true;

            DateTime date;
            if (tag == false)//�ڶ�������ҲΪʱ��
            {
                dateString = dateString + s1;
                date = DateTime.ParseExact(dateString, "yyyyMMddHHmmss", null);

            }
            else
            {
                date = DateTime.ParseExact(dateString, "yyyyMMdd", null);

                date = new DateTime(date.Ticks + TimeSpan.TicksPerMinute * 60 * 23);

            }

            ArrayList list = new ArrayList(2);
            list.Add(date);
            list.Add(tag);

            return list;
        }

        /// <summary>
        /// �ж��ַ����Ƿ���һ��ʱ���ʽHHmmss
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>

        private static  bool isTimeString(string s)
        {
            if (s.Length != 6) return false;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] < '0' || s[i] > '9')
                    return false;
            }

            //�ж�Сʱ
            int a = (s[0] - '0') * 10;
            int b = s[1] - '0';
            if (a + b > 24)
                return false;
            //�жϷ���
            a = (s[2] - '0') * 10;
            b = s[3] - '0';
            if (a + b > 60)
                return false;
            //�ж���
            a = (s[4] - '0') * 10;
            b = s[5] - '0';
            if (a + b > 60)
                return false;

            return true;

        }



        hammergo.BLL.ApparatusBLL appBLL;
        private void outPutTxt(FileInfo info)
        {
            string id = getIDFromFile(info);

           
            if (appBLL.ExistsBy_AppName(id)==false)
                return;

            

            string line = getLastLine(info.FullName);

            string[] ss = splitString(line, ' ');

            ArrayList blankList = getFilledBankString(line);


            ArrayList list = parseDateString(ss);

            DateTime date = (DateTime)list[0];
            bool tag = (bool)list[1];


            //�����ݿ��в�ѯ

            //�����

            AppIntegratedInfo appInfo = new AppIntegratedInfo(id, 0, date.AddSeconds(1), null);

            //����

             appInfo.CalcParams.Sort(new CalculdateDisplayComparer());
            

            SortedList<DateTime, object> vlist = new SortedList<DateTime, object>(20);
            //��˳��ʱ�����ӵ�vlist��
            foreach( CalculateValue cv in  appInfo.CalcValues)
            {

                DateTime time = cv.Date.Value;
                if (vlist.ContainsKey(time) == false)
                {
                    vlist.Add(time,null);
                }
            }


            using (StreamWriter sw = new StreamWriter(info.FullName, true))
            {

                for (int i = 0; i < vlist.Count; i++)
                {
                    int blandPos = 0;
                    DateTime time = vlist.Keys[i];


                    string outputString = "";
                    if (tag == false)//�ڶ�������ҲΪʱ��
                    {
                        outputString = time.ToString(string.Format("yyyyMMdd{0}HHmmss", blankList[blandPos++]));



                    }
                    else
                    {
                        outputString = time.ToString("yyyyMMdd");

                    }

                    outputString = outputString.Remove(0, 2);



                    //DataRow[] rows = table.Select(string.Format("ʱ��=#{0}#", time.ToString(customString)), "��� asc");

                    foreach (CalculateParam cp in appInfo.CalcParams)
                    {

                        CalculateValue cv = appInfo.CalcValues.Find(delegate(CalculateValue item) { return item.Date.Value == time&&item.CalculateParamID==cp.CalculateParamID; });


                            outputString += blankList[blandPos++];

                            int bits = getС��λ��(ss[blandPos]);
                            //object val = row["ֵ"];
                            object val = cv.Val;

                            string valString;
                            if (val is double)
                            {
                                if (bits >= 0)
                                    valString = Utility.Utility.round((double)val, bits).ToString(string.Format("f{0}", bits));
                                else
                                    valString = val.ToString();

                                outputString += valString;
                            }


                        
                    }

                    sw.WriteLine(outputString);
                    sw.Flush();



                }
                sw.Close();
            }
        }

        private static  int getС��λ��(string numString)
        {
            int index = numString.IndexOf('.');
            if (index == -1)
                return 0;

            return numString.Length - 1 - index;
        }

        private static string getIDFromFile(FileInfo info)
        {

            using (StreamReader sr = new StreamReader(info.FullName))
            {
                string line = sr.ReadLine().Trim();
                string[] ss = line.Split(new char[] { ' ', '\t' });
                sr.Close();
                return ss[0];
            }

        }

        /// <summary>
        /// ����ָ���ָ��ַ���
        /// </summary>
        /// <param name="s"></param>
        /// <param name="splitChar"></param>
        /// <returns></returns>
        private static  string[] splitString(string s, char splitChar)
        {
            s = s.Trim();
            ArrayList list = new ArrayList(10);
            char[] chars = new char[s.Length];
            int index = 0;
            bool previous = false;//��ʾǰһ���ַ����ǿո�

            for (int i = 0; i < s.Length; i++)
            {

                if (s[i] != splitChar)
                {
                    chars[index++] = s[i];
                    previous = false;
                }
                else if (previous == false)
                {
                    list.Add(new string(chars, 0, index));
                    index = 0;
                    previous = true;

                }
            }

            //�����ַ�����ĩβ
            if (previous == false)
                list.Add(new string(chars, 0, index));




            string[] strings = new string[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                strings[i] = (string)list[i];
            }
            return strings;
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

        private void AppendExport_Load(object sender, EventArgs e)
        {
            appBLL = new hammergo.BLL.ApparatusBLL();
        }
    }
}