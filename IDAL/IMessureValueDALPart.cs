using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using hammergo.Tracking;

namespace hammergo.IDAL
{
    partial interface IMessureValueDAL
    {
        /// <summary>
        /// 指定messureParamID,获取messureValue的最大日期
        /// </summary>				
        DateTime? getMaxDate(Guid? messureParamID);



        /// <summary>
        /// 根据测点编号，获取计算值对象列表
        /// </summary>
        /// <param name="appName">测点编号</param>
        /// <param name="topNum">需要返回对象的个数，当topNum</param>
        /// <param name="startDate">起始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns></returns>
        TrackedList<hammergo.Model.MessureValue> GetList(string appName, int topNum, DateTime? startDate, DateTime? endDate);

    }
}
