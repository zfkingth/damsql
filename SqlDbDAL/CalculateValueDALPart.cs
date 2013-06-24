using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using hammergo.Tracking;

namespace hammergo.SqlDbDAL
{
    partial class CalculateValueDAL
    {
        /// <summary>
        /// 计算参数对应值的最大日期
        /// </summary>
        public DateTime? getMaxDate(Guid? calculateParamID)
        {
            string sql = "select max(Date) from [CalculateValue] where [calculateParamID]=@calculateParamID ";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@calculateParamID", System.Data.SqlDbType.UniqueIdentifier);
            parameters[0].Value = calculateParamID.Value;

            DateTime? val = null;


            object obj = Maticsoft.DBUtility.DbHelperSQL.ExecuteScalar(System.Data.CommandType.Text, sql, parameters);

            if (obj is System.DBNull == false)
            {
                val = (DateTime)obj;
            }
            return val;
        }

        /// <summary>
        /// 根据测点编号，获取计算值对象列表
        /// </summary>
        /// <param name="appName">测点编号</param>
        /// <param name="topNum">需要返回对象的个数，当topNum</param>
        /// <param name="startDate">起始时间</param>
        /// <param name="endDate">结束时间</param>
        /// <returns></returns>
        public TrackedList<hammergo.Model.CalculateValue> GetList(string appName, int topNum, DateTime? startDate, DateTime? endDate)
        {
            //SqlParameter[] parameters = new SqlParameter[2];
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
            string snCondition = string.Format("CalculateParam.appName='{0}'", appName);
            if (startDate.HasValue)
            {
                paramList.Add(startParam);

                if (endDate.HasValue)
                {
                    paramList.Add(endParam);
                    sql = string.Format("select  {1} from CalculateParam INNER JOIN CalculateValue ON CalculateParam.CalculateParamID = CalculateValue.calculateParamID where {0} and CalculateValue.Date >= @startDate and CalculateValue.Date <= @endDate ", snCondition, SQL_Field);
                }
                else if (topNum > 0)
                    sql = string.Format("select top {0}  {2} from CalculateParam INNER JOIN CalculateValue ON CalculateParam.CalculateParamID = CalculateValue.calculateParamID where {1} and  CalculateValue.Date>= @startDate order by CalculateValue.Date  asc", topNum, snCondition, SQL_Field);
                else
                    sql = string.Format("select  {1} from CalculateParam INNER JOIN CalculateValue ON CalculateParam.CalculateParamID = CalculateValue.calculateParamID where {0} and  CalculateValue.Date>= @startDate ", snCondition, SQL_Field);
            }
            else if (endDate.HasValue)
            {
                paramList.Add(endParam);

                if (topNum > 0)
                    sql = string.Format("select top {0}  {2} from CalculateParam INNER JOIN CalculateValue ON CalculateParam.CalculateParamID = CalculateValue.calculateParamID where {1} and  CalculateValue.Date<= @endDate order by CalculateValue.Date  desc", topNum, snCondition, SQL_Field);
                else
                    sql = string.Format("select  {1} from CalculateParam INNER JOIN CalculateValue ON CalculateParam.CalculateParamID = CalculateValue.calculateParamID where {0} and  CalculateValue.Date<= @endDate ", snCondition,  SQL_Field);
            }
            else
            {
                if (topNum > 0)
                    sql = string.Format("select top {0}  {2} from CalculateParam INNER JOIN CalculateValue ON CalculateParam.CalculateParamID = CalculateValue.calculateParamID where {1} order by CalculateValue.Date desc", topNum, snCondition, SQL_Field);
                else
                    sql = string.Format("select  {1} from CalculateParam INNER JOIN CalculateValue ON CalculateParam.CalculateParamID = CalculateValue.calculateParamID where {0} ", snCondition, SQL_Field);

            }

            return QueryModelList(sql, paramList.ToArray());

        }

    }
}
