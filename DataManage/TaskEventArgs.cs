using System;
using System.Collections.Generic;
using System.Text;
using hammergo.Model;

namespace hammergo.DataManage
{
    public class TaskEventArgs:EventArgs
    {
        private readonly Guid appCollectionID;
        private readonly DateTime currentDate;

        public TaskEventArgs(Guid appCollectionID,DateTime currentDate)
        {
            this.appCollectionID = appCollectionID;
            this.currentDate = currentDate;
        }

        public Guid TaskAppCollectionID
        {
            get
            {
                return appCollectionID;
            }
        }

        public DateTime CurrentDate
        {
            get
            {
                return currentDate;
            }
        }
    }
}
