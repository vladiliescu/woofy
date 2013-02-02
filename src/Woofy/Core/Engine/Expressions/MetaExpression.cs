using System.Collections.Generic;
using Woofy.Core.SystemProxies;
using Woofy.Flows.ApplicationLog;

namespace Woofy.Core.Engine.Expressions
{
    /// <summary>
    /// Used to retrieve metadata from a page, based on a given regular expression.
    /// </summary>
	public class MetaExpression : BaseWebExpression
	{
		private readonly IPageParser parser;

		public MetaExpression(IAppLog appLog, IWebClientProxy webClient, IPageParser parser) : base(appLog, webClient)
		{
			this.parser = parser;
		}

		public override IEnumerable<object> Invoke(object argument, Context context)
		{
			if (ContentIsEmpty(context))
				InitializeContent(context);
    
			var args = (string[])argument;
			var key = args[0];
			var regex = args[1];

			var content = parser.RetrieveContent(regex, context.PageContent);
			if (content.Length == 0)
			{
				Log(context, "WARNING: haven't found anything.");
				return null;
			}

			Log(context, "found {0}:{1}", key, content[0]);
			context.Metadata[key] = content[0];

			return null;
		}

		protected override string ExpressionName
		{
			get { return Expressions.Meta; }
		}
	}
}