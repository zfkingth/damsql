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
    
    public class  TaskAppratusBLLBase
    {
		protected readonly ITaskAppratusDAL dal=DataAccess.CreateTaskAppratusDAL(); //has cache
		
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool ExistsBy_appCollectionID_appName(System.Guid appCollectionID,string appName)
		{
			return dal.ExistsBy_appCollectionID_appName(appCollectionID,appName);
		}
		
		/// <summary>
		/// 更新记录
		/// </summary>
		public bool UpdateBy_appCollectionID_appName(System.Guid appCollectionID,string appName, System.Guid newappCollectionID,string newappName)
		{
			return dal.UpdateBy_appCollectionID_appName(appCollectionID,appName,newappCollectionID,newappName);
		}
		
		/// <summary>
		/// 使用事务
		/// </summary>
		public bool UpdateBy_appCollectionID_appName(System.Guid appCollectionID,string appName, System.Guid newappCollectionID,string newappName,System.Data.IDbTransaction trans)
		{
			return dal.UpdateBy_appCollectionID_appName(appCollectionID,appName,newappCollectionID,newappName,trans);
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public hammergo.Model.TaskAppratus GetModelBy_appCollectionID_appName(System.Guid appCollectionID,string appName)
		{
			
			return dal.GetModelBy_appCollectionID_appName(appCollectionID,appName);
		}
		
		
		
		
		/// <summary>
		/// 对TrackedList里的更改的对象更新到数据库,如删除的,新增的,更新的
		/// </summary>
        public void UpdateList(TrackedList<hammergo.Model.TaskAppratus> modeList)
        {
           

            foreach (hammergo.Model.TaskAppratus mode in modeList.GetDeleted())
            {
                Delete(mode);
            }
            foreach (hammergo.Model.TaskAppratus mode in modeList.GetUpdated())
            {
                Update(mode);
            }
            foreach (hammergo.Model.TaskAppratus mode in modeList.GetCreated())
            {
                Add(mode);
            }
			
			 modeList.AcceptChanges();

        }


		
		/// <summary>
		/// 使用事物对TrackedList里的更改的对象更新到数据库,如删除的,新增的,更新的
		/// </summary>
        public void UpdateList(TrackedList<hammergo.Model.TaskAppratus> modeList ,System.Data.IDbTransaction trans)
        {
           

            foreach (hammergo.Model.TaskAppratus mode in modeList.GetDeleted())
            {
                Delete(mode,trans);
            }
            foreach (hammergo.Model.TaskAppratus mode in modeList.GetUpdated())
            {
                Update(mode,trans);
            }
            foreach (hammergo.Model.TaskAppratus mode in modeList.GetCreated())
            {
                Add(mode,trans);
            }
			
			 modeList.AcceptChanges();

        }	

		

		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(hammergo.Model.TaskAppratus model)
		{
			return dal.Add(model);
		}
		
		/// <summary>
		/// 使用事务增加一条数据
		/// </summary>
		public bool Add(hammergo.Model.TaskAppratus model,System.Data.IDbTransaction tb)
		{
			return dal.Add(model,tb);
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(hammergo.Model.TaskAppratus  model)
		{
			return dal.Update(model);
		}		
		
		/// <summary>
		/// 使用事务更新一条数据
		/// </summary>
		public bool Update(hammergo.Model.TaskAppratus  model,System.Data.IDbTransaction tb)
		{
			return dal.Update(model,tb);
		}			
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(System.Guid appCollectionID,string appName)
		{
			
			return dal.Delete(appCollectionID ,appName );
		}
		
		/// <summary>
		/// 使用事务通过主键删除一条数据
		/// </summary>
		public bool Delete(System.Guid appCollectionID,string appName,System.Data.IDbTransaction tb)
		{
			
			return dal.Delete(appCollectionID ,appName ,tb);
		}
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(hammergo.Model.TaskAppratus  model)
		{
			
			return dal.Delete((System.Guid)model.AppCollectionID ,(string)model.AppName );
		}
		
		/// <summary>
		/// 使用事务通过对象删除一条数据
		/// </summary>
		public bool Delete(hammergo.Model.TaskAppratus  model,System.Data.IDbTransaction tb)
		{
			
			return dal.Delete((System.Guid)model.AppCollectionID ,(string)model.AppName ,tb);
		}
		


		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public TrackedList<hammergo.Model.TaskAppratus > GetList()
		{
    		return dal.GetList(); 
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.TaskAppratus> GetListByappCollectionID(System.Guid  AppCollectionID)
		{
			return dal.GetListByappCollectionID( AppCollectionID);	
		}	
		
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByappCollectionID(System.Guid  AppCollectionID)
		{
			return dal.GetCountByappCollectionID( AppCollectionID);	
		}

		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.TaskAppratus> GetListByappName(string  AppName)
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
		public TrackedList<hammergo.Model.TaskAppratus> GetListByOrder(int  Order)
		{
			return dal.GetListByOrder( Order);	
		}	
		
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByOrder(int  Order)
		{
			return dal.GetCountByOrder( Order);	
		}

		
		
		
		
				
		/// <summary>
		/// 获取Order的最大值
		/// </summary>				
		public int? GetMaxOrder()
		{
  			return dal.GetMaxOrder();
		}
				
				
		
		
				
		/// <summary>
		/// 获取Order的最小值
		/// </summary>				
		public int? GetMinOrder()
		{

  			return dal.GetMinOrder();
		}
				
				

		
    }
}



