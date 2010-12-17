using Woofy.Core.Engine;
using Woofy.Core.Infrastructure;
using Xunit;

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
            definition.ComicInstance = new Core.ComicManagement.Comic();
            definition.Run();

			var foo = (FooExpression)ContainerAccessor.Resolve<IExpression>("foo");
            Assert.True(foo.TimesInvoked > 1);
        }

		[Fact]
		public void Should_pass_the_context_to_the_expression()
		{
			var assembly = CompileReferencingTests("custom_keywords.boo");

			var definition = (Definition)assembly.CreateInstance("_custom_keywords");
            definition.ComicInstance = new Core.ComicManagement.Comic();
			definition.Run();

			var foo = (FooExpression)ContainerAccessor.Resolve<IExpression>("foo");
			Assert.NotNull(foo.Context);
		}

		[Fact]
		public void Should_pass_the_same_context_to_all_the_expressions()
		{
			var assembly = CompileReferencingTests("custom_keywords.boo");

			var definition = (Definition)assembly.CreateInstance("_custom_keywords");
            definition.ComicInstance = new Core.ComicManagement.Comic();
			definition.Run();

			var foo = (FooExpression)ContainerAccessor.Resolve<IExpression>("foo");
			var bar = (BarExpression)ContainerAccessor.Resolve<IExpression>("bar");
			Assert.Same(foo.Context, bar.Context);
		}

		[Fact]
		public void Should_pass_the_argument_to_the_expression()
		{
			var assembly = CompileReferencingTests("custom_keywords.boo");

			var definition = (Definition)assembly.CreateInstance("_custom_keywords");
            definition.ComicInstance = new Core.ComicManagement.Comic();
			definition.Run();
            
			var bar = (BarExpression)ContainerAccessor.Resolve<IExpression>("bar");
			Assert.Equal("baz", bar.Argument);
		}

		[Fact]
		public void Can_compile_expressions_with_multiple_arguments()
		{
			var assembly = CompileReferencingTests("several_arguments.boo");
			var definition = (Definition)assembly.CreateInstance("_several_arguments");
            definition.ShouldNotBeNull();
		}

        [Fact]
        public void Can_compile_expressions_with_arguments_other_than_string_literals()
        {
            var assembly = CompileReferencingTests("reference_arguments.boo");
            var definition = (Definition)assembly.CreateInstance("_reference_arguments");
            definition.ShouldNotBeNull();
        }
    }
}