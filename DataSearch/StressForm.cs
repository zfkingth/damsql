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
                XtraMessageBox.Show(this, ex.Message, "错误!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }

        }

        private void formatGridView(DevExpress.XtraGrid.Views.Grid.GridView gv)
        {

            //foreach (DevExpress.XtraGrid.Columns.GridColumn column in gv.Columns)
            //{


            //    if (column.Caption =="时间")
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
                throw new Exception("弹性模量数据错误!");


            }

            try
            {
                jianParam = double.Parse(textBoxJian.Text.Trim());

            }
            catch (Exception)
            {
                throw new Exception("剪应力系数错误!");


            }

            try
            {
                tbDateFormat.Text = tbDateFormat.Text.Trim();
                System.DateTime.Now.ToString(tbDateFormat.Text);
            }
            catch (Exception)
            {
                throw new Exception("时间格式不正确!");


            }

            string nameYingbian = tbyingbian.Text.Trim();
            string nameTemprature = tbTemperature.Text.Trim();




            //应变计个数

            int numOfApp = 5;

            //检查仪器是否有5个

            if (appSelector1.lbcSelectedApps.Items.Count != numOfApp)
            {
                throw new Exception("应变计的个数必须为5,请重新选择!");

            }

            AppIntegratedInfo[] appInfoArray = new AppIntegratedInfo[numOfApp + 1];//为了使数据的索引从1开始,方便阅读

            System.Collections.SortedList dateList = new System.Collections.SortedList(new System.Collections.CaseInsensitiveComparer(), 500);



            //从数据库中读取仪器数据

            //			int degree=0;//测量的最多次数
            //			int indexMaxApp=1;//拥有最多测次的仪器的索引

            for (int i = 0; i < numOfApp; i++)
            {
                //依次取数据
                //DataTable tempTable = Utility.get成果(lvTarget.Items[i].SubItems[0].Text, 0, null, null, true).Tables["组合"];
                AppIntegratedInfo appInfo = new AppIntegratedInfo(appSelector1.lbcSelectedApps.Items[i].ToString(), 0, null, null);


                appInfoArray[i + 1] = appInfo;

                //foreach (DataRow row in tempTable.Rows)
                //{
                //    DateTime t = (DateTime)row["时间"];
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




            double[,] data = new double[1, numOfApp + 1];//应力数据
            double[,] tData = new double[1, numOfApp + 1];//平均温度数据

            int[][] bad = new int[1][];

            for (int i = 0; i < bad.GetLength(0); i++)
            {
                bad[i] = new int[numOfApp + 1];

                for (int j = 0; j < bad[i].Length; j++)
                {
                    bad[i][j] = 0;
                }

            }

            //只是把值传递进去，以及创建对象
            Stress.SimpleStressCalc simple = new Stress.SimpleStressCalc(E, jianParam, data, bad, 1, tData);


            DataTable resultTable = createTable( appInfoArray);



            int rowIndex = 0;
            for (int d = 0; d < dateList.Keys.Count; d++)
            {
                DateTime date = (DateTime)dateList.GetKey(d);

                //清除bad里的数据
                for (int k = 0; k < numOfApp + 1; k++)
                {
                    bad[0][k] = 0;
                }

                for (int k = 0; k < data.GetLength(1); k++)
                {
                    data[0, k] = 0;
                    tData[0, k] = 0;
                }
                //查找其他表里的数据



                //代表应力计算的5个方向
                for (int direction = 1; direction < appInfoArray.Length; direction++)//从1到5
                {
                    AppIntegratedInfo appInfo = appInfoArray[direction];
                    List<CalculateValue> calcValues = appInfo.CalcValues.FindAll(delegate(CalculateValue item) { return item.Date == date; });

                    //DataRow row = appInfoArray[direction].Rows.Find(new object[] { date });
                    if (calcValues != null && calcValues.Count != 0)
                    {
                        CalculateParam cp = appInfo.CalcParams.Find(delegate(CalculateParam item) { return item.ParamName == nameYingbian; });
                        if (cp == null)
                        {
                            throw new Exception(nameYingbian + "对应的列不存在!");

                        }
                        else
                        {
                            //存在应变这一列

                            CalculateValue cv = calcValues.Find(delegate(CalculateValue item) { return item.CalculateParamID == cp.CalculateParamID; });


                            object val = cv.Val;// row[nameYingbian];

                            //检测这个值是否正常

                            if (val is double == false || Utility.Utility.isErrorValue((double)val))
                            {
                                //非正常值


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
                            throw new Exception(nameTemprature + "对应的列不存在!");

                        }
                        else
                        {
                            //存在温度这一列
                            CalculateValue cv = calcValues.Find(delegate(CalculateValue item) { return item.CalculateParamID == cp.CalculateParamID; });

                            object val = cv.Val;// row[nameTemprature];

                            //检测这个值是否正常

                            if (val is double == false || Utility.Utility.isErrorValue((double)val))
                            {
                                //非正常值


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

                        //无温度数据

                        tData[0, direction] = double.NaN;

                    }



                }

                //保持应力数据的副本
                double[,] yinbianData = data.Clone() as double[,];

                //数据已经获取，开始计算



                double[,] exyz = simple.handle();

                //data现在是平衡后的应变

                if (exyz != null)
                {
                    DataRow drow = resultTable.NewRow();
                    resultTable.Rows.Add(drow);

                    drow["时间"] = date.ToString(tbDateFormat.Text);
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

                    //平均温度列
                    if (double.IsNaN(exyz[0, 5]) == false)
                    {
                        drow["T"] = Utility.Utility.round(exyz[0, 5], 1).ToString("f1");
                    }
                    else
                    {
                        drow["T"] = "/";

                    }

                    //列出原始的应变和平衡后的应变
                    drow["Y1"] = Utility.Utility.round(yinbianData[0, 1], 2);
                    drow["Y2"] = Utility.Utility.round(yinbianData[0, 2], 2);
                    drow["Y3"] = Utility.Utility.round(yinbianData[0, 3], 2);
                    drow["Y4"] = Utility.Utility.round(yinbianData[0, 4], 2);
                    drow["Y5"] = Utility.Utility.round(yinbianData[0, 5], 2);


                    //平衡后应变
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

            //设置grid显示为最后的一行

            gv.FocusedRowHandle = rowIndex - 1;

        }

        private DataTable createTable(AppIntegratedInfo[] appInfoArray)
        {
            DataTable table = new DataTable();
            DataColumn column = new DataColumn("时间", typeof(string));
            column.Caption = "时间";
            table.Columns.Add(column); 

            column = new DataColumn("dz", typeof(double));
            column.Caption = "误差";
            table.Columns.Add(column);

            column = new DataColumn("X", typeof(double));
            column.Caption = "水流向应力X";
            table.Columns.Add(column);

            column = new DataColumn("Y", typeof(double));
            column.Caption = "坝轴向应力Y";
            table.Columns.Add(column);

            column = new DataColumn("Z", typeof(double));
            column.Caption = "铅直向应力Z";
            table.Columns.Add(column);

            column = new DataColumn("Yxz", typeof(double));
            column.Caption = "剪应力Yxz";
            table.Columns.Add(column);

            column = new DataColumn("T", typeof(double));
            column.Caption = "平均温度T";
            table.Columns.Add(column);

            //计算的是5向应变计
            column = new DataColumn("Y1", typeof(double));
            column.Caption = "∠XOZ 0°方向应力应变"+appInfoArray[1].appName;
            table.Columns.Add(column);

            column = new DataColumn("Y2", typeof(double));
            column.Caption = "∠XOZ 45°方向应力应变"+appInfoArray[2].appName;
            table.Columns.Add(column);

            column = new DataColumn("Y3", typeof(double));
            column.Caption = "∠XOZ 90°方向应力应变"+appInfoArray[3].appName;
            table.Columns.Add(column);

            column = new DataColumn("Y4", typeof(double));
            column.Caption = "∠XOZ 145°方向应力应变"+appInfoArray[4].appName;
            table.Columns.Add(column);

            column = new DataColumn("Y5", typeof(double));
            column.Caption = "∠Y 方向应力应变"+appInfoArray[5].appName;
            table.Columns.Add(column);

            //平衡后应力应变
            column = new DataColumn("PY1", typeof(double));
             column.Caption = "平衡后∠XOZ 0°";
            table.Columns.Add(column);

            column = new DataColumn("PY2", typeof(double));
             column.Caption = "平衡后∠XOZ 45°";
            table.Columns.Add(column);

            column = new DataColumn("PY3", typeof(double));
             column.Caption = "平衡后∠XOZ 90°";
            table.Columns.Add(column);

            column = new DataColumn("PY4", typeof(double));
             column.Caption = "平衡后∠XOZ 145°";
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

        private void 复制Item_Click(object sender, EventArgs e)
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
