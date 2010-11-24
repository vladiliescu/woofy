using System;
using System.Collections.Generic;
using System.Web.UI.WebControls.WebParts;
using Woofy.Core.ComicManagement;
using Woofy.Core.Infrastructure;
using Woofy.Core.SystemProxies;
using Woofy.Flows.ApplicationLog;

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
	    public Comic ComicInstance { get; set; }

		private readonly ThreadCanceler canceler = new ThreadCanceler();

	    protected Definition()
		{
			Id = GetType().Name.Substring(1);
		}

		public void Run()
		{
            canceler.AllowResuming();

            var context = new Context(Id, Comic, ComicInstance.CurrentPage ?? new Uri(StartAt));
            InitializeContent(context);

			try
			{
				RunImpl(context);
			}
			catch (OperationCanceledException)
			{
			}
		}

	    private void InitializeContent(Context context)
	    {
            var log = ContainerAccessor.Resolve<IAppLog>();
            var webClient = ContainerAccessor.Resolve<IWebClientProxy>();

            log.Send(new AppLogEntryAdded(context.Comic, "starting at {0}".FormatTo(context.CurrentAddress)));
            context.PageContent = webClient.DownloadString(context.CurrentAddress);
	    }

	    protected IEnumerable<object> InvokeExpression(string expressionName, object argument, Context context)
        {
			canceler.ThrowIfCancellationRequested();

	    	var expression = ContainerAccessor.Resolve<IExpression>(expressionName);
            return expression.Invoke(argument, context);
        }

		public void Stop()
		{
			canceler.Cancel();
		}
	}
}