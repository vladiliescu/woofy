using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Woofy.Core;
using Woofy.Core.Engine;
using Woofy.Core.SystemProxies;

namespace Woofy.Tests.DefinitionCompilerTests
{
    public class BaseDefinitionCompilerTest
    {
        private readonly ObjectMother factory = new ObjectMother();
        private readonly DefinitionCompiler compiler;
        
        public BaseDefinitionCompilerTest()
        {
            factory.AppSettings.Setup(x => x.DefinitionsAssemblyPath).Returns(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Definitions.dll"));
            compiler = factory.CreateDefinitionCompiler();
        }

        protected Assembly Compile(params string[] definitionNames)
        {
            var assembly = compiler.Compile(definitionNames.Select(name => "DefinitionCompilerTests\\" + name).ToArray());
            return assembly;
        }

		protected Assembly CompileReferencingTests(params string[] definitionNames)
		{
			var assembly = compiler.Compile(new[] { Assembly.GetExecutingAssembly() }, definitionNames.Select(name => "DefinitionCompilerTests\\" + name).ToArray());
			return assembly;
		}
    }
}