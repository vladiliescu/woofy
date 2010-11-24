using Autofac;

namespace Woofy.Core.Engine.Expressions
{
    public class ExpressionsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<VisitExpression>().Named<IExpression>("visit").InstancePerDependency();
            builder.RegisterType<DownloadExpression>().Named<IExpression>("download").InstancePerDependency();
            builder.RegisterType<SleepExpression>().Named<IExpression>("sleep").InstancePerDependency();
        }
    }
}
