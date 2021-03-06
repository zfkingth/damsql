 /**////////////////////////////////////////////////////////////////////////////////////////
 // Description: BLL class .
 // ---------------------
 // Copyright  2009 hammergo@163.com
 // ---------------------
 // History
 //    2013年2月2日 20:06:23    zfking    
 /**////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Data;
using System.Collections.Generic;
using hammergo.Model;
using hammergo.DALFactory;
using hammergo.IDAL;
using hammergo.Tracking;


namespace hammergo.BLL
{
    /// <summary>
    /// 业务逻辑类的摘要说明。
    /// </summary>

    public class MessureValueBLL : MessureValueBLLBase
    {

        public DateTime? getMaxDate(Guid? messureParamID)
        {
            return dal.getMaxDate(messureParamID);
        }

        /// <summary>
        /// 根据测点编号，获取计算值对象列表
        /// </summary>
        /// <param name="appName">测点编号</param>
        /// <param name="topNum">需要返回对象的个数，当topNum</param>
        /// <param name="startDate">起始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns></returns>
        public TrackedList<hammergo.Model.MessureValue> GetList(string appName, int topNum, DateTime? startDate, DateTime? endDate)
        {
            return dal.GetList(appName, topNum, startDate, endDate);
        }

    }
}



