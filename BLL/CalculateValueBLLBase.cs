 /**////////////////////////////////////////////////////////////////////////////////////////
 // Description: BLL class .
 // ---------------------
 // Copyright  2009 hammergo@163.com
 // ---------------------
 // History
 //    2013年3月6日 22:29:14    zfking    
 /**////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Data;
using System.Collections.Generic;
using hammergo.Model;
using hammergo.DALFactory;
using hammergo.IDAL;
using hammergo.Tracking;


namespace hammergo.BLL
{
	/// <summary>
	/// 业务逻辑类的摘要说明。
	/// </summary>
    
    public class  CalculateValueBLLBase
    {
		protected readonly ICalculateValueDAL dal=DataAccess.CreateCalculateValueDAL(); //has cache
		
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool ExistsBy_calculateParamID_Date(System.Guid calculateParamID,System.DateTime Date)
		{
			return dal.ExistsBy_calculateParamID_Date(calculateParamID,Date);
		}
		
		/// <summary>
		/// 更新记录
		/// </summary>
		public bool UpdateBy_calculateParamID_Date(System.Guid calculateParamID,System.DateTime Date, System.Guid newcalculateParamID,System.DateTime newDate)
		{
			return dal.UpdateBy_calculateParamID_Date(calculateParamID,Date,newcalculateParamID,newDate);
		}
		
		/// <summary>
		/// 使用事务
		/// </summary>
		public bool UpdateBy_calculateParamID_Date(System.Guid calculateParamID,System.DateTime Date, System.Guid newcalculateParamID,System.DateTime newDate,System.Data.IDbTransaction trans)
		{
			return dal.UpdateBy_calculateParamID_Date(calculateParamID,Date,newcalculateParamID,newDate,trans);
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public hammergo.Model.CalculateValue GetModelBy_calculateParamID_Date(System.Guid calculateParamID,System.DateTime Date)
		{
			
			return dal.GetModelBy_calculateParamID_Date(calculateParamID,Date);
		}
		
		
		
		
		/// <summary>
		/// 对TrackedList里的更改的对象更新到数据库,如删除的,新增的,更新的
		/// </summary>
        public void UpdateList(TrackedList<hammergo.Model.CalculateValue> modeList)
        {
           

            foreach (hammergo.Model.CalculateValue mode in modeList.GetDeleted())
            {
                Delete(mode);
            }
            foreach (hammergo.Model.CalculateValue mode in modeList.GetUpdated())
            {
                Update(mode);
            }
            foreach (hammergo.Model.CalculateValue mode in modeList.GetCreated())
            {
                Add(mode);
            }
			
			 modeList.AcceptChanges();

        }


		
		/// <summary>
		/// 使用事物对TrackedList里的更改的对象更新到数据库,如删除的,新增的,更新的
		/// </summary>
        public void UpdateList(TrackedList<hammergo.Model.CalculateValue> modeList ,System.Data.IDbTransaction trans)
        {
           

            foreach (hammergo.Model.CalculateValue mode in modeList.GetDeleted())
            {
                Delete(mode,trans);
            }
            foreach (hammergo.Model.CalculateValue mode in modeList.GetUpdated())
            {
                Update(mode,trans);
            }
            foreach (hammergo.Model.CalculateValue mode in modeList.GetCreated())
            {
                Add(mode,trans);
            }
			
			 modeList.AcceptChanges();

        }	

		

		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(hammergo.Model.CalculateValue model)
		{
			return dal.Add(model);
		}
		
		/// <summary>
		/// 使用事务增加一条数据
		/// </summary>
		public bool Add(hammergo.Model.CalculateValue model,System.Data.IDbTransaction tb)
		{
			return dal.Add(model,tb);
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(hammergo.Model.CalculateValue  model)
		{
			return dal.Update(model);
		}		
		
		/// <summary>
		/// 使用事务更新一条数据
		/// </summary>
		public bool Update(hammergo.Model.CalculateValue  model,System.Data.IDbTransaction tb)
		{
			return dal.Update(model,tb);
		}			
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(System.Guid calculateParamID,System.DateTime Date)
		{
			
			return dal.Delete(calculateParamID ,Date );
		}
		
		/// <summary>
		/// 使用事务通过主键删除一条数据
		/// </summary>
		public bool Delete(System.Guid calculateParamID,System.DateTime Date,System.Data.IDbTransaction tb)
		{
			
			return dal.Delete(calculateParamID ,Date ,tb);
		}
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(hammergo.Model.CalculateValue  model)
		{
			
			return dal.Delete((System.Guid)model.CalculateParamID ,(System.DateTime)model.Date );
		}
		
		/// <summary>
		/// 使用事务通过对象删除一条数据
		/// </summary>
		public bool Delete(hammergo.Model.CalculateValue  model,System.Data.IDbTransaction tb)
		{
			
			return dal.Delete((System.Guid)model.CalculateParamID ,(System.DateTime)model.Date ,tb);
		}
		


		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public TrackedList<hammergo.Model.CalculateValue > GetList()
		{
    		return dal.GetList(); 
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.CalculateValue> GetListBycalculateParamID(System.Guid  CalculateParamID)
		{
			return dal.GetListBycalculateParamID( CalculateParamID);	
		}	
		
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountBycalculateParamID(System.Guid  CalculateParamID)
		{
			return dal.GetCountBycalculateParamID( CalculateParamID);	
		}

		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.CalculateValue> GetListByDate(System.DateTime  Date)
		{
			return dal.GetListByDate( Date);	
		}	
		
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByDate(System.DateTime  Date)
		{
			return dal.GetCountByDate( Date);	
		}

		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.CalculateValue> GetListByVal(double  Val)
		{
			return dal.GetListByVal( Val);	
		}	
		
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByVal(double  Val)
		{
			return dal.GetCountByVal( Val);	
		}

		
		
		
		
				
		/// <summary>
		/// 获取Val的最大值
		/// </summary>				
		public double? GetMaxVal()
		{
  			return dal.GetMaxVal();
		}
				
				
		
		
				
		/// <summary>
		/// 获取Val的最小值
		/// </summary>				
		public double? GetMinVal()
		{

  			return dal.GetMinVal();
		}
				
				

		
    }
}



