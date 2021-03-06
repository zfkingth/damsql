
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
    
    public partial interface IRoleDAL
    {
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool ExistsBy_RoleID( int RoleID );		
		
		/// <summary>
		/// 更新记录的记录
		/// </summary>
		bool UpdateBy_RoleID( int RoleID, int newRoleID);
		
		/// <summary>
		/// 使用事务更新记录
		/// </summary>
		bool UpdateBy_RoleID( int RoleID, int newRoleID,System.Data.IDbTransaction trans);
		
				
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		hammergo.Model.Role GetModelBy_RoleID(int RoleID);
		

		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool Add(hammergo.Model.Role model);
		
		/// <summary>
		/// 使用事务增加一条数据
		/// </summary>
		bool Add(hammergo.Model.Role model, System.Data.IDbTransaction trans);		
		

		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(hammergo.Model.Role model);
		
		/// <summary>
		/// 使用事务更新一条数据
		/// </summary>
		bool Update(hammergo.Model.Role model,System.Data.IDbTransaction trans);		
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(int RoleID);
		
		/// <summary>
		/// 使用事务删除一条数据
		/// </summary>
		bool Delete(int RoleID, System.Data.IDbTransaction trans);		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>
		TrackedList<hammergo.Model.Role> GetList();		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.Role> GetListByRoleID(int  RoleID);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByRoleID(int  RoleID);
			
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.Role> GetListByRoleName(string  RoleName);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByRoleName(string  RoleName);
			
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.Role> GetListByDescription(string  Description);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByDescription(string  Description);
			
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.Role> GetListByPower(byte  Power);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByPower(byte  Power);
			
		
		
		
		
				
		/// <summary>
		/// 获取RoleID的最小值
		/// </summary>				
		int? GetMinRoleID();

				
				
				
		/// <summary>
		/// 获取Power的最小值
		/// </summary>				
		byte? GetMinPower();

				
				
		
				
		/// <summary>
		/// 获取RoleID的最大值
		/// </summary>				
		int? GetMaxRoleID();

				
				
				
		/// <summary>
		/// 获取Power的最大值
		/// </summary>				
		byte? GetMaxPower();

				
				
		

		
    }
}



