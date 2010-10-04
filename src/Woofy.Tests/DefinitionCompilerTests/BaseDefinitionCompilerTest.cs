using System.Linq;
using System.Reflection;
using Woofy.Core.Engine;

namespace Woofy.Tests.DefinitionCompilerTests
{
    public class BaseDefinitionCompilerTest
    {
        protected Assembly Compile(params string[] definitionNames)
        {
            var compiler = new DefinitionCompiler();
            var assembly = compiler.Compile(definitionNames.Select(name => "DefinitionCompilerTests\\" + name).ToArray());
            return assembly;
        }

		protected Assembly CompileReferencingTests(params string[] definitionNames)
		{
			var compiler = new DefinitionCompiler();
			var assembly = compiler.Compile(new[] { Assembly.GetExecutingAssembly() }, definitionNames.Select(name => "DefinitionCompilerTests\\" + name).ToArray());
			return assembly;
		}
    }
}