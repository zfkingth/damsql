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
    
    public partial class  ProjectPartDAL:IProjectPartDAL
    {
		private const int columnsCnt = 3;
		protected string SQL_SELECT_All="select [ProjectPartID] ,[PartName] ,[ParentPart]  from [ProjectPart]";
		protected string SQL_Field="ProjectPart.ProjectPartID ,ProjectPart.PartName ,ProjectPart.ParentPart ";
		
		
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool ExistsBy_ProjectPartID(System.Guid ProjectPartID)
		{

			
			SqlParameter[] parameters =new SqlParameter[1];
				parameters[0]= new SqlParameter("@ProjectPartID", SqlDbType.UniqueIdentifier);
				parameters[0].Value=ProjectPartID;
				
			
			string sql="select count(1) from ProjectPart where [ProjectPartID] = @ProjectPartID";
			return DbHelperSQL.Exists(sql, parameters);
		}
		
		/// <summary>
		/// 更新记录的记录
		/// </summary>
		public bool UpdateBy_ProjectPartID( System.Guid ProjectPartID, System.Guid newProjectPartID)
		{
			SqlParameter[] parameters =new SqlParameter[2];
				parameters[0]= new SqlParameter("@newProjectPartID", SqlDbType.UniqueIdentifier);
				parameters[0].Value=newProjectPartID;
				
				parameters[1]= new SqlParameter("@ProjectPartID", SqlDbType.UniqueIdentifier);
				parameters[1].Value=ProjectPartID;
				
			
			string sql="update [ProjectPart] set [ProjectPartID]=@newProjectPartID where [ProjectPartID] = @ProjectPartID";
			
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
		public bool UpdateBy_ProjectPartID( System.Guid ProjectPartID, System.Guid newProjectPartID,System.Data.IDbTransaction trans)
		{
			SqlParameter[] parameters =new SqlParameter[2];
				parameters[0]= new SqlParameter("@newProjectPartID", SqlDbType.UniqueIdentifier);
				parameters[0].Value=newProjectPartID;
				
				parameters[1]= new SqlParameter("@ProjectPartID", SqlDbType.UniqueIdentifier);
				parameters[1].Value=ProjectPartID;
				
			
			string sql="update [ProjectPart] set [ProjectPartID]=@newProjectPartID where [ProjectPartID] = @ProjectPartID";
			
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
		public hammergo.Model.ProjectPart GetModelBy_ProjectPartID(System.Guid ProjectPartID)
		{
		
			
			SqlParameter[] parameters =new SqlParameter[1];
				parameters[0]= new SqlParameter("@ProjectPartID", SqlDbType.UniqueIdentifier);
				parameters[0].Value=ProjectPartID;
			string sql="select [ProjectPartID] ,[PartName] ,[ParentPart]  from [ProjectPart] where [ProjectPartID] = @ProjectPartID";
			return QueryModel(sql, parameters);

		}				
		
		
		
		
		



		
		
		/// <summary>
		/// 为model创建参数数组
		/// </summary>
		private SqlParameter[] createAllParams(hammergo.Model.ProjectPart model)
        {
			SqlParameter[] parameters =new SqlParameter[3];
			parameters[0]= new SqlParameter("@ProjectPartID", SqlDbType.UniqueIdentifier);
					if (model.ProjectPartID.HasValue)
					{
						parameters[0].Value = model.ProjectPartID;
					}
					else
					{
						parameters[0].Value = DBNull.Value;
					}
			parameters[1]= new SqlParameter("@PartName", SqlDbType.NVarChar);
					if(model.PartName != null)
					{
						parameters[1].Value =model.PartName;
					}else
					{
						parameters[1].Value =DBNull.Value;
					}
			parameters[2]= new SqlParameter("@ParentPart", SqlDbType.UniqueIdentifier);
					if (model.ParentPart.HasValue)
					{
						parameters[2].Value = model.ParentPart;
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
		public bool Add(hammergo.Model.ProjectPart model)
		{
			string sql="insert into ProjectPart ([ProjectPartID] ,[PartName] ,[ParentPart]  ) values (@ProjectPartID,@PartName,@ParentPart)";
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
		public bool Add(hammergo.Model.ProjectPart model,System.Data.IDbTransaction trans)
		{
			string sql="insert into ProjectPart ([ProjectPartID] ,[PartName] ,[ParentPart]  ) values (@ProjectPartID,@PartName,@ParentPart)";
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
		public bool Update(hammergo.Model.ProjectPart model)
		{
			string sql="update [ProjectPart] set [ProjectPartID]=@ProjectPartID,[PartName]=@PartName,[ParentPart]=@ParentPart where [ProjectPartID]=@ProjectPartID";
			
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
		public bool Update(hammergo.Model.ProjectPart model,System.Data.IDbTransaction trans)
		{
			string sql="update [ProjectPart] set [ProjectPartID]=@ProjectPartID,[PartName]=@PartName,[ParentPart]=@ParentPart where [ProjectPartID]=@ProjectPartID";
			
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
		public bool Delete(System.Guid ProjectPartID)
		{
			string sql="delete from ProjectPart  where [ProjectPartID]=@ProjectPartID";
			SqlParameter[] parameters =new SqlParameter[1];
				parameters[0]= new SqlParameter("@ProjectPartID", SqlDbType.UniqueIdentifier);
				parameters[0].Value=ProjectPartID;
				
				if (DbHelperSQL.ExecuteNonQuery(CommandType.Text, sql, parameters)== 0)
				{
					return false;
				}
	
				return true;
		}		
		
		
		/// <summary>
		/// 使用事务删除一条数据
		/// </summary>
		public bool Delete(System.Guid ProjectPartID,System.Data.IDbTransaction trans)
		{
			string sql="delete from ProjectPart  where [ProjectPartID]=@ProjectPartID";
			SqlParameter[] parameters =new SqlParameter[1];
				parameters[0]= new SqlParameter("@ProjectPartID", SqlDbType.UniqueIdentifier);
				parameters[0].Value=ProjectPartID;
				
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
        private hammergo.Model.ProjectPart createModel(object[] values)
        {
            hammergo.Model.ProjectPart model = null;
            //创建model对象
            if (values != null)
            {
                model = new hammergo.Model.ProjectPart();
				object obj=null;
                //赋值
					 obj = values[0];
					if (obj is System.DBNull)
					{
						model.ProjectPartID = null;
					}
					else
					{
						model.ProjectPartID = (System.Guid)obj;
					}
					 obj = values[1];
					if (obj is System.DBNull)
					{
						model.PartName = null;
					}
					else
					{
						model.PartName = (string)obj;
					}
					 obj = values[2];
					if (obj is System.DBNull)
					{
						model.ParentPart = null;
					}
					else
					{
						model.ParentPart = (System.Guid)obj;
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
        private hammergo.Model.ProjectPart QueryModel(string sql, params SqlParameter[] parameters)
        {
            //Set up a return value
            hammergo.Model.ProjectPart model = null;

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
        private TrackedList<hammergo.Model.ProjectPart> QueryModelList(string sql, params SqlParameter[] parameters)
        {
            TrackedList<hammergo.Model.ProjectPart> list = new TrackedList<hammergo.Model.ProjectPart>(100);


            hammergo.Model.ProjectPart model = null;
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
        public TrackedList<hammergo.Model.ProjectPart> GetList()
		{
			string sql="select [ProjectPartID] ,[PartName] ,[ParentPart]  from [ProjectPart]";
			 return QueryModelList(sql, null);
		}			
		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.ProjectPart> GetListByProjectPartID(System.Guid  ProjectPartID)
		{
			string sql="select [ProjectPartID] ,[PartName] ,[ParentPart]  from [ProjectPart] where ProjectPartID =@ProjectPartID";
			SqlParameter parameter=new SqlParameter("@ProjectPartID",SqlDbType.UniqueIdentifier);
			parameter.Value=ProjectPartID;
			return QueryModelList(sql, parameter);
		}	
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByProjectPartID(System.Guid  ProjectPartID)
		{
			string sql="select count(1) from ProjectPart where ProjectPartID =@ProjectPartID";
			SqlParameter parameter=new SqlParameter("@ProjectPartID",SqlDbType.UniqueIdentifier);
			parameter.Value=ProjectPartID;
			object obj= DbHelperSQL.ExecuteScalar(CommandType.Text,sql, parameter);
			
			return (int)obj;
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.ProjectPart> GetListByPartName(string  PartName)
		{
			string sql="select [ProjectPartID] ,[PartName] ,[ParentPart]  from [ProjectPart] where PartName =@PartName";
			SqlParameter parameter=new SqlParameter("@PartName",SqlDbType.NVarChar);
			parameter.Value=PartName;
			return QueryModelList(sql, parameter);
		}	
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByPartName(string  PartName)
		{
			string sql="select count(1) from ProjectPart where PartName =@PartName";
			SqlParameter parameter=new SqlParameter("@PartName",SqlDbType.NVarChar);
			parameter.Value=PartName;
			object obj= DbHelperSQL.ExecuteScalar(CommandType.Text,sql, parameter);
			
			return (int)obj;
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.ProjectPart> GetListByParentPart(System.Guid  ParentPart)
		{
			string sql="select [ProjectPartID] ,[PartName] ,[ParentPart]  from [ProjectPart] where ParentPart =@ParentPart";
			SqlParameter parameter=new SqlParameter("@ParentPart",SqlDbType.UniqueIdentifier);
			parameter.Value=ParentPart;
			return QueryModelList(sql, parameter);
		}	
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByParentPart(System.Guid  ParentPart)
		{
			string sql="select count(1) from ProjectPart where ParentPart =@ParentPart";
			SqlParameter parameter=new SqlParameter("@ParentPart",SqlDbType.UniqueIdentifier);
			parameter.Value=ParentPart;
			object obj= DbHelperSQL.ExecuteScalar(CommandType.Text,sql, parameter);
			
			return (int)obj;
		}
		
		
		
    }
}



