using System;
using System.Collections.Generic;
using System.Text;

namespace hammergo.TestDLL
{
    public class DataSourceItem
    {

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }


        public override string ToString()
        {
            return Name;
                
        }
    }
}
