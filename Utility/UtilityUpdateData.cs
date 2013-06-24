using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using hammergo.Model;
using hammergo.Tracking;
using hammergo.GlobalConfig;
using DevExpress.XtraGrid.Views.Grid;
using System.Windows.Forms;


namespace hammergo.Utility
{
    public class UtilityUpdateData
    {

        /// <summary>
        /// ����������Ϣ��ʱ��ɾ�����ݼ�¼
        /// </summary>
        /// <param name="appInfo"></param>
        /// <param name="dateArray"></param>
        public static void deleteRecord(AppIntegratedInfo appInfo, List<DateTime> dateArray)
        {

            
                List<MessureValue> mesDelValues = new List<MessureValue>(100);
                List<CalculateValue> calcDelValues = new List<CalculateValue>(100);
                List<Remark> delRemarks = new List<Remark>(50);
                //ɾ������ֵ
                foreach (DateTime date in dateArray)
                {
                    mesDelValues.AddRange(appInfo.MessureValues.FindAll(delegate(MessureValue item)
                    {
                        return item.Date.Value == date;
                    }));

                    calcDelValues.AddRange(appInfo.CalcValues.FindAll(delegate(CalculateValue item)
                    {
                        return item.Date.Value == date;
                    }));

                    delRemarks.AddRange(appInfo.Remarks.FindAll(delegate(Remark item)
                        {
                            return item.Date == date;
                        }));



                }


                hammergo.BLL.MessureValueBLL mesValueBLL = new hammergo.BLL.MessureValueBLL();
                hammergo.BLL.CalculateValueBLL calcValueBLL = new hammergo.BLL.CalculateValueBLL();
                hammergo.BLL.RemarkBLL remarkBLL = new hammergo.BLL.RemarkBLL();

              


                using (System.Data.IDbConnection connection = hammergo.ConnectionPool.Pool.GetOpenConnection())
                {
                    //connection.Open();

                    // Start a local transaction.
                    System.Data.IDbTransaction trans = connection.BeginTransaction();


                    try
                    {
                        foreach (MessureValue item in mesDelValues)
                        {
                            mesValueBLL.Delete(item, trans);
                        }

                        foreach (CalculateValue item in calcDelValues)
                        {
                            calcValueBLL.Delete(item, trans);
                        }

                        foreach (Remark item in delRemarks)
                        {
                            remarkBLL.Delete(item, trans);
                        }




                        trans.Commit();
                        //Console.WriteLine("Both records are written to database.");
                    }
                    catch (Exception ex)
                    {


                        // Attempt to roll back the transaction.

                        trans.Rollback();
                        throw ex;
                    }
                }




        }


        /// <summary>
        /// ����GridView ��cell Value Changed,�������ڸ�����ʾ��DataTable����̨���ݿ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="appInfo"></param>
        /// <param name="update">�Ƿ���º�̨���ݿ�</param>
        public static void handleCellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e, AppIntegratedInfo appInfo, bool update)
        {
            GridView gridView = sender as GridView;

            int handle = e.RowHandle;

            DataRow drv = gridView.GetDataRow(handle);

            DateTime date = (DateTime)drv[PubConstant.timeColumnName];

            if (e.Column.FieldName == PubConstant.remarkColumnName)
            {
                hammergo.BLL.RemarkBLL remarkBLL = new hammergo.BLL.RemarkBLL();
                Remark remark = appInfo.Remarks.Find(delegate(Remark item) { return item.Date.Value == date; });


                if (e.Value is string && ((string)e.Value).Trim().Length != 0)
                {



                    if (remark == null)
                    {

                        //create new object
                        remark = new Remark();

                        remark.Date = date;
                        remark.AppName = appInfo.appName;

                        appInfo.Remarks.Add(remark);

                    }

                    remark.RemarkText = (e.Value as string).Trim();


                }
                else
                {

                    if (remark != null)
                    {
                        appInfo.Remarks.Remove(remark);
                    }
                }

                if (update)
                    remarkBLL.UpdateList(appInfo.Remarks);


            }
            else
            {

                //���¼������ֵ������ӳ��gridview��
                redirectToObjects(appInfo, drv, e.Column.FieldName);


                ////�����ݿ��������ر�
                if (update)
                    ThreadParameters.startNewThreadToUpdateData(appInfo, date);


            }



        }


        //���¼������ֵ������ӳ��gridview��
        public static void redirectToObjects(AppIntegratedInfo appInfo, DataRow drv, string columnName)
        {
            DateTime date = (DateTime)drv[PubConstant.timeColumnName];

            MessureParam mp = appInfo.MessureParams.Find(delegate(MessureParam item) { return item.ParamName == columnName; });


            if (mp != null)
            {


                MessureValue editedValue = appInfo.MessureValues.Find(delegate(MessureValue item)
                {
                    return item.Date.Value == date && item.MessureParamID == mp.MessureParamID;
                });

                if (editedValue == null)
                {
                    //create new object
                    editedValue = new MessureValue();
                    editedValue.Date = date;
                    editedValue.MessureParamID = mp.MessureParamID;
                    appInfo.MessureValues.Add(editedValue);
                }


                editedValue.Val = (double)drv[columnName];

                //recalculate the calc values
                reCalcValues(appInfo, drv);

            }
            else
            {
                CalculateParam cp = appInfo.CalcParams.Find(delegate(CalculateParam item)
                {
                    return item.ParamName == columnName;
                });

                if (cp != null)
                {



                    CalculateValue calcValue = appInfo.CalcValues.Find(delegate(CalculateValue item)
                   {
                       return item.Date.Value == date && item.CalculateParamID == cp.CalculateParamID;
                   });

                    if (calcValue == null)
                    {
                        //create new object
                        calcValue = new CalculateValue();
                        calcValue.Date = date;
                        calcValue.CalculateParamID = cp.CalculateParamID;
                        appInfo.CalcValues.Add(calcValue);
                    }

                    calcValue.Val = (double)drv[columnName];


                }
            }

            //may be need reset the filter to null

        }


        /// <summary>
        /// ����messure values������,��Ҫ���¼���calc values��ֵ
        /// </summary>
        /// <param name="appInfo"></param>
        /// <param name="currentRow"></param>
        public static void reCalcValues(AppIntegratedInfo appInfo, DataRow currentRow)
        {
            hammergo.caculator.CalcFunction calc = new hammergo.caculator.CalcFunction();



            hammergo.caculator.MyList list = new hammergo.caculator.MyList();

            DateTime date = (DateTime)currentRow[PubConstant.timeColumnName];

            //��䱾�����ĳ�������
            foreach (ConstantParam cp in appInfo.ConstantParams)
            {
                object obj = cp.Val;
                double val = 0;       //û�����ݵĻ�Ĭ��ֵ��0
                if (obj is double)
                    val = (double)obj;

                list.add(cp.ParamSymbol, val);

            }

            //��䱾�����Ĳ�������
            foreach (MessureParam mp in appInfo.MessureParams)
            {
                object obj = currentRow[mp.ParamName];
                double val = 0;
                if (obj is double)
                    val = (double)obj;

                list.add(mp.ParamSymbol, val);
            }

            //�����������Ƿ���������������������
            hammergo.caculator.MyList appCalcNameList = new hammergo.caculator.MyList();//���õ������������Ƶļ���



            //������밴��calc order ��˳�� 
            foreach (CalculateParam cp in appInfo.CalcParams)
            {
                string formula = cp.CalculateExpress;//��ȡ���ʽ
                System.Collections.ArrayList vars = calc.getVaribles(formula);
                //Ϊ�˱����ĳ�����������ظ���ѯ,��һ�������һ֧����������


                for (int j = 0; j < vars.Count; j++)
                {
                    string vs = (string)vars[j];
                    int pos = vs.IndexOf('.');
                    if (pos != -1)
                    {
                        string otherID = vs.Substring(0, pos);
                        appCalcNameList.add(otherID, 0);
                        //�������ڼ���ʱ������������������������û�д˿̵ļ�¼ʱ�������쳣
                        list.add(vs, 0);
                    }
                }
            }

            //������Ĳ���
            for (int i = 0; i < appCalcNameList.Length; i++)
            {

                AppIntegratedInfo simpleAppInfo = new AppIntegratedInfo(appCalcNameList.getKey(i), date);
                UtilityGetData.fillListByCalcName_Date(list, appCalcNameList.getKey(i), date, true, simpleAppInfo);

            }

            //���Խ��б��ʽ��ֵ��


            appInfo.CalcParams.Sort(new CalculateOrderComparer());
            foreach (CalculateParam cp in appInfo.CalcParams)
            {
                //˳������order ��������
                string formula = cp.CalculateExpress;//��ȡ���ʽ

                double v = calc.compute(formula, list);

                byte precision = cp.PrecisionNum.Value;

                if (precision >= 0)
                {
                    v = Utility.round(v, precision);
                }



                CalculateValue calcValue = appInfo.CalcValues.Find(delegate(CalculateValue item)
                    {
                        return item.Date.Value == date && item.CalculateParamID == cp.CalculateParamID;
                    }); ;

                if (calcValue == null)
                {
                    //create new object
                    calcValue = new CalculateValue();
                    calcValue.Date = date;
                    calcValue.CalculateParamID = cp.CalculateParamID;

                    appInfo.CalcValues.Add(calcValue);
                }

                calcValue.Val = v;


                list.add(cp.ParamSymbol, v);

                currentRow[cp.ParamName] = v;
            }

        }

        /// <summary>
        /// ��ʽ��������
        /// </summary>
        /// <param name="sn"></param>
        /// <param name="date"></param>
        public static void reCalculateLink(string calculateName, DateTime date)
        {
            try
            {
                //��������ͼ
                ALGraph.MyGraph graph = new ALGraph.MyGraph();

                hammergo.BLL.ApparatusBLL appBLL = new hammergo.BLL.ApparatusBLL();
                appBLL.constructGraph(calculateName, graph);

                System.Collections.ArrayList toplist = graph.topSort();
                if (toplist.Count != graph.Vexnum)
                {
                    throw new Exception("������ʽ����ѭ������");
                }

                //��������˳�����θ���

                for (int i = 0; i < toplist.Count; i++)
                {
                    string csn = (string)toplist[i];
                    //���ظ��±���
                    if (csn != calculateName)
                        reCalcAppByDate(csn, date);
                }


            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, "���¼������", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Utility.log(ex);
            }

        }


        /// <summary>
        /// ���¼�������������
        /// </summary>
        /// <param name="calculateName">��������</param>
        /// <param name="date">ʱ��</param>
        public static void reCalcAppByDate(string calculateName, DateTime date)
        {
            hammergo.caculator.CalcFunction calc = new hammergo.caculator.CalcFunction();



            hammergo.caculator.MyList list = new hammergo.caculator.MyList();
            //�ȳ�ʼ�����ݼ��еĲ������ŵ�ֵ

            hammergo.caculator.MyList appCalcNameList = new hammergo.caculator.MyList();//����ű�

            
            //�ڴ����ݿ���ȡ��ʱ������������仯�����������ݸ������ύ�����ݿ�
            AppIntegratedInfo appInfo = new AppIntegratedInfo(calculateName, date);
             if (appInfo.CalcValues.Count != 0)
            {  //ֻ�����ݴ���ʱ��������

                UtilityGetData.fillListByCalcName_Date(list, calculateName, date, false,appInfo);

          

                foreach (CalculateParam cp in appInfo.CalcParams)
                {
                    string formula = cp.CalculateExpress;//��ȡ���ʽ
                    System.Collections.ArrayList vars = calc.getVaribles(formula);
                    //Ϊ�˱����ĳ�����������ظ���ѯ,��һ�������һ֧����������


                    for (int j = 0; j < vars.Count; j++)
                    {
                        string vs = (string)vars[j];
                        int pos = vs.IndexOf('.');
                        if (pos != -1)
                        {
                            string otherID = vs.Substring(0, pos);
                            appCalcNameList.add(otherID, 0);
                            //�������ڼ���ʱ������������������������û�д˿̵ļ�¼ʱ�������쳣
                            list.add(vs, 0);
                        }
                    }
                }

                //������Ĳ���
                for (int i = 0; i < appCalcNameList.Length; i++)
                {

                    AppIntegratedInfo simpleAppInfo = new AppIntegratedInfo(appCalcNameList.getKey(i), date);
                    UtilityGetData.fillListByCalcName_Date(list, appCalcNameList.getKey(i), date, true,simpleAppInfo);

                }

                //���Խ��б��ʽ��ֵ��



                //������밴��calc order ��˳��,��������fillListByCalcName_Date��������
                foreach (CalculateParam cp in appInfo.CalcParams)
                {

                    string formula = cp.CalculateExpress;//��ȡ���ʽ

                    double v = calc.compute(formula, list);

                    byte precision = cp.PrecisionNum.Value;

                    if (precision >= 0)
                    {
                        v = Utility.round(v, precision);
                    }



                    CalculateValue calcValue = appInfo.CalcValues.Find(delegate(CalculateValue item)
                       {
                           return item.Date.Value == date && item.CalculateParamID == cp.CalculateParamID;
                       });


                    //if (calcValue == null)
                    //{

                    //    //create new object
                    //    calcValue = new CalculateValue();
                    //    calcValue.Date = date;
                    //    calcValue.CalculateParamID = cp.CalculateParamID;

                    //    appInfo.CalcValues.Add(calcValue);
                    //}

                    //calcValue.Val = v;

                    if (calcValue != null)
                    {
                        calcValue.Val = v;
                    }


                    list.add(cp.ParamSymbol, v);


                }

                appInfo.Update();
            }


        }



    }
}
