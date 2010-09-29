using Woofy.Core.Engine;
using Xunit;
using System.Linq;

namespace Woofy.Tests.DefinitionCompilerTests
{
	public class Compiled_definitions
	{
		[Fact]
		public void Should_be_named_after_their_filenames()
		{
			var assembly = new DefinitionCompiler().Compile(
				@"DefinitionCompilerTests\alpha.boo",
				@"DefinitionCompilerTests\beta.boo",
				@"DefinitionCompilerTests\gamma.boo"
				);
			
			var types = assembly.GetTypes();
			var expectedNames = new[] { "_alpha", "_beta", "_gamma" };

			Assert.Equal(
				3, types.Select(type => type.Name).Intersect(expectedNames).Count()
			);
		}

		[Fact]
		public void Should_inherit_the_base_class()
		{
			var compiler = new DefinitionCompiler();
			var assembly = compiler.Compile(@"DefinitionCompilerTests\alpha.boo");

			var alpha = assembly.GetType("_alpha");

			Assert.True(alpha.IsSubclassOf(typeof(BaseDefinition)));
		}
	}
}