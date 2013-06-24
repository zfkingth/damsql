using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace hammergo.SqlDbDAL
{
    partial class CalculateParamDAL
    {
        /// <summary>
        /// 获取应用该测点的子测点的计算名称
        /// </summary>
        /// <param name="appCalcName"></param>
        /// <returns></returns>
        public List<string> getChildAppCalcName(string appCalcName)
        {


            string sql = "SELECT [Apparatus].[CalculateName] FROM [Apparatus], [CalculateParam] WHERE (([Ap" +
         "paratus].[AppName] = [CalculateParam].[appName]) AND ([CalculateParam].[Calculat" +
         "eExpress] like @CalculateExpress))";


            SqlParameter parameter = new SqlParameter("@CalculateExpress", SqlDbType.NVarChar);
            parameter.Value = "%" + appCalcName + ".%"; //.是计算公式引用时必须使用的符号

            SqlParameter[] parameters = new SqlParameter[] { parameter };

            List<string> list = new List<string>(10);

            //Execute the query	
            using (System.Data.IDbConnection dbCon =hammergo.ConnectionPool.Pool.GetOpenConnection())
            {
                using (IDataReader rdr =
                    Maticsoft.DBUtility.DbHelperSQL.ExecuteReader(dbCon, CommandType.Text,
                    sql, parameters))
                {
                    while (rdr.Read())//前进到下一条记录
                    {

                        list.Add(rdr.GetString(0));
                    }
                    rdr.Close();//关闭reader

                }
            }


            return list;
        }
    }
}
