using System.Runtime.CompilerServices;
using Boo.Lang;
using Boo.Lang.Compiler.Ast;

namespace Woofy.Core.Engine.Macros
{
    [CompilerGlobalScope]
    public static class MetaMethods
    {
		public static MethodInvocationExpression GenerateIExpressionInvocationFor(string keyword)
		{
			//todo: it would be best if i accessed the types and their corresponding method via strong-typed expressions, instead of string literals
			var containerAccessor = new ReferenceExpression("ContainerAccessor");
			var resolve = new MemberReferenceExpression(containerAccessor, "Resolve");
			var genericReference = new GenericReferenceExpression();
			genericReference.Target = resolve;
			genericReference.GenericArguments.Add(new SimpleTypeReference("IExpression"));

			var invokeVisit = new MethodInvocationExpression(genericReference, new StringLiteralExpression(keyword));
			var execute = new MemberReferenceExpression(invokeVisit, "Invoke");

			var invokeExecute = new MethodInvocationExpression(execute, new NullLiteralExpression());
			return invokeExecute;
		}

    	[Meta]
        public static MethodInvocationExpression visit()
        {
			return GenerateIExpressionInvocationFor("visit");
        }

		[Meta]
		public static MethodInvocationExpression download()
		{
			return GenerateIExpressionInvocationFor("download");
		}
    }
}