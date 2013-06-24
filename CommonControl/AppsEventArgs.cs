using System;
using System.Collections.Generic;
using System.Text;

namespace hammergo.CommonControl
{
    public class AppsEventArgs:EventArgs
    {
        private readonly List<string> appNameList=null;

        public AppsEventArgs(List<string> appNameList)
        {
            this.appNameList = appNameList;
        }

        public List<string> AppNameList
        {
            get
            {
                return appNameList;
            }
        }
    }
}
