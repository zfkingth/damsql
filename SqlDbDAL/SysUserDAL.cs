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
    
    public partial class  SysUserDAL:ISysUserDAL
    {
		private const int columnsCnt = 6;
		protected string SQL_SELECT_All="select [UserName] ,[PasswordHash] ,[Salt] ,[Question] ,[Answer] ,[roleID]  from [SysUser]";
		protected string SQL_Field="SysUser.UserName ,SysUser.PasswordHash ,SysUser.Salt ,SysUser.Question ,SysUser.Answer ,SysUser.roleID ";
		
		
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool ExistsBy_UserName(string UserName)
		{

			
			SqlParameter[] parameters =new SqlParameter[1];
				parameters[0]= new SqlParameter("@UserName", SqlDbType.NVarChar);
				parameters[0].Value=UserName;
				
			
			string sql="select count(1) from SysUser where [UserName] = @UserName";
			return DbHelperSQL.Exists(sql, parameters);
		}
		
		/// <summary>
		/// 更新记录的记录
		/// </summary>
		public bool UpdateBy_UserName( string UserName, string newUserName)
		{
			SqlParameter[] parameters =new SqlParameter[2];
				parameters[0]= new SqlParameter("@newUserName", SqlDbType.NVarChar);
				parameters[0].Value=newUserName;
				
				parameters[1]= new SqlParameter("@UserName", SqlDbType.NVarChar);
				parameters[1].Value=UserName;
				
			
			string sql="update [SysUser] set [UserName]=@newUserName where [UserName] = @UserName";
			
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
		public bool UpdateBy_UserName( string UserName, string newUserName,System.Data.IDbTransaction trans)
		{
			SqlParameter[] parameters =new SqlParameter[2];
				parameters[0]= new SqlParameter("@newUserName", SqlDbType.NVarChar);
				parameters[0].Value=newUserName;
				
				parameters[1]= new SqlParameter("@UserName", SqlDbType.NVarChar);
				parameters[1].Value=UserName;
				
			
			string sql="update [SysUser] set [UserName]=@newUserName where [UserName] = @UserName";
			
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
		public hammergo.Model.SysUser GetModelBy_UserName(string UserName)
		{
		
			
			SqlParameter[] parameters =new SqlParameter[1];
				parameters[0]= new SqlParameter("@UserName", SqlDbType.NVarChar);
				parameters[0].Value=UserName;
			string sql="select [UserName] ,[PasswordHash] ,[Salt] ,[Question] ,[Answer] ,[roleID]  from [SysUser] where [UserName] = @UserName";
			return QueryModel(sql, parameters);

		}				
		
		
		
		
		



		
		
		/// <summary>
		/// 为model创建参数数组
		/// </summary>
		private SqlParameter[] createAllParams(hammergo.Model.SysUser model)
        {
			SqlParameter[] parameters =new SqlParameter[6];
			parameters[0]= new SqlParameter("@UserName", SqlDbType.NVarChar);
					if(model.UserName != null)
					{
						parameters[0].Value =model.UserName;
					}else
					{
						parameters[0].Value =DBNull.Value;
					}
			parameters[1]= new SqlParameter("@PasswordHash", SqlDbType.NVarChar);
					if(model.PasswordHash != null)
					{
						parameters[1].Value =model.PasswordHash;
					}else
					{
						parameters[1].Value =DBNull.Value;
					}
			parameters[2]= new SqlParameter("@Salt", SqlDbType.NVarChar);
					if(model.Salt != null)
					{
						parameters[2].Value =model.Salt;
					}else
					{
						parameters[2].Value =DBNull.Value;
					}
			parameters[3]= new SqlParameter("@Question", SqlDbType.NVarChar);
					if(model.Question != null)
					{
						parameters[3].Value =model.Question;
					}else
					{
						parameters[3].Value =DBNull.Value;
					}
			parameters[4]= new SqlParameter("@Answer", SqlDbType.NVarChar);
					if(model.Answer != null)
					{
						parameters[4].Value =model.Answer;
					}else
					{
						parameters[4].Value =DBNull.Value;
					}
			parameters[5]= new SqlParameter("@roleID", SqlDbType.Int);
					if (model.RoleID.HasValue)
					{
						parameters[5].Value = model.RoleID;
					}
					else
					{
						parameters[5].Value = DBNull.Value;
					}
			return  parameters;
		}
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(hammergo.Model.SysUser model)
		{
			string sql="insert into SysUser ([UserName] ,[PasswordHash] ,[Salt] ,[Question] ,[Answer] ,[roleID]  ) values (@UserName,@PasswordHash,@Salt,@Question,@Answer,@roleID)";
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
		public bool Add(hammergo.Model.SysUser model,System.Data.IDbTransaction trans)
		{
			string sql="insert into SysUser ([UserName] ,[PasswordHash] ,[Salt] ,[Question] ,[Answer] ,[roleID]  ) values (@UserName,@PasswordHash,@Salt,@Question,@Answer,@roleID)";
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
		public bool Update(hammergo.Model.SysUser model)
		{
			string sql="update [SysUser] set [UserName]=@UserName,[PasswordHash]=@PasswordHash,[Salt]=@Salt,[Question]=@Question,[Answer]=@Answer,[roleID]=@roleID where [UserName]=@UserName";
			
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
		public bool Update(hammergo.Model.SysUser model,System.Data.IDbTransaction trans)
		{
			string sql="update [SysUser] set [UserName]=@UserName,[PasswordHash]=@PasswordHash,[Salt]=@Salt,[Question]=@Question,[Answer]=@Answer,[roleID]=@roleID where [UserName]=@UserName";
			
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
		public bool Delete(string UserName)
		{
			string sql="delete from SysUser  where [UserName]=@UserName";
			SqlParameter[] parameters =new SqlParameter[1];
				parameters[0]= new SqlParameter("@UserName", SqlDbType.NVarChar);
				parameters[0].Value=UserName;
				
				if (DbHelperSQL.ExecuteNonQuery(CommandType.Text, sql, parameters)== 0)
				{
					return false;
				}
	
				return true;
		}		
		
		
		/// <summary>
		/// 使用事务删除一条数据
		/// </summary>
		public bool Delete(string UserName,System.Data.IDbTransaction trans)
		{
			string sql="delete from SysUser  where [UserName]=@UserName";
			SqlParameter[] parameters =new SqlParameter[1];
				parameters[0]= new SqlParameter("@UserName", SqlDbType.NVarChar);
				parameters[0].Value=UserName;
				
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
        private hammergo.Model.SysUser createModel(object[] values)
        {
            hammergo.Model.SysUser model = null;
            //创建model对象
            if (values != null)
            {
                model = new hammergo.Model.SysUser();
				object obj=null;
                //赋值
					 obj = values[0];
					if (obj is System.DBNull)
					{
						model.UserName = null;
					}
					else
					{
						model.UserName = (string)obj;
					}
					 obj = values[1];
					if (obj is System.DBNull)
					{
						model.PasswordHash = null;
					}
					else
					{
						model.PasswordHash = (string)obj;
					}
					 obj = values[2];
					if (obj is System.DBNull)
					{
						model.Salt = null;
					}
					else
					{
						model.Salt = (string)obj;
					}
					 obj = values[3];
					if (obj is System.DBNull)
					{
						model.Question = null;
					}
					else
					{
						model.Question = (string)obj;
					}
					 obj = values[4];
					if (obj is System.DBNull)
					{
						model.Answer = null;
					}
					else
					{
						model.Answer = (string)obj;
					}
					 obj = values[5];
					if (obj is System.DBNull)
					{
						model.RoleID = null;
					}
					else
					{
						model.RoleID = (int)obj;
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
        private hammergo.Model.SysUser QueryModel(string sql, params SqlParameter[] parameters)
        {
            //Set up a return value
            hammergo.Model.SysUser model = null;

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
        private TrackedList<hammergo.Model.SysUser> QueryModelList(string sql, params SqlParameter[] parameters)
        {
            TrackedList<hammergo.Model.SysUser> list = new TrackedList<hammergo.Model.SysUser>(100);


            hammergo.Model.SysUser model = null;
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
		/// 获取roleID的最大值
		/// </summary>				
		public int? GetMaxroleID()
		{
			int? val=null;
			string sql="select max(roleID) from SysUser";
			object obj =DbHelperSQL.ExecuteScalar(CommandType.Text, sql);

			if (obj is System.DBNull ==false)
			{
				val = (int)obj;
			}
  			return val;
		}
				
				
		
		
				
		/// <summary>
		/// 获取roleID的最小值
		/// </summary>				
		public int? GetMinroleID()
		{
			int? val=null;
			string sql="select min(roleID) from SysUser";
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
        public TrackedList<hammergo.Model.SysUser> GetList()
		{
			string sql="select [UserName] ,[PasswordHash] ,[Salt] ,[Question] ,[Answer] ,[roleID]  from [SysUser]";
			 return QueryModelList(sql, null);
		}			
		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.SysUser> GetListByUserName(string  UserName)
		{
			string sql="select [UserName] ,[PasswordHash] ,[Salt] ,[Question] ,[Answer] ,[roleID]  from [SysUser] where UserName =@UserName";
			SqlParameter parameter=new SqlParameter("@UserName",SqlDbType.NVarChar);
			parameter.Value=UserName;
			return QueryModelList(sql, parameter);
		}	
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByUserName(string  UserName)
		{
			string sql="select count(1) from SysUser where UserName =@UserName";
			SqlParameter parameter=new SqlParameter("@UserName",SqlDbType.NVarChar);
			parameter.Value=UserName;
			object obj= DbHelperSQL.ExecuteScalar(CommandType.Text,sql, parameter);
			
			return (int)obj;
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.SysUser> GetListByPasswordHash(string  PasswordHash)
		{
			string sql="select [UserName] ,[PasswordHash] ,[Salt] ,[Question] ,[Answer] ,[roleID]  from [SysUser] where PasswordHash =@PasswordHash";
			SqlParameter parameter=new SqlParameter("@PasswordHash",SqlDbType.NVarChar);
			parameter.Value=PasswordHash;
			return QueryModelList(sql, parameter);
		}	
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByPasswordHash(string  PasswordHash)
		{
			string sql="select count(1) from SysUser where PasswordHash =@PasswordHash";
			SqlParameter parameter=new SqlParameter("@PasswordHash",SqlDbType.NVarChar);
			parameter.Value=PasswordHash;
			object obj= DbHelperSQL.ExecuteScalar(CommandType.Text,sql, parameter);
			
			return (int)obj;
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.SysUser> GetListBySalt(string  Salt)
		{
			string sql="select [UserName] ,[PasswordHash] ,[Salt] ,[Question] ,[Answer] ,[roleID]  from [SysUser] where Salt =@Salt";
			SqlParameter parameter=new SqlParameter("@Salt",SqlDbType.NVarChar);
			parameter.Value=Salt;
			return QueryModelList(sql, parameter);
		}	
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountBySalt(string  Salt)
		{
			string sql="select count(1) from SysUser where Salt =@Salt";
			SqlParameter parameter=new SqlParameter("@Salt",SqlDbType.NVarChar);
			parameter.Value=Salt;
			object obj= DbHelperSQL.ExecuteScalar(CommandType.Text,sql, parameter);
			
			return (int)obj;
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.SysUser> GetListByQuestion(string  Question)
		{
			string sql="select [UserName] ,[PasswordHash] ,[Salt] ,[Question] ,[Answer] ,[roleID]  from [SysUser] where Question =@Question";
			SqlParameter parameter=new SqlParameter("@Question",SqlDbType.NVarChar);
			parameter.Value=Question;
			return QueryModelList(sql, parameter);
		}	
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByQuestion(string  Question)
		{
			string sql="select count(1) from SysUser where Question =@Question";
			SqlParameter parameter=new SqlParameter("@Question",SqlDbType.NVarChar);
			parameter.Value=Question;
			object obj= DbHelperSQL.ExecuteScalar(CommandType.Text,sql, parameter);
			
			return (int)obj;
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.SysUser> GetListByAnswer(string  Answer)
		{
			string sql="select [UserName] ,[PasswordHash] ,[Salt] ,[Question] ,[Answer] ,[roleID]  from [SysUser] where Answer =@Answer";
			SqlParameter parameter=new SqlParameter("@Answer",SqlDbType.NVarChar);
			parameter.Value=Answer;
			return QueryModelList(sql, parameter);
		}	
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByAnswer(string  Answer)
		{
			string sql="select count(1) from SysUser where Answer =@Answer";
			SqlParameter parameter=new SqlParameter("@Answer",SqlDbType.NVarChar);
			parameter.Value=Answer;
			object obj= DbHelperSQL.ExecuteScalar(CommandType.Text,sql, parameter);
			
			return (int)obj;
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.SysUser> GetListByroleID(int  RoleID)
		{
			string sql="select [UserName] ,[PasswordHash] ,[Salt] ,[Question] ,[Answer] ,[roleID]  from [SysUser] where roleID =@roleID";
			SqlParameter parameter=new SqlParameter("@roleID",SqlDbType.Int);
			parameter.Value=RoleID;
			return QueryModelList(sql, parameter);
		}	
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByroleID(int  RoleID)
		{
			string sql="select count(1) from SysUser where roleID =@roleID";
			SqlParameter parameter=new SqlParameter("@roleID",SqlDbType.Int);
			parameter.Value=RoleID;
			object obj= DbHelperSQL.ExecuteScalar(CommandType.Text,sql, parameter);
			
			return (int)obj;
		}
		
		
		
    }
}



