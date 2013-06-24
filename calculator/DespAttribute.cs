using System;

namespace hammergo.caculator
{
	/// <summary>
	/// DespAttribute ��ժҪ˵����
	/// </summary>
	public class DespAttribute:Attribute
	{
		/// <summary>
		/// ������ʽ
		/// </summary>
		string pattern;
		/// <summary>
		/// ��������˵��
		/// </summary>
		string description;
		/// <summary>
		/// ��������˵��
		/// </summary>
		string paDescription;
		
		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="pattern">������ʽ</param>
		/// <param name="description">������������</param>
		/// <param name="paDescription">������������</param>
		public DespAttribute(string pattern,string description,string paDescription)
		{
			this.pattern=pattern;
			this.description=description;
			this.paDescription=paDescription;
		}

		/// <summary>
		/// ��������ʽ
		/// </summary>

		public string Pattern
		{
			get
			{
				return pattern;
			}
		}

		/// <summary>
		/// ��������
		/// </summary>
		public string Description
		{
			get
			{
				return description;
			}
		}


		/// <summary>
		/// ��������˵��
		/// </summary>
		public string PaDescription
		{
			get
			{
				return paDescription;
			}
		}

	}
}
