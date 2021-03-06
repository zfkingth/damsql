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
    
    public class  SysUserBLLBase
    {
		protected readonly ISysUserDAL dal=DataAccess.CreateSysUserDAL(); //has cache
		
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool ExistsBy_UserName(string UserName)
		{
			return dal.ExistsBy_UserName(UserName);
		}
		
		/// <summary>
		/// 更新记录
		/// </summary>
		public bool UpdateBy_UserName(string UserName, string newUserName)
		{
			return dal.UpdateBy_UserName(UserName,newUserName);
		}
		
		/// <summary>
		/// 使用事务
		/// </summary>
		public bool UpdateBy_UserName(string UserName, string newUserName,System.Data.IDbTransaction trans)
		{
			return dal.UpdateBy_UserName(UserName,newUserName,trans);
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public hammergo.Model.SysUser GetModelBy_UserName(string UserName)
		{
			
			return dal.GetModelBy_UserName(UserName);
		}
		
		
		
		
		/// <summary>
		/// 对TrackedList里的更改的对象更新到数据库,如删除的,新增的,更新的
		/// </summary>
        public void UpdateList(TrackedList<hammergo.Model.SysUser> modeList)
        {
           

            foreach (hammergo.Model.SysUser mode in modeList.GetDeleted())
            {
                Delete(mode);
            }
            foreach (hammergo.Model.SysUser mode in modeList.GetUpdated())
            {
                Update(mode);
            }
            foreach (hammergo.Model.SysUser mode in modeList.GetCreated())
            {
                Add(mode);
            }
			
			 modeList.AcceptChanges();

        }


		
		/// <summary>
		/// 使用事物对TrackedList里的更改的对象更新到数据库,如删除的,新增的,更新的
		/// </summary>
        public void UpdateList(TrackedList<hammergo.Model.SysUser> modeList ,System.Data.IDbTransaction trans)
        {
           

            foreach (hammergo.Model.SysUser mode in modeList.GetDeleted())
            {
                Delete(mode,trans);
            }
            foreach (hammergo.Model.SysUser mode in modeList.GetUpdated())
            {
                Update(mode,trans);
            }
            foreach (hammergo.Model.SysUser mode in modeList.GetCreated())
            {
                Add(mode,trans);
            }
			
			 modeList.AcceptChanges();

        }	

		

		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(hammergo.Model.SysUser model)
		{
			return dal.Add(model);
		}
		
		/// <summary>
		/// 使用事务增加一条数据
		/// </summary>
		public bool Add(hammergo.Model.SysUser model,System.Data.IDbTransaction tb)
		{
			return dal.Add(model,tb);
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(hammergo.Model.SysUser  model)
		{
			return dal.Update(model);
		}		
		
		/// <summary>
		/// 使用事务更新一条数据
		/// </summary>
		public bool Update(hammergo.Model.SysUser  model,System.Data.IDbTransaction tb)
		{
			return dal.Update(model,tb);
		}			
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string UserName)
		{
			
			return dal.Delete(UserName );
		}
		
		/// <summary>
		/// 使用事务通过主键删除一条数据
		/// </summary>
		public bool Delete(string UserName,System.Data.IDbTransaction tb)
		{
			
			return dal.Delete(UserName ,tb);
		}
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(hammergo.Model.SysUser  model)
		{
			
			return dal.Delete((string)model.UserName );
		}
		
		/// <summary>
		/// 使用事务通过对象删除一条数据
		/// </summary>
		public bool Delete(hammergo.Model.SysUser  model,System.Data.IDbTransaction tb)
		{
			
			return dal.Delete((string)model.UserName ,tb);
		}
		


		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public TrackedList<hammergo.Model.SysUser > GetList()
		{
    		return dal.GetList(); 
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.SysUser> GetListByUserName(string  UserName)
		{
			return dal.GetListByUserName( UserName);	
		}	
		
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByUserName(string  UserName)
		{
			return dal.GetCountByUserName( UserName);	
		}

		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.SysUser> GetListByPasswordHash(string  PasswordHash)
		{
			return dal.GetListByPasswordHash( PasswordHash);	
		}	
		
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByPasswordHash(string  PasswordHash)
		{
			return dal.GetCountByPasswordHash( PasswordHash);	
		}

		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.SysUser> GetListBySalt(string  Salt)
		{
			return dal.GetListBySalt( Salt);	
		}	
		
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountBySalt(string  Salt)
		{
			return dal.GetCountBySalt( Salt);	
		}

		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.SysUser> GetListByQuestion(string  Question)
		{
			return dal.GetListByQuestion( Question);	
		}	
		
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByQuestion(string  Question)
		{
			return dal.GetCountByQuestion( Question);	
		}

		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.SysUser> GetListByAnswer(string  Answer)
		{
			return dal.GetListByAnswer( Answer);	
		}	
		
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByAnswer(string  Answer)
		{
			return dal.GetCountByAnswer( Answer);	
		}

		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.SysUser> GetListByroleID(int  RoleID)
		{
			return dal.GetListByroleID( RoleID);	
		}	
		
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByroleID(int  RoleID)
		{
			return dal.GetCountByroleID( RoleID);	
		}

		
		
		
		
				
		/// <summary>
		/// 获取roleID的最大值
		/// </summary>				
		public int? GetMaxroleID()
		{
  			return dal.GetMaxroleID();
		}
				
				
		
		
				
		/// <summary>
		/// 获取roleID的最小值
		/// </summary>				
		public int? GetMinroleID()
		{

  			return dal.GetMinroleID();
		}
				
				

		
    }
}



