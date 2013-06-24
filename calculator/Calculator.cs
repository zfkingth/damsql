using System;
using System.Collections;
namespace hammergo.caculator
{
	/// <summary>
	/// Calculator ��ժҪ˵����
	/// </summary>
	internal class Calculator
	{
		
		
		Stack stack=new Stack(16);

		LinkList suffixList;//��ź�׺���ʽ

		LinkList list; //��żǷ������Ľ��

		
		const int opeCount=6;
		/// <summary>
		/// ���������ȼ��Ƚϱ�
		/// </summary>
		 public static readonly int [,] priorities=new int[opeCount,opeCount];


		/// <summary>
		/// 
		/// </summary>
		static Calculator()
		{
			//priorities=new int[opeCount,opeCount];

			for(int i=0;i<opeCount;i++)
				for(int j=0;j<opeCount;j++)
				{
					priorities[i,j]=0;
				}

			for(int i=0;i<opeCount-1;i++)
			{
				priorities[i,0]=1;
				priorities[i,1]=1;
			}

			priorities[2,2]=1;
			priorities[2,3]=1;
			priorities[3,2]=1;
			priorities[3,3]=1;

			
			priorities[4,0]=1;
			priorities[4,1]=1;
			priorities[4,2]=1;
			priorities[4,3]=1;
			priorities[4,4]=1;

			
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}


		public Calculator()
		{
		}


		/// <summary>
		/// ������Ҫ���������
		/// </summary>

		public LinkList List
		{
			get
			{
				return list;
			}
			set
			{
				list=value;
			}
		}

	

		public double compute(string s,AbstractScan scan)
		{
			//�Ƿ�ɨ����
//			Scan scan=new Scan();

			if(s.Trim().Length==0)
			{
				throw new Exception("���ʽΪ��");
			}
			list=scan.parse(s);
			
			 createSuffixExpression();
			return reckon();
		}

		/// <summary>
		/// ������׺���ʽ��ֵ
		/// </summary>
		/// <param name="list"></param>
		/// <returns></returns>


		public double compute(LinkList list)
		{
			this.list=list;
			createSuffixExpression();
			return reckon();
		}

		

		/// <summary>
		/// ɨ��list������׺���ʽ
		/// </summary>

		internal LinkList createSuffixExpression()
		{
			if(list.Count==0)
				throw new Exception("���ʽԪ�ظ���Ϊ��");
			 suffixList=new LinkList();
			stack.Clear();
			LinkNode node=list.First;

			while(node!=null)
			{
				LinkNode next=node.Next;;
				switch(node.getWord().wordType)
				{
					case WordType.Number:list.remove(node);	suffixList.add(node);break;
					case WordType.Plus:dealOperator(node);break;
					case WordType.Minus:dealOperator(node);break;
					case WordType.Mul:dealOperator(node);break;
					case WordType.Div:dealOperator(node);break;
					case WordType.Power:dealOperator(node);break;
					case WordType.Leftp:dealOperator(node);break;
					case WordType.Rightp:dealRight(node);break;
					 default:break;
				}
				node=next;
			}

			//������ջ��ʣ���Ԫ��
			while(stack.Count!=0)
			{
				Word word=(Word)stack.Pop();
				if(word.wordType==WordType.Leftp)
					throw new Exception("������(ƥ�����");
				suffixList.add(new LinkNode(word));
			}

			return suffixList;
		
		}


		/// <summary>
		/// ��������ջ
		/// </summary>
		/// <param name="node"></param>

		private void dealOperator(LinkNode node)
		{
	



			list.remove(node);

			while(stack.Count!=0&&priorities[(int)((Word)stack.Peek()).wordType,(int)node.getWord().wordType]==1)
			{
				Word word=(Word)stack.Pop();
				suffixList.add(new LinkNode(word));


			}


			stack.Push(node.getWord());



		}


		/// <summary>
		/// ����������
		/// </summary>
		/// <param name="node"></param>
		private void dealRight(LinkNode node)
		{
			list.remove(node);
			Word word=null;

			while(stack.Count!=0)
			{
				word=(Word)stack.Pop();
				

				if(word.wordType==WordType.Leftp)//�����ź������ųɹ�ƥ��
				return;

				suffixList.add(new LinkNode(word));

			}


			if(word==null||word.wordType!=WordType.Rightp)
				throw new Exception("����ƥ�����");
		}

		/// <summary>
		/// ������ʽ��ֵ
		/// </summary>
		/// <returns></returns>


		private double reckon()
		{
			//��ʱstack���ŵ���double����
			stack.Clear();
			double r1,r2;

			LinkNode node=suffixList.First;

			while(node!=null)
			{
				if(node.getWord().wordType==WordType.Number)
					stack.Push(double.Parse(node.getWord().valueString));
				else if(node.getWord().wordType<WordType.Leftp)
				{
					if(stack.Count>=2)
					{
						r2=(double)stack.Pop();
						r1=(double)stack.Pop();

						switch(node.getWord().wordType)
						{
							case WordType.Plus:stack.Push(r1+r2);break;
							case WordType.Minus:stack.Push(r1-r2);break;
							case WordType.Mul:stack.Push(r1*r2);break;
							case WordType.Div:stack.Push(r1/r2);break;
							case WordType.Power:stack.Push(Math.Pow(r1,r2));break;
						}

					}
					else
						throw new Exception("���ʽ��������Ŀ����ȷ");
				}
				else throw new Exception("���ʽ���� 0");

				node=node.Next;
			}

			if(stack.Count!=1)
				throw new Exception("���ʽ��������Ŀ����ȷ");
			else 	return  (double)stack.Pop();
		}


		/// <summary>
		/// ���+ - * / �������ĸ�ʽ�Ƿ���ȷ
		/// </summary>

		private void observeList()
		{
			LinkNode node=list.First;
			LinkNode pre,next;

			while(node!=null)
			{
				//word��+ - * / ����������
				if(node.getWord().wordType<=WordType.Div)
				{
					pre=node.Previous;
					next=node.Next;

					if(next==null)
						throw new Exception(node.getWord().valueString+"��������1");

					if(pre.getWord().wordType!=WordType.Rightp&&pre.getWord().wordType!=WordType.Number)
						throw new Exception(node.getWord().valueString+"ǰ������");

					if(next.getWord().wordType!=WordType.Leftp&&next.getWord().wordType!=WordType.Number)
						throw new Exception(node.getWord().valueString+"��������2");


				


				}


					node=node.Next;

			}

		}



		


	
	
	
	}
}
