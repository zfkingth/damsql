using System;
using System.Collections.Generic;
using System.Text;

namespace hammergo.TestDLL
{
    public class ChooseDBConfig
    {

        /// <summary>
        /// Ĭ�ϵı�ѡ�������Դ������
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
