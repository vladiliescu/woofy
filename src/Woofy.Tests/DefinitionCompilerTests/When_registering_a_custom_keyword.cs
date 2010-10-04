using System.Runtime.CompilerServices;
using Autofac;
using Boo.Lang;
using Boo.Lang.Compiler.Ast;
using Woofy.Core.Engine;
using Woofy.Core.Engine.Macros;
using Woofy.Core.Infrastructure;
using Xunit;
using Module = Autofac.Module;

namespace Woofy.Tests.DefinitionCompilerTests
{
    public class When_registering_a_custom_keyword : BaseDefinitionCompilerTest
    {
        static When_registering_a_custom_keyword()
        {
            ContainerAccessor.RegisterComponents(new CustomKeywordModule());
        }

        [Fact]
        public void Should_invoke_the_associated_expression()
        {
			var expression = (CustomExpression)ContainerAccessor.Resolve<IExpression>("custom_keyword");

			var assembly = CompileReferencingTests("custom_keyword.boo");

			var visit = (Definition)assembly.CreateInstance("_custom_keyword");
            visit.Run();

            Assert.Equal(3, expression.TimesInvoked);
        }
    }

	public class CustomKeywordModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<CustomExpression>()
				.Named<IExpression>("custom_keyword")
				.SingleInstance();
		}
	}

	public class CustomExpression : IExpression
    {
        public int TimesInvoked { get; private set; }

        public object Invoke(Context context)
        {
            return ++TimesInvoked;
        }
    }
}

namespace Woofy.Core.Engine.Macros
{
	[CompilerGlobalScope]
	public static class CustomKeywordMetaMethodContainer
	{
		[Meta]
		public static MethodInvocationExpression custom_keyword()
		{
			return MetaMethods.GenerateIExpressionInvocationFor("custom_keyword");
		}
	}
}