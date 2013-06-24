using System;
using System.Collections;
namespace hammergo.DALFactory
{
	/// <summary>
	/// 缓存操作类
	/// </summary>
	public class DataCache
	{
        private static Hashtable cache = Hashtable.Synchronized( new Hashtable());
		/// <summary>
		/// 获取当前应用程序指定CacheKey的Cache值
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
		/// 设置当前应用程序指定CacheKey的Cache值
		/// </summary>
		/// <param name="CacheKey"></param>
		/// <param name="objObject"></param>
		public static void SetCache(string CacheKey, object objObject)
		{
			
			cache.Add(CacheKey, objObject);
		}
	}
}
