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
    
    public class  MessureValueBLLBase
    {
		protected readonly IMessureValueDAL dal=DataAccess.CreateMessureValueDAL(); //has cache
		
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool ExistsBy_messureParamID_Date(System.Guid messureParamID,System.DateTime Date)
		{
			return dal.ExistsBy_messureParamID_Date(messureParamID,Date);
		}
		
		/// <summary>
		/// 更新记录
		/// </summary>
		public bool UpdateBy_messureParamID_Date(System.Guid messureParamID,System.DateTime Date, System.Guid newmessureParamID,System.DateTime newDate)
		{
			return dal.UpdateBy_messureParamID_Date(messureParamID,Date,newmessureParamID,newDate);
		}
		
		/// <summary>
		/// 使用事务
		/// </summary>
		public bool UpdateBy_messureParamID_Date(System.Guid messureParamID,System.DateTime Date, System.Guid newmessureParamID,System.DateTime newDate,System.Data.IDbTransaction trans)
		{
			return dal.UpdateBy_messureParamID_Date(messureParamID,Date,newmessureParamID,newDate,trans);
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public hammergo.Model.MessureValue GetModelBy_messureParamID_Date(System.Guid messureParamID,System.DateTime Date)
		{
			
			return dal.GetModelBy_messureParamID_Date(messureParamID,Date);
		}
		
		
		
		
		/// <summary>
		/// 对TrackedList里的更改的对象更新到数据库,如删除的,新增的,更新的
		/// </summary>
        public void UpdateList(TrackedList<hammergo.Model.MessureValue> modeList)
        {
           

            foreach (hammergo.Model.MessureValue mode in modeList.GetDeleted())
            {
                Delete(mode);
            }
            foreach (hammergo.Model.MessureValue mode in modeList.GetUpdated())
            {
                Update(mode);
            }
            foreach (hammergo.Model.MessureValue mode in modeList.GetCreated())
            {
                Add(mode);
            }
			
			 modeList.AcceptChanges();

        }


		
		/// <summary>
		/// 使用事物对TrackedList里的更改的对象更新到数据库,如删除的,新增的,更新的
		/// </summary>
        public void UpdateList(TrackedList<hammergo.Model.MessureValue> modeList ,System.Data.IDbTransaction trans)
        {
           

            foreach (hammergo.Model.MessureValue mode in modeList.GetDeleted())
            {
                Delete(mode,trans);
            }
            foreach (hammergo.Model.MessureValue mode in modeList.GetUpdated())
            {
                Update(mode,trans);
            }
            foreach (hammergo.Model.MessureValue mode in modeList.GetCreated())
            {
                Add(mode,trans);
            }
			
			 modeList.AcceptChanges();

        }	

		

		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(hammergo.Model.MessureValue model)
		{
			return dal.Add(model);
		}
		
		/// <summary>
		/// 使用事务增加一条数据
		/// </summary>
		public bool Add(hammergo.Model.MessureValue model,System.Data.IDbTransaction tb)
		{
			return dal.Add(model,tb);
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(hammergo.Model.MessureValue  model)
		{
			return dal.Update(model);
		}		
		
		/// <summary>
		/// 使用事务更新一条数据
		/// </summary>
		public bool Update(hammergo.Model.MessureValue  model,System.Data.IDbTransaction tb)
		{
			return dal.Update(model,tb);
		}			
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(System.Guid messureParamID,System.DateTime Date)
		{
			
			return dal.Delete(messureParamID ,Date );
		}
		
		/// <summary>
		/// 使用事务通过主键删除一条数据
		/// </summary>
		public bool Delete(System.Guid messureParamID,System.DateTime Date,System.Data.IDbTransaction tb)
		{
			
			return dal.Delete(messureParamID ,Date ,tb);
		}
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(hammergo.Model.MessureValue  model)
		{
			
			return dal.Delete((System.Guid)model.MessureParamID ,(System.DateTime)model.Date );
		}
		
		/// <summary>
		/// 使用事务通过对象删除一条数据
		/// </summary>
		public bool Delete(hammergo.Model.MessureValue  model,System.Data.IDbTransaction tb)
		{
			
			return dal.Delete((System.Guid)model.MessureParamID ,(System.DateTime)model.Date ,tb);
		}
		


		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public TrackedList<hammergo.Model.MessureValue > GetList()
		{
    		return dal.GetList(); 
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.MessureValue> GetListBymessureParamID(System.Guid  MessureParamID)
		{
			return dal.GetListBymessureParamID( MessureParamID);	
		}	
		
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountBymessureParamID(System.Guid  MessureParamID)
		{
			return dal.GetCountBymessureParamID( MessureParamID);	
		}

		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.MessureValue> GetListByDate(System.DateTime  Date)
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
		public TrackedList<hammergo.Model.MessureValue> GetListByVal(double  Val)
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



