
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
    
    public partial interface IConstantParamDAL
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
		hammergo.Model.ConstantParam GetModelBy_appName_ParamName(string appName,string ParamName);
		
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
		hammergo.Model.ConstantParam GetModelBy_appName_ParamSymbol(string appName,string ParamSymbol);
		
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool ExistsBy_ConstantParamID( System.Guid ConstantParamID );		
		
		/// <summary>
		/// 更新记录的记录
		/// </summary>
		bool UpdateBy_ConstantParamID( System.Guid ConstantParamID, System.Guid newConstantParamID);
		
		/// <summary>
		/// 使用事务更新记录
		/// </summary>
		bool UpdateBy_ConstantParamID( System.Guid ConstantParamID, System.Guid newConstantParamID,System.Data.IDbTransaction trans);
		
				
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		hammergo.Model.ConstantParam GetModelBy_ConstantParamID(System.Guid ConstantParamID);
		

		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool Add(hammergo.Model.ConstantParam model);
		
		/// <summary>
		/// 使用事务增加一条数据
		/// </summary>
		bool Add(hammergo.Model.ConstantParam model, System.Data.IDbTransaction trans);		
		

		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(hammergo.Model.ConstantParam model);
		
		/// <summary>
		/// 使用事务更新一条数据
		/// </summary>
		bool Update(hammergo.Model.ConstantParam model,System.Data.IDbTransaction trans);		
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(System.Guid ConstantParamID);
		
		/// <summary>
		/// 使用事务删除一条数据
		/// </summary>
		bool Delete(System.Guid ConstantParamID, System.Data.IDbTransaction trans);		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>
		TrackedList<hammergo.Model.ConstantParam> GetList();		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.ConstantParam> GetListByConstantParamID(System.Guid  ConstantParamID);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByConstantParamID(System.Guid  ConstantParamID);
			
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.ConstantParam> GetListByappName(string  AppName);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByappName(string  AppName);
			
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.ConstantParam> GetListByParamName(string  ParamName);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByParamName(string  ParamName);
			
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.ConstantParam> GetListByParamSymbol(string  ParamSymbol);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByParamSymbol(string  ParamSymbol);
			
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.ConstantParam> GetListByUnitSymbol(string  UnitSymbol);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByUnitSymbol(string  UnitSymbol);
			
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.ConstantParam> GetListByPrecisionNum(byte  PrecisionNum);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByPrecisionNum(byte  PrecisionNum);
			
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.ConstantParam> GetListByOrder(byte  Order);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByOrder(byte  Order);
			
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.ConstantParam> GetListByVal(double  Val);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByVal(double  Val);
			
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.ConstantParam> GetListByDescription(string  Description);
		
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
		/// 获取Val的最小值
		/// </summary>				
		double? GetMinVal();

				
				
		
				
		/// <summary>
		/// 获取PrecisionNum的最大值
		/// </summary>				
		byte? GetMaxPrecisionNum();

				
				
				
		/// <summary>
		/// 获取Order的最大值
		/// </summary>				
		byte? GetMaxOrder();

				
				
				
		/// <summary>
		/// 获取Val的最大值
		/// </summary>				
		double? GetMaxVal();

				
				
		

		
    }
}



