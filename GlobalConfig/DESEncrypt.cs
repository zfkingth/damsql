using System;
using System.Security.Cryptography;  
using System.Text;
namespace hammergo.GlobalConfig
{
	/// <summary>
	/// DES����/�����ࡣ
	/// </summary>
	public class DESEncrypt
	{
		public DESEncrypt()
		{			
		}

		#region ========����======== 
 
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
		public static string Encrypt(string Text) 
		{
			return Encrypt(Text,"litianping");
		}
		/// <summary> 
		/// �������� 
		/// </summary> 
		/// <param name="Text"></param> 
		/// <param name="sKey"></param> 
		/// <returns></returns> 
		public static string Encrypt(string Text,string sKey) 
		{
            return "";
		} 

		#endregion
		
		#region ========����======== 
   
 
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="Text"></param>
        /// <returns></returns>
		public static string Decrypt(string Text) 
		{
			return Decrypt(Text,"litianping");
		}
		/// <summary> 
		/// �������� 
		/// </summary> 
		/// <param name="Text"></param> 
		/// <param name="sKey"></param> 
		/// <returns></returns> 
		public static string Decrypt(string Text,string sKey) 
		{

            return "";
		} 
 
		#endregion 


	}
}
