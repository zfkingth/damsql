using System;
using System.Collections;
namespace hammergo.caculator
{
	/// <summary>
	/// ��溯����Ϣ����
	/// </summary>

	internal class FunInfo
	{
		private string funName;
		private string description;
		private int paramsCount;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="funName"></param>
		/// <param name="paramsCount"></param>
		/// <param name="description"></param>

		public FunInfo(string funName,int paramsCount,string description)
		{
			this.funName=funName.ToLower();
			this.paramsCount=paramsCount;
			this.description=description;
		}

		/// <summary>
		/// 
		/// </summary>
		public string FunName
		{
			get
			{
				return funName;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public string Description
		{
			get
			{
				return description;
			}
		}


		/// <summary>
		/// 
		/// </summary>
		public int ParamsCount
		{ 
			get
			{
				return paramsCount;
			}
		}
	}


	/// <summary>
	/// 
	/// ���ڱ�����Ҫʵ�ֵ���ѧ��������Ϣ�������������Ͳ�������
	/// </summary>
	internal class FunNames
	{
		private Hashtable funs=new Hashtable(40);
		/// <summary>
		/// 
		/// </summary>
		public FunNames()
		{
			
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		/// <summary>
		/// ����ϣ������Ӻ�����Ϣ
		/// </summary>
		/// <param name="funName"></param>
		/// <param name="paramsCount"></param>
		/// <param name="description"></param>
		/// <returns></returns>
		
		public bool addFun(string funName,int paramsCount,string description)
		{
			string name=funName.ToLower();
			if(funs.ContainsKey(name))//�ú����Ѵ���
				return false;
			funs.Add(name,new FunInfo(name,paramsCount,description));
			return true;
		}



		
		/// <summary>
		/// �жϺ�����ΪfunNamet�ĺ����ڹ�ϣ�����Ƿ����
		/// </summary>
		/// <param name="funName"></param>
		/// <returns></returns>
		public bool funExist(string funName)
		{
			return funs.ContainsKey(funName.ToLower());
		}

		/// <summary>
		/// ���ݺ��������ض�Ӧ�Ĳ����ĸ���
		/// </summary>
		/// <param name="funName"></param>
		/// <returns></returns>
		public FunInfo getFunInfo(string funName)
		{
			return (FunInfo)funs[funName.ToLower()];
		}
	}
}
