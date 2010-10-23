using System;
using Autofac;

namespace Woofy.Core.Infrastructure
{
	public static class ContainerAccessor
	{
		public static IContainer Container { get; private set; }

		public static void RegisterComponents(params Module[] additionalModules)
		{
			if (Container != null)
				throw new InvalidOperationException("The container has already been initialized.");

			var builder = new ContainerBuilder();
			builder.RegisterModule<DefaultComponentsModule>();

		    foreach (var module in additionalModules)
                builder.RegisterModule(module);

			Container = builder.Build();
		}
		
		public static T Resolve<T>()
		{
			return Container.Resolve<T>();
		}

        public static T Resolve<T>(string serviceName)
        {
            return Container.Resolve<T>(serviceName);
        }
	}
}