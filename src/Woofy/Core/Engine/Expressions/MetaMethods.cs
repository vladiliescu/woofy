using System.Runtime.CompilerServices;
using Boo.Lang;
using Boo.Lang.Compiler.Ast;

namespace Woofy.Core.Engine.Expressions
{
    [CompilerGlobalScope]
    public static class MetaMethods
    {
		public static MethodInvocationExpression GenerateIExpressionInvocationFor(string expressionName, StringLiteralExpression argument)
		{
            var @this = new SelfLiteralExpression();
            var invoke = new MemberReferenceExpression(@this, "InvokeExpression");

            return new MethodInvocationExpression(invoke,
                new StringLiteralExpression(expressionName),
                argument ?? (Expression)new NullLiteralExpression(), 
                new ReferenceExpression("context")
            );
		}

    	[Meta]
		public static MethodInvocationExpression visit(StringLiteralExpression regex)
        {
			return GenerateIExpressionInvocationFor("visit", regex);
        }

		[Meta]
		public static MethodInvocationExpression download(StringLiteralExpression regex)
		{
			return GenerateIExpressionInvocationFor("download", regex);
		}

        [Meta]
        public static MethodInvocationExpression sleep()
        {
            return GenerateIExpressionInvocationFor("sleep", null);
        }
    }
}