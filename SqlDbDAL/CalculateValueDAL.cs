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
    
    public partial class  CalculateValueDAL:ICalculateValueDAL
    {
		private const int columnsCnt = 3;
		protected string SQL_SELECT_All="select [calculateParamID] ,[Date] ,[Val]  from [CalculateValue]";
		protected string SQL_Field="CalculateValue.calculateParamID ,CalculateValue.Date ,CalculateValue.Val ";
		
		
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool ExistsBy_calculateParamID_Date(System.Guid calculateParamID,System.DateTime Date)
		{

			
			SqlParameter[] parameters =new SqlParameter[2];
				parameters[0]= new SqlParameter("@calculateParamID", SqlDbType.UniqueIdentifier);
				parameters[0].Value=calculateParamID;
				
				parameters[1]= new SqlParameter("@Date", SqlDbType.DateTime);
				parameters[1].Value=Date;
				
			
			string sql="select count(1) from CalculateValue where [calculateParamID] = @calculateParamID and [Date] = @Date";
			return DbHelperSQL.Exists(sql, parameters);
		}
		
		/// <summary>
		/// 更新记录的记录
		/// </summary>
		public bool UpdateBy_calculateParamID_Date( System.Guid calculateParamID,System.DateTime Date, System.Guid newcalculateParamID,System.DateTime newDate)
		{
			SqlParameter[] parameters =new SqlParameter[4];
				parameters[0]= new SqlParameter("@newcalculateParamID", SqlDbType.UniqueIdentifier);
				parameters[0].Value=newcalculateParamID;
				
				parameters[1]= new SqlParameter("@newDate", SqlDbType.DateTime);
				parameters[1].Value=newDate;
				
				parameters[2]= new SqlParameter("@calculateParamID", SqlDbType.UniqueIdentifier);
				parameters[2].Value=calculateParamID;
				
				parameters[3]= new SqlParameter("@Date", SqlDbType.DateTime);
				parameters[3].Value=Date;
				
			
			string sql="update [CalculateValue] set [calculateParamID]=@newcalculateParamID,[Date]=@newDate where [calculateParamID] = @calculateParamID and [Date] = @Date";
			
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
		public bool UpdateBy_calculateParamID_Date( System.Guid calculateParamID,System.DateTime Date, System.Guid newcalculateParamID,System.DateTime newDate,System.Data.IDbTransaction trans)
		{
			SqlParameter[] parameters =new SqlParameter[4];
				parameters[0]= new SqlParameter("@newcalculateParamID", SqlDbType.UniqueIdentifier);
				parameters[0].Value=newcalculateParamID;
				
				parameters[1]= new SqlParameter("@newDate", SqlDbType.DateTime);
				parameters[1].Value=newDate;
				
				parameters[2]= new SqlParameter("@calculateParamID", SqlDbType.UniqueIdentifier);
				parameters[2].Value=calculateParamID;
				
				parameters[3]= new SqlParameter("@Date", SqlDbType.DateTime);
				parameters[3].Value=Date;
				
			
			string sql="update [CalculateValue] set [calculateParamID]=@newcalculateParamID,[Date]=@newDate where [calculateParamID] = @calculateParamID and [Date] = @Date";
			
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
		public hammergo.Model.CalculateValue GetModelBy_calculateParamID_Date(System.Guid calculateParamID,System.DateTime Date)
		{
		
			
			SqlParameter[] parameters =new SqlParameter[2];
				parameters[0]= new SqlParameter("@calculateParamID", SqlDbType.UniqueIdentifier);
				parameters[0].Value=calculateParamID;
				parameters[1]= new SqlParameter("@Date", SqlDbType.DateTime);
				parameters[1].Value=Date;
			string sql="select [calculateParamID] ,[Date] ,[Val]  from [CalculateValue] where [calculateParamID] = @calculateParamID and [Date] = @Date";
			return QueryModel(sql, parameters);

		}				
		
		
		
		
		



		
		
		/// <summary>
		/// 为model创建参数数组
		/// </summary>
		private SqlParameter[] createAllParams(hammergo.Model.CalculateValue model)
        {
			SqlParameter[] parameters =new SqlParameter[3];
			parameters[0]= new SqlParameter("@calculateParamID", SqlDbType.UniqueIdentifier);
					if (model.CalculateParamID.HasValue)
					{
						parameters[0].Value = model.CalculateParamID;
					}
					else
					{
						parameters[0].Value = DBNull.Value;
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
			parameters[2]= new SqlParameter("@Val", SqlDbType.Float);
					if (model.Val.HasValue)
					{
						parameters[2].Value = model.Val;
					}
					else
					{
						parameters[2].Value = DBNull.Value;
					}
			return  parameters;
		}
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(hammergo.Model.CalculateValue model)
		{
			string sql="insert into CalculateValue ([calculateParamID] ,[Date] ,[Val]  ) values (@calculateParamID,@Date,@Val)";
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
		public bool Add(hammergo.Model.CalculateValue model,System.Data.IDbTransaction trans)
		{
			string sql="insert into CalculateValue ([calculateParamID] ,[Date] ,[Val]  ) values (@calculateParamID,@Date,@Val)";
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
		public bool Update(hammergo.Model.CalculateValue model)
		{
			string sql="update [CalculateValue] set [calculateParamID]=@calculateParamID,[Date]=@Date,[Val]=@Val where [calculateParamID]=@calculateParamID and [Date]=@Date";
			
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
		public bool Update(hammergo.Model.CalculateValue model,System.Data.IDbTransaction trans)
		{
			string sql="update [CalculateValue] set [calculateParamID]=@calculateParamID,[Date]=@Date,[Val]=@Val where [calculateParamID]=@calculateParamID and [Date]=@Date";
			
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
		public bool Delete(System.Guid calculateParamID,System.DateTime Date)
		{
			string sql="delete from CalculateValue  where [calculateParamID]=@calculateParamID and [Date]=@Date";
			SqlParameter[] parameters =new SqlParameter[2];
				parameters[0]= new SqlParameter("@calculateParamID", SqlDbType.UniqueIdentifier);
				parameters[0].Value=calculateParamID;
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
		public bool Delete(System.Guid calculateParamID,System.DateTime Date,System.Data.IDbTransaction trans)
		{
			string sql="delete from CalculateValue  where [calculateParamID]=@calculateParamID and [Date]=@Date";
			SqlParameter[] parameters =new SqlParameter[2];
				parameters[0]= new SqlParameter("@calculateParamID", SqlDbType.UniqueIdentifier);
				parameters[0].Value=calculateParamID;
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
        private hammergo.Model.CalculateValue createModel(object[] values)
        {
            hammergo.Model.CalculateValue model = null;
            //创建model对象
            if (values != null)
            {
                model = new hammergo.Model.CalculateValue();
				object obj=null;
                //赋值
					 obj = values[0];
					if (obj is System.DBNull)
					{
						model.CalculateParamID = null;
					}
					else
					{
						model.CalculateParamID = (System.Guid)obj;
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
						model.Val = null;
					}
					else
					{
						model.Val = (double)obj;
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
        private hammergo.Model.CalculateValue QueryModel(string sql, params SqlParameter[] parameters)
        {
            //Set up a return value
            hammergo.Model.CalculateValue model = null;

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
        private TrackedList<hammergo.Model.CalculateValue> QueryModelList(string sql, params SqlParameter[] parameters)
        {
            TrackedList<hammergo.Model.CalculateValue> list = new TrackedList<hammergo.Model.CalculateValue>(100);


            hammergo.Model.CalculateValue model = null;
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
		/// 获取Val的最大值
		/// </summary>				
		public double? GetMaxVal()
		{
			double? val=null;
			string sql="select max(Val) from CalculateValue";
			object obj =DbHelperSQL.ExecuteScalar(CommandType.Text, sql);

			if (obj is System.DBNull ==false)
			{
				val = (double)obj;
			}
  			return val;
		}
				
				
		
		
				
		/// <summary>
		/// 获取Val的最小值
		/// </summary>				
		public double? GetMinVal()
		{
			double? val=null;
			string sql="select min(Val) from CalculateValue";
			object obj =DbHelperSQL.ExecuteScalar(CommandType.Text, sql);

			if (obj is System.DBNull ==false)
			{
				val = (double)obj;
			}
  			return val;
		}
				
				

		/// <summary>
		/// 获得对象实体列表
		/// </summary>
        public TrackedList<hammergo.Model.CalculateValue> GetList()
		{
			string sql="select [calculateParamID] ,[Date] ,[Val]  from [CalculateValue]";
			 return QueryModelList(sql, null);
		}			
		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.CalculateValue> GetListBycalculateParamID(System.Guid  CalculateParamID)
		{
			string sql="select [calculateParamID] ,[Date] ,[Val]  from [CalculateValue] where calculateParamID =@calculateParamID";
			SqlParameter parameter=new SqlParameter("@calculateParamID",SqlDbType.UniqueIdentifier);
			parameter.Value=CalculateParamID;
			return QueryModelList(sql, parameter);
		}	
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountBycalculateParamID(System.Guid  CalculateParamID)
		{
			string sql="select count(1) from CalculateValue where calculateParamID =@calculateParamID";
			SqlParameter parameter=new SqlParameter("@calculateParamID",SqlDbType.UniqueIdentifier);
			parameter.Value=CalculateParamID;
			object obj= DbHelperSQL.ExecuteScalar(CommandType.Text,sql, parameter);
			
			return (int)obj;
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.CalculateValue> GetListByDate(System.DateTime  Date)
		{
			string sql="select [calculateParamID] ,[Date] ,[Val]  from [CalculateValue] where Date =@Date";
			SqlParameter parameter=new SqlParameter("@Date",SqlDbType.DateTime);
			parameter.Value=Date;
			return QueryModelList(sql, parameter);
		}	
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByDate(System.DateTime  Date)
		{
			string sql="select count(1) from CalculateValue where Date =@Date";
			SqlParameter parameter=new SqlParameter("@Date",SqlDbType.DateTime);
			parameter.Value=Date;
			object obj= DbHelperSQL.ExecuteScalar(CommandType.Text,sql, parameter);
			
			return (int)obj;
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.CalculateValue> GetListByVal(double  Val)
		{
			string sql="select [calculateParamID] ,[Date] ,[Val]  from [CalculateValue] where Val =@Val";
			SqlParameter parameter=new SqlParameter("@Val",SqlDbType.Float);
			parameter.Value=Val;
			return QueryModelList(sql, parameter);
		}	
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByVal(double  Val)
		{
			string sql="select count(1) from CalculateValue where Val =@Val";
			SqlParameter parameter=new SqlParameter("@Val",SqlDbType.Float);
			parameter.Value=Val;
			object obj= DbHelperSQL.ExecuteScalar(CommandType.Text,sql, parameter);
			
			return (int)obj;
		}
		
		
		
    }
}



