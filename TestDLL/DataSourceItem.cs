using System;
using System.Collections.Generic;
using System.Text;

namespace hammergo.TestDLL
{
    public class DataSourceItem
    {

        /// <summary>
        /// ����
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// �ļ�·��
        /// </summary>
        public string FilePath { get; set; }


        public override string ToString()
        {
            return Name;
                
        }
    }
}
