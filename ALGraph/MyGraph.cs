using System;
using System.Collections;
namespace ALGraph
{
	/// <summary>
	/// MyGraph ��ժҪ˵����ͼ���ڽӱ�ṹ����
	/// </summary>
	public class MyGraph:ICloneable
	{

		/// <summary>
		/// 
		/// </summary>
		public readonly int startSize=10;
		/// <summary>
		/// ��������
		/// </summary>
		public ArrayList vertices=null;

		/// <summary>
		/// ͼ�ĵ�ǰ����
		/// </summary>
		public int Arcnum=0;

		/// <summary>
		/// 
		/// </summary>
		public MyGraph()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			vertices=new ArrayList(startSize);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public object Clone()
		{
			MyGraph graph=new MyGraph();

			graph.Arcnum=this.Arcnum;
			graph.vertices=new ArrayList(vertices.Capacity);

			for(int i=0;i<vertices.Count;i++)
			{
				graph.vertices.Add(((VNode)vertices[i]).Clone());

			}


			return graph;
		}

		/// <summary>
		/// ͼ�ĵ�ǰ������
		/// </summary>
		public int Vexnum
		{
		
			get
			{
				return vertices.Count;
			}
		}

		/// <summary>
		///  �򶥵����������һ���µĶ���
		/// </summary>
		/// <param name="nodeData">���������</param>
		/// <returns></returns>
		public VNode addVNode(object nodeData)
		{
			
		
			for(int i=0;i<vertices.Count;i++)
			{
				VNode node=(VNode)vertices[i];

				if(nodeData.Equals(node.data))
					return node;
			}
				

			VNode newnode=new VNode();
			newnode.data=nodeData;

			vertices.Add(newnode);

				return newnode;
			
		}

		/// <summary>
		/// ���ݶ����������Ϣ�����Ҷ���
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public VNode getVNode(object data)
		{


			for(int i=0;i<vertices.Count;i++)
			{
				VNode node=(VNode)vertices[i];
				if(node.data.Equals(data))
					return node;
			}

			return null;
		}


		/// <summary>
		/// ��ӻ�
		/// </summary>
		/// <param name="arcNode"></param>
		/// <param name="fromData"></param>
		/// <param name="toData"></param>
		/// <returns></returns>

		public bool addArcNode(ArcNode arcNode,object fromData,object toData)
		{

//			if(vertices.Contains(from)==false||vertices.Contains(to)==false)
//			{
//				//��ӻ�ʱҪ��֤�������ӵ���������Ҫ����
//				return false;
//			}

			//��ӻ�ʱҪ��֤�������ӵ���������Ҫ����


			VNode from=addVNode(fromData);

			VNode to=addVNode(toData);



			//��֤��һ�����㵽��һ������ֻ��һ����

			ArcNode arc=from.firstarc;

			for(;arc!=null;arc=arc.nextarc)
			{
				if(arc.adjvex==to)
					return false;//�Ѿ�����һ���Ӷ���from������to�Ļ�
			}

			//������ӵ�ͼ��


			//������ǴӸö�������ĵ�һ����
			if(from.firstarc==null)
			{
				from.firstarc=from.endarc=arcNode;
			}
			else
			{
				//���ǵ�һ����
				from.endarc.nextarc=arcNode;
				from.endarc=arcNode;
			}
			
			arcNode.adjvex=to;

			
			to.inDegree++;
			from.outDegree++;

			return true;
		}




		/// <summary>
		/// ��������
		/// </summary>
		/// <returns>���������������</returns>
		public ArrayList topSort()
		{
			Stack stack=new Stack(startSize);

			ArrayList list=new ArrayList(startSize);

			//������ǰ�������Ϊ��Ķ�����ջ

			for(int i=0;i<vertices.Count;i++)
			{
				VNode node=(VNode)vertices[i];

				if(node.inDegree==0)
					stack.Push(node);
			}


			while(stack.Count!=0)
			{
				VNode v=(VNode)stack.Pop();
				// �����ǰ���Ķ���
				list.Add(v.data);
				
				ArcNode arc=v.firstarc;

				//���������ڸö�������л�
				for(;arc!=null;arc=arc.nextarc)
				{

					//�����ڽӵ����ȼ�1

					arc.adjvex.inDegree--;
					if(arc.adjvex.inDegree==0)
					{
						//�µ���ǰ���Ķ���
						stack.Push(arc.adjvex);
					}
				}
			}
			return list;


		}
	}
}
