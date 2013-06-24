using System;
using System.Collections;
namespace hammergo.DALFactory
{
	/// <summary>
	/// ���������
	/// </summary>
	public class DataCache
	{
        private static Hashtable cache = Hashtable.Synchronized( new Hashtable());
		/// <summary>
		/// ��ȡ��ǰӦ�ó���ָ��CacheKey��Cacheֵ
		/// </summary>
		/// <param name="CacheKey"></param>
		/// <returns></returns>
		public static object GetCache(string CacheKey)
		{
            object obj=null;
            if(cache.ContainsKey(CacheKey))
            {
                obj= cache[CacheKey];
            }
			
            return obj;
		}

		/// <summary>
		/// ���õ�ǰӦ�ó���ָ��CacheKey��Cacheֵ
		/// </summary>
		/// <param name="CacheKey"></param>
		/// <param name="objObject"></param>
		public static void SetCache(string CacheKey, object objObject)
		{
			
			cache.Add(CacheKey, objObject);
		}
	}
}
