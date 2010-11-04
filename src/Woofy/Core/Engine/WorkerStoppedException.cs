using System;
using System.Runtime.Serialization;

namespace Woofy.Core.Engine
{
	[Serializable]
	public class WorkerStoppedException : Exception
	{
		public WorkerStoppedException()
		{
		}

		public WorkerStoppedException(string message) : base(message)
		{
		}

		public WorkerStoppedException(string message, Exception inner) : base(message, inner)
		{
		}

		protected WorkerStoppedException(
			SerializationInfo info,
			StreamingContext context) : base(info, context)
		{
		}
	}
}