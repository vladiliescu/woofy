using System.Collections.Generic;
using Woofy.Flows.ApplicationLog;

namespace Woofy.Core.Engine.Expressions
{
    public class LogExpression : BaseExpression
    {
        public LogExpression(IAppLog appLog) : base(appLog)
        {
        }

        public override IEnumerable<object> Invoke(object argument, Context context)
        {
            Log(context, (string)argument);
            return null;
        }

        protected override string ExpressionName
        {
            get { return Expressions.Log; }
        }
    }
}