using Autofac;

namespace Woofy.Core.Engine.Expressions
{
    public class ExpressionsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<VisitExpression>().Named<IExpression>(Expressions.Visit).InstancePerDependency();
            builder.RegisterType<GoToExpression>().Named<IExpression>(Expressions.GoTo).InstancePerDependency();
            builder.RegisterType<DownloadExpression>().Named<IExpression>(Expressions.Download).InstancePerDependency();
            builder.RegisterType<PeekExpression>().Named<IExpression>(Expressions.Peek).InstancePerDependency();
            builder.RegisterType<SleepExpression>().Named<IExpression>(Expressions.Sleep).InstancePerDependency();
			builder.RegisterType<MetaExpression>().Named<IExpression>(Expressions.Meta).InstancePerDependency();
            builder.RegisterType<WriteMetaToTextExpression>().Named<IExpression>(Expressions.WriteMetaToText).InstancePerDependency();
            builder.RegisterType<MatchExpression>().Named<IExpression>(Expressions.Match).InstancePerDependency();
        }
    }
}
