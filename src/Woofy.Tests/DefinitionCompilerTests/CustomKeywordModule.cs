using Autofac;
using Woofy.Core.Engine;

namespace Woofy.Tests.DefinitionCompilerTests
{
    public class CustomKeywordModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FooExpression>()
                .Named<IExpression>("foo")
                .SingleInstance();

            builder.RegisterType<BarExpression>()
                .Named<IExpression>("bar")
                .SingleInstance();
        }
    }
}