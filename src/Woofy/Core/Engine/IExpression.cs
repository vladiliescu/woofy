using System.Collections.Generic;

namespace Woofy.Core.Engine
{
    public interface IExpression
    {
        IEnumerable<object> Invoke(object argument, Context context);
    }
}