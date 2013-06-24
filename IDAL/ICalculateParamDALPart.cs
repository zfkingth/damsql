using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hammergo.IDAL
{
    partial interface ICalculateParamDAL
    {
        List<string> getChildAppCalcName(string appCalcName);
    }
}
