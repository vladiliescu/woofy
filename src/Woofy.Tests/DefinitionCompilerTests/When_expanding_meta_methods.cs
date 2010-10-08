using System.Runtime.CompilerServices;
using Autofac;
using Boo.Lang;
using Boo.Lang.Compiler.Ast;
using Woofy.Core.Engine;
using Woofy.Core.Infrastructure;
using Xunit;
using Module = Autofac.Module;

namespace Woofy.Tests.DefinitionCompilerTests
{
    public class When_expanding_meta_methods : BaseDefinitionCompilerTest
    {
        static When_expanding_meta_methods()
        {
            ContainerAccessor.RegisterComponents(new CustomKeywordModule());
        }

        [Fact]
        public void Should_invoke_the_associated_expression()
        {
			var assembly = CompileReferencingTests("custom_keywords.boo");

			var definition = (Definition)assembly.CreateInstance("_custom_keywords");
            definition.Run();

			var foo = (FooExpression)ContainerAccessor.Resolve<IExpression>("foo");
            Assert.True(foo.TimesInvoked > 1);
        }

		[Fact]
		public void Should_pass_the_context_to_the_expression()
		{
			var assembly = CompileReferencingTests("custom_keywords.boo");

			var definition = (Definition)assembly.CreateInstance("_custom_keywords");
			definition.Run();

			var foo = (FooExpression)ContainerAccessor.Resolve<IExpression>("foo");
			Assert.NotNull(foo.Context);
		}

		[Fact]
		public void Should_pass_the_same_context_to_all_the_expressions()
		{
			var assembly = CompileReferencingTests("custom_keywords.boo");

			var definition = (Definition)assembly.CreateInstance("_custom_keywords");
			definition.Run();

			var foo = (FooExpression)ContainerAccessor.Resolve<IExpression>("foo");
			var bar = (BarExpression)ContainerAccessor.Resolve<IExpression>("bar");
			Assert.Same(foo.Context, bar.Context);
		}

		[Fact]
		public void Should_pass_the_argument_to_the_expression()
		{
			Assert.True(false);
		}
    }

	public class CustomKeywordModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			builder.RegisterType<FooExpression>()
				.Named<IExpression>("foo")
				.SingleInstance();

			builder.RegisterType<BarExpression>()
				.Named<IExpression>("bar")
				.SingleInstance();
		}
	}

	public class FooExpression : IExpression
    {
        public int TimesInvoked { get; private set; }
		public Context Context { get; private set; }

        public object Invoke(Context context)
        {
			Context = context;
            return ++TimesInvoked;
        }
    }

	public class BarExpression : IExpression
	{
		public Context Context { get; private set; }

		public object Invoke(Context context)
		{
			Context = context;
			return null;
		}
	}
}

namespace Woofy.Core.Engine.Macros
{
	[CompilerGlobalScope]
	public static class CustomKeywordMetaMethodContainer
	{
		[Meta]
		public static MethodInvocationExpression foo()
		{
			return MetaMethods.GenerateIExpressionInvocationFor("foo");
		}

		[Meta]
		public static MethodInvocationExpression bar()
		{
			return MetaMethods.GenerateIExpressionInvocationFor("bar");
		}
	}
}