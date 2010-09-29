using System;
using Boo.Lang.Compiler;
using Boo.Lang.Compiler.Ast;
using Boo.Lang.Compiler.IO;
using Boo.Lang.Compiler.Pipelines;
using Boo.Lang.Compiler.Steps;
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

            parameters.Pipeline.Insert(1, new ComicBaseClassCompilerStep());

            var compiler = new BooCompiler(parameters);

            var context = compiler.Run();

            if (context.Errors.Count > 0)
                throw new CompilerError(context.Errors.ToString(true));

            return context.GeneratedAssembly;
        }
    }

    public class ComicBaseClassCompilerStep : AbstractCompilerStep
    {
        Random random = new Random();

        public override void Run()
        {
           // if (Context.References.Contains(baseClass.Assembly) == false)
           //     Context.Parameters.References.Add(baseClass.Assembly);

            foreach (Module module in CompileUnit.Modules)
            {
                //foreach (string ns in namespaces)
                //{
                //    module.Imports.Add(new Import(module.LexicalInfo, ns));
                //}

                var definition = new ClassDefinition();
                definition.Name = "C" + random.Next();
                //definition.BaseTypes.Add(new SimpleTypeReference(baseClass.FullName));

                //GenerateConstructors(definition);

                // This is called before the module.Globals is set to a new block so that derived classes may retrieve the
                // block from the module.
                //ExtendBaseClass(module, definition);

                module.Globals = new Block();
                module.Members.Add(definition);
            }
        }
    }
}