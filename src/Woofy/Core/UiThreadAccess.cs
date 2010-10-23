using System;
using System.Threading;

namespace Woofy.Core
{
	public interface IUiThreadAccess
	{
		void Send(Action action);
	}

	public class UiThreadAccess : IUiThreadAccess
	{
		private readonly SynchronizationContext synchContext;

		public UiThreadAccess(SynchronizationContext synchContext)
		{
			this.synchContext = synchContext;
		}

		public void Send(Action action)
		{
			synchContext.Send(o => action(), null);
		}
	}
}