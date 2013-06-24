using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.Xml.Serialization;
using System.IO;
using System.Data.SqlClient;
using System.Data;

namespace hammergo.ConnectionPool
{
    /// <summary>
    /// 此连接池没有考虑连接生命周期，以及回收泄漏的连接
    /// </summary>
    public sealed class Pool
    {
        private Pool()
        {

        }
        public static System.Data.IDbConnection GetOpenConnection()
        {
            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection();
            con.ConnectionString = hammergo.GlobalConfig.PubConstant.ConnectionString;
            con.Open();

            return con;
        }

        //public static System.Data.IDbTransaction GetBeginTransaction()
        //{

        //    return GetOpenConnection().BeginTransaction();
        //}


    }
}
