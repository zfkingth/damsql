using System;
using System.Collections.Generic;
using System.Text;

namespace hammergo.TestDLL
{
    public class ChooseDBConfig
    {

        /// <summary>
        /// 默认的被选择的数据源的名称
        /// </summary>
        public string SelectedName { get; set; }

        List<DataSourceItem> _dataSourceList = null;
        public List<DataSourceItem> DataSourceList
        {
            get
            {
                return _dataSourceList;
            }

            set
            {
                _dataSourceList = value;

            }
        }

    }
}
