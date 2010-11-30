using System.Runtime.CompilerServices;
using Boo.Lang;
using Boo.Lang.Compiler.Ast;

namespace Woofy.Core.Engine.Expressions
{
    [CompilerGlobalScope]
    public static class MetaMethods
    {
		public static MethodInvocationExpression GenerateIExpressionInvocationFor(string expressionName, params StringLiteralExpression[] arguments)
		{
            var @this = new SelfLiteralExpression();
            var invoke = new MemberReferenceExpression(@this, "InvokeExpression");

            Expression args;

            if (arguments == null)
                args = new NullLiteralExpression();
            else if (arguments.Length == 1)
                args = arguments[0];
            else
                args = new ArrayLiteralExpression { Items = ExpressionCollection.FromArray(arguments) };

		    return new MethodInvocationExpression(invoke,
                    new StringLiteralExpression(expressionName),
                    args,
                    new ReferenceExpression("context")
                );
		}

    	[Meta]
		public static MethodInvocationExpression visit(StringLiteralExpression regex)
        {
			return GenerateIExpressionInvocationFor(Expressions.Visit, regex);
        }

		[Meta]
		public static MethodInvocationExpression download(StringLiteralExpression regex)
		{
			return GenerateIExpressionInvocationFor(Expressions.Download, regex);
		}

        [Meta]
        public static MethodInvocationExpression sleep()
        {
            return GenerateIExpressionInvocationFor(Expressions.Sleep);
        }

        [Meta]
		public static MethodInvocationExpression meta(StringLiteralExpression key, StringLiteralExpression value)
		{
			return GenerateIExpressionInvocationFor(Expressions.Meta, key, value);
		}

        [Meta]
        public static MethodInvocationExpression write_meta_to_file()
        {
            return GenerateIExpressionInvocationFor(Expressions.WriteMetaToFile);
        }
    }
}