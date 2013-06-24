using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using hammergo.DALFactory;
	
		 
	

namespace hammergo.BLL
{
    public class TransCreator 
    {
        private TransCreator()
        {
        }

        public static System.Data.IDbTransaction GetBeginTransaction()
        {
            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection();
            con.ConnectionString = hammergo.GlobalConfig.PubConstant.ConnectionString;
            con.Open();
            return con.BeginTransaction();
        }
    }
}
