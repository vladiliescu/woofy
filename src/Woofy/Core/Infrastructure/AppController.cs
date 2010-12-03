using System;
using System.Collections.Generic;
using System.Threading;
using Autofac;
using System.Linq;

namespace Woofy.Core.Infrastructure
{
	public interface IAppController
	{
        void Execute<T>() where T : class, ICommand;
        void Execute<T>(T commandData) where T : class, ICommand;
		void Raise<T>() where T : class, IEvent;
        void Raise<T>(T eventData) where T : class, IEvent;
	}

	public class AppController : IAppController
	{
		private readonly ILifetimeScope container;

		public AppController(ILifetimeScope container)
		{
			this.container = container;
		}

        public void Execute<T>() where T : class, ICommand
		{
			Execute<T>(null);
		}

        public void Execute<T>(T commandData) where T : class, ICommand
		{
			var commandHandler = container.Resolve<ICommandHandler<T>>();
			commandHandler.Handle(commandData);
		}

        public void Raise<T>() where T : class, IEvent
		{
			Raise<T>(null);
		}

        public void Raise<T>(T eventData) where T : class, IEvent
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