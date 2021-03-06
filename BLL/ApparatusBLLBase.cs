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
    
    public class  ApparatusBLLBase
    {
		protected readonly IApparatusDAL dal=DataAccess.CreateApparatusDAL(); //has cache
		
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool ExistsBy_CalculateName(string CalculateName)
		{
			return dal.ExistsBy_CalculateName(CalculateName);
		}
		
		/// <summary>
		/// 更新记录
		/// </summary>
		public bool UpdateBy_CalculateName(string CalculateName, string newCalculateName)
		{
			return dal.UpdateBy_CalculateName(CalculateName,newCalculateName);
		}
		
		/// <summary>
		/// 使用事务
		/// </summary>
		public bool UpdateBy_CalculateName(string CalculateName, string newCalculateName,System.Data.IDbTransaction trans)
		{
			return dal.UpdateBy_CalculateName(CalculateName,newCalculateName,trans);
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public hammergo.Model.Apparatus GetModelBy_CalculateName(string CalculateName)
		{
			
			return dal.GetModelBy_CalculateName(CalculateName);
		}
		
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool ExistsBy_AppName(string AppName)
		{
			return dal.ExistsBy_AppName(AppName);
		}
		
		/// <summary>
		/// 更新记录
		/// </summary>
		public bool UpdateBy_AppName(string AppName, string newAppName)
		{
			return dal.UpdateBy_AppName(AppName,newAppName);
		}
		
		/// <summary>
		/// 使用事务
		/// </summary>
		public bool UpdateBy_AppName(string AppName, string newAppName,System.Data.IDbTransaction trans)
		{
			return dal.UpdateBy_AppName(AppName,newAppName,trans);
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public hammergo.Model.Apparatus GetModelBy_AppName(string AppName)
		{
			
			return dal.GetModelBy_AppName(AppName);
		}
		
		
		
		
		/// <summary>
		/// 对TrackedList里的更改的对象更新到数据库,如删除的,新增的,更新的
		/// </summary>
        public void UpdateList(TrackedList<hammergo.Model.Apparatus> modeList)
        {
           

            foreach (hammergo.Model.Apparatus mode in modeList.GetDeleted())
            {
                Delete(mode);
            }
            foreach (hammergo.Model.Apparatus mode in modeList.GetUpdated())
            {
                Update(mode);
            }
            foreach (hammergo.Model.Apparatus mode in modeList.GetCreated())
            {
                Add(mode);
            }
			
			 modeList.AcceptChanges();

        }


		
		/// <summary>
		/// 使用事物对TrackedList里的更改的对象更新到数据库,如删除的,新增的,更新的
		/// </summary>
        public void UpdateList(TrackedList<hammergo.Model.Apparatus> modeList ,System.Data.IDbTransaction trans)
        {
           

            foreach (hammergo.Model.Apparatus mode in modeList.GetDeleted())
            {
                Delete(mode,trans);
            }
            foreach (hammergo.Model.Apparatus mode in modeList.GetUpdated())
            {
                Update(mode,trans);
            }
            foreach (hammergo.Model.Apparatus mode in modeList.GetCreated())
            {
                Add(mode,trans);
            }
			
			 modeList.AcceptChanges();

        }	

		

		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(hammergo.Model.Apparatus model)
		{
			return dal.Add(model);
		}
		
		/// <summary>
		/// 使用事务增加一条数据
		/// </summary>
		public bool Add(hammergo.Model.Apparatus model,System.Data.IDbTransaction tb)
		{
			return dal.Add(model,tb);
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(hammergo.Model.Apparatus  model)
		{
			return dal.Update(model);
		}		
		
		/// <summary>
		/// 使用事务更新一条数据
		/// </summary>
		public bool Update(hammergo.Model.Apparatus  model,System.Data.IDbTransaction tb)
		{
			return dal.Update(model,tb);
		}			
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(string AppName)
		{
			
			return dal.Delete(AppName );
		}
		
		/// <summary>
		/// 使用事务通过主键删除一条数据
		/// </summary>
		public bool Delete(string AppName,System.Data.IDbTransaction tb)
		{
			
			return dal.Delete(AppName ,tb);
		}
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(hammergo.Model.Apparatus  model)
		{
			
			return dal.Delete((string)model.AppName );
		}
		
		/// <summary>
		/// 使用事务通过对象删除一条数据
		/// </summary>
		public bool Delete(hammergo.Model.Apparatus  model,System.Data.IDbTransaction tb)
		{
			
			return dal.Delete((string)model.AppName ,tb);
		}
		


		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public TrackedList<hammergo.Model.Apparatus > GetList()
		{
    		return dal.GetList(); 
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.Apparatus> GetListByAppName(string  AppName)
		{
			return dal.GetListByAppName( AppName);	
		}	
		
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByAppName(string  AppName)
		{
			return dal.GetCountByAppName( AppName);	
		}

		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.Apparatus> GetListByCalculateName(string  CalculateName)
		{
			return dal.GetListByCalculateName( CalculateName);	
		}	
		
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByCalculateName(string  CalculateName)
		{
			return dal.GetCountByCalculateName( CalculateName);	
		}

		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.Apparatus> GetListByProjectPartID(System.Guid  ProjectPartID)
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
		public TrackedList<hammergo.Model.Apparatus> GetListByAppTypeID(System.Guid  AppTypeID)
		{
			return dal.GetListByAppTypeID( AppTypeID);	
		}	
		
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByAppTypeID(System.Guid  AppTypeID)
		{
			return dal.GetCountByAppTypeID( AppTypeID);	
		}

		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.Apparatus> GetListByX(string  X)
		{
			return dal.GetListByX( X);	
		}	
		
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByX(string  X)
		{
			return dal.GetCountByX( X);	
		}

		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.Apparatus> GetListByY(string  Y)
		{
			return dal.GetListByY( Y);	
		}	
		
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByY(string  Y)
		{
			return dal.GetCountByY( Y);	
		}

		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.Apparatus> GetListByZ(string  Z)
		{
			return dal.GetListByZ( Z);	
		}	
		
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByZ(string  Z)
		{
			return dal.GetCountByZ( Z);	
		}

		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.Apparatus> GetListByBuriedTime(System.DateTime  BuriedTime)
		{
			return dal.GetListByBuriedTime( BuriedTime);	
		}	
		
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByBuriedTime(System.DateTime  BuriedTime)
		{
			return dal.GetCountByBuriedTime( BuriedTime);	
		}

		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.Apparatus> GetListByOtherInfo(string  OtherInfo)
		{
			return dal.GetListByOtherInfo( OtherInfo);	
		}	
		
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByOtherInfo(string  OtherInfo)
		{
			return dal.GetCountByOtherInfo( OtherInfo);	
		}

		
		
		
		
		
		

		
    }
}



