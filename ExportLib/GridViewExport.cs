using System;
using System.Collections.Generic;
using DevExpress.XtraGrid.Export;
using DevExpress.XtraExport;
using DevExpress.XtraEditors;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;

namespace hammergo.ExportLib
{
    public class GridViewExport
    {
        GridView view = null;
        public string name = "default";
        public GridViewExport(GridView view, string name)
        {
            this.view = view;
            this.name = name;

        }
        //<sbExportToHTML>
        //public void sbExportToHTML_Click(object sender, System.EventArgs e)
        //{
        //    string fileName = ShowSaveFileDialog("HTML Document", "HTML Documents|*.html");
        //    if (fileName != "")
        //    {
        //        Cursor currentCursor = Cursor.Current;
        //        Cursor.Current = Cursors.WaitCursor;

        //        view.ExportToMht(fileName);



        //        Cursor.Current = currentCursor;




              
        //        OpenFile(fileName);
        //    }
        //}
        //</sbExportToHTML>

        //<sbExportToXML>
        public void sbExportToXML_Click(object sender, System.EventArgs e)
        {
            string fileName = ShowSaveFileDialog("XML Document", "XML Documents|*.xml");
            if (fileName != "")
            {
                ExportTo(new ExportXmlProvider(fileName));
                OpenFile(fileName);
            }
        }
        public void sbExportToXLS(string fileName,string appName)
        {
            DevExpress.XtraPrinting.XlsExportOptions opt = new DevExpress.XtraPrinting.XlsExportOptions();
            opt.SheetName = appName;
            view.ExportToXls(fileName, opt);
        }
        //</sbExportToXML>

        //<sbExportToXLS>
        public void sbExportToXLS_Click(object sender, System.EventArgs e)
        {
            string fileName = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xls");
            if (fileName != "")
            {
                Cursor currentCursor = Cursor.Current;
                Cursor.Current = Cursors.WaitCursor;

                sbExportToXLS(fileName, name);



                Cursor.Current = currentCursor;




                OpenFile(fileName);
               
            }
        }
        //</sbExportToXLS>

        //<sbExportToTXT>
        public void sbExportToTXT_Click(object sender, System.EventArgs e)
        {
            string fileName = ShowSaveFileDialog("Text Document", "Text Files|*.txt");
            if (fileName != "")
            {

                Cursor currentCursor = Cursor.Current;
                Cursor.Current = Cursors.WaitCursor;

                view.ExportToText(fileName);

                

                Cursor.Current = currentCursor;


                

                OpenFile(fileName);
            }
        }
        //</sbExportToTXT>

        public void sbExportToXml_Scheme_Click(object sender, System.EventArgs e)
        {
            string fileName = ShowSaveFileDialog("Xml and Scheme", "Xml and Scheme|*.xml");
            if (fileName != "")
            {
                //System.Data.DataView tempdv = view.DataSource as System.Data.DataView;
                //tempdv.Table.WriteXml(fileName, System.Data.XmlWriteMode.WriteSchema);
                System.Data.DataTable tempTable = view.GridControl.DataSource as System.Data.DataTable;

                // Presuming the DataTable has a column named Date.
                string expression = "";

                // Sort descending by column named CompanyName.
                string sortOrder = "时间 asc";
                System.Data.DataRow[] foundRows;

                // Use the Select method to find all rows matching the filter.
                foundRows = tempTable.Select(expression, sortOrder);

                System.Data.DataTable outTable = tempTable.Clone();

                outTable.BeginLoadData();

                foreach (System.Data.DataRow row in foundRows)
                {
                    outTable.ImportRow(row);
                }

                outTable.TableName = "数据";

                outTable.EndLoadData();



                outTable.WriteXml(fileName, System.Data.XmlWriteMode.WriteSchema);
                OpenFile(fileName);
            }
        }

        /// <summary>
        /// 设置导出文件的默认名称
        /// </summary>
        /// <param name="name"></param>
        public void setName(string name)
        {
            this.name = name;
        }

        private void OpenFile(string fileName)
        {
            if (XtraMessageBox.Show("Do you want to open this file?", "Export To...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (System.Diagnostics.Process process = new System.Diagnostics.Process())
                    {
                        process.StartInfo.FileName = fileName;
                        process.StartInfo.Verb = "Open";
                        process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                        process.Start();
                    }
                }
                catch
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show("Cannot find an application on your system suitable for openning the file with exported data.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //<sbExportToHTML>
        private void ExportTo(IExportProvider provider)
        {
            Cursor currentCursor = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;

            view.GridControl.FindForm().Refresh();
           
            BaseExportLink link = view.CreateExportLink(provider);
            //link.ExpandAll = false;
            link.ExportTo(true);
            provider.Dispose();
            
            Cursor.Current = currentCursor;
        }
        //</sbExportToHTML>

        private string ShowSaveFileDialog(string title, string filter)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            //  string name = Application.ProductName;
            //int n = name.LastIndexOf(".") + 1;
            //  if (n > 0) name = name.Substring(n, name.Length - n);
            dlg.Title = "Export To " + title;
            dlg.FileName = name;
            dlg.Filter = filter;
            if (dlg.ShowDialog() == DialogResult.OK) return dlg.FileName;
            return "";
        }

    }
}
