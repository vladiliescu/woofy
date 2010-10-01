using System.Reflection;
using Autofac;
using Boo.Lang.Compiler;
using Boo.Lang.Compiler.IO;
using Boo.Lang.Compiler.Pipelines;
using Woofy.Core.Engine;
using Woofy.Core.Infrastructure;
using Xunit;
using Module = Autofac.Module;

namespace Woofy.Tests.DefinitionCompilerTests
{
    public class Visit_macro : BaseDefinitionCompilerTest
    {
        [Fact]
        public void Should_invoke_the_registered_visit_statement()
        {
            ContainerAccessor.RegisterComponents(new DummyModule());
            var expression = ContainerAccessor.Resolve<IExpression>("visit");

            var assembly = Compile("visit.boo");

            var visit = (Definition)assembly.CreateInstance("_visit");
            visit.Run();

            Assert.True(((DummyVisitExpression)expression).HasBeenInvoked);
        }

        [Fact]
        public void Should_pass_the_parameter_to_the_invoked_statement()
        {
            var code = @"
import Boo.Lang.PatternMatching

macro hello:
	print [|
		a = Woofy.Core.DownloadOutcome.NoStripMatchesRuleBroken
	|].ToString()	

hello ""world""
";

            var parameters = new CompilerParameters
                                 {
                OutputType = CompilerOutputType.ConsoleApplication,
                Pipeline = new CompileToFile(),
                OutputAssembly = "CompiledBooScript.dll",
                Input = { new StringInput("integration.boo", code) }
            };
            parameters.References.Add(Assembly.GetAssembly(typeof(ContainerAccessor)));

            var compiler = new BooCompiler(parameters);


            var context = compiler.Run();

            if (context.Errors.Count > 0)
                throw new CompilerError("");
        }
    }

    public class DummyVisitExpression : IExpression
    {
        public bool HasBeenInvoked { get; private set; }

        public object Execute(Context context)
        {
            HasBeenInvoked = true;

            return null;
        }
    }

    public class DummyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DummyVisitExpression>()
                .Named<IExpression>("visit")
                .SingleInstance();
        }
    }
}