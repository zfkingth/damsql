using System;
using System.Collections.Generic;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace hammergo.ExportLib
{
    public class ExcelExportBase:IDisposable
    {
       

         protected Excel.Application excel = null;//excel 程序

        /// <summary>
        /// 从真值到显示值之间的转化
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public  object handleValue(object obj)
        {
            object ret = obj;
            if (obj == null || obj.ToString().Trim().Length == 0)
            {
                ret = hammergo.GlobalConfig.PubConstant.ConfigData.NoDataConvertString;
            }
            else if (obj.ToString() == double.NaN.ToString())
            {
                ret = "";
            }
            else if (obj is double && Utility.Utility.isErrorValue((double)obj))
            {
                ret = hammergo.GlobalConfig.PubConstant.ConfigData.ErrorConvertString;
            }

            return ret;
        }

        public  void initialExcel()
        {
            if (excel == null)//当excel没有被初始化时才进行初始化
            {
                excel = new Excel.Application();
                if (excel == null)
                {
                    throw new Exception("错误: EXCEL 不能正确启动");

                }
                excel.ScreenUpdating = false;
                excel.DisplayAlerts = false;

                excel.Visible = false;




            }
        }




        public  void QuitExcel()
        {


            if (excel != null)
            {
                setExcelVisible(false, excel);//不显示关闭时的提示信息

                excel.Workbooks.Close();

                excel.Quit();

                int generation = System.GC.GetGeneration(excel);
                excel = null;
                System.GC.Collect(generation);
            }
        }

        /// <summary>
        /// 获得工作表
        /// </summary>
        /// <returns></returns>
        public  Excel.Workbook getWorkbook()
        {
            Excel.Workbook wb = null;

            if (excel.Workbooks.Count == 0)
            {
                wb = addWorkbook();
            }
            else
            {
                ((Excel.Worksheet) excel.Workbooks[1]).Activate();  //com 的数据是从1开始的
                

                wb = excel.ActiveWorkbook;
            }

            return wb;
        }

        public  Excel.Workbook addWorkbook()
        {
            Excel.Workbook wb = excel.Workbooks.Add(Type.Missing);

            Excel.Worksheet ws = (Excel.Worksheet)wb.ActiveSheet;

           

            return wb;

        }

        protected void setDefaultFormat(Excel.Worksheet ws)
        {
            ws.Columns.Font.Size = 10;

            ws.Columns.Font.Name = "Times New Roman";


            ws.Columns.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;//居中对齐
            ws.Columns.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
        }

        /// <summary>
        /// 打开并显示excel表
        /// </summary>
        /// <param name="fileName"></param>
        public  void openAndShowExcel(string fileName)
        {
            Excel.Workbook wb = excel.Workbooks.Open(fileName, Excel.XlUpdateLinks.xlUpdateLinksNever, false,
                5, "", "", false, Excel.XlPlatform.xlWindows, Type.Missing, true, true, 0, false, true, Excel.XlCorruptLoad.xlNormalLoad);



            wb.Activate();

            excel.Visible = true;

        }

        public  void setExcelVisible(bool visible)
        {
            setExcelVisible(visible, excel);
        }

        public  void setExcelVisible(bool visible, Excel.Application aexcel)
        {
            if (aexcel != null)
            {
                aexcel.ScreenUpdating = visible;


                aexcel.Visible = visible;
            }
        }
        public  void closeAndSaveWorkbook(Excel.Workbook wb, string fileName)
        {

            excel.ScreenUpdating = true;


            Excel.Worksheet ws = (Excel.Worksheet)wb.ActiveSheet;






            wb.SaveAs(fileName, Excel.XlFileFormat.xlWorkbookNormal, Missing.Value, Missing.Value,
                false, false, Excel.XlSaveAsAccessMode.xlNoChange,
                Excel.XlSaveConflictResolution.xlLocalSessionChanges, false, Missing.Value, Missing.Value, Missing.Value);

            excel.Workbooks.Close();//关闭工作本

        }



     
    


        //public  string unitFeildName = "unit";
        //public  string preciseFeildName = "precise";
       




        protected  Excel.Range setArrayValue(Excel.Worksheet ws, object[,] datas, int startRowIndex, int startColIndex)
        {
            int rowsCount = datas.GetLength(0);
            int colsCount = datas.GetLength(1);

            Excel.Range ra = setValue(ws, startRowIndex, startColIndex, startRowIndex + rowsCount - 1, startColIndex + colsCount - 1, datas, false);


            //wb.Activate();
            return ra;
        }



        /// <summary>
        /// 设置worksheet的内容
        /// </summary>
        /// <param name="ws">excel表</param>
        /// <param name="cell1rowIndex">起始行索引</param>
        /// <param name="cell1colIndex">起始列索引</param>
        /// <param name="cell2rowIndex">结束行索引</param>
        /// <param name="cell2colIndex">结束列索引</param>
        /// <param name="val">数据值，也可以是二维数据</param>
        /// <param name="merge">是否合并单元格</param>
        /// <returns></returns>
        protected  Excel.Range setValue(Excel.Worksheet ws, int cell1rowIndex, int cell1colIndex, int cell2rowIndex, int cell2colIndex, object val, bool merge)
        {

            object cell1 = ws.Cells[cell1rowIndex, cell1colIndex];
            object cell2 = ws.Cells[cell2rowIndex, cell2colIndex];

            Excel.Range ra = ws.Range[cell1, cell2];

            if (merge)
                ra.Merge(false);

            ra.Value2 = val;

            return ra;



        }


        public Excel.Range mergeAndSetValue(Excel.Worksheet ws, int cell1rowIndex, int cell1colIndex, int cell2rowIndex, int cell2colIndex, object val, bool merge)
        {

            object cell1 = ws.Cells[cell1rowIndex, cell1colIndex];
            object cell2 = ws.Cells[cell2rowIndex, cell2colIndex];

            Excel.Range ra = ws.Range[cell1, cell2];

            if (merge)
                ra.Merge(false);

            ra.Value2 = val;

            return ra;



        }


       

        #region IDisposable 成员
        // 已经被处理过的标记
        private bool _alreadyDisposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);

        }

        // 虚拟的Dispose方法
        protected virtual void Dispose(bool isDisposing)
        {
            // 不要多次处理
            if (_alreadyDisposed)
                return;
            if (isDisposing)
            {
                // TODO: 此处释放受控资源

                QuitExcel();
               
            }
            // TODO: 此处释放非受控资源。设置被处理过标记
            _alreadyDisposed = true;
        }


        #endregion


    }
}
