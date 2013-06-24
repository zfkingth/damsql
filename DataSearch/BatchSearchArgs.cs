using System;
using System.Collections.Generic;
using System.Text;
using hammergo.Model;

namespace hammergo.DataSearch
{
    class BatchSearchArgs
    {
       
        private readonly  List<AppCollection> _matchAppColList ;

        private readonly string _filterTypeName;

        private readonly string _filterName;

        public BatchSearchArgs(List<AppCollection> matchAppColList, string filterTypeName, string filterName)
        {

            this._matchAppColList = matchAppColList;
            this._filterTypeName = filterTypeName;
            this._filterName = filterName;
        }


        public string FilterTypeName
        {
            get
            {
                return _filterTypeName;
            }
        }

        public string FilterName
        {
            get
            {
                return _filterName;
            }
        }

        public List<AppCollection> MatchAppColList
        {
            get
            {
                return _matchAppColList;
            }
        }

    }
}
