using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace hammergo.Model
{
    public interface IParamInterface
    {
        string ParamName
        {
            get;
            set;
        }

        string ParamSymbol
        {
            get;
            set;
        }

        string UnitSymbol
        {
            get;
            set;
        }

        byte? PrecisionNum
        {
            get;
            set;
        }


        byte? Order
        {
            get;
            set;
        }

        string Description
        {
            get;
            set;
        }
    }
}
