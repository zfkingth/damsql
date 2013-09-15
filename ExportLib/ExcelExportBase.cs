using System;
using System.Collections.Generic;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace hammergo.ExportLib
{
    public class ExcelExportBase:IDisposable
    {
       

         protected Excel.Application excel = null;//excel ����

        /// <summary>
        /// ����ֵ����ʾֵ֮���ת��
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
            if (excel == null)//��excelû�б���ʼ��ʱ�Ž��г�ʼ��
            {
                excel = new Excel.Application();
                if (excel == null)
                {
                    throw new Exception("����: EXCEL ������ȷ����");

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
                setExcelVisible(false, excel);//����ʾ�ر�ʱ����ʾ��Ϣ

                excel.Workbooks.Close();

                excel.Quit();

                int generation = System.GC.GetGeneration(excel);
                excel = null;
                System.GC.Collect(generation);
            }
        }

        /// <summary>
        /// ��ù�����
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
                ((Excel.Worksheet) excel.Workbooks[1]).Activate();  //com �������Ǵ�1��ʼ��
                

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


            ws.Columns.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;//���ж���
            ws.Columns.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
        }

        /// <summary>
        /// �򿪲���ʾexcel��
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

            excel.Workbooks.Close();//�رչ�����

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
        /// ����worksheet������
        /// </summary>
        /// <param name="ws">excel��</param>
        /// <param name="cell1rowIndex">��ʼ������</param>
        /// <param name="cell1colIndex">��ʼ������</param>
        /// <param name="cell2rowIndex">����������</param>
        /// <param name="cell2colIndex">����������</param>
        /// <param name="val">����ֵ��Ҳ�����Ƕ�ά����</param>
        /// <param name="merge">�Ƿ�ϲ���Ԫ��</param>
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


       

        #region IDisposable ��Ա
        // �Ѿ���������ı��
        private bool _alreadyDisposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(true);

        }

        // �����Dispose����
        protected virtual void Dispose(bool isDisposing)
        {
            // ��Ҫ��δ���
            if (_alreadyDisposed)
                return;
            if (isDisposing)
            {
                // TODO: �˴��ͷ��ܿ���Դ

                QuitExcel();
               
            }
            // TODO: �˴��ͷŷ��ܿ���Դ�����ñ���������
            _alreadyDisposed = true;
        }


        #endregion


    }
}
