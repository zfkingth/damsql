using System;

namespace ALGraph
{
	/// <summary>
	/// ArcNode ��ժҪ˵���������Ľṹ
	/// </summary>
	public class ArcNode
	{
		/// <summary>
		/// �û���ָ��Ķ���
		/// </summary>
		public VNode adjvex=null;

		/// <summary>
		/// ��һ����������
		/// </summary>
		public ArcNode nextarc=null;

		/// <summary>
		/// �뻡��ص�Ȩֵ����Ȩ��Ϊ0
		/// </summary>
		public int weight=0;

		/// <summary>
		/// ���������Ϣ������
		/// </summary>
		public object info=null;



		/// <summary>
		/// 
		/// </summary>
		public ArcNode()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
	}
}
