using System;
using Boo.Lang.Compiler;
using Boo.Lang.Compiler.IO;
using Boo.Lang.Compiler.Pipelines;
using Rhino.DSL;
using Assembly = System.Reflection.Assembly;

namespace Woofy.Core.Engine
{
    public interface IDefinitionCompiler
    {
    }

    public class DefinitionCompiler : IDefinitionCompiler
    {
        public Assembly Compile(params string[] definitionFiles)
        {
            var parameters = new CompilerParameters
            {
                OutputType = CompilerOutputType.Library,
                Pipeline = new CompileToFile(),
                OutputAssembly = "Definitions.dll"
            };

            parameters.References.Add(Assembly.GetExecutingAssembly());

            foreach (var definitionFile in definitionFiles)
                parameters.Input.Add(new FileInput(definitionFile));

            parameters.Pipeline.Insert(1, new DefinitionClassCompilerStep());

            var compiler = new BooCompiler(parameters);
            var context = compiler.Run();

            if (context.Errors.Count > 0)
                throw new CompilerError(context.Errors.ToString(true));

            return context.GeneratedAssembly;
        }
    }
}