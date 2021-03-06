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
    
    public partial class  AppCollectionDAL:IAppCollectionDAL
    {
		private const int columnsCnt = 5;
		protected string SQL_SELECT_All="select [AppCollectionID] ,[taskTypeID] ,[CollectionName] ,[Description] ,[Order]  from [AppCollection]";
		protected string SQL_Field="AppCollection.AppCollectionID ,AppCollection.taskTypeID ,AppCollection.CollectionName ,AppCollection.Description ,AppCollection.Order ";
		
		
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool ExistsBy_CollectionName_taskTypeID(string CollectionName,int taskTypeID)
		{

			
			SqlParameter[] parameters =new SqlParameter[2];
				parameters[0]= new SqlParameter("@CollectionName", SqlDbType.NVarChar);
				parameters[0].Value=CollectionName;
				
				parameters[1]= new SqlParameter("@taskTypeID", SqlDbType.Int);
				parameters[1].Value=taskTypeID;
				
			
			string sql="select count(1) from AppCollection where [CollectionName] = @CollectionName and [taskTypeID] = @taskTypeID";
			return DbHelperSQL.Exists(sql, parameters);
		}
		
		/// <summary>
		/// 更新记录的记录
		/// </summary>
		public bool UpdateBy_CollectionName_taskTypeID( string CollectionName,int taskTypeID, string newCollectionName,int newtaskTypeID)
		{
			SqlParameter[] parameters =new SqlParameter[4];
				parameters[0]= new SqlParameter("@newCollectionName", SqlDbType.NVarChar);
				parameters[0].Value=newCollectionName;
				
				parameters[1]= new SqlParameter("@newtaskTypeID", SqlDbType.Int);
				parameters[1].Value=newtaskTypeID;
				
				parameters[2]= new SqlParameter("@CollectionName", SqlDbType.NVarChar);
				parameters[2].Value=CollectionName;
				
				parameters[3]= new SqlParameter("@taskTypeID", SqlDbType.Int);
				parameters[3].Value=taskTypeID;
				
			
			string sql="update [AppCollection] set [CollectionName]=@newCollectionName,[taskTypeID]=@newtaskTypeID where [CollectionName] = @CollectionName and [taskTypeID] = @taskTypeID";
			
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
		public bool UpdateBy_CollectionName_taskTypeID( string CollectionName,int taskTypeID, string newCollectionName,int newtaskTypeID,System.Data.IDbTransaction trans)
		{
			SqlParameter[] parameters =new SqlParameter[4];
				parameters[0]= new SqlParameter("@newCollectionName", SqlDbType.NVarChar);
				parameters[0].Value=newCollectionName;
				
				parameters[1]= new SqlParameter("@newtaskTypeID", SqlDbType.Int);
				parameters[1].Value=newtaskTypeID;
				
				parameters[2]= new SqlParameter("@CollectionName", SqlDbType.NVarChar);
				parameters[2].Value=CollectionName;
				
				parameters[3]= new SqlParameter("@taskTypeID", SqlDbType.Int);
				parameters[3].Value=taskTypeID;
				
			
			string sql="update [AppCollection] set [CollectionName]=@newCollectionName,[taskTypeID]=@newtaskTypeID where [CollectionName] = @CollectionName and [taskTypeID] = @taskTypeID";
			
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
		public hammergo.Model.AppCollection GetModelBy_CollectionName_taskTypeID(string CollectionName,int taskTypeID)
		{
		
			
			SqlParameter[] parameters =new SqlParameter[2];
				parameters[0]= new SqlParameter("@CollectionName", SqlDbType.NVarChar);
				parameters[0].Value=CollectionName;
				parameters[1]= new SqlParameter("@taskTypeID", SqlDbType.Int);
				parameters[1].Value=taskTypeID;
			string sql="select [AppCollectionID] ,[taskTypeID] ,[CollectionName] ,[Description] ,[Order]  from [AppCollection] where [CollectionName] = @CollectionName and [taskTypeID] = @taskTypeID";
			return QueryModel(sql, parameters);

		}				
		
		
		
		
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool ExistsBy_AppCollectionID(System.Guid AppCollectionID)
		{

			
			SqlParameter[] parameters =new SqlParameter[1];
				parameters[0]= new SqlParameter("@AppCollectionID", SqlDbType.UniqueIdentifier);
				parameters[0].Value=AppCollectionID;
				
			
			string sql="select count(1) from AppCollection where [AppCollectionID] = @AppCollectionID";
			return DbHelperSQL.Exists(sql, parameters);
		}
		
		/// <summary>
		/// 更新记录的记录
		/// </summary>
		public bool UpdateBy_AppCollectionID( System.Guid AppCollectionID, System.Guid newAppCollectionID)
		{
			SqlParameter[] parameters =new SqlParameter[2];
				parameters[0]= new SqlParameter("@newAppCollectionID", SqlDbType.UniqueIdentifier);
				parameters[0].Value=newAppCollectionID;
				
				parameters[1]= new SqlParameter("@AppCollectionID", SqlDbType.UniqueIdentifier);
				parameters[1].Value=AppCollectionID;
				
			
			string sql="update [AppCollection] set [AppCollectionID]=@newAppCollectionID where [AppCollectionID] = @AppCollectionID";
			
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
		public bool UpdateBy_AppCollectionID( System.Guid AppCollectionID, System.Guid newAppCollectionID,System.Data.IDbTransaction trans)
		{
			SqlParameter[] parameters =new SqlParameter[2];
				parameters[0]= new SqlParameter("@newAppCollectionID", SqlDbType.UniqueIdentifier);
				parameters[0].Value=newAppCollectionID;
				
				parameters[1]= new SqlParameter("@AppCollectionID", SqlDbType.UniqueIdentifier);
				parameters[1].Value=AppCollectionID;
				
			
			string sql="update [AppCollection] set [AppCollectionID]=@newAppCollectionID where [AppCollectionID] = @AppCollectionID";
			
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
		public hammergo.Model.AppCollection GetModelBy_AppCollectionID(System.Guid AppCollectionID)
		{
		
			
			SqlParameter[] parameters =new SqlParameter[1];
				parameters[0]= new SqlParameter("@AppCollectionID", SqlDbType.UniqueIdentifier);
				parameters[0].Value=AppCollectionID;
			string sql="select [AppCollectionID] ,[taskTypeID] ,[CollectionName] ,[Description] ,[Order]  from [AppCollection] where [AppCollectionID] = @AppCollectionID";
			return QueryModel(sql, parameters);

		}				
		
		
		
		
		



		
		
		/// <summary>
		/// 为model创建参数数组
		/// </summary>
		private SqlParameter[] createAllParams(hammergo.Model.AppCollection model)
        {
			SqlParameter[] parameters =new SqlParameter[5];
			parameters[0]= new SqlParameter("@AppCollectionID", SqlDbType.UniqueIdentifier);
					if (model.AppCollectionID.HasValue)
					{
						parameters[0].Value = model.AppCollectionID;
					}
					else
					{
						parameters[0].Value = DBNull.Value;
					}
			parameters[1]= new SqlParameter("@taskTypeID", SqlDbType.Int);
					if (model.TaskTypeID.HasValue)
					{
						parameters[1].Value = model.TaskTypeID;
					}
					else
					{
						parameters[1].Value = DBNull.Value;
					}
			parameters[2]= new SqlParameter("@CollectionName", SqlDbType.NVarChar);
					if(model.CollectionName != null)
					{
						parameters[2].Value =model.CollectionName;
					}else
					{
						parameters[2].Value =DBNull.Value;
					}
			parameters[3]= new SqlParameter("@Description", SqlDbType.NVarChar);
					if(model.Description != null)
					{
						parameters[3].Value =model.Description;
					}else
					{
						parameters[3].Value =DBNull.Value;
					}
			parameters[4]= new SqlParameter("@Order", SqlDbType.Int);
					if (model.Order.HasValue)
					{
						parameters[4].Value = model.Order;
					}
					else
					{
						parameters[4].Value = DBNull.Value;
					}
			return  parameters;
		}
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(hammergo.Model.AppCollection model)
		{
			string sql="insert into AppCollection ([AppCollectionID] ,[taskTypeID] ,[CollectionName] ,[Description] ,[Order]  ) values (@AppCollectionID,@taskTypeID,@CollectionName,@Description,@Order)";
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
		public bool Add(hammergo.Model.AppCollection model,System.Data.IDbTransaction trans)
		{
			string sql="insert into AppCollection ([AppCollectionID] ,[taskTypeID] ,[CollectionName] ,[Description] ,[Order]  ) values (@AppCollectionID,@taskTypeID,@CollectionName,@Description,@Order)";
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
		public bool Update(hammergo.Model.AppCollection model)
		{
			string sql="update [AppCollection] set [AppCollectionID]=@AppCollectionID,[taskTypeID]=@taskTypeID,[CollectionName]=@CollectionName,[Description]=@Description,[Order]=@Order where [AppCollectionID]=@AppCollectionID";
			
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
		public bool Update(hammergo.Model.AppCollection model,System.Data.IDbTransaction trans)
		{
			string sql="update [AppCollection] set [AppCollectionID]=@AppCollectionID,[taskTypeID]=@taskTypeID,[CollectionName]=@CollectionName,[Description]=@Description,[Order]=@Order where [AppCollectionID]=@AppCollectionID";
			
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
		public bool Delete(System.Guid AppCollectionID)
		{
			string sql="delete from AppCollection  where [AppCollectionID]=@AppCollectionID";
			SqlParameter[] parameters =new SqlParameter[1];
				parameters[0]= new SqlParameter("@AppCollectionID", SqlDbType.UniqueIdentifier);
				parameters[0].Value=AppCollectionID;
				
				if (DbHelperSQL.ExecuteNonQuery(CommandType.Text, sql, parameters)== 0)
				{
					return false;
				}
	
				return true;
		}		
		
		
		/// <summary>
		/// 使用事务删除一条数据
		/// </summary>
		public bool Delete(System.Guid AppCollectionID,System.Data.IDbTransaction trans)
		{
			string sql="delete from AppCollection  where [AppCollectionID]=@AppCollectionID";
			SqlParameter[] parameters =new SqlParameter[1];
				parameters[0]= new SqlParameter("@AppCollectionID", SqlDbType.UniqueIdentifier);
				parameters[0].Value=AppCollectionID;
				
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
        private hammergo.Model.AppCollection createModel(object[] values)
        {
            hammergo.Model.AppCollection model = null;
            //创建model对象
            if (values != null)
            {
                model = new hammergo.Model.AppCollection();
				object obj=null;
                //赋值
					 obj = values[0];
					if (obj is System.DBNull)
					{
						model.AppCollectionID = null;
					}
					else
					{
						model.AppCollectionID = (System.Guid)obj;
					}
					 obj = values[1];
					if (obj is System.DBNull)
					{
						model.TaskTypeID = null;
					}
					else
					{
						model.TaskTypeID = (int)obj;
					}
					 obj = values[2];
					if (obj is System.DBNull)
					{
						model.CollectionName = null;
					}
					else
					{
						model.CollectionName = (string)obj;
					}
					 obj = values[3];
					if (obj is System.DBNull)
					{
						model.Description = null;
					}
					else
					{
						model.Description = (string)obj;
					}
					 obj = values[4];
					if (obj is System.DBNull)
					{
						model.Order = null;
					}
					else
					{
						model.Order = (int)obj;
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
        private hammergo.Model.AppCollection QueryModel(string sql, params SqlParameter[] parameters)
        {
            //Set up a return value
            hammergo.Model.AppCollection model = null;

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
        private TrackedList<hammergo.Model.AppCollection> QueryModelList(string sql, params SqlParameter[] parameters)
        {
            TrackedList<hammergo.Model.AppCollection> list = new TrackedList<hammergo.Model.AppCollection>(100);


            hammergo.Model.AppCollection model = null;
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
		/// 获取taskTypeID的最大值
		/// </summary>				
		public int? GetMaxtaskTypeID()
		{
			int? val=null;
			string sql="select max(taskTypeID) from AppCollection";
			object obj =DbHelperSQL.ExecuteScalar(CommandType.Text, sql);

			if (obj is System.DBNull ==false)
			{
				val = (int)obj;
			}
  			return val;
		}
				
				
				
		/// <summary>
		/// 获取Order的最大值
		/// </summary>				
		public int? GetMaxOrder()
		{
			int? val=null;
			string sql="select max(Order) from AppCollection";
			object obj =DbHelperSQL.ExecuteScalar(CommandType.Text, sql);

			if (obj is System.DBNull ==false)
			{
				val = (int)obj;
			}
  			return val;
		}
				
				
		
		
				
		/// <summary>
		/// 获取taskTypeID的最小值
		/// </summary>				
		public int? GetMintaskTypeID()
		{
			int? val=null;
			string sql="select min(taskTypeID) from AppCollection";
			object obj =DbHelperSQL.ExecuteScalar(CommandType.Text, sql);

			if (obj is System.DBNull ==false)
			{
				val = (int)obj;
			}
  			return val;
		}
				
				
				
		/// <summary>
		/// 获取Order的最小值
		/// </summary>				
		public int? GetMinOrder()
		{
			int? val=null;
			string sql="select min(Order) from AppCollection";
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
        public TrackedList<hammergo.Model.AppCollection> GetList()
		{
			string sql="select [AppCollectionID] ,[taskTypeID] ,[CollectionName] ,[Description] ,[Order]  from [AppCollection]";
			 return QueryModelList(sql, null);
		}			
		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.AppCollection> GetListByAppCollectionID(System.Guid  AppCollectionID)
		{
			string sql="select [AppCollectionID] ,[taskTypeID] ,[CollectionName] ,[Description] ,[Order]  from [AppCollection] where AppCollectionID =@AppCollectionID";
			SqlParameter parameter=new SqlParameter("@AppCollectionID",SqlDbType.UniqueIdentifier);
			parameter.Value=AppCollectionID;
			return QueryModelList(sql, parameter);
		}	
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByAppCollectionID(System.Guid  AppCollectionID)
		{
			string sql="select count(1) from AppCollection where AppCollectionID =@AppCollectionID";
			SqlParameter parameter=new SqlParameter("@AppCollectionID",SqlDbType.UniqueIdentifier);
			parameter.Value=AppCollectionID;
			object obj= DbHelperSQL.ExecuteScalar(CommandType.Text,sql, parameter);
			
			return (int)obj;
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.AppCollection> GetListBytaskTypeID(int  TaskTypeID)
		{
			string sql="select [AppCollectionID] ,[taskTypeID] ,[CollectionName] ,[Description] ,[Order]  from [AppCollection] where taskTypeID =@taskTypeID";
			SqlParameter parameter=new SqlParameter("@taskTypeID",SqlDbType.Int);
			parameter.Value=TaskTypeID;
			return QueryModelList(sql, parameter);
		}	
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountBytaskTypeID(int  TaskTypeID)
		{
			string sql="select count(1) from AppCollection where taskTypeID =@taskTypeID";
			SqlParameter parameter=new SqlParameter("@taskTypeID",SqlDbType.Int);
			parameter.Value=TaskTypeID;
			object obj= DbHelperSQL.ExecuteScalar(CommandType.Text,sql, parameter);
			
			return (int)obj;
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.AppCollection> GetListByCollectionName(string  CollectionName)
		{
			string sql="select [AppCollectionID] ,[taskTypeID] ,[CollectionName] ,[Description] ,[Order]  from [AppCollection] where CollectionName =@CollectionName";
			SqlParameter parameter=new SqlParameter("@CollectionName",SqlDbType.NVarChar);
			parameter.Value=CollectionName;
			return QueryModelList(sql, parameter);
		}	
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByCollectionName(string  CollectionName)
		{
			string sql="select count(1) from AppCollection where CollectionName =@CollectionName";
			SqlParameter parameter=new SqlParameter("@CollectionName",SqlDbType.NVarChar);
			parameter.Value=CollectionName;
			object obj= DbHelperSQL.ExecuteScalar(CommandType.Text,sql, parameter);
			
			return (int)obj;
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.AppCollection> GetListByDescription(string  Description)
		{
			string sql="select [AppCollectionID] ,[taskTypeID] ,[CollectionName] ,[Description] ,[Order]  from [AppCollection] where Description =@Description";
			SqlParameter parameter=new SqlParameter("@Description",SqlDbType.NVarChar);
			parameter.Value=Description;
			return QueryModelList(sql, parameter);
		}	
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByDescription(string  Description)
		{
			string sql="select count(1) from AppCollection where Description =@Description";
			SqlParameter parameter=new SqlParameter("@Description",SqlDbType.NVarChar);
			parameter.Value=Description;
			object obj= DbHelperSQL.ExecuteScalar(CommandType.Text,sql, parameter);
			
			return (int)obj;
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.AppCollection> GetListByOrder(int  Order)
		{
			string sql="select [AppCollectionID] ,[taskTypeID] ,[CollectionName] ,[Description] ,[Order]  from [AppCollection] where Order =@Order";
			SqlParameter parameter=new SqlParameter("@Order",SqlDbType.Int);
			parameter.Value=Order;
			return QueryModelList(sql, parameter);
		}	
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByOrder(int  Order)
		{
			string sql="select count(1) from AppCollection where Order =@Order";
			SqlParameter parameter=new SqlParameter("@Order",SqlDbType.Int);
			parameter.Value=Order;
			object obj= DbHelperSQL.ExecuteScalar(CommandType.Text,sql, parameter);
			
			return (int)obj;
		}
		
		
		
    }
}



