using System.Linq;

namespace Woofy.Core.Engine
{
	public class DoWhile : IStatement
	{
	    public IExpression[] Condition { get; set; }
	    public IStatement[] Body { get; set; }

	    public DoWhile()
		{
		}

		public void Run(Context context)
		{
            //do
            //{
            //    Body.ForEach(statement => statement.Run(context));
            //}
            //while(ConditionIsFulfilled(context));
		}

	    private bool ConditionIsFulfilled(Context context)
	    {
	        foreach (var expressionResult in Condition.Select(expression => expression.Execute(context)))
	        {
                if (expressionResult == null)
                    return false;

                if (expressionResult is bool && (bool)expressionResult == false)
                    return false;
	        }

            return true;
	    }
	}
}