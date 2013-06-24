using System;
using System.Collections;

namespace hammergo.caculator
{
	/// <summary>
	/// LinkList ��ժҪ˵����
	/// </summary>
	internal class LinkList
	{
		/// <summary>
		/// �׽��
		/// </summary>
		LinkNode first;
		

		/// <summary>
		/// β���
		/// </summary>
		LinkNode end;


		/// <summary>
		/// �����н��ĸ���
		/// </summary>
		int count=0;


		/// <summary>
		/// Constructor 
		/// </summary>
		public LinkList()
		{
			first=end=null;
			count=0;
		}

		/// <summary>
		/// Constructor 
		/// </summary>
		/// <param name="first"></param>
		/// <param name="end"></param>

		public LinkList(LinkNode first,LinkNode end)
		{
			this.first=first;
			this.end=end;
			if(end!=null)
			first.Previous=end.Next=null;
			count=Acount();
		}

		/// <summary>
		/// ɾ�����еĽ��
		/// </summary>

		public void  removeAll()
		{
			first=end=null;
			count=0;

		}

		/// <summary>
		/// ���������н��ĸ���
		/// </summary>

		public int Count
		{
			get
			{
				return count;
			}
		}

		/// <summary>
		/// ɾ�����
		/// </summary>
		/// <param name="node"></param>

		public void remove(LinkNode node)
		{
			
			
			if(node==first)
			{
				first=first.Next;
				if(first==null)
					end=null;
			}
			else if(node==end)
			{
				end=node.Previous;

				end.Next=null;
			}
				
			else
			{
				node.Previous.Next=node.Next;
				node.Next.Previous=node.Previous;
			}

			count--;

			
		}

		/// <summary>
		/// ɾ���������֮��Ľ�㣬�������������
		/// </summary>
		/// <param name="start"></param>
		/// <param name="finish"></param>
		public void removeBetween(LinkNode start,LinkNode finish)
		{
			if(start==first&&finish==end)
			{
				first=end=null;
				return;
			}

			if(start==first)
			{
				first=finish.Next;
				first.Previous=null;
				return;
			}

			if(finish==end)
			{
				end=start.Previous;
				end.Next=null;
				return;
			}
			else
			{
				start.Previous.Next=finish.Next;
				finish.Next.Previous=start.Previous;
			}





			count=Acount();


		}


		/// <summary>
		/// �����׽��
		/// </summary>
		public LinkNode First
		{
			get
			{
				return first;
			}

		}


		/// <summary>
		/// ��������һ�����
		/// </summary>
		public LinkNode End
		{
			get
			{
				return end;
			}
		}

		/// <summary>
		/// �������ĩβ���һ���µĽ��
		/// </summary>
		/// <param name="node"></param>

		public void add(LinkNode node)
		{
			if(first==null)
			{
				first=end=node;
				node.Previous=null;
				node.Next=null;
				
			}
			else
			{
				end.Next=node;
				node.Previous=end;
				end=node;
				end.Next=null;
			}
				
			count++;
		}



		
		/// <summary>
		/// �ڽ��nodeǰ����һ���µĽ��newNode
		/// </summary>
		/// <param name="node"></param>
		/// <param name="newNode"></param>

		public void insert(LinkNode node,LinkNode newNode)
		{
			 if(node==null)
			{
				add(newNode);
			}

			else if(node==first)
			{
				first.Previous=newNode;
				newNode.Next=node;
				first=newNode;
			}

			

			else
			{
				LinkNode preNode=node.Previous;

				newNode.Next=node;

				newNode.Previous=preNode;
				node.Previous=newNode;
				preNode.Next=newNode;
				
			}


			count++;

		}

		


		/// <summary>
		/// ��+��-ǰ�������0
		/// </summary>
		/// <returns></returns>
		public LinkList modify()
		{
			//����ͷ���
			LinkNode node=this.First;
			if(node.getWord().wordType==WordType.Plus||node.getWord().wordType==WordType.Minus)
				insert(node,new LinkNode(new Word(WordType.Number,"0")));


			LinkNode preNode=node;//ǰһ�����
			node=node.Next;

			while(node!=null)
			{
				//��+ -��ǰ����(����+ ����ǰ���һ��0
				if(node.getWord().wordType==WordType.Plus||node.getWord().wordType==WordType.Minus)
				{
					if(preNode.getWord().wordType==WordType.Leftp||preNode.getWord().wordType==WordType.Comma)
						insert(node,new LinkNode(new Word(WordType.Number,"0")));

					
				}

				//ȷ��������
				if(preNode.getWord().wordType==WordType.Identifier&&node.getWord().wordType==WordType.Leftp)
					preNode.getWord().wordType=WordType.FunName;

				 preNode=node;

				node=node.Next;

			}
				


			return this;
		}


		/// <summary>
		/// ͳ������������ĸ���
		/// </summary>
		
		public int Acount()
		{
			
				int count=0;
				LinkNode node=first;
				while(node!=null)
				{
					count++;
					node=node.Next;
				}
				return count;

			
		}
		
		/// <summary>
		/// ��������Ѱ�Ҷ��ţ������������
		/// </summary>
		/// <returns></returns>


		public LinkNode searchComma()
		{
			LinkNode node=first;
			while(node!=null)
			{
				if(node.getWord().wordType==WordType.Comma)
					return node;

				node=node.Next;
			}
			return null;//û�ж���������
		}


		
		/// <summary>
		/// �����ʽ�еı����滻Ϊ���������
		/// </summary>
		/// <param name="slist"></param>

		public void replaceVariable(MyList slist)
		{

			LinkNode node=first;
			while(node!=null)
			{
				Word word=node.getWord();
				if(word.wordType==WordType.Identifier||word.wordType==WordType.IdentifierWithDot)
					
				{
					string name=node.getWord().valueString;
				
					double v=slist[name];

				

					word.wordType=WordType.Number;
					word.valueString=v.ToString();
						
				
				}

				node=node.Next;
			}

		}



	}
}
