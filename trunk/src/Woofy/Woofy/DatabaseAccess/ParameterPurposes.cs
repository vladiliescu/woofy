using System;
using System.Collections.Generic;
using System.Text;

namespace Woofy.DatabaseAccess
{
    [Flags]
    public enum ParameterPurposes
    {
        None = 0,
        Regular = 1 ,
        Where = 1 << 1,
        OrderBy = 1 << 2,
        Any = Regular | Where | OrderBy
    }
}
