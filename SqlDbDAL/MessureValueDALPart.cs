using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using hammergo.Tracking;
using System.Data.SqlClient;

namespace hammergo.SqlDbDAL
{
    partial class MessureValueDAL
    {
        /// <summary>
        /// 测量参数对应值的最大日期
        /// </summary>
        public DateTime? getMaxDate(Guid? messureParamID)
        {
            string sql = "select max(Date) from [MessureValue] where [messureParamID]=@messureParamID ";
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@messureParamID", System.Data.SqlDbType.UniqueIdentifier);
            parameters[0].Value = messureParamID.Value;

            DateTime? val = null;


            object obj = Maticsoft.DBUtility.DbHelperSQL.ExecuteScalar(System.Data.CommandType.Text,sql, parameters);

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
        public TrackedList<hammergo.Model.MessureValue> GetList(string appName, int topNum, DateTime? startDate, DateTime? endDate)
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
            string snCondition = string.Format("MessureParam.appName='{0}'", appName);



            if (startDate.HasValue)
            {
                  paramList.Add(startParam);

                  if (endDate.HasValue)
                  {
                      paramList.Add(endParam);
                      sql = string.Format("select  {1} FROM MessureParam INNER JOIN MessureValue ON MessureParam.MessureParamID = MessureValue.messureParamID where {0} and MessureValue.Date >= @startDate and MessureValue.Date <= @endDate ", snCondition, SQL_Field);
                  }
                  else if (topNum > 0)
                      sql = string.Format("select top {0}  {2} FROM MessureParam INNER JOIN MessureValue ON MessureParam.MessureParamID = MessureValue.messureParamID where {1} and  MessureValue.Date>= @startDate order by MessureValue.Date  asc", topNum, snCondition, SQL_Field);
                  else
                      sql = string.Format("select  {1} FROM MessureParam INNER JOIN MessureValue ON MessureParam.MessureParamID = MessureValue.messureParamID where {0} and  MessureValue.Date>= @startDate ", snCondition, SQL_Field);
            }
            else if (endDate.HasValue)
            {
                paramList.Add(endParam);
                if (topNum > 0)
                    sql = string.Format("select top {0}  {2} FROM MessureParam INNER JOIN MessureValue ON MessureParam.MessureParamID = MessureValue.messureParamID where {1} and  MessureValue.Date<= @endDate order by MessureValue.Date  desc", topNum, snCondition,  SQL_Field);
                else
                    sql = string.Format("select  {1} FROM MessureParam INNER JOIN MessureValue ON MessureParam.MessureParamID = MessureValue.messureParamID where {0} and  MessureValue.Date<= @endDate ", snCondition, SQL_Field);
            }
            else
            {
                if (topNum > 0)
                    sql = string.Format("select top {0}  {2} FROM MessureParam INNER JOIN MessureValue ON MessureParam.MessureParamID = MessureValue.messureParamID where {1} order by MessureValue.Date desc", topNum, snCondition, SQL_Field);
                else
                    sql = string.Format("select  {1} FROM MessureParam INNER JOIN MessureValue ON MessureParam.MessureParamID = MessureValue.messureParamID where {0} ", snCondition, SQL_Field);

            }


            return QueryModelList(sql, paramList.ToArray());
        }



    }
}
