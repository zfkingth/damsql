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
    
    public class  ApparatusTypeBLLBase
    {
		protected readonly IApparatusTypeDAL dal=DataAccess.CreateApparatusTypeDAL(); //has cache
		
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool ExistsBy_ApparatusTypeID(System.Guid ApparatusTypeID)
		{
			return dal.ExistsBy_ApparatusTypeID(ApparatusTypeID);
		}
		
		/// <summary>
		/// 更新记录
		/// </summary>
		public bool UpdateBy_ApparatusTypeID(System.Guid ApparatusTypeID, System.Guid newApparatusTypeID)
		{
			return dal.UpdateBy_ApparatusTypeID(ApparatusTypeID,newApparatusTypeID);
		}
		
		/// <summary>
		/// 使用事务
		/// </summary>
		public bool UpdateBy_ApparatusTypeID(System.Guid ApparatusTypeID, System.Guid newApparatusTypeID,System.Data.IDbTransaction trans)
		{
			return dal.UpdateBy_ApparatusTypeID(ApparatusTypeID,newApparatusTypeID,trans);
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public hammergo.Model.ApparatusType GetModelBy_ApparatusTypeID(System.Guid ApparatusTypeID)
		{
			
			return dal.GetModelBy_ApparatusTypeID(ApparatusTypeID);
		}
		
		
		
		
		/// <summary>
		/// 对TrackedList里的更改的对象更新到数据库,如删除的,新增的,更新的
		/// </summary>
        public void UpdateList(TrackedList<hammergo.Model.ApparatusType> modeList)
        {
           

            foreach (hammergo.Model.ApparatusType mode in modeList.GetDeleted())
            {
                Delete(mode);
            }
            foreach (hammergo.Model.ApparatusType mode in modeList.GetUpdated())
            {
                Update(mode);
            }
            foreach (hammergo.Model.ApparatusType mode in modeList.GetCreated())
            {
                Add(mode);
            }
			
			 modeList.AcceptChanges();

        }


		
		/// <summary>
		/// 使用事物对TrackedList里的更改的对象更新到数据库,如删除的,新增的,更新的
		/// </summary>
        public void UpdateList(TrackedList<hammergo.Model.ApparatusType> modeList ,System.Data.IDbTransaction trans)
        {
           

            foreach (hammergo.Model.ApparatusType mode in modeList.GetDeleted())
            {
                Delete(mode,trans);
            }
            foreach (hammergo.Model.ApparatusType mode in modeList.GetUpdated())
            {
                Update(mode,trans);
            }
            foreach (hammergo.Model.ApparatusType mode in modeList.GetCreated())
            {
                Add(mode,trans);
            }
			
			 modeList.AcceptChanges();

        }	

		

		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(hammergo.Model.ApparatusType model)
		{
			return dal.Add(model);
		}
		
		/// <summary>
		/// 使用事务增加一条数据
		/// </summary>
		public bool Add(hammergo.Model.ApparatusType model,System.Data.IDbTransaction tb)
		{
			return dal.Add(model,tb);
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(hammergo.Model.ApparatusType  model)
		{
			return dal.Update(model);
		}		
		
		/// <summary>
		/// 使用事务更新一条数据
		/// </summary>
		public bool Update(hammergo.Model.ApparatusType  model,System.Data.IDbTransaction tb)
		{
			return dal.Update(model,tb);
		}			
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(System.Guid ApparatusTypeID)
		{
			
			return dal.Delete(ApparatusTypeID );
		}
		
		/// <summary>
		/// 使用事务通过主键删除一条数据
		/// </summary>
		public bool Delete(System.Guid ApparatusTypeID,System.Data.IDbTransaction tb)
		{
			
			return dal.Delete(ApparatusTypeID ,tb);
		}
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(hammergo.Model.ApparatusType  model)
		{
			
			return dal.Delete((System.Guid)model.ApparatusTypeID );
		}
		
		/// <summary>
		/// 使用事务通过对象删除一条数据
		/// </summary>
		public bool Delete(hammergo.Model.ApparatusType  model,System.Data.IDbTransaction tb)
		{
			
			return dal.Delete((System.Guid)model.ApparatusTypeID ,tb);
		}
		


		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public TrackedList<hammergo.Model.ApparatusType > GetList()
		{
    		return dal.GetList(); 
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.ApparatusType> GetListByApparatusTypeID(System.Guid  ApparatusTypeID)
		{
			return dal.GetListByApparatusTypeID( ApparatusTypeID);	
		}	
		
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByApparatusTypeID(System.Guid  ApparatusTypeID)
		{
			return dal.GetCountByApparatusTypeID( ApparatusTypeID);	
		}

		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.ApparatusType> GetListByTypeName(string  TypeName)
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

		
		
		
		
		
		

		
    }
}



