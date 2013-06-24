using System;
using System.Reflection;
using System.Configuration;
using hammergo.IDAL;
namespace hammergo.DALFactory
{
	/// <summary>
	/// 抽象工厂模式创建DAL。
	/// web.config 需要加入配置：(利用工厂模式+反射机制+缓存机制,实现动态创建不同的数据层对象接口)  
	/// DataCache类在导出代码的文件夹里
	/// <appSettings>  
	/// <add key="DAL" value="hammergo.OleDbDAL" /> (这里的命名空间根据实际情况更改为自己项目的命名空间)
	/// </appSettings> 
	/// </summary>
	public sealed class DataAccess
	{
        private static readonly string AssemblyPath = hammergo.GlobalConfig.PubConstant.ConfigData.DALAssemblyName;
		/// <summary>
		/// 创建对象或从缓存获取
		/// </summary>
		public static object CreateObject(string AssemblyPath,string ClassNamespace)
		{
			object objType = DataCache.GetCache(ClassNamespace);//从缓存读取
			if (objType == null)
			{

					objType = Assembly.Load(AssemblyPath).CreateInstance(ClassNamespace);//反射创建
					DataCache.SetCache(ClassNamespace, objType);// 写入缓存

			}
			return objType;
		}


		/// <summary>
		/// 创建ApparatusDAL数据层接口
		/// </summary>
		public static hammergo.IDAL.IApparatusDAL CreateApparatusDAL()
		{

			string ClassNamespace = AssemblyPath +".ApparatusDAL";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (hammergo.IDAL.IApparatusDAL)objType;
		}

		/// <summary>
		/// 创建ApparatusTypeDAL数据层接口
		/// </summary>
		public static hammergo.IDAL.IApparatusTypeDAL CreateApparatusTypeDAL()
		{

			string ClassNamespace = AssemblyPath +".ApparatusTypeDAL";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (hammergo.IDAL.IApparatusTypeDAL)objType;
		}


		/// <summary>
		/// 创建AppCollectionDAL数据层接口
		/// </summary>
		public static hammergo.IDAL.IAppCollectionDAL CreateAppCollectionDAL()
		{

			string ClassNamespace = AssemblyPath +".AppCollectionDAL";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (hammergo.IDAL.IAppCollectionDAL)objType;
		}


		/// <summary>
		/// 创建CalculateParamDAL数据层接口
		/// </summary>
		public static hammergo.IDAL.ICalculateParamDAL CreateCalculateParamDAL()
		{

			string ClassNamespace = AssemblyPath +".CalculateParamDAL";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (hammergo.IDAL.ICalculateParamDAL)objType;
		}


		/// <summary>
		/// 创建CalculateValueDAL数据层接口
		/// </summary>
		public static hammergo.IDAL.ICalculateValueDAL CreateCalculateValueDAL()
		{

			string ClassNamespace = AssemblyPath +".CalculateValueDAL";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (hammergo.IDAL.ICalculateValueDAL)objType;
		}


		/// <summary>
		/// 创建ConstantParamDAL数据层接口
		/// </summary>
		public static hammergo.IDAL.IConstantParamDAL CreateConstantParamDAL()
		{

			string ClassNamespace = AssemblyPath +".ConstantParamDAL";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (hammergo.IDAL.IConstantParamDAL)objType;
		}







		/// <summary>
		/// 创建MessureParamDAL数据层接口
		/// </summary>
		public static hammergo.IDAL.IMessureParamDAL CreateMessureParamDAL()
		{

			string ClassNamespace = AssemblyPath +".MessureParamDAL";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (hammergo.IDAL.IMessureParamDAL)objType;
		}


		/// <summary>
		/// 创建MessureValueDAL数据层接口
		/// </summary>
		public static hammergo.IDAL.IMessureValueDAL CreateMessureValueDAL()
		{

			string ClassNamespace = AssemblyPath +".MessureValueDAL";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (hammergo.IDAL.IMessureValueDAL)objType;
		}


		/// <summary>
		/// 创建ProjectPartDAL数据层接口
		/// </summary>
		public static hammergo.IDAL.IProjectPartDAL CreateProjectPartDAL()
		{

			string ClassNamespace = AssemblyPath +".ProjectPartDAL";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (hammergo.IDAL.IProjectPartDAL)objType;
		}


		/// <summary>
		/// 创建RemarkDAL数据层接口
		/// </summary>
		public static hammergo.IDAL.IRemarkDAL CreateRemarkDAL()
		{

			string ClassNamespace = AssemblyPath +".RemarkDAL";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (hammergo.IDAL.IRemarkDAL)objType;
		}


		/// <summary>
		/// 创建RoleDAL数据层接口
		/// </summary>
		public static hammergo.IDAL.IRoleDAL CreateRoleDAL()
		{

			string ClassNamespace = AssemblyPath +".RoleDAL";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (hammergo.IDAL.IRoleDAL)objType;
		}


		/// <summary>
		/// 创建TaskAppratusDAL数据层接口
		/// </summary>
		public static hammergo.IDAL.ITaskAppratusDAL CreateTaskAppratusDAL()
		{

			string ClassNamespace = AssemblyPath +".TaskAppratusDAL";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (hammergo.IDAL.ITaskAppratusDAL)objType;
		}


		/// <summary>
		/// 创建TaskTypeDAL数据层接口
		/// </summary>
		public static hammergo.IDAL.ITaskTypeDAL CreateTaskTypeDAL()
		{

			string ClassNamespace = AssemblyPath +".TaskTypeDAL";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (hammergo.IDAL.ITaskTypeDAL)objType;
		}


		/// <summary>
		/// 创建UserDAL数据层接口
		/// </summary>
		public static hammergo.IDAL.ISysUserDAL CreateSysUserDAL()
		{

			string ClassNamespace = AssemblyPath +".SysUserDAL";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (hammergo.IDAL.ISysUserDAL)objType;
		}

        //public static hammergo.IDAL.ITransWrapper CreateTansWrapperNotCache()
        //{
        //    string ClassNamespace = AssemblyPath + ".TransactionWrapper";
        //    object objType = Assembly.Load(AssemblyPath).CreateInstance(ClassNamespace);//反射创建

        //    return (hammergo.IDAL.ITransWrapper)objType;
        //}



}
}