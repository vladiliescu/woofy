using System.Collections.Generic;
using Woofy.Core.Engine;

namespace Woofy.Tests.DefinitionCompilerTests
{
    public class FooExpression : IExpression
    {
        public int TimesInvoked { get; private set; }
        public Context Context { get; private set; }

        public IEnumerable<object> Invoke(object argument, Context context)
        {
            Context = context;
            TimesInvoked++;
            return new object[] { TimesInvoked };
        }
    }
}