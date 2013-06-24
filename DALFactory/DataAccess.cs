using System;
using System.Reflection;
using System.Configuration;
using hammergo.IDAL;
namespace hammergo.DALFactory
{
	/// <summary>
	/// ���󹤳�ģʽ����DAL��
	/// web.config ��Ҫ�������ã�(���ù���ģʽ+�������+�������,ʵ�ֶ�̬������ͬ�����ݲ����ӿ�)  
	/// DataCache���ڵ���������ļ�����
	/// <appSettings>  
	/// <add key="DAL" value="hammergo.OleDbDAL" /> (����������ռ����ʵ���������Ϊ�Լ���Ŀ�������ռ�)
	/// </appSettings> 
	/// </summary>
	public sealed class DataAccess
	{
        private static readonly string AssemblyPath = hammergo.GlobalConfig.PubConstant.ConfigData.DALAssemblyName;
		/// <summary>
		/// ���������ӻ����ȡ
		/// </summary>
		public static object CreateObject(string AssemblyPath,string ClassNamespace)
		{
			object objType = DataCache.GetCache(ClassNamespace);//�ӻ����ȡ
			if (objType == null)
			{

					objType = Assembly.Load(AssemblyPath).CreateInstance(ClassNamespace);//���䴴��
					DataCache.SetCache(ClassNamespace, objType);// д�뻺��

			}
			return objType;
		}


		/// <summary>
		/// ����ApparatusDAL���ݲ�ӿ�
		/// </summary>
		public static hammergo.IDAL.IApparatusDAL CreateApparatusDAL()
		{

			string ClassNamespace = AssemblyPath +".ApparatusDAL";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (hammergo.IDAL.IApparatusDAL)objType;
		}

		/// <summary>
		/// ����ApparatusTypeDAL���ݲ�ӿ�
		/// </summary>
		public static hammergo.IDAL.IApparatusTypeDAL CreateApparatusTypeDAL()
		{

			string ClassNamespace = AssemblyPath +".ApparatusTypeDAL";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (hammergo.IDAL.IApparatusTypeDAL)objType;
		}


		/// <summary>
		/// ����AppCollectionDAL���ݲ�ӿ�
		/// </summary>
		public static hammergo.IDAL.IAppCollectionDAL CreateAppCollectionDAL()
		{

			string ClassNamespace = AssemblyPath +".AppCollectionDAL";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (hammergo.IDAL.IAppCollectionDAL)objType;
		}


		/// <summary>
		/// ����CalculateParamDAL���ݲ�ӿ�
		/// </summary>
		public static hammergo.IDAL.ICalculateParamDAL CreateCalculateParamDAL()
		{

			string ClassNamespace = AssemblyPath +".CalculateParamDAL";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (hammergo.IDAL.ICalculateParamDAL)objType;
		}


		/// <summary>
		/// ����CalculateValueDAL���ݲ�ӿ�
		/// </summary>
		public static hammergo.IDAL.ICalculateValueDAL CreateCalculateValueDAL()
		{

			string ClassNamespace = AssemblyPath +".CalculateValueDAL";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (hammergo.IDAL.ICalculateValueDAL)objType;
		}


		/// <summary>
		/// ����ConstantParamDAL���ݲ�ӿ�
		/// </summary>
		public static hammergo.IDAL.IConstantParamDAL CreateConstantParamDAL()
		{

			string ClassNamespace = AssemblyPath +".ConstantParamDAL";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (hammergo.IDAL.IConstantParamDAL)objType;
		}







		/// <summary>
		/// ����MessureParamDAL���ݲ�ӿ�
		/// </summary>
		public static hammergo.IDAL.IMessureParamDAL CreateMessureParamDAL()
		{

			string ClassNamespace = AssemblyPath +".MessureParamDAL";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (hammergo.IDAL.IMessureParamDAL)objType;
		}


		/// <summary>
		/// ����MessureValueDAL���ݲ�ӿ�
		/// </summary>
		public static hammergo.IDAL.IMessureValueDAL CreateMessureValueDAL()
		{

			string ClassNamespace = AssemblyPath +".MessureValueDAL";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (hammergo.IDAL.IMessureValueDAL)objType;
		}


		/// <summary>
		/// ����ProjectPartDAL���ݲ�ӿ�
		/// </summary>
		public static hammergo.IDAL.IProjectPartDAL CreateProjectPartDAL()
		{

			string ClassNamespace = AssemblyPath +".ProjectPartDAL";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (hammergo.IDAL.IProjectPartDAL)objType;
		}


		/// <summary>
		/// ����RemarkDAL���ݲ�ӿ�
		/// </summary>
		public static hammergo.IDAL.IRemarkDAL CreateRemarkDAL()
		{

			string ClassNamespace = AssemblyPath +".RemarkDAL";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (hammergo.IDAL.IRemarkDAL)objType;
		}


		/// <summary>
		/// ����RoleDAL���ݲ�ӿ�
		/// </summary>
		public static hammergo.IDAL.IRoleDAL CreateRoleDAL()
		{

			string ClassNamespace = AssemblyPath +".RoleDAL";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (hammergo.IDAL.IRoleDAL)objType;
		}


		/// <summary>
		/// ����TaskAppratusDAL���ݲ�ӿ�
		/// </summary>
		public static hammergo.IDAL.ITaskAppratusDAL CreateTaskAppratusDAL()
		{

			string ClassNamespace = AssemblyPath +".TaskAppratusDAL";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (hammergo.IDAL.ITaskAppratusDAL)objType;
		}


		/// <summary>
		/// ����TaskTypeDAL���ݲ�ӿ�
		/// </summary>
		public static hammergo.IDAL.ITaskTypeDAL CreateTaskTypeDAL()
		{

			string ClassNamespace = AssemblyPath +".TaskTypeDAL";
			object objType=CreateObject(AssemblyPath,ClassNamespace);
			return (hammergo.IDAL.ITaskTypeDAL)objType;
		}


		/// <summary>
		/// ����UserDAL���ݲ�ӿ�
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
        //    object objType = Assembly.Load(AssemblyPath).CreateInstance(ClassNamespace);//���䴴��

        //    return (hammergo.IDAL.ITransWrapper)objType;
        //}



}
}