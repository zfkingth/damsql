using System;
using System.Collections.Generic;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
using System.Runtime.InteropServices;

namespace hammergo.DataImport
{
    public class ExcelHelper:IDisposable
    {
        protected Excel.Application excel = null;//excel ����

      
        public void initialExcel()
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


        public void closeWorkbooks()
        {
            if (excel != null)
            {
                excel.Workbooks.Close();
            }
        }

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID);
        public static void Kill(Microsoft.Office.Interop.Excel.Application excel)
        {
            IntPtr t = new IntPtr(excel.Hwnd);   //�õ������������������ǵõ�����ڴ���� 
            int k = 0;
            GetWindowThreadProcessId(t, out k);   //�õ�������Ψһ��־k
            System.Diagnostics.Process p = System.Diagnostics.Process.GetProcessById(k);   //�õ��Խ���k������
            p.Kill();     //�رս���k
        }


        public void QuitExcel()
        {


            if (excel != null)
            {
                setExcelVisible(false, excel);//����ʾ�ر�ʱ����ʾ��Ϣ

                excel.Workbooks.Close();

                excel.Quit();

                Kill(excel);
                //int generation = System.GC.GetGeneration(excel);

                //System.Runtime.InteropServices.Marshal.ReleaseComObject(excel);


                //excel = null;
                //System.GC.Collect(generation);
            }
        }


        public  object[,] getArrayValue(Excel.Worksheet ws, int startRowIndex, int startColIndex, int rowCnt, int colCnt)
        {
            object cell1 = ws.Cells[startRowIndex, startColIndex];
            object cell2 = ws.Cells[startRowIndex + rowCnt - 1, startColIndex + colCnt - 1];
            Excel.Range ra = ws.Range[cell1, cell2];
            return (object[,])ra.get_Value(Type.Missing);


        }


        public void setExcelVisible(bool visible, Excel.Application aexcel)
        {
            if (aexcel != null)
            {
                aexcel.ScreenUpdating = visible;


                aexcel.Visible = visible;
            }
        }

        /// <summary>
        /// ��ù�����
        /// </summary>
        /// <returns></returns>
        public Excel.Workbook getWorkbook()
        {
            Excel.Workbook wb = null;

            if (excel.Workbooks.Count == 0)
            {
                wb = addWorkbook();
            }
            else
            {
                excel.Workbooks[1].Activate();  //com �������Ǵ�1��ʼ��


                wb = excel.ActiveWorkbook;
            }

            return wb;
        }

        public Excel.Workbook addWorkbook()
        {
            Excel.Workbook wb = excel.Workbooks.Add(Type.Missing);

            Excel.Worksheet ws = (Excel.Worksheet)wb.ActiveSheet;



            return wb;

        }


        public  Excel.Workbook openWorkbookWithoutDisplay(string fileName)
        {
            Excel.Workbook wb = excel.Workbooks.Open(fileName, Excel.XlUpdateLinks.xlUpdateLinksNever, false,
               5, "", "", false, Excel.XlPlatform.xlWindows, Type.Missing, true, true, 0, false, true, Excel.XlCorruptLoad.xlNormalLoad);

            return wb;
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
