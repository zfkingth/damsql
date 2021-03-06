
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
    
    public partial interface IApparatusTypeDAL
    {
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool ExistsBy_ApparatusTypeID( System.Guid ApparatusTypeID );		
		
		/// <summary>
		/// 更新记录的记录
		/// </summary>
		bool UpdateBy_ApparatusTypeID( System.Guid ApparatusTypeID, System.Guid newApparatusTypeID);
		
		/// <summary>
		/// 使用事务更新记录
		/// </summary>
		bool UpdateBy_ApparatusTypeID( System.Guid ApparatusTypeID, System.Guid newApparatusTypeID,System.Data.IDbTransaction trans);
		
				
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		hammergo.Model.ApparatusType GetModelBy_ApparatusTypeID(System.Guid ApparatusTypeID);
		

		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool Add(hammergo.Model.ApparatusType model);
		
		/// <summary>
		/// 使用事务增加一条数据
		/// </summary>
		bool Add(hammergo.Model.ApparatusType model, System.Data.IDbTransaction trans);		
		

		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(hammergo.Model.ApparatusType model);
		
		/// <summary>
		/// 使用事务更新一条数据
		/// </summary>
		bool Update(hammergo.Model.ApparatusType model,System.Data.IDbTransaction trans);		
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(System.Guid ApparatusTypeID);
		
		/// <summary>
		/// 使用事务删除一条数据
		/// </summary>
		bool Delete(System.Guid ApparatusTypeID, System.Data.IDbTransaction trans);		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>
		TrackedList<hammergo.Model.ApparatusType> GetList();		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.ApparatusType> GetListByApparatusTypeID(System.Guid  ApparatusTypeID);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByApparatusTypeID(System.Guid  ApparatusTypeID);
			
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.ApparatusType> GetListByTypeName(string  TypeName);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByTypeName(string  TypeName);
			
		
		
		
		
		
		

		
    }
}



