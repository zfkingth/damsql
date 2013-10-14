using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using hammergo.GlobalConfig;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using hammergo.Model;
using System.Drawing.Imaging;
using hammergo.Utility;
using System.Data.Linq;
using System.Linq;

namespace hammergo.Graphics
{
    public partial class GraphicForm : DevExpress.XtraEditors.XtraUserControl, hammergo.Utility.IShowAppData
    {
        public event EventHandler<hammergo.Utility.AppSearchEventArgs> ShowDataEvent;

        public GraphicForm()
        {
            InitializeComponent();
        }

        private void pasteDateMenuItem1_Click(object sender, EventArgs e)
        {
            Utility.Utility.handlePasteInDateEdit(c1DateEdit1);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Utility.Utility.handlePasteInDateEdit(c1DateEdit2);
        }

        private void GraphicForm_Load(object sender, EventArgs e)
        {
            appBLL = new hammergo.BLL.ApparatusBLL();
            calcBLL = new hammergo.BLL.CalculateParamBLL();
            partControl1.initial();
            partControl1.disableEdit();
           

            c1DateEdit1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            c1DateEdit1.Properties.DisplayFormat.FormatString = PubConstant.customString;

            c1DateEdit2.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            c1DateEdit2.Properties.DisplayFormat.FormatString = PubConstant.customString;

            //panelControlChart.Dock = DockStyle.None;
            //panelControlChart.Size = new Size(500, 200);
            rightPanel.Size = new Size(GlobalConfig.PubConstant.ConfigData.GraphicPaddingRightWidth, rightPanel.Height);
            memoEdit1.Size = new Size(memoEdit1.Width, GlobalConfig.PubConstant.ConfigData.GraphicPaddingBottomHeight);
           
        }

        void setPropertyGrid()
        {

            propertyGrid1.SelectedObject = new GraphicProperty(c1Chart1);
           
        }


        TreeListNode currentNode = null;
        ProjectPart currentPart = null;
        private void partControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point point = new Point(e.X, e.Y);
                TreeListHitInfo hit = partControl1.CalcHitInfo(point);
                if (hit.Column != null && hit.Node != null)
                {


                    currentNode = hit.Node;


                    currentPart = partControl1.partCollection[currentNode.Id] as ProjectPart;
                    if (currentNode.Nodes.Count == 0)
                    {
                        setAppsByPartInListBox(currentPart);
                    }
                    else
                    {

                        apparatusBindingSource.DataSource = null;
                    }




                }
            }
        }

        hammergo.BLL.ApparatusBLL appBLL = null;

        private void setAppsByPartInListBox(ProjectPart part)
        {
            //lbcApps.DataSource = part.Apps;
            //lbcApps.DisplayMember = "AppName";
            if (part != null)
            {
                apparatusBindingSource.DataSource = appBLL.GetListByProjectPartID(part.ProjectPartID.Value);
            }
        }

        private void setAppsByPartInListBox(Guid? partID)
        {

            apparatusBindingSource.DataSource = appBLL.GetListByProjectPartID(partID.Value);

        }

        private void partControl1_SearchItemClick()
        {
            Guid? partID = null;

            string strInput = Utility.Utility.InputBox("查找测点", "输入测点编号", "", null);

            if (strInput != null)
            {
                Apparatus app = appBLL.GetModelBy_AppName(strInput);



                if (app != null && app.ProjectPartID != null)
                {
                    partID = app.ProjectPartID;

                    TreeListNode findNode = partControl1.FindNodeByKeyID(partID.Value);

                    if (findNode != null)
                    {
                        currentNode = findNode;
                        currentPart = partControl1.partCollection[currentNode.Id] as ProjectPart;
                        TreeListNode parentNode = findNode;
                        do
                        {
                            parentNode.Expanded = true;
                            parentNode = parentNode.ParentNode;

                        } while (parentNode != null);

                        partControl1.Selection.Clear();
                        findNode.Selected = true;

                        setAppsByPartInListBox(partID);


                    }
                }




            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (lbcApps.SelectedItems.Count > 0)
            {
                if (ShowDataEvent != null)
                {
                    ShowDataEvent(sender, new hammergo.Utility.AppSearchEventArgs(lbcApps.SelectedValue.ToString()));
                }
            }
        }

        hammergo.BLL.CalculateParamBLL calcBLL = null;
        private void lbcApps_DoubleClick(object sender, EventArgs e)
        {
            if (lbcApps.SelectedItems.Count > 0)
            {
                try
                {
                    string appName = ((Apparatus)lbcApps.SelectedItems[0]).AppName;
                    

                    var orderList = (from item in calcBLL.GetListByappName(appName)
                                     orderby item.Order ascending
                                     select item).ToList<CalculateParam>();

                    foreach (CalculateParam cp in orderList )
                    {
                        Graphics.图形Row row = graphics.图形.New图形Row();

                        row.测点编号 = appName;

                        row.刻度轴 = "主轴";

                        row.线条名称 = cp.ParamName;

                        row.图例名称 = appName + "." + cp.ParamName;

                        row.EndEdit();

                        graphics.图形.Add图形Row(row);

                    }

                    graphics.AcceptChanges();
                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(this, ex.Message, "错误!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    graphics.RejectChanges();
                }


            }
        }

       


      

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Clipboard.SetDataObject("", true);
            c1Chart1.SaveImage(ImageFormat.Bmp);
            c1Chart1.SaveImage(ImageFormat.Emf);
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Clipboard.SetDataObject("", true);
            c1Chart1.SaveImage(ImageFormat.Bmp);
        }

        private int lastFilterIndex = 1;
        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfg = new SaveFileDialog();

            sfg.Filter = "Metafiles (*.emf)|*.emf|" +
                "Bmp files (*.bmp)|*.bmp|" +
                "Gif files (*.gif)|*.gif|" +
                "Jpeg files (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                "Png files (*.png)|*.png|" +
                "All graphic files (*.emf;*.bmp;*.gif;*.jpg;*.jpeg;*.png)|*.emf;*.bmp;*.gif;*.jpg;*.jpeg;*.png";
            sfg.FilterIndex = lastFilterIndex;
            sfg.OverwritePrompt = true;
            sfg.CheckPathExists = true;
            sfg.RestoreDirectory = false;
            sfg.ValidateNames = true;

            if (sfg.ShowDialog() == DialogResult.OK)
            {
                string fn = sfg.FileName;
                int indext = fn.LastIndexOf('.');
                if (indext < 0)
                {
                    indext = fn.Length + 1;
                    fn += ".emf";
                }
                else
                    indext++;

                string ext = fn.Substring(indext);
                ImageFormat imgfmt = null;

                switch (ext)
                {
                    case "emf":
                        imgfmt = ImageFormat.Emf;
                        c1Chart1.SaveImage(fn, imgfmt);
                        break;

                    case "bmp":
                        imgfmt = ImageFormat.Bmp;
                        break;

                    case "gif":
                        imgfmt = ImageFormat.Gif;
                        break;

                    case "jpeg":
                    case "jpg":
                        imgfmt = ImageFormat.Jpeg;
                        break;

                    case "png":
                        imgfmt = ImageFormat.Png;
                        break;

                    default:
                        return;
                }

                lastFilterIndex = sfg.FilterIndex;

                if (!imgfmt.Equals(ImageFormat.Emf))
                {
                    Image img = c1Chart1.GetImage();
                    img.Save(fn, imgfmt);
                    img.Dispose();
                }
            }
            sfg.Dispose();
        }

        /// <summary>
        /// 也就是labeInfo label显示的初始文本，对应GraphicProperty里的文本属性。
        /// </summary>
        private string xyzInfo = "";
      
        private void lbcApps_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbcApps.SelectedItems.Count > 0 && lbcApps.SelectedItems[0]!=null)
            {
#if (LayoutDEBUG)
                System.Console.Write("\n\n"+DateTime.Now.ToString("HH:mm:ss:") + DateTime.Now.Millisecond.ToString());
                System.Console.WriteLine(" app selected c1Chart1.ChartArea.Location.Y= " + c1Chart1.ChartArea.Location.Y);
                
#endif

                string appName = ((Apparatus)lbcApps.SelectedItems[0]).AppName;

                Graphics graDS = ChartFun.createGraphicsDataSetByApp(appName,textBoxFilterVariable.Text.Trim());

                setImage(graDS);

                //只为传入xyzInfo信息到labels

            }
        }

        private void setImage(Graphics graDS)
        {
            call = false;

            DateTime? startDate = null, endDate = null;
            if (c1DateEdit1.EditValue != null)
            {
                startDate = c1DateEdit1.DateTime;
            }
            if (c1DateEdit2.EditValue != null)
            {
                endDate = c1DateEdit2.DateTime;
            }


            List<AppIntegratedInfo> listAppInfo = new List<AppIntegratedInfo>(4);

            foreach (Graphics.图形Row row in graDS.图形.Rows)
            {
                AppIntegratedInfo appInfo = new AppIntegratedInfo(row.测点编号, 0, startDate, endDate);
                if (listAppInfo.Exists(delegate(AppIntegratedInfo item) { return item.appName == row.测点编号; }) == false)
                {
                    listAppInfo.Add(appInfo);
                }
            }

            if (listAppInfo.Count == 1)
            {
                Apparatus app = listAppInfo[0].App;
                //只有是单独的一支仪器才显示坐标信息
                xyzInfo = string.Format("{0}{1}{2}", app.X, app.Y, app.Z);
            }
            else
            {
                xyzInfo = "";
            }

            ChartFun.createGraphic(listAppInfo, graDS, c1Chart1,cePadding.Checked);

            //set other information
            setPropertyGrid();

            memoEdit1.Text = ChartFun.getInfoAndExtream(listAppInfo, c1Chart1);

          
        }

        private void c1Chart1_LayoutLabels(object sender, EventArgs e)
        {
            ChartFun.LayoutLables((C1.Win.C1Chart.C1Chart)sender, xyzInfo);


        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            setPropertyGrid();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = c1Chart1;
           
        }

       

        private void c1Chart1_SizeChanged(object sender, EventArgs e)
        {
            if (call)
            {
                DirectDraw();
            }
            c1Chart1.Refresh();
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            string name = snBox.Text.Trim();
            Apparatus app = appBLL.GetModelBy_AppName(name);
            if (app != null)
            {
                apparatusBindingSource.Add(app);
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            setImage(graphics);
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            graphics.Clear();
        }


        ToolTip tp =null;
        private void c1Chart1_MouseMove(object sender, MouseEventArgs e)
        {
            if (tp == null)
            {
               tp= new ToolTip();
               tp.AutomaticDelay = 1500;
               tp.AutoPopDelay = 500;
            }
          

            int gi=0, si=0, pi=0;
           
            if ( ChartFun.mouseTest(c1Chart1,ref gi,ref si,ref pi, e))
            {
                Cursor = Cursors.Cross;
                C1.Win.C1Chart.ChartDataSeries cds = c1Chart1.ChartGroups[gi].ChartData.SeriesList[si];

                string info = string.Format("{0}:{1} 日期:{2}", cds.Label, cds.Y[pi], ((DateTime)cds.X[pi]).ToString("yyyy-MM-dd"));

                tp.SetToolTip((Control)sender, info);
                tp.Active = true;
            }
            else
            {
                Cursor = Cursors.Default;

            }

        }

        private void c1Chart1_MouseLeave(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        private void c1Chart1_MouseDown(object sender, MouseEventArgs e)
        {
            if (MouseButtons.Left == e.Button)
            {
                int gi=0, si =0, pi = 0;

                if (ChartFun.mouseTest(c1Chart1,ref gi,ref si,ref pi,e))
                {

                    propertyGrid1.SelectedObject = new CustomProperty(c1Chart1.ChartGroups[gi].ChartData.SeriesList[si]);

                }

            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            ChartFun.saveStyles(c1Chart1);
        }

        bool call = false;
        int xcor1 = 0, ycor1 = 0, xcor2 = 0, ycor2 = 0, xcor_yMax = 0, ycor_yMax = 0;
        int[,] cors = null;
        int minYear, maxYear;
        protected void DirectDraw()
        {
            c1Chart1.ChartArea.AxisX.AnnoMethod = C1.Win.C1Chart.AnnotationMethodEnum.ValueLabels;

            c1Chart1.Refresh();

            double xMin = c1Chart1.ChartArea.AxisX.Min;
            double xMax = c1Chart1.ChartArea.AxisX.Max;
            double yMin = c1Chart1.ChartArea.AxisY.Min;
            double yMax = c1Chart1.ChartArea.AxisY.Max;



            c1Chart1.ChartGroups[0].DataCoordToCoord(xMin, yMin, ref xcor1, ref ycor1);
            c1Chart1.ChartGroups[0].DataCoordToCoord(xMax, yMin, ref xcor2, ref ycor2);
            c1Chart1.ChartGroups[0].DataCoordToCoord(xMax, yMax, ref xcor_yMax, ref ycor_yMax);


            DateTime minDate = Utility.Utility.NumToDateTime(xMin);
            DateTime maxDate = Utility.Utility.NumToDateTime(xMax);

            minYear = int.Parse(minDate.ToString("yyyy"));

            maxYear = int.Parse(maxDate.ToString("yyyy"));

            cors = new int[maxYear - minYear + 1, 3];//第一列为起始的x坐标,第二列为结束的x的坐标,第三列为时间

            for (int i = 0; i < cors.GetLength(0); i++)
            {
                int year = minYear + i;
                DateTime sdate = new DateTime(year, 1, 1);
                DateTime edate = new DateTime(year + 1, 1, 1);


                double _snum = Utility.Utility.DateTimeToNum(sdate);
                double _enum = Utility.Utility.DateTimeToNum(edate);

                if (_snum <= xMin)
                {
                    _snum = xMin;
                }
                if (_enum >= xMax)
                {
                    _enum = xMax;
                }

                int temp = 0;
                c1Chart1.ChartGroups[0].DataCoordToCoord(_snum, yMin, ref cors[i, 0], ref temp);
                c1Chart1.ChartGroups[0].DataCoordToCoord(_enum, yMin, ref cors[i, 1], ref temp);
                cors[i, 2] = year;


            }

            call = true;



            c1Chart1.Refresh();





        }

        private void c1Chart1_Paint(object sender, PaintEventArgs e)
        {

            if (call)
            {

                Font font = new Font(FontFamily.GenericSansSerif.Name, 9);
                System.Drawing.Pen pen = System.Drawing.Pens.Black;

                System.Drawing.Graphics ghs = e.Graphics;

                SizeF sf = ghs.MeasureString("1983", font);

                Pen dashPen = new Pen(Color.Gray);
                dashPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

                float height = sf.Height - 2f;

                float stringWidth = sf.Width;

                Font smallFont = new Font(FontFamily.GenericSansSerif.Name, 8);



                ghs.DrawLine(pen, new PointF(xcor1, ycor1 + height), new PointF(xcor2, ycor2 + height));

                for (int i = 0; i < cors.GetLength(0); i++)
                {
                    PointF beforePoint1 = new PointF(cors[i, 0], ycor1);
                    PointF beforePoint2 = new PointF(cors[i, 0], ycor1 + height);

                    PointF beforeHighPoint = new PointF(cors[i, 0], ycor_yMax);

                    PointF lastHighPoint = new PointF(cors[i, 1], ycor_yMax);



                    PointF lastPoint1 = new PointF(cors[i, 1], ycor1);
                    PointF lastPoint2 = new PointF(cors[i, 1], ycor1 + height);



                    ghs.DrawLine(pen, beforePoint1, beforePoint2);
                    ghs.DrawLine(pen, lastPoint1, lastPoint2);



                    ghs.DrawLine(dashPen, beforeHighPoint, beforePoint1);

                    ghs.DrawLine(dashPen, lastHighPoint, lastPoint1);

                    //显示年份
                    float distance = cors[i, 1] - cors[i, 0];
                    if (distance >= stringWidth)
                    {


                        float startXCor = cors[i, 0] + (distance - stringWidth) / 2.0f;
                        float startYCor = ycor1 + 0.2f;

                        ghs.DrawString((minYear + i).ToString(), smallFont, Brushes.Black, new PointF(startXCor, startYCor));
                    }
                }


                ghs.Save();


            }

        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            DirectDraw();
        }

        private void 保存图形区域大小ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GlobalConfig.PubConstant.ConfigData.GraphicPaddingRightWidth = rightPanel.Width;
            GlobalConfig.PubConstant.ConfigData.GraphicPaddingBottomHeight=memoEdit1.Height;

            GlobalConfig.PubConstant.updateConfigData();
        }

        private void 清除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<object> list = new List<object>(10);
            foreach (object obj in lbcApps.SelectedItems)
            {
                list.Add(obj);
            }

            apparatusBindingSource.SuspendBinding();
            foreach (object obj in list)
            {
                apparatusBindingSource.Remove(obj);
            }
            apparatusBindingSource.ResumeBinding();
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string clipString = (string)System.Windows.Forms.Clipboard.GetDataObject().GetData(typeof(string));

            if (clipString == null || clipString.Length == 0) return;


            string[] sns = clipString.Split(new char[] { '\n', '\r', '\t' });

            apparatusBindingSource.SuspendBinding();

            for (int i = 0; i < sns.Length; i++)
            {
                string name = sns[i];
                Apparatus app=appBLL.GetModelBy_AppName(name);
                if (app!=null)
                {
                    apparatusBindingSource.Add(app);
                }
            }

            apparatusBindingSource.ResumeBinding();
        }

  


    }
}
