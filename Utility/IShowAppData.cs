using System;
using System.Collections.Generic;
using System.Text;

namespace hammergo.Utility
{
    public interface IShowAppData
    {
        event EventHandler<hammergo.Utility.AppSearchEventArgs> ShowDataEvent;
    }
}
