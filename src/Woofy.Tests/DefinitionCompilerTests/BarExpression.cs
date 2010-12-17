using System.Collections.Generic;
using Woofy.Core.Engine;

namespace Woofy.Tests.DefinitionCompilerTests
{
    public class BarExpression : IExpression
    {
        public Context Context { get; private set; }
        public string Argument { get; private set; }

        public IEnumerable<object> Invoke(object argument, Context context)
        {
            Context = context;
            Argument = (string)argument;
            return null;
        }
    }
}