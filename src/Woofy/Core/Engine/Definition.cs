using System;
using System.Collections.Generic;
using Woofy.Core.Infrastructure;

namespace Woofy.Core.Engine
{
#warning should be renamed to Worker, for clarity purposes
	public abstract class Definition
	{
        public abstract string Comic { get; }
        public abstract string StartAt { get; }

		protected abstract void RunImpl(Context context);

		/// <summary>
		/// Basically the definition's filename without the extension.
		/// </summary>
		public string Id { get; set; }

		protected Definition()
		{
			Id = GetType().Name.Substring(1);
		}

		public void Run()
		{
            var context = new Context(Id, Comic, new Uri(StartAt)); 
            
		    RunImpl(context);
		}

	    protected IEnumerable<object> InvokeExpression(string expressionName, object argument, Context context)
        {
            var expression = ContainerAccessor.Resolve<IExpression>(expressionName);
            return expression.Invoke(argument, context);
        }
	}
}