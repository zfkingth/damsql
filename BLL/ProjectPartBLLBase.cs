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
    
    public class  ProjectPartBLLBase
    {
		protected readonly IProjectPartDAL dal=DataAccess.CreateProjectPartDAL(); //has cache
		
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool ExistsBy_ProjectPartID(System.Guid ProjectPartID)
		{
			return dal.ExistsBy_ProjectPartID(ProjectPartID);
		}
		
		/// <summary>
		/// 更新记录
		/// </summary>
		public bool UpdateBy_ProjectPartID(System.Guid ProjectPartID, System.Guid newProjectPartID)
		{
			return dal.UpdateBy_ProjectPartID(ProjectPartID,newProjectPartID);
		}
		
		/// <summary>
		/// 使用事务
		/// </summary>
		public bool UpdateBy_ProjectPartID(System.Guid ProjectPartID, System.Guid newProjectPartID,System.Data.IDbTransaction trans)
		{
			return dal.UpdateBy_ProjectPartID(ProjectPartID,newProjectPartID,trans);
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public hammergo.Model.ProjectPart GetModelBy_ProjectPartID(System.Guid ProjectPartID)
		{
			
			return dal.GetModelBy_ProjectPartID(ProjectPartID);
		}
		
		
		
		
		/// <summary>
		/// 对TrackedList里的更改的对象更新到数据库,如删除的,新增的,更新的
		/// </summary>
        public void UpdateList(TrackedList<hammergo.Model.ProjectPart> modeList)
        {
           

            foreach (hammergo.Model.ProjectPart mode in modeList.GetDeleted())
            {
                Delete(mode);
            }
            foreach (hammergo.Model.ProjectPart mode in modeList.GetUpdated())
            {
                Update(mode);
            }
            foreach (hammergo.Model.ProjectPart mode in modeList.GetCreated())
            {
                Add(mode);
            }
			
			 modeList.AcceptChanges();

        }


		
		/// <summary>
		/// 使用事物对TrackedList里的更改的对象更新到数据库,如删除的,新增的,更新的
		/// </summary>
        public void UpdateList(TrackedList<hammergo.Model.ProjectPart> modeList ,System.Data.IDbTransaction trans)
        {
           

            foreach (hammergo.Model.ProjectPart mode in modeList.GetDeleted())
            {
                Delete(mode,trans);
            }
            foreach (hammergo.Model.ProjectPart mode in modeList.GetUpdated())
            {
                Update(mode,trans);
            }
            foreach (hammergo.Model.ProjectPart mode in modeList.GetCreated())
            {
                Add(mode,trans);
            }
			
			 modeList.AcceptChanges();

        }	

		

		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(hammergo.Model.ProjectPart model)
		{
			return dal.Add(model);
		}
		
		/// <summary>
		/// 使用事务增加一条数据
		/// </summary>
		public bool Add(hammergo.Model.ProjectPart model,System.Data.IDbTransaction tb)
		{
			return dal.Add(model,tb);
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(hammergo.Model.ProjectPart  model)
		{
			return dal.Update(model);
		}		
		
		/// <summary>
		/// 使用事务更新一条数据
		/// </summary>
		public bool Update(hammergo.Model.ProjectPart  model,System.Data.IDbTransaction tb)
		{
			return dal.Update(model,tb);
		}			
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(System.Guid ProjectPartID)
		{
			
			return dal.Delete(ProjectPartID );
		}
		
		/// <summary>
		/// 使用事务通过主键删除一条数据
		/// </summary>
		public bool Delete(System.Guid ProjectPartID,System.Data.IDbTransaction tb)
		{
			
			return dal.Delete(ProjectPartID ,tb);
		}
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(hammergo.Model.ProjectPart  model)
		{
			
			return dal.Delete((System.Guid)model.ProjectPartID );
		}
		
		/// <summary>
		/// 使用事务通过对象删除一条数据
		/// </summary>
		public bool Delete(hammergo.Model.ProjectPart  model,System.Data.IDbTransaction tb)
		{
			
			return dal.Delete((System.Guid)model.ProjectPartID ,tb);
		}
		


		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public TrackedList<hammergo.Model.ProjectPart > GetList()
		{
    		return dal.GetList(); 
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.ProjectPart> GetListByProjectPartID(System.Guid  ProjectPartID)
		{
			return dal.GetListByProjectPartID( ProjectPartID);	
		}	
		
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByProjectPartID(System.Guid  ProjectPartID)
		{
			return dal.GetCountByProjectPartID( ProjectPartID);	
		}

		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.ProjectPart> GetListByPartName(string  PartName)
		{
			return dal.GetListByPartName( PartName);	
		}	
		
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByPartName(string  PartName)
		{
			return dal.GetCountByPartName( PartName);	
		}

		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.ProjectPart> GetListByParentPart(System.Guid  ParentPart)
		{
			return dal.GetListByParentPart( ParentPart);	
		}	
		
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByParentPart(System.Guid  ParentPart)
		{
			return dal.GetCountByParentPart( ParentPart);	
		}

		
		
		
		
		
		

		
    }
}



