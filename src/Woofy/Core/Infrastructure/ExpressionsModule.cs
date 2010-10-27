using Autofac;
using Woofy.Core.Engine;

namespace Woofy.Core.Infrastructure
{
    public class ExpressionsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<VisitExpression>().Named<IExpression>("visit");
            builder.RegisterType<DownloadExpression>().Named<IExpression>("download");
        }
    }
}
