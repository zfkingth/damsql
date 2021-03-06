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
    
    public partial class  RoleDAL:IRoleDAL
    {
		private const int columnsCnt = 4;
		protected string SQL_SELECT_All="select [RoleID] ,[RoleName] ,[Description] ,[Power]  from [Role]";
		protected string SQL_Field="Role.RoleID ,Role.RoleName ,Role.Description ,Role.Power ";
		
		
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool ExistsBy_RoleID(int RoleID)
		{

			
			SqlParameter[] parameters =new SqlParameter[1];
				parameters[0]= new SqlParameter("@RoleID", SqlDbType.Int);
				parameters[0].Value=RoleID;
				
			
			string sql="select count(1) from Role where [RoleID] = @RoleID";
			return DbHelperSQL.Exists(sql, parameters);
		}
		
		/// <summary>
		/// 更新记录的记录
		/// </summary>
		public bool UpdateBy_RoleID( int RoleID, int newRoleID)
		{
			SqlParameter[] parameters =new SqlParameter[2];
				parameters[0]= new SqlParameter("@newRoleID", SqlDbType.Int);
				parameters[0].Value=newRoleID;
				
				parameters[1]= new SqlParameter("@RoleID", SqlDbType.Int);
				parameters[1].Value=RoleID;
				
			
			string sql="update [Role] set [RoleID]=@newRoleID where [RoleID] = @RoleID";
			
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
		public bool UpdateBy_RoleID( int RoleID, int newRoleID,System.Data.IDbTransaction trans)
		{
			SqlParameter[] parameters =new SqlParameter[2];
				parameters[0]= new SqlParameter("@newRoleID", SqlDbType.Int);
				parameters[0].Value=newRoleID;
				
				parameters[1]= new SqlParameter("@RoleID", SqlDbType.Int);
				parameters[1].Value=RoleID;
				
			
			string sql="update [Role] set [RoleID]=@newRoleID where [RoleID] = @RoleID";
			
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
		public hammergo.Model.Role GetModelBy_RoleID(int RoleID)
		{
		
			
			SqlParameter[] parameters =new SqlParameter[1];
				parameters[0]= new SqlParameter("@RoleID", SqlDbType.Int);
				parameters[0].Value=RoleID;
			string sql="select [RoleID] ,[RoleName] ,[Description] ,[Power]  from [Role] where [RoleID] = @RoleID";
			return QueryModel(sql, parameters);

		}				
		
		
		
		
		



		
		
		/// <summary>
		/// 为model创建参数数组
		/// </summary>
		private SqlParameter[] createAllParams(hammergo.Model.Role model)
        {
			SqlParameter[] parameters =new SqlParameter[4];
			parameters[0]= new SqlParameter("@RoleID", SqlDbType.Int);
					if (model.RoleID.HasValue)
					{
						parameters[0].Value = model.RoleID;
					}
					else
					{
						parameters[0].Value = DBNull.Value;
					}
			parameters[1]= new SqlParameter("@RoleName", SqlDbType.NVarChar);
					if(model.RoleName != null)
					{
						parameters[1].Value =model.RoleName;
					}else
					{
						parameters[1].Value =DBNull.Value;
					}
			parameters[2]= new SqlParameter("@Description", SqlDbType.NVarChar);
					if(model.Description != null)
					{
						parameters[2].Value =model.Description;
					}else
					{
						parameters[2].Value =DBNull.Value;
					}
			parameters[3]= new SqlParameter("@Power", SqlDbType.TinyInt);
					if (model.Power.HasValue)
					{
						parameters[3].Value = model.Power;
					}
					else
					{
						parameters[3].Value = DBNull.Value;
					}
			return  parameters;
		}
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(hammergo.Model.Role model)
		{
			string sql="insert into Role ([RoleID] ,[RoleName] ,[Description] ,[Power]  ) values (@RoleID,@RoleName,@Description,@Power)";
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
		public bool Add(hammergo.Model.Role model,System.Data.IDbTransaction trans)
		{
			string sql="insert into Role ([RoleID] ,[RoleName] ,[Description] ,[Power]  ) values (@RoleID,@RoleName,@Description,@Power)";
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
		public bool Update(hammergo.Model.Role model)
		{
			string sql="update [Role] set [RoleID]=@RoleID,[RoleName]=@RoleName,[Description]=@Description,[Power]=@Power where [RoleID]=@RoleID";
			
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
		public bool Update(hammergo.Model.Role model,System.Data.IDbTransaction trans)
		{
			string sql="update [Role] set [RoleID]=@RoleID,[RoleName]=@RoleName,[Description]=@Description,[Power]=@Power where [RoleID]=@RoleID";
			
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
		public bool Delete(int RoleID)
		{
			string sql="delete from Role  where [RoleID]=@RoleID";
			SqlParameter[] parameters =new SqlParameter[1];
				parameters[0]= new SqlParameter("@RoleID", SqlDbType.Int);
				parameters[0].Value=RoleID;
				
				if (DbHelperSQL.ExecuteNonQuery(CommandType.Text, sql, parameters)== 0)
				{
					return false;
				}
	
				return true;
		}		
		
		
		/// <summary>
		/// 使用事务删除一条数据
		/// </summary>
		public bool Delete(int RoleID,System.Data.IDbTransaction trans)
		{
			string sql="delete from Role  where [RoleID]=@RoleID";
			SqlParameter[] parameters =new SqlParameter[1];
				parameters[0]= new SqlParameter("@RoleID", SqlDbType.Int);
				parameters[0].Value=RoleID;
				
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
        private hammergo.Model.Role createModel(object[] values)
        {
            hammergo.Model.Role model = null;
            //创建model对象
            if (values != null)
            {
                model = new hammergo.Model.Role();
				object obj=null;
                //赋值
					 obj = values[0];
					if (obj is System.DBNull)
					{
						model.RoleID = null;
					}
					else
					{
						model.RoleID = (int)obj;
					}
					 obj = values[1];
					if (obj is System.DBNull)
					{
						model.RoleName = null;
					}
					else
					{
						model.RoleName = (string)obj;
					}
					 obj = values[2];
					if (obj is System.DBNull)
					{
						model.Description = null;
					}
					else
					{
						model.Description = (string)obj;
					}
					 obj = values[3];
					if (obj is System.DBNull)
					{
						model.Power = null;
					}
					else
					{
						model.Power = (byte)obj;
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
        private hammergo.Model.Role QueryModel(string sql, params SqlParameter[] parameters)
        {
            //Set up a return value
            hammergo.Model.Role model = null;

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
        private TrackedList<hammergo.Model.Role> QueryModelList(string sql, params SqlParameter[] parameters)
        {
            TrackedList<hammergo.Model.Role> list = new TrackedList<hammergo.Model.Role>(100);


            hammergo.Model.Role model = null;
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
		/// 获取RoleID的最大值
		/// </summary>				
		public int? GetMaxRoleID()
		{
			int? val=null;
			string sql="select max(RoleID) from Role";
			object obj =DbHelperSQL.ExecuteScalar(CommandType.Text, sql);

			if (obj is System.DBNull ==false)
			{
				val = (int)obj;
			}
  			return val;
		}
				
				
				
		/// <summary>
		/// 获取Power的最大值
		/// </summary>				
		public byte? GetMaxPower()
		{
			byte? val=null;
			string sql="select max(Power) from Role";
			object obj =DbHelperSQL.ExecuteScalar(CommandType.Text, sql);

			if (obj is System.DBNull ==false)
			{
				val = (byte)obj;
			}
  			return val;
		}
				
				
		
		
				
		/// <summary>
		/// 获取RoleID的最小值
		/// </summary>				
		public int? GetMinRoleID()
		{
			int? val=null;
			string sql="select min(RoleID) from Role";
			object obj =DbHelperSQL.ExecuteScalar(CommandType.Text, sql);

			if (obj is System.DBNull ==false)
			{
				val = (int)obj;
			}
  			return val;
		}
				
				
				
		/// <summary>
		/// 获取Power的最小值
		/// </summary>				
		public byte? GetMinPower()
		{
			byte? val=null;
			string sql="select min(Power) from Role";
			object obj =DbHelperSQL.ExecuteScalar(CommandType.Text, sql);

			if (obj is System.DBNull ==false)
			{
				val = (byte)obj;
			}
  			return val;
		}
				
				

		/// <summary>
		/// 获得对象实体列表
		/// </summary>
        public TrackedList<hammergo.Model.Role> GetList()
		{
			string sql="select [RoleID] ,[RoleName] ,[Description] ,[Power]  from [Role]";
			 return QueryModelList(sql, null);
		}			
		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.Role> GetListByRoleID(int  RoleID)
		{
			string sql="select [RoleID] ,[RoleName] ,[Description] ,[Power]  from [Role] where RoleID =@RoleID";
			SqlParameter parameter=new SqlParameter("@RoleID",SqlDbType.Int);
			parameter.Value=RoleID;
			return QueryModelList(sql, parameter);
		}	
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByRoleID(int  RoleID)
		{
			string sql="select count(1) from Role where RoleID =@RoleID";
			SqlParameter parameter=new SqlParameter("@RoleID",SqlDbType.Int);
			parameter.Value=RoleID;
			object obj= DbHelperSQL.ExecuteScalar(CommandType.Text,sql, parameter);
			
			return (int)obj;
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.Role> GetListByRoleName(string  RoleName)
		{
			string sql="select [RoleID] ,[RoleName] ,[Description] ,[Power]  from [Role] where RoleName =@RoleName";
			SqlParameter parameter=new SqlParameter("@RoleName",SqlDbType.NVarChar);
			parameter.Value=RoleName;
			return QueryModelList(sql, parameter);
		}	
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByRoleName(string  RoleName)
		{
			string sql="select count(1) from Role where RoleName =@RoleName";
			SqlParameter parameter=new SqlParameter("@RoleName",SqlDbType.NVarChar);
			parameter.Value=RoleName;
			object obj= DbHelperSQL.ExecuteScalar(CommandType.Text,sql, parameter);
			
			return (int)obj;
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.Role> GetListByDescription(string  Description)
		{
			string sql="select [RoleID] ,[RoleName] ,[Description] ,[Power]  from [Role] where Description =@Description";
			SqlParameter parameter=new SqlParameter("@Description",SqlDbType.NVarChar);
			parameter.Value=Description;
			return QueryModelList(sql, parameter);
		}	
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByDescription(string  Description)
		{
			string sql="select count(1) from Role where Description =@Description";
			SqlParameter parameter=new SqlParameter("@Description",SqlDbType.NVarChar);
			parameter.Value=Description;
			object obj= DbHelperSQL.ExecuteScalar(CommandType.Text,sql, parameter);
			
			return (int)obj;
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.Role> GetListByPower(byte  Power)
		{
			string sql="select [RoleID] ,[RoleName] ,[Description] ,[Power]  from [Role] where Power =@Power";
			SqlParameter parameter=new SqlParameter("@Power",SqlDbType.TinyInt);
			parameter.Value=Power;
			return QueryModelList(sql, parameter);
		}	
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByPower(byte  Power)
		{
			string sql="select count(1) from Role where Power =@Power";
			SqlParameter parameter=new SqlParameter("@Power",SqlDbType.TinyInt);
			parameter.Value=Power;
			object obj= DbHelperSQL.ExecuteScalar(CommandType.Text,sql, parameter);
			
			return (int)obj;
		}
		
		
		
    }
}



