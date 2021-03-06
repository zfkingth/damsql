
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
    
    public partial interface IMessureValueDAL
    {
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool ExistsBy_messureParamID_Date( System.Guid messureParamID,System.DateTime Date );		
		
		/// <summary>
		/// 更新记录的记录
		/// </summary>
		bool UpdateBy_messureParamID_Date( System.Guid messureParamID,System.DateTime Date, System.Guid newmessureParamID,System.DateTime newDate);
		
		/// <summary>
		/// 使用事务更新记录
		/// </summary>
		bool UpdateBy_messureParamID_Date( System.Guid messureParamID,System.DateTime Date, System.Guid newmessureParamID,System.DateTime newDate,System.Data.IDbTransaction trans);
		
				
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		hammergo.Model.MessureValue GetModelBy_messureParamID_Date(System.Guid messureParamID,System.DateTime Date);
		

		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		bool Add(hammergo.Model.MessureValue model);
		
		/// <summary>
		/// 使用事务增加一条数据
		/// </summary>
		bool Add(hammergo.Model.MessureValue model, System.Data.IDbTransaction trans);		
		

		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		bool Update(hammergo.Model.MessureValue model);
		
		/// <summary>
		/// 使用事务更新一条数据
		/// </summary>
		bool Update(hammergo.Model.MessureValue model,System.Data.IDbTransaction trans);		
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(System.Guid messureParamID,System.DateTime Date);
		
		/// <summary>
		/// 使用事务删除一条数据
		/// </summary>
		bool Delete(System.Guid messureParamID,System.DateTime Date, System.Data.IDbTransaction trans);		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>
		TrackedList<hammergo.Model.MessureValue> GetList();		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.MessureValue> GetListBymessureParamID(System.Guid  MessureParamID);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountBymessureParamID(System.Guid  MessureParamID);
			
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.MessureValue> GetListByDate(System.DateTime  Date);
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		int GetCountByDate(System.DateTime  Date);
			
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		TrackedList<hammergo.Model.MessureValue> GetListByVal(double  Val);
		
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



