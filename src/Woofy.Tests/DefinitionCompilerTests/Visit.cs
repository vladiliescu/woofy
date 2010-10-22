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
    public class Visit : BaseDefinitionCompilerTest
    {
        static Visit()
        {
            ContainerAccessor.RegisterComponents(new TestModule());
        }

        [Fact]
        public void Should_invoke_the_registered_visit_expression()
        {
            var expression = (CountingVisitExpression)ContainerAccessor.Resolve<IExpression>("visit");

            var assembly = Compile("visit.boo");

            var visit = (Definition)assembly.CreateInstance("_visit");
            visit.Run();

            Assert.True(0 < expression.ExecutionCount);
        }

        [Fact]
        public void Should_invoke_the_same_expression_each_time()
        {
            var expression = (CountingVisitExpression)ContainerAccessor.Resolve<IExpression>("visit");

            var assembly = Compile("visit.boo");

            var visit = (Definition)assembly.CreateInstance("_visit");
            visit.Run();

            Assert.Equal(3, expression.ExecutionCount);
        }

        [Fact]
        public void Should_pass_the_parameter_to_the_invoked_statement()
        {
            Assert.False(true);
        }
    }

    public class CountingVisitExpression : IExpression
    {
        public int ExecutionCount { get; private set; }

        public object Execute(Context context)
        {
            return ++ExecutionCount;
        }
    }

    public class TestModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CountingVisitExpression>()
                .Named<IExpression>("visit")
                .SingleInstance();
        }
    }
}