using System;
using System.Collections.Generic;
using System.Text;

namespace hammergo.DataExport
{
    class ExportAllDataArgs
    {
        private readonly DateTime? _startDate;
        private readonly DateTime? _endDate;
        private readonly List<string> _appNameList;

        public ExportAllDataArgs(DateTime? startDate, DateTime? endDate, List<string> appNameList)
        {
            this._startDate = startDate;
            this._endDate = endDate;
            this._appNameList = appNameList;
        }

        public DateTime? StartDate
        {
            get
            {
                return _startDate;
            }
        }

        public DateTime? EndDate
        {
            get
            {
                return _endDate;
            }
        }

        public List<string> AppNameList
        {
            get
            {
                return _appNameList;
            }
        }
    }
}
