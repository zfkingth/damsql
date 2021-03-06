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
    
    public partial class  TaskTypeDAL:ITaskTypeDAL
    {
		private const int columnsCnt = 2;
		protected string SQL_SELECT_All="select [TaskTypeID] ,[TypeName]  from [TaskType]";
		protected string SQL_Field="TaskType.TaskTypeID ,TaskType.TypeName ";
		
		
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool ExistsBy_TypeName(string TypeName)
		{

			
			SqlParameter[] parameters =new SqlParameter[1];
				parameters[0]= new SqlParameter("@TypeName", SqlDbType.NVarChar);
				parameters[0].Value=TypeName;
				
			
			string sql="select count(1) from TaskType where [TypeName] = @TypeName";
			return DbHelperSQL.Exists(sql, parameters);
		}
		
		/// <summary>
		/// 更新记录的记录
		/// </summary>
		public bool UpdateBy_TypeName( string TypeName, string newTypeName)
		{
			SqlParameter[] parameters =new SqlParameter[2];
				parameters[0]= new SqlParameter("@newTypeName", SqlDbType.NVarChar);
				parameters[0].Value=newTypeName;
				
				parameters[1]= new SqlParameter("@TypeName", SqlDbType.NVarChar);
				parameters[1].Value=TypeName;
				
			
			string sql="update [TaskType] set [TypeName]=@newTypeName where [TypeName] = @TypeName";
			
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
		public bool UpdateBy_TypeName( string TypeName, string newTypeName,System.Data.IDbTransaction trans)
		{
			SqlParameter[] parameters =new SqlParameter[2];
				parameters[0]= new SqlParameter("@newTypeName", SqlDbType.NVarChar);
				parameters[0].Value=newTypeName;
				
				parameters[1]= new SqlParameter("@TypeName", SqlDbType.NVarChar);
				parameters[1].Value=TypeName;
				
			
			string sql="update [TaskType] set [TypeName]=@newTypeName where [TypeName] = @TypeName";
			
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
		public hammergo.Model.TaskType GetModelBy_TypeName(string TypeName)
		{
		
			
			SqlParameter[] parameters =new SqlParameter[1];
				parameters[0]= new SqlParameter("@TypeName", SqlDbType.NVarChar);
				parameters[0].Value=TypeName;
			string sql="select [TaskTypeID] ,[TypeName]  from [TaskType] where [TypeName] = @TypeName";
			return QueryModel(sql, parameters);

		}				
		
		
		
		
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool ExistsBy_TaskTypeID(int TaskTypeID)
		{

			
			SqlParameter[] parameters =new SqlParameter[1];
				parameters[0]= new SqlParameter("@TaskTypeID", SqlDbType.Int);
				parameters[0].Value=TaskTypeID;
				
			
			string sql="select count(1) from TaskType where [TaskTypeID] = @TaskTypeID";
			return DbHelperSQL.Exists(sql, parameters);
		}
		
		/// <summary>
		/// 更新记录的记录
		/// </summary>
		public bool UpdateBy_TaskTypeID( int TaskTypeID, int newTaskTypeID)
		{
			SqlParameter[] parameters =new SqlParameter[2];
				parameters[0]= new SqlParameter("@newTaskTypeID", SqlDbType.Int);
				parameters[0].Value=newTaskTypeID;
				
				parameters[1]= new SqlParameter("@TaskTypeID", SqlDbType.Int);
				parameters[1].Value=TaskTypeID;
				
			
			string sql="update [TaskType] set [TaskTypeID]=@newTaskTypeID where [TaskTypeID] = @TaskTypeID";
			
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
		public bool UpdateBy_TaskTypeID( int TaskTypeID, int newTaskTypeID,System.Data.IDbTransaction trans)
		{
			SqlParameter[] parameters =new SqlParameter[2];
				parameters[0]= new SqlParameter("@newTaskTypeID", SqlDbType.Int);
				parameters[0].Value=newTaskTypeID;
				
				parameters[1]= new SqlParameter("@TaskTypeID", SqlDbType.Int);
				parameters[1].Value=TaskTypeID;
				
			
			string sql="update [TaskType] set [TaskTypeID]=@newTaskTypeID where [TaskTypeID] = @TaskTypeID";
			
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
		public hammergo.Model.TaskType GetModelBy_TaskTypeID(int TaskTypeID)
		{
		
			
			SqlParameter[] parameters =new SqlParameter[1];
				parameters[0]= new SqlParameter("@TaskTypeID", SqlDbType.Int);
				parameters[0].Value=TaskTypeID;
			string sql="select [TaskTypeID] ,[TypeName]  from [TaskType] where [TaskTypeID] = @TaskTypeID";
			return QueryModel(sql, parameters);

		}				
		
		
		
		
		



		
		
		/// <summary>
		/// 为model创建参数数组
		/// </summary>
		private SqlParameter[] createAllParams(hammergo.Model.TaskType model)
        {
			SqlParameter[] parameters =new SqlParameter[2];
			parameters[0]= new SqlParameter("@TaskTypeID", SqlDbType.Int);
					if (model.TaskTypeID.HasValue)
					{
						parameters[0].Value = model.TaskTypeID;
					}
					else
					{
						parameters[0].Value = DBNull.Value;
					}
			parameters[1]= new SqlParameter("@TypeName", SqlDbType.NVarChar);
					if(model.TypeName != null)
					{
						parameters[1].Value =model.TypeName;
					}else
					{
						parameters[1].Value =DBNull.Value;
					}
			return  parameters;
		}
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(hammergo.Model.TaskType model)
		{
			string sql="insert into TaskType ([TaskTypeID] ,[TypeName]  ) values (@TaskTypeID,@TypeName)";
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
		public bool Add(hammergo.Model.TaskType model,System.Data.IDbTransaction trans)
		{
			string sql="insert into TaskType ([TaskTypeID] ,[TypeName]  ) values (@TaskTypeID,@TypeName)";
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
		public bool Update(hammergo.Model.TaskType model)
		{
			string sql="update [TaskType] set [TaskTypeID]=@TaskTypeID,[TypeName]=@TypeName where [TaskTypeID]=@TaskTypeID";
			
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
		public bool Update(hammergo.Model.TaskType model,System.Data.IDbTransaction trans)
		{
			string sql="update [TaskType] set [TaskTypeID]=@TaskTypeID,[TypeName]=@TypeName where [TaskTypeID]=@TaskTypeID";
			
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
		public bool Delete(int TaskTypeID)
		{
			string sql="delete from TaskType  where [TaskTypeID]=@TaskTypeID";
			SqlParameter[] parameters =new SqlParameter[1];
				parameters[0]= new SqlParameter("@TaskTypeID", SqlDbType.Int);
				parameters[0].Value=TaskTypeID;
				
				if (DbHelperSQL.ExecuteNonQuery(CommandType.Text, sql, parameters)== 0)
				{
					return false;
				}
	
				return true;
		}		
		
		
		/// <summary>
		/// 使用事务删除一条数据
		/// </summary>
		public bool Delete(int TaskTypeID,System.Data.IDbTransaction trans)
		{
			string sql="delete from TaskType  where [TaskTypeID]=@TaskTypeID";
			SqlParameter[] parameters =new SqlParameter[1];
				parameters[0]= new SqlParameter("@TaskTypeID", SqlDbType.Int);
				parameters[0].Value=TaskTypeID;
				
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
        private hammergo.Model.TaskType createModel(object[] values)
        {
            hammergo.Model.TaskType model = null;
            //创建model对象
            if (values != null)
            {
                model = new hammergo.Model.TaskType();
				object obj=null;
                //赋值
					 obj = values[0];
					if (obj is System.DBNull)
					{
						model.TaskTypeID = null;
					}
					else
					{
						model.TaskTypeID = (int)obj;
					}
					 obj = values[1];
					if (obj is System.DBNull)
					{
						model.TypeName = null;
					}
					else
					{
						model.TypeName = (string)obj;
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
        private hammergo.Model.TaskType QueryModel(string sql, params SqlParameter[] parameters)
        {
            //Set up a return value
            hammergo.Model.TaskType model = null;

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
        private TrackedList<hammergo.Model.TaskType> QueryModelList(string sql, params SqlParameter[] parameters)
        {
            TrackedList<hammergo.Model.TaskType> list = new TrackedList<hammergo.Model.TaskType>(100);


            hammergo.Model.TaskType model = null;
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
		/// 获取TaskTypeID的最大值
		/// </summary>				
		public int? GetMaxTaskTypeID()
		{
			int? val=null;
			string sql="select max(TaskTypeID) from TaskType";
			object obj =DbHelperSQL.ExecuteScalar(CommandType.Text, sql);

			if (obj is System.DBNull ==false)
			{
				val = (int)obj;
			}
  			return val;
		}
				
				
		
		
				
		/// <summary>
		/// 获取TaskTypeID的最小值
		/// </summary>				
		public int? GetMinTaskTypeID()
		{
			int? val=null;
			string sql="select min(TaskTypeID) from TaskType";
			object obj =DbHelperSQL.ExecuteScalar(CommandType.Text, sql);

			if (obj is System.DBNull ==false)
			{
				val = (int)obj;
			}
  			return val;
		}
				
				

		/// <summary>
		/// 获得对象实体列表
		/// </summary>
        public TrackedList<hammergo.Model.TaskType> GetList()
		{
			string sql="select [TaskTypeID] ,[TypeName]  from [TaskType]";
			 return QueryModelList(sql, null);
		}			
		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.TaskType> GetListByTaskTypeID(int  TaskTypeID)
		{
			string sql="select [TaskTypeID] ,[TypeName]  from [TaskType] where TaskTypeID =@TaskTypeID";
			SqlParameter parameter=new SqlParameter("@TaskTypeID",SqlDbType.Int);
			parameter.Value=TaskTypeID;
			return QueryModelList(sql, parameter);
		}	
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByTaskTypeID(int  TaskTypeID)
		{
			string sql="select count(1) from TaskType where TaskTypeID =@TaskTypeID";
			SqlParameter parameter=new SqlParameter("@TaskTypeID",SqlDbType.Int);
			parameter.Value=TaskTypeID;
			object obj= DbHelperSQL.ExecuteScalar(CommandType.Text,sql, parameter);
			
			return (int)obj;
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.TaskType> GetListByTypeName(string  TypeName)
		{
			string sql="select [TaskTypeID] ,[TypeName]  from [TaskType] where TypeName =@TypeName";
			SqlParameter parameter=new SqlParameter("@TypeName",SqlDbType.NVarChar);
			parameter.Value=TypeName;
			return QueryModelList(sql, parameter);
		}	
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByTypeName(string  TypeName)
		{
			string sql="select count(1) from TaskType where TypeName =@TypeName";
			SqlParameter parameter=new SqlParameter("@TypeName",SqlDbType.NVarChar);
			parameter.Value=TypeName;
			object obj= DbHelperSQL.ExecuteScalar(CommandType.Text,sql, parameter);
			
			return (int)obj;
		}
		
		
		
    }
}



