using System.Runtime.CompilerServices;
using Boo.Lang;
using Boo.Lang.Compiler.Ast;

namespace Woofy.Core.Engine.Macros
{
    [CompilerGlobalScope]
    public static class MetaMethods
    {
        [Meta]
        public static MethodInvocationExpression visit()
        {
            var containerAccessor = new ReferenceExpression("ContainerAccessor");
            var resolve = new MemberReferenceExpression(containerAccessor, "Resolve");
            var genericReference = new GenericReferenceExpression();
            genericReference.Target = resolve;
            genericReference.GenericArguments.Add(new SimpleTypeReference("IExpression"));

            var invokeVisit = new MethodInvocationExpression(genericReference, new StringLiteralExpression("visit"));
            var execute = new MemberReferenceExpression(invokeVisit, "Execute");

            var invokeExecute = new MethodInvocationExpression(execute, new NullLiteralExpression());
            return invokeExecute;
        }
    }
}