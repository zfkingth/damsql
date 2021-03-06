
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
    
    public partial interface IApparatusDAL
    {
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool ExistsBy_CalculateName( string CalculateName );		
		
		/// <summary>
		/// 更新记录的记录
		/// </summary>
		bool UpdateBy_CalculateName( string CalculateName, string newCalculateName);
		
		/// <summary>
		/// 使用事务更新记录
		/// </summary>
		bool UpdateBy_CalculateName( string CalculateName, string newCalculateName,System.Data.IDbTransaction trans);
		
				
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		hammergo.Model.Apparatus GetModelBy_CalculateName(string CalculateName);
		
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool ExistsBy_AppName( string AppName );		
		
		/// <summary>
		/// 更新记录的记录
		/// </summary>
		bool UpdateBy_AppName( string AppName, string newAppName);
		
		/// <summary>
		/// 使用事务更新记录
		/// </summary>
		bool UpdateBy_AppName( string AppName, string newAppName,System.Data.IDbTransaction trans);
		
				
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		hammergo.Model.Apparatus GetModelBy_AppName(string AppName);
		

		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool Add(hammergo.Model.Apparatus model);
		
		/// <summary>
		/// 使用事务增加一条数据
		/// </summary>
		bool Add(hammergo.Model.Apparatus model, System.Data.IDbTransaction trans);		
		

		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(hammergo.Model.Apparatus model);
		
		/// <summary>
		/// 使用事务更新一条数据
		/// </summary>
		bool Update(hammergo.Model.Apparatus model,System.Data.IDbTransaction trans);		
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(string AppName);
		
		/// <summary>
		/// 使用事务删除一条数据
		/// </summary>
		bool Delete(string AppName, System.Data.IDbTransaction trans);		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>
		TrackedList<hammergo.Model.Apparatus> GetList();		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.Apparatus> GetListByAppName(string  AppName);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByAppName(string  AppName);
			
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.Apparatus> GetListByCalculateName(string  CalculateName);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByCalculateName(string  CalculateName);
			
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.Apparatus> GetListByProjectPartID(System.Guid  ProjectPartID);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByProjectPartID(System.Guid  ProjectPartID);
			
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.Apparatus> GetListByAppTypeID(System.Guid  AppTypeID);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByAppTypeID(System.Guid  AppTypeID);
			
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.Apparatus> GetListByX(string  X);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByX(string  X);
			
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.Apparatus> GetListByY(string  Y);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByY(string  Y);
			
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.Apparatus> GetListByZ(string  Z);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByZ(string  Z);
			
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.Apparatus> GetListByBuriedTime(System.DateTime  BuriedTime);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByBuriedTime(System.DateTime  BuriedTime);
			
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.Apparatus> GetListByOtherInfo(string  OtherInfo);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByOtherInfo(string  OtherInfo);
			
		
		
		
		
		
		

		
    }
}



