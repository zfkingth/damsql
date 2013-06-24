using System;

namespace hammergo.caculator
{
	/// <summary>
	/// LinkNode ��ժҪ˵����
	/// 
	/// </summary>
	internal class LinkNode
	{
		/// <summary>
		/// ǰһ���ڵ�
		/// </summary>
		LinkNode preNode=null;


		/// <summary>
		/// ��һ���ڵ�
		/// </summary>
		
		LinkNode nextNode=null;

		Word word;
		/// <summary>
		/// ����һ�����
		/// </summary>
		public LinkNode()
		{
			
		}

		/// <summary>
		/// ����һ����������ͷ��㲻Ϊ��
		/// </summary>
		/// <param name="word"></param>
		public LinkNode(Word word)
		{
			this.word=word;
		}

		

		

		/// <summary>
		/// ǰһ���������
		/// </summary>
		public LinkNode Previous
		{
			get
			{
				return preNode;
			}

			set
			{
				preNode=value;
			}
			
		}

		/// <summary>
		/// ��ȡ����ֵword
		/// </summary>
		/// <returns></returns>
		public Word getWord()
		{
			return word;
		}


		/// <summary>
		/// ��һ����������
		/// </summary>
		public LinkNode Next
		{
			get
			{
				return nextNode;
			}
			set
			{
				nextNode=value;
			}

			
		}

		
		


	}
}
