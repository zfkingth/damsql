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
    
    public class  ConstantParamBLLBase
    {
		protected readonly IConstantParamDAL dal=DataAccess.CreateConstantParamDAL(); //has cache
		
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
		public hammergo.Model.ConstantParam GetModelBy_appName_ParamName(string appName,string ParamName)
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
		public hammergo.Model.ConstantParam GetModelBy_appName_ParamSymbol(string appName,string ParamSymbol)
		{
			
			return dal.GetModelBy_appName_ParamSymbol(appName,ParamSymbol);
		}
		
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool ExistsBy_ConstantParamID(System.Guid ConstantParamID)
		{
			return dal.ExistsBy_ConstantParamID(ConstantParamID);
		}
		
		/// <summary>
		/// 更新记录
		/// </summary>
		public bool UpdateBy_ConstantParamID(System.Guid ConstantParamID, System.Guid newConstantParamID)
		{
			return dal.UpdateBy_ConstantParamID(ConstantParamID,newConstantParamID);
		}
		
		/// <summary>
		/// 使用事务
		/// </summary>
		public bool UpdateBy_ConstantParamID(System.Guid ConstantParamID, System.Guid newConstantParamID,System.Data.IDbTransaction trans)
		{
			return dal.UpdateBy_ConstantParamID(ConstantParamID,newConstantParamID,trans);
		}
		
		/// <summary>
		/// 得到一个对象实体
		/// </summary>
		public hammergo.Model.ConstantParam GetModelBy_ConstantParamID(System.Guid ConstantParamID)
		{
			
			return dal.GetModelBy_ConstantParamID(ConstantParamID);
		}
		
		
		
		
		/// <summary>
		/// 对TrackedList里的更改的对象更新到数据库,如删除的,新增的,更新的
		/// </summary>
        public void UpdateList(TrackedList<hammergo.Model.ConstantParam> modeList)
        {
           

            foreach (hammergo.Model.ConstantParam mode in modeList.GetDeleted())
            {
                Delete(mode);
            }
            foreach (hammergo.Model.ConstantParam mode in modeList.GetUpdated())
            {
                Update(mode);
            }
            foreach (hammergo.Model.ConstantParam mode in modeList.GetCreated())
            {
                Add(mode);
            }
			
			 modeList.AcceptChanges();

        }


		
		/// <summary>
		/// 使用事物对TrackedList里的更改的对象更新到数据库,如删除的,新增的,更新的
		/// </summary>
        public void UpdateList(TrackedList<hammergo.Model.ConstantParam> modeList ,System.Data.IDbTransaction trans)
        {
           

            foreach (hammergo.Model.ConstantParam mode in modeList.GetDeleted())
            {
                Delete(mode,trans);
            }
            foreach (hammergo.Model.ConstantParam mode in modeList.GetUpdated())
            {
                Update(mode,trans);
            }
            foreach (hammergo.Model.ConstantParam mode in modeList.GetCreated())
            {
                Add(mode,trans);
            }
			
			 modeList.AcceptChanges();

        }	

		

		
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public bool Add(hammergo.Model.ConstantParam model)
		{
			return dal.Add(model);
		}
		
		/// <summary>
		/// 使用事务增加一条数据
		/// </summary>
		public bool Add(hammergo.Model.ConstantParam model,System.Data.IDbTransaction tb)
		{
			return dal.Add(model,tb);
		}
		
		
		/// <summary>
		/// 更新一条数据
		/// </summary>
		public bool Update(hammergo.Model.ConstantParam  model)
		{
			return dal.Update(model);
		}		
		
		/// <summary>
		/// 使用事务更新一条数据
		/// </summary>
		public bool Update(hammergo.Model.ConstantParam  model,System.Data.IDbTransaction tb)
		{
			return dal.Update(model,tb);
		}			
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(System.Guid ConstantParamID)
		{
			
			return dal.Delete(ConstantParamID );
		}
		
		/// <summary>
		/// 使用事务通过主键删除一条数据
		/// </summary>
		public bool Delete(System.Guid ConstantParamID,System.Data.IDbTransaction tb)
		{
			
			return dal.Delete(ConstantParamID ,tb);
		}
		
		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(hammergo.Model.ConstantParam  model)
		{
			
			return dal.Delete((System.Guid)model.ConstantParamID );
		}
		
		/// <summary>
		/// 使用事务通过对象删除一条数据
		/// </summary>
		public bool Delete(hammergo.Model.ConstantParam  model,System.Data.IDbTransaction tb)
		{
			
			return dal.Delete((System.Guid)model.ConstantParamID ,tb);
		}
		


		
		/// <summary>
		/// 获得数据列表
		/// </summary>
		public TrackedList<hammergo.Model.ConstantParam > GetList()
		{
    		return dal.GetList(); 
		}
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.ConstantParam> GetListByConstantParamID(System.Guid  ConstantParamID)
		{
			return dal.GetListByConstantParamID( ConstantParamID);	
		}	
		
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByConstantParamID(System.Guid  ConstantParamID)
		{
			return dal.GetCountByConstantParamID( ConstantParamID);	
		}

		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.ConstantParam> GetListByappName(string  AppName)
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
		public TrackedList<hammergo.Model.ConstantParam> GetListByParamName(string  ParamName)
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
		public TrackedList<hammergo.Model.ConstantParam> GetListByParamSymbol(string  ParamSymbol)
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
		public TrackedList<hammergo.Model.ConstantParam> GetListByUnitSymbol(string  UnitSymbol)
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
		public TrackedList<hammergo.Model.ConstantParam> GetListByPrecisionNum(byte  PrecisionNum)
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
		public TrackedList<hammergo.Model.ConstantParam> GetListByOrder(byte  Order)
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
		public TrackedList<hammergo.Model.ConstantParam> GetListByVal(double  Val)
		{
			return dal.GetListByVal( Val);	
		}	
		
		
		/// <summary>
		/// 获得对象个数
		/// </summary>		
		public int GetCountByVal(double  Val)
		{
			return dal.GetCountByVal( Val);	
		}

		
		
		/// <summary>
		/// 获得对象实体列表
		/// </summary>		
		public TrackedList<hammergo.Model.ConstantParam> GetListByDescription(string  Description)
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
		/// 获取Val的最大值
		/// </summary>				
		public double? GetMaxVal()
		{
  			return dal.GetMaxVal();
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
				
				
				
		/// <summary>
		/// 获取Val的最小值
		/// </summary>				
		public double? GetMinVal()
		{

  			return dal.GetMinVal();
		}
				
				

		
    }
}



