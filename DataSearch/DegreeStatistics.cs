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

namespace hammergo.DataSearch
{
    public partial class DegreeStatistics : DevExpress.XtraEditors.XtraUserControl, hammergo.Utility.IShowAppData
    {
        public DegreeStatistics()
        {
            InitializeComponent();
        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //(gridControl1.MainView as DevExpress.XtraGrid.Views.Grid.GridView).CopyToClipboard();
            Utility.Utility.copyGridSelection(gridControl1.MainView as DevExpress.XtraGrid.Views.Grid.GridView);

        }

        #region IShowAppData 成员

        public event EventHandler<hammergo.Utility.AppSearchEventArgs> ShowDataEvent;

        #endregion

        private void appSelector1_ShowDataEvent(object sender, hammergo.Utility.AppSearchEventArgs e)
        {
            if (ShowDataEvent != null)
            {
                ShowDataEvent(sender, e);
            }
        }

        private void DegreeStatistics_Load(object sender, EventArgs e)
        {
            appSelector1.initial();

            c1DateEdit1.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            c1DateEdit1.Properties.DisplayFormat.FormatString = PubConstant.customString;

            c1DateEdit2.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            c1DateEdit2.Properties.DisplayFormat.FormatString = PubConstant.customString;
        }

        private void appSelector1_SearchExeClick(object sender, hammergo.CommonControl.AppsEventArgs e)
        {
            List<string> appNameList = e.AppNameList;

         

            DateTime? startDate = null, endDate = null;
            if (c1DateEdit1.EditValue != null)
            {
                startDate = c1DateEdit1.DateTime;
            }
            if (c1DateEdit2.EditValue != null)
            {
                endDate = c1DateEdit2.DateTime;
            }

            DataTable dt = new DataTable();
            dt.Columns.Add(PubConstant.appColumnName, typeof(string));
            dt.Columns.Add("测次", typeof(string));

            foreach (string appName in appNameList)
            {
                AppIntegratedInfo appInfo = new AppIntegratedInfo(appName, 0, startDate, endDate);

                DataRow row = dt.NewRow();
                row.BeginEdit();
                row[PubConstant.appColumnName] = appName;
                int cnt = appInfo.CalcParams.Count;
                if (cnt != 0)
                {
                    cnt = appInfo.CalcValues.Count / cnt;
                }

                row["测次"] = cnt.ToString();
                row.EndEdit();
                dt.Rows.Add(row);
            }
            dt.AcceptChanges();

            gridControl1.DataSource = dt;
            (gridControl1.MainView as DevExpress.XtraGrid.Views.Grid.GridView).BestFitColumns();
        }


    }
}
