using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using hammergo.Tracking;
using System.Data.SqlClient;

namespace hammergo.SqlDbDAL
{
    partial class RemarkDAL
    {
        /// <summary>
        /// 根据测点编号，获取计算值对象列表
        /// </summary>
        /// <param name="appName">测点编号</param>
        /// <param name="topNum">需要返回对象的个数，当topNum</param>
        /// <param name="startDate">起始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns></returns>
        public TrackedList<hammergo.Model.Remark> GetList(string appName, int topNum, DateTime? startDate, DateTime? endDate)
        {
            List<SqlParameter> paramList = new List<SqlParameter>(4);
            SqlParameter startParam = new SqlParameter("@startDate", System.Data.SqlDbType.DateTime);
            SqlParameter endParam = new SqlParameter("@endDate", System.Data.SqlDbType.DateTime);

            if (startDate.HasValue)
            {
                startParam.Value = startDate.Value;
            }

            if (endDate.HasValue)
            {
                endParam.Value = endDate.Value;
            }
            

            string sql = "";
            string snCondition = string.Format("appName='{0}'", appName);


            if (startDate.HasValue)
            {
                  paramList.Add(startParam);

                  if (endDate.HasValue)
                  {
                      paramList.Add(endParam);
                      sql = string.Format("select  {1} FROM Remark where {0} and Date >= @startDate and Date <= @endDate ", snCondition, SQL_Field);
                  }
                  else if (topNum > 0)
                      sql = string.Format("select top {0}  {2} FROM Remark where {1} and  Date>= @startDate order by Date  asc", topNum, snCondition, SQL_Field);
                  else
                      sql = string.Format("select  {1} FROM Remark where {0} and  Date>= @startDate ", snCondition, SQL_Field);
            }
            else if (endDate.HasValue)
            {
                paramList.Add(endParam);
                if (topNum > 0)
                    sql = string.Format("select top {0}  {2} FROM Remark where {1} and  Date<= @endDate order by Date  desc", topNum, snCondition, SQL_Field);
                else
                    sql = string.Format("select  {1} FROM Remark where {0} and  Date<= @endDate ", snCondition, SQL_Field);
            }
            else
            {
                if (topNum > 0)
                    sql = string.Format("select top {0}  {2} FROM Remark where {1} order by Date desc", topNum, snCondition, SQL_Field);
                else
                    sql = string.Format("select  {1} FROM Remark where {0} ", snCondition, SQL_Field);

            }


            return QueryModelList(sql, paramList.ToArray());
        }

    }
}
