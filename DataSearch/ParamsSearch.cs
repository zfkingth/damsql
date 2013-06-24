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
using hammergo.GlobalConfig;

namespace hammergo.DataSearch
{
    public partial class ParamsSearch : DevExpress.XtraEditors.XtraUserControl, hammergo.Utility.IShowAppData
    {
        public ParamsSearch()
        {
            InitializeComponent();
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

        private void appSelector1_Load(object sender, EventArgs e)
        {
            appSelector1.initial();
        }

        private void appSelector1_SearchExeClick(object sender, hammergo.CommonControl.AppsEventArgs e)
        {
            List<string> appNameList = e.AppNameList;

            List<AppIntegratedInfo> appInfoList = new List<AppIntegratedInfo>(appNameList.Count);

            int maxParamsCnt = 0;
            foreach (string appName in appNameList)
            {
                AppIntegratedInfo appInfo = new AppIntegratedInfo(appName, 0, null, null);

                appInfoList.Add(appInfo);

                if (appInfo.ConstantParams.Count > maxParamsCnt)
                {
                    maxParamsCnt = appInfo.ConstantParams.Count;
                }

            }

            DataTable dt = new DataTable();
            dt.Columns.Add(PubConstant.appColumnName, typeof(string));
            for (int i = 0; i < maxParamsCnt; i++)
            {
                //参数的名称
                dt.Columns.Add((2 * i+1).ToString(), typeof(string));//第一列为测点编号

                //参数的值
                dt.Columns.Add((2 * i + 1+1).ToString(), typeof(string));
            }



            foreach (AppIntegratedInfo appInfo in appInfoList)
            {
                appInfo.ConstantParams.Sort(new ConstantDisplayComparer());

                DataRow row = dt.NewRow();
                row.BeginEdit();

                row[PubConstant.appColumnName] = appInfo.appName;

                for (int i = 0; i < appInfo.ConstantParams.Count; i++)
                {
                    ConstantParam cp = appInfo.ConstantParams[i];
                    row[2 * i + 1] = cp.ParamName;
                    if (cp.Val != null)
                    {
                        row[2 * i + 1 + 1] = cp.Val.Value;
                    }

                }
                row.EndEdit();
                dt.Rows.Add(row);
            }

            dt.AcceptChanges();

            gridControl1.DataSource = dt;
            (gridControl1.MainView as DevExpress.XtraGrid.Views.Grid.GridView).BestFitColumns();

        }

        private void 复制ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //(gridControl1.MainView as DevExpress.XtraGrid.Views.Grid.GridView).CopyToClipboard();
            Utility.Utility.copyGridSelection(gridControl1.MainView as DevExpress.XtraGrid.Views.Grid.GridView);
        }
    }
}
