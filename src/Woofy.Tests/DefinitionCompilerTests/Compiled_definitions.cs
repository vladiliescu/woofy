using Woofy.Core.Engine;
using Xunit;
using System.Linq;

namespace Woofy.Tests.DefinitionCompilerTests
{
	public class Compiled_definitions : BaseDefinitionCompilerTest
	{
	    [Fact]
		public void Should_be_named_after_their_filenames_and_prefixed_with_underscores()
		{
			var assembly = Compile("alpha.boo", "beta.boo", "gamma.boo");
			
			var types = assembly.GetTypes();
			var expectedNames = new[] { "_alpha", "_beta", "_gamma" };

			Assert.Equal(
				3, types.Select(type => type.Name).Intersect(expectedNames).Count()
			);
		}

		[Fact]
		public void Should_inherit_the_base_class()
		{
			var assembly = Compile("alpha.boo");
			var alphaType = assembly.GetType("_alpha");

			Assert.True(alphaType.IsSubclassOf(typeof(Definition)));
		}

        [Fact]
        public void Should_fill_the_comic_property()
        {
            var assembly = Compile("alpha.boo");
            var alpha = (Definition)assembly.CreateInstance("_alpha");

            Assert.Equal("alpha comic", alpha.Comic);
        }

        [Fact]
        public void Should_fill_the_start_at_property()
        {
            var assembly = Compile("alpha.boo");
            var alpha = (Definition)assembly.CreateInstance("_alpha");

            Assert.Equal("http://example.com/alpha", alpha.StartAt);
        }
	}
}