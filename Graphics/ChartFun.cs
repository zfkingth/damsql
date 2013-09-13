using System.Data.Linq;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using C1.Win.C1Chart;
using hammergo.Utility;
using hammergo.Model;
using hammergo.GlobalConfig;
namespace hammergo.Graphics
{
    public class ChartFun
    {
        private static List<ChartLineStyle> lineStyleList = new List<ChartLineStyle>(6);
        private static List<ChartSymbolStyle> symbolStyleList = new List<ChartSymbolStyle>(6);

        static ChartFun()
        {

            foreach (hammergo.GlobalConfig.LineStyleInfo lineInfo in hammergo.GlobalConfig.PubConstant.ConfigData.LineStyleInfoList)
            {
                C1.Win.C1Chart.ChartLineStyle cls = new C1.Win.C1Chart.ChartLineStyle();

                C1.Win.C1Chart.ChartSymbolStyle css = new C1.Win.C1Chart.ChartSymbolStyle();


                //cls.Thickness = (int)row["line粗细"];
                cls.Thickness = lineInfo.LineThickness;
                //cls.Color = System.Drawing.Color.FromArgb((int)row["line颜色"]);
                cls.Color = System.Drawing.Color.FromArgb(lineInfo.LineColor);
                //cls.Pattern = (C1.Win.C1Chart.LinePatternEnum)Enum.Parse(typeof(C1.Win.C1Chart.LinePatternEnum), row["line样式"].ToString(), false);
                cls.Pattern = (C1.Win.C1Chart.LinePatternEnum)Enum.Parse(typeof(C1.Win.C1Chart.LinePatternEnum), lineInfo.LineStyle, false);


                //css.Size = (int)row["symbol大小"];
                css.Size = lineInfo.SymbolSize;
                //css.OutlineWidth = (int)row["symbol外围粗细"];
                css.OutlineWidth = lineInfo.SymbolOutThickness;
                //css.Color = System.Drawing.Color.FromArgb((int)row["symbol颜色"]);
                css.Color = System.Drawing.Color.FromArgb(lineInfo.SymbolColor);
                //css.OutlineColor = System.Drawing.Color.FromArgb((int)row["symbol外围颜色"]);
                css.OutlineColor = System.Drawing.Color.FromArgb(lineInfo.SymbolOutColor);
                //css.Shape = (C1.Win.C1Chart.SymbolShapeEnum)Enum.Parse(typeof(C1.Win.C1Chart.SymbolShapeEnum), row["symbol形状"].ToString(), false);
                css.Shape = (C1.Win.C1Chart.SymbolShapeEnum)Enum.Parse(typeof(C1.Win.C1Chart.SymbolShapeEnum), lineInfo.SymbolShape, false);


                //ConnectionLib.Connection.LineStyleList.Add(cls);
                lineStyleList.Add(cls);
                //ConnectionLib.Connection.SymbolStyleList.Add(css);
                symbolStyleList.Add(css);



            }

        }

        public static void LayoutLables(C1Chart c1Chart1, string infotext)
        {
            //保存info label的信息

            System.Drawing.Graphics g = c1Chart1.CreateGraphics();

            const int offset = 6;

            ////清除labels
            //c1Chart1.ChartLabels.LabelsCollection.Clear();

            //设置坐标轴描述,如果已经存在y1,y2 label，将不会添加label
            addAxisLabels(c1Chart1, infotext);

            C1.Win.C1Chart.Label laby2 = c1Chart1.ChartLabels.LabelsCollection["y2"];
            C1.Win.C1Chart.Label laby1 = c1Chart1.ChartLabels.LabelsCollection["y1"];



            if (laby2 != null)
            {

                if (laby2.Size.Width != -1)
                {
                    laby2.AttachMethodData.X = c1Chart1.ChartArea.Location.X + c1Chart1.ChartArea.Size.Width - laby2.Size.Width;
                }
                else
                {
                    float laby2width = g.MeasureString(laby2.Text, laby2.Style.Font).Width;
                    laby2.AttachMethodData.X = c1Chart1.ChartArea.Location.X + c1Chart1.ChartArea.Size.Width - laby2width;
                }

               
                laby2.AttachMethodData.Y = c1Chart1.ChartArea.Location.Y - offset;


            }


            if (laby1 != null)
            {

                laby1.AttachMethodData.X = c1Chart1.ChartArea.Location.X;
                laby1.AttachMethodData.Y = c1Chart1.ChartArea.Location.Y - offset;


            }


            C1.Win.C1Chart.Label labinfo = c1Chart1.ChartLabels.LabelsCollection["info"];



            float labinfoWidth = g.MeasureString(labinfo.Text, labinfo.Style.Font).Width;
            labinfo.AttachMethodData.X = c1Chart1.ChartArea.Location.X + (c1Chart1.ChartArea.Size.Width - labinfoWidth) / 2;

            labinfo.AttachMethodData.Y = c1Chart1.ChartArea.Location.Y - offset;




            g.Dispose();




#if (LayoutDEBUG)
                        System.Console.Write(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString());
                        if (labinfo != null)
                            System.Console.WriteLine(" LayoutLabels invoked." + " labinfo AttachMethodData X= " + labinfo.AttachMethodData.X + ", Y=" + labinfo.AttachMethodData.Y);

                        System.Console.WriteLine();

#endif

        }


        public static void addAxisLabels(C1.Win.C1Chart.C1Chart c1Chart1, string infotext)
        {


            if (c1Chart1.ChartLabels.LabelsCollection["y1"] == null)
            {
                C1.Win.C1Chart.Label laby1 = c1Chart1.ChartLabels.LabelsCollection.AddNewLabel();

                if (c1Chart1.ChartGroups[0].ChartData.SeriesList.Count > 1)
                {
                    laby1.Text = "单位:" + c1Chart1.ChartGroups[0].ChartData.SeriesList[0].Tag.ToString();
                }
                else if (c1Chart1.ChartGroups[0].ChartData.SeriesList.Count == 1)
                {
                    laby1.Text = c1Chart1.ChartGroups[0].ChartData.SeriesList[0].Label + string.Format("({0})", c1Chart1.ChartGroups[0].ChartData.SeriesList[0].Tag);
                }
                laby1.Name = "y1";

                laby1.AttachMethod = C1.Win.C1Chart.AttachMethodEnum.Coordinate;
                laby1.Visible = true;


#if(LayoutDEBUG)

                System.Console.Write(DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString());
                System.Console.WriteLine(" addAxisLabels() invoked." + " y1 AttachMethodData X= " + laby1.AttachMethodData.X + ", Y=" + laby1.AttachMethodData.Y);
#endif
            }


            if (c1Chart1.ChartLabels.LabelsCollection["y2"] == null)
            {
                //设置y2坐标轴的信息
                C1.Win.C1Chart.Label laby2 = c1Chart1.ChartLabels.LabelsCollection.AddNewLabel();

                if (c1Chart1.ChartGroups[1].ChartData.SeriesList.Count == 1)
                {
                    laby2.Text = c1Chart1.ChartGroups[1].ChartData.SeriesList[0].Label + string.Format("({0})", c1Chart1.ChartGroups[1].ChartData.SeriesList[0].Tag);

                }
                else if (c1Chart1.ChartGroups[1].ChartData.SeriesList.Count > 1)
                {

                    laby2.Text = "单位:" + c1Chart1.ChartGroups[1].ChartData.SeriesList[0].Tag.ToString();

                }


                laby2.AttachMethod = C1.Win.C1Chart.AttachMethodEnum.Coordinate;
                laby2.Name = "y2";
                laby2.Visible = true;


            }


            if (c1Chart1.ChartLabels.LabelsCollection["info"] == null)
            {
                C1.Win.C1Chart.Label labinfo = c1Chart1.ChartLabels.LabelsCollection.AddNewLabel();

                labinfo.Name = "info";
                labinfo.Text = infotext;
                labinfo.AttachMethod = C1.Win.C1Chart.AttachMethodEnum.Coordinate;
                labinfo.Visible = true;

                labinfo.Style.HorizontalAlignment = AlignHorzEnum.Center;


            }
        }

        public static Graphics createGraphicsDataSetByApp(string appName,string filterResultName)
        {
            Graphics graDataSet = new Graphics();

            hammergo.BLL.CalculateParamBLL calcBLL = new hammergo.BLL.CalculateParamBLL();
            
            Tracking.TrackedList<CalculateParam> calcParams = calcBLL.GetListByappName(appName);

            CalculdateDisplayComparer discom = new CalculdateDisplayComparer();
            calcParams.Sort(discom);

            var orderList = (from item in calcParams
                            orderby item.Order ascending
                             select item).ToList < CalculateParam>();

            for (int i = 0; i < orderList.Count; i++)
            {
                CalculateParam cp = orderList[i];
                if (cp.ParamName.ToUpper() != filterResultName.ToUpper())
                {
                    Graphics.图形Row row = graDataSet.图形.New图形Row();

                    row.测点编号 = appName;
                    row.图例名称 = cp.ParamName;
                    row.显示 = true;
                    row.线条名称 = cp.ParamName;

                    if (cp.UnitSymbol == calcParams[0].UnitSymbol)
                    {
                        row.刻度轴 = "主轴";
                    }
                    else
                    {
                        row.刻度轴 = "副轴";
                    }

                    row.EndEdit();

                    graDataSet.图形.Add图形Row(row);
                }

            }
            graDataSet.AcceptChanges();
            return graDataSet;
        }




        /// <summary>
        ///  设置线条样式
        /// </summary>
        /// <param name="c1Chart1"></param>
        /// <param name="preserveAxisX">是否在时间轴上预留一下月</param>
        public static void setStyles(C1Chart c1Chart1,bool preserveAxisX)
        {
            //ArrayList lineStyles = ConnectionLib.Connection.LineStyleList;
            //ArrayList symbolStyles = ConnectionLib.Connection.SymbolStyleList;


            int i = 0;
            double maxDateNum = 0,minDateNum=double.MaxValue;
            for (int gi = 0; gi < 2; gi++)
            {
                for (int si = 0; si < c1Chart1.ChartGroups[gi].ChartData.SeriesList.Count; si++)
                {

                    if (i < lineStyleList.Count)
                    {
                        C1.Win.C1Chart.ChartDataSeries series = c1Chart1.ChartGroups[gi].ChartData.SeriesList[si];

                        if (series.MaxX > maxDateNum)
                        {
                            maxDateNum = series.MaxX;
                        }

                        if (series.MinX < minDateNum)
                        {
                            minDateNum = series.MinX;
                        }

                        series.LineStyle = lineStyleList[i];

                        series.SymbolStyle = symbolStyleList[i];
                        i++;
                    }

                }
            }

            if (preserveAxisX)
            {
                maxDateNum += (maxDateNum-minDateNum)*0.015;//留*%
                minDateNum -= (maxDateNum - minDateNum) * 0.015;
            }
                c1Chart1.ChartArea.AxisX.Max = maxDateNum;
                c1Chart1.ChartArea.AxisX.Min = minDateNum;


        }

        /// <summary>
        /// 将当前图形里线条样式保存到配置文件里
        /// </summary>
        /// <param name="c1Chart1"></param>
        public static void saveStyles(C1Chart c1Chart1)
        {
          


            int index = 0;

            for (int gi = 0; gi < 2; gi++)
            {
                for (int si = 0; si < c1Chart1.ChartGroups[gi].ChartData.SeriesList.Count; si++)
                {
                    ChartLineStyle linestyle = c1Chart1.ChartGroups[gi].ChartData.SeriesList[si].LineStyle;
                    ChartSymbolStyle symbolStyle = c1Chart1.ChartGroups[gi].ChartData.SeriesList[si].SymbolStyle;
                    if (index < lineStyleList.Count)
                    {
                        lineStyleList[index] = linestyle;
                        symbolStyleList[index] = symbolStyle;
                        index++;
                    }
                    else
                    {
                        lineStyleList.Add(linestyle);

                        symbolStyleList.Add(symbolStyle);
                    }

                }
            }


            int count=hammergo.GlobalConfig.PubConstant.ConfigData.LineStyleInfoList.Count;

            for (int i = 0; i < lineStyleList.Count; i++)
            {
                LineStyleInfo  lineInfo = null;

                if (i <count)
                {
                    lineInfo = hammergo.GlobalConfig.PubConstant.ConfigData.LineStyleInfoList[i];
                }
                else
                {
                    lineInfo = new LineStyleInfo();
                    hammergo.GlobalConfig.PubConstant.ConfigData.LineStyleInfoList.Add(lineInfo);
                }

                

                //row["line样式"] = ((ChartLineStyle)lineStyles[i]).Pattern.ToString();
                lineInfo.LineStyle = lineStyleList[i].Pattern.ToString();

                //row["line颜色"] = ((ChartLineStyle)lineStyles[i]).Color.ToArgb();
                lineInfo.LineColor = lineStyleList[i].Color.ToArgb();


                //row["line粗细"] = ((ChartLineStyle)lineStyles[i]).Thickness;
                lineInfo.LineThickness = lineStyleList[i].Thickness;


                //row["symbol形状"] = ((ChartSymbolStyle)symbolStyles[i]).Shape.ToString();
                lineInfo.SymbolShape = symbolStyleList[i].Shape.ToString();


                //row["symbol颜色"] = ((ChartSymbolStyle)symbolStyles[i]).Color.ToArgb();
                lineInfo.SymbolColor = symbolStyleList[i].Color.ToArgb();


                //row["symbol大小"] = ((ChartSymbolStyle)symbolStyles[i]).Size;
                lineInfo.SymbolSize = symbolStyleList[i].Size;

                //row["symbol外围颜色"] = ((ChartSymbolStyle)symbolStyles[i]).OutlineColor.ToArgb();
                lineInfo.SymbolOutColor = symbolStyleList[i].OutlineColor.ToArgb();


                //row["symbol外围粗细"] = ((ChartSymbolStyle)symbolStyles[i]).OutlineWidth;
                lineInfo.SymbolOutThickness = symbolStyleList[i].OutlineWidth;

                hammergo.GlobalConfig.PubConstant.updateConfigData();


               
            }


           
        }

        public static void createGraphic(List<AppIntegratedInfo> listAppInfo, Graphics graDS, C1Chart c1Chart1, bool preserveAxisX)
        {



            c1Chart1.SuspendLayout();
            //清除图形中的所有数据
            ChartDataSeriesCollection cdc =
                c1Chart1.ChartGroups[0].ChartData.SeriesList;

            cdc.Clear();

            c1Chart1.ChartGroups[1].ChartData.SeriesList.Clear();

            //清除labels
            c1Chart1.ChartLabels.LabelsCollection.Clear();

            c1Chart1.ChartArea.AxisX.AnnoMethod = C1.Win.C1Chart.AnnotationMethodEnum.Values;

            foreach (AppIntegratedInfo appInfo in listAppInfo)
            {
                appInfo.CalcValues.Sort(new CalculateValueComparer());
            }



            for (int i = 0; i < graDS.图形.Rows.Count; i++)
            {
                Graphics.图形Row row = (Graphics.图形Row)graDS.图形.Rows[i];
                if (row.显示 == true)
                {
                    string appName = row.测点编号;


                    AppIntegratedInfo appInfo = listAppInfo.Find(delegate(AppIntegratedInfo item) { return item.appName == appName; });

                    string columnName = row.线条名称;

                    ChartDataSeries cds = null;

                    if (row.刻度轴 == "主轴")
                    {
                        cds = c1Chart1.ChartGroups[0].ChartData.SeriesList.AddNewSeries();
                    }
                    else
                    {
                        cds = c1Chart1.ChartGroups[1].ChartData.SeriesList.AddNewSeries();
                    }

                    cds.Label = row.图例名称;

                    CalculateParam cp = appInfo.CalcParams.Find(delegate(CalculateParam item) { return item.ParamName == row.线条名称; });

                    List<CalculateValue> listValues = appInfo.CalcValues.FindAll(delegate(CalculateValue item) { return item.CalculateParamID == cp.CalculateParamID; });


                    cds.PointData.Length = listValues.Count;

                    //cds.PointData.Length = chartDV.Count;

                    cds.X.DataField = "时间";

                    for (int j = 0; j < listValues.Count; j++)
                    {
                        CalculateValue cv = listValues[j];

                        if (cv.Val == null || Utility.Utility.isErrorValue((double)cv.Val))
                        {
                            cds.Y[j] = Double.NaN;

                        }
                        else
                        {
                            cds.Y[j] = cv.Val;
                        }

                        cds.X[j] = cv.Date.Value;
                    }

                    if (cp.UnitSymbol == null)
                    {
                        cds.Tag = "";
                    }
                    else
                    {
                        cds.Tag = cp.UnitSymbol;
                    }

                }

            }

            setStyles(c1Chart1, preserveAxisX);
            c1Chart1.ResumeLayout();




        }

        internal static string getInfoAndExtream(List<AppIntegratedInfo> listAppInfo, C1Chart c1Chart1)
        {
            //显示仪器的常规信息

            StringBuilder sb = new StringBuilder(1000);


            foreach (AppIntegratedInfo appInfo in listAppInfo)
            {

                Apparatus app = appInfo.App;
                sb.Append("测点编号:").Append(app.AppName);
                sb.Append(", X=").Append(app.X);
                sb.Append(", Y=").Append(app.Y);
                sb.Append(", Z=").Append(app.Z);
                sb.Append(", 埋设日期:").Append(app.BuriedTime);
                sb.Append(", 备注:").Append(app.OtherInfo);
                sb.Append("\r\n"); ;//换行
            }


            for (int gi = 0; gi < 2; gi++)
            {

                for (int si = 0; si < c1Chart1.ChartGroups[gi].ChartData.SeriesList.Count; si++)
                {

                    ChartDataSeries dataSeries = c1Chart1.ChartGroups[gi].ChartData.SeriesList[si];

                    sb.Append(dataSeries.Label);
                    if (dataSeries.MaxY.Equals(double.NaN)==false)
                    {
                        sb.Append("\t最大值:").Append(dataSeries.MaxY);
                        sb.Append("\t时间:").Append(((DateTime)dataSeries.X[dataSeries.Y.IndexOf(dataSeries.MaxY)]).ToString(PubConstant.customString));
                    }

                    if (dataSeries.MinY.Equals(double.NaN)==false)
                    {
                        sb.Append("\t最小值:").Append(dataSeries.MinY);
                        sb.Append("\t时间:").Append(((DateTime)dataSeries.X[dataSeries.Y.IndexOf(dataSeries.MinY)]).ToString(PubConstant.customString));
                    }
                    if (dataSeries.X.Length > 0)
                    {
                        sb.Append("\t起始时间:").Append(((DateTime)dataSeries.X[0]).ToString(PubConstant.customString));
                        sb.Append("\t结束时间:").Append(((DateTime)dataSeries.X[dataSeries.Length - 1]).ToString(PubConstant.customString));
                    }
                    sb.Append("\r\n");

                }
            }


            return sb.ToString();


        }



        public static bool mouseTest(C1Chart c1Chart1,  ref int gi, ref int si, ref int pi, System.Windows.Forms.MouseEventArgs e)
        {
            bool find = false;

            gi = si = pi = 0;
            int dist = 0;

            for (; gi < 2; gi++)
            {
                if (c1Chart1.ChartGroups[gi].CoordToDataIndex(e.X, e.Y, C1.Win.C1Chart.CoordinateFocusEnum.XandYCoord, ref si, ref pi, ref dist)
                    && dist <= 3)
                {

                    find = true;
                    break;

                }
            }

            return find;
        }
    }
}
