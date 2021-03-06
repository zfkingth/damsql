
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
    
    public partial interface ITaskAppratusDAL
    {
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool ExistsBy_appCollectionID_appName( System.Guid appCollectionID,string appName );		
		
		/// <summary>
		/// 更新记录的记录
		/// </summary>
		bool UpdateBy_appCollectionID_appName( System.Guid appCollectionID,string appName, System.Guid newappCollectionID,string newappName);
		
		/// <summary>
		/// 使用事务更新记录
		/// </summary>
		bool UpdateBy_appCollectionID_appName( System.Guid appCollectionID,string appName, System.Guid newappCollectionID,string newappName,System.Data.IDbTransaction trans);
		
				
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		hammergo.Model.TaskAppratus GetModelBy_appCollectionID_appName(System.Guid appCollectionID,string appName);
		

		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool Add(hammergo.Model.TaskAppratus model);
		
		/// <summary>
		/// 使用事务增加一条数据
		/// </summary>
		bool Add(hammergo.Model.TaskAppratus model, System.Data.IDbTransaction trans);		
		

		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(hammergo.Model.TaskAppratus model);
		
		/// <summary>
		/// 使用事务更新一条数据
		/// </summary>
		bool Update(hammergo.Model.TaskAppratus model,System.Data.IDbTransaction trans);		
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(System.Guid appCollectionID,string appName);
		
		/// <summary>
		/// 使用事务删除一条数据
		/// </summary>
		bool Delete(System.Guid appCollectionID,string appName, System.Data.IDbTransaction trans);		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>
		TrackedList<hammergo.Model.TaskAppratus> GetList();		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.TaskAppratus> GetListByappCollectionID(System.Guid  AppCollectionID);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByappCollectionID(System.Guid  AppCollectionID);
			
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.TaskAppratus> GetListByappName(string  AppName);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByappName(string  AppName);
			
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.TaskAppratus> GetListByOrder(int  Order);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByOrder(int  Order);
			
		
		
		
		
				
		/// <summary>
		/// 获取Order的最小值
		/// </summary>				
		int? GetMinOrder();

				
				
		
				
		/// <summary>
		/// 获取Order的最大值
		/// </summary>				
		int? GetMaxOrder();

				
				
		

		
    }
}



