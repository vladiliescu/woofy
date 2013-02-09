using System;
using System.Threading;

namespace Woofy.Core
{
	public interface IUiThread
	{
		void Send(Action action);
	}

	public class UiThread : IUiThread
	{
		private readonly SynchronizationContext synchContext;

		public UiThread(SynchronizationContext synchContext)
		{
			this.synchContext = synchContext;
		}

		public void Send(Action action)
		{
			synchContext.Send(o => action(), null);
		}
	}
}