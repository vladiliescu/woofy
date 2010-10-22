using Autofac;

namespace Woofy.Core.Infrastructure
{
	public interface IApplicationController
	{
		void Execute<T>() where T : class;
		void Execute<T>(T commandData) where T : class;
	}

	public class ApplicationController : IApplicationController
	{
		private readonly IContainer container;

		public ApplicationController(IContainer container)
		{
			this.container = container;
		}

		public void Execute<T>()
			where T : class
		{
			Execute<T>(null);
		}

		public void Execute<T>(T commandData)
			where T : class
		{
			var command = container.ResolveOptional<ICommandHandler<T>>();
			if (command == null)
				return;

			command.Handle(commandData);
		}
	}
}