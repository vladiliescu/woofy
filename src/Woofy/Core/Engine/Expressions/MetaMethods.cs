using System.Runtime.CompilerServices;
using Boo.Lang;
using Boo.Lang.Compiler.Ast;

namespace Woofy.Core.Engine.Expressions
{
    [CompilerGlobalScope]
    public static class MetaMethods
    {
		public static MethodInvocationExpression GenerateIExpressionInvocationFor(string expressionName, params Expression[] arguments)
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
        public static MethodInvocationExpression go_to(StringLiteralExpression regex)
        {
            return GenerateIExpressionInvocationFor(Expressions.GoTo, regex);
        }

        [Meta]
        public static MethodInvocationExpression peek(StringLiteralExpression regex)
        {
            return GenerateIExpressionInvocationFor(Expressions.Peek, regex);
        }

        [Meta]
		public static MethodInvocationExpression download(StringLiteralExpression regex)
		{
			return GenerateIExpressionInvocationFor(Expressions.Download, regex);
		}

        [Meta]
        public static MethodInvocationExpression download(ReferenceExpression reference)
        {
            return GenerateIExpressionInvocationFor(Expressions.Download, reference);
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
        public static MethodInvocationExpression title(StringLiteralExpression value)
        {
            return meta(new StringLiteralExpression("title"), value);
        }

        [Meta]
        public static MethodInvocationExpression description(StringLiteralExpression value)
        {
            return meta(new StringLiteralExpression("description"), value);
        }

        [Meta]
        public static MethodInvocationExpression write_meta_to_text()
        {
            return GenerateIExpressionInvocationFor(Expressions.WriteMetaToText);
        }

        [Meta]
        public static MethodInvocationExpression match(StringLiteralExpression value)
        {
            return GenerateIExpressionInvocationFor(Expressions.Match, value);
        }

        [Meta]
        public static MethodInvocationExpression warn(Expression value)
        {
            return GenerateIExpressionInvocationFor(Expressions.Warn, value);
        }

        [Meta]
        public static MethodInvocationExpression log(Expression value)
        {
            return GenerateIExpressionInvocationFor(Expressions.Log, value);
        }
    }
}