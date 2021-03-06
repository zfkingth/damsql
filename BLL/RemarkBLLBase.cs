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
    
    public class  RemarkBLLBase
    {
		protected readonly IRemarkDAL dal=DataAccess.CreateRemarkDAL(); //has cache
		
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool ExistsBy_appName_Date(string appName,System.DateTime Date)
		{
			return dal.ExistsBy_appName_Date(appName,Date);
		}
		
		/// <summary>
		/// 更新记录
		/// </summary>
		public bool UpdateBy_appName_Date(string appName,System.DateTime Date, string newappName,System.DateTime newDate)
		{
			return dal.UpdateBy_appName_Date(appName,Date,newappName,newDate);
		}
		
		/// <summary>
		/// 使用事务
		/// </summary>
		public bool UpdateBy_appName_Date(string appName,System.DateTime Date, string newappName,System.DateTime newDate,System.Data.IDbTransaction trans)
		{
			return dal.UpdateBy_appName_Date(appName,Date,newappName,newDate,trans);
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public hammergo.Model.Remark GetModelBy_appName_Date(string appName,System.DateTime Date)
		{
			
			return dal.GetModelBy_appName_Date(appName,Date);
		}
		
		
		
		
		/// <summary>
		/// 对TrackedList里的更改的对象更新到数据库,如删除的,新增的,更新的
		/// </summary>
        public void UpdateList(TrackedList<hammergo.Model.Remark> modeList)
        {
           

            foreach (hammergo.Model.Remark mode in modeList.GetDeleted())
            {
                Delete(mode);
            }
            foreach (hammergo.Model.Remark mode in modeList.GetUpdated())
            {
                Update(mode);
            }
            foreach (hammergo.Model.Remark mode in modeList.GetCreated())
            {
                Add(mode);
            }
			
			 modeList.AcceptChanges();

        }


		
		/// <summary>
		/// 使用事物对TrackedList里的更改的对象更新到数据库,如删除的,新增的,更新的
		/// </summary>
        public void UpdateList(TrackedList<hammergo.Model.Remark> modeList ,System.Data.IDbTransaction trans)
        {
           

            foreach (hammergo.Model.Remark mode in modeList.GetDeleted())
            {
                Delete(mode,trans);
            }
            foreach (hammergo.Model.Remark mode in modeList.GetUpdated())
            {
                Update(mode,trans);
            }
            foreach (hammergo.Model.Remark mode in modeList.GetCreated())
            {
                Add(mode,trans);
            }
			
			 modeList.AcceptChanges();

        }	

		

		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(hammergo.Model.Remark model)
		{
			return dal.Add(model);
		}
		
		/// <summary>
		/// 使用事务增加一条数据
		/// </summary>
		public bool Add(hammergo.Model.Remark model,System.Data.IDbTransaction tb)
		{
			return dal.Add(model,tb);
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(hammergo.Model.Remark  model)
		{
			return dal.Update(model);
		}		
		
		/// <summary>
		/// 使用事务更新一条数据
		/// </summary>
		public bool Update(hammergo.Model.Remark  model,System.Data.IDbTransaction tb)
		{
			return dal.Update(model,tb);
		}			
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string appName,System.DateTime Date)
		{
			
			return dal.Delete(appName ,Date );
		}
		
		/// <summary>
		/// 使用事务通过主键删除一条数据
		/// </summary>
		public bool Delete(string appName,System.DateTime Date,System.Data.IDbTransaction tb)
		{
			
			return dal.Delete(appName ,Date ,tb);
		}
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(hammergo.Model.Remark  model)
		{
			
			return dal.Delete((string)model.AppName ,(System.DateTime)model.Date );
		}
		
		/// <summary>
		/// 使用事务通过对象删除一条数据
		/// </summary>
		public bool Delete(hammergo.Model.Remark  model,System.Data.IDbTransaction tb)
		{
			
			return dal.Delete((string)model.AppName ,(System.DateTime)model.Date ,tb);
		}
		


		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public TrackedList<hammergo.Model.Remark > GetList()
		{
    		return dal.GetList(); 
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.Remark> GetListByappName(string  AppName)
		{
			return dal.GetListByappName( AppName);	
		}	
		
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByappName(string  AppName)
		{
			return dal.GetCountByappName( AppName);	
		}

		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.Remark> GetListByDate(System.DateTime  Date)
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
		public TrackedList<hammergo.Model.Remark> GetListByRemarkText(string  RemarkText)
		{
			return dal.GetListByRemarkText( RemarkText);	
		}	
		
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByRemarkText(string  RemarkText)
		{
			return dal.GetCountByRemarkText( RemarkText);	
		}

		
		
		
		
		
		

		
    }
}



