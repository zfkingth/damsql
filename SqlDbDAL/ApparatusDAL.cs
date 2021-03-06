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
    
    public partial class  ApparatusDAL:IApparatusDAL
    {
		private const int columnsCnt = 9;
		protected string SQL_SELECT_All="select [AppName] ,[CalculateName] ,[ProjectPartID] ,[AppTypeID] ,[X] ,[Y] ,[Z] ,[BuriedTime] ,[OtherInfo]  from [Apparatus]";
		protected string SQL_Field="Apparatus.AppName ,Apparatus.CalculateName ,Apparatus.ProjectPartID ,Apparatus.AppTypeID ,Apparatus.X ,Apparatus.Y ,Apparatus.Z ,Apparatus.BuriedTime ,Apparatus.OtherInfo ";
		
		
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool ExistsBy_CalculateName(string CalculateName)
		{

			
			SqlParameter[] parameters =new SqlParameter[1];
				parameters[0]= new SqlParameter("@CalculateName", SqlDbType.NVarChar);
				parameters[0].Value=CalculateName;
				
			
			string sql="select count(1) from Apparatus where [CalculateName] = @CalculateName";
			return DbHelperSQL.Exists(sql, parameters);
		}
		
		/// <summary>
		/// 更新记录的记录
		/// </summary>
		public bool UpdateBy_CalculateName( string CalculateName, string newCalculateName)
		{
			SqlParameter[] parameters =new SqlParameter[2];
				parameters[0]= new SqlParameter("@newCalculateName", SqlDbType.NVarChar);
				parameters[0].Value=newCalculateName;
				
				parameters[1]= new SqlParameter("@CalculateName", SqlDbType.NVarChar);
				parameters[1].Value=CalculateName;
				
			
			string sql="update [Apparatus] set [CalculateName]=@newCalculateName where [CalculateName] = @CalculateName";
			
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
		public bool UpdateBy_CalculateName( string CalculateName, string newCalculateName,System.Data.IDbTransaction trans)
		{
			SqlParameter[] parameters =new SqlParameter[2];
				parameters[0]= new SqlParameter("@newCalculateName", SqlDbType.NVarChar);
				parameters[0].Value=newCalculateName;
				
				parameters[1]= new SqlParameter("@CalculateName", SqlDbType.NVarChar);
				parameters[1].Value=CalculateName;
				
			
			string sql="update [Apparatus] set [CalculateName]=@newCalculateName where [CalculateName] = @CalculateName";
			
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
		public hammergo.Model.Apparatus GetModelBy_CalculateName(string CalculateName)
		{
		
			
			SqlParameter[] parameters =new SqlParameter[1];
				parameters[0]= new SqlParameter("@CalculateName", SqlDbType.NVarChar);
				parameters[0].Value=CalculateName;
			string sql="select [AppName] ,[CalculateName] ,[ProjectPartID] ,[AppTypeID] ,[X] ,[Y] ,[Z] ,[BuriedTime] ,[OtherInfo]  from [Apparatus] where [CalculateName] = @CalculateName";
			return QueryModel(sql, parameters);

		}				
		
		
		
		
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool ExistsBy_AppName(string AppName)
		{

			
			SqlParameter[] parameters =new SqlParameter[1];
				parameters[0]= new SqlParameter("@AppName", SqlDbType.NVarChar);
				parameters[0].Value=AppName;
				
			
			string sql="select count(1) from Apparatus where [AppName] = @AppName";
			return DbHelperSQL.Exists(sql, parameters);
		}
		
		/// <summary>
		/// 更新记录的记录
		/// </summary>
		public bool UpdateBy_AppName( string AppName, string newAppName)
		{
			SqlParameter[] parameters =new SqlParameter[2];
				parameters[0]= new SqlParameter("@newAppName", SqlDbType.NVarChar);
				parameters[0].Value=newAppName;
				
				parameters[1]= new SqlParameter("@AppName", SqlDbType.NVarChar);
				parameters[1].Value=AppName;
				
			
			string sql="update [Apparatus] set [AppName]=@newAppName where [AppName] = @AppName";
			
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
		public bool UpdateBy_AppName( string AppName, string newAppName,System.Data.IDbTransaction trans)
		{
			SqlParameter[] parameters =new SqlParameter[2];
				parameters[0]= new SqlParameter("@newAppName", SqlDbType.NVarChar);
				parameters[0].Value=newAppName;
				
				parameters[1]= new SqlParameter("@AppName", SqlDbType.NVarChar);
				parameters[1].Value=AppName;
				
			
			string sql="update [Apparatus] set [AppName]=@newAppName where [AppName] = @AppName";
			
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
		public hammergo.Model.Apparatus GetModelBy_AppName(string AppName)
		{
		
			
			SqlParameter[] parameters =new SqlParameter[1];
				parameters[0]= new SqlParameter("@AppName", SqlDbType.NVarChar);
				parameters[0].Value=AppName;
			string sql="select [AppName] ,[CalculateName] ,[ProjectPartID] ,[AppTypeID] ,[X] ,[Y] ,[Z] ,[BuriedTime] ,[OtherInfo]  from [Apparatus] where [AppName] = @AppName";
			return QueryModel(sql, parameters);

		}				
		
		
		
		
		



		
		
		/// <summary>
		/// 为model创建参数数组
		/// </summary>
		private SqlParameter[] createAllParams(hammergo.Model.Apparatus model)
        {
			SqlParameter[] parameters =new SqlParameter[9];
			parameters[0]= new SqlParameter("@AppName", SqlDbType.NVarChar);
					if(model.AppName != null)
					{
						parameters[0].Value =model.AppName;
					}else
					{
						parameters[0].Value =DBNull.Value;
					}
			parameters[1]= new SqlParameter("@CalculateName", SqlDbType.NVarChar);
					if(model.CalculateName != null)
					{
						parameters[1].Value =model.CalculateName;
					}else
					{
						parameters[1].Value =DBNull.Value;
					}
			parameters[2]= new SqlParameter("@ProjectPartID", SqlDbType.UniqueIdentifier);
					if (model.ProjectPartID.HasValue)
					{
						parameters[2].Value = model.ProjectPartID;
					}
					else
					{
						parameters[2].Value = DBNull.Value;
					}
			parameters[3]= new SqlParameter("@AppTypeID", SqlDbType.UniqueIdentifier);
					if (model.AppTypeID.HasValue)
					{
						parameters[3].Value = model.AppTypeID;
					}
					else
					{
						parameters[3].Value = DBNull.Value;
					}
			parameters[4]= new SqlParameter("@X", SqlDbType.NVarChar);
					if(model.X != null)
					{
						parameters[4].Value =model.X;
					}else
					{
						parameters[4].Value =DBNull.Value;
					}
			parameters[5]= new SqlParameter("@Y", SqlDbType.NVarChar);
					if(model.Y != null)
					{
						parameters[5].Value =model.Y;
					}else
					{
						parameters[5].Value =DBNull.Value;
					}
			parameters[6]= new SqlParameter("@Z", SqlDbType.NVarChar);
					if(model.Z != null)
					{
						parameters[6].Value =model.Z;
					}else
					{
						parameters[6].Value =DBNull.Value;
					}
			parameters[7]= new SqlParameter("@BuriedTime", SqlDbType.DateTime);
					if (model.BuriedTime.HasValue)
					{
						parameters[7].Value = model.BuriedTime;
					}
					else
					{
						parameters[7].Value = DBNull.Value;
					}
			parameters[8]= new SqlParameter("@OtherInfo", SqlDbType.NVarChar);
					if(model.OtherInfo != null)
					{
						parameters[8].Value =model.OtherInfo;
					}else
					{
						parameters[8].Value =DBNull.Value;
					}
			return  parameters;
		}
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(hammergo.Model.Apparatus model)
		{
			string sql="insert into Apparatus ([AppName] ,[CalculateName] ,[ProjectPartID] ,[AppTypeID] ,[X] ,[Y] ,[Z] ,[BuriedTime] ,[OtherInfo]  ) values (@AppName,@CalculateName,@ProjectPartID,@AppTypeID,@X,@Y,@Z,@BuriedTime,@OtherInfo)";
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
		public bool Add(hammergo.Model.Apparatus model,System.Data.IDbTransaction trans)
		{
			string sql="insert into Apparatus ([AppName] ,[CalculateName] ,[ProjectPartID] ,[AppTypeID] ,[X] ,[Y] ,[Z] ,[BuriedTime] ,[OtherInfo]  ) values (@AppName,@CalculateName,@ProjectPartID,@AppTypeID,@X,@Y,@Z,@BuriedTime,@OtherInfo)";
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
		public bool Update(hammergo.Model.Apparatus model)
		{
			string sql="update [Apparatus] set [AppName]=@AppName,[CalculateName]=@CalculateName,[ProjectPartID]=@ProjectPartID,[AppTypeID]=@AppTypeID,[X]=@X,[Y]=@Y,[Z]=@Z,[BuriedTime]=@BuriedTime,[OtherInfo]=@OtherInfo where [AppName]=@AppName";
			
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
		public bool Update(hammergo.Model.Apparatus model,System.Data.IDbTransaction trans)
		{
			string sql="update [Apparatus] set [AppName]=@AppName,[CalculateName]=@CalculateName,[ProjectPartID]=@ProjectPartID,[AppTypeID]=@AppTypeID,[X]=@X,[Y]=@Y,[Z]=@Z,[BuriedTime]=@BuriedTime,[OtherInfo]=@OtherInfo where [AppName]=@AppName";
			
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
		public bool Delete(string AppName)
		{
			string sql="delete from Apparatus  where [AppName]=@AppName";
			SqlParameter[] parameters =new SqlParameter[1];
				parameters[0]= new SqlParameter("@AppName", SqlDbType.NVarChar);
				parameters[0].Value=AppName;
				
				if (DbHelperSQL.ExecuteNonQuery(CommandType.Text, sql, parameters)== 0)
				{
					return false;
				}
	
				return true;
		}		
		
		
		/// <summary>
		/// 使用事务删除一条数据
		/// </summary>
		public bool Delete(string AppName,System.Data.IDbTransaction trans)
		{
			string sql="delete from Apparatus  where [AppName]=@AppName";
			SqlParameter[] parameters =new SqlParameter[1];
				parameters[0]= new SqlParameter("@AppName", SqlDbType.NVarChar);
				parameters[0].Value=AppName;
				
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
        private hammergo.Model.Apparatus createModel(object[] values)
        {
            hammergo.Model.Apparatus model = null;
            //创建model对象
            if (values != null)
            {
                model = new hammergo.Model.Apparatus();
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
						model.CalculateName = null;
					}
					else
					{
						model.CalculateName = (string)obj;
					}
					 obj = values[2];
					if (obj is System.DBNull)
					{
						model.ProjectPartID = null;
					}
					else
					{
						model.ProjectPartID = (System.Guid)obj;
					}
					 obj = values[3];
					if (obj is System.DBNull)
					{
						model.AppTypeID = null;
					}
					else
					{
						model.AppTypeID = (System.Guid)obj;
					}
					 obj = values[4];
					if (obj is System.DBNull)
					{
						model.X = null;
					}
					else
					{
						model.X = (string)obj;
					}
					 obj = values[5];
					if (obj is System.DBNull)
					{
						model.Y = null;
					}
					else
					{
						model.Y = (string)obj;
					}
					 obj = values[6];
					if (obj is System.DBNull)
					{
						model.Z = null;
					}
					else
					{
						model.Z = (string)obj;
					}
					 obj = values[7];
					if (obj is System.DBNull)
					{
						model.BuriedTime = null;
					}
					else
					{
						model.BuriedTime = (System.DateTime)obj;
					}
					 obj = values[8];
					if (obj is System.DBNull)
					{
						model.OtherInfo = null;
					}
					else
					{
						model.OtherInfo = (string)obj;
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
        private hammergo.Model.Apparatus QueryModel(string sql, params SqlParameter[] parameters)
        {
            //Set up a return value
            hammergo.Model.Apparatus model = null;

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
        private TrackedList<hammergo.Model.Apparatus> QueryModelList(string sql, params SqlParameter[] parameters)
        {
            TrackedList<hammergo.Model.Apparatus> list = new TrackedList<hammergo.Model.Apparatus>(100);


            hammergo.Model.Apparatus model = null;
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
        public TrackedList<hammergo.Model.Apparatus> GetList()
		{
			string sql="select [AppName] ,[CalculateName] ,[ProjectPartID] ,[AppTypeID] ,[X] ,[Y] ,[Z] ,[BuriedTime] ,[OtherInfo]  from [Apparatus]";
			 return QueryModelList(sql, null);
		}			
		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.Apparatus> GetListByAppName(string  AppName)
		{
			string sql="select [AppName] ,[CalculateName] ,[ProjectPartID] ,[AppTypeID] ,[X] ,[Y] ,[Z] ,[BuriedTime] ,[OtherInfo]  from [Apparatus] where AppName =@AppName";
			SqlParameter parameter=new SqlParameter("@AppName",SqlDbType.NVarChar);
			parameter.Value=AppName;
			return QueryModelList(sql, parameter);
		}	
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByAppName(string  AppName)
		{
			string sql="select count(1) from Apparatus where AppName =@AppName";
			SqlParameter parameter=new SqlParameter("@AppName",SqlDbType.NVarChar);
			parameter.Value=AppName;
			object obj= DbHelperSQL.ExecuteScalar(CommandType.Text,sql, parameter);
			
			return (int)obj;
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.Apparatus> GetListByCalculateName(string  CalculateName)
		{
			string sql="select [AppName] ,[CalculateName] ,[ProjectPartID] ,[AppTypeID] ,[X] ,[Y] ,[Z] ,[BuriedTime] ,[OtherInfo]  from [Apparatus] where CalculateName =@CalculateName";
			SqlParameter parameter=new SqlParameter("@CalculateName",SqlDbType.NVarChar);
			parameter.Value=CalculateName;
			return QueryModelList(sql, parameter);
		}	
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByCalculateName(string  CalculateName)
		{
			string sql="select count(1) from Apparatus where CalculateName =@CalculateName";
			SqlParameter parameter=new SqlParameter("@CalculateName",SqlDbType.NVarChar);
			parameter.Value=CalculateName;
			object obj= DbHelperSQL.ExecuteScalar(CommandType.Text,sql, parameter);
			
			return (int)obj;
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.Apparatus> GetListByProjectPartID(System.Guid  ProjectPartID)
		{
			string sql="select [AppName] ,[CalculateName] ,[ProjectPartID] ,[AppTypeID] ,[X] ,[Y] ,[Z] ,[BuriedTime] ,[OtherInfo]  from [Apparatus] where ProjectPartID =@ProjectPartID";
			SqlParameter parameter=new SqlParameter("@ProjectPartID",SqlDbType.UniqueIdentifier);
			parameter.Value=ProjectPartID;
			return QueryModelList(sql, parameter);
		}	
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByProjectPartID(System.Guid  ProjectPartID)
		{
			string sql="select count(1) from Apparatus where ProjectPartID =@ProjectPartID";
			SqlParameter parameter=new SqlParameter("@ProjectPartID",SqlDbType.UniqueIdentifier);
			parameter.Value=ProjectPartID;
			object obj= DbHelperSQL.ExecuteScalar(CommandType.Text,sql, parameter);
			
			return (int)obj;
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.Apparatus> GetListByAppTypeID(System.Guid  AppTypeID)
		{
			string sql="select [AppName] ,[CalculateName] ,[ProjectPartID] ,[AppTypeID] ,[X] ,[Y] ,[Z] ,[BuriedTime] ,[OtherInfo]  from [Apparatus] where AppTypeID =@AppTypeID";
			SqlParameter parameter=new SqlParameter("@AppTypeID",SqlDbType.UniqueIdentifier);
			parameter.Value=AppTypeID;
			return QueryModelList(sql, parameter);
		}	
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByAppTypeID(System.Guid  AppTypeID)
		{
			string sql="select count(1) from Apparatus where AppTypeID =@AppTypeID";
			SqlParameter parameter=new SqlParameter("@AppTypeID",SqlDbType.UniqueIdentifier);
			parameter.Value=AppTypeID;
			object obj= DbHelperSQL.ExecuteScalar(CommandType.Text,sql, parameter);
			
			return (int)obj;
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.Apparatus> GetListByX(string  X)
		{
			string sql="select [AppName] ,[CalculateName] ,[ProjectPartID] ,[AppTypeID] ,[X] ,[Y] ,[Z] ,[BuriedTime] ,[OtherInfo]  from [Apparatus] where X =@X";
			SqlParameter parameter=new SqlParameter("@X",SqlDbType.NVarChar);
			parameter.Value=X;
			return QueryModelList(sql, parameter);
		}	
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByX(string  X)
		{
			string sql="select count(1) from Apparatus where X =@X";
			SqlParameter parameter=new SqlParameter("@X",SqlDbType.NVarChar);
			parameter.Value=X;
			object obj= DbHelperSQL.ExecuteScalar(CommandType.Text,sql, parameter);
			
			return (int)obj;
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.Apparatus> GetListByY(string  Y)
		{
			string sql="select [AppName] ,[CalculateName] ,[ProjectPartID] ,[AppTypeID] ,[X] ,[Y] ,[Z] ,[BuriedTime] ,[OtherInfo]  from [Apparatus] where Y =@Y";
			SqlParameter parameter=new SqlParameter("@Y",SqlDbType.NVarChar);
			parameter.Value=Y;
			return QueryModelList(sql, parameter);
		}	
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByY(string  Y)
		{
			string sql="select count(1) from Apparatus where Y =@Y";
			SqlParameter parameter=new SqlParameter("@Y",SqlDbType.NVarChar);
			parameter.Value=Y;
			object obj= DbHelperSQL.ExecuteScalar(CommandType.Text,sql, parameter);
			
			return (int)obj;
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.Apparatus> GetListByZ(string  Z)
		{
			string sql="select [AppName] ,[CalculateName] ,[ProjectPartID] ,[AppTypeID] ,[X] ,[Y] ,[Z] ,[BuriedTime] ,[OtherInfo]  from [Apparatus] where Z =@Z";
			SqlParameter parameter=new SqlParameter("@Z",SqlDbType.NVarChar);
			parameter.Value=Z;
			return QueryModelList(sql, parameter);
		}	
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByZ(string  Z)
		{
			string sql="select count(1) from Apparatus where Z =@Z";
			SqlParameter parameter=new SqlParameter("@Z",SqlDbType.NVarChar);
			parameter.Value=Z;
			object obj= DbHelperSQL.ExecuteScalar(CommandType.Text,sql, parameter);
			
			return (int)obj;
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.Apparatus> GetListByBuriedTime(System.DateTime  BuriedTime)
		{
			string sql="select [AppName] ,[CalculateName] ,[ProjectPartID] ,[AppTypeID] ,[X] ,[Y] ,[Z] ,[BuriedTime] ,[OtherInfo]  from [Apparatus] where BuriedTime =@BuriedTime";
			SqlParameter parameter=new SqlParameter("@BuriedTime",SqlDbType.DateTime);
			parameter.Value=BuriedTime;
			return QueryModelList(sql, parameter);
		}	
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByBuriedTime(System.DateTime  BuriedTime)
		{
			string sql="select count(1) from Apparatus where BuriedTime =@BuriedTime";
			SqlParameter parameter=new SqlParameter("@BuriedTime",SqlDbType.DateTime);
			parameter.Value=BuriedTime;
			object obj= DbHelperSQL.ExecuteScalar(CommandType.Text,sql, parameter);
			
			return (int)obj;
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.Apparatus> GetListByOtherInfo(string  OtherInfo)
		{
			string sql="select [AppName] ,[CalculateName] ,[ProjectPartID] ,[AppTypeID] ,[X] ,[Y] ,[Z] ,[BuriedTime] ,[OtherInfo]  from [Apparatus] where OtherInfo =@OtherInfo";
			SqlParameter parameter=new SqlParameter("@OtherInfo",SqlDbType.NVarChar);
			parameter.Value=OtherInfo;
			return QueryModelList(sql, parameter);
		}	
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByOtherInfo(string  OtherInfo)
		{
			string sql="select count(1) from Apparatus where OtherInfo =@OtherInfo";
			SqlParameter parameter=new SqlParameter("@OtherInfo",SqlDbType.NVarChar);
			parameter.Value=OtherInfo;
			object obj= DbHelperSQL.ExecuteScalar(CommandType.Text,sql, parameter);
			
			return (int)obj;
		}
		
		
		
    }
}



