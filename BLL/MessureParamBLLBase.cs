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
    
    public class  MessureParamBLLBase
    {
		protected readonly IMessureParamDAL dal=DataAccess.CreateMessureParamDAL(); //has cache
		
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool ExistsBy_appName_ParamName(string appName,string ParamName)
		{
			return dal.ExistsBy_appName_ParamName(appName,ParamName);
		}
		
		/// <summary>
		/// 更新记录
		/// </summary>
		public bool UpdateBy_appName_ParamName(string appName,string ParamName, string newappName,string newParamName)
		{
			return dal.UpdateBy_appName_ParamName(appName,ParamName,newappName,newParamName);
		}
		
		/// <summary>
		/// 使用事务
		/// </summary>
		public bool UpdateBy_appName_ParamName(string appName,string ParamName, string newappName,string newParamName,System.Data.IDbTransaction trans)
		{
			return dal.UpdateBy_appName_ParamName(appName,ParamName,newappName,newParamName,trans);
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public hammergo.Model.MessureParam GetModelBy_appName_ParamName(string appName,string ParamName)
		{
			
			return dal.GetModelBy_appName_ParamName(appName,ParamName);
		}
		
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool ExistsBy_appName_ParamSymbol(string appName,string ParamSymbol)
		{
			return dal.ExistsBy_appName_ParamSymbol(appName,ParamSymbol);
		}
		
		/// <summary>
		/// 更新记录
		/// </summary>
		public bool UpdateBy_appName_ParamSymbol(string appName,string ParamSymbol, string newappName,string newParamSymbol)
		{
			return dal.UpdateBy_appName_ParamSymbol(appName,ParamSymbol,newappName,newParamSymbol);
		}
		
		/// <summary>
		/// 使用事务
		/// </summary>
		public bool UpdateBy_appName_ParamSymbol(string appName,string ParamSymbol, string newappName,string newParamSymbol,System.Data.IDbTransaction trans)
		{
			return dal.UpdateBy_appName_ParamSymbol(appName,ParamSymbol,newappName,newParamSymbol,trans);
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public hammergo.Model.MessureParam GetModelBy_appName_ParamSymbol(string appName,string ParamSymbol)
		{
			
			return dal.GetModelBy_appName_ParamSymbol(appName,ParamSymbol);
		}
		
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool ExistsBy_MessureParamID(System.Guid MessureParamID)
		{
			return dal.ExistsBy_MessureParamID(MessureParamID);
		}
		
		/// <summary>
		/// 更新记录
		/// </summary>
		public bool UpdateBy_MessureParamID(System.Guid MessureParamID, System.Guid newMessureParamID)
		{
			return dal.UpdateBy_MessureParamID(MessureParamID,newMessureParamID);
		}
		
		/// <summary>
		/// 使用事务
		/// </summary>
		public bool UpdateBy_MessureParamID(System.Guid MessureParamID, System.Guid newMessureParamID,System.Data.IDbTransaction trans)
		{
			return dal.UpdateBy_MessureParamID(MessureParamID,newMessureParamID,trans);
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public hammergo.Model.MessureParam GetModelBy_MessureParamID(System.Guid MessureParamID)
		{
			
			return dal.GetModelBy_MessureParamID(MessureParamID);
		}
		
		
		
		
		/// <summary>
		/// 对TrackedList里的更改的对象更新到数据库,如删除的,新增的,更新的
		/// </summary>
        public void UpdateList(TrackedList<hammergo.Model.MessureParam> modeList)
        {
           

            foreach (hammergo.Model.MessureParam mode in modeList.GetDeleted())
            {
                Delete(mode);
            }
            foreach (hammergo.Model.MessureParam mode in modeList.GetUpdated())
            {
                Update(mode);
            }
            foreach (hammergo.Model.MessureParam mode in modeList.GetCreated())
            {
                Add(mode);
            }
			
			 modeList.AcceptChanges();

        }


		
		/// <summary>
		/// 使用事物对TrackedList里的更改的对象更新到数据库,如删除的,新增的,更新的
		/// </summary>
        public void UpdateList(TrackedList<hammergo.Model.MessureParam> modeList ,System.Data.IDbTransaction trans)
        {
           

            foreach (hammergo.Model.MessureParam mode in modeList.GetDeleted())
            {
                Delete(mode,trans);
            }
            foreach (hammergo.Model.MessureParam mode in modeList.GetUpdated())
            {
                Update(mode,trans);
            }
            foreach (hammergo.Model.MessureParam mode in modeList.GetCreated())
            {
                Add(mode,trans);
            }
			
			 modeList.AcceptChanges();

        }	

		

		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(hammergo.Model.MessureParam model)
		{
			return dal.Add(model);
		}
		
		/// <summary>
		/// 使用事务增加一条数据
		/// </summary>
		public bool Add(hammergo.Model.MessureParam model,System.Data.IDbTransaction tb)
		{
			return dal.Add(model,tb);
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(hammergo.Model.MessureParam  model)
		{
			return dal.Update(model);
		}		
		
		/// <summary>
		/// 使用事务更新一条数据
		/// </summary>
		public bool Update(hammergo.Model.MessureParam  model,System.Data.IDbTransaction tb)
		{
			return dal.Update(model,tb);
		}			
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(System.Guid MessureParamID)
		{
			
			return dal.Delete(MessureParamID );
		}
		
		/// <summary>
		/// 使用事务通过主键删除一条数据
		/// </summary>
		public bool Delete(System.Guid MessureParamID,System.Data.IDbTransaction tb)
		{
			
			return dal.Delete(MessureParamID ,tb);
		}
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(hammergo.Model.MessureParam  model)
		{
			
			return dal.Delete((System.Guid)model.MessureParamID );
		}
		
		/// <summary>
		/// 使用事务通过对象删除一条数据
		/// </summary>
		public bool Delete(hammergo.Model.MessureParam  model,System.Data.IDbTransaction tb)
		{
			
			return dal.Delete((System.Guid)model.MessureParamID ,tb);
		}
		


		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public TrackedList<hammergo.Model.MessureParam > GetList()
		{
    		return dal.GetList(); 
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.MessureParam> GetListByMessureParamID(System.Guid  MessureParamID)
		{
			return dal.GetListByMessureParamID( MessureParamID);	
		}	
		
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByMessureParamID(System.Guid  MessureParamID)
		{
			return dal.GetCountByMessureParamID( MessureParamID);	
		}

		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.MessureParam> GetListByappName(string  AppName)
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
		public TrackedList<hammergo.Model.MessureParam> GetListByParamName(string  ParamName)
		{
			return dal.GetListByParamName( ParamName);	
		}	
		
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByParamName(string  ParamName)
		{
			return dal.GetCountByParamName( ParamName);	
		}

		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.MessureParam> GetListByParamSymbol(string  ParamSymbol)
		{
			return dal.GetListByParamSymbol( ParamSymbol);	
		}	
		
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByParamSymbol(string  ParamSymbol)
		{
			return dal.GetCountByParamSymbol( ParamSymbol);	
		}

		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.MessureParam> GetListByUnitSymbol(string  UnitSymbol)
		{
			return dal.GetListByUnitSymbol( UnitSymbol);	
		}	
		
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByUnitSymbol(string  UnitSymbol)
		{
			return dal.GetCountByUnitSymbol( UnitSymbol);	
		}

		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.MessureParam> GetListByPrecisionNum(byte  PrecisionNum)
		{
			return dal.GetListByPrecisionNum( PrecisionNum);	
		}	
		
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByPrecisionNum(byte  PrecisionNum)
		{
			return dal.GetCountByPrecisionNum( PrecisionNum);	
		}

		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.MessureParam> GetListByOrder(byte  Order)
		{
			return dal.GetListByOrder( Order);	
		}	
		
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByOrder(byte  Order)
		{
			return dal.GetCountByOrder( Order);	
		}

		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.MessureParam> GetListByDescription(string  Description)
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
		/// 获取PrecisionNum的最大值
		/// </summary>				
		public byte? GetMaxPrecisionNum()
		{
  			return dal.GetMaxPrecisionNum();
		}
				
				
				
		/// <summary>
		/// 获取Order的最大值
		/// </summary>				
		public byte? GetMaxOrder()
		{
  			return dal.GetMaxOrder();
		}
				
				
		
		
				
		/// <summary>
		/// 获取PrecisionNum的最小值
		/// </summary>				
		public byte? GetMinPrecisionNum()
		{

  			return dal.GetMinPrecisionNum();
		}
				
				
				
		/// <summary>
		/// 获取Order的最小值
		/// </summary>				
		public byte? GetMinOrder()
		{

  			return dal.GetMinOrder();
		}
				
				

		
    }
}



