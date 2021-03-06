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
    
    public partial class  ApparatusTypeDAL:IApparatusTypeDAL
    {
		private const int columnsCnt = 2;
		protected string SQL_SELECT_All="select [ApparatusTypeID] ,[TypeName]  from [ApparatusType]";
		protected string SQL_Field="ApparatusType.ApparatusTypeID ,ApparatusType.TypeName ";
		
		
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool ExistsBy_ApparatusTypeID(System.Guid ApparatusTypeID)
		{

			
			SqlParameter[] parameters =new SqlParameter[1];
				parameters[0]= new SqlParameter("@ApparatusTypeID", SqlDbType.UniqueIdentifier);
				parameters[0].Value=ApparatusTypeID;
				
			
			string sql="select count(1) from ApparatusType where [ApparatusTypeID] = @ApparatusTypeID";
			return DbHelperSQL.Exists(sql, parameters);
		}
		
		/// <summary>
		/// 更新记录的记录
		/// </summary>
		public bool UpdateBy_ApparatusTypeID( System.Guid ApparatusTypeID, System.Guid newApparatusTypeID)
		{
			SqlParameter[] parameters =new SqlParameter[2];
				parameters[0]= new SqlParameter("@newApparatusTypeID", SqlDbType.UniqueIdentifier);
				parameters[0].Value=newApparatusTypeID;
				
				parameters[1]= new SqlParameter("@ApparatusTypeID", SqlDbType.UniqueIdentifier);
				parameters[1].Value=ApparatusTypeID;
				
			
			string sql="update [ApparatusType] set [ApparatusTypeID]=@newApparatusTypeID where [ApparatusTypeID] = @ApparatusTypeID";
			
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
		public bool UpdateBy_ApparatusTypeID( System.Guid ApparatusTypeID, System.Guid newApparatusTypeID,System.Data.IDbTransaction trans)
		{
			SqlParameter[] parameters =new SqlParameter[2];
				parameters[0]= new SqlParameter("@newApparatusTypeID", SqlDbType.UniqueIdentifier);
				parameters[0].Value=newApparatusTypeID;
				
				parameters[1]= new SqlParameter("@ApparatusTypeID", SqlDbType.UniqueIdentifier);
				parameters[1].Value=ApparatusTypeID;
				
			
			string sql="update [ApparatusType] set [ApparatusTypeID]=@newApparatusTypeID where [ApparatusTypeID] = @ApparatusTypeID";
			
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
		public hammergo.Model.ApparatusType GetModelBy_ApparatusTypeID(System.Guid ApparatusTypeID)
		{
		
			
			SqlParameter[] parameters =new SqlParameter[1];
				parameters[0]= new SqlParameter("@ApparatusTypeID", SqlDbType.UniqueIdentifier);
				parameters[0].Value=ApparatusTypeID;
			string sql="select [ApparatusTypeID] ,[TypeName]  from [ApparatusType] where [ApparatusTypeID] = @ApparatusTypeID";
			return QueryModel(sql, parameters);

		}				
		
		
		
		
		



		
		
		/// <summary>
		/// 为model创建参数数组
		/// </summary>
		private SqlParameter[] createAllParams(hammergo.Model.ApparatusType model)
        {
			SqlParameter[] parameters =new SqlParameter[2];
			parameters[0]= new SqlParameter("@ApparatusTypeID", SqlDbType.UniqueIdentifier);
					if (model.ApparatusTypeID.HasValue)
					{
						parameters[0].Value = model.ApparatusTypeID;
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
		public bool Add(hammergo.Model.ApparatusType model)
		{
			string sql="insert into ApparatusType ([ApparatusTypeID] ,[TypeName]  ) values (@ApparatusTypeID,@TypeName)";
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
		public bool Add(hammergo.Model.ApparatusType model,System.Data.IDbTransaction trans)
		{
			string sql="insert into ApparatusType ([ApparatusTypeID] ,[TypeName]  ) values (@ApparatusTypeID,@TypeName)";
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
		public bool Update(hammergo.Model.ApparatusType model)
		{
			string sql="update [ApparatusType] set [ApparatusTypeID]=@ApparatusTypeID,[TypeName]=@TypeName where [ApparatusTypeID]=@ApparatusTypeID";
			
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
		public bool Update(hammergo.Model.ApparatusType model,System.Data.IDbTransaction trans)
		{
			string sql="update [ApparatusType] set [ApparatusTypeID]=@ApparatusTypeID,[TypeName]=@TypeName where [ApparatusTypeID]=@ApparatusTypeID";
			
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
		public bool Delete(System.Guid ApparatusTypeID)
		{
			string sql="delete from ApparatusType  where [ApparatusTypeID]=@ApparatusTypeID";
			SqlParameter[] parameters =new SqlParameter[1];
				parameters[0]= new SqlParameter("@ApparatusTypeID", SqlDbType.UniqueIdentifier);
				parameters[0].Value=ApparatusTypeID;
				
				if (DbHelperSQL.ExecuteNonQuery(CommandType.Text, sql, parameters)== 0)
				{
					return false;
				}
	
				return true;
		}		
		
		
		/// <summary>
		/// 使用事务删除一条数据
		/// </summary>
		public bool Delete(System.Guid ApparatusTypeID,System.Data.IDbTransaction trans)
		{
			string sql="delete from ApparatusType  where [ApparatusTypeID]=@ApparatusTypeID";
			SqlParameter[] parameters =new SqlParameter[1];
				parameters[0]= new SqlParameter("@ApparatusTypeID", SqlDbType.UniqueIdentifier);
				parameters[0].Value=ApparatusTypeID;
				
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
        private hammergo.Model.ApparatusType createModel(object[] values)
        {
            hammergo.Model.ApparatusType model = null;
            //创建model对象
            if (values != null)
            {
                model = new hammergo.Model.ApparatusType();
				object obj=null;
                //赋值
					 obj = values[0];
					if (obj is System.DBNull)
					{
						model.ApparatusTypeID = null;
					}
					else
					{
						model.ApparatusTypeID = (System.Guid)obj;
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
        private hammergo.Model.ApparatusType QueryModel(string sql, params SqlParameter[] parameters)
        {
            //Set up a return value
            hammergo.Model.ApparatusType model = null;

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
        private TrackedList<hammergo.Model.ApparatusType> QueryModelList(string sql, params SqlParameter[] parameters)
        {
            TrackedList<hammergo.Model.ApparatusType> list = new TrackedList<hammergo.Model.ApparatusType>(100);


            hammergo.Model.ApparatusType model = null;
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
        public TrackedList<hammergo.Model.ApparatusType> GetList()
		{
			string sql="select [ApparatusTypeID] ,[TypeName]  from [ApparatusType]";
			 return QueryModelList(sql, null);
		}			
		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.ApparatusType> GetListByApparatusTypeID(System.Guid  ApparatusTypeID)
		{
			string sql="select [ApparatusTypeID] ,[TypeName]  from [ApparatusType] where ApparatusTypeID =@ApparatusTypeID";
			SqlParameter parameter=new SqlParameter("@ApparatusTypeID",SqlDbType.UniqueIdentifier);
			parameter.Value=ApparatusTypeID;
			return QueryModelList(sql, parameter);
		}	
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByApparatusTypeID(System.Guid  ApparatusTypeID)
		{
			string sql="select count(1) from ApparatusType where ApparatusTypeID =@ApparatusTypeID";
			SqlParameter parameter=new SqlParameter("@ApparatusTypeID",SqlDbType.UniqueIdentifier);
			parameter.Value=ApparatusTypeID;
			object obj= DbHelperSQL.ExecuteScalar(CommandType.Text,sql, parameter);
			
			return (int)obj;
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.ApparatusType> GetListByTypeName(string  TypeName)
		{
			string sql="select [ApparatusTypeID] ,[TypeName]  from [ApparatusType] where TypeName =@TypeName";
			SqlParameter parameter=new SqlParameter("@TypeName",SqlDbType.NVarChar);
			parameter.Value=TypeName;
			return QueryModelList(sql, parameter);
		}	
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByTypeName(string  TypeName)
		{
			string sql="select count(1) from ApparatusType where TypeName =@TypeName";
			SqlParameter parameter=new SqlParameter("@TypeName",SqlDbType.NVarChar);
			parameter.Value=TypeName;
			object obj= DbHelperSQL.ExecuteScalar(CommandType.Text,sql, parameter);
			
			return (int)obj;
		}
		
		
		
    }
}



