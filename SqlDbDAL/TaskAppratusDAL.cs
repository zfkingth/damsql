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
    
    public partial class  TaskAppratusDAL:ITaskAppratusDAL
    {
		private const int columnsCnt = 3;
		protected string SQL_SELECT_All="select [appCollectionID] ,[appName] ,[Order]  from [TaskAppratus]";
		protected string SQL_Field="TaskAppratus.appCollectionID ,TaskAppratus.appName ,TaskAppratus.Order ";
		
		
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool ExistsBy_appCollectionID_appName(System.Guid appCollectionID,string appName)
		{

			
			SqlParameter[] parameters =new SqlParameter[2];
				parameters[0]= new SqlParameter("@appCollectionID", SqlDbType.UniqueIdentifier);
				parameters[0].Value=appCollectionID;
				
				parameters[1]= new SqlParameter("@appName", SqlDbType.NVarChar);
				parameters[1].Value=appName;
				
			
			string sql="select count(1) from TaskAppratus where [appCollectionID] = @appCollectionID and [appName] = @appName";
			return DbHelperSQL.Exists(sql, parameters);
		}
		
		/// <summary>
		/// 更新记录的记录
		/// </summary>
		public bool UpdateBy_appCollectionID_appName( System.Guid appCollectionID,string appName, System.Guid newappCollectionID,string newappName)
		{
			SqlParameter[] parameters =new SqlParameter[4];
				parameters[0]= new SqlParameter("@newappCollectionID", SqlDbType.UniqueIdentifier);
				parameters[0].Value=newappCollectionID;
				
				parameters[1]= new SqlParameter("@newappName", SqlDbType.NVarChar);
				parameters[1].Value=newappName;
				
				parameters[2]= new SqlParameter("@appCollectionID", SqlDbType.UniqueIdentifier);
				parameters[2].Value=appCollectionID;
				
				parameters[3]= new SqlParameter("@appName", SqlDbType.NVarChar);
				parameters[3].Value=appName;
				
			
			string sql="update [TaskAppratus] set [appCollectionID]=@newappCollectionID,[appName]=@newappName where [appCollectionID] = @appCollectionID and [appName] = @appName";
			
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
		public bool UpdateBy_appCollectionID_appName( System.Guid appCollectionID,string appName, System.Guid newappCollectionID,string newappName,System.Data.IDbTransaction trans)
		{
			SqlParameter[] parameters =new SqlParameter[4];
				parameters[0]= new SqlParameter("@newappCollectionID", SqlDbType.UniqueIdentifier);
				parameters[0].Value=newappCollectionID;
				
				parameters[1]= new SqlParameter("@newappName", SqlDbType.NVarChar);
				parameters[1].Value=newappName;
				
				parameters[2]= new SqlParameter("@appCollectionID", SqlDbType.UniqueIdentifier);
				parameters[2].Value=appCollectionID;
				
				parameters[3]= new SqlParameter("@appName", SqlDbType.NVarChar);
				parameters[3].Value=appName;
				
			
			string sql="update [TaskAppratus] set [appCollectionID]=@newappCollectionID,[appName]=@newappName where [appCollectionID] = @appCollectionID and [appName] = @appName";
			
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
		public hammergo.Model.TaskAppratus GetModelBy_appCollectionID_appName(System.Guid appCollectionID,string appName)
		{
		
			
			SqlParameter[] parameters =new SqlParameter[2];
				parameters[0]= new SqlParameter("@appCollectionID", SqlDbType.UniqueIdentifier);
				parameters[0].Value=appCollectionID;
				parameters[1]= new SqlParameter("@appName", SqlDbType.NVarChar);
				parameters[1].Value=appName;
			string sql="select [appCollectionID] ,[appName] ,[Order]  from [TaskAppratus] where [appCollectionID] = @appCollectionID and [appName] = @appName";
			return QueryModel(sql, parameters);

		}				
		
		
		
		
		



		
		
		/// <summary>
		/// 为model创建参数数组
		/// </summary>
		private SqlParameter[] createAllParams(hammergo.Model.TaskAppratus model)
        {
			SqlParameter[] parameters =new SqlParameter[3];
			parameters[0]= new SqlParameter("@appCollectionID", SqlDbType.UniqueIdentifier);
					if (model.AppCollectionID.HasValue)
					{
						parameters[0].Value = model.AppCollectionID;
					}
					else
					{
						parameters[0].Value = DBNull.Value;
					}
			parameters[1]= new SqlParameter("@appName", SqlDbType.NVarChar);
					if(model.AppName != null)
					{
						parameters[1].Value =model.AppName;
					}else
					{
						parameters[1].Value =DBNull.Value;
					}
			parameters[2]= new SqlParameter("@Order", SqlDbType.Int);
					if (model.Order.HasValue)
					{
						parameters[2].Value = model.Order;
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
		public bool Add(hammergo.Model.TaskAppratus model)
		{
			string sql="insert into TaskAppratus ([appCollectionID] ,[appName] ,[Order]  ) values (@appCollectionID,@appName,@Order)";
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
		public bool Add(hammergo.Model.TaskAppratus model,System.Data.IDbTransaction trans)
		{
			string sql="insert into TaskAppratus ([appCollectionID] ,[appName] ,[Order]  ) values (@appCollectionID,@appName,@Order)";
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
		public bool Update(hammergo.Model.TaskAppratus model)
		{
			string sql="update [TaskAppratus] set [appCollectionID]=@appCollectionID,[appName]=@appName,[Order]=@Order where [appCollectionID]=@appCollectionID and [appName]=@appName";
			
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
		public bool Update(hammergo.Model.TaskAppratus model,System.Data.IDbTransaction trans)
		{
			string sql="update [TaskAppratus] set [appCollectionID]=@appCollectionID,[appName]=@appName,[Order]=@Order where [appCollectionID]=@appCollectionID and [appName]=@appName";
			
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
		public bool Delete(System.Guid appCollectionID,string appName)
		{
			string sql="delete from TaskAppratus  where [appCollectionID]=@appCollectionID and [appName]=@appName";
			SqlParameter[] parameters =new SqlParameter[2];
				parameters[0]= new SqlParameter("@appCollectionID", SqlDbType.UniqueIdentifier);
				parameters[0].Value=appCollectionID;
				parameters[1]= new SqlParameter("@appName", SqlDbType.NVarChar);
				parameters[1].Value=appName;
				
				if (DbHelperSQL.ExecuteNonQuery(CommandType.Text, sql, parameters)== 0)
				{
					return false;
				}
	
				return true;
		}		
		
		
		/// <summary>
		/// 使用事务删除一条数据
		/// </summary>
		public bool Delete(System.Guid appCollectionID,string appName,System.Data.IDbTransaction trans)
		{
			string sql="delete from TaskAppratus  where [appCollectionID]=@appCollectionID and [appName]=@appName";
			SqlParameter[] parameters =new SqlParameter[2];
				parameters[0]= new SqlParameter("@appCollectionID", SqlDbType.UniqueIdentifier);
				parameters[0].Value=appCollectionID;
				parameters[1]= new SqlParameter("@appName", SqlDbType.NVarChar);
				parameters[1].Value=appName;
				
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
        private hammergo.Model.TaskAppratus createModel(object[] values)
        {
            hammergo.Model.TaskAppratus model = null;
            //创建model对象
            if (values != null)
            {
                model = new hammergo.Model.TaskAppratus();
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
						model.AppName = null;
					}
					else
					{
						model.AppName = (string)obj;
					}
					 obj = values[2];
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
        private hammergo.Model.TaskAppratus QueryModel(string sql, params SqlParameter[] parameters)
        {
            //Set up a return value
            hammergo.Model.TaskAppratus model = null;

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
        private TrackedList<hammergo.Model.TaskAppratus> QueryModelList(string sql, params SqlParameter[] parameters)
        {
            TrackedList<hammergo.Model.TaskAppratus> list = new TrackedList<hammergo.Model.TaskAppratus>(100);


            hammergo.Model.TaskAppratus model = null;
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
		/// 获取Order的最大值
		/// </summary>				
		public int? GetMaxOrder()
		{
			int? val=null;
			string sql="select max(Order) from TaskAppratus";
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
			string sql="select min(Order) from TaskAppratus";
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
        public TrackedList<hammergo.Model.TaskAppratus> GetList()
		{
			string sql="select [appCollectionID] ,[appName] ,[Order]  from [TaskAppratus]";
			 return QueryModelList(sql, null);
		}			
		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.TaskAppratus> GetListByappCollectionID(System.Guid  AppCollectionID)
		{
			string sql="select [appCollectionID] ,[appName] ,[Order]  from [TaskAppratus] where appCollectionID =@appCollectionID";
			SqlParameter parameter=new SqlParameter("@appCollectionID",SqlDbType.UniqueIdentifier);
			parameter.Value=AppCollectionID;
			return QueryModelList(sql, parameter);
		}	
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByappCollectionID(System.Guid  AppCollectionID)
		{
			string sql="select count(1) from TaskAppratus where appCollectionID =@appCollectionID";
			SqlParameter parameter=new SqlParameter("@appCollectionID",SqlDbType.UniqueIdentifier);
			parameter.Value=AppCollectionID;
			object obj= DbHelperSQL.ExecuteScalar(CommandType.Text,sql, parameter);
			
			return (int)obj;
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.TaskAppratus> GetListByappName(string  AppName)
		{
			string sql="select [appCollectionID] ,[appName] ,[Order]  from [TaskAppratus] where appName =@appName";
			SqlParameter parameter=new SqlParameter("@appName",SqlDbType.NVarChar);
			parameter.Value=AppName;
			return QueryModelList(sql, parameter);
		}	
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByappName(string  AppName)
		{
			string sql="select count(1) from TaskAppratus where appName =@appName";
			SqlParameter parameter=new SqlParameter("@appName",SqlDbType.NVarChar);
			parameter.Value=AppName;
			object obj= DbHelperSQL.ExecuteScalar(CommandType.Text,sql, parameter);
			
			return (int)obj;
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.TaskAppratus> GetListByOrder(int  Order)
		{
			string sql="select [appCollectionID] ,[appName] ,[Order]  from [TaskAppratus] where Order =@Order";
			SqlParameter parameter=new SqlParameter("@Order",SqlDbType.Int);
			parameter.Value=Order;
			return QueryModelList(sql, parameter);
		}	
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByOrder(int  Order)
		{
			string sql="select count(1) from TaskAppratus where Order =@Order";
			SqlParameter parameter=new SqlParameter("@Order",SqlDbType.Int);
			parameter.Value=Order;
			object obj= DbHelperSQL.ExecuteScalar(CommandType.Text,sql, parameter);
			
			return (int)obj;
		}
		
		
		
    }
}



