using Woofy.Core.Engine;
using Xunit;

namespace Woofy.Tests.DefinitionCompilerTests
{
	public class When_compiling_the_definitions
	{
		[Fact]
		public void It_should_generate_exactly_one_type_for_each_definition()
		{
            var compiler = new DefinitionCompiler();
            var assembly = compiler.Compile(
                @"DefinitionCompilerTests\alpha.boo",
                @"DefinitionCompilerTests\beta.boo",
                @"DefinitionCompilerTests\gamma.boo"
                );

            var generatedTypes = assembly.GetTypes();
            Assert.Equal(3, generatedTypes.Length);
		}
	}
}