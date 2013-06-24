using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using hammergo.Model;
using hammergo.Tracking;
using hammergo.GlobalConfig;

namespace hammergo.Utility
{
    public class MyComparer : System.Collections.Generic.IComparer<DateTime>
    {


        // Calls CaseInsensitiveComparer.Compare with the parameters reversed.
        //int IComparer.Compare(DateTime x, DateTime y)
        //{
        //   // return ((new CaseInsensitiveComparer()).Compare(y, x));
        //}

        public int Compare(DateTime x, DateTime y)
        {
            if (x.Ticks > y.Ticks)
            {
                return -1;
            }
            else if (x.Ticks == y.Ticks)
            {
                return 0;
            }
            else
            {
                return 1;
            }

        }

    }

    public class AppCollectionComparer : System.Collections.Generic.IComparer<AppCollection>
    {

        #region IComparer<AppCollection> ��Ա

        public int Compare(AppCollection x, AppCollection y)
        {
            if (x.Order > y.Order)
            {
                return 1;
            }
            else if (x.Order == y.Order)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }

        #endregion
    }



    public class ConstantDisplayComparer : System.Collections.Generic.IComparer<ConstantParam>
    {

        #region IComparer<ConstantParam> ��Ա

        public int Compare(ConstantParam x, ConstantParam y)
        {
            if (x.Order > y.Order)
            {
                return 1;
            }
            else if (x.Order == y.Order)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }

        #endregion
    }


    public class MessureDisplayComparer : System.Collections.Generic.IComparer<MessureParam>
    {

        #region IComparer<MessureParam> ��Ա

        public int Compare(MessureParam x, MessureParam y)
        {
            if (x.Order > y.Order)
            {
                return 1;
            }
            else if (x.Order == y.Order)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }

        #endregion
    }

    public class CalculdateDisplayComparer : System.Collections.Generic.IComparer<CalculateParam>
    {
        #region IComparer<CalculateParam> ��Ա

        public int Compare(CalculateParam x, CalculateParam y)
        {
            if (x.Order > y.Order)
            {
                return 1;
            }
            else if (x.Order == y.Order)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }

        #endregion
    }

    public class CalculateOrderComparer : System.Collections.Generic.IComparer<CalculateParam>
    {
        #region IComparer<CalculateParam> ��Ա

        public int Compare(CalculateParam x, CalculateParam y)
        {
            if (x.CalculateOrder > y.CalculateOrder)
            {
                return 1;
            }
            else if (x.CalculateOrder == y.CalculateOrder)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }

        #endregion
    }

    /// <summary>
    /// �Լ������ݵ�ֵ��ʱ�����˳������
    /// </summary>
    public class CalculateValueComparer : System.Collections.Generic.IComparer<CalculateValue>
    {

        #region IComparer<CalculateValue> ��Ա

        public int Compare(CalculateValue x, CalculateValue y)
        {
            if (x.Date.Value > y.Date.Value)
            {
                return 1;
            }
            else if (x.Date.Value == y.Date.Value)
            {
                return 0;
            }
            else
            {
                return -1;
            }
            
        }

        #endregion
    }


    /// <summary>
    /// �Լ������ݵ�ֵ��ʱ�������������
    /// </summary>
    public class CalculateValueReserveComparer : System.Collections.Generic.IComparer<CalculateValue>
    {

        #region IComparer<CalculateValue> ��Ա

        public int Compare(CalculateValue x, CalculateValue y)
        {
            if (x.Date.Value > y.Date.Value)
            {
                return -1;
            }
            else if (x.Date.Value == y.Date.Value)
            {
                return 0;
            }
            else
            {
                return 1;
            }

        }

        #endregion
    }


    public class UtilityGetData
    {

        /// <summary>
        /// ����������ݱ���
        /// </summary>
        /// <param name="appInfo"></param>
        /// <returns></returns>
        public static DataTable createDataTableSchema(AppIntegratedInfo appInfo)
        {
            DataTable dt = new DataTable();

            DataColumn dateColumn = new DataColumn(PubConstant.timeColumnName, typeof(DateTime));
            dateColumn.AllowDBNull = false;
            dateColumn.ReadOnly = true;
            dt.Columns.Add(dateColumn);
            dt.PrimaryKey = new DataColumn[] { dateColumn };



            foreach (MessureParam mp in appInfo.MessureParams)
            {
                DataColumn column = new DataColumn(mp.ParamName, typeof(double));
                dt.Columns.Add(column);
            }

            foreach (CalculateParam cp in appInfo.CalcParams)
            {
                DataColumn column = new DataColumn(cp.ParamName, typeof(double));
                dt.Columns.Add(column);
            }
            dt.Columns.Add(PubConstant.remarkColumnName, typeof(string)); //add remark column

            return dt;
        }
        /// <summary>
        /// ����AppIntegratedInfo����Ϣ������ϱ������������ݡ��������ݡ���ע
        /// </summary>
        /// <param name="appInfo">��Ż�õ�app information</param>
        /// <returns></returns>
        public static DataTable constructTable(AppIntegratedInfo appInfo)
        {
            if (appInfo == null) return null;

            int num = appInfo.topNum;

            TrackedList<MessureParam> messureParams = appInfo.MessureParams;
            TrackedList<CalculateParam> calcParams = appInfo.CalcParams;


            if ( calcParams.Count == 0)
            {
                return null;
            }


            //����
           
            calcParams.Sort(new CalculdateDisplayComparer());
            messureParams.Sort(new MessureDisplayComparer());



            //get messure values **************************************
            TrackedList<MessureValue> messureValues = appInfo.MessureValues;

     

            ////get calc values ****************************************
            TrackedList<CalculateValue> calcValues = appInfo.CalcValues;


            ////get remaks
            TrackedList<Remark> remarks = appInfo.Remarks;

            //add app intergrated information
            //******************************************************************
            //����Table
            DataTable dt = createDataTableSchema(appInfo);

           


            ////////////////////////calc order /////////////////////////
            

           

            SortedList<DateTime, object> timeList = null;

            //ʱ���ɽ���Զ����

            if (num > 0)
            {
                timeList = new SortedList<DateTime, object>(num, new MyComparer());
            }
            else
                timeList = new SortedList<DateTime, object>(300, new MyComparer());


            //�õ����е�ʱ��
            if (appInfo.MessureParams.Count != 0)
            {
                //�в������������
                foreach (MessureValue mv in messureValues)
                {
                    DateTime date = mv.Date.Value;
                    if (timeList.ContainsKey(date) == false)
                    {
                        timeList.Add(date, null);
                    }
                }
            }
            else
            {
                //û�в������������

                foreach (CalculateValue cv in calcValues)
                {
                    DateTime date = cv.Date.Value;
                    if (timeList.ContainsKey(date) == false)
                    {
                        timeList.Add(date, null);
                    }
                }
            }


            int temp = int.MaxValue;
            if (num > 0)
            {
                temp = num;
            }

            for (int index = 0; index < timeList.Count && index < temp; index++)
            {
                DataRow row = dt.NewRow();

                DateTime date = timeList.Keys[index];
                row[PubConstant.timeColumnName] = date;

             

                foreach (MessureValue mv in messureValues.FindAll(delegate(MessureValue item){return item.Date.Value==date;}))
                {
                    string aParamName = messureParams.Find(delegate(MessureParam item) { return item.MessureParamID == mv.MessureParamID; }).ParamName; //mv.messureParam.ParamName;
                    if (mv.Val != null)
                    {
                        row[aParamName] = mv.Val;
                    }
                }

               

                foreach (CalculateValue cv in calcValues.FindAll(delegate(CalculateValue item){return item.Date.Value==date;}))
                {
                    string aParamName = calcParams.Find(delegate(CalculateParam item) { return item.CalculateParamID == cv.CalculateParamID; }).ParamName; //cv.calculateParam.ParamName;
                    if (cv.Val != null)
                    {
                        row[aParamName] = cv.Val;
                    }
                }

                Remark remark =remarks.Find(delegate(Remark item) { return item.Date.Value == date; });
                if (remark!=null)
                {
                    row[PubConstant.remarkColumnName] = remark.RemarkText;
                }
                dt.Rows.Add(row);

            }

            dt.AcceptChanges();

            return dt;

        }

        /// <summary>
        /// ����AppIntegratedInfo�ļ������ݴ�����ȫ������AppIntegratedInfo����Ҫʱ�Ż��ѯ���ص㣬��˱�ʹ��constructTable������������
        /// </summary>
        /// <param name="appInfo"></param>
        /// <returns></returns>
        public static DataTable constructTableByCalcValues(AppIntegratedInfo appInfo)
        {
            if (appInfo == null) return null;

            int num = appInfo.topNum;

           
            TrackedList<CalculateParam> calcParams = appInfo.CalcParams;


            if (calcParams.Count == 0)
            {
                return null;
            }


            //����

            calcParams.Sort(new CalculdateDisplayComparer());
           


            ////get calc values ****************************************
            TrackedList<CalculateValue> calcValues = appInfo.CalcValues;


            //add app intergrated information
            //******************************************************************
            //����Table
            DataTable dt = new DataTable();

            DataColumn dateColumn = new DataColumn(PubConstant.timeColumnName, typeof(DateTime));
            dateColumn.ReadOnly = true;
            dt.Columns.Add(dateColumn);
            dt.PrimaryKey = new DataColumn[] { dateColumn };



      

            foreach (CalculateParam cp in calcParams)
            {
                DataColumn column = new DataColumn(cp.ParamName, typeof(double));
                dt.Columns.Add(column);
            }
            dt.Columns.Add(PubConstant.remarkColumnName, typeof(string)); //add remark column


            ////////////////////////calc order /////////////////////////

            SortedList<DateTime, object> timeList = null;

            //ʱ����Զ��������

            if (num > 0)
            {
                timeList = new SortedList<DateTime, object>(num);
            }
            else
                timeList = new SortedList<DateTime, object>(300);


            //�õ����е�ʱ��
            foreach (CalculateValue cv in calcValues)
            {
                DateTime date = cv.Date.Value;
                if (timeList.ContainsKey(date) == false)
                {
                    timeList.Add(date, null);
                }
            }

           
            

            int temp = int.MaxValue;
            if (num > 0)
            {
                temp = num;
            }

            for (int index = 0; index < timeList.Count && index < temp; index++)
            {
                DataRow row = dt.NewRow();

                DateTime date = timeList.Keys[index];
                row[PubConstant.timeColumnName] = date;

                foreach (CalculateValue cv in calcValues.FindAll(delegate(CalculateValue item) { return item.Date.Value == date; }))
                {
                    string aParamName = calcParams.Find(delegate(CalculateParam item) { return item.CalculateParamID == cv.CalculateParamID; }).ParamName; //cv.calculateParam.ParamName;
                    row[aParamName] = cv.Val;
                }

              
                dt.Rows.Add(row);

            }

            dt.AcceptChanges();

            return dt;

        }

   
        /// <summary>
        /// ��ʵ�ʵ�ֵ��䵽�����б��У��������û����Ӧ��ֵ����������ֵ0
        /// </summary>
        /// <param name="list">�����б�</param>
        /// <param name="appCalcName">���ļ�������</param>
        /// <param name="date">����</param>
        /// <param name="appendDot">�Ƿ񽫼������ƺ͵���ڲ�����ǰ��</param>
        /// <param name="simpleInfo">���������Ϣ</param>
        public static AppIntegratedInfo fillListByCalcName_Date(hammergo.caculator.MyList list, string appCalcName, DateTime date, bool appendDot,AppIntegratedInfo simpleInfo)
        {
          


            foreach (ConstantParam cp in simpleInfo.ConstantParams)
            {
                string key = cp.ParamSymbol;
                if (appendDot)
                {
                    key = String.Format("{0}.{1}", appCalcName, key);
                }
                list.add(key, cp.Val.Value);

            }

            //����ֵ0

            foreach (MessureParam mp in simpleInfo.MessureParams)
            {
                string key = mp.ParamSymbol;
                if (appendDot)
                {
                    key = String.Format("{0}.{1}", appCalcName, key);
                }
                list.add(key, 0);
            }

            //����ֵ0
            foreach (CalculateParam cp in simpleInfo.CalcParams)
            {
                string key = cp.ParamSymbol;
                if (appendDot)
                {
                    key = String.Format("{0}.{1}", appCalcName, key);
                }
                list.add(key, 0);
            }

            foreach (MessureValue mv in simpleInfo.MessureValues)
            {
                MessureParam mp = simpleInfo.MessureParams.Find(delegate(MessureParam item) { return item.MessureParamID == mv.MessureParamID; });

                string key = mp.ParamSymbol;
                if (appendDot)
                {
                    key = String.Format("{0}.{1}", appCalcName, key);
                }

                    list[key] = mv.Val.Value;
            }


            foreach (CalculateValue cv in simpleInfo.CalcValues)
            {
                CalculateParam cp = simpleInfo.CalcParams.Find(delegate(CalculateParam item) { return item.CalculateParamID == cv.CalculateParamID; });
                string key = cp.ParamSymbol;
                if (appendDot)
                {
                    key = String.Format("{0}.{1}", appCalcName, key);
                }

                    list[key] = cv.Val.Value;
         
            }


            return simpleInfo;
        }


        /// <summary>
        /// �ж�appName�Ƿ������Ӧ��ֵ
        /// </summary>
        /// <param name="appName"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static bool existData(string appName, DateTime date)
        {
            AppIntegratedInfo appInfo = new AppIntegratedInfo(appName, 0, date, date);
            bool flag = false;
            if (appInfo.MessureValues.Count != 0)
            {
                flag = true;
            }

           
            return flag;
        }
    }
}
