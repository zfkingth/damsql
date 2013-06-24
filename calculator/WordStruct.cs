using System;

namespace hammergo.caculator
{
	/// <summary>
	/// WordStruct ��ժҪ˵����
	/// ���ڱ��浥�ʵ���Ϣ�������������ͺ�ֵ��
	/// </summary>
	internal class Word
	{
		/// <summary>
		/// ��������
		/// </summary>
		public WordType wordType; 

		/// <summary>
		/// ֵ��
		/// </summary>
		public string valueString;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="wordType"></param>
		/// <param name="s"></param>

		public Word(WordType wordType,string s)
		{
			this.wordType=wordType;
			valueString=s;
			
		}

		/// <summary>
		/// ɾ�����ֵ�������
		/// </summary>


		public void removeSymbol()
		{
			if(valueString[0]=='+'||valueString[0]=='-')
			{
//				int i=0;
//				for(;i<valueString.Length-1;i++)
//					valueString[i]=valueString[i+1];
//				valueString[i]='\0';

				valueString.Remove(0,1);

			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public  override string  ToString()
		{
			return string.Format("wordType:{0},valueString:{1}",wordType,valueString);
		}
	}
}
