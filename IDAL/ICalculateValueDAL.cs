
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
    
    public partial interface ICalculateValueDAL
    {
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool ExistsBy_calculateParamID_Date( System.Guid calculateParamID,System.DateTime Date );		
		
		/// <summary>
		/// 更新记录的记录
		/// </summary>
		bool UpdateBy_calculateParamID_Date( System.Guid calculateParamID,System.DateTime Date, System.Guid newcalculateParamID,System.DateTime newDate);
		
		/// <summary>
		/// 使用事务更新记录
		/// </summary>
		bool UpdateBy_calculateParamID_Date( System.Guid calculateParamID,System.DateTime Date, System.Guid newcalculateParamID,System.DateTime newDate,System.Data.IDbTransaction trans);
		
				
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		hammergo.Model.CalculateValue GetModelBy_calculateParamID_Date(System.Guid calculateParamID,System.DateTime Date);
		

		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool Add(hammergo.Model.CalculateValue model);
		
		/// <summary>
		/// 使用事务增加一条数据
		/// </summary>
		bool Add(hammergo.Model.CalculateValue model, System.Data.IDbTransaction trans);		
		

		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(hammergo.Model.CalculateValue model);
		
		/// <summary>
		/// 使用事务更新一条数据
		/// </summary>
		bool Update(hammergo.Model.CalculateValue model,System.Data.IDbTransaction trans);		
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(System.Guid calculateParamID,System.DateTime Date);
		
		/// <summary>
		/// 使用事务删除一条数据
		/// </summary>
		bool Delete(System.Guid calculateParamID,System.DateTime Date, System.Data.IDbTransaction trans);		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>
		TrackedList<hammergo.Model.CalculateValue> GetList();		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.CalculateValue> GetListBycalculateParamID(System.Guid  CalculateParamID);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountBycalculateParamID(System.Guid  CalculateParamID);
			
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.CalculateValue> GetListByDate(System.DateTime  Date);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByDate(System.DateTime  Date);
			
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.CalculateValue> GetListByVal(double  Val);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByVal(double  Val);
			
		
		
		
		
				
		/// <summary>
		/// 获取Val的最小值
		/// </summary>				
		double? GetMinVal();

				
				
		
				
		/// <summary>
		/// 获取Val的最大值
		/// </summary>				
		double? GetMaxVal();

				
				
		

		
    }
}



