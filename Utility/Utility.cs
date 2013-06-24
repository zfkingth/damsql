using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using hammergo.Model;
using hammergo.GlobalConfig;

namespace hammergo.Utility
{
   

   public  class Utility
    {

       /// <summary>
       /// ����������ֵ
       /// </summary>
       /// <param name="v"></param>
       /// <param name="precision"></param>
       /// <returns></returns>
       public static double round(double v, int precision)
       {
           if (precision > 0&&double.IsNaN(v)==false)
           {
               double tmpv = v;
               if (v < 0)
                   tmpv = -v;
               double fMul = Math.Pow(10, precision);
               double fTmp = tmpv * fMul;
               int iTmp = (int)fTmp;

               double r = 0.0;
               if (fTmp - iTmp >= 0.5)
               {
                   r = (iTmp + 1) / fMul;
               }
               else
               {
                   r = iTmp / fMul;
               }

               return v > 0 ? r : -r;



           }

           return v;
       }

       //��¼�쳣��Ϣ
       public static void log(System.Exception ex)
       {
           try
           {
               System.Diagnostics.EventLog mylog = new System.Diagnostics.EventLog();

               const string source = "dam mis";

               if (!System.Diagnostics.EventLog.SourceExists(source))
               {
                   System.Diagnostics.EventLog.CreateEventSource(source, source);


               }

               mylog.Source = source;


               mylog.WriteEntry(String.Format("{0}\n{1}\n\n�йظ�����Ϣ,����ϵhammergo@163.com\n", ex.Message, ex.StackTrace), System.Diagnostics.EventLogEntryType.Warning);

           }
           catch (Exception nex)
           {
               DevExpress.XtraEditors.XtraMessageBox.Show(nex.Message, "д����־����", MessageBoxButtons.OK, MessageBoxIcon.Error);
           }
       }

       /// <summary>
       /// ȥ�����˵ĳɹ����ж������ɹ������Ƿ�һ�£���һ���׳��쳣
       /// </summary>
       /// <param name="listNames">����б�</param>
       /// <param name="paramNamelist">�յĳɹ����б�</param>
       /// <param name="filterVariable">���˵ĳɹ�������</param>
       /// <param name="appInfoList">�����ѯ��appInfo</param>
       public static void isSameAppResult(List<string> listNames, List<string> paramNamelist, string filterVariable, List<AppIntegratedInfo>appInfoList)
       {
         

           //�ж��Ƿ������ͬ�Ĳ���,�����������ƺ͸���
           //��Ҫ���п��ٵ��ж�
           bool initial = false;

        

           foreach (string appName in listNames)
           {
               int paramsCnt = 0;
               hammergo.Utility.AppIntegratedInfo appInfo = new hammergo.Utility.AppIntegratedInfo(appName, 1, null, null);

               appInfoList.Add(appInfo);

               foreach (CalculateParam cp in appInfo.CalcParams)
               {



                   if (initial == false)
                   {
                       //��ʼ��list
                       paramNamelist.Add(cp.ParamName);
                   }
                   else
                   {
                       if (cp.ParamName.Equals(filterVariable))
                       {
                           continue;//�ж���һ������
                       }

                       if (paramNamelist.Contains(cp.ParamName) == false)
                       {
                           throw new Exception(string.Format("���{0}�������������ݲ�һ�£��޷�����!", appName));

                       }

                       paramsCnt++;

                   }


               }

               if (initial == false)
               {
                   initial = true;
                   int searchIndex = paramNamelist.IndexOf(filterVariable);
                   if (searchIndex >= 0)
                   {
                       paramNamelist.RemoveAt(searchIndex);
                   }
               }
               else
               {
                   //һ�������Ĳ�����֤���
                   //�жϸ����Ƿ�һ��

                   if (paramsCnt != paramNamelist.Count)
                   {
                       throw new Exception(string.Format("���{0}�������������ݲ�һ�£��޷�����!", appName));

                   }


               }
           }
       }

       /// <summary>
       /// �����е�gridview������ѡ����Ҫ����
       /// </summary>
       /// <param name="obj"></param>
       /// <param name="gv"></param>
       public static void selectRow(object obj,DevExpress.XtraGrid.Views.Grid.GridView gv)
       {
           for (int i = 0; i < gv.RowCount; i++)
           {
               object item = gv.GetRow(i);
               if (item == obj)
               {
                   gv.ClearSelection();
                   gv.SelectRow(i);
                   gv.FocusedRowHandle = i;
                   break;
               }
           }
       }

       //input box

       public static string InputBox(string Caption, string Hint, string Default,char? passwordChar)
       {

           DevExpress.XtraEditors.XtraForm InputForm = new DevExpress.XtraEditors.XtraForm();
           InputForm.MinimizeBox = false;
           InputForm.MaximizeBox = false;
           InputForm.StartPosition = FormStartPosition.CenterScreen;
           InputForm.Width = 220;
           InputForm.Height = 150;
           //InputForm.Font.Name = "����";
           //InputForm.Font.Size = 10;

          

           InputForm.Text = Caption;
           DevExpress.XtraEditors.LabelControl lbl = new DevExpress.XtraEditors.LabelControl();
           lbl.Text = Hint;
           lbl.Left = 10;
           lbl.Top = 20;
           lbl.Parent = InputForm;
           lbl.AutoSize = true;

           System.Drawing.Size size= TextRenderer.MeasureText(Hint, lbl.Font);
           lbl.Size = new System.Drawing.Size(size.Width + 2, size.Height + 2);

           int newWidth = size.Width + 6;

           if(newWidth>InputForm.Width)
           InputForm.Width = newWidth;
           
           


           DevExpress.XtraEditors.TextEdit tb = new DevExpress.XtraEditors.TextEdit();
           tb.Left = 30;
           tb.Top = 45;
           tb.Width = 160;
           tb.Parent = InputForm;
           tb.Text = Default;
           tb.SelectAll();
           if (passwordChar!=null)
           {
               tb.Properties.PasswordChar = passwordChar.Value;
           }

           DevExpress.XtraEditors.SimpleButton btnok = new DevExpress.XtraEditors.SimpleButton();
           btnok.Left = 30;
           btnok.Top = 80;
           btnok.Parent = InputForm;
           btnok.Text = "ȷ��";
           InputForm.AcceptButton = btnok;//�س���Ӧ

           btnok.DialogResult = DialogResult.OK;
           DevExpress.XtraEditors.SimpleButton btncancal = new DevExpress.XtraEditors.SimpleButton();
           btncancal.Left = 120;
           btncancal.Top = 80;
           btncancal.Parent = InputForm;
           btncancal.Text = "ȡ��";
           btncancal.DialogResult = DialogResult.Cancel;
           try
           {
               if (InputForm.ShowDialog() == DialogResult.OK)
               {
                   return tb.Text;
               }
               else
               {
                   return null;
               }
           }
           finally
           {
               InputForm.Dispose();
           }

       }

       public static void copyGridSelection(DevExpress.XtraGrid.Views.Grid.GridView gv)
       {
              System.Text.StringBuilder strTemp = new System.Text.StringBuilder(2560); ; //string to be copied to the clipboard

            DevExpress.XtraGrid.Views.Base.GridCell[] cells = gv.GetSelectedCells();



            //��С�������������
            int visibleIndexMin = int.MaxValue;
            int visibleIndexMax = int.MinValue;

            foreach (DevExpress.XtraGrid.Views.Base.GridCell cell in cells)
            {
                int visibleIndex = cell.Column.VisibleIndex;

                if (visibleIndexMin > visibleIndex)
                {
                    visibleIndexMin = visibleIndex;
                }

                if (visibleIndexMax < visibleIndex)
                {
                    visibleIndexMax = visibleIndex;
                }
            }
            const string CellDelimiter = "\t";
            const string LineDelimiter = "\r\n";

            int cellIndex = 0;
            foreach (int selRowHandle in gv.GetSelectedRows())
            {
                //��
                for (int j = visibleIndexMin; j <= visibleIndexMax; j++)
                {
                    //��
                    if (cells[cellIndex].Column.VisibleIndex == j)
                    {
                        DevExpress.XtraGrid.Views.Base.GridCell cell = cells[cellIndex];
                        strTemp.Append(gv.GetRowCellDisplayText(selRowHandle,cell.Column));

                        
                        cellIndex++;
                    }

                    strTemp.Append(CellDelimiter);


                }

                // strTemp.Append(LineDelimiter);
                strTemp.Replace(CellDelimiter, LineDelimiter, strTemp.Length - 1, 1);
            }

            string outstring = strTemp.ToString();


            System.Windows.Forms.Clipboard.SetDataObject(outstring, true, 3, 300);
       }

       public static void copyItemInListBox(DevExpress.XtraEditors.ListBoxControl listBox)
       {
           System.Windows.Forms.Clipboard.SetDataObject("", true);


           System.Text.StringBuilder sb = new System.Text.StringBuilder(200);

           foreach (string name in listBox.SelectedItems)
           {
               sb.Append(name);
               sb.Append("\r\n");
           }

           string outstring = sb.ToString();
           System.Windows.Forms.Clipboard.SetDataObject(outstring, true);
           System.Windows.Forms.Clipboard.SetDataObject(outstring, true);
       }


       public static void pasteItemInListBox(DevExpress.XtraEditors.ListBoxControl listBox)
       {
           string clipString = (string)System.Windows.Forms.Clipboard.GetDataObject().GetData(typeof(string));

           if (clipString == null || clipString.Length == 0) return;


           string[] sns = clipString.Split(new char[] { '\n', '\r', '\t' });
           hammergo.BLL.ApparatusBLL appBLL = new hammergo.BLL.ApparatusBLL();

           for (int i = 0; i < sns.Length; i++)
           {
               string name = sns[i];
               if (appBLL.ExistsBy_AppName(name))
              {
                  addAppNameInListBox(name,listBox);
              }
           }

       }

       public static void addAppNameInListBox(string name, DevExpress.XtraEditors.ListBoxControl listBox)
       {
           for (int i = 0; i < listBox.Items.Count; i++)
           {
               if (name == listBox.Items[i].ToString())
               {
                   return;
               }
           }

           listBox.Items.Add(name);

       }



       public static bool isErrorValue(double v)
       {
           foreach (double vi in hammergo.GlobalConfig.PubConstant.ConfigData.ErrorValList)
           {
               if (vi == v)
                   return true;
           }

           return false;
       }


       public static void handlePasteInDateEdit(DevExpress.XtraEditors.DateEdit dateEdit)
       {
           string clipString = (string)System.Windows.Forms.Clipboard.GetDataObject().GetData(typeof(string));

           if (clipString == null || clipString.Length == 0)
           {
               dateEdit.EditValue = null;
           }
           else
           {
               try
               {

                   clipString = clipString.Trim();

                   DateTime date = DateTime.MinValue; ;
                   if (clipString.Length == PubConstant.customString.Length)
                   {
                       date = DateTime.ParseExact(clipString, PubConstant.customString, null);

                   }
                   else
                   {
                       date = DateTime.ParseExact(clipString.Split(' ')[0], PubConstant.shortString, null);
                   }

                  dateEdit.DateTime = date;

               }
               catch (Exception)
               {
               }
           }

       }


       public static void handlePasteInGridView(DevExpress.XtraGrid.Views.Grid.GridView gv)
       {
           string clipString = (string)System.Windows.Forms.Clipboard.GetDataObject().GetData(typeof(string));

           if (clipString == null || clipString.Length == 0)
           {
               gv.EditingValue = "";
           }
           else
           {
               try
               {

                   clipString = clipString.Trim();

                   DateTime date = DateTime.MinValue; ;
                   if (clipString.Length == PubConstant.customString.Length)
                   {
                       date = DateTime.ParseExact(clipString, PubConstant.customString, null);

                   }
                   else
                   {
                       date = DateTime.ParseExact(clipString.Split(' ')[0], PubConstant.shortString, null);
                   }

                   gv.EditingValue = date;

               }
               catch (Exception)
               {
               }
           }

       }

       /// <summary>
       /// ������ת��Ϊʱ��
       /// </summary>
       /// <param name="num"></param>
       /// <returns></returns>
       public static DateTime NumToDateTime(double num)
       {
           DateTime origin = new DateTime(1900, 1, 1, 0, 0, 0, 0);



           return origin.AddDays(num);

       }

       /// <summary>
       /// ��ʱ��ת��Ϊ����
       /// </summary>
       /// <param name="num"></param>
       /// <returns></returns>
       public static double DateTimeToNum(DateTime date)
       {

           DateTime origin = new DateTime(1900, 1, 1, 0, 0, 0, 0);

           return (date.Ticks - origin.Ticks) / TimeSpan.TicksPerDay;

       }



       public static object getNumbericString(byte precision)
       {
           string numberFormatString = "0";
           if (precision > 0)
           {
               numberFormatString += ".";//�ȼ�һ����
               for (int p = 0; p < precision; p++)
               {
                   numberFormatString += "0";
               }

           }

           return numberFormatString;
       }
   }


}
