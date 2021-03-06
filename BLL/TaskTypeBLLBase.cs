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
    
    public class  TaskTypeBLLBase
    {
		protected readonly ITaskTypeDAL dal=DataAccess.CreateTaskTypeDAL(); //has cache
		
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool ExistsBy_TypeName(string TypeName)
		{
			return dal.ExistsBy_TypeName(TypeName);
		}
		
		/// <summary>
		/// 更新记录
		/// </summary>
		public bool UpdateBy_TypeName(string TypeName, string newTypeName)
		{
			return dal.UpdateBy_TypeName(TypeName,newTypeName);
		}
		
		/// <summary>
		/// 使用事务
		/// </summary>
		public bool UpdateBy_TypeName(string TypeName, string newTypeName,System.Data.IDbTransaction trans)
		{
			return dal.UpdateBy_TypeName(TypeName,newTypeName,trans);
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public hammergo.Model.TaskType GetModelBy_TypeName(string TypeName)
		{
			
			return dal.GetModelBy_TypeName(TypeName);
		}
		
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool ExistsBy_TaskTypeID(int TaskTypeID)
		{
			return dal.ExistsBy_TaskTypeID(TaskTypeID);
		}
		
		/// <summary>
		/// 更新记录
		/// </summary>
		public bool UpdateBy_TaskTypeID(int TaskTypeID, int newTaskTypeID)
		{
			return dal.UpdateBy_TaskTypeID(TaskTypeID,newTaskTypeID);
		}
		
		/// <summary>
		/// 使用事务
		/// </summary>
		public bool UpdateBy_TaskTypeID(int TaskTypeID, int newTaskTypeID,System.Data.IDbTransaction trans)
		{
			return dal.UpdateBy_TaskTypeID(TaskTypeID,newTaskTypeID,trans);
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public hammergo.Model.TaskType GetModelBy_TaskTypeID(int TaskTypeID)
		{
			
			return dal.GetModelBy_TaskTypeID(TaskTypeID);
		}
		
		
		
		
		/// <summary>
		/// 对TrackedList里的更改的对象更新到数据库,如删除的,新增的,更新的
		/// </summary>
        public void UpdateList(TrackedList<hammergo.Model.TaskType> modeList)
        {
           

            foreach (hammergo.Model.TaskType mode in modeList.GetDeleted())
            {
                Delete(mode);
            }
            foreach (hammergo.Model.TaskType mode in modeList.GetUpdated())
            {
                Update(mode);
            }
            foreach (hammergo.Model.TaskType mode in modeList.GetCreated())
            {
                Add(mode);
            }
			
			 modeList.AcceptChanges();

        }


		
		/// <summary>
		/// 使用事物对TrackedList里的更改的对象更新到数据库,如删除的,新增的,更新的
		/// </summary>
        public void UpdateList(TrackedList<hammergo.Model.TaskType> modeList ,System.Data.IDbTransaction trans)
        {
           

            foreach (hammergo.Model.TaskType mode in modeList.GetDeleted())
            {
                Delete(mode,trans);
            }
            foreach (hammergo.Model.TaskType mode in modeList.GetUpdated())
            {
                Update(mode,trans);
            }
            foreach (hammergo.Model.TaskType mode in modeList.GetCreated())
            {
                Add(mode,trans);
            }
			
			 modeList.AcceptChanges();

        }	

		

		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(hammergo.Model.TaskType model)
		{
			return dal.Add(model);
		}
		
		/// <summary>
		/// 使用事务增加一条数据
		/// </summary>
		public bool Add(hammergo.Model.TaskType model,System.Data.IDbTransaction tb)
		{
			return dal.Add(model,tb);
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(hammergo.Model.TaskType  model)
		{
			return dal.Update(model);
		}		
		
		/// <summary>
		/// 使用事务更新一条数据
		/// </summary>
		public bool Update(hammergo.Model.TaskType  model,System.Data.IDbTransaction tb)
		{
			return dal.Update(model,tb);
		}			
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int TaskTypeID)
		{
			
			return dal.Delete(TaskTypeID );
		}
		
		/// <summary>
		/// 使用事务通过主键删除一条数据
		/// </summary>
		public bool Delete(int TaskTypeID,System.Data.IDbTransaction tb)
		{
			
			return dal.Delete(TaskTypeID ,tb);
		}
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(hammergo.Model.TaskType  model)
		{
			
			return dal.Delete((int)model.TaskTypeID );
		}
		
		/// <summary>
		/// 使用事务通过对象删除一条数据
		/// </summary>
		public bool Delete(hammergo.Model.TaskType  model,System.Data.IDbTransaction tb)
		{
			
			return dal.Delete((int)model.TaskTypeID ,tb);
		}
		


		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public TrackedList<hammergo.Model.TaskType > GetList()
		{
    		return dal.GetList(); 
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.TaskType> GetListByTaskTypeID(int  TaskTypeID)
		{
			return dal.GetListByTaskTypeID( TaskTypeID);	
		}	
		
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByTaskTypeID(int  TaskTypeID)
		{
			return dal.GetCountByTaskTypeID( TaskTypeID);	
		}

		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.TaskType> GetListByTypeName(string  TypeName)
		{
			return dal.GetListByTypeName( TypeName);	
		}	
		
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByTypeName(string  TypeName)
		{
			return dal.GetCountByTypeName( TypeName);	
		}

		
		
		
		
				
		/// <summary>
		/// 获取TaskTypeID的最大值
		/// </summary>				
		public int? GetMaxTaskTypeID()
		{
  			return dal.GetMaxTaskTypeID();
		}
				
				
		
		
				
		/// <summary>
		/// 获取TaskTypeID的最小值
		/// </summary>				
		public int? GetMinTaskTypeID()
		{

  			return dal.GetMinTaskTypeID();
		}
				
				

		
    }
}



