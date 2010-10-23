using System;
using System.Collections.Generic;
using System.Threading;
using Autofac;
using System.Linq;

namespace Woofy.Core.Infrastructure
{
	public interface IApplicationController
	{
		void Execute<T>() where T : class;
		void Execute<T>(T commandData) where T : class;
		void Raise<T>() where T : class;
		void Raise<T>(T eventData) where T : class;
	}

	public class ApplicationController : IApplicationController
	{
		private readonly ILifetimeScope container;

		public ApplicationController(ILifetimeScope container)
		{
			this.container = container;
		}

		public void Execute<T>() where T : class
		{
			Execute<T>(null);
		}

		public void Execute<T>(T commandData) where T : class
		{
			var commandHandler = container.Resolve<ICommandHandler<T>>();
			commandHandler.Handle(commandData);
		}

		public void Raise<T>() where T : class
		{
			Raise<T>(null);
		}

		public void Raise<T>(T eventData) where T : class
		{
			var eventHandlers = container.Resolve<IEnumerable<IEventHandler<T>>>();
			if (!eventHandlers.Any())
				throw new ArgumentException("No event handler exists for {0}.".FormatTo(typeof(T).Name, "eventData"));

			foreach (var handler in eventHandlers)
			{
				var eventHandler = handler;
				ThreadPool.QueueUserWorkItem(o => eventHandler.Handle(eventData));
			}
		}
	}
}