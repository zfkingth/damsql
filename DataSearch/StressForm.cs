using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using hammergo.Utility;
using hammergo.Model;

namespace hammergo.DataSearch
{
    public partial class StressForm : DevExpress.XtraEditors.XtraUserControl, hammergo.Utility.IShowAppData
    {
        public event EventHandler<hammergo.Utility.AppSearchEventArgs> ShowDataEvent;
        public StressForm()
        {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {
                handleyingli();

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(this, ex.Message, "����!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }

        }

        private void formatGridView(DevExpress.XtraGrid.Views.Grid.GridView gv)
        {

            //foreach (DevExpress.XtraGrid.Columns.GridColumn column in gv.Columns)
            //{


            //    if (column.Caption =="ʱ��")
            //    {
            //        column.Width = TextRenderer.MeasureText("2009-12-22 00:00", column.AppearanceHeader.Font).Width + 2;

            //    }
            //    else
            //    {
            //       // column.Width = TextRenderer.MeasureText(column.Caption, column.AppearanceHeader.Font).Width + 10;
            //    }


            //}
            gv.BestFitColumns();
        }


        private void handleyingli()
        {
            double E = 0.0, jianParam = 0.0;


            fgrid.DataSource = null;



            try
            {
                E = double.Parse(textBoxE.Text.Trim());
            }
            catch (Exception)
            {
                throw new Exception("����ģ�����ݴ���!");


            }

            try
            {
                jianParam = double.Parse(textBoxJian.Text.Trim());

            }
            catch (Exception)
            {
                throw new Exception("��Ӧ��ϵ������!");


            }

            try
            {
                tbDateFormat.Text = tbDateFormat.Text.Trim();
                System.DateTime.Now.ToString(tbDateFormat.Text);
            }
            catch (Exception)
            {
                throw new Exception("ʱ���ʽ����ȷ!");


            }

            string nameYingbian = tbyingbian.Text.Trim();
            string nameTemprature = tbTemperature.Text.Trim();




            //Ӧ��Ƹ���

            int numOfApp = 5;

            //��������Ƿ���5��

            if (appSelector1.lbcSelectedApps.Items.Count != numOfApp)
            {
                throw new Exception("Ӧ��Ƶĸ�������Ϊ5,������ѡ��!");

            }

            AppIntegratedInfo[] appInfoArray = new AppIntegratedInfo[numOfApp + 1];//Ϊ��ʹ���ݵ�������1��ʼ,�����Ķ�

            System.Collections.SortedList dateList = new System.Collections.SortedList(new System.Collections.CaseInsensitiveComparer(), 500);



            //�����ݿ��ж�ȡ��������

            //			int degree=0;//������������
            //			int indexMaxApp=1;//ӵ������ε�����������

            for (int i = 0; i < numOfApp; i++)
            {
                //����ȡ����
                //DataTable tempTable = Utility.get�ɹ�(lvTarget.Items[i].SubItems[0].Text, 0, null, null, true).Tables["���"];
                AppIntegratedInfo appInfo = new AppIntegratedInfo(appSelector1.lbcSelectedApps.Items[i].ToString(), 0, null, null);


                appInfoArray[i + 1] = appInfo;

                //foreach (DataRow row in tempTable.Rows)
                //{
                //    DateTime t = (DateTime)row["ʱ��"];
                //    if (dateList.ContainsKey(t) == false)
                //    {
                //        dateList.Add(t, null);

                //    }
                //}

                foreach (CalculateValue cv in appInfo.CalcValues)
                {
                    DateTime t = cv.Date.Value;
                    if (dateList.ContainsKey(t) == false)
                    {
                        dateList.Add(t, null);

                    }
                }
            }




            double[,] data = new double[1, numOfApp + 1];//Ӧ������
            double[,] tData = new double[1, numOfApp + 1];//ƽ���¶�����

            int[][] bad = new int[1][];

            for (int i = 0; i < bad.GetLength(0); i++)
            {
                bad[i] = new int[numOfApp + 1];

                for (int j = 0; j < bad[i].Length; j++)
                {
                    bad[i][j] = 0;
                }

            }

            //ֻ�ǰ�ֵ���ݽ�ȥ���Լ���������
            Stress.SimpleStressCalc simple = new Stress.SimpleStressCalc(E, jianParam, data, bad, 1, tData);


            DataTable resultTable = createTable( appInfoArray);



            int rowIndex = 0;
            for (int d = 0; d < dateList.Keys.Count; d++)
            {
                DateTime date = (DateTime)dateList.GetKey(d);

                //���bad�������
                for (int k = 0; k < numOfApp + 1; k++)
                {
                    bad[0][k] = 0;
                }

                for (int k = 0; k < data.GetLength(1); k++)
                {
                    data[0, k] = 0;
                    tData[0, k] = 0;
                }
                //�����������������



                //����Ӧ�������5������
                for (int direction = 1; direction < appInfoArray.Length; direction++)//��1��5
                {
                    AppIntegratedInfo appInfo = appInfoArray[direction];
                    List<CalculateValue> calcValues = appInfo.CalcValues.FindAll(delegate(CalculateValue item) { return item.Date == date; });

                    //DataRow row = appInfoArray[direction].Rows.Find(new object[] { date });
                    if (calcValues != null && calcValues.Count != 0)
                    {
                        CalculateParam cp = appInfo.CalcParams.Find(delegate(CalculateParam item) { return item.ParamName == nameYingbian; });
                        if (cp == null)
                        {
                            throw new Exception(nameYingbian + "��Ӧ���в�����!");

                        }
                        else
                        {
                            //����Ӧ����һ��

                            CalculateValue cv = calcValues.Find(delegate(CalculateValue item) { return item.CalculateParamID == cp.CalculateParamID; });


                            object val = cv.Val;// row[nameYingbian];

                            //������ֵ�Ƿ�����

                            if (val is double == false || Utility.Utility.isErrorValue((double)val))
                            {
                                //������ֵ


                                bad[0][direction] = -1;
                            }
                            else
                            {
                                data[0, direction] = (double)val;
                            }
                        }

                        cp = appInfo.CalcParams.Find(delegate(CalculateParam item) { return item.ParamName == nameTemprature; });
                        if (cp == null)
                        {
                            throw new Exception(nameTemprature + "��Ӧ���в�����!");

                        }
                        else
                        {
                            //�����¶���һ��
                            CalculateValue cv = calcValues.Find(delegate(CalculateValue item) { return item.CalculateParamID == cp.CalculateParamID; });

                            object val = cv.Val;// row[nameTemprature];

                            //������ֵ�Ƿ�����

                            if (val is double == false || Utility.Utility.isErrorValue((double)val))
                            {
                                //������ֵ


                                tData[0, direction] = double.NaN;
                            }
                            else
                            {
                                tData[0, direction] = (double)val;
                            }
                        }




                    }
                    else
                    {
                        bad[0][direction] = -1;

                        //���¶�����

                        tData[0, direction] = double.NaN;

                    }



                }

                //����Ӧ�����ݵĸ���
                double[,] yinbianData = data.Clone() as double[,];

                //�����Ѿ���ȡ����ʼ����



                double[,] exyz = simple.handle();

                //data������ƽ����Ӧ��

                if (exyz != null)
                {
                    DataRow drow = resultTable.NewRow();
                    resultTable.Rows.Add(drow);

                    drow["ʱ��"] = date.ToString(tbDateFormat.Text);
                    if (double.IsNaN(exyz[0, 1]) == false)
                    {
                        drow["Y"] = Utility.Utility.round(exyz[0, 1], 2);
                    }
                    else
                    {
                        drow["Y"] = double.NaN;// "/";

                    }
                    drow["X"] = Utility.Utility.round(exyz[0, 2], 2);
                    drow["Z"] = Utility.Utility.round(exyz[0, 3], 2);
                    drow["Yxz"] = Utility.Utility.round(exyz[0, 4], 2);

                    drow["dz"] = Utility.Utility.round(exyz[0, 0], 2);

                    //ƽ���¶���
                    if (double.IsNaN(exyz[0, 5]) == false)
                    {
                        drow["T"] = Utility.Utility.round(exyz[0, 5], 1).ToString("f1");
                    }
                    else
                    {
                        drow["T"] = "/";

                    }

                    //�г�ԭʼ��Ӧ���ƽ����Ӧ��
                    drow["Y1"] = Utility.Utility.round(yinbianData[0, 1], 2);
                    drow["Y2"] = Utility.Utility.round(yinbianData[0, 2], 2);
                    drow["Y3"] = Utility.Utility.round(yinbianData[0, 3], 2);
                    drow["Y4"] = Utility.Utility.round(yinbianData[0, 4], 2);
                    drow["Y5"] = Utility.Utility.round(yinbianData[0, 5], 2);


                    //ƽ���Ӧ��
                    drow["PY1"] = Utility.Utility.round(data[0, 1], 2);
                    drow["PY2"] = Utility.Utility.round(data[0, 2], 2);
                    drow["PY3"] = Utility.Utility.round(data[0, 3], 2);
                    drow["PY4"] = Utility.Utility.round(data[0, 4], 2);


                    rowIndex++;



                }



            }

            resultTable.AcceptChanges();

            fgrid.DataSource = resultTable;
            DevExpress.XtraGrid.Views.Grid.GridView gv = fgrid.MainView as DevExpress.XtraGrid.Views.Grid.GridView;
            formatGridView(gv);

            //����grid��ʾΪ����һ��

            gv.FocusedRowHandle = rowIndex - 1;

        }

        private DataTable createTable(AppIntegratedInfo[] appInfoArray)
        {
            DataTable table = new DataTable();
            DataColumn column = new DataColumn("ʱ��", typeof(string));
            column.Caption = "ʱ��";
            table.Columns.Add(column); 

            column = new DataColumn("dz", typeof(double));
            column.Caption = "���";
            table.Columns.Add(column);

            column = new DataColumn("X", typeof(double));
            column.Caption = "ˮ����Ӧ��X";
            table.Columns.Add(column);

            column = new DataColumn("Y", typeof(double));
            column.Caption = "������Ӧ��Y";
            table.Columns.Add(column);

            column = new DataColumn("Z", typeof(double));
            column.Caption = "Ǧֱ��Ӧ��Z";
            table.Columns.Add(column);

            column = new DataColumn("Yxz", typeof(double));
            column.Caption = "��Ӧ��Yxz";
            table.Columns.Add(column);

            column = new DataColumn("T", typeof(double));
            column.Caption = "ƽ���¶�T";
            table.Columns.Add(column);

            //�������5��Ӧ���
            column = new DataColumn("Y1", typeof(double));
            column.Caption = "��XOZ 0�㷽��Ӧ��Ӧ��"+appInfoArray[1].appName;
            table.Columns.Add(column);

            column = new DataColumn("Y2", typeof(double));
            column.Caption = "��XOZ 45�㷽��Ӧ��Ӧ��"+appInfoArray[2].appName;
            table.Columns.Add(column);

            column = new DataColumn("Y3", typeof(double));
            column.Caption = "��XOZ 90�㷽��Ӧ��Ӧ��"+appInfoArray[3].appName;
            table.Columns.Add(column);

            column = new DataColumn("Y4", typeof(double));
            column.Caption = "��XOZ 145�㷽��Ӧ��Ӧ��"+appInfoArray[4].appName;
            table.Columns.Add(column);

            column = new DataColumn("Y5", typeof(double));
            column.Caption = "��Y ����Ӧ��Ӧ��"+appInfoArray[5].appName;
            table.Columns.Add(column);

            //ƽ���Ӧ��Ӧ��
            column = new DataColumn("PY1", typeof(double));
             column.Caption = "ƽ����XOZ 0��";
            table.Columns.Add(column);

            column = new DataColumn("PY2", typeof(double));
             column.Caption = "ƽ����XOZ 45��";
            table.Columns.Add(column);

            column = new DataColumn("PY3", typeof(double));
             column.Caption = "ƽ����XOZ 90��";
            table.Columns.Add(column);

            column = new DataColumn("PY4", typeof(double));
             column.Caption = "ƽ����XOZ 145��";
            table.Columns.Add(column);


            table.AcceptChanges();

            return table;
        }

        

        private void appSelector1_ShowDataEvent(object sender,hammergo.Utility.AppSearchEventArgs e)
        {
            if (ShowDataEvent != null)
            {
                ShowDataEvent(sender,e);
            }
        }

        private void ����Item_Click(object sender, EventArgs e)
        {
            //(fgrid.MainView as DevExpress.XtraGrid.Views.Grid.GridView).CopyToClipboard();

            Utility.Utility.copyGridSelection(fgrid.MainView as DevExpress.XtraGrid.Views.Grid.GridView);
        }

        ExportLib.GridViewExport exporter = null;
        private void StressForm_Load(object sender, EventArgs e)
        {
            appSelector1.initial();

            exporter = new ExportLib.GridViewExport(gridView2, "");
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string fileName = "";
            foreach(var obj in appSelector1.lbcSelectedApps.Items)
            {
                fileName += obj.ToString()+"_";

            }
            exporter.name = fileName;
            exporter.sbExportToXLS_Click(sender, e);
        }

        private void gridView2_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.CellValue is double && double.IsNaN((double)e.CellValue ))
            {
                e.DisplayText = "/";
            }
        }
    }
}
