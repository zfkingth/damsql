
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
    
    public partial interface ITaskTypeDAL
    {
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool ExistsBy_TypeName( string TypeName );		
		
		/// <summary>
		/// 更新记录的记录
		/// </summary>
		bool UpdateBy_TypeName( string TypeName, string newTypeName);
		
		/// <summary>
		/// 使用事务更新记录
		/// </summary>
		bool UpdateBy_TypeName( string TypeName, string newTypeName,System.Data.IDbTransaction trans);
		
				
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		hammergo.Model.TaskType GetModelBy_TypeName(string TypeName);
		
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool ExistsBy_TaskTypeID( int TaskTypeID );		
		
		/// <summary>
		/// 更新记录的记录
		/// </summary>
		bool UpdateBy_TaskTypeID( int TaskTypeID, int newTaskTypeID);
		
		/// <summary>
		/// 使用事务更新记录
		/// </summary>
		bool UpdateBy_TaskTypeID( int TaskTypeID, int newTaskTypeID,System.Data.IDbTransaction trans);
		
				
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		hammergo.Model.TaskType GetModelBy_TaskTypeID(int TaskTypeID);
		

		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool Add(hammergo.Model.TaskType model);
		
		/// <summary>
		/// 使用事务增加一条数据
		/// </summary>
		bool Add(hammergo.Model.TaskType model, System.Data.IDbTransaction trans);		
		

		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(hammergo.Model.TaskType model);
		
		/// <summary>
		/// 使用事务更新一条数据
		/// </summary>
		bool Update(hammergo.Model.TaskType model,System.Data.IDbTransaction trans);		
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(int TaskTypeID);
		
		/// <summary>
		/// 使用事务删除一条数据
		/// </summary>
		bool Delete(int TaskTypeID, System.Data.IDbTransaction trans);		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>
		TrackedList<hammergo.Model.TaskType> GetList();		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.TaskType> GetListByTaskTypeID(int  TaskTypeID);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByTaskTypeID(int  TaskTypeID);
			
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.TaskType> GetListByTypeName(string  TypeName);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByTypeName(string  TypeName);
			
		
		
		
		
				
		/// <summary>
		/// 获取TaskTypeID的最小值
		/// </summary>				
		int? GetMinTaskTypeID();

				
				
		
				
		/// <summary>
		/// 获取TaskTypeID的最大值
		/// </summary>				
		int? GetMaxTaskTypeID();

				
				
		

		
    }
}



