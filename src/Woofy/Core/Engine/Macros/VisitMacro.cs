using System.Runtime.CompilerServices;
using Boo.Lang;
using Boo.Lang.Compiler;
using Boo.Lang.Compiler.Ast;

namespace Woofy.Core.Engine.Macros
{
    [CompilerGlobalScope]
    public static class MetaMethods
    {
        [Meta]
        public static MethodInvocationExpression Visit2()
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

    public class VisitMacro : LexicalInfoPreservingMacro
    {
        protected override Statement ExpandImpl(MacroStatement macro)
        {
            var containerAccessor = new ReferenceExpression("ContainerAccessor");
            
            var resolve = new MemberReferenceExpression(containerAccessor, "Resolve");
            var genericReference = new GenericReferenceExpression();
            genericReference.Target = resolve;
            genericReference.GenericArguments.Add(new SimpleTypeReference("IExpression"));

            var invokeVisit = new MethodInvocationExpression(genericReference, new StringLiteralExpression("visit"));
            var execute = new MemberReferenceExpression(invokeVisit, "Execute");

            var invokeExecute = new MethodInvocationExpression(execute, new NullLiteralExpression());
            return new ExpressionStatement(invokeExecute);
        }
    }
}