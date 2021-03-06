 /**////////////////////////////////////////////////////////////////////////////////////////
 // Description: SqlDAL class .
 // ---------------------
 // Copyright  2009 hammergo@163.com
 // ---------------------
 // History
 //    2013年3月6日 22:35:14    zfking    
 /**////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using hammergo.IDAL;
using Maticsoft.DBUtility;
using System.Collections.Generic;
using hammergo.Tracking;
using hammergo.ConnectionPool;


namespace hammergo.SqlDbDAL
{
    /// <summary>
	/// 数据访问层逻辑类摘要说明。
	/// </summary>
    
    public partial class  RemarkDAL:IRemarkDAL
    {
		private const int columnsCnt = 3;
		protected string SQL_SELECT_All="select [appName] ,[Date] ,[RemarkText]  from [Remark]";
		protected string SQL_Field="Remark.appName ,Remark.Date ,Remark.RemarkText ";
		
		
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool ExistsBy_appName_Date(string appName,System.DateTime Date)
		{

			
			SqlParameter[] parameters =new SqlParameter[2];
				parameters[0]= new SqlParameter("@appName", SqlDbType.NVarChar);
				parameters[0].Value=appName;
				
				parameters[1]= new SqlParameter("@Date", SqlDbType.DateTime);
				parameters[1].Value=Date;
				
			
			string sql="select count(1) from Remark where [appName] = @appName and [Date] = @Date";
			return DbHelperSQL.Exists(sql, parameters);
		}
		
		/// <summary>
		/// 更新记录的记录
		/// </summary>
		public bool UpdateBy_appName_Date( string appName,System.DateTime Date, string newappName,System.DateTime newDate)
		{
			SqlParameter[] parameters =new SqlParameter[4];
				parameters[0]= new SqlParameter("@newappName", SqlDbType.NVarChar);
				parameters[0].Value=newappName;
				
				parameters[1]= new SqlParameter("@newDate", SqlDbType.DateTime);
				parameters[1].Value=newDate;
				
				parameters[2]= new SqlParameter("@appName", SqlDbType.NVarChar);
				parameters[2].Value=appName;
				
				parameters[3]= new SqlParameter("@Date", SqlDbType.DateTime);
				parameters[3].Value=Date;
				
			
			string sql="update [Remark] set [appName]=@newappName,[Date]=@newDate where [appName] = @appName and [Date] = @Date";
			
			if (DbHelperSQL.ExecuteNonQuery( CommandType.Text, sql, parameters)
                == 0)
            {
                return false;
            }

            return true;
			
		}
		
		
			/// <summary>
		/// 更新记录的记录
		/// </summary>
		public bool UpdateBy_appName_Date( string appName,System.DateTime Date, string newappName,System.DateTime newDate,System.Data.IDbTransaction trans)
		{
			SqlParameter[] parameters =new SqlParameter[4];
				parameters[0]= new SqlParameter("@newappName", SqlDbType.NVarChar);
				parameters[0].Value=newappName;
				
				parameters[1]= new SqlParameter("@newDate", SqlDbType.DateTime);
				parameters[1].Value=newDate;
				
				parameters[2]= new SqlParameter("@appName", SqlDbType.NVarChar);
				parameters[2].Value=appName;
				
				parameters[3]= new SqlParameter("@Date", SqlDbType.DateTime);
				parameters[3].Value=Date;
				
			
			string sql="update [Remark] set [appName]=@newappName,[Date]=@newDate where [appName] = @appName and [Date] = @Date";
			
			if (DbHelperSQL.ExecuteNonQuery(trans.Connection ,trans , CommandType.Text, sql, parameters)
                == 0)
            {
                return false;
            }

            return true;
			
		}
		
		
		
		
		/// <summary>
		/// 根据表的主键得到一个对象实体
		/// </summary>
		public hammergo.Model.Remark GetModelBy_appName_Date(string appName,System.DateTime Date)
		{
		
			
			SqlParameter[] parameters =new SqlParameter[2];
				parameters[0]= new SqlParameter("@appName", SqlDbType.NVarChar);
				parameters[0].Value=appName;
				parameters[1]= new SqlParameter("@Date", SqlDbType.DateTime);
				parameters[1].Value=Date;
			string sql="select [appName] ,[Date] ,[RemarkText]  from [Remark] where [appName] = @appName and [Date] = @Date";
			return QueryModel(sql, parameters);

		}				
		
		
		
		
		



		
		
		/// <summary>
		/// 为model创建参数数组
		/// </summary>
		private SqlParameter[] createAllParams(hammergo.Model.Remark model)
        {
			SqlParameter[] parameters =new SqlParameter[3];
			parameters[0]= new SqlParameter("@appName", SqlDbType.NVarChar);
					if(model.AppName != null)
					{
						parameters[0].Value =model.AppName;
					}else
					{
						parameters[0].Value =DBNull.Value;
					}
			parameters[1]= new SqlParameter("@Date", SqlDbType.DateTime);
					if (model.Date.HasValue)
					{
						parameters[1].Value = model.Date;
					}
					else
					{
						parameters[1].Value = DBNull.Value;
					}
			parameters[2]= new SqlParameter("@RemarkText", SqlDbType.NVarChar);
					if(model.RemarkText != null)
					{
						parameters[2].Value =model.RemarkText;
					}else
					{
						parameters[2].Value =DBNull.Value;
					}
			return  parameters;
		}
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(hammergo.Model.Remark model)
		{
			string sql="insert into Remark ([appName] ,[Date] ,[RemarkText]  ) values (@appName,@Date,@RemarkText)";
			if (DbHelperSQL.ExecuteNonQuery( CommandType.Text, sql, createAllParams(model))
                == 0)
            {
                return false;
            }

            return true;
			
			
		}		
		
		
		/// <summary>
		/// 使用事务增加一条数据
		/// </summary>
		public bool Add(hammergo.Model.Remark model,System.Data.IDbTransaction trans)
		{
			string sql="insert into Remark ([appName] ,[Date] ,[RemarkText]  ) values (@appName,@Date,@RemarkText)";
			if (DbHelperSQL.ExecuteNonQuery(trans.Connection ,trans , 
					CommandType.Text, sql, createAllParams(model))
				== 0)
				{
					return false;
				}
				
				return true;
			
		
		}	
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(hammergo.Model.Remark model)
		{
			string sql="update [Remark] set [appName]=@appName,[Date]=@Date,[RemarkText]=@RemarkText where [appName]=@appName and [Date]=@Date";
			
			if (DbHelperSQL.ExecuteNonQuery(CommandType.Text, sql, createAllParams(model))
                == 0)
            {
                return false;
            }

            return true;
		}		
		
		/// <summary>
		/// 使用事务更新一条数据
		/// </summary>
		public bool Update(hammergo.Model.Remark model,System.Data.IDbTransaction trans)
		{
			string sql="update [Remark] set [appName]=@appName,[Date]=@Date,[RemarkText]=@RemarkText where [appName]=@appName and [Date]=@Date";
			
			if (DbHelperSQL.ExecuteNonQuery(trans.Connection ,trans ,
					CommandType.Text, sql, createAllParams(model))
				== 0)
				{
					return false;
				}
				
				return true;
		}		

		
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string appName,System.DateTime Date)
		{
			string sql="delete from Remark  where [appName]=@appName and [Date]=@Date";
			SqlParameter[] parameters =new SqlParameter[2];
				parameters[0]= new SqlParameter("@appName", SqlDbType.NVarChar);
				parameters[0].Value=appName;
				parameters[1]= new SqlParameter("@Date", SqlDbType.DateTime);
				parameters[1].Value=Date;
				
				if (DbHelperSQL.ExecuteNonQuery(CommandType.Text, sql, parameters)== 0)
				{
					return false;
				}
	
				return true;
		}		
		
		
		/// <summary>
		/// 使用事务删除一条数据
		/// </summary>
		public bool Delete(string appName,System.DateTime Date,System.Data.IDbTransaction trans)
		{
			string sql="delete from Remark  where [appName]=@appName and [Date]=@Date";
			SqlParameter[] parameters =new SqlParameter[2];
				parameters[0]= new SqlParameter("@appName", SqlDbType.NVarChar);
				parameters[0].Value=appName;
				parameters[1]= new SqlParameter("@Date", SqlDbType.DateTime);
				parameters[1].Value=Date;
				
			if (DbHelperSQL.ExecuteNonQuery(trans.Connection ,trans ,
				CommandType.Text, sql, parameters)
				== 0)
				{
					return false;
				}
			
				return true;
		}		

		
        /// <summary>
        /// 根据dataReader读取的值创建Model实体对象
        /// </summary>
        /// <param name="values">值的数值，有固定的顺序</param>
        private hammergo.Model.Remark createModel(object[] values)
        {
            hammergo.Model.Remark model = null;
            //创建model对象
            if (values != null)
            {
                model = new hammergo.Model.Remark();
				object obj=null;
                //赋值
					 obj = values[0];
					if (obj is System.DBNull)
					{
						model.AppName = null;
					}
					else
					{
						model.AppName = (string)obj;
					}
					 obj = values[1];
					if (obj is System.DBNull)
					{
						model.Date = null;
					}
					else
					{
						model.Date = (System.DateTime)obj;
					}
					 obj = values[2];
					if (obj is System.DBNull)
					{
						model.RemarkText = null;
					}
					else
					{
						model.RemarkText = (string)obj;
					}
            }

            return model;
        }
		

		/// <summary>
        /// 根据sql语句及参数数组，得到查询结果，并实例化实体对象
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private hammergo.Model.Remark QueryModel(string sql, params SqlParameter[] parameters)
        {
            //Set up a return value
            hammergo.Model.Remark model = null;

			object[] values = new object[columnsCnt];
            //Execute the query	
            using (System.Data.IDbConnection dbCon =hammergo.ConnectionPool.Pool.GetOpenConnection())
            {
				using (IDataReader rdr =
					DbHelperSQL.ExecuteReader(dbCon, CommandType.Text,
					sql, parameters))
				{
	
					if (rdr.Read())//前进到下一条记录
					{
						rdr.GetValues(values);
						rdr.Close();//关闭reader,需要最短的时间内关闭连接
						model = createModel(values);//创建对象并赋值
					}
				}
			}
            return model;
        }
		
 		/// <summary>
        /// 根据sql语句及参数数组，得到查询结果，并实例化实体对象列表
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private TrackedList<hammergo.Model.Remark> QueryModelList(string sql, params SqlParameter[] parameters)
        {
            TrackedList<hammergo.Model.Remark> list = new TrackedList<hammergo.Model.Remark>(100);


            hammergo.Model.Remark model = null;
            object[] values = new object[columnsCnt];

            //Execute the query	
			            using (System.Data.IDbConnection dbCon =hammergo.ConnectionPool.Pool.GetOpenConnection())
            {
				using (IDataReader rdr =
					DbHelperSQL.ExecuteReader(dbCon, CommandType.Text,
					sql, parameters))
				{
					while (rdr.Read())//前进到下一条记录
					{
						rdr.GetValues(values);
	
						model = createModel(values);//创建对象并赋值
						list.Add(model);
					}
					rdr.Close();//关闭reader
	
				}
			}

			list.Tracking = true;
            return list;

        }
		
		
		
		
		
		

		/// <summary>
		/// 获得对象实体列表
		/// </summary>
        public TrackedList<hammergo.Model.Remark> GetList()
		{
			string sql="select [appName] ,[Date] ,[RemarkText]  from [Remark]";
			 return QueryModelList(sql, null);
		}			
		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.Remark> GetListByappName(string  AppName)
		{
			string sql="select [appName] ,[Date] ,[RemarkText]  from [Remark] where appName =@appName";
			SqlParameter parameter=new SqlParameter("@appName",SqlDbType.NVarChar);
			parameter.Value=AppName;
			return QueryModelList(sql, parameter);
		}	
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByappName(string  AppName)
		{
			string sql="select count(1) from Remark where appName =@appName";
			SqlParameter parameter=new SqlParameter("@appName",SqlDbType.NVarChar);
			parameter.Value=AppName;
			object obj= DbHelperSQL.ExecuteScalar(CommandType.Text,sql, parameter);
			
			return (int)obj;
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.Remark> GetListByDate(System.DateTime  Date)
		{
			string sql="select [appName] ,[Date] ,[RemarkText]  from [Remark] where Date =@Date";
			SqlParameter parameter=new SqlParameter("@Date",SqlDbType.DateTime);
			parameter.Value=Date;
			return QueryModelList(sql, parameter);
		}	
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByDate(System.DateTime  Date)
		{
			string sql="select count(1) from Remark where Date =@Date";
			SqlParameter parameter=new SqlParameter("@Date",SqlDbType.DateTime);
			parameter.Value=Date;
			object obj= DbHelperSQL.ExecuteScalar(CommandType.Text,sql, parameter);
			
			return (int)obj;
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.Remark> GetListByRemarkText(string  RemarkText)
		{
			string sql="select [appName] ,[Date] ,[RemarkText]  from [Remark] where RemarkText =@RemarkText";
			SqlParameter parameter=new SqlParameter("@RemarkText",SqlDbType.NVarChar);
			parameter.Value=RemarkText;
			return QueryModelList(sql, parameter);
		}	
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByRemarkText(string  RemarkText)
		{
			string sql="select count(1) from Remark where RemarkText =@RemarkText";
			SqlParameter parameter=new SqlParameter("@RemarkText",SqlDbType.NVarChar);
			parameter.Value=RemarkText;
			object obj= DbHelperSQL.ExecuteScalar(CommandType.Text,sql, parameter);
			
			return (int)obj;
		}
		
		
		
    }
}



