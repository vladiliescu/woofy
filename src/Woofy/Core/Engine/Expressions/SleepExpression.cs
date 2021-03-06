using System.Collections.Generic;
using System.Threading;
using Woofy.Flows.ApplicationLog;

namespace Woofy.Core.Engine.Expressions
{
    /// <summary>
    /// Used to pause the download for two seconds.
    /// </summary>
    public class SleepExpression : BaseExpression
    {
        public SleepExpression(IAppLog appLog) : base(appLog)
        {
        }

        public override IEnumerable<object> Invoke(object argument, Context context)
        {
            Log(context, "sleeping for 2 seconds..");
            Thread.Sleep(2000);

            return null;
        }

        protected override string ExpressionName
        {
            get { return Expressions.Sleep; }
        }
    }
}