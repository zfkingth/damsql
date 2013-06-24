using System;
using System.Runtime.InteropServices; 
using System.Reflection; 
using System.Runtime.CompilerServices;

namespace hammergo.caculator
{
	/// <summary>
	/// MyList ��ժҪ˵����
	/// </summary>
	/// 

//	public interface IMyList
//	{
//
//		void add(string key,double val);
//		 void clear();
//
//		double this[string key]
//		{
//			get;
//			set;
//		}
//
//	}
//
//
//	[ClassInterface(ClassInterfaceType.None)] 
	public class MyList //:IMyList
	{
		/// <summary>
		/// ��ʼ��С
		/// </summary>
		const int initialSize=5;
//		/// <summary>
//		/// ������С
//		/// </summary>
//		const int plus=10;
		/// <summary>
		/// �������
		/// </summary>
		int maxSize;
		/// <summary>
		/// ��ʾ�����п�Ԫ�ص���ʼ���꣬Ҳ��ʾ��������ӵ��Ԫ�ص�ʵ������
		/// </summary>
		int currentIndex=0;
		/// <summary>
		/// ������
		/// </summary>
		string [] keys;
		/// <summary>
		/// ֵ����
		/// </summary>
		double [] values;

		/// <summary>
		/// ���캯��
		/// </summary>

		public MyList()
		{
			keys=new string [initialSize];
			values=new double[initialSize];
			maxSize=initialSize;
		}

		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="initialSize">��ĳ�ʼ��С</param>
		public MyList(int initialSize)
		{
			keys=new string [initialSize];
			values=new double[initialSize];
			maxSize=initialSize;

		}


		/// <summary>
		/// ��Ӽ�ֵ��,���key���Ѵ��ڣ���ôֻ���޸�����Ӧ��ֵ
		/// </summary>
		/// <param name="key"></param>
		/// <param name="val"></param>

		public void add(string key,double val)
		{

			for(int i=0;i<currentIndex;i++)
			{
				if(key==keys[i])
				{
					values[i]=val;
					return;
				}
			}

			if(currentIndex>=maxSize)
			{
				double[] values2=new double[maxSize*2];
				string [] keys2=new string[maxSize*2];

				values.CopyTo(values2,0);

				keys.CopyTo(keys2,0);

				
				maxSize*=2;

				values=values2;
				keys=keys2;

			}

			keys[currentIndex]=key;
			values[currentIndex]=val;

			currentIndex++;
		}


		/// <summary>
		/// ������ޱ��Ԫ��s
		/// </summary>
		public void clear()
		{
			currentIndex=0;
		}


		/// <summary>
		/// ���ݼ�������ֵ
		/// </summary>
		public double this[string key]
		{
			get
			{
				for(int i=0;i<currentIndex;i++)
				{
					if(key==keys[i])
						return values[i];
				}

				throw new Exception("����"+key+"�ڲ����б��в�����");

			}

			set
			{
				this.add(key,value);
			}
		}


		/// <summary>
		/// ��ȡ���ַ���
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		public string getKey(int index)
		{
			if(index<this.Length)
			{
				return keys[index];
			}
			else
			{
				throw new Exception("��MyList��������ʵ�ʴ�С");
			}

		}


		/// <summary>
		/// ��ȡ���ʵ�ʴ�С
		/// </summary>

		public int Length
		{
			get
			{
				return currentIndex;
			}
		}
		}
}
