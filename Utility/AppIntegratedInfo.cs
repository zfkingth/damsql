using System;
using System.Collections.Generic;
using System.Text;
using hammergo.Model;
using hammergo.Tracking;
using System.Linq;

namespace hammergo.Utility
{
    public class AppIntegratedInfo
    {
        public string appName = null;
        public int topNum = 0;
        public DateTime? startDate, endDate;

        hammergo.BLL.MessureParamBLL _mesBLL = null;
        private hammergo.BLL.MessureParamBLL MesBLL
        {
            get
            {
                if (_mesBLL == null)
                {
                    _mesBLL = new hammergo.BLL.MessureParamBLL();
                }
                return _mesBLL;
            }
        }

        hammergo.BLL.CalculateParamBLL _calcBLL = null;

        private hammergo.BLL.CalculateParamBLL CalcBLL
        {
            get
            {
                if (_calcBLL == null)
                {
                    _calcBLL = new hammergo.BLL.CalculateParamBLL();
                }
                return _calcBLL;
            }
        }

        hammergo.BLL.MessureValueBLL _mesValueBLL = null;
        public hammergo.BLL.MessureValueBLL MesValueBLL
        {
            get
            {
                if (_mesValueBLL == null)
                {
                    _mesValueBLL = new hammergo.BLL.MessureValueBLL();
                }
                return _mesValueBLL;
            }
        }

        private hammergo.BLL.CalculateValueBLL _calcValueBLL = null;
        public hammergo.BLL.CalculateValueBLL CalcValueBLL
        {
            get
            {
                if (_calcValueBLL == null)
                {
                    _calcValueBLL = new hammergo.BLL.CalculateValueBLL();
                }
                return _calcValueBLL;
            }
        }

        hammergo.BLL.RemarkBLL _remarkBLL = null;
        public hammergo.BLL.RemarkBLL RemarkBLL
        {
            get
            {
                if (_remarkBLL == null)
                {
                    _remarkBLL = new hammergo.BLL.RemarkBLL();
                }
                return _remarkBLL;
            }
        }

        hammergo.BLL.ApparatusBLL _appBLL = null;
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


        hammergo.BLL.ApparatusTypeBLL _appTypeBLL = null;
        private hammergo.BLL.ApparatusTypeBLL AppTypeBLL
        {
            get
            {
                if (_appTypeBLL == null)
                {
                    _appTypeBLL = new hammergo.BLL.ApparatusTypeBLL();
                }
                return _appTypeBLL;
            }
        }

        private ApparatusType _appType = null;
        public ApparatusType AppType
        {
            get
            {
                if (_appType == null)
                {
                    if (App.AppTypeID != null)
                    {
                        _appType = AppTypeBLL.GetModelBy_ApparatusTypeID(App.AppTypeID.Value);
                    }
                }
                return _appType;
            }
        }



        private TrackedList<MessureParam> _messureParams = null;
        public TrackedList<MessureParam> MessureParams
        {
            get
            {
                if (_messureParams == null)
                {
                    _messureParams = MesBLL.GetListByappName(appName);
                }
                return _messureParams;
            }
        }

        private TrackedList<CalculateParam> _calcParams = null;
        public TrackedList<CalculateParam> CalcParams
        {
            get
            {
                if (_calcParams == null)
                {
                    _calcParams = CalcBLL.GetListByappName(appName);
                }

                return _calcParams;
            }
        }
        private TrackedList<MessureValue> _messureValues = null;
        public TrackedList<MessureValue> MessureValues
        {
            get
            {
                if (_messureValues == null)
                {
                    _messureValues = MesValueBLL.GetList(appName, topNum * MessureParams.Count, startDate, endDate);
                }
                return _messureValues;
            }
        }

        private TrackedList<CalculateValue> _calcValues = null;
        public TrackedList<CalculateValue> CalcValues
        {
            get
            {
                if (_calcValues == null)
                {
                    _calcValues = CalcValueBLL.GetList(appName, topNum * CalcParams.Count, startDate, endDate);
                }
                return _calcValues;
            }
        }

        private TrackedList<Remark> _remarks = null;
        public TrackedList<Remark> Remarks
        {
            get
            {
                if (_remarks == null)
                {
                    _remarks = RemarkBLL.GetList(appName, topNum, startDate, endDate);
                }
                return _remarks;
            }
        }





        private TrackedList<ConstantParam> _constantParams = null;

        public TrackedList<ConstantParam> ConstantParams
        {
            get
            {
                if (_constantParams == null)
                {
                    hammergo.BLL.ConstantParamBLL constBLL = new hammergo.BLL.ConstantParamBLL();
                    _constantParams = constBLL.GetListByappName(appName);
                }
                return _constantParams;
            }

        }

        private Apparatus _app = null;
        public Apparatus App
        {
            get
            {
                if (_app == null)
                {

                    _app = AppBLL.GetModelBy_AppName(appName);
                }
                return _app;
            }

        }

        public AppIntegratedInfo(string appName, int topNum, DateTime? startDate, DateTime? endDate)
        {
            this.appName = appName;
            this.topNum = topNum;
            this.startDate = startDate;
            this.endDate = endDate;

        }

        /// <summary>
        /// 重置参数并清除测量数据和计算数据
        /// </summary>
        /// <param name="topNum"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        public void  Reset(int topNum,DateTime? startDate,DateTime? endDate)
        {
            this.topNum = topNum;
            this.startDate = startDate;
            this.endDate = endDate;
            this._messureValues = null;
            this._calcValues = null;
        }

        public AppIntegratedInfo(string calcName, DateTime? date)
        {
            _app = AppBLL.GetModelBy_CalculateName(calcName);
            if (_app == null)
            {
                throw new Exception(string.Format("找不到计算编号为{0}的测点", calcName));
            }

            this.appName = _app.AppName; ;
            this.topNum = 0;
            this.startDate = date;
            this.endDate = date;
        }

        /// <summary>
        /// 清除AppInfo中的先有数据，并离指定时间最近的数据的记录
        /// </summary>
        /// <param name="preConcertDate">指定的时间,也就是预定时间</param>
        /// <param name="days">"最近"这个词所允许的与预期时间相隔最大的天数</param>
        public AppIntegratedInfo getNearTimeData(DateTime preConcertDate, double days)
        {

            DateTime newSTime = preConcertDate.AddDays(-days);
            DateTime newETime = preConcertDate.AddDays(days);
            topNum = 0;//没有意义
            AppIntegratedInfo appInfo = new AppIntegratedInfo(this.appName, 0, newSTime, newETime);


            //复制了calc参数
            appInfo.SetCalcParams(this.CalcParams);

            //进行查询
            if (appInfo.CalcValues.Count == 0)
            {
                return null;//没有数据
            }



            DateTime? before = (from v in appInfo.CalcValues where v.Date < preConcertDate select v.Date).Max();

            DateTime? after = (from v in appInfo.CalcValues where v.Date >= preConcertDate select v.Date).Min();

            DateTime? concreteTime = null;

            if (before != null)
            {
                if (after != null)
                {
                    if (preConcertDate.Ticks - before.Value.Ticks < after.Value.Ticks - preConcertDate.Ticks)
                    {
                        concreteTime = before;
                    }
                    else
                    {
                        concreteTime = after;
                    }
                }
                else
                {
                    concreteTime = before;
                }
            }
            else
            {
                if (after != null)
                {
                    concreteTime = after;
                }
            }


            if (concreteTime == null)
            {
                return null;//没有符合条件的数据
            }

            var result = from v in appInfo.CalcValues where v.Date == concreteTime select v;

            TrackedList<CalculateValue> vs = new TrackedList<CalculateValue>(result);
            appInfo.SetCalcValues(vs);

            return appInfo;


        }

       
        private void SetMessParams(TrackedList<MessureParam> mps)
        {
            _messureParams = mps;
        }

        private void SetCalcParams(TrackedList<CalculateParam> cps)
        {
            _calcParams = cps;
        }

        private void SetCalcValues(TrackedList<CalculateValue> vs)
        {
            _calcValues = vs;
        }

        /// <summary>
        /// 查询离指定时间最近的数据的记录
        /// </summary>
        /// <param name="appName">测点编号</param>
        /// <param name="preConcertDate">指定的时间,也就是预定时间</param>
        /// <param name="days">"最近"这个词所允许的与预期时间相隔最大的天数</param>
        /// <returns></returns>
        public static AppIntegratedInfo getAppInfoNearTime(string appName, DateTime preConcertDate, double days)
        {

            AppIntegratedInfo appInfo = null;

            AppIntegratedInfo infoAfter = new AppIntegratedInfo(appName, 1, preConcertDate, null);

            AppIntegratedInfo infoBefore = new AppIntegratedInfo(appName, 1, null, preConcertDate);

            if (infoAfter.CalcValues.Count == 0 && infoBefore.CalcValues.Count == 0)
                return null;

            long after = long.MaxValue, before = long.MaxValue;



            if (infoAfter.CalcValues.Count != 0)
            {

                after = (infoAfter.CalcValues[0].Date).Value.Ticks - preConcertDate.Ticks;

            }

            if (infoBefore.CalcValues.Count != 0)
            {

                before = preConcertDate.Ticks - (infoBefore.CalcValues[0].Date).Value.Ticks;
            }

            long cha = 0;
            if (after > before)
            {
                cha = before;
                appInfo = infoBefore;
            }
            else
            {
                cha = after;
                appInfo = infoAfter;
            }




            if (cha > TimeSpan.TicksPerDay * days)
            {
                appInfo = null;
            }


            //存在月变化量




            return appInfo;
        }


        /// <summary>
        /// 使用事务集成更新测量数据,计算数据和备注,不更新参数,如果有问题会抛出异常
        /// </summary>
        public void Update()
        {

            using (System.Data.IDbConnection connection = hammergo.ConnectionPool.Pool.GetOpenConnection())
            {
                //connection.Open();

                // Start a local transaction.
                System.Data.IDbTransaction trans = connection.BeginTransaction();

                
                try
                {
                    if (_messureValues != null)
                        MesValueBLL.UpdateList(_messureValues, trans);

                    if (_calcValues != null)
                        CalcValueBLL.UpdateList(_calcValues, trans);

                    if (_remarks != null)
                        RemarkBLL.UpdateList(_remarks, trans);

                    // Attempt to commit the transaction.
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


        bool _tracking = false;
        /// <summary>
        /// 设置
        /// </summary>
        public bool Tracking
        {
            get { return _tracking; }
            set
            {

                _tracking = value;
                MessureValues.Tracking = _tracking;
                CalcValues.Tracking = _tracking;
                Remarks.Tracking = _tracking;
            }
        }

    }
}
