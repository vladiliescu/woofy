using System.Collections.Generic;
using Woofy.Flows.ApplicationLog;

namespace Woofy.Core.Engine.Expressions
{
    public abstract class BaseExpression : IExpression
    {
        public abstract IEnumerable<object> Invoke(object argument, Context context);
        protected abstract string ExpressionName { get; }

        private readonly IAppLog appLog;

        protected BaseExpression(IAppLog appLog)
        {
            this.appLog = appLog;
        }

        protected void Log(Context context, string messageFormat, params object[] args)
        {
            appLog.Send(new AppLogEntryAdded("{0}-{1}".FormatTo(context.Comic, ExpressionName), messageFormat.FormatTo(args)));
        }

        protected void Warn(Context context, string messageFormat, params object[] args)
        {
            Log(context, "WARNING: " + messageFormat, args);
        }
    }
}