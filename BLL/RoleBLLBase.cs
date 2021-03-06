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
    
    public class  RoleBLLBase
    {
		protected readonly IRoleDAL dal=DataAccess.CreateRoleDAL(); //has cache
		
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool ExistsBy_RoleID(int RoleID)
		{
			return dal.ExistsBy_RoleID(RoleID);
		}
		
		/// <summary>
		/// 更新记录
		/// </summary>
		public bool UpdateBy_RoleID(int RoleID, int newRoleID)
		{
			return dal.UpdateBy_RoleID(RoleID,newRoleID);
		}
		
		/// <summary>
		/// 使用事务
		/// </summary>
		public bool UpdateBy_RoleID(int RoleID, int newRoleID,System.Data.IDbTransaction trans)
		{
			return dal.UpdateBy_RoleID(RoleID,newRoleID,trans);
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public hammergo.Model.Role GetModelBy_RoleID(int RoleID)
		{
			
			return dal.GetModelBy_RoleID(RoleID);
		}
		
		
		
		
		/// <summary>
		/// 对TrackedList里的更改的对象更新到数据库,如删除的,新增的,更新的
		/// </summary>
        public void UpdateList(TrackedList<hammergo.Model.Role> modeList)
        {
           

            foreach (hammergo.Model.Role mode in modeList.GetDeleted())
            {
                Delete(mode);
            }
            foreach (hammergo.Model.Role mode in modeList.GetUpdated())
            {
                Update(mode);
            }
            foreach (hammergo.Model.Role mode in modeList.GetCreated())
            {
                Add(mode);
            }
			
			 modeList.AcceptChanges();

        }


		
		/// <summary>
		/// 使用事物对TrackedList里的更改的对象更新到数据库,如删除的,新增的,更新的
		/// </summary>
        public void UpdateList(TrackedList<hammergo.Model.Role> modeList ,System.Data.IDbTransaction trans)
        {
           

            foreach (hammergo.Model.Role mode in modeList.GetDeleted())
            {
                Delete(mode,trans);
            }
            foreach (hammergo.Model.Role mode in modeList.GetUpdated())
            {
                Update(mode,trans);
            }
            foreach (hammergo.Model.Role mode in modeList.GetCreated())
            {
                Add(mode,trans);
            }
			
			 modeList.AcceptChanges();

        }	

		

		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(hammergo.Model.Role model)
		{
			return dal.Add(model);
		}
		
		/// <summary>
		/// 使用事务增加一条数据
		/// </summary>
		public bool Add(hammergo.Model.Role model,System.Data.IDbTransaction tb)
		{
			return dal.Add(model,tb);
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(hammergo.Model.Role  model)
		{
			return dal.Update(model);
		}		
		
		/// <summary>
		/// 使用事务更新一条数据
		/// </summary>
		public bool Update(hammergo.Model.Role  model,System.Data.IDbTransaction tb)
		{
			return dal.Update(model,tb);
		}			
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(int RoleID)
		{
			
			return dal.Delete(RoleID );
		}
		
		/// <summary>
		/// 使用事务通过主键删除一条数据
		/// </summary>
		public bool Delete(int RoleID,System.Data.IDbTransaction tb)
		{
			
			return dal.Delete(RoleID ,tb);
		}
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(hammergo.Model.Role  model)
		{
			
			return dal.Delete((int)model.RoleID );
		}
		
		/// <summary>
		/// 使用事务通过对象删除一条数据
		/// </summary>
		public bool Delete(hammergo.Model.Role  model,System.Data.IDbTransaction tb)
		{
			
			return dal.Delete((int)model.RoleID ,tb);
		}
		


		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public TrackedList<hammergo.Model.Role > GetList()
		{
    		return dal.GetList(); 
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.Role> GetListByRoleID(int  RoleID)
		{
			return dal.GetListByRoleID( RoleID);	
		}	
		
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByRoleID(int  RoleID)
		{
			return dal.GetCountByRoleID( RoleID);	
		}

		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.Role> GetListByRoleName(string  RoleName)
		{
			return dal.GetListByRoleName( RoleName);	
		}	
		
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByRoleName(string  RoleName)
		{
			return dal.GetCountByRoleName( RoleName);	
		}

		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.Role> GetListByDescription(string  Description)
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
		public TrackedList<hammergo.Model.Role> GetListByPower(byte  Power)
		{
			return dal.GetListByPower( Power);	
		}	
		
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByPower(byte  Power)
		{
			return dal.GetCountByPower( Power);	
		}

		
		
		
		
				
		/// <summary>
		/// 获取RoleID的最大值
		/// </summary>				
		public int? GetMaxRoleID()
		{
  			return dal.GetMaxRoleID();
		}
				
				
				
		/// <summary>
		/// 获取Power的最大值
		/// </summary>				
		public byte? GetMaxPower()
		{
  			return dal.GetMaxPower();
		}
				
				
		
		
				
		/// <summary>
		/// 获取RoleID的最小值
		/// </summary>				
		public int? GetMinRoleID()
		{

  			return dal.GetMinRoleID();
		}
				
				
				
		/// <summary>
		/// 获取Power的最小值
		/// </summary>				
		public byte? GetMinPower()
		{

  			return dal.GetMinPower();
		}
				
				

		
    }
}



