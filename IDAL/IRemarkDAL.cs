
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
    
    public partial interface IRemarkDAL
    {
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool ExistsBy_appName_Date( string appName,System.DateTime Date );		
		
		/// <summary>
		/// 更新记录的记录
		/// </summary>
		bool UpdateBy_appName_Date( string appName,System.DateTime Date, string newappName,System.DateTime newDate);
		
		/// <summary>
		/// 使用事务更新记录
		/// </summary>
		bool UpdateBy_appName_Date( string appName,System.DateTime Date, string newappName,System.DateTime newDate,System.Data.IDbTransaction trans);
		
				
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		hammergo.Model.Remark GetModelBy_appName_Date(string appName,System.DateTime Date);
		

		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool Add(hammergo.Model.Remark model);
		
		/// <summary>
		/// 使用事务增加一条数据
		/// </summary>
		bool Add(hammergo.Model.Remark model, System.Data.IDbTransaction trans);		
		

		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(hammergo.Model.Remark model);
		
		/// <summary>
		/// 使用事务更新一条数据
		/// </summary>
		bool Update(hammergo.Model.Remark model,System.Data.IDbTransaction trans);		
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(string appName,System.DateTime Date);
		
		/// <summary>
		/// 使用事务删除一条数据
		/// </summary>
		bool Delete(string appName,System.DateTime Date, System.Data.IDbTransaction trans);		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>
		TrackedList<hammergo.Model.Remark> GetList();		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.Remark> GetListByappName(string  AppName);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByappName(string  AppName);
			
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.Remark> GetListByDate(System.DateTime  Date);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByDate(System.DateTime  Date);
			
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.Remark> GetListByRemarkText(string  RemarkText);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByRemarkText(string  RemarkText);
			
		
		
		
		
		
		

		
    }
}



