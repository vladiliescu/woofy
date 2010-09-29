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

		[Fact]
		public void Generated_types_should_have_the_same_names_as_the_definition_filenames()
		{
			Assert.True(false);
		}

		[Fact]
		public void Compiled_definitions_should_return_their_names()
		{
			Assert.True(false);
		}

		[Fact]
		public void Compiled_definitions_should_return_their_start_addresses()
		{
			Assert.True(false);
		}
	}
}