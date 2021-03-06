
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
    
    public partial interface IProjectPartDAL
    {
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool ExistsBy_ProjectPartID( System.Guid ProjectPartID );		
		
		/// <summary>
		/// 更新记录的记录
		/// </summary>
		bool UpdateBy_ProjectPartID( System.Guid ProjectPartID, System.Guid newProjectPartID);
		
		/// <summary>
		/// 使用事务更新记录
		/// </summary>
		bool UpdateBy_ProjectPartID( System.Guid ProjectPartID, System.Guid newProjectPartID,System.Data.IDbTransaction trans);
		
				
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		hammergo.Model.ProjectPart GetModelBy_ProjectPartID(System.Guid ProjectPartID);
		

		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool Add(hammergo.Model.ProjectPart model);
		
		/// <summary>
		/// 使用事务增加一条数据
		/// </summary>
		bool Add(hammergo.Model.ProjectPart model, System.Data.IDbTransaction trans);		
		

		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(hammergo.Model.ProjectPart model);
		
		/// <summary>
		/// 使用事务更新一条数据
		/// </summary>
		bool Update(hammergo.Model.ProjectPart model,System.Data.IDbTransaction trans);		
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(System.Guid ProjectPartID);
		
		/// <summary>
		/// 使用事务删除一条数据
		/// </summary>
		bool Delete(System.Guid ProjectPartID, System.Data.IDbTransaction trans);		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>
		TrackedList<hammergo.Model.ProjectPart> GetList();		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.ProjectPart> GetListByProjectPartID(System.Guid  ProjectPartID);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByProjectPartID(System.Guid  ProjectPartID);
			
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.ProjectPart> GetListByPartName(string  PartName);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByPartName(string  PartName);
			
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.ProjectPart> GetListByParentPart(System.Guid  ParentPart);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByParentPart(System.Guid  ParentPart);
			
		
		
		
		
		
		

		
    }
}



