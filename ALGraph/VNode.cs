using System;

namespace ALGraph
{
	/// <summary>
	/// VNode ��ժҪ˵����������Ľṹ
	/// </summary>
	public class VNode:ICloneable
	{
		/// <summary>
		/// ������Ϣ
		/// </summary>
		public object data=null;

		/// <summary>
		/// �����ڸö���ĵ�һ����
		/// </summary>
		public ArcNode firstarc=null;

		/// <summary>
		/// �����ڸö�������һ����
		/// </summary>
		public ArcNode endarc=null;

		/// <summary>
		/// ���:ָ��ö���Ļ��ĸ���
		/// </summary>
		public int inDegree=0;

		/// <summary>
		/// ����:�Ӹö�������Ļ��ĸ���
		/// </summary>
		public int outDegree=0;

		/// <summary>
		/// ��¡����
		/// </summary>
		/// <returns></returns>
		public object Clone()
		{
			VNode cnode=new VNode();

			cnode.data=this.data;
			cnode.firstarc=this.firstarc;
			cnode.endarc=this.endarc;
			cnode.inDegree=this.inDegree;
			cnode.outDegree=this.outDegree;

			return cnode;

			
		}

	
		
		
		

		/// <summary>
		/// ���캯��
		/// </summary>
		public VNode()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		
	}
}
