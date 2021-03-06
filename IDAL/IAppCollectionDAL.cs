
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
    
    public partial interface IAppCollectionDAL
    {
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool ExistsBy_CollectionName_taskTypeID( string CollectionName,int taskTypeID );		
		
		/// <summary>
		/// 更新记录的记录
		/// </summary>
		bool UpdateBy_CollectionName_taskTypeID( string CollectionName,int taskTypeID, string newCollectionName,int newtaskTypeID);
		
		/// <summary>
		/// 使用事务更新记录
		/// </summary>
		bool UpdateBy_CollectionName_taskTypeID( string CollectionName,int taskTypeID, string newCollectionName,int newtaskTypeID,System.Data.IDbTransaction trans);
		
				
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		hammergo.Model.AppCollection GetModelBy_CollectionName_taskTypeID(string CollectionName,int taskTypeID);
		
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool ExistsBy_AppCollectionID( System.Guid AppCollectionID );		
		
		/// <summary>
		/// 更新记录的记录
		/// </summary>
		bool UpdateBy_AppCollectionID( System.Guid AppCollectionID, System.Guid newAppCollectionID);
		
		/// <summary>
		/// 使用事务更新记录
		/// </summary>
		bool UpdateBy_AppCollectionID( System.Guid AppCollectionID, System.Guid newAppCollectionID,System.Data.IDbTransaction trans);
		
				
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		hammergo.Model.AppCollection GetModelBy_AppCollectionID(System.Guid AppCollectionID);
		

		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool Add(hammergo.Model.AppCollection model);
		
		/// <summary>
		/// 使用事务增加一条数据
		/// </summary>
		bool Add(hammergo.Model.AppCollection model, System.Data.IDbTransaction trans);		
		

		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(hammergo.Model.AppCollection model);
		
		/// <summary>
		/// 使用事务更新一条数据
		/// </summary>
		bool Update(hammergo.Model.AppCollection model,System.Data.IDbTransaction trans);		
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(System.Guid AppCollectionID);
		
		/// <summary>
		/// 使用事务删除一条数据
		/// </summary>
		bool Delete(System.Guid AppCollectionID, System.Data.IDbTransaction trans);		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>
		TrackedList<hammergo.Model.AppCollection> GetList();		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.AppCollection> GetListByAppCollectionID(System.Guid  AppCollectionID);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByAppCollectionID(System.Guid  AppCollectionID);
			
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.AppCollection> GetListBytaskTypeID(int  TaskTypeID);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountBytaskTypeID(int  TaskTypeID);
			
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.AppCollection> GetListByCollectionName(string  CollectionName);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByCollectionName(string  CollectionName);
			
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.AppCollection> GetListByDescription(string  Description);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByDescription(string  Description);
			
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.AppCollection> GetListByOrder(int  Order);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByOrder(int  Order);
			
		
		
		
		
				
		/// <summary>
		/// 获取taskTypeID的最小值
		/// </summary>				
		int? GetMintaskTypeID();

				
				
				
		/// <summary>
		/// 获取Order的最小值
		/// </summary>				
		int? GetMinOrder();

				
				
		
				
		/// <summary>
		/// 获取taskTypeID的最大值
		/// </summary>				
		int? GetMaxtaskTypeID();

				
				
				
		/// <summary>
		/// 获取Order的最大值
		/// </summary>				
		int? GetMaxOrder();

				
				
		

		
    }
}



