using System.Runtime.CompilerServices;
using Boo.Lang;
using Boo.Lang.Compiler.Ast;
using Woofy.Core.Engine.Expressions;

namespace Woofy.Core.Engine
{
    [CompilerGlobalScope]
    public static class CustomKeywordMetaMethodContainer
    {
        [Meta]
        public static MethodInvocationExpression foo()
        {
            return MetaMethods.GenerateIExpressionInvocationFor("foo", null);
        }

        [Meta]
        public static MethodInvocationExpression bar(StringLiteralExpression argument)
        {
            return MetaMethods.GenerateIExpressionInvocationFor("bar", argument);
        }
    }
}