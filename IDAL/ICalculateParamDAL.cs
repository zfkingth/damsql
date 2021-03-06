
 /**////////////////////////////////////////////////////////////////////////////////////////
 // Description: DAL Interface class .
 // ---------------------
 // Copyright  2009 hammergo@163.com
 // ---------------------
 // History
 //    2013年3月6日 22:29:14    zfking    
 /**////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using hammergo.Tracking;

namespace hammergo.IDAL
{
    /// <summary>
	/// 接口摘要说明。
	/// </summary>
    
    public partial interface ICalculateParamDAL
    {
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool ExistsBy_appName_ParamName( string appName,string ParamName );		
		
		/// <summary>
		/// 更新记录的记录
		/// </summary>
		bool UpdateBy_appName_ParamName( string appName,string ParamName, string newappName,string newParamName);
		
		/// <summary>
		/// 使用事务更新记录
		/// </summary>
		bool UpdateBy_appName_ParamName( string appName,string ParamName, string newappName,string newParamName,System.Data.IDbTransaction trans);
		
				
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		hammergo.Model.CalculateParam GetModelBy_appName_ParamName(string appName,string ParamName);
		
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool ExistsBy_appName_ParamSymbol( string appName,string ParamSymbol );		
		
		/// <summary>
		/// 更新记录的记录
		/// </summary>
		bool UpdateBy_appName_ParamSymbol( string appName,string ParamSymbol, string newappName,string newParamSymbol);
		
		/// <summary>
		/// 使用事务更新记录
		/// </summary>
		bool UpdateBy_appName_ParamSymbol( string appName,string ParamSymbol, string newappName,string newParamSymbol,System.Data.IDbTransaction trans);
		
				
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		hammergo.Model.CalculateParam GetModelBy_appName_ParamSymbol(string appName,string ParamSymbol);
		
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool ExistsBy_CalculateParamID( System.Guid CalculateParamID );		
		
		/// <summary>
		/// 更新记录的记录
		/// </summary>
		bool UpdateBy_CalculateParamID( System.Guid CalculateParamID, System.Guid newCalculateParamID);
		
		/// <summary>
		/// 使用事务更新记录
		/// </summary>
		bool UpdateBy_CalculateParamID( System.Guid CalculateParamID, System.Guid newCalculateParamID,System.Data.IDbTransaction trans);
		
				
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		hammergo.Model.CalculateParam GetModelBy_CalculateParamID(System.Guid CalculateParamID);
		

		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool Add(hammergo.Model.CalculateParam model);
		
		/// <summary>
		/// 使用事务增加一条数据
		/// </summary>
		bool Add(hammergo.Model.CalculateParam model, System.Data.IDbTransaction trans);		
		

		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(hammergo.Model.CalculateParam model);
		
		/// <summary>
		/// 使用事务更新一条数据
		/// </summary>
		bool Update(hammergo.Model.CalculateParam model,System.Data.IDbTransaction trans);		
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(System.Guid CalculateParamID);
		
		/// <summary>
		/// 使用事务删除一条数据
		/// </summary>
		bool Delete(System.Guid CalculateParamID, System.Data.IDbTransaction trans);		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>
		TrackedList<hammergo.Model.CalculateParam> GetList();		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.CalculateParam> GetListByCalculateParamID(System.Guid  CalculateParamID);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByCalculateParamID(System.Guid  CalculateParamID);
			
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.CalculateParam> GetListByappName(string  AppName);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByappName(string  AppName);
			
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.CalculateParam> GetListByParamName(string  ParamName);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByParamName(string  ParamName);
			
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.CalculateParam> GetListByParamSymbol(string  ParamSymbol);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByParamSymbol(string  ParamSymbol);
			
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.CalculateParam> GetListByUnitSymbol(string  UnitSymbol);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByUnitSymbol(string  UnitSymbol);
			
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.CalculateParam> GetListByPrecisionNum(byte  PrecisionNum);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByPrecisionNum(byte  PrecisionNum);
			
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.CalculateParam> GetListByOrder(byte  Order);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByOrder(byte  Order);
			
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.CalculateParam> GetListByCalculateExpress(string  CalculateExpress);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByCalculateExpress(string  CalculateExpress);
			
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.CalculateParam> GetListByCalculateOrder(byte  CalculateOrder);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByCalculateOrder(byte  CalculateOrder);
			
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.CalculateParam> GetListByDescription(string  Description);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByDescription(string  Description);
			
		
		
		
		
				
		/// <summary>
		/// 获取PrecisionNum的最小值
		/// </summary>				
		byte? GetMinPrecisionNum();

				
				
				
		/// <summary>
		/// 获取Order的最小值
		/// </summary>				
		byte? GetMinOrder();

				
				
				
		/// <summary>
		/// 获取CalculateOrder的最小值
		/// </summary>				
		byte? GetMinCalculateOrder();

				
				
		
				
		/// <summary>
		/// 获取PrecisionNum的最大值
		/// </summary>				
		byte? GetMaxPrecisionNum();

				
				
				
		/// <summary>
		/// 获取Order的最大值
		/// </summary>				
		byte? GetMaxOrder();

				
				
				
		/// <summary>
		/// 获取CalculateOrder的最大值
		/// </summary>				
		byte? GetMaxCalculateOrder();

				
				
		

		
    }
}



