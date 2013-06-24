using System;
using System.Collections.Generic;
using System.Text;

namespace hammergo.Utility
{
    public class AppSearchEventArgs:EventArgs
    {
        private readonly string appName;

        public AppSearchEventArgs(string appName)
        {
            this.appName = appName;
        }

        public string AppName
        {
            get
            {
                return appName;
            }
        }
    }
}
