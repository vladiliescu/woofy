using System.Runtime.CompilerServices;
using Boo.Lang;
using Boo.Lang.Compiler.Ast;

namespace Woofy.Core.Engine
{
    [CompilerGlobalScope]
    public static class MetaMethods
    {
		public static MethodInvocationExpression GenerateIExpressionInvocationFor(string keyword, StringLiteralExpression argument)
		{
			//todo: it would be best if i accessed the types and their corresponding method via strong-typed expressions, instead of string literals
			var containerAccessor = new ReferenceExpression("ContainerAccessor");
			var resolve = new MemberReferenceExpression(containerAccessor, "Resolve");
			var genericReference = new GenericReferenceExpression();
			genericReference.Target = resolve;
			genericReference.GenericArguments.Add(new SimpleTypeReference("IExpression"));

			var invokeVisit = new MethodInvocationExpression(genericReference, new StringLiteralExpression(keyword));
			var execute = new MemberReferenceExpression(invokeVisit, "Invoke");

			var invokeExecute = argument != null ?
				new MethodInvocationExpression(execute, argument, new ReferenceExpression("context")) :
				new MethodInvocationExpression(execute, new NullLiteralExpression(), new ReferenceExpression("context"));
			return invokeExecute;
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
    }
}