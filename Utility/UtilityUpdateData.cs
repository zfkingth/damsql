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
        /// 根据仪器信息和时间删除数据记录
        /// </summary>
        /// <param name="appInfo"></param>
        /// <param name="dateArray"></param>
        public static void deleteRecord(AppIntegratedInfo appInfo, List<DateTime> dateArray)
        {

            
                List<MessureValue> mesDelValues = new List<MessureValue>(100);
                List<CalculateValue> calcDelValues = new List<CalculateValue>(100);
                List<Remark> delRemarks = new List<Remark>(50);
                //删除测量值
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
        /// 处理GridView 的cell Value Changed,包括用于更新显示的DataTable及后台数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="appInfo"></param>
        /// <param name="update">是否更新后台数据库</param>
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

                //重新计算相关值，并反映在gridview中
                redirectToObjects(appInfo, drv, e.Column.FieldName);


                ////在数据库中相关相关表
                if (update)
                    ThreadParameters.startNewThreadToUpdateData(appInfo, date);


            }



        }


        //重新计算相关值，并反映在gridview中
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
        /// 由于messure values更改了,需要重新计算calc values的值
        /// </summary>
        /// <param name="appInfo"></param>
        /// <param name="currentRow"></param>
        public static void reCalcValues(AppIntegratedInfo appInfo, DataRow currentRow)
        {
            hammergo.caculator.CalcFunction calc = new hammergo.caculator.CalcFunction();



            hammergo.caculator.MyList list = new hammergo.caculator.MyList();

            DateTime date = (DateTime)currentRow[PubConstant.timeColumnName];

            //填充本仪器的常量参数
            foreach (ConstantParam cp in appInfo.ConstantParams)
            {
                object obj = cp.Val;
                double val = 0;       //没有数据的话默认值是0
                if (obj is double)
                    val = (double)obj;

                list.add(cp.ParamSymbol, val);

            }

            //填充本仪器的测量参数
            foreach (MessureParam mp in appInfo.MessureParams)
            {
                object obj = currentRow[mp.ParamName];
                double val = 0;
                if (obj is double)
                    val = (double)obj;

                list.add(mp.ParamSymbol, val);
            }

            //分析此仪器是否引用了其它仪器的数据
            hammergo.caculator.MyList appCalcNameList = new hammergo.caculator.MyList();//引用的其它仪器名称的集合



            //这里必须按照calc order 的顺序 
            foreach (CalculateParam cp in appInfo.CalcParams)
            {
                string formula = cp.CalculateExpress;//获取表达式
                System.Collections.ArrayList vars = calc.getVaribles(formula);
                //为了避免对某个测点的数据重复查询,将一次性填充一支仪器的数据


                for (int j = 0; j < vars.Count; j++)
                {
                    string vs = (string)vars[j];
                    int pos = vs.IndexOf('.');
                    if (pos != -1)
                    {
                        string otherID = vs.Substring(0, pos);
                        appCalcNameList.add(otherID, 0);
                        //避免由于计算时依赖其它仪器，而其它仪器没有此刻的记录时，导致异常
                        list.add(vs, 0);
                    }
                }
            }

            //填充带点的参数
            for (int i = 0; i < appCalcNameList.Length; i++)
            {

                AppIntegratedInfo simpleAppInfo = new AppIntegratedInfo(appCalcNameList.getKey(i), date);
                UtilityGetData.fillListByCalcName_Date(list, appCalcNameList.getKey(i), date, true, simpleAppInfo);

            }

            //可以进行表达式求值了


            appInfo.CalcParams.Sort(new CalculateOrderComparer());
            foreach (CalculateParam cp in appInfo.CalcParams)
            {
                //顺序是以order 升序排列
                string formula = cp.CalculateExpress;//获取表达式

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
        /// 链式更新数据
        /// </summary>
        /// <param name="sn"></param>
        /// <param name="date"></param>
        public static void reCalculateLink(string calculateName, DateTime date)
        {
            try
            {
                //生成拓扑图
                ALGraph.MyGraph graph = new ALGraph.MyGraph();

                hammergo.BLL.ApparatusBLL appBLL = new hammergo.BLL.ApparatusBLL();
                appBLL.constructGraph(calculateName, graph);

                System.Collections.ArrayList toplist = graph.topSort();
                if (toplist.Count != graph.Vexnum)
                {
                    throw new Exception("仪器公式存在循环依赖");
                }

                //根据拓扑顺序依次更新

                for (int i = 0; i < toplist.Count; i++)
                {
                    string csn = (string)toplist[i];
                    //不必更新本身
                    if (csn != calculateName)
                        reCalcAppByDate(csn, date);
                }


            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message, "重新计算错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Utility.log(ex);
            }

        }


        /// <summary>
        /// 重新计算仪器的数据
        /// </summary>
        /// <param name="calculateName">计算名称</param>
        /// <param name="date">时间</param>
        public static void reCalcAppByDate(string calculateName, DateTime date)
        {
            hammergo.caculator.CalcFunction calc = new hammergo.caculator.CalcFunction();



            hammergo.caculator.MyList list = new hammergo.caculator.MyList();
            //先初始化数据集中的参数符号的值

            hammergo.caculator.MyList appCalcNameList = new hammergo.caculator.MyList();//测点编号表

            
            //在从数据库中取数时，引起此仪器变化的仪器的数据更改已提交给数据库
            AppIntegratedInfo appInfo = new AppIntegratedInfo(calculateName, date);
             if (appInfo.CalcValues.Count != 0)
            {  //只有数据存在时才做更新

                UtilityGetData.fillListByCalcName_Date(list, calculateName, date, false,appInfo);

          

                foreach (CalculateParam cp in appInfo.CalcParams)
                {
                    string formula = cp.CalculateExpress;//获取表达式
                    System.Collections.ArrayList vars = calc.getVaribles(formula);
                    //为了避免对某个测点的数据重复查询,将一次性填充一支仪器的数据


                    for (int j = 0; j < vars.Count; j++)
                    {
                        string vs = (string)vars[j];
                        int pos = vs.IndexOf('.');
                        if (pos != -1)
                        {
                            string otherID = vs.Substring(0, pos);
                            appCalcNameList.add(otherID, 0);
                            //避免由于计算时依赖其它仪器，而其它仪器没有此刻的记录时，导致异常
                            list.add(vs, 0);
                        }
                    }
                }

                //填充带点的参数
                for (int i = 0; i < appCalcNameList.Length; i++)
                {

                    AppIntegratedInfo simpleAppInfo = new AppIntegratedInfo(appCalcNameList.getKey(i), date);
                    UtilityGetData.fillListByCalcName_Date(list, appCalcNameList.getKey(i), date, true,simpleAppInfo);

                }

                //可以进行表达式求值了



                //这里必须按照calc order 的顺序,这里是在fillListByCalcName_Date中已设置
                foreach (CalculateParam cp in appInfo.CalcParams)
                {

                    string formula = cp.CalculateExpress;//获取表达式

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
