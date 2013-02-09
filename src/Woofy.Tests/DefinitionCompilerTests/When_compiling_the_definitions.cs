using Xunit;

namespace Woofy.Tests.DefinitionCompilerTests
{
	public class When_compiling_the_definitions : BaseDefinitionCompilerTest
	{
		[Fact]
		public void It_should_generate_exactly_one_type_for_each_definition()
		{
            var assembly = Compile("alpha.boo", "beta.boo", "gamma.boo");

            var generatedTypes = assembly.GetTypes();
            Assert.Equal(3, generatedTypes.Length);
		}
	}
}