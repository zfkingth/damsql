using System;

namespace hammergo.caculator
{
	/// <summary>
	/// ScanFactory ��ժҪ˵������̬����
	/// </summary>
	 internal  class ScanFactory
	{
		/// <summary>
		/// ����ɨ����
		/// </summary>
        /// <param name="usePrefix"></param>
		/// <returns></returns>
         public static AbstractScan createScan(bool usePrefix)
		{
			AbstractScan scan=null;

            if (!usePrefix)
			{
				scan=new StrictScan();

			}
			else
			{
				scan=new RelaxedScan();
			}

			return scan;
		}
	}
}
