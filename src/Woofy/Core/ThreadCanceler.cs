using System;

namespace Woofy.Core
{
	public class ThreadCanceler
	{
		private readonly object @lock = new object();
		private bool cancelRequest;

		public bool IsCancellationRequested
		{
			get { lock (@lock) { return cancelRequest; } }
		}

		public void Cancel()
		{
			lock (@lock) { cancelRequest = true; }
		}

		public void ThrowIfCancellationRequested()
		{
			if (IsCancellationRequested)
				throw new OperationCanceledException();
		}
	}
}