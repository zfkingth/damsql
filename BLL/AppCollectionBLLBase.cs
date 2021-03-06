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
    
    public class  AppCollectionBLLBase
    {
		protected readonly IAppCollectionDAL dal=DataAccess.CreateAppCollectionDAL(); //has cache
		
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool ExistsBy_CollectionName_taskTypeID(string CollectionName,int taskTypeID)
		{
			return dal.ExistsBy_CollectionName_taskTypeID(CollectionName,taskTypeID);
		}
		
		/// <summary>
		/// 更新记录
		/// </summary>
		public bool UpdateBy_CollectionName_taskTypeID(string CollectionName,int taskTypeID, string newCollectionName,int newtaskTypeID)
		{
			return dal.UpdateBy_CollectionName_taskTypeID(CollectionName,taskTypeID,newCollectionName,newtaskTypeID);
		}
		
		/// <summary>
		/// 使用事务
		/// </summary>
		public bool UpdateBy_CollectionName_taskTypeID(string CollectionName,int taskTypeID, string newCollectionName,int newtaskTypeID,System.Data.IDbTransaction trans)
		{
			return dal.UpdateBy_CollectionName_taskTypeID(CollectionName,taskTypeID,newCollectionName,newtaskTypeID,trans);
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public hammergo.Model.AppCollection GetModelBy_CollectionName_taskTypeID(string CollectionName,int taskTypeID)
		{
			
			return dal.GetModelBy_CollectionName_taskTypeID(CollectionName,taskTypeID);
		}
		
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool ExistsBy_AppCollectionID(System.Guid AppCollectionID)
		{
			return dal.ExistsBy_AppCollectionID(AppCollectionID);
		}
		
		/// <summary>
		/// 更新记录
		/// </summary>
		public bool UpdateBy_AppCollectionID(System.Guid AppCollectionID, System.Guid newAppCollectionID)
		{
			return dal.UpdateBy_AppCollectionID(AppCollectionID,newAppCollectionID);
		}
		
		/// <summary>
		/// 使用事务
		/// </summary>
		public bool UpdateBy_AppCollectionID(System.Guid AppCollectionID, System.Guid newAppCollectionID,System.Data.IDbTransaction trans)
		{
			return dal.UpdateBy_AppCollectionID(AppCollectionID,newAppCollectionID,trans);
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public hammergo.Model.AppCollection GetModelBy_AppCollectionID(System.Guid AppCollectionID)
		{
			
			return dal.GetModelBy_AppCollectionID(AppCollectionID);
		}
		
		
		
		
		/// <summary>
		/// 对TrackedList里的更改的对象更新到数据库,如删除的,新增的,更新的
		/// </summary>
        public void UpdateList(TrackedList<hammergo.Model.AppCollection> modeList)
        {
           

            foreach (hammergo.Model.AppCollection mode in modeList.GetDeleted())
            {
                Delete(mode);
            }
            foreach (hammergo.Model.AppCollection mode in modeList.GetUpdated())
            {
                Update(mode);
            }
            foreach (hammergo.Model.AppCollection mode in modeList.GetCreated())
            {
                Add(mode);
            }
			
			 modeList.AcceptChanges();

        }


		
		/// <summary>
		/// 使用事物对TrackedList里的更改的对象更新到数据库,如删除的,新增的,更新的
		/// </summary>
        public void UpdateList(TrackedList<hammergo.Model.AppCollection> modeList ,System.Data.IDbTransaction trans)
        {
           

            foreach (hammergo.Model.AppCollection mode in modeList.GetDeleted())
            {
                Delete(mode,trans);
            }
            foreach (hammergo.Model.AppCollection mode in modeList.GetUpdated())
            {
                Update(mode,trans);
            }
            foreach (hammergo.Model.AppCollection mode in modeList.GetCreated())
            {
                Add(mode,trans);
            }
			
			 modeList.AcceptChanges();

        }	

		

		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(hammergo.Model.AppCollection model)
		{
			return dal.Add(model);
		}
		
		/// <summary>
		/// 使用事务增加一条数据
		/// </summary>
		public bool Add(hammergo.Model.AppCollection model,System.Data.IDbTransaction tb)
		{
			return dal.Add(model,tb);
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(hammergo.Model.AppCollection  model)
		{
			return dal.Update(model);
		}		
		
		/// <summary>
		/// 使用事务更新一条数据
		/// </summary>
		public bool Update(hammergo.Model.AppCollection  model,System.Data.IDbTransaction tb)
		{
			return dal.Update(model,tb);
		}			
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(System.Guid AppCollectionID)
		{
			
			return dal.Delete(AppCollectionID );
		}
		
		/// <summary>
		/// 使用事务通过主键删除一条数据
		/// </summary>
		public bool Delete(System.Guid AppCollectionID,System.Data.IDbTransaction tb)
		{
			
			return dal.Delete(AppCollectionID ,tb);
		}
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(hammergo.Model.AppCollection  model)
		{
			
			return dal.Delete((System.Guid)model.AppCollectionID );
		}
		
		/// <summary>
		/// 使用事务通过对象删除一条数据
		/// </summary>
		public bool Delete(hammergo.Model.AppCollection  model,System.Data.IDbTransaction tb)
		{
			
			return dal.Delete((System.Guid)model.AppCollectionID ,tb);
		}
		


		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public TrackedList<hammergo.Model.AppCollection > GetList()
		{
    		return dal.GetList(); 
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.AppCollection> GetListByAppCollectionID(System.Guid  AppCollectionID)
		{
			return dal.GetListByAppCollectionID( AppCollectionID);	
		}	
		
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByAppCollectionID(System.Guid  AppCollectionID)
		{
			return dal.GetCountByAppCollectionID( AppCollectionID);	
		}

		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.AppCollection> GetListBytaskTypeID(int  TaskTypeID)
		{
			return dal.GetListBytaskTypeID( TaskTypeID);	
		}	
		
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountBytaskTypeID(int  TaskTypeID)
		{
			return dal.GetCountBytaskTypeID( TaskTypeID);	
		}

		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.AppCollection> GetListByCollectionName(string  CollectionName)
		{
			return dal.GetListByCollectionName( CollectionName);	
		}	
		
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByCollectionName(string  CollectionName)
		{
			return dal.GetCountByCollectionName( CollectionName);	
		}

		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.AppCollection> GetListByDescription(string  Description)
		{
			return dal.GetListByDescription( Description);	
		}	
		
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByDescription(string  Description)
		{
			return dal.GetCountByDescription( Description);	
		}

		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.AppCollection> GetListByOrder(int  Order)
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
		/// 获取taskTypeID的最大值
		/// </summary>				
		public int? GetMaxtaskTypeID()
		{
  			return dal.GetMaxtaskTypeID();
		}
				
				
				
		/// <summary>
		/// 获取Order的最大值
		/// </summary>				
		public int? GetMaxOrder()
		{
  			return dal.GetMaxOrder();
		}
				
				
		
		
				
		/// <summary>
		/// 获取taskTypeID的最小值
		/// </summary>				
		public int? GetMintaskTypeID()
		{

  			return dal.GetMintaskTypeID();
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



