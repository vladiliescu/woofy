using Autofac;

namespace Woofy.Core.Engine.Expressions
{
    public class ExpressionsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterExpression<VisitExpression>(Expressions.Visit);
            builder.RegisterExpression<GoToExpression>(Expressions.GoTo);
            builder.RegisterExpression<DownloadExpression>(Expressions.Download);
            builder.RegisterExpression<PeekExpression>(Expressions.Peek);
            builder.RegisterExpression<SleepExpression>(Expressions.Sleep);
			builder.RegisterExpression<MetaExpression>(Expressions.Meta);
            builder.RegisterExpression<WriteMetaToTextExpression>(Expressions.WriteMetaToText);
            builder.RegisterExpression<MatchExpression>(Expressions.Match);
            builder.RegisterExpression<WarnExpression>(Expressions.Warn);
            builder.RegisterExpression<LogExpression>(Expressions.Log);
        }
    }

    public static class BuilderExtensions
    {
        public static void RegisterExpression<T>(this ContainerBuilder builder, string expressionName)
            where T : IExpression
        {
            builder.RegisterType<T>().Named<IExpression>(expressionName).InstancePerDependency();
        }
    }
}
