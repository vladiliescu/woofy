using System;

namespace Woofy.Core
{
	public class ThreadCanceler
	{
		private readonly object @lock = new object();
		private bool cancelRequest;
		public bool CancelRequest
		{
			get { lock (@lock) { return cancelRequest; } }
            set { lock (@lock) { cancelRequest = value; } }
		}

		public void Cancel()
		{
            CancelRequest = true;
		}

        public void AllowResuming()
        {
            CancelRequest = false;
        }

	    public void ThrowIfCancellationRequested()
		{
            if (CancelRequest)
				throw new OperationCanceledException();
		}
	}
}