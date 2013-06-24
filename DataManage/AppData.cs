using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using hammergo.Utility;
using hammergo.GlobalConfig;
using hammergo.Model;

namespace hammergo.DataManage
{
    public partial class AppData : DevExpress.XtraEditors.XtraUserControl
    {
        public AppData()
        {
            InitializeComponent();
        }

    

        public AppData(string appName)
        {
            InitializeComponent();
            txtName.Text = appName;
           

        }


        DateTime? _startDate = null, _endDate = null;
        public void search(string appName,DateTime? startDate,DateTime? endDate,bool fetchAll)
        {
            _appName = appName;
            txtName.Text = appName;
            fetchAllDataFlag = fetchAll;
            _startDate = startDate;
            _endDate = endDate;

            fetchData();
            setDataSource();

           
           
        }

        string _appName;

        public AppIntegratedInfo appInfo = null;
         hammergo.BLL.ApparatusBLL _appBLL ;

        private hammergo.BLL.ApparatusBLL AppBLL
        {
            get
            {
                if (_appBLL == null)
                {
                    _appBLL = new hammergo.BLL.ApparatusBLL();
                }

                return _appBLL;
            }
        }

        public void SearchButton_Click(object sender, EventArgs e)
        {
            try
            {
                _appName = txtName.Text.Trim();
                if (exporter != null)
                {
                    exporter.name = _appName;
                }
                appInfo = null;

               

                if (AppBLL.ExistsBy_AppName(_appName) == false)
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(this, "找不到此编号的仪器", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);


                    gridControl1.DataSource = null;

                    return;
                }

                if (txtName.Properties.Items.Contains(_appName) == false)
                {
                    txtName.Properties.Items.Add(_appName);
                }

                fetchData();
                setDataSource();
               
            }
            catch (Exception ex)
            {
                Utility.Utility.log(ex);
                DevExpress.XtraEditors.XtraMessageBox.Show(this, "请在检查此仪器的参数: " + ex.Message, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void setDataSource()
        {
            gridControl1.DataSource = UtilityGetData.constructTable(appInfo);
            formatGrid();

            DevExpress.XtraTab.XtraTabPage tabPage = this.Parent as DevExpress.XtraTab.XtraTabPage;
            if (tabPage != null)
            {
                tabPage.Text = _appName;
            }
            setEditable(editable);

            //选中最后的一行
            gridView1.FocusedRowHandle = gridView1.RowCount - 1;
        }



        bool editable = false;

        private void setEditable(bool editable)
        {

            foreach (DevExpress.XtraGrid.Columns.GridColumn column in gridView1.Columns)
            {
                if (column.Caption != PubConstant.timeColumnName)
                    column.OptionsColumn.AllowEdit = editable;
            }
        }

        ExportLib.GridViewExport exporter = null;
        private void AppData_Load(object sender, EventArgs e)
        {
            txtName.Focus();
            txtName.SelectAll();
            exporter = new ExportLib.GridViewExport(gridView1, "");
        }

        private void txtName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchButton_Click(simpleButton1, e);
            }
        }

        bool fetchAllDataFlag = false;
        private void fetchData()
        {
            int num = PubConstant.ConfigData.LastedRecordNum;
            if (fetchAllDataFlag) num = 0;
            appInfo = new AppIntegratedInfo(_appName, num, _startDate, _endDate);
          

        }

        //public DevExpress.XtraGrid.Views.Grid.GridView DataGridView
        //{
        //    get
        //    {
        //        return gridView1;
        //    }
        //}

        public void focusLastRow()
        {
            gridView1.FocusedRowHandle = gridView1.RowCount - 1;
        }

        private void formatGrid()
        {
            gridControl1.MainView.PopulateColumns();

            foreach (DevExpress.XtraGrid.Columns.GridColumn column in gridView1.Columns)
            {
                if (column.ColumnType == typeof(DateTime))
                {
                    column.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    column.DisplayFormat.FormatString = PubConstant.customString;
                    column.OptionsColumn.AllowEdit = false;
                    column.SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                }
            }


            gridView1.BestFitColumns();
        }

        private void 所有仪器ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fetchAllDataFlag = !fetchAllDataFlag;

            SearchButton_Click(sender, e);
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //gridView1.CopyToClipboard();
            Utility.Utility.copyGridSelection(gridView1);
        }

        void tsmiXml_Click(object sender, EventArgs e)
        {
            exporter.sbExportToXML_Click(sender, e);

        }

        //void tsmiHtml_Click(object sender, EventArgs e)
        //{
        //    exporter.sbExportToHTML_Click(sender, e);
        //}

        void tsmiTxt_Click(object sender, EventArgs e)
        {
            exporter.sbExportToTXT_Click(sender, e);
        }

        void tsmiExcel_Click(object sender, EventArgs e)
        {
            exporter.sbExportToXLS_Click(sender, e);

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            exporter.setName(_appName);
        }





        private void 设置仪器参数ToolStripMenuItem_Click(object sender, EventArgs e)
        {


            if (AppBLL.ExistsBy_AppName(_appName))
            {

                AppManage.ParamsForm apm = new AppManage.ParamsForm(_appName);
                DialogResult result = apm.ShowDialog();
                if (result == DialogResult.OK)
                {

                    SearchButton_Click(sender, e);
                }
            }


        }



        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            UtilityUpdateData.handleCellValueChanged(sender, e, appInfo, true);
        }

        private void xmlDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            exporter.sbExportToXml_Scheme_Click(sender, e);
        }

        private void 删除数据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show(this, "此操作将删除选中的数据，且不可恢复!\n确认要删除这些数据吗?", "删除数据!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2
                                   ) == DialogResult.Yes)
            {


                if (gridView1.SelectedRowsCount != 0)
                {

                    int[] indexes = gridView1.GetSelectedRows();
                    List<DateTime> delTimes = new List<DateTime>(20);
                    for (int i = 0; i < indexes.Length; i++)
                    {
                        DateTime delTime = (DateTime)gridView1.GetDataRow(indexes[i])[PubConstant.timeColumnName];
                        delTimes.Add(delTime);
                    }


                    if (delTimes.Count > 0)
                    {
                        UtilityUpdateData.deleteRecord(appInfo, delTimes);

                        txtName.Text = appInfo.appName;

                        SearchButton_Click(sender, e);
                    }

                }
            }

        }

        private void 修改数据item_Click(object sender, EventArgs e)
        {
            editable = true;
            setEditable(editable);
        }

        private DataRow selectRow(DateTime date)
        {
            DataRow row = null;
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                DataRow currentRow = gridView1.GetDataRow(i);
                DateTime rowDate =(DateTime) currentRow[PubConstant.timeColumnName];
                if (date == rowDate)
                {
                    //gridView1.SelectRow(i);
                    row = currentRow;
                    gridView1.FocusedRowHandle = i;
                    break;
                }

            }

            return row;
        }

        private bool _showInputPanel = true;
        public bool ShowInputPanel
        {
            get
            {
                return _showInputPanel;
            }
            set
            {
                _showInputPanel = value;
                panelControl1.Visible = _showInputPanel;
                

            }
        }

        private void 添加数据Item_Click(object sender, EventArgs e)
        {
            if (appInfo != null)
            {
                DateTime?[] dates = new DateTime?[1];
                hammergo.CommonControl.InputDateForm inputForm = new hammergo.CommonControl.InputDateForm(dates, true);
                if (inputForm.ShowDialog() == DialogResult.OK)
                {
                    if (dates[0].HasValue)
                    {
                        DateTime newDate = dates[0].Value;


                        DateTime i = newDate;
                        newDate = new DateTime(i.Year, i.Month, i.Day, i.Hour, i.Minute, 0);
                        

                        if (Utility.UtilityGetData.existData(appInfo.appName, newDate))
                        {
                            XtraMessageBox.Show(this, string.Format("{0} 时数据已存在!", newDate.ToString(PubConstant.customString)), "数据已存在",
                                MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                        }
                        else
                        {
                            addNewRow(newDate);
                            appInfo.Update();

                        }


                    }
                }
            }
        }


        public DataRow addNewRow(DateTime newDate)
        {
            foreach (MessureParam mp in appInfo.MessureParams)
            {
                MessureValue mv = new MessureValue();
                mv.Date = newDate;
                mv.MessureParamID = mp.MessureParamID;
                mv.Val = 0;
                appInfo.MessureValues.Add(mv);
            }

            foreach (CalculateParam cp in appInfo.CalcParams)
            {
                CalculateValue cv = new CalculateValue();
                cv.Date = newDate;
                cv.CalculateParamID = cp.CalculateParamID;
                cv.Val = 0;
                appInfo.CalcValues.Add(cv);
            }

            setDataSource();


            return selectRow(newDate);

        }

      
    }
}
