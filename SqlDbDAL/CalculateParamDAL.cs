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
    
    public partial class  CalculateParamDAL:ICalculateParamDAL
    {
		private const int columnsCnt = 10;
		protected string SQL_SELECT_All="select [CalculateParamID] ,[appName] ,[ParamName] ,[ParamSymbol] ,[UnitSymbol] ,[PrecisionNum] ,[Order] ,[CalculateExpress] ,[CalculateOrder] ,[Description]  from [CalculateParam]";
		protected string SQL_Field="CalculateParam.CalculateParamID ,CalculateParam.appName ,CalculateParam.ParamName ,CalculateParam.ParamSymbol ,CalculateParam.UnitSymbol ,CalculateParam.PrecisionNum ,CalculateParam.Order ,CalculateParam.CalculateExpress ,CalculateParam.CalculateOrder ,CalculateParam.Description ";
		
		
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool ExistsBy_appName_ParamName(string appName,string ParamName)
		{

			
			SqlParameter[] parameters =new SqlParameter[2];
				parameters[0]= new SqlParameter("@appName", SqlDbType.NVarChar);
				parameters[0].Value=appName;
				
				parameters[1]= new SqlParameter("@ParamName", SqlDbType.NVarChar);
				parameters[1].Value=ParamName;
				
			
			string sql="select count(1) from CalculateParam where [appName] = @appName and [ParamName] = @ParamName";
			return DbHelperSQL.Exists(sql, parameters);
		}
		
		/// <summary>
		/// 更新记录的记录
		/// </summary>
		public bool UpdateBy_appName_ParamName( string appName,string ParamName, string newappName,string newParamName)
		{
			SqlParameter[] parameters =new SqlParameter[4];
				parameters[0]= new SqlParameter("@newappName", SqlDbType.NVarChar);
				parameters[0].Value=newappName;
				
				parameters[1]= new SqlParameter("@newParamName", SqlDbType.NVarChar);
				parameters[1].Value=newParamName;
				
				parameters[2]= new SqlParameter("@appName", SqlDbType.NVarChar);
				parameters[2].Value=appName;
				
				parameters[3]= new SqlParameter("@ParamName", SqlDbType.NVarChar);
				parameters[3].Value=ParamName;
				
			
			string sql="update [CalculateParam] set [appName]=@newappName,[ParamName]=@newParamName where [appName] = @appName and [ParamName] = @ParamName";
			
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
		public bool UpdateBy_appName_ParamName( string appName,string ParamName, string newappName,string newParamName,System.Data.IDbTransaction trans)
		{
			SqlParameter[] parameters =new SqlParameter[4];
				parameters[0]= new SqlParameter("@newappName", SqlDbType.NVarChar);
				parameters[0].Value=newappName;
				
				parameters[1]= new SqlParameter("@newParamName", SqlDbType.NVarChar);
				parameters[1].Value=newParamName;
				
				parameters[2]= new SqlParameter("@appName", SqlDbType.NVarChar);
				parameters[2].Value=appName;
				
				parameters[3]= new SqlParameter("@ParamName", SqlDbType.NVarChar);
				parameters[3].Value=ParamName;
				
			
			string sql="update [CalculateParam] set [appName]=@newappName,[ParamName]=@newParamName where [appName] = @appName and [ParamName] = @ParamName";
			
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
		public hammergo.Model.CalculateParam GetModelBy_appName_ParamName(string appName,string ParamName)
		{
		
			
			SqlParameter[] parameters =new SqlParameter[2];
				parameters[0]= new SqlParameter("@appName", SqlDbType.NVarChar);
				parameters[0].Value=appName;
				parameters[1]= new SqlParameter("@ParamName", SqlDbType.NVarChar);
				parameters[1].Value=ParamName;
			string sql="select [CalculateParamID] ,[appName] ,[ParamName] ,[ParamSymbol] ,[UnitSymbol] ,[PrecisionNum] ,[Order] ,[CalculateExpress] ,[CalculateOrder] ,[Description]  from [CalculateParam] where [appName] = @appName and [ParamName] = @ParamName";
			return QueryModel(sql, parameters);

		}				
		
		
		
		
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool ExistsBy_appName_ParamSymbol(string appName,string ParamSymbol)
		{

			
			SqlParameter[] parameters =new SqlParameter[2];
				parameters[0]= new SqlParameter("@appName", SqlDbType.NVarChar);
				parameters[0].Value=appName;
				
				parameters[1]= new SqlParameter("@ParamSymbol", SqlDbType.NVarChar);
				parameters[1].Value=ParamSymbol;
				
			
			string sql="select count(1) from CalculateParam where [appName] = @appName and [ParamSymbol] = @ParamSymbol";
			return DbHelperSQL.Exists(sql, parameters);
		}
		
		/// <summary>
		/// 更新记录的记录
		/// </summary>
		public bool UpdateBy_appName_ParamSymbol( string appName,string ParamSymbol, string newappName,string newParamSymbol)
		{
			SqlParameter[] parameters =new SqlParameter[4];
				parameters[0]= new SqlParameter("@newappName", SqlDbType.NVarChar);
				parameters[0].Value=newappName;
				
				parameters[1]= new SqlParameter("@newParamSymbol", SqlDbType.NVarChar);
				parameters[1].Value=newParamSymbol;
				
				parameters[2]= new SqlParameter("@appName", SqlDbType.NVarChar);
				parameters[2].Value=appName;
				
				parameters[3]= new SqlParameter("@ParamSymbol", SqlDbType.NVarChar);
				parameters[3].Value=ParamSymbol;
				
			
			string sql="update [CalculateParam] set [appName]=@newappName,[ParamSymbol]=@newParamSymbol where [appName] = @appName and [ParamSymbol] = @ParamSymbol";
			
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
		public bool UpdateBy_appName_ParamSymbol( string appName,string ParamSymbol, string newappName,string newParamSymbol,System.Data.IDbTransaction trans)
		{
			SqlParameter[] parameters =new SqlParameter[4];
				parameters[0]= new SqlParameter("@newappName", SqlDbType.NVarChar);
				parameters[0].Value=newappName;
				
				parameters[1]= new SqlParameter("@newParamSymbol", SqlDbType.NVarChar);
				parameters[1].Value=newParamSymbol;
				
				parameters[2]= new SqlParameter("@appName", SqlDbType.NVarChar);
				parameters[2].Value=appName;
				
				parameters[3]= new SqlParameter("@ParamSymbol", SqlDbType.NVarChar);
				parameters[3].Value=ParamSymbol;
				
			
			string sql="update [CalculateParam] set [appName]=@newappName,[ParamSymbol]=@newParamSymbol where [appName] = @appName and [ParamSymbol] = @ParamSymbol";
			
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
		public hammergo.Model.CalculateParam GetModelBy_appName_ParamSymbol(string appName,string ParamSymbol)
		{
		
			
			SqlParameter[] parameters =new SqlParameter[2];
				parameters[0]= new SqlParameter("@appName", SqlDbType.NVarChar);
				parameters[0].Value=appName;
				parameters[1]= new SqlParameter("@ParamSymbol", SqlDbType.NVarChar);
				parameters[1].Value=ParamSymbol;
			string sql="select [CalculateParamID] ,[appName] ,[ParamName] ,[ParamSymbol] ,[UnitSymbol] ,[PrecisionNum] ,[Order] ,[CalculateExpress] ,[CalculateOrder] ,[Description]  from [CalculateParam] where [appName] = @appName and [ParamSymbol] = @ParamSymbol";
			return QueryModel(sql, parameters);

		}				
		
		
		
		
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool ExistsBy_CalculateParamID(System.Guid CalculateParamID)
		{

			
			SqlParameter[] parameters =new SqlParameter[1];
				parameters[0]= new SqlParameter("@CalculateParamID", SqlDbType.UniqueIdentifier);
				parameters[0].Value=CalculateParamID;
				
			
			string sql="select count(1) from CalculateParam where [CalculateParamID] = @CalculateParamID";
			return DbHelperSQL.Exists(sql, parameters);
		}
		
		/// <summary>
		/// 更新记录的记录
		/// </summary>
		public bool UpdateBy_CalculateParamID( System.Guid CalculateParamID, System.Guid newCalculateParamID)
		{
			SqlParameter[] parameters =new SqlParameter[2];
				parameters[0]= new SqlParameter("@newCalculateParamID", SqlDbType.UniqueIdentifier);
				parameters[0].Value=newCalculateParamID;
				
				parameters[1]= new SqlParameter("@CalculateParamID", SqlDbType.UniqueIdentifier);
				parameters[1].Value=CalculateParamID;
				
			
			string sql="update [CalculateParam] set [CalculateParamID]=@newCalculateParamID where [CalculateParamID] = @CalculateParamID";
			
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
		public bool UpdateBy_CalculateParamID( System.Guid CalculateParamID, System.Guid newCalculateParamID,System.Data.IDbTransaction trans)
		{
			SqlParameter[] parameters =new SqlParameter[2];
				parameters[0]= new SqlParameter("@newCalculateParamID", SqlDbType.UniqueIdentifier);
				parameters[0].Value=newCalculateParamID;
				
				parameters[1]= new SqlParameter("@CalculateParamID", SqlDbType.UniqueIdentifier);
				parameters[1].Value=CalculateParamID;
				
			
			string sql="update [CalculateParam] set [CalculateParamID]=@newCalculateParamID where [CalculateParamID] = @CalculateParamID";
			
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
		public hammergo.Model.CalculateParam GetModelBy_CalculateParamID(System.Guid CalculateParamID)
		{
		
			
			SqlParameter[] parameters =new SqlParameter[1];
				parameters[0]= new SqlParameter("@CalculateParamID", SqlDbType.UniqueIdentifier);
				parameters[0].Value=CalculateParamID;
			string sql="select [CalculateParamID] ,[appName] ,[ParamName] ,[ParamSymbol] ,[UnitSymbol] ,[PrecisionNum] ,[Order] ,[CalculateExpress] ,[CalculateOrder] ,[Description]  from [CalculateParam] where [CalculateParamID] = @CalculateParamID";
			return QueryModel(sql, parameters);

		}				
		
		
		
		
		



		
		
		/// <summary>
		/// 为model创建参数数组
		/// </summary>
		private SqlParameter[] createAllParams(hammergo.Model.CalculateParam model)
        {
			SqlParameter[] parameters =new SqlParameter[10];
			parameters[0]= new SqlParameter("@CalculateParamID", SqlDbType.UniqueIdentifier);
					if (model.CalculateParamID.HasValue)
					{
						parameters[0].Value = model.CalculateParamID;
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
			parameters[2]= new SqlParameter("@ParamName", SqlDbType.NVarChar);
					if(model.ParamName != null)
					{
						parameters[2].Value =model.ParamName;
					}else
					{
						parameters[2].Value =DBNull.Value;
					}
			parameters[3]= new SqlParameter("@ParamSymbol", SqlDbType.NVarChar);
					if(model.ParamSymbol != null)
					{
						parameters[3].Value =model.ParamSymbol;
					}else
					{
						parameters[3].Value =DBNull.Value;
					}
			parameters[4]= new SqlParameter("@UnitSymbol", SqlDbType.NVarChar);
					if(model.UnitSymbol != null)
					{
						parameters[4].Value =model.UnitSymbol;
					}else
					{
						parameters[4].Value =DBNull.Value;
					}
			parameters[5]= new SqlParameter("@PrecisionNum", SqlDbType.TinyInt);
					if (model.PrecisionNum.HasValue)
					{
						parameters[5].Value = model.PrecisionNum;
					}
					else
					{
						parameters[5].Value = DBNull.Value;
					}
			parameters[6]= new SqlParameter("@Order", SqlDbType.TinyInt);
					if (model.Order.HasValue)
					{
						parameters[6].Value = model.Order;
					}
					else
					{
						parameters[6].Value = DBNull.Value;
					}
			parameters[7]= new SqlParameter("@CalculateExpress", SqlDbType.NVarChar);
					if(model.CalculateExpress != null)
					{
						parameters[7].Value =model.CalculateExpress;
					}else
					{
						parameters[7].Value =DBNull.Value;
					}
			parameters[8]= new SqlParameter("@CalculateOrder", SqlDbType.TinyInt);
					if (model.CalculateOrder.HasValue)
					{
						parameters[8].Value = model.CalculateOrder;
					}
					else
					{
						parameters[8].Value = DBNull.Value;
					}
			parameters[9]= new SqlParameter("@Description", SqlDbType.NVarChar);
					if(model.Description != null)
					{
						parameters[9].Value =model.Description;
					}else
					{
						parameters[9].Value =DBNull.Value;
					}
			return  parameters;
		}
		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(hammergo.Model.CalculateParam model)
		{
			string sql="insert into CalculateParam ([CalculateParamID] ,[appName] ,[ParamName] ,[ParamSymbol] ,[UnitSymbol] ,[PrecisionNum] ,[Order] ,[CalculateExpress] ,[CalculateOrder] ,[Description]  ) values (@CalculateParamID,@appName,@ParamName,@ParamSymbol,@UnitSymbol,@PrecisionNum,@Order,@CalculateExpress,@CalculateOrder,@Description)";
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
		public bool Add(hammergo.Model.CalculateParam model,System.Data.IDbTransaction trans)
		{
			string sql="insert into CalculateParam ([CalculateParamID] ,[appName] ,[ParamName] ,[ParamSymbol] ,[UnitSymbol] ,[PrecisionNum] ,[Order] ,[CalculateExpress] ,[CalculateOrder] ,[Description]  ) values (@CalculateParamID,@appName,@ParamName,@ParamSymbol,@UnitSymbol,@PrecisionNum,@Order,@CalculateExpress,@CalculateOrder,@Description)";
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
		public bool Update(hammergo.Model.CalculateParam model)
		{
			string sql="update [CalculateParam] set [CalculateParamID]=@CalculateParamID,[appName]=@appName,[ParamName]=@ParamName,[ParamSymbol]=@ParamSymbol,[UnitSymbol]=@UnitSymbol,[PrecisionNum]=@PrecisionNum,[Order]=@Order,[CalculateExpress]=@CalculateExpress,[CalculateOrder]=@CalculateOrder,[Description]=@Description where [CalculateParamID]=@CalculateParamID";
			
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
		public bool Update(hammergo.Model.CalculateParam model,System.Data.IDbTransaction trans)
		{
			string sql="update [CalculateParam] set [CalculateParamID]=@CalculateParamID,[appName]=@appName,[ParamName]=@ParamName,[ParamSymbol]=@ParamSymbol,[UnitSymbol]=@UnitSymbol,[PrecisionNum]=@PrecisionNum,[Order]=@Order,[CalculateExpress]=@CalculateExpress,[CalculateOrder]=@CalculateOrder,[Description]=@Description where [CalculateParamID]=@CalculateParamID";
			
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
		public bool Delete(System.Guid CalculateParamID)
		{
			string sql="delete from CalculateParam  where [CalculateParamID]=@CalculateParamID";
			SqlParameter[] parameters =new SqlParameter[1];
				parameters[0]= new SqlParameter("@CalculateParamID", SqlDbType.UniqueIdentifier);
				parameters[0].Value=CalculateParamID;
				
				if (DbHelperSQL.ExecuteNonQuery(CommandType.Text, sql, parameters)== 0)
				{
					return false;
				}
	
				return true;
		}		
		
		
		/// <summary>
		/// 使用事务删除一条数据
		/// </summary>
		public bool Delete(System.Guid CalculateParamID,System.Data.IDbTransaction trans)
		{
			string sql="delete from CalculateParam  where [CalculateParamID]=@CalculateParamID";
			SqlParameter[] parameters =new SqlParameter[1];
				parameters[0]= new SqlParameter("@CalculateParamID", SqlDbType.UniqueIdentifier);
				parameters[0].Value=CalculateParamID;
				
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
        private hammergo.Model.CalculateParam createModel(object[] values)
        {
            hammergo.Model.CalculateParam model = null;
            //创建model对象
            if (values != null)
            {
                model = new hammergo.Model.CalculateParam();
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
						model.AppName = null;
					}
					else
					{
						model.AppName = (string)obj;
					}
					 obj = values[2];
					if (obj is System.DBNull)
					{
						model.ParamName = null;
					}
					else
					{
						model.ParamName = (string)obj;
					}
					 obj = values[3];
					if (obj is System.DBNull)
					{
						model.ParamSymbol = null;
					}
					else
					{
						model.ParamSymbol = (string)obj;
					}
					 obj = values[4];
					if (obj is System.DBNull)
					{
						model.UnitSymbol = null;
					}
					else
					{
						model.UnitSymbol = (string)obj;
					}
					 obj = values[5];
					if (obj is System.DBNull)
					{
						model.PrecisionNum = null;
					}
					else
					{
						model.PrecisionNum = (byte)obj;
					}
					 obj = values[6];
					if (obj is System.DBNull)
					{
						model.Order = null;
					}
					else
					{
						model.Order = (byte)obj;
					}
					 obj = values[7];
					if (obj is System.DBNull)
					{
						model.CalculateExpress = null;
					}
					else
					{
						model.CalculateExpress = (string)obj;
					}
					 obj = values[8];
					if (obj is System.DBNull)
					{
						model.CalculateOrder = null;
					}
					else
					{
						model.CalculateOrder = (byte)obj;
					}
					 obj = values[9];
					if (obj is System.DBNull)
					{
						model.Description = null;
					}
					else
					{
						model.Description = (string)obj;
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
        private hammergo.Model.CalculateParam QueryModel(string sql, params SqlParameter[] parameters)
        {
            //Set up a return value
            hammergo.Model.CalculateParam model = null;

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
        private TrackedList<hammergo.Model.CalculateParam> QueryModelList(string sql, params SqlParameter[] parameters)
        {
            TrackedList<hammergo.Model.CalculateParam> list = new TrackedList<hammergo.Model.CalculateParam>(100);


            hammergo.Model.CalculateParam model = null;
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
		/// 获取PrecisionNum的最大值
		/// </summary>				
		public byte? GetMaxPrecisionNum()
		{
			byte? val=null;
			string sql="select max(PrecisionNum) from CalculateParam";
			object obj =DbHelperSQL.ExecuteScalar(CommandType.Text, sql);

			if (obj is System.DBNull ==false)
			{
				val = (byte)obj;
			}
  			return val;
		}
				
				
				
		/// <summary>
		/// 获取Order的最大值
		/// </summary>				
		public byte? GetMaxOrder()
		{
			byte? val=null;
			string sql="select max(Order) from CalculateParam";
			object obj =DbHelperSQL.ExecuteScalar(CommandType.Text, sql);

			if (obj is System.DBNull ==false)
			{
				val = (byte)obj;
			}
  			return val;
		}
				
				
				
		/// <summary>
		/// 获取CalculateOrder的最大值
		/// </summary>				
		public byte? GetMaxCalculateOrder()
		{
			byte? val=null;
			string sql="select max(CalculateOrder) from CalculateParam";
			object obj =DbHelperSQL.ExecuteScalar(CommandType.Text, sql);

			if (obj is System.DBNull ==false)
			{
				val = (byte)obj;
			}
  			return val;
		}
				
				
		
		
				
		/// <summary>
		/// 获取PrecisionNum的最小值
		/// </summary>				
		public byte? GetMinPrecisionNum()
		{
			byte? val=null;
			string sql="select min(PrecisionNum) from CalculateParam";
			object obj =DbHelperSQL.ExecuteScalar(CommandType.Text, sql);

			if (obj is System.DBNull ==false)
			{
				val = (byte)obj;
			}
  			return val;
		}
				
				
				
		/// <summary>
		/// 获取Order的最小值
		/// </summary>				
		public byte? GetMinOrder()
		{
			byte? val=null;
			string sql="select min(Order) from CalculateParam";
			object obj =DbHelperSQL.ExecuteScalar(CommandType.Text, sql);

			if (obj is System.DBNull ==false)
			{
				val = (byte)obj;
			}
  			return val;
		}
				
				
				
		/// <summary>
		/// 获取CalculateOrder的最小值
		/// </summary>				
		public byte? GetMinCalculateOrder()
		{
			byte? val=null;
			string sql="select min(CalculateOrder) from CalculateParam";
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
        public TrackedList<hammergo.Model.CalculateParam> GetList()
		{
			string sql="select [CalculateParamID] ,[appName] ,[ParamName] ,[ParamSymbol] ,[UnitSymbol] ,[PrecisionNum] ,[Order] ,[CalculateExpress] ,[CalculateOrder] ,[Description]  from [CalculateParam]";
			 return QueryModelList(sql, null);
		}			
		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.CalculateParam> GetListByCalculateParamID(System.Guid  CalculateParamID)
		{
			string sql="select [CalculateParamID] ,[appName] ,[ParamName] ,[ParamSymbol] ,[UnitSymbol] ,[PrecisionNum] ,[Order] ,[CalculateExpress] ,[CalculateOrder] ,[Description]  from [CalculateParam] where CalculateParamID =@CalculateParamID";
			SqlParameter parameter=new SqlParameter("@CalculateParamID",SqlDbType.UniqueIdentifier);
			parameter.Value=CalculateParamID;
			return QueryModelList(sql, parameter);
		}	
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByCalculateParamID(System.Guid  CalculateParamID)
		{
			string sql="select count(1) from CalculateParam where CalculateParamID =@CalculateParamID";
			SqlParameter parameter=new SqlParameter("@CalculateParamID",SqlDbType.UniqueIdentifier);
			parameter.Value=CalculateParamID;
			object obj= DbHelperSQL.ExecuteScalar(CommandType.Text,sql, parameter);
			
			return (int)obj;
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.CalculateParam> GetListByappName(string  AppName)
		{
			string sql="select [CalculateParamID] ,[appName] ,[ParamName] ,[ParamSymbol] ,[UnitSymbol] ,[PrecisionNum] ,[Order] ,[CalculateExpress] ,[CalculateOrder] ,[Description]  from [CalculateParam] where appName =@appName";
			SqlParameter parameter=new SqlParameter("@appName",SqlDbType.NVarChar);
			parameter.Value=AppName;
			return QueryModelList(sql, parameter);
		}	
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByappName(string  AppName)
		{
			string sql="select count(1) from CalculateParam where appName =@appName";
			SqlParameter parameter=new SqlParameter("@appName",SqlDbType.NVarChar);
			parameter.Value=AppName;
			object obj= DbHelperSQL.ExecuteScalar(CommandType.Text,sql, parameter);
			
			return (int)obj;
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.CalculateParam> GetListByParamName(string  ParamName)
		{
			string sql="select [CalculateParamID] ,[appName] ,[ParamName] ,[ParamSymbol] ,[UnitSymbol] ,[PrecisionNum] ,[Order] ,[CalculateExpress] ,[CalculateOrder] ,[Description]  from [CalculateParam] where ParamName =@ParamName";
			SqlParameter parameter=new SqlParameter("@ParamName",SqlDbType.NVarChar);
			parameter.Value=ParamName;
			return QueryModelList(sql, parameter);
		}	
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByParamName(string  ParamName)
		{
			string sql="select count(1) from CalculateParam where ParamName =@ParamName";
			SqlParameter parameter=new SqlParameter("@ParamName",SqlDbType.NVarChar);
			parameter.Value=ParamName;
			object obj= DbHelperSQL.ExecuteScalar(CommandType.Text,sql, parameter);
			
			return (int)obj;
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.CalculateParam> GetListByParamSymbol(string  ParamSymbol)
		{
			string sql="select [CalculateParamID] ,[appName] ,[ParamName] ,[ParamSymbol] ,[UnitSymbol] ,[PrecisionNum] ,[Order] ,[CalculateExpress] ,[CalculateOrder] ,[Description]  from [CalculateParam] where ParamSymbol =@ParamSymbol";
			SqlParameter parameter=new SqlParameter("@ParamSymbol",SqlDbType.NVarChar);
			parameter.Value=ParamSymbol;
			return QueryModelList(sql, parameter);
		}	
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByParamSymbol(string  ParamSymbol)
		{
			string sql="select count(1) from CalculateParam where ParamSymbol =@ParamSymbol";
			SqlParameter parameter=new SqlParameter("@ParamSymbol",SqlDbType.NVarChar);
			parameter.Value=ParamSymbol;
			object obj= DbHelperSQL.ExecuteScalar(CommandType.Text,sql, parameter);
			
			return (int)obj;
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.CalculateParam> GetListByUnitSymbol(string  UnitSymbol)
		{
			string sql="select [CalculateParamID] ,[appName] ,[ParamName] ,[ParamSymbol] ,[UnitSymbol] ,[PrecisionNum] ,[Order] ,[CalculateExpress] ,[CalculateOrder] ,[Description]  from [CalculateParam] where UnitSymbol =@UnitSymbol";
			SqlParameter parameter=new SqlParameter("@UnitSymbol",SqlDbType.NVarChar);
			parameter.Value=UnitSymbol;
			return QueryModelList(sql, parameter);
		}	
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByUnitSymbol(string  UnitSymbol)
		{
			string sql="select count(1) from CalculateParam where UnitSymbol =@UnitSymbol";
			SqlParameter parameter=new SqlParameter("@UnitSymbol",SqlDbType.NVarChar);
			parameter.Value=UnitSymbol;
			object obj= DbHelperSQL.ExecuteScalar(CommandType.Text,sql, parameter);
			
			return (int)obj;
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.CalculateParam> GetListByPrecisionNum(byte  PrecisionNum)
		{
			string sql="select [CalculateParamID] ,[appName] ,[ParamName] ,[ParamSymbol] ,[UnitSymbol] ,[PrecisionNum] ,[Order] ,[CalculateExpress] ,[CalculateOrder] ,[Description]  from [CalculateParam] where PrecisionNum =@PrecisionNum";
			SqlParameter parameter=new SqlParameter("@PrecisionNum",SqlDbType.TinyInt);
			parameter.Value=PrecisionNum;
			return QueryModelList(sql, parameter);
		}	
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByPrecisionNum(byte  PrecisionNum)
		{
			string sql="select count(1) from CalculateParam where PrecisionNum =@PrecisionNum";
			SqlParameter parameter=new SqlParameter("@PrecisionNum",SqlDbType.TinyInt);
			parameter.Value=PrecisionNum;
			object obj= DbHelperSQL.ExecuteScalar(CommandType.Text,sql, parameter);
			
			return (int)obj;
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.CalculateParam> GetListByOrder(byte  Order)
		{
			string sql="select [CalculateParamID] ,[appName] ,[ParamName] ,[ParamSymbol] ,[UnitSymbol] ,[PrecisionNum] ,[Order] ,[CalculateExpress] ,[CalculateOrder] ,[Description]  from [CalculateParam] where Order =@Order";
			SqlParameter parameter=new SqlParameter("@Order",SqlDbType.TinyInt);
			parameter.Value=Order;
			return QueryModelList(sql, parameter);
		}	
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByOrder(byte  Order)
		{
			string sql="select count(1) from CalculateParam where Order =@Order";
			SqlParameter parameter=new SqlParameter("@Order",SqlDbType.TinyInt);
			parameter.Value=Order;
			object obj= DbHelperSQL.ExecuteScalar(CommandType.Text,sql, parameter);
			
			return (int)obj;
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.CalculateParam> GetListByCalculateExpress(string  CalculateExpress)
		{
			string sql="select [CalculateParamID] ,[appName] ,[ParamName] ,[ParamSymbol] ,[UnitSymbol] ,[PrecisionNum] ,[Order] ,[CalculateExpress] ,[CalculateOrder] ,[Description]  from [CalculateParam] where CalculateExpress =@CalculateExpress";
			SqlParameter parameter=new SqlParameter("@CalculateExpress",SqlDbType.NVarChar);
			parameter.Value=CalculateExpress;
			return QueryModelList(sql, parameter);
		}	
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByCalculateExpress(string  CalculateExpress)
		{
			string sql="select count(1) from CalculateParam where CalculateExpress =@CalculateExpress";
			SqlParameter parameter=new SqlParameter("@CalculateExpress",SqlDbType.NVarChar);
			parameter.Value=CalculateExpress;
			object obj= DbHelperSQL.ExecuteScalar(CommandType.Text,sql, parameter);
			
			return (int)obj;
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.CalculateParam> GetListByCalculateOrder(byte  CalculateOrder)
		{
			string sql="select [CalculateParamID] ,[appName] ,[ParamName] ,[ParamSymbol] ,[UnitSymbol] ,[PrecisionNum] ,[Order] ,[CalculateExpress] ,[CalculateOrder] ,[Description]  from [CalculateParam] where CalculateOrder =@CalculateOrder";
			SqlParameter parameter=new SqlParameter("@CalculateOrder",SqlDbType.TinyInt);
			parameter.Value=CalculateOrder;
			return QueryModelList(sql, parameter);
		}	
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByCalculateOrder(byte  CalculateOrder)
		{
			string sql="select count(1) from CalculateParam where CalculateOrder =@CalculateOrder";
			SqlParameter parameter=new SqlParameter("@CalculateOrder",SqlDbType.TinyInt);
			parameter.Value=CalculateOrder;
			object obj= DbHelperSQL.ExecuteScalar(CommandType.Text,sql, parameter);
			
			return (int)obj;
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.CalculateParam> GetListByDescription(string  Description)
		{
			string sql="select [CalculateParamID] ,[appName] ,[ParamName] ,[ParamSymbol] ,[UnitSymbol] ,[PrecisionNum] ,[Order] ,[CalculateExpress] ,[CalculateOrder] ,[Description]  from [CalculateParam] where Description =@Description";
			SqlParameter parameter=new SqlParameter("@Description",SqlDbType.NVarChar);
			parameter.Value=Description;
			return QueryModelList(sql, parameter);
		}	
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByDescription(string  Description)
		{
			string sql="select count(1) from CalculateParam where Description =@Description";
			SqlParameter parameter=new SqlParameter("@Description",SqlDbType.NVarChar);
			parameter.Value=Description;
			object obj= DbHelperSQL.ExecuteScalar(CommandType.Text,sql, parameter);
			
			return (int)obj;
		}
		
		
		
    }
}



