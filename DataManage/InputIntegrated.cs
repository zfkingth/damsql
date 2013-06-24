using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using hammergo.GlobalConfig;
using hammergo.Model;
using hammergo.Utility;
using System.Collections;

namespace hammergo.DataManage
{
    public partial class InputIntegrated : DevExpress.XtraEditors.XtraUserControl
    {
        public InputIntegrated()
        {
            InitializeComponent();
        }

        hammergo.BLL.TaskAppratusBLL taskAppBLL = null;
        hammergo.BLL.TaskTypeBLL taskTypeBLL = null;
        hammergo.BLL.AppCollectionBLL appCollectionBLL = null;

        private void InputIntegrated_Load(object sender, EventArgs e)
        {
            c1DateEdit1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            c1DateEdit1.Properties.DisplayFormat.FormatString = PubConstant.customString;

            lblRemark.Text = PubConstant.remarkColumnName;

            taskAppBLL = new hammergo.BLL.TaskAppratusBLL();
            taskTypeBLL = new hammergo.BLL.TaskTypeBLL();
            appCollectionBLL = new hammergo.BLL.AppCollectionBLL();

            TaskType type = taskTypeBLL.GetModelBy_TypeName(PubConstant.inputTaskName);
            if (type != null)
            {
                appCollectionBindingSource.DataSource = appCollectionBLL.GetListBytaskTypeID(type.TaskTypeID.Value);

            }

        }

        private void appCollectionBindingSource_CurrentChanged(object sender, EventArgs e)
        {
            if (appCollectionBindingSource.Current != null)
            {
                rowList.Clear();

                hammergo.Model.AppCollection appCol = appCollectionBindingSource.Current as hammergo.Model.AppCollection;

                if (appCol != null)
                {

                    taskAppratusBindingSource.DataSource = taskAppBLL.GetListByappCollectionID(appCol.AppCollectionID.Value);

                    gridApps.FocusedRowHandle = 0;
                }
            }
        }

        private void gbInput_SizeChanged(object sender, EventArgs e)
        {


            Control control = sender as Control;

            setControlCenter(control, panelControl1);

        }

        private void setControlCenter(Control parentControl, Control childControl)
        {
            this.SuspendLayout();

            int startX = (parentControl.ClientSize.Width - childControl.Size.Width) / 2;
            int startY = (parentControl.ClientSize.Height - childControl.Size.Height) / 2;

            childControl.Location = new Point(startX, startY);

            this.ResumeLayout();
        }

        private void bottomPanel_SizeChanged(object sender, EventArgs e)
        {
            Control control = sender as Control;

            setControlCenter(control, panelControl2);

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckEdit ce = sender as CheckEdit;
            if (ce != null)
            {
                if (ce.Checked == false)
                {
                    groupControl5.Visible = false;
                    gbInput.Dock = DockStyle.Fill;




                }
                else
                {
                    groupControl5.Visible = true;
                    gbInput.Dock = DockStyle.Right;

                    if (currentAppName.Trim().Length != 0)
                    {
                        showAppGraphic(currentAppName);
                    }


                }
            }
        }

        string currentAppName = "";
        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            taskAppratusBindingSource_PositionChanged(null, null);

        }

        hammergo.Graphics.Graphics currentGraDS = null;
        List<AppIntegratedInfo> currentListAppInfo = null;
        AppIntegratedInfo currentAppInfo = null;
        private void showAppGraphic(string appName)
        {
           currentGraDS = hammergo.Graphics.ChartFun.createGraphicsDataSetByApp(appName,"");

            currentListAppInfo = new List<AppIntegratedInfo>(2);


             currentAppInfo = new AppIntegratedInfo(appName, 0, null, null);

            currentListAppInfo.Add(currentAppInfo);


            hammergo.Graphics.ChartFun.createGraphic(currentListAppInfo, currentGraDS, c1Chart1,true);
        }

        ToolTip tp = null;
        private void c1Chart1_MouseMove(object sender, MouseEventArgs e)
        {
            if (tp == null)
            {
                tp = new ToolTip();
                tp.AutomaticDelay = 1500;
                tp.AutoPopDelay = 500;
            }


            int gi = 0, si = 0, pi = 0;

            if (hammergo.Graphics.ChartFun.mouseTest(c1Chart1, ref gi, ref si, ref pi, e))
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

        DataRow newRow = null;
        private void inputButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (c1DateEdit1.EditValue == null)
                {
                    throw new Exception("请输入观测日期");
                }

                DateTime currentDate = c1DateEdit1.DateTime;

                //查找数据是否存在
                AppIntegratedInfo appInfo = appData1.appInfo;
                if (appInfo == null)
                {
                    throw new Exception("该测点存在异常，请检查其参数");
                }

                //首先在当前程序缓存中查找
                if (appInfo.MessureValues.Exists(delegate(MessureValue item) { return item.Date.Value == currentDate; }))
                {
                    throw new Exception("该时间的数据已存在");

                }

                MessureParam mp = appInfo.MessureParams[0];
                if (appInfo.MesValueBLL.ExistsBy_messureParamID_Date(mp.MessureParamID.Value,currentDate))
                {
                    throw new Exception("该日期的数据在数据库中已存在,请刷新窗口");
                }

                //添加数据

                newRow = appData1.addNewRow(currentDate);

                //在查询数据时已经排过序
                currentMessureParam = appData1.appInfo.MessureParams[0];
                paInfo.Text = currentMessureParam.ParamName;

                setStates(true);

                btnSave.Enabled = false;



            }
            catch (Exception ex)
            {
                Utility.Utility.log(ex);
                XtraMessageBox.Show(this, ex.Message, "错误!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        MessureParam currentMessureParam = null;

        private void setStates(bool val)
        {
            txtValue.Enabled = val;
            txtRemark.Enabled = val;

            txtValue.Text = "0";

            if (val)
            {
                txtValue.SelectAll();
                txtValue.Focus();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {

            if (gridApps.FocusedRowHandle == gridApps.RowCount - 1)
            {

                XtraMessageBox.Show(this, "输入完成", "完成!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                gridApps.FocusedRowHandle++;




                inputButton_Click(null, null);


            }


        }

        private void taskAppratusBindingSource_PositionChanged(object sender, EventArgs e)
        {

            TaskAppratus ta = taskAppratusBindingSource.Current as TaskAppratus;
            if (ta != null)
            {
                currentAppName = ta.AppName;
                appData1.search(currentAppName, null, null, false);

                //设置状态
                position = 0;
                setStates(false);

                appData1.focusLastRow();

                gcData.Text = currentAppName;
                gcInput.Text = currentAppName;

                //设置过程线
                if (checkBox1.Checked)
                {
                    showAppGraphic(currentAppName);


                }
            }
        }




        /// <summary>
        /// 判断数字val与list中的数比较相关是否较大，较大返回false,较小返回true
        /// </summary>
        /// <param name="list"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public static bool matchRegular(List<double> list, double val)
        {

            if (list.Count == 0) return true;
            //				throw new Exception("数组不能为空");

            if (list.Count < 6)
                return true;


            //相对差
            List<double> nl = new List<double>(9);
            for (int i = 1; i < list.Count; i++)
            {
                nl.Add(Math.Abs(list[i] - list[i - 1]));

            }

            double maxCha = 0;
            foreach (double cha in nl)
            {
                if (cha > maxCha)
                {
                    maxCha = cha;
                }
            }

            if (Math.Abs(val - list[0]) > PubConstant.ConfigData.CheckTimes * maxCha)
                return false;



            return true;



        }


        int position = 0;
        private void txtValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                if (currentMessureParam != null)
                {
                    double v = 0;

                    object preobj = currentMessureParam.PrecisionNum;
                    int precision = 3;
                    if (preobj is byte)
                    {
                        precision = (byte)preobj;
                    }

                    string pname = currentMessureParam.ParamName;
                    try
                    {

                        v = double.Parse(txtValue.Text);
                        v = Utility.Utility.round(v, precision);
                        txtValue.Text = v.ToString("f" + precision);

                    }
                    catch (Exception)
                    {

                        //表达式或其它

                        try
                        {
                            hammergo.caculator.CalcFunction calc = new hammergo.caculator.CalcFunction();
                            v = calc.compute(txtValue.Text);

                            txtValue.Text = v.ToString("f" + precision);

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);



                        }
                        return;

                    }




                    //验证成功

                    v = double.Parse(txtValue.Text);

                    List<double> list = new List<double>(20);

                    DevExpress.XtraGrid.Views.Grid.GridView gv = appData1.gridControl1.MainView as DevExpress.XtraGrid.Views.Grid.GridView;


                    for (int i = gv.RowCount - 2; i >= 0; i--)
                    {
                        DataRow itemRow = gv.GetDataRow(i);
                        if (itemRow != newRow)
                        {
                            object obj = itemRow[pname];
                            if (obj is double)
                            {
                                list.Add((double)obj);
                            }
                        }
                    }
                    if (matchRegular(list, v) == false)
                    {
                        if (MessageBox.Show(this, v.ToString() + "变化较大,是否保存?", "变化较大", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
                        {
                            txtValue.SelectAll();
                            return;
                        }
                    }

                    newRow[pname] = v;
                    newRow.EndEdit();

                    //Search.cellChange(dsapp, newRow, pname);

                    //gridAdd.RefetchRow(gridAdd.Row);
                    Utility.UtilityUpdateData.redirectToObjects(appData1.appInfo, newRow, pname);

                    txtValue.Text = "0";

                    position++;
                    if (position < appData1.appInfo.MessureParams.Count)
                    {
                        currentMessureParam = appData1.appInfo.MessureParams[position];
                        txtValue.SelectAll();

                        paInfo.Text = currentMessureParam.ParamName;
                    }
                    else
                    {
                        currentMessureParam = null;

                        
                        txtRemark.Focus();
                    }

                    //激活保存按钮

                    btnSave.Enabled = true;



                }
            }
        }

        List<int> rowList = new List<int>(100);

        private void txtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                saveData();
            }
        }

        /// <summary>
        /// 保存数据到数据库中
        /// </summary>
        /// <returns></returns>
        private void saveData()
        {
            
            if (appData1.appInfo != null && newRow != null)
            {

                refreshGraphic(appData1.appInfo);

                string remarkText = txtRemark.Text.Trim();

                txtRemark.Text = "";

                newRow.BeginEdit();
                newRow["批注"] = remarkText;
                newRow.EndEdit();

                DateTime currentDateTime = (DateTime)newRow[PubConstant.timeColumnName];

                if (remarkText.Length != 0)
                {
                    //只有数据非空才会创造数据库对象矛以保存
                    Remark reamark = new Remark();
                    reamark.AppName = appData1.appInfo.appName;
                    reamark.Date = currentDateTime;
                    reamark.RemarkText = remarkText;

                    appData1.appInfo.Remarks.Add(reamark);
                }

                //System.Threading.Thread thread = Search.updateData(dsapp, newRow["时间"].ToString(), remark);

                System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(writeDataToDB));

                SaveParam sp = new SaveParam(appData1.appInfo, currentDateTime);

                thread.Start(sp);

                newRow = null;

                int rowHandle = gridApps.FocusedRowHandle;
                rowList.Add(rowHandle);
                gridApps.RefreshRow(rowHandle);

                setStates(false);

                btnNext.Focus();

            }


        }

        private void refreshGraphic(AppIntegratedInfo appInfo)
        {
            List<CalculateValue> changeValues= appInfo.CalcValues.GetChanges();

            foreach (CalculateValue item in changeValues)
            {
                if (item.TrackingState == Tracking.TrackingInfo.Created)
                {
                    currentAppInfo.CalcValues.Add(item);
                }
            }

            hammergo.Graphics.ChartFun.createGraphic(currentListAppInfo, currentGraDS, c1Chart1, true);
        }

        private void writeDataToDB(object param)
        {
            try
            {
                SaveParam sp = param as SaveParam;

                if (sp != null)
                {
                    sp.AppInfo.Update();

                    Utility.UtilityUpdateData.reCalculateLink(sp.AppInfo.App.CalculateName, sp.UpdateTime);

                }
            }
            catch (Exception ex)
            {
                Utility.Utility.log(ex);
                DevExpress.XtraEditors.XtraMessageBox.Show(this, "请在检查此仪器的参数: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void gridApps_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (rowList.Contains(e.RowHandle))
            {
                e.Appearance.BackColor = Color.Yellow;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            saveData();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            //gridApps.CopyToClipboard();
            Utility.Utility.copyGridSelection(gridApps);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                string newAppName = Utility.Utility.InputBox("输入", "请输入测点编号", "", null);

                if (newAppName != null)
                {
                    newAppName = newAppName.Trim().ToUpper();
                    int i = 0;
                    for (; i < appCollectionBindingSource.Count; i++)
                    {
                        AppCollection appCollection = appCollectionBindingSource[i] as AppCollection;

                        if (taskAppBLL.ExistsBy_appCollectionID_appName(appCollection.AppCollectionID.Value, newAppName))
                        {
                            break;
                        }
                    }
                    if (i < appCollectionBindingSource.Count)
                    {
                        //找到仪器
                        appCollectionBindingSource.Position = i;

                        Tracking.TrackedList<TaskAppratus> taskApps = taskAppratusBindingSource.DataSource as Tracking.TrackedList<TaskAppratus>;
                        int index = taskApps.FindIndex(delegate(TaskAppratus item) { return item.AppName == newAppName; });

                        taskAppratusBindingSource.Position = index;


                    }
                    else
                    {
                        throw new Exception("请输入正确的测点编号,并注意大小写!");
                    }
                    //Tracking.TrackedList<TaskAppratus> taskApps = taskAppBLL.GetListByappName(newAppName);
                    //if (taskApps.Count != 0)
                    //{
                    //    Tracking.TrackedList<AppCollection> tasks = appCollectionBindingSource.DataSource as Tracking.TrackedList<AppCollection>;
                    //    int index = tasks.FindIndex(delegate(AppCollection item) { return item.AppCollectionID == taskApps[0].AppCollectionID; });
                    //    appCollectionBindingSource.Position = index;

                    //    taskApps = taskAppratusBindingSource.DataSource as Tracking.TrackedList<TaskAppratus>;
                    //    index = taskApps.FindIndex(delegate(TaskAppratus item) { return item.AppName == newAppName; });

                    //    taskAppratusBindingSource.Position = index;



                    //}
                    //else
                    //{
                    //    throw new Exception("请注意测点编号的大小写!");
                    //}
                }


            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(this, ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

            }
        }

        public event EventHandler<TaskEventArgs> DataObserveEvent;

        private void button1_Click(object sender, EventArgs e)
        {
            if (DataObserveEvent != null)
            {
                AppCollection current = appCollectionBindingSource.Current as AppCollection;
                if (current != null)
                {
                    TaskEventArgs args = new TaskEventArgs(current.AppCollectionID.Value, c1DateEdit1.DateTime);
                    
                    DataObserveEvent(sender,args);

                }
            }
        }

        private void 粘贴ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Utility.Utility.handlePasteInDateEdit(c1DateEdit1);
        }

        private void c1DateEdit1_ParseEditValue(object sender, DevExpress.XtraEditors.Controls.ConvertEditValueEventArgs e)
        {
            if (e.Value != null)
            {
                DateTime i = (DateTime)e.Value;
                e.Value = new DateTime(i.Year, i.Month, i.Day, i.Hour, i.Minute, 0);
            }
            
        }
    }

    public class MyComparer : IComparer
    {

        // Calls CaseInsensitiveComparer.Compare with the parameters reversed.
        int IComparer.Compare(Object x, Object y)
        {
            return ((new CaseInsensitiveComparer()).Compare(y, x));
        }

    }

    public class SaveParam
    {
        private readonly AppIntegratedInfo _appInfo;
        private readonly DateTime _updateTime;



        public SaveParam(AppIntegratedInfo appInfo, DateTime updateTime)
        {
            _appInfo = appInfo;
            _updateTime = updateTime;
        }

        public AppIntegratedInfo AppInfo
        {
            get
            {
                return _appInfo;
            }
        }

        public DateTime UpdateTime
        {
            get
            {
                return _updateTime;
            }
        }

    }
}
