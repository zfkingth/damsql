
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
    
    public partial interface ISysUserDAL
    {
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool ExistsBy_UserName( string UserName );		
		
		/// <summary>
		/// 更新记录的记录
		/// </summary>
		bool UpdateBy_UserName( string UserName, string newUserName);
		
		/// <summary>
		/// 使用事务更新记录
		/// </summary>
		bool UpdateBy_UserName( string UserName, string newUserName,System.Data.IDbTransaction trans);
		
				
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		hammergo.Model.SysUser GetModelBy_UserName(string UserName);
		

		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool Add(hammergo.Model.SysUser model);
		
		/// <summary>
		/// 使用事务增加一条数据
		/// </summary>
		bool Add(hammergo.Model.SysUser model, System.Data.IDbTransaction trans);		
		

		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(hammergo.Model.SysUser model);
		
		/// <summary>
		/// 使用事务更新一条数据
		/// </summary>
		bool Update(hammergo.Model.SysUser model,System.Data.IDbTransaction trans);		
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(string UserName);
		
		/// <summary>
		/// 使用事务删除一条数据
		/// </summary>
		bool Delete(string UserName, System.Data.IDbTransaction trans);		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>
		TrackedList<hammergo.Model.SysUser> GetList();		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.SysUser> GetListByUserName(string  UserName);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByUserName(string  UserName);
			
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.SysUser> GetListByPasswordHash(string  PasswordHash);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByPasswordHash(string  PasswordHash);
			
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.SysUser> GetListBySalt(string  Salt);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountBySalt(string  Salt);
			
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.SysUser> GetListByQuestion(string  Question);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByQuestion(string  Question);
			
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.SysUser> GetListByAnswer(string  Answer);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByAnswer(string  Answer);
			
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.SysUser> GetListByroleID(int  RoleID);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByroleID(int  RoleID);
			
		
		
		
		
				
		/// <summary>
		/// 获取roleID的最小值
		/// </summary>				
		int? GetMinroleID();

				
				
		
				
		/// <summary>
		/// 获取roleID的最大值
		/// </summary>				
		int? GetMaxroleID();

				
				
		

		
    }
}



