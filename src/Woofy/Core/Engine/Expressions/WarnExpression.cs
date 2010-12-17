using System.Collections.Generic;
using Woofy.Flows.ApplicationLog;

namespace Woofy.Core.Engine.Expressions
{
    public class WarnExpression : BaseExpression
    {
        public WarnExpression(IAppLog appLog) : base(appLog)
        {
        }

        public override IEnumerable<object> Invoke(object argument, Context context)
        {
            Warn(context, (string)argument);
            return null;
        }

        protected override string ExpressionName
        {
            get { return Expressions.Warn; }
        }
    }
}